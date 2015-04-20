<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="Taxes.aspx.cs" Inherits="MiniERP.Admin.Taxes" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Taxes</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="25%">
                                    Service Tax<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:TextBox ID="txtServiceTax" runat="server" CssClass="TextBox" MaxLength="10" TabIndex="2"></asp:TextBox>
                                    %
                                    <asp:RequiredFieldValidator ID="rfvServiceTax" runat="server" ControlToValidate="txtServiceTax"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Service Tax"
                                        ValidationGroup="tax"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="revServiceTax" runat="server" ValidationGroup="tax"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtServiceTax"></asp:RegularExpressionValidator>--%>
                                        <asp:RangeValidator ID="rngServiceTax" runat="server" ValidationGroup="tax" ForeColor="Red"
                                         Display="None" SetFocusOnError="true" ControlToValidate="txtServiceTax" 
                                        MaximumValue="9999999999" MinimumValue="0" Type="Double" ></asp:RangeValidator>
                                </td>
                                <td class="center" width="25%">
                                    VAT
                                </td>
                                <td class="center" width="25%">
                                    <asp:TextBox ID="txtVat" runat="server" CssClass="TextBox" MaxLength="10" TabIndex="3" ></asp:TextBox>                                        
                                    %
                                    <%--<asp:RegularExpressionValidator ID="revVat" runat="server" ValidationGroup="tax"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtVat"></asp:RegularExpressionValidator>--%>
                                        <asp:RangeValidator ID="rngVat" runat="server" ValidationGroup="tax" ForeColor="Red"
                                         Display="None" SetFocusOnError="true" ControlToValidate="txtVat" 
                                        MaximumValue="9999999999" MinimumValue="0" Type="Double" ></asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    CST (with C Form)
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtCSTWithForm" runat="server" CssClass="TextBox" MaxLength="10" TabIndex="4"></asp:TextBox>
                                    %
                                    <%--<asp:RegularExpressionValidator ID="revCSTWith" runat="server" ValidationGroup="tax"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtCSTWithForm"></asp:RegularExpressionValidator>--%>
                                        <asp:RangeValidator ID="rngCST" runat="server" ValidationGroup="tax" ForeColor="Red"
                                         Display="None" SetFocusOnError="true" ControlToValidate="txtCSTWithForm" 
                                        MaximumValue="9999999999" MinimumValue="0" Type="Double" ></asp:RangeValidator>
                                </td>
                                <td class="center">
                                    CST (Without C Form)
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtCSTWithoutForm" CssClass="TextBox" runat="server" MaxLength="10" TabIndex="5"></asp:TextBox>
                                    %
                                    <%--<asp:RegularExpressionValidator ID="revCSTWithout" runat="server" ValidationGroup="tax"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtCSTWithoutForm"></asp:RegularExpressionValidator>--%>
                                        <asp:RangeValidator ID="rngCSTWithout" runat="server" ValidationGroup="tax" ForeColor="Red"
                                         Display="None" SetFocusOnError="true" 
                                        ControlToValidate="txtCSTWithoutForm" MaximumValue="9999999999" MinimumValue="0" 
                                        Type="Double" ></asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Freight
                                </td>
                                <td class="center">
                                    <img alt="" src="../Images/rupee_symbol5.png"   /> 
                                    <asp:TextBox ID="txtFreight" CssClass="TextBox" runat="server" MaxLength="10" TabIndex="6"></asp:TextBox>
                                     
                                    <%--<asp:RegularExpressionValidator ID="revFreight" runat="server" ValidationGroup="tax"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtFreight"></asp:RegularExpressionValidator>--%>
                                        <asp:RangeValidator ID="rngFreight" runat="server" ValidationGroup="tax" ForeColor="Red"
                                         Display="None" SetFocusOnError="true" ControlToValidate="txtFreight" 
                                        MaximumValue="9999999999" MinimumValue="0" Type="Double" ></asp:RangeValidator>
                                </td>
                                <td class="center">
                                    Packaging
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtPackaging" CssClass="TextBox" runat="server" MaxLength="10" TabIndex="7"></asp:TextBox>
                                    <img alt="" src="../Images/rupee_symbol5.png"   />
                                    <%--<asp:RegularExpressionValidator ID="revPackaging" runat="server" ValidationGroup="tax"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtPackaging"></asp:RegularExpressionValidator>--%>
                                        <asp:RangeValidator ID="rngPackaging" runat="server" ValidationGroup="tax" ForeColor="Red"
                                         Display="None" SetFocusOnError="true" ControlToValidate="txtPackaging" 
                                        MaximumValue="9999999999" MinimumValue="0" Type="Double" ></asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Discount Mode<span style="color: Red;">*</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddlDiscountMode" runat="server" CssClass="dropdown" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDiscountMode_SelectedIndexChanged" TabIndex="8">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlDiscount" runat="server" InitialValue="0" ControlToValidate="ddlDiscountMode"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Select Discount Mode"
                                        ValidationGroup="tax"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="center">
                                    Discount<span style="color: Red;">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalDiscount" CssClass="TextBox" runat="server" MaxLength="10" 
                                        TabIndex="9"></asp:TextBox>
                                    <%--<asp:RegularExpressionValidator ID="revDiscount" runat="server" ValidationGroup="tax"
                                        ForeColor="Red" Display="None" SetFocusOnError="true" ControlToValidate="txtTotalDiscount"></asp:RegularExpressionValidator>--%>
                                    <asp:RequiredFieldValidator ID="rfvTotalDiscount" runat="server" ControlToValidate="txtTotalDiscount"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Total Discount"
                                        ValidationGroup="tax"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rngTotalDiscount" runat="server" 
                                        ValidationGroup="tax" ForeColor="Red"
                                         Display="None" SetFocusOnError="true" 
                                        ControlToValidate="txtTotalDiscount" MaximumValue="9999999999" MinimumValue="0" 
                                        Type="Double" ></asp:RangeValidator>
                                    <asp:Label ID="lblPerInr" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_color action red"
                            OnClick="btnCancel_Click" TabIndex="11" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" CssClass="button_color action blue"
                            ValidationGroup="tax" OnClick="btnUpdate_Click" TabIndex="12" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="tax" CssClass="button_color action green"
                            OnClick="btnSave_Click" TabIndex="10" />
                    </div>
                    <br />
                    <br />
                    <asp:ValidationSummary ID="valSumDepartment" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="tax" />
                    <asp:GridView ID="gvTax" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" PageSize="10" AlternatingRowStyle-CssClass="alt"
                        OnRowCommand="gvTax_RowCommand" OnRowDataBound="gvTax_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ServiceTax" ItemStyle-CssClass="taright" HeaderText="Service tax (%)" />
                            <asp:BoundField DataField="VAT" ItemStyle-CssClass="taright" HeaderText="Vat (%)" />
                            <asp:BoundField DataField="CSTWithCForm" ItemStyle-CssClass="taright" HeaderText="CST (with C Form) (%)" />
                            <asp:BoundField DataField="CSTWithoutCForm" ItemStyle-CssClass="taright" HeaderText="CST (Without C Form) (%)" />
                            <asp:BoundField DataField="Freight" ItemStyle-CssClass="taright" HeaderText="Freight(INR)" />
                            <asp:BoundField DataField="TotalDiscount" ItemStyle-CssClass="taright" HeaderText="Total Discount (%/INR)" />
                            <%-- <asp:BoundField DataField="Name" HeaderText="Discount Mode" />--%>
                            <asp:TemplateField HeaderText="Discount Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscountMode" runat="server" Text='<%# Eval("DiscountMode.Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Packaging" ItemStyle-CssClass="taright" HeaderText="Packaging(INR)" />
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("TaxId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit">
                                        <asp:Image ID="imgEdit" runat="server" CssClass="button icon145 " /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("TaxId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure, You Want To Delete These Taxes ?') "
                                        ToolTip="Delete">
                                        <asp:Image ID="imgDelete" runat="server" CssClass="button icon186 " /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <%--<table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <table class="mGrid">
        <tr>
            
            <th>
                Service tax (%)
            </th>
            <th>
               Vat (%)
            </th>
            <th>
                CST (with C Form) (%)
            </th>
            <th>
                CST (Without C Form) (%)
            </th>
            <th>
                Discount (Fix/Percentage)
            </th>
            <th>
                Total Discount (%/INR)
            </th>
            <th>
                Total Net Value (INR)
            </th>
            
            
        </tr>
        <tr>
            
            <td>
                3%
            </td>
            <td>
                2%
            </td>
            <td>
                0%
            </td>
            <td>
                0%
            </td>
            <td>
                Fix
            </td>
            <td>
                879 INR
            </td>
            <td>
                306,600 INR
            </td>
            
            
        </tr>
        
        
    </table>
                        </tbody>
                    </table>--%>
            <br />
            <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
