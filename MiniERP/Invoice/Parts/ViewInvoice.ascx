<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewInvoice.ascx.cs"
    Inherits="MiniERP.Invoice.Parts.ViewInvoice" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>


<div class="box span12">
    <div class="box-header well">
        <h2>
            <asp:Label ID="lbl_ViewInvoice" runat="server"></asp:Label>
        </h2>
    </div>
    <div class="box-content">
        <table class="table table-bordered table-striped table-condensed">
            <tbody>
                <tr>
                    <td class="Center" width="23%">
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </td>
                    <td class="Center" width="27%">
                        <asp:DropDownList ID="ddlContractorName" CssClass="dropdown" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="Center" width="23%">
                        <asp:Label ID="lblWorkOrder" runat="server" Text="Contractor Work Order No"></asp:Label>
                    </td>
                    <td class="Center" width="27%">
                        <asp:TextBox ID="txtContQuotNo" CssClass="TextBox" runat="server"></asp:TextBox>
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
                    <td class="center" width="23%">
                        Invoice Number
                    </td>
                    <td width="77%">
                        <asp:TextBox ID="txtInvoiceNo" CssClass="TextBox" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                            ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>
                    </td>
                </tr>
            </tbody>
        </table>
        <div>
            <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="20" EmptyDataText="No Record Found(s)" AlternatingRowStyle-CssClass="alt"
                OnPageIndexChanging="gvInvoice_PageIndexChanging" OnRowCommand="gvInvoice_RowCommand"
                OnRowDataBound="gvInvoice_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Invoice No." ControlStyle-Width="100px">
                        <ItemTemplate>
                            <%-- <asp:Label ID="lnkbtnInvoice" runat="server" CommandName="lnkInvoice" CommandArgument='<%#Eval("ContractorInvoiceId") %>'
                                Text='<%#Eval("InvoiceNumber") %>'></asp:Label>--%>
                            <asp:LinkButton ID="lnkbtnInvoice" runat="server" CommandName="lnkInvoice" CommandArgument='<%#Eval("ContractorInvoiceId") %>'
                                Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>
                            <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("IssueMaterial.DemandVoucher.Quotation.StatusType.Id") %>' />
                            <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("IssueMaterial.DemandVoucher.Quotation.UploadDocumentId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Date">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%#Eval("InvoiceDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contractor Name">
                        <ItemTemplate>
                            <asp:Label ID="lblConName" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contractor Work Order No.">
                        <ItemTemplate>
                            <asp:Label ID="lblConNo" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Type">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoicetypeName" runat="server" Text='<%#Eval("InvoiceType.Name") %>'></asp:Label>
                            <asp:HiddenField ID="hdfInvoiceTypeId" runat="server" Value='<%#Eval("InvoiceType.Id") %>' />
                            <%-- <asp:Label ID="lblInvoiceTypeId" runat="server" Text='<%#Eval("InvoiceType.Id") %>' Visible="false"></asp:Label>--%>
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
                    <asp:BoundField DataField="PayableAmount" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />
                    <asp:TemplateField HeaderText="Freight(INR)" ItemStyle-CssClass="taright" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.Freight") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Packaging(INR)" ItemStyle-CssClass="taright" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPackaging" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.Packaging") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblremarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approval Remark" ControlStyle-Width="100px">
                    <ItemTemplate>
                    <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("RemarkReject") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.StatusType.Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action" ControlStyle-Width="50px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" Visible="false" runat="server" CommandName="lnkEdit"
                                CommandArgument='<%#Eval("ContractorInvoiceId") %>' CssClass="button icon145 "
                                ToolTip="Edit"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="lnkDelete" CommandArgument='<%#Eval("ContractorInvoiceId") %>'
                                CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure To Delete?') "></asp:LinkButton>
                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="lnkPrint" CommandArgument='<%#Eval("ContractorInvoiceId")+","+Eval("InvoiceNumber") %>'
                                CssClass="button icon153 " ToolTip="Print"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnGenerate" runat="server" CommandName="lnkGenerate" CommandArgument='<%#Eval("ContractorInvoiceId") %>'
                                CssClass="button icon189 " ToolTip="Generate Invoice" OnClientClick="return confirm('Are You Sure To Generate This Invoice?') "></asp:LinkButton>
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
        Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button1">
    </asp:ModalPopupExtender>
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
                            <asp:Label ID="lbl_Invoice_Mapping" runat="server"></asp:Label>
                        </h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        Contractor Name
                                    </td>
                                    <td>
                                        <asp:Label ID="lblContName" runat="server"></asp:Label>
                                    </td>
                                    <td class="center">
                                        Invoice Type
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
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="mGriditem">
                        <asp:GridView ID="gvItemInfoPopup" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" ShowFooter="true" OnRowDataBound="gvItemInfoPopup_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Activity Discription">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActDisc" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                <asp:TemplateField HeaderText="Category level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UnitForBilled" HeaderText="Item Billed" />
                                <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR)" />
                                <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount (%)" />
                                <asp:BoundField DataField="Discount_Rates" HeaderText="Discount Rates (INR)" />
                                <asp:TemplateField HeaderText="Service Tax%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceTax" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVAT" runat="server" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST (With C Form)%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTWithFrom" runat="server" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST (Without C Form)%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTWithoutForm" runat="server" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblGrandTotal" runat="server" CssClass="total" Text="Grand Total"></asp:Label>
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
   

