<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ViewSupplierInvoice.aspx.cs" Inherits="MiniERP.Invoice.ViewSupplierInvoice" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        View Supplier Invoice
                    </h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="Center">
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                                <td class="Center">
                                    <asp:DropDownList ID="ddlSupplierName" CssClass="dropdown" runat="server">
                                    </asp:DropDownList>
                                </td>

                                <td class="Center">
                                    Supplier Purchase Order
                                </td>
                                <td class="Center">
                                    <asp:TextBox ID="txtSupplierPONo" CssClass="TextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Center">
                                    From Date
                                </td>
                                <td class="Center">
                                    <asp:TextBox ID="txtFromDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="LnkFromDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtFromDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="LnkFromDate"
                                        TargetControlID="txtFromDate">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="lnkbtnClear" runat="server" OnClick="LinkbtnClear_Click" ToolTip="Clear"
                                        CssClass="Search icon188"></asp:LinkButton>
                                </td>
                                <td class="centre">
                                    To Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtToDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="LnkToDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="LnkToDate"
                                        TargetControlID="txtToDate">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="lnkbuttonClear" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="lnkbuttonClear_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="button_color  go" OnClick="btnSearch_Click1" />
                    </div>
                    <br />
                    <br />
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center">
                                    Invoice Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvoiceNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                        ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:GridView ID="gvSupplierInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="30" EmptyDataText="No Record Found(s)"
                            OnRowCommand="gvSupplierInvoice_RowCommand" OnRowDataBound="gvSupplierInvoice_RowDataBound"
                            OnPageIndexChanging="gvSupplierInvoice_PageIndexChanging1">
                            <Columns>
                                <asp:TemplateField HeaderText="Invoice No" ControlStyle-Width="100px">
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lnkbtnInvoice1" runat="server" CommandName="lnkInvoice1" CommandArgument='<%#Eval("SupplierInvoiceId") %>'
                                            Text='<%#Eval("InvoiceNumber") %>'></asp:Label>--%>
                                        <asp:LinkButton ID="lnkbtnInvoice1" runat="server" CommandName="lnkInvoice1" CommandArgument='<%#Eval("SupplierInvoiceId") %>'
                                            Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("ReceiveMaterial.Quotation.UploadDocumentId") %>' />
                                        <asp:HiddenField ID="hdfStatusId1" runat="server" Value='<%#Eval("ReceiveMaterial.Quotation.StatusType.Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceDate1" runat="server" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnInvoiceType" runat="server" Value='<%#Eval("InvoiceType.Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConName1" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.SupplierName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Purchase Order No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConNo1" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.SupplierQuotationNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bill Date">
                    <ItemTemplate>
                            <asp:Label ID="lblBillDate" runat="server" Text='<%#Eval("BillDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill Number">
                    <ItemTemplate>
                    <asp:Label ID="lblBillNumber" runat="server" Text='<%#Eval("BillNumber") %>'></asp:Label>
                    </ItemTemplate>
                    
                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceTypeName" runat="server" Text='<%#Eval("InvoiceType.Name") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfInvoiceTypeId" runat="server" Value='<%#Eval("InvoiceType.Id") %>' />
                                        <%--<asp:Label ID="lblInvoiceTypeId" runat="server" Text='<%#Eval("InvoiceType.Id") %>' Visible="false"></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaybleAmt" runat="server" Text='<%#Eval("PayableAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Freight (INR)" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.Freight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packaging (INR)" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPackaging" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.Packaging") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblremarks1" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remark" ControlStyle-Width="100px">
                                <ItemTemplate>
                                <asp:Label ID="lblRemarkReject" runat="server" Text='<%#Eval("RemarkReject") %>'></asp:Label>
                                
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus1" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.StatusType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ControlStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit1" Visible="false" runat="server" CommandName="lnkEdit1"
                                            CommandArgument='<%#Eval("SupplierInvoiceId") %>' CssClass="button icon145 "
                                            ToolTip="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete1" runat="server" CommandName="lnkDelete1" CommandArgument='<%#Eval("SupplierInvoiceId") %>'
                                            CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure To Delete?') "></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnPrint1" runat="server" CommandName="lnkPrint1" CommandArgument='<%#Eval("SupplierInvoiceId")+","+Eval("InvoiceNumber") %>'
                                            CssClass="button icon153 " ToolTip="Print"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnGenerate1" runat="server" CommandName="lnkGenerate1" CommandArgument='<%#Eval("SupplierInvoiceId") %>'
                                            CssClass="button icon189 " ToolTip="Generate Invoice" OnClientClick="return confirm('Are You Sure To Generate This Invoice?') "></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created By" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="Button_align">
                            <asp:Button ID="LnkBtnExport" runat="server" Text="Export To Excel" CssClass="button_color action green"
                                OnClick="LnkBtnExport_Click" />
                        </div>
                        <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                            CommandName="Select" />
                    </div>
                </div>
            </div>
            <div style="position: absolute; top: 1000px;">
                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button3">
                </asp:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                            <div class="btnclosepopup">
                                <asp:Button ID="Button3" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
                        </div>
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                            <div class="box-content">
                                <div class="box-header well">
                                    <h2>
                                        Attached Documents
                                    </h2>
                                </div>
                                <div>
                                    <udc:UploadDocuments runat="server" ID="updcFile" />
                                </div>
                                <div class="box-header well">
                                    <h2>
                                        <asp:Label ID="lbl_Invoice" runat="server"></asp:Label>
                                    </h2>
                                </div>
                                <div class="box-content">
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td class="center">
                                                    <b>Supplier Name</b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSupplrName" runat="server"></asp:Label>
                                                </td>
                                                <td class="center">
                                                    <b>Invoice Type</b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblInvoiceT" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="center">
                                                    <b>Supplier PO No.</b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSuppPONo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="mGriditem">
                                    <asp:GridView ID="gvItemInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" ShowFooter="true" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No-Items Available" OnRowDataBound="gvItemInfo_RowDataBound">
                                        <Columns>
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
                                            <asp:TemplateField HeaderText="Category level">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR) " />
                                            <asp:BoundField DataField="ActualNumberofUnit" HeaderText="Per Unit Discount" />
                                            <asp:BoundField DataField="Discount_Rates" HeaderText="Discount Rate (INR) " />
                                            <asp:BoundField DataField="UnitForBilled" HeaderText="Item Billed" />
                                            <asp:TemplateField HeaderText="Excise Duty (%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExciseDuty" runat="server" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Tax (%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceTax" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VAT (%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVAT" runat="server" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CST (With C Form) (%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCSTWithFrom" runat="server" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CST (Without C Form) (%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCSTWithoutForm" runat="server" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGrandTotal" runat="server" CssClass="total" Text="Grand Total (INR)"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount (INR)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" CssClass="total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkBtnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
