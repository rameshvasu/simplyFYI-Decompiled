// Decompiled with JetBrains decompiler
// Type: RameshInnovation.SimplyfyiExpress.PayPalIPNTest
// Assembly: PayPalIPNHandler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B89D298-7395-4305-9308-85895A08B41A
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\PayPalIPNHandler.dll

using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Data;
using System.Net;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace RameshInnovation.SimplyfyiExpress;

public class PayPalIPNTest : Page
{
  protected HtmlForm form1;
  protected CheckBox CheckBox1;
  protected DropDownList DDAccounts;
  protected Label Label1;
  protected TextBox TextBox1;
  protected Label Label2;
  protected TextBox TextBox2;
  protected Button Button1;

  protected void Page_Load(object sender, EventArgs e)
  {
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    DataSet allAccountNames = fyiDataAccess.GetAllAccountNames();
    this.DDAccounts.Items.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) allAccountNames.Tables[0].Rows)
    {
      string text = row["businessname"].ToString();
      int int32 = Convert.ToInt32(row["id"]);
      RegistrationData registrationData = fyiDataAccess.GetRegistrationData(int32);
      this.DDAccounts.Items.Add(new ListItem(text, registrationData.WindowsLiveClientID.Trim()));
    }
    this.DDAccounts.SelectedIndex = this.DDAccounts.Items.Count - 1;
  }

  protected void Button1_Click(object sender, EventArgs e)
  {
    string empty = string.Empty;
    new WebClient().UploadString(this.CheckBox1.Checked ? $"http://ipnhandlers.simplyfyi.com/paypal/paypalipnhandler.aspx?clid={this.DDAccounts.SelectedItem.Value}&passthru=1" : "http://localhost:63174/paypalipnhandler.aspx?clid=82acaacd-39f3-45f9-bdeb-b1750f4fb970&passthru=1", "POST", this.TextBox1.Text);
  }
}
