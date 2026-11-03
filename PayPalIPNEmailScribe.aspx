<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/MainTemplate.master" AutoEventWireup="true" CodeBehind="PayPalIPNEmailScribe.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.PayPalIPNEmailScribe" Title="Transactional emails made easy for PayPal merchants" %>

<%--<%@ Register Src="Controls/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc20" %>--%>

<%@ Register Src="Controls/PayPalIPNEmailScribe.ascx" TagName="EmailScribe2" TagPrefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server"></asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <%--<script type="text/javascript" src="/Scripts/floating-1.12.js"></script>

    <script type="text/javascript">
        floatingMenu.add('floatdiv',
        {
            // Represents distance from left or right browser window  
            // border depending upon property used. Only one should be  
            // specified.  
            //targetLeft: 10,  
            targetRight: 10,

            // Represents distance from top or bottom browser window  
            // border depending upon property used. Only one should be  
            // specified.  
            targetTop: 50,
            // targetBottom: 0,  

            // Uncomment one of those if you need centering on  
            // X- or Y- axis.  
            // centerX: true,  
            // centerY: true,  

            // Remove this one if you don't want snap effect  
            snap: true
        });  
    </script> --%>

    <div id="maincontent">        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>       
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar">                 
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>            
            </div>                        
        </div>   
            
        <div style="padding-left:5px; display:none">
            <span id="pagetitle">                                
                <span class="stepsbubble">1. Personalize</span>
                <span style="color:#9a9a9a">...2. Engage</span> 
            </span>
        </div>
        <br />
        <div class="instructioncontainer"> 
            <div class="instruction-big">Personalize EMAIL message with customer data</div>
            <div class="instruction">
                <table>
                    <tr>
                        <td valign="top"  width="450">
                            <ul class="instruction-list">
                                <li>Specify your "From" identity.</li>
                                <li>Personalize the message.</li>
                                <li>Add Signature.</li>                              
                            </ul> 
                        </td>
                        <td valign="top">
                            <ul class="instruction-list">                                
                                <li>Click Preview to finalize the message.</li>
                                <li>Click "Save Changes" (always save your work often)</li>
                                <li><a href="http://www.simplyfyi.com/Tutorials/TutorialIndex.aspx" target="_blank">HELP - Learn about editing and testing FYI Apps</a></li>
                            </ul> 
                        </td>
                    </tr>
                </table>                               
            </div>
        </div>       

        <%--<asp:UpdatePanel ID="AutoSaveTimerPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
             <ContentTemplate>
                <asp:Timer ID="AutoSaveTimer" runat="server" Interval="5000" ontick="AutoSaveTimer_Tick"></asp:Timer>
             </ContentTemplate>   
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="AutoSaveTimer" EventName="Tick"/>
            </Triggers>       
        </asp:UpdatePanel>--%>

        <asp:UpdatePanel ID="AutoSaveUpdatePanel" runat="server">
            <ContentTemplate>
                <table bgcolor="#ffffff" style="border:solid 0px #dadada">
                    <tr>
                        <td valign=top>
                            <uc1:EmailScribe2 ID="EmailScribe2_1" runat="server" />
                        </td>                
                    </tr>
                </table>                             
            </ContentTemplate>
                       
        </asp:UpdatePanel>               
    </div> 
    
    <%--<uc20:ModalDialog ID="ModalDialog1" runat="server" />--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
    <asp:Button ID="btnNext" runat="server" Text="Save Changes" OnClick="btnNext_Click" />
    
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
