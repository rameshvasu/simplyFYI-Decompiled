<%@ Page Title="" Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="DigitalCodesFreeOffers.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.DigitalCodesFreeOffers" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
     <div class="sectiondivider"> 
        <div id="pagedescriptionbar"> 
            <span><asp:Label ID="PageDescription" Text="Free Offer Sign Ups" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
        </div>
    </div>
     <div>            
        <span style="float:right; text-align:right"><a id="ControlPanelLink" runat="server" href="./Start.aspx">Go back to Control Panel</a></span>        
    </div>

 <asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>            
         <div style="margin-left:10px;margin-top:20px">
             <asp:Label runat="server">Sign Ups On:&nbsp;</asp:Label>   
             <asp:DropDownList ID="DDFiles" runat="server" OnSelectedIndexChanged="DDFiles_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Download</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Download All</asp:LinkButton>
            </div>
            <br />
    
             <div class="tabsectiondivider">
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" OnRowDataBound="GridView1_RowDataBound"></asp:GridView>
                </div> 
             <br />
             <br />            
    </ContentTemplate>
      <Triggers>
        <asp:PostBackTrigger ControlID="LinkButton1" />
        <asp:PostBackTrigger ControlID="LinkButton2" />
    </Triggers>
</asp:UpdatePanel>

   
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>--%>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
