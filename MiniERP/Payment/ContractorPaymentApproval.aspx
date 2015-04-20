<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ContractorPaymentApproval.aspx.cs" Inherits="MiniERP.Payment.ContractorPaymentApproval" %>

<%@ Register Src="Parts/PaymentApproval.ascx" TagName="CSPayment" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:CSPayment ID="CSPayment" runat="server"></uc1:CSPayment>
</asp:Content>
