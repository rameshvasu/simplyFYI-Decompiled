<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogFile.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Admin.Log" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Log File</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border-bottom:solid 1px #dadada;">
        <table width=100%>
            <tr>
                <td align=left valign=middle><span runat="server" id="title">LOG FILE</span></td>
                <td><asp:CheckBox ID="chkVerbose" runat="server" Text="Verbose" AutoPostBack="True" OnCheckedChanged="chkVerbose_CheckedChanged" /></td>
                <td valign=middle>                    
                    <span><asp:Button ID="Button1" runat="server" Text="Back" CssClass="NormalButton" OnClick="Button1_Click" /></span></td>
            </tr>
        </table>
     </div>
    <div runat="server" style="text-align:left" id="log">
    
    </div>
    </form>
</body>
</html>
