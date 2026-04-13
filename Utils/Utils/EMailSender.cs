// Decompiled with JetBrains decompiler
// Type: Utils.EMailSender
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.Configuration;
using System.Net.Mail;

#nullable disable
namespace Utils;

public class EMailSender
{
  public static void SendSuccessfulRegistrationEmail(string To, string ToName)
  {
    string MessageBody = $"{"<html><body><table width='600px'><tr><td>" + "<div><img src='http://www.simplyfyi.com/img/simplyfyi/logo.png' alt='logo'/></div>" + "<div style='font-style:Arial;font-size:15px;font-weight:bold'>simplyfyi.com Registration Successful</div><br/><br/>"}Dear {ToName}<br/><br/>Thank you for registering with simplyfyi.com." + ". To learn more about how to use simplyfyi.com, please visit <a href='http://www.simplyfyi.com'>simplyfyi.com</a>." + "<br/><br/>The simplyfyi Team</td></tr></table></body></html>";
    EMailSender.SendMail(To.Trim(), "fyi@simplyfyi.com", MessageBody);
  }

  public static void NotifyAdmin(string MessageBody)
  {
    string appSetting = ConfigurationManager.AppSettings["AdministratorEmail"];
    EMailSender.SendMail(appSetting, appSetting, MessageBody);
  }

  public static bool SendMail(string To, string From, string MessageBody)
  {
    bool flag = true;
    SmtpClient smtpClient = new SmtpClient();
    MailMessage message = new MailMessage(new MailAddress(From, "simplyfyi.com"), new MailAddress(To));
    message.Subject = "simplyfyi Notification";
    try
    {
      message.Body = MessageBody;
      message.IsBodyHtml = true;
      smtpClient.Send(message);
    }
    catch (Exception ex)
    {
      flag = false;
    }
    return flag;
  }

  public static bool SendMail(
    string Subject,
    string To,
    string FromEmail,
    string FromName,
    string MessageBody)
  {
    bool flag = true;
    SmtpClient smtpClient = new SmtpClient();
    MailMessage message = new MailMessage(new MailAddress(FromEmail, FromName), new MailAddress(To));
    message.Subject = Subject;
    try
    {
      message.Body = MessageBody;
      message.IsBodyHtml = true;
      smtpClient.Port = 80 /*0x50*/;
      smtpClient.Send(message);
    }
    catch (Exception ex)
    {
      flag = false;
    }
    return flag;
  }
}
