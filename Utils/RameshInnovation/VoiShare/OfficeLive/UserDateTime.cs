// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.UserDateTime
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive;

public class UserDateTime
{
  private static string GetTZModifier(UserDateTime.TZ UserTimeZone)
  {
    int startIndex = 0;
    string tzModifier = ((int) UserTimeZone).ToString();
    if (tzModifier == "0")
    {
      tzModifier = "+00:00";
    }
    else
    {
      string str;
      if (tzModifier.StartsWith("-"))
      {
        str = "-";
        startIndex = 1;
      }
      else
        str = "+";
      if (tzModifier.Substring(startIndex).Length == 1)
        tzModifier = $"{str}00:0{tzModifier.Substring(1)}";
      else if (tzModifier.Substring(startIndex).Length == 2)
        tzModifier = $"{str}00:{tzModifier.ToString()}";
      else if (tzModifier.Substring(startIndex).Length == 3)
        tzModifier = $"{str}0{tzModifier.Substring(startIndex, 1)}:{tzModifier.Substring(startIndex + 1)}";
      else if (tzModifier.Substring(startIndex).Length == 4)
        tzModifier = $"{str}{tzModifier.Substring(startIndex, 2)}:{tzModifier.Substring(startIndex + 2)}";
    }
    return tzModifier;
  }

  public static DateTime Now(UserDateTime.TZ UserTimeZone)
  {
    int num1 = 0;
    string tzModifier = UserDateTime.GetTZModifier(UserTimeZone);
    DateTime now = DateTime.Now;
    if (now.IsDaylightSavingTime())
      num1 = 60;
    DateTime universalTime = now.ToUniversalTime();
    int num2 = int.Parse(tzModifier.Substring(0, 3));
    int num3 = int.Parse(tzModifier.Substring(4));
    if (num2 < 0)
      num3 *= -1;
    int num4 = num2 * 60 + num3 + num1;
    return universalTime.AddMinutes((double) num4);
  }

  public static DateTime NowUTC() => DateTime.Now.ToUniversalTime();

  public static DateTime UTCToUserTime(DateTime UTC, UserDateTime.TZ UserTimeZone)
  {
    UTC.ToLocalTime().IsDaylightSavingTime();
    int num1 = 0;
    string tzModifier = UserDateTime.GetTZModifier(UserTimeZone);
    int num2 = int.Parse(tzModifier.Substring(0, 3));
    int num3 = int.Parse(tzModifier.Substring(4));
    if (num2 < 0)
      num3 *= -1;
    int num4 = num2 * 60 + num3 + num1;
    return UTC.AddMinutes((double) num4);
  }

  public static DateTime UserTimeToUTC(DateTime UserTime, UserDateTime.TZ UserTimeZone)
  {
    UserTime.IsDaylightSavingTime();
    int num1 = 0;
    string tzModifier = UserDateTime.GetTZModifier(UserTimeZone);
    int num2 = int.Parse(tzModifier.Substring(0, 3));
    int num3 = int.Parse(tzModifier.Substring(4));
    int num4;
    if (num2 < 0)
    {
      num4 = num2 * -1;
    }
    else
    {
      num4 = num2 * -1;
      num3 *= -1;
    }
    int num5 = num4 * 60 + num3 + num1;
    return UserTime.AddMinutes((double) num5);
  }

  public enum TZ
  {
    USPacificStandard = -800, // 0xFFFFFCE0
    USMountainStandard = -700, // 0xFFFFFD44
    USCentralStandard = -600, // 0xFFFFFDA8
    USEasternStandard = -500, // 0xFFFFFE0C
    USAtlanticStandard = -400, // 0xFFFFFE70
    GMT = 0,
    IndianStandard = 530, // 0x00000212
  }
}
