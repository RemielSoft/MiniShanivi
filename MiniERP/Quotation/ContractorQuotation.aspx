<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="ContractorQuotation.aspx.cs" Inherits="MiniERP.Quotation.ContractorQuotation" ValidateRequest="false"%>

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
        AutoPostBack="true" BorderStyle="None">
        <ajaxtoolkit:TabPanel ID="tbpnl_quotation" HeaderText="Generate Contractor Quotation"
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
   <%-- <ul class="tabs">
        <li class="active" rel="tab1">Generate Contractor Quotation </li>
        <li rel="tab2">Delivery Schedule </li>
        <li rel="tab3">Payment Terms</li>
        <li rel="tab4">Terms & Conditions </li>
    </ul>
    <div class="tab_container">
        <div id="tab1" class="tab_content">
        </div>
        <!-- #tab1 -->
        <div id="tab2" class="tab_content">
        </div>
        <!-- #tab2 -->
        <div id="tab3" class="tab_content">
        </div>
        <!-- #tab3 -->
        <div id="tab4" class="tab_content">
        </div>
        <!-- #tab4 -->
    </div>--%>
</asp:Content>
