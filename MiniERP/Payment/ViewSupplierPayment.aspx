<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" 
CodeBehind="ViewSupplierPayment.aspx.cs" Inherits="MiniERP.Payment.ViewSupplierPayment" ValidateRequest="false" %>
<%@ Register Src="Parts/PaymentView.ascx" TagName="payment" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<uc1:payment id="paymentView" runat="server"></uc1:payment>
</asp:Content>
