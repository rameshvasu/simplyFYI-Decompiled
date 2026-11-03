<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="SignUpThanks.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.SignUpThanks" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div id="maincontent">
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Thanks for Signing Up" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>

        <div class="instructioncontainer">
            <div class="instruction-big">You are almost done! Please verify your email address.</div><br />
            <div class="instruction">We have sent you a verification email.  <br /><br />
            Please open it and click the link to verify your email address.
            <br /><br />
            Note: Sometimes your email provider may send the email into your Junk folder. So please look in your Junk folder as well.
            <br /><br />
            </div>                    
        </div>        
    </div>    
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
