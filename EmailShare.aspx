<%@ Page Language="C#" MasterPageFile="~/MainTemplate.master" AutoEventWireup="true" CodeBehind="EmailShare.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.EmailShare" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register Src="Controls/EmailShare.ascx" TagName="EmailShare" TagPrefix="uc1" %>
<%@ Register Src="Controls/DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="uc2" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div id="maincontent">        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar">                  
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>            
            </div>               
        </div>
         <div class="instructioncontainer">
            <div style="padding-left:5px"><span id=pagetitle>
                <span style="color:#9a9a9a;padding-left:0px">1. Sync ... </span>
                <span style="color:#9a9a9a">2. Personalize ...</span>
                 <span class="stepsbubble">3. Engage</span>
            </span></div><br />
       
            <div class="instruction-big">Engage each customer with a personalized EMAIL message</div><br />
            <div class="instruction">
                <ul class="instruction-list">                        
                    <li>Select one of the options below.</li>                                          
                    <li>Click Save/Submit.</li>                        
                </ul>
            </div>
        </div>
        <div style="float:left">
            <uc2:DateTimePicker id="DateTimePicker1" runat="server">
        </uc2:DateTimePicker>
        </div>         
       <%--  <div style="float:right;width:352px; padding-right:100px; padding-top:10px">         
             <div style="float:right; padding-right:100px; padding-top:10px">
                <div style="padding:3px 3px 0px 3px;width:350px; background-color:#f9f9f9; color:#3c7faf; font-weight:normal; text-align:center;border: solid 1px #dbe1e6; font-size:24px">REMAINING UNITS</div>
                <div id="startpagebalancediv123" runat="server" style="padding:3px 3px 3px 3px; width:350px; background-color:#ffffff; border-left: solid 1px #dbe1e6;border-right: solid 1px #dbe1e6;border-bottom: solid 1px #dbe1e6; font-size:14px;">                
                Your account has 250 units available for use. To add more units click the "Topup" button.
                </div>
               
                <div>                   
                    <div style="float:right;padding-top:15px">
                        <asp:LinkButton ID="BalanceRefreshLink" runat="server" OnClick="BalanceRefreshLink_Click">Refresh</asp:LinkButton>
                    </div>
                
                </div>      
            </div>   
         </div>--%>
                          
    </div>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">    
    <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnNext_Click" />
    <div style="padding-top:50px;">
        <div id="startpagebalancediv" runat="server" style="padding:3px 3px 3px 10px; background-color:#ffffff; border-left: solid 0px #dbe1e6;border-right: solid 0px #dbe1e6;border-bottom: solid 0px #dbe1e6; font-size:14px;">                
        Your account has 250 units available for use. To add more units click the "Topup" button.
        </div>
         <div>
            <asp:Button ID="Topup" CssClass="NormalButton" runat="server" Text="Buy UNITS" OnClick="Topup_Click" />
        </div>
    </div>                 
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
