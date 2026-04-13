// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.PayPalHelper
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive;

public class PayPalHelper
{
  public static DateTime MapTransactionTimeZoneToUTC(
    string TransactionDate,
    string TransactionTime,
    string TransactionTimeZone)
  {
    DateTime dateTime = new DateTime();
    TransactionDate = Utilities.StripLeadingAndTrailingQuotes(TransactionDate);
    TransactionTime = Utilities.StripLeadingAndTrailingQuotes(TransactionTime);
    TransactionTimeZone = Utilities.StripLeadingAndTrailingQuotes(TransactionTimeZone);
    UserDateTime.TZ UserTimeZone = UserDateTime.TZ.GMT;
    DateTime utc;
    if (!TransactionTimeZone.Trim().Equals("UTC"))
    {
      if (TransactionTimeZone.Trim().Equals("PST") || TransactionTimeZone.Trim().Equals("PDT"))
        UserTimeZone = UserDateTime.TZ.USPacificStandard;
      else if (TransactionTimeZone.Trim().Equals("EST") || TransactionTimeZone.Trim().Equals("EDT"))
        UserTimeZone = UserDateTime.TZ.USEasternStandard;
      else if (TransactionTimeZone.Trim().Equals("MST") || TransactionTimeZone.Trim().Equals("MDT"))
        UserTimeZone = UserDateTime.TZ.USMountainStandard;
      else if (TransactionTimeZone.Trim().Equals("AST") || TransactionTimeZone.Trim().Equals("ADT"))
        UserTimeZone = UserDateTime.TZ.USAtlanticStandard;
      else if (TransactionTimeZone.Trim().Equals("CST") || TransactionTimeZone.Trim().Equals("CDT"))
        UserTimeZone = UserDateTime.TZ.USCentralStandard;
      utc = UserDateTime.UserTimeToUTC(DateTime.Parse($"{TransactionDate} {TransactionTime}"), UserTimeZone);
    }
    else
      utc = DateTime.Parse($"{TransactionDate} {TransactionTime}");
    return utc;
  }

  public static string UrlDecode(string Url) => HttpUtility.UrlDecode(Url);

  public static string UrlEncode(string Url) => HttpUtility.UrlEncode(Url);

  private static string ConvertTimeFromCSVToIPNFormat(string CSVFormat)
  {
    string str = string.Empty;
    string[] strArray = CSVFormat.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    switch (strArray[0])
    {
      case "1":
      case "01":
        str = "Jan";
        break;
      case "2":
      case "02":
        str = "Feb";
        break;
      case "3":
      case "03":
        str = "Mar";
        break;
      case "4":
      case "04":
        str = "Apr";
        break;
      case "5":
      case "05":
        str = "May";
        break;
      case "6":
      case "06":
        str = "Jun";
        break;
      case "7":
      case "07":
        str = "Jul";
        break;
      case "8":
      case "08":
        str = "Aug";
        break;
      case "9":
      case "09":
        str = "Sep";
        break;
      case "10":
        str = "Oct";
        break;
      case "11":
        str = "Nov";
        break;
      case "12":
        str = "Dec";
        break;
    }
    return $"{str} {strArray[1]}, {strArray[2]}";
  }

  public static string MapTransactionDetailsCSVToIPNFormatForCart(string[] ResponseRows)
  {
    return (string) null;
  }

  public static string MapTransactionDetailsCSVToIPNFormat(string Response)
  {
    StringBuilder stringBuilder = new StringBuilder();
    XmlDocument xmlDocument = new XmlDocument();
    StreamReader streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\CSV2IPNMap\\CSV2IPNMap.xml");
    xmlDocument.LoadXml(streamReader.ReadToEnd());
    streamReader.Close();
    string[] strArray = Response.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = PayPalHelper.UrlDecode(strArray[index]);
    string str1 = string.Empty;
    string str2 = string.Empty;
    string empty = string.Empty;
    for (int index = 1; index < strArray.Length + 1; ++index)
    {
      if (index < 15)
      {
        XmlNode xmlNode = xmlDocument.SelectSingleNode($"//map/fields/field/csvfields/csvfield[text() = '{index.ToString()}']");
        if (xmlNode != null)
        {
          XmlNode nextSibling = xmlNode.ParentNode.NextSibling;
          int int32 = Convert.ToInt32(xmlNode.InnerText);
          switch (int32)
          {
            case 1:
              str1 = PayPalHelper.ConvertTimeFromCSVToIPNFormat(PayPalHelper.GetFieldValueFromCSVTransactionRecord(Response, int32 - 1));
              break;
            case 2:
              str2 = " " + PayPalHelper.GetFieldValueFromCSVTransactionRecord(Response, int32 - 1);
              break;
            case 3:
              string transactionRecord = PayPalHelper.GetFieldValueFromCSVTransactionRecord(Response, int32 - 1);
              stringBuilder.Append($"payment_date={str2} {str1} {transactionRecord}");
              stringBuilder.Append("&");
              break;
            default:
              IEnumerator enumerator = nextSibling.GetEnumerator();
              try
              {
                while (enumerator.MoveNext())
                {
                  XmlNode current = (XmlNode) enumerator.Current;
                  stringBuilder.Append($"{current.InnerText}={PayPalHelper.GetFieldValueFromCSVTransactionRecord(Response, int32 - 1)}");
                  stringBuilder.Append("&");
                }
                break;
              }
              finally
              {
                if (enumerator is IDisposable disposable)
                  disposable.Dispose();
              }
          }
        }
      }
      else
      {
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode($"//map/fields/lineitem/field/csvfields/csvfield[text() = '{index.ToString()}']");
        if (xmlNode1 != null && (index == 15 || index == 17 || index == 19))
        {
          XmlNode nextSibling = xmlNode1.ParentNode.NextSibling;
          int int32 = Convert.ToInt32(xmlNode1.InnerText);
          foreach (XmlNode xmlNode2 in nextSibling)
          {
            stringBuilder.Append($"{xmlNode2.InnerText}={PayPalHelper.GetFieldValueFromCSVTransactionRecord(Response, int32 - 1)}");
            stringBuilder.Append("&");
          }
        }
      }
    }
    return stringBuilder.ToString();
  }

  public static int GetFieldIndexFromCSVTransactionRecord(
    string TransactionRecord,
    string FieldName)
  {
    string[] strArray = TransactionRecord.Split(",".ToCharArray());
    int transactionRecord = -1;
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (strArray[index].Trim().Equals(FieldName))
      {
        transactionRecord = index;
        break;
      }
    }
    return transactionRecord;
  }

  public static string GetFieldValueFromCSVTransactionRecord(
    string TransactionRecord,
    int FieldIndex)
  {
    return FieldIndex >= 0 ? PayPalHelper.StripLeadingAndTrailingQuotes(TransactionRecord.Split(",".ToCharArray())[FieldIndex]) : string.Empty;
  }

  private static string StripLeadingAndTrailingQuotes(string Text)
  {
    int num1 = Text.IndexOf("\"");
    int num2 = Text.LastIndexOf("\"");
    if (num1 == 0 && num2 > 0)
      Text = Text.Substring(num1 + 1, num2 - 1);
    return Text;
  }

  public static string CreateResponsiveTransactionSummaryTableAsHtml(
    string Request,
    string TemplateName,
    string TemplateFile)
  {
    StreamReader streamReader = new StreamReader(TemplateFile);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(streamReader.ReadToEnd());
    streamReader.Close();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    string empty4 = string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    switch (TemplateName)
    {
      case "[%PAYMENTDETAILS%]":
        empty1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/lineitem").ChildNodes[0].Value;
        empty2 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/shippinghandling").ChildNodes[0].Value;
        empty3 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/tax").ChildNodes[0].Value;
        empty4 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/total").ChildNodes[0].Value;
        break;
    }
    string valueFromRequest1 = PayPalHelper.ExtractValueFromRequest(Request, "num_cart_items");
    string newValue1 = PayPalHelper.ExtractValueFromRequest(Request, "quantity");
    if (string.IsNullOrEmpty(newValue1) || newValue1.Trim().Equals("0"))
      newValue1 = "1";
    string str1 = PayPalHelper.ExtractValueFromRequest(Request, "tax");
    if (string.IsNullOrEmpty(str1))
      str1 = "0.00";
    string newValue2 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(str1));
    PayPalHelper.ExtractValueFromRequest(Request, "invoice");
    string str2 = PayPalHelper.ExtractValueFromRequest(Request, "mc_shipping");
    if (string.IsNullOrEmpty(str2))
      str2 = "0.00";
    string str3 = PayPalHelper.ExtractValueFromRequest(Request, "mc_handling");
    if (string.IsNullOrEmpty(str3))
      str3 = "0.00";
    string newValue3 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(str2) + Convert.ToDouble(str3));
    string newValue4 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(PayPalHelper.ExtractValueFromRequest(Request, "mc_gross")));
    string valueFromRequest2 = PayPalHelper.ExtractValueFromRequest(Request, "mc_currency");
    string empty5 = string.Empty;
    string empty6 = string.Empty;
    string empty7 = string.Empty;
    string empty8 = string.Empty;
    string empty9 = string.Empty;
    string empty10 = string.Empty;
    string empty11 = string.Empty;
    string empty12 = string.Empty;
    if (string.IsNullOrEmpty(valueFromRequest1))
    {
      string newValue5;
      if (PayPalHelper.ExtractValueFromRequest(Request, "txn_type").Equals("send_money"))
      {
        string valueFromRequest3 = PayPalHelper.ExtractValueFromRequest(Request, "txn_id");
        newValue5 = "Payment received";
        if (!string.IsNullOrEmpty(valueFromRequest3))
          newValue5 = $"{newValue5} for transaction # {valueFromRequest3}";
      }
      else
        newValue5 = PayPalHelper.ExtractValueFromRequest(Request, "item_name");
      string valueFromRequest4 = PayPalHelper.ExtractValueFromRequest(Request, "item_number");
      string valueFromRequest5 = PayPalHelper.ExtractValueFromRequest(Request, "option_name");
      string valueFromRequest6 = PayPalHelper.ExtractValueFromRequest(Request, "option_selection");
      string str4 = empty1.Replace("{ITEMNAME}", newValue5);
      string newValue6 = string.Empty;
      if (!string.IsNullOrEmpty(valueFromRequest4))
      {
        newValue6 = "Item # " + valueFromRequest4;
        if (!string.IsNullOrEmpty(valueFromRequest6))
          newValue6 = $"{newValue6}<br>{valueFromRequest5}:{valueFromRequest6}";
      }
      else if (!string.IsNullOrEmpty(valueFromRequest6))
        newValue6 = $"{valueFromRequest5}:{valueFromRequest6}";
      string str5 = str4.Replace("{ITEMSELECTION}", newValue6);
      double USDValue = (Convert.ToDouble(newValue4) - Convert.ToDouble(newValue2) - Convert.ToDouble(newValue3)) / Convert.ToDouble(newValue1);
      string newValue7 = PayPalHelper.FormatUSDFormat(USDValue * Convert.ToDouble(newValue1));
      string str6 = str5.Replace("{ITEMGROSS}", PayPalHelper.FormatUSDFormat(USDValue)).Replace("{CURRENCY}", valueFromRequest2).Replace("{QUANTITY}", newValue1).Replace("{AMOUNT}", newValue7);
      stringBuilder.Append(str6);
      string str7 = empty2.Replace("{SHIPPING}", newValue3).Replace("{CURRENCY}", valueFromRequest2);
      stringBuilder.Append(str7);
      string str8 = empty3.Replace("{TAX}", newValue2).Replace("{CURRENCY}", valueFromRequest2);
      stringBuilder.Append(str8);
      string str9 = empty4.Replace("{GROSS}", newValue4).Replace("{CURRENCY}", valueFromRequest2);
      stringBuilder.Append(str9);
    }
    else
    {
      int int32 = Convert.ToInt32(valueFromRequest1);
      for (int index = 1; index <= int32; ++index)
      {
        string valueFromRequest7 = PayPalHelper.ExtractValueFromRequest(Request, "item_name" + index.ToString());
        string valueFromRequest8 = PayPalHelper.ExtractValueFromRequest(Request, "item_number" + index.ToString());
        string valueFromRequest9 = PayPalHelper.ExtractValueFromRequest(Request, "option_name" + index.ToString());
        string valueFromRequest10 = PayPalHelper.ExtractValueFromRequest(Request, "option_selection" + index.ToString());
        string str10 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(PayPalHelper.ExtractValueFromRequest(Request, "mc_gross_" + index.ToString())));
        string valueFromRequest11 = PayPalHelper.ExtractValueFromRequest(Request, "mc_shipping" + index.ToString());
        string str11 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(string.IsNullOrEmpty(valueFromRequest11) ? "0.00" : valueFromRequest11));
        string valueFromRequest12 = PayPalHelper.ExtractValueFromRequest(Request, "quantity" + index.ToString());
        string valueFromRequest13 = PayPalHelper.ExtractValueFromRequest(Request, "tax" + index.ToString());
        string str12 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(string.IsNullOrEmpty(valueFromRequest13) ? "0.00" : valueFromRequest13));
        string str13 = empty1.Replace("{ITEMNAME}", valueFromRequest7);
        string newValue8 = string.Empty;
        if (!string.IsNullOrEmpty(valueFromRequest8))
        {
          newValue8 = "Item # " + valueFromRequest8;
          if (!string.IsNullOrEmpty(valueFromRequest9))
            newValue8 = $"{newValue8}<br>{valueFromRequest9}:{valueFromRequest10}";
        }
        else if (!string.IsNullOrEmpty(valueFromRequest9))
          newValue8 = $"{newValue8}{valueFromRequest9}:{valueFromRequest10}";
        string str14 = str13.Replace("{ITEMSELECTION}", newValue8);
        double USDValue = (Convert.ToDouble(str10) - Convert.ToDouble(str12) - Convert.ToDouble(str11)) / Convert.ToDouble(valueFromRequest12);
        string newValue9 = PayPalHelper.FormatUSDFormat(USDValue * Convert.ToDouble(valueFromRequest12));
        string newValue10 = PayPalHelper.FormatUSDFormat(USDValue);
        string str15 = str14.Replace("{ITEMGROSS}", newValue10).Replace("{CURRENCY}", valueFromRequest2).Replace("{QUANTITY}", valueFromRequest12).Replace("{AMOUNT}", newValue9);
        stringBuilder.Append(str15);
      }
      string str16 = empty2.Replace("{SHIPPING}", newValue3).Replace("{CURRENCY}", valueFromRequest2);
      stringBuilder.Append(str16);
      string str17 = empty3.Replace("{TAX}", newValue2).Replace("{CURRENCY}", valueFromRequest2);
      stringBuilder.Append(str17);
      string str18 = empty4.Replace("{GROSS}", newValue4).Replace("{CURRENCY}", valueFromRequest2);
      stringBuilder.Append(str18);
    }
    return stringBuilder.ToString();
  }

  public static string CreateTransactionSummaryTableAsHtml(
    string Request,
    string TemplateName,
    string TemplateFile)
  {
    PayPalHelper.ExtractValueFromRequest(Request, "address_name");
    PayPalHelper.ExtractValueFromRequest(Request, "address_street");
    PayPalHelper.ExtractValueFromRequest(Request, "address_city");
    PayPalHelper.ExtractValueFromRequest(Request, "address_state");
    PayPalHelper.ExtractValueFromRequest(Request, "address_zip");
    PayPalHelper.ExtractValueFromRequest(Request, "address_country");
    StreamReader streamReader = new StreamReader(TemplateFile);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(streamReader.ReadToEnd());
    streamReader.Close();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    string str1 = string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    switch (TemplateName)
    {
      case "[%PAYMENTDETAILS%]":
        empty1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/headerbegin").ChildNodes[0].Value;
        empty2 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/headerend").ChildNodes[0].Value;
        empty3 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/lineitem").ChildNodes[0].Value;
        str1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/footer").ChildNodes[0].Value;
        break;
      case "[%PAYMENTDETAILS_2COLUMN%]":
        empty1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details2column/headerbegin").ChildNodes[0].Value;
        empty2 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details2column/headerend").ChildNodes[0].Value;
        empty3 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details2column/lineitem").ChildNodes[0].Value;
        str1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details2column/footer").ChildNodes[0].Value;
        break;
      case "[%DONATIONDETAILS%]":
        empty1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/donationdetails/headerbegin").ChildNodes[0].Value;
        empty2 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/donationdetails/headerend").ChildNodes[0].Value;
        empty3 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/donationdetails/lineitem").ChildNodes[0].Value;
        str1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/donationdetails/footer").ChildNodes[0].Value;
        break;
      case "[%DONATIONDETAILS_2COLUMN%]":
        empty1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/donationdetails2column/headerbegin").ChildNodes[0].Value;
        empty2 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/donationdetails2column/headerend").ChildNodes[0].Value;
        empty3 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/donationdetails2column/lineitem").ChildNodes[0].Value;
        str1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/donationdetails2column/footer").ChildNodes[0].Value;
        break;
    }
    string valueFromRequest1 = PayPalHelper.ExtractValueFromRequest(Request, "num_cart_items");
    string newValue1 = PayPalHelper.ExtractValueFromRequest(Request, "quantity");
    if (string.IsNullOrEmpty(newValue1) || newValue1.Trim().Equals("0"))
      newValue1 = "1";
    string str2 = PayPalHelper.ExtractValueFromRequest(Request, "tax");
    if (string.IsNullOrEmpty(str2))
      str2 = "0.00";
    string newValue2 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(str2));
    PayPalHelper.ExtractValueFromRequest(Request, "invoice");
    string str3 = PayPalHelper.ExtractValueFromRequest(Request, "mc_shipping");
    if (string.IsNullOrEmpty(str3))
      str3 = "0.00";
    string str4 = PayPalHelper.ExtractValueFromRequest(Request, "mc_handling");
    if (string.IsNullOrEmpty(str4))
      str4 = "0.00";
    string newValue3 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(str3) + Convert.ToDouble(str4));
    string newValue4 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(PayPalHelper.ExtractValueFromRequest(Request, "mc_gross")));
    string valueFromRequest2 = PayPalHelper.ExtractValueFromRequest(Request, "mc_currency");
    string empty4 = string.Empty;
    string empty5 = string.Empty;
    string empty6 = string.Empty;
    string empty7 = string.Empty;
    string empty8 = string.Empty;
    string empty9 = string.Empty;
    string empty10 = string.Empty;
    string empty11 = string.Empty;
    if (string.IsNullOrEmpty(valueFromRequest1))
    {
      string newValue5;
      if (PayPalHelper.ExtractValueFromRequest(Request, "txn_type").Equals("send_money"))
      {
        string valueFromRequest3 = PayPalHelper.ExtractValueFromRequest(Request, "txn_id");
        newValue5 = "Payment received";
        if (!string.IsNullOrEmpty(valueFromRequest3))
          newValue5 = $"{newValue5} for transaction # {valueFromRequest3}";
      }
      else
        newValue5 = PayPalHelper.ExtractValueFromRequest(Request, "item_name");
      string valueFromRequest4 = PayPalHelper.ExtractValueFromRequest(Request, "item_number");
      string valueFromRequest5 = PayPalHelper.ExtractValueFromRequest(Request, "option_name");
      string valueFromRequest6 = PayPalHelper.ExtractValueFromRequest(Request, "option_selection");
      stringBuilder.Append(empty1);
      string str5 = empty3.Replace("{ITEMNAME}", newValue5);
      string newValue6 = string.Empty;
      if (!string.IsNullOrEmpty(valueFromRequest4))
      {
        newValue6 = "Item # " + valueFromRequest4;
        if (!string.IsNullOrEmpty(valueFromRequest6))
          newValue6 = $"{newValue6}<br>{valueFromRequest5}:{valueFromRequest6}";
      }
      else if (!string.IsNullOrEmpty(valueFromRequest6))
        newValue6 = $"{valueFromRequest5}:{valueFromRequest6}";
      string str6 = str5.Replace("{ITEMSELECTION}", newValue6);
      double USDValue = (Convert.ToDouble(newValue4) - Convert.ToDouble(newValue2) - Convert.ToDouble(newValue3)) / Convert.ToDouble(newValue1);
      string newValue7 = PayPalHelper.FormatUSDFormat(USDValue * Convert.ToDouble(newValue1));
      string str7 = str6.Replace("{ITEMGROSS}", PayPalHelper.FormatUSDFormat(USDValue)).Replace("{CURRENCY}", valueFromRequest2).Replace("{QUANTITY}", newValue1).Replace("{AMOUNT}", newValue7);
      stringBuilder.Append(str7);
      stringBuilder.Append(empty2);
      if (!TemplateName.Equals("[%DONATIONDETAILS%]"))
        str1 = str1.Replace("{SHIPPING}", newValue3).Replace("{TAX}", newValue2);
      string str8 = str1.Replace("{GROSS}", newValue4).Replace("{CURRENCY}", valueFromRequest2);
      stringBuilder.Append(str8);
    }
    else
    {
      int int32 = Convert.ToInt32(valueFromRequest1);
      stringBuilder.Append(empty1);
      for (int index = 1; index <= int32; ++index)
      {
        string valueFromRequest7 = PayPalHelper.ExtractValueFromRequest(Request, "item_name" + index.ToString());
        string valueFromRequest8 = PayPalHelper.ExtractValueFromRequest(Request, "item_number" + index.ToString());
        string valueFromRequest9 = PayPalHelper.ExtractValueFromRequest(Request, "option_name" + index.ToString());
        string valueFromRequest10 = PayPalHelper.ExtractValueFromRequest(Request, "option_selection" + index.ToString());
        string str9 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(PayPalHelper.ExtractValueFromRequest(Request, "mc_gross_" + index.ToString())));
        string valueFromRequest11 = PayPalHelper.ExtractValueFromRequest(Request, "mc_shipping" + index.ToString());
        string str10 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(string.IsNullOrEmpty(valueFromRequest11) ? "0.00" : valueFromRequest11));
        string valueFromRequest12 = PayPalHelper.ExtractValueFromRequest(Request, "quantity" + index.ToString());
        string valueFromRequest13 = PayPalHelper.ExtractValueFromRequest(Request, "tax" + index.ToString());
        string str11 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(string.IsNullOrEmpty(valueFromRequest13) ? "0.00" : valueFromRequest13));
        string str12 = empty3.Replace("{ITEMNAME}", valueFromRequest7);
        string newValue8 = string.Empty;
        if (!string.IsNullOrEmpty(valueFromRequest8))
        {
          newValue8 = "Item # " + valueFromRequest8;
          if (!string.IsNullOrEmpty(valueFromRequest9))
            newValue8 = $"{newValue8}<br>{valueFromRequest9}:{valueFromRequest10}";
        }
        else if (!string.IsNullOrEmpty(valueFromRequest9))
          newValue8 = $"{newValue8}{valueFromRequest9}:{valueFromRequest10}";
        string str13 = str12.Replace("{ITEMSELECTION}", newValue8);
        double USDValue = (Convert.ToDouble(str9) - Convert.ToDouble(str11) - Convert.ToDouble(str10)) / Convert.ToDouble(valueFromRequest12);
        string newValue9 = PayPalHelper.FormatUSDFormat(USDValue * Convert.ToDouble(valueFromRequest12));
        string newValue10 = PayPalHelper.FormatUSDFormat(USDValue);
        string str14 = str13.Replace("{ITEMGROSS}", newValue10).Replace("{CURRENCY}", valueFromRequest2).Replace("{QUANTITY}", valueFromRequest12).Replace("{AMOUNT}", newValue9);
        stringBuilder.Append(str14);
      }
      stringBuilder.Append(empty2);
      string str15 = str1.Replace("{SHIPPING}", newValue3).Replace("{TAX}", newValue2).Replace("{GROSS}", newValue4).Replace("{CURRENCY}", valueFromRequest2);
      stringBuilder.Append(str15);
    }
    return stringBuilder.ToString();
  }

  public static string CreateTransactionSummaryTableForSpecificCartItemsAsHtml(
    string Request,
    string TemplateName,
    string TemplateFile,
    string FilterBy,
    string FilterCondition,
    string FilterValue)
  {
    PayPalHelper.ExtractValueFromRequest(Request, "address_name");
    PayPalHelper.ExtractValueFromRequest(Request, "address_street");
    PayPalHelper.ExtractValueFromRequest(Request, "address_city");
    PayPalHelper.ExtractValueFromRequest(Request, "address_state");
    PayPalHelper.ExtractValueFromRequest(Request, "address_zip");
    PayPalHelper.ExtractValueFromRequest(Request, "address_country");
    StreamReader streamReader = new StreamReader(TemplateFile);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(streamReader.ReadToEnd());
    streamReader.Close();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    string empty4 = string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    switch (TemplateName)
    {
      case "[%PAYMENTDETAILS%]":
        empty1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/headerbegin").ChildNodes[0].Value;
        empty2 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/headerend").ChildNodes[0].Value;
        empty3 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/lineitem").ChildNodes[0].Value;
        empty4 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details/footer").ChildNodes[0].Value;
        break;
      case "[%PAYMENTDETAILS_2COLUMN%]":
        empty1 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details2column/headerbegin").ChildNodes[0].Value;
        empty2 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details2column/headerend").ChildNodes[0].Value;
        empty3 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details2column/lineitem").ChildNodes[0].Value;
        empty4 = xmlDocument.SelectSingleNode("//paymentdetailstemplatestyles/details2column/footer").ChildNodes[0].Value;
        break;
    }
    string valueFromRequest1 = PayPalHelper.ExtractValueFromRequest(Request, "num_cart_items");
    if (string.IsNullOrEmpty(PayPalHelper.ExtractValueFromRequest(Request, "quantity")))
      ;
    string valueFromRequest2 = PayPalHelper.ExtractValueFromRequest(Request, "mc_currency");
    string newValue1 = string.Empty;
    string str1 = string.Empty;
    string empty5 = string.Empty;
    string empty6 = string.Empty;
    string empty7 = string.Empty;
    string empty8 = string.Empty;
    string empty9 = string.Empty;
    int int32 = Convert.ToInt32(valueFromRequest1);
    stringBuilder.Append(empty1);
    for (int index = 1; index <= int32; ++index)
    {
      switch (FilterBy)
      {
        case "ITEMNAME":
          newValue1 = PayPalHelper.ExtractValueFromRequest(Request, "item_name" + index.ToString());
          str1 = PayPalHelper.ExtractValueFromRequest(Request, "item_number" + index.ToString());
          switch (FilterCondition)
          {
            case "EQUALS":
              if (newValue1.Equals(FilterValue))
                goto label_14;
              continue;
            case "CONTAINS":
              if (newValue1.Contains(FilterValue))
                goto label_14;
              continue;
            default:
              goto label_14;
          }
        case "ITEMNUMBER":
          str1 = PayPalHelper.ExtractValueFromRequest(Request, "item_number" + index.ToString());
          newValue1 = PayPalHelper.ExtractValueFromRequest(Request, "item_name" + index.ToString());
          switch (FilterCondition)
          {
            case "EQUALS":
              if (str1.Equals(FilterValue))
                break;
              continue;
            case "CONTAINS":
              if (str1.Contains(FilterValue))
                break;
              continue;
          }
          goto default;
        default:
label_14:
          string valueFromRequest3 = PayPalHelper.ExtractValueFromRequest(Request, "option_name" + index.ToString());
          string valueFromRequest4 = PayPalHelper.ExtractValueFromRequest(Request, "option_selection" + index.ToString());
          string newValue2 = PayPalHelper.FormatUSDFormat(Convert.ToDouble(PayPalHelper.ExtractValueFromRequest(Request, "mc_gross_" + index.ToString())));
          string valueFromRequest5 = PayPalHelper.ExtractValueFromRequest(Request, "quantity" + index.ToString());
          string str2 = empty3.Replace("{ITEMNAME}", newValue1);
          string newValue3 = string.Empty;
          if (!string.IsNullOrEmpty(str1))
          {
            newValue3 = "Item # " + str1;
            if (!string.IsNullOrEmpty(valueFromRequest3))
              newValue3 = $"{newValue3}<br>{valueFromRequest3}:{valueFromRequest4}";
          }
          else if (!string.IsNullOrEmpty(valueFromRequest3))
            newValue3 = $"{valueFromRequest3}:{valueFromRequest4}";
          string str3 = str2.Replace("{ITEMSELECTION}", newValue3).Replace("{ITEMGROSS}", PayPalHelper.FormatUSDFormat(Convert.ToDouble(newValue2) / (double) Convert.ToInt32(valueFromRequest5))).Replace("{CURRENCY}", valueFromRequest2).Replace("{QUANTITY}", valueFromRequest5).Replace("{AMOUNT}", newValue2);
          stringBuilder.Append(str3);
          break;
      }
    }
    stringBuilder.Append(empty2);
    stringBuilder.Append(empty4);
    return stringBuilder.ToString();
  }

  public static string FormatUSDFormat(double USDValue)
  {
    return USDValue.ToString().IndexOf(".") != -1 ? (USDValue.ToString().Substring(USDValue.ToString().IndexOf(".") + 1).Length != 1 ? USDValue.ToString() : USDValue.ToString() + "0") : USDValue.ToString() + ".00";
  }

  public static string PersonalizeMessage(
    int AccountID,
    string MessageTemplate,
    string TestIPNRequest,
    string[] FieldNames)
  {
    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
    PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "business");
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    string userTimeZoneCode = fyiDataAccess.GetRegistrationData(AccountID).UserTimeZoneCode;
    int timeZoneOffset = fyiDataAccess.GetTimeZoneOffset(userTimeZoneCode);
    foreach (string fieldName in FieldNames)
    {
      switch (fieldName)
      {
        case "{first_name}":
          string newValue1 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "first_name");
          if (!string.IsNullOrEmpty(newValue1))
          {
            string lower = newValue1.ToLower();
            newValue1 = lower.Substring(0, 1).ToUpper() + lower.Substring(1);
          }
          MessageTemplate = MessageTemplate.Replace("{first_name}", newValue1);
          break;
        case "{last_name}":
          string newValue2 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "last_name");
          if (!string.IsNullOrEmpty(newValue2))
          {
            string lower = newValue2.ToLower();
            newValue2 = lower.Substring(0, 1).ToUpper() + lower.Substring(1);
          }
          MessageTemplate = MessageTemplate.Replace("{last_name}", newValue2);
          break;
        case "{payer_email}":
          MessageTemplate = MessageTemplate.Replace("{payer_email}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "payer_email"));
          break;
        case "{payment_date|TZ|DATE}":
          string valueFromRequest1 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "payment_date");
          DateTime userTime1 = UserDateTime.UTCToUserTime(DateTime.Parse(valueFromRequest1.Substring(0, valueFromRequest1.LastIndexOf(" "))), (UserDateTime.TZ) timeZoneOffset);
          string str1 = $"{userTime1.ToString("MMM")} {userTime1.ToString("dd")}, {userTime1.Year.ToString()}";
          MessageTemplate = MessageTemplate.Replace("{payment_date|TZ|DATE}", $"{str1} {userTimeZoneCode}");
          break;
        case "{payment_date|TZ}":
          string valueFromRequest2 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "payment_date");
          DateTime userTime2 = UserDateTime.UTCToUserTime(DateTime.Parse(valueFromRequest2.Substring(0, valueFromRequest2.LastIndexOf(" "))), (UserDateTime.TZ) timeZoneOffset);
          string str2 = $"{userTime2.ToString("MMM")} {userTime2.ToString("dd")}, {userTime2.Year.ToString()} {userTime2.ToString("HH:mm:ss")}";
          MessageTemplate = MessageTemplate.Replace("{payment_date|TZ}", $"{str2} {userTimeZoneCode}");
          break;
        case "{payment_date}":
          MessageTemplate = MessageTemplate.Replace("{payment_date}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "payment_date"));
          break;
        case "{mc_gross}":
          MessageTemplate = MessageTemplate.Replace("{mc_gross}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "mc_gross"));
          break;
        case "{mc_currency}":
          MessageTemplate = MessageTemplate.Replace("{mc_currency}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "mc_currency"));
          break;
        case "{txn_id}":
          MessageTemplate = MessageTemplate.Replace("{txn_id}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "txn_id"));
          break;
        case "{address_zip}":
          MessageTemplate = MessageTemplate.Replace("{address_zip}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_zip"));
          break;
        case "{address_country_code}":
          MessageTemplate = MessageTemplate.Replace("{address_country_code}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_country_code"));
          break;
        case "{address_street}":
          MessageTemplate = MessageTemplate.Replace("{address_street}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_street"));
          break;
        case "{address_state}":
          MessageTemplate = MessageTemplate.Replace("{address_state}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_state"));
          break;
        case "{address_city}":
          MessageTemplate = MessageTemplate.Replace("{address_city}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_city"));
          break;
        case "{address_country}":
          MessageTemplate = MessageTemplate.Replace("{address_country}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_country"));
          break;
        case "{address_name}":
          MessageTemplate = MessageTemplate.Replace("{address_name}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_name"));
          break;
        case "{mc_amount3}":
          MessageTemplate = MessageTemplate.Replace("{mc_amount3}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "mc_amount3"));
          break;
        case "{amount3}":
          MessageTemplate = MessageTemplate.Replace("{amount3}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "amount3"));
          break;
        case "{period3}":
          string str3 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "period3");
          string[] strArray1 = str3.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
          switch (strArray1[1])
          {
            case "D":
              str3 = "month";
              break;
            case "W":
              str3 = "week";
              break;
            case "M":
              str3 = "month";
              break;
            case "Y":
              str3 = "year";
              break;
          }
          string newValue3 = Convert.ToInt32(strArray1[0]) <= 1 ? $"{strArray1[0]} {str3}" : $"{strArray1[0]} {str3}s";
          MessageTemplate = MessageTemplate.Replace("{period3}", newValue3);
          break;
        case "{item_name}":
          MessageTemplate = MessageTemplate.Replace("{item_name}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "item_name"));
          break;
        case "{subscr_date}":
          string[] strArray2 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "subscr_date").Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
          string newValue4 = $"{strArray2[1]} {strArray2[2]} {strArray2[3]}";
          MessageTemplate = MessageTemplate.Replace("{subscr_date}", newValue4);
          break;
      }
    }
    return MessageTemplate;
  }

  public static string PersonalizeMessage(
    string MessageTemplate,
    string TestIPNRequest,
    string[] FieldNames)
  {
    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
    string valueFromRequest1 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "business");
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    string userTimeZoneCode = fyiDataAccess.GetRegistrationData(valueFromRequest1, "simplyfyipayments").UserTimeZoneCode;
    int timeZoneOffset = fyiDataAccess.GetTimeZoneOffset(userTimeZoneCode);
    DateTime userTime;
    int year;
    foreach (string fieldName in FieldNames)
    {
      switch (fieldName)
      {
        case "{first_name}":
          string newValue1 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "first_name");
          if (!string.IsNullOrEmpty(newValue1))
          {
            string lower = newValue1.ToLower();
            newValue1 = lower.Substring(0, 1).ToUpper() + lower.Substring(1);
          }
          MessageTemplate = MessageTemplate.Replace("{first_name}", newValue1);
          break;
        case "{last_name}":
          string newValue2 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "last_name");
          if (!string.IsNullOrEmpty(newValue2))
          {
            string lower = newValue2.ToLower();
            newValue2 = lower.Substring(0, 1).ToUpper() + lower.Substring(1);
          }
          MessageTemplate = MessageTemplate.Replace("{last_name}", newValue2);
          break;
        case "{payer_email}":
          MessageTemplate = MessageTemplate.Replace("{payer_email}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "payer_email"));
          break;
        case "{payment_date|TZ|DATE}":
          string valueFromRequest2 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "payment_date");
          userTime = UserDateTime.UTCToUserTime(DateTime.Parse(valueFromRequest2.Substring(0, valueFromRequest2.LastIndexOf(" "))), (UserDateTime.TZ) timeZoneOffset);
          string[] strArray1 = new string[5]
          {
            userTime.ToString("MMM"),
            " ",
            userTime.ToString("dd"),
            ", ",
            null
          };
          string[] strArray2 = strArray1;
          year = userTime.Year;
          string str1 = year.ToString();
          strArray2[4] = str1;
          string str2 = string.Concat(strArray1);
          MessageTemplate = MessageTemplate.Replace("{payment_date|TZ|DATE}", $"{str2} {userTimeZoneCode}");
          break;
        case "{payment_date|TZ}":
          string valueFromRequest3 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "payment_date");
          userTime = UserDateTime.UTCToUserTime(DateTime.Parse(valueFromRequest3.Substring(0, valueFromRequest3.LastIndexOf(" "))), (UserDateTime.TZ) timeZoneOffset);
          string[] strArray3 = new string[7]
          {
            userTime.ToString("MMM"),
            " ",
            userTime.ToString("dd"),
            ", ",
            null,
            null,
            null
          };
          string[] strArray4 = strArray3;
          year = userTime.Year;
          string str3 = year.ToString();
          strArray4[4] = str3;
          strArray3[5] = " ";
          strArray3[6] = userTime.ToString("HH:mm:ss");
          string str4 = string.Concat(strArray3);
          MessageTemplate = MessageTemplate.Replace("{payment_date|TZ}", $"{str4} {userTimeZoneCode}");
          break;
        case "{payment_date}":
          MessageTemplate = MessageTemplate.Replace("{payment_date}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "payment_date"));
          break;
        case "{mc_gross}":
          MessageTemplate = MessageTemplate.Replace("{mc_gross}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "mc_gross"));
          break;
        case "{mc_currency}":
          MessageTemplate = MessageTemplate.Replace("{mc_currency}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "mc_currency"));
          break;
        case "{txn_id}":
          MessageTemplate = MessageTemplate.Replace("{txn_id}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "txn_id"));
          break;
        case "{address_zip}":
          MessageTemplate = MessageTemplate.Replace("{address_zip}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_zip"));
          break;
        case "{address_country_code}":
          MessageTemplate = MessageTemplate.Replace("{address_country_code}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_country_code"));
          break;
        case "{address_street}":
          MessageTemplate = MessageTemplate.Replace("{address_street}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_street"));
          break;
        case "{address_state}":
          MessageTemplate = MessageTemplate.Replace("{address_state}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_state"));
          break;
        case "{address_city}":
          MessageTemplate = MessageTemplate.Replace("{address_city}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_city"));
          break;
        case "{address_country}":
          MessageTemplate = MessageTemplate.Replace("{address_country}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_country"));
          break;
        case "{address_name}":
          MessageTemplate = MessageTemplate.Replace("{address_name}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "address_name"));
          break;
        case "{mc_amount3}":
          MessageTemplate = MessageTemplate.Replace("{mc_amount3}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "mc_amount3"));
          break;
        case "{amount3}":
          MessageTemplate = MessageTemplate.Replace("{amount3}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "amount3"));
          break;
        case "{period3}":
          string str5 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "period3");
          string[] strArray5 = str5.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
          switch (strArray5[1])
          {
            case "D":
              str5 = "month";
              break;
            case "W":
              str5 = "week";
              break;
            case "M":
              str5 = "month";
              break;
            case "Y":
              str5 = "year";
              break;
          }
          string newValue3 = Convert.ToInt32(strArray5[0]) <= 1 ? $"{strArray5[0]} {str5}" : $"{strArray5[0]} {str5}s";
          MessageTemplate = MessageTemplate.Replace("{period3}", newValue3);
          break;
        case "{item_name}":
          MessageTemplate = MessageTemplate.Replace("{item_name}", PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "item_name"));
          break;
        case "{subscr_date}":
          string[] strArray6 = PayPalHelper.ExtractValueFromRequest(TestIPNRequest, "subscr_date").Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
          string newValue4 = $"{strArray6[1]} {strArray6[2]} {strArray6[3]}";
          MessageTemplate = MessageTemplate.Replace("{subscr_date}", newValue4);
          break;
      }
    }
    return MessageTemplate;
  }

  public static string ExtractValueFromRequest(string strRequest, string Variable)
  {
    string str1 = (string) null;
    string Url = string.Empty;
    if (!string.IsNullOrEmpty(strRequest))
    {
      int startIndex = strRequest.StartsWith(Variable) ? 0 : strRequest.IndexOf($"&{Variable}=");
      if (startIndex >= 0)
      {
        int num = strRequest.IndexOf("&", startIndex + 1);
        if (num > 0 && num > startIndex)
          str1 = strRequest.Substring(startIndex, num - startIndex);
        else if (num == -1)
          str1 = strRequest.Substring(startIndex);
      }
      if (str1 != null)
        Url = Utilities.UrlDecode(str1.Substring(str1.IndexOf("=") + 1)).Replace("+", " ");
      if (Variable.Equals("payment_date"))
      {
        Url = Utilities.UrlDecode(Url);
        if (!string.IsNullOrEmpty(Url))
        {
          string str2 = Url.Substring(0, 9).Replace("+", " ");
          string str3 = Url.Substring(9, 13).Replace("+", " ");
          string str4 = Url.Substring(22).Replace("+", " ");
          Url = str3 + str2 + str4;
        }
      }
    }
    return Url;
  }

  public static string CreateXMLMessage(
    string FromAddress,
    string FromName,
    string Subject,
    string HTMLMessagePart,
    string PlainTextMessagePart,
    string ToAddress,
    string Signature)
  {
    StringWriter w = new StringWriter();
    XmlTextWriter xmlTextWriter1 = new XmlTextWriter((TextWriter) w);
    xmlTextWriter1.WriteStartElement("fyimessage");
    XmlTextWriter xmlTextWriter2 = xmlTextWriter1;
    DateTime dateTime = DateTime.Now;
    dateTime = dateTime.ToUniversalTime();
    string shortDateString = dateTime.ToShortDateString();
    xmlTextWriter2.WriteAttributeString("senddate", shortDateString);
    xmlTextWriter1.WriteStartElement("email", (string) null);
    xmlTextWriter1.WriteElementString("from", FromAddress);
    xmlTextWriter1.WriteElementString("fromname", FromName);
    xmlTextWriter1.WriteElementString("subject", Subject);
    xmlTextWriter1.WriteElementString("htmlmessage", HTMLMessagePart);
    xmlTextWriter1.WriteElementString("plaintextmessage", PlainTextMessagePart);
    xmlTextWriter1.WriteElementString("to", ToAddress);
    xmlTextWriter1.WriteElementString("signature", Signature);
    xmlTextWriter1.WriteEndElement();
    xmlTextWriter1.WriteEndElement();
    xmlTextWriter1.Flush();
    w.Flush();
    w.Close();
    xmlTextWriter1.Close();
    return w.ToString();
  }

  public static string TranslateTransactionTypeToEndPointSubType(string TransactionType)
  {
    string empty = string.Empty;
    int num;
    switch (TransactionType)
    {
      case "web_accept":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_WEBACCEPT.ToString();
        goto label_21;
      case "cart":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_CART.ToString();
        goto label_21;
      case "paypal_here":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_PAYPALHERE.ToString();
        goto label_21;
      case "subscr_payment":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION.ToString();
        goto label_21;
      case "subscr_signup":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_SIGNUP.ToString();
        goto label_21;
      case "subscr_eot":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_EOT.ToString();
        goto label_21;
      case "subscr_modify":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_MODIFY.ToString();
        goto label_21;
      case "subscr_cancel":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_CANCEL.ToString();
        goto label_21;
      case "subscr_failed":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_FAILED.ToString();
        goto label_21;
      case "recurring_payment":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION.ToString();
        goto label_21;
      case "recurring_payment_expired":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_EOT.ToString();
        goto label_21;
      case "recurring_payment_profile_cancel":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_CANCEL.ToString();
        goto label_21;
      case "recurring_payment_profile_created":
        empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_SIGNUP.ToString();
        goto label_21;
      case "recurring_payment_failed":
      case "recurring_payment_skipped":
      case "recurring_payment_suspended":
        num = 0;
        break;
      default:
        num = !TransactionType.Equals("recurring_payment_suspended_due_to_max_failed_payment") ? 1 : 0;
        break;
    }
    if (num == 0)
    {
      empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SUBSCRIPTION_FAILED.ToString();
    }
    else
    {
      switch (TransactionType)
      {
        case "virtual_terminal":
          empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_VIRTUAL_TERMINAL.ToString();
          break;
        case "send_money":
          empty = VoiShareOLTypes.EndPointSubType.PAYPAL_PAYMENT_SEND_MONEY.ToString();
          break;
      }
    }
label_21:
    return empty;
  }

  public static string GetShipmentTrackingTemplateHtml(
    string TemplateFile,
    string PersonalizedMessageHTML,
    int LayoutThemeColumns,
    string Carrier,
    string CarrierImageUrl,
    string TrackingNumber,
    string TrackingLink,
    ref string TrackingTemplatePlaceholder)
  {
    string empty = string.Empty;
    StreamReader streamReader = new StreamReader(TemplateFile);
    string xml = streamReader.ReadToEnd().Replace("\r\n", " ");
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(xml);
    string str;
    if (LayoutThemeColumns == 1)
    {
      str = xmlDocument.SelectSingleNode("//shippingtrackingtemplate/details").ChildNodes[0].Value;
      TrackingTemplatePlaceholder = "[%SHIPMENTTRACKINGDETAILS%]";
    }
    else
    {
      str = xmlDocument.SelectSingleNode("//shippingtrackingtemplate/details2column").ChildNodes[0].Value;
      TrackingTemplatePlaceholder = "[%SHIPMENTTRACKINGDETAILS_2COLUMN%]";
    }
    streamReader.Close();
    return str.Replace("{shipping_tracking_number}", TrackingNumber).Replace("{shipping_tracking_link}", TrackingLink).Replace("{shipping_carrier_image}", CarrierImageUrl);
  }
}
