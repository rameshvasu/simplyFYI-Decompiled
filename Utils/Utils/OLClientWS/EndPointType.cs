// Decompiled with JetBrains decompiler
// Type: Utils.OLClientWS.EndPointType
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace Utils.OLClientWS;

[GeneratedCode("System.Xml", "4.0.30319.233")]
[XmlType(Namespace = "http://tempuri.org/")]
[Serializable]
public enum EndPointType
{
  Email,
  Phone,
  SMS,
}
