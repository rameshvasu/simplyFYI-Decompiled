// Decompiled with JetBrains decompiler
// Type: RameshInnovation.SimplyfyiExpress.CommentBox
// Assembly: PayPalIPNHandler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B89D298-7395-4305-9308-85895A08B41A
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\PayPalIPNHandler.dll

using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

#nullable disable
namespace RameshInnovation.SimplyfyiExpress;

public class CommentBox : Page
{
  protected HtmlHead Head1;
  protected HtmlForm form1;

  protected void Page_Load(object sender, EventArgs e)
  {
    if (this.IsPostBack)
      return;
    string str = this.Request["metaid"].ToString();
    if (str != null)
    {
      int int32 = Convert.ToInt32(str);
      EmailContext.Scribe scribeObject = (EmailContext.Scribe) ((FYIDataExpress) new FYIDataAccess().ReadFYIDataFromDB(int32)[(object) int32]).Scribe.ScribeObject;
      string key = scribeObject.ExtendedProperties[(object) "LINKTOFACEBOOKCOMMENTS"].ToString();
      string newValue = scribeObject.ExtendedProperties[(object) key].ToString();
      StreamReader streamReader = new StreamReader(this.Server.MapPath("/App_Data/SocialPluginScripts/Facebook/SOC1_FBComments.xml"));
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(streamReader.ReadToEnd());
      streamReader.Close();
      this.Response.Write(xmlDocument.SelectSingleNode("//facebook/fbcommentscript").InnerXml.Replace("{LINKTOFBCOMMENT}", newValue));
    }
  }
}
