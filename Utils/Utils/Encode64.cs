// Decompiled with JetBrains decompiler
// Type: Utils.Encode64
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.Text;

#nullable disable
namespace Utils;

public class Encode64
{
  public string DecodeFrom64(string stringToDecrypt)
  {
    return Encoding.UTF8.GetString(Convert.FromBase64String(stringToDecrypt));
  }

  public string EncodeTo64(string stringToEncrypt)
  {
    return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncrypt));
  }
}
