<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" CodeBehind="DeliverySechedule.aspx.cs" Inherits="MiniERP.PurchaseOrder.DeliverySechedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

<div class="box span12">
        <div class="box-header well">
            <h2>
                Delivery Schedule</h2>
        </div>
        <div class="box-content">
            <table class="table table-bordered table-striped table-condensed">
                <tbody>
                    <tr>
                        <td class="center">
                          Delivery Schedule Name
                        </td> 
                        <td class="center">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td class="center">
                            Supplier
                        </td>
                        <td class="center">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="center">
                           Purchase Order
                        </td>
                        <td class="center">
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="center">
                            Schedule Date
                        </td>
                        <td class="center">
                            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox> 
                            <asp:LinkButton ID="ImgBtn" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="center">
                           Description
                        </td>
                        <td class="center">
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="center">
                            
                        </td>
                        <td class="center">
                            
                        </td>
                    </tr>
                    
                    
                </tbody>
            </table>
            <div class="Button_align">
                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="button_color action red" />
                <asp:Button ID="Button1" runat="server" Text="Update" CssClass="button_color action blue" />
                <asp:Button ID="Button3" runat="server" Text="Save" CssClass="button_color action green" />
            </div>
        </div>
    </div>
    <table class="mGrid">
   
    <tr><th>Delivery Schedule Name</th>
    <th>Supplier </th>
    <th>Purchase Order</th>
    <th>Schedule Date </th>
    <th>Action</th>
    </tr>
    
    <tr>
    <td>ER25-001</td>
    <td>Amit</td>
    <td>ER25</td>
    <td>12/23/2012</td>
    <td><asp:Image ID="imgEdit" runat="server" CssClass="button icon145 " />
    <asp:Image ID="imgDelete" runat="server" CssClass="button icon186 " />
    </td>
    </tr>

    <tr>
    <td>ER45-002</td>
    <td>Sumit</td>
    <td>ER45</td>
    <td>12/27/2012</td>
    <td>
    <asp:Image ID="Image1" runat="server" CssClass="button icon145 " />
    <asp:Image ID="Image2" runat="server" CssClass="button icon186 " />
    </td>
    </tr>
    </table>

    </asp:Content>
