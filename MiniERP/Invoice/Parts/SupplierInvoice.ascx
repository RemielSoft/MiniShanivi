<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierInvoice.ascx.cs"
    Inherits="MiniERP.Invoice.Parts.SupplierInvoice" ViewStateMode="Enabled" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div>
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/ajax_loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="width:40px; height:40px; position:fixed; top:0; right:0; left:0; bottom:0; margin:auto" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
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
                    function ValidateSupplierName() {
                        debugger
                        var supplierName = document.getElementById("ContentPlaceHolderMain_Supplier_Invoice_txtName")//ContentPlaceHolderMain_Invoice_Controls_txtContractorName
                        var frmDate = document.getElementById("ContentPlaceHolderMain_Supplier_Invoice_txtFromDate")
                        var toDate = document.getElementById("ContentPlaceHolderMain_Supplier_Invoice_txtToDate")
                        if (supplierName.value != "") {
                            //alert("Please enter Contractor Name");
                            return true;
                        }
                        else if (frmDate.value != "" && toDate.value != "") {

                            return true;
                        }
                        else if (frmDate.value != "" && toDate.value == "" || frmDate.value == "" && toDate.value != "") {
                            alert("Please Enter From Date Or To Dates..");
                            return false;
                        }
                        else if (frmDate.value == "" && toDate.value == "" && supplierName.value == "") {
       
                            alert("Please Enter Supplier Name Or Dates..");
                            return false;
                        }
                            else {
                                return true;
                            }
                        }
                </script>
            </div>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        <asp:Label ID="lbl_head" runat="server" Text="Supplier Invoice"></asp:Label>
                    </h2>
                </div>
                <div class="box-content">
                    <asp:Panel ID="pnlSearch" runat="server">
                        <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                            <tbody>
                                <tr>
                                    <td class="center">Supplier Purchase Order Number
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtSupplierPONumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtn_search" runat="server" ToolTip="Search" CssClass="Search icon198"
                                            OnClick="imgbtn_search_Click"></asp:LinkButton>
                                    </td>
                                    <td class="center">Supplier Name
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtName" CssClass="TextBox" runat="server"></asp:TextBox>
                                   </td>
                                    
                                     <td class="center" style="vertical-align: middle; text-align: center;" rowspan="2">
                                        
                                         
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                                            ToolTip="Search" OnClick="btnSearch_Click" OnClientClick="return ValidateSupplierName();"></asp:Button>
                                           
                                         
                                            
                                    </td>
                                </tr>
                                <tr>
                                    <td>From Date
                                         </td>
                                        <td class="center">
                                        <asp:TextBox ID="txtFromDate" CssClass="TextBox" Enabled="false" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="LinkButton2">
                                        </asp:CalendarExtender>
                                    </td>
                                   <td>To Date
                                         </td>
                                        <td class="center">
                                        <asp:TextBox ID="txtToDate" CssClass="TextBox" Enabled="false" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="LinkButton3">
                                        </asp:CalendarExtender>
                                    </td>
                                   
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                    <br />
                    <table class="table table-bordered table-striped table-condensed searchbg">
                        <tr id="SPON" runat="server">
                            <td class="center" style="width: 25%">Supplier Purchase Order Number
                                <asp:HiddenField ID="hdnStatusTypeId" runat="server" />
                            </td>
                            <td style="width: 25%">
                                <asp:Label ID="lblSupplierPurchaseOrderNumber" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnSupplierPOId" runat="server" />
                            </td>
                            <td class="center" style="width: 25%">Supplier Name
                                <asp:HiddenField ID="hdnPaymentTermId" runat="server" />
                            </td>
                            <td class="center" style="width: 25%">
                                <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnSupplierId" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:GridView ID="gvSupplierAdd" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" OnRowDataBound="gvSupplierAdd_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                            OnCheckedChanged="chbxSelectAll_Click" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_Click" />
                                        <asp:HiddenField ID="hdfSupplierPOMappingId" runat="server" Value='<%# Eval("Item.ItemId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnfldSupplierPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
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
                                <asp:TemplateField HeaderText="Per Unit Discount (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPerUnitDiscount" runat="server" Text='<%#Eval("PerUnitDiscount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Measurement">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recieved Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecItem" runat="server" Text='<%#Eval("QuantityReceived") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoiced Item" HeaderStyle-Width="55px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecieveQuantity" runat="server" Width="80px" Text='<%#Eval("BilledUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="Button_align">
                        <asp:Button ID="btnAddSupplierItem" runat="server" Text="Add" CssClass="button_color action"
                            OnClick="btnAddSupplierItem_Click" />
                    </div>
                    <br />
                    <br />
                    <asp:Panel ID="Panel1" runat="server" CssClass="mitem">
                        <div style="width: 100%; display: none;">
                            <div class="center" style="font-size: 14px; float: left; position: relative; top: 30px; border-right: 1px solid #green; text-align: center; font-weight: bold; vertical-align: middle; width: 25%;">
                                Payment Invoice Type
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
                                                <%--  <asp:Label ID="lblNormalDays" runat="server" Text='<% Eval("NumberOfDays") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Percentage Value" ItemStyle-CssClass="taright">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="lblPercenatageValue" runat="server" Text='<%#Eval("PercentageValue") %>'></asp:Label>--%>
                                                <asp:TextBox ID="txtPercenatageValue" runat="server" Text='<%# Eval("PercentageValue") %>'></asp:TextBox>
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
                            <caption>
                                <br />
                                <tr>
                                    <td class="center" style="width: 25%">Total Order Value
                                    </td>
                                    <td class="center" style="width: 25%">
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                        <asp:Label ID="lblTotalNetValue" runat="server"></asp:Label>
                                    </td>
                                    <td class="center" style="width: 25%">Paid Amount
                                    </td>
                                    <td class="center" style="width: 25%">
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                        <asp:Label ID="lblInvoicedAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">Pending Amount
                                    </td>
                                    <td class="center">
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                        <asp:Label ID="lblLeftAmount" runat="server"></asp:Label>
                                    </td>
                                    <td class="center" style="color: Blue;">
                                        <asp:Label ID="Label1" runat="server" Visible="false" Text="Invoice Amount"></asp:Label>
                                    </td>
                                    <td class="center" style="color: Blue;">
                                        <img alt="" id="imgPayableAmt" visible="false" runat="server" src="../../Images/rupee_symbol5.png" />
                                        <asp:Label ID="lblPayableAmount" Visible="false" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" style="width: 25%">Invoice Type
                                    </td>
                                    <td style="width: 25%">
                                        <asp:RadioButtonList ID="rbtnInvoiceType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtnInvoiceType_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                            <%--<asp:ListItem Value="1">Advance</asp:ListItem>
                                            <asp:ListItem Value="2" Selected="True">Normal</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td colspan="2" style="width: 50%">
                                        <table>
                                            <tr id="excise3" runat="server">
                                                <td>
                                                    <span style="color: Red">*</span>
                                                    <asp:TextBox ID="txtAdvance" runat="server"></asp:TextBox>
                                                    %
                                                    <asp:RangeValidator ID="rvAdvance" runat="server" ControlToValidate="txtAdvance"
                                                        ForeColor="Red" MaximumValue="100" MinimumValue="1" ValidationGroup="temp" ErrorMessage="Enter value between 1 to 100 in Advance."
                                                        SetFocusOnError="True" Display="None" Type="Double"></asp:RangeValidator>
                                                    <asp:RequiredFieldValidator ID="rfvAdvance" runat="server" ControlToValidate="txtAdvance"
                                                        SetFocusOnError="false" Display="None" ErrorMessage="Please enter percentage advance amount"
                                                        ValidationGroup="temp"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <%--  -------------sundeep--------------%>
                                <tr id="excise" runat="server">
                                    <td class="center">Excise Duty
                                    </td>
                                    <td class="center" colspan="3">
                                        <asp:TextBox ID="txtExciseDuty" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                        %
                                        <asp:RangeValidator ID="rv_Excise" runat="server" ControlToValidate="txtExciseDuty"
                                            ForeColor="Red" MaximumValue="100" MinimumValue="1" ValidationGroup="temp" ErrorMessage="Enter value between 1 to 100 in Excise Duty."
                                            SetFocusOnError="True" Display="None" Type="Double"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr id="excise1" runat="server">
                                    <td class="center">Service Tax
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txt_service_tax" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                        %
                                        <asp:RangeValidator ID="rv_ServiceTax" runat="server" ControlToValidate="txt_service_tax"
                                            ForeColor="Red" MaximumValue="100" MinimumValue="0" ValidationGroup="temp" ErrorMessage="Enter value between 1 to 100 in Service Tax."
                                            SetFocusOnError="True" Display="None" Type="Double"></asp:RangeValidator>
                                    </td>
                                    <td class="center">VAT
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txt_vat" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                        %
                                        <asp:RangeValidator ID="rv_Vat" runat="server" ControlToValidate="txt_vat" ForeColor="Red"
                                            MaximumValue="100" MinimumValue="1" ValidationGroup="temp" ErrorMessage="Enter value between 1 to 100 in Vat."
                                            SetFocusOnError="True" Display="None" Type="Double"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr id="excise2" runat="server">
                                    <td class="center">CST (with C Form)
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txt_cst_with_c_form" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                        %
                                        <asp:RangeValidator ID="rv_Cst_with" runat="server" ControlToValidate="txt_cst_with_c_form"
                                            ForeColor="Red" MaximumValue="100" MinimumValue="1" ValidationGroup="temp" ErrorMessage="Enter value between 1 to 100 in CST (with C Form)."
                                            SetFocusOnError="True" Display="None" Type="Double"></asp:RangeValidator>
                                    </td>
                                    <td class="center">CST (Without C Form)
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txt_cst_without_c_form" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                        %
                                        <asp:RangeValidator ID="rv_Cst_without" runat="server" ControlToValidate="txt_cst_without_c_form"
                                            ForeColor="Red" MaximumValue="100" MinimumValue="1" ValidationGroup="temp" ErrorMessage="Enter value between 1 to 100 in CST (Without C Form)."
                                            SetFocusOnError="True" Display="None" Type="Double"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">Freight
                                    </td>
                                    <td class="center">
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                        <asp:TextBox ID="txt_Freight" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revtxt_Freight" runat="server" ControlToValidate="txt_Freight"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="temp"
                                            ErrorMessage="Plese Enter the NUmeric Value in Freight" ValidationExpression="[0-9]{1,20}"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">Packaging
                                    </td>
                                    <td class="center" colspan="3">
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                        <asp:TextBox ID="txt_Packaging" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revtxt_Packaging" runat="server" ControlToValidate="txt_Packaging"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="temp"
                                            ErrorMessage="Plese Enter the NUmeric Value in Packaging" ValidationExpression="[0-9]{1,20}"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </caption>
                        </table>
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center" style="width: 25%">Invoice Date
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
                                        <asp:TextBox ID="txtRemarks" runat="server" MaxLength="10" CssClass="mlttext" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" style="width: 25%">Supplier Bill Date
                                    </td>
                                    <td class="center" style="width: 25%">
                                        <asp:TextBox ID="txtBillDate" CssClass="TextBox" Enabled="false" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtBillDate" PopupButtonID="LinkButton1">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="center" style="width: 25%">Suppilier Bill Number <span style="color: Red">*</span>
                                    </td>
                                    <td class="center" style="width: 25%">
                                        <asp:TextBox ID="txtBillNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtBillNumber" runat="server" ErrorMessage="Please Enter Supplier Bill Number."
                                            ControlToValidate="txtBillNumber" ValidationGroup="temp" Display="None" ForeColor="Red"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" style="width: 25%">
                                        <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                                        <asp:HiddenField ID="hdnQutnGenDate" runat="server" />
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
                                                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="True" ShowSummary="False"
                            ValidationGroup="temp" runat="server" />
                        <div id="excies4" runat="server">
                            <asp:Button ID="btnAdvance" runat="server" CssClass="button_color action green" Text="Save"
                                OnClick="btnAdvance_Click" ValidationGroup="temp" />
                        </div>
                        <br />
                        <br />
                    </asp:Panel>
                    <br />
                    <div>
                        <asp:Literal ID="ltrl_err_msg" runat="server"></asp:Literal>
                    </div>
                    <div>
                        <asp:GridView ID="gvAddItems" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" OnRowCommand="gvAddItems_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Index">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnfldSupplierPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
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
                                        <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Item Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("ItemReceivedQuality") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Percentage Discount (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPercentageDicount" runat="server" Text='<%#Eval("PerUnitDiscount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Quantity" HeaderStyle-Width="55px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRecieveQuantity" runat="server" MaxLength="10" onkeypress="AllowOnlyNumeric(event);"
                                            Width="40px" Text='<%#Eval("UnitLeft") %>' AutoPostBack="true" OnTextChanged="TextLeave_Click"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Excise Duty (%)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_excise_duty" runat="server" MaxLength="10" Width="40px" onkeypress="AllowOnlyNumeric(event);"
                                            Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:TextBox>
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
                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete?') "
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
                        <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" OnRowDataBound="gvInvoice_RowDataBound" ShowFooter="True"
                            OnRowCommand="gvInvoice_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Index" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnfldSupplierPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
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
                                <asp:TemplateField HeaderText="Make(Brand)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Per Unit Cost (INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPerUnitCost" runat="server" Text='<%#Eval("PerUnitCost") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtFreight" Visible="false" runat="server" Width="50px" Text='<%#Eval("TaxInformation.Freight") %>'></asp:TextBox>
                                    </FooterTemplate>
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
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtPackaging" Visible="false" runat="server" Width="50px" Text='<%#Eval("TaxInformation.Packaging") %>'></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Percentage Discount (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPercentageDicount" runat="server" MaxLength="10" Width="40px" onkeypress="AllowOnlyNumeric(event);"
                                            Text='<%#Eval("TaxInformation.PercentageQuty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Excise Duty (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_excise_duty" runat="server" Width="40px" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Total Tax (INR)" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitForBilled") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" CssClass="total" Text="Total Grand (INR)"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount (INR)">
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
                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure to delete?') "
                                            ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div>
                        <asp:GridView ID="gvAdvanceSave" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" OnRowDataBound="gvAdvanceSave_RowDataBound"
                            ShowFooter="True" OnRowCommand="gvAdvanceSave_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue" runat="server" Text='<%#Eval("TotalAmount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Advance (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue" runat="server" Text='<%#Eval("AdvanceValue")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Excise Duty (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_excise_duty" runat="server" Width="40px" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Freight (INR)">
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
                                        <asp:Label ID="lblTotalTe" runat="server" CssClass="total" Text="Grand Total (INR)"></asp:Label>
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
                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure to delete?') "
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
                            Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button8">
                        </cc1:ModalPopupExtender>
                        <div>
                            <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                                <div class="PopUpClose">
                                    <div class="btnclosepopup">
                                        <asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                                    </div>
                                </div>
                                <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                                    <div>
                                        <asp:GridView ID="gvRSM" runat="server" CssClass="mGrid" AllowPaging="false" AutoGenerateColumns="false"
                                            EmptyDataText="No Record Found(s)" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged"
                                                            AutoPostBack="true"></asp:RadioButton>
                                                        <asp:HiddenField ID="hdfReceivematerialId" runat="server" Value='<%# Eval("SupplierRecieveMatarialId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receive Material Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lnkSRMNo" runat="server" CommandArgument='<%#Eval("SupplierRecieveMatarialId") %>'
                                                            Text='<%#Eval("SupplierRecieveMaterialNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purchase Order Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRMNo" runat="server" Text='<%#Eval("Quotation.SupplierQuotationNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Challan Number" ItemStyle-CssClass="taright">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChallanNo" runat="server" Text='<%#Eval("DeliveryChallanNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receive Material Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecieveDate" runat="server" Text='<%#Eval("RecieveMaterialDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--  <asp:GridView ID="gvIssueMaterialNo" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found(s)" 
                                        onpageindexchanging="gvIssueMaterialNo_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged" AutoPostBack="true">
                                                    </asp:RadioButton>
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
                                          <%--  <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("DemandVoucher.Remarks") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("DemandVoucher.Quotation.StatusType.Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns> </asp:GridView><%----%>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="Button_align">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button_color action red"
                            OnClick="btnReset_Click" />
                        <asp:Button ID="btnSaveDraft" runat="server" Text="Submit Draft" CssClass="button_color action green"
                            OnClick="btnSaveDraft_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_upload" />
        </Triggers>
    </asp:UpdatePanel>
</div>
