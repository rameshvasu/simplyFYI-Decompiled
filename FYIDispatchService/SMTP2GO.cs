// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.SMTP2GO
// Assembly: FYIDispatchService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95564A78-F0F7-4554-BAAC-3D7128A876B8
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\FYIDispatchService.exe

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Collections;
using System.Configuration;
using System.Net.Mail;
using System.Xml;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

internal class SMTP2GO
{
  private bool SendEmail(string FYIMessage)
  {
    bool flag = true;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(FYIMessage);
    SmtpClient smtpClient = new SmtpClient();
    string str1 = ConfigurationManager.AppSettings.Get("ServiceFromEmailAddress");
    string innerText1 = xmlDocument.SelectSingleNode("//fyimessage/email/to").InnerText;
    if (this.IsEmailAddressValid(innerText1) && this.IsEmailAddressValid(str1))
    {
      string innerText2 = xmlDocument.SelectSingleNode("//fyimessage/email/subject").InnerText;
      string innerText3 = xmlDocument.SelectSingleNode("//fyimessage/email/htmlmessage").InnerText;
      string str2 = (string) null;
      string innerText4 = xmlDocument.SelectSingleNode("//fyimessage/email/fromname").InnerText;
      MailMessage message = new MailMessage(new MailAddress(str1, innerText4), new MailAddress(innerText1));
      message.Subject = innerText2;
      try
      {
        if (innerText3 != null && innerText3.Length > 0)
        {
          message.Body = innerText3;
          message.IsBodyHtml = true;
          smtpClient.Send(message);
        }
        if (str2 != null && str2.Length > 0)
        {
          message.Body = str2;
          message.IsBodyHtml = false;
          smtpClient.Send(message);
        }
      }
      catch (Exception ex)
      {
        flag = false;
      }
    }
    else
      flag = false;
    return flag;
  }

  private bool IsEmailAddressValid(string EmailAddress)
  {
    bool flag = true;
    if (EmailAddress.Trim().Length > 0)
    {
      try
      {
        MailAddress mailAddress = new MailAddress(EmailAddress);
      }
      catch
      {
        flag = false;
      }
    }
    else
      flag = false;
    return flag;
  }

  public JangoMailService.EmailSendResult[] SendTransactionalEmails(
    FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList = new ArrayList();
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    XmlDocument xmlDocument = new XmlDocument();
    if (FYIDataRecords != null && FYIDataRecords.Length > 0)
    {
      foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
      {
        JangoMailService.EmailSendResult emailSendResult = new JangoMailService.EmailSendResult();
        emailSendResult.AccountID = fyiDataRecord.AccountID;
        emailSendResult.FYIMessageID = fyiDataRecord.MessageID;
        emailSendResult.IsTransactionalMessage = true;
        emailSendResult.PXID = Guid.NewGuid().ToString();
        emailSendResult.ResultDateTime = UserDateTime.NowUTC();
        try
        {
          this.SendEmail(fyiDataRecord.XMLMessageBody);
          emailSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) 1;
        }
        catch
        {
          emailSendResult.PXID = "0";
          emailSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -1;
        }
        arrayList.Add((object) emailSendResult);
      }
    }
    return (JangoMailService.EmailSendResult[]) arrayList.ToArray(typeof (JangoMailService.EmailSendResult));
  }
}
