<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentTransactions.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.PaymentTransactions" %>    
    <div id='reportdisplaydiv' runat="server"></div>    
        <div>
            <div id='reportdiv' runat="server">
            <fieldset class="fieldsetwithoutborder">
               <table style="display:none">
                    <tr>                   
                        <td style="border-right:dashed 1px #bebebe;border-bottom:dashed 1px #bebebe; padding:10px;" valign="top"><asp:Label ID="ChartArea1" runat="server"></asp:Label></td>
                        <td style="border-bottom:dashed 1px #bebebe; padding:10px;" valign="top"><asp:Label ID="GraphData" runat="server"></asp:Label></td>                    
                    </tr>
                </table>
            </fieldset>
            <div style="clear:both; padding-top: 5px">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    EmptyDataText="No messages found."
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    PageSize="1000"
                    CssClass="GridViewStats" Width="100%" OnRowDataBound="GridView1_RowDataBound"  EmptyDataRowStyle-Font-Size="14px">
                </asp:GridView>
                <asp:Label runat="server" ID="lblTransactionDetails" BackColor="Wheat"></asp:Label>
            </div>
        </div>    
    </div>  
    
