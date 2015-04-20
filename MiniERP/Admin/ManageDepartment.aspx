<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="ManageDepartment.aspx.cs" Inherits="MiniERP.Admin.ManageDepartment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Manage Department</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="25%">
                                    Name <span style="color: Red">*</span>
                                </td>
                                <td class="center" width="75%">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Department Name"
                                        ValidationGroup="dept"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revName" runat="server" ValidationGroup="dept"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtName"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Description
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtdiscription" CssClass="mlttext" runat="server" TextMode="MultiLine"
                                        MaxLength="250" TabIndex="2"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="button_color action red"
                            OnClick="btnCancel_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" CssClass="button_color action blue"
                            ValidationGroup="dept" OnClick="btnUpdate_Click" TabIndex="6" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="dept" CssClass="button_color action green"
                            OnClick="btnSave_Click" TabIndex="4" />
                    </div>
                    <br />
                    <br />
                    <asp:ValidationSummary ID="valSumDepartment" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="dept" />
                    <asp:GridView ID="gvDepartment" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="gvDepartment_RowCommand" OnPageIndexChanging="gvDepartment_PageIndexChanging"
                        AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="20" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-Width="47%" />
                            <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="47%" />
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("DepartmentId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145 ">
                                        <asp:Image ID="imgEdit" runat="server" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("DepartmentId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure You Want To Delete This Department?') "
                                        ToolTip="Delete" CssClass="button icon186 ">
                                        <asp:Image ID="imgDelete" runat="server" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
