<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralTermConditions.ascx.cs"
    Inherits="MiniERP.Parts.TermConditions" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    General Terms And Conditions</h2>
            </div>
            <div class="box-content">
                <div id="dv_terms" runat="server">
                    <asp:GridView ID="gv_TermConditions_Master" runat="server" AlternatingRowStyle-CssClass="alt"
                        AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No Terms & Conditions Added By Admin"
                        PagerStyle-CssClass="pgr" OnRowDataBound="gv_TermConditions_Master_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="56px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chbx_select_all" runat="server" Text="Select" AutoPostBack="true"
                                        OnCheckedChanged="on_check_uncheck_all" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbx_Terms" runat="server" AutoPostBack="true" OnCheckedChanged="on_chbx_Terms" />
                                    <asp:HiddenField ID="hdf_term_master_id" runat="server" Value='<%#Eval("TermsId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="General Terms And Conditions">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_terms_master" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Name")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="Button_align" id="dv_button" runat="server">
                    <asp:Button ID="btnAddTerm" runat="server" Text="Add" CausesValidation="false" CssClass="button_color action"
                        OnClick="btnAddTerm_Click" />
                    <br />
                    <asp:Label ID="lbl_duplicate_term_condition" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                </div>
                <table class="table table-bordered table-striped table-condensed" id="tbl_add_term"
                    runat="server">
                    <tbody>
                        <tr>
                            <td class="center" width="40%">
                               General Terms And Conditions
                            </td>
                            <td class="center" width="75%">
                                <div style="padding-right: 40%">
                                    <asp:Button ID="btn_cancel" runat="server" CausesValidation="false" CssClass="button_color action red"
                                        OnClick="btn_cancel_Click" Text="Cancel" />
                                    <asp:Button ID="btn_add_final" runat="server" Text="Add" CssClass="button_color action"
                                        OnClick="btn_add_final_Click" ValidationGroup="term_condition_add" />
                                </div>
                                <div>
                                    <asp:TextBox ID="txt_term" Width="200px" Text="" runat="server" CausesValidation="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_term_condition" runat="server" ControlToValidate="txt_term"
                                        Display="None" ErrorMessage="Please Enter Terms & Conditions" ForeColor="Red"
                                        SetFocusOnError="True" ToolTip="Terms & Conditions" ValidationGroup="term_condition_add"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <asp:GridView ID="gv_final_TermsConditions" runat="server" AlternatingRowStyle-CssClass="alt"
                    AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No Term and condtions Added"
                    PagerStyle-CssClass="pgr" OnPageIndexChanging="gv_final_TermsConditions_PageIndexChanging"
                    OnRowCommand="gv_final_TermsConditions_RowCommand" OnRowDataBound="gv_final_TermsConditions_RowDataBound"
                    OnRowDeleting="gv_final_TermsConditions_RowDeleting" OnRowEditing="gv_final_TermsConditions_RowEditing">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#Container.DataItemIndex + 1%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" General Terms And Conditions">
                            <ItemTemplate>
                                <asp:Label ID="lblTerms" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Name")) %>'></asp:Label>
                                <asp:HiddenField ID="hdf_term_id" runat="server" Value='<%#Eval("TermsId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandArgument="<%#Container.DataItemIndex%>"
                                    CommandName="Edit" ToolTip="Edit" CssClass="button icon145" ></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument="<%#Container.DataItemIndex%>"
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure to delete this Term and Condition?') "
                                    ToolTip="Delete" CssClass="button icon186" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
