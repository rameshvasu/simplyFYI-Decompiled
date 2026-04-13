// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.TwilioService
// Assembly: FYIDispatchService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95564A78-F0F7-4554-BAAC-3D7128A876B8
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\FYIDispatchService.exe

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Collections;
using System.Configuration;
using System.Net;
using System.Text;
using System.Xml;
using TwilioRest;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

internal class TwilioService
{
  protected const string API_VERSION = "2010-04-01";

  private string GetTwilioPostBackUrl()
  {
    return ConfigurationManager.AppSettings.Get("TwilioPostbackUrl");
  }

  private string GetTwilioAccountSID() => ConfigurationManager.AppSettings.Get("TwilioAccountSID");

  private string GetTwilioAuthToken() => ConfigurationManager.AppSettings.Get("TwilioAuthToken");

  public TwilioService.TwilioSendResult[] SendVoiceMessages(FYIMessageCompact[] FYIDataRecords)
  {
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    ArrayList arrayList = new ArrayList();
    Account account = new Account(this.GetTwilioAccountSID(), this.GetTwilioAuthToken());
    string path = $"/{"2010-04-01"}/Accounts/{this.GetTwilioAccountSID()}/Calls";
    XmlDocument xmlDocument1 = new XmlDocument();
    if (FYIDataRecords != null && FYIDataRecords.Length > 0)
    {
      foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
      {
        TwilioService.TwilioSendResult twilioSendResult = new TwilioService.TwilioSendResult();
        twilioSendResult.AccountID = fyiDataRecord.AccountID;
        twilioSendResult.FYIMessageID = fyiDataRecord.MessageID;
        twilioSendResult.IsTransactionalMessage = true;
        twilioSendResult.PXID = "0";
        twilioSendResult.ResultDateTime = UserDateTime.NowUTC();
        xmlDocument1.LoadXml(fyiDataRecord.XMLMessageBody);
        Logger.Write(fyiDataRecord.XMLMessageBody);
        string innerText1 = xmlDocument1.SelectSingleNode("//fyimessage/voice/to").InnerText;
        string innerText2 = xmlDocument1.SelectSingleNode("//fyimessage/voice/from").InnerText;
        string innerText3 = xmlDocument1.SelectSingleNode("//fyimessage/voice/message").InnerText;
        Hashtable vars = new Hashtable(3);
        vars.Add((object) "From", (object) innerText2);
        vars.Add((object) "To", (object) innerText1);
        vars.Add((object) "Url", (object) this.GetTwilioPostBackUrl());
        vars.Add((object) "IfMachine", (object) "Continue");
        try
        {
          string xml = account.request(path, "POST", vars);
          XmlDocument xmlDocument2 = new XmlDocument();
          xmlDocument2.LoadXml(xml);
          twilioSendResult.PXID = xmlDocument2.SelectSingleNode("//TwilioResponse/Call/Sid").InnerText;
          string innerText4 = xmlDocument2.SelectSingleNode("//TwilioResponse/Call/Status").InnerText;
          Logger.Write("Twilio call status: " + innerText4);
          if (innerText4 != "queued")
          {
            twilioSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -1;
          }
          else
          {
            twilioSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) 1;
            this.RemotePostTwilioGrammar(innerText3, twilioSendResult.PXID, fyiDataRecord.BatchID);
          }
          arrayList.Add((object) twilioSendResult);
          Logger.Write("Twilio send result: " + twilioSendResult.SendStatus.ToString());
        }
        catch (Exception ex)
        {
          string message = ex.Message;
          twilioSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -1;
          arrayList.Add((object) twilioSendResult);
          Logger.Write("Twilio send result: " + message);
        }
      }
    }
    return (TwilioService.TwilioSendResult[]) arrayList.ToArray(typeof (TwilioService.TwilioSendResult));
  }

  public TwilioService.TwilioSendResult[] SendSMSMessages(FYIMessageCompact[] FYIDataRecords)
  {
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    ArrayList arrayList = new ArrayList();
    Account account = new Account(this.GetTwilioAccountSID(), this.GetTwilioAuthToken());
    string path = $"/{"2010-04-01"}/Accounts/{this.GetTwilioAccountSID()}/SMS/Messages";
    XmlDocument xmlDocument1 = new XmlDocument();
    if (FYIDataRecords != null && FYIDataRecords.Length > 0)
    {
      foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
      {
        TwilioService.TwilioSendResult twilioSendResult = new TwilioService.TwilioSendResult();
        twilioSendResult.AccountID = fyiDataRecord.AccountID;
        twilioSendResult.FYIMessageID = fyiDataRecord.MessageID;
        twilioSendResult.IsTransactionalMessage = true;
        twilioSendResult.PXID = "0";
        twilioSendResult.ResultDateTime = UserDateTime.NowUTC();
        xmlDocument1.LoadXml(fyiDataRecord.XMLMessageBody);
        Logger.Write(fyiDataRecord.XMLMessageBody);
        string innerText1 = xmlDocument1.SelectSingleNode("//fyimessage/email/to").InnerText;
        string innerText2 = xmlDocument1.SelectSingleNode("//fyimessage/email/from").InnerText;
        string innerText3 = xmlDocument1.SelectSingleNode("//fyimessage/email/plaintextmessage").InnerText;
        Hashtable vars = new Hashtable(3);
        vars.Add((object) "From", (object) innerText2);
        vars.Add((object) "To", (object) innerText1);
        vars.Add((object) "Body", (object) innerText3);
        try
        {
          Logger.Write("Posting SMS message to Twilio");
          string xml = account.request(path, "POST", vars);
          Logger.Write("Posting Ok");
          XmlDocument xmlDocument2 = new XmlDocument();
          xmlDocument2.LoadXml(xml);
          twilioSendResult.PXID = xmlDocument2.SelectSingleNode("//TwilioResponse/SMSMessage/Sid").InnerText;
          XmlNode xmlNode = xmlDocument2.SelectSingleNode("//TwilioResponse/RestException");
          string str;
          if (xmlNode != null)
          {
            Logger.Write("SMS exception");
            str = $"{xmlNode.SelectSingleNode("//Status").Value}-{xmlNode.SelectSingleNode("//Code").Value}";
            twilioSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -1;
          }
          else
          {
            str = xmlDocument2.SelectSingleNode("//TwilioResponse/SMSMessage/Status").InnerText;
            if (str != "0")
            {
              if (str == "queued")
                twilioSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) 0;
            }
            else
              twilioSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) 1;
          }
          Logger.Write("Twilio call status: " + str);
          arrayList.Add((object) twilioSendResult);
        }
        catch (Exception ex)
        {
          twilioSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -1;
          arrayList.Add((object) twilioSendResult);
          Logger.Write("Twilio send result: " + ex.Message);
        }
      }
    }
    return (TwilioService.TwilioSendResult[]) arrayList.ToArray(typeof (TwilioService.TwilioSendResult));
  }

  private void RemotePostTwilioGrammar(string VoiceMessage, string CallSid, Guid BatchID)
  {
    XmlDocument xmlDocument = new XmlDocument();
    string address = ConfigurationManager.AppSettings.Get("TwilioDataPostUrl");
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    string str1 = (string) null;
    if (address != null)
    {
      string str2 = ConfigurationManager.AppSettings.Get("TwilioPostbackUrl");
      VoiceMessage = VoiceMessage.Replace("[", "<").Replace("]", ">");
      VoiceMessage = VoiceMessage.Replace("<div>", " ").Replace("</div>", " ");
      VoiceMessage = VoiceMessage.Replace("\r\n", " ");
      VoiceMessage = VoiceMessage.Replace("PROMPT", "prompt");
      VoiceMessage = VoiceMessage.Replace("KEYACTION", "keyaction");
      VoiceMessage = $"<voicemessage>{VoiceMessage}</voicemessage>";
      xmlDocument.LoadXml(VoiceMessage);
      XmlNode xmlNode = xmlDocument.SelectSingleNode("//voicemessage/prompt");
      string str3 = xmlNode.Attributes["mode"] == null ? (string) null : xmlNode.Attributes["mode"].Value;
      string str4 = xmlNode.Attributes["voice"] == null ? (string) null : xmlNode.Attributes["voice"].Value;
      str1 = xmlNode.Attributes["callparticipant"] == null ? (string) null : xmlNode.Attributes["callparticipant"].Value;
      string str5 = xmlNode.Attributes["promptdata"] == null ? (string) null : xmlNode.Attributes["promptdata"].Value;
      string str6 = xmlNode.Attributes["promptname"] == null ? (string) null : xmlNode.Attributes["promptname"].Value;
      stringBuilder2.Append("<prompt promptid=\"1\"");
      if (str6 != null)
        stringBuilder2.Append($" promptname=\"{str6}\"");
      if (str5 != null)
        stringBuilder2.Append($" promptdata=\"{str5}\"");
      stringBuilder2.Append(">");
      stringBuilder2.Append($"<Response><Gather action=\"{str2}\" numDigits=\"1\">");
      if (str3 != null)
        stringBuilder2.Append($"<Play>{xmlNode.InnerText.Trim()}</Play></Gather></Response></prompt>");
      else
        stringBuilder2.Append($"<Say voice='{str4}'>{xmlNode.InnerText.Trim()}</Say></Gather></Response></prompt>");
      stringBuilder1.Append("<keyactions>");
      foreach (XmlNode selectNode in xmlDocument.SelectNodes("//voicemessage/keyaction"))
      {
        string str7 = selectNode.Attributes["end"] == null ? (string) null : selectNode.Attributes["end"].Value;
        string str8 = selectNode.Attributes["dial"] == null ? (string) null : selectNode.Attributes["dial"].Value;
        string str9 = selectNode.Attributes["mode"] == null ? (string) null : selectNode.Attributes["mode"].Value;
        string str10 = selectNode.Attributes["redirect"] == null ? (string) null : selectNode.Attributes["redirect"].Value;
        string str11 = selectNode.Attributes["isdata"] == null ? (string) null : selectNode.Attributes["isdata"].Value;
        string str12 = selectNode.Attributes["datatag"] == null ? (string) null : selectNode.Attributes["datatag"].Value;
        stringBuilder1.Append($"<keyaction key=\"{selectNode.Attributes["key"].Value}\"");
        if (str11 != null && str11 == "true")
          stringBuilder1.Append($" datatag=\"{str12}\">");
        else
          stringBuilder1.Append(">");
        stringBuilder1.Append("<Response>");
        if (str9 == null)
        {
          if (selectNode.InnerText != null)
            stringBuilder1.Append($"<Say voice='{str4}'>{selectNode.InnerText.Trim()}</Say>");
        }
        else
          stringBuilder1.Append($"<Play>{selectNode.InnerText.Trim()}</Play>");
        if (str8 != null && str8.Length > 0)
          stringBuilder1.Append($"<Dial>{str8}</Dial>");
        if (str10 != null)
        {
          if (str11 == "true")
            str10 = $"{str10}?datatag={str12}";
          stringBuilder1.Append("<Pause length=\"3\"/>");
          stringBuilder1.Append($"<Redirect>{str10}</Redirect>");
        }
        stringBuilder1.Append("</Response></keyaction>");
      }
      stringBuilder1.Append("</keyactions>");
    }
    string str13 = $"{$"<voicegrammar sid=\"{CallSid}\""} batchid=\"{BatchID.ToString()}\"";
    string data = (!(str1 != string.Empty) ? str13 + ">" : $"{str13} callparticipant=\"{str1}\">") + stringBuilder2.ToString() + stringBuilder1.ToString() + "</voicegrammar>";
    WebClient webClient = new WebClient();
    Logger.Write("DataToPost:" + data);
    webClient.UploadString(address, data);
  }

  public struct TwilioSendResult
  {
    public int AccountID;
    public int FYIMessageID;
    public string PXID;
    public bool IsTransactionalMessage;
    public VoiShareOLTypes.SendStatusCode SendStatus;
    public DateTime ResultDateTime;
  }
}
