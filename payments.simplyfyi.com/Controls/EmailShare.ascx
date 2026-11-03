<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailShare.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.EmailShare" %>
<%@ Register Src="DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="uc1" %>
<div class="contentframe"> 
<table width="542">               
    <tr>        
        <td valign="top" align="left">
            <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px">
                <uc1:DateTimePicker ID="DateTimePicker1" runat="server" /> 
            </asp:Panel>                 
        </td>                        
    </tr>    
</table>
</div>

