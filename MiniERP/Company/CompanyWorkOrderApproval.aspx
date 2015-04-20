<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="CompanyWorkOrderApproval.aspx.cs" Inherits="MiniERP.Company.CompanyWorkOrderApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Company Work Order Approval
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
                        <asp:GridView ID="gvViewCWO" runat="server" AutoGenerateColumns="false" OnRowCommand="gvViewCWO_RowCommand"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Record Found !" OnPageIndexChanging="gvViewCWO_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                            OnCheckedChanged="chbxSelectAll" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbxCWO" runat="server" AutoPostBack="true" OnCheckedChanged="chbxSelect" />
                                        <asp:HiddenField ID="hdfCWOId" runat="server" Value='<%#Eval("CompanyWorkOrderId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                <asp:TextBox ID="txtRemarkReject" runat="server" MaxLength="50"  TextMode="MultiLine" Width="100" Text='<%#Eval("RemarkReject") %>'></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalNetValue" runat="server" Text='<%#Eval("TotalNetValue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                            CommandName="Select" />
                    </div>
                    <div class="Button_align" style="margin-left: 80px">
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button_color action"
                            CommandName="Reject" OnClientClick="return confirm('Are You Sure To Reject?') " OnCommand="btnApproveReject_Click" />
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button_color action gray"
                            CommandName="Approve" OnClientClick="return confirm('Are You Sure To Approve?') " OnCommand="btnApproveReject_Click" />
                    </div>
                </div>
                <div style="position: absolute; top: 1000px;">
                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
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
                                            Company Work Order
                                        </h2>
                                    </div>
                                    <div>
                                        <asp:GridView ID="gvWorkOrder" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                                            <Columns>
                                                <asp:BoundField DataField="WorkOrderNumber" HeaderText="Work Order Number" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount (INR)" />
                                                <asp:BoundField DataField="Area" HeaderText="Area" />
                                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                                <asp:TemplateField HeaderText="Service Tax (%)" ItemStyle-CssClass="tarigh">
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
                                        <asp:GridView ID="gvBankGuarantee" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt">
                                            <Columns>
                                                <asp:BoundField DataField="StartDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="Start Date" />
                                                <asp:BoundField DataField="EndDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="End Date" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                <asp:BoundField DataField="BankName" HeaderText="BankName" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="box-header well">
                                        <h2>
                                            Attached Documents
                                        </h2>
                                    </div>
                                    <div>
                                        <udc:UploadDocuments runat="server" ID="updcFile" />
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
