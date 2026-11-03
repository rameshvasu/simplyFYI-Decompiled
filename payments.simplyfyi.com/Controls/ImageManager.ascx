<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageManager.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.ImageManager" %>

<%@ Register src="ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>

<script type="text/javascript">
    function UpdateCustomerImageScale(CLID, ImageFilename, DropDownID, ImageHyperLink) {
        //See http://www.codegateway.com/2012/03/get-dropdown-selected-item-onchange-in.html

        var e = document.getElementById(DropDownID);
        
        // Update the link with the new scale
        var imagelink = document.getElementById(ImageHyperLink);                
        var currentimagescale = imagelink.href.substr(imagelink.href.length - 1);        
        var imagescale = e.options[e.selectedIndex].value;        
        imagelink.href = imagelink.href.replace('scale=' + currentimagescale, 'scale=' + imagescale);          
              
        // Update the database
        //$.get("./ClientWebServices.aspx?cmd=2&clid=" + CLID + "&image=" + ImageFilename + "&scale=" + imagescale);
        $.get("/payments/ClientWebServices.aspx?cmd=2&clid=" + CLID + "&image=" + ImageFilename + "&scale=" + imagescale);
    } 
</script>
 
<table>
    <tr>
        <td style="width:40%; vertical-align:middle">
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="NormalButton" Width="251px" />            
        </td>
        <td style="vertical-align:top;padding-left:15px">                           
            <asp:Button ID="btnUpload" runat="server" Text="Upload image" CssClass="NormalButton" onclick="btnUpload_Click" />                                              
        </td>
        <td>
            <div style="color:#ff0000"><asp:Label ID="lblErrorMessage" runat="server"></asp:Label></div>
        </td>
    </tr>
</table>
                        
        
<asp:GridView ID="gvImages" runat="server" CssClass="GridView" 
    EmptyDataText="No images " EnableModelValidation="True" 
    onrowdatabound="gvImages_RowDataBound" 
    onrowdeleting="gvImages_RowDeleting">
    <Columns>
        <asp:HyperLinkField HeaderText="Image" />
        <asp:TemplateField HeaderText="Width x Height (pixels)"></asp:TemplateField>
        <asp:TemplateField HeaderText="Auto resize image">
            <ItemTemplate>
                <asp:DropDownList ID="DDImageScale" runat="server">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Delete Image" ShowHeader="False">
            <ItemTemplate>
               <%-- <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                    Text="Delete" CommandName="Delete" oncommand="LinkButton1_Command"></asp:LinkButton>--%>
                    &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" Height="8px"
                        ImageUrl="~/images/delete.png" OnCommand="ImageButton1_Command" Width="8px" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<br />
<asp:Image ID="srcImage" runat="server" Visible="false"/>






