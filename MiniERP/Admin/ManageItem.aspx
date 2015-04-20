<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ManageItem.aspx.cs" ValidateRequest="false" Inherits="MiniERP.Admin.ManageItem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/ajax_loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="width:40px; height:40px; position:fixed; top:0; right:0; left:0; bottom:0; margin:auto" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>Manage Item
                    </h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td colspan="4" style="border-bottom: 2px solid green;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button_color action blue"
                                        OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    <asp:Label ID="lblName123" runat="server" Text="Item Name"></asp:Label>
                                    <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtname" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtname"
                                        ValidationGroup="Item" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Item Name"
                                        Display="None">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td class="center">Item Description
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtDescription" CssClass="TextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <span style="color: #000; font-size: 14px; font-weight: bold;">Model Specifications</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">Specification <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtSpecification" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSpecification" runat="server" ControlToValidate="txtSpecification"
                                        ValidationGroup="Add" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please Enter Specification"
                                        Display="None">
                                    </asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="revSpecification" runat="server" ControlToValidate="txtSpecification"
                                        Display="None" ForeColor="Red" ValidationGroup="Add" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>
                                </td>
                                <td class="center">Unit Measurement <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddlUnitMeasure" CssClass="dropdown" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvUnitMeasure" runat="server" ControlToValidate="ddlUnitMeasure"
                                        ValidationGroup="Add" SetFocusOnError="true" InitialValue="0" ForeColor="Red"
                                        ErrorMessage="Please Select Unit Measurement" Display="None">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">Yearly Consumption Value <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtUsageValue" CssClass="TextBox" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtUsageValue_TextChanged"></asp:TextBox>
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="rfvUsageValue" runat="server" ControlToValidate="txtUsageValue"
                                        ValidationGroup="Add" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please Enter Yearly Consumption Value"
                                        Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revUsageValue" runat="server" ControlToValidate="txtUsageValue"
                                        Display="None" ForeColor="Red" ValidationGroup="Add" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    <asp:DropDownList ID="ddlRanges" CssClass="dropdown" runat="server">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td class="center"></td>
                                <td class="center">
                                    <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                </td>
                                <td class="center">
                                    <asp:Label ID="lblCatId" Visible="false" runat="server"></asp:Label>
                                </td>
                                <td class="center">
                                    <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" CssClass="button_color action red" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button_color action green"
                                        ValidationGroup="Add" OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <asp:GridView ID="gvSpecification" runat="server" AllowPaging="true" AlternatingRowStyle-CssClass="alt"
                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" OnRowCommand="gvSpecification_RowCommand"
                        OnRowUpdating="gvSpecification_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ModelSpecificationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitofMeasurement" runat="server" Text='<%#Eval("UnitMeasurement.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Yearly Consumption">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsageValue" runat="server" Text='<%#Eval("CategoryUsageValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Level">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Category.ItemCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Brand.BrandName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandArgument='<%#Container.DataItemIndex%>'
                                        CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145">
                                        <asp:Image ID="imgEdit" runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Container.DataItemIndex%>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure, You Want To Delete This Item?') "
                                        ToolTip="Delete" CssClass="button icon186">
                                        <asp:Image ID="imgDelete" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="Button_align">
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="button_color action red"
                            OnClick="btncancel_Click" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" ValidationGroup="Item" Visible="false"
                            CssClass="button_color action blue" OnClick="btnupdate_Click" />
                        <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="Item" CssClass="button_color action green"
                            OnClick="btnsave_Click" />
                    </div>
                    <br />
                    <br />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="Item" />
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="Add" />
                    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        PageSize="20" OnPageIndexChanging="gvItem_PageIndexChanging" OnRowCommand="gvItem_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Model Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblModelCode" runat="server" Text='<%#Eval("ModelSpecification.ModelCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkName" runat="server" CssClass="gridLink" CommandName="Details"
                                        CommandArgument='<%#Eval("ItemId") %>' Text='<%#Eval("ItemName") %>' ></asp:LinkButton>
                                   ( <asp:LinkButton ID="Lnkspecification" runat="server" CssClass="gridLink" CommandName="Details" CommandArgument='<%#Eval("ItemId") %>'
                                       Text='<%#Eval("ModelSpecification.ModelSpecificationName") %>'></asp:LinkButton>)
                                     <%--<asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ModelSpecification.ModelSpecificationName") %>'>' /></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ModelSpecification.ModelSpecificationName") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Unit Of Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitOfMeasurement" runat="server" Text='<%#Eval("ModelSpecification.UnitMeasurement.Name") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate>
                                    <asp:Label ID="lblMake" runat="server" Text='<%#Eval("ModelSpecification.Brand.BrandName") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("ModelSpecification.Category.ItemCategoryName") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Yearly Consumption Value" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategoryUsage" runat="server" Text='<%#Eval("ModelSpecification.CategoryUsageValue") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manage Brand" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMangeBrand" runat="server" CssClass="gridLink" CommandArgument='<%#Eval("ModelSpecification.ModelSpecificationId") %>' CommandName="ManageBrand">Manage Brand</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ModelSpecification.ModelSpecificationId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145 ">
                                        <asp:Image ID="imgEdit" runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ModelSpecification.ModelSpecificationId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure, You Want To Delete This Item?') "
                                        ToolTip="Delete" CssClass="button icon186">
                                        <asp:Image ID="imgDelete" runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkAddition" runat="server" CssClass="button icon3" CommandName="cmdAddition"
                                        CommandArgument='<%#Eval("ItemId") %>' ToolTip="Add New Model"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="Button_align">
                        <asp:Button ID="LnkBtnExport" runat="server" Text="Export To Excel" CssClass="button_color action green"
                            OnClick="LnkBtnExport_Click" />
                    </div>
                    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                        CommandName="Select" />
                    <asp:Button ID="btnPopUpBrand" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
                        CommandName="Select" />
                </div>
            </div>
            <div style="position: absolute; top: 3000px;">
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
                    PopupControlID="panel3" BackgroundCssClass="modalBackground" CancelControlID="Button1" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                        <div class="PopUpClose">
                            <div class="btnclosepopup">
                                <asp:Button ID="Button1" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                            </div>
                        </div>
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                            <div class="box span12">
                                <div class="box-header well">
                                    <h2>View Item Details</h2>
                                </div>
                                <div class="box-content">
                                    <table class="table table-bordered ">
                                        <tr>
                                            <td class="center">
                                                <b>Item Name</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblItemName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">
                                                <b>Item Description</b>
                                            </td>
                                            <td class="center">
                                                <asp:Label ID="lblItemDescription" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <b style="color: Green">Model Specification</b>
                                    <br />
                                    <br />
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" OnRowCommand="gvSpecification_RowCommand"
                                        OnRowUpdating="gvSpecification_RowUpdating" PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Model Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModelCode" runat="server" Text='<%#Eval("ModelCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Of Measurement">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitOfMeasurement" runat="server" Text='<%#Eval("UnitMeasurement.Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ModelSpecificationName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Category.ItemCategoryName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Yearly Consumption Value" ItemStyle-CssClass="taright">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUsageValue" runat="server" Text='<%#Eval("CategoryUsageValue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Brand.BrandName") %>'></asp:Label>
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
            <div style="position: absolute; top: 3000px;">
                <cc1:ModalPopupExtender ID="ModalPopupManageBrand" runat="server" TargetControlID="btnPopUpBrand"
                    PopupControlID="panel1" BackgroundCssClass="modalBackground" CancelControlID="Button2" DropShadow="true"
                    Enabled="True" PopupDragHandleControlID="PopupMenu">
                </cc1:ModalPopupExtender>
                <div>
                    <asp:Panel ID="panel1" runat="server" CssClass="popup1">
                        <%-- <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Brand" />--%>
                        <div class="PopUpClose">
                            <div class="btnclosepopup">
                                <asp:Button ID="Button2" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                            </div>
                        </div>
                        <asp:Panel ID="Panel4" runat="server" CssClass="popup" ScrollBars="Vertical">
                            <div class="box span12">
                                <div class="box-header well">
                                    <h2>Manage Brand for Item</h2>
                                </div>
                                <div class="box-content">
                                    <table class="table table-bordered ">
                                        <tr>
                                            <td class="center">Brand
                                            </td>
                                            <td class="center">
                                                <asp:DropDownList ID="ddlMake" CssClass="dropdown" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlMake" runat="server" ControlToValidate="ddlMake"
                                                    ValidationGroup="Brand" Enabled="true" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please Select Item's Brand">
                                                </asp:RequiredFieldValidator>
                                                <%--<asp:TextBox ID="txtMake" CssClass="TextBox" runat="server"></asp:TextBox>--%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddBrand" runat="server" Text="Add" ValidationGroup="Brand" CssClass="button_color action green"
                                                    OnClick="btnAddBrand_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:GridView ID="gvBrand" runat="server" AllowPaging="true" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" OnRowCommand="gvBrand_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrandName" runat="server" Text='<%#Eval("Brand.BrandName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBrandDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Brand.BrandId") %>'
                                                        CommandName="cmdBrandDelete" OnClientClick="return confirm('Are You Sure, You Want To Delete This Item?') "
                                                        ToolTip="Delete" CssClass="button icon186">
                                                        <asp:Image ID="imgDelete" runat="server" />
                                                    </asp:LinkButton>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkBtnExport" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
