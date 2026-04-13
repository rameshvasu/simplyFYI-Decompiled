// Decompiled with JetBrains decompiler
// Type: Utils.SessionValidator
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using System;
using System.Web;

#nullable disable
namespace Utils;

public class SessionValidator
{
  public static string GetLogoutJavascript()
  {
    return "<script>window.open('', '_self', ''); window.close(); window.open('Logout2.aspx');</script>";
  }

  private static bool IsSessionActive(HttpContext Context)
  {
    bool flag = true;
    if (!Context.Request.Url.ToString().ToLower().Contains("verify2.aspx"))
      flag = false;
    else if (!Context.Request.Url.ToString().ToLower().Contains("id="))
    {
      flag = false;
    }
    else
    {
      int num = Context.Request.Url.ToString().IndexOf("id=");
      string g = Context.Request.Url.ToString().Substring(num + "id=".Length);
      try
      {
        if (new FYIDataAccess().GetCachedQueryStringFromDB(new Guid(g)) == null)
          flag = false;
      }
      catch
      {
        flag = false;
      }
    }
    return flag;
  }

  private static bool IsExpressSessionActive(HttpContext Context) => true;

  public static void ValidateExpressSession(HttpContext Context)
  {
    if (Context.Session != null)
    {
      if (!Context.Session.IsNewSession)
        return;
      if (Context.Session["EngageUser"] == null)
      {
        Context.Response.Redirect("./Login.aspx");
      }
      else
      {
        string header = Context.Request.Headers["Cookie"];
        if (header != null && header.IndexOf("ASP.NET_SessionId") >= 0)
          Context.Response.Redirect("./Login.aspx");
      }
    }
    else
      Context.Response.Redirect("http://www.simplyfyi.com");
  }

  public static void ValidateSession(HttpContext Context)
  {
    if (Context.Session != null)
    {
      if (Context.Session.IsNewSession)
      {
        string header = Context.Request.Headers["Cookie"];
        if (null != header)
        {
          if (header.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length == 2)
          {
            Context.Response.Redirect("Logout.aspx?sessionended=1");
          }
          else
          {
            if (SessionValidator.IsSessionActive(Context))
              return;
            Context.Response.Redirect("http://www.simplyfyi.com");
          }
        }
        else
        {
          if (SessionValidator.IsSessionActive(Context))
            return;
          Context.Response.Redirect("http://www.simplyfyi.com");
        }
      }
      else if (Context.Request.Url.ToString().ToLower().Contains("verify2.aspx") && Context.Request.QueryString["select"] == null)
        Context.Response.Redirect("SessionActive.aspx");
    }
    else
      Context.Response.Redirect("http://www.simplyfyi.com");
  }
}
