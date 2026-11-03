<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OptOutThanks.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.OptOutThanks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Opt Out Confirmation</title>
</head>
<body style="background:#ffffff; text-align:left; margin-left:30px;">
    <form id="form1" runat="server">
    <div style="width:700px">
        <div id="logodiv" runat="server">
            <asp:Image ID="Logo" runat="server" />
        </div>
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Unsubscribe Successful" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>
        <div id="optoutdiv" runat="server" style="padding-left:10px;padding-top:10px">
            You have successfully unsubscribed from our email list.
            <br/><br />We make every effort to email only those who have chosen to receive special offers,
            newsletters and communications&nbsp; from us. We believe in honoring our customer's
            privacy choices.
        </div>        
        <div>
            <br />
            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server">Close</asp:LinkButton><br />
        </div>
    </div>
    </form>
</body>
</html>
