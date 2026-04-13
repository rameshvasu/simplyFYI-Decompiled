// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.Adapters.SendGridAdapter
// Assembly: SendGridAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A443CF4D-3A7D-4E0F-A659-050F618DAD39
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\SendGridAdapter.dll

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;

#nullable disable
namespace RameshInnovation.VoiShare.Services.Adapters;

public class SendGridAdapter
{
  private static string ConvertToJsonString(SendGridAdapter.SendGridMessage SendGridMessage)
  {
    return $"{$"{$"{$"{$"{$"{$"{{\"From\":\"{SendGridMessage.From}\""},\"FromName\":\"{SendGridMessage.FromName}\""},\"To\":[\"{SendGridMessage.To[0]}\"]"},\"Subject\":\"{SendGridMessage.Subject}\""},\"PlainTextMessage\":\"{SendGridMessage.PlainTextMessage}\""},\"HtmlMessage\":\"{SendGridMessage.HtmlMessage}\""},\"SendgridSendOptions\":\"{SendGridMessage.SendgridSendOptions}\"" + "}";
  }

  public static string CleanseMessageBodyForJson(string XMLMessageBody)
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
    XMLMessageBody = XMLMessageBody.Replace("\u00B2", "<sup>2</sup>");
    return XMLMessageBody;
  }

  public static string SendGridSendTransactionalEmail(
    SendGridAdapter.SendGridMessage SendGridMessage)
  {
    string str = "false";
    string jsonString = SendGridAdapter.ConvertToJsonString(SendGridMessage);
    if (SendGridMessage.From.Equals("master@aba-elearning.com"))
      Logger.Write(jsonString);
    if (!string.IsNullOrEmpty(jsonString))
    {
      string address = "https://sendgridqueueapi20170401104625.azurewebsites.net/api/emailmessages/send";
      WebClient webClient = new WebClient();
      webClient.Headers.Add("Content-Type", "application/json");
      RemoteCertificateValidationCallback validationCallback = (RemoteCertificateValidationCallback) ((param0, param1, param2, param3) => true);
      try
      {
        ServicePointManager.ServerCertificateValidationCallback += validationCallback;
        str = webClient.UploadString(address, jsonString);
        ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
      }
      catch (Exception ex)
      {
        ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
        str = "exception: " + ex.Message;
      }
    }
    else
      Logger.Write("SendGridSendTransactionalEmail->ConvertToJsonString returned null: " + SendGridMessage.SendgridSendOptions + "\r\nThis causes a poison message.");
    Logger.Write(str);
    return str;
  }

  public static bool UserHasUnsubscribed(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    return false;
  }

  public static bool UserHasUnsubscribedInSendGrid(string To) => false;

  public struct EmailSendResult
  {
    public int AccountID;
    public int FYIMessageID;
    public string PXID;
    public bool IsTransactionalMessage;
    public VoiShareOLTypes.SendStatusCode SendStatus;
    public DateTime ResultDateTime;
  }

  public class SendGridMessage
  {
    public string From { get; set; }

    public string FromName { get; set; }

    public List<string> To { get; set; }

    public string Subject { get; set; }

    public string PlainTextMessage { get; set; }

    public string HtmlMessage { get; set; }

    public string SendgridSendOptions { get; set; }
  }
}
