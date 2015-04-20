<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewContractorQuotation.aspx.cs" Inherits="MiniERP.Quotation.ViewContractorQuotation" ValidateRequest="false"%>

<%@ Register Src="Parts/ViewQuotation.ascx" TagName="Quotation" TagPrefix="uc_quotation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc_quotation:Quotation ID="quotation" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
