﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveMaterialReport.aspx.cs"
    Inherits="MiniERP.SSRReport.ReceiveMaterialReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Receive Material</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin-left:50px;">
        <rsweb:ReportViewer ID="rptReceiveMaterial" runat="server" ShowParameterPrompts="False"
            ShowPromptAreaButton="False" Width="80%" Height="80%" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
