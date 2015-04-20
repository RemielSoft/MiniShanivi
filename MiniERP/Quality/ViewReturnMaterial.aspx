<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewReturnMaterial.aspx.cs" Inherits="MiniERP.Quality.ViewReturnMaterial" %>

<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        View Return Material Note</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" >
                                    Supplier Name
                                </td>
                                <td colspan="3" class="center">
                                    <asp:DropDownList ID="ddlSupplierName" CssClass="dropdown" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" width="20%">
                                    From Date
                                </td>
                                <td class="center" width="30%">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgBtn1" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtFromDate"  runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                        PopupButtonID="ImgBtn1">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="lnkbtnClear" runat="server" ToolTip="Clear" CssClass="Search icon188"
                                        OnClick="lnkbtnClear_Click"></asp:LinkButton>
                                </td>
                                <td class="center" width="20%">
                                    To Date
                                </td>
                                <td class="center" width="30%">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgBtn2" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalExtToDate"  runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                        PopupButtonID="ImgBtn2">
                                    </asp:CalendarExtender>
                                    <asp:LinkButton ID="lnkbtn" runat="server" ToolTip="Clear" 
                                        CssClass="Search icon188" onclick="lnkbtn_Click"
                                        ></asp:LinkButton>
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
                    <br />
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="55%">
                                    <asp:RadioButtonList ID="rbtn" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Return Material Note No" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Purchase Order No"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Delevery Chalan No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td  width="45%">
                                    <asp:TextBox ID="txtSearch" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" CssClass="Search icon198" 
                                        ToolTip="Search" onclick="lnkSearch_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:GridView ID="gvReturnMaterial" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No Record Found(s)" 
                            onrowcommand="gvReturnMaterial_RowCommand" 
                            onrowdatabound="gvReturnMaterial_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Return Material Note No.">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReturnMaterialNoteNo" runat="server" CommandName="lnkReturnMaterialDetails" CommandArgument='<%#Eval("RetutrnMaterialId")+","+Eval("ReturnMaterialNumber") %>'
                                            Text='<%#Eval("ReturnMaterialNumber") %>'></asp:LinkButton>
                                            <asp:HiddenField ID="hdf_documnent_Id" Value='<%#Eval("RecieveMatarial.Quotation.UploadDocumentId")%>'  runat="server" />                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Order Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text='<%#Eval("RecieveMatarial.Quotation.SupplierQuotationNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive Material Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiveMaterialNumber" runat="server" Text='<%#Eval("RecieveMatarial.SupplierRecieveMaterialNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="delivery challan Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeleveryChalanNumber" runat="server" Text='<%#Eval("RecieveMatarial.DeliveryChallanNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return Material Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReturnMaterialDate" runat="server" Text='<%#Eval("ReturnMaterialDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Status Type">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfStatusTypeId" runat="server" Value='<%#Eval("RecieveMatarial.Quotation.StatusType.Id") %>' />
                                        <asp:Label ID="lblStatusType" runat="server" Text='<%#Eval("RecieveMatarial.Quotation.StatusType.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="lnkEdit" CommandArgument='<%#Eval("RetutrnMaterialId") %>'
                                            CssClass="button icon145 " ToolTip="Edit">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkDelete" runat="server" CommandName="lnkDelete" CommandArgument='<%#Eval("RetutrnMaterialId") %>'
                                            CssClass="button icon186 " ToolTip="Delete">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkPrint" runat="server" CommandName="lnkPrint" CommandArgument='<%#Eval("RetutrnMaterialId")+","+Eval("ReturnMaterialNumber") %>'
                                            CssClass="button icon153 " ToolTip="Print">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkGenQuot" runat="server" CommandName="lnkGenerate" CommandArgument='<%#Eval("RetutrnMaterialId") %>'
                                            CssClass="button icon189 " ToolTip="Generate Issue Demand Voucher">
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
            <%--<table class="mGrid">
                <tr>
                    <th>
                        Return Material Note Number
                    </th>
                    <th>
                        Purchase Order Number
                    </th>
                    <th>
                        Delevery Chalan Number
                    </th>
                    <th>
                        Return Material Date
                    </th>
                    
                    
                    <th>
                        Action
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="btnPopUp" runat="server" Text="RMNN-1" CommandName="cmdEdit"></asp:LinkButton>
                    </td>
                    <td>
                        PON-1
                    </td>
                    <td>
                        4554365
                    </td>
                    <td>
                        12/jan/2013
                    </td>
                    
                    
                    <td>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="cmdEdit" CssClass="button icon145 "
                            ToolTip="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="lnkDelete" CssClass="button icon186 "
                            ToolTip="Delete"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="lnkPrint" CssClass="button icon153 "
                            ToolTip="Print"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="lnkGenerate" CssClass="button icon189 "
                            ToolTip="Generate Quotation"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                       <asp:LinkButton ID="LinkButton1" runat="server" Text="RMNN-2" CommandName="cmdEdit"></asp:LinkButton>
                    </td>
                    <td>
                        PON-2
                    </td>
                    <td>
                        4452454
                    </td>
                    <td>
                        15/jan/2013
                    </td>
                    
                   
                    <td>
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="cmdEdit" CssClass="button icon145 "
                            ToolTip="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="lnkDelete" CssClass="button icon186 "
                            ToolTip="Delete"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton7" runat="server" CommandName="lnkPrint" CssClass="button icon153 "
                            ToolTip="Print"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton8" runat="server" CommandName="lnkGenerate" CssClass="button icon189 "
                            ToolTip="Generate Quotation"></asp:LinkButton>
                    </td>
                </tr>
            </table>--%>
            <div  style=" position:absolute; top:1000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                          <div class="btnclosepopup"><asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" /></div>
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
                                    Return Material</h2>
                            </div>
                            <div class="mGrid">
                            <asp:GridView ID="gvItemInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    EmptyDataText="No-Items Available">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Activity Discription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityDiscription1" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
                                        <asp:TemplateField HeaderText="Item Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRecvd" runat="server" Width="100px" Text='<%#Eval("QuantityReceived") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Return">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemReturn" runat="server" Width="100px" Text='<%#Eval("QuantityReturned") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <%--<table class="mGrid">
                                <tr>
                                    <th>
                                        Item Category
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Make (Brand)
                                    </th>
                                    <th>
                                        Specification
                                    </th>
                                    <th>
                                        Model
                                    </th>
                                    <th>
                                        Item Quantity
                                    </th>
                                    <th>
                                        Item Received
                                    </th>
                                    <th>
                                        Item Returned
                                    </th>
                                    <th>
                                        Remarks
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        36mm
                                    </td>
                                    <td>
                                        Two Wheeler Motor
                                    </td>
                                    <td>
                                        Bajaj
                                    </td>
                                    <td>
                                        abc
                                    </td>
                                    <td>
                                        M-21
                                    </td>
                                    <td>
                                        5
                                    </td>
                                    <td>
                                        3
                                    </td>
                                    <td>
                                        1
                                    </td>
                                    <td>
                                        abcdf
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        blackberry
                                    </td>
                                    <td>
                                        Phone
                                    </td>
                                    <td>
                                        Samsung
                                    </td>
                                    <td>
                                        mkv
                                    </td>
                                    <td>
                                        i-3
                                    </td>
                                    <td>
                                        6
                                    </td>
                                    <td>
                                        4
                                    </td>
                                    <td>
                                        2
                                    </td>
                                    <td>
                                        mkvnps
                                    </td>
                                </tr>
                            </table>--%>
                            
                        </div>
                   </asp:Panel>
                   </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
