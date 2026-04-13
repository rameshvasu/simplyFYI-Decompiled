// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.Types.AccountOptions
// Assembly: VoiShare4OLTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4300920-3BDD-4B26-9880-36B6DF8D0959
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\VoiShare4OLTypes.dll

using System;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive.Types;

[Serializable]
public class AccountOptions
{
  private string m_FromEmailName = (string) null;
  private bool m_IngnoreOptIn = false;
  private string m_CompanyLogoUrl = (string) null;
  private string m_FooterText = (string) null;

  public string FromEmailName
  {
    set => this.m_FromEmailName = value;
    get => this.m_FromEmailName;
  }

  public bool IngnoreOptIn
  {
    set => this.m_IngnoreOptIn = value;
    get => this.m_IngnoreOptIn;
  }

  public string CompanyLogoUrl
  {
    set => this.m_CompanyLogoUrl = value;
    get => this.m_CompanyLogoUrl;
  }

  public string FooterText
  {
    set => this.m_FooterText = value;
    get => this.m_FooterText;
  }
}
