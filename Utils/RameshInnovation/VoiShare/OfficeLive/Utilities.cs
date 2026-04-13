// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.Utilities
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using RameshInnovation.VoiShare.Services.Adapters;
using System;
using System.Collections;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive;

public class Utilities
{
  private const string SPACE = " ";

  public static bool IsValidEmail(string Email)
  {
    try
    {
      MailAddress mailAddress = new MailAddress(Email.Trim());
    }
    catch (Exception ex)
    {
      return false;
    }
    return true;
  }

  public static string CleanupCaseForName(string Name)
  {
    string str1 = string.Empty;
    string empty = string.Empty;
    foreach (string str2 in Name.Split(" ".ToCharArray()))
    {
      if (!string.IsNullOrEmpty(str1))
        str1 += " ";
      string lower = str2.ToLower();
      char ch;
      string str3;
      if (lower.Length > 1)
      {
        ch = lower[0];
        str3 = ch.ToString().ToUpper() + lower.Substring(1);
      }
      else
      {
        ch = lower[0];
        str3 = ch.ToString().ToUpper();
      }
      str1 = $"{str1}{str3} ";
    }
    return str1.Trim();
  }

  public static Control FindControlIterative(ControlCollection Controls, string ControlID)
  {
    Control controlIterative = (Control) null;
    foreach (Control control in Controls)
    {
      if (controlIterative == null)
      {
        if (control != null && control.ID != null && control.ID.Equals(ControlID))
        {
          controlIterative = control;
          break;
        }
        controlIterative = Utilities.FindControlIterative(control.Controls, ControlID);
      }
      else
        break;
    }
    return controlIterative;
  }

  public static string TransformDatasetXmlToHtmlTable(
    string DatasetXml,
    string TableName,
    string TableNamespace,
    bool TestPassed,
    string[] ViewFields)
  {
    bool flag = false;
    StringBuilder stringBuilder = new StringBuilder();
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(DatasetXml);
    Hashtable hashtable = new Hashtable();
    int key1 = 0;
    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
    XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//" + TableName, nsmgr);
    if (TestPassed)
    {
      stringBuilder.Append("<div style=\"height: 300px; overflow: scroll\">");
      stringBuilder.Append("<table class='results' cellspacing='0'>");
      stringBuilder.Append("<tr>");
      foreach (string viewField in ViewFields)
      {
        string[] strArray = viewField.Split("-".ToCharArray());
        if (!hashtable.ContainsValue((object) strArray[1]))
        {
          ++key1;
          hashtable.Add((object) key1, (object) strArray[1]);
        }
        stringBuilder.Append($"<th align='left' class='heading'>{strArray[0]}[{(object) key1}]</th>");
      }
      stringBuilder.Append("</tr>");
      string str1 = "#ffffff";
      foreach (XmlNode xmlNode1 in xmlNodeList)
      {
        stringBuilder.Append($"<tr style='background-color:{str1}'>");
        foreach (string viewField in ViewFields)
        {
          foreach (XmlNode xmlNode2 in xmlNode1)
          {
            flag = false;
            string str2 = viewField.StartsWith("ows_") ? viewField : "ows_" + viewField;
            if (xmlNode2.Name.Replace("_x005F_", "_").Equals(str2.Replace(" ", "_x0020_")))
            {
              string innerText = xmlNode2.InnerText;
              stringBuilder.Append($"<td class='row'>{innerText.Trim()}</td>");
              flag = true;
              break;
            }
          }
          if (!flag)
            stringBuilder.Append("<td style='color:red' align='left' 'row'>Missing data</td>");
        }
        stringBuilder.Append("</tr>");
        str1 = !str1.Equals("#eeeeee") ? "#eeeeee" : "#ffffff";
      }
      stringBuilder.Append("</table>");
      stringBuilder.Append("<table>");
      for (int key2 = 1; key2 <= key1; ++key2)
        stringBuilder.Append($"<tr align='left'><td align='left'>[{key2.ToString()}] : {hashtable[(object) key2].ToString()}</td><tr>");
      stringBuilder.Append("Row count: " + (object) xmlNodeList.Count);
      stringBuilder.Append("</table>");
      stringBuilder.Append("</div>");
    }
    else
    {
      stringBuilder.Append("<div style=\"height: 300px; overflow: scroll\">");
      stringBuilder.Append("<table class='results' cellspacing='0'>");
      stringBuilder.Append("<tr>");
      stringBuilder.Append("<td align='left' 'row'>");
      stringBuilder.Append(xmlNodeList[0].ChildNodes[0].InnerText);
      stringBuilder.Append("</td>");
      stringBuilder.Append("</td>");
      stringBuilder.Append("</tr>");
      stringBuilder.Append("</table>");
      stringBuilder.Append("</div>");
    }
    return stringBuilder.ToString();
  }

  public static string UrlDecode(string Url) => HttpUtility.UrlDecode(Url);

  public static void SendPushNotification(string Title, string PushMessage)
  {
    new PushoverAdapter().Notify("u52wVSuUQ5wWBnHi5uoT8JKg9RrK8j", Title, PushMessage);
  }

  public static string UrlEncode(string Url)
  {
    return Url.Replace("\"", "%22").Replace("#", "%23").Replace("$", "%24").Replace("&", "%26").Replace("@", "%40").Replace("/", "%2F").Replace("+", "%2B").Replace(":", "%3A").Replace(",", "%2C");
  }

  public static string HtmlEncode(string Html)
  {
    return Html.Replace("#", "%23").Replace("/", "%2F").Replace("@", "%40").Replace("&", "%26").Replace("\"", "%22").Replace("+", "%2B");
  }

  public static string RemoveWhiteSpacesFromPhoneNumber_E164(string InputPhoneNumber)
  {
    return Utilities.RemoveWhiteSpacesFromPhoneNumbers_E164(InputPhoneNumber)[0];
  }

  public static string RemoveWhiteSpacesFromPhoneNumber_US(string InputPhoneNumber_US)
  {
    return (InputPhoneNumber_US.StartsWith("+") ? Utilities.RemoveWhiteSpacesFromPhoneNumbers_E164(InputPhoneNumber_US) : Utilities.RemoveWhiteSpacesFromPhoneNumbers_US(InputPhoneNumber_US))[0];
  }

  public static string[] RemoveWhiteSpacesFromPhoneNumbers_US(
    string CommaSeparatedInputPhoneNumbers_US)
  {
    ArrayList arrayList = new ArrayList();
    string[] strArray = CommaSeparatedInputPhoneNumbers_US.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    string str = (string) null;
    foreach (string InputPhoneNumber in strArray)
    {
      if (!InputPhoneNumber.StartsWith("+"))
      {
        foreach (char ch in InputPhoneNumber.ToCharArray())
        {
          int num;
          switch (ch)
          {
            case ' ':
            case '(':
            case '-':
              num = 1;
              break;
            default:
              num = ch == ')' ? 1 : 0;
              break;
          }
          if (num == 0)
            str += ch.ToString();
        }
      }
      else
        str = Utilities.RemoveWhiteSpacesFromPhoneNumber_E164(InputPhoneNumber);
      arrayList.Add((object) str);
      str = (string) null;
    }
    return (string[]) arrayList.ToArray(typeof (string));
  }

  public static string[] RemoveWhiteSpacesFromPhoneNumbers_E164(
    string CommaSeparatedInputPhoneNumbers)
  {
    ArrayList arrayList = new ArrayList();
    string[] strArray = CommaSeparatedInputPhoneNumbers.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    string str1 = (string) null;
    foreach (string str2 in strArray)
    {
      int num1 = str2.IndexOf(" ");
      if (num1 > 1 && num1 <= 3)
      {
        string str3 = str2.Substring(1, num1 - 1);
        foreach (char ch in str2.Substring(num1 + 1).ToCharArray())
        {
          int num2;
          switch (ch)
          {
            case ' ':
            case '(':
            case '-':
              num2 = 1;
              break;
            default:
              num2 = ch == ')' ? 1 : 0;
              break;
          }
          if (num2 == 0)
            str1 += ch.ToString();
        }
        arrayList.Add((object) $"+{str3} {str1}");
        str1 = (string) null;
      }
      else if (num1 == -1)
        arrayList.Add((object) str2);
    }
    return (string[]) arrayList.ToArray(typeof (string));
  }

  public static bool IsValidUSPhoneNumber(string strToCheck, ref string ValidatedPhoneNumber)
  {
    bool flag = true;
    string empty = string.Empty;
    foreach (char ch in strToCheck.ToCharArray())
    {
      if ((ch < '0' || ch > '9') && ch != ' ' && ch != '-' && ch != '(' && ch != ')')
      {
        flag = false;
        break;
      }
      if (ch >= '0' && ch <= '9')
        empty += ch.ToString();
    }
    if (flag && empty.Length != 10)
      return false;
    if (flag)
      ValidatedPhoneNumber = empty;
    else
      flag = Utilities.IsValidE164PhoneNumber(strToCheck, ref ValidatedPhoneNumber);
    return flag;
  }

  public static bool IsValidPostalCode(string PostalCode, string CountryCode)
  {
    bool flag = true;
    if (PostalCode.Trim().Length <= 10)
    {
      if (PostalCode.Contains(" ") || PostalCode.Contains("-"))
      {
        string[] separator = new string[2]{ " ", "-" };
        foreach (string strToCheck in PostalCode.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        {
          if (!Utilities.IsAlphaNumeric(strToCheck))
          {
            flag = false;
            break;
          }
        }
      }
      else if (!Utilities.IsAlphaNumeric(PostalCode))
        flag = false;
    }
    else
      flag = false;
    return flag;
  }

  public static bool IsValidCountryCode(string CountryCode) => true;

  public static bool IsValidE164PhoneNumber(string strToCheck, ref string ValidatedPhoneNumber)
  {
    bool flag = true;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string CountryCode = string.Empty;
    if (strToCheck.StartsWith("+"))
    {
      int num = strToCheck.IndexOf(" ");
      if (num > 1 && num <= 4)
      {
        CountryCode = strToCheck.Substring(1, num - 1);
        if (Utilities.IsValidCountryCode(CountryCode))
        {
          foreach (char ch in strToCheck.Substring(num + 1).ToCharArray())
          {
            if ((ch < '0' || ch > '9') && ch != ' ')
            {
              flag = false;
              break;
            }
            if (ch >= '0' && ch <= '9')
              empty2 += ch.ToString();
          }
          if (flag && empty2.Length > 15)
            flag = false;
        }
      }
      else
      {
        switch (num)
        {
          case -1:
            flag = false;
            break;
          case 1:
            flag = false;
            break;
        }
      }
      if (flag)
        ValidatedPhoneNumber = $"+{CountryCode} {empty2}";
    }
    else
      flag = false;
    return flag;
  }

  public static string CleansePhoneNumberToAllNumbers(string PhoneNumber)
  {
    string empty = string.Empty;
    foreach (char ch in PhoneNumber.ToCharArray())
    {
      if (ch >= '0' && ch <= '9')
        empty += ch.ToString();
    }
    return empty;
  }

  public static bool IsNumeric(string strToCheck)
  {
    bool flag = true;
    foreach (char ch in strToCheck.ToCharArray())
    {
      if (ch < '0' || ch > '9')
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  public static bool IsFYIAppNameValid(string AppName)
  {
    bool flag = true;
    foreach (char ch in AppName.ToCharArray())
    {
      if ((ch < 'a' || ch > 'z') && (ch < 'A' || ch > 'Z') && (ch < '0' || ch > '9') && ch != ' ' && ch != '(' && ch != ')' && ch != '-' && ch != '_')
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  public static bool IsAlphaNumeric(string strToCheck)
  {
    bool flag = true;
    foreach (char ch in strToCheck.ToCharArray())
    {
      if ((ch < 'a' || ch > 'z') && (ch < 'A' || ch > 'Z') && (ch < '0' || ch > '9') && ch != ' ')
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  public static string ResolveTodayDateExpression(string TodayExpression)
  {
    string[] strArray = (string[]) null;
    string str1 = (string) null;
    string str2 = (string) null;
    bool flag = true;
    DateTime result = new DateTime();
    if (!string.IsNullOrEmpty(TodayExpression))
    {
      if (!DateTime.TryParse(TodayExpression, out result))
      {
        foreach (char ch in TodayExpression.ToCharArray())
        {
          if (ch != ' ')
            str1 += ch.ToString();
        }
        if (str1.ToLower() == "[today]")
          str2 = DateTime.Now.ToString("yyyy-MM-dd");
        else if (str1.EndsWith("]"))
        {
          string str3 = (string) null;
          if (str1.Contains("+"))
            str3 = "+";
          else if (str1.Contains("-"))
            str3 = "-";
          if (str3 == "+" || str3 == "-")
          {
            strArray = str1.Substring(1, str1.Length - 2).Split(str3.ToCharArray());
            foreach (char c in strArray[1].ToCharArray())
            {
              if (!char.IsDigit(c))
              {
                flag = false;
                break;
              }
            }
          }
          else
            flag = false;
          if (flag)
          {
            result = DateTime.Now;
            int num = str3 == "+" ? int.Parse(strArray[1]) : -1 * int.Parse(strArray[1]);
            result = result.AddDays((double) num);
            str2 = result.ToShortDateString();
          }
        }
      }
      else
        str2 = TodayExpression;
    }
    return str2;
  }

  public static bool IsURL(string Url)
  {
    return new Regex("(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?").IsMatch(Url);
  }

  public static string ResolveReceipientEmail2SMSAddress(string ToAddress)
  {
    string s;
    if (ToAddress.IndexOf("@") > 0)
    {
      s = ToAddress.Substring(0, ToAddress.IndexOf("@"));
      if (!long.TryParse(s, out long _))
        s = ToAddress;
    }
    else
      s = ToAddress;
    return s;
  }

  public static string DeserializeFYIXMLMessageBody(byte[] FYIXMLMessageBodyBinaryDataArray)
  {
    string str = (string) null;
    MemoryStream serializationStream = new MemoryStream(FYIXMLMessageBodyBinaryDataArray);
    if (serializationStream != null && serializationStream.Length > 0L)
    {
      serializationStream.Position = 0L;
      str = (string) new BinaryFormatter().Deserialize((Stream) serializationStream);
      serializationStream.Close();
    }
    return str;
  }

  public static string StripLeadingAndTrailingQuotes(string Text)
  {
    int num1 = Text.IndexOf("\"");
    int num2 = Text.LastIndexOf("\"");
    if (num1 == 0 && num2 > 0)
      Text = Text.Substring(num1 + 1, num2 - 1);
    return Text;
  }
}
