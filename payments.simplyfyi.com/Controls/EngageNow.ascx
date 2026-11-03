<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EngageNow.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.EngageNow" %>
<%@ Register Src="WaitControl.ascx" TagName="WaitControl" TagPrefix="uc4" %>
<%@ Register Src="ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc3" %>
<%--<%@ Register Src="FYIDetail.ascx" TagName="FYIDetail" TagPrefix="uc2" %>
<%@ Register Src="FYISummary.ascx" TagName="FYISummary" TagPrefix="uc1" %>--%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
    function showtabs(tabs) {        
        for (i = 0; i < tabs.length; i++) 
        {            
            thisTab = document.getElementById(tabs[i]);
            if (thisTab != null) 
            {
                thisTab.style.display = "block";               
            }                      
        }
    }

    function hidetabs(tabs) {        
        for (i = 0; i < tabs.length; i++) {
            thisTab = document.getElementById(tabs[i]);
            if (thisTab != null) {
                thisTab.style.display = "none";
            }
        }
    }

    function settabfocus(tabid) {
        var thisTab;       

        thisTab = document.getElementById('tab1');
        if (thisTab != null) thisTab.removeAttribute("class");
        thisTab = document.getElementById('tab2');
        if (thisTab != null) thisTab.removeAttribute("class");
        thisTab = document.getElementById('tab3');
        if (thisTab != null) thisTab.removeAttribute("class");
        thisTab = document.getElementById('tab4');
        if (thisTab != null) thisTab.removeAttribute("class");
        thisTab = document.getElementById('tab5');
        if (thisTab != null) thisTab.removeAttribute("class");
        thisTab = document.getElementById('tab6');
        if (thisTab != null) thisTab.removeAttribute("class");

        thisTab = document.getElementById(tabid);
        thisTab.setAttribute("class", "engagenowverticaltab-ul-li-a-selected");

        if (tabid != 'tab1')
            document.getElementById('tab1').setAttribute("class", "engagenowverticaltab-ul-li-a");        
        if (tabid != 'tab2')
            document.getElementById('tab2').setAttribute("class", "engagenowverticaltab-ul-li-a");
         if (tabid != 'tab3')
            document.getElementById('tab3').setAttribute("class", "engagenowverticaltab-ul-li-a");
         if (tabid != 'tab4')
            document.getElementById('tab4').setAttribute("class", "engagenowverticaltab-ul-li-a");
         if (tabid != 'tab5')
             document.getElementById('tab5').setAttribute("class", "engagenowverticaltab-ul-li-a");
         if (tabid != 'tab6')
             document.getElementById('tab6').setAttribute("class", "engagenowverticaltab-ul-li-a");
                  
    }

    function domouseover(tabid) 
    {
        //alert(document.getElementById(tabid).getAttribute("class"));
        var tabclass = document.getElementById(tabid).getAttribute("class");
        
        if (tabclass != "engagenowverticaltab-ul-li-a-selected") {
            document.getElementById(tabid).setAttribute("class", "engagenowverticaltab-ul-li-a-hover");
        }                           
    }

    function domouseout(tabid) {
        var tabclass = document.getElementById(tabid).getAttribute("class");
        //alert(tabclass);
        
        if (tabclass != "engagenowverticaltab-ul-li-a-selected") 
        {
            document.getElementById(tabid).setAttribute("class", "engagenowverticaltab-ul-li-a");
        }        
    }

</script>
<br />
<div class="tabsectiondivider4engagenow">
<div style="text-align:right; display:none">
    <asp:Label ID="lblMessage" runat="server" Text="No saved FYIs found."></asp:Label> 
</div>
<br />
<div style="margin-left:0px;"> 
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>   
            <asp:Panel runat="server" 
                ID="GridViewPanel"                                                              
                BackColor="Transparent"
                BorderStyle="Solid" 
                BorderWidth="0px" Width="875px"> 
                    <div style="float:left">                  
                        <div id="engagenowverticaltabdiv" class="engagenowverticaltab" style="float:left;margin-left:2px; margin-right:10px; padding-top:0px;">                    
                            <div class="instruction">I want to engage with...</div>
                            <ul class="engagenowverticaltab-ul">        
                                <%--<li class="engagenowverticaltab-ul-li"><a onmouseover="javascript:domouseover('tab5');" onmouseout="javascript:domouseout('tab5');" class="engagenowverticaltab-ul-li-a" id='tab5' onclick="javascript:settabfocus('tab5');" href="../EngageNow.aspx?id=5">Sign Up Apps</a></li>                        
                                <li class="engagenowverticaltab-ul-li"><a onmouseover="javascript:domouseover('tab2');" onmouseout="javascript:domouseout('tab2');" class="engagenowverticaltab-ul-li-a" id='tab2' onclick="javascript:settabfocus('tab2');" href="../EngageNow.aspx?id=2">Newsletter Apps</a></li>                        
                                <li class="engagenowverticaltab-ul-li"><a onmouseover="javascript:domouseover('tab3');" onmouseout="javascript:domouseout('tab3');" class="engagenowverticaltab-ul-li-a" id='tab3' onclick="javascript:settabfocus('tab3');" href="../EngageNow.aspx?id=3">Announcement Apps</a></li>                        
                                <li class="engagenowverticaltab-ul-li"><a onmouseover="javascript:domouseover('tab4');" onmouseout="javascript:domouseout('tab4');" class="engagenowverticaltab-ul-li-a" id='tab4' onclick="javascript:settabfocus('tab4');" href="../EngageNow.aspx?id=4">Reminder Apps</a></li>                        
                                <li class="engagenowverticaltab-ul-li"><a onmouseover="javascript:domouseover('tab1');" onmouseout="javascript:domouseout('tab1');" class="engagenowverticaltab-ul-li-a" id='tab1' onclick="javascript:settabfocus('tab1');" href="../EngageNow.aspx?id=1">Other Apps</a></li>--%>

                                <li class="engagenowverticaltab-ul-li"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab5'                                     
                                    PostBackUrl="../EngageNow.aspx?id=5" onclick="tab5_Click">Sign Up Apps</asp:LinkButton>
                                </li> 
                                <li class="engagenowverticaltab-ul-li"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab2'                                     
                                    PostBackUrl="../EngageNow.aspx?id=2" onclick="tab2_Click">Newsletter Apps</asp:LinkButton>
                                </li>  
                                <li class="engagenowverticaltab-ul-li"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab3'                                     
                                    PostBackUrl="../EngageNow.aspx?id=3" onclick="tab3_Click">Announcement Apps</asp:LinkButton>
                                </li>  
                                <li class="engagenowverticaltab-ul-li"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab4'                                     
                                    PostBackUrl="../EngageNow.aspx?id=4" onclick="tab4_Click">Reminder Apps</asp:LinkButton>
                                </li>  
                                <li class="engagenowverticaltab-ul-li"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab1'                                     
                                    PostBackUrl="../EngageNow.aspx?id=1" onclick="tab1_Click">Other Apps</asp:LinkButton>
                                </li> 
                                <li class="engagenowverticaltab-ul-li"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab6'                                     
                                    PostBackUrl="../EngageNow.aspx?id=6" onclick="tab6_Click">PayPal Notifications</asp:LinkButton>
                                </li>                                                                                         
                            </ul>
                        </div>             
                    </div>                                    
                    <div style="float:inherit; margin-left: 200px"> 
                        <div class="instruction">My Apps...</div>  
                        <div style="position:relative; top:-3px">
                            <fieldset class="fieldsetwithbackground">                      
                            <asp:GridView CssClass="GridViewEngageNow" ID="GridView1" runat="server"                                 
                                OnSelectedIndexChanging="GridView1_SelectedIndexChanging"  
                                OnRowDataBound="GridView1_RowDataBound1" AlternatingRowStyle-BackColor="#fafafa" 
                                OnRowCommand="GridView1_RowCommand" DataKeyNames="MetaID" AutoGenerateEditButton="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCreated="GridView1_RowCreated" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">                
                                <Columns>                                                                                        
                                    <asp:TemplateField HeaderText="Open" ShowHeader="False">
                                        <ItemTemplate>
                                            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CommandName="Open" 
                                                ImageUrl="~/images/button-get-started.gif" Height="65%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deactivate">
                                        <ItemTemplate>
                                            &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/images/down-icon.png"
                                                Width="16px" CommandName="ConfirmedDelete" AlternateText="deactivate" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Clone">
                                        <ItemTemplate>
                                            &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" Height="16px" ImageUrl="~/images/clone1.png"
                                                Width="16px" CommandName="Clone" AlternateText="clone" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                        </asp:GridView> 
                        </fieldset>
                        </div>                
                        
                
                    </div>
                                                                   
            </asp:Panel>
        </ContentTemplate>
     </asp:UpdatePanel>
    
     
</div>
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

