<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewDemandIssueVoucher.aspx.cs" Inherits="MiniERP.Quality.ViewDemandIssueVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        View Issue Demand Voucher</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="23%">
                                    Contractor Name
                                </td>
                                <td class="center" width="27%">
                                    <asp:DropDownList ID="ddlContractor" CssClass="dropdown" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="center" width="23%">
                                    Contract No.
                                </td>
                                <td class="center" width="27%">
                                    <asp:DropDownList ID="ddlContractNo" CssClass="dropdown" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    From Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="imgFromDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                        PopupButtonID="imgFromDate">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="LnkBtn" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="LnkBtn_Click"></asp:LinkButton>
                                </td>
                                <td class="center">
                                    To Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="imgToDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                        PopupButtonID="imgToDate">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="lnkbtnClear" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="lnkbtnClear_Click"></asp:LinkButton>
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
                                <td class="center" width="23%">
                                    Issue Demand Voucher Number
                                </td>
                                <td colspan="3" class="center" width="77%">
                                    <asp:TextBox ID="txtIssueDmdVoucher" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lnksearch" runat="server" CssClass="Search icon198" ToolTip="Search"
                                        OnClick="lnksearch_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:GridView ID="gvIssueDemandVoucher" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Record Found(s)" OnPageIndexChanging="gvIssueDemandVoucher_PageIndexChanging"
                            OnRowCommand="gvIssueDemandVoucher_RowCommand" OnRowDataBound="gvIssueDemandVoucher_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Issue Demand Voucher No.">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkIDVNo" runat="server" CommandName="lnkIDVNDetails" CommandArgument='<%#Eval("IssueDemandVoucherId") %>'
                                            Text='<%#Eval("IssueDemandVoucherNumber") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("Quotation.StatusType.Id") %>' />
                                        <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("Quotation.UploadDocumentId") %>' />
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
                                <asp:TemplateField HeaderText="Work Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractQuotDate" runat="server" Text='<%#Eval("Quotation.OrderDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractNo" runat="server" Text='<%#Eval("Quotation.ContractNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Demand Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatDemandDate" runat="server" Text='<%#Eval("MaterialDemandDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Quotation.StatusType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="cmdEdit" CommandArgument='<%#Eval("IssueDemandVoucherId") %>'
                                            CssClass="button icon145 " ToolTip="Edit">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkDelete" runat="server" CommandName="cmdDelete" CommandArgument='<%#Eval("IssueDemandVoucherId") %>'
                                            CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure to Delete this Issue Demand Voucher?')">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkPrint" runat="server" CommandName="cmdPrint" CommandArgument='<%#Eval("IssueDemandVoucherId")+","+Eval("IssueDemandVoucherNumber") %>'
                                            CssClass="button icon153 " ToolTip="Print">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkGenQuot" runat="server" CommandName="cmdGenerate" CommandArgument='<%#Eval("IssueDemandVoucherId") %>'
                                            CssClass="button icon189 " ToolTip="Generate Issue Demand Voucher" OnClientClick="return confirm('Are You Sure to Generate this Issue Demand Voucher?')">
                                        </asp:LinkButton>
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
                                <div class="box-header well">
                                    <h2>
                                        Attached Documents
                                    </h2>
                                </div>
                                <div>
                                    <udc:UploadDocuments runat="server" ID="updcFile" />
                                </div>
                                <div class="box-header well">
                                    <h2>
                                        Issue Demand Voucher
                                    </h2>
                                </div>
                                <div class="mGriditem">
                                    <asp:GridView ID="gvItemInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No-Items Available">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Activity Discription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityDiscription1" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Item Required">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemReq" runat="server" Width="100px" Text='<%#Eval("ItemRequired") %>'></asp:Label>
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
