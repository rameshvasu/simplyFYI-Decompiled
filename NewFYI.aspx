<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.Master" AutoEventWireup="true" CodeBehind="NewFYI.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.NewFYI" Title="Transactional emails made easy for PayPal merchants" %>
<%@ Register Src="Controls/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>

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
        <%--<div style="padding-left:5px"><span id="pagetitle">Give your App a name</span></div>               --%>
        </div>
        <div class="instructioncontainer">
            <div class="instruction">
                <div class="instruction-big">Give your FYI a descriptive name</div>
                <%--<ul class="instruction-list">
                    <li>Enter a descriptive name for your FYI App.</li>
                    <li>Click Next</li>
                </ul> --%>  
            </div>                     
        </div>
        
        <table>
            <tr>
                <td valign="middle"> <asp:TextBox Height="29px" ID="txtNameOfNewFYI" BorderWidth="1px" BorderColor="#dadada" runat="server" Width="425px" MaxLength="120"></asp:TextBox></td>
                <td style="padding-top:12px; padding-left:10px"><asp:Button  ID="Button1" runat="server" Text="Next" OnClick="Button1_Click" TabIndex="1"/></td>
            </tr>
        </table>                     
     <br />
     <div style="margin-left:0px; width:525px;">
        <fieldset class="editorsectionfieldset" id="FYISavedListOrExamplesFieldset" runat="server">
        <legend class="editorsectionlegend" id="FYISavedListOrExamplesLegend" runat="server" ></legend>
            <div style="padding-top:30px; font-size:14px; color:#888"><asp:Label ID="ListOfSavedFYIs" runat="server" Text="Label"></asp:Label></div>
        </fieldset>               
     </div>        
    
    </div>        
    <uc1:ModalDialog ID="ModalDialog1" runat="server" />
</asp:Content>

