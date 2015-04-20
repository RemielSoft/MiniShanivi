<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssuingMaterial.ascx.cs"
    Inherits="MiniERP.Quality.Parts.IssuingMaterial" ViewStateMode="Enabled" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div>
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div>
        <script type="text/javascript" language="javascript">
            function AllowOnlyNumeric(e) {
                if (window.event) // IE 
                {
                    if (((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) & e.keyCode != 46) {
                        event.returnValue = false;
                        alert("Unit Issue allows only valid numeric value up to 2 decimal places!");
                        return false;
                    }
                }
                else { // Fire Fox
                    if (((e.which < 48 || e.which > 57) & e.which != 8) & e.which != 46) {
                        e.preventDefault();
                        alert("Unit Issue allows only valid numeric value up to 2 decimal places!");
                        return false;
                    }
                }
            }
        </script>
    </div>
    <div class="box span12">
        <div class="box-header well">
            <h2>Issue Material</h2>

        </div>
        <div class="box-content">
            <asp:Panel ID="pnlSearch" runat="server">
                <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">Issue Demand Voucher Number
                            </td>
                            <td class="center" width="55%">
                                <asp:TextBox ID="txtDemandNoteNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDemandNoteNumber" runat="server" ControlToValidate="txtDemandNoteNumber"
                                    Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Issue Demand Voucher Number"
                                    ValidationGroup="IssureturnMaterial"></asp:RequiredFieldValidator>
                                <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CausesValidation="false"
                                    CssClass="Search icon198" ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>
                            </td>
                            <td class="center" width="30%">
                                <asp:DropDownList ID="ddlIssueMat" CssClass="TextBox" runat="server">
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
            <asp:Panel ID="pnlIssueMaterial" runat="server">
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">Contractor Work Order
                            </td>
                            <td class="center" width="75%" colspan="3">
                                <asp:Label ID="lblContractorWorkOrder" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="center" width="25%">
                                <asp:Label ID="lblIssueDemandVoucher1" runat="server" Text="Issue Demand Voucher"></asp:Label>
                            </td>
                            <td class="center" width="25%">
                                <asp:Label ID="lblIssueDemandVoucher" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnDemandVoucherId" runat="server" />
                            </td>
                            <td class="center" width="25%">Issue Demand Date
                            </td>
                            <td class="center" width="25%">
                                <asp:Label ID="lblIssueDemandDate" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnStatusTypeId" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="mGriditem">
                    <asp:GridView ID="gvDemandMaterial" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No-Items Available" OnRowDataBound="gvDemandMaterial_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="56px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                        OnCheckedChanged="chbxSelectAll_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkbxDemandVoucherDetails" runat="server" AutoPostBack="true" OnCheckedChanged="on_chbx_Activity_Click" />
                                    <asp:HiddenField ID="hdfContractorPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Description">
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
                                    <asp:HiddenField ID="hdfUnitMeasurementId" runat="server" Value='<%# Eval("Item.ModelSpecification.UnitMeasurement.Id")%>' />
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
                            <asp:TemplateField HeaderText="No. Of Unit" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Demanded" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitDemanded" runat="server" Text='<%#Eval("ItemRequired") %>'></asp:Label>
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
                    <asp:Button ID="btnAddDemandMaterial" runat="server" Text="Add" CssClass="button_color action"
                        OnClick="btnAddDemandMaterial_Click" />
                </div>
                <br />
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
                            <td class="center">Issue Date
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtIssueDate" Enabled="false" CssClass="TextBox" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="ImageButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtIssueDate" PopupButtonID="ImageButton3">
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
                    </tbody>
                </table>
                <hr />
                <br />
                <div style="color: Red">
                    <asp:Literal ID="ltrl_err_msg" runat="server">
                    </asp:Literal>
                </div>
                <div>
                    <asp:GridView ID="gvIssueMaterial" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No-Items Available" OnRowCommand="gvIssueMaterial_RowCommand"
                        OnRowDataBound="gvIssueMaterial_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderStyle-Width="56px">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                                OnCheckedChanged="chbxSelectAll_Click" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkbxDemandVoucherDetails" runat="server" AutoPostBack="true" OnCheckedChanged="on_chbx_Activity_Click" />
                                            <asp:HiddenField ID="hdfContractorPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Activity Description">
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
                            <asp:TemplateField HeaderText="Unit Demanded">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitDemanded" runat="server" Text='<%#Eval("UnitDemanded") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Store">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Required">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtUnitIssue" runat="server" onkeypress="AllowOnlyNumeric(event);"
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
                    <%-- <table class="table table-bordered table-striped table-condensed">
                        <tr>
                            <td>Item
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlItem"></asp:DropDownList>
                            </td>
                            <td>Specification
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlSpecification"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Brand</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlBrand"></asp:DropDownList>
                            </td>
                            <td>Unit Issued</td>
                            <td>
                                <asp:TextBox ID="txtUnitIssued" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>--%>
                </div>
                <div class="Button_align">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button_color action red"
                        OnClick="btnReset_Click" />
                    <asp:Button ID="btnSaveDraft" runat="server" Text="SaveDraft" CssClass="button_color action green" ValidationGroup="submit"
                        OnClick="btnSaveDraft_Click" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit Draft" CssClass="button_color action green"
                        OnClick="btnSubmit_Click" Visible="false" />
                </div>
            </asp:Panel>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
<%--//For Search the button Popup Find the Issue Demand Vocuher Number--%>
<asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
    CommandName="Select" />
<div style="position: absolute; top: 3000px;">
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
        PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
        Enabled="True" PopupDragHandleControlID="PopupMenu">
    </asp:ModalPopupExtender>
    <div>
        <asp:Panel ID="panel3" runat="server" CssClass="popup1">
            <div class="PopUpClose">
                <div class="btnclosepopup">
                    <asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                </div>
            </div>
            <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                <div>
                    <asp:GridView ID="gvIssueDemandVoucher" runat="server" AutoGenerateColumns="false"
                        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No Record Found(s)">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged"
                                        AutoPostBack="true"></asp:RadioButton>
                                    <asp:HiddenField ID="hdfIssueDemandId" runat="server" Value='<%# Eval("IssueDemandVoucherId")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Demand Voucher No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDVNo" runat="server" CommandName="lnkIDVNDetails" CommandArgument='<%#Eval("IssueDemandVoucherId") %>'
                                        Text='<%#Eval("IssueDemandVoucherNumber") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("Quotation.StatusType.Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contractor Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblContractorName" runat="server" Text='<%#Eval("Quotation.ContractorName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contractor Work Order No">
                                <ItemTemplate>
                                    <asp:Label ID="lblContractQuotNo" runat="server" Text='<%#Eval("Quotation.ContractQuotationNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Work Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractQuotDate" runat="server" Text='<%#Eval("Quotation.OrderDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Contract No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractNo" runat="server" Text='<%#Eval("Quotation.ContractNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Material Demand Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblMatDemandDate" runat="server" Text='<%#Eval("MaterialDemandDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</div>
