using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using RameshInnovation.VoiShare.OfficeLive.Types;
using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using System.Drawing;

namespace RameshInnovation.SimplyfyiExpress
{
    public partial class SimplyfyiExpressMasterPageWide : System.Web.UI.MasterPage
    {
        private void DisableAdapterForMenuControl()
        {
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }
        }

        private int GetAccountBalance()
        {
            FYIDataAccess DAL = new FYIDataAccess();

            RegistrationData RegData = (RegistrationData)Session["RegistrationData"];

            int ParentAccountID = RegData.ParentAccountID;
            int AccountID = RegData.AccountID;
            int Balance = 0;

            Balance = DAL.GetUsageInventoryBalanceByAccountID(AccountID);

            return Balance;
        }

        private bool IsAccountEnabled()
        {
            FYIDataAccess DAL = new FYIDataAccess();

            RegistrationData RegData = (RegistrationData)Session["RegistrationData"];
            return RegData.Enabled;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Utils.SessionValidator.ValidateExpressSession(Context);

            DisableAdapterForMenuControl();

            if (!IsPostBack)
            {
                if (Session.Count > 0)
                {                    
                    Label WelcomeUser = (Label)Utilities.FindControlIterative(this.Controls, "lblWelcomeUser");
                    WelcomeUser.Text = string.Empty;

                    HtmlGenericControl TagLine = (HtmlGenericControl)Utilities.FindControlIterative(this.Controls, "taglinecontent");
                    HtmlGenericControl AccountLockedMessage = (HtmlGenericControl)Utilities.FindControlIterative(this.Controls, "accountlockedmessage");

                    RegistrationData RegData = (RegistrationData)Session["RegistrationData"];
                    if (RegData != null && RegData.BusinessName != null)
                    {                        
                        TagLine.InnerText = RegData.BusinessName.Trim();
                        AccountLockedMessage.InnerText = string.Empty;
                        AccountLockedMessage.Style.Add("display", "");
                        
                        int AccountBalance = GetAccountBalance();
                        Session["TopupPageReturnUrl"] = "./Start.aspx";
                                                
                        if (AccountBalance < 0 && !IsAccountEnabled())
                        {
                            AccountLockedMessage.InnerHtml = "You do not have enough UNITS to send messages. <a href='./TopupNow.aspx'>Buy UNITS now</a> to resume sending messages.";
                        }
                        else if (!IsAccountEnabled())
                        {
                            AccountLockedMessage.InnerHtml = "Your messages have been temporarily blocked. Contact support@simplyfyi.com immediately for details.";
                        }
                        else if (AccountBalance < 0)
                        {
                            AccountLockedMessage.InnerHtml = "You do not have enough UNITS to send messages. <a href='./Topup.aspx'>Buy UNITS now</a> to prevent messages from being blocked.";
                        }   
                        else
                        {
                            AccountLockedMessage.InnerHtml = string.Empty;
                            AccountLockedMessage.Style.Add("display", "none");
                        }

                        if (Session["SubAccountRegistrationData"] != null)
                        {
                            RegistrationData SubAccountRegistrationData = (RegistrationData)Session["SubAccountRegistrationData"];
                            if (SubAccountRegistrationData.FullName != null && SubAccountRegistrationData.FullName.Trim().Length > 0)
                            {
                                WelcomeUser.Text = "Hello " + SubAccountRegistrationData.FullName.Trim();
                                WelcomeUser.Visible = true;
                            }
                            else
                            {
                                WelcomeUser.Visible = false;
                            }
                        }
                        else
                        {
                            if (RegData.FullName != null && RegData.FullName.Trim().Length > 0)
                            {
                                WelcomeUser.Text = "Hello " + RegData.FullName.Trim();
                                WelcomeUser.Visible = true;
                            }
                            else
                            {
                                WelcomeUser.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        protected void LogoutLink_Click(object sender, EventArgs e)
        {
            //string js = Utils.SessionValidator.GetLogoutJavascript();
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "logout", js);
            Response.Redirect("~/Logout2.aspx");
        }
        
        protected void HomeLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Start.aspx");
        }
    } 
}
