<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="ManageUser.aspx.cs" Inherits="MiniERP.Admin.ManageUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc2" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
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
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <script type="text/javascript" language="javascript">
                    function setDate(sender, args) {
                        var d = new Date(); //Today
                        d.setYear(d.getYear() - 15); //15 years ago
                        $find("myDate").set_selectedDate(d);
                    }
                </script>
            </div>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Manage User</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="25%">
                                    Login Id<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:TextBox ID="txtlogin" runat="server" CssClass="TextBox" AutoPostBack="True"
                                        TabIndex="2" MaxLength="90"></asp:TextBox><br />
                                    <asp:Label ID="lblMsg" runat="server" Text="Please Enter Different Login-Id " Visible="false"
                                        ForeColor="Red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtlogin"
                                        ValidationGroup="User" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Login-Id"
                                        Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revLoginId" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtlogin"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center" width="25%">
                                    Password<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:TextBox ID="txtpasswrd" runat="server" CssClass="TextBox" TabIndex="3" 
                                        TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtpasswrd"
                                        ValidationGroup="User" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please Enter Password"
                                        Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revPassword" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtpasswrd"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Employee Code<span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtempcode" runat="server" CssClass="TextBox" MaxLength="10" TabIndex="4"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revEmpCode" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtempcode"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvEmpCode" runat="server" ControlToValidate="txtempcode"
                                        ValidationGroup="User" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please Enter Employee Code"
                                        Display="None">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td class="center">
                                    Confirm Password<span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="TextBox" 
                                        TabIndex="5" TextMode="Password"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvConfirmPwd" runat="server" ControlToValidate="txtConfirmPassword"
                                        ValidationGroup="User" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please Enter Confirm Password"
                                   Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToCompare="txtpasswrd"
                                        ControlToValidate="txtConfirmPassword" SetFocusOnError="true" ValidationGroup="User"
                                        Text="Password Not Matched" ErrorMessage="Password Not Matched" ForeColor="Red"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    First Name<span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtfname" runat="server" CssClass="TextBox" MaxLength="20" TabIndex="6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtfname"
                                        ValidationGroup="User" ForeColor="Red" ErrorMessage="Please Enter First Name"
                                        SetFocusOnError="true" Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revFName" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtfname"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    Middle Name
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtmname" runat="server" CssClass="TextBox" MaxLength="20" TabIndex="7"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revMName" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtmname"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Last Name
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtlname" runat="server" CssClass="TextBox" MaxLength="20" TabIndex="8"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revLName" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtlname"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    Date Of Birth (mm/dd/yy)
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtdob" runat="server" CssClass="TextBox" Enabled="false" TabIndex="9"></asp:TextBox>
                                    <asp:LinkButton ID="lnkBtn" runat="server" TabIndex="21" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExt" OnClientShowing="setDate" BehaviorID="myDate" runat="server"
                                        Format="MM/dd/yyyy" TargetControlID="txtdob" PopupButtonID="lnkBtn">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Address<span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtaddress" runat="server" CssClass="mlttext" TextMode="MultiLine"
                                        TabIndex="10" MaxLength="250"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Please Enter Address"
                                        SetFocusOnError="true" ControlToValidate="txtaddress" ValidationGroup="User"
                                        Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td class="center">
                                    Department<span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddldepartmentId" runat="server" TabIndex="11" CssClass="dropdown">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddldepartmentId"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Department"
                                        ValidationGroup="User"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Gender
                                </td>
                                <td class="center">
                                    <asp:RadioButtonList ID="rdbgender" runat="server" TabIndex="12" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="center">
                                    Marital Status
                                </td>
                                <td class="center">
                                    <asp:RadioButtonList ID="rdbMaritalstatus" runat="server" TabIndex="13" RepeatDirection="Horizontal">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Email-Id<span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtemail" runat="server" CssClass="TextBox" MaxLength="40" TabIndex="14"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtemail"
                                        ValidationGroup="User" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please Enter Email-Id"
                                        Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmailId" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtemail"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    Phone No.
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtphn" runat="server" CssClass="TextBox" MaxLength="15" TabIndex="15"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revPhone" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtphn"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Mobile No.
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtmobile" runat="server" CssClass="TextBox" MaxLength="15" TabIndex="16"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revMobile" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtmobile"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    Office Ext. No.
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtofficeextno" CssClass="TextBox" runat="server" MaxLength="10"
                                        TabIndex="17"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revOfficeExtNo" runat="server" ValidationGroup="User"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtofficeextno"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Group<span style="color: Red">*</span>
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="chbgrp" runat="server" TabIndex="18" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                    <asp:RadioButtonList ID="rdoGroup" runat="server" RepeatDirection="Horizontal" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" OnClick="btnCancel_Click" TabIndex="19" CssClass="button_color action red"
                            runat="server" Text="Cancel" />
                        <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" TabIndex="20" Visible="false"
                            CssClass="button_color action blue" runat="server" Text="Update" ValidationGroup="User" />
                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" TabIndex="21" ValidationGroup="User"
                            CssClass="button_color action green" Text="Save" />
                    </div>
                    <br />
                    <br />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="User" />
                    <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" OnRowCommand="gvUser_RowCommand1"
                        OnPageIndexChanging="gvUser_PageIndexChanging1" AllowPaging="true" CssClass="mGrid"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowDataBound="gvUser_RowDataBound"
                        TabIndex="23">
                        <Columns>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkName" runat="server" CssClass="gridLink" Text='<%# Eval("FullName") %>'
                                        CommandArgument='<%# Eval("UserId") %>' TabIndex="22" CommandName="Details"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                            <asp:BoundField DataField="UserLoginId" HeaderText="Login Id" />
                            <asp:TemplateField HeaderText="Date Of Birth">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblDOB" runat="server" Text='<%#Eval("DateOfBirth","{0:dd/MM/yyyy}") %>' />--%>
                                    <asp:Label ID="lblDOB" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepName" runat="server" Text='<%#Eval("Department.Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("UserId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145">
                                        <asp:Image ID="imgEdit" runat="server" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("UserId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure You Want To Delete This User?') "
                                        ToolTip="Delete" CssClass="button icon186">
                                        <asp:Image ID="imgDelete" runat="server" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" TabIndex="24" BorderStyle="None"
                        BorderWidth="0px" CommandName="Select" />
                </div>
            </div>
            <div style="position: absolute; top: 1000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button1">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                    <div class="PopUpClose">
                        <div class="btnclosepopup">
                            <asp:Button ID="Button1" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
                    </div>
                    <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                        <div class="box span12">
                            <div class="box-header well">
                                <h2>
                                    View User Details</h2>
                            </div>
                            <div class="box-content">
                                <table class="table table-bordered ">
                                    <tr>
                                        <td class="center">
                                            <b>User Name</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>Address</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>Login-Id</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblLoginId" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>Employee Code</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblEmpCode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>Gender</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblGender" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>Marital Status</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>Office Ext. No</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblOfficeExtNo" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>Date Of Birth</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblDob" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>Mobile No.</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>Password</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblPwd" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>Email-Id</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>Department</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblDept" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>Phone No.</b>
                                        </td>
                                        <td class="center" colspan="3">
                                            <asp:Label ID="lblPhn" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
