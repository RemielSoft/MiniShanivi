<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" CodeBehind="ViewIssueMaterialVoucher.aspx.cs" Inherits="MiniERP.Quality.ViewIssueMaterialVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                       View Issue Material Voucher</h2>
                </div>
                <div class="box-content">
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center">
                                    Contract Name
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem>--select--</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td class="center">
                                    Project Name
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="DropDownList4" runat="server">
                                        <asp:ListItem>--select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                            <td class="center">
                                    Issue Date
                                </td>
                                <td class="center">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="ImgBtn" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                </td>
                                <td class="center">
                                    Remark
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                
                                    
                                
                            </tr>
                        </tbody>
                    </table>
                    <hr />
                    <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            <tr>
                                <td class="center">
                                    Item Category
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>--select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="center">
                                    Item
                                </td>
                                <td class="center">
                                    <asp:DropDownList ID="DropDownList3" runat="server">
                                        <asp:ListItem>--select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Item Quantity
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="TextBox3" runat="server" ></asp:TextBox>
                                </td>
                                <td class="center">
                                   Item Brand
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="TextBox5" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Item Model
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="TextBox6" CssClass="width80" runat="server"></asp:TextBox>
                                </td>
                                <td class="center">
                                    
                                </td>
                                <td class="center">
                                    
                                </td>
                            </tr>
                            
                        </tbody>
                    </table>
                    <div class="Button_align">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_color action red" />
                        <asp:Button ID="Button2" runat="server" Text="Add" CssClass="button_color action" />
                    </div>
                </div>
            </div>
            <table class="mGrid">
                <tr>
                    <th>
                        Contract Name 
                    </th>
                    <th>
                        Project Name 
                    </th>
                    <th>
                        Issue Date 
                    </th>
                    <th>
                        Item Category 
                    </th>
                    <th>
                        Item 
                    </th>
                    <th>
                        Item Quantity 
                    </th>
                    <th>
                        Item Brand 
                    </th>
                    <th>
                        Item Model 
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
                <tr>
                    <td>
                        Vipin 
                    </td>
                    <td>
                      ABC Project
                    </td>
                    <td>
                        12/03/2013
                    </td>
                    <td>
                        Pipe
                    </td>
                    <td>
                        intex Pipe
                    </td>
                    <td>
                        10
                    </td>
                    <td>
                        Intex
                    </td>
                    <td>
                        3X-200
                    </td>
                    <td>
                        <asp:Image ID="imgEdit" runat="server" CssClass="button icon145 " />
                        <asp:Image ID="imgDelete" runat="server" CssClass="button icon153 " />
                    </td>
                </tr>
                
                 
            </table>
            <asp:GridView ID="gvSupplierQuotation" runat="server" AutoGenerateColumns="False"
                AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblContractorName" runat="server" Text='<%#Eval("Item") %>'>' /></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("ItemCategory") %>'>' /></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("ItemUnit") %>'>' /></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ItemStockId") %>'
                                CommandName="cmdEdit" ToolTip="Edit">
                                <asp:Image ID="imgEdit" runat="server" CssClass="button icon145 " /></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ItemStockId") %>'
                                CommandName="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete this Item?') "
                                ToolTip="Delete">
                                <asp:Image ID="imgDelete" runat="server" CssClass="button icon186" /></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
          
       
            <div class="Button_align">
                <asp:Button ID="Button1" runat="server" Text="Reset" CssClass="button_color action red" />
                <asp:Button ID="Button3" runat="server" Text="Save" CssClass="button_color action green" />
                <asp:Button ID="btnUpdate" runat="server" Text="View Issue Material Voucher" CssClass="button_color action gray" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
