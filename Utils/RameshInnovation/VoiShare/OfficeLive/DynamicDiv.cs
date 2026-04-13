// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.DynamicDiv
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System.Web.UI;
using System.Web.UI.HtmlControls;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive;

public class DynamicDiv
{
  public static HtmlGenericControl CreateDIV(
    string ClientIDBase,
    string ID,
    string HeaderStyle,
    string ContentAreaStyle,
    Control Content,
    string OuterDivStyle,
    string HeaderDisplayText,
    string HeaderDisplayNoneText,
    string InitialState)
  {
    HtmlGenericControl div = new HtmlGenericControl("div");
    div.Attributes["style"] = OuterDivStyle;
    HtmlGenericControl child1 = new HtmlGenericControl("div");
    child1.ID = "divcontent" + ID;
    child1.Attributes["style"] = ContentAreaStyle;
    child1.Controls.Add(Content);
    switch (InitialState)
    {
      case "open":
        child1.Attributes["style"] = ContentAreaStyle;
        break;
      case "closed":
        child1.Attributes["style"] = ContentAreaStyle + ";display:none";
        break;
      case "notoggle":
        child1.Attributes["style"] = ContentAreaStyle;
        break;
    }
    HtmlGenericControl child2 = new HtmlGenericControl("div");
    child2.ID = "divheader" + ID;
    if (!InitialState.Equals("notoggle"))
      child2.Attributes["onClick"] = DynamicDiv.GetDivToggleScript(ClientIDBase + child1.ID, ClientIDBase + child2.ID, HeaderDisplayText, HeaderDisplayNoneText);
    child2.Attributes["onMouseOver"] = $"document.getElementById('{ClientIDBase}{child2.ID}').style.cursor='pointer';";
    child2.Attributes["onMouseOut"] = $"document.getElementById('{ClientIDBase}{child2.ID}').style.cursor='auto';";
    child2.Attributes["style"] = HeaderStyle;
    if (InitialState.Equals("open"))
      child2.InnerHtml = HeaderDisplayText;
    else
      child2.InnerHtml = HeaderDisplayNoneText;
    HtmlGenericControl child3 = new HtmlGenericControl("div");
    child3.Attributes["style"] = "margin-bottom:5px;";
    div.Controls.Add((Control) child2);
    div.Controls.Add((Control) child1);
    div.Controls.Add((Control) child3);
    return div;
  }

  public static HtmlGenericControl CreateDIV(
    string ClientIDBase,
    string ID,
    string HeaderStyle,
    string ContentAreaStyle,
    Control Content,
    string OuterDivStyle,
    string HeaderDisplayText,
    string HeaderDisplayNoneText,
    string InitialState,
    bool Collapsable)
  {
    HtmlGenericControl div = new HtmlGenericControl("div");
    div.Attributes["style"] = OuterDivStyle;
    HtmlGenericControl child1 = new HtmlGenericControl("div");
    child1.ID = "divcontent" + ID;
    child1.Attributes["style"] = ContentAreaStyle;
    child1.Controls.Add(Content);
    if (InitialState.Equals("open"))
      child1.Attributes["style"] = ContentAreaStyle;
    else
      child1.Attributes["style"] = ContentAreaStyle + ";display:none";
    HtmlGenericControl child2 = new HtmlGenericControl("div");
    child2.ID = "divheader" + ID;
    if (Collapsable)
    {
      child2.Attributes["onClick"] = DynamicDiv.GetDivToggleScript(ClientIDBase + child1.ID, ClientIDBase + child2.ID, HeaderDisplayText, HeaderDisplayNoneText);
      child2.Attributes["onMouseOver"] = $"document.getElementById('{ClientIDBase}{child2.ID}').style.cursor='pointer';";
      child2.Attributes["onMouseOut"] = $"document.getElementById('{ClientIDBase}{child2.ID}').style.cursor='auto';";
    }
    child2.Attributes["style"] = HeaderStyle;
    if (InitialState.Equals("open"))
      child2.InnerHtml = HeaderDisplayText;
    else
      child2.InnerHtml = HeaderDisplayNoneText;
    HtmlGenericControl child3 = new HtmlGenericControl("div");
    child3.Attributes["style"] = "margin-bottom:5px;";
    div.Controls.Add((Control) child2);
    div.Controls.Add((Control) child1);
    div.Controls.Add((Control) child3);
    return div;
  }

  private static string GetDivToggleScript(
    string ContentAreaDivID,
    string HeaderDivID,
    string HeaderDisplayText,
    string HeaderDisplayNoneText)
  {
    return $"{$"{$"{$"{$"if (document.getElementById('{ContentAreaDivID}').style.display == '')"}{{$('#{ContentAreaDivID}').hide(500);"}document.getElementById('{HeaderDivID}').innerHTML = \"{HeaderDisplayNoneText}\";" + "}" + "else {"}document.getElementById('{ContentAreaDivID}').style.display = '';"}document.getElementById('{HeaderDivID}').innerHTML = \"{HeaderDisplayText}\";}}";
  }

  private static string GetDivToggleScriptFireFox(
    string ContentAreaDivID,
    string HeaderDivID,
    string HeaderDisplayText,
    string HeaderDisplayNoneText)
  {
    return $"{$"{$"{$"{$"if ({ContentAreaDivID}.style.visibility == 'visible')"}{{{ContentAreaDivID}.style.visibility = 'hidden';"}{HeaderDivID}.innerHTML = \"{HeaderDisplayNoneText}\";" + "}" + "else {"}{ContentAreaDivID}.style.visibility = 'visible';"}{HeaderDivID}.innerHTML = \"{HeaderDisplayText}\";}}";
  }
}
