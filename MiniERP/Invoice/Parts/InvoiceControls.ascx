<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceControls.ascx.cs"
    Inherits="MiniERP.Invoice.Parts.InvoiceControls" ViewStateMode="Enabled" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">
    function CheckOtherIsCheckedByGVID(rdbtn) {

        var IsChecked = rdbtn.checked;

        if (IsChecked) {

            rdbtn.parentElement.parentElement.style.backgroundColor = '#228b22';

            rdbtn.parentElement.parentElement.style.color = 'white';

        }
        var CurrentRdbID = rdbtn.id;

        var Chk = rdbtn;

        Parent = document.getElementById("<%=gvPaymentType.ClientID%>");

        var items = Parent.getElementsByTagName('input');

        for (i = 0; i < items.length; i++) {

            if (items[i].id != CurrentRdbID && items[i].type == "radio") {

                if (items[i].checked) {

                    items[i].checked = false;

                    items[i].parentElement.parentElement.style.backgroundColor = 'white'

                    items[i].parentElement.parentElement.style.color = 'black';
                }

            }

        }

    }

    function ValidateContractorName() {
        debugger
        var contractorName = document.getElementById("ContentPlaceHolderMain_Invoice_Controls_txtContractorName")//ContentPlaceHolderMain_Invoice_Controls_txtContractorName
        var fromDate = document.getElementById("ContentPlaceHolderMain_Invoice_Controls_txtFromDate")
        var todate = document.getElementById("ContentPlaceHolderMain_Invoice_Controls_txtToDate")

        if (contractorName.value != "") {
            //alert("Please enter Contractor Name");
            return true;
        }
        else if (fromDate.value != "" && todate.value != "") {

            return true;
        }
        else if (fromDate.value != "" && todate.value == "" || fromDate.value == "" && todate.value != "") {
                alert("Please Enter From date  Or ToDates..");
                return false;
        }
        else if (fromDate.value == "" && todate.value == "" && contractorName.value == "") {
       
            alert("Please Enter Contractor Name Or Dates..");
                return false;
        }
        else {
            return true;
        }
    }
</script>
<script type="text/javascript">
    function Count(text, long) {

        var maxlength = new Number(long); // Change number to your max length.

        if (document.getElementById('<%=txtRemarks.ClientID%>').value.length > maxlength) {

            text.value = text.value.substring(0, maxlength);

            alert(" More than " + long + " Characters are not Allowed in Remarks");

        }
    }
</script>
<asp:UpdateProgress ID="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/ajax_loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 40px; height: 40px; position: fixed; top: 0; right: 0; left: 0; bottom: 0; margin: auto" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <asp:Label ID="lbl_head" runat="server" Text="Contractor Invoice"></asp:Label>
                </h2>
            </div>
            <div class="box-content">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="imgbtn_search">
                    <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                        <tbody>
                            <tr>
                                <td class="center">Contractor Work Order Number
                                </td>
                                <td class="center" colspan="6">
                                    <asp:TextBox ID="txtContractorWorkOrderNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="imgbtn_search" runat="server" CausesValidation="false" ToolTip="Search"
                                        CssClass="Search icon198" OnClick="imgbtn_search_Click"></asp:LinkButton>
                                </td>


                            </tr>
                            <tr>
                                <td>

                                    <asp:Label ID="lblContractor" runat="server" Text="ContractorName"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContractorName" CssClass="TextBox ContractorName" runat="server"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="autoContractorName" runat="server" DelimiterCharacters="" Enabled="true" ServiceMethod="GetContractorName" MinimumPrefixLength="1" EnableCaching="true"
                                        ServicePath="InvoiceControls.ascx" TargetControlID="txtContractorName">
                                    </asp:AutoCompleteExtender>

                                </td>
                                <td>
                                    <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromDate" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtFromDate" PopupButtonID="LinkButton2">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtToDate" PopupButtonID="LinkButton3">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="center">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                                        ToolTip="Search" OnClick="btnSearch_Click" OnClientClick="javascript:return ValidateContractorName()"></asp:Button>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                </asp:Panel>
                <br />
                <asp:Panel ID="Panel1" runat="server" CssClass="mitem">
                    <div style="width: 100%; display: none">
                        <div class="center" style="font-size: 14px; float: left; position: relative; top: 30px; border-right: 1px solid #green; text-align: center; font-weight: bold; vertical-align: middle; width: 25%;">
                            Payment Type
                        </div>
                        <div style="width: 75%; position: relative; float: left; top: 0px;">
                            <asp:GridView ID="gvPaymentType" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                EmptyDataText="No-Items Available" OnRowDataBound="gvPaymentType_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="56px" HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdbtn" runat="server" OnCheckedChanged="rdbtn_Click" onclick="javascript:CheckOtherIsCheckedByGVID(this);"
                                                GroupName="a" AutoPostBack="true" />
                                            <asp:HiddenField ID="hdfPaymentTermId" runat="server" Value='<%# Eval("PaymentTermId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentTypeName" runat="server" Text='<%#Eval("PaymentType.Name") %>'></asp:Label>
                                            <asp:HiddenField ID="hdfPaymentTypeId" runat="server" Value='<%# Eval("PaymentType.Id")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. Of Days" ItemStyle-CssClass="taright">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoOfDays" runat="server" Text='<%#Eval("NumberOfDays") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Percentage Value" ItemStyle-CssClass="taright">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPercenatageValue" runat="server" Text='<%#Eval("PercentageValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentDescription" runat="server" Text='<%#Eval("PaymentDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <table class="table table-bordered table-striped table-condensed">
                        <tr>
                            <td class="center" style="width: 25%">Contractor Work Order Number
                                <asp:HiddenField ID="hdnStatusTypeId" runat="server" />
                            </td>
                            <td style="width: 25%">
                                <asp:Label ID="lblContractorWorkOrderNumber" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnContractorWOId" runat="server" />
                            </td>
                            <td class="center" style="width: 25%">Contractor Name
                                <asp:HiddenField ID="hdnPaymentTermId" runat="server" />
                            </td>
                            <td class="center" style="width: 25%">
                                <asp:Label ID="lblContractorName" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnContractorId" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div>
                        <asp:GridView ID="gvContractorAddItem" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" OnRowDataBound="gvContractorAddItem_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                            OnCheckedChanged="chbxSelectAll_Click" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_Click" />
                                        <asp:HiddenField ID="hdfActivityId" runat="server" Value='<%# Eval("DeliverySchedule.ActivityDescriptionId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityDescription" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                        <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval(" Item.DeliveryScheduleDOM.ServiceDescription") %>'></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make(Brand)" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfUnit" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Per Unint Discount (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPerunitDiscount" runat="server" Text='<%#Eval("PerUnitDiscount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Rate (INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountRate" runat="server" Text='<%#Eval("Discount_Rates") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UnitIssued" HeaderText="Issued Unit" />
                                <asp:TemplateField HeaderText="Per Unit Cost (INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPerUnitCost" runat="server" Text='<%#Eval("PerUnitCost") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Consumed Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssuedItem" runat="server" Text='<%#Eval("ConsumedUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Measurement">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lost Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLostUnit" runat="server" Text='<%#Eval("LostUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoiced Item" HeaderStyle-Width="55px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoicedItem" runat="server" Width="80px" Text='<%#Eval("QuantityReceived") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="Button_align">
                        <asp:Button ID="btnContractorAdd" runat="server" Text="Add" CssClass="button_color action"
                            OnClick="btnContractorAdd_Click" />
                    </div>
                    <br />
                    <table class="table table-bordered table-striped table-condensed searchbg">
                        <tr>
                            <td class="center" style="width: 25%">Contract Number
                            </td>
                            <td class="center" style="width: 25%">
                                <asp:Label ID="lblContractNo" runat="server"></asp:Label>
                            </td>
                            <td class="center" style="width: 25%">Work Order Number
                            </td>
                            <td class="center" style="width: 25%">
                                <asp:Label ID="lblWorkOrderNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="center" style="width: 25%">Total Net Value
                            </td>
                            <td class="center" style="width: 25%">
                                <img alt="" src="../../Images/rupee_symbol5.png" />
                                <asp:Label ID="lblTotalNetValue" runat="server"></asp:Label>
                            </td>
                            <td class="center" style="color: Blue; visibility: hidden;">Invoiced Amount
                            </td>
                            <td class="center" style="color: Blue; visibility: hidden;">
                                <img alt="" src="../../Images/rupee_symbol5.png" />
                                <asp:Label ID="lblInvoicedAmount" runat="server"></asp:Label>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td class="center">Left Amount
                            </td>
                            <td class="center">
                                <img alt="" src="../../Images/rupee_symbol5.png" />
                                <asp:Label ID="lblLeftAmount" runat="server"></asp:Label>
                                &nbsp
                            </td>
                            <td class="center" style="color: Blue; visibility: hidden;">Payable Amount
                            </td>
                            <td class="center" style="color: Blue; visibility: hidden;">
                                <img alt="" id="imgPayableAmt" runat="server" src="~/Images/rupee_symbol5.png" />
                                <asp:Label ID="lblPayableAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="center" style="width: 25%">Invoice Type
                            </td>
                            <td style="width: 25%">
                                <asp:RadioButtonList ID="rbtnInvoiceType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtnInvoiceType_SelectedIndexChanged"
                                    RepeatDirection="Horizontal">
                                    <%--<asp:ListItem Value="0">Advance</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">Normal</asp:ListItem>--%>
                                </asp:RadioButtonList>
                            </td>
                            <td colspan="2" style="width: 50%">
                                <table>
                                    <tr id="excise3" runat="server">
                                        <td>Advance <span style="color: Red">*</span>
                                            <asp:TextBox ID="txtAdvance" runat="server"></asp:TextBox>
                                            %
                                            <asp:RegularExpressionValidator ID="revtxtAdvance" runat="server" ErrorMessage="Advance  allows only numeric values upto 100."
                                                ValidationExpression="([0-9]|[0-9]\d|100)$" ControlToValidate="txtAdvance" SetFocusOnError="true"
                                                ValidationGroup="CI" Display="None" ForeColor="Red"> </asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="rfvtxtAdvance" runat="server" ErrorMessage="Please enter Advance."
                                                ControlToValidate="txtAdvance" ValidationGroup="CI" Display="None" ForeColor="Red"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%--  -------------sundeep--------------%>
                        <tr id="excise1" runat="server">
                            <td class="center">Service Tax <span style="color: Red"></span>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txt_service_tax" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                %
                                <asp:RegularExpressionValidator ID="rev_service_tax" runat="server" ControlToValidate="txt_service_tax"
                                    Display="None" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Service Tax allows only numeric values upto 100."
                                    ValidationGroup="CI" ValidationExpression="([0-9]|[0-9]\d|100)$"></asp:RegularExpressionValidator>
                            </td>
                            <td class="center">VAT
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txt_vat" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                %
                                <asp:RegularExpressionValidator ID="rev_vat" runat="server" ControlToValidate="txt_vat"
                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="CI" ErrorMessage="Vat allows only numeric values upto 100."
                                    ValidationExpression="([0-9]|[0-9]\d|100)$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr id="excise2" runat="server">
                            <td class="center">CST (with C Form)
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txt_cst_with_c_form" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                %
                                <asp:RegularExpressionValidator ID="rev_cst_with_c_form" runat="server" ControlToValidate="txt_cst_with_c_form"
                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="CI" ErrorMessage="CST (with C Form) allows only numeric values upto 100."
                                    ValidationExpression="([0-9]|[0-9]\d|100)$"></asp:RegularExpressionValidator>
                            </td>
                            <td class="center">CST (Without C Form)
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txt_cst_without_c_form" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                %
                                <asp:RegularExpressionValidator ID="rev_cst_without_c_form" runat="server" ControlToValidate="txt_cst_without_c_form"
                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="CI" ErrorMessage="CST (Without C Form) allows only numeric values upto 100."
                                    ValidationExpression="([0-9]|[0-9]\d|100)$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">Freight
                            </td>
                            <td class="center">
                                <img alt="" src="../../Images/rupee_symbol5.png" />
                                <asp:TextBox ID="txt_Freight" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revtxt_Freight" runat="server" ControlToValidate="txt_Freight"
                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="CI" ErrorMessage="Freight allows only numeric values."
                                    ValidationExpression="[0-9]{1,20}"></asp:RegularExpressionValidator>
                            </td>
                            <td class="center">Packaging
                            </td>
                            <td class="center" colspan="3">
                                <img alt="" src="../../Images/rupee_symbol5.png" />
                                <asp:TextBox ID="txt_Packaging" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revtxt_Packaging" runat="server" ControlToValidate="txt_Packaging"
                                    Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="CI" ErrorMessage="Packaging allows only numeric values."
                                    ValidationExpression="[0-9]{1,20}"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" style="width: 25%">Invoice Date
                                    <asp:HiddenField ID="hdnQutnGenDate" runat="server" />
                                </td>
                                <td class="center" style="width: 25%">
                                    <asp:TextBox ID="txtInvoiceDate" CssClass="TextBox" Enabled="false" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LnkButton5" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtInvoiceDate" PopupButtonID="LnkButton5">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="center" style="width: 25%">Remarks
                                </td>
                                <td class="center" style="width: 25%">
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="307px" CausesValidation="false"
                                        onKeyUp="javascript:Count(this,250);" onChange="javascript:Count(this,250);"
                                        CssClass="mlttext" Rows="5" Columns="50" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" style="width: 25%">Contractor Bill Date
                                </td>
                                <td class="center" style="width: 25%">
                                    <asp:TextBox ID="txtBillDate" CssClass="TextBox" Enabled="false" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtBillDate" PopupButtonID="LinkButton1">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="center" style="width: 25%">Contractor Bill Number <span style="color: Red">*</span>
                                </td>
                                <td class="center" style="width: 25%">
                                    <asp:TextBox ID="txtBillNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtBillNumber" runat="server" ErrorMessage="Please Enter Contractor Bill Number."
                                        ControlToValidate="txtBillNumber" ValidationGroup="CI" Display="None" ForeColor="Red"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" style="width: 25%">
                                    <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                                </td>
                                <td class="center" colspan="3" style="width: 75%">
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
                                                                Width="250px">
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
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ValidationSummary ID="vdsContractorInvoice" runat="server" ValidationGroup="CI"
                                        ShowMessageBox="true" ShowSummary="false" ForeColor="Red" />
                                </td>
                                <td align="right" colspan="4">
                                    <div id="excies4" runat="server">
                                        <asp:Button ID="btnAdvance" runat="server" ValidationGroup="CI" CssClass="button_color action green"
                                            Text="Save" OnClick="btnAdvance_Click" />
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <br />
                <br />
                <div style="color: Red;">
                    <asp:Literal ID="ltrl_err_msg" runat="server"></asp:Literal>
                </div>
                <div>
                    <asp:GridView ID="gvAddContractorItem" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No-Items Available" OnRowCommand="gvAddContractorItem_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityDescription" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfActivityId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Level">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make(Brand)" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Per Unit Cost (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPerUnitCost" runat="server" Text='<%#Eval("PerUnitCost") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Per Unit Discount">
                                <ItemTemplate>
                                    <asp:Label ID="lblPerUnitdiscount" runat="server" Text='<%#Eval("PerUnitDiscount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount Rate (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscountRate" runat="server" Text='<%#Eval("Discount_Rates") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Quantity" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Quantity" HeaderStyle-Width="55px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRecieveQuantity" runat="server" MaxLength="10" onkeypress="AllowOnlyNumeric(event);"
                                        Width="40px" Text='<%#Eval("QuantityReceived") %>' OnTextChanged="TextLeave_Click"
                                        AutoPostBack="true"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Tax (%)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_service_tax" runat="server" MaxLength="10" Width="40px" onkeypress="AllowOnlyNumeric(event);"
                                        Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAT (%)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_vat" runat="server" MaxLength="10" Width="40px" onkeypress="AllowOnlyNumeric(event);"
                                        Text='<%#Eval("TaxInformation.VAT") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST (with C Form) (%)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_cst_with_c_form" runat="server" MaxLength="10" Width="40px"
                                        onkeypress="AllowOnlyNumeric(event);" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'
                                        AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST (Without C Form) (%)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_cst_without_c_form" runat="server" MaxLength="10" Width="40px"
                                        onkeypress="AllowOnlyNumeric(event);" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmdDelete"
                                        CommandArgument='<%#Container.DataItemIndex %>' OnClientClick="return confirm('Are you sure you want to delete?') "
                                        ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="Button_align">
                    <asp:Button ID="btnAddInvoice" runat="server" Text="Create Invoice" CssClass="button_color action"
                        OnClick="btnAddInvoice_Click" />
                </div>
                <br />
                <div>
                    <asp:GridView ID="gvContractorInvoice" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No-Items Available" ShowFooter="True" OnRowDataBound="gvContractorInvoice_RowDataBound"
                        OnRowCommand="gvContractorInvoice_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityDescription" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfActivityId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Level">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make(Brand)" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Per Unit Cost (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPerUnitCost" runat="server" Text='<%#Eval("PerUnitCost") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Quantity" HeaderStyle-Width="55px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRecieveQuantity" runat="server" Width="80px" Text='<%#Eval("ItemReceivedInvoice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Tax (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_service_tax" runat="server" Width="40px" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAT (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_vat" runat="server" Width="40px" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST (with C Form) (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_cst_with_c_form" runat="server" Width="40px" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST (Without C Form) (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_cst_without_c_form" runat="server" Width="40px" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Tax (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitForBilled") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalTe" runat="server" CssClass="total" Text="Grand Total (INR)"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total With Tax (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalWithTax" runat="server" Width="80px" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotal" runat="server" CssClass="total"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmdDelete"
                                        CommandArgument='<%#Container.DataItemIndex %>' OnClientClick="return confirm('Are you sure you want to delete?') "
                                        ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div>
                    <asp:GridView ID="gvContractorAdvanceSave" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No-Items Available" ShowFooter="True" OnRowDataBound="gvContractorAdvanceSave_RowDataBound"
                        OnRowCommand="gvContractorAdvanceSave_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Index" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Amount (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Width="40px" Text='<%#Eval("") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Advance (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdvance" runat="server" Width="40px" Text='<%#Eval("AdvanceValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Tax (%)">
                                <ItemTemplate>
                                    <asp:Label ID="txt_service_tax" runat="server" Width="40px" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAT (%)">
                                <ItemTemplate>
                                    <asp:Label ID="txt_vat" runat="server" Width="40px" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST (with C Form) (%)">
                                <ItemTemplate>
                                    <asp:Label ID="txt_cst_with_c_form" runat="server" Width="40px" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST (Without C Form) (%)">
                                <ItemTemplate>
                                    <asp:Label ID="txt_cst_without_c_form" runat="server" Width="40px" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Freight (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblFreight" runat="server" Width="40px" Text='<%#Eval("TaxInformation.Freight") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Packaging (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPackaging" runat="server" Width="40px" Text='<%#Eval("TaxInformation.Packaging") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Tax (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("TaxInformation.TotalTax") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTtotaltext" runat="server" CssClass="total" Text="Grand Total (INR)"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalWithTax" runat="server" Width="80px" Text='<%#Eval("TaxInformation.TotalNetValue") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotal" runat="server" CssClass="total"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmdDelete"
                                        CommandArgument='<%#Container.DataItemIndex %>' OnClientClick="return confirm('Are you sure you want to delete?') "
                                        ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                    CommandName="Select" />
                <%-- for popup Search the Issue material Genreated Contractoe Work Order Number Gridview Name=gvIssueMaterialNo--%>
                <div style="position: absolute; top: 3000px;">
                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                        PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                        Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button1">
                    </cc1:ModalPopupExtender>
                    <div>
                        <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                            <div class="PopUpClose">
                                <div class="btnclosepopup">
                                    <asp:Button ID="Button1" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                                </div>
                            </div>
                            <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                                <div>
                                    <asp:GridView ID="gvIssueMaterialNo" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="10"
                                        EmptyDataText="No Record Found(s)" OnPageIndexChanging="gvIssueMaterialNo_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged"
                                                        AutoPostBack="true"></asp:RadioButton>
                                                    <asp:HiddenField ID="hdfIssuematerialId" runat="server" Value='<%# Eval("IssueMaterialId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contractor Work Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContractorWONo" runat="server" Text='<%#Eval("DemandVoucher.Quotation.ContractQuotationNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contractor Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContractorName" runat="server" Text='<%#Eval("DemandVoucher.Quotation.ContractorName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contract No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContractNo" runat="server" Text='<%#Eval("DemandVoucher.Quotation.ContractNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issue Material Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIssueDate" runat="server" Text='<%#Eval("IssueMaterialDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>
                <div class="Button_align">
                    <asp:Button ID="btnReset" runat="server" Text=" Clear " CssClass="button_color action red"
                        OnClick="btnReset_Click" />
                    <asp:Button ID="btnSaveDraft" runat="server" Text="Submit Draft" CssClass="button_color action green"
                        OnClick="btnSaveDraft_Click" />
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_upload" />
        <asp:AsyncPostBackTrigger ControlID="imgbtn_search" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
