<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchItemStock.aspx.cs"
    ValidateRequest="false" Inherits="MiniERP.Admin.SearchItemStock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlSearchItemStock" runat="server">
        <div class="box span12">
            <div class="box-header well">
                <h2>Search Item Stock</h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblStore" runat="server" Text="Store"></asp:Label>
                            </td>
                            <td  class="center">
                                <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropdown">
                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblItemNameS" runat="server" Text="Item Name"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtItemNameS" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                          
                        </tr>
                        <tr>
                              <td class="center">
                                <asp:Label ID="lblSpecificationS" runat="server" Text="Specification"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtSpecificationS" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblUnitMeasurementS" runat="server" Text="Unit Measurement"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:DropDownList ID="ddlUnitMeasurementS" runat="server" CssClass="dropdown">
                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div class="Button_align">
                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="button_color action red"
                        OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlSearchItem" runat="server">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    Search Item
                </h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtItemName" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblSpecification" runat="server" Text="Specification"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtSpecification" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblUnitMeasurement" runat="server" Text="Unit Measurement"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:DropDownList ID="ddlUnitMeasurement" runat="server" CssClass="dropdown">
                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="dropdown">
                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblBrand" runat="server" Text="Brand"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txBrand" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblModelCode" runat="server" Text="Model Code"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtModelCode" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div class="Button_align">
                    <asp:Button ID="Button2" runat="server" Text="Search" CssClass="button_color action red"
                        OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlSearchContractor" runat="server">
        <div class="box span12">
            <div class="box-header well">
                <%--<h2>
                    Search Contractor</h2>--%>
                    <h2><asp:Label ID="lblHeaderSearch" runat="server" Text=""></asp:Label></h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtCompanyName" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblCpnEmailId" runat="server" Text="Company Email-Id"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtCpnEmailId" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtCity" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtState" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblCpnPhone" runat="server" Text="Company Phone No."></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtCpnPhone" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblCpnMobile" runat="server" Text="Company Mobile No."></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtCpnMobile" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>

                            <td class="center">
                                <asp:Label ID="lblWebSite" runat="server" Text="WebSite"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtWebSite" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>

                            
                            <td class="center">
                                <asp:Label ID="lblPAN" runat="server" Text="PAN"></asp:Label>
                                <br />
                                <asp:Label ID="lblPanType" runat="server" Text="(AAAAA9999A)"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtPAN" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblESI" runat="server" Text="ESI"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtESI" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblTAN" runat="server" Text="TAN"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtTAN" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblFax" runat="server" Text="Fax"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtFax" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <td class="center">
                                <asp:Label ID="lblPF" runat="server" Text="PF"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtPF" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                                <asp:Label ID="lblServiceTaxNo" runat="server" Text="Service Tax No."></asp:Label>
                            </td>
                            <td colspan="3" class="center">
                                <asp:TextBox ID="txtServiceTaxNo" CssClass="TextBox" runat="server"></asp:TextBox>
                            </td>
                            <%--<td class="center">
                                <asp:Label ID="lblWebSite" runat="server" Text="WebSite"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtWebSite" runat="server"></asp:TextBox>
                            </td>--%>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div class="Button_align">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button_color action red"
                        OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
