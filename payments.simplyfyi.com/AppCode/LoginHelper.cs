using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.IO;

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;

namespace RameshInnovation.SimplyfyiExpress
{
    public class LoginHelper
    {	
	const string SUBSCRIPTION = "simplyfyiPayments";

        public static bool ValidateUser(string email, string password)
        {            
		bool ValidLogin = false;
		FYIDataAccess DAL = new FYIDataAccess();             
 		if (DAL.IsUserRegistered(email, SUBSCRIPTION))
		{
			ValidLogin = ValidatePassword(email, password);
		}
		return ValidLogin;			
        }

        public static void AuthenticateUser(string email, string password, HttpContext context)
        {
            if (ValidateUser(email, password))
            {
                EngageUser EngageUser = new EngageUser();
                EngageUser.Email = email;
                EngageUser.FirstName = string.Empty;
                EngageUser.LastName = string.Empty;
                context.Session["EngageUser"] = EngageUser;
				
				DoPostAuthActions(EngageUser, context);
				
            }
			else
			{
			    context.Response.Redirect("~/Login/NewLogin.aspx");
			}
		}

	private static void DoPostAuthActions(EngageUser EngageUser, HttpContext context)
	{
    string empty = string.Empty;
    if (EngageUser != null)
    {
        context.Session["EngageUser"] = EngageUser;
        FYIDataAccess fYIDataAccess = new FYIDataAccess();
        if (!fYIDataAccess.IsUserRegistered(EngageUser.Email, "SimplyfyiPayments"))
        {
            empty = context.Request["clid"];
            string text = "./Registration.aspx?action=1";
            if (!string.IsNullOrEmpty(empty))
            {
                string text2 = text;
                text = text2 + "&clid=" + empty + "&email=" + EngageUser.Email;
            }

            context.Response.Redirect(text);
            return;
        }

        RegistrationData registrationData = fYIDataAccess.GetRegistrationData(EngageUser.Email, "SimplyfyiPayments");
        registrationData = FixupPhoneNumberFormat(EngageUser.Email, registrationData);
        if (fYIDataAccess.IsParentAccount(registrationData.AccountID))
        {
            context.Session["RegistrationData"] = registrationData;
            context.Session["SubAccountRegistrationData"] = null;
        }
        else if (registrationData.Enabled)
        {
            RegistrationData registrationData2 = fYIDataAccess.GetRegistrationData(registrationData.ParentAccountID);
            context.Session["RegistrationData"] = registrationData2;
            context.Session["SubAccountRegistrationData"] = registrationData;
        }
        else
        {
            context.Response.Redirect("./LoginNotAllowed.aspx");
        }

        context.Session["AccountID"] = registrationData.AccountID;
        context.Session["ShowFooter"] = true;
        if (fYIDataAccess.GetAccountOptions(registrationData.AccountID).FooterText.StartsWith("showfooter=false;"))
        {
            context.Session["ShowFooter"] = false;
        }

        if (GetAccountBalance(context) > 0)
        {
            context.Response.Redirect("~/Start.aspx");
        }
        else if (!IsAutoPayOnForAccount(registrationData.WindowsLiveClientID.Trim()))
        {
            context.Session["TopupPageReturnUrl"] = "~/Start.aspx";
            context.Response.Redirect("~/TopupNow.aspx");
        }
    }
    else
    {
        WriteToLog("Login failed for: " + EngageUser.Email, context);
        context.Response.Redirect("~/Error.aspx?autherr=1");
    }
}

private static RegistrationData FixupPhoneNumberFormat(string UserEmail, RegistrationData RegData)
{
    FYIDataAccess fYIDataAccess = new FYIDataAccess();
    RegData.Phone = Utilities.CleansePhoneNumberToAllNumbers(RegData.Phone.Trim());
    fYIDataAccess.WriteRegistrationDataToDB(RegData);
    RegData = fYIDataAccess.GetRegistrationData(UserEmail, "SimplyfyiPayments");
    return RegData;
}

private static int GetAccountBalance(HttpContext context)
{
    FYIDataAccess fYIDataAccess = new FYIDataAccess();
    RegistrationData registrationData = (RegistrationData)context.Session["RegistrationData"];
    int parentAccountID = registrationData.ParentAccountID;
    int accountID = registrationData.AccountID;
    int num = 0;
    return fYIDataAccess.GetUsageInventoryBalanceByAccountID(accountID);
}

private static bool IsAutoPayOnForAccount(string WLID)
{
    bool result = false;
    FYIDataAccess fYIDataAccess = new FYIDataAccess();
    DataSet payPalBillingAgreementByCLID = fYIDataAccess.GetPayPalBillingAgreementByCLID(WLID);
    if (payPalBillingAgreementByCLID.Tables.Count > 0 && payPalBillingAgreementByCLID.Tables[0].Rows.Count > 0)
    {
        result = true;
    }

    return result;
}

private static void WriteToLog(string Message, HttpContext context)
{
    FileStream fileStream = null;
    StreamWriter streamWriter = null;
    string path = context.Server.MapPath("~/App_Data/Log/LogFile.txt");
    fileStream = File.Open(path, FileMode.Append);
    streamWriter = new StreamWriter(fileStream);
    streamWriter.WriteLine("\r\n[" + DateTime.Now.ToString() + "]" + Message);
    streamWriter.Close();
}

private static bool ValidatePassword(string email, string password)
{
	bool IsValid = false;
 	if(email == "ramesh@rameshinnovation.com" && password == "6GMYtLyM") IsValid = true;
 	else if(email == "powertoperuse@gmail.com" && password == "HehAQQSU") IsValid = true;
	else if(email == "vladislav.boz@gmail.com" && password == "QQSUHehA") IsValid = true;
	else if(email == "kv@r12c.org" && password == "rgspCY0F") IsValid = true;
	else if(email == "accounting@soprissun.com" && password == "uTEQPtCR") IsValid = true;


	return IsValid;
}

    }
}