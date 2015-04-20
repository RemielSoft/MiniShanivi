<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiveMaterialCompanyWorkOrder.ascx.cs" 
  Inherits="MiniERP.Quality.Parts.ReceiveMaterialCompanyWorkOrder" ViewStateMode="Enabled"  %>

<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>

<script type="text/javascript">
    function Count(text, long) {

        var maxlength = new Number(long); // Change number to your max length.

        if (document.getElementById('<%=txt_description.ClientID%>').value.length > maxlength) {

            text.value = text.value.substring(0, maxlength);

            alert(" More than " + long + " Characters are not Allowed in Work Order Description.");

        }

    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="box span12">
    <div class="box-header well">
                <h2>
                    <asp:Label ID="lbl_head" runat="server" Text="Company Work Order Receive Material"></asp:Label>
                </h2>
                <%--<div style="margin: 35px 0px 0px 0px; padding-right: 300px;">
                </div>--%>
            </div>
            <div>
                <asp:Literal ID="ltrl_error_msg" runat="server"></asp:Literal>
            </div>
            <div class="box-content">
                 <%--<table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">
                                <asp:Label ID="lblCompayWorkOrderNumber" runat="server" Text="Company Work Order Number"></asp:Label>
                            </td>
                            <td class="center" width="75%">
                                <asp:TextBox ID="txt_search" CssClass="TextBox" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="lnkbtn_search" runat="server" CausesValidation="false" ToolTip="Search"
                                   CommandName="Search" CommandArgument="s" CssClass="Search icon198" ></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtn_copy" runat="server" CausesValidation="false" ToolTip="Copy"
                                    CommandName="Copy" CommandArgument="c" CssClass="Search icon55" ></asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>--%>
           
           <%-- <br />--%>
            <asp:Panel ID="pnl_CWON" runat="server" CssClass="mitem">
                <table class="table table-bordered table-striped table-condensed" runat="server">
                        
                    <tbody>
                          <tr id="rw_contractor" runat="server">
                                <td class="center" width="25%">
                                    <asp:Label ID="lbl_CWO_Number" runat="server" Text="Company Work Order Number"></asp:Label><span
                                        style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_CWO_Number" Width="150px" runat="server" 
                                        onselectedindexchanged="ddl_CWO_Number_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_contractor" runat="server" ControlToValidate="ddl_CWO_Number"
                                        Display="None" ErrorMessage="Please Select Company Work Order Number" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </td>
                                 <td class="center" width="25%">
                                    Work Order Number<span style="color: Red">*</span>
                                </td>
                                 <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_work_order_number" CssClass="dropdown" runat="server">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfv_work_order_number" runat="server" ControlToValidate="ddl_work_order_number"
                                        Display="None" ErrorMessage="Please Select Work Order Number" ForeColor="Red"
                                        InitialValue="0" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </td>
                       </tr>
                        <tr>
                                <td class="center">
                                    <asp:Label ID="lbl_receive_date" runat="server" Text="Receive Date"></asp:Label>
                                    
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_receive_date" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                    <%--<asp:LinkButton ID="img_btn_calander_order" runat="server" ToolTip="Select Date"
                                        CssClass="Calender icon175" ></asp:LinkButton>--%>
                                    <asp:LinkButton ID="lnk_btn_calander_order" runat="server" CssClass="Calender icon175" ToolTip="Select Date">
                                    </asp:LinkButton>
                                    <ajaxtoolkit:CalendarExtender ID="cal_ext_receive_material_date" runat="server"
                                    Format="dd/MM/yyyy" TargetControlID="txt_receive_date" PopupButtonID="lnk_btn_calander_order">
                                    </ajaxtoolkit:CalendarExtender>
                                    
                                    
                                </td>
                                <td class="center" >
                                    <asp:Label ID="lbl_description" runat="server" Text="Description"></asp:Label>
                               </td>
                               <td class="center" >
                                    <asp:TextBox ID="txt_description" CssClass="TextBox" TextMode="MultiLine" runat="server"
                                    onKeyUp="javascript:Count(this,250);" onChange="javascript:Count(this,250);" Columns="50"></asp:TextBox>
                               </td>
                     </tr>
                      <tr>
                            <td class="center" width="25%">
                                <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                            </td>
                            <td colspan="3" class="center" width="75%">
                                <div>
                                    <table class="table table-bordered table-striped table-condensed">
                                        <tr>
                                            <td class="center" id="ajaxupload" runat="server">
                                                <asp:FileUpload ID="FileUpload_Control" runat="server" />
                                                <asp:Button ID="btn_upload" runat="server" Text="Upload" 
                                                    CausesValidation="false" onclick="btn_upload_Click"
                                                     />
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
                                                                        <asp:LinkButton ID="lnk_btn_file" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                                                                            CommandName="Open_File" Text='<%#Eval("Original_Name")%>' CausesValidation="false" ></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandName="FileDelete"
                                                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                                            ToolTip="Delete" CssClass="button icon186" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnl_activity" runat="server" CssClass="mitem">
                <table class="table table-bordered table-striped table-condensed" id="tbl_itemTransaction"
                        runat="server">
                        <tbody>
                        <tr>
                                <td class="center" colspan="4">
                                    <asp:Label ID="lbl_duplicate_msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                                <tr>
                                <td class="center" width="25%">
                                    Item<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_item" runat="server" CssClass="dropdown"
                                        AutoPostBack="True" onselectedindexchanged="ddl_item_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_item" runat="server" ControlToValidate="ddl_item"
                                        Display="None" ErrorMessage="Please Select Item" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                </td>
                                <td class="center" width="25%">
                                    Specification<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_item_model" CssClass="dropdown" runat="server"
                                        AutoPostBack="True" 
                                        onselectedindexchanged="ddl_item_model_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_item_model" runat="server" ControlToValidate="ddl_item_model"
                                        Display="None" ErrorMessage="Please Select Specification" ForeColor="Red" InitialValue="0" 
                                        SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td class="center">
                                    Category Level
                                </td>
                                <td class="center">
                                    <asp:Label ID="lbl_category_level" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdf_category_level_id" runat="server" />
                                </td>
                                 <td class="center">
                                    Store
                                </td>
                                <td class="center" >
                                    <asp:DropDownList ID="ddlStore" runat="server"></asp:DropDownList>
                                </td>
                                <td class="center">
                                    Brand
                                </td>
                                <td class="center" >
                                    <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Quantity<span style="color: Red">*</span>&nbsp;
                                </td>
                                <td class="center" colspan="3">
                                    <asp:TextBox ID="txt_Number_of_Unit" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="lbl_unit_of_measurement" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdf_unit_of_measurement" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfv_number_of_unit" runat="server" ControlToValidate="txt_Number_of_Unit"
                                        Display="None" ErrorMessage="Please Enter Quantity" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_number_of_unit" runat="server" ControlToValidate="txt_Number_of_Unit"
                                        Display="None" ErrorMessage="Only Numeric Value is allowed in Quantity" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="add"></asp:RegularExpressionValidator>
                                   
                                </td>
                            </tr>
                        </tbody>
                </table>
                <br />
                <div class="Button_align" id="div_button" runat="server">
                        <asp:Button ID="btn_cancel" runat="server" CausesValidation="false" Text="Reset"
                            CssClass="button_color action red" onclick="btn_cancel_Click"  />
                        <asp:Button ID="btn_add" runat="server" Text="Add" CssClass="button_color action"
                            OnClick="btn_add_Click" ValidationGroup="add" />
                        <%-- <asp:Literal ID="Literal1" runat="server"></asp:Literal>--%>
                        <asp:ValidationSummary ID="vs_itemTransaction" runat="server" ShowMessageBox="True"
                            ShowSummary="false" ValidationGroup="add" />
                        <br />
                        <br />
               </div>
                <div class="mGriditem">
                        <asp:GridView ID="gv_Item_Data" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" onrowcommand="gv_Item_Data_RowCommand" 
                            onrowdeleting="gv_Item_Data_RowDeleting" 
                            onrowediting="gv_Item_Data_RowEditing"  >
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                            <asp:TemplateField HeaderText="Index">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemModel" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStore" runat="server" Text='<%#Eval("Item.ModelSpecification.Store.StoreName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                               
                               <%-- <asp:TemplateField HeaderText="Total Amount (INR)" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" CssClass="total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%#Container.DataItemIndex%>' ToolTip="Edit" CssClass="button icon145" ></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Item?') "
                                            ToolTip="Delete" CssClass="button icon186" ></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                    </div>
                    <br />
                    <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <%--<tr id="totalNetValue" runat="server">
                            <td align="right">
                                Total Net Value :
                                <asp:Label ID="lbl_total_net_value" runat="server"></asp:Label>&nbsp;&nbsp; INR
                            </td>
                        </tr>--%>
                        
                    </tbody>
                </table>
            </asp:Panel>
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                            <td align="right">
                                <asp:ValidationSummary ID="vs_control" runat="server" ShowMessageBox="True" ValidationGroup="save"
                                    ShowSummary="false" />
                                <asp:Button ID="btn_cancel_draft" runat="server" CausesValidation="false" Text="Reset"
                                    CssClass="button_color action red" onclick="btn_cancel_draft_Click"  />
                                <asp:Button runat="server" Text="Save Draft" CssClass="button_color action green"
                                    ID="btn_save_draft" ValidationGroup="save" onclick="btn_save_draft_Click"></asp:Button>
                            </td>
                        </tr>
                </tbody>
            </table>
</div>
 </div>
</ContentTemplate>
 <Triggers>
        <asp:PostBackTrigger ControlID="btn_upload" />
    </Triggers>
</asp:UpdatePanel>
