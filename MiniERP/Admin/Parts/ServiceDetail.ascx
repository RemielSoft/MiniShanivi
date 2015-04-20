<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceDetail.ascx.cs"
    Inherits="MiniERP.Parts.ServiceDetail"%>
<div class="box span12">
    <asp:ValidationSummary ID="vsService" runat="server" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="SD" />
    <div class="box-header well">
        <h2>
            Manage Service Details</h2>
    </div>
    <div class="box-content">
        <table class="table table-bordered table-striped table-condensed">
            <tbody>
                <tr>

                    <%--<td class="center">
                        WO Number <span style="color: Red">*</span>
                    </td>
                    <td class="center">
                        <asp:TextBox ID="txtWONumber" runat="server" Text="RA-001" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWONumber"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Work Order Number"
                            ValidationGroup="SD"></asp:RequiredFieldValidator>
                    </td>--%>
                    <td class="center">
                        Service Description <span style="color: Red">*</span>
                    </td>
                    <td class="center" colspan="3">
                        <asp:TextBox ID="txtServiceDesc" CssClass="TextBox" runat="server" TabIndex="0" Width="590"
                            MaxLength="80"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvServiceDesc" runat="server" ControlToValidate="txtServiceDesc"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Service Description"
                            ValidationGroup="SD"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revServiceDesc" runat="server" ControlToValidate="txtServiceDesc"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ValidationGroup="save"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtWONumber" runat="server" Text="RA-001" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr id="a" runat="server" visible="false">
                    <td class="center">
                        Service Number <span style="color: Red">*</span>
                    </td>
                    <td class="center">
                        <asp:TextBox ID="txtServiceNumber" Text="Service-001" CssClass="TextBox" runat="server"
                            MaxLength="10" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvServiceNumber" runat="server" ControlToValidate="txtServiceNumber"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Service Number"
                            ValidationGroup="SD"></asp:RequiredFieldValidator>
                    </td>
                    <td class="center">
                        Item Number <span style="color: Red">*</span>
                    </td>
                    <td class="center">
                        <asp:TextBox ID="txtItemNumber" CssClass="TextBox" Text="Item-001" runat="server"
                            TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvItemNumber" runat="server" ControlToValidate="txtItemNumber"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Item Number"
                            ValidationGroup="SD"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="b" runat="server" visible="false">
                    <td class="center">
                        Unit<span style="color: Red">*</span>
                    </td>
                    <td class="center">
                        <asp:DropDownList ID="ddlUnit" Width="120px" runat="server" TabIndex="3">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="ddlUnit"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Select Any Unit"
                            InitialValue="0" ValidationGroup="SD"></asp:RequiredFieldValidator>
                    </td>
                    <td class="center">
                        Quantity
                    </td>
                    <td class="center">
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="TextBox" Text="10" TabIndex="4"></asp:TextBox>
                        <asp:RangeValidator ID="rvQuantity" runat="server" ValidationGroup="SD" ForeColor="Red"
                            Display="None" SetFocusOnError="True" ControlToValidate="txtQuantity" MaximumValue="9999999999"
                            MinimumValue="0" Type="Double"></asp:RangeValidator>
                    </td>
                </tr>
                <tr id="c" runat="server" visible="false">
                    <td class="center">
                        Original Unit Rate<span style="color: Red">*</span>
                    </td>
                    <td class="center">
                        <asp:TextBox ID="txtUnitRate" runat="server" CssClass="TextBox" Text="10" TabIndex="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUnitRate" runat="server" ControlToValidate="txtUnitRate"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Original Unit Rate"
                            ValidationGroup="SD"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rvRate" runat="server" ValidationGroup="SD" ForeColor="Red"
                            Display="None" SetFocusOnError="True" ControlToValidate="txtUnitRate" MaximumValue="9999999999"
                            MinimumValue="0" Type="Double"></asp:RangeValidator>
                    </td>
                    <td class="center">
                        Applicable Rate<span style="color: Red">*</span>
                    </td>
                    <td class="center">
                        <asp:TextBox ID="txtApplicableRate" runat="server" CssClass="TextBox" Text="10" TabIndex="6"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvApplicableRate" runat="server" ControlToValidate="txtApplicableRate"
                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Applicable Rate"
                            ValidationGroup="SD"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rvAppRate" runat="server" ValidationGroup="SD" ForeColor="Red"
                            Display="None" SetFocusOnError="True" ControlToValidate="txtApplicableRate" MaximumValue="9999999999"
                            MinimumValue="0" Type="Double"></asp:RangeValidator>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <div class="Button_align">
            <asp:Button ID="btnResetSD" runat="server" Text="Cancel" OnClick="btnResetSD_Click"
                CssClass="button_color action red" />
            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="SD" OnClick="btnSave_Click"
                CssClass="button_color action" />
        </div>
        <br />
        <br />
        <asp:GridView ID="gvServiceDetail" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CssClass="mGrid" OnRowCommand="gvServiceDetail_RowCommand" PageSize="20" EmptyDataText="No Service Detail Added !"
            OnPageIndexChanging="gvServiceDetail_PageIndexChanging" AlternatingRowStyle-CssClass="alt"
            PagerStyle-CssClass="pgr" onrowediting="gvServiceDetail_RowEditing">
            <Columns>
                <asp:BoundField DataField="WorkOrderNumber" HeaderText="WO Number" Visible="false" />
                <asp:BoundField DataField="ServiceDescription" HeaderText="Service Description" />
                <asp:BoundField DataField="ItemNumber" HeaderText="ItemNumber" Visible="false" />
                <asp:BoundField DataField="ServiceNumber" HeaderText="Service Number" Visible="false" />
                <asp:TemplateField HeaderText="Unit" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit.Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" Visible="false" />
                <asp:BoundField DataField="UnitRate" HeaderText="Unit Rate" Visible="false" />
                <asp:BoundField DataField="ApplicableRate" HeaderText="Applicable Rate" Visible="false" />
                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="7%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="lnkEdit" CommandArgument='<%#Eval("ServiceDetailId")%>'
                            CssClass="button icon145 " ToolTip="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="lnkDelete" CommandArgument='<%#Eval("ServiceDetailId")%>'
                            OnClientClick="return confirm('Are You Sure To Delete?') " CssClass="button icon186 "
                            ToolTip="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>
