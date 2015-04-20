<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="SupplierInvoice.aspx.cs" ViewStateMode="Enabled" ValidateRequest="false" Inherits="MiniERP.Invoice.SupplierInvoice" %>
    
 <%@ Register Src="~/Invoice/Parts/SupplierInvoice.ascx" TagName="SupplierInvoice" TagPrefix="udc" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<div>
     <udc:SupplierInvoice runat="server" ID="Supplier_Invoice" />
</div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        
        <div>
            <script type="text/javascript">
                function CheckOtherIsCheckedByGVID(rdbtn) {

                    var IsChecked = rdbtn.checked;

                    if (IsChecked) {

                        rdbtn.parentElement.parentElement.style.backgroundColor = '#228b22';

                        rdbtn.parentElement.parentElement.style.color = 'white';

                    }

                    var CurrentRdbID = rdbtn.id;

                    var Chk = rdbtn;

                    Parent = document.getElementById("<%=gvPaymentType.ClientID%>");

                    var items = Parent.getElementsByTagName('input');

                    for (i = 0; i < items.length; i++) {

                        if (items[i].id != CurrentRdbID && items[i].type == "radio") {

                            if (items[i].checked) {

                                items[i].checked = false;

                                items[i].parentElement.parentElement.style.backgroundColor = 'white'

                                items[i].parentElement.parentElement.style.color = 'black';
                            }

                        }

                    }

                }
</script>

        </div>        
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <asp:Label ID="lbl_head" runat="server" Text="Supplier Invoice"></asp:Label>
                </h2>               
            </div>
            <div class="box-content">
            <asp:Panel ID="pnlSearch" runat="server">
                <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                    <tbody>                       
                        <tr>
                            <td class="center" width="25%">
                                Supplier Purchase Order Number
                            </td>
                            <td class="center" width="75%">
                                <asp:TextBox ID="txtSupplierPONumber" CssClass="TextBox" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="imgbtn_search" runat="server" CausesValidation="false" ToolTip="Search"
                                    CssClass="Search icon198" OnClick="imgbtn_search_Click" />
                            </td>
                        </tr>                                
                    </tbody>
                </table>
                </asp:Panel>
                <br />
                <asp:Panel ID="Panel1" runat="server" CssClass="mitem">
                   
                        <div style="width:100%;">
                            <div class="center" style="font-size:14px; float:left; position:relative; top:30px;  border-right:1px solid #green; text-align:center; font-weight:bold; vertical-align:middle; width:25%;">
                                Payment Type
                            </div> 
                            <div style="width:75%; position:relative; float:left; top:0px;">
                            
                               <asp:GridView ID="gvPaymentType" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            EmptyDataText="No-Items Available" OnRowDataBound="gvPaymentType_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="56px" HeaderText="Select">                                   
                                    <ItemTemplate>                                        
                                        <asp:RadioButton ID="rdbtn" runat="server" OnCheckedChanged="rdbtn_Click" onclick="javascript:CheckOtherIsCheckedByGVID(this);" GroupName="a" AutoPostBack="true" />
                                        <asp:HiddenField ID="hdfPaymentTermId" runat="server" Value='<%# Eval("PaymentTermId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                                <asp:TemplateField HeaderText="Payment Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentTypeName" runat="server" Text='<%#Eval("PaymentType.Name") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfPaymentTypeId" runat="server" Value='<%# Eval("PaymentType.Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Of Days" ItemStyle-CssClass="taright" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%#Eval("NumberOfDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Percentage Value" ItemStyle-CssClass="taright" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblPercenatageValue" runat="server" Text='<%#Eval("PercentageValue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Payment Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentDescription" runat="server" Text='<%#Eval("PaymentDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                            </Columns>
                        </asp:GridView>
                        </div>
                           </div>                            
                        
                         <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                        <tr>
                                <td class="center" width="25%">
                                    Supplier Purchase Order Number
                                    <asp:HiddenField ID="hdnStatusTypeId" runat="server" />
                                </td>
                                <td width="25%">
                                    <asp:Label ID="lblSupplierPurchaseOrderNumber" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnSupplierPOId" runat="server" />
                                </td>
                               <td class="center" width="25%">
                                    Supplier Name
                                    <asp:HiddenField ID="hdnPaymentTermId" runat="server" />
                                </td>
                                <td class="center" width="25%">
                                    <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnSupplierId" runat="server" />
                                </td>
                            </tr>                            
                        <tr>
                            <td class="center" >
                               Total Net Value
                            </td>
                            <td class="center">
                                <asp:Label ID="lblTotalNetValue" runat="server"></asp:Label> 
                                <img alt="" src="../../Images/rupee_symbol5.png"  />                                                                
                            </td>
                             <td class="center">
                                    Invoiced Amount
                                </td>
                                <td class="center">
                                    <asp:Label ID="lblInvoicedAmount" runat="server"></asp:Label>                                                                    
                                    <img alt="" src="../../Images/rupee_symbol5.png" />
                                </td>
                        </tr> 
                              
                        <tr>
                        
                            <td class="center" >
                               Left Amount
                            </td>
                            <td class="center">
                                <asp:Label ID="lblLeftAmount" runat="server"></asp:Label>
                                <img alt="" src="../../Images/rupee_symbol5.png"   />
                            </td>
                             <td class="center" style="color:Blue;">
                                    Payable Amount
                                </td>
                                <td class="center" style="color:Blue;">
                                    <asp:Label ID="lblPayableAmount" runat="server"></asp:Label> 
                                    <img alt="" id="imgPayableAmt" visible="false" runat="server" src="../../Images/rupee_symbol5.png"   />                                   
                                </td>
                        </tr> 
                       <asp:Panel ID="visiblepannel" runat="server" Visible="false">
                        <tr>
                            <td class="center" >
                                Freight
                            </td>
                            <td class="center">
                                <asp:Label ID="lblFreight" runat="server"></asp:Label>
                                <img alt="" src="../../Images/rupee_symbol5.png"   />
                            </td>
                             <td class="center">
                                    Packaging
                                </td>
                                <td class="center" colspan="3">
                                    <asp:Label ID="lblPackaging" runat="server"></asp:Label>
                                    <img alt="" src="../../Images/rupee_symbol5.png"   />
                                </td>
                        </tr>
                          </asp:Panel>                                             
                        </tbody>
                    </table>                
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center" width="25%">
                                    Invoice Date
                                </td>
                                <td class="center" width="25%">
                                    <asp:TextBox ID="txtInvoiceDate" CssClass="TextBox" Enabled="false" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton5" runat="server" ToolTip="Select Date" CssClass="Calender icon175" />
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtInvoiceDate" PopupButtonID="ImageButton5">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="center" width="25%">
                                    Remarks
                                </td>
                                <td class="center" width="25%">
                                    <asp:TextBox ID="txtRemarks" runat="server" MaxLength="10" CssClass="mlttext" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" width="25%">
                                    Upload Document
                                    <asp:HiddenField ID="hdfUploadDocId" runat="server" />
                                </td>
                                <td colspan="3" class="center" width="75%">                                    
                                    <udc:UploadDocuments ID="ctrl_UploadDocument" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>                    
                </asp:Panel>
                <br />
                <div class="Button_align">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" 
                        CssClass="button_color action red" onclick="btnReset_Click" />
                    <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft" CssClass="button_color action green"
                        OnClick="btnSaveDraft_Click" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
