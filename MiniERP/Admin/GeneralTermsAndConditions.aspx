<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="GeneralTermsAndConditions.aspx.cs" Inherits="MiniERP.Admin.TermAndConditionMaster" %>

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
                        General Terms And Conditions </h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="40%">
                                General Terms And Conditions For <span style="color: Red">*</span>
                                </td>
                                <td class="center" width="75%">
                                    <%--<asp:CheckBoxList ID="chbxlst" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>--%>
                                    <asp:RadioButtonList ID="rdbtnlst" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" width="25%">
                                    Name <span style="color: Red">*</span>
                                </td>
                                <td class="center" width="75%">
                                    <asp:TextBox ID="txtName" CssClass="mlttext" runat="server" MaxLength="300" TextMode="MultiLine"
                                        TabIndex="2" Width="480px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                        Display="None" ForeColor="Red" ErrorMessage="Please Enter Name." SetFocusOnError="true"
                                        ValidationGroup="terms"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="revName" runat="server" ValidationGroup="terms" ForeColor="Red" 
                                Display="None" SetFocusOnError="true" ControlToValidate="txtName"></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    <%--Description--%>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtdescription" CssClass="mlttext" runat="server" TextMode="MultiLine"
                                        MaxLength="200" TabIndex="3" Visible="false"></asp:TextBox>
                                    <%--<asp:RegularExpressionValidator ID="revDescription" runat="server" ValidationGroup="terms" ForeColor="Red" 
                                Display="None" ControlToValidate="txtdescription" SetFocusOnError="true" ></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4" CssClass="button_color action red"
                            OnClick="btnCancel_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" ValidationGroup="terms"
                            CssClass="button_color action blue" OnClick="btnUpdate_Click" TabIndex="5" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="terms" CssClass="button_color action green"
                            OnClick="btnSave_Click" TabIndex="3" />
                    </div>
                    <br />
                    <br />
                    <br />
                    <asp:ValidationSummary ID="valSumTerms" runat="server" ShowMessageBox="True" ShowSummary="False"
                        ValidationGroup="terms" />
                    <asp:GridView ID="gvterms" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowCommand="gvterms_RowCommand" 
                        OnPageIndexChanging="gvterms_PageIndexChanging" AllowPaging="True">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Name" ApplyFormatInEditMode="true"  HeaderStyle-Width="47%" />
                          <%--  <asp:BoundField DataField="Name" ItemStyle-Width="80%"
                             ItemStyle-Wrap="false" HeaderText="Name" ApplyFormatInEditMode="true" ControlStyle-Width="300"
                              HtmlEncode="false" HeaderStyle-Width="47%" />--%>
                          
                          
                          
                            <asp:BoundField DataField="TermAndConditionName" HeaderText="General Terms And Conditions For"
                                HeaderStyle-Width="47%" />
                            <asp:BoundField DataField="Description" Visible="false" HeaderText="Description"  />
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("TermsId") %>'
                                        CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145 " >
                                        <asp:Image ID="imgEdit" runat="server" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("TermsId") %>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure, You Want To Delete This Term & Condition?') "
                                        ToolTip="Delete" CssClass="button icon186 ">
                                        <asp:Image ID="imgDelete" runat="server"  /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
