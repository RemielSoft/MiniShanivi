<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ManageItemStock.aspx.cs" Inherits="MiniERP.Admin.ManageItemStock" %>

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
                    <h2>Manage Item Stock</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td colspan="4" style="border-bottom: 2px solid green;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                        CssClass="button_color action blue" />
                                </td>
                            </tr>
                            <tr>
                                <td class="center">Store <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddlStore" CssClass="dropdown" runat="server" TabIndex="2">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStore"
                                        Display="None" ForeColor="Red" InitialValue="0" ErrorMessage="Please Select Store"
                                        ValidationGroup="ItemStock" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td class="center">Item <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddlItem" CssClass="dropdown" runat="server" TabIndex="2" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="ddlItem"
                                        Display="None" ForeColor="Red" InitialValue="--Select--" ErrorMessage="Please Select Item"
                                        ValidationGroup="ItemStock" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">Specification <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddlModelSpecification" CssClass="dropdown" runat="server" TabIndex="3"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlModelSpecification_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSpecification" runat="server" ControlToValidate="ddlModelSpecification"
                                        Display="None" ForeColor="Red" InitialValue="0" ErrorMessage="Please Select Specification"
                                        ValidationGroup="ItemStock" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td class="center">Brand <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddlBrand" CssClass="dropdown" runat="server" TabIndex="3"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBrand"
                                        Display="None" ForeColor="Red" InitialValue="0" ErrorMessage="Please Select Brand"
                                        ValidationGroup="ItemStock" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <%--<td class="center">
                                    Item Category <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddlCategory" Width="147px" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                                        Display="None" ForeColor="Red" InitialValue="--Select--" ErrorMessage="Please Select Category"
                                        ValidationGroup="ItemStock" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>--%>
                            </tr>
                            <tr>
                                <td class="center">Item Unit Measurement
                                </td>
                                <td class="center">
                                    <%--<asp:TextBox ID="txtUnit" CssClass="TextBox" runat="server" Enabled="false" TabIndex="4"></asp:TextBox>--%>
                                    <asp:Label ID="lblUnit" runat="server" TabIndex="4"></asp:Label>
                                </td>
                                <td class="center">Quantity In Hand <span style="color: Red">*</span>
                                </td>
                                <td class="center" colspan="3">
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="TextBox" TabIndex="5" MaxLength="5"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revQtOnHand" runat="server" ControlToValidate="txtQuantity"
                                        Display="None" ValidationGroup="ItemStock" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Please Enter Quantity In Hand"
                                        ControlToValidate="txtQuantity" ValidationGroup="ItemStock" ForeColor="Red" SetFocusOnError="true"
                                        Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">Minimum Consumption <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtMinimumlevel" runat="server" CssClass="TextBox" MaxLength="7"
                                        TabIndex="6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMinConsp" runat="server" ErrorMessage="Please Enter Minimum Consumption"
                                        ControlToValidate="txtMinimumlevel" ValidationGroup="ItemStock" ForeColor="Red"
                                        SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revMinConsp" runat="server" ControlToValidate="txtMinimumlevel"
                                        Display="None" ValidationGroup="ItemStock" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">Maximum Consumption <span style="color: Red">*</span>
                                </td>
                                <td class="center" colspan="3">
                                    <asp:TextBox ID="txtMaximumlevel" runat="server" CssClass="TextBox" MaxLength="7"
                                        TabIndex="7"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMaxConsp" runat="server" ErrorMessage="Please Enter Maximum Consumption"
                                        ControlToValidate="txtMaximumlevel" ValidationGroup="ItemStock" ForeColor="Red"
                                        SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revMaxConsp" runat="server" ControlToValidate="txtMaximumlevel"
                                        Display="None" ValidationGroup="ItemStock" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">Normal Consumption <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtNormallevel" runat="server" CssClass="TextBox" MaxLength="7"
                                        TabIndex="8"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNormalConsp" runat="server" ErrorMessage="Please Enter Normal Consumption"
                                        ControlToValidate="txtNormallevel" ValidationGroup="ItemStock" ForeColor="Red"
                                        SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revNormalConsp" runat="server" ControlToValidate="txtNormallevel"
                                        Display="None" ValidationGroup="ItemStock" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">Lead Time (In Days)<span style="color: Red">*</span>
                                </td>
                                <td class="center" colspan="3">
                                    <asp:TextBox ID="txtLeadTime" runat="server" CssClass="TextBox" MaxLength="3" TabIndex="9"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLeadTime" runat="server" ErrorMessage="Please Enter Lead Time"
                                        ControlToValidate="txtLeadTime" ValidationGroup="ItemStock" ForeColor="Red" SetFocusOnError="true"
                                        Display="None"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revLeadTime" runat="server" ControlToValidate="txtLeadTime"
                                        Display="None" ValidationGroup="ItemStock" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_color action red"
                            OnClick="btnCancel_Click" TabIndex="11" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" ValidationGroup="ItemStock"
                            CssClass="button_color action blue" OnClick="btnUpdate_Click" TabIndex="11" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button_color action green"
                            OnClick="btnSave_Click" ValidationGroup="ItemStock" TabIndex="12" />
                    </div>
                    <br />
                    <br />
                    <div style="width: 100%; height: 20px; background-color: transparent; position: relative;">
                        <div style="position: relative; float: left; margin-right: 150px;">
                            <div style="background-color: Orange; width: 15px; height: 15px;">
                            </div>
                            <label style="position: absolute; margin: -15px 0px 0px 25px; height: 20px;">
                                ReOrderLevel
                            </label>
                        </div>
                        <div style="position: relative; float: left; margin-right: 150px;">
                            <div style="background-color: Yellow; width: 15px; height: 15px;">
                            </div>
                            <label style="position: absolute; margin: -15px 0px 0px 25px; height: 20px;">
                                MinimumLevel
                            </label>
                        </div>
                        <div style="position: relative; float: left; margin-right: 150px;">
                            <div style="background-color: Red; width: 15px; height: 15px;">
                            </div>
                            <label style="position: absolute; margin: -15px 0px 0px 25px; height: 20px;">
                                Emergency
                            </label>
                        </div>
                    </div>
                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="ItemStock" runat="server"
                        ShowMessageBox="true" ShowSummary="false" />
                    <asp:GridView ID="gvItemStock" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="75"
                        OnRowCommand="gvItemStock_RowCommand" OnPageIndexChanging="gvItemStock_PageIndexChanging"
                        OnRowDataBound="gvItemStock_RowDataBound" OnSelectedIndexChanged="gvItemStock_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="Store">
                                <ItemTemplate>
                                    <asp:Label ID="lblStoreName" runat="server" Text='<%#Eval("Store.StoreName") %>'>'/></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblContractorName" runat="server" Text='<%#Eval("ItemName") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecificationName" runat="server" Text='<%#Eval("ItemSpecificationName") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Brand.BrandName") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("ItemUnit") %>'>' /></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="QuantityOnhand" ItemStyle-CssClass="taright" HeaderText="Quantity In Hand" />
                            <asp:BoundField DataField="MinimumLevel" ItemStyle-CssClass="taright" HeaderText="Minimum Level" />
                            <asp:BoundField DataField="MaximumLevel" ItemStyle-CssClass="taright" HeaderText="Maximum Level" />
                            <asp:BoundField DataField="ReorderLevel" ItemStyle-CssClass="taright" HeaderText="Reorder Level" />
                            <asp:BoundField DataField="LeadTime" ItemStyle-CssClass="taright" HeaderText="Lead Time" />
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ItemStockId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145 ">
                                        <asp:Image ID="imgEdit" runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ItemStockId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure You Want To Delete This Item?') "
                                        ToolTip="Delete" CssClass="button icon186">
                                        <asp:Image ID="imgDelete" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="Button_align">
                        <asp:Button ID="LnkBtnExport" runat="server" Text="Export To Excel" CssClass="button_color action green"
                            OnClick="LnkBtnExport_Click" />
                        <div style="width: 200px; float: left; display: none">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </div>
                        <div style="width: 100px; float: left; display: none">
                            <asp:Button ID="btnImport" runat="server" Text="Import To Excel" CssClass="button_color action green" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkBtnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
