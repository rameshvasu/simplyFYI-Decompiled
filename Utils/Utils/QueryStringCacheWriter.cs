// Decompiled with JetBrains decompiler
// Type: Utils.QueryStringCacheWriter
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.Configuration;
using System.Net;
using System.Xml;
using Utils.OLClientWS;

#nullable disable
namespace Utils;

public class QueryStringCacheWriter
{
  public static string WriteQueryStringToCache(string query)
  {
    string empty = string.Empty;
    string cache;
    switch (ConfigurationManager.AppSettings.Get("UseHttpGet"))
    {
      case null:
        cache = QueryStringCacheWriter.WriteQueryStringToCache_SOAP(query);
        goto label_5;
      case "1":
        cache = QueryStringCacheWriter.WriteQueryStringToCache_HTTPGET(query);
        break;
      default:
        cache = QueryStringCacheWriter.WriteQueryStringToCache_SOAP(query);
        break;
    }
label_5:
    return cache;
  }

  private static string WriteQueryStringToCache_SOAP(string query)
  {
    string empty = string.Empty;
    ClientConnectWS clientConnectWs = new ClientConnectWS();
    string cache;
    try
    {
      cache = clientConnectWs.WriteQueryStringToCache(query);
    }
    catch (Exception ex)
    {
      throw new ApplicationException("Error WC0002 has occurred");
    }
    return cache;
  }

  private static string WriteQueryStringToCache_HTTPGET(string query)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string innerText;
    try
    {
      WebClient webClient = new WebClient();
      string str = ConfigurationManager.AppSettings.Get("olcws");
      webClient.BaseAddress = !(str.Substring(str.Length - 1) != "/") ? str : str + "/";
      string xml = webClient.DownloadString("WriteQueryStringToCache?QueryString=" + query);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(xml);
      innerText = xmlDocument.GetElementsByTagName("string")[0].InnerText;
    }
    catch (Exception ex)
    {
      throw new ApplicationException("Error WC0001 has occurred");
    }
    return innerText;
  }
}
