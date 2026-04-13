// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.Program
// Assembly: FYIDispatchService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95564A78-F0F7-4554-BAAC-3D7128A876B8
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\FYIDispatchService.exe

using System.ServiceProcess;

#nullable disable
namespace RameshInnovation.VoiShare.Services;

internal static class Program
{
  private static void Main()
  {
    ServiceBase.Run(new ServiceBase[1]
    {
      (ServiceBase) new FYIDispatchService()
    });
  }
}
