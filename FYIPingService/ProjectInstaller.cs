// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.ProjectInstaller
// Assembly: FYIPingService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29D7C420-693D-4E14-9F40-61A33DF9CC18
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIPingService\FYIPingService.exe

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
  private IContainer components = (IContainer) null;
  private ServiceProcessInstaller serviceProcessInstaller1;
  private ServiceInstaller FYIPingService;

  public ProjectInstaller() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.serviceProcessInstaller1 = new ServiceProcessInstaller();
    this.FYIPingService = new ServiceInstaller();
    this.serviceProcessInstaller1.Account = ServiceAccount.LocalService;
    this.serviceProcessInstaller1.Password = (string) null;
    this.serviceProcessInstaller1.Username = (string) null;
    this.FYIPingService.Description = "This FYI service monitors the health of the FYI Dispatch Service.";
    this.FYIPingService.DisplayName = "FYI Ping Service";
    this.FYIPingService.ServiceName = "FYIPingService";
    this.Installers.AddRange(new Installer[2]
    {
      (Installer) this.serviceProcessInstaller1,
      (Installer) this.FYIPingService
    });
  }
}
