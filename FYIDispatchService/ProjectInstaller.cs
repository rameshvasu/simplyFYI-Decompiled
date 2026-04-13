// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.ProjectInstaller
// Assembly: FYIDispatchService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95564A78-F0F7-4554-BAAC-3D7128A876B8
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\FYIDispatchService.exe

using System;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
  private IContainer components = (IContainer) null;
  private ServiceProcessInstaller FYIDispatcherProcessInstaller;
  private ServiceInstaller FYIDispatchService;
  private ServiceInstaller FYIPingService;

  public ProjectInstaller() => this.InitializeComponent();

  private void FYIDispatchServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
  {
  }

  private void FYIDispatcherProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
  {
  }

  private string GetConfigurationValue(string key)
  {
    System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetAssembly(typeof (ProjectInstaller)).Location);
    if (configuration.AppSettings.Settings[key] != null)
      return configuration.AppSettings.Settings[key].Value;
    throw new IndexOutOfRangeException("Settings collection does not contain the requested key: " + key);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.FYIDispatcherProcessInstaller = new ServiceProcessInstaller();
    this.FYIDispatchService = new ServiceInstaller();
    this.FYIDispatcherProcessInstaller.Account = ServiceAccount.LocalService;
    this.FYIDispatcherProcessInstaller.Password = (string) null;
    this.FYIDispatcherProcessInstaller.Username = (string) null;
    this.FYIDispatcherProcessInstaller.AfterInstall += new InstallEventHandler(this.FYIDispatcherProcessInstaller_AfterInstall);
    this.FYIDispatchService.Description = "FYI Service that manages the dispatch of messages.";
    this.FYIDispatchService.DisplayName = this.GetConfigurationValue("FYIDispatchServiceDisplayName");
    this.FYIDispatchService.ServiceName = this.GetConfigurationValue("FYIDispatchServiceName");
    this.FYIDispatchService.AfterInstall += new InstallEventHandler(this.FYIDispatchServiceInstaller_AfterInstall);
    this.Installers.AddRange(new Installer[2]
    {
      (Installer) this.FYIDispatcherProcessInstaller,
      (Installer) this.FYIDispatchService
    });
  }
}
