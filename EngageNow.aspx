<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="EngageNow.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.EngageNow" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register Src="Controls/WaitControl.ascx" TagName="WaitControl" TagPrefix="uc2" %>

<%@ Register Src="Controls/EngageNow.ascx" TagName="EngageNow" TagPrefix="uc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <script type="text/javascript" src="aspnet_client/scrollsaver.min.js"></script>
    <div id="maincontent">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <div class="sectiondivider">
            <div id="pagedescriptionbar">             
                 <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>            
        </div>
        
         <div class="instructioncontainer4engagenow">                        
            <div class="instruction-big">Launch an App and Engage Now</div>
            <br />
            <div class="instruction">                
                <ul class="instruction-list">
                    <li>Click the type of App you want to use.</li>                    
                    <li>Click "show details" to see what the App does.</li>
                    <li>Click "Get Started".</li>
                </ul>
            </div>                        
        </div>
        
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">  
                    <ContentTemplate> 
                        <uc1:EngageNow ID="EngageNow1" runat="server" />                       
                        <asp:Timer ID="Timer1" runat="server" Interval="100" OnTick="Timer1_Tick" Enabled="False">
                        </asp:Timer>
                        <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="Update1" runat="server">
                            <ProgressTemplate>
                                <div></div>
                                <div class="UpdateProgress" style="width:300px; border: #000000 1px solid; left: 37%; vertical-align: middle; bottom: 50%;">
                                    <table>
                                        <tr>
                                            <td valign="middle"><asp:Image runat="server" ID="imgWait" ImageUrl="./images/ajax-loader.gif" AlternateText="please wait"/></td>
                                            <td valign="middle"><span style="padding-left:5px;padding-right:5px;font-family:Arial;font-size:12px; color:#3C7FAF">processing...</span></td>
                                        </tr>                                    
                                    </table>                                
                                </div>                                                                                                   
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        
         
    </div>        
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
    
</asp:Content>--%>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
