<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ContractorPayment.aspx.cs" Inherits="MiniERP.Payment.ContractorPayment"
    ValidateRequest="false" %>
<%@ Register Src="Parts/Payment.ascx" TagName="PaymentUC" TagPrefix="ucPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>
        <ucPayment:PaymentUC ID="payment" runat="server"></ucPayment:PaymentUC>
    </div>
</asp:Content>
