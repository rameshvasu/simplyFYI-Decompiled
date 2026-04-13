// Decompiled with JetBrains decompiler
// Type: Utils.Properties.Settings
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Utils.Properties;

[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
[CompilerGenerated]
internal sealed class Settings : ApplicationSettingsBase
{
  private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

  public static Settings Default
  {
    get
    {
      Settings defaultInstance = Settings.defaultInstance;
      return defaultInstance;
    }
  }

  [SpecialSetting(SpecialSetting.WebServiceUrl)]
  [DefaultSettingValue("http://www.simplyfyiapps.com/service/olsb/OLClientConnectWS.asmx")]
  [ApplicationScopedSetting]
  [DebuggerNonUserCode]
  public string Utils_OLClientWS_ClientConnectWS
  {
    get => (string) this[nameof (Utils_OLClientWS_ClientConnectWS)];
  }
}
