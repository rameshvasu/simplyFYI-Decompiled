// Decompiled with JetBrains decompiler
// Type: SomeNamespace.PayPalIPNHandler
// Assembly: PayPalIPNHandler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B89D298-7395-4305-9308-85895A08B41A
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\PayPalIPNHandler.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

#nullable disable
namespace SomeNamespace;

public class PayPalIPNHandler : IHttpHandler
{
  public void ProcessRequest(HttpContext context)
  {
    string empty = string.Empty;
    string str1 = Encoding.ASCII.GetString(context.Request.BinaryRead(HttpContext.Current.Request.ContentLength));
    HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr");
    httpWebRequest.Method = "POST";
    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
    NameValueCollection nameValueCollection = new NameValueCollection();
    string[] strArray1 = str1.Split('&');
    char[] chArray = new char[1]{ '=' };
    for (int index = 0; index < strArray1.Length; ++index)
    {
      string[] strArray2 = strArray1[index].Split(chArray);
      if (strArray2.Length > 1)
        nameValueCollection.Add(strArray2[0], HttpUtility.UrlDecode(strArray2[1]));
    }
    string str2 = str1 + "&cmd=_notify-validate";
    httpWebRequest.ContentLength = (long) str2.Length;
    try
    {
      StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream(), Encoding.ASCII);
      streamWriter.Write(str2);
      streamWriter.Close();
      StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream());
      string end = streamReader.ReadToEnd();
      streamReader.Close();
      switch (end)
      {
      }
    }
    catch (Exception ex)
    {
    }
  }

  public bool IsReusable => false;
}
