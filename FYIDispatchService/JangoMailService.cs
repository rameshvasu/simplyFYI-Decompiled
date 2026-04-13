// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.JangoMailService
// Assembly: FYIDispatchService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95564A78-F0F7-4554-BAAC-3D7128A876B8
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\FYIDispatchService.exe

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using RameshInnovation.VoiShare.Services.JangoMailWS;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Net;
using System.Xml;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

internal class JangoMailService
{
  private const string SORTBYCLICKTIME = "t";
  private const string SORTASCENDING = "a";

  private int GetJangoTransactionGroupID()
  {
    return Convert.ToInt32(ConfigurationManager.AppSettings.Get("JangoTransactionGroupID"));
  }

  private string GetJangoMailUserName()
  {
    return ConfigurationManager.AppSettings.Get("JangoMailUserName");
  }

  private string GetJangoMailPassword()
  {
    return ConfigurationManager.AppSettings.Get("JangoMailPassword");
  }

  private string GetJangoMailSendOptions()
  {
    return ConfigurationManager.AppSettings.Get("JangoMailSendOptions");
  }

  private bool IsEmailAddressValid(string Email) => Utilities.IsValidEmail(Email);

  public bool UserHasUnsubscribedInJangoMail(
    string JangoUserName,
    string JangoPassword,
    string EmailAddress)
  {
    return JangoMailService.CheckUnsubscribeInJangoMail_HTTPGET(JangoUserName, JangoPassword, EmailAddress);
  }

  public bool UserHasUnsubscribed(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    bool flag = false;
    if (new OptInOutEngine().GetOptOutStatus(AccountID, MetaID, EndPointType, EndPointAddress) == 3)
      flag = true;
    return flag;
  }

  public void UpdateMessageEmailBounces(
    string SentFromDate,
    string SentToDate,
    string[] PXIDsToUpdate)
  {
    string jangoMailUserName = this.GetJangoMailUserName();
    string jangoMailPassword = this.GetJangoMailPassword();
    this.GetJangoTransactionGroupID();
    JangoMail jangoMail = new JangoMail();
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    Logger.Write($"Bounce check from {SentFromDate} to {SentToDate}");
    foreach (string str1 in PXIDsToUpdate)
    {
      Logger.Write("Checking bounce status for " + str1);
      string str2 = str1.Substring(0, str1.IndexOf(";"));
      if (!str2.Equals("0") && !str2.Equals("999999"))
      {
        try
        {
          DataSet emailStatsDataset = jangoMail.Reports_Transactional_GetSingleEmailStats_Dataset(jangoMailUserName, jangoMailPassword, Convert.ToInt64(str2));
          if (emailStatsDataset.Tables.Count > 0 && emailStatsDataset.Tables[0].Rows.Count > 0)
          {
            if (Convert.ToInt32(emailStatsDataset.Tables[0].Rows[0]["Bounces"]) > 0)
            {
              DateTime universalTime = DateTime.Now.ToUniversalTime();
              Logger.Write($"[Bounce] {str2}:{(object) universalTime}");
              fyiDataAccess.UpdateFYIMessageBouncedDateTime(str2, universalTime);
            }
          }
        }
        catch (Exception ex)
        {
          if (ex.Message.Contains("InvalidTransactionalIDException"))
            Logger.Write("UpdateMessageEmailBounces() -> EmailInvalidTransactionalIDException thrown for transaction ID " + str2);
        }
      }
    }
  }

  private DateTime ConvertInterprettedJangoMailServerTimeToUTC(
    DateTime InterprettedJangoMailServerOpenedDateTime)
  {
    return InterprettedJangoMailServerOpenedDateTime.ToUniversalTime().AddHours(DateTime.Now.IsDaylightSavingTime() ? 4.0 : 5.0);
  }

  public JangoMailService.EmailSendResult[] SendTransactionalEmails(
    FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList = new ArrayList();
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    JangoMail JMail = new JangoMail();
    string jangoMailUserName = this.GetJangoMailUserName();
    string jangoMailPassword = this.GetJangoMailPassword();
    XmlDocument xmlDocument = new XmlDocument();
    if (FYIDataRecords != null && FYIDataRecords.Length > 0)
    {
      foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
      {
        JangoMailService.EmailSendResult Result = new JangoMailService.EmailSendResult();
        Result.AccountID = fyiDataRecord.AccountID;
        Result.FYIMessageID = fyiDataRecord.MessageID;
        Result.IsTransactionalMessage = true;
        Result.PXID = "0";
        Result.ResultDateTime = UserDateTime.NowUTC();
        xmlDocument.LoadXml(fyiDataRecord.XMLMessageBody);
        Logger.Write(fyiDataRecord.XMLMessageBody);
        string innerText1 = xmlDocument.SelectSingleNode("//fyimessage/email/from").InnerText;
        string innerText2 = xmlDocument.SelectSingleNode("//fyimessage/email/fromname").InnerText;
        string innerText3 = xmlDocument.SelectSingleNode("//fyimessage/email/to").InnerText;
        if (this.IsEmailAddressValid(innerText3))
        {
          if (!this.UserHasUnsubscribed(fyiDataRecord.AccountID, fyiDataRecord.MetaID, (VoiShareOLTypes.EndPointType) 1, innerText3))
          {
            if (!this.UserHasUnsubscribedInJangoMail(jangoMailUserName, jangoMailPassword, innerText3))
            {
              string innerText4 = xmlDocument.SelectSingleNode("//fyimessage/email/subject").InnerText;
              string innerText5 = xmlDocument.SelectSingleNode("//fyimessage/email/htmlmessage").InnerText;
              string innerText6 = xmlDocument.SelectSingleNode("//fyimessage/email/plaintextmessage").InnerText;
              string jangoMailSendOptions = this.GetJangoMailSendOptions();
              this.JangoSendTransactionalEmail(JMail, ref Result, jangoMailUserName, jangoMailPassword, innerText1, innerText2, innerText3, innerText4, innerText6, innerText5, jangoMailSendOptions);
              Logger.Write($"JangoMail send result: PXID={Result.PXID}; Status={(object) Result.SendStatus}; BatchID={fyiDataRecord.BatchID.ToString()}");
            }
            else
            {
              Logger.Write($"Send failed: email unsubscribed ({innerText3}) in JangoMail");
              Result.SendStatus = (VoiShareOLTypes.SendStatusCode) -2;
            }
          }
          else
          {
            Logger.Write($"Send failed: email unsubscribed ({innerText3})");
            Result.SendStatus = (VoiShareOLTypes.SendStatusCode) -2;
          }
        }
        else
        {
          Logger.Write($"Send failed: bad email ID ({innerText3})");
          Result.SendStatus = (VoiShareOLTypes.SendStatusCode) -3;
        }
        arrayList.Add((object) Result);
      }
    }
    return (JangoMailService.EmailSendResult[]) arrayList.ToArray(typeof (JangoMailService.EmailSendResult));
  }

  private string JangoSendTransactionalEmail(
    JangoMail JMail,
    ref JangoMailService.EmailSendResult Result,
    string JangoUserName,
    string JangoPassword,
    string From,
    string FromName,
    string To,
    string Subject,
    string PlainTextMessage,
    string HtmlMessage,
    string JMailSendOptions)
  {
    string empty = string.Empty;
    string str;
    switch (ConfigurationManager.AppSettings.Get("UseHttpGet"))
    {
      case null:
        str = JangoMailService.SendTransactionalEmail_SOAP(JMail, ref Result, JangoUserName, JangoPassword, From, FromName, To, Subject, PlainTextMessage, HtmlMessage, JMailSendOptions);
        goto label_5;
      case "1":
        str = JangoMailService.SendTransactionalEmail_HTTPGET(ref Result, JangoUserName, JangoPassword, From, FromName, To, Subject, PlainTextMessage, HtmlMessage, JMailSendOptions);
        break;
      default:
        str = JangoMailService.SendTransactionalEmail_SOAP(JMail, ref Result, JangoUserName, JangoPassword, From, FromName, To, Subject, PlainTextMessage, HtmlMessage, JMailSendOptions);
        break;
    }
label_5:
    return str;
  }

  private static string SendTransactionalEmail_SOAP(
    JangoMail JMail,
    ref JangoMailService.EmailSendResult Result,
    string JangoUserName,
    string JangoPassword,
    string From,
    string FromName,
    string To,
    string Subject,
    string PlainTextMessage,
    string HtmlMessage,
    string JMailSendOptions)
  {
    string str = string.Empty;
    try
    {
      str = JMail.SendTransactionalEmail(JangoUserName, JangoPassword, From, FromName, To, Subject, PlainTextMessage, HtmlMessage, JMailSendOptions);
      string[] strArray = str.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      if (strArray[0] == "0")
      {
        Result.PXID = strArray[2];
        Result.SendStatus = (VoiShareOLTypes.SendStatusCode) 1;
      }
    }
    catch (Exception ex)
    {
    }
    return str;
  }

  private static bool CheckUnsubscribeInJangoMail_HTTPGET(
    string JangoUserName,
    string JangoPassword,
    string EmailAddress)
  {
    string empty = string.Empty;
    bool flag = false;
    WebClient webClient = new WebClient();
    string address = $"{$"{$"http://api.jangomail.com/api.asmx/CheckUnsubscribe?Username={JangoUserName}&"}Password={JangoPassword}&"}EmailAddress={EmailAddress}";
    try
    {
      if (webClient.DownloadString(address).ToLower().Contains("success"))
        flag = true;
    }
    catch (Exception ex)
    {
      Logger.Write(ex.Message);
    }
    return flag;
  }

  private static string SendTransactionalEmail_HTTPGET(
    ref JangoMailService.EmailSendResult Result,
    string JangoUserName,
    string JangoPassword,
    string From,
    string FromName,
    string To,
    string Subject,
    string PlainTextMessage,
    string HtmlMessage,
    string JMailSendOptions)
  {
    string xml = string.Empty;
    WebClient webClient = new WebClient();
    string address = $"{$"{$"{$"{$"{$"{$"{$"{$"http://api.jangomail.com/api.asmx/SendTransactionalEmail?Username={JangoUserName}&"}Password={JangoPassword}&"}FromEmail={From}&"}FromName={FromName}&"}ToEmailAddress={To}&"}Subject={Subject}&"}MessagePlain={PlainTextMessage}&"}MessageHTML={Utilities.HtmlEncode(HtmlMessage)}&"}Options={JMailSendOptions}";
    try
    {
      xml = webClient.DownloadString(address);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(xml);
      xml = xmlDocument.GetElementsByTagName("string")[0].InnerText;
      string[] strArray = xml.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      if (strArray[0] == "0")
      {
        Result.PXID = strArray[2];
        Result.SendStatus = (VoiShareOLTypes.SendStatusCode) 1;
      }
    }
    catch (Exception ex)
    {
      Logger.Write(ex.Message);
    }
    return xml;
  }

  public struct EmailSendResult
  {
    public int AccountID;
    public int FYIMessageID;
    public string PXID;
    public bool IsTransactionalMessage;
    public VoiShareOLTypes.SendStatusCode SendStatus;
    public DateTime ResultDateTime;
  }
}
