<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="ManageContractor.aspx.cs" Inherits="MiniERP.Admin.ManageContractor" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>Manage Contractor</h2>
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
                                    <td class="center" width="25%">Company Name <span style="color: Red">*</span>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:TextBox ID="txtcontractorName" CssClass="TextBox" runat="server" MaxLength="50"
                                            TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvcontractorName" runat="server" ErrorMessage="Please Enter Company Name"
                                            ControlToValidate="txtcontractorName" ValidationGroup="Contractor" Display="None"
                                            ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="revContractorName" runat="server" ControlToValidate="txtcontractorName"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        --%> </td>
                                    <td width="25%">Company Email-Id
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox" MaxLength="40" TabIndex="2"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Company Address <span style="color: Red">*</span>
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtAddress" CssClass="mlttext" runat="server" TextMode="MultiLine"
                                            MaxLength="500" TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Please Enter Company Address"
                                            SetFocusOnError="true" ControlToValidate="txtAddress" ValidationGroup="Contractor"
                                            Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="revAddress" runat="server" ControlToValidate="txtAddress"
                                        Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        --%> </td>
                                    <td>City
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="4"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revCity" runat="server" ControlToValidate="txtCity"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>State
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtState" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="6"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revState" runat="server" ControlToValidate="txtState"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>Pin Code
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtPin" runat="server" CssClass="TextBox" MaxLength="6" TabIndex="7"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPin" runat="server" ControlToValidate="txtPin"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Company Phone No.
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="TextBox" MaxLength="15" TabIndex="8"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPhn" runat="server" ControlToValidate="txtPhone"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>Company Mobile No. <span style="color: Red">*</span>
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="TextBox" MaxLength="15" TabIndex="9"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtMobile"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ErrorMessage="Please Enter Company Mobile No."
                                            SetFocusOnError="true" ControlToValidate="txtMobile" ValidationGroup="Contractor"
                                            Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">Service Tax No
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtServicetax" runat="server" CssClass="TextBox" MaxLength="30"
                                            TabIndex="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revService" runat="server" ControlToValidate="txtServicetax"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">PAN <span style="color: Red">*</span><br />
                                        (AAAAA9999A)
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPan" runat="server" CssClass="TextBox" MaxLength="30" TabIndex="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPan" runat="server" ControlToValidate="txtPan"
                                            ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter PAN Number"
                                            Display="None">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPan" runat="server" ControlToValidate="txtPan"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">ESI
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEsi" runat="server" MaxLength="30" CssClass="TextBox" TabIndex="12"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEsi" runat="server" ControlToValidate="txtEsi"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">TIN
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTan" runat="server" MaxLength="30" CssClass="TextBox" TabIndex="13"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revTan" runat="server" ControlToValidate="txtTan"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">Fax
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax" runat="server" MaxLength="15" CssClass="TextBox" TabIndex="14"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revFax" runat="server" ControlToValidate="txtFax"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">PF
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPf" runat="server" MaxLength="30" CssClass="TextBox" TabIndex="15"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPf" runat="server" ControlToValidate="txtPf"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Website<br />
                                        (http://www.google.co.in)
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtWebsite" runat="server" MaxLength="50" CssClass="TextBox" TabIndex="16"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revWebsite" runat="server" ControlToValidate="txtWebsite"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>Description
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtDescription" Width="150px" runat="server" CssClass="mlttext"
                                            TextMode="MultiLine" MaxLength="500" TabIndex="17"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator ID="revdescription" runat="server" ControlToValidate="txtDescription"
                                        Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div class="box-header well">
                            <h2>Contact Person Details</h2>
                        </div>
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center" width="25%">Contact Person Name <span style="color: Red">*</span>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:TextBox ID="txtContactPName" runat="server" CssClass="TextBox" MaxLength="40"
                                            TabIndex="18"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ErrorMessage="Please Enter Contact Person Name"
                                            ControlToValidate="txtContactPName" ValidationGroup="Contractor" Display="None"
                                            ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="revContactName" runat="server" ControlToValidate="txtContactPName"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" ErrorMessage="Only Alphabets Are Allowed"
                                            ValidationExpression="^[a-zA-Z''-'\s]{1,40}$" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                        --%> </td>
                                    <td width="25%">Email-Id
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:TextBox ID="txtContactPEmail" runat="server" CssClass="TextBox" MaxLength="40"
                                            TabIndex="19"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revContactEmail" runat="server" ControlToValidate="txtContactPEmail"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Phone No.
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtContactPPhone" runat="server" CssClass="TextBox" MaxLength="15"
                                            TabIndex="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revContactPhn" runat="server" ControlToValidate="txtContactPPhone"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>Mobile No.
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtContactPMobile" runat="server" CssClass="TextBox" MaxLength="15"
                                            TabIndex="21"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revContactMobile" runat="server" ControlToValidate="txtContactPMobile"
                                            Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" CssClass="button_color action red" runat="server" Text="Cancel"
                            OnClick="btnCancel_Click" TabIndex="24" />
                        <asp:Button ID="btnUpdate" Visible="false" CssClass="button_color action blue" runat="server"
                            Text="Update" OnClick="btnUpdate_Click" ValidationGroup="Contractor" TabIndex="23" />
                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="button_color action green"
                            Text="Save" ValidationGroup="Contractor" TabIndex="22" />
                    </div>
                    <br />
                    <br />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="Contractor" runat="server" />
                </div>
                <div class="box-content">
                    <asp:GridView ID="gvContractor" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowCommand="gvContractor_RowCommand" PageSize="20" OnPageIndexChanging="gvContractor_PageIndexChanging">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>--%>
                                    <asp:LinkButton ID="lnkName" runat="server" CssClass="gridLink" CommandName="Details"
                                        CommandArgument='<%#Eval("ContractorId") %>' Text='<%#Eval("Name") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendorCode" runat="server" Text='<%#Eval("Information.VendorCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Mobile No." ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile") %>'>' /></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="taright" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ContractorId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145 ">
                                        <asp:Image ID="imgEdit" runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ContractorId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure You Want To Delete This Contractor?') "
                                        ToolTip="Delete" CssClass="button icon186 ">
                                        <asp:Image ID="imgDelete" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                    <div style="display: none">
                        <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                            CommandName="Select" />
                    </div>
                </div>
            </div>
            <div style="position: absolute; top: 1000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" CancelControlID="Button3" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                            <div class="btnclosepopup">
                                <asp:Button ID="Button3" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                            </div>
                        </div>
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                            <div class="box span12">
                                <div class="box-header well">
                                    <h2>View Company Details</h2>
                                </div>
                                <div class="box-content">
                                    <table class="table table-bordered ">
                                        <tr>
                                            <td class="center" width="20%">
                                                <b>Company Name</b>
                                            </td>
                                            <td class="center" width="30%">
                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                            </td>
                                            <td class="center" width="20%">
                                                <b>Address</b>
                                            </td>
                                            <td class="center" width="30%">
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
                                                <b>Phone No.</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
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
                                                <b>Service Tax No</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblServiceNo" runat="server"></asp:Label>
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
                                                <b>Pin Code</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblPcode" runat="server"></asp:Label>
                                            </td>
                                            <td class="center">
                                                <b>Description</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblDes" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                <b>Vendor Code</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblVendorCode" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div class="box-header well">
                                        <h2>Contact Person Details</h2>
                                    </div>
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td class="center" width="20%">
                                                    <b>Contact Person Name</b>
                                                </td>
                                                <td class="center" width="30%">
                                                    <asp:Label ID="lblContactPName" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <b>Email-Id</b>
                                                </td>
                                                <td class="center" width="30%">
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
                                    <br />
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
