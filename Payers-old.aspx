<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="Payers.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Payers" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register src="Controls/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="headEmbeds" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>

<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
    <%--<script type="text/javascript">
        $(function () {
            $(document).ready(function () {
                // Invite user                
                $('#sendcontent').hide();

                $("#<%=btnSendEmails.ClientID %>").bind('click', function () {
                    setTimeout(function () {
                        $("#<%=lblSendStatus.ClientID %>").hide(1000);
                    }, 10000);
                });

                $("#sendheading").click(function () {

                    if ($("#<%= btnSendEmails.ClientID %>").is(":focus")) {
                        $("#senddescription").hide(500);
                        return;
                    }

                    $("#sendcontent").slideToggle(500);

                    //alert($("#sendcontent").height());
                    if ($("#sendcontent").height() == 1) {
                        $('#senddescription').html('<table><tr><td><img src="images/yellow_mail_send.png" alt="send emails" width="32px"/></td><td valign="middle">Click to send newsletters and announcements to payers</td></tr></table>');
                        $("#senddescription").hide(500);
                    }
                    else {
                        $("#senddescription").show(500);
                    }

                });
            })
        });      
    </script>--%>
 
     <script type="text/javascript">
         function pageLoad() {
             $("#<%=datetimepicker.ClientID %>").datetimepicker({ format: 'm/d/Y H:i' });            
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

    <script type="text/javascript">
        $(function () {
            $(document).ready(function () {
                $("#segmentationdiv").hide();

                $("#segmentationshowhidediv").click(function () {
                    $("#segmentationdiv").slideToggle(500);
                });

                $("#sendemaildiv").hide();

                $("#sendemailshowhidediv").click(function () {
                    $("#sendemaildiv").slideToggle(500);
                });
            });
        });
    </script>

    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
                <div class="sectiondivider">     
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>                
    </div>

    <asp:Panel runat="server" ID="InstructionContainerPanel">                 
        <div class="instructioncontainer">
            <div class="instruction-big">Segment payers and send them email newsletter and announcements</div>
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
            <table style="width:100%">                
                <tr>
                    <td colspan="4" style="vertical-align:middle">
                        <div class="instruction" id="segmentationshowhidediv" style="cursor:pointer; color:#0094ff; margin-top:10px">Segment Payers By Purchased Item<span class="newfeature">NEW FEATURE</span></div>
                        <div id="segmentationdiv" style="background-color:#fafafa; border:solid 1px #dadada; padding-left:5px; padding-right:5px; margin-left:10px; margin-top:5px">
                            <table width="100%">
                                <tr>
                                    <td style="width:200px">
                                        <asp:DropDownList runat="server" ID="DDWhereModifier">
                                            <asp:ListItem Selected="True" Text="Who bought the item" Value="like"></asp:ListItem>
                                            <asp:ListItem Text="Who did not buy the item" Value="notlike"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="padding-left:3px; padding-right:5px; width:165px"><asp:TextBox runat="server" ID="txtItemBought"></asp:TextBox><span style="padding-left:5px">?</span></td>
                                    <td style="vertical-align:middle; max-width:150px"><asp:Button runat="server" CssClass="NormalButton" ID="btnSearch" Text="Search" OnClick="btnSearch_Click"/></td>
                                    <td style="text-align:right; width:300px; vertical-align:middle">
                                        <span style="padding-right:5px">Filter: </span>
                                        <asp:DropDownList runat="server" ID="DDContactViews" Width="200" 
                                            AutoPostBack="true" 
                                            onselectedindexchanged="DDContactViews_SelectedIndexChanged">
                                            <asp:ListItem Text="Payers by name" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Payers by total paid"></asp:ListItem>
                                            <asp:ListItem Text="Inactive payers"></asp:ListItem>
                                            <asp:ListItem Text="Active payers"></asp:ListItem>
                                        </asp:DropDownList><br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                    </td>
                </tr>
                <tr>
                                   <%-- <td><img src="images/yellow_mail_send.png" alt="send emails" width="32px"/></td>--%>
                                    <td valign="middle">
                                        <div class="instruction" id="sendemailshowhidediv" style="cursor:pointer; color:#0094ff; padding-top:10px">Send Emails to Segmented Payer List<span class="newfeature">NEW FEATURE</span></div>
                                        <div id="sendemaildiv" style="background-color:#fafafa; border:solid 1px #dadada; padding-left:5px; padding-right:5px; margin-left:10px; margin-top:5px">
                                            <table style="padding-left:5px;">
                                                <tr>
                                                    <td>What to send: </td>
                                                    <td style="width:250px">
                                                        <asp:DropDownList runat="server" ID="DDEmailFYIList" Width="200"></asp:DropDownList> 
                                                    </td>
                                                    <td style="width:300px">                        
                                                        <span style="padding-left:15px; padding-right:5px">When to send: </span><asp:TextBox ID="datetimepicker" runat="server"></asp:TextBox>
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
                                        
                                    </td>                    
                                    <td ></td>                                         
                                    <td style="text-align:right; width:300px; display:none">
                                        <fieldset>
                                            <%--<legend class="smalllegend">Payer Stats</legend>--%>   
                                            <table>
                                                <tr>
                                                    <td style="width:50px">
                                                        <div style="font-size:20px; text-align:center; color:rgb(224,128,32)"><asp:Label runat="server" ID="lblCustomerStatsInactivePercent"></asp:Label></div>
                                                    </td>
                                                    <td>
                                                        <div style="text-align:left; padding-left:5px; font-size:12px">PAYERS ACTIVE LAST 3 MONTHS</div>
                                                        <div style="text-align:left; padding-left:5px; font-size:12px"><asp:Label runat="server" ID="lblCustomerStats"></asp:Label></div>  
                                                    </td>
                                                </tr>
                                            </table>                                                                                 
                                        </fieldset>                                              
                                    </td>
                                </tr>
            </table>                                              
        </div>         
   </div>           
   <br />
   <div style="clear:both">
        <div class="tabsectiondivider">
             <asp:GridView ID="GridView1" CssClass="GridView" runat="server" 
                 EmptyDataRowStyle-Font-Size="14px" EmptyDataText="There are no payers that match this search criteria." 
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
