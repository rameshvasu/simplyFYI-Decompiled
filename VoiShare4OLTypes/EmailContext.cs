// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.Types.EmailContext
// Assembly: VoiShare4OLTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4300920-3BDD-4B26-9880-36B6DF8D0959
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\VoiShare4OLTypes.dll

using System;
using System.Collections;
using System.Web.SessionState;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive.Types;

[Serializable]
public class EmailContext
{
  public const string HEADERTEXT = "HEADERTEXT";
  public const string HEADERIMAGE = "HEADERIMAGE";
  public const string SHOWHEADERIMAGE = "SHOWHEADERIMAGE";
  public const string HTMLTHEME = "HTMLTHEME";
  public const string SIDECOLUMNCONTENT = "SIDECOLUMNCONTENT";
  public const string INCLUDEUNSUBSCRIBETEXT = "INCLUDEUNSUBSCRIBETEXT";
  public const string TEXTEMAILONLY = "TEXTEMAILONLY";
  public const string ENDPOINTFIELDNAME = "ENDPOINTFIELDNAME";
  public const string CUSTOMEMAILSENDERIDENTITY = "CUSTOMEMAILSENDERIDENTITY";
  public const string WEBSITEURL = "WEBSITEURL";
  public const string FACEBOOKPAGEURL = "FACEBOOKPAGEURL";
  public const string LINKTOFACEBOOKCOMMENTS = "LINKTOFACEBOOKCOMMENTS";
  public const string ENABLESHIPPINGCONFIRMATIONEMAIL = "ENABLESHIPPINGCONFIRMATIONEMAIL";
  private EmailContext.Scribe m_Scribe;
  private EmailContext.Share m_Share;
  [NonSerialized]
  private HttpSessionState m_Session;

  public EmailContext(HttpSessionState Session) => this.m_Session = Session;

  public EmailContext.Scribe EmailScribe
  {
    set => this.m_Scribe = value;
    get => this.m_Scribe;
  }

  public EmailContext.Share EmailShare
  {
    set => this.m_Share = value;
    get => this.m_Share;
  }

  public HttpSessionState Session => this.m_Session;

  public enum Urgency
  {
    SendEchoImmediately = -1, // 0xFFFFFFFF
    SendImmediately = 0,
    SaveDoNotSend = 1,
    SendLater = 2,
    UpdateChanges = 3,
    SendByValue = 4,
  }

  public enum SendMode
  {
    EchoTest,
    Production,
  }

  [Serializable]
  public struct Scribe
  {
    private string m_Subject;
    private string m_From;
    private string m_Message;
    private string m_MessageHTML;
    private string m_MessageHTMLUnformatted;
    private string m_MessagePlainTextUnformatted;
    private string m_Signature;
    private string[] m_FieldTokens;
    private Hashtable m_ExtendedProperties;

    public Hashtable ExtendedProperties
    {
      set => this.m_ExtendedProperties = value;
      get => this.m_ExtendedProperties;
    }

    public string Subject
    {
      set => this.m_Subject = value;
      get => this.m_Subject;
    }

    public string From
    {
      set => this.m_From = value;
      get => this.m_From;
    }

    public string Message
    {
      set => this.m_Message = value;
      get => this.m_Message;
    }

    public string MessageHTML
    {
      set => this.m_MessageHTML = value;
      get => this.m_MessageHTML;
    }

    public string MessageHTMLUnformatted
    {
      set => this.m_MessageHTMLUnformatted = value;
      get => this.m_MessageHTMLUnformatted;
    }

    public string MessagePlainTextUnformatted
    {
      set => this.m_MessagePlainTextUnformatted = value;
      get => this.m_MessagePlainTextUnformatted;
    }

    public string Signature
    {
      set => this.m_Signature = value;
      get => this.m_Signature;
    }

    public string[] FieldTokens
    {
      set => this.m_FieldTokens = value;
      get => this.m_FieldTokens;
    }
  }

  [Serializable]
  public struct Share
  {
    private EmailContext.Urgency m_MessageUrgency;
    private EmailContext.SendMode m_SendMode;
    private DateTime m_SendDateTime;
    private int m_TimeValueOffset;
    private string m_DateValueFieldName;

    public EmailContext.Urgency MessageUrgency
    {
      set => this.m_MessageUrgency = value;
      get => this.m_MessageUrgency;
    }

    public EmailContext.SendMode SendMode
    {
      set => this.m_SendMode = value;
      get => this.m_SendMode;
    }

    public DateTime SendDateTime
    {
      set => this.m_SendDateTime = value;
      get => this.m_SendDateTime;
    }

    public int TimeValueOffset
    {
      set => this.m_TimeValueOffset = value;
      get => this.m_TimeValueOffset;
    }

    public string DateValueFieldName
    {
      set => this.m_DateValueFieldName = value;
      get => this.m_DateValueFieldName;
    }
  }
}
