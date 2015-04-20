<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewOrder.ascx.cs" Inherits="MiniERP.Parts.ViewOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<div class="box span12">
    <div class="box-header well">
        <h2>
            <asp:Label ID="lbl_WorkOrder" runat="server"></asp:Label>
        </h2>
    </div>
    <div class="box-content">
        <table class="table table-bordered table-striped table-condensed">
            <tbody>
                <tr>
                    <td class="center" width="20%">
                        <%-- Contractor Name--%>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </td>
                    <td class="center" width="30%">
                        <asp:DropDownList ID="ddlContractor" CssClass="dropdown" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="center" width="20%">
                        <asp:Label ID="lblContractNo" runat="server"></asp:Label>
                    </td>
                    <td class="center" width="30%">
                        <asp:DropDownList ID="ddlContractNo" CssClass="TextBox" runat="server">
                            <%--<asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">ABVP-001</asp:ListItem>
                            <asp:ListItem Value="2">ABVP-002</asp:ListItem>
                            <asp:ListItem Value="3">ABVP-003</asp:ListItem>
                            <asp:ListItem Value="4">ABVP-004</asp:ListItem>
                            <asp:ListItem Value="5">ABVP-005</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="center">
                        From Date
                    </td>
                    <td class="center">
                        <asp:TextBox ID="TextFromDate1" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                        <asp:LinkButton ID="LnkBtn1" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                        <asp:CalendarExtender ID="CalFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TextFromDate1"
                            PopupButtonID="LnkBtn1">
                        </asp:CalendarExtender>
                        <asp:LinkButton ID="LinkbtnClear" runat="server" OnClick="LinkbtnClear_Click" ToolTip="Clear"
                            CssClass="Search icon188">
                        </asp:LinkButton>
                    </td>
                    <td class="center">
                        To date
                    </td>
                    <td class="center">
                        <asp:TextBox ID="TextToDate1" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                        <asp:LinkButton ID="LnkButton11" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                        <asp:CalendarExtender ID="CalToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TextToDate1"
                            PopupButtonID="LnkButton11">
                        </asp:CalendarExtender>
                        <asp:LinkButton ID="lnkbtn" runat="server" OnClick="lnkbtn_Click" ToolTip="Clear"
                            CssClass="Search icon188"></asp:LinkButton>
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
        <asp:Panel ID="panel1" runat="server" DefaultButton="LinkSearch">
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                        <td class="center" width="20%">
                            Work Order Number
                        </td>
                        <td width="80%">
                            <asp:TextBox ID="txtWrkOrderNo" CssClass="TextBox" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
        <div>
            <asp:GridView ID="gvViewOrder" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                OnPageIndexChanging="gvViewOrder_PageIndexChanging" OnRowCommand="gvViewOrder_RowCommand"
                EmptyDataText="No Record Found(s)" OnRowDataBound="gvViewOrder_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Work order No.">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnQuotation" runat="server" CommandName="lnkQuotation" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                                Text='<%#Eval("ContractQuotationNumber") %>'></asp:LinkButton>
                            <asp:HiddenField ID="approveStatus" runat="server" Value='<%#Eval("StatusType.Id") %>' />
                            <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("UploadDocumentId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="QuotationDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="Quotation Date" />--%>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblHCName" runat="server" Text="Contractor Name"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCName" runat="server" Text='<%#Eval("ContractorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quotation Date">
                        <ItemTemplate>
                            <asp:Label ID="lblQuotationDate" runat="server" DataFormatString="{0:dd MM yy}" Text='<%#Eval("OrderDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Closing Date">
                        <ItemTemplate>
                            <asp:Label ID="lblClosingDate" runat="server" Text='<%#Bind("ClosingDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DeliveryDescription" HeaderText="Delivery Description" />
                    <asp:BoundField DataField="Freight" HeaderText="Freight (INR)" ItemStyle-CssClass="taright" />
                    <asp:BoundField DataField="Packaging" HeaderText="Packaging (INR)" ItemStyle-CssClass="taright" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblHCNumber" runat="server" Text="Contract Number"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCNumber" runat="server" Text='<%#Eval("ContractNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TotalNetValue" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />
                    <asp:TemplateField HeaderText="Status" ControlStyle-Width="50px">
                    <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("StatusType.Name") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action" ControlStyle-Width="50px">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="lbtnEdit" runat="server" CommandName="lnkEdit" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                            CssClass="button icon145 " ToolTip="Edit"></asp:LinkButton>--%>
                            <%--<asp:LinkButton ID="lbtnDelete" runat="server" CommandName="lnkDelete" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                            CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure To Delete?') "></asp:LinkButton>--%>
                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="lnkPrint" CommandArgument='<%#Eval("ContractorQuotationId")+","+Eval("ContractQuotationNumber") %>'
                                CssClass="button icon153 " ToolTip="Print"></asp:LinkButton>
                            <%--<asp:LinkButton ID="lbtnGenerate" runat="server" CommandName="lnkGenerate" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                            CssClass="button icon189 " ToolTip="Generate Quotation"></asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
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
                    <div class="box-header well">
                        <h2>
                            <asp:Label ID="lbl_Order" runat="server"></asp:Label>
                        </h2>
                    </div>
                    <div>
                        <asp:GridView ID="gvContractorPOItems" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField HeaderText="Activity Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityDesc" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem1" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speci- fication">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel1" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCatg1" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make (Brand)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake1" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NumberOfUnit" HeaderText="Quantity" />
                                <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR)" />
                                <asp:TemplateField HeaderText="Discount Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountType1" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount" />
                                <%--<asp:TemplateField HeaderText="Excise Duty (%)">
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
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount (INR)" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvSupplierPOItems" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField HeaderText="Activity Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityDesc" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speci- fication">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCatg" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make(Brand)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NumberOfUnit" HeaderText="Quantity" />
                                <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost(INR)" />
                                <asp:TemplateField HeaderText="Discount Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountType" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount" />
                                <%--<asp:TemplateField HeaderText="Excise Duty (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExcise" runat="server" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Tax (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceTax" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vat(%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVAT" runat="server" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST (With C Form) (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTwith_C_Form" runat="server" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST (Without C Form) (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTWithout_C_Form" runat="server" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packaging (INR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPackaging" runat="server" Text='<%#Eval("TaxInformation.Packaging") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount(INR)" />
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
                                    
                                </tr>
                                <tr>
                                    <td class="center" style="display:none">
                                        <asp:Label ID="lblTotalTax" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center" style="display:none">
                                        <asp:Label ID="lblPackaging" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center" style="display:none">
                                        <asp:Label ID="lblfreight" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                    <td class="center" colspan="2">
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
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:BoundField DataField="ItemDescription" HeaderText="Service Description" />
                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                <asp:BoundField DataField="Specification" HeaderText="Specification" />
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
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField HeaderText="Payment Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentName" runat="server" Text='<%#Eval("PaymentType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NumberOfDays" HeaderText="Number Of Days" />
                                <asp:TemplateField HeaderText="Percentage Value (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPercentageValue" runat="server" Text='<%#Eval("PercentageValue") %>'></asp:Label>
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
                    <div class="box-header well">
                        <h2>
                            Terms &amp; Conditions
                        </h2>
                    </div>
                    <div>
                        <asp:GridView ID="gvTermAndCondition" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSN" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Terms & Conditions" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</div>
