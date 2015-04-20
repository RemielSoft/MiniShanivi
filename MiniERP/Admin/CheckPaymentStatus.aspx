<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" CodeBehind="CheckPaymentStatus.aspx.cs" Inherits="MiniERP.Invoice.SupplierBillAmount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box span12" style="min-height: 100px">
                <tr>
                    <td>From Date
                    </td>
                    <td class="center">
                        <asp:TextBox ID="txtFrmDate" CssClass="TextBox" Enabled="true" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="lnk_btn_calander_closing" runat="server" ToolTip="Select Date"
                            CssClass="Calender icon175"></asp:LinkButton>
                        <ajaxtoolkit:CalendarExtender ID="cal_ext_From_date" CssClass="calbox" runat="server"
                            Format="MM/dd/yyyy" TargetControlID="txtFrmDate" PopupButtonID="lnk_btn_calander_closing">
                        </ajaxtoolkit:CalendarExtender>
                    </td>
                    <td>To Date
                    </td>
                    <td class="center">
                        <asp:TextBox ID="txttoDate" CssClass="TextBox" Enabled="true" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Select Date"
                            CssClass="Calender icon175"></asp:LinkButton>
                        <ajaxtoolkit:CalendarExtender ID="cal_ext_To_date" CssClass="calbox" runat="server"
                            Format="MM/dd/yyyy" TargetControlID="txttoDate" PopupButtonID="LinkButton1">
                        </ajaxtoolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="center" style="vertical-align: middle; text-align: center;" rowspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                            ToolTip="Search" OnClick="btnSearch_Click"></asp:Button>
                    </td>
                </tr>
            </div>
            <div class="Button_align">
                <asp:Button ID="LinkBtnExport" runat="server" Text="Export To Excel" CssClass="button_color action green"
                    OnClick="LinkBtnExport_Click" />
            </div>
            <div>
                <asp:GridView ID="gvBilledAmount" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                    EmptyDataText="No-Items Available">
                    <Columns>

                        <asp:TemplateField HeaderText="Supplier Order Number">
                            <ItemTemplate>
                                <asp:Label ID="lblorderNo" runat="server" Text='<%#Eval("SupplierOrderNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("SupplierName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Date">
                            <ItemTemplate>
                                <asp:Label ID="lblOrderDate" runat="server" Text='<%#Eval("OrderDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Order Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbltotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount Pending For Approval <br /> (Invoice Recived)">
                            <ItemTemplate>
                                <asp:Label ID="lblPendingAmount" runat="server" Text='<%#Eval("PendingAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblApprovedAmount" runat="server" Text='<%#Eval("ApprovedAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Billed Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblBillAmount" runat="server" Text='<%#Eval("ToBillAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LinkBtnExport" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
