<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WaitControl.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.WaitControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>      
<div>
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">  
                    <ContentTemplate>                    &nbsp;
                        <asp:Timer ID="Timer1" runat="server" Interval="100" OnTick="Timer1_Tick" Enabled="False">
                        </asp:Timer>
                        <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="Update1" runat="server">
                            <ProgressTemplate>
                                <div></div>
                                <div class="UpdateProgress" style="width:300px; border-top: #dadada 1px solid; left: 37%; vertical-align: middle; bottom: 50%;">
                                    <table>
                                        <tr>
                                            <td valign="middle"><asp:Image runat="server" ID="imgWait" ImageUrl="../images/ajax-loader.gif" AlternateText="please wait"/></td>
                                            <td valign="middle"><span id="waitmessage" runat="server" style="padding-left:5px;padding-right:5px;font-family:Arial;font-size:14px; color:#3C7FAF">processing...</span></td>
                                        </tr>                                    
                                    </table>                                
                                </div>   
                                
                               <%-- <div id="logosplashdiv" style="left: 37%; bottom: 60%; width: 308px;">
                                    <div>
                                        <span style="padding-right:0;" id="logopart1">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/simplyfyi-icon.bmp" />simply</span><span style="padding-left:0;" id="logopart2">fyi</span>                                                
                                    </div>
                                    <div>
                                        <span id="splashtagline">where content becomes communication</span>  
                                    </div>                           
                                </div>--%>
                                
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>