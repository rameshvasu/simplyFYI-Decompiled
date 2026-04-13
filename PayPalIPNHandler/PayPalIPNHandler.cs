// Decompiled with JetBrains decompiler
// Type: RameshInnovation.SimplyfyiExpress.PayPalIPNHandler
// Assembly: PayPalIPNHandler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B89D298-7395-4305-9308-85895A08B41A
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\PayPalIPNHandler.dll

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using RameshInnovation.VoiShare.Services.Adapters;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

#nullable disable
namespace RameshInnovation.SimplyfyiExpress;

public class PayPalIPNHandler : Page
{
  private const string FAILED_TO_RECEIVE_RESPONSE_FROM_PAYPAL = "PayPal IPN Error: Failed to receive response from PayPal";
  private const string FAILED_TO_UPDATE_VERIFIED_STATUS_AND_UPDATE_USAGE_INVENTORY = "PayPal IPN Error: Failed to either update VERIFIED status or update Usage Inventory";
  private const string FAILED_TO_UPDATE_INVALID_STATUS = "PayPal PIN Error: Failed to update INVALID status";
  private const string FAILED_TO_UPDATE_STATUS = "PayPal PIN Error: Failed to update status other than VERIFIED and INVALID";
  private const string UNIQUE_CODE_SEPARATOR = "<br/>";
  protected HtmlForm form1;

  protected void Page_Load(object sender, EventArgs e)
  {
    string empty1 = string.Empty;
    int HandlerFYIMetaID = 0;
    string str1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    string str2 = this.Request["passthru"];
    string CLID = this.Request["clid"];
    string str3 = Encoding.ASCII.GetString(this.Request.BinaryRead(HttpContext.Current.Request.ContentLength));
    if (string.IsNullOrEmpty(str2))
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(ConfigurationManager.AppSettings.Get("PayPalIPNVerificationUrl"));
      httpWebRequest.Method = "POST";
      httpWebRequest.ContentType = "application/x-www-form-urlencoded";
      httpWebRequest.Proxy = (IWebProxy) new WebProxy("http://www.paypal.com");
      httpWebRequest.Connection = "Close";
      NameValueCollection nameValueCollection = new NameValueCollection();
      string[] strArray1 = str3.Split('&');
      char[] chArray = new char[1]{ '=' };
      for (int index = 0; index < strArray1.Length; ++index)
      {
        string[] strArray2 = strArray1[index].Split(chArray);
        if (strArray2.Length > 1)
          nameValueCollection.Add(strArray2[0], HttpUtility.UrlDecode(strArray2[1]));
      }
      str3 += "&cmd=_notify-validate";
      httpWebRequest.ContentLength = (long) str3.Length;
      try
      {
        StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream(), Encoding.ASCII);
        streamWriter.Write(str3);
        streamWriter.Close();
        StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream());
        str1 = streamReader.ReadToEnd();
        streamReader.Close();
      }
      catch (Exception ex)
      {
      }
    }
    else
      str1 = "VERIFIED";
    if (!(str1 == "VERIFIED"))
      return;
    string endPointSubType = PayPalHelper.TranslateTransactionTypeToEndPointSubType(PayPalHelper.ExtractValueFromRequest(str3, "txn_type"));
    string valueFromRequest1 = PayPalHelper.ExtractValueFromRequest(str3, "payer_email");
    if (!string.IsNullOrEmpty(valueFromRequest1) && !valueFromRequest1.Contains("@dcc.paypal.com"))
    {
      string valueFromRequest2 = PayPalHelper.ExtractValueFromRequest(str3, "payment_status");
      string empty4 = string.Empty;
      PayPalIPNHandler.PayPalPayer payerStruct1 = this.CreatePayerStruct(str3);
      PayPalIPNHandler.PayPalTransaction transactionStruct = this.CreateTransactionStruct(str3);
      int FYIMessageID = 0;
      if (!this.SendMessageForThisPayPalTransaction(transactionStruct.OriginatorTransactionID, ref FYIMessageID))
      {
        string valueFromRequest3 = PayPalHelper.ExtractValueFromRequest(str3, "receiver_email");
        FYIDataAccess DAL = new FYIDataAccess();
        RegistrationData registrationData = DAL.GetRegistrationData(CLID);
        bool flag = DAL.PayPalContactExists(payerStruct1.PayPalPayerID, registrationData.AccountID);
        int PayPalPayerContactID1 = 0;
        this.WriteToLog(empty3, "ContactByPayerIDExists = " + flag.ToString());
        if (flag)
        {
          PayPalIPNHandler.PayPalPayer payerStruct2 = this.CreatePayerStruct(DAL.GetPayerContactDataAllFieldsByPayPalPayerID(payerStruct1.PayPalPayerID));
          this.ReconcilePayerValue(ref payerStruct1, payerStruct2);
          FYIMessageID = this.SendTransactionalEmail(registrationData.AccountID, str3, str1, CLID, valueFromRequest3, payerStruct1.PayerEmail, endPointSubType, ref HandlerFYIMetaID);
          int PayPalPayerContactID2 = this.WritePayPalPayerContactData2(registrationData.AccountID, payerStruct1, DAL);
          string valueFromRequest4 = PayPalHelper.ExtractValueFromRequest(str3, "payment_date");
          string TransactionTimeZone = valueFromRequest4.Substring(valueFromRequest4.Length - 3);
          string s = valueFromRequest4.Substring(0, valueFromRequest4.Length - 3);
          DateTime utc = PayPalHelper.MapTransactionTimeZoneToUTC(DateTime.Parse(s).ToShortDateString(), DateTime.Parse(s).ToShortTimeString(), TransactionTimeZone);
          DAL.WriteIPNTransactionDetail(utc, PayPalPayerContactID2, transactionStruct.GrossAmount, HandlerFYIMetaID, transactionStruct.OriginatorTransactionID, transactionStruct.OriginatorIPNID, str3, str1, FYIMessageID);
          if (FYIMessageID != 0 && endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_CART.ToString()))
            this.SendItemSpecificAdditionalEmailMessages(registrationData.AccountID, str3, str1, CLID, valueFromRequest3, payerStruct1.PayerEmail, endPointSubType, HandlerFYIMetaID);
        }
        else if (endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_PAYPALHERE.ToString()) && valueFromRequest2.ToLower().Equals("completed"))
        {
          string valueFromRequest5 = PayPalHelper.ExtractValueFromRequest(str3, "payment_date");
          string TransactionTimeZone = valueFromRequest5.Substring(valueFromRequest5.Length - 3);
          string s = valueFromRequest5.Substring(0, valueFromRequest5.Length - 3);
          DateTime utc = PayPalHelper.MapTransactionTimeZoneToUTC(DateTime.Parse(s).ToShortDateString(), DateTime.Parse(s).ToShortTimeString(), TransactionTimeZone);
          DAL.WriteIPNTransactionDetail(utc, PayPalPayerContactID1, transactionStruct.GrossAmount, HandlerFYIMetaID, transactionStruct.OriginatorTransactionID, transactionStruct.OriginatorIPNID, str3, str1, FYIMessageID);
        }
        else if ((endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_WEBACCEPT.ToString()) || endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_EXPRESSCHECKOUT.ToString()) || endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_CART.ToString()) || endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_VIRTUAL_TERMINAL.ToString()) || endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SEND_MONEY.ToString()) || endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION.ToString())) && valueFromRequest2.ToLower().Equals("completed"))
        {
          FYIMessageID = this.SendTransactionalEmail(registrationData.AccountID, str3, str1, CLID, valueFromRequest3, payerStruct1.PayerEmail, endPointSubType, ref HandlerFYIMetaID);
          int PayPalPayerContactID3 = this.WritePayPalPayerContactData2(registrationData.AccountID, payerStruct1, DAL);
          DAL.WriteIPNTransactionDetail(DateTime.UtcNow, PayPalPayerContactID3, transactionStruct.GrossAmount, HandlerFYIMetaID, transactionStruct.OriginatorTransactionID, transactionStruct.OriginatorIPNID, str3, str1, FYIMessageID);
          if (FYIMessageID != 0 && endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_CART.ToString()))
            this.SendItemSpecificAdditionalEmailMessages(registrationData.AccountID, str3, str1, CLID, valueFromRequest3, payerStruct1.PayerEmail, endPointSubType, HandlerFYIMetaID);
        }
        else if (!endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_SIGNUP.ToString()) && !endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_EOT.ToString()) && !endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_CANCEL.ToString()) && !endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_MODIFY.ToString()) && !endPointSubType.Equals(VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_FAILED.ToString()))
          ;
      }
    }
  }

  private void ReconcilePayerValue(
    ref PayPalIPNHandler.PayPalPayer Payer,
    PayPalIPNHandler.PayPalPayer PayerFromDB)
  {
    if (string.IsNullOrEmpty(Payer.PayerBusinessName) && !string.IsNullOrEmpty(PayerFromDB.PayerBusinessName))
      Payer.PayerBusinessName = PayerFromDB.PayerBusinessName;
    if (string.IsNullOrEmpty(Payer.PayerCity) && !string.IsNullOrEmpty(PayerFromDB.PayerCity))
      Payer.PayerCity = PayerFromDB.PayerCity;
    if (string.IsNullOrEmpty(Payer.PayerContactPhone) && !string.IsNullOrEmpty(PayerFromDB.PayerContactPhone))
      Payer.PayerContactPhone = PayerFromDB.PayerContactPhone;
    if (string.IsNullOrEmpty(Payer.PayerCountry) && !string.IsNullOrEmpty(PayerFromDB.PayerCountry))
      Payer.PayerCountry = PayerFromDB.PayerCountry;
    if (string.IsNullOrEmpty(Payer.PayerEmail) && !string.IsNullOrEmpty(PayerFromDB.PayerEmail))
      Payer.PayerEmail = PayerFromDB.PayerEmail;
    if (string.IsNullOrEmpty(Payer.PayerFirstName) && !string.IsNullOrEmpty(PayerFromDB.PayerFirstName))
      Payer.PayerFirstName = PayerFromDB.PayerFirstName;
    if (string.IsNullOrEmpty(Payer.PayerLastName) && !string.IsNullOrEmpty(PayerFromDB.PayerLastName))
      Payer.PayerLastName = PayerFromDB.PayerLastName;
    if (string.IsNullOrEmpty(Payer.PayerState) && !string.IsNullOrEmpty(PayerFromDB.PayerState))
      Payer.PayerState = PayerFromDB.PayerState;
    if (string.IsNullOrEmpty(Payer.PayerStreetAddress) && !string.IsNullOrEmpty(PayerFromDB.PayerStreetAddress))
      Payer.PayerStreetAddress = PayerFromDB.PayerStreetAddress;
    if (!string.IsNullOrEmpty(Payer.PayerZipCode) || string.IsNullOrEmpty(PayerFromDB.PayerZipCode))
      return;
    Payer.PayerZipCode = PayerFromDB.PayerZipCode;
  }

  private bool SendMessageForThisPayPalTransaction(
    string OriginatorTransactionID,
    ref int FYIMessageID)
  {
    bool flag1 = false;
    bool flag2 = true;
    string str = ConfigurationManager.AppSettings.Get("AllowMultipleEmailSendsForSameIPN");
    if (!string.IsNullOrEmpty(str))
    {
      try
      {
        if (Convert.ToBoolean(str))
          flag2 = false;
      }
      catch
      {
      }
    }
    if (flag2)
    {
      DataSet originatorTransactionId = new FYIDataAccess().GetIPNTransactionsByOriginatorTransactionID(OriginatorTransactionID);
      if (originatorTransactionId.Tables[0].Rows.Count > 0)
      {
        FYIMessageID = Convert.ToInt32(originatorTransactionId.Tables[0].Rows[0][nameof (FYIMessageID)]);
        flag1 = true;
      }
    }
    return flag1;
  }

  private PayPalIPNHandler.PayPalTransaction CreateTransactionStruct(string strRequest)
  {
    PayPalIPNHandler.PayPalTransaction transactionStruct = new PayPalIPNHandler.PayPalTransaction();
    string valueFromRequest = PayPalHelper.ExtractValueFromRequest(strRequest, "mc_gross");
    string s = valueFromRequest.Equals(string.Empty) ? "0.00" : valueFromRequest;
    transactionStruct.GrossAmount = double.Parse(s);
    transactionStruct.OriginatorTransactionID = PayPalHelper.ExtractValueFromRequest(strRequest, "txn_id");
    transactionStruct.OriginatorIPNID = PayPalHelper.ExtractValueFromRequest(strRequest, "ipn_track_id");
    transactionStruct.OriginatorInvoiceID = PayPalHelper.ExtractValueFromRequest(strRequest, "invoice_id");
    return transactionStruct;
  }

  private PayPalIPNHandler.PayPalPayer CreatePayerStruct(DataSet PayerContactDS)
  {
    PayPalIPNHandler.PayPalPayer payerStruct = new PayPalIPNHandler.PayPalPayer();
    payerStruct.PayerBusinessName = PayerContactDS.Tables[0].Rows[0]["BusinessName"].ToString();
    payerStruct.PayerCity = PayerContactDS.Tables[0].Rows[0]["City"].ToString();
    payerStruct.PayerContactPhone = PayerContactDS.Tables[0].Rows[0]["Phone"].ToString();
    payerStruct.PayerCountry = PayerContactDS.Tables[0].Rows[0]["Country"].ToString();
    payerStruct.PayerEmail = PayerContactDS.Tables[0].Rows[0]["Email"].ToString();
    if (string.IsNullOrEmpty(payerStruct.PayerEmail))
      payerStruct.PayerEmail = "NA";
    payerStruct.PayerFirstName = PayerContactDS.Tables[0].Rows[0]["FirstName"].ToString();
    payerStruct.PayerLastName = PayerContactDS.Tables[0].Rows[0]["LastName"].ToString();
    payerStruct.PayerState = PayerContactDS.Tables[0].Rows[0]["State"].ToString();
    payerStruct.PayerStreetAddress = PayerContactDS.Tables[0].Rows[0]["StreetAddress"].ToString();
    payerStruct.PayerZipCode = PayerContactDS.Tables[0].Rows[0]["ZIPCode"].ToString();
    payerStruct.PayPalPayerID = PayerContactDS.Tables[0].Rows[0]["ExternalPayerID"].ToString();
    return payerStruct;
  }

  private PayPalIPNHandler.PayPalPayer CreatePayerStruct(string strRequest)
  {
    PayPalIPNHandler.PayPalPayer payerStruct = new PayPalIPNHandler.PayPalPayer();
    payerStruct.PayerFirstName = PayPalHelper.ExtractValueFromRequest(strRequest, "first_name");
    payerStruct.PayerLastName = PayPalHelper.ExtractValueFromRequest(strRequest, "last_name");
    payerStruct.PayerStreetAddress = PayPalHelper.ExtractValueFromRequest(strRequest, "address_street");
    payerStruct.PayerCity = PayPalHelper.ExtractValueFromRequest(strRequest, "address_city");
    payerStruct.PayerState = PayPalHelper.ExtractValueFromRequest(strRequest, "address_state");
    payerStruct.PayerZipCode = PayPalHelper.ExtractValueFromRequest(strRequest, "address_zip");
    payerStruct.PayerCountry = PayPalHelper.ExtractValueFromRequest(strRequest, "address_country");
    payerStruct.PayerContactPhone = PayPalHelper.ExtractValueFromRequest(strRequest, "contact_phone");
    payerStruct.PayerBusinessName = PayPalHelper.ExtractValueFromRequest(strRequest, "payer_business_name");
    payerStruct.PayPalPayerID = PayPalHelper.ExtractValueFromRequest(strRequest, "payer_id");
    payerStruct.PayerEmail = PayPalHelper.ExtractValueFromRequest(strRequest, "payer_email");
    if (string.IsNullOrEmpty(payerStruct.PayerEmail))
      payerStruct.PayerEmail = payerStruct.PayPalPayerID + "@tempemail.com";
    return payerStruct;
  }

  private int WritePayPalPayerContactData2(
    int AccountID,
    PayPalIPNHandler.PayPalPayer Payer,
    FYIDataAccess DAL)
  {
    int PayerContactID = DAL.WritePayPalPayerContactData2(Payer.PayerFirstName, Payer.PayerLastName, Payer.PayerStreetAddress, Payer.PayerCity, Payer.PayerState, Payer.PayerZipCode, Payer.PayerCountry, Payer.PayerEmail, Payer.PayerContactPhone, Payer.PayerBusinessName, Payer.PayPalPayerID, AccountID);
    DAL.WriteAccount2PayerMap(AccountID, PayerContactID);
    return PayerContactID;
  }

  private int WritePayPalPayerContactData(
    int AccountID,
    PayPalIPNHandler.PayPalPayer Payer,
    FYIDataAccess DAL)
  {
    return DAL.WritePayPalPayerContactData(Payer.PayerFirstName, Payer.PayerLastName, Payer.PayerStreetAddress, Payer.PayerCity, Payer.PayerState, Payer.PayerZipCode, Payer.PayerCountry, Payer.PayerEmail, Payer.PayerContactPhone, Payer.PayerBusinessName, Payer.PayPalPayerID, AccountID);
  }

  private int GetDynamicFYIHandler(
    int DefaultFYIHandler,
    string EndPointSubType,
    int AccountID,
    string strRequest)
  {
    int dynamicFyiHandler = DefaultFYIHandler;
    if (EndPointSubType.Equals("PAYPAL_PAYMENT_WEBACCEPT") || EndPointSubType.Equals("PAYPAL_PAYMENT_SUBSCRIPTION"))
    {
      foreach (DataRow row in (InternalDataCollectionBase) new FYIDataAccess().GetFYIHandlerSelectorsForDefaultFYIHandler(DefaultFYIHandler).Tables[0].Rows)
      {
        string str1 = row["Field Name"].ToString();
        string str2 = row["Condition"].ToString();
        string str3 = row["Value"].ToString();
        int int32 = Convert.ToInt32(row["MetaID"]);
        string str4 = string.Empty;
        switch (str1)
        {
          case "ITEMNAME":
            str4 = PayPalHelper.ExtractValueFromRequest(strRequest, "item_name");
            break;
          case "ITEMNUMBER":
            str4 = PayPalHelper.ExtractValueFromRequest(strRequest, "item_number");
            break;
          case "COUNTRY":
            str4 = PayPalHelper.ExtractValueFromRequest(strRequest, "address_country");
            break;
        }
        if (!string.IsNullOrEmpty(str4))
        {
          switch (str2)
          {
            case "EQUALS":
              if (str4.ToLower().Equals(str3.ToLower()))
              {
                dynamicFyiHandler = int32;
                goto label_19;
              }
              break;
            case "CONTAINS":
              if (str4.ToLower().Contains(str3.ToLower()))
              {
                dynamicFyiHandler = int32;
                goto label_19;
              }
              break;
          }
        }
      }
label_19:;
    }
    return dynamicFyiHandler;
  }

  private int[] GetDynamicFYIHandlersForCart(int DefaultFYIHandler, string EndPointSubType)
  {
    ArrayList arrayList = new ArrayList();
    if (EndPointSubType.Equals("PAYPAL_PAYMENT_CART"))
    {
      foreach (DataRow row in (InternalDataCollectionBase) new FYIDataAccess().GetFYIHandlerSelectorsForDefaultFYIHandler(DefaultFYIHandler).Tables[0].Rows)
        arrayList.Add((object) Convert.ToInt32(row["MetaID"]));
    }
    return (int[]) arrayList.ToArray(typeof (int));
  }

  private int SendTransactionalEmail(
    int AccountID,
    string strRequest,
    string strResponse,
    string CLID,
    string SellerEmail,
    string PayerEmail,
    string EndPointSubType,
    ref int HandlerFYIMetaID)
  {
    int FYIMessageID = 0;
    string empty = string.Empty;
    bool flag = true;
    FYIDataAccess fyiDataAccess1 = new FYIDataAccess();
    DataSet transaction2HandlerMap = fyiDataAccess1.GetTransaction2HandlerMap(EndPointSubType, "PAYPAL", CLID);
    if (transaction2HandlerMap.Tables[0].Rows.Count > 0 && Convert.ToBoolean(transaction2HandlerMap.Tables[0].Rows[0]["HandlerEnabled"]))
    {
      int int32_1 = Convert.ToInt32(transaction2HandlerMap.Tables[0].Rows[0][nameof (HandlerFYIMetaID)]);
      int notificationMessageFlowMode = fyiDataAccess1.GetPayPalNotificationMessageFlowMode(int32_1);
      HandlerFYIMetaID = this.GetDynamicFYIHandler(int32_1, EndPointSubType, AccountID, strRequest);
      if (notificationMessageFlowMode >= 10 && int32_1 == HandlerFYIMetaID)
        flag = false;
      this.WriteToLog(empty, "SendThisMessage = " + flag.ToString());
      if (flag)
      {
        this.WriteToLog(empty, "HandlerFYIMetaID=" + HandlerFYIMetaID.ToString());
        FYIDataExpress fyiDataExpress = (FYIDataExpress) fyiDataAccess1.ReadFYIDataFromDB(HandlerFYIMetaID)[(object) HandlerFYIMetaID];
        EmailContext emailContext = new EmailContext(this.Session);
        EmailContext.Scribe scribeObject = (EmailContext.Scribe) fyiDataExpress.Scribe.ScribeObject;
        emailContext.EmailScribe = scribeObject;
        EmailContext.Share scheduleObject = (EmailContext.Share) fyiDataExpress.Schedule.ScheduleObject;
        emailContext.EmailShare = scheduleObject;
        Hashtable hashtable1 = new Hashtable();
        if (scribeObject.FieldTokens != null)
        {
          foreach (string fieldToken in scribeObject.FieldTokens)
          {
            string str1 = fieldToken.Substring(fieldToken.IndexOf(":") + 2);
            string str2 = str1.Substring(0, str1.Length - 1);
            string valueFromRequest = PayPalHelper.ExtractValueFromRequest(strRequest, str2);
            hashtable1.Add((object) str2, (object) valueFromRequest);
          }
        }
        string TemplateFile = this.Server.MapPath("./App_Data/PaymentDetailsTemplates/PDT1_Payment Details Template.xml");
        string str3 = emailContext.EmailScribe.ExtendedProperties[(object) "HTMLTHEME"].ToString();
        string TemplateName = "[%PAYMENTDETAILS%]";
        string messageHtml = scribeObject.MessageHTML;
        string oldValue = (string) null;
        if (messageHtml.Contains("[%PAYMENTDETAILS%]"))
        {
          oldValue = "[%PAYMENTDETAILS%]";
          if (!str3.StartsWith("1 COLUMN LAYOUT"))
            TemplateName = "[%PAYMENTDETAILS_2COLUMN%]";
        }
        else if (messageHtml.Contains("[%DONATIONDETAILS%]"))
        {
          oldValue = "[%DONATIONDETAILS%]";
          TemplateName = "[%DONATIONDETAILS%]";
          if (!str3.StartsWith("1 COLUMN LAYOUT"))
            TemplateName = "[%DONATIONDETAILS_2COLUMN%]";
        }
        if (!string.IsNullOrEmpty(oldValue))
          scribeObject.MessageHTML = scribeObject.MessageHTML.Replace(oldValue, PayPalHelper.CreateTransactionSummaryTableAsHtml(strRequest, TemplateName, TemplateFile));
        scribeObject.MessageHTML = scribeObject.MessageHTML.Replace("[%FACEBOOK_COMMENT%]", this.GetFacebookCommentsScript());
        scribeObject.MessageHTML = scribeObject.MessageHTML.Replace("[%HANDLERMETAID%]", HandlerFYIMetaID.ToString());
        string newValue = ConfigurationManager.AppSettings.Get("CommentBoxPageHomeUrl");
        scribeObject.MessageHTML = scribeObject.MessageHTML.Replace("[%COMMENTBOXPAGEHOMEURL%]", newValue);
        ArrayList arrayList = new ArrayList();
        if (scribeObject.FieldTokens != null)
        {
          foreach (string fieldToken in scribeObject.FieldTokens)
          {
            string str4 = fieldToken.Substring(fieldToken.IndexOf(":") + 1);
            arrayList.Add((object) str4);
          }
        }
        string[] array = (string[]) arrayList.ToArray(typeof (string));
        scribeObject.MessageHTML = PayPalHelper.PersonalizeMessage(AccountID, scribeObject.MessageHTML, strRequest, array);
        scribeObject.Subject = PayPalHelper.PersonalizeMessage(AccountID, scribeObject.Subject, strRequest, array);
        if (!string.IsNullOrEmpty(scribeObject.MessagePlainTextUnformatted))
          scribeObject.MessagePlainTextUnformatted = PayPalHelper.PersonalizeMessage(AccountID, scribeObject.MessagePlainTextUnformatted, strRequest, array);
        if (!string.IsNullOrEmpty(scribeObject.Message))
          scribeObject.Message = PayPalHelper.PersonalizeMessage(AccountID, scribeObject.Message, strRequest, array);
        AccountEndPointIdentities accountEndPoints = fyiDataAccess1.GetAccountEndPoints(AccountID);
        EndPointIdentity endPointIdentity1 = new EndPointIdentity();
        foreach (EndPointIdentity endPointIdentity2 in accountEndPoints.EndPointIdentities)
        {
          if (endPointIdentity2.EndPointType == VoiShareOLTypes.EndPointType.EMAIL)
          {
            endPointIdentity1 = endPointIdentity2;
            break;
          }
        }
        if (notificationMessageFlowMode == 0 || notificationMessageFlowMode == 10)
          PayerEmail = fyiDataAccess1.GetRegistrationData(CLID).Email.Trim();
        string str5 = this.CreateXMLMessage(endPointIdentity1.SenderAddress, endPointIdentity1.SenderName, scribeObject.Subject, scribeObject.MessageHTML, scribeObject.Message, PayerEmail, scribeObject.Signature);
        string DigitalCode1 = string.Empty;
        Hashtable hashtable2 = new Hashtable();
        if (AccountID == 1320 && str5.Contains("{digital_code}"))
        {
          string valueFromRequest1 = PayPalHelper.ExtractValueFromRequest(strRequest, "num_cart_items");
          if (string.IsNullOrEmpty(valueFromRequest1))
          {
            string valueFromRequest2 = PayPalHelper.ExtractValueFromRequest(strRequest, "quantity");
            PushoverAdapter pushoverAdapter = new PushoverAdapter();
            pushoverAdapter.Notify("u52wVSuUQ5wWBnHi5uoT8JKg9RrK8j", "The Ride & Ski Card", $"Processing {valueFromRequest2} codes for {PayerEmail}");
            str5 = this.ReplaceDigitalCodeTokenWithValue(str5, AccountID, PayerEmail, Convert.ToInt32(valueFromRequest2), ref DigitalCode1);
            this.WriteToLog(empty, $"DigitalCode: '{DigitalCode1}' for {PayerEmail}");
            pushoverAdapter.Notify("u52wVSuUQ5wWBnHi5uoT8JKg9RrK8j", "The Ride & Ski Card", "Codes: " + DigitalCode1);
          }
          else
          {
            int int32_2 = Convert.ToInt32(valueFromRequest1);
            int num = 0;
            for (int index1 = 1; index1 <= int32_2; ++index1)
            {
              string valueFromRequest3 = PayPalHelper.ExtractValueFromRequest(strRequest, "item_name" + index1.ToString());
              if (valueFromRequest3.ToUpper().Contains("DIGITAL"))
              {
                string valueFromRequest4 = PayPalHelper.ExtractValueFromRequest(strRequest, "quantity" + index1.ToString());
                num += Convert.ToInt32(valueFromRequest4);
                for (int index2 = 0; index2 < Convert.ToInt32(valueFromRequest4); ++index2)
                  DigitalCode1 = index2 != 0 ? $"{DigitalCode1}<br/>{this.GetNewDigitalCode(AccountID, PayerEmail)}" : this.GetNewDigitalCode(AccountID, PayerEmail);
                hashtable2.Add((object) valueFromRequest3, (object) DigitalCode1);
              }
            }
            string s = string.Empty;
            if (hashtable2.Count > 0)
            {
              foreach (DictionaryEntry dictionaryEntry in hashtable2)
              {
                s = !string.IsNullOrEmpty(s) ? $"{s}<br/><br/><u>Code(s) for {dictionaryEntry.Key.ToString()}:</u><br/>" : $"<br/><br/><u>Code(s) for {dictionaryEntry.Key.ToString()}:</u><br/>";
                s += dictionaryEntry.Value.ToString();
              }
            }
            else
              s = "DIGITAL CARD(S) NOT PURCHASED";
            DigitalCode1 = HttpUtility.HtmlEncode(s);
            str5 = str5.Replace("{digital_code}", DigitalCode1);
            new PushoverAdapter().Notify("u52wVSuUQ5wWBnHi5uoT8JKg9RrK8j", "The Ride & Ski Card (Cart)", $"Processing {(object) num} codes for {PayerEmail}");
          }
        }
        Guid BatchID1 = Guid.NewGuid();
        FYIDataAccess fyiDataAccess2 = fyiDataAccess1;
        int MetaID = HandlerFYIMetaID;
        string CreationDate = UserDateTime.NowUTC().ToString();
        DateTime dateTime = UserDateTime.NowUTC();
        string shortDateString1 = dateTime.ToShortDateString();
        dateTime = UserDateTime.NowUTC();
        string ScheduledSendTime = dateTime.ToString("HH:mm");
        string XMLMessageBody = str5;
        int int32_3 = Convert.ToInt32((object) VoiShareOLTypes.SendStatusCode.QUEUED);
        Guid BatchID2 = BatchID1;
        fyiDataAccess2.WriteFYIMessageToDB(MetaID, CreationDate, shortDateString1, ScheduledSendTime, 5, 1, XMLMessageBody, int32_3, true, 1, BatchID2);
        FYIMessageID = Convert.ToInt32(fyiDataAccess1.GetMessageByBatchID(BatchID1).Tables[0].Rows[0]["FYIMessageID"]);
        if (string.IsNullOrEmpty(PayerEmail.Trim()) || PayerEmail.Equals("NA") || PayerEmail.EndsWith("@tempemail.com"))
        {
          dateTime = DateTime.UtcNow;
          string shortDateString2 = dateTime.ToShortDateString();
          dateTime = DateTime.UtcNow;
          string ActualSendTime = dateTime.ToString("HH:mm");
          fyiDataAccess1.UpdateFYIMessageActualSendDateTimeAndStatus(FYIMessageID, shortDateString2, ActualSendTime, "999999", Convert.ToInt32((object) VoiShareOLTypes.SendStatusCode.SENT));
        }
        int int32_4 = Convert.ToInt32(ConfigurationManager.AppSettings.Get("EmailRateMultiplier"));
        DataSet inventoryByAccountId = fyiDataAccess1.GetUsageInventoryByAccountID(AccountID);
        int num1 = 0;
        if (inventoryByAccountId.Tables[0].Rows.Count > 0)
          num1 = Convert.ToInt32(inventoryByAccountId.Tables[0].Rows[0]["Balance"]);
        fyiDataAccess1.WriteUsageInventoryToDB(AccountID, VoiShareOLTypes.InventoryTransactionType.REDEEM, BatchID1.ToString(), int32_4 * -1, num1 + int32_4 * -1);
        if (AccountID == 1320)
        {
          if (string.IsNullOrEmpty(PayPalHelper.ExtractValueFromRequest(strRequest, "num_cart_items")))
          {
            foreach (string DigitalCode2 in DigitalCode1.Split("<br/>".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
              this.MapFYIMessageIDToDigitalCode(DigitalCode2, FYIMessageID);
            this.UpdateEmailEventDateForDigitalCode(FYIMessageID, "delivered", DateTime.UtcNow);
            string LogFile = empty;
            object[] objArray1 = new object[6]
            {
              (object) "FYIMessageID: ",
              (object) FYIMessageID,
              (object) " for Digital Code '",
              (object) DigitalCode1,
              (object) "' queued at ",
              null
            };
            object[] objArray2 = objArray1;
            dateTime = DateTime.UtcNow;
            string str6 = dateTime.ToString();
            objArray2[5] = (object) str6;
            string Message = string.Concat(objArray1);
            this.WriteToLog(LogFile, Message);
          }
          else
          {
            foreach (DictionaryEntry dictionaryEntry in hashtable2)
            {
              foreach (string DigitalCode3 in dictionaryEntry.Value.ToString().Replace("<br/>", ";").Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                this.MapFYIMessageIDToDigitalCode(DigitalCode3, FYIMessageID);
              this.UpdateEmailEventDateForDigitalCode(FYIMessageID, "delivered", DateTime.UtcNow);
              string LogFile = empty;
              object[] objArray3 = new object[6]
              {
                (object) "FYIMessageID: ",
                (object) FYIMessageID,
                (object) " for Digital Code '",
                (object) DigitalCode1,
                (object) "' queued at ",
                null
              };
              object[] objArray4 = objArray3;
              dateTime = DateTime.UtcNow;
              string str7 = dateTime.ToString();
              objArray4[5] = (object) str7;
              string Message = string.Concat(objArray3);
              this.WriteToLog(LogFile, Message);
            }
          }
          str5 = str5.Replace(PayerEmail, "rideandskine@comcast.net");
          this.QueueFYIMessageForDelivery(AccountID, HandlerFYIMetaID, str5, PayerEmail);
        }
        if (AccountID == 2385)
        {
          string FYIMessage = str5.Replace(PayerEmail, "info@connielecture.com");
          this.QueueFYIMessageForDelivery(AccountID, HandlerFYIMetaID, FYIMessage, PayerEmail);
        }
      }
    }
    if (FYIMessageID > 0)
      this.WriteToLog(empty, $"Account ID{AccountID.ToString()} sent email to {PayerEmail}");
    return FYIMessageID;
  }

  private int QueueFYIMessageForDelivery(
    int AccountID,
    int HandlerFYIMetaID,
    string FYIMessage,
    string PayerEmail)
  {
    FYIDataAccess fyiDataAccess1 = new FYIDataAccess();
    Guid BatchID1 = Guid.NewGuid();
    FYIDataAccess fyiDataAccess2 = fyiDataAccess1;
    int MetaID = HandlerFYIMetaID;
    DateTime dateTime = UserDateTime.NowUTC();
    string CreationDate = dateTime.ToString();
    dateTime = UserDateTime.NowUTC();
    string shortDateString1 = dateTime.ToShortDateString();
    dateTime = UserDateTime.NowUTC();
    string ScheduledSendTime = dateTime.ToString("HH:mm");
    string XMLMessageBody = FYIMessage;
    int int32_1 = Convert.ToInt32((object) VoiShareOLTypes.SendStatusCode.QUEUED);
    Guid BatchID2 = BatchID1;
    fyiDataAccess2.WriteFYIMessageToDB(MetaID, CreationDate, shortDateString1, ScheduledSendTime, 5, 1, XMLMessageBody, int32_1, true, 1, BatchID2);
    int int32_2 = Convert.ToInt32(fyiDataAccess1.GetMessageByBatchID(BatchID1).Tables[0].Rows[0]["FYIMessageID"]);
    if (string.IsNullOrEmpty(PayerEmail.Trim()) || PayerEmail.Equals("NA") || PayerEmail.EndsWith("@tempemail.com"))
    {
      string shortDateString2 = DateTime.UtcNow.ToShortDateString();
      string ActualSendTime = DateTime.UtcNow.ToString("HH:mm");
      fyiDataAccess1.UpdateFYIMessageActualSendDateTimeAndStatus(int32_2, shortDateString2, ActualSendTime, "999999", Convert.ToInt32((object) VoiShareOLTypes.SendStatusCode.SENT));
    }
    int int32_3 = Convert.ToInt32(ConfigurationManager.AppSettings.Get("EmailRateMultiplier"));
    DataSet inventoryByAccountId = fyiDataAccess1.GetUsageInventoryByAccountID(AccountID);
    int num = 0;
    if (inventoryByAccountId.Tables[0].Rows.Count > 0)
      num = Convert.ToInt32(inventoryByAccountId.Tables[0].Rows[0]["Balance"]);
    fyiDataAccess1.WriteUsageInventoryToDB(AccountID, VoiShareOLTypes.InventoryTransactionType.REDEEM, BatchID1.ToString(), int32_3 * -1, num + int32_3 * -1);
    return int32_2;
  }

  private void WriteToLog(string LogFile, string Message)
  {
    if (string.IsNullOrEmpty(LogFile))
    {
      string str = DateTime.Now.ToString("MMddyyyy");
      LogFile = $"{this.Server.MapPath(".")}/{str}_LogFile.txt";
    }
    StreamWriter streamWriter = System.IO.File.Exists(LogFile) ? new StreamWriter((Stream) System.IO.File.Open(LogFile, FileMode.Append)) : System.IO.File.CreateText(LogFile);
    streamWriter.WriteLine(Message);
    streamWriter.Close();
  }

  private string GetFacebookCommentsScript()
  {
    StreamReader streamReader = new StreamReader(this.Server.MapPath("./App_Data/SocialPluginScripts/Facebook/SOC1_FBComments.xml"));
    string end = streamReader.ReadToEnd();
    streamReader.Close();
    return end;
  }

  private string CreateXMLMessage(
    string FromAddress,
    string FromName,
    string Subject,
    string HTMLMessagePart,
    string PlainTextMessagePart,
    string ToAddress,
    string Signature)
  {
    StringWriter w = new StringWriter();
    XmlTextWriter xmlTextWriter1 = new XmlTextWriter((TextWriter) w);
    xmlTextWriter1.WriteStartElement("fyimessage");
    XmlTextWriter xmlTextWriter2 = xmlTextWriter1;
    DateTime dateTime = DateTime.Now;
    dateTime = dateTime.ToUniversalTime();
    string shortDateString = dateTime.ToShortDateString();
    xmlTextWriter2.WriteAttributeString("senddate", shortDateString);
    xmlTextWriter1.WriteStartElement("email", (string) null);
    xmlTextWriter1.WriteElementString("from", FromAddress);
    xmlTextWriter1.WriteElementString("fromname", FromName);
    xmlTextWriter1.WriteElementString("subject", Subject);
    xmlTextWriter1.WriteElementString("htmlmessage", HTMLMessagePart);
    xmlTextWriter1.WriteElementString("plaintextmessage", PlainTextMessagePart);
    xmlTextWriter1.WriteElementString("to", ToAddress);
    xmlTextWriter1.WriteElementString("signature", Signature);
    xmlTextWriter1.WriteEndElement();
    xmlTextWriter1.WriteEndElement();
    xmlTextWriter1.Flush();
    w.Flush();
    w.Close();
    xmlTextWriter1.Close();
    return w.ToString();
  }

  private string GetNewDigitalCode(int AccountID, string ReceiverEmail)
  {
    string newDigitalCode = string.Empty;
    string address = "http://simplyfyidigitalcodeapi.azurewebsites.net/api/v1/digitalcodes";
    WebClient webClient = new WebClient();
    webClient.Headers.Add("Content-Type", "application/json");
    try
    {
      string data = $"{{'accountid':{AccountID.ToString()}, 'receiveremail':'{ReceiverEmail}'}}";
      newDigitalCode = webClient.UploadString(address, data);
    }
    catch (Exception ex)
    {
      this.WriteToLog(string.Empty, "GetNewDigitalCode() exception: " + ex.Message);
    }
    return newDigitalCode;
  }

  private void MapFYIMessageIDToDigitalCode(string DigitalCode, int FYIMessageID)
  {
    string empty = string.Empty;
    string address = "http://simplyfyidigitalcodeapi.azurewebsites.net/api/v1/digitalcodes";
    WebClient webClient = new WebClient();
    webClient.Headers.Add("Content-Type", "application/json");
    try
    {
      string data = $"{{'action':'mapfyimessageidtodigitalcode', 'data':{{'digitalcode':'{DigitalCode}', 'fyimessageid':{(object) FYIMessageID}}}" + "}";
      webClient.UploadString(address, "PUT", data);
    }
    catch (Exception ex)
    {
    }
  }

  private void UpdateEmailEventDateForDigitalCode(
    int FYIMessageID,
    string EmailEventType,
    DateTime EmailEventDateTime)
  {
    string empty = string.Empty;
    string address = "http://simplyfyidigitalcodeapi.azurewebsites.net/api/v1/digitalcodes";
    WebClient webClient = new WebClient();
    webClient.Headers.Add("Content-Type", "application/json");
    try
    {
      string data = $"{{'action':'mapemaileventtodigitalcode', 'data':{{'eventtype':'{EmailEventType}', 'eventdatetime':'{EmailEventDateTime.ToString()}', 'fyimessageid':{(object) FYIMessageID}}}" + "}";
      webClient.UploadString(address, "PUT", data);
    }
    catch (Exception ex)
    {
    }
  }

  private string ReplaceDigitalCodeTokenWithValue(
    string Emailmessage,
    int AccountID,
    string ReceivedEmail,
    int Quantity,
    ref string DigitalCode)
  {
    string str = Emailmessage;
    if (Emailmessage.Contains("{digital_code}"))
    {
      for (int index = 0; index < Quantity; ++index)
      {
        if (index == 0)
        {
          DigitalCode = this.GetNewDigitalCode(AccountID, ReceivedEmail);
        }
        else
        {
          ref string local = ref DigitalCode;
          local = $"{local}<br/>{this.GetNewDigitalCode(AccountID, ReceivedEmail)}";
        }
      }
      if (!string.IsNullOrEmpty(DigitalCode))
        str = Emailmessage.Replace("{digital_code}", DigitalCode);
    }
    return str;
  }

  private bool DynamicFYIHandlerFilterMatchesCartItems(
    string Request,
    string FilterBy,
    string FilterCondition,
    string FilterValue)
  {
    string str1 = string.Empty;
    string empty = string.Empty;
    string str2 = string.Empty;
    int int32 = Convert.ToInt32(PayPalHelper.ExtractValueFromRequest(Request, "num_cart_items"));
    int num = 0;
    for (int index = 1; index <= int32; ++index)
    {
      switch (FilterBy)
      {
        case "ITEMNAME":
          str1 = PayPalHelper.ExtractValueFromRequest(Request, "item_name" + index.ToString());
          switch (FilterCondition)
          {
            case "EQUALS":
              if (str1.Equals(FilterValue))
              {
                ++num;
                continue;
              }
              continue;
            case "CONTAINS":
              if (str1.Contains(FilterValue))
                ++num;
              continue;
            default:
              continue;
          }
        case "ITEMNUMBER":
          string valueFromRequest = PayPalHelper.ExtractValueFromRequest(Request, "item_number" + index.ToString());
          switch (FilterCondition)
          {
            case "EQUALS":
              if (valueFromRequest.Equals(FilterValue))
              {
                ++num;
                continue;
              }
              continue;
            case "CONTAINS":
              if (valueFromRequest.Contains(FilterValue))
                ++num;
              continue;
            default:
              continue;
          }
        case "COUNTRY":
          str2 = PayPalHelper.ExtractValueFromRequest(Request, "address_country");
          switch (FilterCondition)
          {
            case "EQUALS":
              if (str1.Equals(FilterValue))
              {
                ++num;
                break;
              }
              break;
            case "CONTAINS":
              if (str1.Contains(FilterValue))
                ++num;
              break;
          }
          break;
      }
    }
    return num > 0;
  }

  private int[] SendItemSpecificAdditionalEmailMessages(
    int AccountID,
    string strRequest,
    string strResponse,
    string CLID,
    string SellerEmail,
    string PayerEmail,
    string EndPointSubType,
    int DefaultHandlerFYIMetaID)
  {
    ArrayList arrayList1 = new ArrayList();
    int[] fyiHandlersForCart = this.GetDynamicFYIHandlersForCart(DefaultHandlerFYIMetaID, EndPointSubType);
    if (fyiHandlersForCart.Length > 0)
    {
      string empty = string.Empty;
      FYIDataAccess fyiDataAccess1 = new FYIDataAccess();
      DataSet defaultFyiHandler = fyiDataAccess1.GetFYIHandlerSelectorsForDefaultFYIHandler(DefaultHandlerFYIMetaID);
      foreach (int num1 in fyiHandlersForCart)
      {
        DataRow[] dataRowArray = defaultFyiHandler.Tables[0].Select("MetaID=" + num1.ToString());
        string FilterBy = dataRowArray[0]["Field Name"].ToString();
        string FilterCondition = dataRowArray[0]["Condition"].ToString();
        string FilterValue = dataRowArray[0]["Value"].ToString();
        if (this.DynamicFYIHandlerFilterMatchesCartItems(strRequest, FilterBy, FilterCondition, FilterValue))
        {
          FYIDataExpress fyiDataExpress = (FYIDataExpress) fyiDataAccess1.ReadFYIDataFromDB(num1)[(object) num1];
          EmailContext emailContext = new EmailContext(this.Session);
          EmailContext.Scribe scribeObject = (EmailContext.Scribe) fyiDataExpress.Scribe.ScribeObject;
          emailContext.EmailScribe = scribeObject;
          EmailContext.Share scheduleObject = (EmailContext.Share) fyiDataExpress.Schedule.ScheduleObject;
          emailContext.EmailShare = scheduleObject;
          Hashtable hashtable = new Hashtable();
          if (scribeObject.FieldTokens != null)
          {
            foreach (string fieldToken in scribeObject.FieldTokens)
            {
              string str1 = fieldToken.Substring(fieldToken.IndexOf(":") + 2);
              string str2 = str1.Substring(0, str1.Length - 1);
              string valueFromRequest = PayPalHelper.ExtractValueFromRequest(strRequest, str2);
              hashtable.Add((object) str2, (object) valueFromRequest);
            }
          }
          string TemplateFile = this.Server.MapPath("./App_Data/PaymentDetailsTemplates/PDT1_Payment Details Grouped Template .xml");
          string str3 = emailContext.EmailScribe.ExtendedProperties[(object) "HTMLTHEME"].ToString();
          string TemplateName = "[%PAYMENTDETAILS%]";
          if (!str3.Equals("1 COLUMN LAYOUT"))
            TemplateName = "[%PAYMENTDETAILS_2COLUMN%]";
          scribeObject.MessageHTML = scribeObject.MessageHTML.Replace("[%PAYMENTDETAILS%]", PayPalHelper.CreateTransactionSummaryTableForSpecificCartItemsAsHtml(strRequest, TemplateName, TemplateFile, FilterBy, FilterCondition, FilterValue));
          scribeObject.MessageHTML = scribeObject.MessageHTML.Replace("[%FACEBOOK_COMMENT%]", this.GetFacebookCommentsScript());
          scribeObject.MessageHTML = scribeObject.MessageHTML.Replace("[%HANDLERMETAID%]", num1.ToString());
          string newValue = ConfigurationManager.AppSettings.Get("CommentBoxPageHomeUrl");
          scribeObject.MessageHTML = scribeObject.MessageHTML.Replace("[%COMMENTBOXPAGEHOMEURL%]", newValue);
          ArrayList arrayList2 = new ArrayList();
          if (scribeObject.FieldTokens != null)
          {
            foreach (string fieldToken in scribeObject.FieldTokens)
            {
              string str4 = fieldToken.Substring(fieldToken.IndexOf(":") + 1);
              arrayList2.Add((object) str4);
            }
          }
          string[] array = (string[]) arrayList2.ToArray(typeof (string));
          scribeObject.MessageHTML = PayPalHelper.PersonalizeMessage(AccountID, scribeObject.MessageHTML, strRequest, array);
          scribeObject.Subject = PayPalHelper.PersonalizeMessage(AccountID, scribeObject.Subject, strRequest, array);
          scribeObject.MessagePlainTextUnformatted = PayPalHelper.PersonalizeMessage(AccountID, scribeObject.MessagePlainTextUnformatted, strRequest, array);
          scribeObject.Message = PayPalHelper.PersonalizeMessage(AccountID, scribeObject.Message, strRequest, array);
          AccountEndPointIdentities accountEndPoints = fyiDataAccess1.GetAccountEndPoints(AccountID);
          EndPointIdentity endPointIdentity1 = new EndPointIdentity();
          foreach (EndPointIdentity endPointIdentity2 in accountEndPoints.EndPointIdentities)
          {
            if (endPointIdentity2.EndPointType == VoiShareOLTypes.EndPointType.EMAIL)
            {
              endPointIdentity1 = endPointIdentity2;
              break;
            }
          }
          if (fyiDataAccess1.GetPayPalNotificationMessageFlowMode(DefaultHandlerFYIMetaID) == 0)
            PayerEmail = fyiDataAccess1.GetRegistrationData(CLID).Email.Trim();
          string xmlMessage = this.CreateXMLMessage(endPointIdentity1.SenderAddress, endPointIdentity1.SenderName, scribeObject.Subject, scribeObject.MessageHTML, scribeObject.Message, PayerEmail, scribeObject.Signature);
          Guid BatchID1 = Guid.NewGuid();
          FYIDataAccess fyiDataAccess2 = fyiDataAccess1;
          int MetaID = num1;
          DateTime dateTime = UserDateTime.NowUTC();
          string CreationDate = dateTime.ToString();
          dateTime = UserDateTime.NowUTC();
          string shortDateString1 = dateTime.ToShortDateString();
          dateTime = UserDateTime.NowUTC();
          string ScheduledSendTime = dateTime.ToString("HH:mm");
          string XMLMessageBody = xmlMessage;
          int int32_1 = Convert.ToInt32((object) VoiShareOLTypes.SendStatusCode.QUEUED);
          Guid BatchID2 = BatchID1;
          fyiDataAccess2.WriteFYIMessageToDB(MetaID, CreationDate, shortDateString1, ScheduledSendTime, 5, 1, XMLMessageBody, int32_1, true, 1, BatchID2);
          int int32_2 = Convert.ToInt32(fyiDataAccess1.GetMessageByBatchID(BatchID1).Tables[0].Rows[0]["FYIMessageID"]);
          arrayList1.Add((object) int32_2);
          if (string.IsNullOrEmpty(PayerEmail.Trim()) || PayerEmail.Equals("NA") || PayerEmail.EndsWith("@tempemail.com"))
          {
            dateTime = DateTime.UtcNow;
            string shortDateString2 = dateTime.ToShortDateString();
            dateTime = DateTime.UtcNow;
            string ActualSendTime = dateTime.ToString("HH:mm");
            fyiDataAccess1.UpdateFYIMessageActualSendDateTimeAndStatus(int32_2, shortDateString2, ActualSendTime, "999999", Convert.ToInt32((object) VoiShareOLTypes.SendStatusCode.SENT));
          }
          int int32_3 = Convert.ToInt32(ConfigurationManager.AppSettings.Get("EmailRateMultiplier"));
          DataSet inventoryByAccountId = fyiDataAccess1.GetUsageInventoryByAccountID(AccountID);
          int num2 = 0;
          if (inventoryByAccountId.Tables[0].Rows.Count > 0)
            num2 = Convert.ToInt32(inventoryByAccountId.Tables[0].Rows[0]["Balance"]);
          fyiDataAccess1.WriteUsageInventoryToDB(AccountID, VoiShareOLTypes.InventoryTransactionType.REDEEM, BatchID1.ToString(), int32_3 * -1, num2 + int32_3 * -1);
        }
      }
    }
    return (int[]) arrayList1.ToArray(typeof (int));
  }

  public struct PayPalPayer
  {
    public string PayerFirstName;
    public string PayerLastName;
    public string PayerStreetAddress;
    public string PayerCity;
    public string PayerState;
    public string PayerZipCode;
    public string PayerCountry;
    public string PayerEmail;
    public string PayerContactPhone;
    public string PayerBusinessName;
    public string PayPalPayerID;
  }

  public struct PayPalTransaction
  {
    public double GrossAmount;
    public string OriginatorTransactionID;
    public string OriginatorIPNID;
    public string OriginatorInvoiceID;
  }
}
