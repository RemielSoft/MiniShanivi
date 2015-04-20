<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierPurchaseOrderReport.aspx.cs" Inherits="MiniERP.SSRReport.SupplierPurchaseOrder" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Purchase Order</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<div style="margin-left:50px; overflow:auto; position:relative;" id="ctl31_ctl10" >--%>
    <%--<div id="ctl31_ctl10" style="height:100%; width:100%; overflow:auto; position:relative;">--%>
    <div style="margin-left:50px;">
        <rsweb:ReportViewer ID="rptSPO" runat="server"  
            ShowParameterPrompts="False" ShowPromptAreaButton="False" Width="100%" Height="100%" SizeToReportContent="true"
             Style="display: table !important; margin: 0px; overflow: auto !important;" ShowBackButton="true">

        </rsweb:ReportViewer>
         <iframe id="frmPrint" name="frmPrint" runat="server" style = "display:none"></iframe> 
    </div>
    </form>
</body>
</html>
