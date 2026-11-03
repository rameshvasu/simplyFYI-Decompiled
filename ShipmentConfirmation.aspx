<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" AutoEventWireup="true" CodeBehind="ShipmentConfirmation.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.ShipmentConfirmation" Title="Transactional emails made easy for PayPal merchants" %>

<%@ Register src="Controls/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="headEmbeds" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu" runat="server">
</asp:Content>--%>


<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">  
    
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  

<style type="text/css">

#tabs {
	list-style: none;
	padding: 5px 0 0px 0;
	margin: 0 0 0 10px;	
	
}
#tabs li {
	display: inline;
}
#tabs li a {
	border: 1px solid #ccc;	
	padding: 4px 6px 7px 6px;
	text-decoration: none;
	background-color: #eeeeee;
	border-bottom: none;
	outline: none;
	border-radius: 5px 5px 0 0;
	-moz-border-radius: 5px 5px 0 0;
	-webkit-border-top-left-radius: 5px;
	-webkit-border-top-right-radius: 5px;
}
#tabs li a:hover {
	background-color: #dddddd;	
	padding: 4px 6px 7px 6px;
}
#tabs li.active a {
	border-bottom: 1px solid #fff;
	background-color: #fff;	
	padding: 4px 6px 8px 6px;
	border-bottom: none;
}
#tabs li.active a:hover {
	background-color: #eeeeee;
	padding: 4px 6px 7px 6px;
	border-bottom: none;
}
#tabs_wrapper {
	width:inherit; 
}
#tabs_container {
	border-bottom: 1px solid #ccc;
}
#tabs_content_container {
	border: 1px solid #ccc;
	border-top: none;
	padding: 10px;
	width:inherit; 
}
.tab_content {
	display: none;
}

/*team member tabs*/
#tabs_team {
	list-style: none;
	padding: 5px 0 0px 0;
	margin: 0 0 0 10px;	
	
}
#tabs_team li {
	display: inline;
}
#tabs_team li a {
	border: 1px solid #ccc;	
	padding: 4px 6px 5px 6px;
	text-decoration: none;
	background-color: #eeeeee;
	border-bottom: none;
	outline: none;
	border-radius: 5px 5px 0 0;
	-moz-border-radius: 5px 5px 0 0;
	-webkit-border-top-left-radius: 5px;
	-webkit-border-top-right-radius: 5px;
}
#tabs_team li a:hover {
	background-color: #dddddd;	
	padding: 4px 6px 7px 6px;
}
#tabs_team li.active a {
	border-bottom: 1px solid #fff;
	background-color: #fff;	
	padding: 4px 6px 6px 6px;
	border-bottom: none; 
}
#tabs_team li.active a:hover {
	background-color: #eeeeee;
	padding: 4px 6px 7px 6px;
	border-bottom: none;
}
#tabs_wrapper_team {
	width:inherit; 
}
#tabs_container_team {
	border-bottom: 1px solid #ccc;
}
#tabs_content_container_team {
	border: 1px solid #ccc;
	border-top: none;
	padding: 10px;
	width:inherit; 
}
</style>
    
   

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
             $("#<%=datetimepicker.ClientID %>").datetimepicker(
                 {
                     format: 'm/d/Y H:i',
                     minDate: 0                     
                 }
             );
        }
    </script>

    <script type = "text/javascript">
        function displayWaitMessage()
        {
            $("#lblPleaseWait").html('<span><img src="./images/ajax-loader.gif"/><span style="padding-left:5px;height:25px;vertical-align:top">working on it</span></span>');
        }

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

                //--------------------------------------------------------------------------
                // On load, show only #tab0
                //--------------------------------------------------------------------------            
                $('#tab1').hide();

                $("#tabs li").click(function () {

                    //	First remove class "active" from currently active tab                
                    $("#tabs li").removeClass('active');

                    //	Now add class "active" to the selected/clicked tab
                    $(this).addClass("active");

                    //	Hide all tab content
                    $(".tab_content").hide();

                    //	Here we get the href value of the selected tab
                    var selected_tab = $(this).find("a").attr("href");

                    //	Show the selected tab content
                    $(selected_tab).fadeIn();

                    //	At the end, we add return false so that the click on the link is not executed
                    return false;
                })
            });

            //-----------------------------------------------------------------------------------------------
            // The following code is required to make sure that the jQuery functions in the ready() function
            // continue to work after a postback.
            // See https://stackoverflow.com/questions/30184643/after-post-back-my-jquery-code-not-working
            //-----------------------------------------------------------------------------------------------

            var parameter = Sys.WebForms.PageRequestManager.getInstance();

            parameter.add_endRequest(function () {
                //jquery code again for working after postback
                $('#tab1').hide();

                $("#tabs li").click(function () {

                    //	First remove class "active" from currently active tab                
                    $("#tabs li").removeClass('active');

                    //	Now add class "active" to the selected/clicked tab
                    $(this).addClass("active");

                    //	Hide all tab content
                    $(".tab_content").hide();

                    //	Here we get the href value of the selected tab
                    var selected_tab = $(this).find("a").attr("href");

                    //	Show the selected tab content
                    $(selected_tab).fadeIn();

                    //	At the end, we add return false so that the click on the link is not executed
                    return false;
                })
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
            <div class="instruction-big">Send shipment confirmation emails to buyers with tracking numbers and carriers</div>
        </div>
    </asp:Panel> 
                                       
    <br />
    <div id="tabs_wrapper">
             <div id="tabs_container">
                <ul id="tabs">
                    <li id="tabitem0" class="active"><a href="#tab0">Shipment Confirmations To Be Sent</a></li>
                    <li id="tabitem1"><a href="#tab1">Shipment Confirmations Sent</a></li>
                </ul>
             </div>
             <div id="tabs_content_container"> 
                <div id="tab0" class="tab_content" style="display: block;">

                    <div class="instructioncontainer">
                        <div class="instruction">
                            <ul class="instruction-list">
                                <li>Check the checkboxes for the transactions to send shipment confirmations for.<br /></li>
                                <li>Enter the Tracking numbers and select the Carriers for the selected transactions.</li>
                                <li>Click "FYI to send" and pick the FYI to use.</li>
                                <li>Click the "When to send" text box and select the date to schedule the shipment confirmation emails.</li>                                
                                <li>If you want to test the emails, check "Send as test". The shipment confirmation email will be sent to your email address but not the customer.</li>
                                <li>Finally, click "Send". The emails will be scheduled to be sent. </li>
                            </ul>                           
                        </div>                                          
                    </div>

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
                    <td valign="middle">
                        <%--<div class="instruction" id="sendemailshowhidediv" style="color:#0094ff; padding-top:10px">Send shipment confirmation emails to selected buyers<span class="newfeature">NEW FEATURE</span></div>--%>
                        <div id="sendemaildiv" style="background-color:#fafafa; border:solid 1px #dadada; padding-left:5px; padding-right:5px; margin-left:0px; margin-top:5px">
                            <table style="padding-left:5px;" width="100%">
                                <tr>
                                    <td>FYI to send: </td>
                                    <td style="width:275px">
                                        <asp:DropDownList runat="server" ID="DDEmailFYIList" Width="250"></asp:DropDownList> 
                                    </td>
                                    <td>When to send: </td>
                                    <td style="width:150px">                        
                                        <asp:TextBox ID="datetimepicker" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <div style="padding-left:5px"><asp:Button CssClass="NormalButton" 
                                            ID="btnSendEmails" runat="server" Text="Send" onclick="btnSendEmails_Click" /></div>  
                                    </td> 
                                    <td>
                                        <div style="padding-left:15px">
                                            <asp:CheckBox ID="cbTestMode" runat="server" Checked="true" Text=" Send as test" />
                                        </div>                                        
                                    </td>                                                                      
                                </tr>                                                                
                            </table>
                        </div>
                                        
                    </td>                                                        
                </tr>
            </table>                                              
        </div>         
   </div>           
   
   <div style="clear:both; margin-left:5px">
        <div class="tabsectiondivider">
            <table>
                <tr>
                    <td>Show transactions for last: </td>
                    <td>                                        
                        <asp:DropDownList ID="DDTimeRange" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDTimeRange_SelectedIndexChanged">
                        </asp:DropDownList>                                        
                    </td> 
                </tr>
            </table>
            <br />
             <asp:GridView ID="GridView1" CssClass="GridView" runat="server" 
                 EmptyDataRowStyle-Font-Size="14px" EmptyDataText="There are no transactions for this time period." 
                 OnRowDataBound="GridView1_RowDataBound">        
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
                     
                     <asp:BoundField DataField="Record" HeaderText="Record" SortExpression="Record" />
                     <asp:BoundField DataField="Transaction Date/Time" HeaderText="Txn Date" SortExpression="Transaction Date/Time" />
                     <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                     <asp:BoundField DataField="Gross Payment" HeaderText="Gross" SortExpression="Gross" />
                     <asp:BoundField DataField="Transaction ID" HeaderText="Txn ID" SortExpression="Transaction ID" />
                     <asp:TemplateField HeaderText="Tracking #">
                         <ItemTemplate>
                             <asp:TextBox ID="TextBox1" runat="server" MaxLength="20"></asp:TextBox>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Carrier">
                         <ItemTemplate>
                             <asp:DropDownList ID="DropDownList1" runat="server">
                             </asp:DropDownList>
                         </ItemTemplate>
                     </asp:TemplateField>
                     
                 </Columns>
                <EmptyDataRowStyle Font-Size="14px"></EmptyDataRowStyle>
             </asp:GridView>                    
             <br />
             <uc1:ModalDialog ID="ModalDialog1" runat="server" />
       </div>
   </div>  
   </div>
   
    <div id="tab1" class="tab_content" style="display: block;">
        <div class="tabsectiondivider">

             <asp:PlaceHolder ID="PlaceHolderForReport" runat="server"></asp:PlaceHolder>

             <%--<asp:GridView ID="GridView2" CssClass="GridView" runat="server" 
                 EmptyDataRowStyle-Font-Size="14px" EmptyDataText="No shipment confirmation emails were sent in this time period." OnRowDataBound="GridView2_RowDataBound" >        
                 
                 <Columns>
                     <asp:BoundField DataField="TransactionDateTime" HeaderText="Txn Date" />
                     <asp:BoundField DataField="PayerName" HeaderText="Name" />
                     <asp:BoundField DataField="OriginatorTransactionID" HeaderText="Txn ID" />
                     <asp:BoundField DataField="TrackingNumber" HeaderText="Tracking #" />
                     <asp:BoundField DataField="ShippingCarrier" HeaderText="Carrier" />
                     <asp:BoundField DataField="ShipmentDateTime" HeaderText="Shipment Email Sent" />
                 </Columns>
                 
                <EmptyDataRowStyle Font-Size="14px"></EmptyDataRowStyle>
             </asp:GridView>--%>                    
             <br />            
        </div>                      
    </div>
    </div>
 </div>

     
        </ContentTemplate>
    </asp:UpdatePanel>


    
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="navigationcontrols" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
