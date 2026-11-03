<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailScribe2.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.EmailScribe2" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Src="ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc2" %>

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

    </script>

            
        <div style="padding-left:0px; padding-top:0px;vertical-align:middle">        
            <div style="float:right; padding-right:15px; padding-top:0px">       
                <div>                
                    <%--<asp:Button ID="LinkButton1" runat="server" CssClass="NormalButtonVariableSize" OnClick="LinkButton1_Click1" Text="Preview"></asp:Button>--%>
                </div>                                      
            </div >
            <div id="toolbardiv" style="background:#dadada" runat="server">                    
                 <div id="headernav">
                    <ul runat="server" id="ListItems">       		    
       		            <li runat="server" id="Item0" class="on" style="display:none" ><a id="Link0" onserverclick="HtmlAnchor_Click" runat=server><span>Template Editor</span></a><span class="navnub"></span></li>       		    
       		            <li runat="server" id="Item1" style="display:none" ><a id="A1" onserverclick="HtmlAnchor_Click" runat=server ><span>Text Editor</span></a><span class="navnub"></span></li>
                        <li runat="server" id="DDHtmlThemesItem">
                            <span style="font-weight:bold; font-size:14px; padding-top:50px;padding-left:10px">Change theme: </span>
                            <asp:DropDownList ID="DDHtmlThemes" CssClass="select-small" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDHtmlThemes_SelectedIndexChanged"></asp:DropDownList>                            
                        </li>
       	            </ul>
                    <div style="float:right; padding-right:15px; padding-top:0px">
                        <asp:Button ID="LinkButton1" runat="server" CssClass="EditorPreviewModeLink" OnClick="LinkButton1_Click1" Text="Preview"></asp:Button>
                    </div>        	                
                </div>               
            </div>               
        </div>  
        <div id="previewdiv" style="clear:both;display:none;padding:0px 10px 0px 3px;width:850px;background-color:#ffffff;border:solid 0px #A5C3FF" runat="server">            
                <asp:Panel id="Panel1" runat="server">                    
                   <%-- <div id="themeselectordiv" runat="server" style="float:right;color:#ffffff;">                                             
                    </div>--%>
                   
                    <div class="tabsectiondivider">
                       <%-- <div class="instructioncontainer">
                            <div class="instruction">
                                <ul class="instruction-list">
                                    <li>Select a theme from the "Email Theme" drop down.</li>
                                    <li>To go back to the editor, clcik "Back to Editor"" button.</li>
                                </ul>
                            </div>
                        </div>--%>
                        <br />
                        <div style="clear:both" id="previewareadiv" runat="server">                        
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <%--<asp:GridView BackColor=white  Width="100%" BorderStyle="None" ID="PreviewGridView" runat="server" OnDataBinding="PreviewGridView_DataBinding" OnPageIndexChanged="PreviewGridView_PageIndexChanged" OnPageIndexChanging="PreviewGridView_PageIndexChanging" OnRowDataBound="PreviewGridView_RowDataBound" PageSize="1" AllowPaging="True" CssClass="GridViewBlue">--%>
                                    <asp:GridView Width="100%" ID="PreviewGridView" runat="server" OnDataBinding="PreviewGridView_DataBinding" OnPageIndexChanged="PreviewGridView_PageIndexChanged" OnPageIndexChanging="PreviewGridView_PageIndexChanging" OnRowDataBound="PreviewGridView_RowDataBound" PageSize="1" AllowPaging="True" CssClass="GridViewPreview">
                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom"  />
                                        <%--<PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle BackColor="#3C7FAF" ForeColor="White" Font-Bold="True" Font-Names="Arial" />--%>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>                    
                        </div>
                    </div>
                </asp:Panel>
           
         </div> 
         
        <!--<div id='editortype' runat="server" style="padding:1px 2px 2px 2px;WIDTH: 700px;display:block;background-color:#ffffff;color:#FF9966;font-weight:bold;margin-left:5px;">EDITOR TYPE</div>     -->
        <div id='editordiv' runat="server" style="clear:both;padding:10px 2px 2px 2px;WIDTH: 850px; background-color:#ffffff; border:solid 0px #A5C3FF">               
       

        <div class="tabsectiondivider"> 
              <div id='senderidentitydiv' style="clear:both;padding-top:10px" >            
                <fieldset class="editorsectionfieldset" id='1'>
                    <legend class="editorsectionlegend">"From" Identity</legend> 
                    <div class="instructioncontainer">
                        <div class="instruction">How do you want receipients to identify your email? <br /><br />Set the Inbox Display Name and the From Email Address.</div>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFromName" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 180px; padding-top:7px;color:#888" valign="top">Email Address *</td>
                                <td style="width: 280px; padding-top:0px"> 
                                    <asp:TextBox BorderWidth="1px" BorderColor="#cacaca" ID="txtFromEmailAddress" runat="server" Width="98%">news@acmecorp.com</asp:TextBox>&nbsp;                                    
                                </td>
                                 <td style="padding-bottom:12px;color:#5a5a5a;font-size:14px;text-align: left;font-weight:bold"><span style="padding-left:5px"><asp:Label runat="server" ID="lblEmailDomain"></asp:Label></span></td>
                                <td style="padding-bottom:12px; padding-left:5px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromEmailAddress" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>                                                                        
                    </div>  
                </fieldset>                                     
            </div>      
            <br />
                       
              <div id='subjectdiv' runat="server"  style="clear:both;padding-top:10px" >            
                <fieldset class="editorsectionfieldset">
                    <legend class="editorsectionlegend">Subject</legend>     
                    <div class="instructioncontainer">
                        <div class="instruction">Provide an appropriate subject line for your message.</div>
                    </div>            
                    <br />                
                    <div style="margin-left:15px;margin-top:5px;">
                        <asp:TextBox BorderWidth="1px" BorderColor="#cacaca" ID="txtSubject" runat="server" Width="98%">Enter the subject for the email</asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject" ErrorMessage="Subject is required"></asp:RequiredFieldValidator>
                    </div>  
                </fieldset>                                     
            </div>      
            <br />  

            <fieldset class="editorsectionfieldset">  
                <legend class="editorsectionlegend">Message</legend> 
                     
                <div style="margin-left:15px; margin-right:15px">                          
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="HtmlTemplateEditorView" runat="server" onactivate="HtmlTemplateEditorView_Activate">                            
                            <div id="headerdiv" class="editorcollapsiblesection" onclick="return ShowHideSection('header');">Email Heading (click to hide)</div>
                            <div id="headercontent" style="border-bottom:1px #solid #dadada;padding-top:5px;padding-left:5px;padding-right:5px; padding-bottom:5px;margin:0px 5px 5px 5px" >                                        
                                <div style="padding-left:1.5em;border-bottom:1px #solid #dadada;">
                                    <%--<asp:Label ID="HeadetTextLabel" runat="server" Text="" Width="100px"></asp:Label>--%>
                                    <asp:TextBox BorderWidth="1px" BorderColor="#cacaca" ID="HeaderText" runat="server" Text="Give your Email an Appropriate Heading (optional)" Width="98%"></asp:TextBox>
                                </div>                                        
                            </div> 
                            <br />             
                            <div id='bodydiv' class="editorcollapsiblesection" onclick="return ShowHideSection('body');">Body - required (click to hide)</div>
                            <div id="bodycontent" style="border-bottom:solid 0px #e5eff8;padding-left:1.5em;padding-right:1.5em; padding-bottom:2px;padding-top:0px;margin:0px 5px 5px 5px" >
                             <div class="instructioncontainer">
                                <div class="instruction">
                                    <ul class="instruction-list">
                                        <li>Type the content for the main body of the email message using the editor below.</li>
                                        <li>To personalize the message, do the following:
                                            <ul style="padding-left:40px">
                                                <li>Place the cursor where you want to insert personalized content.</li>
                                                <li>Select the personalization field from the "Insert Tokens" drop down in the toolbar.</li>                                        
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                             </div>
                                <div style="vertical-align:top">                                
                                    <div id="TemplateEditorFieldTokenDiv" runat=server>                 
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList id="FieldList" Width="200px" runat="server"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <input id="Button3" class=NormalButtonVariableSize runat="server" type="button" value="<< Insert Message Token" onclick="return insertAtCursor(1);"/>                                        
                                                </td>
                                                <td>
                                                    <asp:Label id="TokenValidationError" runat="server" Text="Invalid tokens found." Visible="False" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>                                                                           
                                </div>
                               <div class="validationerror">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="BodyText"
                                            ErrorMessage="The Body is required"  Text="<img src='images/delete.ico' style='padding-top:3px' /><span style='padding-left:5px'>Body is required</span>">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="freetextboxcontainer">                                            
                                    <FTB:FreeTextBox runat="Server" RemoveServerNameFromUrls="false" 
                                        BreakMode="LineBreak" BackColor="#e5eff8" Width="100%" ID="BodyText" 
                                        DesignModeCss="../App_Themes/DefaultTheme/FreeTextBox.css" 
                                        ButtonSet="Office2003" ToolbarBackColor="Transparent"/>                                                    
                                </div>                        
                            </div>
                            <br />
                            <div id='sidecolumndiv' class="editorcollapsiblesection" onclick="return ShowHideSection('sidecolumn');">Side Column - optional (click to hide)</div>
                            <div id="sidecolumncontent" style="border-bottom:solid 1px #e5eff8;padding-left:1.5em;padding-right:10px; padding-bottom:2px;padding-top:0px;margin:0px 5px 5px 5px" >
                                <div class="instructioncontainer">
                                <div class="instruction">
                                    <ul class="instruction-list">
                                        <li>Type the content for the side column of the email message using the editor below.</li>
                                        <li>To personalize the message, do the following:
                                            <ul style="padding-left:40px">
                                                <li>Place the cursor where you want to insert personalized content.</li>
                                                <li>Select the personalization field from the "Insert Tokens" drop down in the toolbar.</li>                                        
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                             </div>
                             <br />
                                <div class="freetextboxcontainer">
                                    <FTB:FreeTextBox runat="Server" RemoveServerNameFromUrls="false" 
                                        BreakMode="LineBreak" BackColor="#e5eff8" Width="100%" ID="SideColumnText" 
                                        DesignModeCss="../App_Themes/DefaultTheme/FreeTextBox.css" 
                                        ButtonSet="Office2003"/>                             
                                </div>                    
                            </div>                                                                                     
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
                <br />
            <div id='signaturediv' runat="server"  style="clear:both;" >
            <fieldset class="editorsectionfieldset">
                <legend class="editorsectionlegend">Signature</legend>                
                <%--<div style="margin-bottom:2px; margin-left:0px; margin-right:15px;">            
                    <div style="margin-left:15px;margin-top:5px">
                        <asp:TextBox Font-Names="Trebuchet MS, Lucida Grande, Arial, Helvetica, sans-serif" BorderWidth="1px" BorderColor="#cacaca" ID="txtSignature" TextMode=MultiLine runat="server" Width="99%" Height="120px" Wrap="False"></asp:TextBox>&nbsp;                
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSignature"
                            ErrorMessage="Signature is required"></asp:RequiredFieldValidator>
                    </div>            
                </div>--%>   
                <div class="instructioncontainer">
                    <div class="instruction">
                        <ul class="instruction-list">
                            <li>Type the content for the signature of the email message using the editor below.</li>                            
                        </ul>
                    </div>
                 </div>
                 <div class="validationerror">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSignature"
                            ErrorMessage="Signature is required"  Text="<img src='images/delete.ico' style='padding-top:3px' /><span style='padding-left:5px'>Signature is required</span>">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="freetextboxcontainer">                                            
                    <FTB:FreeTextBox runat="Server" RemoveServerNameFromUrls="false" 
                        BreakMode="LineBreak" BackColor="#e5eff8" Width="100%" ID="txtSignature" 
                        DesignModeCss="../App_Themes/DefaultTheme/FreeTextBox.css" 
                        ButtonSet="Office2003" ToolbarBackColor="Transparent"/>                                    
                </div>               
            </fieldset>
            </div>      
        </div>

        </div>          
        <div id="includeunsubscribetextdiv">
            <asp:CheckBox id="cbIncludeUnsubscribeText" runat="server" Text="Include unsubscribe text" Checked="True" Visible="False"></asp:CheckBox>
        </div>
        <uc2:ModalDialog ID="ModalDialog1" runat="server" />
            
   
    
    

   
                            
        
