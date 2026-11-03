<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalDialog.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.ModalDialog" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>
<div>        
        <asp:Button ID="btnModalDialogDummy" CssClass="AjaxDummyButton" runat="server" Text="" Width="0"/>          
        <asp:Button ID="btnDataGridModalDialogDummy" CssClass="AjaxDummyButton" runat="server" Text="" Width="0" />          
        <asp:Panel ID="PopupPanelForControl" runat="server">                              
            <cc1:ModalPopupExtender
                ID="ModalPopupControl"
                runat="server" 
                OkControlID="btnModalPopupControlDlgOK"
                PopupControlID="pnlModalPopupControlPopup"                                     
                DropShadow="false"             
                BackgroundCssClass="modalBackground"
                TargetControlID="btnModalDialogDummy"                                                
                BehaviorID="ModalPopupControl" 
                Drag=true>
            </cc1:ModalPopupExtender>
                      
            <asp:Panel ID="pnlModalPopupControlPopup" runat="server" style="display:none" CssClass="modalpopup">                
                <div id="container" runat="server">
                    <div class="popupheader">                    
                        <asp:Label ID="TitleLabel" runat="server" CssClass="msg" Text="Title" />
                        <asp:LinkButton ID="PopupControlCloseLinkButton" runat="server" CssClass="popupclose" OnClientClick="$find('ModalPopupControl').hide(); return false;" />
                    </div>
                </div>
                <div id=popupbodyDIV runat=server class="popupbody">                    
                    <asp:Label CssClass="msg" ID="lblModalPopupControlInfo" runat="server" Text="Initial Text" />                                                       
                </div>
                <div class="popupfooter">
                    <asp:Button ID="btnModalPopupControlDlgOK" CssClass="NormalButton" runat="server" Text="OK" />
                </div>            
            </asp:Panel>              
        </asp:Panel>   
        
        <asp:Panel ID="PopupPanelDataGrid" runat="server">                    
          <cc1:ModalPopupExtender
                ID="ModalPopupDataGrid"
                runat="server" 
                OkControlID="btnDlgOKDataGrid"
                PopupControlID="pnlPopupDataGrid"                                     
                DropShadow="false"             
                BackgroundCssClass="modalBackground"
                TargetControlID="btnDataGridModalDialogDummy"                                                
                BehaviorID="ModalPopupDataGrid">
            </cc1:ModalPopupExtender>
                      
            <asp:Panel ID="pnlPopupDataGrid" runat="server" style="display:none" CssClass="modalpopup">                
                <div id="containerdatagrid" runat="server">
                    <div class="popupheader">                    
                        <asp:Label ID="TitleLabelDataGrid" runat="server" CssClass="msg" Text="Title" />
                        <asp:LinkButton ID="DataGridCloseLinkButton" runat="server" CssClass="popupclose" OnClientClick="$find('ModalPopupDataGrid').hide(); return false;" />
                    </div>
                </div>
                <div class="popupbody">                                       
                   <asp:Panel ID=GridViewPanel Height=250px runat=server ScrollBars=Auto>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging1" SkinID="gvGridViewJournal">
                        <PagerSettings Position="Top" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                       <EditRowStyle BackColor="#2461BF" />
                    </asp:GridView>
                   </asp:Panel>
                   
                </div>
                <div class="popupfooter">
                    <asp:Button ID="btnDlgOKDataGrid"  CssClass="NormalButton" runat="server" Text="OK" />
                </div>            
            </asp:Panel>                                   
        </asp:Panel>                                 
   </div>
