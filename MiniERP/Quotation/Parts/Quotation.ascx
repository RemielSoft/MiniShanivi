<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Quotation.ascx.cs" Inherits="MiniERP.Parts.Quotation"
    ViewStateMode="Enabled" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <asp:Label ID="lbl_head" runat="server" Text="Generate Contract Purchase Order"></asp:Label>
                </h2>
                <%--<div style="margin: 35px 0px 0px 0px; padding-right: 300px;">
                </div>--%>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                    <tbody>
                        <tr>
                            <td class="center" width="25%">
                                <asp:Label ID="lbl_quotation_number" runat="server"></asp:Label>
                            </td>
                            <td class="center" width="75%">
                                <asp:TextBox ID="txt_search" CssClass="TextBox" runat="server"></asp:TextBox>
                                <%-- <asp:ImageButton ID="imgbtn_search" runat="server" CausesValidation="false" ToolTip="Search"
                                    OnCommand="imgbtn_search_Click" CommandName="Search" CommandArgument="s" CssClass="Search icon198" />--%>
                                <asp:LinkButton ID="imgbtn_search" runat="server" CausesValidation="false" ToolTip="Search"
                                    OnCommand="imgbtn_search_Click" CommandName="Search" CommandArgument="s" CssClass="Search icon198"></asp:LinkButton>
                                <%--<asp:ImageButton ID="imgbtn_copy" runat="server" CausesValidation="false" ToolTip="Copy"
                                    OnCommand="imgbtn_search_Click" CommandName="Copy" CommandArgument="c" CssClass="Search icon55" />--%>
                                <asp:LinkButton ID="imgbtn_copy" runat="server" CausesValidation="false" ToolTip="Copy"
                                    OnCommand="imgbtn_search_Click" CommandName="Copy" CommandArgument="c" CssClass="Search icon55">LinkButton</asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div>
                    <asp:HiddenField ID="hdf_quotation_id" runat="server" />
                </div>
                <div style="color: Red">
                    <asp:LinkButton ID="lbtn_error" runat="server" Height="0" Width="0"></asp:LinkButton>
                    <asp:Literal ID="ltrl_error_msg" runat="server"></asp:Literal>
                </div>
                <br />
                <asp:Panel ID="Panel2" runat="server" CssClass="mitem">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr id="rw_contractor" runat="server">
                                <td class="center" width="25%">
                                    <asp:Label ID="lbl_contractor" runat="server" Text="Contractor Name"></asp:Label><span
                                        style="color: Red">*</span>
                                </td>
                                <td class="center" colspan="3" width="75%">
                                    <asp:DropDownList ID="ddl_contractor_name" Width="300px" runat="server">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_contractor" runat="server" ControlToValidate="ddl_contractor_name"
                                        Display="None" ErrorMessage="Please Select Contractor Name" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="rw_supplier" runat="server">
                                <td class="center" width="25%">
                                    <asp:Label ID="lbl_supplier" runat="server" Text="Supplier Name"></asp:Label><span
                                        style="color: Red">*</span>
                                </td>
                                <td class="center" colspan="3" width="75%">
                                    <asp:DropDownList ID="ddl_supplier" CssClass="dropdown" runat="server">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_supplier" runat="server" ControlToValidate="ddl_supplier"
                                        Display="None" ErrorMessage="Please Select Supplier Name" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="rw_contract" runat="server">
                                <td class="center" width="25%">Company Work Order Number<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_contrcat_number" CssClass="dropdown" runat="server" OnSelectedIndexChanged="ddl_contrcat_number_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        <%--<asp:ListItem Value="1">ABVP-001</asp:ListItem>
                                        <asp:ListItem Value="2">ABVP-002</asp:ListItem>
                                        <asp:ListItem Value="3">ABVP-003</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_contrcat_number" runat="server" ControlToValidate="ddl_contrcat_number"
                                        Display="None" ErrorMessage="Please Select Contract Number" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </td>
                                <td class="center" width="25%">Work Order Number<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_work_order_number" CssClass="dropdown" runat="server">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:LinkButton ID="lbtn_work_details" runat="server" CssClass="" OnClick="lbtn_work_details_Click"
                                        Visible="false">View Work Details</asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="rfv_work_order_number" runat="server" ControlToValidate="ddl_work_order_number"
                                        Display="None" ErrorMessage="Please Select Work Order Number" ForeColor="Red"
                                        InitialValue="0" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" width="27%">
                                    <asp:Label ID="lbl_order_date" runat="server"></asp:Label>
                                    <span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:TextBox ID="txt_order_date" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="img_btn_calander_order" runat="server" ToolTip="Select Date"
                                        CssClass="Calender icon175"></asp:LinkButton>
                                    <ajaxtoolkit:CalendarExtender ID="cal_ext_order_date" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txt_order_date" PopupButtonID="img_btn_calander_order">
                                    </ajaxtoolkit:CalendarExtender>
                                </td>
                                <td colspan="2" width="50%">
                                    <asp:RadioButtonList ID="rbtnTaxType" runat="server" RepeatDirection="Horizontal">
                                        <%--<asp:ListItem Value="0" Selected="True">All Inclusive</asp:ListItem>
                                        <asp:ListItem Value="1">Exclusive</asp:ListItem>
                                        <asp:ListItem Value="2">Not Applicable</asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <%--<tr>
                             <td width="25%">
                                <asp:Label ID="lblInclusive" runat="server" Text=" Inclusive"></asp:Label>
                                </td>
                                <td width="25%" colspan="3">
                                <asp:RadioButton ID="rbtnExclusive" GroupName="q" runat="server" />
                                </td>
                                
                                 
                            </tr>
                            <tr>
                            <td width="25%">
                                <asp:Label ID="lblAllInclusive" runat="server" Text="All Inclusive"></asp:Label>
                                </td>
                                <td width="25%" colspan="3">
                                <asp:RadioButton ID="rbtnAllInclusive" GroupName="q" runat="server" />
                                </td>
                            </tr>
                            <tr>
                            <td width="25%">
                                <asp:Label ID="lblNotApplicable" runat="server" Text="Not Applicable"></asp:Label>
                                </td>
                                <td width="25%" colspan="3">
                                <asp:RadioButton ID="rbtnApplicable" GroupName="q" runat="server" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="center" width="25%">
                                    <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                                </td>
                                <td colspan="3" class="center" width="75%">
                                    <div>
                                        <table class="table table-bordered table-striped table-condensed">
                                            <tr>
                                                <td class="center" id="ajaxupload" runat="server">
                                                    <%-- <asp:FileUpload ID="FileUpload_Control" runat="server" onchange="this.form.submit();" />--%>
                                                    <asp:FileUpload ID="FileUpload_Control" runat="server" />
                                                    <asp:Button ID="btn_upload" runat="server" Text="Upload" CausesValidation="false"
                                                        OnClick="btn_upload_Click" />
                                                </td>
                                                <td class="center">
                                                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" Height="80px" Width="100%">
                                                        <asp:GridView ID="gv_documents" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" EmptyDataText="No-Documents Uploaded."
                                                            AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="gv_documents_RowCommand"
                                                            Width="250px">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="File">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn_file" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                                                                            CommandName="OpenFile" Text='<%#Eval("Original_Name")%>'></asp:LinkButton>
                                                                        <%--<asp:HiddenField ID="hdf_doc_id" runat="server" Value='<%#Eval("Id")%>' />--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandName="FileDelete"
                                                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                                            ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                                                        <%--<asp:LinkButton ID="lbtn_delete" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                                                                                OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                                                CommandName="FileDelete" Text="Delete"></asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>

                            <tr id="date_and_desc" runat="server" width="25%">
                                <td class="center" id="closing_date_lbl" runat="server">
                                    <asp:Label ID="lbl_Closing_Date" runat="server" Text="Valid Till Date(Closing Date)"></asp:Label>
                                </td>
                                <td class="center" id="closing_date_txt" runat="server" width="25%">
                                    <asp:TextBox ID="txt_Closing_Date" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                                    <%--<asp:ImageButton ID="img_btn_calander_closing" runat="server" ToolTip="Select Date"
                                        CssClass="Calender icon175" />--%>
                                    <asp:LinkButton ID="lnk_btn_calander_closing" runat="server" ToolTip="Select Date"
                                        CssClass="Calender icon175"></asp:LinkButton>
                                    <ajaxtoolkit:CalendarExtender ID="cal_ext_closing_date" CssClass="calbox" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="txt_Closing_Date" PopupButtonID="lnk_btn_calander_closing">
                                    </ajaxtoolkit:CalendarExtender>
                                </td>
                                <td class="center" id="delivery_desc_lbl" runat="server" width="25%">
                                    <asp:Label ID="lbl_Delivery_Desc" runat="server" Text="Delivery Description"></asp:Label>
                                </td>
                                <td class="center" id="delivery_desc_txt" runat="server" width="25%">
                                    <asp:TextBox ID="txt_Delivery_Description" CssClass="mlttext" runat="server" TextMode="MultiLine"
                                        MaxLength="150"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rev_delivery_desc" runat="server" ControlToValidate="txt_Delivery_Description"
                                        Display="None" ForeColor="Red" SetFocusOnError="True" ValidationGroup="save"></asp:RegularExpressionValidator>
                                </td>

                            </tr>
                            <tr>

                                <td class="center" id="Td1" runat="server" width="25%">
                                    <asp:Label ID="lbl_Subject_Desc" runat="server" Text="Subject Description"></asp:Label>
                                </td>

                                <td class="center" id="Td2" runat="server" width="25%">
                                    <asp:TextBox ID="txt_Subject_Description" CssClass="mlttext" runat="server" TextMode="MultiLine"
                                        MaxLength="500"></asp:TextBox>
                                    <%--   <asp:RegularExpressionValidator ID="rev_Subject_desc" runat="server" ControlToValidate="txt_Subject_Description"
                                        Display="None" ForeColor="Red" SetFocusOnError="True" ValidationGroup="save"></asp:RegularExpressionValidator>
                                    --%>                                
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
                                    <asp:Label ID="lbl_duplicate_activity" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr id="rw_activity_desc" runat="server">
                                <td class="center" width="25%">
                                    <asp:Label ID="Label1" runat="server" Text="Service Description"></asp:Label>
                                    <span style="color: Red">*</span>
                                </td>
                                <td class="center" colspan="3" width="75%">
                                    <asp:DropDownList ID="ddlServiceDescription" Width="300px" runat="server">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_activity_desc" runat="server" ControlToValidate="ddlServiceDescription"
                                        Display="None" ErrorMessage="Please Select Service Description" ForeColor="Red"
                                        SetFocusOnError="True" InitialValue="0" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <%--<asp:textbox id="txt_activity_description" runat="server" cssclass="mlttext" textmode="multiline"
                                        maxlength="150" width="530px"></asp:textbox>
                                    <asp:RequiredFieldValidator ID="rfv_activity_desc" runat="server" ControlToValidate="txt_activity_description"
                                        Display="None" ErrorMessage="Please Enter Activity Description" ForeColor="Red"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ID="rev_activity_desc" runat="server" Display="None"
                                        ErrorMessage="Do not enter &lt; or &gt; in Activity Description" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="add" ControlToValidate="txt_activity_description"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="rev_activity_desc" runat="server" ControlToValidate="txt_activity_description"
                                        Display="None" ForeColor="Red" SetFocusOnError="True" ValidationGroup="add"></asp:RegularExpressionValidator>--%>
                                    <asp:LinkButton ID="lbtnServiceDescription" runat="server" Visible="false" CssClass=""
                                        OnClick="lbtnServiceDescription_Click">Add New Service Description </asp:LinkButton>
                                </td>
                            </tr>
                            <tr id="rw_addItem" runat="server">
                                <td class="center" width="25%">Add Item<span style="color: red">*</span>
                                </td>
                                <td colspan="3" class="center" width="75%">
                                    <asp:RadioButtonList ID="rbtn_AddItem" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbtn_AddItem_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Yes</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="center">
                                    &nbsp;Category Level<span style="color: Red">&nbsp; *</span>
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="ddl_item_category" runat="server" OnSelectedIndexChanged="ddl_item_category_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_item_category" runat="server" ControlToValidate="ddl_item_category"
                                        Display="None" ErrorMessage="Please Select Item Category" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="center" width="25%">Item<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_item" runat="server" CssClass="dropdown" OnSelectedIndexChanged="ddl_item_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_item" runat="server" ControlToValidate="ddl_item"
                                        Display="None" ErrorMessage="Please Select Item" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                                <td class="center" width="25%">Specification<span style="color: Red">*</span>
                                </td>
                                <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_item_model" CssClass="dropdown" runat="server" OnSelectedIndexChanged="ddl_item_model_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_item_model" runat="server" ControlToValidate="ddl_item_model"
                                        Display="None" ErrorMessage="Please Select Specification" ForeColor="Red" InitialValue="0"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">Category Level
                                </td>
                                <td class="center">
                                    <asp:Label ID="lbl_category_level" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdf_category_level_id" runat="server" />
                                </td>
                                <td class="center"></td>
                                <td class="center">
                                    <asp:Label ID="lbl_make" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">Quantity<span style="color: Red">*</span>&nbsp;
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_Number_of_Unit" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="lbl_unit_of_measurement" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdf_unit_of_measurement" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfv_number_of_unit" runat="server" ControlToValidate="txt_Number_of_Unit"
                                        Display="None" ErrorMessage="Please Enter Quantity" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_number_of_unit" runat="server" ControlToValidate="txt_Number_of_Unit"
                                        Display="None" ErrorMessage="Only Numeric Value is allowed in Quantity" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="add"></asp:RegularExpressionValidator>
                                    <%-- <asp:CompareValidator ID="cv_number_of_unit" runat="server"  ControlToValidate="txt_Number_of_Unit"
                                        Display="None" ErrorMessage="Only Numeric Value is allowed in Quantity" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="add"></asp:CompareValidator>--%>
                                </td>
                                <td class="center">
                                    <asp:Label ID="lbl_cost" runat="server"></asp:Label>
                                    <span style="color: Red">*</span>
                                </td>
                                <td class="center">
                                    <img alt="" src="../../Images/rupee_symbol5.png" />
                                    <asp:TextBox ID="txt_Per_Unit_Cost" runat="server" CssClass="TextBox" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_per_unit_cost" runat="server" ControlToValidate="txt_Per_Unit_Cost"
                                        Display="None" ErrorMessage="Please Enter Cost" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_per_unit_cost" runat="server" ControlToValidate="txt_Per_Unit_Cost"
                                        Display="None" ErrorMessage="Only Numeric Value is allowed in Cost" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="add"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" width="25%">Discount Type
                                </td>
                                <td class="center" width="25%">
                                    <asp:DropDownList ID="ddl_discount_mode" CssClass="dropdown" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddl_discount_mode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdf_discount_mode" runat="server" />
                                </td>
                                <td class="center">
                                    <asp:Label ID="lblPerUnitDiscount" runat="server" Text="Per Unit Discount"></asp:Label>
                                </td>
                                <td class="center">
                                    <img alt="" src="../../Images/rupee_symbol5.png" id="imgRupee" runat="server" visible="false" />
                                    <%--<asp:TextBox ID="txt_Per_Unit_Discount" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>--%>
                                    <asp:TextBox ID="txt_Per_Unit_Discount" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="lbl_discount_mode" runat="server"></asp:Label>
                                    <asp:RegularExpressionValidator ID="rev_per_unit_discount" runat="server" ControlToValidate="txt_Per_Unit_Discount"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="add"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfv_per_unit_discount" runat="server" ControlToValidate="txt_Per_Unit_Discount"
                                        Display="None" ErrorMessage="Please Enter Per Unit Discount" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <%--<asp:RangeValidator ID="rng_Per_Unit_Discount" runat="server" ControlToValidate="txt_Per_Unit_Discount"
                                        Display="None" ForeColor="Red" MaximumValue="100" MinimumValue="0" SetFocusOnError="true"
                                        Type="Double" ValidationGroup="add" ErrorMessage="Only Numeric values less than 100 are allowed in Per Unit Discount"></asp:RangeValidator>--%>
                                </td>
                            </tr>
                            <%--   -------------sundeep----------------%>
                            <%--  <tr style=" display:none;">--%>
                            <tr id="excise" runat="server" style="display: none;">
                                <td class="center">Excise Duty
                                </td>
                                <td class="center" colspan="3">
                                    <asp:TextBox ID="txtExciseDuty" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="revExciseDuty" runat="server" ControlToValidate="txtExciseDuty"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="add"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="center">Service Tax
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_service_tax" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="rev_service_tax" runat="server" ControlToValidate="txt_service_tax"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="add"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">VAT
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_vat" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="rev_vat" runat="server" ControlToValidate="txt_vat"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="add"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="center">CST (with C Form)
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_cst_with_c_form" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="rev_cst_with_c_form" runat="server" ControlToValidate="txt_cst_with_c_form"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="add"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">CST (Without C Form)
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_cst_without_c_form" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="rev_cst_without_c_form" runat="server" ControlToValidate="txt_cst_without_c_form"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="add"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                      <%-- Add By RK Chauhan For Excel Sheet Upload--%>
                    <asp:GridView ID="GridView1" runat="server" CellPadding="6" ForeColor="#333333" GridLines="None" >  
            <AlternatingRowStyle BackColor="White" />  
            <EditRowStyle BackColor="#7C6F57" />  
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />  
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />  
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />  
            <RowStyle BackColor="#E3EAEB" />  
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />  
            <SortedAscendingCellStyle BackColor="#F8FAFA" />  
            <SortedAscendingHeaderStyle BackColor="#246B61" />  
            <SortedDescendingCellStyle BackColor="#D4DFE1" />  
            <SortedDescendingHeaderStyle BackColor="#15524A" />  
        </asp:GridView>
        
                    <div class="Button_align" id="div_button" runat="server">
                     
                                    <asp:FileUpload ID="FileUpload1" runat="server" />

                        <asp:Button ID="Button1" runat="server" Text="Upload" CausesValidation="false" OnClick="btn_uploadExcel_Click" />

                        <asp:Button ID="btn_cancel" runat="server" CausesValidation="false" Text="Cancel"
                            CssClass="button_color action red" OnClick="btn_cancel_Click" />
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
                            OnRowCommand="gv_Item_Data_RowCommand" OnRowEditing="gv_Item_Data_RowEditing"
                            OnRowDeleting="gv_Item_Data_RowDeleting" EmptyDataText="No-Items Available" OnPageIndexChanging="gv_Item_Data_PageIndexChanging"
                            OnRowDataBound="gv_Item_Data_RowDataBound" ShowFooter="true">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:TemplateField HeaderText="Service Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("DeliverySchedule.ActivityDescription")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ItemName")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemModel" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.ModelSpecificationName")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.Brand.BrandName")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Item.ModelSpecification.UnitMeasurement.Name")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost (INR)" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPUC" runat="server" Text='<%#Eval("PerUnitCost") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT" runat="server" Text='<%#Eval("TaxInformation.DiscountMode.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Per Unit Discount" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPUD" runat="server" Text='<%#Eval("PerUnitDiscount") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <%--     Change upto invoice visble fale colume upto--%>
                                <asp:TemplateField HeaderText="Excise Duty (%)" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExciseDuty" runat="server" Text='<%#Eval("TaxInformation.ExciseDuty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Tax" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblST" runat="server" Text='<%#Eval("TaxInformation.ServiceTax") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVAT" runat="server" Text='<%#Eval("TaxInformation.VAT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST (with C Form)" ItemStyle-CssClass="taright" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTW" runat="server" Text='<%#Eval("TaxInformation.CSTWithCForm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST (Without C Form)" ItemStyle-CssClass="taright"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTWO" runat="server" Text='<%#Eval("TaxInformation.CSTWithoutCForm") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <img alt="" src="../../Images/rupee_symbol5.png" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Freight" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("TaxInformation.Freight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packa- ging" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPackaging" Visible="false" runat="server" Text='<%#Eval("TaxInformation.Packaging") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disount Cost (INR) ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountCost" runat="server" Text='<%#Eval("Discount_Rates") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" Text="Total : " EnableViewState="True"
                                            CssClass="total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Amount (INR)" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" CssClass="total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <%--<asp:ImageButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%#Container.DataItemIndex%>' ToolTip="Edit" CssClass="button icon145" />--%>
                                        <%--<asp:ImageButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Item?') "
                                            ToolTip="Delete" CssClass="button icon186" />--%>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%#Container.DataItemIndex%>' ToolTip="Edit" CssClass="button icon145"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                            CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Item?') "
                                            ToolTip="Delete" CssClass="button icon186"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                    </div>
                    <br />
                </asp:Panel>
                <asp:Label ID="lbl_calculate_msg" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <br />
                <asp:Panel ID="pnl_total" runat="server" CssClass="mitem">
                    <table class="table table-bordered table-striped table-condensed" id="tbl_tax" runat="server">
                        <tbody>
                            <%-- change upto- display none --%>
                            <tr style="display: none;">
                                <td class="center">Freight
                                </td>
                                <td class="center">
                                    <img alt="" src="../../Images/rupee_symbol5.png" />
                                    <asp:TextBox ID="txt_freight" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rev_freight" runat="server" ControlToValidate="txt_freight"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="addTotal"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">Packaging
                                </td>
                                <td class="center">
                                    <img alt="" src="../../Images/rupee_symbol5.png" />
                                    <asp:TextBox ID="txt_packaging" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rev_packaging" runat="server" ControlToValidate="txt_packaging"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="addTotal"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    <asp:Button ID="btnAddTotal" runat="server" Text="Total" CssClass="button_color action"
                                        ValidationGroup="addTotal" OnClick="btnAddTotal_Click" />
                                    <asp:ValidationSummary ID="vsAddTotal" runat="server" ShowMessageBox="True" ShowSummary="false"
                                        ValidationGroup="addTotal" />
                                </td>
                                <td class="center">
                                    <asp:HiddenField ID="hdnfGrandTotal" runat="server" />
                                    <asp:Label ID="lblGrandTotal" runat="server" CssClass="total"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="center">
                                    Total Discount
                                </td>
                                <td class="center" colspan="3">
                                    <asp:DropDownList ID="ddl_discount_mode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_discount_mode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txt_total_discount" CssClass="TextBox" runat="server" MaxLength="8"></asp:TextBox>
                                    <asp:Label ID="lbl_discount_mode" runat="server"></asp:Label>
                                    <asp:RegularExpressionValidator ID="rev_Discount" runat="server" ControlToValidate="txt_total_discount"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="tax"></asp:RegularExpressionValidator>
                                    <asp:HiddenField ID="hdf_discount_mode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Service Tax
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_service_tax" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="rev_service_tax" runat="server" ControlToValidate="txt_service_tax"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="tax"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    VAT
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_vat" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="rev_vat" runat="server" ControlToValidate="txt_vat"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="tax"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    CST (with C Form)
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_cst_with_c_form" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="rev_cst_with_c_form" runat="server" ControlToValidate="txt_cst_with_c_form"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="tax"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    CST (Without C Form)
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_cst_without_c_form" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    %
                                    <asp:RegularExpressionValidator ID="rev_cst_without_c_form" runat="server" ControlToValidate="txt_cst_without_c_form"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="tax"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Freight
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_freight" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    INR
                                    <asp:RegularExpressionValidator ID="rev_freight" runat="server" ControlToValidate="txt_freight"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="tax"></asp:RegularExpressionValidator>
                                </td>
                                <td class="center">
                                    Packaging
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txt_packaging" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                    INR
                                    <asp:RegularExpressionValidator ID="rev_packaging" runat="server" ControlToValidate="txt_packaging"
                                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="tax"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right">
                                    <asp:CheckBox ID="chbx_tax" runat="server" AutoPostBack="True" OnCheckedChanged="chbx_tax_CheckedChanged"
                                        Text="With Tax" ValidationGroup="tax" />
                                    <asp:ValidationSummary ID="vs_tax" runat="server" ShowMessageBox="True" ValidationGroup="tax"
                                        ShowSummary="false" />
                                    <asp:Button ID="btn_calculate" runat="server" Text="Calculate" OnClick="btn_calculate_Click"
                                        Visible="false" />-
                                </td>
                            </tr>--%>
                        </tbody>
                    </table>
                </asp:Panel>
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <%--<tr id="totalNetValue" runat="server">
                            <td align="right">
                                Total Net Value :
                                <asp:Label ID="lbl_total_net_value" runat="server"></asp:Label>&nbsp;&nbsp; INR
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Button runat="server" Text="Submit Draft" CssClass="button_color1 action green"
                                    ID="btn_submit_draft" ValidationGroup="save" OnCommand="btn_submit_draft_Click"
                                    CommandName="Submit" CommandArgument="st"></asp:Button>
                            </td>
                            <td>
                                <asp:ValidationSummary ID="vs_control" runat="server" ShowMessageBox="True" ValidationGroup="save"
                                    ShowSummary="false" />
                                <asp:Button ID="btn_cancel_draft" runat="server" CausesValidation="false" Text="Cancel"
                                    CssClass="button_color action red" OnClick="btn_cancel_draft_Click" />
                                <asp:Button runat="server" Text="Save Draft" CssClass="button_color action green"
                                    ID="btn_save_draft" OnCommand="btn_save_draft_Click" ValidationGroup="save" CommandName="Save"
                                    CommandArgument="s"></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_upload" />
        <asp:PostBackTrigger ControlID="Button1" />
    </Triggers>
</asp:UpdatePanel>
