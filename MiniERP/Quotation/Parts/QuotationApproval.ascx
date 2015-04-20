<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuotationApproval.ascx.cs"
    Inherits="MiniERP.Parts.QuotationApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
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
                    <td class="center">
                        Status
                    </td>
                    <td colspan="3" style="padding-right: 200px;">
                        <asp:DropDownList ID="ddl_status" CssClass="dropdown" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddl_status_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <div>
            <asp:Label ID="lbl_error_msg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="false" OnRowCommand="gvQuotation_RowCommand"
                AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                PageSize="10" AlternatingRowStyle-CssClass="alt"
                OnPageIndexChanging="gvQuotation_PageIndexChanging" OnRowDeleting="gvQuotation_RowDeleting"
                OnRowEditing="gvQuotation_RowEditing" 
                OnRowDataBound="gvQuotation_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="56px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chbx_select_all" runat="server" Text="Select" AutoPostBack="true"
                                OnCheckedChanged="on_check_uncheck_all" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chbx_Quotation" runat="server" AutoPostBack="true" OnCheckedChanged="on_check_uncheck_quotation" />
                            <asp:HiddenField ID="hdf_quotation_id" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Work Order Number">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnQuotation" runat="server" CommandName="lnkQuotation"></asp:LinkButton>
                            <asp:HiddenField ID="hdf_documnent_Id" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ContractorName" HeaderText="Contractor Name" />
                    <asp:BoundField DataField="CompanyWorkOrderNumber" HeaderText="Company Work Order Number" />
                    <asp:BoundField DataField="WorkOrderNumber" HeaderText="Work Order Number" />
                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                    <asp:TemplateField HeaderText="Purchase Order Date">
                        <ItemTemplate>
                            <asp:Label ID="lblQuotationDate" runat="server" Text='<%#Bind("OrderDate","{0:MMM d, yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Closing Date" ItemStyle-CssClass="taright">
                        <ItemTemplate>
                            <asp:Label ID="lblClosingDate" runat="server" Text='<%#Bind("ClosingDate","{0:MMM d, yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DeliveryDescription" HeaderText="Delivery Description" />
                    <%--<asp:BoundField DataField="Packaging" HeaderText="Packaging (INR)" ItemStyle-CssClass="taright" />
                    <asp:BoundField DataField="Freight" HeaderText="Freight (INR)" ItemStyle-CssClass="taright" />--%>
                    <asp:BoundField DataField="TotalNetValue" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />
                   <asp:TemplateField HeaderText="Remark">
                   <ItemTemplate>
                       <asp:TextBox ID="txtRemark" runat="server" MaxLength="50"  TextMode="MultiLine" Width="100" Text='<%# Eval("RemarkReject") %>'></asp:TextBox>
                   </ItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusType" runat="server" Text='<%#Eval("StatusType.Name") %>'></asp:Label>
                            <asp:HiddenField ID="hdf_status_id" runat="server" Value='<%#Eval("StatusType.Id")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="Button_align" style="margin-left: 80px">
            <asp:Button ID="btn_Reject" runat="server" Text="Reject" CssClass="button_color action"
                CommandName="Reject" OnClientClick="return confirm('Are You Sure To Reject?') " CommandArgument="r" OnCommand="btn_Approve_Reject_Click" />
            <asp:Button ID="btn_Approve" runat="server" Text="Approve" CssClass="button_color action gray"
                CommandName="Approve" OnClientClick="return confirm('Are You Sure To Approve?') " CommandArgument="a" OnCommand="btn_Approve_Reject_Click" />
        </div>
        <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
            CommandName="Select" />
    </div>
</div>
<div style="position: absolute; top: 1000px;">
    <%--<cc1:ModalPopupExtender ID="mpe_msg" runat="server" TargetControlID="btnPopUp" PopupControlID="Panel1"
        BackgroundCssClass="modalBackground" DropShadow="true" Enabled="True" PopupDragHandleControlID="PopupMenu">
    </cc1:ModalPopupExtender>
    <div>
        <asp:Panel ID="Panel1" runat="server" CssClass="popup" ScrollBars="Vertical">
            <div class="box-content">
                <asp:Label ID="lbl_msg" runat="server"></asp:Label>
                <br />
                <asp:Button ID="btn_ok" runat="server" Text="Ok" OnClick="btn_ok_cancel_Click" CommandName="Ok"
                    CommandArgument="o" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_ok_cancel_Click"
                    CommandName="Cancel" CommandArgument="c" />
            </div>
        </asp:Panel>
    </div>--%>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
        PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
        Enabled="True" PopupDragHandleControlID="PopupMenu">
    </cc1:ModalPopupExtender>
    <div>
        <asp:Panel ID="panel3" runat="server" CssClass="popup1">
            <div class="PopUpClose">
                <div class="btnclosepopup">
                    <asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
            </div>
            <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                <div class="box-content">
                    <div class="box-header well">
                        <h2>
                            Attached Documents
                        </h2>
                    </div>
                    <div>
                        <udc:UploadDocuments runat="server" ID="updcFile"  
                            EnableViewState="True" />
                    </div>
                    <div class="box-header well">
                        <h2>
                            <asp:Label ID="lbl_quotation" runat="server"></asp:Label>
                        </h2>
                    </div>
                    <div>
                        <%--<asp:GridView ID="gvContractorQuotationItems" runat="server" AutoGenerateColumns="false"
                        AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemNumber" runat="server" Text='<%#Eval("Service_Detail.ItemNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Number">
                                <ItemTemplate>
                                    <asp:Label ID="ServiceNumber" runat="server" Text='<%#Eval("Service_Detail.ServiceNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblService_Description" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity1" runat="server" Text='<%#Eval("Service_Detail.Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity Issued">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantityIssued1" runat="server" Text='<%#Eval("Service_Detail.QuantityIssued") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitMeasurement1" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitRate1" runat="server" Text='<%#Eval("Service_Detail.UnitRate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Applicable Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblApplicableRate1" runat="server" Text='<%#Eval("Service_Detail.ApplicableRate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscountType1" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount" />
                            <asp:TemplateField HeaderText="Excise Duty (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblExciseDuty1" runat="server" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Tax (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblServiceTax1" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vat (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblVAT1" runat="server" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST (With C Form) (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblCSTwith_C_Form1" runat="server" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST (Without C Form) (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblCSTWithout_C_Form1" runat="server" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount (INR)" />
                        </Columns>
                    </asp:GridView>--%>
                        <asp:GridView ID="gvSupplierQuotationItems" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="15" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField HeaderText="Activity Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("DeliverySchedule.ActivityDescription")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ItemName")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speci- fication">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.ModelSpecificationName")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCatg" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.Category.ItemCategoryName")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make (Brand)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.Brand.BrandName")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NumberOfUnit" HeaderText="Quantity" ItemStyle-CssClass="taright" />
                                <asp:TemplateField HeaderText="Unit Measurment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUM" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.UnitMeasurement.Name")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PreviousUnitRate" HeaderText="Previous Unit Rate" ItemStyle-CssClass="taright" />
                                <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR)" ItemStyle-CssClass="taright" />
                                <asp:TemplateField HeaderText="Discount Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountType" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount" ItemStyle-CssClass="taright" />
                                  <asp:BoundField DataField="Discount_Rates" HeaderText="Discount Rates (INR)" ItemStyle-CssClass="taright" />
                                <%--<asp:TemplateField HeaderText="Excise Duty (%)" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExciseDuty" runat="server" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Tax (%)" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceTax" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vat (%)" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVAT" runat="server" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST (With C Form) (%)" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTwith_C_Form" runat="server" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST (Without C Form) (%)" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTWithout_C_Form" runat="server" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Freight (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("TaxInformation.Freight") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Packaging (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPackaging" runat="server" Text='<%#Eval("TaxInformation.Packaging") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount (INR)" ItemStyle-CssClass="taright" />
                            </Columns>
                        </asp:GridView>
                        <table class="table table-bordered table-striped table-condensed" id="tbl_itemTransaction"
                            runat="server">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        <asp:Label ID="lblTotal" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center">
                                        <asp:Label ID="lblTotalDiscount" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center" style="display:none">
                                        <asp:Label ID="lblTotalExciseDuty" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" style="visibility:hidden;">
                                        <asp:Label ID="lblTotalTax" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center" style="display:none">
                                        <asp:Label ID="lblPackaging" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center" style="display:none">
                                        <asp:Label ID="lblfreight" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center">
                                        <asp:Label ID="lblGrandTotal" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                    </div>
                    <div class="box-header well">
                        <h2>
                            Delivery Schedule
                        </h2>
                    </div>
                    <div>
                        <asp:GridView ID="gvDeliverySchedule" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:BoundField DataField="ItemDescription" HeaderText="Service Description" HtmlEncode="false" />
                                <asp:BoundField DataField="Item" HeaderText="Item" HtmlEncode="false" />
                                <asp:BoundField DataField="Specification" HeaderText="Specification" HtmlEncode="false" />
                                <asp:BoundField DataField="ItemQuantity" HeaderText="Item Quantity" />
                                <asp:BoundField DataField="DeliveryDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="Delivery Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="box-header well">
                        <h2>
                            Payment Term
                        </h2>
                    </div>
                    <div>
                        <asp:GridView ID="gvPaymentTerm" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField HeaderText="Payment Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("PaymentType.Name")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NumberOfDays" HeaderText="Number Of Days" ItemStyle-CssClass="taright" />
                                <asp:TemplateField HeaderText="Percentage Value (%)" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPercentageValue" runat="server" Text='<%#Eval("PercentageValue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentDescription" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("PaymentDescription")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="box-header well">
                        <h2>
                            Terms &amp; Conditions
                        </h2>
                    </div>
                    <div>
                        <asp:GridView ID="gvTermAndCondition" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSN" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Terms & Conditions" HtmlEncode="false" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</div>
