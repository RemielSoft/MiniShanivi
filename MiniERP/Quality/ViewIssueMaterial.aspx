<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewIssueMaterial.aspx.cs" Inherits="MiniERP.Quality.ViewIssueMaterial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>View Issue Material</h2>
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
                                    <asp:CalendarExtender ID="CalExtfromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                        PopupButtonID="ImgBtnFromDate">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="lnkFromDate" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="lnkFromDate_Click"></asp:LinkButton>
                                </td>
                                <td class="center">To Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgBtnToDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                        PopupButtonID="ImgBtnToDate">
                                    </asp:CalendarExtender>
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

                                <td class="center" width="50%">
                                    <asp:RadioButtonList ID="rbtn" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Issue Material No" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Contractor Work Order No"></asp:ListItem>
                                        <%--<asp:ListItem Value="3" Text="Delevery Chalan No"></asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="50%" class="center">
                                    <asp:TextBox ID="txtIssueNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                        ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:GridView ID="gvIssueMaterialNo" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Record Found(s)" OnPageIndexChanging="gvIssueMaterialNo_PageIndexChanging"
                            OnRowCommand="gvIssueMaterialNo_RowCommand" OnRowDataBound="gvIssueMaterialNo_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Issue Material No.">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkIssueMaterialNo" runat="server" CommandName="lnkIMNoDetails"
                                            CommandArgument='<%#Eval("IssueMaterialId") %>' Text='<%#Eval("IssueMaterialNumber") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("DemandVoucher.Quotation.StatusType.Id") %>' />
                                        <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("DemandVoucher.Quotation.UploadDocumentId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contractor Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractorName" runat="server" Text='<%#Eval("DemandVoucher.Quotation.ContractorName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Demand Voucher No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDVNo" runat="server" Text='<%#Eval("DemandVoucher.IssueDemandVoucherNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contractor Work Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractorWONo" runat="server" Text='<%#Eval("DemandVoucher.Quotation.ContractQuotationNumber") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("DemandVoucher.Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("DemandVoucher.Quotation.StatusType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="cmdEdit" CommandArgument='<%#Eval("IssueMaterialId") %>'
                                            CssClass="button icon145 " ToolTip="Edit">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkDelete" runat="server" CommandName="cmdDelete" CommandArgument='<%#Eval("IssueMaterialId") %>'
                                            CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure to Delete this Issue Material?')">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkPrint" runat="server" CommandName="cmdPrint" CommandArgument='<%#Eval("IssueMaterialId")+","+ Eval("IssueMaterialNumber")%>'
                                            CssClass="button icon153 " ToolTip="Print">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkGenQuot" runat="server" CommandName="cmdGenerate" CommandArgument='<%#Eval("IssueMaterialId") %>'
                                            CssClass="button icon189 " ToolTip="Generate Issue Demand Voucher" OnClientClick="return confirm('Are You Sure to Generate this Issue Material?')">
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

                                    <%-- Grid Main Starts--%>

                                    <asp:GridView ID="gvMainGrid" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No-Items Available" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Activity Discription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityDiscription" runat="server" Text='<%#Eval("ActivityDescription") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ItemSpecification") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category level">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("ItemCategoryName") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="No. Of Unit" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNOF1" runat="server" Text='<%#Eval("AvailableQuantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Demanded">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemReq" runat="server" Width="100px" Text='<%#Eval("QuantityDemand") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Issued">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemIssue" runat="server" Width="100px" Text='<%#Eval("IssuedQuantity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblItemIssuetotal" Font-Bold="true" runat="server" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Acknowledge Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAckStatus" runat="server" Width="100px" Text='<%#Eval("AcknowledgeStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                    <%-- Grid Main Ends--%>
                                    <asp:GridView ID="gvItemInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No-Items Available" ShowFooter="true" OnRowDataBound="gvItemInfo_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Activity Discription" Visible="false">
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
                                            <asp:TemplateField HeaderText="Category level" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Store">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStore" runat="server" Text='<%#Eval("Item.ModelSpecification.Store.StoreName") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. Of Unit" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNOF1" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Demanded" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemReq" runat="server" Width="100px" Text='<%#Eval("UnitDemanded") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Issued">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemIssue" runat="server" Width="100px" Text='<%#Eval("ItemRequired") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Acknowledge Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAckStatus" runat="server" Width="100px" Text='<%#Eval("AcknowledgeStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
