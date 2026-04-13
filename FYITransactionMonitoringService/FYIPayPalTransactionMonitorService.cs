// Decompiled with JetBrains decompiler
// Type: FYITransactionMonitorService.FYIPayPalTransactionMonitorService
// Assembly: FYITransactionMonitorService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BFDFF65-9CAF-4A03-971B-47A6FFC4492C
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\TransactionMonitoringService\FYITransactionMonitorService.exe

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Xml;
using Utils;

#nullable disable
namespace FYITransactionMonitorService;

public class FYIPayPalTransactionMonitorService : ServiceBase
{
  private System.Timers.Timer m_TaskTimer = (System.Timers.Timer) null;
  private IContainer components = (IContainer) null;

  public FYIPayPalTransactionMonitorService() => this.InitializeComponent();

  protected override void OnStart(string[] args)
  {
    this.m_TaskTimer = new System.Timers.Timer();
    string s = ConfigurationManager.AppSettings.Get("TimerIntervalInSeconds");
    int num = 15;
    if (s != null)
      num = int.Parse(s);
    this.m_TaskTimer.Elapsed += new ElapsedEventHandler(this.m_TaskTimer_Elapsed);
    this.m_TaskTimer.Interval = (double) (num * 1000);
    this.m_TaskTimer.Enabled = true;
    this.m_TaskTimer.Start();
  }

  protected override void OnStop()
  {
  }

  private void m_TaskTimer_Elapsed(object sender, ElapsedEventArgs e)
  {
    this.m_TaskTimer.Stop();
    try
    {
      this.MonitorTransactions();
    }
    catch (Exception ex)
    {
      string str = $"FYIPayPalTransactionMonitor Service exception [:{DateTime.Now.ToString()}]\r\n" + ex.Message;
    }
    finally
    {
      this.m_TaskTimer.Start();
    }
  }

  private void MonitorTransactions()
  {
    bool boolean = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("PayPayApiLiveMode"));
    ArrayList arrayList = new ArrayList();
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    string EndDate = DateTime.UtcNow.ToString("s") + "Z";
    DateTime dateTime = fyiDataAccess.GetPayPalTransactionMonitorServiceLastRunDateTime();
    int int32 = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TransactionSearchWindowStartTimePaddingInSeconds"));
    dateTime = dateTime.AddSeconds((double) (-1 * int32));
    string StartDate = dateTime.ToString("s") + "Z";
    fyiDataAccess.SetPayPalTransactionMonitorServiceLastRunDateTime(DateTime.UtcNow);
    DataSet handlersForPayPal = fyiDataAccess.GetAccountInfoOfProductionEnabledFYIHandlersForPayPal();
    if (handlersForPayPal.Tables[0].Rows.Count <= 0)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) handlersForPayPal.Tables[0].Rows)
    {
      string str1 = row["PayPalPrimaryEmail"].ToString();
      string AccountCLID = row["CLID"].ToString();
      if (fyiDataAccess.GetRegistrationData(AccountCLID).Enabled)
      {
        string str2 = FYIPayPalTransactionMonitorService.UrlDecode(FYIPayPalTransactionMonitorService.GetNewestTransactionNVP(str1, boolean, StartDate, EndDate));
        if (!string.IsNullOrEmpty(str2))
        {
          if (str2.Contains("ACK=Success") && str2.Contains("TRANSACTIONID"))
          {
            foreach (string str3 in str2.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
              if (str3.StartsWith("L_TRANSACTIONID"))
                arrayList.Add((object) str3.Substring(str3.IndexOf("=") + 1));
            }
            foreach (string TransactionID in arrayList)
            {
              string transactionDetailsNvp = FYIPayPalTransactionMonitorService.GetTransactionDetailsNVP(TransactionID, str1, boolean);
              if (transactionDetailsNvp.Contains("ACK=Success"))
              {
                if (!FYIPayPalTransactionMonitorService.PostSimulatedIPN(FYIPayPalTransactionMonitorService.MapGetTransactionDetailsNVPToIPNFormat(transactionDetailsNvp), boolean, str1, AccountCLID))
                {
                  Logger.Write($"Error POSTing simulated IPN for {str1} -> txn id: {TransactionID}");
                  EMailSender.NotifyAdmin($"Error POSTing simulated IPN for {str1} -> txn id: {TransactionID}");
                }
                else
                  Logger.Write($"IPN POST successful for Transaction {TransactionID} for {str1} processed; email queued.");
              }
            }
          }
          else if (!str2.Contains("ACK=Failure") || !str2.Contains("L_ERRORCODE0=10002"))
            ;
        }
      }
    }
  }

  private static string MapGetTransactionDetailsNVPToIPNFormat(string Response)
  {
    string empty1 = string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    XmlDocument xmlDocument = new XmlDocument();
    StreamReader streamReader = new StreamReader(ConfigurationManager.AppSettings["NVPToIPNMapFile"].ToString());
    xmlDocument.LoadXml(streamReader.ReadToEnd());
    streamReader.Close();
    string[] strArray1 = Response.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    for (int index = 0; index < strArray1.Length; ++index)
      strArray1[index] = FYIPayPalTransactionMonitorService.UrlDecode(strArray1[index]);
    foreach (string str1 in strArray1)
    {
      string[] strArray2 = str1.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      XmlNode xmlNode1 = (XmlNode) null;
      if (!strArray2[0].StartsWith("L_"))
      {
        XmlNode xmlNode2 = xmlDocument.SelectSingleNode($"//map/fields/field/nvpfield[text() = '{strArray2[0].Trim()}']");
        if (xmlNode2 != null)
        {
          bool flag = true;
          do
          {
            if (flag)
            {
              flag = false;
              xmlNode1 = xmlNode2.NextSibling.Clone();
            }
            else
              xmlNode1 = xmlNode1.NextSibling.Clone();
            if (xmlNode1.InnerText.Trim().Length > 0)
            {
              xmlNode1.InnerText = xmlNode1.InnerText.Replace("\r\n", "").Trim();
              if (xmlNode1.InnerText.Equals("payment_date"))
                strArray2[1] = FYIPayPalTransactionMonitorService.ReFormatDateTime(strArray2[1]);
              else if (xmlNode1.InnerText.Equals("txn_type"))
              {
                if (strArray2[1].ToLower().Equals("webaccept"))
                  strArray2[1] = "web_accept";
                else if (strArray2[1].ToLower().Equals("subscrpayment"))
                  strArray2[1] = "subscr_payment";
                else if (strArray2[1].ToLower().Equals("none"))
                {
                  if (!new ArrayList((ICollection) strArray1).Contains((object) "EMAIL="))
                    strArray2[1] = "paypal_here";
                }
                else if (strArray2[1].ToLower().Equals("virtualterminal"))
                  strArray2[1] = "virtual_terminal";
                else if (strArray2[1].ToLower().Equals("sendmoney"))
                  strArray2[1] = "send_money";
              }
              else if (xmlNode1.InnerText.Equals("txn_id"))
                empty1 = strArray2[1];
              stringBuilder.Append(xmlNode1.InnerText + "=");
              stringBuilder.Append(strArray2[1] + "&");
            }
          }
          while (xmlNode1.NextSibling != null);
        }
      }
      else
      {
        string empty2 = string.Empty;
        int int32;
        string str2;
        if (Utilities.IsNumeric(strArray2[0].Substring(strArray2[0].Length - 3)))
        {
          int32 = Convert.ToInt32(strArray2[0].Substring(strArray2[0].Length - 3));
          str2 = strArray2[0].Substring(0, strArray2[0].Length - 3);
        }
        else if (Utilities.IsNumeric(strArray2[0].Substring(strArray2[0].Length - 2)))
        {
          int32 = Convert.ToInt32(strArray2[0].Substring(strArray2[0].Length - 2));
          str2 = strArray2[0].Substring(0, strArray2[0].Length - 2);
        }
        else
        {
          int32 = Convert.ToInt32(strArray2[0].Substring(strArray2[0].Length - 1));
          str2 = strArray2[0].Substring(0, strArray2[0].Length - 1);
        }
        XmlNode xmlNode3 = xmlDocument.SelectSingleNode($"//map/fields/lineitem/field/nvpfield[text() = '{str2}#']");
        if (xmlNode3 != null)
        {
          XmlNode xmlNode4 = xmlNode3.NextSibling.Clone();
          if (xmlNode4.InnerText.Trim().Length > 0)
          {
            if (int32 == 0)
            {
              if (!Response.Contains("TRANSACTIONTYPE=cart"))
              {
                if (xmlNode4.InnerText.Contains("_#"))
                  xmlNode4.InnerText = xmlNode4.InnerText.Substring(0, xmlNode4.InnerText.Length - "_#".Length);
                else if (xmlNode4.InnerText.Contains("#"))
                  xmlNode4.InnerText = xmlNode4.InnerText.Substring(0, xmlNode4.InnerText.Length - "#".Length);
              }
              else
              {
                int num = int32 + 1;
                xmlNode4.InnerText = xmlNode4.InnerText.Replace("#", num.ToString());
              }
            }
            else
            {
              int num = int32 + 1;
              xmlNode4.InnerText = xmlNode4.InnerText.Replace("#", num.ToString());
            }
            stringBuilder.Append(xmlNode4.InnerText + "=");
            if (xmlNode4.InnerText.Equals("quantity") && strArray2[1].Equals("0"))
              stringBuilder.Append("1&");
            else
              stringBuilder.Append(strArray2[1] + "&");
          }
        }
      }
    }
    if (stringBuilder.ToString().Contains("txn_type=cart"))
      stringBuilder.Append($"num_cart_items={FYIPayPalTransactionMonitorService.GetNumberOfCartItems(strArray1).ToString()}&");
    stringBuilder.Append($"ipn_track_id=SIM{empty1}&payment_status=Completed");
    return stringBuilder.ToString();
  }

  private static string GetMonthLiteral(string Month)
  {
    string monthLiteral = string.Empty;
    switch (Month)
    {
      case "01":
        monthLiteral = "Jan";
        break;
      case "02":
        monthLiteral = "Feb";
        break;
      case "03":
        monthLiteral = "Mar";
        break;
      case "04":
        monthLiteral = "Apr";
        break;
      case "05":
        monthLiteral = "May";
        break;
      case "06":
        monthLiteral = "Jun";
        break;
      case "07":
        monthLiteral = "Jul";
        break;
      case "08":
        monthLiteral = "Aug";
        break;
      case "09":
        monthLiteral = "Sep";
        break;
      case "10":
        monthLiteral = "Oct";
        break;
      case "11":
        monthLiteral = "Nov";
        break;
      case "12":
        monthLiteral = "Dec";
        break;
    }
    return monthLiteral;
  }

  private static string ReFormatDateTime(string ISODateTimeFormat)
  {
    string empty = string.Empty;
    string str1 = ISODateTimeFormat.Substring(0, 4);
    string Month = ISODateTimeFormat.Substring(5, 2);
    string str2 = ISODateTimeFormat.Substring(8, 2);
    return $"{ISODateTimeFormat.Substring(11, 2)}:{ISODateTimeFormat.Substring(14, 2)}:{ISODateTimeFormat.Substring(17, 2)} {FYIPayPalTransactionMonitorService.GetMonthLiteral(Month)} {str2}, {str1} UTC";
  }

  private static int GetNumberOfCartItems(string[] NameValuePairs)
  {
    int numberOfCartItems = 0;
    foreach (string nameValuePair in NameValuePairs)
    {
      if (nameValuePair.StartsWith("L_NAME"))
        ++numberOfCartItems;
    }
    return numberOfCartItems;
  }

  private static string UrlDecode(string Url)
  {
    return Url.Replace("%2E", ".").Replace("%40", "@").Replace("%20", " ").Replace("%2D", "-").Replace("%3A", ":").Replace("%2e", ".").Replace("%40", "@").Replace("%20", " ").Replace("%2d", "-").Replace("%3a", ":").Replace(" ", "+");
  }

  private static bool IssimplyFYITransaction(string IPNMessage)
  {
    bool flag = false;
    if (PayPalHelper.ExtractValueFromRequest(IPNMessage, "business").Equals("ramesh@rameshinnovation.com"))
      flag = true;
    return flag;
  }

  public static bool PostSimulatedIPN(
    string IPNMessage,
    bool LiveMode,
    string PayPalPrimaryEmail,
    string AccountCLID)
  {
    string empty = string.Empty;
    bool flag1 = true;
    bool flag2 = true;
    string str = ConfigurationManager.AppSettings.Get("PayPalIPNHandlerUrl");
    PayPalPrimaryEmail = LiveMode ? PayPalPrimaryEmail : "ramesh@simplyfyi.com";
    if (!AccountCLID.Equals(string.Empty))
    {
      WebClient webClient = new WebClient();
      if (FYIPayPalTransactionMonitorService.IssimplyFYITransaction(IPNMessage))
      {
        if (PayPalHelper.ExtractValueFromRequest(IPNMessage, "txn_type").Equals("web_accept"))
        {
          RegistrationData registrationData = new FYIDataAccess().GetRegistrationData(PayPalHelper.ExtractValueFromRequest(IPNMessage, "payer_email"), "simplyfyiPayments");
          if (registrationData != null)
            AccountCLID = registrationData.WindowsLiveClientID.Trim();
          str = ConfigurationManager.AppSettings.Get("PayPalBillingHandler4UnitsUrl");
        }
        else
          flag2 = false;
      }
      if (flag2)
      {
        string address = $"{str}?clid={AccountCLID}&passthru=1";
        try
        {
          webClient.UploadString(address, IPNMessage);
        }
        catch (Exception ex)
        {
          flag1 = false;
        }
      }
    }
    else
      flag1 = false;
    return flag1;
  }

  public static string GetNewestTransactionNVP(
    string PayPalMerchantEmail,
    bool LiveMode,
    string StartDate,
    string EndDate)
  {
    WebClient webClient = new WebClient();
    string str1 = "106.0-7652849";
    string Url = string.Empty;
    string empty = string.Empty;
    string str2 = "Received";
    string PostBody;
    if (!LiveMode)
    {
      PayPalMerchantEmail = "ramesh@simplyfyi.com";
      PostBody = $"{$"{$"{"PWD=1364316109" + "&USER=ramesh-facilitator_api1.rameshinnovation.com"}&subject={FYIPayPalTransactionMonitorService.UrlEncode(PayPalMerchantEmail)}" + "&SIGNATURE=AFcWxV21C7fd0v3bYYYRCpSSRl31Apt3ZDVBYYcSvvLMSjlzgt6Hiq6J"}&METHOD=TransactionSearch&VERSION={str1}&STARTDATE={StartDate}&ENDDATE={EndDate}"}&TRANSACTIONCLASS={str2}";
    }
    else
    {
      Url = "https://api-3t.paypal.com/nvp";
      string str3 = "PWD=22NVU5ALWGAPJUN6" + "&USER=ramesh_api1.rameshinnovation.com";
      if (!PayPalMerchantEmail.Trim().Equals("ramesh@rameshinnovation.com"))
        str3 = $"{str3}&subject={FYIPayPalTransactionMonitorService.UrlEncode(PayPalMerchantEmail)}";
      PostBody = $"{$"{str3 + "&SIGNATURE=AFFFlRg6iBQ23yN4FDWZqvToB8RvASvA7e4cwbUzjwSGtMvdWRH5wuNN"}&METHOD=TransactionSearch&VERSION={str1}&STARTDATE={StartDate}&ENDDATE={EndDate}"}&TRANSACTIONCLASS={str2}";
    }
    try
    {
      return FYIPayPalTransactionMonitorService.MakeNVPCall(Url, PostBody);
    }
    catch (Exception ex)
    {
      return string.Empty;
    }
  }

  private void SetLastTimeRunTimestamp()
  {
    new FYIDataAccess().SetPayPalTransactionMonitorServiceLastRunDateTime(DateTime.Now.ToUniversalTime());
  }

  public static string GetTransactionDetailsNVP(
    string TransactionID,
    string PayPalMerchantEmail,
    bool LiveMode)
  {
    if (PayPalMerchantEmail.Trim().Equals("ramesh@rameshinnovation.com"))
      LiveMode = true;
    WebClient webClient = new WebClient();
    string str1 = "106.0-7652849";
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string Url;
    string PostBody;
    if (!LiveMode)
    {
      PayPalMerchantEmail = "ramesh@simplyfyi.com";
      Url = "https://api-3t.sandbox.paypal.com/nvp";
      PostBody = $"{$"{"PWD=1364316109" + "&USER=ramesh-facilitator_api1.rameshinnovation.com"}&subject={FYIPayPalTransactionMonitorService.UrlEncode(PayPalMerchantEmail)}" + "&SIGNATURE=AFcWxV21C7fd0v3bYYYRCpSSRl31Apt3ZDVBYYcSvvLMSjlzgt6Hiq6J"}&METHOD=GetTransactionDetails&VERSION={str1}&TRANSACTIONID={TransactionID}";
    }
    else
    {
      Url = "https://api-3t.paypal.com/nvp";
      string str2 = "PWD=22NVU5ALWGAPJUN6" + "&USER=ramesh_api1.rameshinnovation.com";
      if (!PayPalMerchantEmail.Trim().Equals("ramesh@rameshinnovation.com"))
        str2 = $"{str2}&subject={FYIPayPalTransactionMonitorService.UrlEncode(PayPalMerchantEmail)}";
      PostBody = $"{str2 + "&SIGNATURE=AFFFlRg6iBQ23yN4FDWZqvToB8RvASvA7e4cwbUzjwSGtMvdWRH5wuNN"}&METHOD=GetTransactionDetails&VERSION={str1}&TRANSACTIONID={TransactionID}";
    }
    return FYIPayPalTransactionMonitorService.MakeNVPCall(Url, PostBody);
  }

  public static string MakeNVPCall(string Url, string PostBody)
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

  public static string UrlEncode(string Url)
  {
    return Url.Replace("\"", "%22").Replace("#", "%23").Replace("$", "%24").Replace("&", "%26").Replace("@", "%40").Replace("/", "%2F").Replace("+", "%2B").Replace(":", "%3A").Replace(",", "%2C");
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent() => this.ServiceName = "Service1";
}
