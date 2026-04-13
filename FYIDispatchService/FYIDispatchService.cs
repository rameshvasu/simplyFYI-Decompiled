// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.FYIDispatchService
// Assembly: FYIDispatchService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95564A78-F0F7-4554-BAAC-3D7128A876B8
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\FYIDispatchService.exe

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using RameshInnovation.VoiShare.Services.Adapters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.ServiceProcess;
using System.Timers;
using System.Xml;
using Utils;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

public class FYIDispatchService : ServiceBase
{
  private System.Timers.Timer m_TaskTimer = (System.Timers.Timer) null;
  private IContainer components = (IContainer) null;
  private ServiceController FYIDispatchServiceController;
  private BackgroundWorker bgWorker;

  public FYIDispatchService()
  {
    this.InitializeComponent();
    this.ServiceName = ConfigurationManager.AppSettings.Get("FYIDispatchServiceName");
  }

  protected override void OnStart(string[] args)
  {
    this.m_TaskTimer = new System.Timers.Timer();
    string s = ConfigurationManager.AppSettings.Get("TimerInterval");
    int num = 5000;
    if (s != null)
      num = int.Parse(s);
    this.m_TaskTimer.Elapsed += new ElapsedEventHandler(this.m_TaskTimer_Elapsed);
    this.m_TaskTimer.Interval = (double) num;
    this.m_TaskTimer.Enabled = true;
    this.m_TaskTimer.Start();
    new PushoverAdapter().Notify("u52wVSuUQ5wWBnHi5uoT8JKg9RrK8j", "FYI Dispatch Service Started", "FYI Dispatch Service Started at " + DateTime.UtcNow.ToString());
  }

  private void m_TaskTimer_Elapsed(object sender, ElapsedEventArgs e)
  {
    this.m_TaskTimer.Stop();
    this.bgWorker.RunWorkerAsync();
  }

  protected override void OnStop() => this.m_TaskTimer.Stop();

  private TwilioService.TwilioSendResult[] SendVoiceMessagesInternal(
    FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
    {
      if (fyiDataRecord.SendMode == 0)
        arrayList1.Add((object) fyiDataRecord);
      else
        arrayList2.Add((object) fyiDataRecord);
    }
    TwilioService.TwilioSendResult[] ProductionResults = (TwilioService.TwilioSendResult[]) null;
    TwilioService.TwilioSendResult[] TestResults = (TwilioService.TwilioSendResult[]) null;
    FYIMessageCompact[] array1 = (FYIMessageCompact[]) arrayList2.ToArray(typeof (FYIMessageCompact));
    if (array1 != null && array1.Length > 0)
      ProductionResults = new TwilioService().SendVoiceMessages(array1);
    FYIMessageCompact[] array2 = (FYIMessageCompact[]) arrayList1.ToArray(typeof (FYIMessageCompact));
    if (array2 != null && array2.Length > 0)
      TestResults = new TwilioService().SendVoiceMessages(array2);
    return this.AggregateTestAndProductionResults(TestResults, ProductionResults);
  }

  private TwilioService.TwilioSendResult[] SendSMSMessagesInternal(
    FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
    {
      if (fyiDataRecord.SendMode == 0)
        arrayList1.Add((object) fyiDataRecord);
      else
        arrayList2.Add((object) fyiDataRecord);
    }
    TwilioService.TwilioSendResult[] twilioSendResultArray = (TwilioService.TwilioSendResult[]) null;
    FYIMessageCompact[] array = (FYIMessageCompact[]) arrayList2.ToArray(typeof (FYIMessageCompact));
    if (array != null && array.Length > 0)
      twilioSendResultArray = new TwilioService().SendSMSMessages(array);
    return twilioSendResultArray;
  }

  private JangoMailService.EmailSendResult[] SendMessages(FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
    {
      if (fyiDataRecord.SendMode == 0)
        arrayList1.Add((object) fyiDataRecord);
      else
        arrayList2.Add((object) fyiDataRecord);
    }
    JangoMailService.EmailSendResult[] ProductionResults = (JangoMailService.EmailSendResult[]) null;
    FYIMessageCompact[] array1 = (FYIMessageCompact[]) arrayList2.ToArray(typeof (FYIMessageCompact));
    if (array1 != null && array1.Length > 0)
      ProductionResults = new JangoMailService().SendTransactionalEmails(array1);
    FYIMessageCompact[] array2 = (FYIMessageCompact[]) arrayList1.ToArray(typeof (FYIMessageCompact));
    JangoMailService.EmailSendResult[] TestResults = (JangoMailService.EmailSendResult[]) null;
    if (array2 != null && array2.Length > 0)
      TestResults = new SMTP2GO().SendTransactionalEmails(array2);
    return this.AggregateTestAndProductionResults(TestResults, ProductionResults);
  }

  private TwilioService.TwilioSendResult[] AggregateTestAndProductionResults(
    TwilioService.TwilioSendResult[] TestResults,
    TwilioService.TwilioSendResult[] ProductionResults)
  {
    TwilioService.TwilioSendResult[] twilioSendResultArray = (TwilioService.TwilioSendResult[]) null;
    ArrayList arrayList = new ArrayList();
    if (TestResults != null)
    {
      foreach (TwilioService.TwilioSendResult twilioSendResult in twilioSendResultArray)
        arrayList.Add((object) twilioSendResult);
    }
    if (ProductionResults != null)
    {
      foreach (TwilioService.TwilioSendResult productionResult in ProductionResults)
        arrayList.Add((object) productionResult);
    }
    if (arrayList.Count > 0)
      twilioSendResultArray = (TwilioService.TwilioSendResult[]) arrayList.ToArray(typeof (TwilioService.TwilioSendResult));
    return twilioSendResultArray;
  }

  private JangoMailService.EmailSendResult[] AggregateTestAndProductionResults(
    JangoMailService.EmailSendResult[] TestResults,
    JangoMailService.EmailSendResult[] ProductionResults)
  {
    JangoMailService.EmailSendResult[] emailSendResultArray = (JangoMailService.EmailSendResult[]) null;
    ArrayList arrayList = new ArrayList();
    if (TestResults != null)
    {
      foreach (JangoMailService.EmailSendResult testResult in TestResults)
        arrayList.Add((object) testResult);
    }
    if (ProductionResults != null)
    {
      foreach (JangoMailService.EmailSendResult productionResult in ProductionResults)
        arrayList.Add((object) productionResult);
    }
    if (arrayList.Count > 0)
      emailSendResultArray = (JangoMailService.EmailSendResult[]) arrayList.ToArray(typeof (JangoMailService.EmailSendResult));
    return emailSendResultArray;
  }

  private int CalculateLookbackWindowInterval()
  {
    return Convert.ToInt32(ConfigurationManager.AppSettings.Get("LookbackWindowInMinutes"));
  }

  private void ResendLocalQueuedMessages()
  {
    ArrayList arrayList = new ArrayList();
    foreach (string file in Directory.GetFiles(ConfigurationManager.AppSettings.Get("simplyFYIQueueLocalFilePath")))
    {
      string str = file.Substring(file.LastIndexOf("\\") + 1);
      string[] strArray = str.Substring(0, str.IndexOf(".txt")).Split('-');
      int int32_1 = Convert.ToInt32(strArray[0]);
      int int32_2 = Convert.ToInt32(strArray[1]);
      try
      {
        if (this.SendMessageTosimplyFYIQueueApi(int32_1, int32_2))
          arrayList.Add((object) file);
      }
      catch
      {
      }
    }
    foreach (string path in arrayList)
      System.IO.File.Delete(path);
  }

  private void SendQueuedMessage(bool HoldMessages)
  {
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    DateTime dateTime = UserDateTime.NowUTC();
    DateTime serviceLastRunDateTime = fyiDataAccess.GetDispatchServiceLastRunDateTime();
    int int32 = Convert.ToInt32((object) (VoiShareOLTypes.SendStatusCode) 0);
    if (HoldMessages)
      this.CheckAccountBalanceAndUnHoldMessages(fyiDataAccess.ReadFYIDataForAllHeldMessagesFromDB());
    FYIMessageCompact[] fyiMessageCompactArray1 = fyiDataAccess.ReadFYIDataFromDB((VoiShareOLTypes.EndPointType) 1, (VoiShareOLTypes.ScheduleType) 1, serviceLastRunDateTime, dateTime, int32);
    if (HoldMessages)
    {
      this.HoldMessagesForNoBalanceAccounts(fyiMessageCompactArray1);
      fyiMessageCompactArray1 = fyiDataAccess.ReadFYIDataFromDB((VoiShareOLTypes.EndPointType) 1, (VoiShareOLTypes.ScheduleType) 1, serviceLastRunDateTime, dateTime, int32);
    }
    this.SendEmailMessages(fyiMessageCompactArray1, (VoiShareOLTypes.EndPointType) 1);
    FYIMessageCompact[] fyiMessageCompactArray2 = fyiDataAccess.ReadFYIDataFromDB((VoiShareOLTypes.EndPointType) 5, (VoiShareOLTypes.ScheduleType) 1, serviceLastRunDateTime, dateTime, int32);
    if (HoldMessages)
    {
      this.HoldMessagesForNoBalanceAccounts(fyiMessageCompactArray2);
      fyiMessageCompactArray2 = fyiDataAccess.ReadFYIDataFromDB((VoiShareOLTypes.EndPointType) 5, (VoiShareOLTypes.ScheduleType) 1, serviceLastRunDateTime, dateTime, int32);
    }
    this.SendEmailMessages(fyiMessageCompactArray2, (VoiShareOLTypes.EndPointType) 5);
    FYIMessageCompact[] FYIDataRecords = fyiDataAccess.ReadFYIDataFromDB((VoiShareOLTypes.EndPointType) 2, (VoiShareOLTypes.ScheduleType) 1, serviceLastRunDateTime, dateTime, int32);
    if (HoldMessages)
    {
      this.HoldMessagesForNoBalanceAccounts(FYIDataRecords);
      FYIDataRecords = fyiDataAccess.ReadFYIDataFromDB((VoiShareOLTypes.EndPointType) 2, (VoiShareOLTypes.ScheduleType) 1, serviceLastRunDateTime, dateTime, int32);
    }
    FYIMessageCompact[] EmailDataRecords = (FYIMessageCompact[]) null;
    FYIMessageCompact[] SMSDataRecords = (FYIMessageCompact[]) null;
    this.CollateRecordsIntoEmailAndSMSMessages(FYIDataRecords, out EmailDataRecords, out SMSDataRecords);
    this.SendEmailMessages(EmailDataRecords, (VoiShareOLTypes.EndPointType) 2);
    this.SendSMSMessages(SMSDataRecords);
    FYIMessageCompact[] fyiMessageCompactArray3 = fyiDataAccess.ReadFYIDataFromDB((VoiShareOLTypes.EndPointType) 3, (VoiShareOLTypes.ScheduleType) 1, serviceLastRunDateTime, dateTime, int32);
    if (HoldMessages)
    {
      this.HoldMessagesForNoBalanceAccounts(fyiMessageCompactArray3);
      fyiMessageCompactArray3 = fyiDataAccess.ReadFYIDataFromDB((VoiShareOLTypes.EndPointType) 3, (VoiShareOLTypes.ScheduleType) 1, serviceLastRunDateTime, dateTime, int32);
    }
    this.SendVoiceMessages(fyiMessageCompactArray3);
    this.MonitorAccountBalance(EmailDataRecords, SMSDataRecords, fyiMessageCompactArray1, fyiMessageCompactArray2, fyiMessageCompactArray3);
  }

  private void CheckAccountBalanceAndUnHoldMessages(FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList = new ArrayList();
    int[] sendingAccounts = this.GetSendingAccounts(FYIDataRecords);
    if (sendingAccounts.Length <= 0)
      return;
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    foreach (int num in sendingAccounts)
    {
      if (fyiDataAccess.GetUsageInventoryBalanceByAccountID(num) > 0)
      {
        foreach (FYIMessageCompact fyiMessageCompact in fyiDataAccess.ReadFYIDataForAllHeldMessagesFromDBForAccount(num))
        {
          DateTime dateTime = UserDateTime.NowUTC();
          string shortDateString = dateTime.ToShortDateString();
          dateTime = UserDateTime.NowUTC();
          string str = dateTime.ToString("HH:mm");
          fyiDataAccess.UnHoldHeldFYIMessage(fyiMessageCompact.MessageID, shortDateString, str);
        }
      }
    }
  }

  private void HoldMessagesForNoBalanceAccounts(FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList1 = new ArrayList();
    int[] sendingAccounts = this.GetSendingAccounts(FYIDataRecords);
    if (sendingAccounts.Length <= 0)
      return;
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    foreach (int num1 in sendingAccounts)
    {
      int balanceByAccountId = fyiDataAccess.GetUsageInventoryBalanceByAccountID(num1);
      if (balanceByAccountId <= 0)
      {
        foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
        {
          if (fyiDataRecord.AccountID == num1)
            fyiDataAccess.HoldBackFYIMessage(fyiDataRecord.MessageID);
        }
      }
      else
      {
        Hashtable hashtable1 = new Hashtable();
        foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
        {
          if (fyiDataRecord.AccountID == num1)
          {
            if (!hashtable1.ContainsKey((object) fyiDataRecord.BatchID))
            {
              hashtable1.Add((object) fyiDataRecord.BatchID, (object) 1);
            }
            else
            {
              int int32 = Convert.ToInt32(hashtable1[(object) fyiDataRecord.BatchID]);
              Hashtable hashtable2 = hashtable1;
              // ISSUE: variable of a boxed type
              __Boxed<Guid> batchId = (System.ValueType) fyiDataRecord.BatchID;
              int num2 = int32;
              int num3 = num2 + 1;
              // ISSUE: variable of a boxed type
              __Boxed<int> local = (System.ValueType) num2;
              hashtable2[(object) batchId] = (object) local;
            }
          }
        }
        ArrayList arrayList2 = new ArrayList();
        foreach (DictionaryEntry dictionaryEntry in hashtable1)
        {
          int num4 = balanceByAccountId;
          if (num4 > Convert.ToInt32(dictionaryEntry.Value))
          {
            arrayList2.Add(dictionaryEntry.Key);
            int num5 = num4 - Convert.ToInt32(dictionaryEntry.Value);
          }
        }
        foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
        {
          if (fyiDataRecord.AccountID == num1)
          {
            foreach (Guid guid in (Guid[]) arrayList2.ToArray(typeof (Guid)))
            {
              if (fyiDataRecord.BatchID == guid)
                fyiDataAccess.ReleaseMessageForImmediateSend(fyiDataRecord.MessageID);
            }
          }
        }
      }
    }
  }

  private bool IsAutoBillEnabledForAccount(string CLID)
  {
    bool flag = false;
    DataSet billingAgreementByClid = new FYIDataAccess().GetPayPalBillingAgreementByCLID(CLID);
    if (billingAgreementByClid.Tables.Count > 0 & billingAgreementByClid.Tables[0].Rows.Count > 0)
      flag = true;
    return flag;
  }

  private void MonitorAccountBalance(
    FYIMessageCompact[] EmailFYIDataRecords,
    FYIMessageCompact[] SMSFYIDataRecords,
    FYIMessageCompact[] EmailOnlyFYIDataRecords,
    FYIMessageCompact[] IPNFYIDataRecords,
    FYIMessageCompact[] VoiceFYIDataRecords)
  {
    int[] sendingAccounts = this.GetSendingAccounts(EmailFYIDataRecords, SMSFYIDataRecords, EmailOnlyFYIDataRecords, IPNFYIDataRecords, VoiceFYIDataRecords);
    if (sendingAccounts.Length <= 0)
      return;
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    foreach (int num in sendingAccounts)
    {
      int balanceByAccountId = fyiDataAccess.GetUsageInventoryBalanceByAccountID(num);
      if (balanceByAccountId <= 300)
      {
        string empty = string.Empty;
        string appSetting = ConfigurationManager.AppSettings["BalanceAlertEmailTemplateFolder"];
        RegistrationData registrationData = fyiDataAccess.GetRegistrationData(num);
        if (!this.IsAutoBillEnabledForAccount(registrationData.WindowsLiveClientID.Trim()))
        {
          string ToEmail = fyiDataAccess.GetRegistrationData(registrationData.AccountID).Email.Trim();
          if (!this.IsAccountInTrialPeriod(registrationData.AccountID))
          {
            string BalanceAlertEmailTemplateFile = appSetting + "\\BalanceAlert.xml";
            this.SendBalanceAlertEmail(registrationData.FullName.Trim(), ToEmail, balanceByAccountId.ToString(), BalanceAlertEmailTemplateFile);
          }
          else if (balanceByAccountId <= 50)
          {
            string BalanceAlertEmailTemplateFile = appSetting + "\\BalanceAlertFreePeriod.xml";
            this.SendBalanceAlertEmail(registrationData.FullName.Trim(), ToEmail, balanceByAccountId.ToString(), BalanceAlertEmailTemplateFile);
            Utilities.SendPushNotification("Balance Alert", $"{registrationData.BusinessName.Trim()}:{balanceByAccountId.ToString()}");
          }
        }
        else if (balanceByAccountId <= 300)
          this.InitiateAutoBillingProcess(fyiDataAccess.GetRegistrationData(num).WindowsLiveClientID.Trim());
      }
    }
  }

  private bool IsAccountInTrialPeriod(int AccountID)
  {
    return new FYIDataAccess().IsAccountInTrialPeriod(AccountID);
  }

  private void InitiateAutoBillingProcess(string CLID)
  {
    string empty1 = string.Empty;
    RegistrationData registrationData = new FYIDataAccess().GetRegistrationData(CLID, "simplyfyiPayments");
    string address = $"{ConfigurationManager.AppSettings["PayPalBillingHandlerUrl"]}?clid={CLID.Trim()}";
    WebClient webClient = new WebClient();
    string empty2 = string.Empty;
    try
    {
      webClient.DownloadData(address);
      EMailSender.NotifyAdmin($"Successfully invoked PayPalBillingHandler.ashx for {registrationData.BusinessName.Trim()}.");
    }
    catch (Exception ex)
    {
      EMailSender.NotifyAdmin($"Error invoking PayPalBillingHandler.ashx for {registrationData.BusinessName.Trim()}.<br/><br/>" + ex.Message);
    }
  }

  private void SendBalanceAlertEmail(
    string FullName,
    string ToEmail,
    string UnitsBalance,
    string BalanceAlertEmailTemplateFile)
  {
    string empty = string.Empty;
    XmlDocument xmlDocument = new XmlDocument();
    try
    {
      Logger.Write($"Balance Alert: {FullName}({ToEmail}) -> {UnitsBalance} UNITS");
      xmlDocument.Load(BalanceAlertEmailTemplateFile);
      string innerText1 = xmlDocument.SelectSingleNode("//balancealert/subject").InnerText;
      string innerXml1 = xmlDocument.SelectSingleNode("//balancealert/body").InnerXml;
      string innerXml2 = xmlDocument.SelectSingleNode("//balancealert/footer").InnerXml;
      string innerText2 = xmlDocument.SelectSingleNode("//balancealert/fromemail").InnerText;
      string innerText3 = xmlDocument.SelectSingleNode("//balancealert/fromname").InnerText;
      string str = (innerXml1 + innerXml2).Replace("{FULLNAME}", FullName).Replace("{UNITS-BALANCE}", UnitsBalance);
      EMailSender.SendMail(innerText1, ToEmail, innerText2, innerText3, str);
    }
    catch (Exception ex)
    {
      Logger.Write("SendBalanceAlertEmail():" + ex.Message);
    }
  }

  private int[] GetSendingAccounts(FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList = new ArrayList();
    foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
    {
      if (!arrayList.Contains((object) fyiDataRecord.AccountID))
        arrayList.Add((object) fyiDataRecord.AccountID);
    }
    return (int[]) arrayList.ToArray(typeof (int));
  }

  private int[] GetSendingAccounts(
    FYIMessageCompact[] EmailFYIDataRecords,
    FYIMessageCompact[] SMSFYIDataRecords,
    FYIMessageCompact[] EmailOnlyFYIDataRecords,
    FYIMessageCompact[] IPNFYIDataRecords,
    FYIMessageCompact[] VoiceFYIDataRecords)
  {
    ArrayList arrayList = new ArrayList();
    foreach (FYIMessageCompact emailFyiDataRecord in EmailFYIDataRecords)
    {
      if (!arrayList.Contains((object) emailFyiDataRecord.AccountID))
        arrayList.Add((object) emailFyiDataRecord.AccountID);
    }
    foreach (FYIMessageCompact smsfyiDataRecord in SMSFYIDataRecords)
    {
      if (!arrayList.Contains((object) smsfyiDataRecord.AccountID))
        arrayList.Add((object) smsfyiDataRecord.AccountID);
    }
    foreach (FYIMessageCompact voiceFyiDataRecord in VoiceFYIDataRecords)
    {
      if (!arrayList.Contains((object) voiceFyiDataRecord.AccountID))
        arrayList.Add((object) voiceFyiDataRecord.AccountID);
    }
    foreach (FYIMessageCompact onlyFyiDataRecord in EmailOnlyFYIDataRecords)
    {
      if (!arrayList.Contains((object) onlyFyiDataRecord.AccountID))
        arrayList.Add((object) onlyFyiDataRecord.AccountID);
    }
    foreach (FYIMessageCompact ipnfyiDataRecord in IPNFYIDataRecords)
    {
      if (!arrayList.Contains((object) ipnfyiDataRecord.AccountID))
        arrayList.Add((object) ipnfyiDataRecord.AccountID);
    }
    return (int[]) arrayList.ToArray(typeof (int));
  }

  private void CollateRecordsIntoEmailAndSMSMessages(
    FYIMessageCompact[] FYIDataRecords,
    out FYIMessageCompact[] EmailDataRecords,
    out FYIMessageCompact[] SMSDataRecords)
  {
    bool flag = true;
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    XmlDocument xmlDocument = new XmlDocument();
    foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
    {
      xmlDocument.LoadXml(fyiDataRecord.XMLMessageBody);
      string innerText = xmlDocument.SelectSingleNode("//fyimessage/email/to").InnerText;
      string str1 = innerText.IndexOf("@") > 0 ? innerText.Substring(0, innerText.IndexOf("@")) : innerText;
      string str2;
      if (innerText.IndexOf("@") > 0)
      {
        str2 = innerText.Substring(0, innerText.IndexOf("@"));
      }
      else
      {
        string str3 = (string) null;
        flag = Utilities.IsValidUSPhoneNumber(str1, ref str3);
        str2 = str3;
      }
      if (flag)
      {
        xmlDocument.SelectSingleNode("//fyimessage/email/to").InnerText = str2;
        arrayList2.Add((object) new FYIMessageCompact()
        {
          AccountID = fyiDataRecord.AccountID,
          BatchID = fyiDataRecord.BatchID,
          MessageID = fyiDataRecord.MessageID,
          MetaID = fyiDataRecord.MetaID,
          SendMode = fyiDataRecord.SendMode,
          XMLMessageBody = xmlDocument.OuterXml
        });
      }
      else
        arrayList1.Add((object) new FYIMessageCompact()
        {
          AccountID = fyiDataRecord.AccountID,
          BatchID = fyiDataRecord.BatchID,
          MessageID = fyiDataRecord.MessageID,
          MetaID = fyiDataRecord.MetaID,
          SendMode = fyiDataRecord.SendMode,
          XMLMessageBody = fyiDataRecord.XMLMessageBody
        });
    }
    EmailDataRecords = (FYIMessageCompact[]) arrayList1.ToArray(typeof (FYIMessageCompact));
    SMSDataRecords = (FYIMessageCompact[]) arrayList2.ToArray(typeof (FYIMessageCompact));
  }

  private JangoMailService.EmailSendResult[] SendEmailMessagesWithSMTP2GO(
    FYIMessageCompact[] FYIDataRecords)
  {
    FYIDataAccess DataAccess = new FYIDataAccess();
    JangoMailService.EmailSendResult[] emailSendResultArray = new SMTP2GO().SendTransactionalEmails(FYIDataRecords);
    int index = 0;
    if (emailSendResultArray != null)
    {
      foreach (JangoMailService.EmailSendResult emailSendResult in emailSendResultArray)
      {
        DateTime resultDateTime = emailSendResult.ResultDateTime;
        string shortDateString = resultDateTime.ToShortDateString();
        resultDateTime = emailSendResult.ResultDateTime;
        string SendTime = resultDateTime.ToString("HH:mm");
        VoiShareOLTypes.SendStatusCode SendStatusCode = emailSendResult.SendStatus;
        if (emailSendResult.PXID.Equals("0") && FYIDataRecords[index].SendMode == 1)
          SendStatusCode = (VoiShareOLTypes.SendStatusCode) -1;
        this.LogSendStatusToDB(SendStatusCode, emailSendResult.FYIMessageID, emailSendResult.AccountID, shortDateString, SendTime, emailSendResult.PXID, DataAccess);
        ++index;
      }
    }
    return emailSendResultArray;
  }

  private void SendEmailMessagesWithJangoMail(FYIMessageCompact[] FYIDataRecords)
  {
    FYIDataAccess DataAccess = new FYIDataAccess();
    JangoMailService.EmailSendResult[] emailSendResultArray = this.SendMessages(FYIDataRecords);
    int index = 0;
    if (emailSendResultArray == null)
      return;
    foreach (JangoMailService.EmailSendResult emailSendResult in emailSendResultArray)
    {
      string shortDateString = emailSendResult.ResultDateTime.ToShortDateString();
      string SendTime = emailSendResult.ResultDateTime.ToString("HH:mm");
      VoiShareOLTypes.SendStatusCode SendStatusCode = emailSendResult.SendStatus;
      if (emailSendResult.PXID.Equals("0") && FYIDataRecords[index].SendMode == 1)
        SendStatusCode = (VoiShareOLTypes.SendStatusCode) -1;
      this.LogSendStatusToDB(SendStatusCode, emailSendResult.FYIMessageID, emailSendResult.AccountID, shortDateString, SendTime, emailSendResult.PXID, DataAccess);
      ++index;
    }
  }

  private SendGridAdapter.EmailSendResult[] SendEmailMessagesWithSendGrid(
    FYIMessageCompact[] FYIDataRecords)
  {
    ArrayList arrayList = new ArrayList();
    FYIDataAccess DataAccess = new FYIDataAccess();
    string SendDate = string.Empty;
    string empty = string.Empty;
    VoiShareOLTypes.SendStatusCode SendStatusCode = (VoiShareOLTypes.SendStatusCode) -1;
    XmlDocument xmlDocument = new XmlDocument();
    if (FYIDataRecords != null && FYIDataRecords.Length > 0)
    {
      foreach (FYIMessageCompact fyiDataRecord in FYIDataRecords)
      {
        SendGridAdapter.EmailSendResult emailSendResult = new SendGridAdapter.EmailSendResult();
        emailSendResult.AccountID = fyiDataRecord.AccountID;
        emailSendResult.FYIMessageID = fyiDataRecord.MessageID;
        emailSendResult.IsTransactionalMessage = true;
        emailSendResult.PXID = "0";
        emailSendResult.ResultDateTime = UserDateTime.NowUTC();
        fyiDataRecord.XMLMessageBody = SendGridAdapter.CleanseMessageBodyForJson(fyiDataRecord.XMLMessageBody);
        try
        {
          xmlDocument.LoadXml(fyiDataRecord.XMLMessageBody);
          SendGridAdapter.SendGridMessage SendGridMessage = new SendGridAdapter.SendGridMessage()
          {
            From = xmlDocument.SelectSingleNode("//fyimessage/email/from").InnerText,
            FromName = xmlDocument.SelectSingleNode("//fyimessage/email/fromname").InnerText,
            HtmlMessage = xmlDocument.SelectSingleNode("//fyimessage/email/htmlmessage").InnerText,
            PlainTextMessage = xmlDocument.SelectSingleNode("//fyimessage/email/plaintextmessage").InnerText,
            Subject = xmlDocument.SelectSingleNode("//fyimessage/email/subject").InnerText
          };
          SendGridMessage.SendgridSendOptions = $"fyimessageid:{(object) emailSendResult.FYIMessageID}#fromemail:{SendGridMessage.From}#accountid:{(object) emailSendResult.AccountID}";
          Encode64 encode64 = new Encode64();
          SendGridMessage.FromName = encode64.EncodeTo64(SendGridMessage.FromName);
          SendGridMessage.HtmlMessage = encode64.EncodeTo64(SendGridMessage.HtmlMessage);
          SendGridMessage.PlainTextMessage = encode64.EncodeTo64(SendGridMessage.PlainTextMessage);
          SendGridMessage.Subject = encode64.EncodeTo64(SendGridMessage.Subject);
          List<string> stringList = new List<string>();
          string innerText = xmlDocument.SelectSingleNode("//fyimessage/email/to").InnerText;
          stringList.Add(innerText);
          SendGridMessage.To = stringList;
          if (Utilities.IsValidEmail(innerText))
          {
            if (!SendGridAdapter.UserHasUnsubscribed(fyiDataRecord.AccountID, fyiDataRecord.MetaID, (VoiShareOLTypes.EndPointType) 1, innerText))
            {
              if (!SendGridAdapter.UserHasUnsubscribedInSendGrid(innerText))
              {
                string str = SendGridAdapter.SendGridSendTransactionalEmail(SendGridMessage);
                emailSendResult.PXID = "SGTEMP" + DateTime.Now.Ticks.ToString();
                emailSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) 2;
                SendDate = emailSendResult.ResultDateTime.ToShortDateString();
                empty = emailSendResult.ResultDateTime.ToString("HH:mm");
                SendStatusCode = emailSendResult.SendStatus;
                if (str.Equals("false") || str.StartsWith("exception"))
                {
                  emailSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -1;
                  Logger.Write("SendGridSendTransactionalEmail return value:" + str);
                }
              }
              else
                emailSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -2;
            }
            else
              emailSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -2;
          }
          else
            emailSendResult.SendStatus = (VoiShareOLTypes.SendStatusCode) -3;
          arrayList.Add((object) emailSendResult);
          this.LogSendStatusToDB(SendStatusCode, emailSendResult.FYIMessageID, emailSendResult.AccountID, SendDate, empty, emailSendResult.PXID, DataAccess);
        }
        catch (Exception ex)
        {
          Logger.Write("SendEmailMessagesWithSendGrid:");
          Logger.Write(ex.Message);
        }
      }
    }
    return (SendGridAdapter.EmailSendResult[]) arrayList.ToArray(typeof (SendGridAdapter.EmailSendResult));
  }

  private MandrillAdapter.EmailSendResult[] SendEmailMessagesWithMandrill(
    FYIMessageCompact[] FYIDataRecords)
  {
    FYIDataAccess DataAccess = new FYIDataAccess();
    MandrillAdapter.EmailSendResult[] emailSendResultArray = new MandrillAdapter().SendTransactionalEmails(FYIDataRecords);
    int index = 0;
    if (emailSendResultArray != null)
    {
      foreach (MandrillAdapter.EmailSendResult emailSendResult in emailSendResultArray)
      {
        DateTime resultDateTime = emailSendResult.ResultDateTime;
        string shortDateString = resultDateTime.ToShortDateString();
        resultDateTime = emailSendResult.ResultDateTime;
        string SendTime = resultDateTime.ToString("HH:mm");
        VoiShareOLTypes.SendStatusCode SendStatusCode = emailSendResult.SendStatus;
        if (emailSendResult.PXID.Equals("0") && FYIDataRecords[index].SendMode == 1)
          SendStatusCode = (VoiShareOLTypes.SendStatusCode) -1;
        this.LogSendStatusToDB(SendStatusCode, emailSendResult.FYIMessageID, emailSendResult.AccountID, shortDateString, SendTime, emailSendResult.PXID, DataAccess);
        ++index;
      }
    }
    return emailSendResultArray;
  }

  private void SendEmailMessages(
    FYIMessageCompact[] FYIDataRecords,
    VoiShareOLTypes.EndPointType EndPointType)
  {
    if (FYIDataRecords.Length <= 0)
      return;
    try
    {
      this.SendEmailMessagesWithSendGrid(FYIDataRecords);
    }
    catch
    {
      this.SendEmailMessagesWithSMTP2GO(FYIDataRecords);
      Logger.Write(FYIDataRecords.Length.ToString() + " messages sent via SMTP2GO.");
    }
    finally
    {
    }
  }

  private bool SendMessageTosimplyFYIQueueApi(int AccountID, int FYIMessageID)
  {
    bool flag = false;
    string idByFyiMessageId = new FYIDataAccess().GetIPNOriginatorTransactionIDByFYIMessageID(FYIMessageID);
    if (!string.IsNullOrEmpty(idByFyiMessageId))
    {
      string str1 = $"{$"{{\"OriginatorTransactionID\":\"{idByFyiMessageId}\""}, \"MerchantAccountID\":{AccountID.ToString()}" + ", \"EventType\":0}";
      string str2 = string.Empty;
      string address = "https://microsoft-apiappe8b966588f914844b26775bbcc967924.azurewebsites.net/api/messages/send";
      WebClient webClient = new WebClient();
      webClient.Headers.Add("Content-Type", "application/json");
      RemoteCertificateValidationCallback validationCallback = (RemoteCertificateValidationCallback) ((param0, param1, param2, param3) => true);
      try
      {
        ServicePointManager.ServerCertificateValidationCallback += validationCallback;
        str2 = webClient.UploadString(address, str1);
        ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
        flag = true;
        Logger.Write($"Sent message (ID: {(object) FYIMessageID}, TXID: {idByFyiMessageId}) to simplyFYIQueueApi.");
        EMailSender.NotifyAdmin($"Sent message (ID: {(object) FYIMessageID}, TXID: {idByFyiMessageId}) to simplyFYIQueueApi.");
      }
      catch (Exception ex)
      {
        ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
        Logger.Write($"Error writing message (ID: {(object) FYIMessageID}, TXID: {idByFyiMessageId}) to simplyFYIQueueApi.<br/>{ex.Message}");
        EMailSender.NotifyAdmin($"Error writing message (ID: {(object) FYIMessageID}, TXID: {idByFyiMessageId}) to simplyFYIQueueApi.<br/>{ex.Message}");
        this.WriteQueueMessageToLocalFile(AccountID, FYIMessageID, str1);
      }
    }
    return flag;
  }

  private void WriteQueueMessageToLocalFile(int AccountID, int FYIMessageID, string QueueMessage)
  {
    FileStream fileStream = (FileStream) null;
    string path1 = ConfigurationManager.AppSettings.Get("simplyFYIQueueLocalFilePath");
    if (!Directory.Exists(path1))
      Directory.CreateDirectory(path1);
    string path2 = $"{path1}\\{AccountID.ToString()}-{FYIMessageID.ToString()}.txt";
    if (!System.IO.File.Exists(path2))
      fileStream = System.IO.File.Create(path2);
    StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
    streamWriter.WriteLine(QueueMessage);
    streamWriter.Close();
  }

  private void SendVoiceMessages(FYIMessageCompact[] FYIDataRecords)
  {
    if (FYIDataRecords.Length <= 0)
      return;
    FYIDataAccess DataAccess = new FYIDataAccess();
    TwilioService.TwilioSendResult[] twilioSendResultArray = this.SendVoiceMessagesInternal(FYIDataRecords);
    if (twilioSendResultArray != null)
    {
      foreach (TwilioService.TwilioSendResult twilioSendResult in twilioSendResultArray)
      {
        string shortDateString = twilioSendResult.ResultDateTime.ToShortDateString();
        string SendTime = twilioSendResult.ResultDateTime.ToString("HH:mm");
        this.LogSendStatusToDB(twilioSendResult.SendStatus, twilioSendResult.FYIMessageID, twilioSendResult.AccountID, shortDateString, SendTime, twilioSendResult.PXID, DataAccess);
      }
    }
  }

  private void SendSMSMessages(FYIMessageCompact[] FYIDataRecords)
  {
    if (FYIDataRecords.Length <= 0)
      return;
    FYIDataAccess DataAccess = new FYIDataAccess();
    TwilioService.TwilioSendResult[] twilioSendResultArray = this.SendSMSMessagesInternal(FYIDataRecords);
    if (twilioSendResultArray != null)
    {
      foreach (TwilioService.TwilioSendResult twilioSendResult in twilioSendResultArray)
      {
        string shortDateString = twilioSendResult.ResultDateTime.ToShortDateString();
        string SendTime = twilioSendResult.ResultDateTime.ToString("HH:mm");
        this.LogSendStatusToDB(twilioSendResult.SendStatus, twilioSendResult.FYIMessageID, twilioSendResult.AccountID, shortDateString, SendTime, twilioSendResult.PXID, DataAccess);
      }
    }
  }

  private string[] GetTodaysUnopenedMessagesPXIDs()
  {
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    string shortDateString1 = UserDateTime.NowUTC().ToShortDateString();
    DateTime dateTime = UserDateTime.NowUTC();
    dateTime = dateTime.AddDays(1.0);
    string shortDateString2 = dateTime.ToShortDateString();
    return fyiDataAccess.GetUnopenedMessagesPXIDs(DateTime.Parse(shortDateString1), DateTime.Parse(shortDateString2));
  }

  private bool IsJangoMailPXID(string PXID)
  {
    bool flag = false;
    if (PXID.Length < 32 /*0x20*/ && Utilities.IsNumeric(PXID))
      flag = true;
    return flag;
  }

  private void UpdateMessageOpensAndBounces()
  {
  }

  private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    Logger.Write("heartbeat");
    try
    {
      try
      {
        this.SendQueuedMessage(false);
        this.UpdateMessageOpensAndBounces();
      }
      catch (Exception ex)
      {
        string str = $"Dispatch Service exception [:{DateTime.Now.ToString()}]\r\n" + ex.Message;
        Logger.Write(str);
        EMailSender.NotifyAdmin(str);
      }
      finally
      {
        this.m_TaskTimer.Start();
        this.SetLastTimeRunTimestamp();
      }
    }
    catch (Exception ex)
    {
      Logger.Write($"Dispatch Service OUTER exception handler [:{DateTime.Now.ToString()}]\r\n" + ex.Message);
    }
  }

  private void SetLastTimeRunTimestamp()
  {
    new FYIDataAccess().SetDispatchServiceLastRunDateTime(DateTime.Now.ToUniversalTime());
  }

  private void button2_Click(object sender, EventArgs e)
  {
  }

  private ArrayList ExtractTokens(string MessageTemplate)
  {
    ArrayList tokens = new ArrayList();
    bool flag = false;
    int startIndex1 = 0;
    while (!flag)
    {
      int startIndex2 = MessageTemplate.Substring(startIndex1).IndexOf('{');
      if (startIndex2 > 0)
      {
        int num = MessageTemplate.Substring(startIndex1).IndexOf('}');
        tokens.Add((object) MessageTemplate.Substring(startIndex1).Substring(startIndex2, num - startIndex2 + 1));
        startIndex1 += num + 1;
      }
      else
        flag = true;
    }
    return tokens;
  }

  private string GetTokenValue(string FieldValue, string FieldFilter)
  {
    string tokenValue = (string) null;
    if (FieldValue != null && FieldFilter != null)
    {
      DateTime dateTime;
      if (FieldFilter.Equals("Date"))
      {
        dateTime = DateTime.Parse(FieldValue);
        tokenValue = dateTime.ToShortDateString();
      }
      if (FieldFilter.Equals("Time"))
      {
        dateTime = DateTime.Parse(FieldValue);
        tokenValue = dateTime.ToShortTimeString();
      }
    }
    return tokenValue;
  }

  private string BuildMessageBody(string MessageTemplate, DataRow Row)
  {
    string str1 = (string) null;
    foreach (string token in this.ExtractTokens(MessageTemplate))
    {
      string str2;
      string FieldFilter;
      if (token.IndexOf("%") > 0)
      {
        str2 = token.Substring(1, token.IndexOf("%") - 1);
        string str3 = token.Substring(token.IndexOf("%") + 1);
        FieldFilter = str3.Substring(0, str3.Length - 2);
      }
      else
      {
        str2 = token.Substring(1, token.Length - 2);
        FieldFilter = (string) null;
      }
      if (Row["ows_" + str2].ToString() != "")
      {
        str1 = (string) null;
        string newValue = FieldFilter == null ? Row["ows_" + str2].ToString() : this.GetTokenValue(Row["ows_" + str2].ToString(), FieldFilter);
        MessageTemplate = MessageTemplate.Replace(token, newValue);
      }
      else
        MessageTemplate = "";
    }
    return MessageTemplate;
  }

  private string ReplaceTokens(string FYIMessage, DataRow Row)
  {
    foreach (string token in this.ExtractTokens(FYIMessage))
    {
      string str1;
      string FieldFilter;
      if (token.IndexOf("%") > 0)
      {
        string str2 = token.Substring(1, token.IndexOf("%") - 1);
        string str3 = token.Substring(token.IndexOf("-") + 1);
        string str4 = str3.Substring(0, str3.Length - 1);
        str1 = $"{str2}-{str4}";
        string str5 = token.Substring(token.IndexOf("%") + 1);
        FieldFilter = str5.Substring(0, str5.IndexOf("%"));
      }
      else
      {
        str1 = token.Substring(1, token.Length - 2);
        FieldFilter = (string) null;
      }
      if (Row["ows_" + str1].ToString() != "")
      {
        string newValue = FieldFilter == null ? Row["ows_" + str1].ToString() : this.GetTokenValue(Row["ows_" + str1].ToString(), FieldFilter);
        FYIMessage = FYIMessage.Replace(token, newValue);
      }
      else
        FYIMessage = "";
    }
    return FYIMessage;
  }

  private void LogSendStatusToDB(
    VoiShareOLTypes.SendStatusCode SendStatusCode,
    int MessageID,
    int AccountID,
    string SendDate,
    string SendTime,
    string PXID,
    FYIDataAccess DataAccess)
  {
    string str1 = (string) null;
    int int32;
    string str2;
    if (SendStatusCode.Equals((object) string.Empty) || SendStatusCode != 1 && SendStatusCode != null && SendStatusCode != 2)
    {
      int32 = Convert.ToInt32((object) SendStatusCode);
      if (str1 == null)
        str2 = $"{UserDateTime.NowUTC().ToString()}- ID: {MessageID.ToString()} [Acct: {(object) AccountID}] Email FAILED.";
      else
        str2 = $"{str1}\r\n{UserDateTime.NowUTC().ToString()}- ID: {MessageID.ToString()} [Acct: {(object) AccountID}] Email FAILED.";
    }
    else
    {
      int32 = Convert.ToInt32((object) (VoiShareOLTypes.SendStatusCode) 1);
      if (str1 == null)
        str2 = $"{UserDateTime.NowUTC().ToString()}- ID: {MessageID.ToString()} [Acct: {(object) AccountID}] Email SENT.";
      else
        str2 = $"{str1}\r\n{UserDateTime.NowUTC().ToString()}- ID: {MessageID.ToString()} [Acct: {(object) AccountID}] Email SENT.";
    }
    DataAccess.UpdateFYIMessageActualSendDateTimeAndStatus(MessageID, SendDate, SendTime, PXID, int32);
  }

  private void LogSendStatusToShadowDB(
    VoiShareOLTypes.SendStatusCode SendStatusCode,
    int MessageID,
    int AccountID,
    string SendDate,
    string SendTime,
    string PXID)
  {
    int int32 = Convert.ToInt32((object) SendStatusCode);
    new FYIDataAccessShadow().UpdateFYIMessageActualSendDateTimeAndStatus(MessageID, SendDate, SendTime, PXID, int32);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.FYIDispatchServiceController = new ServiceController();
    this.bgWorker = new BackgroundWorker();
    this.FYIDispatchServiceController.ServiceName = "FYI Dispatch Service";
    this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
    this.ServiceName = nameof (FYIDispatchService);
  }

  public enum SendStatusCode
  {
    SENT = 1,
    FAILED = 2,
  }
}
