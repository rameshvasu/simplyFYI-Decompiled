// Decompiled with JetBrains decompiler
// Type: FYITransactionMonitorService.Program
// Assembly: FYITransactionMonitorService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BFDFF65-9CAF-4A03-971B-47A6FFC4492C
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\TransactionMonitoringService\FYITransactionMonitorService.exe

using System.ServiceProcess;

#nullable disable
namespace FYITransactionMonitorService;

internal static class Program
{
  private static void Main()
  {
    ServiceBase.Run(new ServiceBase[1]
    {
      (ServiceBase) new FYIPayPalTransactionMonitorService()
    });
  }
}
