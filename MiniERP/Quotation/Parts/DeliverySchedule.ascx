<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliverySchedule.ascx.cs"
    Inherits="MiniERP.Parts.DeliverySchedule" %>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/progressbar.gif" Height="100px" Width="100px" />--%>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <%--<Triggers>
        <asp:AsyncPostBackTrigger ControlID="Image1" />
    </Triggers>--%>
    <ContentTemplate>
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    Delivery Schedule</h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped table-condensed" id="tbl_delivery"
                    runat="server">
                    <tbody>
                        <tr>
                            <td class="center" colspan="2">
                                <asp:Label ID="lbl_duplicate_activity" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rw_activity" runat="server">
                            <td class="center">
                                Service Description<span style="color: Red">*</span>&nbsp;
                            </td>
                            <td class="center">
                                <asp:DropDownList ID="ddl_Activity_Desc_DS" Width="300px" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_activity_description" runat="server" ControlToValidate="ddl_Activity_Desc_DS"
                                    Display="None" ErrorMessage="Please Select Activity Description" ForeColor="Red"
                                    InitialValue="0" SetFocusOnError="True" ToolTip="Activity Description" ValidationGroup="deliveryschedule_add"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="rw_item" runat="server">
                            <td class="center">
                                Item<span style="color: Red">*</span>&nbsp;
                            </td>
                            <td class="center">
                                <asp:DropDownList ID="ddl_Item" Width="200px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_Item_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_item" runat="server" ControlToValidate="ddl_Item"
                                    Display="None" ErrorMessage="Please Select Item" ForeColor="Red" InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="deliveryschedule_add"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdf_number_of_unit" runat="server" />
                            </td>
                            <td class="center">
                                Item Quantity<span style="color: Red">*</span>&nbsp;
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txt_Item_Quantity" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:Label ID="lbl_unit_of_measurement" runat="server"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfv_Item_Quantity" runat="server" ControlToValidate="txt_Item_Quantity"
                                    Display="None" ErrorMessage="Please Enter Item Quantity" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="deliveryschedule_add"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rev_Item_Quantity" runat="server" ControlToValidate="txt_Item_Quantity"
                                    Display="None" ErrorMessage="Only Numeric Value is allowed in Item Quantity"
                                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="deliveryschedule_add"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                Delivery Date <span style="color: Red">*</span>
                            </td>
                            <td class="center" colspan="3">
                                <asp:TextBox ID="txt_Delivery_Date_DS" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:LinkButton ID="imgbtn_calender" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                <ajaxtoolkit:CalendarExtender ID="cal_ext_delivery" runat="server" Format="MM/dd/yyyy"
                                    TargetControlID="txt_Delivery_Date_DS" PopupButtonID="imgbtn_calender">
                                </ajaxtoolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfv_delivery_date" runat="server" ControlToValidate="txt_Delivery_Date_DS"
                                    Display="None" ErrorMessage="Please Enter Delivery Date" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="deliveryschedule_add"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div class="Button_align" id="dv_button" runat="server">
                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="button_color action red"
                        OnClick="btn_cancel_Click" />
                    <asp:Button ID="btn_add" runat="server" Text="Add" CssClass="button_color action"
                        OnClick="btn_add_Click" ValidationGroup="deliveryschedule_add" />
                    <br />
                </div>
                <div>
                    <asp:ValidationSummary ID="vs_delivery_schedule" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="deliveryschedule_add" />
                    <br />
                </div>
                <asp:GridView ID="gv_Delivery_Schedule" runat="server" AutoGenerateColumns="False"
                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                    EmptyDataText="No-Schedule Generated for Activity Description" OnRowCommand="gv_Delivery_Schedule_RowCommand"
                    OnPageIndexChanging="gv_Delivery_Schedule_PageIndexChanging" OnRowDeleting="gv_Delivery_Schedule_RowDeleting"
                    OnRowEditing="gv_Delivery_Schedule_RowEditing">
                    <Columns>
                        <asp:TemplateField HeaderText="Service Description">
                            <ItemTemplate>
                                <asp:Label ID="lblActivityDescription" runat="server" Text='<%#Eval("ActivityDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Number of Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblNumberOfUnit" runat="server" Text='<%#Eval("ActualNumberOfUnit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("ItemQuantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivery Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#Bind("DeliveryDate","{0:MMMM d, yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="DeliveryDate" DataFormatString="{0:MMMM d, yyyy}"  HeaderText="Delivery Date" />--%>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                    CommandArgument='<%#Container.DataItemIndex%>' ToolTip="Edit" CssClass="button icon145" ></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                    CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Delivery Schedule?') "
                                    ToolTip="Delete" CssClass="button icon186" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
