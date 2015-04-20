<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewCompanyWorkOrder.aspx.cs" Inherits="MiniERP.PurchaseOrder.ViewCompanyWorkOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="box span12">
        <div class="box-header well">
            <h2>
                View Company Work Order</h2>
        </div>
        <div class="box-content">
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                        <td class="center">
                            From Date
                        </td>
                        <td class="center">
                            <asp:TextBox ID="TextFromDate1" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="ImageButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TextFromDate1"
                                PopupButtonID="ImageButton1" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td class="center">
                            To Date
                        </td>
                        <td class="center">
                            <asp:TextBox ID="TextToDate1" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="ImageButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TextToDate1"
                                PopupButtonID="ImageButton2" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <div class="Button_align">
                <asp:Button ID="btnGO" runat="server" Text="Go" CssClass="button_color action green" /></div>
            <br />
            <br />
            <table class="table table-bordered table-striped table-condensed searchbg">
                <tbody>
                    <tr>
                        <td class="center" width="22%">
                            Contract Number
                        </td>
                        <td class="center">
                            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                ToolTip="Search"></asp:LinkButton>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table class="mGrid">
                <tr>
                    <th>
                        Contract Number
                    </th>
                    <th>
                        Contract Date
                    </th>
                    <th>
                        Work Order Description
                    </th>
                    <th>
                        Service tax (%)
                    </th>
                    <th>
                        Vat (%)
                    </th>
                    <th>
                        CST (with C Form) (%)
                    </th>
                    <th>
                        CST (Without C Form) (%)
                    </th>
                    <th>
                        Frieght (INR)
                    </th>
                    <th>
                        Discount (Fix /Percentage)
                    </th>
                    <th>
                        Total Discount (%/INR)
                    </th>
                    <th>
                        Total Net Value (INR)
                    </th>
                    <th width="70px">
                        Action
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="btnPopUp" runat="server" Text="Contract-1" CommandName="cmdEdit"></asp:LinkButton>
                    </td>
                    <td>
                        05/01/2012
                    </td>
                    <td>
                        abcdf
                    </td>
                    <td>
                        3
                    </td>
                    <td>
                        2
                    </td>
                    <td>
                        0
                    </td>
                    <td>
                        0
                    </td>
                    <td>
                        4,032
                    </td>
                    <td>
                        Fix
                    </td>
                    <td>
                        879
                    </td>
                    <td>
                        306,600
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="cmdEdit" CssClass="button icon145 "
                            ToolTip="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="lnkDelete" CssClass="button icon186 "
                            ToolTip="Delete"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="lnkPrint" CssClass="button icon153 "
                            ToolTip="Print"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton8" runat="server" Text="Contract-2" CommandName="cmdEdit"></asp:LinkButton>
                    </td>
                    <td>
                        03/Jan/2012
                    </td>
                    <td>
                        mkvnps
                    </td>
                    <td>
                        3
                    </td>
                    <td>
                        2
                    </td>
                    <td>
                        0
                    </td>
                    <td>
                        0
                    </td>
                    <td>
                        6,032
                    </td>
                    <td>
                        Percentage
                    </td>
                    <td>
                        2
                    </td>
                    <td>
                        306,600
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="cmdEdit" CssClass="button icon145 "
                            ToolTip="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="lnkDelete" CssClass="button icon186 "
                            ToolTip="Delete"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="lnkPrint" CssClass="button icon153 "
                            ToolTip="Print"></asp:LinkButton>
                    </td>
                </tr>
            </table>
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
                        <h3>
                            Company Work Order</h3>
                        <table class="mGrid">
                            <tr>
                                <th>
                                    Work Order Number
                                </th>
                                <th>
                                    Amount (INR)
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    WO-001
                                </td>
                                <td>
                                    200,000
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    WO-002
                                </td>
                                <td>
                                    300,000
                                </td>
                            </tr>
                        </table>
                        <h3>
                            Delivery Schedule</h3>
                        <table class="mGrid">
                            <tr>
                                <th>
                                    Item
                                </th>
                                <th>
                                    Item Quantity
                                </th>
                                <th>
                                    Delivery Date
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    Section Pipe
                                </td>
                                <td>
                                    3
                                </td>
                                <td>
                                    23/03/2013
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Roof Sheet
                                </td>
                                <td>
                                    4
                                </td>
                                <td>
                                    23/02/2013
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
