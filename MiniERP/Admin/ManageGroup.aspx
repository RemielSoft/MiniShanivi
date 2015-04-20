<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ManageGroup.aspx.cs" Inherits="MiniERP.Admin.ManageGroup" %>

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
                        Manage Group</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td width="25%">
                                    Name
                                    <span style="color: Red">*</span>
                                </td>
                                <td class="center" width="75%">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="2" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ErrorMessage="Please Enter Group Name"
                                        ControlToValidate="txtName" SetFocusOnError="true" ValidationGroup="Group" Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName"
                                        Display="None"  ValidationGroup="Group" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Description
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtdiscription" runat="server" CssClass="mlttext"
                                        TextMode="MultiLine" MaxLength="250" TabIndex="3"></asp:TextBox>
                                    <%--<asp:RegularExpressionValidator ID="revdescription" runat="server" ControlToValidate="txtdiscription"
                                        Display="None" SetFocusOnError="true"  ValidationGroup="Group" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" CssClass="button_color action red"
                            runat="server" Text="Cancel" TabIndex="6"/>

                        <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" ValidationGroup="Group" 
                            Visible="false" CssClass="button_color action blue"
                            runat="server" Text="Update" TabIndex="5" />

                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" 
                            CssClass="button_color action green" ValidationGroup="Group"
                            Text="Save" TabIndex="4" />
                        
                    </div>
                    <br /><br />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="Group" runat="server" />
                
            <asp:GridView ID="gvGroup" runat="server" AutoGenerateColumns="False" OnRowCommand="gvGroup_RowCommand"
                OnPageIndexChanging="gvGroup_PageIndexChanging" AllowPaging="true" OnRowDataBound="gvGroup_RowDataBound"
                CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="20" AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="cmdEdit"
                                ToolTip="Edit">
                                <asp:Image ID="imgEdit" runat="server" CssClass="button icon145 " /></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("Id") %>'
                                CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure, You Want To Delete This Group ?') "
                                ToolTip="Delete">
                                <asp:Image ID="imgDelete" runat="server" CssClass="button icon186" /></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
