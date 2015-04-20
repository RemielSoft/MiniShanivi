<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewContractorPayment.aspx.cs" Inherits="MiniERP.Payment.ViewContractorPayment"
    ValidateRequest="false" %>

<%@ Register Src="Parts/PaymentView.ascx" TagName="payment" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:payment ID="paymentView" runat="server"></uc1:payment>
</asp:Content>
