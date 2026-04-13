// Decompiled with JetBrains decompiler
// Type: PayPalBillingHandler.PayPalBillingHandler
// Assembly: PayPalBillingHandler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E12B0CE-84FF-499E-8889-7D74F7638DEC
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\BillingHandlers.simplyfyi.com\PayPal\bin\PayPalBillingHandler.dll

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Xml;
using Utils;

#nullable disable
namespace PayPalBillingHandler;

public class PayPalBillingHandler : IHttpHandler
{
  private bool IsBillingRequestBeingProcessed(string CLID)
  {
    bool flag = false;
    DataSet agreementTransaction = new FYIDataAccess().GetLockEntryForPayPalBillingAgreementTransaction(CLID);
    if (agreementTransaction.Tables.Count > 0 && agreementTransaction.Tables[0].Rows.Count > 0)
      flag = true;
    return flag;
  }

  public void ProcessRequest(HttpContext context)
  {
    string CLID = context.Request["clid"];
    if (string.IsNullOrEmpty(CLID) || this.IsBillingRequestBeingProcessed(CLID))
      return;
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    fyiDataAccess.WritePayPalBillingAgreementTransactionLock(CLID, DateTime.UtcNow);
    RegistrationData registrationData = fyiDataAccess.GetRegistrationData(CLID);
    DataSet billingAgreementByClid = fyiDataAccess.GetPayPalBillingAgreementByCLID(CLID);
    string ReferenceID = billingAgreementByClid.Tables[0].Rows[0]["BillingAgreementID"].ToString();
    Decimal AmountToBill = Convert.ToDecimal(billingAgreementByClid.Tables[0].Rows[0]["AmountToBill"]);
    string TransactionID = this.CallDoReferenceTransaction(registrationData.Email, ReferenceID, AmountToBill, registrationData.AccountID, registrationData.WindowsLiveClientID.Trim());
    if (!string.IsNullOrEmpty(TransactionID))
    {
      fyiDataAccess.DeleteLockEntryForPayPalBillingAgreementTransaction(CLID);
      int int32 = Convert.ToInt32(100M * AmountToBill);
      int num = this.UpdateusageInventory(registrationData.AccountID, "PAYPAL-AUTOPAY:" + TransactionID, int32);
      bool LockRemoved = false;
      if (!registrationData.Enabled)
      {
        LockRemoved = true;
        registrationData.Enabled = true;
        fyiDataAccess.WriteRegistrationDataToDB(registrationData);
      }
      this.SendConfirmationEmail("simplyfyipayments", registrationData.FullName.Trim(), registrationData.Email.Trim(), int32.ToString(), num.ToString(), TransactionID, context.Server, LockRemoved);
      Utilities.SendPushNotification("Auto-billing successful", "Auto-billing successful for " + registrationData.BusinessName.Trim());
    }
    else
    {
      fyiDataAccess.DeleteLockEntryForPayPalBillingAgreementTransaction(CLID);
      EMailSender.NotifyAdmin($"Auto-bill transaction for {registrationData.BusinessName.Trim()} failed.<br/><br/>");
      Utilities.SendPushNotification("Auto-billing error", "Auto-billing error for " + registrationData.BusinessName.Trim());
    }
  }

  public bool IsReusable => false;

  private void SendConfirmationEmail(
    string Subscription,
    string FullName,
    string ToEmail,
    string Quantity,
    string UnitsBalance,
    string TransactionID,
    HttpServerUtility Server,
    bool LockRemoved)
  {
    Decimal num = Convert.ToDecimal(Quantity) / 100M;
    XmlDocument xmlDocument = new XmlDocument();
    string path = string.Empty;
    if (Subscription.ToLower().Equals("simplyfyiexpress"))
      path = "./App_Data/EmailNotificationTemplates/PaymentConfirmation.xml";
    else if (Subscription.ToLower().Equals("simplyfyipayments"))
      path = "./App_Data/EmailNotificationTemplates/PaymentConfirmation.xml";
    xmlDocument.Load(Server.MapPath(path));
    string innerText1 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/subject").InnerText;
    string innerXml1 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/body").InnerXml;
    string innerXml2 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/footer").InnerXml;
    string innerText2 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/fromemail").InnerText;
    string innerText3 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/fromname").InnerText;
    string MessageBody = (innerXml1 + innerXml2).Replace("{FULLNAME}", FullName).Replace("{AUTOBILL-AMOUNT}", num.ToString("0.00")).Replace("{AUTOBILL-UNITS}", Quantity).Replace("{UNITS-BALANCE}", UnitsBalance).Replace("{AUTOBILL-DATE}", DateTime.UtcNow.ToShortDateString()).Replace("{TXID}", TransactionID);
    EMailSender.SendMail(innerText1, ToEmail, innerText2, innerText3, MessageBody);
  }

  public bool GetApiLiveModeForBillingAgreement()
  {
    return Convert.ToBoolean(ConfigurationManager.AppSettings.Get("PayPayApiLiveModeForBillingAgreement"));
  }

  private string CallDoReferenceTransaction(
    string PayPalEmail,
    string ReferenceID,
    Decimal AmountToBill,
    int AccountID,
    string CLID)
  {
    string empty1 = string.Empty;
    string TransactionID = string.Empty;
    bool billingAgreement = this.GetApiLiveModeForBillingAgreement();
    WebClient webClient = new WebClient();
    string str1 = "117";
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    string Url;
    string str2;
    if (!billingAgreement)
    {
      Url = "https://api-3t.sandbox.paypal.com/nvp";
      str2 = "PWD=1249594596" + "&USER=ramesh_1249594591_biz_api1.rameshinnovation.com" + "&SIGNATURE=A7TFmOxjLOvc9pP1M-kHZOxLtxp2AD2NSbLMMJKRHDHjum2-uIeX03U4";
    }
    else
    {
      Url = "https://api-3t.paypal.com/nvp";
      str2 = "PWD=22NVU5ALWGAPJUN6" + "&USER=ramesh_api1.rameshinnovation.com" + "&SIGNATURE=AFFFlRg6iBQ23yN4FDWZqvToB8RvASvA7e4cwbUzjwSGtMvdWRH5wuNN";
    }
    string PostBody = $"{$"{$"{str2}&METHOD=DoReferenceTransaction&VERSION={str1}"}&REFERENCEID={ReferenceID}" + "&PAYMENTACTION=Sale"}&AMT={AmountToBill.ToString()}" + "&CURRENCYCODE=USD" + "&DESC=simplyFYI-auto-recharge";
    try
    {
      string str3 = this.UrlDecode(this.MakeNVPCall(Url, PostBody));
      Decimal GrossAmount = Convert.ToDecimal(this.GetGrossAmount(str3));
      FYIDataAccess fyiDataAccess = new FYIDataAccess();
      if (this.CallWasSuccessful(str3))
      {
        TransactionID = this.GetTransactionID(str3);
        fyiDataAccess.WriteTransactionForPayPalBillingAgreementID(ReferenceID, TransactionID, DateTime.UtcNow, GrossAmount, str3);
      }
      else
        fyiDataAccess.WriteTransactionForPayPalBillingAgreementID(ReferenceID, "ERROR", DateTime.UtcNow, GrossAmount, str3);
    }
    catch (Exception ex)
    {
    }
    return TransactionID;
  }

  private int UpdateusageInventory(int AccountID, string TransactionID, int Quantity)
  {
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    DataSet inventoryByAccountId = fyiDataAccess.GetUsageInventoryByAccountID(AccountID);
    int UnitsBalance = (inventoryByAccountId.Tables[0].Rows[0]["Balance"] == DBNull.Value ? 0 : Convert.ToInt32(inventoryByAccountId.Tables[0].Rows[0]["Balance"])) + Quantity;
    fyiDataAccess.WriteUsageInventoryToDB(AccountID, VoiShareOLTypes.InventoryTransactionType.REPLENISH, TransactionID, Quantity, UnitsBalance);
    return UnitsBalance;
  }

  private string GetGrossAmount(string Response)
  {
    string grossAmount = string.Empty;
    foreach (string str in Response.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
    {
      if (str.StartsWith("AMT"))
        grossAmount = str.Substring(str.IndexOf("=") + 1);
    }
    return grossAmount;
  }

  private bool CallWasSuccessful(string Response)
  {
    bool flag = false;
    Response = this.UrlDecode(Response);
    if (!string.IsNullOrEmpty(Response) && Response.Contains("ACK=Success"))
      flag = true;
    return flag;
  }

  private string UrlDecode(string Url)
  {
    return Url.Replace("%2E", ".").Replace("%40", "@").Replace("%20", " ").Replace("%2D", "-").Replace("%3A", ":").Replace("%2e", ".").Replace("%40", "@").Replace("%20", " ").Replace("%2d", "-").Replace("%3a", ":").Replace(" ", "+");
  }

  private string GetTransactionID(string Response)
  {
    string transactionId = string.Empty;
    foreach (string str in Response.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
    {
      if (str.StartsWith("TRANSACTIONID"))
        transactionId = str.Substring(str.IndexOf("=") + 1);
    }
    return transactionId;
  }

  public string MakeNVPCall(string Url, string PostBody)
  {
    string empty = string.Empty;
    WebClient webClient = new WebClient();
    RemoteCertificateValidationCallback validationCallback = (RemoteCertificateValidationCallback) ((param0, param1, param2, param3) => true);
    string str;
    try
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      ServicePointManager.ServerCertificateValidationCallback += validationCallback;
      str = webClient.UploadString(Url, PostBody);
      ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
    }
    catch (Exception ex)
    {
      ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
      str = "exception: " + ex.Message;
    }
    return str;
  }
}
