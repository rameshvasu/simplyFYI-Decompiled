<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayerSignUpMobile.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.PayerSignUpMobile" Title="Transactional emails made easy for PayPal merchants" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="background:#fff; max-width:600px">
    <form id="form1" runat="server">
        <div style="margin-bottom:10px;max-width:400px;font-size:16px; text-align:left; color:#000; border-bottom:1px solid #000">simplyFYI - Add/Update Customer Contact</div>
        <table style="max-width:600px">    
                <tr>
                    <td style="padding-top:5px; text-align:left;width:100px">
                        Transaction ID</td>
                    <td style="padding-top:5px; text-align:left">
                        <asp:Label BorderWidth="1px" BorderColor="#cacaca" ID="lblTransactionID" runat="server" Width="266px" Text="Transaction ID"></asp:Label>
                    </td>                    
                </tr> 
                <tr>
                    <td style="padding-top:5px; text-align:left;width:100px">
                        Full Name</td>
                    <td style="padding-top:5px; text-align:left">
                        <asp:TextBox BorderWidth="1px" BorderColor="#cacaca" ID="txtFullName" runat="server" MaxLength="50" Width="266px"></asp:TextBox>
                    </td>                    
                </tr>                
                <tr>
                    <td style="padding-top:5px; text-align:left;width:100px">
                        Email</td>
                    <td style="padding-top:5px; text-align:left">
                        <asp:TextBox ID="txtEmail" BorderWidth="1px" BorderColor="#cacaca" runat="server" MaxLength="50" ReadOnly="True" Width="266px"></asp:TextBox>
                    </td>                    
                </tr>
                <tr>
                <%--<td>
                    <asp:HiddenField ID="HiddenFieldInvoiceID" runat="server" />
                    </td>--%>
                <td colspan="2" style="text-align:right"><asp:Button ID="btnSignUp" runat="server" 
                        Text="Submit" /> </td>
                <td></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblErrorMessage" runat="server" Visible="false"/>
                    </td>                    
                </tr>
            
            </table>
    </form>
</body>
</html>   

   
