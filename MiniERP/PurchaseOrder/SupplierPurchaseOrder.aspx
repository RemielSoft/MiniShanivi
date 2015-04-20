<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="SupplierPurchaseOrder.aspx.cs" Inherits="MiniERP.PurchaseOrder.SupplierPurchaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="../Parts/SupplierItem.ascx" TagName="SupplierItem" TagPrefix="uc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel runat="server" HeaderText="Supplier Purchase Order" ID="TabPanel1">
            <ContentTemplate>
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            Supplier Purchase Order</h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td colspan="2" class="center">
                                        <asp:Button ID="Button6" runat="server" Text="Search Quotation" CssClass="button_color action" />
                                    </td>
                                    <td colspan="2" class="center">
                                        <div style="padding-right: 200px">
                                            <asp:Button ID="Button1" runat="server" Text="View Purchase Order" CssClass="button_color action" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Purchase Order Title
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtQuotationName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="center">
                                        Supplier Name
                                    </td>
                                    <td class="center">
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                            <asp:ListItem>--select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Order Date
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextOrderDate" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="ImgBtn1" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextOrderDate"
                                            PopupButtonID="ImgBtn1" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="center">
                                        Remark
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Upload Document
                                    </td>
                                    <td colspan="3" class="center">
                                        <asp:FileUpload ID="FileUpload1" CssClass="upload" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Delivery Description
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="textDeliveryDate1" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td class="center">
                                        Valid Till Date(Closing Date)
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="textValidTillDate" runat="server" ReadOnly="True"></asp:TextBox>
                                         <asp:LinkButton ID="ImageButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton> 
                           <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" TargetControlID="textValidTillDate"
                                        PopupButtonID="ImageButton2">
                           </asp:CalendarExtender> 
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <hr />
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        Quotation Title/Number
                                    </td>
                                    <td class="center">
                                        <asp:Label ID="Label2" runat="server" Text="POrder-title-1"></asp:Label>
                                    </td>
                                    <td class="center">
                                        Quotation Date
                                    </td>
                                    <td class="center">
                                        <asp:Label ID="Label1" runat="server" Text="02/01/2012"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class="mGrid">
                            <tr>
                                <th style="width: 60px">
                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                    Select
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
                                    <asp:CheckBox ID="chk1" runat="server" />
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
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
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
                        <div class="Button_align">
                            <asp:Button ID="btnAdd1" runat="server" Text="Add" CssClass="button_color action" />
                        </div>
                    </div>
                </div>
                <%--<uc1:SupplierItem ID="SupplierItem1" runat="server" />--%>
                <br />
                <div class="Button_align">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button_color action red" />
                    <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft" CssClass="button_color action green" />
                </div>
                <div>
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="panel4"
                        PopupDragHandleControlID="PopupMenu" TargetControlID="Button6">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="panel4" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                          <div class="btnclosepopup"><asp:Button ID="Button9" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
                        </div>
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup" ScrollBars="Vertical">
                        <div class="box span12">
                            <div class="box-header well">
                                <h2>
                                    View Supplier Quotation</h2>
                            </div>
                            <div class="box-content">
                                <table class="table table-bordered table-striped table-condensed" style="border: 2px solid green">
                                    <tbody>
                                        <tr>
                                            <td colspan="4" style="background-color: #feb012; color: #000; text-align: center;
                                                font-weight: bold;">
                                                Search Criteria
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Supplier Name
                                            </td>
                                            <td class="center">
                                                <asp:DropDownList ID="DropDownList4" runat="server">
                                                    <asp:ListItem>--select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="center">
                                            </td>
                                            <td class="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                From Date
                                            </td>
                                            <td class="center">
                                                <asp:TextBox ID="TextFromDate" runat="server"></asp:TextBox>
                                               <asp:LinkButton ID="ImgBtn" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                                <asp:CalendarExtender ID="CalExt" runat="server" Format="dd/MM/yyyy" TargetControlID="TextFromDate"
                                                    PopupButtonID="ImgBtn" Enabled="True">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td class="center">
                                                To Date
                                            </td>
                                            <td class="center">
                                                <asp:TextBox ID="TextToDate" runat="server"></asp:TextBox>
                                                <asp:LinkButton ID="ImageButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TextToDate"
                                                    PopupButtonID="ImageButton1" Enabled="True">
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
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td>
                                                Quotation Title/Number
                                            </td>
                                            <td colspan="3" style="padding-right: 250px;">
                                                <asp:DropDownList ID="DropDownList5" runat="server">
                                                    <asp:ListItem>--select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Quotation Number
                                            </td>
                                            <td colspan="3">
                                                <div style="padding-right: 240px;">
                                                    <asp:Button ID="Button4" runat="server" Text="Search" CssClass="button_color action" />
                                                </div>
                                                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Quotation Date
                                            </td>
                                            <td colspan="3" class="center">
                                                <asp:Label ID="Label3" runat="server" Text="03/01/2012"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
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
                                            <tr>
                                                <td colspan="9" style="text-align: right; font-weight: bold;">
                                                    Total
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox12" Text="292,000 INR" CssClass="width70" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </tbody>
                                </table>
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td class="center">
                                                Service Tax
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label4" runat="server" Text="2"></asp:Label>%
                                            </td>
                                            <td class="center">
                                                VAT
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label5" runat="server" Text="3"></asp:Label>%
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                CST (with C Form)
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label6" runat="server" Text="--"></asp:Label>%
                                            </td>
                                            <td class="center">
                                                CST (Without C Form)
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label7" runat="server" Text="--"></asp:Label>%
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Freight
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label8" runat="server" Text="40 %"></asp:Label>
                                            </td>
                                            <td class="center">
                                            </td>
                                            <td class="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Total Discount
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="879 "></asp:Label>
                                                INR
                                            </td>
                                            <td>
                                                Total Net Value
                                            </td>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" Text="306,600"></asp:Label>
                                                INR
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <div class="Button_align">
                                    <asp:Button ID="Button10" runat="server" Text="Submit" CssClass="button_color action" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    </asp:Panel>
                </div>
                <div>
                    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                        DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="panel3"
                        PopupDragHandleControlID="PopupMenu" TargetControlID="Button1">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                          <div class="btnclosepopup"><asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
                        </div>
                    <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                        <div class="box span12">
                            <div class="box-header well">
                                <h2>
                                    View Supplier Purchase Order</h2>
                            </div>
                            <div class="box-content">
                                <table class="table table-bordered table-striped table-condensed" style="border: 2px solid green">
                                    <tbody>
                                        <tr>
                                            <td colspan="4" style="background-color: #feb012; color: #000; text-align: center;
                                                font-weight: bold;">
                                                Search Criteria
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Supplier Name
                                            </td>
                                            <td class="center">
                                                <asp:DropDownList ID="DropDownList3" runat="server">
                                                    <asp:ListItem>--select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="center">
                                            </td>
                                            <td class="center">
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
                                                <asp:LinkButton ID="ImgBtn6" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="TextToDate1"
                                                    PopupButtonID="ImgBtn6" Enabled="True">
                                                </asp:CalendarExtender>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <div class="Button_align">
                                    <asp:Button ID="Button2" runat="server" Text="Go" CssClass="button_color  go" />
                                </div>
                                <br />
                                <br />
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td class="center">
                                                Purchase Order Title/Number
                                            </td>
                                            <td colspan="3" class="center" style="padding-right: 250px;">
                                                <asp:DropDownList ID="DropDownList9" runat="server">
                                                    <asp:ListItem>--select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Purchase Order Number
                                            </td>
                                            <td colspan="3">
                                                <div style="padding-right: 200px;">
                                                    <asp:Button ID="Button5" runat="server" Text="Search" CssClass="button_color action" />
                                                </div>
                                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Purchase Order Date
                                            </td>
                                            <td colspan="3" class="center">
                                                <asp:Label ID="Label10" runat="server" Text="03/01/2012"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-bordered table-striped table-condensed">
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
                                        <tr>
                                            <td colspan="8" style="text-align: right; font-weight: bold;">
                                                Total
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox8" Text="292,000 INR" CssClass="width70" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </table>
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td class="center">
                                                Service Tax
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label11" runat="server" Text="2"></asp:Label>%
                                            </td>
                                            <td class="center">
                                                VAT
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label12" runat="server" Text="3"></asp:Label>%
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                CST (with C Form)
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label13" runat="server" Text="--"></asp:Label>%
                                            </td>
                                            <td class="center">
                                                CST (Without C Form)
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label14" runat="server" Text="--"></asp:Label>%
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Freight
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label17" runat="server" Text="40 %"></asp:Label>
                                            </td>
                                            <td class="center">
                                            </td>
                                            <td class="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Total Discount
                                            </td>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" Text="879 "></asp:Label>
                                                INR
                                            </td>
                                            <td>
                                                Total Net Value
                                            </td>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" Text="306,600"></asp:Label>
                                                INR
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <div class="Button_align">
                                    <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="button_color action" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="Delivery Schedule" ID="TabPanel2">
            <ContentTemplate>
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            Delivery Schedule</h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        Item
                                    </td>
                                    <td class="center">
                                        <asp:DropDownList ID="DropDownList6" runat="server">
                                            <asp:ListItem>--select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="center">
                                        Item Quantity
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox31" CssClass="width80" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Delivery Date
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextDeliveryDate" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="ImgBtn5" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="TextDeliveryDate"
                                            PopupButtonID="ImgBtn5" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="center">
                                    </td>
                                    <td class="center">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div class="Button_align">
                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CssClass="button_color action red" />
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button_color action" />
                        </div>
                    </div>
                </div>
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
                        <th>
                            Action
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
                        <td>
                            <asp:Image ID="Image3" runat="server" CssClass="button icon145 " />
                            <asp:Image ID="Image4" runat="server" CssClass="button icon186 " />
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
                        <td>
                            <asp:Image ID="Image5" runat="server" CssClass="button icon145 " />
                            <asp:Image ID="Image6" runat="server" CssClass="button icon186 " />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Payment Terms">
            <ContentTemplate>
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            Payment Terms</h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        Payment Type
                                    </td>
                                    <td class="center">
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                            <asp:ListItem>--select--</asp:ListItem>
                                            <asp:ListItem>Advance</asp:ListItem>
                                            <asp:ListItem>After Days</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="center">
                                        Number Of Days
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div class="Button_align">
                            <asp:Button ID="btnAdd3" runat="server" Text="Add" CssClass="button_color action" />
                        </div>
                    </div>
                </div>
                <table class="mGrid">
                    <tr>
                        <th>
                            Payment Type
                        </th>
                        <th>
                            Number Of Days
                        </th>
                        <th>
                            Action
                        </th>
                    </tr>
                    <tr>
                        <td>
                            Advance
                        </td>
                        <td>
                            --
                        </td>
                        <td>
                            <asp:Image ID="Image7" runat="server" CssClass="button icon145 " />
                            <asp:Image ID="Image8" runat="server" CssClass="button icon186 " />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            After Days
                        </td>
                        <td>
                            15
                        </td>
                        <td>
                            <asp:Image ID="Image1" runat="server" CssClass="button icon145 " />
                            <asp:Image ID="Image2" runat="server" CssClass="button icon186 " />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Terms & Condition">
            <ContentTemplate>
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            Terms & Condition</h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <table class="mGrid">
                                    <tr>
                                        <th style="width: 60px;">
                                            <asp:CheckBox ID="CheckBox3" runat="server" />
                                            Select
                                        </th>
                                        <th>
                                            Terms
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox4" runat="server" />
                                        </td>
                                        <td>
                                            Terms-1
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox5" runat="server" />
                                        </td>
                                        <td>
                                            Terms-2
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox6" runat="server" />
                                        </td>
                                        <td>
                                            Terms-3
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox7" runat="server" />
                                        </td>
                                        <td>
                                            Terms-4
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox8" runat="server" />
                                        </td>
                                        <td>
                                            Terms-5
                                        </td>
                                    </tr>
                                </table>
                            </tbody>
                        </table>
                        <div class="Button_align">
                            <asp:Button ID="btnAddTerm" runat="server" Text="Add" CssClass="button_color action" /></div>
                    </div>
                    <br />
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td>
                                    Terms
                                </td>
                                <td colspan="3">
                                    <div style="padding-right: 350px">
                                        <asp:Button ID="Button7" runat="server" Text="Add" CssClass="button_color action" />
                                    </div>
                                    <asp:TextBox ID="TextBox4" Width="200px" Text="Custom Terms-6 " runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
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
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
