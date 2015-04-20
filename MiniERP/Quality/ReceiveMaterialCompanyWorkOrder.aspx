<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveMaterialCompanyWorkOrder.aspx.cs" 
Inherits="MiniERP.Quality.ReceiveMaterialCompanyWorkOrder" MasterPageFile="~/Masters/ERP.Master" ViewStateMode="Enabled" 
ValidateRequest="false"%>

<%@ Register Src="Parts/ReceiveMaterialCompanyWorkOrder.ascx" TagName="ReceiveMaterialUC" TagPrefix="ucQuality" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <ucQuality:ReceiveMaterialUC id="receiveQuality" runat="server"></ucQuality:ReceiveMaterialUC>
    
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>


