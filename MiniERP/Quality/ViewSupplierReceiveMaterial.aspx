<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewSupplierReceiveMaterial.aspx.cs" Inherits="MiniERP.Quality.ViewSupplierReceiveMaterial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit, Version=4.1.60623.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        View Receive Material Supplier Purchase Order</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <%--<tr id="rbtn" runat="server">
                                <td class="center" width="23%">
                                    Order
                                </td>
                                <td colspan="3" class="center" width="77%">
                                    <asp:RadioButtonList ID="rblbtnOrder" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem>Purchase Order</asp:ListItem>
                                        <asp:ListItem>Company Work Order</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="center" width="23%">
                                    From Date
                                </td>
                                <td class="center" width="27%">
                                    <asp:TextBox ID="txtFromDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="imgbtnFromdate" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                        PopupButtonID="imgbtnFromdate">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="LnkBtn" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="LnkBtn_Click"></asp:LinkButton>
                                </td>
                                <td class="center" width="23%">
                                    To Date
                                </td>
                                <td class="center" width="27%">
                                    <asp:TextBox ID="txtToDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="imgbtnToDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                        PopupButtonID="imgbtnToDate">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="lnkBtnClear" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="lnkBtnClear_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" width="23%">
                                    Purchase Order Number
                                </td>
                                <td colspan="3" class="center" width="77%">
                                    <asp:TextBox ID="txtPurchaseOrderNumber" CssClass="TextBox" runat="server"></asp:TextBox>
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
                                    Delivery Challan Number
                                </td>
                                <td colspan="3" class="center" width="77%">
                                    <asp:TextBox ID="txtDelChallanNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                        ToolTip="Search" OnClick="lnkbtnSearch_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="gvRSM" runat="server" CssClass="mGrid" AllowPaging="false" AutoGenerateColumns="false"
                        EmptyDataText="No Record Found(s)" OnPageIndexChanging="gvRSM_PageIndexChanging"
                        AlternatingRowStyle-CssClass="alt" OnRowCommand="gvRSM_RowCommand" OnRowDataBound="gvRSM_RowDataBound"
                        PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="Receive Material Number">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lnkOrder" runat="server" CommandName="lnkSRMDetails" CommandArgument='<%#Eval("SupplierRecieveMatarialId") %>'
                                        Text='<%#Eval("Quotation.SupplierQuotationNumber") %>'></asp:LinkButton>--%>
                                    <asp:LinkButton ID="lnkSRMNo" runat="server" CommandName="lnkSRMDetails" CommandArgument='<%#Eval("SupplierRecieveMatarialId") %>'
                                        Text='<%#Eval("SupplierRecieveMaterialNumber") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("Quotation.StatusType.Id") %>' />
                                    <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("UploadFile.Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purchase Order Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblSRMNo" runat="server" Text='<%#Eval("Quotation.SupplierQuotationNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupName" runat="server" Text='<%#Eval("Quotation.SupplierName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivery Challan Number" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblChallanNo" runat="server" Text='<%#Eval("DeliveryChallanNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receive Material Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblRecieveDate" runat="server" Text='<%#Eval("RecieveMaterialDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Quotation.StatusType.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="cmdEdit" CommandArgument='<%#Eval("SupplierRecieveMatarialId") %>'
                                        CssClass="button icon145 " ToolTip="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="cmdDelete" CommandArgument='<%#Eval("SupplierRecieveMatarialId") %>'
                                        CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure To Delete?') ">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkPrint" runat="server" CommandName="cmdPrint" CommandArgument='<%#Eval("SupplierRecieveMatarialId")+","+Eval("SupplierRecieveMaterialNumber") %>'
                                        CssClass="button icon153 " ToolTip="Print"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkGenerate" runat="server" CommandName="cmdGenerate" CommandArgument='<%#Eval("SupplierRecieveMatarialId") %>'
                                        CssClass="button icon189 " ToolTip="Generate Receive Material" OnClientClick="return confirm('Are You Sure To Generate?') "></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                        CommandName="Select" />
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
                                <asp:Button ID="Button3" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
                        </div>
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                            <div class="box span12">
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
                                        Receive Material</h2>
                                </div>
                                <div class="box-content">
                                    <div class="mGriditem">
                                        <asp:GridView ID="gvItemInfo" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            EmptyDataText="No-Items Available">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="ItemId">
                                            <ItemTemplate>
                                                 <asp:HiddenField ID="hdfContractorPOMappingId" runat="server" Value='<%# Eval("Item.ItemId")%>' />                                                                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpecificationA" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category level">
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
                                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Of Measurement">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receive Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemReq" runat="server" Width="100px" Text='<%#Eval("ItemRequired") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
