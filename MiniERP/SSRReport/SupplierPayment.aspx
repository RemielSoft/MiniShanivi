﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierPayment.aspx.cs"
    Inherits="MiniERP.SSRReport.SupplierPayment" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin-left: 50px;">
        <rsweb:ReportViewer ID="rptSupplierPayment" runat="server" ShowParameterPrompts="False"
            ShowPromptAreaButton="False" Width="100%" Height="100%" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
