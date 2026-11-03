<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateTimePicker.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.DateTimePicker" %>
<%@ Register Src="ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>
<script language="javascript" type="text/javascript">
    function ShowCalendar()
    {    
        var CalendarDIV = document.getElementById("CalendarDIV");
        if (CalendarDIV.style.display == 'none')
            CalendarDIV.style.display = 'block';
        else
            CalendarDIV.style.display = 'none';
    }
    
    function ShowSendByValueDIV()
    {    
        var SendByValueDIV = document.getElementById("SendByValueDIV");
        if (SendByValueDIV.style.display == 'none')
            SendByValueDIV.style.display = 'block';
        else
            SendByValueDIV.style.display = 'none';
    }
</script>
<div>
    <table>
        <tr>
            <td valign="top">
                <div>
                    <span>
                        <asp:RadioButtonList CssClass="RadioButtonList" Font-Size="14px" ID="UrgencyOptions" runat="server" Width="450px" AutoPostBack="True" OnSelectedIndexChanged="UrgencyOptions_SelectedIndexChanged1">                       
                        <asp:ListItem Value="0" Selected="True">Activate PayPal Notification Handler now</asp:ListItem>                      
                        <asp:ListItem Value="1">Save PayPal Notification Handler as draft, do not activate</asp:ListItem>           
                        </asp:RadioButtonList>
                    </span>  
                    <span>
                    </span>    
                </div>
            </td>                
            <td valign="top">
                <div style="padding-top:20px">
                    <fieldset id="schedulelaterfieldset" style="width:300px" class="instructionfieldset" runat="server">
                        <legend class="instructionlegend" >Schedule for later</legend>
                        <div id="dateselectordiv" style="vertical-align:middle" class="labeltext">
                            <table>
                            <tr>
                                <td valign=middle><asp:Literal ID="DateLiteral" runat="server" Text="Date"></asp:Literal></td>
                                <td valign=middle><asp:TextBox ID="DatePicked" runat="server" Width="175px"></asp:TextBox></td>
                                <td valign=top><asp:Button CssClass="NormalButtonShort" ID="DatePicker" runat="server" Text="Select Date..." OnClick="DatePicker_Click" /></td>
                            </tr>                
                            </table>                                                
                        </div>
                        <div id="CalendarDIV" runat="server" style="display:none; clear:both; margin-left:30px">
                            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged">
                                <WeekendDayStyle BackColor="#E0E0E0" />
                                <TodayDayStyle BackColor="#FFC080" />
                            </asp:Calendar>
                        </div>
                        <div id="timeselectordiv" runat="server" class="labeltext" >
                            <asp:Literal ID="TimeLiteral" runat="server" Text="Time"></asp:Literal>
                            <asp:DropDownList CssClass="select-small" ID="Hour" runat="server" OnSelectedIndexChanged="Hour_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                            </asp:DropDownList>
                        <asp:DropDownList CssClass="select-small" ID="Minute" runat="server" OnSelectedIndexChanged="Minute_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                        </asp:DropDownList>            
                        <asp:DropDownList CssClass="select-small" ID="AMPM" runat="server" OnSelectedIndexChanged="AMPM_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                        </asp:DropDownList>
                    </div> 
                    </fieldset>
                                   
                    </div>     
            </td>
        </tr>
    </table>        
</div>                           
<div id="DateTimePickerModalDialogDIV" runat="server">
 <asp:Button ID="btnDummy" CssClass="AjaxDummyButton" runat="server" Text="" Width="0px" />
 <uc1:ModalDialog ID="ModalDialog1" runat="server" />
</div>     