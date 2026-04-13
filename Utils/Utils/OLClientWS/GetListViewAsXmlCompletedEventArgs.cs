// Decompiled with JetBrains decompiler
// Type: Utils.OLClientWS.GetListViewAsXmlCompletedEventArgs
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

#nullable disable
namespace Utils.OLClientWS;

[DebuggerStepThrough]
[GeneratedCode("System.Web.Services", "4.0.30319.1")]
[DesignerCategory("code")]
public class GetListViewAsXmlCompletedEventArgs : AsyncCompletedEventArgs
{
  private object[] results;

  internal GetListViewAsXmlCompletedEventArgs(
    object[] results,
    Exception exception,
    bool cancelled,
    object userState)
    : base(exception, cancelled, userState)
  {
    this.results = results;
  }

  public XmlNode Result
  {
    get
    {
      this.RaiseExceptionIfNecessary();
      return (XmlNode) this.results[0];
    }
  }
}
