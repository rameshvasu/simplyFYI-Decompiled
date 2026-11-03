<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayPalCartItemFYISelector.ascx.cs" Inherits="RameshInnovation.SimplyfyiExpress.Controls.PayPalCartItemFYISelector" %>

<%@ Register src="ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>


    <table style="border:solid 0px #dadada; width:650px">
        <tr>
            <td>
                <asp:GridView Width="100%" CssClass="GridView" ID="GridView1" runat="server" EnableModelValidation="True" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting1" EmptyDataText="No item-based FYIs found for the default FYI selected above. To add one, click &quot;Add New&quot;.">
                    <Columns>
                        <asp:TemplateField HeaderText="" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnCommand="LinkButton2_Command"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Selector ID">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text="lblSelectorID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text="lblRank"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FieldName">
                            <ItemTemplate>
                                <asp:DropDownList ID="DDFieldName" runat="server">
                                    <asp:ListItem Value="ITEMNAME">Item Name</asp:ListItem>
                                    <asp:ListItem Value="ITEMNUMBER">Item Number</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Condition">
                            <ItemTemplate>
                                <asp:DropDownList ID="DDFieldCondition" runat="server">
                                    <asp:ListItem Value="EQUALS">Equals</asp:ListItem>
                                    <asp:ListItem Value="CONTAINS">Contains</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FieldValue">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFieldValue" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SelectorFYI">
                            <ItemTemplate>
                                <asp:DropDownList ID="DDSelectorFYIs" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align:center; padding-right:10px">

                <asp:Button ID="btnAddSelector" runat="server" CssClass="NormalButton" OnClick="btnAddSelector_Click" Text="Add New" />
                <span style="padding-left:10px"><asp:Button CssClass="NormalButton" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></span>
                <uc1:ModalDialog ID="ModalDialog1" runat="server" />
            </td>
        </tr>
    </table>







<asp:HiddenField ID="HiddenField4DefaultFYIHandlerID" runat="server" />








