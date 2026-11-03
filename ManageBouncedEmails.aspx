<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="ManageBouncedEmails.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.ManageBouncedEmails" Title="Transactional emails made easy for PayPal merchants" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="headEmbeds" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">   
    <div class="sectiondivider">     
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <span id="pagetitle">Bounced Email Addresses</span>
        <br /><br />
    </div>

    <asp:Panel runat="server" ID="InstructionContainerPanel">
         <div class="instructioncontainer">
            <div class="instruction">
                List of all email IDs that bounced since this account was set up.
            </div>
        </div>
        <br />
    </asp:Panel>
   
    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" EmptyDataRowStyle-Font-Size="14px" EmptyDataText="There are no bounced emails to report." OnRowDataBound="GridView1_RowDataBound">
        
    </asp:GridView>
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
