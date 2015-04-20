<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" CodeBehind="ReconsilationRpt.aspx.cs" Inherits="MiniERP.SSRReport.ReconsilationRpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="box-content">
        <table class="table table-bordered table-striped table-condensed searchbg" style="border: 2px solid green;">
            <tbody>
                <tr>
                    <td class="center">
                        <asp:Label ID="lblWorkOrderNo" runat="server" Text="Contractor Work Order Number"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkOrderNo" runat="server"></asp:TextBox>
                    </td>
                   
                    <td>
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="View Report" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div style="margin-left: 50px;">
        <rsweb:ReportViewer ID="rptWorkOrderNo" runat="server" ShowParameterPrompts="false"
            ShowPromptAreaButton="False" Width="100%" Height="100%" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
