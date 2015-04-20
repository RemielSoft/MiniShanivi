<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ViewContractorInvoice.aspx.cs" Inherits="MiniERP.Invoice.ViewContractorInvoice" %>

<%@ Register Src="~/Invoice/Parts/ViewInvoice.ascx" TagName="Invoice1" TagPrefix="uc_invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

        <ContentTemplate>
            <uc_invoice:Invoice1 ID="invoice" runat="Server" />
        </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>
