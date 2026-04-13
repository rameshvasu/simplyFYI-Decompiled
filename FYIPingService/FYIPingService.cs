// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.FYIPingService
// Assembly: FYIPingService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29D7C420-693D-4E14-9F40-61A33DF9CC18
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIPingService\FYIPingService.exe

using RameshInnovation.VoiShare.OfficeLive;
using RameshInnovation.VoiShare.Services.Adapters;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using Utils;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

public class FYIPingService : ServiceBase
{
  private Timer m_TaskTimer = (Timer) null;
  private IContainer components = (IContainer) null;
  private BackgroundWorker bgWorker;

  public FYIPingService() => this.InitializeComponent();

  protected override void OnStart(string[] args)
  {
    this.m_TaskTimer = new Timer();
    string s1 = ConfigurationManager.AppSettings.Get("TimerInterval");
    string s2 = ConfigurationManager.AppSettings.Get("PingTimerIntervalFactor");
    int num1 = 5000;
    if (s1 != null)
      num1 = int.Parse(s1);
    int num2 = num1 * int.Parse(s2);
    this.m_TaskTimer.Elapsed += new ElapsedEventHandler(this.m_TaskTimer_Elapsed);
    this.m_TaskTimer.Interval = (double) num2;
    this.m_TaskTimer.Enabled = true;
    this.m_TaskTimer.Start();
  }

  protected override void OnStop()
  {
  }

  private void m_TaskTimer_Elapsed(object sender, ElapsedEventArgs e)
  {
    this.m_TaskTimer.Stop();
    this.bgWorker.RunWorkerAsync();
  }

  private DateTime GetDispatchServiceLastRunDateFromFileSystem()
  {
    string newValue = DateTime.Now.ToString("MMddyyyy");
    return File.GetLastWriteTime(ConfigurationManager.AppSettings.Get("DispatchServiceLogFile").Replace("MMDDYYYY", newValue));
  }

  private void SendComms(string Subject, string MessageBody)
  {
    string To = "ramesh@rameshinnovation.com";
    new PushoverAdapter().Notify("u52wVSuUQ5wWBnHi5uoT8JKg9RrK8j", Subject, MessageBody);
    EMailSender.SendMail(Subject, To, "admin@simplyfyi.com", "simplyFYI Admin", MessageBody);
    EMailSender.SendMail(string.Empty, "9162766586@messaging.sprintpcs.com", "admin@simplyfyi.com", "simplyFYI Admin", MessageBody);
  }

  private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    try
    {
      DateTime dateFromFileSystem = this.GetDispatchServiceLastRunDateFromFileSystem();
      string str1 = ConfigurationManager.AppSettings.Get("TimerInterval");
      string str2 = ConfigurationManager.AppSettings.Get("PingTimerIntervalFactor");
      long num1 = (DateTime.Now.Ticks - dateFromFileSystem.Ticks) / 10000L;
      long num2 = Convert.ToInt64(str1) * Convert.ToInt64(str2);
      try
      {
        if (num1 <= num2)
          return;
        Logger.Write("MilliSecondsDiff = " + num1.ToString());
        this.SendComms("RESTART SERVICE - FYI Disptach  Service down", "FYI Disptach Service down -> RESTART SERVICE. Last run at UTC " + dateFromFileSystem.ToUniversalTime().ToString());
      }
      catch (Exception ex)
      {
        string str3 = "FYI Ping encountered an exception:\r\n\r\n" + ex.Message;
        string Subject = "ERROR - FYI Ping Service";
        Logger.Write(str3);
        this.SendComms(Subject, str3);
      }
      finally
      {
        this.m_TaskTimer.Start();
      }
    }
    catch (Exception ex)
    {
      string str = "FYI Ping encountered an exception:\r\n\r\n" + ex.Message;
      string Subject = "ERROR - FYI Ping Service";
      Logger.Write(str);
      this.SendComms(Subject, str);
      this.m_TaskTimer.Start();
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.bgWorker = new BackgroundWorker();
    this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
    this.ServiceName = nameof (FYIPingService);
  }
}
