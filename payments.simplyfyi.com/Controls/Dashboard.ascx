<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.Dashboard" %>    
    <div id='reportdisplaydiv' runat="server"></div>    
        <div>
            <div id='reportdiv' runat="server">
            <fieldset class="fieldsetwithoutborder">
                <table>
                    <tr>                   
                        <td style="border-right:dashed 0px #bebebe;border-bottom:dashed 0px #bebebe; padding:10px;" valign="top"><asp:Label ID="ChartArea1" runat="server"></asp:Label></td>
                        <td style="border-bottom:dashed 0px #bebebe; padding:10px;" valign="middle"><asp:Label ID="GraphData" runat="server"></asp:Label></td>                    
                    </tr>
                </table>                
            </fieldset>
            <div style="clear:both; padding-top: 5px">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"  
                    EmptyDataText="As of this time, there are no opened messages to report for this batch."                  
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    PageSize="1000"
                    CssClass="GridViewStats" Width="100%" OnRowDataBound="GridView1_RowDataBound"  EmptyDataRowStyle-Font-Size="14px">
                </asp:GridView>
            </div>
        </div>    
    </div>  
    
