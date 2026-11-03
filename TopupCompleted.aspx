<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="TopupCompleted.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.TopupCompleted" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="sectiondivider"> 
        <div id="pagedescriptionbar"> 
            <span><asp:Label ID="PageDescription" Text="Purchase Status" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
        </div>
    </div>
    
    <div style="margin-left:10px;margin-top:10px;">
        <div>
        <asp:Label ID="TopupStatusMessage" runat="server"></asp:Label>            
        </div>
        <br />
        <div>
            <asp:HyperLink ID="HyperLink1" runat="server">Back to Control Panel</asp:HyperLink>
        </div>        
    </div>                                         
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
