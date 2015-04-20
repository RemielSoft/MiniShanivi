<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" CodeBehind="ContractorPurchaseOrderApproval.aspx.cs" Inherits="MiniERP.PurchaseOrder.ContractorPurchaseOrderApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<div class="box span12">
        <div class="box-header well">
            <h2>
                Contractor Work Order Approval</h2>
        </div>
        <div class="box-content">
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                        <td class="center">
                          Status
                        </td>
                        <td colspan="3" style=" padding-right:200px;" >
                             <asp:DropDownList ID="ddlist1" runat="server">
                                 <asp:ListItem>Pending</asp:ListItem>
                                 <asp:ListItem>Approved</asp:ListItem>
                                 <asp:ListItem>Rejected</asp:ListItem>
                                 <asp:ListItem></asp:ListItem>
                             </asp:DropDownList>     
                        </td>
                         

                        
                    </tr>
                    
                  </tbody>
                  </table>  
               
        
    

     <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <table class="mGrid">
        <tr>
            <th style=" width:60px">
                <asp:CheckBox ID="CheckBox1" runat="server" />
                Select
            </th>
            <th>
                Work Order Number
            </th>
            <th>
                Work Order Date
            </th>
            <th>
                Contractor Name
            </th>
            <th>
                Service tax (%)
            </th>
            <th>
               Vat (%)
            </th>
            <th>
                CST (with C Form) (%)
            </th>
            <th>
                CST (Without C Form) (%)
            </th>
            <th>
                Discount (Fix/Percentage)
            </th>
            <th>
                Total Discount (%/INR)
            </th>
            <th>
                Total Net Value (INR)
            </th>
            <th>
                Status
            </th>
            
            
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="CheckBox2" runat="server" />
            </td>
            <td>
                <a href="#">WorkOrder-001</a>
            </td>
            <td>
                03/01/2012
            </td>
            <td>
                Atul
            </td>
            <td>
                3%
            </td>
            <td>
                2%
            </td>
            <td>
                0%
            </td>
            <td>
                0%
            </td>
            <td>
                Fix
            </td>
            <td>
                879 INR
            </td>
            <td>
                306,600 INR
            </td>
            <td>
                Pending
            </td>
            
            
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="CheckBox3" runat="server" />
            </td>
            <td>
                <a href="#">WorkOrder-002</a>
            </td>
            <td>
                03/01/2012
            </td>
            <td>
                Amit
            </td>
            <td>
                3%
            </td>
            <td>
                2%
            </td>
            <td>
                0%
            </td>
            <td>
                0%
            </td>
            <td>
                Percentage
            </td>
            <td>
                2%
            </td>
            <td>
                306,600 INR
            </td>
            <td>
                Pending
            </td>
           
            
        </tr>
        
    </table>
                        </tbody>
                    </table>
            
            
    <br />
    <div class="Button_align">
                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button_color action" />
                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button_color action gray" />
                
            </div>
            </div>
    </div>
</asp:Content>
