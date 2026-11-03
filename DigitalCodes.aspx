<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="DigitalCodes.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.DigitalCodes" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div id="maincontent">  
         <div class="sectiondivider">     
            <div id="pagedescriptionbar"> 
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>            
            </div>
        </div>
         <div style="text-align:right"><a id="ControlPanelLink" runat="server" href="./Start.aspx">Go back to Control Panel</a><br /><br /></div>    
        <div class="tabsectiondivider">
            <asp:GridView ID="GridView1" CssClass="GridView" runat="server" OnRowDataBound="GridView1_RowDataBound"></asp:GridView>
        </div>        
    </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>--%>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
