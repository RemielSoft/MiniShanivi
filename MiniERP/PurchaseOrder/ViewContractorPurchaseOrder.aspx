<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ViewContractorPurchaseOrder.aspx.cs" Inherits="MiniERP.PurchaseOrder.ViewContractorPurchaseOrder" %>


<%@ Register Src="~/Parts/ViewOrder.ascx" TagName="PurchaseOrder" TagPrefix="uc_ViewOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <uc_ViewOrder:PurchaseOrder ID="ViewContractororder" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
