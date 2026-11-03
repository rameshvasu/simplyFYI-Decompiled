<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayPalIPNEmailScribe.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.PayPalIPNEmailScribe" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<%@ Register Src="ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc2" %>

<script type="text/javascript">
    function dragStart(ev) {
        //alert('dragStart');
        var originalData = ev.dataTransfer.getData("Text");
        originalData = "<img src='" + originalData + "' alt='img0' />";

        $("#dragndropdata").html(originalData);
        //alert($("#dragndropdata").html());

        var HeaderText_designEditorArea = document.getElementById("ctl00_main_EmailScribe2_1_HeaderText_designEditor");        
        HeaderText_designEditorArea.addEventListener('dragover', handleDragOver, false);        
        ev.dataTransfer.setData("Text", originalData);

        //----------------------------------------------------------------------------
        // Add mouseup event listener to FTB editor
        //----------------------------------------------------------------------------             
        var iFrameDoc = HeaderText_designEditorArea.contentWindow.document;
        iFrameDoc.addEventListener('mouseup', handleMouseUp, false);
              
        return true;
    }

    function handleDragOver(e) {
        //alert('in handleDragOver()');
        e.preventDefault(); // Necessary. Allows us to drop.        
    }  

    function handleDrop(ev) {
        alert('in handleDrop()');
        ev.preventDefault();       
        var data = ev.dataTransfer.getData("Text");
        alert(data);
        e.stopPropagation();
        return false;
    }

    function handleMouseUp(e) {
        //alert('in handleMouseUp()');    
        var dragndropdata = $("#dragndropdata").html();
        dropImageAtCursor('ctl00_main_EmailScribe2_1_HeaderText', dragndropdata);
        $("#dragndropdata").html("")
        $("#ctl00_main_EmailScribe2_1_HeaderText").focus();        
    }
</script>
   
<script type="text/javascript">
    function WebForm_OnSubmit() {        
        if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
            $("#validation_dialog").dialog({
                title: "You forgot something!",
                modal: true,
                resizable: false,
                buttons: {  
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
            return false;
        }
        return true;
    }  
   </script> 

<script language="Javascript" type="text/javascript">               
    // FreeTextBox Javascript to insert Tokens
    function insertHtml(editor,htmlmode,text,value)
    {
        editor.focus();
        sel = editor.document.selection.createRange();
        sel.pasteHTML(text);
    }

    function ShowHideSection(section)
    {
        var HeaderDiv = document.getElementById('headerdiv');        
        var HeaderContent = document.getElementById('headercontent');
        var BodyDiv = document.getElementById('bodydiv');        
        var BodyContent = document.getElementById('bodycontent');
        var TextBodyDiv = document.getElementById('textbodydiv');  
        var TextBodyContent = document.getElementById('textbodycontent');                
        var SideColumnDiv = document.getElementById('sidecolumndiv');        
        var SideColumnContent = document.getElementById('sidecolumncontent');           
            
        if (section == 'header') {            
            if (HeaderContent.style.display == '') {
                HeaderContent.style.display = 'none';                
                $('#headerdiv').text('Email Heading (click to show)');
            }
            else {
                HeaderContent.style.display = '';
                $('#headerdiv').text('Email Heading (click to hide)');
            }           
        }
            
        if (section == 'body')
        {
            if (BodyContent.style.display == '')
            {
                BodyContent.style.display = 'none';
                $('#bodydiv').text('Body - required (click to show)');
            }
            else
            {
                BodyContent.style.display = '';
                $('#bodydiv').text('Body - required (click to hide)');
            }
        }                
            
        if (section == 'sidecolumn')
        {
            if (SideColumnContent.style.display == '')
            {
                SideColumnContent.style.display = 'none';                
                $('#sidecolumndiv').text('Side Column - optional (click to show)');
            }
            else
            {
                SideColumnContent.style.display = '';                
                $('#sidecolumndiv').text('Side Column - optional (click to hide)');
            }
        }
        
        if (section == 'textbody')
        {
            if (TextBodyContent.style.display == '')
            {
                TextBodyContent.style.display = 'none';                                                
                $('textbodydiv').text('Body - required (click to show)');
            }
            else
            {
                TextBodyContent.style.display = '';                                
                $('textbodydiv').text('Body - required (click to hide)');
            }
        }
    }

    function dropImageAtCursor(FTBEditorID, ImageHtml) {       
        var HTMLMessageArea = document.getElementById(FTBEditorID);
        HTMLMessageArea.mode = FTB_MODE_HTML;

        //IE support
        if (document.selection) {
            HTMLMessageArea.focus();
            var sel = document.selection.createRange();
            sel.text = ImageHtml;
        }
        //MOZILLA/NETSCAPE support
        else if (HTMLMessageArea.selectionStart || HTMLMessageArea.selectionStart == 0) {        
            var startPos = HTMLMessageArea.selectionStart;
            var endPos = HTMLMessageArea.selectionEnd;           
            //HTMLMessageArea.value = HTMLMessageArea.value.substring(0, startPos) + ImageHtml + HTMLMessageArea.value.substring(endPos, HTMLMessageArea.value.length);
            HTMLMessageArea.value = ImageHtml;
        }
        else {           
            HTMLMessageArea.value += ImageHtml;
        }

        var HeaderText_designEditor_iframe = document.getElementById('ctl00_main_EmailScribe2_1_HeaderText_designEditor');    
        HeaderText_designEditor_iframe.contentWindow.document.body.innerHTML = ImageHtml;             
    }

    function insertAtCursor(editortype)
    {
        var TokenValue;
        var HTMLMessageArea;
        if (editortype == 1) 
        {
            TokenValue = document.getElementById("<%=FieldList.ClientID%>");
            HTMLMessageArea = document.getElementById("<%=BodyText.ClientID%>");
        }
        
        if (editortype == 2) 
        {
            TokenValue = document.getElementById("<%=FieldListTextEditor.ClientID%>");
            HTMLMessageArea = document.getElementById("<%=TextEmailBody.ClientID%>");            
        }
                
        //var TokenValueMod = TokenValue.value;
        var TokenValueMod = TokenValue.options[TokenValue.selectedIndex].text;
        
        if (TokenValueMod == 'SUMMARY')
        {
            TokenValueMod = '[SUMMARY:BEGIN]\r\n\r\n[SUMMARY:END]';
        }
        
        //IE support
        if (document.selection) 
        {        
            HTMLMessageArea.focus();            
            var sel = document.selection.createRange();            
            sel.text = TokenValueMod;
        }
        //MOZILLA/NETSCAPE support
        else if (HTMLMessageArea.selectionStart || HTMLMessageArea.selectionStart == 0) 
        {
            var startPos = HTMLMessageArea.selectionStart;
            var endPos = HTMLMessageArea.selectionEnd;            
            HTMLMessageArea.value = HTMLMessageArea.value.substring(0, startPos)+ TokenValueMod + HTMLMessageArea.value.substring(endPos, HTMLMessageArea.value.length);            
        } 
        else 
        {            
            HTMLMessageArea.value += TokenValueMod;            
        }
    }
    
    function insertEmailPart(type, toolbar)
    {       
        var MessageArea;
        var EmailPart; 
     
        if (toolbar == 1)
            MessageArea = document.getElementById("<%=BodyText.ClientID%>");
        else
            MessageArea = document.getElementById("<%=SideColumnText.ClientID%>");          
        
        if (type == 'title')
            EmailPart = "[TITLE]Enter title here[/TITLE]";
        else if (type == 'subtitle')
            EmailPart = "[SUBTITLE]Enter sub-title here[/SUBTITLE]";
        else if (type == 'imagerow')
            EmailPart = "[IMAGEROW Image='http://yourimage.jpg' Url='http://yourimagelink.com' Height='xx' Width='xx']Description[/IMAGEROW]";            
        else if (type == 'imagepanel')
            EmailPart = "[IMAGEPANEL Image='http://yourimage.jpg' Url='http://yourimagelink.com' Height='xx' Width='xx']Description[/IMAGEPANEL]";            
        else if (type == 'image')
            EmailPart = "[IMAGE Image='http://yourimage.jpg' Url='http://yourimagelink.com' Height='xx' Width='xx'][/IMAGE]";            
        else if (type == 'datarow2c')
            EmailPart = "[DATAROW2C COL1='col1 data' COL2='col2 data'][/DATAROW2C]";
        else if (type == 'link1' || type == 'link2')
            EmailPart = "[LINK http://www.yourlink.com]Description[/LINK]";                       
        
        //IE support
        if (document.selection) 
        {        
            MessageArea.focus();
            var sel = document.selection.createRange();            
            sel.text = EmailPart;
        }
        //MOZILLA/NETSCAPE support
        else if (MessageArea.selectionStart || MessageArea.selectionStart == 0) 
        {
            var startPos = MessageArea.selectionStart;
            var endPos = MessageArea.selectionEnd;            
            MessageArea.value = MessageArea.value.substring(0, startPos)+ EmailPart + MessageArea.value.substring(endPos, MessageArea.value.length);
        } 
        else 
        {            
            MessageArea.value += EmailPart;
        }
    }

    function ReinitTabs() {
        alert('ReinitTabs');
        
        $('#tabs-1').show();
        $('#tabs-2').hide();
        $('#tabs-3').hide();
        $('#tabs-4').hide();
        $('#tabs-5').hide();
        $('#tabs-6').hide();
        $('#tabs-7').hide();
        $('#tabs-8').hide();               
        
        $("#<%=mycolor.ClientID %>").colorpicker();
    }

    </script>

<script type="text/javascript">

    function pageLoad() 
    {
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

            //--------------------------------------------------------------------------
            // Color picker
            //--------------------------------------------------------------------------
            $("#<%=mycolor.ClientID %>").colorpicker();

            //	At the end, we add return false so that the click on the link is not executed
            return false;
        });        
    }

    $(document).ready(function () {
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
        });


        //--------------------------------------------------------------------------
        // Color picker
        //--------------------------------------------------------------------------
        $("#<%=mycolor.ClientID %>").colorpicker();

        $("#<%=mycolor.ClientID %>").on("change.color", function (event, color) {
            //            alert($("#<%=mycolor.ClientID %>").colorpicker("val"));
        });

        //--------------------------------------------------------------------------
        // Toggle radio button in Social tab to link website or FB page to FB comments
        //--------------------------------------------------------------------------
        $("#<%=rbLinkToWebsite.ClientID %>").click(function () {
            $("#<%=rbLinkToFacebookPage.ClientID %>").attr('checked', false);
        });

        $("#<%=rbLinkToFacebookPage.ClientID %>").click(function () {
            $("#<%=rbLinkToWebsite.ClientID %>").attr('checked', false);
        });

        //--------------------------------------------------------------------------
        // Enable/disable radio buttons depending on the contents of the text box as it is typed in
        //--------------------------------------------------------------------------
        $("#<%=txtWebsiteUrl.ClientID %>").on('input', function () {
            if ($("#<%=txtWebsiteUrl.ClientID %>").val().length == 0) {
                //$("#<%=rbLinkToWebsite.ClientID %>").hide().parent().hide();                
                $("#<%=rbLinkToWebsite.ClientID %>").attr('disabled', true);
            }
            else {
                //$("#<%=rbLinkToWebsite.ClientID %>").show().parent().show();                
                $("#<%=rbLinkToWebsite.ClientID %>").attr('disabled', false);
            }
        })

        //--------------------------------------------------------------------------
        // Enable/disable radio buttons depending on the contents of he text box as it is typed in
        //--------------------------------------------------------------------------
        $("#<%=txtFacebookPageUrl.ClientID %>").on('input', function () {
            if ($("#<%=txtFacebookPageUrl.ClientID %>").val().length == 0) {
                //$("#<%=rbLinkToWebsite.ClientID %>").hide().parent().hide();                
                $("#<%=rbLinkToFacebookPage.ClientID %>").attr('disabled', true);
            }
            else {
                //$("#<%=rbLinkToWebsite.ClientID %>").show().parent().show();                
                $("#<%=rbLinkToFacebookPage.ClientID %>").attr('disabled', false);
            }
        })

        //--------------------------------------------------------------------------
        // Set the state of the LinkToWebsite radio button on page load
        //--------------------------------------------------------------------------
        if ($("#<%=txtWebsiteUrl.ClientID %>").val().length == 0) {
            //$("#<%=rbLinkToWebsite.ClientID %>").hide().parent().hide();                
            $("#<%=rbLinkToWebsite.ClientID %>").attr('disabled', true);
        }
        else {
            //$("#<%=rbLinkToWebsite.ClientID %>").show().parent().show();                
            $("#<%=rbLinkToWebsite.ClientID %>").attr('disabled', false);
        }

        //--------------------------------------------------------------------------
        // Set the state of the LinkToFacebookPage radio button on page load
        //--------------------------------------------------------------------------
        if ($("#<%=txtFacebookPageUrl.ClientID %>").val().length == 0) {
            //$("#<%=rbLinkToWebsite.ClientID %>").hide().parent().hide();                
            $("#<%=rbLinkToFacebookPage.ClientID %>").attr('disabled', true);
        }
        else {
            //$("#<%=rbLinkToWebsite.ClientID %>").show().parent().show();                
            $("#<%=rbLinkToWebsite.ClientID %>").attr('disabled', false);
        }

        //--------------------------------------------------------------------------
        // On load, show only #tabs-1
        //--------------------------------------------------------------------------
        $('#tabs-2').hide();
        $('#tabs-3').hide();
        $('#tabs-4').hide();
        $('#tabs-5').hide();
        $('#tabs-6').hide();
        $('#tabs-7').hide();
        $('#tabs-8').hide();        
      
        ////--------------------------------------------------------------------------
        //        // Validate Urls
        ////--------------------------------------------------------------------------
        //        $("#<%=txtWebsiteUrl.ClientID %>").keyup(function () {
        //            var $th = $(this);

        //            if (isValidUrl($th.val()) == -1) {
        //                alert("The web page address format is not valid!");
        //            }

        //        });

        //        $("#<%=txtFacebookPageUrl.ClientID %>").keyup(function () {
        //            var $th = $(this);

        //            if (isValidUrl($th.val()) == -1) {
        //                alert("The Facebook page address is not valid!");
        //            }

        //        });

        //--------------------------------------------------------------------------
        // Url validation function
        //--------------------------------------------------------------------------
        function isValidUrl(url) {

            var myVariable = url;
            if (/^(http|https|ftp):\/\/[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/i.test(myVariable)) {
                return 1;
            } else {
                return -1;
            }
        }       
    });

</script>

<script type="text/javascript">
    function disablePreviewButton() {
        var previewButton = $("#<%=LinkButton1.ClientID %>");
        previewButton.val("Please wait...");
        previewButton.attr('disabled', 'disabled');
        previewButton.attr('style', 'text-decoration:none');
    }
</script>

<input type="hidden" id="dragndropdata" />
            
        <div style="padding-left:0px; padding-top:0px;vertical-align:middle">        
            <div style="float:right; padding-right:15px; padding-top:0px">       
                <div>                
                    <%--<asp:Button ID="LinkButton1" runat="server" CssClass="NormalButtonVariableSize" OnClick="LinkButton1_Click1" Text="Preview"></asp:Button>--%>
                </div>                                      
            </div >
            <div id="toolbardiv" style="background:#dadada" runat="server">                    
                 <div id="headernav">
                    <%--Hide ListItems below. Not using it. Instead using rbEmailLayout to select layout--%>
                    <ul runat="server" id="ListItems" style="display:none">       		    
       		            <li runat="server" id="Item0" class="on" style="display:none" ><a id="Link0" onserverclick="HtmlAnchor_Click" runat=server><span>Template Editor</span></a><span class="navnub"></span></li>       		    
       		            <li runat="server" id="Item1" style="display:none" ><a id="A1" onserverclick="HtmlAnchor_Click" runat=server ><span>Text Editor</span></a><span class="navnub"></span></li>
                        <li runat="server" id="DDHtmlThemesItem">
                            <span style="font-weight:bold; font-size:14px; padding-top:50px;padding-left:10px">Select Layout: </span>
                            <asp:DropDownList ID="DDHtmlThemes" CssClass="select-small" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDHtmlThemes_SelectedIndexChanged"></asp:DropDownList>                            
                        </li>
       	            </ul>                    

                    <div style="float:left; padding-left:15px; padding-right:15px; padding-top:10px;">
                        <asp:Label ID="lblSelectPreviewTransaction" runat="server" CssClass="instruction">Select a transaction for preview</asp:Label>
                        <asp:DropDownList ID="DDSampleTransactions" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DDSampleTransactions_SelectedIndexChanged">                            
                        </asp:DropDownList>
                        <asp:Button ID="btnSendTestEmail" CssClass="NormalButtonVariableSize" 
                            runat="server" Text="Send Test Email" onclick="btnSendTestEmail_Click" />
                    </div>
                    <div style="float:right; padding-right:15px; padding-top:10px;">
                        <asp:Button ID="LinkButton1" runat="server" OnClientClick="disablePreviewButton();" CssClass="EditorPreviewModeLink" OnClick="LinkButton1_Click1" Text="Preview" UseSubmitBehavior="false"></asp:Button>
                    </div>        	                
                </div>               
            </div>               
        </div>  
         
        <div id="previewdiv" style="clear:both;display:none;padding:0px 10px 0px 3px;width:850px;background-color:#ffffff;border:solid 0px #A5C3FF" runat="server">            
                <asp:Panel id="Panel1" runat="server">                                      
                   
                    <div class="tabsectiondivider">
                       <br />
                        <div style="clear:both" id="previewareadiv" runat="server">                        
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>                                    
                                    <asp:GridView Width="100%" ID="PreviewGridView" runat="server" OnDataBinding="PreviewGridView_DataBinding" OnPageIndexChanged="PreviewGridView_PageIndexChanged" OnPageIndexChanging="PreviewGridView_PageIndexChanging" OnRowDataBound="PreviewGridView_RowDataBound" PageSize="1" AllowPaging="True" CssClass="GridViewPreview">
                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom"  />                                        
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>                    
                        </div>
                    </div>
                </asp:Panel>           
         </div>       

        <div id='editordiv' runat="server" style="clear:both;padding:10px 2px 2px 2px;WIDTH: 840px; background-color:#ffffff; border:solid 0px #A5C3FF">               
            <div class="tabsectiondivider" style="width:100%"> 
                <div id="tabs_wrapper" style="clear:both;">
                    <div runat="server" id="divErrorLabel" style="display:none;"/>
	                <div id="tabs_container">
                        <ul id="tabs">
                            <li class="active"><a href="#tabs-1">"From" Identity</a></li>
                            <li><a href="#tabs-2">Subject</a></li>
                            <li><a href="#tabs-3">Logo</a></li>
                            <li><a href="#tabs-4">Payment Confirmation</a></li>
                            <li><a href="#tabs-5">Promotional</a></li>
                            <li><a href="#tabs-6">Signature</a></li>
                            <li><a href="#tabs-7">Social Links</a></li>                            
                            <li><a href="#tabs-8">Layout and Theme</a></li>
                        </ul>
                    </div>
                    <div id="tabs_content_container">
                        <div id="tabs-1" class="tab_content" style="display: block;">
                            <div id='senderidentitydiv' style="clear:both;padding-top:10px" > 
                                <div class="instructioncontainer">
                                    <div class="instruction-big">The "From" email address and name</div><br />
                                    <div class="instruction">
                                        <ul class="instruction-list"> 
                                            <li>Set the Inbox Display Name and the From Email Address.</li>
                                        </ul>
                                    </div>
                                    
                                </div>            
                                <br />       
                                <div style="margin-left:15px;margin-top:5px;font-size:14px;font-family:Trebuchet MS">
                                    <table>
                                        <tr>
                                            <td style="width: 180px; padding-top:7px;color:#888" valign="top">Inbox Display Name *</td>
                                            <td style="width: 280px; padding-top:0px"> 
                                                <asp:TextBox BorderWidth="1px" BorderColor="#cacaca" ID="txtFromName" 
                                                    runat="server" Width="98%" MaxLength="20">Eg. Acme Corporation</asp:TextBox>                                    
                                            </td>
                                            <td style="padding-top:0px;color:#5a5a5a;font-size:14px; text-align: left;"></td>
                                                <td style="padding-bottom:12px; padding-left:5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFromName" ErrorMessage="Inbox Display Name is required"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 180px; padding-top:7px;color:#888" valign="top">Email Address *</td>
                                            <td style="width: 280px; padding-top:0px"> 
                                                <asp:TextBox BorderWidth="1px" BorderColor="#cacaca" ID="txtFromEmailAddress" runat="server" Width="98%">news@acmecorp.com</asp:TextBox>&nbsp;                                    
                                            </td>
                                                <td style="padding-bottom:12px;color:#5a5a5a;font-size:14px;text-align: left;font-weight:bold"><span style="padding-left:5px"><asp:Label runat="server" ID="lblEmailDomain"></asp:Label></span></td>
                                            <td style="padding-bottom:12px; padding-left:5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromEmailAddress" ErrorMessage="From email address is required"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>                                                                        
                                </div>                                                                                
                            </div> 
                        </div>            
                                            
                        <div id="tabs-2" class="tab_content" style="display: none;">
                            <div id='subjectdiv' runat="server"  style="clear:both;padding-top:10px" > 
                                <div class="instructioncontainer">
                                    <div class="instruction-big">Subject of the Email</div><br />
                                    <div class="instruction">
                                        <ul class="instruction-list">
                                            <li>Provide an appropriate subject line for your message.</li>
                                            <li>Examples:<br />Your payment confirmation from XYZ Cruises
                                            <br />Thank you for your order with XYZ Cruises
                                            </li>
                                        </ul>
                                    </div>
                                </div>            
                                <br />                
                                <div style="margin-left:15px;margin-top:5px;">                                    
                                    <div class="validationerror">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Subject is required." ControlToValidate="txtSubject" Text="<img src='images/delete.ico' style='padding-top:3px' /><span style='padding-left:5px'>Subject is required</span>"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                    <div class="freetextboxcontainer">                                            
                                        <FTB:FreeTextBox runat="Server" RemoveServerNameFromUrls="false" 
                                            BreakMode="LineBreak" BackColor="#e5eff8" Width="100%" Height="100px" ID="txtSubject" 
                                            DesignModeCss="../App_Themes/DefaultTheme/FreeTextBox.css" 
                                            ButtonSet="Office2003" ToolbarBackColor="Transparent"/>                                                    
                                    </div>                                     
                                </div>                                                                                                                   
                            </div>
                        </div>
                        <div id="tabs-3" class="tab_content" style="display: none;">                            
                            <div class="instructioncontainer">
                                <div class="instruction-big">Your banner logo</div>
                                <div class="instruction">
                                    <ul class="instruction-list"> 
                                        <li>Add your logo and establish your brand in the mind of the customer.</li>
                                        <li>A logo banner about 600 pixels in width is recommended.</li>
                                        <li>Add a spritely message about your brand</li>
                                    </ul>
                                </div>                                    
                            </div>           
                            <br />                                                 
                            <div runat="server" id="divImageToolbar4LogoTab" style="border:1px solid #dadada; padding:3px; background-color:#fafafa">
                                <asp:Panel ID="pnlImageToolbar4LogoTab" runat="server" Width="100%" ScrollBars="Horizontal">                                    
                                    <asp:Label runat="server" ID="lblImageToolbar4LogoTab" />
                                </asp:Panel>  
                            </div>
                                                                                                                 
                            <div class="freetextboxcontainer">                                            
                                <FTB:FreeTextBox runat="Server" RemoveServerNameFromUrls="false" 
                                    BreakMode="LineBreak" BackColor="#e5eff8" Width="100%" ID="HeaderText" 
                                    DesignModeCss="../App_Themes/DefaultTheme/FreeTextBox.css" 
                                    ButtonSet="Office2003" ToolbarBackColor="Transparent" SupportFolder="../aspnet_client/FreeTextBox/"/>                                                    
                            </div>                                                                                                                

                            <%--Hiding the HTML/Text Views--%>
                            <fieldset class="editorsectionfieldset" style="display:none">  
                                <legend class="editorsectionlegend">Confirmation Message</legend>                      
                                <div style="margin-left:15px; margin-right:15px">                          
                                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="HtmlTemplateEditorView" runat="server" onactivate="HtmlTemplateEditorView_Activate">                                                                                                                                                                                                                                                   
                                        </asp:View>                                   
                                        <asp:View ID="View1" runat="server" onactivate="View1_Activate">
                                                <div id='textbodydiv' class="editorcollapsiblesection" onclick="return ShowHideSection('textbody');">Body - required (click to hide)</div>
                                                <div id="textbodycontent" style="border:solid 0px #e5eff8;margin-left:0px;margin-right:5px; margin-bottom:2px; padding-left:1.5em" >

                                                <div class="instructioncontainer">
                                                <div class="instruction">
                                                    <ul class="instruction-list">
                                                        <li>Click "Auto Create Text" to create a text version of the Email message from the HTML version. All personalization fields will be brought over automatically.</li>
                                                        <li>To personalize the message further, do the following:
                                                            <ul style="padding-left:40px">
                                                                <li>Place the cursor where you want to insert personalized content.</li>
                                                                <li>Select the personalization field from the "Insert Tokens" drop down in the toolbar.</li>                                        
                                                            </ul>
                                                        </li>
                                                        <li>If you wish to send the message as text only (not the HTML version), check "Send email as TEXT only".</li>
                                                    </ul>
                                                </div>
                                             </div>

                                                <div style="background-color:transparent">
                                                    <div style="vertical-align:top">                        
                                                        <span style="vertical-align:middle;margin-left:5px;">
                                                            <asp:Button ID="btnCreateTextFromTemplate" CssClass="NormalButtonVariableSize" runat="server" OnClick="btnCreateTextFromTemplate_Click1" Text="Auto Create Text" />
                                                        </span>
                                                        <span style="padding-left:40px;">
                                                            <asp:CheckBox ID="cbTextEmailOnly" Font-Size="14px" Text="&nbsp;Send email as TEXT only" runat="server" AutoPostBack="True" />
                                                        </span>
                                                        <span  style="padding-left:40px">
                                                            <asp:CheckBox ID="cbTrackOpen" Text="&nbsp;Track when email is opened" runat="server" AutoPostBack="True" Visible="False" />
                                                        </span>
                                                        <div id="TextEditorFieldTokenDiv" runat="server" style="margin-left:5px">
                                                            <table>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:DropDownList id="FieldListTextEditor" Width="200px" runat="server"></asp:DropDownList>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <input id="Button1" class="NormalButtonVariableSize" runat="server" type="button" value="<< Insert Message Token" onclick="return insertAtCursor(2);" style="width: 195px;margin:1em 0;border:1px solid #1CCD1A;color:#3C7FAF;font-weight:bold;margin-top:2px" />                        
                                                                        <asp:Label id="TokenValidationErrorTextEditor" runat="server" Text="Invalid tokens found." ForeColor="Red"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>                                                        
                                                        </div>                        
                                                    </div>                       
                                                 </div>                     
                                                <div style="clear:both">
                                                    <asp:TextBox ID="TextEmailBody"  BorderWidth="1px" BorderColor="#cacaca" runat="server" TextMode="MultiLine" Width="98%" Height="300px" Text='Type in the text for the TEXT version of the email.' Columns="80"></asp:TextBox>
                                                </div>                    
                                                </div>
                                        </asp:View>
                                    </asp:MultiView> 
                                </div>                        
                            </fieldset>                        
                        </div>
                        <div id="tabs-4" class="tab_content" style="display: none;">                            
                            <div id="bodycontent" style="border-bottom:solid 0px #e5eff8;padding-left:1.5em;padding-right:1.5em; padding-bottom:2px;padding-top:0px;margin:0px 5px 5px 5px" >
                                <div class="instructioncontainer">
                                <div class="instruction-big">Payment Confirmation</div>
                                <div class="instruction">
                                    <ul class="instruction-list">
                                        <li>Type the content for payment or order confirmation using the editor below.</li>
                                        <li>Bring out your style and tell the customer that you care.</li>
                                        <li>You may use the personalized confirmation message templates provided. Just place the cursor in the editor and then pick a template from the list provided in the toolbar. </li>
                                        <li>To further customize the confirmation message, do the following:
                                            <ul style="padding-left:40px">
                                                <li>Place the cursor where you want to insert personalized content.</li>
                                                <li>Select the personalization field from the "Insert Tokens" drop down in the toolbar.</li>                                        
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                </div>
                                <div style="vertical-align:top">                                
                                    <div id="TemplateEditorFieldTokenDiv" runat="server">                 
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList id="FieldList" Width="200px" runat="server"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <input id="Button3" class="NormalButtonVariableSize" runat="server" type="button" value="<< Insert Message Token" onclick="return insertAtCursor(1);"/>                                        
                                                </td>
                                                <td>
                                                    <asp:Label id="TokenValidationError" runat="server" Text="Invalid tokens found." Visible="False" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>                                                                           
                                </div>
                                <div class="validationerror">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="BodyText" ErrorMessage="Confirmation message is required."
                                            Text="<img src='images/delete.ico' style='padding-top:3px' /><span style='padding-left:5px'>Message is required</span>">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div runat="server" id="divImageToolbar4MessageTab" style="border:1px solid #dadada; padding:3px; background-color:#fafafa">
                                    <asp:Panel ID="pnlImageToolbar4MessageTab" runat="server" Width="100%" ScrollBars="Horizontal">
                                        <asp:Label runat="server" ID="lblImageToolbar4MessageTab" />
                                    </asp:Panel>  
                                </div>
                                <div class="freetextboxcontainer">                                            
                                    <FTB:FreeTextBox runat="Server" RemoveServerNameFromUrls="false" 
                                        BreakMode="LineBreak" BackColor="#e5eff8" Width="100%" ID="BodyText" 
                                        DesignModeCss="../App_Themes/DefaultTheme/FreeTextBox.css" 
                                        ButtonSet="Office2003" ToolbarBackColor="Transparent"/>                                                    
                                </div>                        
                            </div>
                        </div>
                        <div id="tabs-5" class="tab_content" style="display: none;">                            
                            <div id="sidecolumncontent" style="border-bottom:solid 1px #e5eff8;padding-left:1.5em;padding-right:10px; padding-bottom:2px;padding-top:0px;margin:0px 5px 5px 5px" >
                                <div class="instructioncontainer">
                                <div class="instruction-big">Promotional</div>
                                <div class="instruction">
                                    <ul class="instruction-list">
                                        <li>Type in a message to promote or cross-sell other products.</li>
                                        <li>You may use the personalized promotional message templates provided. Just place the cursor in the editor and then pick a template from the list provided in the toolbar. </li>
                                        <li>To further customize the promotional message, do the following:
                                            <ul style="padding-left:40px">
                                                <li>Place the cursor where you want to insert personalized content.</li>
                                                <li>Select the personalization field from the "Insert Tokens" drop down in the toolbar.</li>                                        
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                </div>
                                <br />
                                <div runat="server" id="divImageToolbar4PromoTab" style="border:1px solid #dadada; padding:3px; background-color:#fafafa">
                                    <asp:Panel ID="pnlImageToolbar4PromoTab" runat="server" Width="100%" ScrollBars="Horizontal">
                                        <asp:Label runat="server" ID="lblImageToolbar4PromoTab" />
                                    </asp:Panel>  
                                </div>
                                <div class="freetextboxcontainer">
                                    <FTB:FreeTextBox runat="Server" RemoveServerNameFromUrls="false" 
                                        BreakMode="LineBreak" BackColor="#e5eff8" Width="100%" ID="SideColumnText" 
                                        DesignModeCss="../App_Themes/DefaultTheme/FreeTextBox.css" 
                                        ButtonSet="Office2003"/>                             
                                </div>                    
                            </div>  
                        </div>
                        <div id="tabs-6" class="tab_content" style="display: none;">
                            <div id='signaturediv' runat="server"  style="clear:both;" >
                                 <div class="instructioncontainer">
                                    <div class="instruction-big">Sign it your way</div>
                                    <div class="instruction">
                                        <ul class="instruction-list">
                                            <li>Enter the signature for the confirmation message.</li> 
                                            <li>Add your picture or your team's</li>                           
                                            <li>Bring out your style and tell the customer that you care.</li>
                                        </ul>
                                    </div>
                                     </div>
                                     <div class="validationerror">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSignature" ErrorMessage="Signature is required."
                                                  Text="<img src='images/delete.ico' style='padding-top:3px' /><span style='padding-left:5px'>Signature is required</span>">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div runat="server" id="divImageToolbar4SignatureTab" style="border:1px solid #dadada; padding:3px; background-color:#fafafa">
                                        <asp:Panel ID="pnlImageToolbar4SignatureTab" runat="server" Width="100%" ScrollBars="Horizontal">
                                            <asp:Label runat="server" ID="lblImageToolbar4SignatureTab" />
                                        </asp:Panel>  
                                    </div>
                                    <div class="freetextboxcontainer">                                            
                                        <FTB:FreeTextBox runat="server" RemoveServerNameFromUrls="false"
                                            BreakMode="LineBreak" BackColor="#e5eff8" Width="100%" Height="200px" ID="txtSignature" 
                                            DesignModeCss="../App_Themes/DefaultTheme/FreeTextBox.css" 
                                            ButtonSet="Office2003" ToolbarBackColor="Transparent"/>                                    
                                    </div>                                  
                            </div>      
                        
                        </div>

                        <div id="tabs-7" class="tab_content" style="display: none;">
                            <div id='socialdiv' runat="server"  style="clear:both;" >
                                <div class="instructioncontainer">
                                    <div class="instruction-big">Let you customers engage socially</div>
                                    <br />
                                    <div class="instruction">
                                        Let your customers/donors and promote your business through their Facebook friends network.<br /><br />
                                        You can insert a "Facebook" button in your payment confirmation email so customers/donors can add a comment about their purchase or donation. Their friends will be able to see the comment and a link to your company webpage or Facebook page.
                                        <br /><br />
                                        <ul class="instruction-list">
                                            <li>Enter the website and / or your company's Facebook page.</li>   
                                            <li>Check the one you want to link a customer's comment to.</li>                                             
                                        </ul>
                                    </div>
                                </div>                                
                                <br />
                                <div class="instruction">
                                <table>
                                    <tr>
                                        <td style="width: 180px; padding-top:7px;color:#888" valign="top">Business Website</td>
                                        <td style="width: 280px; padding-top:0px"> 
                                            <asp:TextBox BorderWidth="1px" BorderColor="#cacaca" ID="txtWebsiteUrl" 
                                                runat="server" Width="98%" MaxLength="100"></asp:TextBox>
                                                <br /><span style="padding-left:15px">Eg: http://www.simplyfyi.com</span>                                    
                                        </td>
                                        <td valign="top" style="padding-top:0px;padding-left:5px;color:#5a5a5a;font-size:14px; text-align: left;">
                                            <asp:RadioButton ID="rbLinkToWebsite" runat="server" Text="Link to Facebook comment" /></td>                                                
                                    </tr>
                                    <tr>                                        
                                        <td style="width: 180px; padding-top:15px;color:#888" valign="top">Business Facebook Page</td>
                                        <td style="width: 280px; padding-top:8px"> 
                                            <asp:TextBox BorderWidth="1px" BorderColor="#cacaca" ID="txtFacebookPageUrl" runat="server" Width="98%"></asp:TextBox>
                                            <br /><span style="padding-left:15px">Eg: http://www.facebook.com/simplyfyi</span>
                                        </td>
                                        <td valign="top" style="padding-top:8px;padding-left:5px;color:#5a5a5a;font-size:14px;text-align: left;">
                                            <asp:RadioButton ID="rbLinkToFacebookPage" runat="server" Text="Link to Facebook comment" /></td>                                            
                                    </tr>
                                </table>
                            </div>
                            </div>                            
                        </div>

                        <div id="tabs-8" class="tab_content" style="display: none;">
                            <div class="instructioncontainer">
                                <div class="instruction-big">Choose an Email Layout and Color Theme</div>
                                <br />
                                <div class="instruction">
                                    Select one of 3 standard layouts for the body of the email and a color theme.                                    
                                </div>
                            </div>
                            <div class="instruction">
                                <table>
                                    <tr>
                                        <td style="width:500px; border-bottom:1px solid #dadada; padding-top:10px;padding-bottom:10px"><span class="instruction-big">LAYOUT</span></td>
                                        <td style="border-bottom:1px solid #dadada"><span class="instruction-big">COLOR THEME</span></td>
                                    </tr>
                                    <tr> 
                                        <td valign="top">
                                            <asp:RadioButtonList ID="rbEmailLayout" runat="server">
                                                <asp:ListItem Selected="True" Text="1 COLUMN (NO PROMOTIONAL MESSAGE)" Value="1 COLUMN LAYOUT" />
                                                <asp:ListItem Text="2 COLUMN (PROMOTINAL MESSAGE ON LEFT)" Value="2 COLUMN LEFT LAYOUT" />
                                                <asp:ListItem Text="2 COLUMN (PROMOTIONAL MESSAGE ON RIGHT)" Value="2 COLUMN RIGHT LAYOUT" />
                                            </asp:RadioButtonList>
                                        </td> 
                                        <td valign="top">
                                            <input style="width:100px;" id="mycolor" class="colorPicker evo-cp0" runat="server" /> 
                                            <div style="font-size:9px">Click above for colors</div><asp:HiddenField ID="HiddenFieldForColorTheme"
                                                runat="server" />  
                                            <div style="padding-top: 10px">
                                                <asp:CheckBox runat="server" id="cbShowBodyBorder" Text="Show border around email body" Checked="true"/>
                                            </div>                                             
                                        </td>                                       
                                    </tr>                                   
                                </table>                                
                            </div>
                            
                        </div>
                     
                    </div>                             
                </div>  
            </div>                          
        <br />
        </div>         
        <div id="includeunsubscribetextdiv">
            <asp:CheckBox id="cbIncludeUnsubscribeText" runat="server" Text="Include unsubscribe text" Checked="True" Visible="False"></asp:CheckBox>
        </div>

        <uc2:ModalDialog ID="ModalDialog1" runat="server" />

        <%--See http://www.aspsnippets.com/demos/264/--%>
         <div id="validation_dialog" style="display: none">  
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummaryListItem" />  
        </div>
   
    
    

   
                            
        
