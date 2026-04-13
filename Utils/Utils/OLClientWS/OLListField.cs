// Decompiled with JetBrains decompiler
// Type: Utils.OLClientWS.OLListField
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
[GeneratedCode("System.Xml", "4.0.30319.233")]
[DesignerCategory("code")]
[DebuggerStepThrough]
[Serializable]
public class OLListField
{
  private string nameField;
  private string displayNameField;
  private string idField;
  private string typeField;
  private bool isLookUpFieldField;
  private string lookupListUniqueIDField;
  private bool isSelectedField;
  private bool isEndPointField;
  private EndPointType endPointTypeField;

  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  public string ID
  {
    get => this.idField;
    set => this.idField = value;
  }

  public string Type
  {
    get => this.typeField;
    set => this.typeField = value;
  }

  public bool IsLookUpField
  {
    get => this.isLookUpFieldField;
    set => this.isLookUpFieldField = value;
  }

  public string LookupListUniqueID
  {
    get => this.lookupListUniqueIDField;
    set => this.lookupListUniqueIDField = value;
  }

  public bool IsSelected
  {
    get => this.isSelectedField;
    set => this.isSelectedField = value;
  }

  public bool IsEndPoint
  {
    get => this.isEndPointField;
    set => this.isEndPointField = value;
  }

  public EndPointType EndPointType
  {
    get => this.endPointTypeField;
    set => this.endPointTypeField = value;
  }
}
