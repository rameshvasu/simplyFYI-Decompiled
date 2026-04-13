// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.Types.EndPointIdentity
// Assembly: VoiShare4OLTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4300920-3BDD-4B26-9880-36B6DF8D0959
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\VoiShare4OLTypes.dll

using System;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive.Types;

[Serializable]
public class EndPointIdentity
{
  private VoiShareOLTypes.EndPointType m_EndPointType;
  private string m_SenderName;
  private string m_SenderAddress;

  public VoiShareOLTypes.EndPointType EndPointType
  {
    set => this.m_EndPointType = value;
    get => this.m_EndPointType;
  }

  public string SenderName
  {
    set => this.m_SenderName = value;
    get => this.m_SenderName;
  }

  public string SenderAddress
  {
    set => this.m_SenderAddress = value;
    get => this.m_SenderAddress;
  }
}
