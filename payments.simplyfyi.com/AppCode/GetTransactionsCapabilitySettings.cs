using System;
using System.Web.UI;
using Utils;
using System.Web.Script.Serialization;
using System.Web.Script.Serialization; // Required for JavaScriptSerializer
using
System.Collections.Generic;    // Required for Dictionary<string, object>

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;

using RameshInnovation.SimplyfyiExpress;

namespace RameshInnovation.SimplyfyiExpress.Controls
{

    public class GetTransactionsCapabilitySettings : UserControl
    {
        protected UpdatePanel UpdatePanel1;
        protected Timer Timer1;
        protected UpdateProgress Update1;

        //Add this line to declare the label and checkbox:
        protected System.Web.UI.WebControls.Label lblResult;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox cbGetTransactions;
        protected System.Web.UI.HtmlControls.HtmlGenericControl linkedStatus;
        protected System.Web.UI.HtmlControls.HtmlGenericControl linkedStatusTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionValidator.ValidateExpressSession(this.Context);
            if (this.IsPostBack)
                return;

	    EngageUser EngageUser = (EngageUser)this.Context.Session["EngageUser"];
	    string merchantEmail = EngageUser.Email;
            
	    //string url = "https://simplyfyi-pp-onboard.ngrok.io/api/v1/merchant/soft-link?primary_email=" + merchantEmail;
	    string url = "https://simplyfyi-onboardmanager.azurewebsites.net/api/v1/merchant/soft-link?code=wIvmoATKFPd_0c7I8zkwaX-So_DGzJIrCZyHIeN3r_QgAzFuq-DNZg==&primary_email=" + merchantEmail;

            string jsonResponse = LegacyHttpHelper.Get(url);

            // Instantiate the built-in serializer
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            // Deserialize into a Dictionary
            Dictionary<string, object> data = serializer.Deserialize<Dictionary<string, object>>(jsonResponse);

            // Access the boolean value
            bool isSoftLinked = (bool)data["soft_linked"];

            this.lblResult.Text = "SOFT LINKED:" + isSoftLinked.ToString();

            // Update the checkbox state based on the API response
            this.cbGetTransactions.Checked = isSoftLinked;

            this.linkedStatus.InnerText = "simplyFYI is " + (isSoftLinked ? "LINKED with PayPal." : "NOT LINKED with PayPal.");
            this.linkedStatusTitle.InnerText = isSoftLinked ? "simplyFYI is LINKED with PayPal" : "simplyFYI is NOT LINKED with PayPal";

        }

        protected void cbGetTransactions_ServerChange(object sender, EventArgs e)

        {
            EngageUser EngageUser = (EngageUser)this.Context.Session["EngageUser"];
	    string merchantEmail = EngageUser.Email;

            // 1. Get the new checkbox state (true/false)
            bool isEnabled = this.cbGetTransactions.Checked;

            // 2. Construct your API URL (including query parameters if needed)
            //string url = "https://simplyfyi-pp-onboard.ngrok.io/api/v1/merchant/soft-link?primary_email=" +  merchantEmail;
	    string url = "https://simplyfyi-onboardmanager.azurewebsites.net/api/v1/merchant/soft-link?code=VwkSPnK2J8PTgeVmljCd6CL320uUr_zjN1f8FIyG1UJ_AzFuJIS-Yw==&primary_email=" +  merchantEmail;

            try
            {
                // 3. Create a JSON payload representing the new state (adjust property name as required by your API)
                string jsonPayload = "{\"soft_linked\":" + isEnabled.ToString().ToLower() + ",";

                jsonPayload += "\"primary_email\":\"" + merchantEmail + "\"}";

                // 3.Make the PUT request using LegacyHttpHelper
                string jsonResponse = LegacyHttpHelper.Put(url, jsonPayload, "application/json");
        
                // 4. Parse the response (just like your GET call)
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                Dictionary<string, object> data = serializer.Deserialize<Dictionary<string, object>>(jsonResponse);

                bool isSoftLinked = (bool)data["soft_linked"];
                string returnedPrimaryEmail = (string)data["primary_email"];

                // 5. Update UI controls with the result
                string updateStatus = "Update successful at ";

                if (string.IsNullOrEmpty(returnedPrimaryEmail))
                {
                    updateStatus = "Update failed at ";
                }

                this.lblResult.Text = updateStatus + DateTime.Now.ToString() + ".";
                this.cbGetTransactions.Checked = isSoftLinked;

                if (this.linkedStatus != null)
                {
                    this.linkedStatus.InnerText = "simplyFYI is " + (isSoftLinked ? "LINKED with PayPal." : "NOT LINKED with PayPal.");
		    this.linkedStatusTitle.InnerText = isSoftLinked ? "simplyFYI is LINKED with PayPal" : "simplyFYI is NOT LINKED with PayPal";
                }
            }

            catch (Exception ex)
            {
                this.lblResult.Text = "Error updating status: " + ex.Message;

                // Revert checkbox state on failure if desired
                this.cbGetTransactions.Checked = !isEnabled;
            }

        }


    }

}


