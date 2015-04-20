<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="MiniERP.Admin.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="box span12">
        <div class="box-header well">
            <h2>
                Change Password</h2>
        </div>
        <div class="box-content">
            <asp:ValidationSummary ID="Validationsummery1" ShowMessageBox="true" ShowSummary="false"
                runat="server" ValidationGroup="User" />
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                        <td class="center" width="25%">
                            Login Id<span style="color: Red">*</span>
                        </td>
                        <td class="center" width="25%">
                            <asp:TextBox ID="txtLoginId" runat="server" CssClass="TextBox" TabIndex="1" MaxLength="90">
                            </asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvUserloginId" runat="server" ControlToValidate="txtLoginId"
                                ValidationGroup="User" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Login-Id"
                                Display="None">
                            </asp:RequiredFieldValidator>
                            <%--   <asp:RegularExpressionValidator ID="revLoginId" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtlogin"></asp:RegularExpressionValidator>
                            --%>
                        </td>
                    </tr>
                    <tr>
                        <td class="center" width="25%">
                            Old Password<span style="color: Red">*</span>
                        </td>
                        <td class="center" width="25%">
                            <asp:TextBox ID="txtOldPassword" runat="server" AutoPostBack="True"   CssClass="TextBox"
                                TabIndex="1" MaxLength="90" ontextchanged="txtOldPassword_TextChanged">
                            </asp:TextBox>
                            <asp:LinkButton ID="lnkBtn" runat="server" ToolTip="Match Password" CssClass="Calender icon44">
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkBtn1" runat="server" ToolTip="Password Not Match" CssClass="Calender icon56">
                            </asp:LinkButton>
                            <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtOldPassword"
                                ValidationGroup="User1" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Old Password"
                                Display="None">
                            </asp:RequiredFieldValidator>
                            <%--   <asp:RegularExpressionValidator ID="revLoginId" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtlogin"></asp:RegularExpressionValidator>
                            --%>
                        </td>
                    </tr>
                    <tr>
                        <td class="center" width="25%">
                            New Password<span style="color: Red">*</span>
                        </td>
                        <td class="center" width="25%">
                            <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="TextBox"
                                TabIndex="2" MaxLength="90">
                            </asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvNewPass" runat="server" ControlToValidate="txtNewPassword"
                                ValidationGroup="User" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter New Password"
                                Display="None">
                            </asp:RequiredFieldValidator>
                            <%--   <asp:RegularExpressionValidator ID="revLoginId" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtlogin"></asp:RegularExpressionValidator>
                            --%>
                        </td>
                    </tr>
                    <tr>
                        <td class="center" width="25%">
                            Confirm New Password<span style="color: Red">*</span>
                        </td>
                        <td class="center" width="25%">
                            <asp:TextBox ID="txtConfirmNewPassword" TextMode="Password" runat="server" CssClass="TextBox"
                                TabIndex="3" MaxLength="90">
                            </asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvConfirmNewPass" runat="server" ControlToValidate="txtConfirmNewPassword"
                                ValidationGroup="User" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Confirm Password"
                                Display="None">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ValidationGroup="User"
                                ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtConfirmNewPassword">
                            </asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToCompare="txtNewPassword"
                                ControlToValidate="txtConfirmNewPassword" SetFocusOnError="true" ValidationGroup="User"
                                ErrorMessage="Password Not Matched" ForeColor="Red">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <div class="Button_align">
                <asp:Button ID="btnCancel" TabIndex="5" CssClass="button_color action red" runat="server"
                    Text="Cancel" onclick="btnCancel_Click1" />
                <asp:Button ID="btnSave" runat="server" TabIndex="4" ValidationGroup="User" CssClass="button_color action green"
                    Text="Save" onclick="btnSave_Click" />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
