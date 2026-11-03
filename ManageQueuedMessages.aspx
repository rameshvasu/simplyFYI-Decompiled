<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="ManageQueuedMessages.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.ManageQueuedMessages" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register src="Controls/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="headEmbeds" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
<script type = "text/javascript">
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {
            //Get the Cell To find out ColumnIndex
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    //If the header checkbox is checked
                    //check all checkboxes
                    //and highlight all rows

                    row.style.backgroundColor = "#white";
                    if (!inputList[i].disabled)
                        inputList[i].checked = true;
                }

                else {
                    //If the header checkbox is checked
                    //uncheck all checkboxes
                    //and change rowcolor back to original

                    if (row.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        row.style.backgroundColor = "white";
                    }
                    else {
                        row.style.backgroundColor = "white";
                    }

                    inputList[i].checked = false;
                }
            }
        }
    }

</script>
    <%--<meta http-equiv="Refresh" content="5">--%>
    <div class="sectiondivider">     
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>                
    </div>

    <asp:Panel runat="server" ID="InstructionContainerPanel">
         <div class="instructioncontainer">
            <div class="instruction-big">Cancel delivery of queued messages</div>
            <br />
            <div class="instruction">                
                <ul class="instruction-list">
                    <li>You can delete messages that are queued for delivery, on-hold for delivery, or that failed to be sent for any reason.</li>                  
                    <li>
                        <span>QUEUE STATUS:</span>
                        <img style="padding-left:15px" src='./images/queued.png' width='16px' alt='On hold'/><span> Queued for delivery</span>
                        <img style="padding-left:15px" src='./images/failed.png' width='16px' alt='Error'/><span > Unable to send</span>
                        <img style="padding-left:15px" src='./images/sleep.png' width='16px' alt='On hold'/><span > Delivery on hold - insufficient UNITS</span>
                    </li>
                </ul>                
            </div>
        </div>
        <br />
    </asp:Panel>
   
   <div class="tabsectiondivider">
   <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="NormalButton" 
           onclick="btnDelete_Click"/>
    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" 
           EmptyDataRowStyle-Font-Size="14px" 
           EmptyDataText="There are no queued messages to manage." 
           OnRowDataBound="GridView1_RowDataBound" 
           EnableModelValidation="True">
        <Columns>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </EditItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" onclick = "checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

<EmptyDataRowStyle Font-Size="14px"></EmptyDataRowStyle>
    </asp:GridView>
       <uc1:ModalDialog ID="ModalDialog1" runat="server" />
   </div>
    
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
