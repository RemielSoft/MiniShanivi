<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="DemandIssueVoucher.aspx.cs" Inherits="MiniERP.IssueMaterial.DemandIssueVoucher" %>

<%@ Register Src="~/Quality/Parts/DemandIssueVoucher.ascx" TagName="DemandIssueVoucher"
    TagPrefix="udc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>
        <udc:DemandIssueVoucher runat="server" ID="Demand_Issue_Voucher" />
    </div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                    <h2>
                        Issue Demand Voucher</h2>
                </div>
                <div class="box-content">
                    <asp:Panel ID="pnlSearch" runat="server">
                        <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                            <tbody>
                                <tr>
                                    <td class="center" width="25%">
                                        Contractor Work Order Number
                                    </td>
                                    <td class="center" width="75%">
                                        <asp:TextBox ID="txtContractorQuotationNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="btnSearchContractorQuotationNumber" runat="server" CssClass="Search icon198"
                                            ToolTip="Search" OnClick="btnSearchContractorQuotationNumber_Click"></asp:LinkButton>
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
                                    <td class="center" width="25%">
                                        Contractor Work Order Number
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblContractorQuotationNumber" runat="server"></asp:Label>
                                    </td>
                                    <td class="center" width="25%">
                                        Contractor Work Order Date
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
                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
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
                                    <td class="center" width="25%">
                                        Contractor Name
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblContractorName" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdfContractorId" runat="server" />
                                    </td>
                                    <td class="center" width="25%">
                                        Contract Number
                                    </td>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lblContractNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Material demand Date
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtMaterialDemandDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtMaterialDemandDate" PopupButtonID="ImageButton3">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="center">
                                        Remarks
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="mlttext" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center" width="25%">
                                        Upload Document
                                    </td>
                                    <td colspan="3" class="center" width="75%">
                                        <udc:UploadDocuments ID="ctrl_UploadDocument" runat="server" />
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
                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
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
                                            <asp:ImageButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmdDelete"
                                                CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Activity Description?') "
                                                ToolTip="Delete" CssClass="button icon186" />
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
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
