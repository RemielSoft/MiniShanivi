<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyWorkOrder.ascx.cs"
    Inherits="MiniERP.Company.Parts.CompanyWorkOrder" ViewStateMode="Enabled" %>
<script type="text/javascript">
    function Count(text, long) {

        var maxlength = new Number(long); // Change number to your max length.

        if (document.getElementById('<%=txtContractDesc.ClientID%>').value.length > maxlength) {

            text.value = text.value.substring(0, maxlength);

            alert(" More than " + long + " Characters are not Allowed in Work Order Description.");

        }

        if (document.getElementById('<%=txtRemark.ClientID%>').value.length > maxlength) {

            text.value = text.value.substring(0, maxlength);

            alert(" More than " + long + " Characters are not Allowed in Remarks.");

        }

    }
</script>
<div>
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <ajaxtoolkit:TabPanel runat="server" HeaderText="Company Work Order" ID="TabPanel1">
                    <HeaderTemplate>
                        Company Work Order
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="box span12">
                            <asp:ValidationSummary ID="vsWO" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="WO" />
                            <div class="box-header well">
                                <h2>
                                    Company Work Order</h2>
                            </div>
                            <div class="box-content">
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td class="center" width="25%">
                                                Contract Date <span style="color: Red">*</span>
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:TextBox ID="txtContractDate" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                                <asp:LinkButton ID="LnkButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175"
                                                    TabIndex="1"></asp:LinkButton>
                                                <ajaxtoolkit:CalendarExtender ID="calContractDate" runat="server" Format="dd/MM/yyyy"
                                                    PopupButtonID="LnkButton2" TargetControlID="txtContractDate" Enabled="True">
                                                </ajaxtoolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvContractDate" runat="server" ControlToValidate="txtContractDate"
                                                    Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Select Contract Date"
                                                    ValidationGroup="WO"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="center" width="25%">
                                                Contract Number <span style="color: Red">*</span>
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:TextBox ID="txtContractNo" TabIndex="2" CssClass="TextBox" runat="server" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvContractNo" runat="server" ControlToValidate="txtContractNo"
                                                    Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Contract Number"
                                                    ValidationGroup="WO"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center" width="25%">
                                                Work Order Description
                                            </td>
                                            <td colspan="3" class="center" width="75%">
                                                <asp:TextBox ID="txtContractDesc" CssClass="mlttext" TextMode="MultiLine" Rows="3"
                                                    Columns="50" runat="server" Width="410px" onKeyUp="javascript:Count(this,250);"
                                                    onChange="javascript:Count(this,250);" TabIndex="2"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                                <asp:ValidationSummary ID="vsWOM" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="WOM" />
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td class="center" width="25%">
                                                Amount <span style="color: Red">*</span>
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:TextBox ID="txtAmount" runat="server" TabIndex="3" CssClass="TextBox" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                                    Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Amount"
                                                    ValidationGroup="WOM"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="rvAmount" runat="server" ValidationGroup="WOM" ForeColor="Red"
                                                    Display="None" SetFocusOnError="True" ControlToValidate="txtAmount" MaximumValue="9999999999"
                                                    MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </td>
                                            <td class="center" width="25%">
                                                Work Order Number <span style="color: Red">*</span>
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:TextBox ID="txtWorkOrderNo" CssClass="TextBox" runat="server" TabIndex="4" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvWONo" runat="server" ControlToValidate="txtWorkOrderNo"
                                                    Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Work Order Number"
                                                    ValidationGroup="WOM"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center" width="25%">
                                                Area
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:TextBox ID="txtArea" CssClass="TextBox" runat="server" TabIndex="5" MaxLength="30"></asp:TextBox>
                                            </td>
                                            <td class="center" width="25%">
                                                Location
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:TextBox ID="txtLocation" CssClass="TextBox" runat="server" TabIndex="6" MaxLength="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td class="center" width="25%">
                                                Service Tax
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:TextBox ID="txtServiceTax" CssClass="TextBox" runat="server" MaxLength="10"
                                                    TabIndex="7"></asp:TextBox>
                                                %
                                                <asp:RangeValidator ID="rngServiceTax" runat="server" ValidationGroup="WOM" ForeColor="Red"
                                                    Display="None" SetFocusOnError="True" ControlToValidate="txtServiceTax" MaximumValue="9999999999"
                                                    MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </td>
                                            <td class="center" width="25%">
                                                VAT
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:TextBox ID="txtVAT" CssClass="TextBox" runat="server" MaxLength="10" TabIndex="8"></asp:TextBox>
                                                %
                                                <asp:RangeValidator ID="rngVat" runat="server" ValidationGroup="WOM" ForeColor="Red"
                                                    Display="None" SetFocusOnError="True" ControlToValidate="txtVAT" MaximumValue="9999999999"
                                                    MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                CST (with C Form)
                                            </td>
                                            <td class="center">
                                                <asp:TextBox ID="txtCSTWithCForm" CssClass="TextBox" runat="server" MaxLength="10"
                                                    TabIndex="9"></asp:TextBox>
                                                %
                                                <asp:RangeValidator ID="rngCST" runat="server" ValidationGroup="WOM" ForeColor="Red"
                                                    Display="None" SetFocusOnError="True" ControlToValidate="txtCSTWithCForm" MaximumValue="9999999999"
                                                    MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </td>
                                            <td class="center">
                                                CST (Without C Form)
                                            </td>
                                            <td class="center">
                                                <asp:TextBox ID="txtCSTWithoutCForm" CssClass="TextBox" runat="server" MaxLength="10"
                                                    TabIndex="10"></asp:TextBox>
                                                %
                                                <asp:RangeValidator ID="rngCSTWithout" runat="server" ValidationGroup="WOM" ForeColor="Red"
                                                    Display="None" SetFocusOnError="True" ControlToValidate="txtCSTWithoutCForm"
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center" width="25%">
                                                Freight
                                            </td>
                                            <td colspan="3" class="center" width="75%">
                                                <img alt="" src="../../Images/rupee_symbol5.png" />
                                                <asp:TextBox ID="txtFreight" CssClass="TextBox" runat="server" TabIndex="11" MaxLength="10"></asp:TextBox>
                                                <asp:RangeValidator ID="rngFreight" runat="server" ValidationGroup="WOM" ForeColor="Red"
                                                    Display="None" SetFocusOnError="True" ControlToValidate="txtFreight" MaximumValue="9999999999"
                                                    MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center" width="25%">
                                                Discount Type
                                            </td>
                                            <td class="center" width="25%">
                                                <asp:DropDownList ID="ddlDiscountType" CssClass="dropdown" runat="server" TabIndex="12"
                                                    AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddlDiscountType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="center" width="25%" id="dis_td" runat="server">
                                                Discount 
                                                <span style="color: Red" id="spndiscount" runat="server" 
                                                    visible="False">*</span>
                                            </td>
                                            <td class="center" width="25%" id="dis_txt_td" runat="server">
                                                <asp:Image ID="imgRupee" runat="server" ImageUrl="~/Images/rupee_symbol5.png" Visible="False" />
                                                <asp:TextBox ID="txtTotalDiscount" CssClass="TextBox" MaxLength="10" runat="server"
                                                    TabIndex="13"></asp:TextBox>
                                                <asp:Label ID="lblPercentage" runat="server" Text="%" Visible="False" />
                                                <asp:RangeValidator ID="rngTotalDiscount" runat="server" ValidationGroup="WOM" ForeColor="Red"
                                                    Display="None" SetFocusOnError="True" ControlToValidate="txtTotalDiscount" MaximumValue="9999999999"
                                                    MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvDiscount" runat="server" ControlToValidate="txtTotalDiscount"
                                                    Display="None" ErrorMessage="Please Enter Discount" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <div class="Button_align">
                                    <asp:Button ID="btnResetWO" runat="server" Text="Reset" OnClick="btnResetWO_Click"
                                        CssClass="button_color action red" TabIndex="1" CausesValidation="False"/>
                                    <asp:Button ID="btnAddWO" runat="server" Text="Add" ValidationGroup="WOM" CssClass="button_color action"
                                        OnClick="btnAddWO_Click" />
                                </div>
                                <br />
                                <br />
                                <div>
                                    <asp:GridView ID="gvWorkOrder" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        OnRowCommand="gvWorkOrder_RowCommand" EmptyDataText="No Work Order Added !">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:BoundField DataField="WorkOrderNumber" HeaderText="Work Order Number" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" >
<ItemStyle CssClass="taright" />
</asp:BoundField>
                                            <asp:BoundField DataField="Area" HeaderText="Area" />
                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                            <asp:TemplateField HeaderText="Service Tax (%)" ><ItemTemplate>
                                                    <asp:Label ID="lblServiceTax" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                                
</ItemTemplate>

<ItemStyle CssClass="taright" />
</asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vat(%)" ><ItemTemplate>
                                                    <asp:Label ID="lblVAT" runat="server" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                                
</ItemTemplate>

<ItemStyle CssClass="taright" />
</asp:TemplateField>
                                            <asp:TemplateField HeaderText="CST (With C Form) (%)" ><ItemTemplate>
                                                    <asp:Label ID="lblCSTwith_C_Form" runat="server" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                                
</ItemTemplate>

<ItemStyle CssClass="taright" />
</asp:TemplateField>
                                            <asp:TemplateField HeaderText="CST (Without C Form) (%)" ><ItemTemplate>
                                                    <asp:Label ID="lblCSTWithout_C_Form" runat="server" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                                
</ItemTemplate>

<ItemStyle CssClass="taright" />
</asp:TemplateField>
                                            <asp:TemplateField HeaderText="Freight (INR)" ><ItemTemplate>
                                                    <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("TaxInformation.Freight") %>'></asp:Label>
                                                
</ItemTemplate>

<ItemStyle CssClass="taright" />
</asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount Type"><ItemTemplate>
                                                    <asp:Label ID="lblDiscountType" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                                
</ItemTemplate>
</asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount" ><ItemTemplate>
                                                    <asp:Label ID="lblDiscount" runat="server" Text='<%#Eval("TaxInformation.TotalDiscount") %>'></asp:Label>
                                                
</ItemTemplate>

<ItemStyle CssClass="taright" />
</asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount" ><ItemTemplate>
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TaxInformation.TotalNetValue") %>'></asp:Label>
                                                
</ItemTemplate>

<ItemStyle CssClass="taright" />
</asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action"><ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" CommandName="lnkEdit"
                                                        CommandArgument='<%#Container.DataItemIndex%>' ToolTip="Edit" CssClass="button icon145"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="lnkDelete"
                                                        CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are You Sure To Delete') "
                                                        ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                                
</ItemTemplate>
</asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>
                                </div>
                                <div>
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td class="right" width="25%">
                                                    Total Amount :
                                                </td>
                                                <td width="75%">
                                                    <img alt="" src="../../Images/rupee_symbol5.png" />
                                                    <asp:TextBox ID="txtTotAmount" CssClass="TextBox" runat="server" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <br />
                                <div class="Button_align">
                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Cancel" 
                                        CssClass="button_color action red" CausesValidation="False"/>
                                    <asp:Button ID="btnSave" runat="server" Text="Submit Draft" ValidationGroup="WO" CssClass="button_color action green"
                                        OnClick="btnSave_Click"/>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxtoolkit:TabPanel>
                <ajaxtoolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Bank Guarantee">
                    <ContentTemplate>
                        <asp:ValidationSummary ID="vsBG" runat="server" ShowMessageBox="True" ShowSummary="False"
                            ValidationGroup="BG" />
                        <div class="box span12">
                            <div class="box-header well">
                                <h2>
                                    Bank Guarantee</h2>
                            </div>
                            <div class="box-content">
                                <asp:Panel ID="Panel1" runat="server" CssClass="mitem">
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td class="center" width="25%">
                                                    <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                                                </td>
                                                <td colspan="3" class="center" width="75%">
                                                    <div>
                                                        <table class="table table-bordered table-striped table-condensed">
                                                            <tr>
                                                                <td class="center" id="ajaxupload" runat="server">
                                                                    <%-- <asp:FileUpload ID="FileUpload_Control" runat="server" onchange="this.form.submit();" />--%>
                                                                    <asp:FileUpload ID="FileUpload_Control" runat="server" />
                                                                    <asp:Button ID="btn_upload" runat="server" Text="Upload" CausesValidation="false"
                                                                        OnClick="btn_upload_Click" />
                                                                </td>
                                                                <td class="center">
                                                                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" Height="80px" Width="100%">
                                                                        <div class="box-content">
                                                                            <asp:GridView ID="gv_documents" runat="server" CellPadding="4" ForeColor="#333333"
                                                                                GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" EmptyDataText="No-Documents Uploaded."
                                                                                AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="gv_documents_RowCommand"
                                                                                Width="100%">
                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="File">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtn_file" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                                                                                                CommandName="OpenFile" Text='<%#Eval("Original_Name")%>' CausesValidation="false"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20px">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandName="FileDelete"
                                                                                                CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                                                                ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnl" runat="server" CssClass="mitem">
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td class="center" width="25%">
                                                    Start Date <span style="color: Red">*</span>
                                                </td>
                                                <td class="center" width="25%">
                                                    <asp:TextBox ID="txtStartDate" CssClass="TextBox" TabIndex="0" runat="server" Enabled="false"
                                                        ViewStateMode="Enabled"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkBtnSD" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                                    <ajaxtoolkit:CalendarExtender ID="calStartDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtStartDate" PopupButtonID="lnkBtnSD" Enabled="True">
                                                    </ajaxtoolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                        Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Select Start Date"
                                                        ValidationGroup="BG"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="center" width="25%">
                                                    End Date <span style="color: Red">*</span>
                                                </td>
                                                <td class="center" width="25%">
                                                    <asp:TextBox ID="txtEndDate" CssClass="TextBox" runat="server" TabIndex="1" Enabled="false"
                                                        ViewStateMode="Enabled"></asp:TextBox>
                                                    <asp:LinkButton ID="imgBtnED" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                                    <ajaxtoolkit:CalendarExtender ID="calEndDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtEndDate" PopupButtonID="imgBtnED" Enabled="True">
                                                    </ajaxtoolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                                        Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Select End Date"
                                                        ValidationGroup="BG"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="center">
                                                    Amount <span style="color: Red">*</span>
                                                </td>
                                                <td class="center">
                                                    <img alt="" src="../../Images/rupee_symbol5.png" />
                                                    <asp:TextBox ID="txtAmountBG" CssClass="TextBox" runat="server" MaxLength="10" TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAmountBG" runat="server" ControlToValidate="txtAmountBG"
                                                        Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Amount"
                                                        ValidationGroup="BG"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvAmountBG" runat="server" ValidationGroup="BG" ForeColor="Red"
                                                        Display="None" SetFocusOnError="True" ControlToValidate="txtAmountBG" MaximumValue="9999999999"
                                                        MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                </td>
                                                <td class="center">
                                                    Bank Name <span style="color: Red">*</span>
                                                </td>
                                                <td class="center">
                                                    <asp:TextBox ID="txtBankName" CssClass="TextBox" runat="server" TabIndex="3" MaxLength="30"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="txtBankName"
                                                        Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Bank Name"
                                                        ValidationGroup="BG"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="center" width="25%">
                                                    Remarks
                                                </td>
                                                <td class="center" colspan="3" width="75%">
                                                    <asp:TextBox ID="txtRemark" Rows="3" Columns="50" runat="server" Width="410px" CssClass="mlttext"
                                                        TextMode="MultiLine" TabIndex="4" CausesValidation="false" onKeyUp="javascript:Count(this,250);"
                                                        onChange="javascript:Count(this,250);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="right">
                                                    <asp:Button ID="btnResetBG" runat="server" Text="Reset" OnClick="btnResetBG_Click"
                                                        CssClass="button_color action red" CausesValidation="false" />
                                                    <asp:Button ID="btnAddBG" runat="server" Text="Add" ValidationGroup="BG" OnClick="btnAddBG_Click"
                                                        CssClass="button_color action" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                                <br />
                                <br />
                                <asp:GridView ID="gbBankGuarantee" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    OnRowCommand="gbBankGuarantee_RowCommand" EmptyDataText="No Bank Guarantee Added !">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:BoundField DataField="StartDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="Start Date" />
                                        <asp:BoundField DataField="EndDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="End Date" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount"  ItemStyle-CssClass="taright" />
                                        <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Edit" runat="server" CausesValidation="false" CommandName="lnkEdit"
                                                    CommandArgument='<%#Container.DataItemIndex%>' ToolTip="Edit" CssClass="button icon145"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtn_Delete" runat="server" CausesValidation="false" CommandName="lnkDelete"
                                                    CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are You Sure To Delete') "
                                                    ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxtoolkit:TabPanel>
            </ajaxtoolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
