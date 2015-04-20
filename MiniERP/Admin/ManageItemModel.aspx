<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ManageItemModel.aspx.cs" Inherits="MiniERP.Admin.ManageItemModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/ajax_loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="width:40px; height:40px; position:fixed; top:0; right:0; left:0; bottom:0; margin:auto" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Manage Item Model</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center">
                                    Model Name <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtName" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ErrorMessage="Enter Model Name"
                                        ControlToValidate="txtName" ValidationGroup="Model" Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                   <%-- <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName"
                                        Display="None" ValidationGroup="Model" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Description
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtdescription" CssClass="mlttext" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                            CssClass="button_color action red" onclick="btnCancel_Click"
                             />
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Model" Text="Save" 
                            CssClass="button_color action green" onclick="btnSave_Click"
                             />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="Model" 
                            CssClass="button_color action blue" Visible="false" onclick="btnUpdate_Click" 
                             />
                    </div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false"
                     ValidationGroup="Model" />
                </div>
            </div>
            <asp:GridView ID="gvItemModel" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                onrowcommand="gvItemModel_RowCommand" onpageindexchanging="gvItemModel_PageIndexChanging"  
                >
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("ItemModelId") %>'
                                CommandName="cmdEdit" ToolTip="Edit">
                                <asp:Image ID="imgEdit" runat="server" CssClass="button icon145 " /></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ItemModelId") %>'
                                CommandName="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete this Item?') "
                                ToolTip="Delete">
                                <asp:Image ID="imgDelete" runat="server" CssClass="button icon186" /></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
