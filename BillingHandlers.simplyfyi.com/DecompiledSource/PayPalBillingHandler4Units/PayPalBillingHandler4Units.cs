// Decompiled with JetBrains decompiler
// Type: PayPalBillingHandler4Units.PayPalBillingHandler4Units
// Assembly: PayPalBillingHandler4Units, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B66D0CD4-B98E-47C2-AE5F-74746E6F6BD6
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\BillingHandlers.simplyfyi.com\PayPal4Units\bin\PayPalBillingHandler4Units.dll

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Text;
using System.Web;
using System.Xml;
using Utils;

#nullable disable
namespace PayPalBillingHandler4Units;

public class PayPalBillingHandler4Units : IHttpHandler
{
  private const string FAILED_TO_RECEIVE_RESPONSE_FROM_PAYPAL = "PayPal IPN Error: Failed to receive response from PayPal";
  private const string FAILED_TO_UPDATE_VERIFIED_STATUS_AND_UPDATE_USAGE_INVENTORY = "PayPal IPN Error: Failed to either update VERIFIED status or update Usage Inventory";
  private const string FAILED_TO_UPDATE_INVALID_STATUS = "PayPal PIN Error: Failed to update INVALID status";
  private const string FAILED_TO_UPDATE_STATUS = "PayPal PIN Error: Failed to update status other than VERIFIED and INVALID";

  public void ProcessRequest(HttpContext context)
  {
    string str1 = string.Empty;
    string str2 = context.Request["passthru"];
    string CLID = context.Request["clid"];
    string strRequest = Encoding.ASCII.GetString(context.Request.BinaryRead(HttpContext.Current.Request.ContentLength));
    if (!string.IsNullOrEmpty(str2))
      str1 = "VERIFIED";
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    if (!(str1 == "VERIFIED"))
      return;
    string TransactionID = PayPalHelper.ExtractValueFromRequest(strRequest, "txn_id").Trim();
    RegistrationData registrationData = fyiDataAccess.GetRegistrationData(CLID);
    string ReferenceID = "PAYPAL:" + TransactionID;
    if (fyiDataAccess.GetUsageInventoryRecordByAccountIDAndReferenceID(registrationData.AccountID, ReferenceID).Tables[0].Rows.Count == 0)
    {
      int ofUnitsPurchased = this.GetQuantityOfUnitsPurchased(strRequest);
      int balanceByAccountId = fyiDataAccess.GetUsageInventoryBalanceByAccountID(registrationData.AccountID);
      int UnitsBalance = ofUnitsPurchased + balanceByAccountId;
      fyiDataAccess.WriteUsageInventoryToDB(registrationData.AccountID, VoiShareOLTypes.InventoryTransactionType.REPLENISH, ReferenceID, ofUnitsPurchased, UnitsBalance);
      bool LockRemoved = false;
      if (UnitsBalance > 0 && !registrationData.Enabled)
      {
        LockRemoved = true;
        registrationData.Enabled = true;
        fyiDataAccess.WriteRegistrationDataToDB(registrationData);
      }
      string ToEmail = registrationData.Email.Trim();
      if (!ToEmail.Equals("ramesh@rameshinnovation.com"))
      {
        this.SendConfirmationEmail("simplyfyipayments", registrationData.FullName.Trim(), ToEmail, ofUnitsPurchased.ToString(), UnitsBalance.ToString(), TransactionID, context.Server, LockRemoved);
        EMailSender.NotifyAdmin($"{registrationData.FullName.Trim()} bought {ofUnitsPurchased.ToString()} UNITS. Confirmation email sent.");
      }
      else
        EMailSender.NotifyAdmin("Unidentified merchant bought UNITS. Handle manually.");
    }
  }

  private string GetCustomerIDAndParentID(string strRequest)
  {
    return PayPalHelper.ExtractValueFromRequest(strRequest, "custom");
  }

  private int GetQuantityOfUnitsPurchased(string strRequest)
  {
    return Convert.ToInt32(PayPalHelper.ExtractValueFromRequest(strRequest, "option_selection").Split("+".ToCharArray())[0].Trim().Split(" ".ToCharArray())[0]);
  }

  private string GetTransactionID(string strRequest)
  {
    return "PAYPAL:" + PayPalHelper.ExtractValueFromRequest(strRequest, "txn_id");
  }

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
    if (LockRemoved)
    {
      if (Subscription.ToLower().Equals("simplyfyiexpress"))
        path = "./App_Data/EmailNotificationTemplates/PaymentConfirmationLockRemoved.xml";
      else if (Subscription.ToLower().Equals("simplyfyipayments"))
        path = "./App_Data/EmailNotificationTemplates/PaymentConfirmationLockRemoved.xml";
    }
    else if (Subscription.ToLower().Equals("simplyfyiexpress"))
      path = "./App_Data/EmailNotificationTemplates/PaymentConfirmation.xml";
    else if (Subscription.ToLower().Equals("simplyfyipayments"))
      path = "./App_Data/EmailNotificationTemplates/PaymentConfirmation.xml";
    xmlDocument.Load(Server.MapPath(path));
    string innerText1 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/subject").InnerText;
    string innerXml1 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/body").InnerXml;
    string innerXml2 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/footer").InnerXml;
    string innerText2 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/fromemail").InnerText;
    string innerText3 = xmlDocument.SelectSingleNode("//paymentconfirmationtemplate/fromname").InnerText;
    string MessageBody = (innerXml1 + innerXml2).Replace("{FULLNAME}", FullName).Replace("{PURCHASED-AMOUNT}", num.ToString("0.00")).Replace("{PURCHASED-UNITS}", Quantity).Replace("{UNITS-BALANCE}", UnitsBalance);
    EMailSender.SendMail(innerText1, ToEmail, innerText2, innerText3, MessageBody);
    EMailSender.NotifyAdmin($"New UNITS purchase by{innerText3}. Confirmation email sent to{ToEmail}.");
  }

  public bool IsReusable => false;
}
