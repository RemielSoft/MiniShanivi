<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" CodeBehind="ManageStore.aspx.cs" Inherits="MiniERP.Admin.ManageStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="box span12">
        <div class="box-header well">
            <h2>Manage Store
            </h2>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Item" />
        <div class="box-content">
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                        <td class="center">
                            <asp:Label ID="lblName123" runat="server" Text="Store Name"></asp:Label>
                            <span style="color: Red">*</span>
                        </td>
                        <td class="center">
                            <asp:TextBox ID="txtname" CssClass="TextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStoreName" runat="server" ControlToValidate="txtname"
                                ValidationGroup="Item" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Store Name"
                                Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />

            <div class="Button_align">
                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="button_color action red" OnClick="btncancel_Click" />
                <asp:Button ID="btnupdate" runat="server" Text="Update" ValidationGroup="Item" Visible="false"
                    CssClass="button_color action blue" OnClick="btnupdate_Click" />
                <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="Item" CssClass="button_color action green"
                    OnClick="btnsave_Click" />
            </div>
            <br />
            <br />
            <asp:GridView ID="gvStore" runat="server" AllowPaging="true" AlternatingRowStyle-CssClass="alt"
                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" OnRowCommand="gvStore_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Store Name">
                        <ItemTemplate>
                            <asp:Label ID="lblStoreName" runat="server" Text='<%#Eval("StoreName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("StoreId") %>'
                                CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145">
                                <asp:Image ID="imgEdit" runat="server" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("StoreId") %>'
                                CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure, You Want To Delete This Store ?') "
                                ToolTip="Delete" CssClass="button icon186">
                                <asp:Image ID="imgDelete" runat="server" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
