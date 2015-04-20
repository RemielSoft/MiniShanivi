<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceApproval.ascx.cs"
    Inherits="MiniERP.Invoice.Parts.InvoiceApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<div class="box span12">
    <div class="box-header well">
        <h2>
            Contractor Invoice Approval</h2>
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
                            OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CausesValidation="false">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <div>
            <div>
                <asp:Label ID="lbl_error_msg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                OnPageIndexChanging="gvInvoice_PageIndexChanging" OnRowCommand="gvInvoice_RowCommand"
                OnRowDataBound="gvInvoice_RowDataBound">
                <columns>
                    <asp:TemplateField HeaderStyle-Width="56px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chbx_select_all" runat="server" Text="Select" AutoPostBack="true"
                                OnCheckedChanged="oncheck_uncheck_all" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chbx_Invoice" runat="server" AutoPostBack="true" OnCheckedChanged="on_check_uncheck_quotation" />
                            <asp:HiddenField ID="hdf_Invoice_id" runat="server" Value='<%#Eval("ContractorInvoiceId")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Number">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnInvoice" runat="server" CommandName="lnkQuotation" CommandArgument='<%#Eval("ContractorInvoiceId") %>'
                                Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>
                            <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("IssueMaterial.DemandVoucher.Quotation.UploadDocumentId") %>' />
                            <%--<asp:Label ID="lbtnInvoice" runat="server" CommandName="lnkQuotation" CommandArgument='<%#Eval("ContractorInvoiceId") %>'
                                Text='<%#Eval("InvoiceNumber") %>'></asp:Label>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Date">
                        <ItemTemplate>
                            <asp:Label ID="lblInvDate" runat="server" Text='<%#Eval("InvoiceDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contractor Name">
                        <ItemTemplate>
                            <asp:Label ID="lblContName" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contractor Work Order No.">
                        <ItemTemplate>
                            <asp:Label ID="lblContQuotNo" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Type">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceTypeName" runat="server" Text='<%#Eval("InvoiceType.Name") %>'></asp:Label>
                            <asp:HiddenField ID="hdfInvoiceTypeId" runat="server" Value='<%#Eval("InvoiceType.Id") %>' />
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

                    <asp:TemplateField HeaderText="Freight (INR)" ItemStyle-CssClass="taright">
                        <ItemTemplate>
                            <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.Freight") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Packaging (INR)" ItemStyle-CssClass="taright">
                        <ItemTemplate>
                            <asp:Label ID="lblPackaging" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.Packaging") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PayableAmount" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"  Visible="true"/>
                    <asp:TemplateField HeaderText="Approval Remark">
                    <ItemTemplate>
                    <asp:TextBox ID="txtRmarkReject" runat="server" MaxLength="50"  TextMode="MultiLine" Width="100" Text='<%#Eval("RemarkReject") %>'></asp:TextBox>
                    </ItemTemplate>
                    
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusType" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.StatusType.Name") %>'></asp:Label>
                            <asp:HiddenField ID="hdf_status_id" runat="server" Value='<%#Eval("IssueMaterial.DemandVoucher.Quotation.StatusType.Id")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </columns>
            </asp:GridView>
        </div>
        <div class="Button_align" style="margin-left: 80px">
            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button_color action"
                CommandName="Reject" OnClientClick="return confirm('Are You Sure To Reject?') "
                CommandArgument="r" OnCommand="btn_Approve_Reject_Click" />
            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button_color action gray"
                CommandName="Approve" OnClientClick="return confirm('Are You Sure To Approve?') "
                CommandArgument="a" OnCommand="btn_Approve_Reject_Click" />
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
                            Contractor Invoice</h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        <b>Contractor Name</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblContName" runat="server"></asp:Label>
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
                                        <b>Contractor Work Oder No.</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblcontractorWONo" runat="server"></asp:Label>
                                    </td>
                                    <%--   <td class="center">
                                        <b>Total Amount (INR).</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTAmount" runat="server"></asp:Label>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="gvInvoicePopupItem" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            ShowFooter="true" OnRowDataBound="gvInvoicePopupItem_RowDataBound">
                            <columns>
                                <asp:TemplateField HeaderText="Activity Des- cription" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityDesc" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Item Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCatg" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NumberOfUnit" Visible="false" HeaderText="Number Of Unit"
                                    ItemStyle-CssClass="taright" />
                                <asp:BoundField DataField="UnitForBilled" HeaderText="Item Billed" />
                                <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR)" />
                                <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount" />
                                <asp:BoundField DataField="Discount_Rates" HeaderText="Discount Rate(INR)" />
                               
                                
                                <asp:TemplateField HeaderText="Discount Type" visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountType" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name")%>'></asp:Label>
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
                            </columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</div>
