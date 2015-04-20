<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="MaterialReconciliationReport.aspx.cs" Inherits="MiniERP.Invoice.MaterialReconciliationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>Material Reconciliation Report</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="23%">Contractor Name
                                </td>
                                <td class="center" width="27%">
                                    <asp:DropDownList ID="ddlContName" CssClass="dropdown" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="center" width="23%">Contract No.
                                </td>
                                <td class="center" width="27%">
                                    <asp:DropDownList ID="ddlContractNo" CssClass="dropdown" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">From Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgBtnFromDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <cc1:CalendarExtender ID="CalExtfromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                        PopupButtonID="ImgBtnFromDate">
                                    </cc1:CalendarExtender>
                                    <asp:LinkButton ID="lnkFromDate" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="lnkFromDate_Click"></asp:LinkButton>
                                </td>
                                <td class="center">To Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgBtnToDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <cc1:CalendarExtender ID="CalExtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                        PopupButtonID="ImgBtnToDate">
                                    </cc1:CalendarExtender>
                                    <asp:LinkButton ID="lnkToDate" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="lnkToDate_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="button_color  go" OnClick="btnSearch_Click" />
                    </div>
                    <br />
                    <br />
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="25%">Material Consumption No.
                                </td>
                                <td width="77%" class="center">
                                    <asp:TextBox ID="txtMCNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                        ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:GridView ID="gvMaterialConsumptionReport" runat="server" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10" CssClass="mGrid"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available " OnPageIndexChanging="gvMaterialConsumptionReport_PageIndexChanging"
                            OnRowCommand="gvMaterialConsumptionReport_RowCommand"
                            OnRowDataBound="gvMaterialConsumptionReport_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Material Consumption No.">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMCN" runat="server" CommandName="lnkIMCNoDetails" CommandArgument='<%#Eval("MaterialConsumptionId") %>'
                                            Text='<%#Eval("MaterialConsumptionNo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contractor Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractorName" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractorName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Order No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWON" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractNo" runat="server" Text='<%#Eval("IssueMaterial.DemandVoucher.Quotation.ContractNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Consumption Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConsumptionDate" runat="server" Text='<%#Eval("MaterialConsumptionDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="cmdEdit" CommandArgument='<%#Eval("MaterialConsumptionId") %>'
                                            CssClass="button icon145 " ToolTip="Edit">
                                            <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("IssueMaterial.DemandVoucher.Quotation.StatusType.Id") %>' />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkDelete" runat="server" CommandName="cmdDelete" CommandArgument='<%#Eval("MaterialConsumptionId") %>'
                                            CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure to Delete this Issue Material?')">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkGenQuot" runat="server" CommandName="cmdGenerate" CommandArgument='<%#Eval("MaterialConsumptionId") %>'
                                            CssClass="button icon189 " ToolTip="Generate Issue Demand Voucher" OnClientClick="return confirm('Are You Sure to Generate this Issue Material?')">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkPrint" runat="server" CommandName="cmdPrint" CommandArgument='<%#Eval("MaterialConsumptionId")%>'
                                            CssClass="button icon153 " ToolTip="Print"> </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                            CommandName="Select" />
                    </div>
                </div>
            </div>
            <div style="position: absolute; top: 1000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu" CancelControlID="Button8">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                            <div class="btnclosepopup">
                                <asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                            </div>
                        </div>
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                            <div class="box-content">
                                <div class="box-header well">
                                    <h2>Attached Documents
                                    </h2>
                                </div>
                                <div>
                                    <udc:UploadDocuments runat="server" ID="updcFile" />
                                </div>
                                <div class="box-header well">
                                    <h2>Issue Material
                                    </h2>
                                </div>
                                <div class="mGriditem">
                                    <asp:GridView ID="gvItemInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No-Items Available">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Activity Discription">
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
                                            <asp:TemplateField HeaderText="Category level">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lost Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("LostUnit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Consumed Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemReq" runat="server" Width="100px" Text='<%#Eval("ConsumedUnit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lost Unit Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemIssue" runat="server" Width="100px" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
