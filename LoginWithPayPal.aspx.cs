using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using RameshInnovation.VoiShare.OfficeLive;

namespace RameshInnovation.SimplyfyiExpress
{
    public partial class LoginWithPayPal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl AccountLockedMessage = (HtmlGenericControl)Utilities.FindControlIterative(this.Controls, "accountlockedmessage");
            AccountLockedMessage.Style.Add("display", "none");

            if (!IsPostBack)
            {
                // Hide Home and Logout links                
                HideHomeAndLogoutMenus(this.Master, false);
            }
        }

        private void HideHomeAndLogoutMenus(MasterPage Master, bool IsVisible)
        {
            for (int i = 0; i < Master.Controls.Count; i++)
            {
                Control ctrl = Master.Controls[i].FindControl("header");
                string TypeString = ctrl.GetType().ToString();
                if (TypeString.Equals("System.Web.UI.WebControls.ContentPlaceHolder"))
                {
                    System.Web.UI.WebControls.ContentPlaceHolder HeaderPlaceHolder = (System.Web.UI.WebControls.ContentPlaceHolder)ctrl;

                    HyperLink HomeHyperLink = (HyperLink)HeaderPlaceHolder.Controls[i].FindControl("HomeHyperLink");
                    //HyperLink LogoutHyperLink = (HyperLink)HeaderPlaceHolder.Controls[i].FindControl("LogoutHyperLink");
                    LinkButton LogoutLink = (LinkButton)HeaderPlaceHolder.Controls[i].FindControl("LogoutLink");
                    HtmlGenericControl DropDownMenuContainer = (HtmlGenericControl)HeaderPlaceHolder.Controls[i].FindControl("dropdownmenucontainer");

                    HomeHyperLink.Visible = IsVisible;
                    //LogoutHyperLink.Visible = IsVisible;
                    LogoutLink.Visible = IsVisible;
                    DropDownMenuContainer.Visible = IsVisible;
                    break;

                }
            }
        }
    }
}