<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="PickFYI.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.PickFYI" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register Src="Controls/WaitControl.ascx" TagName="WaitControl" TagPrefix="uc2" %>

<%@ Register Src="Controls/ManageFYI.ascx" TagName="ManageFYI" TagPrefix="uc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div id="maincontent">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <div class="sectiondivider">
            <div id="pagedescriptionbar">             
                 <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
            <%--<span style="padding-left:5px"><span id="pagetitle">Run or Edit a FYI</span></span>--%>
        </div>
        <%--<uc1:ManageFYI ID="ManageFYI1" runat="server" />--%>
        <div class="instructioncontainer">
            <%--<div class="instruction-big">Run or Edit a FYI</div>--%>
            <div class="instruction-big">Choose an action</div>
            <div class="instruction"> 
                <table>
                    <tr>
                        <td>
                            <ul class="instruction-list">                   
                                <li><strong>Open:</strong> Open the App and edit message content</li>                                
                                <li><strong>Rename:</strong> Type a new name for the App, click "Rename" again</li>                    
                                <li><strong>Shelve:</strong> Shelve the App for now</li> 
                            </ul>
                        </td>
                        <td style="padding-left:10px">
                            <ul class="instruction-list">                   
                                <li><strong>Clone:</strong> Create a copy of the App. Then, rename it</li>
                                <li><strong>Unshelve:</strong> Move App to the current Apps list</li>
                                <li><strong>App Category:</strong> Set the engagement category that best fits the App</li>   
                            </ul>
                        </td>
                    </tr>
                </table>
               <%-- <ul class="instruction-list">                   
                    <li><strong>Rename:</strong> Type a new name for the App, click "Rename" again.</li>                    
                    <li><strong>Open:</strong> Edit or Run the App.</li>
                    <li><strong>Deactivate:</strong> Move App to the "Deactivated FYIs" list.</li>  
                    <li><strong>Clone:</strong> Create a copy of the App. Then, rename it.</li>
                    <li><strong>Activate:</strong> Move App to the active Apps list.</li>
                    <li><strong>App Category:</strong> Set the engagement category that best fits the App.</li>                  
                </ul>--%>
            </div>
        </div>
         <div style="clear:both">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">  
                    <ContentTemplate> 
                        <uc1:ManageFYI ID="ManageFYI1" runat="server" />                       
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
