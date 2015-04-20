<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ItemDetail.aspx.cs" Inherits="MiniERP.SSRReport.ItemDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="box-content">
        <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
            <tbody>
                <tr>
                    
                    <td>
                        <asp:Label ID="lblWorkOrderNo" runat="server" Text="Contractor Work Order"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkOrderNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="LnkButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175"
                            TabIndex="1"></asp:LinkButton>
                        <ajaxtoolkit:CalendarExtender ID="calFromDate" runat="server" Format="MM/dd/yyyy"
                            PopupButtonID="LnkButton1" TargetControlID="txtFromDate" Enabled="True">
                        </ajaxtoolkit:CalendarExtender>
                    </td>
                    <td>
                        <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="LnkButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175"
                            TabIndex="1"></asp:LinkButton>
                        <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                            PopupButtonID="LnkButton2" TargetControlID="txtToDate" Enabled="True">
                        </ajaxtoolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="View Report" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div style="margin-left: 50px;">
        <rsweb:ReportViewer ID="rptItemDetails" runat="server" ShowParameterPrompts="false"
            ShowPromptAreaButton="False" Width="100%" Height="100%" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
