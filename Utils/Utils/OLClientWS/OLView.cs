// Decompiled with JetBrains decompiler
// Type: Utils.OLClientWS.OLView
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

[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.233")]
[XmlType(Namespace = "http://tempuri.org/")]
[DebuggerStepThrough]
[Serializable]
public class OLView
{
  private string nameField;
  private string idField;
  private bool isDefaultViewField;

  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  public string ID
  {
    get => this.idField;
    set => this.idField = value;
  }

  public bool IsDefaultView
  {
    get => this.isDefaultViewField;
    set => this.isDefaultViewField = value;
  }
}
