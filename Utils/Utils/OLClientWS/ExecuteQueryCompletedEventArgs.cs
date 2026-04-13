// Decompiled with JetBrains decompiler
// Type: Utils.OLClientWS.ExecuteQueryCompletedEventArgs
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Utils.OLClientWS;

[DesignerCategory("code")]
[GeneratedCode("System.Web.Services", "4.0.30319.1")]
[DebuggerStepThrough]
public class ExecuteQueryCompletedEventArgs : AsyncCompletedEventArgs
{
  private object[] results;

  internal ExecuteQueryCompletedEventArgs(
    object[] results,
    Exception exception,
    bool cancelled,
    object userState)
    : base(exception, cancelled, userState)
  {
    this.results = results;
  }

  public DataSet Result
  {
    get
    {
      this.RaiseExceptionIfNecessary();
      return (DataSet) this.results[0];
    }
  }
}
