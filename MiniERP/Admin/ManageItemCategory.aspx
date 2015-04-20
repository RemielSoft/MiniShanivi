<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ManageItemCategory.aspx.cs" Inherits="MiniERP.Admin.ManageItemCategory" %>


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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div>
            <script language="javascript" type="text/javascript">
                function MaxLenghtMultilineTextBox(txt, maxlength) {
                    try {
                        if (txt.valueOf.length > (maxlength - 1))
                            return false;
                    }
                    catch (e) {

                    }
                }
    </script>
        </div>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Manage Category Level</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="25%">
                                    Category Level <span style="color: Red">*</span>
                                </td>
                                <td class="center" width="75%">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="TextBox" MaxLength="20" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ForeColor="Red" ErrorMessage="Please Enter Category Level"
                                        ControlToValidate="txtName" SetFocusOnError="true" ValidationGroup="ItemCategory" Display="None"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName"
                                        Display="None" ValidationGroup="ItemCategory" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="center">
                                    Category Level Start Range <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtStart" runat="server" CssClass="TextBox" MaxLength="7" 
                                        TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvStart" runat="server" ForeColor="Red" ErrorMessage="Please Enter Category Level Start Range"
                                        ControlToValidate="txtStart" SetFocusOnError="true" ValidationGroup="ItemCategory" Display="None"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revStartRange" runat="server" ControlToValidate="txtStart"
                                        Display="None" ValidationGroup="ItemCategory" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>&nbsp;
                                        <asp:DropDownList ID="ddlRanges" runat="server" TabIndex="2" 
                                        ></asp:DropDownList>                                        
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Category Level End Range <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtEnd" runat="server" CssClass="TextBox" MaxLength="7" 
                                        TabIndex="4"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEnd" runat="server" ForeColor="Red" ErrorMessage="Please Enter Category Level End Range"
                                        ControlToValidate="txtEnd" SetFocusOnError="true" ValidationGroup="ItemCategory" Display="None"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEndRange" runat="server" ControlToValidate="txtEnd"
                                        Display="None" SetFocusOnError="true" ValidationGroup="ItemCategory" ForeColor="Red"></asp:RegularExpressionValidator>
                                        
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Description
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" 
                                         onkeypress="MaxLenghtMultilineTextBox(this,15)" CssClass="mlttext" 
                                        TabIndex="5"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="txtdescription"
                                        Display="None" ValidationGroup="ItemCategory" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_color action red"
                            OnClick="btnCancel_Click" TabIndex="7" />
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="ItemCategory" 
                            Text="Save" CssClass="button_color action green"
                            OnClick="btnSave_Click" TabIndex="6" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"  
                            CssClass="button_color action blue" Visible="false" 
                            ValidationGroup="ItemCategory" OnClick="btnUpdate_Click" TabIndex="8" />
                    </div>
                    <br /><br />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false"
                     ValidationGroup="ItemCategory" />
                
            <asp:GridView ID="gvItemCategory" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="gvItemCategory_PageIndexChanging"
                OnRowCommand="gvItemCategory_RowCommand"  AllowPaging="True">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("ItemCategoryName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Range" ItemStyle-CssClass="taright">
                        <ItemTemplate>
                            <asp:Label ID="lblStart" runat="server" Text='<%#Eval("StartRange") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Range" ItemStyle-CssClass="taright">
                        <ItemTemplate>
                            <asp:Label ID="lblEnd" runat="server" Text='<%#Eval("EndRange") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("ItemCategoryId") %>'
                                CommandName="cmdEdit" ToolTip="Edit" CssClass="button icon145 ">
                                <asp:Image ID="imgEdit" runat="server"  /></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ItemCategoryId") %>'
                                CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure, You Want To Delete This Item Category ?') "
                                ToolTip="Delete" CssClass="button icon186">
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
