<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="SupplierInvoiceApproval.aspx.cs" Inherits="MiniERP.Invoice.SupplierInvoiceApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
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
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Supplier Invoice Approval</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center">
                                    Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" Width="147px" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <div>
                            <asp:Label ID="lbl_error_msg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <asp:GridView ID="gvSupplierInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                            OnPageIndexChanging="gvSupplierInvoice_PageIndexChanging" OnRowDataBound="gvSupplierInvoice_RowDataBound"
                            OnRowCommand="gvSupplierInvoice_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbx_select_all" runat="server" Text="Select" AutoPostBack="true"
                                            OnCheckedChanged="oncheck_uncheck_all" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbx_Invoice" runat="server" AutoPostBack="true" OnCheckedChanged="on_check_uncheck_quotation" />
                                        <asp:HiddenField ID="hdf_Invoice_id" runat="server" Value='<%#Eval("SupplierInvoiceId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Number">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lbtnInvoice" runat="server" CommandName="lnkQuotation" CommandArgument='<%#Eval("SupplierInvoiceId") %>'
                                            Text='<%#Eval("InvoiceNumber") %>'></asp:Label>--%>
                                        <asp:LinkButton ID="lnkbtnInvoice" runat="server" CommandName="lnkInvoice" CommandArgument='<%#Eval("SupplierInvoiceId") %>'
                                            Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("ReceiveMaterial.Quotation.UploadDocumentId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvDate" runat="server" Text='<%#Eval("InvoiceDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.SupplierName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Purchase Order No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierPONo" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.SupplierQuotationNumber") %>'></asp:Label>
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
                                        <asp:Label ID="lblInvoiceTypeId" runat="server" Text='<%#Eval("InvoiceType.Id") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Freight (INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.Freight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packaging (INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPackaging" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.Packaging") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PayableAmount" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" Visible="false" />
                                <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                <asp:TextBox ID="txtRemarkReject" runat="server" MaxLength="50"  TextMode="MultiLine" Width="100" Text='<%#Eval("RemarkReject") %>'></asp:TextBox>
                                
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusType" runat="server" Text='<%#Eval("ReceiveMaterial.Quotation.StatusType.Name") %>'></asp:Label>
                                        <asp:HiddenField ID="hdf_status_id" runat="server" Value='<%#Eval("ReceiveMaterial.Quotation.StatusType.Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="Button_align" style="margin-left: 80px">
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button_color action"
                            CommandName="Reject" OnClientClick="return confirm('Are You Sure To Reject?') " CommandArgument="r" OnCommand="btn_Approve_Reject_Click" />
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button_color action gray"
                            CommandName="Approve" OnClientClick="return confirm('Are You Sure To Approve?') " CommandArgument="a" OnCommand="btn_Approve_Reject_Click" />
                    </div>
                    <asp:Button ID="btnPopUp1" runat="server" BackColor="#f8f8f8" BorderStyle="None"
                        BorderWidth="0px" CommandName="Select" />
                </div>
            </div>
            <div style="position: absolute; top: 1000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp1"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button1">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                            <div class="btnclosepopup">
                                <asp:Button ID="Button1" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
                        </div>
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                            <div class="box-header well">
                                <h2>
                                    Attached Documents
                                </h2>
                            </div>
                            <div>
                                <udc:UploadDocuments runat="server" ID="updcFile" />
                            </div>
                            <div class="box-content">
                                <div class="box-header well">
                                    <h2>
                                        Supplier Invoice</h2>
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
                                <div>
                                    <asp:GridView ID="gvInvoiceItems" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr"  ShowFooter="true" 
                                        PageSize="10" AlternatingRowStyle-CssClass="alt" 
                                        onrowdatabound="gvInvoiceItems_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Speci- fication">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCatg" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR)" />
                                            <asp:BoundField DataField="ActualNumberofUnit" HeaderText="Per Uni Discount" />
                                            <asp:BoundField DataField="Discount_Rates" HeaderText="Discount Rate (INR)" />
                                            <asp:BoundField DataField="UnitForBilled" HeaderText="Item Billed" />
                                            <asp:TemplateField HeaderText="Discount Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiscountType" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Excise Duty (%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExciseDuty" runat="server" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Tax (%)" ItemStyle-CssClass="taright">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceTax" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VAT (%)" ItemStyle-CssClass="taright">
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



                                           <%-- <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount(INR)" ItemStyle-CssClass="taright" />--%>
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
