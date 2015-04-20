<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ContractorPurchaseOrder.aspx.cs" Inherits="MiniERP.PurchaseOrder.ContractorPurchaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="../Parts/ContratorItem.ascx" TagName="ContratorItem" TagPrefix="uc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="MyTabStyle">
        <asp:TabPanel runat="server" HeaderText="Contractor Work Order" ID="TabPanel1">
            <ContentTemplate>
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            Contractor Work Order</h2>
                        <div style="margin: 35px 0px 0px 0px; padding-right: 300px;">
                        </div>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td colspan="2" class="center">
                                        <asp:Button ID="Button6" runat="server" Text="Search Quotation" CssClass="button_color action" />
                                    </td>
                                    <td colspan="2" class="center">
                                        <div style="padding-right: 210px">
                                            <asp:Button ID="Button1" runat="server" Text="View contractor Work Order" CssClass="button_color action" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Work Order Title
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txt_workOrderTitle" runat="server"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td class="center">
                                        Contractor Name
                                    </td>
                                    <td class="center">
                                        <asp:DropDownList ID="ddlContractorName" runat="server">
                                            <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Project
                                    </td>
                                    <td class="center">
                                        <asp:DropDownList ID="ddlProject" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="center">
                                        Work Order Date
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txt_workOrderDate" runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender_workOrderDate" runat="server" 
                                            Enabled="True" TargetControlID="txt_workOrderDate">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Upload Document
                                    </td>
                                    <td colspan="3" class="center">
                                        <asp:FileUpload ID="fup_UploadDocument" CssClass="upload" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Delivery Description
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txt_DeliveryDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td class="center">
                                        Valid Till Date(Closing Date)
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txt_closingDate" runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender_closingDate" runat="server" 
                                            Enabled="True" TargetControlID="txt_closingDate">
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
                                        Quotation Title
                                    </td>
                                    <td class="center">
                                        <asp:Label ID="Label2" runat="server" Text="Quo-title-1"></asp:Label>
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
                                    <asp:CheckBox ID="chk1" runat="server" />
                                </td>
                                <td>
                                    Wire Line contract
                                </td>
                                <td>
                                    Category A
                                </td>
                                <td>
                                    blind flange 12&quot;300#
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
                                    Pipe Line Contract
                                </td>
                                <td>
                                    Category A
                                </td>
                                <td>
                                    C.S Blind Flange 2&quot;
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
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button_color action" />
                        </div>
                    </div>
                </div>
                <div>
                    <%--<uc1:ContratorItem ID="ContratorItem1" runat="server" />--%>
                </div>
                <br />
                <div class="Button_align">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button_color action red" />
                    <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft" CssClass="button_color action green" />
                </div>
                <div>
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="panel3"
                        PopupDragHandleControlID="PopupMenu" TargetControlID="Button6">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                          <div class="btnclosepopup"><asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
                        </div>
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup" ScrollBars="Vertical">
                        <div class="box span12">
                            <div class="box-header well">
                                <h2>
                                    View Contractor Quotation</h2>
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
                                                Contractor Name
                                            </td>
                                            <td class="center">
                                                <asp:DropDownList ID="DropDownList4" runat="server">
                                                    <asp:ListItem>--select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="center">
                                                Project
                                            </td>
                                            <td class="center">
                                                <asp:DropDownList ID="DropDownList8" runat="server">
                                                    <asp:ListItem>--select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                From Date
                                            </td>
                                            <td class="center">
                                                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="center">
                                                To Date
                                            </td>
                                            <td class="center">
                                                <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
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
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button_color action" />
                                                </div>
                                                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
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
                                                    blind flange 12&quot;300#
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
                                                    C.S Blind Flange 2&quot;
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
                                                    <asp:TextBox ID="TextBox16" Text="292,000 INR" CssClass="width70" runat="server"></asp:TextBox>
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
                                                <asp:Label ID="Label4" runat="server" Text="2"></asp:Label>
                                                %
                                            </td>
                                            <td class="center">
                                                VAT
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label5" runat="server" Text="3"></asp:Label>
                                                %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                CST (with C Form)
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label6" runat="server" Text="--"></asp:Label>
                                                %
                                            </td>
                                            <td class="center">
                                                CST (Without C Form)
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label7" runat="server" Text="--"></asp:Label>
                                                %
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
                                                <asp:Label ID="Label19" runat="server" Text="879 "></asp:Label>
                                                INR
                                            </td>
                                            <td>
                                                Total Net Value
                                            </td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" Text="306,600"></asp:Label>
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
                        DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="panel4"
                        PopupDragHandleControlID="PopupMenu" TargetControlID="Button1">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="panel4" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                          <div class="btnclosepopup"><asp:Button ID="Button9" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
                        </div>
                    <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                        <div class="box span12">
                            <div class="box-header well">
                                <h2>
                                    View Contractor Work Order
                                </h2>
                            </div>
                            <div class="box-content">
                                <table class="table table-bordered table-striped table-condensed" style="border: 2px solid green">
                                    <tbody>
                                        <tr>
                                            <td colspan="4" style="background-color: #feb012; color: #000; text-align: center;
                                                font-weight: bold;">
                                                Search Criteria
                                            </td>
                                            <tr>
                                                <td class="center">
                                                    Contractor Name
                                                </td>
                                                <td class="center">
                                                    <asp:DropDownList ID="DropDownList6" runat="server">
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
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="center">
                                                    To Date
                                                </td>
                                                <td class="center">
                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
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
                                            <td>
                                                Work Order Title/Number
                                            </td>
                                            <td colspan="3" style="padding-right: 250px;">
                                                <asp:DropDownList ID="DropDownList9" runat="server">
                                                    <asp:ListItem>--select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Work Order Number
                                            </td>
                                            <td colspan="3">
                                                <div style="padding-right: 240px;">
                                                    <asp:Button ID="Button4" runat="server" Text="Search" CssClass="button_color action" />
                                                </div>
                                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                Work Order Date
                                            </td>
                                            <td colspan="3" class="center">
                                                <asp:Label ID="Label10" runat="server" Text="03/01/2012"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
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
                                            blind flange 12&quot;300#
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
                                            C.S Blind Flange 2&quot;
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
                                            <asp:TextBox ID="TextBox5" Text="292,000 INR" CssClass="width70" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <table class="table table-bordered table-striped table-condensed">
                                    <tbody>
                                        <tr>
                                            <td class="center">
                                                Service Tax
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label11" runat="server" Text="2"></asp:Label>
                                                %
                                            </td>
                                            <td class="center">
                                                VAT
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label12" runat="server" Text="3"></asp:Label>
                                                %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                CST (with C Form)
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label13" runat="server" Text="--"></asp:Label>
                                                %
                                            </td>
                                            <td class="center">
                                                CST (Without C Form)
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="Label14" runat="server" Text="--"></asp:Label>
                                                %
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
        <asp:TabPanel runat="server" HeaderText="Delivery Schedule" ID="TabPanel4">
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
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                            <asp:ListItem>--select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="center">
                                        Item Quantity
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox2" CssClass="width80" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Delivery Date
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
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
                            <asp:Button ID="Button5" runat="server" Text="Add" CssClass="button_color action" />
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
                            <asp:Button ID="btnAdd1" runat="server" Text="Add" CssClass="button_color action" /></div>
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
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Terms & Conditions">
            <ContentTemplate>
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            Issue Material</h2>
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
                                    <asp:TextBox ID="TextBox9" Width="200px" Text="Custom Terms-6 " runat="server"></asp:TextBox>
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
