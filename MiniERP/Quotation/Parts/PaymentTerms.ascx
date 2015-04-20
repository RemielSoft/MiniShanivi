<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentTerms.ascx.cs"
    Inherits="MiniERP.Parts.PaymentTerms" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    Payment Terms</h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped table-condensed" id="tbl_payment"
                    runat="server">
                    <tbody>
                        <tr>
                            <td class="center" colspan="6">
                                <asp:Label ID="lbl_duplicate_payment_term" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                Payment Type<span style="color: Red"> *</span>&nbsp;
                            </td>
                            <td class="center">
                                <asp:DropDownList ID="ddl_payment_type" CssClass="dropdown" Width="120px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_payment_type_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_payment_type" runat="server" ControlToValidate="ddl_payment_type"
                                    Display="None" ErrorMessage="Please Select Payment Type" ForeColor="Red" InitialValue="0"
                                    SetFocusOnError="True" ToolTip="Payment Type" ValidationGroup="payment_term_add"></asp:RequiredFieldValidator>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblPaymentTypeText" Text="Number Of Days" runat="server"></asp:Label><span
                                    style="color: Red">*</span>&nbsp;
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txt_number_of_days" CssClass="TextBox" Width="100px" runat="server" MaxLength="3"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfv_no_of_days" runat="server" ControlToValidate="txt_number_of_days"
                                    Display="None" ErrorMessage="Please Enter Number of Days" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="payment_term_add"></asp:RequiredFieldValidator>--%>
                                <ajaxtoolkit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txt_number_of_days"
                                    FilterType="Numbers" /> 
                            </td>
                            <td class="center">
                                Percentage Value (%)<span style="color: Red"> *</span>&nbsp;
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtPercentageValue" CssClass="TextBox" Width="100px" runat="server" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPercentageValue" runat="server" ControlToValidate="txtPercentageValue"
                                    Display="None" ErrorMessage="Please Enter Percentage Value" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="payment_term_add"></asp:RequiredFieldValidator>
                                <ajaxtoolkit:FilteredTextBoxExtender ID="ftbePercentageValue" runat="server" TargetControlID="txtPercentageValue"
                                    FilterType="Custom, Numbers" ValidChars="." />
                            </td>
                        </tr>
                        <tr> 
                            <td class="center">
                                Payment Description
                            </td>
                            <td class="center" colspan="5">
                                <asp:TextBox ID="txtPaymentDescription" CssClass="mlttext" Width="95.3%" runat="server" TextMode="MultiLine"
                                    MaxLength="150"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revPaymentDescription" runat="server" ControlToValidate="txtPaymentDescription"
                                    Display="None" ForeColor="Red" SetFocusOnError="True" ValidationGroup="save"></asp:RegularExpressionValidator>
                            </td>
                            
                        </tr>
                    </tbody>
                </table>
                <br />
                <div class="Button_align" id="dv_button" runat="server">
                    <asp:Button ID="btn_cancel" runat="server" CausesValidation="false" CssClass="button_color action red"
                        Text="Cancel" OnClick="btn_cancel_Click" />
                    <asp:Button ID="btn_add" runat="server" Text="Add" CssClass="button_color action"
                        ValidationGroup="payment_term_add" OnClick="btn_add_Click" />
                    <br />
                </div>
                <asp:ValidationSummary ID="vs_delivery_schedule" runat="server" ShowMessageBox="True"
                    ShowSummary="false" ValidationGroup="payment_term_add" />
                <asp:GridView ID="gv_paymnetTerms" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Payment Terms Generated...."
                    OnRowCommand="gv_paymnetTerms_RowCommand" OnPageIndexChanging="gv_paymnetTerms_PageIndexChanging"
                    OnRowDeleting="gv_paymnetTerms_RowDeleting" OnRowEditing="gv_paymnetTerms_RowEditing">
                    <Columns>
                        <asp:TemplateField HeaderText="Payment Type">
                            <ItemTemplate>
                                <asp:Label ID="lblpaymentType" runat="server" Text='<%#Eval("PaymentType.Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Number Of Days">
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfDays" runat="server" Text='<%#Eval("NumberOfDays") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Percentage Value (%)">
                            <ItemTemplate>
                                <asp:Label ID="lblPercentageValue" runat="server" Text='<%#Eval("PercentageValue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Description">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentDescription" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("PaymentDescription")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                    CommandArgument='<%#Container.DataItemIndex%>' ToolTip="Edit" CssClass="button icon145" Visible="false" ></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                    CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Payment Term?') "
                                    ToolTip="Delete" CssClass="button icon186" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
