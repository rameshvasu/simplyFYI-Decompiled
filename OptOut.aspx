<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OptOut.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.OptOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Opt Out</title>
     <link type="text/css" href="app_themes/defaulttheme/styles.css" rel="Stylesheet" />  
</head>
<body>
<%--<div id="mainheader">
	    <div style="padding-top:1px; padding-bottom:5px; border-bottom:solid 0px #dadada; width:100%;">
                        <table width="100%">
                            <tr>
                                <td width=200px align=left>
                                    <span style="padding-left:5px;padding-right:0;" id="logopart1">                            
                                    simply</span><span style="padding-left:0;" id="logopart2">fyi</span>
                                    <span id="beta">beta</span>
                                    <span style="font-size:small; color:#CC6600; font-weight:bold"></span>
                                </td>
                                <td>
                                    <span id="tagline">
                                        <span runat="server" id="taglinecontent">where content becomes communication</span>                           
                                    </span>     
                                </td>
                                <td>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align=left>
                                    <span id="logotagline">on-demand messaging</span>
                                </td>
                                <td colspan=2>                                    
                                </td>
                            </tr>
                        </table>
                    </div>                     
    </div>--%>
    
    <div id="mainwrapper">	
    <div id="copy">
    <div id=maindivwide>
        <div id=maincontent>
            <form id="form1" runat="server">
                <div style="width:700px">
                    <div id="logodiv" runat="server" style="padding-bottom:5px; padding-left:10px; padding-top:5px">
                        <asp:Image ID="Logo" runat="server" />
                    </div>
                    <div class="sectiondivider"> 
                        <div id="pagedescriptionbar"> 
                            <span><asp:Label ID="PageDescription" Text="Unsubscribe" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
                        </div>
                    </div>
                    <div id="optoutdiv" runat="server" style="font-size:14px;padding-top:10px; padding-left:10px" >
                        <h3>You have requested to be unsubscribed (removed) from our mailing list. Your email address will be removed from our list and you will no longer receive email communications from %BusinessName%.</h3>
                    </div>
                    <div style="padding-top:10px; padding-left:10px">
                        <asp:LinkButton ID="OptOutLink" runat="server" OnClick="OptOutLink_Click">Click to unsubscribe</asp:LinkButton>
                    </div>
                </div>
            </form>                                                                                                                                                                                                                                                                                                
        </div>

            
       </div>
    </div>
    </div>
</body>
</html>
