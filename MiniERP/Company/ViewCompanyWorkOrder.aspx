<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewCompanyWorkOrder.aspx.cs" Inherits="MiniERP.Company.ViewCompanyWorkOrder" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>
        <script type="text/javascript">
            function clearFrom() {
                document.getElementById('<%= txtFromDate.ClientID %>').value = "";
            }
            function clearTo() {
                document.getElementById('<%= txtToDate.ClientID %>').value = "";
            }
        </script>
    </div>
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/ajax_loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="width:40px; height:40px; position:fixed; top:0; right:0; left:0; bottom:0; margin:auto" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <asp:ValidationSummary ID="vsVCWO" ValidationGroup="vcwo" ShowMessageBox="true" ShowSummary="false"
                    runat="server" />
                <div class="box-header well">
                    <h2>
                        View Company Work Order</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="22%">
                                    From Date&nbsp;
                                </td>
                                <td class="center" width="28%">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="LnkBtn" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:LinkButton ID="input_from" runat="server" OnClientClick="clearFrom()" class="Search icon188"></asp:LinkButton>
                                    <%-- <input type="button" id="input_from" onclick="clearFrom()" class="Search icon188" />--%>
                                    <asp:CalendarExtender ID="calFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                        PopupButtonID="LnkBtn">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                        ValidationGroup="vcwo" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Form Date"
                                        Display="None"></asp:RequiredFieldValidator>
                                </td>
                                <td class="center" width="22%">
                                    To Date&nbsp;
                                </td>
                                <td class="center" width="28%">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="LnkButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:LinkButton ID="input_to" runat="server" OnClientClick="clearTo()" class="Search icon188"></asp:LinkButton>
                                    <%--<input type="button" id="input_to" onclick="clearTo()" class="Search icon188" />--%>
                                    <asp:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                        PopupButtonID="LnkButton1" ViewStateMode="Inherit">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                        ValidationGroup="vcwo" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter To Date"
                                        Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnGO" runat="server" Text="Go" ValidationGroup="" CommandName="Go"
                            OnCommand="btnSearch_Click" CssClass="button_color action green" /></div>
                    <br />
                    <br />
                    <asp:Panel ID="panel1" runat="server" DefaultButton="LinkSearch">
                        <table class="table table-bordered table-striped table-condensed searchbg">
                            <tbody>
                                <tr>
                                    <td class="center" width="22%">
                                        Company Work Order Number
                                    </td>
                                    <td class="center" width="78%">
                                        <asp:TextBox ID="txtContractNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="LinkSearch" runat="server" OnCommand="btnSearch_Click" CommandName="Search"
                                            CssClass="Search icon198" ToolTip="Search"></asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                    <div>
                        <asp:GridView ID="gvViewCWO" runat="server" AutoGenerateColumns="false" OnRowCommand="gvViewCWO_RowCommand"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Record Found !" OnRowDataBound="gvViewCWO_RowDataBound" OnPageIndexChanging="gvViewCWO_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Company Work Order Number">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnQuotation" runat="server" CommandName="lnkContractNo" CommandArgument='<%#Eval("CompanyWorkOrderId") %>'
                                            Text='<%#Eval("CompanyWorkOrderNumber") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("UploadDocumentId") %>' />
                                        <%--<asp:HiddenField ID="approveStatus" runat="server" Value='<%#Eval("StatusType.Id") %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ContractNumber" HeaderText="Contract Number" />
                                <asp:BoundField DataField="ContractDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="Contract Date" />
                                <asp:BoundField DataField="WorkOrderDescription" HeaderText="Work Order Description" />
                                <asp:TemplateField HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalNetValue" runat="server" Text='<%#Eval("TotalNetValue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                <asp:Label ID="lblRemarReject" runat="server" Text='<%# Eval("RemarkReject") %>'></asp:Label>
                                </ItemTemplate>
                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("StatusType.Name") %>'></asp:Label>
                                        <asp:HiddenField ID="approveStatus" runat="server" Value='<%#Eval("StatusType.Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="lnkEdit" CommandArgument='<%#Eval("CompanyWorkOrderId") %>'
                                            CssClass="button icon145 " ToolTip="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="lnkDelete" CommandArgument='<%#Eval("CompanyWorkOrderId") %>'
                                            CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure To Delete?') "></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="lnkPrint" CommandArgument='<%#Eval("CompanyWorkOrderId")+","+Eval("StatusType.Id")%>'
                                            CssClass="button icon153 " ToolTip="Print"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnGenerate" runat="server" CommandName="lnkGenerate" CommandArgument='<%#Eval("CompanyWorkOrderId") %>'
                                            OnClientClick="return confirm('Are You Sure To Generate ?') " CssClass="button icon189 "
                                            ToolTip="Generate Work Order"></asp:LinkButton>
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
                </div>
            </div>
            <div style="position: absolute; top: 1000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="btnClose">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                            <div class="btnclosepopup">
                                <asp:Button ID="btnClose" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
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
                                        Company Work Order
                                    </h2>
                                </div>
                                <div>
                                    <asp:GridView ID="gvWorkOrder" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                            <asp:BoundField DataField="WorkOrderNumber" HeaderText="Work Order Number" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount (INR)" />
                                            <asp:BoundField DataField="Area" HeaderText="Area" />
                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                            <asp:TemplateField HeaderText="Service Tax (%)">
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
                                                    <asp:Label ID="lblCSTWithoutCForm" runat="server" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Freight (INR)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("TaxInformation.Freight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount Mode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiscountType" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Discount (%/INR)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalDiscount" runat="server" Text='<%#Eval("TaxInformation.TotalDiscount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Value (INR)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalNetValue" runat="server" Text='<%#Eval("TaxInformation.TotalNetValue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="box-header well">
                                    <h2>
                                        Bank Guarantee
                                    </h2>
                                </div>
                                <div>
                                    <asp:GridView ID="gvBankGuarantee" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                        CssClass="mGrid" OnRowDataBound="gvBankGuarantee_RowDataBound" PagerStyle-CssClass="pgr"
                                        PageSize="10" AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                            <asp:BoundField DataField="StartDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="Start Date" />
                                            <asp:BoundField DataField="EndDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="End Date" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="BankName" HeaderText="BankName" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <%--<div class="box-header well">
                                <h2>
                                    Service Detail
                                </h2>
                            </div>
                            <div>
                                <asp:GridView ID="gvServiceDetail" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" OnPageIndexChanging="gvServiceDetail_PageIndexChanging"
                                    PageSize="10" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:BoundField DataField="WorkOrderNumber" HeaderText="WO Number" />
                                        <asp:BoundField DataField="ItemNumber" HeaderText="Item Number" />
                                        <asp:BoundField DataField="ServiceNumber" HeaderText="Service Number" />
                                        <asp:BoundField DataField="ServiceDescription" HeaderText="Service Description" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Qunatity" />
                                        
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit.Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UnitRate" HeaderText="Unit Rate" />
                                        <asp:BoundField DataField="ApplicableRate" HeaderText="Applicable Rate" />
                                    </Columns>
                                </asp:GridView>
                            </div>--%>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
