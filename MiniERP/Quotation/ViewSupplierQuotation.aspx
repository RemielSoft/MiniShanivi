<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewSupplierQuotation.aspx.cs" Inherits="MiniERP.Quotation.ViewSupplierQuotation" ValidateRequest="false"%>

<%@ Register Src="Parts/ViewQuotation.ascx" TagName="Quotation" TagPrefix="uc_quotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc_quotation:Quotation ID="quotation" runat="server" />
</asp:Content>
