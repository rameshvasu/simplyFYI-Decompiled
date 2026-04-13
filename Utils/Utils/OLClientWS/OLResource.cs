// Decompiled with JetBrains decompiler
// Type: Utils.OLClientWS.OLResource
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace Utils.OLClientWS;

[XmlType(Namespace = "http://tempuri.org/")]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.233")]
[DebuggerStepThrough]
[Serializable]
public class OLResource
{
  private string nameField;
  private OfficeLiveResourceType typeField;
  private string urlField;

  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  public OfficeLiveResourceType Type
  {
    get => this.typeField;
    set => this.typeField = value;
  }

  public string Url
  {
    get => this.urlField;
    set => this.urlField = value;
  }
}
