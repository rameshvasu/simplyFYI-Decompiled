<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Admin.Customers" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register src="~/Controls/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="headEmbeds" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>

<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
    <script type="text/javascript">            
        function pageLoad() {
            $("#<%=datetimepicker.ClientID %>").datetimepicker({format:'m/d/Y H:i'});
        }
    </script>
 
    <script type = "text/javascript">
        function checkAll(objRef) 
        {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) 
            {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) 
                    {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows

                        row.style.backgroundColor = "#white";
                        if (!inputList[i].disabled)
                            inputList[i].checked = true;
                    }

                    else 
                    {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) 
                        {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else 
                        {
                            row.style.backgroundColor = "white";
                        }

                        inputList[i].checked = false;
                    }
                }
            }
        }

</script>     

    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
                <div class="sectiondivider">     
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>                
    </div>

    <asp:Panel runat="server" ID="InstructionContainerPanel">                 
        <div class="instructioncontainer">
            <div class="instruction-big">View payers and send them email newsletter and announcements</div>
        </div>
    </asp:Panel>       
    <div id="headernav">
        <%--<asp:Panel runat="server" ID="SendContentPanel">
            <div id="sendheading" style="min-width:500; cursor:pointer; z-index:-100;">
                <div id='senddescription'>
                    <table width="100%">
                        <tr>
                            <td style="width:36px"><img src="images/yellow_mail_send.png" alt="send emails" width="32px"/></td>
                            <td style="width:600px" valign="middle">Click to send newsletters and announcements to payers</td>
                            <td align="right"><asp:Label runat="server" ID="lblSendStatus" ForeColor="Green"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div id="sendcontent">
                    <h3>Select an email FYI to send to all payers:</h3>
                    <asp:DropDownList runat="server" ID="DDEmailFYIList"></asp:DropDownList>                
                    <div><asp:Button CssClass="NormalButton" ID="btnInviteUsers" runat="server" Text="Invite" /></div>            
                </div>                                         
            </div>
        </asp:Panel>--%>
         <asp:Panel runat="server" ID="NoFYIPanel">
         </asp:Panel>
        
        <div id="sendcontent" runat="server">
        <%--<h3>Select an email FYI to send to all payers:</h3>--%>
            <table>
                <tr>
                    <td></td>
                    <td valign="middle">Select:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="DDEmailFYIList" Width="200"></asp:DropDownList> 
                    </td>
                    <td>                        
                        <span style="padding-left:15px; padding-right:5px">Date/Time to send:</span><asp:TextBox ID="datetimepicker" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <div style="padding-left:5px"><asp:Button CssClass="NormalButton" 
                                ID="btnSendEmails" runat="server" Text="Send" onclick="btnSendEmails_Click" /></div>  
                    </td> 
                    <td>
                        <div style="padding-left:5px"><asp:Label ID="lblMessage" runat="server" ForeColor="Red" /></div>  
                    </td>                   
                </tr>
            </table>                                              
        </div>         
   </div>           
        
   <div style="clear:both">
        <div class="tabsectiondivider">
             <asp:GridView ID="GridView1" CssClass="GridView" runat="server" 
                 EmptyDataRowStyle-Font-Size="14px" EmptyDataText="There are no payers on file." 
                 OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True" 
                 onrowcreated="GridView1_RowCreated">        
                 <Columns>
                     <asp:TemplateField HeaderText="Engage">
                         <EditItemTemplate>
                             <asp:CheckBox ID="CheckBox1" runat="server" />
                         </EditItemTemplate>
                         <HeaderTemplate>
                             <asp:CheckBox ID="CheckBox2" runat="server" onclick = "checkAll(this);"/>
                         </HeaderTemplate>
                         <ItemTemplate>
                             <asp:CheckBox ID="CheckBox1" runat="server" />
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
<EmptyDataRowStyle Font-Size="14px"></EmptyDataRowStyle>
             </asp:GridView>                    
             <br />
             <uc1:ModalDialog ID="ModalDialog1" runat="server" />
       </div>
   </div>   
        </ContentTemplate>
    </asp:UpdatePanel>


    
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>

