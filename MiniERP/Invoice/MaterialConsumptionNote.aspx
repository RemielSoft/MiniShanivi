<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    EnableEventValidation="false" ViewStateEncryptionMode="Never" EnableViewStateMac="false"
    CodeBehind="MaterialConsumptionNote.aspx.cs" ValidateRequest="false" Inherits="MiniERP.Invoice.MaterialConsumptionNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <%--  <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>--%>
    <div>
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
                <h2>Material Consumption Note</h2>
            </div>
            <div class="box-content">
                <asp:Panel ID="pnlSearch" runat="server">
                    <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                        <tbody>
                            <tr>
                                <td class="center" width="25%">Contractor Work Order Number
                                </td>
                                <td class="center" width="45%">
                                    <asp:TextBox ID="txtWOrderNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvIssueNumber" runat="server" ControlToValidate="txtWOrderNumber"
                                        Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Issue Material Number"
                                        ValidationGroup="IssueMaterial"></asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CausesValidation="false"
                                        CssClass="Search icon198" ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>
                                </td>
                                <td class="center" width="30%">
                                    <asp:DropDownList ID="ddlConsumption" CssClass="TextBox" runat="server" ToolTip="Contractor name">
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
                <asp:Panel ID="pnlMaterialConsumptionNotes" runat="server">
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
                                <td class="center" width="25%">Issue Date
                                </td>
                                <td class="center" width="75%" colspan="3">
                                    <asp:Label ID="lblIssueMaterialDate" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnStatusTypeId" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="mGriditem">
                        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityDiscription" runat="server" Text='<%#Eval("ActivityDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ItemSpecification") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Per Unit Cost" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPUC" runat="server" Text='<%#Eval("PerUnitCost") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" No.Of Unit" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Issued" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitIssued" runat="server" Text='<%#Eval("IssuedQuantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="unit left" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblunitleft" runat="server" Text='<%#Eval("AvailableQuantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Lost Unit" ItemStyle-CssClass="taright">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLostUnit" runat="server" Width="70px" Text='<%#Eval("LostUnit") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Consumed Unit" ItemStyle-CssClass="taright">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtConsumedUnit" runat="server" Width="70px" Text='<%#Eval("ActualNumberofUnit") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <%--  <asp:TemplateField HeaderText="ReturnItemUnit">
                                    <ItemTemplate>
                                    <asp:TextBox ID="txtReturnMaterial" runat="server" Text='<%#Eval("") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="250" Height="22px" Width="140px"
                                                CssClass="mlttext" Text='<%#Eval("Remark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="mGriditem">
                        <asp:GridView ID="gvIssueMaterial" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available"
                            OnRowDataBound="gvIssueMaterial_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                            OnCheckedChanged="chbxSelectAll_Click" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkbxIssueDetails" runat="server" AutoPostBack="true" OnCheckedChanged="on_chbx_Activity_Click" />
                                        <asp:HiddenField ID="hdfContractorPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity Description" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityDiscription" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Category" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnBrand" runat="server" Value='<%#Eval("Item.ModelSpecification.Brand.BrandId") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Per Unit Cost" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPUC" runat="server" Text='<%#Eval("PerUnitCost") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" No.Of Unit" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Issued" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitIssued" runat="server" Text='<%#Eval("UnitIssued") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="unit left" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblunitleft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lost Unit" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLostUnit" runat="server" Width="70px" Text='<%#Eval("LostUnit") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Consumed Unit" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConsumedUnit" runat="server" Width="70px" Text='<%#Eval("ActualNumberofUnit") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="ReturnItemUnit">
                                    <ItemTemplate>
                                    <asp:TextBox ID="txtReturnMaterial" runat="server" Text='<%#Eval("") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" MaxLength="250" Height="22px" Width="140px"
                                            CssClass="mlttext" Text='<%#Eval("Remark") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="Button_align">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button_color action"
                            OnClick="btnAdd_Click" />
                    </div>
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
                                <td class="center">Consumption Date
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
                                                    <asp:Button ID="btn_upload" runat="server" CausesValidation="false"
                                                        Text="Upload" OnClick="btn_upload_Click1" />
                                                </td>
                                                <td class="center">
                                                    <asp:Panel ID="Panel4" runat="server" Height="80px" ScrollBars="Vertical" Width="100%">
                                                        <div class="box-content">
                                                            <asp:GridView ID="gv_documents" runat="server" AlternatingRowStyle-CssClass="alt"
                                                                AutoGenerateColumns="false" CellPadding="4" CssClass="mGrid" EmptyDataText="No-Documents Uploaded."
                                                                ForeColor="#333333" GridLines="None" ShowHeader="false" Width="250px">
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
                        <asp:GridView ID="gvMaterialConsumption" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available " OnRowCommand="gvMaterialConsumption_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
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
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Per Unit Cost" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPUC" runat="server" Text='<%#Eval("PerUnitCost") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" No. Of Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblONOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Of Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Consumed Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConsumedunit" runat="server" Text='<%#Eval("ConsumedUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lost Unit" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLostUnit" runat="server" Width="70px" Text='<%#Eval("LostUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="Return Unit">
                                    <ItemTemplate>
                                    <asp:Label ID="lblRetunUnit" runat="server" Text='<%#Eval("") %>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Total Lost Unit Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountLostUnit" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmdDelete"
                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete?') "
                                            ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="Button_align">

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_color red"
                            OnClick="btnCancel_Click" />
                        <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft" CssClass="button_color green"
                            OnClick="btnSaveDraft_Click" />


                    </div>

                </asp:Panel>
            </div>
        </div>
        <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
            CommandName="Select" />
        <%-- for popup Search the Issue material Genreated Contractoe Work Order Number Gridview Name=gvIssueMaterialNo--%>
        <div style="position: absolute; top: 3000px;">
            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button8">
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
                            <asp:GridView ID="gvIssueMaterialNo" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                EmptyDataText="No Record Found(s)">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged" AutoPostBack="true"></asp:RadioButton>
                                            <asp:HiddenField ID="hdfIssuematerialId" runat="server" Value='<%# Eval("IssueMaterialId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contractor Work Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContractorWONo" runat="server" Text='<%#Eval("DemandVoucher.Quotation.ContractQuotationNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contractor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContractorName" runat="server" Text='<%#Eval("DemandVoucher.Quotation.ContractorName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contract No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContractNo" runat="server" Text='<%#Eval("DemandVoucher.Quotation.ContractNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Material Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%#Eval("IssueMaterialDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btn_upload" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
