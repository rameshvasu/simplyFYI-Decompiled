<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Registration.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.Registration" %>
<%@ Register Src="ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>

<%@ Register src="PayPalHandlerSettings.ascx" tagname="PayPalHandlerSettings" tagprefix="uc2" %>
<%@ Register src="PayPalHandlerSettingsForMobile.ascx" tagname="PayPalHandlerSettingsForMobile" tagprefix="uc5" %>
<%@ Register src="PayPalHandlersContainer.ascx" tagname="PayPalHandlersContainer" tagprefix="uc6" %>
<%@ Register src="PayPalMerchantEmailForwarderSettings.ascx" tagname="PayPalMerchantEmailForwarderSettings" tagprefix="uc3" %>

<%@ Register src="GetTransactionsCapabilitySettings.ascx" tagname="GetTransactionsCapabilitySettings" tagprefix="uc7" %>

<%@ Register src="ImageManager.ascx" tagname="ImageManager" tagprefix="uc4" %>
<%@ Register src="PayPalHandlerSettingsWithDynamicSelector.ascx" tagname="PayPalHandlerSettingsWithDynamicSelector" tagprefix="uc7" %>

    <%--<script src="./payments/Scripts/lightbox/jquery-1.10.2.min.js" type="text/javascript"></script>--%>
    <script src="./Scripts/lightbox/lightbox-2.6.min.js" type="text/javascript"></script>
    <link href="./App_Themes/lightbox/lightbox.css" rel="stylesheet" />

<%--<div style="background-color:#F7F6F3; width:100%; padding-top:8px; padding-bottom:8px; border:solid 1px #adadad">
    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" OnMenuItemClick="Menu1_MenuItemClick" Width="100%" BackColor="#F7F6F3">
    <Items>
        <asp:MenuItem Text="Account Info" Value="0" ToolTip="Account Info" Selected="True"></asp:MenuItem>
        <asp:MenuItem Text="Options" Value="1" ToolTip="Options"></asp:MenuItem>
        <asp:MenuItem Text="Usage" Value="2" ToolTip="Usage"></asp:MenuItem>        
    </Items>        
    <StaticSelectedStyle ForeColor="White" BorderStyle="Solid" BorderWidth="1px" BorderColor="#adadad" HorizontalPadding="5px" BackColor="#ffffff" Font-Bold="False" VerticalPadding="5px"  />
    <StaticMenuItemStyle ItemSpacing="30px" BackColor="#F7F6F3" BorderColor="#F7F6F3" HorizontalPadding="10px" VerticalPadding="5px" Font-Bold="false" />
    <StaticMenuStyle HorizontalPadding="5px" VerticalPadding="0px" />
    <DynamicHoverStyle BackColor="#cacaca" ForeColor="White" />
    <DynamicMenuStyle BackColor="#F7F6F3" />
    <DynamicSelectedStyle BackColor="#5D7B9D" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticHoverStyle/>
    <LevelMenuItemStyles>
        <asp:MenuItemStyle  ItemSpacing="1px" BackColor="#F7F6F3" BorderColor="#F7F6F3" HorizontalPadding="5px" VerticalPadding="5px" Font-Bold="false" />
    </LevelMenuItemStyles> 
</asp:Menu>
</div>--%>

<%--See http://www.lessthanweb.com/blog/simple-css-tabs-with-jquery--%>
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

<script type="text/javascript">
    $(function () {
        $(document).ready(function () {
            // Invite user
            $('#invitecontent').show();

            <%--$("#inviteheading").click(function () {

                if ( $("#<%= btnInviteUsers.ClientID %>").is(":focus")) {
                    return;
                }

                $("#invitecontent").slideToggle(500);

                //alert($("#invitecontent").height());
                if ($("#invitecontent").height() == 1) {
                    $('#invitedescription').text('Click here to invite users');
                    $("#invitedescription").hide(500);
                }
                else {
                    $("#invitedescription").show(500);
                }

            });--%>

            // New Org
            $('#neworgcontent').hide();

            $("#neworgdescription").click(function () {

                $("#neworgcontent").slideToggle(500);

                //alert($("#invitecontent").height());
                if ($("#neworgcontent").height() == 1) {
                    $('#neworgdescription').text('Add new organization key');
                    $("#neworgdescription").hide(500);
                }
                else {
                    $("#neworgdescription").show(500);
                }

            });

            // New Contact List
            $('#newcontactlistcontent').hide();

            $("#newcontactlistdescription").click(function () {

                $("#newcontactlistcontent").slideToggle(500);

                //alert($("#invitecontent").height());
                if ($("#newcontactlistcontent").height() == 1) {
                    $('#newcontactlistdescription').text('Add new organization key');
                    $("#newcontactlistdescription").hide(500);
                }
                else {
                    $("#newcontactlistdescription").show(500);
                }

            });

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

            $("#tabs_team li").click(function () {
                //	First remove class "active" from currently active tab
                $("#tabs_team li").removeClass('active');

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

            //--------------------------------------------------------------------------
            // On load, show only #tab0
            //--------------------------------------------------------------------------            
            $('#tab1').hide();
            $('#tab2').hide();
            $('#tab3').hide();
            $('#tab4').hide();

            $('#tab1_team').hide();

            //alert(document.URL);

            if (document.URL.indexOf("event=") > 0)
            {
                $('#tab0').hide();
                $('#tab5').show();

                $("#tabs li").removeClass('active');

                //	Now add class "active" to tab5
                $("#tabitem5").addClass("active");

                //	Here we get the href value of the selected tab (tab5)
                var selected_tab = $("#tabitem5").find("a").attr("href");
                                
                //	Show the selected tab content
                $(selected_tab5).fadeIn();                

                //	At the end, we add return false so that the click on the link is not executed
                return false;
            }
        })
    });      
</script>

<script type="text/javascript">
/* <![CDATA[ */
$(document).ready(function(){
	$("#tabs li").click(function() {
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
	});

	$("#tabs_team li").click(function () {
	    //	First remove class "active" from currently active tab
	    $("#tabs_team li").removeClass('active');

	    //	Now add class "active" to the selected/clicked tab
	    $(this).addClass("active");

	    //	Hide all tab content
	    $(".tab_content_team").hide();

	    //	Here we get the href value of the selected tab
	    var selected_tab = $(this).find("a").attr("href");

	    //	Show the selected tab content
	    $(selected_tab).fadeIn();

	    //	At the end, we add return false so that the click on the link is not executed
	    return false;
	});
});
/* ]]> */
</script>

<script type="text/javascript">
    function disableRegisterButton()
    {       
        var registerButton = $("#<%=btnRegister.ClientID %>");
        registerButton.val("...Please wait");
        registerButton.attr('disabled', 'disabled');
    }
</script>

<div id="headernav">
    <ul runat=server id="ListItems">       		    
        <li runat=server id="Item0" class="on"><a id="Link0" onserverclick="HtmlAnchor_Click" runat=server><span>Profile</span></a><span class="navnub"></span></li>       		    
        <li runat=server id="Item1" ><a id="Link1" onserverclick="HtmlAnchor_Click" runat=server ><span>"From" Identity</span></a><span class="navnub"></span></li>       		    
        <li runat=server id="Item2" ><a id="Link2" onserverclick="HtmlAnchor_Click" runat=server ><span>Usage</span></a><span class="navnub"></span></li>
        <li runat=server id="Item3"><a id="Link3" onserverclick="HtmlAnchor_Click" runat=server ><span>Team</span></a><span class="navnub"></span></li>
        <li runat=server id="Item4" ><a id="Link4" onserverclick="HtmlAnchor_Click" runat=server ><span>PayPal Settings</span></a><span class="navnub"></span></li>        
        <li runat=server id="Item5" ><a id="Link5" onserverclick="HtmlAnchor_Click" runat=server ><span>Images</span></a><span class="navnub"></span></li>        
        <li runat=server id="Item6" ><a id="Link6" onserverclick="HtmlAnchor_Click" runat=server ><span>Add-Ons</span></a><span class="navnub"></span></li>        
    </ul> 
          	                
</div>   

<br /><br /><br />


<div style="clear:both">

        
<div class="tabsectiondivider">

<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

    <asp:View ID="AccountInfoView" runat="server">
    <table border="0">
<tr>
<td valign="top">
    <asp:Label ID="lblPageMessage" runat="server" Text="Label" Visible=false></asp:Label>
</td>
</tr>
</table>

<div style="clear:both; padding-top:15px">
    <fieldset style="min-width:500" class="editorsectionfieldset">
        <legend class="editorsectionlegend">About You</legend>
        <div style="margin-left:10px; font-size:14px; margin-top:25px; margin-bottom:10px">
            <table>    
                <tr>
                    <td style="width: 221px; padding-top:5px">
                        Full Name *</td>
                    <td style="width: 268px; padding-top:5px">
                        <asp:TextBox ToolTip="Enter full name" BorderWidth="1px" BorderColor="#cacaca" ID="txtFullName" runat="server" MaxLength="50" Width="266px"></asp:TextBox></td>
                    <td style="width: 160px; padding-top:5px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtFullName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 221px; padding-top:5px">
                        Username (Email address) * </td>
                    <td style="width: 268px; padding-top:5px">
                        <asp:TextBox ID="txtEmail" BorderWidth="1px" BorderColor="#cacaca" runat="server" MaxLength="50" ReadOnly="True" Width="266px"></asp:TextBox></td>
                    <td style="width: 160px; padding-top:5px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" ControlToValidate="txtEmail"></asp:RequiredFieldValidator></td>
                </tr>
            
            </table>
        </div>        
    </fieldset>
    <br/>
    <fieldset style="min-width:500" class="editorsectionfieldset">
    <legend class="editorsectionlegend">About Your Organization</legend>
    <div style=" margin-top:30px;">
        <div class="instructioncontainer">
            <div class="instruction">
                Please enter valid and accurate information.<br /><br />Information entered here will be used to reflect your organization's identity in emails.
            </div>
        </div>
    </div>    
   
     <div style="margin-left:10px; font-size:14px;margin-top:15px; margin-bottom:10px">         
     <table>
        <tr>
            <td style="width: 218px; padding-top:5px">
                Organization Name *</td>
            <td style="width: 264px; padding-top:5px">
                <asp:TextBox ID="txtBusinessName" BorderWidth="1px" BorderColor="#cacaca"  runat="server" MaxLength="50" Width="264px"></asp:TextBox></td>
            <td style="width: 100px; padding-top:5px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBusinessName"
                    ErrorMessage="Required"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 218px; padding-top:5px">
                Address1 *</td>
            <td style="width: 264px; padding-top:5px">
                <asp:TextBox ID="txtAddress1" BorderWidth="1px" BorderColor="#cacaca"  runat="server" MaxLength="50" Width="264px"></asp:TextBox></td>
            <td style="width: 100px; padding-top:5px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAddress1"
                    ErrorMessage="Required"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 218px; padding-top:5px">
                Address2</td>
            <td style="width: 264px; padding-top:5px">
                <asp:TextBox ID="txtAddress2" BorderWidth="1px" BorderColor="#cacaca"  runat="server" MaxLength="50" Width="264px"></asp:TextBox></td>
            <td style="width: 100px; padding-top:5px">
                </td>
        </tr>
        <tr>
            <td style="width: 218px; padding-top:5px">
                City *</td>
            <td style="width: 264px; padding-top:5px">
                <asp:TextBox ID="txtCity" BorderWidth="1px" BorderColor="#cacaca"  runat="server" MaxLength="50" Width="264px"></asp:TextBox></td>
            <td style="width: 100px; padding-top:5px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtZIP"
                    ErrorMessage="Required"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 218px; padding-top:5px">
                State *</td>
            <td style="width: 264px; padding-top:5px">
                <asp:TextBox ID="txtState" BorderWidth="1px" BorderColor="#cacaca"  runat="server" MaxLength="50" Width="264px"></asp:TextBox></td>
            <td style="width: 100px; padding-top:5px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtState"
                    ErrorMessage="Required"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 218px; padding-top:5px">
                ZIP/Postal Code *</td>
            <td style="width: 264px; padding-top:5px">
                <asp:TextBox ID="txtZIP" BorderWidth="1px" BorderColor="#cacaca"  runat="server" MaxLength="10" Width="264px"></asp:TextBox></td>
            <td style="width: 100px; padding-top:5px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtZIP"
                    ErrorMessage="Required"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 218px; padding-top:5px">
                Country *</td>
            <td style="width: 264px; padding-top:5px">                
                <asp:DropDownList ID="DDCountries" BorderWidth="1px" BorderColor="#cacaca"  runat="server" Width="270" AutoPostBack="True" OnSelectedIndexChanged="DDCountries_SelectedIndexChanged"/>
            </td>          
            <td></td>
        </tr>
        <tr>
                    <td style="width: 221px; padding-top:5px">Phone *</td>
                    <td style="width: 264px; padding-top:5px;">
                        <span>
                            <span style="padding-right:4px"><asp:TextBox runat="server" BorderWidth="1" BorderColor="#cacaca" ID="txtCountryCode" Width="38" Height="24" Text="+1" ReadOnly="True"></asp:TextBox></span>
                            <asp:TextBox ID="txtPhone" BorderWidth="1px" BorderColor="#cacaca"  runat="server" MaxLength="12" Width="210"  Height="24"></asp:TextBox>
                        </span>                        
                    </td>
                    <td style="width: 160px; padding-top:5px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone"
                            ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 221px; padding-top:5px">
                        Time Zone</td>
                    <td style="width: 264px; padding-top:5px">
                        <asp:DropDownList ID="DDTimeZone" runat="server">
                        </asp:DropDownList></td>
                    <td style="width: 160px; padding-top:5px">
                    </td>
                </tr>
        </table>
     </div>
    
        </fieldset>
</div>
   
    <div class="instruction" id="divTermsOfUse" runat="server">
        
        <asp:CheckBox ID="cbAgreeTermsOfUse" runat="server" Text="I have read and agree to the "  />
        <asp:HyperLink ID="TermsOfUseLink" runat="server" NavigateUrl="~/TermsOfUse.aspx" Target="_blank">terms of use.</asp:HyperLink>
        <br />
     </div>
     <%--see http://encosia.com/disable-a-button-control-during-postback/--%>
        <asp:Button ID="btnRegister" runat="server" OnClientClick="disableRegisterButton();" OnClick="btnRegister_Click" Text="Register" UseSubmitBehavior="false"  />
    
            
    </asp:View>
    

    <asp:View ID="AccountOptionsView" runat="server" OnActivate="AccountOptionsView_Activate">
    
        <div style="margin-left:-5px; font-size:14px">
            <table style="width:100%">    
                <%--<tr>
                    <td style="width: 221px">From Email Name</td>
                    <td style="width: 350px">
                        <asp:TextBox ID="txtFromEmailName" runat="server" MaxLength="50" Width="388px"></asp:TextBox></td>                
                </tr>--%>
                
                <tr>
                    <td>
                        <div style="padding-top:15px; display:none">
                            <fieldset style="min-width:500px" class="editorsectionfieldset">
                                <legend class="editorsectionlegend">General Settings</legend>                                      
                                 <div style="margin-left:10px; font-size:14px">
                                <table>
                                    <tr>
                                        <td style="width: 221px; padding-top:5px">Company Logo Url</td>
                                        <td style="width: 350px; padding-top:5px">
                                            <asp:TextBox ID="txtCompanyLogoUrl" BorderWidth="1px" BorderColor="#cacaca"  runat="server" MaxLength="255" Width="388px"></asp:TextBox>
                                        </td>    
                                    </tr>
                                </table> 
                                </div>
                            </fieldset>  
                        </div>
                                                                     
                    </td>                    
                </tr>
                                 
                <tr>
                <td>
                    <div style="padding-top:15px">
                        <fieldset class="editorsectionfieldset">
                        <legend class="editorsectionlegend">Email Sender Identity Settings</legend>
                        <div style="margin-top:30px">
                            <div class="instructioncontainer">
                                <div class="instruction">
                                    <ul class="instruction-list">
                                        <li>Sender Email Address - the "From" email ID displayed in the receiver's email inbox</li>
                                        <li>Sender Name - the name of your organization (20 characters max)</li>
                                        <li>Unsubscribe Text - Set the opt-out text. ##here## will be automatically replaced with an opt-out link.</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                                                
                         <div style="margin-left:10px; font-size:14px; margin-top:5px;margin-bottom:10px">
                        <table>
                            <tr>
                                <td style="width: 221px; padding-top:5px">
                                    Sender Email Address</td>
                                <td style="width: 350px; padding-top:5px">
                                    <asp:TextBox ID="txtEmailFromAddress" runat="server" BorderColor="#cacaca" 
                                        BorderWidth="1px" Enabled="False" MaxLength="255" Width="388px"></asp:TextBox>
                                </td>
                                <td style="padding-left:5px; padding-top:5px">
                                    Customize this? <a href="http://www.simplyfyi.com/Tutorials/TutorialUseCustomFromEmailAddress/CustomIdentity.aspx" 
                                        target="_blank">Learn more.</a></td>
                            </tr>
                            <tr>
                                <td style="width: 221px; padding-top:5px">
                                    Sender Name</td>
                                <td style="width: 350px; padding-top:5px">
                                    <asp:TextBox ID="txtEmailFromName" runat="server" BorderColor="#cacaca" 
                                        BorderWidth="1px" MaxLength="255" Width="388px"></asp:TextBox>
                                </td>
                                <td; style='padding-top:5px'>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="txtEmailFromName" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 221px; padding-top:5px" valign="top">
                                    Unsubscribe Text</td>
                                <td style="width: 350px; padding-top:5px">
                                    <asp:TextBox ID="txtFooterText" runat="server" BorderColor="#cacaca" 
                                        BorderWidth="1px" Height="111px" MaxLength="1024" TextMode="MultiLine" 
                                        Width="388px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <!--<td style="width: 221px;display:none" valign="top">-->
                                <td style="width: 221px; padding-top:5px; display:none" valign="top">
                                    Show Footer Message</td>
                                <!--<td style="width: 350px; display:none ">-->
                                <td style="width: 350px; padding-top:5px; display:none">
                                    <asp:CheckBox ID="cbShowFooter" runat="server" Checked="true" />
                                </td>
                            </tr>
                        </table>
                        </div>
                    </fieldset>
                    </div>
                   
                </td>
                </tr>
                <tr style="display:none">
                    <td>
                         <div style="padding-top:15px">
                            <fieldset class="editorsectionfieldset">
                            <legend class="editorsectionlegend">SMS Sender Identity Settings</legend>

                            <div class="instructioncontainer">
                                <div class="instruction">
                                    <ul class="instruction-list">                                        
                                        <li>The "From" phone number the receiver of text messages will see.</li> 
                                        <li>Contact us to setup your custom "From" phone number.</li>                                        
                                    </ul>
                                </div>
                            </div>
                            <br />
                             <div style="margin-left:10px; font-size:14px">
                            <table>
                            <tr>
                                <td style="width: 221px; padding-top:5px">Mobile Phone Number</td>
                                <td style="width: 350px; padding-top:5px"><asp:TextBox  BorderWidth="1px" BorderColor="#cacaca" ID="txtSMSFromAddress" runat="server" MaxLength="255" Width="388px" Enabled="False"></asp:TextBox></td>
                                <td style="padding-left:5px; padding-top:5px">Customize this? <a target=_blank href="http://www.simplyfyi.com/CustomIdentity.aspx">Learn more.</a></td>
                            </tr>
                            </table>   
                            </div>                                             
                        </fieldset>
                         </div>
                        
                    </td>
                </tr>
                <tr style="display:none">
                    <td>
                         <div style="padding-top:15px">
                            <fieldset class="editorsectionfieldset">
                            <legend class="editorsectionlegend">Phone Sender Identity Settings</legend>

                            <div class="instructioncontainer">
                                <div class="instruction">
                                    <ul class="instruction-list">                                        
                                        <li>The "From" phone number the receiver of voice messages will see.</li> 
                                        <li>Contact us to setup your custom "From" phone number.</li>                                        
                                    </ul>
                                </div>
                            </div>
                            <br />


                             <div style="margin-left:10px; font-size:14px">
                            <table>
                                <tr>
                                    <td style="width:221px; padding-top:5px">Phone Number</td>
                                    <td style="width:350px; padding-top:5px"><asp:TextBox  BorderWidth="1px" BorderColor="#cacaca" ID="txtPhoneFromAddress" runat="server" MaxLength="255" Width="388px" OnTextChanged="txtPhoneFromAddress_TextChanged" Enabled="False"></asp:TextBox></td>
                                    <td style="padding-left:5px; padding-top:5px">Customize this? <a target=_blank href="http://www.simplyfyi.com/CustomIdentity.aspx">Learn more.</a></td>
                                </tr>
                            </table>   
                            </div>                                             
                        </fieldset>
                         </div>
                        
                    </td>
                </tr>
                <tr style="display:none">
                    <td>
                        <div  style="padding-top:15px">
                            <fieldset class="editorsectionfieldset">
                                <legend class="editorsectionlegend">Organization Keyword (for opt-in by text)</legend>

                                <div class="instructioncontainer">
                                    <div class="instruction">
                                        <ul class="instruction-list">                                        
                                            <li>Create a Text Keyword for customers to opt-in using mobile</li>                                                                              
                                        </ul>
                                    </div>
                                </div>
                                <br />

                                 <div style="margin-left:10px; font-size:14px">
                                <table>
                                    <tr>
                                        <td style="width:221px; padding-top:5px">Organization Keys</td>
                                        <td style="width:350px; padding-top:5px"><asp:DropDownList CssClass="select-small"  BorderWidth="1px" BorderColor="#cacaca" ID="DDOrgKeys" runat="server" MaxLength="255" Width="388px" Enabled="True"></asp:DropDownList></td>                                                                              
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <br />
                                            <div id='neworgdescription' style="cursor:pointer">Create new organization key</div>
                                            <div id="neworgcontent">
                                                <h3>Enter new organization key</h3>
                                                <div><asp:TextBox ID="txtOrgKey" BorderWidth="1px" BorderColor="#cacaca" 
                                                        Font-Size="Large" Width="400px" runat="server"></asp:TextBox></div>
                                                <div><asp:Button CssClass="NormalButton" ID="btnAddNewOrgKey" runat="server" 
                                                        Text="Create Org Key" onclick="btnAddNewOrgKey_Click" /></div>            
                                            </div>                                          
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr style="display:none">
                    <td>
                        <div  style="padding-top:15px">
                            <fieldset class="editorsectionfieldset">
                                <legend class="editorsectionlegend">Contact Lists (for opt-in by text)</legend>

                                <div class="instructioncontainer">
                                    <div class="instruction">
                                        <ul class="instruction-list">                                        
                                            <li>Create contact lists to add opt-in customers</li>                                                                              
                                        </ul>
                                    </div>
                                </div>
                                <br />
                                 <div style="margin-left:10px; font-size:14px">
                                <table>
                                    <tr>
                                        <td style="width:221px; padding-top:5px">Contact Lists</td>
                                        <td style="width:350px; padding-top:5px"><asp:DropDownList CssClass="select-small"  BorderWidth="1px" BorderColor="#cacaca" ID="DDContactList" runat="server" MaxLength="255" Width="388px" Enabled="True"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <br />
                                            <div id='newcontactlistdescription' style="cursor:pointer">Create new contact list</div>
                                            <div id="newcontactlistcontent">
                                                <h3>Enter name of new contact list</h3>
                                                <div><asp:TextBox ID="txtContactListName" BorderWidth="1px" BorderColor="#cacaca" 
                                                        Font-Size="Large" Width="400px" runat="server"></asp:TextBox></div>
                                                <div><asp:Button CssClass="NormalButton" ID="Button1" runat="server" 
                                                        Text="Create List" onclick="Button1_Click" /></div>            
                                            </div>                                          
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </fieldset>
                        </div>
                    </td>
                </tr>
             </table> 
             <br />
             <asp:Button ID="SaveOptions" runat="server" OnClick="SaveOptions_Click" Text="Save Changes" />                         
        </div>               
        
        
    </asp:View>
    
    <asp:View ID="BillingView" runat="server" OnActivate="BillingView_Activate">
         <div style="padding-top:15px">
             <asp:GridView ID="BillingGridView" CssClass="GridView" runat="server" EmptyDataText="No transactions found." OnRowDataBound="BillingGridView_RowDataBound" PageSize="50">
             </asp:GridView> 
         </div>
              
    </asp:View>
    <asp:View ID="UsersView" runat="server" OnActivate="UsersView_Activate">
     <div id="ManageUsersDIV" runat="server" style="clear:both; padding-top:0px">
         <fieldset style="min-width:500; margin-top:15px" class="editorsectionfieldset">
             <legend class="editorsectionlegend">Manage Team Members<span class="newfeature">NEW FEATURE</span></legend> 
             <div style="margin-left:10px; font-size:14px; margin-top:30px"> 
                 <div id="tabs_wrapper_team">
	                <div id="tabs_container_team">
                        <ul id="tabs_team">
                            <li class="active"><a href="#tab0_team">Invite</a></li>
                            <li><a href="#tab1_team">Permission</a></li>                                                                             
                        </ul>                        
                    </div>
                    <div id="tabs_content_container_team">
			            <div id="tab0_team" class="tab_content" style="display: block;">                                                                                     
                                <div class="instructioncontainer">
                                    <div class="instruction">
                                        <div class="instruction-big">Invite Team Members</div>
                                        <ul class="instruction-list">                        
                                            <li>Enter the email addresses of additional team members you want to give access to your simplyFYI account.</li>
                                            <li>Click "Invite". Invitees will receive an email with a link to join your account. Once they complete the registration process, they will be added to your account and you will be notified.</li>                                            
                                        </ul>                                        
                                    </div>
                                </div>
                
                            <br />

                            <div id='invitedescription'>Enter email addresses (one per line) of people you would like to invite and add to your account.<br /></div>
                            <div id="invitecontent">                
                                <div><asp:TextBox ID="txtInviteesEmailAddresses" BorderWidth="1px" BorderColor="#cacaca" Font-Size="Medium" Width="400px" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                <div><asp:Button ID="btnInviteUsers" runat="server" Text="Invite" onclick="btnInviteUsers_Click" /></div>            
                            </div>
			            </div>
			            <div id="tab1_team" class="tab_content" style="display: block;">
                            <div style="margin-top:00px">
                                <asp:Panel ID="pnlInstructionPanel" runat="server">
                                    <div class="instructioncontainer">
                                        <div class="instruction">
                                            <div class="instruction-big">Set Team Member Permission</div>
                                            <ul class="instruction-list">                        
                                                <li>Check/uncheck "Allow" to allow/disallow users to access your simplyFYI account.</li>                            
                                            </ul>
                                        </div>
                                    </div>   
                                </asp:Panel>
                                 <div class="instruction"><asp:Label runat="server" ID="lblMessage"></asp:Label></div>
                                
                                                           
                                </div>
                                <br />
                                <div style="padding-top:10px; margin-left:10px">
                                    <asp:GridView ID="UsersGridView"  CssClass="GridView" runat="server" AutoGenerateColumns="False" OnRowDataBound="UsersGridView_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Allow">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br/>
                                </div>                                
			                </div> 
                    </div>
                </div>
            </div>
             <br />
         </fieldset>                  
    </div>

    <%--<div style="clear:both;">
        <fieldset id="inviteheading" style="min-width:500; margin-top:15px; cursor:pointer; z-index:-100;">
            <legend class="editorsectionlegend">Invite Team Members</legend> 
             
        </fieldset>    
     </div>--%>
         
    
    </asp:View>

    <asp:View ID="IPNView" runat="server" onactivate="IPNView_Activate" >
        <div style="clear:both;">
        <fieldset id="Fieldset1" style="min-width:500px; margin-top:15px" class="editorsectionfieldset">
            <legend class="editorsectionlegend">Manage PayPal Settings</legend>             
            <div style="margin-left:10px; font-size:14px; margin-top:30px">            
                <div id="tabs_wrapper">
	                <div id="tabs_container">
                        <ul id="tabs">
                            <li id="tabitem0" class="active"><a href="#tab0">PayPal Connect</a></li>
                            <li id="tabitem5"><a href="#tab5">Payment Actions</a></li>
                            <li id="tabitem1" style="display:none"><a href="#tab1">Buy Now / Pay Now / Donate</a></li>
                            <li id="tabitem2" style="display:none"><a href="#tab2">Subscription</a></li>
                            <li id="tabitem3" style="display:none"><a href="#tab3">Shopping Cart</a></li>  
                            <li id="tabitem4" style="display:none"><a href="#tab4">Mobile</a></li>                                                     
                        </ul>                        
                    </div>
                    <div id="tabs_content_container">       
                        <div id="tab0" class="tab_content" style="display: block;">
                            <%--<asp:Panel runat="server" ID="pnlNewConnectInstructions">
                                <div class="instructioncontainer">
                                <div class="instruction-big">Connect PayPal to simplyFYI</div>                           
                                <br />
                                <div class="instruction">
                                    <ul class="instruction-list">   
                                    <li>simplyFYI needs to communicate with PayPal to detect when a payment has been made and send your custom confirmation emails.</li>                     
                                    <li>In this step, you will grant permission to simplyFYI to query PayPal on your behalf to look for transactions.</li>                                       
                                    <li>When you click "Continue" below, you will be taken to PayPal where you will login and grant simplyFYI the necessary permissions.</li>
                                    <li>This process is secure and completely managed by PayPal.</li>
                                    </ul> 
                                </div> 
                             </div>
                            </asp:Panel>--%>
                        <uc3:PayPalMerchantEmailForwarderSettings Visible="false" ID="PayPalMerchantEmailForwarderSettings1" runat="server" /> 
		  	<uc7:GetTransactionsCapabilitySettings ID="GetTransactionsCapabilitySettings1" runat="server" /> 
                                                  
                        <fieldset id="Fieldset2" style="display:none; min-width:500px; margin-top:15px" class="editorsectionfieldset">
                            <legend class="editorsectionlegend">PayPal Instant Payment Notification (IPN)</legend>
                            <div style="padding-top:30px">
                                <div class="instructioncontainer">
                                    <div class="instruction">NOTE: This is applicable only to simplyFYI customers that have previously enabled IPN for simplyFYI.</div>                           
                                    <br />
                                    <div class="instruction">
                                        <ul class="instruction-list" style="display:none">   
                                        <li>NOTE: We have released a new method called "PayPal Connect" to connect simplyFYI and PayPal (see above).</li>
                                        <li>This new method is an alternative to the IPN method to connect simplyFYI and PayPal.</li>
                                        <li>"PayPal Connect" allows simplyFYI to be used with online stores that accept PayPal (eg. Shopify, BigCommerce, Volusion, Wix, etc.).</li>
                                        <li><u>We recommend all new simplyFYI customers to use "PayPal Connect" and not the IPN method.</u></li>
                                        <li><u>Existing customers who have enabled IPN for simplyFYI and are processing live payments, need not do anything for now.</u></li>                                        
                                        </ul> 
                                    </div>  
                                    
                                    <div class="instruction-big">Your IPN URL:</div>
                                    <table width="100%" style="background:#fff; border:solid 1px #888; padding:5px">                                                                                  
                                        <tr>                                       
                                            <td style="width: 600px; padding-top:5px">                                                
                                                <asp:Label ID="lblIPNUrl" runat="server" />                                            
                                            </td>                    
                                        </tr> 
                                    </table> 
                                    <br /><a href="http://www.simplyfyi.com/Tutorials/TutorialConfigurePayPalIPN/TutorialConfigurePayPalIPN.aspx" target="_blank">Learn more about configuring PayPal IPN.</a>
                                 </div>
                                <br />
                            </div>
                              
                        </fieldset>
                                         
                        
                                                                                                                   
                        <br />                                                     
                        </div>  
                         <div id="tab5" class="tab_content">
                             <uc6:PayPalHandlersContainer ID="PayPalHandlersContainer1" runat="server" />                                                                                 
                         </div>                                          
                        <div id="tab1" class="tab_content" style="display: block;">
                            <div class="instructioncontainer">                               
                                <div class="instruction-big">Select the FYI to run when "Buy Now" or "Pay Now" or "Donate" button is clicked</div>
                                <br />
                                <div class="instruction">
                                    <ul class="instruction-list">                                                               
                                        <li>Select a Notification Handler FYI to handle the transaction.</li>                                     
                                        <li>Click "Test the Notification Handler" to simulate an email response.</li>
                                        <li>To begin/stop processing notifications, check/uncheck "Process notifications from PayPal and send confirmation emails ". </li>                                                                                                                 
                                    </ul>                                        
                                </div>
                            </div>
                            <br />
                            <uc7:PayPalHandlerSettingsWithDynamicSelector ID="PayPalHandlerSettings4WebAccept" runat="server" />                            
                        </div>                          
                        <div id="tab2" class="tab_content">
                            <div class="instructioncontainer">
                                <div class="instruction-big">Select the FYI to run when "Subscription" button is clicked</div>
                                <br />
                                <div class="instruction">
                                    <ul class="instruction-list">                                                               
                                       <li>Select a Notification Handler FYI to handle the transaction.</li>                                     
                                        <li>Click "Test the Notification Handler" to simulate an email response.</li>
                                        <li>To begin/stop processing notifications, check/uncheck "Process notifications from PayPal and send confirmation emails ". </li>                                                                                                                                                              
                                    </ul>                                        
                                </div>
                            </div>
                            <br />
                            <%--<uc2:PayPalHandlerSettings Visible="true" ID="PayPalHandlerSettings4Subscription" runat="server" />--%>
                            <uc7:PayPalHandlerSettingsWithDynamicSelector ID="PayPalHandlerSettings4Subscription" runat="server" /> 
                        </div>
                        <div id="tab3" class="tab_content"  style="display: block;">
                            <div class="instructioncontainer">
                                <div class="instruction-big">Select the FYI to run when you use a shopping cart and PayPal Checkout.</div>
                                <br />
                                <div class="instruction">
                                    <ul class="instruction-list">                                                                
                                        <li>Select a Notification Handler FYI to handle the transaction.</li>                                     
                                        <li>Click "Test the Notification Handler" to simulate an email response.</li>
                                        <li>To begin/stop processing notifications, check/uncheck "Process notifications from PayPal and send confirmation emails ". </li>   
                                    </ul>                                        
                                </div>
                            </div>
                            <br />
                            <uc2:PayPalHandlerSettings ID="PayPalHandlerSettings4ShoppingCart" runat="server" />
                        </div>
                        <div id="tab4" class="tab_content"  style="display: block;">
                            <div class="instructioncontainer">
                                <div class="instruction-big">Select the FYI to run for mobile payments using "PayPal Here"</div>
                                <br />
                                <div class="instruction">
                                    <ul class="instruction-list">                        
                                        <li>Select a Notification Handler FYI to handle the transaction.</li>                                     
                                        <li>Click "Test the Notification Handler" to simulate an email response.</li>
                                        <li>To begin/stop processing notifications, check/uncheck "Process notifications from PayPal and send confirmation emails ". </li>   
                                    </ul>                                        
                                </div>
                            </div>
                            <br />
                            <uc5:PayPalHandlerSettingsForMobile ID="PayPalHandlerSettings4PayPalHere" runat="server" />
                       
                        <div id="tab5" class="tab_content" style="display: block;">
                            <div class="instructioncontainer">                               
                                <div class="instruction-big">Select the FYI to run for Virtual Terminal transactions.</div>
                                <br />
                                <div class="instruction">
                                    <ul class="instruction-list">                                                               
                                        <li>Select a Notification Handler FYI to handle the transaction.</li>                                     
                                        <li>Click "Test the Notification Handler" to simulate an email response.</li>
                                        <li>To begin/stop processing notifications, check/uncheck "Process notifications from PayPal and send confirmation emails ". </li>                                                                                                                 
                                    </ul>                                        
                                </div>
                            </div>
                            <br />
                            <uc2:PayPalHandlerSettings ID="PayPalHandlerSettings4VirtualTerminal" runat="server" />                            
                        </div>  
                        <div id="tab6" class="tab_content" style="display: block;">
                            <div class="instructioncontainer">                               
                                <div class="instruction-big">Select the FYI to run for Invoice and Send Money transactions.</div>
                                <br />
                                <div class="instruction">
                                    <ul class="instruction-list">                                                               
                                        <li>Select a Notification Handler FYI to handle the transaction.</li>                                     
                                        <li>Click "Test the Notification Handler" to simulate an email response.</li>
                                        <li>To begin/stop processing notifications, check/uncheck "Process notifications from PayPal and send confirmation emails ". </li>                                                                                                                 
                                    </ul>                                        
                                </div>
                            </div>
                            <br />
                            <uc2:PayPalHandlerSettings ID="PayPalHandlerSettings4SendMoney" runat="server" />                            
                        </div>                        
                    </div> 
                    </div>
                </div> 
                </div>                           
                <br />                                                                                
        </fieldset>  
                        
    </div>            
    </asp:View>

    <asp:View ID="ImagesView" runat="server">
        <fieldset class="editorsectionfieldset" style="margin-top:15px">
            <legend class="editorsectionlegend">Manage images</legend>
            <div style="margin-top:30px">
                <div class="instructioncontainer">
                    <div class="instruction">
                        <ul class="instruction-list">
                            <li>To upload an image, click "Browse" and select an image from your computer. Then click "Upload image".</li>
                            <li>Select the desired dimension of the image using the "Auto resize image" options. Your image will be automatically resized to either a Horizontal Banner (width of 600px), or to a Small Thumbnail Image (80px wide) or a Large Thumbnail Image (130px wide) or leave it in its original size.</li>
                            <li>You can also delete an image by clicking the "Delete image" button.</li>
                        </ul>
                    </div>
                </div>
            </div>
                      
            <uc4:ImageManager ID="ImageManager1" runat="server" />
        </fieldset>
    </asp:View>
    
    <asp:View ID="AddOnsView" runat="server">
        <fieldset class="editorsectionfieldset" style="margin-top:15px; padding-bottom:10px">
            <legend class="editorsectionlegend">Manage Feature Add-Ons</legend>
            <div style="margin-top:30px">
                <div class="instructioncontainer">
                    <div class="instruction">
                        <ul class="instruction-list">
                            <li>Enable additional add-on features to your simplyFYI account.</li>                            
                        </ul>
                    </div>                    
                </div>
            </div>  
            <br />
            <fieldset style="padding-bottom:15px">
                <legend style="font-size:16px">Shipment Confirmation Emails</legend>
                <table>
                    <tr>
                         <td style="width: 600px; padding-top:5px">
                               <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 300px; padding-top:5px">Enable Shipment Confirmation Emails</td>
                                            <td style="width: 350px; padding-top:5px; text-align:left">
                                                <asp:CheckBox ID="cbShipmentConfirmation" runat="server" AutoPostBack="True" OnCheckedChanged="cbShipmentConfirmation_CheckedChanged"></asp:CheckBox>
                                            </td>    
                                        </tr>
                                    </table> 
                             </ContentTemplate>
                               </asp:UpdatePanel>     
                         </td>
                    </tr>
                </table>              
            </fieldset>
            <div>
                
            </div>                                          
        </fieldset>
        
    </asp:View>

    <%--<asp:View ID="ApiView" runat="server" OnActivate="ApiView_Activate">
    <fieldset>
        <legend>API</legend>
        <asp:GridView ID="ApiGridView" runat="server" SkinID="gvGridViewJournal" OnRowDataBound="ApiGridView_RowDataBound" OnRowCommand="ApiGridView_RowCommand">
            <Columns>
                <asp:ButtonField CommandName="Api" HeaderText="API" Text="API" />
            </Columns>
        </asp:GridView>
    </fieldset>
        
    </asp:View>--%>
</asp:MultiView>
</div>
          
    
</div>

 
 <%--<asp:Panel id="PopupPanel" runat="server" ScrollBars="Auto">
    <cc1:ModalPopupExtender 
                ID="ModalPopup"
                runat="server" 
                OkControlID="btnDlgOK"
                PopupControlID="pnlPopup"                                     
                DropShadow="false"             
                BackgroundCssClass="modalBackground"
                TargetControlID="btnDummy"
                DynamicControlID="lblInfo"             
                DynamicServiceMethod="DisplayMessage" 
                DynamicContextKey='font-weight:bold'                                
                BehaviorID="ModalPopup">                 
    </cc1:ModalPopupExtender>
         
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" style="display:none" >                                               
        <table style="overflow:scroll;" border="0" cellpadding="0" cellspacing="0" id="tblPopup">
            <tr>
                <td style="text-align: center; background-color:#3c7faf;">
                    <span style="font-weight:bold; color:White">Registration Error</span></td>
            </tr>
            <tr>
                <td style="text-align: center" valign="top">
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button CssClass="NormalButton" ID="btnDlgOK" runat="server" Text="OK" /></td>
            </tr>
        </table>       
    </asp:Panel>    
</asp:Panel>--%>
<br />
    <asp:Button ID="btnDummy" CssClass="AjaxDummyButton" runat="server" Text="" Width="0px" />&nbsp;
<uc1:ModalDialog id="ModalDialog1" runat="server">

</uc1:ModalDialog>
