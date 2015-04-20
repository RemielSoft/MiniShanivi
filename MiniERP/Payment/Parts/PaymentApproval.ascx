<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentApproval.ascx.cs"
    Inherits="MiniERP.Parts.PaymentApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:UpdatePanel ID="upnlPayment" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <asp:Label ID="lbl_quotaion_approval" runat="server"></asp:Label>
                </h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center" width="20%">
                                Status
                            </td>
                            <td width="80%">
                                <asp:DropDownList ID="ddlStatus" CssClass="dropdown" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlStatusSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div>
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td colspan="4" class="center" style="font-size: 13px; color: #000; background-color: #ececec;
                                    font-weight: bold;">
                                    Payment Detail
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:GridView ID="gvInvoiceDetail" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Record Found !" OnRowCommand="gvInvoiceDetail_RowCommand" OnPageIndexChanging="gvInvoiceDetail_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                            OnCheckedChanged="chbxSelectAll" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbxPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chbxSelect" />
                                        <asp:HiddenField ID="hdfPaymentId" runat="server" Value='<%#Eval("PaymentId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Number">
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("InvoiceNumber") %>'></asp:Label>--%>
                                        <asp:LinkButton ID="lbtnInvoiceNo" runat="server" CommandName="lnkInvoiceNo" CommandArgument='<%#Eval("InvoiceNumber") %>'
                                            Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("UploadedDocument") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PaymentDate" DataFormatString="{0:MMM d, yyyy}" HeaderText="Payment Date" />
                            <%--    hghgfjhgj--%>
                                <asp:TemplateField HeaderText="QuotationNumber">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblHQuotation" runat="server"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuotation" runat="server" Text='<%#Eval("QuotationNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ContractorSupplierName">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblHName" runat="server"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCName" runat="server" Text='<%#Eval("ContractorSupplierName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractNO" runat="server" Text='<%#Eval("ContractNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Order Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkOrderNumber" runat="server" Text='<%#Eval("WorkOrderNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Remark" HeaderText="Pament Remark" />
                                <asp:TemplateField HeaderText="Payment Mode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentType" runat="server" Text='<%#Eval("PaymentModeType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                                  <asp:BoundField DataField="BillNumber" HeaderText="Bill Number" />
                                    <asp:BoundField DataField="TDS" HeaderText="TDS (%)" />
                                      <asp:BoundField DataField="TDSWithPayment" HeaderText="TDS With Amount (INR)" />
                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Reference Number" ItemStyle-CssClass="taright" />
                                <asp:TemplateField ItemStyle-CssClass="taright">
                                    <HeaderTemplate>
                                        Payment Amount (INR)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentAmount" runat="server" Text='<%#Eval("PaymentAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                <asp:TextBox ID="txtRemarkReject" runat="server" MaxLength="50" Width="100" TextMode="MultiLine" Text='<%#Eval("RemarkReject") %>'></asp:TextBox>
                                </ItemTemplate>
                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ApprovalStatusType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="Button_align">
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button_color action"
                                CommandName="Reject" OnClientClick="return confirm('Are You Sure To Reject?') " OnCommand="btnApproveReject_Click" />
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button_color action gray"
                                CommandName="Approve" OnClientClick="return confirm('Are You Sure To Approve?') " OnCommand="btnApproveReject_Click" />
                        </div>
                    </div>
                    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                        CommandName="Select" />
                </div>
            </div>
        </div>
        <div style="position: absolute; top: 1000px;">
            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                Enabled="True" PopupDragHandleControlID="PopupMenu">
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


                             <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td colspan="4" class="center" style="font-size: 13px; color: #000; background-color: #ececec;
                                    font-weight: bold;">
                                    Payment Detail
                                </td>
                            </tr>
                        </tbody>
                    </table>

                            <asp:GridView ID="gvInvoiceDetailPopup" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Record Found !" OnRowCommand="gvInvoiceDetail_RowCommand" OnPageIndexChanging="gvInvoiceDetail_PageIndexChanging">
                            <Columns>
                               <%-- <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                            OnCheckedChanged="chbxSelectAll" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbxPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chbxSelect" />
                                        <asp:HiddenField ID="hdfPaymentId" runat="server" Value='<%#Eval("PaymentId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Invoice Number">
                                    <ItemTemplate>
                                         <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("InvoiceNumber") %>'></asp:Label>
                                      <%--  <asp:LinkButton ID="lbtnInvoiceNo" runat="server" CommandName="lnkInvoiceNo" CommandArgument='<%#Eval("InvoiceNumber") %>'
                                            Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>--%>
                                        <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("UploadedDocument") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PaymentDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="Payment Date" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblHQuotation" runat="server" Text="Quotation Number"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuotation" runat="server" Text='<%#Eval("QuotationNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblHName" runat="server" Text="Contractor Name"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCName" runat="server" Text='<%#Eval("ContractorSupplierName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract Number" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractNO" runat="server" Text='<%#Eval("ContractNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Order Number" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkOrderNumber" runat="server" Text='<%#Eval("WorkOrderNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                <asp:TemplateField HeaderText="Payment Mode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentType" runat="server" Text='<%#Eval("PaymentModeType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Reference Number" ItemStyle-CssClass="taright" />
                                <asp:TemplateField ItemStyle-CssClass="taright">
                                    <HeaderTemplate>
                                        Payment Amount (INR)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentAmount" runat="server" Text='<%#Eval("PaymentAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ApprovalStatusType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
