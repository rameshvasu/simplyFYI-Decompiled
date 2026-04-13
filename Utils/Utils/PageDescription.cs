// Decompiled with JetBrains decompiler
// Type: Utils.PageDescription
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using RameshInnovation.VoiShare.OfficeLive;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

#nullable disable
namespace Utils;

public class PageDescription
{
  public static string GetPageDescriptionText(PageDescription.PageID PageIdentifier)
  {
    XmlDocument xmlDocument = new XmlDocument();
    string filename = (string) null;
    switch (PageIdentifier)
    {
      case PageDescription.PageID.SolutionsEMail:
        filename = HttpContext.Current.Server.MapPath("./App_Data/solutions-email.xml");
        break;
      case PageDescription.PageID.SolutionsSMS:
        filename = HttpContext.Current.Server.MapPath("./App_Data/solutions-sms.xml");
        break;
      case PageDescription.PageID.SolutionsIVR:
        filename = HttpContext.Current.Server.MapPath("./App_Data/solutions-ivr.xml");
        break;
    }
    xmlDocument.Load(filename);
    return xmlDocument.OuterXml;
  }

  public static Control GetPageDescription(
    PageDescription.PageID PageIdentifier,
    string PageDescriptionText)
  {
    Label Content = new Label();
    string HeaderStyle = "overflow: auto; font-family:arial; font-size:12px; color:#3c7faf; border-bottom:dotted 0px;";
    string ContentAreaStyle = "font-family:arial; font-size:12px; font-weight:normal; overflow: auto; border-top:dotted 1px #dadada; border-left:dotted 1px #dadada;border-right:dotted 1px #3c7faf;border-bottom:dotted 1px #3c7faf; border-width:1px; background-color:#ffffff;padding:5px;color:#5a5a5a";
    string OuterDivStyle = "padding:5px 5px 5px 5px;";
    string str = "font-family: Verdana, sans-serif, Arial;font-weight: normal;font-size:28px;color:#293f4f;";
    XmlDocument xmlDocument = new XmlDocument();
    string filename = (string) null;
    switch (PageIdentifier)
    {
      case PageDescription.PageID.NewFYI:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-newfyi.xml");
        break;
      case PageDescription.PageID.PickFYI:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-pickfyi.xml");
        break;
      case PageDescription.PageID.Search:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-search.xml");
        break;
      case PageDescription.PageID.Select:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-select.xml");
        break;
      case PageDescription.PageID.EmailScribe_Step2:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-emailscribe.xml");
        break;
      case PageDescription.PageID.EmailScribe_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-emailscribe.xml");
        break;
      case PageDescription.PageID.EmailShare_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-emailshare.xml");
        break;
      case PageDescription.PageID.EmailShare_Step5:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-emailshare.xml");
        break;
      case PageDescription.PageID.SMSScribe_Step2:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-smsscribe.xml");
        break;
      case PageDescription.PageID.SMSScribe_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-smsscribe.xml");
        break;
      case PageDescription.PageID.SMSShare_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-smsshare.xml");
        break;
      case PageDescription.PageID.SMSShare_Step5:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-smsshare.xml");
        break;
      case PageDescription.PageID.VoiceScribe_Step2:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-voicescribe.xml");
        break;
      case PageDescription.PageID.VoiceScribe_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-voicescribe.xml");
        break;
      case PageDescription.PageID.VoiceShare_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-voiceshare.xml");
        break;
      case PageDescription.PageID.VoiceShare_Step5:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-voiceshare.xml");
        break;
      case PageDescription.PageID.ManageQueuedMessages:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-managequeuedmessages.xml");
        break;
      case PageDescription.PageID.MessageAnalytics:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-analytics.xml");
        break;
      case PageDescription.PageID.Registration:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-registration.xml");
        break;
      case PageDescription.PageID.NewRegistration:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-registration.xml");
        break;
      case PageDescription.PageID.ManageLists:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-managelists.xml");
        break;
      case PageDescription.PageID.UploadList_Step2:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-uploadlist.xml");
        break;
      case PageDescription.PageID.UploadList_Step3:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-uploadlist.xml");
        break;
      case PageDescription.PageID.SelectChannel:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-selectchannel.xml");
        break;
      case PageDescription.PageID.IVRResponses:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-ivrresponses.xml");
        break;
      case PageDescription.PageID.PaymentTransactionReport:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-analytics.xml");
        break;
    }
    if (filename != null)
    {
      xmlDocument.Load(filename);
      Content.Text = xmlDocument.OuterXml;
    }
    string HeaderDisplayText = $"<div><span style='float:left'><span style='{str}'>{PageDescriptionText}</span></span><span style='padding-left:5px;font-weight:normal;display:none'>hide help</span></div>";
    string HeaderDisplayNoneText = $"<span style='float:left'><span style='{str}'>{PageDescriptionText}</span></span><span style='padding-left:5px;font-weight:normal;display:none'> help</span>";
    return (Control) DynamicDiv.CreateDIV("ctl00_main_", "9999", HeaderStyle, ContentAreaStyle, (Control) Content, OuterDivStyle, HeaderDisplayText, HeaderDisplayNoneText, "closed", false);
  }

  public static Control GetPageDescription(PageDescription.PageID PageIdentifier)
  {
    Label Content = new Label();
    string HeaderStyle = "overflow: auto; font-family:arial; font-size:12px; color:#3c7faf; border-bottom:dotted 0px;";
    string ContentAreaStyle = "font-family:arial; font-size:12px; font-weight:normal; overflow: auto; border-top:dotted 1px #dadada; border-left:dotted 1px #dadada;border-right:dotted 1px #3c7faf;border-bottom:dotted 1px #3c7faf; border-width:1px; background-color:#ffffff;padding:5px;color:#5a5a5a";
    string OuterDivStyle = "padding:5px 5px 5px 5px;";
    string str1 = "font-family: Verdana, sans-serif, Arial;font-weight: normal;font-size:28px;color:rgb(0, 175, 227);";
    XmlDocument xmlDocument = new XmlDocument();
    string filename = (string) null;
    string str2 = (string) null;
    switch (PageIdentifier)
    {
      case PageDescription.PageID.NewFYI:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-newfyi.xml");
        str2 += "Create a FYI";
        break;
      case PageDescription.PageID.PickFYI:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-pickfyi.xml");
        str2 = "Manage FYIs";
        break;
      case PageDescription.PageID.Search:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-search.xml");
        str2 = "Search lists";
        break;
      case PageDescription.PageID.Select:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-select.xml");
        str2 = "Communication Channel";
        break;
      case PageDescription.PageID.EmailScribe_Step2:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-emailscribe.xml");
        str2 = "Email Message Template";
        break;
      case PageDescription.PageID.EmailScribe_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-emailscribe.xml");
        str2 = "Email Message Template";
        break;
      case PageDescription.PageID.EmailShare_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-emailshare.xml");
        str2 = "Schedule and Deliver Messages";
        break;
      case PageDescription.PageID.EmailShare_Step5:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-emailshare.xml");
        str2 = "Schedule and Deliver Messages";
        break;
      case PageDescription.PageID.SMSScribe_Step2:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-smsscribe.xml");
        str2 = "Text Message Template";
        break;
      case PageDescription.PageID.SMSScribe_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-smsscribe.xml");
        str2 = "Text Message Template";
        break;
      case PageDescription.PageID.SMSShare_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-smsshare.xml");
        str2 = "Schedule and Deliver Messages";
        break;
      case PageDescription.PageID.SMSShare_Step5:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-smsshare.xml");
        str2 = "Schedule and Deliver Messages";
        break;
      case PageDescription.PageID.VoiceScribe_Step2:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-voicescribe.xml");
        str2 = "Voice Message Template";
        break;
      case PageDescription.PageID.VoiceScribe_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-voicescribe.xml");
        str2 = "Create Message Template";
        break;
      case PageDescription.PageID.VoiceShare_Step4:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-voiceshare.xml");
        str2 = "Schedule and Deliver Messages";
        break;
      case PageDescription.PageID.VoiceShare_Step5:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-voiceshare.xml");
        str2 = "Schedule and Deliver Messages";
        break;
      case PageDescription.PageID.ManageQueuedMessages:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-managequeuedmessages.xml");
        str2 = "Queued Messages";
        break;
      case PageDescription.PageID.MessageAnalytics:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-analytics.xml");
        str2 = "Analytics";
        break;
      case PageDescription.PageID.Registration:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-registration.xml");
        str2 = "Your Account";
        break;
      case PageDescription.PageID.NewRegistration:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-registration.xml");
        str2 = "New User Registration";
        break;
      case PageDescription.PageID.ManageLists:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-managelists.xml");
        str2 = "Contact List";
        break;
      case PageDescription.PageID.UploadList_Step2:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-uploadlist.xml");
        str2 = "Set Customer Data to Merge";
        break;
      case PageDescription.PageID.UploadList_Step3:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-uploadlist.xml");
        str2 = "Set Customer Data to Merge";
        break;
      case PageDescription.PageID.SelectChannel:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-selectchannel.xml");
        str2 = "Communication Channel";
        break;
      case PageDescription.PageID.IVRResponses:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-ivrresponses.xml");
        str2 = "Voice Responses";
        break;
      case PageDescription.PageID.EngageNow:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-pickfyi.xml");
        str2 = "Engage Now!";
        break;
      case PageDescription.PageID.SignupBySMS:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-pickfyi.xml");
        str2 = "Signup Customers By SMS";
        break;
      case PageDescription.PageID.PayPalHandler:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-pickfyi.xml");
        str2 = "Manage FYIs";
        break;
      case PageDescription.PageID.PayersContactList:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-pickfyi.xml");
        str2 = "Engage with Payers";
        break;
      case PageDescription.PageID.PaymentTransactionReport:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-ivrresponses.xml");
        str2 = "Payment Transactions";
        break;
      case PageDescription.PageID.SubscriptionTrigger:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-subscriptiontrigger.xml");
        str2 = "Create a FYI - Choose Subscription Trigger";
        break;
      case PageDescription.PageID.DigitalCodes:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-digitalcodes.xml");
        str2 = "View Digital Codes";
        break;
      case PageDescription.PageID.ShipmentConfirmation:
        filename = HttpContext.Current.Server.MapPath("./App_Data/pagedescription-shipmentconfirmation.xml");
        str2 = "Manage Shipment Confirmation Emails";
        break;
    }
    if (filename != null)
    {
      xmlDocument.Load(filename);
      Content.Text = xmlDocument.OuterXml;
    }
    string HeaderDisplayText = $"<span style='float:left'><span style='{str1}'>{str2}</span></span><span style='padding-left:5px;font-weight:normal;display:none'>hide help</span>";
    string HeaderDisplayNoneText = $"<span style='float:left'><span style='{str1}'>{str2}</span></span><span style='padding-left:5px;font-weight:normal;display:none'> help</span>";
    return (Control) DynamicDiv.CreateDIV("ctl00_main_", "9999", HeaderStyle, ContentAreaStyle, (Control) Content, OuterDivStyle, HeaderDisplayText, HeaderDisplayNoneText, "closed", false);
  }

  public enum PageID
  {
    NewFYI,
    PickFYI,
    Search,
    Select,
    EmailScribe_Step2,
    EmailScribe_Step4,
    EmailShare_Step4,
    EmailShare_Step5,
    SMSScribe_Step2,
    SMSScribe_Step4,
    SMSShare_Step4,
    SMSShare_Step5,
    VoiceScribe_Step2,
    VoiceScribe_Step4,
    VoiceShare_Step4,
    VoiceShare_Step5,
    ManageQueuedMessages,
    MessageAnalytics,
    Registration,
    NewRegistration,
    ManageLists,
    UploadList_Step2,
    UploadList_Step3,
    SelectChannel,
    SolutionsEMail,
    SolutionsSMS,
    SolutionsIVR,
    IVRResponses,
    EngageNow,
    SignupBySMS,
    PayPalHandler,
    PayersContactList,
    PaymentTransactionReport,
    SubscriptionTrigger,
    DigitalCodes,
    ShipmentConfirmation,
  }
}
