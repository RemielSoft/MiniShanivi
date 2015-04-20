<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewQuotation.ascx.cs"
    Inherits="MiniERP.Parts.ViewQuotation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<%--<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="box span12">
            <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="vq" ShowMessageBox="true"
                ShowSummary="false" runat="server" />
            <div class="box-header well">
                <h2>
                    <asp:Label ID="lblTitle" runat="server" Text="View Contractor Work Order"></asp:Label></h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rbtnList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="rbtnList_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="True" Text="Contractor Name"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Contract Number"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Work Order Number"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlName" runat="server">
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    From Date<span style="color: Red;">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="ImgBtn" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                                    <cc1:CalendarExtender ID="calFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                                        PopupButtonID="ImgBtn">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                        ValidationGroup="vq" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Form Date"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    To Date<span style="color: Red;">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtToDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="ImageButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                                    <cc1:CalendarExtender ID="calToDate" CssClass="" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtToDate" PopupButtonID="ImageButton1">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                        ValidationGroup="vq" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter To Date"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text="Contractor Name"></asp:Label>
                                                    <span style="color: Red;">*</span>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="ddlName" CssClass="dropdown" runat="server">
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="ddlName"
                                                        ValidationGroup="vq" InitialValue="0" ForeColor="Red" SetFocusOnError="true"
                                                        ErrorMessage="Please Select Contractor Name" Display="None"></asp:RequiredFieldValidator>--%>
                                                    <asp:LinkButton ID="LinkSearch" runat="server" ValidationGroup="vq" OnClick="btnSearch_Click"
                                                        CssClass="Search icon198" ToolTip="Search"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlNumber" runat="server" Visible="false">
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td width="180px">
                                                    <asp:Label ID="lblNumber" runat="server" Text="Work Order Number"></asp:Label>
                                                    <span style="color: Red;">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumber" CssClass="TextBox" runat="server" CausesValidation="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumber" runat="server" ControlToValidate="txtNumber"
                                                        ValidationGroup="vq" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Any Number"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="vq" OnClick="btnSearch_Click"
                                                        CssClass="Search icon198" ToolTip="Search"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div>
                    <asp:GridView ID="gvViewQuotation" runat="server" AutoGenerateColumns="false" OnRowCommand="gvViewQuotation_RowCommand"
                        EmptyDataText="No Records Found !" AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr"
                        PageSize="20" AlternatingRowStyle-CssClass="alt" OnRowDataBound="gvViewQuotation_RowDataBound"
                        OnPageIndexChanging="gvViewQuotation_PageIndexChanging" >
                        <Columns>
                         <asp:TemplateField HeaderStyle-Width="56px" Visible="False">
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
                                    <asp:LinkButton ID="lbtnQuotation" runat="server" CommandName="lnkQuotation" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                                        Text='<%#Eval("ContractQuotationNumber") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdf_documnent_Id" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Order Date">
                             <HeaderTemplate>
                                    <asp:Label ID="lblWorkOrderDate" runat="server" Text=""></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblQuotationDate" runat="server" Text='<%#Bind("OrderDate","{0:MMM d, yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             
                             

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHCName" runat="server" Text="Contractor Name"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CompanyWorkOrderNumber" HeaderText="Company Work Order Number" ItemStyle-CssClass="taright" />
                            <asp:BoundField DataField="ContractNumber" HeaderText="Contract Number" ItemStyle-CssClass="taright" />
                            <asp:TemplateField HeaderText="Closing Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingDate" runat="server" Text='<%#Bind("ClosingDate","{0:MMMM d, yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DeliveryDescription" HeaderText="Delivery Description"
                                HtmlEncode="false" />
                            <%--<asp:TemplateField>
                                <HeaderTemplate>
                                    Packaging
                                    <img alt="" src="../../Images/rupee_symbol5.png"  />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPackagingView" runat="server" Text='<%#Eval("Packaging") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:BoundField DataField="Packaging" HeaderText="Packaging (INR)" ItemStyle-CssClass="taright" />
                            <asp:BoundField DataField="Freight" HeaderText="Freight (INR)" ItemStyle-CssClass="taright" />--%>
                            <asp:BoundField DataField="TotalNetValue" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />
                            <asp:TemplateField HeaderText="Remark" ControlStyle-Width="100px">
                            <ItemTemplate>
                            <asp:Label ID="lblRemarkReject" runat="server"  Text='<%#Eval("RemarkReject") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusType" runat="server" Text='<%#Eval("StatusType.Name") %>'></asp:Label>
                                    <asp:HiddenField ID="approveStatus" runat="server" Value='<%#Eval("StatusType.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="lnkEdit" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                                        CssClass="button icon145 " ToolTip="Edit"></asp:LinkButton>
                                         <asp:LinkButton ID="lbtnClose" runat="server" CommandName="lnkClose" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                                        CssClass="button icon48 " ToolTip="close" OnClientClick="return confirm('Are You Sure To close?') "></asp:LinkButton>

                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="lnkDelete" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                                        CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure To Delete?') "></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="lnkPrint" CommandArgument='<%#Eval("ContractorQuotationId")+","+Eval("ContractQuotationNumber") %>'
                                        CssClass="button icon153 " ToolTip="Print"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnGenerate" runat="server" CommandName="lnkGenerate" CommandArgument='<%#Eval("ContractorQuotationId") %>'
                                        CssClass="button icon189 " ToolTip="Generate Quotation" OnClientClick="return confirm('Are You Sure To Generate Quotation?') "></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                        CommandName="Select" />
                </div>
                <div class="Button_align" style="margin-left: 80px; display:none;">
            <asp:Button ID="btn_Close" runat="server" Text="Close Quotations" CssClass="button_color action"
                CommandName="Close " OnClientClick="return confirm('Are You Sure To Close?') " 
                        CommandArgument="c" oncommand="btn_Close_Command" />
          
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
                                <udc:UploadDocuments runat="server" ID="updcFile" />
                            </div>
                            <div class="box-header well">
                                <h2>
                                    <asp:Label ID="lblSubtitle" runat="server" Text="Contractor Work Order"></asp:Label>
                                </h2>
                            </div>
                            <div>
                                <asp:GridView ID="gvContractorQuotationItems" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activity Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("DeliverySchedule.ActivityDescription")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem1" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ItemName")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Speci- fication">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModel1" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.ModelSpecificationName")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Level">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCatg1" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.Category.ItemCategoryName")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Make (Brand)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMake1" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Item.ModelSpecification.Brand")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NumberOfUnit" HeaderText="Quantity" />
                                        <asp:TemplateField HeaderText="Unit Measurment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUM" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.UnitMeasurement.Name")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR)" />
                                        <asp:TemplateField HeaderText="Discount Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiscountType1" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount" />
                                        <asp:BoundField DataField="Discount_Rates" HeaderText="Discount Rate (INR)" />
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
                                <asp:GridView ID="gvSupplierQuotationItems" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
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
                                                <asp:Label ID="lblMake" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Item.ModelSpecification.Brand")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NumberOfUnit" HeaderText="Quantity" />
                                        <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR)" />
                                        <asp:TemplateField HeaderText="Discount Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiscountType" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount" />
                                         <asp:BoundField DataField="Discount_Rates" HeaderText="Discount Rate (INR)" />
                                        <%--<asp:TemplateField HeaderText="Excise Duty (%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExciseDuty" runat="server" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Service Tax (%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblServiceTax" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vat (%)">
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
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount (INR)" />
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
                                            <td class="Right">
                                                <asp:Label ID="lblGrandTotal" runat="server" CssClass="total"></asp:Label>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                        <%--<td>
                                        <asp:Label ID="lblGrandTotal" runat="server" CssClass="total"></asp:Label
                                        </td>
                                        <td colspan="3">
                                        </td>--%>
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
                                        <asp:BoundField DataField="NumberOfDays" HeaderText="Number Of Days" />
                                        <asp:TemplateField HeaderText="Percentage Value (%)">
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
    </ContentTemplate>
</asp:UpdatePanel>
