// Decompiled with JetBrains decompiler
// Type: Utils.CAMLClauseEvaluator
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using RameshInnovation.VoiShare.OfficeLive;
using System;

#nullable disable
namespace Utils;

public class CAMLClauseEvaluator
{
  public static string EvaluateTodayExpression(
    string TodayExpression,
    UserDateTime.TZ TimeZoneOffset)
  {
    string str = (string) null;
    int num1 = 0;
    string todayExpression = (string) null;
    if (TodayExpression.Length > 0)
    {
      if (TodayExpression.ToLower().StartsWith("[today".ToLower()) && TodayExpression.EndsWith("]"))
      {
        TodayExpression = TodayExpression.Substring(1, TodayExpression.Length - 2).Trim();
        if (TodayExpression.Contains("+"))
          str = TodayExpression.Substring(TodayExpression.IndexOf("+"), 1);
        else if (TodayExpression.Contains("-"))
        {
          str = TodayExpression.Substring(TodayExpression.IndexOf("-"), 1);
          num1 = -1 * int.Parse(TodayExpression.Substring(TodayExpression.IndexOf("-") + 1).Trim());
        }
        else if (TodayExpression.ToLower().Equals("today"))
        {
          str = "ignore";
          num1 = 0;
        }
      }
      if (str == null)
        throw new FormatException();
      DateTime dateTime = UserDateTime.Now(TimeZoneOffset);
      if (!str.Equals("ignore"))
      {
        try
        {
          int num2 = int.Parse(TodayExpression.Substring(TodayExpression.IndexOf(str) + 1).Trim());
          if (str.Equals("-"))
            num2 *= -1;
          todayExpression = dateTime.AddDays((double) num2).ToString("yyyy-MM-dd");
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }
      else
        todayExpression = dateTime.ToString("yyyy-MM-dd");
    }
    return todayExpression;
  }
}
