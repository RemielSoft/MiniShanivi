<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemDetails.aspx.cs" Inherits="MiniERP.Quotation.Parts.ItemDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Work Details</title>
    <link href="../../Styles/layout.css" rel="stylesheet" type="text/css" />
    <%--<link href="../Styles/MessageBox.css" rel="stylesheet" type="text/css"/>--%>
    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    <script src="../../Scripts/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/hideshow.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tablesorter.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/jquery.equalHeight.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".tablesorter").tablesorter();
        }
	);
        $(document).ready(function () {

            //When page loads...
            $(".tab_content").hide(); //Hide all content
            $("ul.tabs li:first").addClass("active").show(); //Activate first tab
            $(".tab_content:first").show(); //Show first tab content

            //On Click Event
            $("ul.tabs li").click(function () {

                $("ul.tabs li").removeClass("active"); //Remove any "active" class
                $(this).addClass("active"); //Add "active" class to selected tab
                $(".tab_content").hide(); //Hide all tab content

                var activeTab = $(this).find("a").attr("href"); //Find the href attribute value to identify the active tab + content
                $(activeTab).fadeIn(); //Fade in the active ID content
                return false;
            });

        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('.column').equalHeight();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
        <asp:Panel ID="Panel1" runat="server" CssClass="mitem">
            <table class="table table-bordered table-striped table-condensed" id="tbl_itemTransaction"
                runat="server">
                <tbody>
                    <tr>
                        <td class="center">
                            Contract Number
                        </td>
                        <td class="center">
                            <asp:Label ID="lbl_contract_number" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="center">
                            Work Order Number
                        </td>
                        <td class="center">
                            <asp:Label ID="lbl_work_order_number" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    </div>
    <br />
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Panel ID="Panel2" runat="server" CssClass="mitem">
                        <asp:GridView ID="gv_Item_Data" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" OnRowDataBound="gv_Item_Data_RowDataBound"
                            ShowFooter="true">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chbx_select_all" runat="server" Text="Select" AutoPostBack="true"
                                            OnCheckedChanged="on_check_uncheck_all" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbx_item" runat="server" AutoPostBack="true" OnCheckedChanged="on_chbx_item" />
                                        <asp:HiddenField ID="hdf_item_id" runat="server" Value='<%#Eval("ItemNumber") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemNo" runat="server" Text='<%#Eval("ItemNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceNo" runat="server" Text='<%#Eval("ServiceNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ServiceDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity Issued">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssued" runat="server" Text='<%#Eval("QuantityIssued") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeft" runat="server" Text='<%#Eval("QuantityLeft") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitt" runat="server" Text='<%#Eval("Unit.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lbluRate" runat="server" Text='<%#Eval("UnitRate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Applicable Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblaRate" runat="server" Text='<%#Eval("ApplicableRate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                        <div class="Button_align">
                            <asp:Button ID="btn_add" runat="server" Text="Add" CssClass="button_color action"
                                OnClick="btn_add_Click" />
                        </div>
                        <br />
                        <br />
                    </asp:Panel>
                </div>
                <br />
                <div>
                    <asp:Panel ID="Panel4" runat="server" CssClass="mitem">
                        <asp:GridView ID="gv_final_item_data" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            OnRowCommand="gv_final_item_data_RowCommand" OnRowEditing="gv_final_item_data_RowEditing"
                            EmptyDataText="No-Items Available" OnRowDataBound="gv_final_item_data_RowDataBound"
                            ShowFooter="true" OnRowDeleting="gv_final_item_data_RowDeleting">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Item_No" runat="server" Text='<%#Eval("Service_Detail.ItemNumber") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfitemid" runat="server" Value='<%#Eval("Service_Detail.ItemNumber") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service No." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Service_No" runat="server" Text='<%#Eval("Service_Detail.ServiceNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Desc." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Specification" runat="server" Text='<%#Eval("Service_Detail.ServiceDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_NOF" runat="server" Text='<%#Eval("Service_Detail.Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity Issued" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Issued" runat="server" Text='<%#Eval("Service_Detail.QuantityIssued") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Left" runat="server" Text='<%#Eval("Service_Detail.QuantityLeft") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Issue_Quantity" runat="server" Width="80px" OnTextChanged="txt_Issue_Quantity_changed"
                                            AutoPostBack="true" Text='<%#Eval("UnitLeft") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Unitt" runat="server" Text='<%#Eval("Service_Detail.Unit.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Unit_Rate" runat="server" Text='<%#Eval("Service_Detail.UnitRate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Applicable Rate">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblaRate" runat="server" Text='<%#Eval("ApplicableRate") %>'></asp:Label>--%>
                                        <asp:TextBox ID="txt_Applicable_Rate" runat="server" Width="80px" OnTextChanged="txt_Applicable_Rate_changed"
                                            AutoPostBack="true" Text='<%#Eval("Service_Detail.ApplicableRate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_discount_type" CssClass="dropdown" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddl_discount_type_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Discount" runat="server" Width="60px" OnTextChanged="txt_Discount_changed"
                                            AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Excise Duty">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblExDuty" runat="server" Text='<%#Eval("") %>'></asp:Label>--%>
                                        <asp:TextBox ID="txt_Excise_Duty" runat="server" Width="60px" OnTextChanged="txt_Excise_Duty_changed"
                                            AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Tax">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Service_Tax" runat="server" Width="60px" OnTextChanged="txt_Service_Tax_changed"
                                            AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_VAT" runat="server" Width="60px" OnTextChanged="txt_VAT_changed"
                                            AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST with C Form">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_CST_with_C_Form" runat="server" Width="60px" OnTextChanged="txt_CST_with_C_Form_changed"
                                            AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST without C Form">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_CST_without_C_Form" runat="server" Width="60px" OnTextChanged="txt_CST_without_C_Form_changed"
                                            AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" Text="Total : " EnableViewState="True"
                                            CssClass="total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount (INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" CssClass="total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument="<%#Container.DataItemIndex%>"
                                            CommandName="Delete" OnClientClick="return confirm('Are you sure to delete this Item?') "
                                            ToolTip="Delete" CssClass="button icon186" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <br />
                <div>
                    <asp:Panel ID="pnl_Total" runat="server" CssClass="mitem">
                        <table class="table table-bordered table-striped table-condensed" id="tbl_tax" runat="server">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        Freight
                                    </td>
                                    <td class="center">
                                        <img alt="" src="../../Images/rupee_symbol5.png"  />
                                        <asp:TextBox ID="txt_freight" CssClass="TextBox" runat="server" MaxLength="10"></asp:TextBox>
                                        
                                        <asp:RegularExpressionValidator ID="rev_freight" runat="server" ControlToValidate="txt_freight"
                                            Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="addTotal"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="center">
                                        Packaging
                                    </td>
                                    <td class="center">
                                        <img alt="" src="../../Images/rupee_symbol5.png"  />
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
                                        <asp:Label ID="lblGrandTotal" runat="server" CssClass="total"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </div>
                <br />
                <div>
                    <asp:Button ID="btn_cancel_draft" runat="server" CausesValidation="false" Text="Cancel"
                        CssClass="button_color action red" OnClick="btn_cancel_draft_Click" />
                    <asp:Button runat="server" Text="Save Work Details" CssClass="button_color action green"
                        ID="btn_save_draft" OnClick="btn_save_draft_Click" ValidationGroup="save"></asp:Button>
                </div>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
