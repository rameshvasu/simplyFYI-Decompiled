<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/MainTemplateWide.master" CodeBehind="Logout.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Logout" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div id="maincontent">
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Login not allowed" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>
        <div style="margin-left:10px;margin-top:10px">                       
            <asp:Label runat="server" ID="lblMessage" class="instruction">The link provided in the email invitation that you received seems to be in error. <br /><br />Please contact the person who sent you the invitation and request that a new invitation be sent to you. <br /><br />Please try to register again with the new link.</asp:Label>
        </div> 
    </div>   
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
