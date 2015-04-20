<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ContractorInvoice.aspx.cs" Inherits="MiniERP.Invoice.ContractorInvoice" %>

<%@ Register Src="~/Invoice/Parts/InvoiceControls.ascx" TagName="InvoiceControls"
    TagPrefix="udc" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>
        <udc:InvoiceControls runat="server" ID="Invoice_Controls" />
    </div>
</asp:Content>
