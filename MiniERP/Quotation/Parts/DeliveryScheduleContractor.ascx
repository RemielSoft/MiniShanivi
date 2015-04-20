<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliveryScheduleContractor.ascx.cs"
    Inherits="MiniERP.Quotation.Parts.DeliveryScheduleContractor" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    Delivery Schedule</h2>
            </div>
            <div class="box-content">
                <asp:GridView ID="gv_Delivery_Schedule" runat="server" AutoGenerateColumns="False"
                    AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                    EmptyDataText="No-Items Available" OnRowDataBound="gv_Delivery_Schedule_RowDataBound"
                    OnRowCommand="gv_Delivery_Schedule_RowCommand">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#Container.DataItemIndex + 1%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Description">
                            <ItemTemplate>
                                <asp:Label ID="lblActivityDescription" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("DeliverySchedule.ActivityDescription")) %>'></asp:Label>
                                <asp:HiddenField ID="hdfActivityId" runat="server" Value='<%#Eval("DeliverySchedule.ActivityDescriptionId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:Label ID="lblItem" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ItemName")) %>'></asp:Label>
                                <asp:HiddenField ID="hdfItemId" runat="server" Value='<%#Eval("Item.ItemId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification">
                            <ItemTemplate>
                                <asp:Label ID="lblSpecification" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.ModelSpecificationName")) %>'></asp:Label>
                                <asp:HiddenField ID="hdfSpecificationId" runat="server" Value='<%#Eval("Item.ModelSpecification.ModelSpecificationId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Scheduled Quantity" HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblScheduledQuantity" runat="server" Text='<%#Eval("Service_Detail.Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Quantity Left" HeaderStyle-Width="60px">
                            <ItemTemplate>
                                <asp:Label ID="lblQuantityLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblUnitt" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.UnitMeasurement.Name")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Schedule Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_Schedule_Quantity" runat="server" Width="60px" Text='<%#Eval("UnitLeft") %>'
                                    ViewStateMode="Enabled"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Schedule Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_shedule_date" CssClass="TextBox" runat="server" Enabled="false"
                                    Width="80px"></asp:TextBox>
                                <asp:LinkButton ID="img_btn_calander_order" runat="server" ToolTip="Select Date"
                                    CssClass="Calender icon175" ></asp:LinkButton>
                                <ajaxtoolkit:CalendarExtender ID="cal_ext_order_date" runat="server"
                                    Format="dd/MM/yyyy" TargetControlID="txt_shedule_date" PopupButtonID="img_btn_calander_order">
                                </ajaxtoolkit:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Add">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAdd" runat="server" CssClass="button icon3" CommandName="add"
                                    CommandArgument='<%#Container.DataItemIndex%>' ToolTip="Add New Schedule"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="box-content" id="divFinal" runat="server">
                <asp:GridView ID="gv_Final_Delivery_Schedule" runat="server" AutoGenerateColumns="False"
                    AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                    EmptyDataText="No-Items Available" OnRowCommand="gv_Final_Delivery_Schedule_RowCommand"
                    OnRowDataBound="gv_Final_Delivery_Schedule_RowDataBound" OnRowDeleting="gv_Final_Delivery_Schedule_RowDeleting"
                    OnRowEditing="gv_Final_Delivery_Schedule_RowEditing">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#Container.DataItemIndex + 1%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Description">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Activity" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("ItemDescription")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Item" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Specification" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Specification")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Quantity" runat="server" Text='<%#Eval("ItemQuantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("SpecificationUnit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Schedule Date">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Schedule_Date" runat="server" Text='<%#Bind("DeliveryDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="del"
                                    CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Item?') "
                                    ToolTip="Delete" CssClass="button icon186" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
