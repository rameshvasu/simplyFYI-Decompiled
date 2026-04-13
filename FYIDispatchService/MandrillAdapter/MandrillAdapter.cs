// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.Adapters.MandrillAdapter
// Assembly: MandrillAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03B45742-E072-49C4-8BB0-818A9396681F
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\MandrillAdapter.dll

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Xml;
using Utils;

#nullable disable
namespace RameshInnovation.VoiShare.Services.Adapters;

public class MandrillAdapter
{
  private string GetMandrillUserName() => ConfigurationManager.AppSettings.Get("MandrillUserName");

  private string GetMandrillPassword() => ConfigurationManager.AppSettings.Get("MandrillPassword");

  private string GetMandrillAPIKey() => "52Tx9WhzdPB4pq7D73t65A";

  public bool UserHasUnsubscribed(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    return false;
  }

  private string MandrillSendTransactionalEmail(
    ref MandrillAdapter.EmailSendResult Result,
    MandrillMessage MandrillMessage)
  {
    string empty = string.Empty;
    string address = "https://mandrillapp.com/api/1.0/messages/send.json";
    string data = MandrillMessage.SerializeMandrillMessage(MandrillMessage);
    WebClient webClient = new WebClient();
    RemoteCertificateValidationCallback validationCallback = (RemoteCertificateValidationCallback) ((param0, param1, param2, param3) => true);
    string str;
    try
    {
      ServicePointManager.ServerCertificateValidationCallback += validationCallback;
      str = webClient.UploadString(address, data);
      ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
      Logger.Write("Mandrill send ok.");
    }
    catch (Exception ex)
    {
      ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
      Logger.Write($"Mandrill send failed:\r\n---------------\r\nException: {ex.Message}\r\n---------------\r\n{data}");
      EMailSender.NotifyAdmin("Mandrill send failed.");
      throw ex;
    }
    return str;
  }

  private Dictionary<string, string> ConvertJsonresponseToDictionary(string JsonResponse)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    JsonResponse = JsonResponse.Substring(2, JsonResponse.Length - 4);
    foreach (string str1 in JsonResponse.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
    {
      string[] strArray = str1.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      string key = strArray[0].Substring(1, strArray[0].Length - 2);
      string str2 = strArray[1];
      if (!strArray[1].Contains("null"))
        str2 = strArray[1].Substring(1, strArray[1].Length - 2);
      dictionary.Add(key, str2);
    }
    return dictionary;
  }

  private bool UserHasUnsubscribedInMandrill(string To) => false;

  private string CleanseMessageBodyForJson(string XMLMessageBody)
  {
    XMLMessageBody = XMLMessageBody.Replace("&quot;", "'");
    XMLMessageBody = XMLMessageBody.Replace("\"", "'");
    XMLMessageBody = XMLMessageBody.Replace("\r\n", "<br/>");
    XMLMessageBody = XMLMessageBody.Replace("\t", " ");
    XMLMessageBody = XMLMessageBody.Replace("<br>", "<br/>");
    XMLMessageBody = XMLMessageBody.Replace("&nbsp;", " ");
    XMLMessageBody = XMLMessageBody.Replace("“", "'");
    XMLMessageBody = XMLMessageBody.Replace("”", "'");
    XMLMessageBody = XMLMessageBody.Replace("’", "'");
    XMLMessageBody = XMLMessageBody.Replace("—", "-");
    return XMLMessageBody;
  }

  public MandrillAdapter.EmailSendResult[] SendTransactionalEmails(
    FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList = new ArrayList();
    string str = (string) null;
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    XmlDocument xmlDocument = new XmlDocument();
    if (FYIDataRecords != null && FYIDataRecords.Length > 0)
    {
      foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
      {
        MandrillAdapter.EmailSendResult Result = new MandrillAdapter.EmailSendResult();
        Result.AccountID = fyiDataRecord.AccountID;
        Result.FYIMessageID = fyiDataRecord.MessageID;
        Result.IsTransactionalMessage = true;
        Result.PXID = "0";
        Result.ResultDateTime = UserDateTime.NowUTC();
        fyiDataRecord.XMLMessageBody = this.CleanseMessageBodyForJson(fyiDataRecord.XMLMessageBody);
        xmlDocument.LoadXml(fyiDataRecord.XMLMessageBody);
        MandrillMessage MandrillMessage = new MandrillMessage();
        MandrillMessage.key = this.GetMandrillAPIKey();
        _message message = new _message();
        message.from_email = xmlDocument.SelectSingleNode("//fyimessage/email/from").InnerText;
        message.from_name = xmlDocument.SelectSingleNode("//fyimessage/email/fromname").InnerText;
        message.html = xmlDocument.SelectSingleNode("//fyimessage/email/htmlmessage").InnerText;
        message.text = xmlDocument.SelectSingleNode("//fyimessage/email/plaintextmessage").InnerText;
        message.subject = xmlDocument.SelectSingleNode("//fyimessage/email/subject").InnerText;
        _to[] toArray = new _to[1];
        toArray[0].email = xmlDocument.SelectSingleNode("//fyimessage/email/to").InnerText;
        toArray[0].name = "";
        toArray[0].type = "to";
        message.to = toArray;
        message.track_opens = "true";
        MandrillMessage.message = message;
        if (Utilities.IsValidEmail(toArray[0].email))
        {
          if (!this.UserHasUnsubscribed(fyiDataRecord.AccountID, fyiDataRecord.MetaID, (VoiShareOLTypes.EndPointType) 1, str))
          {
            if (!this.UserHasUnsubscribedInMandrill(str))
            {
              string innerText1 = xmlDocument.SelectSingleNode("//fyimessage/email/subject").InnerText;
              string innerText2 = xmlDocument.SelectSingleNode("//fyimessage/email/htmlmessage").InnerText;
              string innerText3 = xmlDocument.SelectSingleNode("//fyimessage/email/plaintextmessage").InnerText;
              Dictionary<string, string> dictionary = this.ConvertJsonresponseToDictionary(this.MandrillSendTransactionalEmail(ref Result, MandrillMessage));
              if (dictionary["status"].Equals("queued"))
                Result.SendStatus = (VoiShareOLTypes.SendStatusCode) 1;
              else if (dictionary["status"].Equals("sent"))
                Result.SendStatus = (VoiShareOLTypes.SendStatusCode) 1;
              Result.PXID = dictionary["_id"];
            }
            else
              Result.SendStatus = (VoiShareOLTypes.SendStatusCode) -2;
          }
          else
            Result.SendStatus = (VoiShareOLTypes.SendStatusCode) -2;
        }
        else
          Result.SendStatus = (VoiShareOLTypes.SendStatusCode) -3;
        arrayList.Add((object) Result);
      }
    }
    return (MandrillAdapter.EmailSendResult[]) arrayList.ToArray(typeof (MandrillAdapter.EmailSendResult));
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
