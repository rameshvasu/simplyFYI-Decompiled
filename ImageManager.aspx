<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="ImageManager.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.ImageManager" %>
<%@ Register src="Controls/ImageManager.ascx" tagname="ImageManager" tagprefix="uc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
<script src="/Scripts/lightbox/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="/Scripts/lightbox/lightbox-2.6.min.js" type="text/javascript"></script>
<link href="/App_Themes/lightbox/lightbox.css" rel="stylesheet" />
<div id="maincontent">   
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sectiondivider">     
        <div id="pagedescriptionbar"> 
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>            
        </div>
        <%--<span id="pagetitle">View when messages were queued, sent and their status</span>--%>            
    </div>
    <div class="instructioncontainer">
        <div class="instruction-big">Upload images and manage them</div>
    </div>
    <br />

     <uc1:ImageManager ID="ImageManager1" runat="server" />     
    </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>--%>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
