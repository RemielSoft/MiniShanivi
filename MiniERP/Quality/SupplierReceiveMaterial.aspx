<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="SupplierReceiveMaterial.aspx.cs" Inherits="MiniERP.StockMaterial.ReceiveMaterial"
    ViewStateMode="Enabled" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div>
        <script type="text/javascript" language="javascript">
            function AllowOnlyNumeric(e) {
                if (window.event) // IE 
                {
                    if (((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) & e.keyCode != 46) {
                        event.returnValue = false;
                        alert("Enter Only Numeric values");
                        return false;
                    }
                }
                else { // Fire Fox
                    if (((e.which < 48 || e.which > 57) & e.which != 8) & e.which != 46) {
                        e.preventDefault();
                        alert("Unit Required allows only numeric value up to 2 decimal places!");
                        return false;
                    }
                }
            }
        </script>
    </div>
    <div class="box span12">
        <div class="box-header well">
            <h2>Receive Material Supplier Purchase Order</h2>
        </div>
        <div class="box-content">
            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="LinkSearch">
                <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">Supplier Purchase Order
                            </td>
                            <td class="center" width="55%">
                                <asp:TextBox ID="txtOrderNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                <%-- <asp:CheckBox ID="CheckBox4" Text="Against Company Work Order" runat="server" />--%>
                                <asp:LinkButton ID="LinkSearch" runat="server" CausesValidation="false" CommandName="cmdEdit"
                                    CssClass="Search icon198" ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>
                            </td>
                            <td class="center" width="30%">
                                <asp:DropDownList ID="ddlSupplierReceiveMaterial" CssClass="TextBox" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="center" style="width: 75%">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                                    ToolTip="Search" OnClick="btnSearch_Click"></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlRecieveMetarial" runat="server">
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">Supplier Purchase Order
                            </td>
                            <td class="center" width="25%">
                                <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="center" width="25%">Purchase Order Date
                            </td>
                            <td class="center" width="25%">
                                <asp:Label ID="lblOrderDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">Delivery Challan Number/Supplier
                            </td>
                            <td class="center" width="25%">
                                <asp:TextBox ID="txtChallanNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdfSupplierId" runat="server" />
                            </td>
                            <td class="center" width="25%">Receive Material Date
                            </td>
                            <td class="center" width="25%">
                                <asp:TextBox ID="txtRMDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:LinkButton ID="ImageButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtRMDate" PopupButtonID="ImageButton3">
                                </ajaxtoolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="center" width="25%">
                                <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                            </td>
                            <td colspan="3" class="center" width="75%">
                                <div>
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tr>
                                            <td class="center" id="ajaxupload" runat="server">
                                                <asp:FileUpload ID="FileUpload_Control" runat="server" />
                                                <asp:Button ID="btn_upload" runat="server" Text="Upload" CausesValidation="false"
                                                    OnClick="btn_upload_Click" />
                                            </td>
                                            <td class="center">
                                                <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" Height="80px" Width="100%">
                                                    <div class="box-content">
                                                        <asp:GridView ID="gv_documents" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" EmptyDataText="No-Documents Uploaded."
                                                            AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="gv_documents_RowCommand"
                                                            Width="250px">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="File">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn_file" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                                                                            CommandName="OpenFile" Text='<%#Eval("Original_Name")%>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandName="FileDelete"
                                                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                                            ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <%-- MAin Grid Starts  --%>
                <asp:GridView ID="gvMainGrid" runat="server" ShowFooter="true" AutoGenerateColumns="False" AllowPaging="false"
                    CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No-Items Available" OnRowDataBound="gvMainGrid_RowDataBound"
                    AlternatingRowStyle-CssClass="alt">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="56px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                    OnCheckedChanged="chbxSelectAll_Click" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_Click" />
                                  <asp:HiddenField ID="hdfSupplierPOMappingId" runat="server" Value='<%# Eval("SupplierPurchaseOrderId")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdfItemId" runat="server" Value='<%# Eval("ItemId")%>' />
                                <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdfSpecificationId" runat="server" Value='<%# Eval("ItemSpecificationId")%>' />
                                <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ItemSpecification") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Level">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("ItemCategoryName") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Unit Measurement">
                            <ItemTemplate>

                                <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("UnitMeasurement") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Quantity" ItemStyle-CssClass="taright">
                            <ItemTemplate>
                                <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("QuantityDemand") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Received" ItemStyle-CssClass="taright">
                            <ItemTemplate>
                                <asp:Label ID="lblItemRecieve" runat="server" Text='<%#Eval("IssuedQuantity") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Left" ItemStyle-CssClass="taright">
                            <ItemTemplate>
                                <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("AvailableQuantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <%-- MAin Grid Ends  --%>
                <asp:GridView ID="gvSupplier" runat="server" ShowFooter="true" AutoGenerateColumns="False" AllowPaging="false"
                    CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No-Items Available"
                    AlternatingRowStyle-CssClass="alt" OnRowDataBound="gvSupplier_RowDataBound" OnPreRender="gvSupplier_PreRender">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No"> 
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSNo" Text='<%#Container.DataItemIndex +1%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdfItemId" runat="server" Value='<%# Eval("Item.ItemId")%>' />
                                <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdfSpecificationId" runat="server" Value='<%# Eval("Item.ModelSpecification.ModelSpecificationId")%>' />
                                <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                            </ItemTemplate>
                            <%--<FooterTemplate>
                                <asp:Label runat="server" ID="lblItemQuantity" Font-Bold="true" Text="Item Quantity"></asp:Label>
                            </FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Level" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                            </ItemTemplate>
                            <%-- <FooterTemplate>
                                <asp:Label runat="server" ID="lblItemQuantityValue" Font-Bold="true"></asp:Label>
                            </FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Store">
                            <ItemTemplate>
                                <asp:Label ID="lblStore" runat="server" Text='<%#Eval("Item.ModelSpecification.Store.StoreName") %>'></asp:Label>
                                <asp:HiddenField ID="hdnfStore" runat="server" Value='<%#Eval("Item.ModelSpecification.Store.StoreId") %>'></asp:HiddenField>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate>
                                <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                <asp:HiddenField ID="hdnfBrand" runat="server" Value='<%#Eval("Item.ModelSpecification.Brand.BrandId") %>'></asp:HiddenField>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Measurement">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdfUnitMeasurementId" runat="server" Value='<%# Eval("Item.ModelSpecification.UnitMeasurement.Id")%>' />
                                <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                            </ItemTemplate>
                            <%--  <FooterTemplate>
                                <asp:Label runat="server" ID="lblItemLeft" Font-Bold="true" Text="Item Left"></asp:Label>
                            </FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Quantity" ItemStyle-CssClass="taright" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Received" ItemStyle-CssClass="taright">
                            <ItemTemplate>
                                <asp:Label ID="lblItemRecieve" runat="server" Text='<%#Eval("UnitIssued") %>'></asp:Label>
                            </ItemTemplate>
                            <%--<FooterTemplate>
                                <div style="text-align: right">
                                    <asp:Label runat="server" Font-Bold="true" ID="lblItemLeftValue"></asp:Label>
                                </div>
                            </FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Left" ItemStyle-CssClass="taright" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="Button_align">
                    <asp:Button ID="btnAdd1" runat="server" Text="Add" CssClass="button_color action"
                        OnClick="btnAdd1_Click" />
                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="button_color action" OnClick="btnExportToExcel_Click" />
                </div>
                <br />
                <div style="color: Red">
                    <asp:Literal ID="ltrl_err_msg" runat="server">
                    </asp:Literal>
                </div>
                <div>
                    <asp:GridView ID="gvSupplierAdd" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowCommand="gvSupplierAdd_RowCommand" EmptyDataText="No-Items Available" OnRowDataBound="gvSupplierAdd_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Index">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Level">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make(Brand)" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Store">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlStore" runat="server" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:HiddenField ID="hdnfStore" runat="server" Value='<%#Eval("Item.ModelSpecification.Store.StoreId") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:HiddenField ID="hdnfBrand" runat="server" Value='<%#Eval("Item.ModelSpecification.Brand.BrandId") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Quantity" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Quantity" HeaderStyle-Width="55px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRecieveQuantity" runat="server" MaxLength="10" OnTextChanged="txtRecieveQuantity_TextChanged" onkeypress="AllowOnlyNumeric(event);"
                                        Width="80px" Text='<%#Eval("ItemRequired") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Container.DataItemIndex%>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are you sure you want to Delete this item?') "
                                        ToolTip="Delete" CssClass="button icon186">
                                        <asp:Image ID="imgDelete" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="Button_align">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button_color action red"
                            OnClick="btnReset_Click" />
                        <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft" CssClass="button_color action green"
                            OnClick="btnSaveDraft_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%-- for popup Search the Issue material Genreated Contractoe Work Order Number Gridview Name=gvIssueMaterialNo--%>
    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
        CommandName="Select" />
    <div style="position: absolute; top: 3000px;">
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
            PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
            Enabled="True" PopupDragHandleControlID="PopupMenu">
        </cc1:ModalPopupExtender>
        <div>
            <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                <div class="PopUpClose">
                    <div class="btnclosepopup">
                        <asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                    </div>
                </div>
                <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                    <div>
                        <asp:GridView ID="gvViewOrder" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Record Found(s)">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged"
                                            AutoPostBack="true"></asp:RadioButton>
                                        <asp:HiddenField ID="hdfIssuematerialId" runat="server" Value='<%# Eval("SupplierQuotationId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work order No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtnQuotation" runat="server" CommandName="lnkQuotation" CommandArgument='<%#Eval("SupplierQuotationId") %>'
                                            Text='<%#Eval("SupplierQuotationNumber") %>'></asp:Label>
                                        <asp:HiddenField ID="approveStatus" runat="server" Value='<%#Eval("StatusType.Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="QuotationDate" DataFormatString="{0:MMMM d, yyyy}" HeaderText="Quotation Date" />--%>
                                <%--  <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHCName" runat="server" Text="Contractor Name"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCName" runat="server" Text='<%#Eval("ContractorName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Suppiler Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsupplierName" runat="server" Text='<%#Eval("SupplierName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingDate" runat="server" Text='<%#Bind("ClosingDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--     <asp:BoundField DataField="DeliveryDescription" HeaderText="Delivery Description" />--%>
                                <%-- <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHCNumber" runat="server" Text="Contract Number"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCNumber" runat="server" Text='<%#Eval("ContractNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                                <asp:BoundField DataField="TotalNetValue" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
