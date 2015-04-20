<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="IssuingMaterial.aspx.cs" Inherits="MiniERP.IssueMaterial.IssuingMaterial" %>

<%@ Register Src="~/Quality/Parts/IssuingMaterial.ascx" TagName="IssuingMaterial"
    TagPrefix="udc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>
        <udc:IssuingMaterial runat="server" ID="Issuing_Material" />
    </div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
                <script type="text/javascript" language="javascript">
                    function AllowOnlyNumeric(e) {
                        if (window.event) // IE 
                        {
                            if (((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) & e.keyCode != 46) {
                                event.returnValue = false;
                                alert("Unit Issue allows only valid numeric value up to 2 decimal places!");
                                return false;
                            }
                        }
                        else { // Fire Fox
                            if (((e.which < 48 || e.which > 57) & e.which != 8) & e.which != 46) {
                                e.preventDefault();
                                alert("Unit Issue allows only valid numeric value up to 2 decimal places!");
                                return false;
                            }
                        }
                    }
                </script>
            </div>
        <div class="box span12">
                <div class="box-header well">
                    <h2>
                        Issue Material</h2>
                </div>
                <div class="box-content">
                <asp:Panel ID="pnlSearch" runat="server">
                    <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                <tbody>
                    <tr>
                        <td class="center" width="25%">
                            Demand Note Number
                        </td>
                        <td class="center" width="75%">
                            <asp:TextBox ID="txtDemandNoteNumber" CssClass="TextBox" runat="server"></asp:TextBox>
                            
                            <asp:LinkButton ID="LinkSearch" runat="server" CommandName="cmdEdit" CssClass="Search icon198"
                                            ToolTip="Search" onclick="LinkSearch_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </tbody>
            </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlIssueMaterial" runat="server">
             <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                        <tr>
                                <td class="center" width="25%">
                                    Contractor Work Order
                                </td>
                                <td class="center" width="75%" colspan="3">
                                    <asp:Label ID="lblContractorWorkOrder" runat="server"></asp:Label>                                    
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="center" width="25%">
                                    <asp:Label ID="lblIssueDemandVoucher1" runat="server" Text="Issue Demand Voucher"></asp:Label>
                                </td>
                                <td class="center" width="25%">
                                    <asp:Label ID="lblIssueDemandVoucher" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnDemandVoucherId" runat="server" />
                                </td>
                                <td class="center" width="25%">
                                    Issue Demand Date
                                </td>
                                <td class="center" width="25%">
                                    <asp:Label ID="lblIssueDemandDate" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnStatusTypeId" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="mGriditem">
                            <asp:GridView ID="gvDemandMaterial" runat="server" AutoGenerateColumns="False"
                                AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                EmptyDataText="No-Items Available" 
                                onrowdatabound="gvDemandMaterial_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="56px">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chbxSelectAll" runat="server" Text="Select" AutoPostBack="true"
                                                OnCheckedChanged="chbxSelectAll_Click" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkbxDemandVoucherDetails" runat="server" AutoPostBack="true" OnCheckedChanged="on_chbx_Activity_Click" />
                                            <asp:HiddenField ID="hdfContractorPOMappingId" runat="server" Value='<%# Eval("DeliverySchedule.Id")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activity Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivityDiscription" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. Of Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Demanded">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitDemanded" runat="server" Text='<%#Eval("ItemRequired") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Issued">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitIssued" runat="server" Text='<%#Eval("UnitIssued") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>                        
                    
                    <div class="Button_align">
                        <asp:Button ID="btnAddDemandMaterial" runat="server" Text="Add" 
                            CssClass="button_color action" onclick="btnAddDemandMaterial_Click" />
                    </div>
            <br /><br /><br />
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            
                            <tr>
                                <td class="center" width="25%">
                                    Contractor Name
                                </td>
                                <td class="center" width="25%">
                                    <asp:Label ID="lblContractorName" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdfContractorId" runat="server" />
                                </td>
                                <td class="center" width="25%">
                                    Contract Number
                                </td>
                                <td class="center" width="25%">
                                    <asp:Label ID="lblContractNumber" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="center">
                                    Issue Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtIssueDate" Enabled="false" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ToolTip="Select Date" CssClass="Calender icon175" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtIssueDate"
                                        PopupButtonID="ImageButton3">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="center">
                                    Remarks
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="mlttext" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                           <tr>
                                <td class="center" width="25%">
                                    Upload Document
                                    <asp:HiddenField ID="hdfUploadDocId" runat="server" />
                                </td>
                                <td width="75%" class="center" colspan="3">                                    
                                    <udc:UploadDocuments ID="ctrl_UploadDocument" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <hr />  
                    <br />
                        <div style="color: Red">
                            <asp:Literal ID="ltrl_err_msg" runat="server">
                            </asp:Literal>
                        </div>                 
                   <div>
                    <asp:GridView ID="gvIssueMaterial" runat="server" AutoGenerateColumns="False"
                                AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                EmptyDataText="No-Items Available" 
                           onrowcommand="gvIssueMaterial_RowCommand" 
                           onrowdatabound="gvIssueMaterial_RowDataBound">
                                <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server" Text='<%#((Container.DataItemIndex)+1)%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Activity Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivityDiscription" runat="server" Text='<%#Eval("DeliverySchedule.ActivityDescription") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Item.ModelSpecification.Brand") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. Of Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNOF" runat="server" Text='<%#Eval("NumberOfUnit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Demanded">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitDemanded" runat="server" Text='<%#Eval("UnitDemanded") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitLeft" runat="server" Text='<%#Eval("UnitLeft") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Issue">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUnitIssue" runat="server" onkeypress="AllowOnlyNumeric(event);"
                                                Width="80px" Text='<%#Eval("UnitIssued") %>'></asp:TextBox>                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmdDelete"
                                                CommandArgument='<%#Container.DataItemIndex%>' OnClientClick="return confirm('Are you sure you want to delete this Activity Description?') "
                                                ToolTip="Delete" CssClass="button icon186" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                   </div>                  
            <div class="Button_align">
                <asp:Button ID="btnReset" runat="server" Text="Reset" 
                    CssClass="button_color action red" onclick="btnReset_Click" />
                <asp:Button ID="btnSaveDraft" runat="server" Text="SaveDraft" 
                    CssClass="button_color action green" onclick="btnSaveDraft_Click" />
            </div>
            </asp:Panel>
            </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>--%>
</asp:Content>
