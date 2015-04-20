<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentView.ascx.cs"
    Inherits="MiniERP.Parts.PaymentView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>--%>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<script type="text/javascript">
    function clearFrom() {
        document.getElementById('<%= txtFromDate.ClientID %>').value = "";
    }
    function clearTo() {
        document.getElementById('<%= txtToDate.ClientID %>').value = "";
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="box span12">
            <asp:ValidationSummary ID="vsPaymentView" ValidationGroup="vgPayment" ShowMessageBox="true"
                ShowSummary="false" runat="server" />
            <div class="box-header well">
                <h2>
                    <asp:Label ID="lblTitle" runat="server" Text="View Contractor Payment"></asp:Label></h2>
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
                                    <asp:ListItem Value="3" Text="Invoice Number"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlName" runat="server">
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td width="20%">
                                                    From Date
                                                </td>
                                                <td width="30%">
                                                    <asp:TextBox ID="txtFromDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="ImgBtn" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                                    <asp:LinkButton ID="input_from" runat="server" OnClientClick="clearFrom()" class="Search icon188"></asp:LinkButton>
                                                    <cc1:CalendarExtender ID="calFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                                        PopupButtonID="ImgBtn">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                        ValidationGroup="vgPayment" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Form Date"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                </td>
                                                <td width="20%">
                                                    To Date
                                                </td>
                                                <td width="30%">
                                                    <asp:TextBox ID="txtToDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="ImageButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                                    <asp:LinkButton ID="input_to" runat="server" OnClientClick="clearTo()" class="Search icon188"></asp:LinkButton>
                                                    <cc1:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                        PopupButtonID="ImageButton1">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                        ValidationGroup="vgPayment" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter To Date"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="lblName" runat="server" Text="Contractor Name"></asp:Label>
                                                </td>
                                                <td colspan="3" width="80%">
                                                    <asp:DropDownList ID="ddlName" Width="400px" CssClass="dropdown" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="ddlName"
                                                        ValidationGroup="vgPayment" InitialValue="0" ForeColor="Red" SetFocusOnError="true"
                                                        ErrorMessage="Please Select Contractor Name" Display="None"></asp:RequiredFieldValidator>
                                                    <asp:Button ID="LinkSearch" runat="server" OnClick="btnSearch_Click" Text="Go" CssClass="button_color action green" 
                                                        ToolTip="Search" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlNumber" runat="server" Visible="false" DefaultButton="btnSearch">
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tbody>
                                            <tr>
                                                <td width="180px">
                                                    <asp:Label ID="lblNumber" runat="server" Text="Contract Number"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumber" CssClass="textbox" runat="server" CausesValidation="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumber" runat="server" ControlToValidate="txtNumber"
                                                        ValidationGroup="vgPayment" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Any Number"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                    <asp:LinkButton ID="btnSearch" runat="server" ValidationGroup="vgPayment" OnClick="btnSearch_Click"
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
                    <asp:GridView ID="gvPaymentDetail" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No Record Found !" OnRowDataBound="gvPaymentDetail_RowDataBound"
                        OnRowCommand="gvPaymentDetail_RowCommand" OnPageIndexChanging="gvPaymentDetail_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Invoice Number">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("InvoiceNumber") %>'></asp:Label>--%>
                                    <asp:LinkButton ID="lbtnInvoiceNo" runat="server" CommandName="lnkInvoiceNo" CommandArgument='<%#Eval("InvoiceNumber") %>'
                                        Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("UploadedDocument") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PaymentDate" DataFormatString="{0:MMM d, yyyy}" HeaderText="Payment Date" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHQuotation" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblQuotation" runat="server" Text='<%#Eval("QuotationNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHName" runat="server" Text=""></asp:Label>
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
                            <asp:BoundField DataField="Remark" HeaderText="Pament Remarks" />
                            <asp:TemplateField HeaderText="Payment Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentType" runat="server" Text='<%#Eval("PaymentModeType.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                            <asp:BoundField DataField="ReferenceNumber" HeaderText="Reference Number" ItemStyle-CssClass="taright" />
                             <asp:BoundField DataField="BillNumber" HeaderText="Bill Number" />
                                    <asp:BoundField DataField="TDS" HeaderText="TDS (%)" />
                                      <asp:BoundField DataField="TDSWithPayment" HeaderText="TDS With Amount (INR)" />

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
                            <asp:Label ID="lblRemarkReject" runat="server" Text='<%#Eval("RemarkReject") %>'></asp:Label>
                            
                            </ItemTemplate>
                            
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ApprovalStatusType.Name") %>'></asp:Label>
                                    <asp:HiddenField ID="approveStatus" runat="server" Value='<%#Eval("ApprovalStatusType.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="lnkEdit" CommandArgument='<%#Eval("PaymentId") %>'
                                        CssClass="button icon145 " ToolTip="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="lnkDelete" CommandArgument='<%#Eval("PaymentId") %>'
                                        CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure To Delete?') "></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="lnkPrint" CommandArgument='<%#Eval("PaymentId") %>'
                                        CssClass="button icon153 " ToolTip="Print"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnGenerate" runat="server" CommandName="lnkGenerate" CommandArgument='<%#Eval("PaymentId") %>'
                                        OnClientClick="return confirm('Are You Sure To Generate?') " CssClass="button icon189 "
                                        ToolTip="Generate Quotation"></asp:LinkButton>
                                     <asp:LinkButton ID="lblAdvicePayment" runat="server" CommandName="lnkPrintAdvice" CommandArgument='<%#Eval("PaymentId") %>'
                                        CssClass="print-payment pull-left" ToolTip="Print Payment Advice"><img src="../../Images/Cash_register.png"  width="22" height="22" alt="Cash_register"/></asp:LinkButton>
                                
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
                           <%-- <div class="box-header well">
                                <h2 >

                           
                                    <asp:Label ID="lblSubTitle"  runat="server" Text="Contractor Invoice"></asp:Label></h2>
                            </div>--%>
                            <table class="mGrid">
                                <asp:GridView ID="gvInvoiceItems" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                            <asp:TemplateField HeaderText="Invoice Number">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("InvoiceNumber") %>'></asp:Label>--%>
                                    <%--<asp:LinkButton ID="lbtnInvoiceNo" runat="server" CommandName="lnkInvoiceNo" CommandArgument='<%#Eval("InvoiceNumber") %>'
                                        Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>--%>
                                        <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("InvoiceNumber") %>' ></asp:Label>
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
                            <asp:BoundField DataField="Remark" HeaderText="Remarks" />
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
                                    <asp:Label ID="lblPaymentAmount" runat="server" Text='<%#Eval("TDSWithPayment") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ApprovalStatusType.Name") %>'></asp:Label>
                                    <asp:HiddenField ID="approveStatus" runat="server" Value='<%#Eval("ApprovalStatusType.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                                </asp:GridView>
                            </table>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
