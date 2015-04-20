<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Payment.ascx.cs" Inherits="MiniERP.Parts.Payment"
    ViewStateMode="Enabled" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<script type="text/javascript">
    function Count(text, long) {

        var maxlength = new Number(long); // Change number to your max length.

        if (document.getElementById('<%=txtRemark.ClientID%>').value.length > maxlength) {

            text.value = text.value.substring(0, maxlength);

            alert(" More than " + long + " Characters are not Allowed in Remarks");

        }
    }
    function ValidateContractor() {

        var isContractor = "<%= this.IsContractor %>";


        var contractor = document.getElementById("ContentPlaceHolderMain_payment_txtContractorName")

        var fromDate = document.getElementById("ContentPlaceHolderMain_payment_txtFromDate")
        var toDate = document.getElementById("ContentPlaceHolderMain_payment_txtToDate")
        if (contractor.value != "") {
            return true;
        }
        else if (fromDate.value != "" && toDate.value != "") {

            return true;
        }
        else if (fromDate.value != "" && toDate.value == "" || fromDate.value == "" && toDate.value != "") {
            if (isContractor == "True") {
                alert("Please Enter Contractor Name Or Dates..");
                return false;
            } else {
                alert("Please Enter Supplier Name Or Dates..");
                return false;
            }

        }
        else if (fromDate.value == "" && toDate.value == "" && contractor.value == "") {

            if (isContractor == "True") {
                alert("Please Enter Contractor Name Or Dates..");
                return false;
            } else {
                alert("Please Enter Supplier Name Or Dates..");
                return false;
            }
        }
        else {
            return true;
        }
    }

</script>

<asp:UpdatePanel ID="upnlPayment" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div>
            <div class="box span12">
                <asp:ValidationSummary ID="vsInvoiceNo" runat="server" ShowMessageBox="True" ShowSummary="False"
                    ValidationGroup="InvNo" />
                <asp:ValidationSummary ID="vsPaymentDetail" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="pd" />
                <div class="box-header well">
                    <h2>
                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
                </div>
                <div class="box-content">
                    <asp:Panel ID="panel3" runat="server" DefaultButton="imgbtnSearch">
                        <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                            <tbody>
                                <tr>
                                    <td class="center">Invoice Number <span style="color: Red">*</span>
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtInvoiceNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" ControlToValidate="txtInvoiceNumber"
                                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Invoice Number"
                                            ValidationGroup="InvNo"></asp:RequiredFieldValidator>
                                        <asp:LinkButton ID="imgbtnSearch" runat="server" CausesValidation="false" ToolTip="Search"
                                            OnCommand="imgbtn_search_Click" CommandName="Search" CommandArgument="s" CssClass="Search icon198"></asp:LinkButton>
                                        <asp:HiddenField ID="hdfInvoiceNumber" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <asp:Label ID="lblContractor" runat="server" Text="Contractor Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContractorName" CssClass="TextBox ContractorName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                        <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" CssClass="calbox" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="txtFromDate" PopupButtonID="LinkButton2">
                                        </ajaxtoolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtToDate" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                        <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" CssClass="calbox" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="txtToDate" PopupButtonID="LinkButton3">
                                        </ajaxtoolkit:CalendarExtender>
                                    </td>
                                    <td class="center">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                                            ToolTip="Search" OnClick="btnSearch_Click1" OnClientClick="javascript:return ValidateContractor()"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </div>
                <div class="box-content">
                    <asp:Panel ID="pnl_main" runat="server" CssClass="mitem">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblCName" runat="server"></asp:Label>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblOrderNumber" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblCOrderNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trContractNumber" runat="server">
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblNumber" runat="server" Text="Contract Number"></asp:Label>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblContrctNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trWorkOrderNumber" runat="server">
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblWOrderNumber" runat="server" Text="Work Order Number"></asp:Label>
                                    </td>
                                    <td class="center" width="25%" colspan="3">
                                        <asp:Label ID="lblWorkOrderNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" style="font-size: 13px; color: #000; background-color: #ececec; font-weight: bold;"
                                        colspan="4">Invoice Detail
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" width="25%">Invoice Amount
                                    </td>
                                    <td class="center" width="25%">
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                        <asp:Label ID="lblInvoiceAmount" runat="server"></asp:Label>
                                    </td>
                                    <td class="center" width="25%">Left Amount
                                    </td>
                                    <td class="center" width="25%">
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                        <asp:Label ID="lblLeftAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" width="25%">Invoice Date
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblBillNumber" runat="server" Text="Bill Number"></asp:Label>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblBill" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" width="25%">Remarks
                                    </td>
                                    <td class="center" width="25%" colspan="3">
                                        <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </div>
                <div class="box-content">
                    <asp:Panel ID="pnl_payment" runat="server" CssClass="mitem">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center" width="25%">Payment Mode<span style="color: Red">*</span>
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:DropDownList ID="ddlPaymentMode" CssClass="dropdown" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPaymentMode" runat="server" ControlToValidate="ddlPaymentMode"
                                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Select Payment Mode"
                                            InitialValue="0" ValidationGroup="pd"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="center">Payment Date<span style="color: Red">*</span>
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtPaymentDate" CssClass="TextBox" runat="server"
                                            CausesValidation="false"></asp:TextBox>
                                        <asp:LinkButton ID="img_btn_payment_date" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                        <ajaxtoolkit:CalendarExtender ID="cal_ext_payment_date" CssClass="calbox" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="txtPaymentDate" PopupButtonID="img_btn_payment_date">
                                        </ajaxtoolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvPaymentDate" runat="server" ControlToValidate="txtPaymentDate"
                                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Select Payment Date"
                                            ValidationGroup="pd"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" width="25%">Payment Amount
                                    </td>
                                    <td class="center" width="25%">
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                        <asp:TextBox ID="txtPaymentAmount" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPaymentAmount" runat="server" ControlToValidate="txtPaymentAmount"
                                            Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Payment Amount"
                                            ValidationGroup="pd"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rvAmount" runat="server" ValidationGroup="pd" ForeColor="Red"
                                            Display="None" SetFocusOnError="True" ControlToValidate="txtPaymentAmount" MaximumValue="9999999999"
                                            ErrorMessage="Amount Must be Numeric" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                    </td>
                                    <td class="center">TDS
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtTDS" CssClass="TextBox" runat="server" CausesValidation="false"
                                            MaxLength="30"></asp:TextBox>%
                                             <asp:RegularExpressionValidator ID="revtxtTDS" runat="server" ControlToValidate="txtTDS"
                                                 Display="None" ValidationGroup="Contractor" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>

                                        <asp:RequiredFieldValidator ID="rfvTDS" runat="server" ControlToValidate="txtTDS"
                                            Display="None" ErrorMessage="Please Enter Reference Number" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnaddPayment" runat="server" ValidationGroup="pd" Text="Add" ToolTip=""
                                            CssClass="button_color action green" OnClick="btnaddPayment_Click" />
                                    </td>
                                </tr>
                                <caption>
                                    <tr>
                                        <td>Bank Name<span style="color: Red">*</span>
                                        </td>
                                        <td class="center">
                                            <asp:TextBox ID="txtBankName" runat="server" CausesValidation="false" CssClass="TextBox"
                                                MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="txtBankName"
                                                Display="None" ErrorMessage="Please Enter Bank Name" ForeColor="Red" SetFocusOnError="True"
                                                ValidationGroup="pd"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="center">Reference Number
                                        </td>
                                        <td class="center">
                                            <asp:TextBox ID="txtReferenceNo" runat="server" CausesValidation="false" CssClass="TextBox"
                                                MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReferenceNumber" runat="server" ControlToValidate="txtReferenceNo"
                                                Display="None" ErrorMessage="Please Enter Reference Number" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center" width="25%">Payment Status<span style="color: Red">*</span>
                                        </td>
                                        <td class="center" width="25%">
                                            <asp:DropDownList ID="ddlPaymentStatus" CssClass="dropdown" runat="server">
                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPaymentStatus" runat="server" ControlToValidate="ddlPaymentStatus"
                                                Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Select Payment Status"
                                                InitialValue="0" ValidationGroup="pd"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="uplfile" runat="server">
                                        <td class="center" width="25%">
                                            <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                                        </td>
                                        <td class="center" colspan="3" width="75%">
                                            <div>
                                                <table class="table table-bordered table-striped table-condensed">
                                                    <tr>
                                                        <td id="ajaxupload" runat="server" class="center">
                                                            <asp:FileUpload ID="FileUpload_Control" runat="server" />
                                                            <asp:Button ID="btn_upload" runat="server" CausesValidation="false" OnClick="btn_upload_Click"
                                                                Text="Upload" />
                                                        </td>
                                                        <td class="center">
                                                            <asp:Panel ID="Panel4" runat="server" Height="80px" ScrollBars="Both" Width="100%">
                                                                <div class="box-content">
                                                                    <asp:GridView ID="gv_documents" runat="server" AlternatingRowStyle-CssClass="alt"
                                                                        AutoGenerateColumns="false" CellPadding="4" CssClass="mGrid" EmptyDataText="No-Documents Uploaded."
                                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gv_documents_RowCommand" ShowHeader="false"
                                                                        Width="100%">
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="File">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtn_file" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                        CommandName="OpenFile" Text='<%#Eval("Original_Name")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-Width="20px" HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                        CommandName="FileDelete" CssClass="button icon186" OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                                                        ToolTip="Delete"></asp:LinkButton>
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
                                    <tr>
                                        <td class="center">Remarks
                                        </td>
                                        <td class="center" colspan="3">
                                            <asp:TextBox ID="txtRemark" runat="server" CausesValidation="false" Columns="50"
                                                CssClass="mlttext" onChange="javascript:Count(this,250);" onKeyUp="javascript:Count(this,250);"
                                                Rows="5" TextMode="MultiLine" Width="307px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </caption>
                            </tbody>
                        </table>
                    </asp:Panel>
                    <br />
                    <div class="Button_align" id="btnPart" runat="server">
                        <asp:Button ID="btnReset" runat="server" Text="Cancel" ToolTip="Reset" CssClass="button_color action red"
                            OnClick="btnReset_Click" CausesValidation="false" />
                        <asp:Button ID="btnSaveDraft" runat="server" ValidationGroup="pd" Text="Save Draft"
                            ToolTip="" CssClass="button_color action green" OnClick="btnSaveDraft_Click" />
                    </div>
                    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                        CommandName="Select" />
                </div>
            </div>
            <div style="position: absolute; top: 3000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel1" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button8">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel1" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                            <div class="btnclosepopup">
                                <asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                            </div>
                        </div>
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                            <div>
                                <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No Record Found(s)"
                                    AlternatingRowStyle-CssClass="alt" OnRowDataBound="gvInvoice_RowDataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged"
                                                    AutoPostBack="true"></asp:RadioButton>
                                                <asp:HiddenField ID="hdfInvoiceNumberId" runat="server" Value='<%# Eval("ContractorInvoiceId")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkbtnInvoice" runat="server" Text='<%#Eval("InvoiceNumber") %>'></asp:Label>
                                                <%-- <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("IssueMaterial.DemandVoucher.Quotation.StatusType.Id") %>' />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QuotationNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuotation" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contractor Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCNames" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractorName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QuotationNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSQuotation" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.SupplierQuotationNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSName" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.SupplierName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceDates" runat="server" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillDate" runat="server" Text='<%#Eval("BillDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillNumber" runat="server" Text='<%#Eval("BillNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Contractor Work Order No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConNo" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Invoice Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoicetypeName" runat="server" Text='<%#Eval("InvoiceType.Name") %>'></asp:Label>
                                                <%--<asp:HiddenField ID="hdfInvoiceTypeId" runat="server" Value='<%#Eval("InvoiceType.Id") %>' />--%>
                                                <%-- <asp:Label ID="lblInvoiceTypeId" runat="server" Text='<%#Eval("InvoiceType.Id") %>' Visible="false"></asp:Label>--%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalText" runat="server" Text="Total : " EnableViewState="True"
                                                    CssClass="total"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Net Value (INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentinvoice" runat="server" Text='<%#Eval("PayableAmount") %>'></asp:Label>
                                                <%--<asp:HiddenField ID="hdfInvoiceTypeId" runat="server" Value='<%#Eval("InvoiceType.Id") %>' />--%>
                                                <%-- <asp:Label ID="lblInvoiceTypeId" runat="server" Text='<%#Eval("InvoiceType.Id") %>' Visible="false"></asp:Label>--%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal" runat="server" CssClass="total"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:BoundField DataField="PayableAmount" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_upload" />
    </Triggers>
</asp:UpdatePanel>
