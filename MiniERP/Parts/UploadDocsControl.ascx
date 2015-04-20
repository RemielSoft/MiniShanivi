<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadDocsControl.ascx.cs"
    Inherits="MiniERP.Parts.UploadDocsControl" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table class="table table-bordered table-striped table-condensed">
            <tr>
                <td class="center" id="ajaxupload" runat="server">
                    <asp:FileUpload ID="FileUpload_Control" runat="server" />
                    <asp:Button ID="btn_upload_doc" runat="server" Text="Upload" CausesValidation="false"
                        OnClick="btn_upload_Click" />
                </td>
                <td class="center">
                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" Height="80px" Width="100%">
                        <div class="box-content">
                            <asp:GridView ID="gv_documents" runat="server" CellPadding="4" ForeColor="#333333"
                                GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" EmptyDataText="No-Documents Uploaded."
                                AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="gv_documents_RowCommand"
                                Width="250px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="File">
                                        <ItemTemplate>
                                        <!--lst_documents[Index].Path + @"\" + lst_documents[Index].Replaced_Name-->
                                           <a target="_blank" href='<%#Eval("FileCompletePath")%>'><%#Eval("Original_Name")%> </a>
                                           <%-- <asp:LinkButton ID="lbtn_file" target="_blank" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                                                CommandName="OpenFile" Text='<%#Eval("Original_Name")%>' CssClass='btn'></asp:LinkButton>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandName="FileDelete"
                                                CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                CssClass="button icon186"></asp:LinkButton>
                                            <%--<asp:ImageButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandName="FileDelete"
                                                CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                ToolTip="Delete" CssClass="button icon186" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_upload_doc" />
    </Triggers>
</asp:UpdatePanel>
