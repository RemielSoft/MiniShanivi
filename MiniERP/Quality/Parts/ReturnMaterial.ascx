<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReturnMaterial.ascx.cs"
    Inherits="MiniERP.Quality.Parts.ReturnMaterial" ViewStateMode="Enabled" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
    <contenttemplate>
         <div class="box span12">
                <div class="box-header well">
                    <h2>
                         Return Material Note</h2>
                 </div>

                 <div> 
                    <script type="text/javascript" language="javascript">
                        function AllowOnlyNumeric(e) {
                            if (window.event) // IE 
                            {
                                if (((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) & e.keyCode != 46) {
                                    event.returnValue = false;
                                    alert("Unit Required allows only numeric value up to 2 decimal places!");
                                    return false;
                                }
                            }
                            else { // Fire Fox
                                if (((e.which < 48 || e.which > 57) & e.which != 8) & e.which != 46) {
                                    e.preventDefault();
                                    alert("Unit Required allows only numeric value up to 2 decimal places!");
                                    return false;
                                }
                            }
                        }
                        function ValidateSupplier() {


                            var contractor = document.getElementById("ContentPlaceHolderMain_Return_Material_txtName")

                            var fromDate = document.getElementById("ContentPlaceHolderMain_Return_Material_txtFromDate")
                            var toDate = document.getElementById("ContentPlaceHolderMain_Return_Material_txtToDate")
                            if (contractor.value != "") {
                                return true;
                            }
                            else if (fromDate.value != "" && toDate.value != "") {

                                return true;
                            }
                            else if (fromDate.value != "" && toDate.value == "" || fromDate.value == "" && toDate.value != "") {
                                alert("Please Enter Supplier Name Or Dates..");
                                return false;
                            }
                            else if (fromDate.value == "" && toDate.value == "" && contractor.value == "") {
                                alert("Please Enter Supplier Name Or Dates..");
                                return false;
                            }

                            else {
                                return true;
                            }
                        }
                </script>
                 </div>
        <div class="box-content">
        <asp:Panel ID="pnlSearch" runat="server">
            <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                <tbody>
                    <tr>
                        <td class="center">
                            Enter Receive Material Number<span style="color: Red">*</span>
                        </td>
                        <td class="center" >
                        
                            <asp:TextBox ID="txtReceiveMaterialNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReceiveMaterialNumber" runat="server" ControlToValidate="txtReceiveMaterialNumber"
                                        Display="None" ForeColor="Red" SetFocusOnError="True" ErrorMessage="Please Enter Receive Material Number"
                                        ValidationGroup="returnMaterial"></asp:RequiredFieldValidator>                                    
                                    
                            <asp:LinkButton ID="lnkSerach" runat="server" CommandName="cmdEdit" CssClass="Search icon198" 
                                            ToolTip="Search" CausesValidation="false" onclick="lnkSerach_Click"></asp:LinkButton>
                                            <asp:HiddenField ID="hdfReceiveMaterialNumber" runat="server" />
                            </td>
                            
                       <%-- </td>
                        <td class="center" width="60%">
                        <asp:DropDownList ID="ddlReturnmat" CssClass="TextBox" runat="server">
                           
                        </asp:DropDownList>--%>
                            <td class="center">
                            Supplier Name<span style="color: Red">*</span>
                        </td>
                            
                        <td class="center">
                        
                            <asp:TextBox ID="txtName" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                   

                         <td style="vertical-align:middle; text-align:center" rowspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                                            ToolTip="Search" onclick="btnSearch_Click" OnClientClick="javascript:return ValidateSupplier()"></asp:Button>
                                    </td>
                    </tr>
                    <tr>
                         <td>
                               <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromDate" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtFromDate" PopupButtonID="LinkButton2">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175"></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtToDate" PopupButtonID="LinkButton3">
                                    </asp:CalendarExtender>
                                </td>
                      

                    </tr>
                </tbody>
            </table>
            </asp:Panel>
            <br />
          <asp:Panel ID="pnlRecieveMetarial" runat="server">
           
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                <tr>
                        <td class="center" width="25%">
                            Receive Material Number
                        </td>
                        <td class="center" width="25%">
                            <asp:Label ID="lblReceiveMaterialNumber" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="center" width="25%">
                            Receive Material Date
                        </td>
                        <td class="center" width="25%">
                            <asp:Label ID="lblReceiveMaterialDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="center" width="25%">
                            Purchase Order Number
                        </td>
                        <td class="center" width="25%">
                            <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="center" width="25%">
                            Purchase Order Date
                        </td>
                        <td class="center" width="25%">
                            <asp:Label ID="lblPurchaseOrderDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr> 
                    <tr>
                        <td class="center" width="25%">
                            Supplier Name
                            <asp:HiddenField ID="hdnSupplierId" runat="server" />
                        </td>
                        <td class="center" width="25%">
                            <asp:Label ID="lblSupplierName" runat="server" ></asp:Label>
                        </td>
                        <td class="center" width="25%">
                            Delivery Challan Number
                        </td>
                        <td class="center" width="25%">
                            <asp:Label ID="lblDeliveryChallanNumber" runat="server"></asp:Label>
                        </td>
                    </tr>                   
                </tbody>
         </table>
             <%-- Main Grid Starts--%>
              <asp:GridView ID="gvMainGrid" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No-Items Available" 
                        AlternatingRowStyle-CssClass="alt" OnRowDataBound="gvMainGrid_RowDataBound">
                        <Columns> 
                            <asp:TemplateField HeaderStyle-Width="56px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                        OnCheckedChanged="chkSelectAll_Click"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" 
                                     OnCheckedChanged="chkSelect_Click"/>
                                    <%--<asp:HiddenField ID="hdfSupplierPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdfItemId" runat="server" Value='<%# Eval("ItemId")%>' />
                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdfSpecificationId" runat="server" Value='<%# Eval("ItemSpecificationId")%>' />
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("ItemSpecification") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Level">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("ItemCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Unit Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("UnitMeasurement") %>'></asp:Label>
                                   <%-- <asp:HiddenField ID="hdfUnitMeasurementId" runat="server" Value='<%# Eval("Id")%>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Quantity" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("QuantityDemand") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Recieve" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemRecieve" runat="server" Text='<%#Eval("IssuedQuantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Left" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("AvailableQuantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
              <%-- Main Grid Starts--%>
                    <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No-Items Available" 
                        AlternatingRowStyle-CssClass="alt" 
                  onrowdatabound="gvSupplier_RowDataBound">
                        <Columns>
                            <%--<asp:TemplateField HeaderStyle-Width="56px" Visible="false">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                        OnCheckedChanged="chkSelectAll_Click"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" 
                                     OnCheckedChanged="chkSelect_Click"/>
                                    <asp:HiddenField ID="hdfSupplierPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1%>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdfItemId" runat="server" Value='<%# Eval("Item.ItemId")%>' />
                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item.ItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdfSpecificationId" runat="server" Value='<%# Eval("Item.ModelSpecification.ModelSpecificationId")%>' />
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Level" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Store">
                                <ItemTemplate>
                                    <asp:Label ID="lblStore" runat="server" Text='<%#Eval("Item.ModelSpecification.Store.StoreName") %>'></asp:Label>
                               <asp:HiddenField ID="hdnStore" runat="server" Value='<%#Eval("Item.ModelSpecification.Store.StoreId") %>'></asp:HiddenField>
                                     </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate>
                                    <asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                               <asp:HiddenField ID="hdnBrand" runat="server" Value='<%#Eval("Item.ModelSpecification.Brand.BrandId") %>'></asp:HiddenField>
                                     </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfUnitMeasurementId" runat="server" Value='<%# Eval("Item.ModelSpecification.UnitMeasurement.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Quantity" ItemStyle-CssClass="taright" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Recieve" ItemStyle-CssClass="taright">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemRecieve" runat="server" Text='<%#Eval("UnitIssued") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Left" ItemStyle-CssClass="taright" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                
            <div class="Button_align">
                <asp:Button ID="btnAddSupplierItem" runat="server" Text="Add" 
                    CssClass="button_color action" onclick="btnAddSupplierItem_Click" />
            </div>
            <br />
            <br />
            <table class="table table-bordered table-striped table-condensed">
            
                <tbody>
                    
                    <tr>
                        <td  class="center" width="25%">
                            Return Material Date
                        </td>
                        <td class="center" width="75%">
                            <asp:TextBox ID="txtRMDate" CssClass="TextBox" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="ImgBtn" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtRMDate"
                                PopupButtonID="ImgBtn">
                            </asp:CalendarExtender>
                        </td>
                        </tr>
                        <tr>
                                    <td class="center" width="25%">
                                        <asp:Label ID="lbl_upload_document" runat="server" Text="Upload Document"></asp:Label>
                                    </td>
                                    <td class="center" width="75%">
                                        <div>
                                            <table class="table table-bordered table-striped table-condensed">
                                                <tr>
                                                    <td id="ajaxupload" runat="server" class="center">
                                                        <asp:FileUpload ID="FileUpload_Control" runat="server" />
                                                        <asp:Button ID="btn_upload" runat="server" CausesValidation="false" OnClick="btn_upload_Click"
                                                            Text="Upload" />
                                                    </td>
                                                    <td class="center">
                                                        <asp:Panel ID="Panel4" runat="server" Height="80px" ScrollBars="Vertical" Width="100%">
                                                            <div class="box-content">
                                                                <asp:GridView ID="gv_documents" runat="server" AlternatingRowStyle-CssClass="alt"
                                                                    AutoGenerateColumns="false" CellPadding="4" CssClass="mGrid" EmptyDataText="No-Documents Uploaded."
                                                                    ForeColor="#333333" GridLines="None" OnRowCommand="gv_documents_RowCommand" ShowHeader="false"
                                                                    Width="250px">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="File">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtn_file" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                    CommandName="OpenFile" Text='<%#Eval("Original_Name")%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-Width="20px" HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="false" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                    CommandName="FileDelete" CssClass="button icon186" OnClientClick="return confirm('Are you sure you want to delete this Document?') "
                                                                                    ToolTip="Delete" ></asp:LinkButton>
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
            <br />
            <br />
                        <div style="color: Red">
                            <asp:Literal ID="ltrl_err_msg" runat="server">
                            </asp:Literal>
                        </div>
         <div class="mGrid">
                    <asp:GridView ID="gvSupplierAdd" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No-Items Available" onrowcommand="gvSupplierAdd_RowCommand" 
                        onrowdatabound="gvSupplierAdd_RowDataBound">
                        <Columns>
                         <asp:TemplateField HeaderText="S.No">
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
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Item.ModelSpecification.ModelSpecificationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Level">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("Item.ModelSpecification.Category.ItemCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Store" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStore" runat="server" Text='<%#Eval("Item.ModelSpecification.Store.StoreName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnStore" runat="server" Value='<%#Eval("Item.ModelSpecification.Store.StoreId") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblMake" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand.BrandName") %>'></asp:Label>
                                     <asp:HiddenField ID="hdnBrand" runat="server" Value='<%#Eval("Item.ModelSpecification.Brand.BrandId") %>'></asp:HiddenField>
                             --%>   
                                    <asp:DropDownList ID="ddlBrandList" runat="server"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Measurement">
                                <ItemTemplate>
                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%#Eval("Item.ModelSpecification.UnitMeasurement.Name") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfUnitMeasurementId" runat="server" Value='<%# Eval("Item.ModelSpecification.UnitMeasurement.Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Quantity" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Recieve" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemRecieve" runat="server" Text='<%#Eval("QuantityReceived") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Left" ItemStyle-CssClass="taright" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Return" HeaderStyle-Width="55px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReturnQuantity"  runat="server"  Width="80px"
                                     onkeypress="AllowOnlyNumeric(event);"  Text='<%#Eval("QuantityReturned") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="55px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server" MaxLength="250"  Width="100px" Text='<%#Eval("Remark") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">

                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Container.DataItemIndex%>'
                                        CommandName="cmdDelete" OnClientClick="return confirm('Are You Sure You Want To Delete This Supplier?') "
                                        ToolTip="Delete">
                                        <asp:Image ID="imgDelete" runat="server" CssClass="button icon186" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            <div class="Button_align">
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button_color action red" />
                <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft" 
                    CssClass="button_color action green" onclick="btnSaveDraft_Click" />
            </div>
         </asp:Panel>
        </div>
    </div>
    </contenttemplate>
    <%--<Triggers>
            <asp:PostBackTrigger ControlID="btn_upload" />
        </Triggers>
</asp:UpdatePanel>--%>


    <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
        CommandName="Select" />
    <%-- for popup Search the Issue material Genreated Contractoe Work Order Number Gridview Name=gvIssueMaterialNo--%>
    <div style="position: absolute; top: 3000px;">
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPopUp"
            PopupControlID="panel3" BackgroundCssClass="modalBackground" DropShadow="true"
            Enabled="True" PopupDragHandleControlID="PopupMenu">
        </asp:ModalPopupExtender>
        <div>
            <asp:Panel ID="panel3" runat="server" CssClass="popup1">
                <div class="PopUpClose">
                    <div class="btnclosepopup">
                        <asp:Button ID="Button8" runat="server" Text="Close X" CssClass="buttonclosepopup" />
                    </div>
                </div>
                <asp:Panel ID="Panel2" runat="server" CssClass="popup" ScrollBars="Vertical">
                    <div>
                        <asp:GridView ID="gvRSM" runat="server" CssClass="mGrid" AllowPaging="false" AutoGenerateColumns="false"
                            EmptyDataText="No Record Found(s)" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rbtSelect" runat="server" OnCheckedChanged="rbtSelect_OncheckChanged"
                                            AutoPostBack="true"></asp:RadioButton>
                                        <asp:HiddenField ID="hdfReceivematerialId" runat="server" Value='<%# Eval("SupplierRecieveMatarialId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive Material Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lnkSRMNo" runat="server" CommandArgument='<%#Eval("SupplierRecieveMatarialId") %>'
                                            Text='<%#Eval("SupplierRecieveMaterialNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Order Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRMNo" runat="server" Text='<%#Eval("Quotation.SupplierQuotationNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSuppilerName" runat="server" Text='<%#Eval("Quotation.SupplierName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Challan Number" ItemStyle-CssClass="taright">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChallanNo" runat="server" Text='<%#Eval("DeliveryChallanNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive Material Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecieveDate" runat="server" Text='<%#Eval("RecieveMaterialDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>



</div>
