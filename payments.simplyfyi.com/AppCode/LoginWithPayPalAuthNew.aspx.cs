using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json.Linq;
using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;



namespace RameshInnovation.SimplyfyiExpress
{

public class LoginWithPayPalAuthNew : Page
{
    private const string CLIENTID = "AdNrNj7z4xXUGyToUbxNvFL6PpaH9GEFQuE0pGFICIqIDiBALR9TeUohWCkEVX5ZrBpVCfUMCCbnGoJy";

    private const string SECRET = "EBe4NEKWorc7ABybmcXdMgmSXaNl6IGGoPE7LMYCauHfIYhpRf9VRxq9cSUy71Fq4v_S_dhupCkSIwdF";

    protected HtmlHead Head1;

    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        string empty = string.Empty;
        EngageUser engageUser = null;

WriteToLog("In LoginWithPayPalAuthNew......");

        if (((Page)this).IsPostBack)
        {
            return;
        }

        byte[] array = ((Page)this).Request.BinaryRead(((Page)this).Request.TotalBytes);
        string text = BitConverter.ToString(array);
        string authCode = ((Page)this).Request.QueryString.Get("code");
        string payPalApiAccessToken = GetPayPalApiAccessToken(authCode);
        if (!string.IsNullOrEmpty(payPalApiAccessToken))
        {
            WriteToLog("Success: GetPayPalApiAccessToken().");
            JObject val = JObject.Parse(payPalApiAccessToken);
            string accessToken = ((object)val["access_token"]).ToString();
            payPalApiAccessToken = GetUserInfo(accessToken);
            if (!string.IsNullOrEmpty(payPalApiAccessToken))
            {
                WriteToLog("Success: GetUserInfo().");
                JObject val2 = JObject.Parse(payPalApiAccessToken);
                string empty2 = string.Empty;
                if (val2["emails"] != null)
                {
                    foreach (JObject item in (IEnumerable<JToken>)val2["emails"])
                    {
                        JObject val3 = item;
                        if (Convert.ToBoolean(val3["primary"]))
                        {
                            empty2 = ((object)val3["value"]).ToString();
                            WriteToLog("User primary email: " + empty2);
                            engageUser = new EngageUser();
                            engageUser.Email = empty2;
                            engageUser.FirstName = string.Empty;
                            engageUser.LastName = string.Empty;
                            break;
                        }
                    }
                }

                DoPostAuthActions(engageUser);
            }
            else
            {
                WriteToLog("Failed: GetUserInfo().");
            }
        }
        else
        {
            WriteToLog("Failed: GetPayPalApiAccessToken().");
        }
    }

    private void DoPostAuthActions(EngageUser EngageUser)
    {
        string empty = string.Empty;
        if (EngageUser != null)
        {
            ((Page)this).Session["EngageUser"] = EngageUser;
            FYIDataAccess fYIDataAccess = new FYIDataAccess();
            if (!fYIDataAccess.IsUserRegistered(EngageUser.Email, "SimplyfyiPayments"))
            {
                empty = ((Page)this).Request["clid"];
                string text = "./Registration.aspx?action=1";
                if (!string.IsNullOrEmpty(empty))
                {
                    string text2 = text;
                    text = text2 + "&clid=" + empty + "&email=" + EngageUser.Email;
                }

                ((Page)this).Response.Redirect(text);
                return;
            }

            RegistrationData registrationData = fYIDataAccess.GetRegistrationData(EngageUser.Email, "SimplyfyiPayments");
            registrationData = FixupPhoneNumberFormat(EngageUser.Email, registrationData);
            if (fYIDataAccess.IsParentAccount(registrationData.AccountID))
            {
                ((Page)this).Session["RegistrationData"] = registrationData;
                ((Page)this).Session["SubAccountRegistrationData"] = null;
            }
            else if (registrationData.Enabled)
            {
                RegistrationData registrationData2 = fYIDataAccess.GetRegistrationData(registrationData.ParentAccountID);
                ((Page)this).Session["RegistrationData"] = registrationData2;
                ((Page)this).Session["SubAccountRegistrationData"] = registrationData;
            }
            else
            {
                ((Page)this).Response.Redirect("./LoginNotAllowed.aspx");
            }

            ((Page)this).Session["AccountID"] = registrationData.AccountID;
            ((Page)this).Session["ShowFooter"] = true;
            if (fYIDataAccess.GetAccountOptions(registrationData.AccountID).FooterText.StartsWith("showfooter=false;"))
            {
                ((Page)this).Session["ShowFooter"] = false;
            }

            if (GetAccountBalance() > 0)
            {
                ((Page)this).Response.Redirect("./Start.aspx");
            }
            else if (!IsAutoPayOnForAccount(registrationData.WindowsLiveClientID.Trim()))
            {
                ((Page)this).Session["TopupPageReturnUrl"] = "./Start.aspx";
                ((Page)this).Response.Redirect("./TopupNow.aspx");
            }
        }
        else
        {
            WriteToLog("Login failed for: " + EngageUser.Email);
            ((Page)this).Response.Redirect("./Error.aspx?autherr=1");
        }
    }

    private RegistrationData FixupPhoneNumberFormat(string UserEmail, RegistrationData RegData)
    {
        FYIDataAccess fYIDataAccess = new FYIDataAccess();
        RegData.Phone = Utilities.CleansePhoneNumberToAllNumbers(RegData.Phone.Trim());
        fYIDataAccess.WriteRegistrationDataToDB(RegData);
        RegData = fYIDataAccess.GetRegistrationData(UserEmail, "SimplyfyiPayments");
        return RegData;
    }

    private int GetAccountBalance()
    {
        FYIDataAccess fYIDataAccess = new FYIDataAccess();
        RegistrationData registrationData = (RegistrationData)((Page)this).Session["RegistrationData"];
        int parentAccountID = registrationData.ParentAccountID;
        int accountID = registrationData.AccountID;
        int num = 0;
        return fYIDataAccess.GetUsageInventoryBalanceByAccountID(accountID);
    }

    private bool IsAutoPayOnForAccount(string WLID)
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

    private void WriteToLog(string Message)
    {
        FileStream fileStream = null;
        StreamWriter streamWriter = null;
        string path = ((Page)this).Server.MapPath("./App_Data/Log/LogFile.txt");
        fileStream = File.Open(path, FileMode.Append);
        streamWriter = new StreamWriter(fileStream);
        streamWriter.WriteLine("\r\n[" + DateTime.Now.ToString() + "]" + Message);
        streamWriter.Close();
    }

    private string GetUserInfo(string AccessToken)
    {
        NameValueCollection nameValueCollection = new NameValueCollection();
        string empty = string.Empty;
        NameValueCollection nameValueCollection2 = new NameValueCollection();
        string httpUrl = "https://www.paypal.com/v1/identity/oauth2/userinfo?schema=paypalv1.1";
        nameValueCollection2.Add("Authorization", "Bearer " + AccessToken);
        try
        {
            empty = CallHttpEndpoint("GET", httpUrl, nameValueCollection2);
        }
        catch (Exception ex)
        {
            empty = ex.Message;
            Console.WriteLine("ERROR-GetPayPalApiAccessToken():" + ex.Message);
        }

        return empty;
    }

    private string GetPayPalApiAccessToken(string AuthCode)
    {
        NameValueCollection nameValueCollection = new NameValueCollection();
        string empty = string.Empty;
        NameValueCollection nameValueCollection2 = new NameValueCollection();
        string httpUrl = "https://www.paypal.com/v1/oauth2/token";
        nameValueCollection.Add("grant_type", "authorization_code");
        nameValueCollection.Add("code", AuthCode);
        string text = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("AdNrNj7z4xXUGyToUbxNvFL6PpaH9GEFQuE0pGFICIqIDiBALR9TeUohWCkEVX5ZrBpVCfUMCCbnGoJy:EBe4NEKWorc7ABybmcXdMgmSXaNl6IGGoPE7LMYCauHfIYhpRf9VRxq9cSUy71Fq4v_S_dhupCkSIwdF"));
        nameValueCollection2.Add("Authorization", "Basic " + text);
        try
        {
            empty = CallHttpEndpoint("POST", httpUrl, nameValueCollection2, nameValueCollection);
            if (string.IsNullOrEmpty(empty))
            {
                WriteToLog("Call to PayPal login failed.");
            }
        }
        catch (Exception ex)
        {
            empty = ex.Message;
            WriteToLog("ERROR-GetPayPalApiAccessToken():" + ex.Message);
        }

        return empty;
    }

    private string CallHttpEndpoint(string HttpMethod, string HttpUrl, NameValueCollection Headers, NameValueCollection Body = null)
    {
        string result = string.Empty;
        WebClient webClient = new WebClient();
        webClient.UseDefaultCredentials = true;
        webClient.Encoding = Encoding.ASCII;
        webClient.Headers.Add(Headers);

WriteToLog("In new CallHttpEndpoint()");

        //RemoteCertificateValidationCallback remoteCertificateValidationCallback = (object param0, X509Certificate param1, X509Chain param2, SslPolicyErrors param3) => true;
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, remoteCertificateValidationCallback);
            if (HttpMethod.ToUpper() == "POST")
            {
                byte[] bytes = webClient.UploadValues(HttpUrl, "POST", Body);
                result = Encoding.ASCII.GetString(bytes);
            }
            else if (HttpMethod.ToUpper() == "GET")
            {
                result = webClient.DownloadString(HttpUrl);
            }

            //ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, remoteCertificateValidationCallback);
        }
        catch (Exception ex)
        {
            WriteToLog("Call to PayPal login failed: " + ex.Message);
            //ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, remoteCertificateValidationCallback);
            throw ex;
        }

        return result;
    }


}
}