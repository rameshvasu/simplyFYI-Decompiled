using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace RameshInnovation.SimplyfyiExpress
{
    public partial class RegisterMerchant : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {

            HtmlGenericControl val = (HtmlGenericControl)Utilities.FindControlIterative(((Control)this).Controls, "accountlockedmessage");
            ((HtmlControl)val).Style.Add("display", "none");

            if (!Page.IsPostBack)
            {
                // On redirect from PayPal onboarding, extract the email from the query string and store it in ViewState 
		HttpContext context = HttpContext.Current;          
                string email = Request.QueryString["email"];
                string tracking_id = context.Request["tracking_id"]; // use this to check origin of this request for security - TBD

		if(string.IsNullOrEmpty(tracking_id))
		{
		    tracking_id = context.Request["clid"];
		}
		
                EngageUser EngageUser = new EngageUser();
                EngageUser.Email = email;
                EngageUser.Provider = "PayPal";
                EngageUser.FirstName = string.Empty;
                EngageUser.LastName = string.Empty;
                context.Session["EngageUser"] = EngageUser;
                
                FYIDataAccess fYIDataAccess = new FYIDataAccess();
		
		if(fYIDataAccess.GetRegistrationData(tracking_id) != null)
		{
                    PPLoginHelper.GotoAppMainPage(EngageUser, context);
                }

                else
                {
                    //-----------------------------------------------------------------------------------------------
                    // Create the basic registration record before redirecting to the registration page.
                    // This will allow the user to complete the registration process.
                    //-----------------------------------------------------------------------------------------------
                    RegistrationData regData = new RegistrationData();
                    regData.Email = email;
		    regData.FullName = string.Empty;
		    regData.Phone = string.Empty;
		    regData.BusinessName = string.Empty;
		    regData.Address1 = string.Empty;
		    regData.Address2 = string.Empty;
		    regData.City = string.Empty;
		    regData.State = string.Empty;
		    regData.ZIP = string.Empty;
		    regData.Country = string.Empty;
		    regData.UserTimeZoneCode = string.Empty;

                    regData.WindowsLiveClientID = tracking_id; // This will get overwritten with a Guid during merchant registration.	    
                    regData.ParentAccountID = 999999;
                    regData.Subscription = "simplyfyiPayments";
                    regData.Enabled = true;
		    regData.WindowsLiveUserID = email;

                    fYIDataAccess.WriteRegistrationDataToDB(regData);
                    regData = fYIDataAccess.GetRegistrationData(tracking_id);
                    this.Session["AccountID"] = regData.AccountID;  
 
                    this.Session["CLID"] = tracking_id; // This will get overwritten with a Guid during merchant registration.	

		    // Set permissions granted flag and set merchant email as PayPal primary email 
		    fYIDataAccess.WritePayPalPrimaryEmailAddress(tracking_id, email, true);                                    
                }
            }
        }

        private void HideHomeAndLogoutMenus(MasterPage Master, bool IsVisible)
        {

            for (int i = 0; i < ((Control)Master).Controls.Count; i++)
            {
                Control val = ((Control)Master).Controls[i].FindControl("header");
                string text = ((object)val).GetType().ToString();
                if (text.Equals("System.Web.UI.WebControls.ContentPlaceHolder"))
                {
                    ContentPlaceHolder val2 = (ContentPlaceHolder)val;
                    HyperLink val3 = (HyperLink)((Control)val2).Controls[i].FindControl("HomeHyperLink");
                    LinkButton val4 = (LinkButton)((Control)val2).Controls[i].FindControl("LogoutLink");
                    HtmlGenericControl val5 = (HtmlGenericControl)((Control)val2).Controls[i].FindControl("dropdownmenucontainer");
                    ((Control)val3).Visible = IsVisible;
                    ((Control)val4).Visible = IsVisible;
                    ((Control)val5).Visible = IsVisible;
                    break;
                }
            }
        }

        protected void btnRegisterMerchant_Click(object sender, EventArgs e)
        {
            string email = Request.QueryString["email"];
            //string tracking_id = Request.QueryString["tracking_id"];
	    HttpContext context = HttpContext.Current;

            // Redirect to the registration page with the email and action=1 in the query string. DO not pass clid.
            string regUrl = "~/Registration.aspx?email=" + email  + "&action=1";
            context.Response.Redirect(regUrl);
        }
    }
}