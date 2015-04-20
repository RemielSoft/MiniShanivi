<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewUserMaster.aspx.cs" Inherits="MiniERP.Admin.ViewUserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        View User Master</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center">
                                    User Name
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtName" CssClass="TextBox" runat="server"></asp:TextBox>
                                </td>
                                <td class="center">
                                    Employee Code
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtEmpCode" CssClass="TextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Go" CssClass="button_color  go" />
                    </div>
                </div>
                <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" OnRowCommand="gvUser_RowCommand1"
                        OnPageIndexChanging="gvUser_PageIndexChanging1" AllowPaging="true" CssClass="mGrid"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowDataBound="gvUser_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                     <asp:Label ID="lblname" runat="server" Text='<%#Eval("FullName") %>'></asp:Label>
                                    <%--<asp:LinkButton ID="lnkName" runat="server" Text='<%# Eval("FullName") %>' CommandArgument='<%# Eval("UserId") %>'
                                        CommandName="Details"></asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                            <%--<asp:BoundField DataField="DateOfBirth" HeaderText="Date Of Birth" />--%>
                            <asp:TemplateField HeaderText="Date Of Birth">
                                <ItemTemplate>
                                    <asp:Label ID="lblDOB" runat="server" Text='<%#Eval("DateOfBirth","{0:dd/MM/yyyy}") %>' />
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepName" runat="server" Text='<%#Eval("Department.Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("UserId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit">
                                        <asp:Image ID="imgEdit" runat="server" CssClass="button icon145" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("UserId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete this Group?') "
                                        ToolTip="Delete">
                                        <asp:Image ID="imgDelete" runat="server" CssClass="button icon186" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
