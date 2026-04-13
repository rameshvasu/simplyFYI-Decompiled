// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.Adapters._message
// Assembly: MandrillAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03B45742-E072-49C4-8BB0-818A9396681F
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\MandrillAdapter.dll

#nullable disable
namespace RameshInnovation.VoiShare.Services.Adapters;

public struct _message
{
  public string html { set; get; }

  public string text { set; get; }

  public string subject { set; get; }

  public string from_email { set; get; }

  public string from_name { set; get; }

  public _to[] to { set; get; }

  public _headers headers { set; get; }

  public string important { set; get; }

  public string track_opens { set; get; }

  public string track_clicks { set; get; }
}
