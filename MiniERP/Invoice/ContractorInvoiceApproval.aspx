<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ContractorInvoiceApproval.aspx.cs" Inherits="MiniERP.Invoice.ContractorInvoiceApproval" ValidateRequest="false"%>

<%@ Register Src="~/Invoice/Parts/InvoiceApproval.ascx" TagName="ApprovalInvoice" TagPrefix="uc_ApprovalInvoice" %>
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
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
        <uc_ApprovalInvoice:ApprovalInvoice ID="AppInvoice" runat="server" />
            <%--<div class="box span12">
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
                                <td colspan="3" style="padding-right: 200px;">
                                    <asp:DropDownList ID="ddlStatus" Width="147px" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />

                    <div>
                        <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                            OnPageIndexChanging="gvInvoice_PageIndexChanging" OnRowCommand="gvInvoice_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbx_select_all" runat="server" Text="Select" AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbx_Quotation" runat="server" />
                                        <asp:HiddenField ID="hdf_quotation_id" runat="server" Value='<%#Eval("ContractorInvoiceId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Number">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnInvoice" runat="server" CommandName="lnkQuotation" CommandArgument='<%#Eval("ContractorInvoiceId") %>'
                                            Text='<%#Eval("InvoiceNumber") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" />
                                <asp:TemplateField HeaderText="ContractorName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContName" runat="server" Text='<%#Eval("Quotation.ContractorName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contractor Work Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContQuotNo" runat="server" Text='<%#Eval("Quotation.ContractQuotationNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusType" runat="server" Text='<%#Eval("Quotation.StatusType.Name") %>'></asp:Label>
                                        <asp:HiddenField ID="hdf_status_id" runat="server" Value='<%#Eval("Quotation.StatusType.Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="Button_align" style="margin-left: 80px">
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button_color action"
                            CommandName="Reject" CommandArgument="r" OnClientClick="return confirm('Are you sure to Reject this Quotation?')" />
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button_color action gray"
                            CommandName="Approve" CommandArgument="a" OnClientClick="return confirm('Are you sure to Approve this Quotation?')" />
                    </div>
                    <asp:Button ID="btnPopUp1" runat="server" BackColor="#f8f8f8" BorderStyle="None"
                        BorderWidth="0px" CommandName="Select" />
                </div>
            </div>
            <div  style=" position:absolute; top:1000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp1"
                    PopupControlID="Panel2" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                        <div class="box-content">
                            <div class="box-header well">
                                <h2>
                                    Contractor Invoice</h2>
                            </div>
                            <div>
                                <asp:GridView ID="gvInvoiceItems" runat="server" AutoGenerateColumns="false" AllowPaging="true"
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
                                        <asp:TemplateField HeaderText="Item Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCatg" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NumberOfUnit" HeaderText="Number Of Unit" />
                                        <asp:BoundField DataField="UnitIssued" HeaderText="Actual Number Of Unit" />
                                        <asp:BoundField DataField="BilledUnit" HeaderText="Order Number Of Unit" />
                                        <asp:BoundField DataField="PerUnitCost" HeaderText="Per Unit Cost (INR)" />
                                        <asp:BoundField DataField="PerUnitDiscount" HeaderText="Per Unit Discount (%)" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="Button_align">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button_color action red" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
