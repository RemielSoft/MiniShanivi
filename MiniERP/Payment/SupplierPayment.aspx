<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="SupplierPayment.aspx.cs" Inherits="MiniERP.Payment.SupplierPayment"  ValidateRequest="false"  %>

<%@ Register Src="~/Payment/Parts/Payment.ascx" TagName="PaymentUC" TagPrefix="ucPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../Scripts/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".remove").live("click", function () {

                $(this).parent("p").remove();
            });
        });
    </script>
    <script type="text/javascript">
        function addFileUploadBox() {
            $(".new-upload:last").after("<p class='new-upload'><input class='newFile' name='newfile[]' type='file' size='50'/><a href='#' class='remove'>remove</a></p>");
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <ucPayment:PaymentUC id="payment" runat="server">
            </ucPayment:PaymentUC>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="payment" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
