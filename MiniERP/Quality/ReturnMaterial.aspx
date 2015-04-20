<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ReturnMaterial.aspx.cs" ValidateRequest="false" Inherits="MiniERP.Quality.ReturnMaterial" %>

<%@ Register Src="~/Quality/Parts/ReturnMaterial.ascx" TagName="ReturnMaterial" TagPrefix="udc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<div>
    <udc:ReturnMaterial runat="server" id="Return_Material" />
</div>   
</asp:Content>
