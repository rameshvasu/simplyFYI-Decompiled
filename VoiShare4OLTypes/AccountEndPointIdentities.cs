// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.Types.AccountEndPointIdentities
// Assembly: VoiShare4OLTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4300920-3BDD-4B26-9880-36B6DF8D0959
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\VoiShare4OLTypes.dll

using System;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive.Types;

[Serializable]
public class AccountEndPointIdentities
{
  private int m_AccountID;
  private EndPointIdentity[] m_EndPointIdentities;

  public int AccountID
  {
    set => this.m_AccountID = value;
    get => this.m_AccountID;
  }

  public EndPointIdentity[] EndPointIdentities
  {
    set => this.m_EndPointIdentities = value;
    get => this.m_EndPointIdentities;
  }
}
