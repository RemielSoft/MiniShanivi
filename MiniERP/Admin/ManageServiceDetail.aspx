<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ManageServiceDetail.aspx.cs" Inherits="MiniERP.Admin.ManageServiceDetail" ValidateRequest="false" %>

<%@ Register Src="~/Admin/Parts/ServiceDetail.ascx" TagName="serviceDetail" TagPrefix="sd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/ajax_loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="width:40px; height:40px; position:fixed; top:0; right:0; left:0; bottom:0; margin:auto" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="upnlServiceDetail" runat="server">
        <ContentTemplate>
            <sd:serviceDetail ID="ucServiceDetail" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
