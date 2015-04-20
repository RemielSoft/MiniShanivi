<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DemandIssueVoucher.ascx.cs"
    Inherits="MiniERP.Quality.Parts.DemandIssueVoucher" ViewStateMode="Enabled" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div>
        <script type="text/javascript" language="javascript">
            function AllowOnlyNumeric(e) {
                if (window.event) // IE 
                {
                    if (((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) & e.keyCode != 46) {
                        event.returnValue = false;
                        alert("Unit Required allows only numeric value up to 2 decimal places!");
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
            <h2>Issue Demand Voucher</h2>
        </div>
        <div class="box-content">
            <asp:Panel ID="pnlSearch" runat="server">
                <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">Contractor Work Order Number
                            </td>
                            <td class="center" width="45%">
                                <asp:TextBox ID="txtContractorQuotationNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="btnSearchContractorQuotationNumber" runat="server" CssClass="Search icon198"
                                    ToolTip="Search" OnClick="btnSearchContractorQuotationNumber_Click"></asp:LinkButton>
                            </td>
                            <td class="center" width="30%">
                                <asp:DropDownList ID="ddlDemandVoucher" CssClass="TextBox" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                                    ToolTip="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlIDV" runat="server">
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">Contractor Work Order Number
                            </td>
                            <td class="center" width="25%">
                                <asp:Label ID="lblContractorQuotationNumber" runat="server"></asp:Label>
                            </td>
                            <td class="center" width="25%">Contractor Work Order Date
                            </td>
                            <td class="center" width="25%">
                                <asp:Label ID="lblContractorQuotationDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="mGriditem">
                    <asp:GridView ID="gvQuotationDetails" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No-Items Available" OnRowDataBound="gvQuotationDetails_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="56px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                        OnCheckedChanged="chbxSelectAll_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkbxQuotationDetails" runat="server" AutoPostBack="true" OnCheckedChanged="chkbxQuotationDetails_Click" />
                                    <asp:HiddenField ID="hdfContractorPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityDiscription" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfItemId" runat="server" Value='<%# Eval("Item.ItemId")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfSpecificationId" runat="server" Value='<%# Eval("Item.ModelSpecification.ModelSpecificationId")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Level">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfUnitMeasurementId" runat="server" Value='<%# Eval("Item.ModelSpecification.UnitMeasurement.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No. Of Unit" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Issued" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitIssued" runat="server" Text='<%#Eval("UnitIssued") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Left" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="Button_align">
                    <asp:Button ID="btnAddQuotationDetails" runat="server" Text="Add" CssClass="button_color action"
                        OnClick="btnAddQuotationDetails_Click" />
                </div>
                <br />
                <br />
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">Contractor Name
                            </td>
                            <td class="center" width="25%">
                                <asp:Label ID="lblContractorName" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdfContractorId" runat="server" />
                            </td>
                            <td class="center" width="25%">Contract Number
                            </td>
                            <td class="center" width="25%">
                                <asp:Label ID="lblContractNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">Material demand Date
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtMaterialDemandDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:LinkButton ID="ImageButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtMaterialDemandDate" PopupButtonID="ImageButton3">
                                </asp:CalendarExtender>
                            </td>
                            <td class="center">Remarks
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="mlttext" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="center" width="25%">
                                <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                            </td>
                            <td class="center" colspan="3" width="75%">
                                <div>
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tr>
                                            <td id="ajaxupload" runat="server" class="center">
                                                <asp:FileUpload ID="FileUpload_Control" runat="server" />
                                                <asp:Button ID="btn_upload" runat="server" CausesValidation="false" OnClick="btn_upload_Click"
                                                    Text="Upload" />
                                            </td>
                                            <td class="center">
                                                <asp:Panel ID="Panel4" runat="server" Height="80px" ScrollBars="Vertical" Width="100%">
                                                    <div class="box-content">
                                                        <asp:GridView ID="gv_documents" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="false" CellPadding="4" CssClass="mGrid" EmptyDataText="No-Documents Uploaded."
                                                            ForeColor="#333333" GridLines="None" OnRowCommand="gv_documents_RowCommand" ShowHeader="false"
                                                            Width="250px">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="File">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn_file" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                            CommandName="OpenFile" Text='<%#Eval("Original_Name")%>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="20px" HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandArgument="<%#Container.DataItemIndex%>"
                                                                            CommandName="FileDelete" CssClass="button icon186" OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                                            ToolTip="Delete"></asp:LinkButton>
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
                        </tr>
                    </tbody>
                </table>
                <hr />
                <br />
                <div style="color: Red">
                    <asp:Literal ID="ltrl_err_msg" runat="server">
                    </asp:Literal>
                </div>
                <div class="mGriditem">
                    <asp:GridView ID="gvIssueDemandVoucher" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No-Items Available" OnRowCommand="gvIssueDemandVoucher_RowCommand"
                        OnRowDataBound="gvIssueDemandVoucher_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Index">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityDiscription" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Brand" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No. Of Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Issued">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitIssued" runat="server" Text='<%#Eval("UnitIssued") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Required">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtItemRequired" runat="server" onkeypress="AllowOnlyNumeric(event);"
                                        Width="80px" Text='<%#Eval("ItemRequired") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmdDelete"
                                        CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Activity Description?') "
                                        ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="Button_align">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button_color action red"
                        OnClick="btnReset_Click" />
                    <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft" CssClass="button_color action green"
                        OnClick="btnSaveDraft_Click" />
                </div>
                <br />
                <asp:ValidationSummary ID="valsumIDV" ShowSummary="false" ShowMessageBox="true" ValidationGroup="IDV"
                    runat="server" />
            </asp:Panel>
        </div>
    </div>
</div>
<%--</ContentTemplate>
    </asp:UpdatePanel>--%>
<asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
    CommandName="Select" />

<div style="position: absolute; top: 2000px;">
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
                <asp:GridView ID="gvViewQuotation" runat="server" AutoGenerateColumns="false"
                    EmptyDataText="No Records Found !" AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged"
                                    AutoPostBack="true"></asp:RadioButton>
                                <asp:HiddenField ID="hdfDemandId" runat="server" Value='<%# Eval("ContractorQuotationId")%>' />
                                <%--CommandArgument='<%#Eval("ContractorQuotationId") %>'--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quotation Number">
                            <ItemTemplate>
                                <asp:Label ID="lbtnQuotation" runat="server" Text='<%#Eval("ContractQuotationNumber") %>'></asp:Label>
                                <asp:HiddenField ID="hdf_documnent_Id" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quotation Date">
                            <ItemTemplate>
                                <asp:Label ID="lblQuotationDate" runat="server" Text='<%#Bind("OrderDate","{0:MMMM d, yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblHCName" runat="server" Text="Contractor Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="CompanyWorkOrderNumber" HeaderText="Company Work Order Number"
                                    ItemStyle-CssClass="taright" />--%>
                        <asp:BoundField DataField="WorkOrderNumber" HeaderText="Work Order Number" ItemStyle-CssClass="taright" />
                        <asp:TemplateField HeaderText="Closing Date" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblClosingDate" runat="server" Text='<%#Bind("ClosingDate","{0:MMMM d, yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="DeliveryDescription" HeaderText="Delivery Description"
                                    HtmlEncode="false" />--%>
                        <asp:BoundField DataField="TotalNetValue" HeaderText="Total Net Value (INR)" ItemStyle-CssClass="taright" />
                        <asp:TemplateField HeaderText="Created By" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

        </asp:Panel>
    </div>
</div>


