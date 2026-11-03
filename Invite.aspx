<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="Invite.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Invite" Title="Transactional emails made easy for PayPal merchants" %>

<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">         
    <%--<div id="maincontent">  
        <div style="text-align:center">
            <h1 style="font-size:2.1em;font-family:Helvetica,Arial,FreeSans,sans-serif;line-height:1em;padding-top:.66em; color:#669900;">Sign Up / Sign In</h1>       
        <br />        
        
        <table width="100%">
                <tr>                
                    <td align="center">
                        <div id="janrainEngageEmbed"></div>                        
                    </td>                    
                </tr>
            </table>
        </div>                      
    </div> --%>   

    <div id="maincontent">   
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Sign Up" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>
        <br /><br />          
        <div style="text-align:center">                        
            <table width="100%">
                <tr>                    
                    <th align="left" style="padding-left:50px"><span class="instruction-big">Create a Username and Password</span></th>
                </tr>
                <tr>                
                    <td align="left" style="padding-left:50px" >
                        <div id="usercredentials" style="margin-top:20px">
                            <table>
                                <tr>
                                    <td><span class="instruction">Email</span></td>
                                    <td style="padding-left:15px"><asp:TextBox runat="server" Width="200" ID="txtUsername"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><span class="instruction">Password</span></td>
                                    <td style="padding-left:15px"><asp:TextBox runat="server" ID="txtPassword" Width="200" TextMode="Password"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><span class="instruction">Confirm Password</span></td>
                                    <td style="padding-left:15px"><asp:TextBox runat="server" ID="txtPasswordReenter" Width="200" TextMode="Password"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="padding-left:15px" align="right"><asp:Button runat="server" ID="btnSignUp" 
                                            Text="Sign Up" onclick="btnSignUp_Click"/></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="instruction"><div style="padding-top:10px; color:Red"><asp:Label runat="server" ID="lblErrorMessage"></asp:Label></div></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                </table>
            <asp:HiddenField runat="server" ID="HiddenFieldParentCLID" />
        </div>             
    </div>
</asp:Content>

