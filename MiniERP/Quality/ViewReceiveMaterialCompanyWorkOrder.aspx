<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewReceiveMaterialCompanyWorkOrder.aspx.cs" Inherits="MiniERP.Quality.ViewReceiveMaterialCompanyWorkOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        View Receive Material Company Work Order</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="25%">
                                    Contract Receive Material Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCRMNumber" runat="server"></asp:TextBox>
                                </td>
                                <td class="center">
                                    Company Work Order Number
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_CWO_Number" Width="150px" runat="server">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" width="25%">
                                    From Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtFromDate" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="lnkFromDate" runat="server" CssClass="Calender icon175" ToolTip="Select Date">
                                    </asp:LinkButton>
                                    <ajaxtoolkit:CalendarExtender ID="CalExtFromDate" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="txtFromDate" PopupButtonID="lnkFromDate">
                                    </ajaxtoolkit:CalendarExtender>
                                    <asp:LinkButton ID="LnkBtnFrom_Clear" runat="server" ToolTip="Clear" 
                                        CssClass="Search icon188" onclick="LnkBtnFrom_Clear_Click"></asp:LinkButton>
                                </td>
                                <td class="center">
                                    To Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtToDate" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="lnkToDate" runat="server" CssClass="Calender icon175" ToolTip="Select Date">
                                    </asp:LinkButton>
                                    <ajaxtoolkit:CalendarExtender ID="CalExtToDate" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="txtToDate" PopupButtonID="lnkToDate">
                                    </ajaxtoolkit:CalendarExtender>
                                    <asp:LinkButton ID="LnkBtnTo_Clear" runat="server" ToolTip="Clear" 
                                        CssClass="Search icon188" onclick="LnkBtnTo_Clear_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="button_color  go" 
                            onclick="btnSearch_Click" />
                    </div>
                    <br />
                    <asp:GridView ID="gvCRM" runat="server" CssClass="mGrid" AllowPaging="false" AutoGenerateColumns="false"
                        EmptyDataText="No Record Found(s)" AlternatingRowStyle-CssClass="alt" 
                        PagerStyle-CssClass="pgr" onpageindexchanging="gvCRM_PageIndexChanging" 
                        onrowcommand="gvCRM_RowCommand" onrowdatabound="gvCRM_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Contract Receive Material Number">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCRMNo" runat="server" CommandName="CRMDetails" CommandArgument='<%#Eval("ContractReceiveMaterialId") %>'
                                        Text='<%#Eval("ContractReceiveMaterialNumber") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hdf_documnent_Id" runat="server" Value='<%#Eval("UploadFile.DocumentId") %>' />
                                        <asp:HiddenField ID="hdfStatusId" runat="server" Value='<%#Eval("Quotation.StatusType.Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Work Order Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblCRMNo" runat="server" Text='<%#Eval("CompanyWorkOrder.CompanyWorkOrderNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Order Number" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkOderNo" runat="server" Text='<%#Eval("Quotation.WorkOrderNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receive Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblRecieveDate" runat="server" Text='<%#Eval("Receive_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Quotation.StatusType.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="cmdEdit" CommandArgument='<%#Eval("ContractReceiveMaterialId") %>'
                                        CssClass="button icon145 " ToolTip="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="cmdDelete" CommandArgument='<%#Eval("ContractReceiveMaterialId") %>'
                                        CssClass="button icon186 " ToolTip="Delete" OnClientClick="return confirm('Are You Sure To Delete?') ">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkPrint" runat="server" CommandName="cmdPrint" CommandArgument='<%#Eval("ContractReceiveMaterialId")+","+Eval("ContractReceiveMaterialNumber") %>'
                                        CssClass="button icon153 " ToolTip="Print" ></asp:LinkButton>
                                    <asp:LinkButton ID="lnkGenerate" runat="server" CommandName="cmdGenerate" CommandArgument='<%#Eval("ContractReceiveMaterialId") %>'
                                        CssClass="button icon189 " ToolTip="Generate" OnClientClick="return confirm('Are You Sure To Generate?') "></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                        CommandName="Select" />
                </div>
            </div>
            <div style="position: absolute; top: 1000px;">
                <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu">
                </ajaxtoolkit:ModalPopupExtender>
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
                                       Company Work Order Receive Material</h2>
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
                                              <%--  <asp:TemplateField HeaderText="Receive Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemReq" runat="server" Width="100px" Text='<%#Eval("ItemRequired") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
