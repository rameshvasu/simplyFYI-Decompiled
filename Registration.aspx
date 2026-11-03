<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Registration" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register Src="Controls/Registration.ascx" TagName="Registration" TagPrefix="uc1" %>
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
        </div>
        <div class="instructioncontainer">
            <div class="instruction-big">Manage account settings</div>                      
        </div>
        <br />
        <uc1:Registration ID="Registration1" runat="server" />  
    </div>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
