<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="CompanyWorkOrder.aspx.cs" Inherits="MiniERP.Company.CompanyWorkOrder" ValidateRequest="false" %>

<%@ Register Src="~/Company/Parts/CompanyWorkOrder.ascx" TagName="CompanyWorkOrder"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>
        <uc:CompanyWorkOrder ID="udc" runat="server"></uc:CompanyWorkOrder>
    </div>
</asp:Content>
