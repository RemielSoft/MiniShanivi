<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="ManageSupplier.aspx.cs" Inherits="MiniERP.Admin.ManageSupplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc2" %>
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
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Manage Supplier</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td colspan="4" style="border-bottom: 2px solid green;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button_color action blue"
                                        OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="pnlSearch" runat="server">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center" width="25%">
                                        Supplier Name<span style="color: Red">*</span>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:TextBox ID="txtSupplier" CssClass="TextBox" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSupplier" runat="server" ControlToValidate="txtSupplier"
                                            ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Supplier Name"
                                            Display="None">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="revSupplierName" runat="server" ControlToValidate="txtSupplier"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    --%></td>
                                    <td class="center" width="25%">
                                        Supplier Address<span style="color: Red">*</span>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="mlttext" TextMode="MultiLine"
                                            TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                            ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Supplier Address"
                                            Display="None">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="revAddress" runat="server" ControlToValidate="txtAddress"
                                        Display="None" ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        City
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="4"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revCity" runat="server" ControlToValidate="txtCity"
                                            Display="None" ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">
                                        State
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtState" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="5"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revState" runat="server" ControlToValidate="txtState"
                                            Display="None" ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Supplier Mobile No.<span style="color: Red">*</span>
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtMobile" CssClass="TextBox" runat="server" MaxLength="15" TabIndex="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile"
                                            ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Supplier Mobile No."
                                            Display="None">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtMobile"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">
                                        Pin Code
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtPin" runat="server" CssClass="TextBox" MaxLength="10" TabIndex="7"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPin" runat="server" ControlToValidate="txtPin"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Supplier Phone No.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhn" runat="server" CssClass="TextBox" MaxLength="15" TabIndex="8"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPhn" runat="server" ControlToValidate="txtPhn"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">
                                        PAN
                                        <br />
                                        (AAAAA9999A)
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPan" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="9"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvPan" runat="server" ControlToValidate="txtPan"
                                            ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter PAN No."
                                            Display="None">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revPan" runat="server" ControlToValidate="txtPan"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        ESI
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEsi" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEsi" runat="server" ControlToValidate="txtEsi"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">
                                        TIN
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTan" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="11"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revTan" runat="server" ControlToValidate="txtTan"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Fax
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax" runat="server" CssClass="TextBox" MaxLength="15" TabIndex="12"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revFax" runat="server" ControlToValidate="txtFax"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">
                                        PF
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPf" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="13"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPf" runat="server" ControlToValidate="txtPf"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Email-Id
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox" MaxLength="40" TabIndex="14"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">
                                        Service Tax No
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtServicetax" runat="server" CssClass="TextBox" MaxLength="30"
                                            TabIndex="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revService" runat="server" ControlToValidate="txtServicetax"
                                            Display="None" ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Website<br />
                                        (http://www.google.co.in)
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="TextBox" MaxLength="50" TabIndex="15"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revWebsite" runat="server" ControlToValidate="txtWebsite"
                                            Display="None" ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div class="box-header well">
                            <h2>
                                Contact Person Details</h2>
                        </div>
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center" width="25%">
                                        Contact Person Name <span style="color: Red">*</span>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:TextBox ID="txtContactPName" runat="server" CssClass="TextBox" MaxLength="20"
                                            TabIndex="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtContactPName"
                                            ValidationGroup="Supplier" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Contact Person Name"
                                            Display="None">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="revContactName" runat="server" ControlToValidate="txtContactPName"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    --%></td>
                                    <td width="25%">
                                        Email-Id
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:TextBox ID="txtContactPEmail" runat="server" CssClass="TextBox" MaxLength="40"
                                            TabIndex="17"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revContactEmail" runat="server" ControlToValidate="txtContactPEmail"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phone No.
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtContactPPhone" CssClass="TextBox" runat="server" MaxLength="15"
                                            TabIndex="18"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revContactPhone" runat="server" ControlToValidate="txtContactPPhone"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Mobile No.
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtContactPMobile" CssClass="TextBox" runat="server" MaxLength="15"
                                            TabIndex="19"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revContactMobile" runat="server" ControlToValidate="txtContactPMobile"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Supplier"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div class="Button_align">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_color action red"
                                OnClick="btnCancel_Click" TabIndex="20" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="Supplier"
                                Visible="false" CssClass="button_color action blue" OnClick="btnUpdate_Click"
                                TabIndex="21" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button_color action green"
                                ValidationGroup="Supplier" OnClick="btnSave_Click" TabIndex="22" />
                            <%--<asp:Button ID="Button1" runat="server" Text="Search Supplier Details" CssClass="button_color action" />--%>
                        </div>
                        <br />
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Supplier" />
                    </asp:Panel>
                </div>
                <div class="box-content">
                    <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        PageSize="20" OnRowCommand="gvSupplier_RowCommand" OnPageIndexChanging="gvSupplier_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Supplier Name">
                                <ItemTemplate>
                                    <%-- <asp:Label ID="lblContractorName" runat="server" Text='<%#Eval("Name") %>'>' /></asp:Label>--%>
                                    <asp:LinkButton ID="lblSupplierName" runat="server" CssClass="gridLink" Text='<%#Eval("Name") %>'
                                        CommandArgument='<%#Eval("SupplierId") %>' CommandName="Details"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblVcode" runat="server" Text='<%#Eval("Information.VendorCode") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("SupplierId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145 ">
                                        <asp:Image ID="imgEdit" runat="server" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("SupplierId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure You Want To Delete This Supplier?') "
                                        ToolTip="Delete" CssClass="button icon186">
                                        <asp:Image ID="imgDelete" runat="server" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                        CommandName="Select" />
                </div>
            </div>
            <div style="position: absolute; top: 2000px;">
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
                                    View Supplier Details</h2>
                            </div>
                            <div class="box-content">
                                <table class="table table-bordered ">
                                    <tr>
                                        <td class="center">
                                            <b>Supplier Name</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>Supplier Address</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>City</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblCity" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>State</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblState" runat="server"></asp:Label>
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
                                            <b>Pin Code</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblPcode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>PAN</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>TAN</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblTan" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>Phone No.</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblPhn" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>FAX</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblFax" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>ESI</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblEsi" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>PF</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblPf" runat="server"></asp:Label>
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
                                            <b>Website</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblWebsite" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center">
                                            <b>Vendor Code</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblVendorCode" runat="server"></asp:Label>
                                        </td>
                                        <td class="center">
                                            <b>Service Tax No</b>
                                        </td>
                                        <td class="center">
                                            <asp:Label ID="lblService" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div class="box-header well">
                                    <h2>
                                        Contact Person Details</h2>
                                </div>
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td class="center">
                                                <b>Contact Person Name</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblContactPName" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <b>Email-Id</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblContactPEmail" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Phone No.</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblContactPPhone" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <b>Mobile No.</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblContactPMobile" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
