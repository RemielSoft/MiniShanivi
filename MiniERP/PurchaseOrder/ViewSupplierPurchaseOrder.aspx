<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewSupplierPurchaseOrder.aspx.cs" Inherits="MiniERP.PurchaseOrder.ViewSupplierPurchaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Parts/ViewOrder.ascx" TagName="SupplierItem" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <uc1:SupplierItem ID="ViewContractororder" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
  
  
   <%-- <div class="box span12">
        <div class="box-header well">
            <h2>
                View Supplier Purchase Order</h2>
        </div>
        <div class="box-content">
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                        <td class="center">
                            Supplier Name
                        </td>
                        <td colspan="3" class="center">
                            <asp:DropDownList ID="ddlSupplier" Width="147px" runat="server">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="center">
                            From Date
                        </td>
                        <td class="center">
                            <asp:TextBox ID="TextFromDate1" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="ImgBtn3" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TextFromDate1"
                                PopupButtonID="ImgBtn3" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td class="center">
                            To Date
                        </td>
                        <td class="center">
                            <asp:TextBox ID="TextToDate1" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImgBtn6" runat="server" ToolTip="Select Date" CssClass="Calender icon175" />
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="TextToDate1"
                                PopupButtonID="ImgBtn6" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <div class="Button_align">
                <asp:Button ID="btnCancel" runat="server" Text="Go" CssClass="button_color  go" />
            </div>
            <br />
            <br />
            <br />
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                        <td class="center" width="154px">
                            Purchase Order Number
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                            ToolTip="Search"></asp:LinkButton>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table class="mGrid">
                <tr>
                    <th>
                        Purchase Order Number
                    </th>
                    <th>
                        Purchase Order Date
                    </th>
                    <th>
                        Supplier Name
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
                        Packaging (INR)
                    </th>
                    <th>
                        Discount (Fix/Percentage)
                    </th>
                    <th>
                        Total Discount (%/INR)
                    </th>
                    <th>
                        Total Net Value (INR)
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="btnPopUp" runat="server" Text="SCPL/S/01/2013/1" CommandName="cmdEdit"></asp:LinkButton>
                    </td>
                    <td>
                        06/Jan/2013
                    </td>
                    <td>
                        Ajay
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
                        5,012
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
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="lnkPrint" CssClass="button icon153 "
                            ToolTip="Print"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton8" runat="server" Text="SCPL/S/01/2013/2" CommandName="cmdEdit"></asp:LinkButton>
                    </td>
                    <td>
                        08/Jan/2013
                    </td>
                    <td>
                        Sumit
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
                        7,012
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
                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="lnkPrint" CssClass="button icon153 "
                            ToolTip="Print"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div  style=" position:absolute; top:1000px;">
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
            PopupControlID="Panel2" BackgroundCssClass="modalBackground" DropShadow="true"
            Enabled="True" PopupDragHandleControlID="PopupMenu">
        </cc1:ModalPopupExtender>
        <div>
            <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                <div class="box-content">
                    <h3>
                        Supplier Purchase Order</h3>
                    <table class="mGrid">
                        <tr>
                            <th>
                                Activity Description
                            </th>
                            <th>
                                Item Category
                            </th>
                            <th>
                                Item
                            </th>
                            <th>
                                Item Model
                            </th>
                            <th>
                                Make(Brand)
                            </th>
                            <th>
                                Specification
                            </th>
                            <th>
                                Number Of Unit
                            </th>
                            <th>
                                Per Unit Cost (INR)
                            </th>
                            <th>
                                Per Unit Discount (%)
                            </th>
                            <th>
                                Total Amount (INR)
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Wire Line contract
                            </td>
                            <td>
                                Category A
                            </td>
                            <td>
                                blind flange 12"300#
                            </td>
                            <td>
                                Aluminum
                            </td>
                            <td>
                                HKTDC
                            </td>
                            <td>
                                abc
                            </td>
                            <td>
                                20
                            </td>
                            <td>
                                10,000
                            </td>
                            <td>
                                3%
                            </td>
                            <td>
                                194,000
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Pipe Line Contract
                            </td>
                            <td>
                                Category A
                            </td>
                            <td>
                                C.S Blind Flange 2"
                            </td>
                            <td>
                                Plastic
                            </td>
                            <td>
                                Deerma Tech
                            </td>
                            <td>
                                mkv
                            </td>
                            <td>
                                25
                            </td>
                            <td>
                                4,000
                            </td>
                            <td>
                                2%
                            </td>
                            <td>
                                98,000
                            </td>
                        </tr>
                    </table>
                    <h3>
                        Delivery Schedule</h3>
                    <table class="mGrid">
                        <tr>
                            <th>
                                Activity Description
                            </th>
                            <th>
                                Delivery Date
                            </th>
                        </tr>
                        <tr>
                            <td>
                                abcd
                            </td>
                            <td>
                                23/03/2013
                            </td>
                        </tr>
                        <tr>
                            <td>
                                mkvs
                            </td>
                            <td>
                                23/02/2013
                            </td>
                        </tr>
                    </table>
                    <h3>
                        Payment Terms</h3>
                    <table class="mGrid">
                        <tr>
                            <th>
                                Payment Type
                            </th>
                            <th>
                                Number Of Days
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Advance
                            </td>
                            <td>
                                --
                            </td>
                        </tr>
                        <tr>
                            <td>
                                After Days
                            </td>
                            <td>
                                15
                            </td>
                        </tr>
                    </table>
                    <h3>
                        Terms & Conditions</h3>
                    <table class="mGrid">
                        <tr>
                            <th style="width: 60px;">
                                S. No.
                            </th>
                            <th>
                                Terms
                            </th>
                        </tr>
                        <tr>
                            <td>
                                1
                            </td>
                            <td>
                                Terms-1
                            </td>
                        </tr>
                        <tr>
                            <td>
                                2
                            </td>
                            <td>
                                Terms-2
                            </td>
                        </tr>
                        <tr>
                            <td>
                                3
                            </td>
                            <td>
                                Terms-3
                            </td>
                        </tr>
                        <tr>
                            <td>
                                4
                            </td>
                            <td>
                                Terms-4
                            </td>
                        </tr>
                        <tr>
                            <td>
                                5
                            </td>
                            <td>
                                Terms-5
                            </td>
                        </tr>
                        <tr>
                            <td>
                                6
                            </td>
                            <td>
                                Custom Terms-6
                            </td>
                        </tr>
                    </table>
                    <div class="Button_align">
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button_color action red" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>--%>
</asp:Content>
