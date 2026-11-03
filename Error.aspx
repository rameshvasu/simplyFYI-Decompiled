<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="RameshInnovation.VoiShare.OfficeLive.Error" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div class="sectiondivider"> 
        <div id="pagedescriptionbar"> 
            <span><asp:Label ID="PageDescription" Text="Oops - something went wrong." runat="server" /></span>                                                                                                                                                                                                                                                                                                   
        </div>
    </div>
    <div style="margin-left:10px;margin-top:10px">
        <asp:Label ID="txtError" runat="server"></asp:Label>
    </div>
    <br /><br />
    <a id="ControlPanelLink" runat=server href="./Start.aspx">Go back to Control Panel</a>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>--%>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
