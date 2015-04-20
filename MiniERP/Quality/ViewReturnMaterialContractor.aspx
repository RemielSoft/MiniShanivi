<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewReturnMaterialContractor.aspx.cs" Inherits="MiniERP.Quality.ViewReturnMaterialContractor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        View Return Material Contractor</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="23%">
                                    Contractor Name
                                </td>
                                <td class="center" width="27%">
                                    <asp:DropDownList ID="ddlContName" CssClass="dropdown" runat="server">
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
                                    <asp:LinkButton ID="ImgBtnFromDate" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <cc1:CalendarExtender ID="CalExtfromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                        PopupButtonID="ImgBtnFromDate">
                                    </cc1:CalendarExtender>
                                    <asp:LinkButton ID="lnkFromDate" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="lnkFromDate_Click"></asp:LinkButton>
                                </td>
                                <td class="center">
                                    To Date
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
                                <td class="center" width="25%">
                                    Return Material Contractor No.
                                </td>
                                <td width="77%" class="center">
                                    <asp:TextBox ID="txtRMCNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                        ToolTip="Search" OnClick="LinkSearch_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:GridView ID="gvReturnMaterialContractor" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" PageSize="10" CssClass="mGrid" 
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" 
                            OnRowCommand="gvReturnMaterialContractor_RowCommand" 
                            onrowdatabound="gvReturnMaterialContractor_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Return Material Contractor No.">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMCN" runat="server" CommandName="lnkIRMCNoDetails" CommandArgument='<%#Eval("ReturnMaterialContractorId") %>'
                                            Text='<%#Eval("ReturnMaterialContractorNo") %>'></asp:LinkButton>
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
                                <asp:TemplateField HeaderText="Return Material Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReturnMaterialDate" runat="server" Text='<%#Eval("ReturnMaterialDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandName="cmdEdit" CommandArgument='<%#Eval("IssueMaterialId") %>'
                                        CssClass="button icon145 " ToolTip="Edit">
                                    </asp:LinkButton>--%>
                                    <asp:LinkButton ID="LinkDelete" runat="server" CommandName="cmdDelete" CommandArgument='<%#Eval("ReturnMaterialContractorId") %>'
                                        CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure to Delete this Issue Material?')">
                                         <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("IssueMaterial.DemandVoucher.Quotation.StatusType.Id") %>' />
                                    </asp:LinkButton>
                                        <asp:LinkButton ID="LinkGenQuot" runat="server" CommandName="cmdGenerate" CommandArgument='<%#Eval("ReturnMaterialContractorId") %>'
                                            CssClass="button icon189 " ToolTip="Generate Item Stock Update" OnClientClick="return confirm('Are You Sure to Generate this Issue Material?')">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkPrint" runat="server" CommandName="cmdPrint" CommandArgument='<%#Eval("ReturnMaterialContractorId")%>'
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
                                        Retun Material Contractor
                                    </h2>
                                </div>
                                <div class="mGriditem">
                                    <asp:GridView ID="gvItemReturnMaterialContractor" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
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
                                                    <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Issued">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("UnitIssued") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReturnMaterial" runat="server" Width="100px" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Lost Unit Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemIssue" runat="server" Width="100px" Text='<%#Eval("TotalAmount") %>'></asp:Label>
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
