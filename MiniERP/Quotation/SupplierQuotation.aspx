<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="SupplierQuotation.aspx.cs" Inherits="MiniERP.Quotation.SupplierQuotation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Parts/Quotation.ascx" TagName="Quotation" TagPrefix="uc_quotation" %>
<%@ Register Src="Parts/DeliveryScheduleContractor.ascx" TagName="Delivery" TagPrefix="uc_delivery" %>
<%@ Register Src="Parts/PaymentTerms.ascx" TagName="Payment" TagPrefix="uc_payment" %>
<%--<%@ Register Src="Parts/TermConditions.ascx" TagName="TermConditions" TagPrefix="uc_term_conditions" %>--%>

<%@ Register Src="Parts/GeneralTermConditions.ascx" TagName="TermConditions" TagPrefix="uc_term_conditions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <ajaxtoolkit:TabContainer ID="tbc_quotation" runat="server" OnActiveTabChanged="tbc_quotation_ActiveTabChanged"
        AutoPostBack="true">
        <ajaxtoolkit:TabPanel ID="tbpnl_quotation" HeaderText="Generate Supplier Purchase Order"
            TabIndex="0" runat="server">
            <ContentTemplate>
                <uc_quotation:Quotation ID="QuotationControl" runat="server" />
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>
        <ajaxtoolkit:TabPanel ID="tbpnl_DeliverySchedule" HeaderText="Delivery Schedule"
            TabIndex="1" runat="server">
            <ContentTemplate>
                <uc_delivery:Delivery ID="DeliveryControl" runat="server" />
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>
        <ajaxtoolkit:TabPanel ID="tbpnl_PaymentTerms" HeaderText="Payment Terms" TabIndex="2"
            runat="server">
            <ContentTemplate>
                <uc_payment:Payment ID="PaymentControl" runat="server" />
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>
        <ajaxtoolkit:TabPanel ID="tbpnl_TermConditions" HeaderText=" General Terms And Conditions" TabIndex="3"
            runat="server">
            <ContentTemplate>
                <uc_term_conditions:TermConditions ID="TermConditionControl" runat="server" />
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>
    </ajaxtoolkit:TabContainer>
</asp:Content>
