<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="Test_Control.aspx.cs" Inherits="MiniERP.Test_Control" %>

<%@ Register Src="~/Parts/ViewQuotation.ascx" TagName="Quotation" TagPrefix="uc_quotation" %>
<%@ Register Src="~/Parts/UploadDocsControl.ascx" TagName="UploadDocuments" TagPrefix="udc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox1" runat="server">
    </ajaxtoolkit:CalendarExtender>

</asp:Content>
