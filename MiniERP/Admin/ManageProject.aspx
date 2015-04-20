<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ManageProject.aspx.cs" Inherits="MiniERP.Admin.ManageProject" %>

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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Manage Project</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center">
                                    Project Name <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtprojectname" runat="server" CssClass="TextBox" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ForeColor="Red" ErrorMessage="Please Enter Project Name" SetFocusOnError="true"
                                        ControlToValidate="txtprojectname" ValidationGroup="Project" Display="None"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revName" runat="server" ForeColor="Red" ControlToValidate="txtprojectname"
                                        Display="None" ValidationGroup="Project" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    City
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtcity" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revCity" runat="server" ControlToValidate="txtcity"
                                        Display="None" ValidationGroup="Project" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    State
                                </td>
                                <td colspan="3" class="center">
                                    <asp:TextBox ID="txtstate" CssClass="TextBox" runat="server" MaxLength="30" 
                                        ontextchanged="txtstate_TextChanged" TabIndex="2"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revState" runat="server" ControlToValidate="txtstate"
                                        Display="None" ValidationGroup="Project" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                </td>
                               
                            </tr>
                            <tr>
                                 <td class="center">
                                    Address <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtadd" CssClass="mlttext" runat="server" MaxLength="250" 
                                        TextMode="MultiLine" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Please Enter Address"
                                        ControlToValidate="txtadd" ValidationGroup="Project" ForeColor="Red" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="revAddress" runat="server" ControlToValidate="txtadd"
                                        Display="None" ValidationGroup="Project" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>
                                </td>
                                <td class="center">
                                    Description
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtdes" runat="server" CssClass="mlttext" TextMode="MultiLine" 
                                        MaxLength="250" TabIndex="4"></asp:TextBox>
                                    <%--<asp:RegularExpressionValidator ID="revdescription" runat="server" ControlToValidate="txtdes"
                                        Display="None" ValidationGroup="Project" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>
                                </td>
                               
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_color action red"
                            OnClick="btnCancel_Click" TabIndex="8" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" 
                            CssClass="button_color action green" ValidationGroup="Project"
                            OnClick="btnSave_Click" TabIndex="7" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button_color action blue"
                           Visible="false"  OnClick="btnUpdate_Click" ValidationGroup="Project" 
                            TabIndex="6" />
                    </div>
                    <br /><br />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="Project" />
                
            <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="gvProject_PageIndexChanging"
                OnRowCommand="gvProject_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Project Name">
                        <ItemTemplate>
                            <asp:Label ID="lblPname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="City" HeaderText="City" />
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDes" runat="server" Text='<%#Eval("Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("ProjectId") %>'
                                CommandName="cmdEdit" ToolTip="Edit">
                                <asp:Image ID="imgEdit" runat="server" CssClass="button icon145 " /></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ProjectId") %>'
                                CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure You Want To Delete This Project?') "
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
