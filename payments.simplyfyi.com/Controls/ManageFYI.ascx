<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageFYI.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.ManageFYI" %>
<%@ Register Src="WaitControl.ascx" TagName="WaitControl" TagPrefix="uc4" %>
<%@ Register Src="ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc3" %>
<%--<%@ Register Src="FYIDetail.ascx" TagName="FYIDetail" TagPrefix="uc2" %>
<%@ Register Src="FYISummary.ascx" TagName="FYISummary" TagPrefix="uc1" %>--%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function ToggleDeactiavtedFYIDIV()
    {
        //var DeactivatedFYIsDIV = $find('DeactivatedFYIsDIV'); 
        var DeactivatedFYIsDIV = document.getElementById("DeactivatedFYIsDIV");
        var DeactivatedFYIsOuterDIV = document.getElementById("<%=DeactivatedFYIsOuterDIV.ClientID%>");

        //alert(DeactivatedFYIsOuterDIV.firstChild.nodeValue);

        if (DeactivatedFYIsDIV.style.display == '')               
        {
	        DeactivatedFYIsDIV.style.display = 'none';
	        DeactivatedFYIsOuterDIV.firstChild.nodeValue = "+ Shelved FYIs";        
        }
        else
        {
	        DeactivatedFYIsDIV.style.display = '';
	        DeactivatedFYIsOuterDIV.firstChild.nodeValue = "- Shelved FYIs";                             
        }
	}

	function UpdateAppCategory(DropDownID) {
	    //See http://www.codegateway.com/2012/03/get-dropdown-selected-item-onchange-in.html
	    
	    var e = document.getElementById(DropDownID);
	    var val = e.options[e.selectedIndex].value;
	    $.get("./ClientWebServices.aspx?cmd=1&param=" + val);
	}       
</script>

<div style="text-align:right; display:none">
    <asp:Label ID="lblMessage" runat="server" Text="No saved FYIs found."></asp:Label> 
</div>
<br />
<div style="margin-left:0px;">    
    <asp:Panel runat="server" 
        ID="GridViewPanel"                                                                
        BackColor="Transparent" 
        BorderStyle="Solid" 
        BorderWidth="0px" Width="100%">                                     
            <asp:GridView CssClass="GridView" Width="100%" ID="GridView1" runat="server"                                
                OnSelectedIndexChanging="GridView1_SelectedIndexChanging" 
                OnRowDataBound="GridView1_RowDataBound1" 
                OnRowCommand="GridView1_RowCommand" DataKeyNames="MetaID" AutoGenerateEditButton="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCreated="GridView1_RowCreated" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">                
                <Columns>                                                                                        
                    <asp:TemplateField HeaderText="Open" ShowHeader="False">
                        <ItemTemplate>
                            <%--&nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CommandName="Open" Height="16px"
                                ImageUrl="~/images/open.png" Width="16px" />--%>
                            &nbsp;<asp:LinkButton ID="ImageButton2" runat="server" CommandName="Open" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shelve It!">
                        <ItemTemplate>
                            <%--&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/images/down-icon.png"
                                Width="16px" CommandName="ConfirmedDelete" AlternateText="deactivate" />--%>
                            &nbsp;<asp:LinkButton ID="ImageButton1" runat="server" CommandName="ConfirmedDelete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Clone">
                        <ItemTemplate>
                            <%--&nbsp;<asp:ImageButton ID="ImageButton4" runat="server" Height="16px" ImageUrl="~/images/clone1.png"
                                Width="16px" CommandName="Clone" AlternateText="clone" />--%>
                                &nbsp;<asp:LinkButton ID="ImageButton4" runat="server" CommandName="Clone" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>                                                        
    </asp:Panel>
    <asp:Panel ID="DeletedGridViewPanel" runat="server" BackColor="Transparent" 
        BorderStyle="Solid" 
        BorderWidth="0px" Width="100%">
        <br />
        <div id="DeactivatedFYIsOuterDIV" runat=server onclick="ToggleDeactiavtedFYIDIV();"  style="color: #3C7FAF;	font-family: Verdana, sans-serif, Arial;font-weight:normal;font-size:16px;margin-bottom:5px;cursor:pointer">+ Shelved FYIs</div>
        <div id="DeactivatedFYIsDIV" style="display:none">
            <asp:GridView ID="GridView2" CssClass="GridView" runat="server" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand">                
                <Columns>
                    <asp:TemplateField HeaderText="Unshelve it!">
                        <ItemTemplate>
                            <%--<asp:ImageButton ID="ImageButton3" runat="server" CommandName="Unshelve" Height="16px" ImageUrl="~/images/up-icon.png"
                                Width="16px" AlternateText="activate" />--%>
                            &nbsp;<asp:LinkButton ID="ImageButton3" runat="server" CommandName="Unshelve" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
     
</div>

<asp:Button ID="btnDummy" CssClass="AjaxDummyButton" runat="server" Text="" Width="0px" />
<asp:Panel id="PopupPanel" runat="server">
    <cc1:ModalPopupExtender 
                ID="ModalPopup"
                runat="server" 
                OkControlID="btnDlgNo"                
                PopupControlID="pnlPopup"                                     
                DropShadow="false"             
                BackgroundCssClass="modalBackground"
                TargetControlID="btnDummy"
                DynamicControlID="lblInfo"             
                DynamicServiceMethod="GetDynamicContent" 
                DynamicContextKey='font-weight:bold'                 
                BehaviorID="ModalPopup">
    </cc1:ModalPopupExtender>
    &nbsp;
    <asp:Panel ID="pnlPopup" runat="server" style="display: none" CssClass="modalPopup">
        <table border="0" cellpadding="0" cellspacing="0" style="width: auto;" id="tblPopup">
            <tr>
                <td class="heading" style="text-align: center">
                    <span><strong>Delete FYI</strong></span></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblInfo" runat="server">Message</asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnDlgOK" runat="server" Text="Yes" OnClick="btnDlgOK_Click" />                    
                    <asp:Button ID="btnDlgNo" runat="server" Text=" No " />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="RowIndexToDelete" runat="server" Visible="False" />
    </asp:Panel>
</asp:Panel>
<asp:SqlDataSource ID="SimplyFYI" runat="server" ConnectionString="<%$ ConnectionStrings:SimplyFYIConnectionString %>" SelectCommand="SELECT [MetaID] , [MetaDataName] as [FYI], [DateCreated]  as [Created] FROM [FYIMetaMessages] &#13;&#10;INNER JOIN ACCOUNTS on accounts.id=fyimetamessages.fyiaccountid&#13;&#10;WHERE [accountid] in &#13;&#10;(&#13;&#10;select AccountID from accounts&#13;&#10;where AccountID=@AccountID&#13;&#10;)" InsertCommand="INSERT INTO [FYIMetaMessages] ([MetaDataName], [DateCreated]) VALUES (@MetaDataName, @DateCreated)" UpdateCommand="UPDATE FYIMetaMessages SET MetaDataName = @MetaDataName, DateCreated = @DateCreated WHERE (MetaID = @MetaID)" DeleteCommand="DELETE FROM [FYIMetaMessages] WHERE  [MetaID] = @MetaID" ProviderName="<%$ ConnectionStrings:SimplyFYIConnectionString.ProviderName %>">
    <UpdateParameters>
        <asp:Parameter Name="MetaDataName" Type="String" />
        <asp:Parameter Name="DateCreated" />
        <asp:Parameter Name="MetaID" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="MetaDataName" Type="String" />
        <asp:Parameter Name="DateCreated" Type="DateTime" />
    </InsertParameters>
    <DeleteParameters>
        <asp:Parameter Name="MetaID" Type="Int32" />
    </DeleteParameters>
    <SelectParameters>
        <asp:SessionParameter Name="AccountID" SessionField="AccountID" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SimplyFYIConnectionString %>" ProviderName="<%$ ConnectionStrings:SimplyFYIConnectionString.ProviderName %>"></asp:SqlDataSource>
<br />
<uc3:ModalDialog ID="ModalDialog1" runat="server" />
&nbsp;&nbsp;

