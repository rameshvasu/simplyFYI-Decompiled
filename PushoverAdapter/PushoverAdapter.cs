// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.Adapters.PushoverAdapter
// Assembly: PushoverAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 51FCF9B3-E244-4EDF-834B-16CF314EE9AF
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\PushoverAdapter.dll

using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

#nullable disable
namespace RameshInnovation.VoiShare.Services.Adapters;

public class PushoverAdapter
{
  public bool Notify(string PushoverUserID, string Title, string Message)
  {
    bool flag = true;
    NameValueCollection data = new NameValueCollection()
    {
      {
        "token",
        "avU8fR1cyjvP6NGNTBjM1ty9CaNdu2"
      },
      {
        "user",
        PushoverUserID
      },
      {
        "message",
        Message
      },
      {
        "title",
        Title
      }
    };
    try
    {
      using (WebClient webClient = new WebClient())
        Encoding.UTF8.GetString(webClient.UploadValues("https://api.pushover.net/1/messages.json", data));
    }
    catch (Exception ex)
    {
      flag = false;
    }
    return flag;
  }
}
