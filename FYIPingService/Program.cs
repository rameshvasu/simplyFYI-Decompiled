// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.Program
// Assembly: FYIPingService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29D7C420-693D-4E14-9F40-61A33DF9CC18
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIPingService\FYIPingService.exe

using System.ServiceProcess;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

internal static class Program
{
  private static void Main()
  {
    ServiceBase.Run(new ServiceBase[1]
    {
      (ServiceBase) new FYIPingService()
    });
  }
}
