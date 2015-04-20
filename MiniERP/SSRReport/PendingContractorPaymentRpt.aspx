<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PendingContractorPaymentRpt.aspx.cs"
    Inherits="MiniERP.SSRReport.PendingContractorPaymentRpt" MasterPageFile="~/Masters/ERP.Master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>
        <div class="box-content">
            <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
                <tbody>
                    <tr>
                        <td class="center">
                            <asp:Label ID="lblContractorName" runat="server" Text="Contractor Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractorName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblQuotationNo" runat="server" Text="Quotation No"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuotationNo" runat="server"></asp:TextBox>
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
                            <ajaxtoolkit:CalendarExtender ID="calFromDate" runat="server" Format="dd/MM/yyyy"
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
                            <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="LnkButton2" TargetControlID="txtToDate" Enabled="True">
                            </ajaxtoolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatusType" runat="server" Text="Status Type"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatusType" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="View Report" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <%--<div class="table">
            <asp:Label ID="lblContractorName" runat="server" Text="Contractor Name"></asp:Label>
            <asp:TextBox ID="txtContractorName" runat="server"></asp:TextBox>
            <asp:Label ID="lblQuotationNo" runat="server" Text="Quotation No"></asp:Label>
            <asp:TextBox ID="txtQuotationNo" runat="server"></asp:TextBox>
        </div>
       
        <div class="table">
            <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
            <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
            <asp:LinkButton ID="LnkButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175"
                TabIndex="1"></asp:LinkButton>
            <ajaxtoolkit:CalendarExtender ID="calFromDate" runat="server" Format="dd/MM/yyyy"
                PopupButtonID="LnkButton1" TargetControlID="txtFromDate" Enabled="True">
            </ajaxtoolkit:CalendarExtender>
            <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
            <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
            <asp:LinkButton ID="LnkButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175"
                TabIndex="1"></asp:LinkButton>
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                PopupButtonID="LnkButton2" TargetControlID="txtToDate" Enabled="True">
            </ajaxtoolkit:CalendarExtender>
            <asp:Label ID="lblStatusType" runat="server" Text="Status Type"></asp:Label>
            <asp:DropDownList ID="ddlStatusType" runat="server">
            </asp:DropDownList>
        </div>--%>
        <div>
            <%--<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="View Report" />--%>
        </div>
    </div>
    <div style="margin-left: 50px;">
        <rsweb:ReportViewer ID="rptPendingContractorPayment" runat="server" ShowParameterPrompts="false"
            ShowPromptAreaButton="False" Width="100%" Height="100%" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
