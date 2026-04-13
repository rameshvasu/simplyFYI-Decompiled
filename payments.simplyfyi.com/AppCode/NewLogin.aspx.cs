using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RameshInnovation.VoiShare.OfficeLive;

namespace RameshInnovation.SimplyfyiExpress
{
    public partial class NewLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
{
   
    HtmlGenericControl val = (HtmlGenericControl)Utilities.FindControlIterative(((Control)this).Controls, "accountlockedmessage");
    ((HtmlControl)val).Style.Add("display", "none");
    //if (!((Page)this).IsPostBack)
    //{
    //    HideHomeAndLogoutMenus(((Page)this).Master, IsVisible: false);
    //}

if (!Page.IsPostBack)
    {
        HideHomeAndLogoutMenus(Page.Master, IsVisible: false);
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
			ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("main");

			TextBox txtPassword = (TextBox)cph.FindControl("txtPassword");
			TextBox txtEmail = (TextBox)cph.FindControl("txtEmail");

            string email = txtEmail.Text;
            string password = txtPassword.Text;

            LoginHelper.AuthenticateUser(email, password, HttpContext.Current);
        }
    }
}