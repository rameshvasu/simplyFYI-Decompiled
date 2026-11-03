<%@ Page Language="C#" MasterPageFile="~/MainTemplateWide.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="SelectChannel.aspx.cs" Inherits="RameshInnovation.SimplyfyiExpress.SelectChannel" Title="Transactional emails made easy for PayPal merchants" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
<script language="javascript" type="text/javascript">
//    function showtabs(tabs) 
//    {
//        for (i = 0; i < tabs.length; i++) {
//            thisTab = document.getElementById(tabs[i]);
//            if (thisTab != null) {
//                thisTab.style.display = "block";
//            }
//        }
//    }

//    function hidetabs(tabs) 
//    {
//        for (i = 0; i < tabs.length; i++) {
//            thisTab = document.getElementById(tabs[i]);
//            if (thisTab != null) {
//                thisTab.style.display = "none";
//            }
//        }
//    }

    function settabfocus_old(tabid) 
    {
        var thisTab;
        
//        thisTab = document.getElementById("<%=tab1.ClientID %>");
//        if (thisTab != null) thisTab.removeAttribute("class");
//        thisTab = document.getElementById("<%=tab2.ClientID %>");
//        if (thisTab != null) thisTab.removeAttribute("class");
//        thisTab = document.getElementById("<%=tab3.ClientID %>");
//        if (thisTab != null) thisTab.removeAttribute("class");
//        thisTab = document.getElementById("<%=tab4.ClientID %>");
//        if (thisTab != null) thisTab.removeAttribute("class");        

        //thisTab = document.getElementById(tabid);
        //thisTab = document.getElementById("<%=tab1.ClientID %>");

        thisTab = document.getElementById(tabid);
        thisTab.removeAttribute("class");
        thisTab.setAttribute("class", "engagenowverticaltab-ul-li-a-selected");

//        if (tabid != 'tab1')
//            document.getElementById('tab1').setAttribute("class", "engagenowverticaltab-ul-li-a");        

//        if (tabid != 'tab2')
//            document.getElementById('tab2').setAttribute("class", "engagenowverticaltab-ul-li-a");        

//        if (tabid != 'tab3')
//            document.getElementById('tab3').setAttribute("class", "engagenowverticaltab-ul-li-a");        

//        if (tabid != 'tab4')
        //            document.getElementById('tab4').setAttribute("class", "engagenowverticaltab-ul-li-a");    


        if (tabid != "<%=tab1.ClientID %>")
            document.getElementById("<%=tab1.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");

        if (tabid != "<%=tab2.ClientID %>")
            document.getElementById("<%=tab2.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");

        if (tabid != "<%=tab3.ClientID %>")
            document.getElementById("<%=tab3.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");

        if (tabid != "<%=tab4.ClientID %>")
            document.getElementById("<%=tab4.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");

        if (tabid != "<%=tab5.ClientID %>")
            document.getElementById("<%=tab5.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");  
        
        if (tabid != "<%=tab6.ClientID %>")
            document.getElementById("<%=tab6.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");  

        $("#<%=SelectedChannelHiddenInput.ClientID %>").val(tabid);

    }

//    function settabfocus(tabid) {
//        var thisTab;        

//        thisTab = document.getElementById(tabid);
//        thisTab.removeAttribute("class");
//        thisTab.setAttribute("class", "engagenowverticaltab-ul-li-a-selected");

//        if (tabid != "<%=tab1.ClientID %>")
//            document.getElementById("<%=tab1.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");

//        if (tabid != "<%=tab2.ClientID %>")
//            document.getElementById("<%=tab2.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");

//        if (tabid != "<%=tab3.ClientID %>")
//            document.getElementById("<%=tab3.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");

//        if (tabid != "<%=tab4.ClientID %>")
//            document.getElementById("<%=tab4.ClientID %>").setAttribute("class", "engagenowverticaltab-ul-li-a");

//        $("#<%=SelectedChannelHiddenInput.ClientID %>").val(tabid);

//    }

    function domouseover(tabid) 
    {
        var tabclass = document.getElementById(tabid).getAttribute("class");        

        if (tabclass != "engagenowverticaltab-ul-li-a-selected") {
            document.getElementById(tabid).setAttribute("class", "engagenowverticaltab-ul-li-a-hover");
        }
    }

    function domouseout(tabid) 
    {
        var tabclass = document.getElementById(tabid).getAttribute("class");        

        if (tabclass != "engagenowverticaltab-ul-li-a-selected") {
            document.getElementById(tabid).setAttribute("class", "engagenowverticaltab-ul-li-a");
        }
    }

</script>


    <div id="maincontent">        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="sectiondivider">
            <div id="pagedescriptionbar"> 
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>            
            </div> 
           <%-- <div style="padding-left:5px"><span id="pagetitle">Select communication channel</span></div> --%>                 
        </div>

        <div class="instructioncontainer">
            <div class="instruction">
                <div class="instruction-big">Create a new FYI that sends Customized Payment Confirmation and other Engagement Emails</div>
               <%-- <ul class="instruction-list">                    
                    <li>Click a button below to choose the communication channel (Email, Text, Voice, Signup by SMS) you want to engage on with your customers.</li>
                    <li>Click "Create App".</li>
                </ul>--%>
            </div>
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%">
            <tr>
                <td style="width:200px" valign="top">	
                    <div id="engagenowverticaltabdiv" class="engagenowverticaltab" style="float:left;margin-left:2px; margin-right:15px; padding-top:0px;">
                        <ul class="engagenowverticaltab-ul">                                
                            <li class="engagenowverticaltab-ul-li"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab5' 
                                    OnClientClick="javascript:settabfocus('tab5');" 
                                    PostBackUrl="./SelectChannel.aspx?channel=5" onclick="tab5_Click">PayPal Confirmation</asp:LinkButton></li>                                                                      
                            <li class="engagenowverticaltab-ul-li" style="display:block"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab1'                                     
                                    PostBackUrl="./SelectChannel.aspx?channel=1" onclick="tab1_Click">Newsletters</asp:LinkButton></li>  
                            <li class="engagenowverticaltab-ul-li" style="display:block"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab6'                                     
                                    PostBackUrl="./SelectChannel.aspx?channel=6" onclick="tab6_Click">Shipment Confirmation</asp:LinkButton></li>                            
                            <li class="engagenowverticaltab-ul-li" style="display:none"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab2'                                     
                                    PostBackUrl="./SelectChannel.aspx?channel=2" onclick="tab2_Click">Engage By Text</asp:LinkButton></li>                            
                            <li class="engagenowverticaltab-ul-li" style="display:none"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab3' 
                                    OnClientClick="javascript:settabfocus('tab3');" 
                                    PostBackUrl="./SelectChannel.aspx?channel=3" onclick="tab3_Click">Engage By Voice</asp:LinkButton></li>                            
                            <li class="engagenowverticaltab-ul-li" style="display:none"><asp:LinkButton runat="server" 
                                    CssClass="engagenowverticaltab-ul-li-a" ID='tab4' 
                                    OnClientClick="javascript:settabfocus('tab4');" 
                                    PostBackUrl="./SelectChannel.aspx?channel=4" onclick="tab4_Click">Opt-In By Text</asp:LinkButton></li>                                                       

                        </ul>                               
                    </div>            	    		            
                </td>
                <td style="vertical-align:top; padding-top:3px; padding-left:10px">
                    <div style="border:1px solid #dadada; width:600px; padding: 10px 20px 20px 20px; box-shadow: -5px -5px 5px #dadada;">                        
                          
                        <%--See http://visuallightbox.com--%>
                        <asp:Panel runat="server" ID="pnlEndPointSubTypeSelection">
                            <div style="text-align:right; margin-top:0px; padding:10px;background:#eaeaea; border: 1px solid #888; -webkit-border-radius:4px;-khtml-border-radius:4px;-moz-border-radius:4px;border-radius:4px">
                                <div class="instruction">
                                    <span style="display:inline-block; padding-right:10px">
                                        <asp:Label runat="server" ID="lblSelectEndPointSubType" Text="Select PayPal payment method to engage on:"/>
                                    </span>
                                    <span style="display:inline-block">
                                        <asp:DropDownList runat="server" ID="DDEndPointSubType" Width="220px">                                        
                                        </asp:DropDownList>                                    
                                    </span>                            
                                </div>                                                         
                            </div>
                        </asp:Panel>  
                        <br /> 
                        <div id="SolutionsDiv" runat="server">                        
                        </div>                                           
                    </div>                    
                </td>
                <td valign="top">
                    <asp:Button ID="Button1" runat="server" Text="Create App >>" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        
        <br/>                        
                    
        <asp:HiddenField ID="SelectedChannelHiddenInput" runat="server" />
        
    </div>    
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="navigationcontrols" runat="server">
            
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>--%>
