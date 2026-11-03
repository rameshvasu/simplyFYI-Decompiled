<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="RegistrationNotAllowed.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.RegistrationNotAllowed" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div id="maincontent">
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Registration not allowed" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>
        <div style="margin-left:10px;margin-top:10px">                       
            <div runat="server" id="MessageDIV" class="instruction">The link provided in the email invitation that you received seems to be in error. <br /><br />Please contact the person who sent you the invitation and request that a new invitation be sent to you. <br /><br />Please try to register again with the new link.</div>
        </div>    
    </div>   
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
