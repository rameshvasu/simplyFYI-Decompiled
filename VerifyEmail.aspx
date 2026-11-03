<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="VerifyEmail.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.VerifyEmail" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div id="maincontent">
        <div class="sectiondivider"> 
            <div id="pagedescriptionbar"> 
                <span><asp:Label ID="PageDescription" Text="Email Verification Error" runat="server" /></span>                                                                                                                                                                                                                                                                                                   
            </div>
        </div>

        <div class="instructioncontainer">
             <div style="vertical-align:middle;"><span class="instruction-big">Oops! We are unable to verify your email address.</span>
             </div>

            <br />           
            <div class="instruction">Let's try again.<br /><br />
            Please open the verification email that we sent you and click the link again to complete the sign up process.            
            </div>                    
        </div>        
    </div>    
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
