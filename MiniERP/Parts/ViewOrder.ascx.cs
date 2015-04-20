using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniERP.Shared;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.Configuration;


namespace MiniERP.Parts
{
    public partial class ViewOrder : System.Web.UI.UserControl
    {
        #region Global Variable(s)

        Int32 daysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        BasePage basePage = new BasePage();

        ContractorBL contractorBL = new ContractorBL();
        SupplierBL supplierBL = new SupplierBL();
        QuotationBL quotationBL = new QuotationBL();
        DeliveryScheduleBL deliveryScheduleBL = new DeliveryScheduleBL();
        PaymentTermBL paymentTermBL = new PaymentTermBL();
        TermAndConditionBL termAndConditionBL = new TermAndConditionBL();
        CompanyWorkOrderBL CompWorkOrderBL = new CompanyWorkOrderBL();

        List<QuotationDOM> lstQuotation = null;
        List<DeliveryScheduleDOM> lstDeliverySchedule = null;
        List<PaymentTerm> lstPaymentTerm = null;
        List<TermAndCondition> lstTermAndCondition = null;
        List<ItemTransaction> lstItemTransaction = null;

        #endregion

        #region Protected Method(s)

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                this.ToDate = DateTime.Now;
                this.FromDate = DateTime.Now.AddDays(-daysCount);


                TextFromDate1.Text = this.FromDate.ToString("dd'/'MM'/'yyyy");
                TextToDate1.Text = this.ToDate.ToString("dd'/'MM'/'yyyy");


                LabelAssociation();
                if (PageType == "Contractor")
                {

                    lblName.Text = "Contractor Name";
                    lblContractNo.Text = "Contract No";
                    basePage.BindDropDownData(ddlContractor, "Name", "ContractorId", contractorBL.ReadContractor(null));
                    basePage.BindDropDown(ddlContractNo, "ContractNumber", "CompanyWorkOrderId", CompWorkOrderBL.ReadCompOrder(null));
                }
                else
                {

                    lblName.Text = "Supplier Name";

                    lblContractNo.Visible = false;
                    ddlContractNo.Visible = false;
                    basePage.BindDropDownData(ddlContractor, "Name", "SupplierId", supplierBL.ReadSupplier(null));
                }

                ddlContractor.Items.Insert(0, new ListItem("All", "0"));
                ddlContractor.SelectedValue = "0";

                BindGrid(0, ToDate, FromDate, String.Empty, String.Empty);
                CalToDate.EndDate = DateTime.Now;
                CalFromDate.EndDate = DateTime.Now;
            }

        }

        private void PageDefaults()
        {
            basePage.Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            ResetViewState();
            if (txtWrkOrderNo.Text.Trim() == string.Empty)
            {
                basePage.Alert("Please Enter Work Order Number", LinkSearch);
            }
            else
            {
                WorkOrderNo = txtWrkOrderNo.Text;
                BindGrid(ContractorId, ToDate, FromDate, ContractNo, WorkOrderNo);
            }
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            ResetViewState();
            if (ddlContractNo.SelectedItem != null && ddlContractNo.SelectedValue!= "0")
                ContractNo = Convert.ToString(ddlContractNo.SelectedItem.Text);
            ContractorId = Convert.ToInt32(ddlContractor.SelectedValue);
            if (!string.IsNullOrEmpty(TextFromDate1.Text))
            {
                FromDate = DateTime.ParseExact(TextFromDate1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(TextToDate1.Text))
            {
                ToDate = DateTime.ParseExact(TextToDate1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            }
            if (!string.IsNullOrEmpty(TextFromDate1.Text) && !string.IsNullOrEmpty(TextToDate1.Text))
            {
                FromDate = DateTime.ParseExact(TextFromDate1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ToDate = DateTime.ParseExact(TextToDate1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            // Compare that to date must be greater than from date
            if (ToDate < FromDate && (ToDate != DateTime.MinValue && FromDate != DateTime.MinValue))
            {
                basePage.Alert("To Date should be Greater Than From Date", btnSearch);
            }

            BindGrid(ContractorId, ToDate, FromDate, ContractNo, WorkOrderNo);

        }
        protected void gvViewOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvViewOrder.PageIndex = e.NewPageIndex;
            BindGrid(ContractorId, ToDate, FromDate, ContractNo, WorkOrderNo);
        }
       
        protected void gvViewOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str = Convert.ToString(e.CommandArgument);
            string[] strid = str.Split(',');
            int id = Convert.ToInt32(strid[0].ToString());
            ViewState["id"] = id;
            if (e.CommandName == "lnkPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtnPrint");

                string quotationNo = strid[1].ToString();
                if (PageType == "Contractor")
                {
                    //Response.Redirect("~/SSRReport/ContractorWorkOrderReport.aspx?id=" + id + "&quotationNo=" + quotationNo + "");
                    basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/ContractorWorkOrderReport.aspx?id=" + id + "&quotationNo=" + quotationNo + "", "ContractorQuotation");
                }
                else
                {
                    //Response.Redirect("~/SSRReport/SupplierPurchaseOrderReport.aspx?id=" + id + "&quotationNo=" + quotationNo + "");
                    basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/SupplierPurchaseOrderReport.aspx?id=" + id + "&quotationNo=" + quotationNo + "", "SupplierQuotation");
                }
            }

            if (e.CommandName == "lnkQuotation")
            {
                lstQuotation = new List<QuotationDOM>();
                if (PageType == "Contractor")
                {
                    lstItemTransaction = new List<ItemTransaction>();
                    lstItemTransaction = quotationBL.ReadContractorQuotationMapping(id);
                    lstQuotation = quotationBL.ReadContractorQuotation(id, null);
                    BindEmptyGrid(gvContractorPOItems);
                    if (lstItemTransaction.Count > 0)
                    {
                        gvContractorPOItems.DataSource = lstItemTransaction;
                        gvContractorPOItems.DataBind();
                    }

                    lstDeliverySchedule = new List<DeliveryScheduleDOM>();
                    lstDeliverySchedule = deliveryScheduleBL.ReadDeliveryScheduleByQuotationID(id, Convert.ToInt16(QuotationType.Contractor));
                    BindEmptyGrid(gvDeliverySchedule);
                    if (lstDeliverySchedule.Count > 0)
                    {
                        gvDeliverySchedule.DataSource = lstDeliverySchedule;
                        gvDeliverySchedule.DataBind();
                        //gvDeliverySchedule.Columns[1].Visible = false;
                        
                    }

                    lstPaymentTerm = new List<PaymentTerm>();
                    lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(id, Convert.ToInt16(QuotationType.Contractor));
                    BindEmptyGrid(gvPaymentTerm);
                    if (lstPaymentTerm.Count > 0)
                    {

                        gvPaymentTerm.DataSource = lstPaymentTerm;
                        gvPaymentTerm.DataBind();
                    }

                    lstTermAndCondition = new List<TermAndCondition>();
                    lstTermAndCondition = termAndConditionBL.ReadTermAndConditionByQuotationID(id, Convert.ToInt16(QuotationType.Contractor));



                    BindEmptyGrid(gvTermAndCondition);
                    if (lstTermAndCondition.Count > 0)
                    {

                        gvTermAndCondition.DataSource = lstTermAndCondition;
                        gvTermAndCondition.DataBind();
                    }
                }
                else
                {
                    lstItemTransaction = new List<ItemTransaction>();
                    lstItemTransaction = quotationBL.ReadSupplierQuotationMapping(id);
                    lstQuotation = quotationBL.ReadSupplierQuotation(id, null);
                    BindEmptyGrid(gvSupplierPOItems);
                    if (lstItemTransaction.Count > 0)
                    {
                        gvSupplierPOItems.DataSource = lstItemTransaction;
                        gvSupplierPOItems.DataBind();
                        gvSupplierPOItems.Columns[0].Visible = false;
                    }

                    lstDeliverySchedule = new List<DeliveryScheduleDOM>();
                    lstDeliverySchedule = deliveryScheduleBL.ReadDeliveryScheduleByQuotationID(id, Convert.ToInt16(QuotationType.Supplier));
                    BindEmptyGrid(gvDeliverySchedule);
                    if (lstDeliverySchedule.Count > 0)
                    {
                        gvDeliverySchedule.DataSource = lstDeliverySchedule;
                        gvDeliverySchedule.DataBind();
                        gvDeliverySchedule.Columns[0].Visible = false;
                    }

                    lstPaymentTerm = new List<PaymentTerm>();
                    lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(id, Convert.ToInt16(QuotationType.Supplier));
                    BindEmptyGrid(gvPaymentTerm);
                    if (lstPaymentTerm.Count > 0)
                    {
                        gvPaymentTerm.DataSource = lstPaymentTerm;
                        gvPaymentTerm.DataBind();
                    }

                    lstTermAndCondition = new List<TermAndCondition>();
                    lstTermAndCondition = termAndConditionBL.ReadTermAndConditionByQuotationID(id, Convert.ToInt16(QuotationType.Supplier));
                    BindEmptyGrid(gvTermAndCondition);
                    if (lstTermAndCondition.Count > 0)
                    {
                        gvTermAndCondition.DataSource = lstTermAndCondition;
                        gvTermAndCondition.DataBind();
                    }

                }

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);

                CalculateAllToalValue();

                ModalPopupExtender2.Show();
            }
        }

        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            TextToDate1.Text = string.Empty;
        }
        protected void LinkbtnClear_Click(object sender, EventArgs e)
        {
            TextFromDate1.Text = string.Empty;
        }

        protected void gvViewOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblHCName = ((Label)gvViewOrder.HeaderRow.Cells[2].FindControl("lblHCName"));
                Label lblHCNumber = ((Label)gvViewOrder.HeaderRow.Cells[3].FindControl("lblHCNumber"));
                Label lblCNumber = ((Label)e.Row.FindControl("lblCNumber"));
                Label CName = ((Label)e.Row.FindControl("lblCName"));
                Label lblQuotationDate = ((Label)e.Row.FindControl("lblQuotationDate"));
                Label CNumber = ((Label)e.Row.FindControl("lblCNumber"));
                LinkButton lbtnQuotation = (LinkButton)e.Row.FindControl("lbtnQuotation");//
                LinkButton lbtnPrint = (LinkButton)e.Row.FindControl("lbtnPrint");
                QuotationDOM ob = (QuotationDOM)e.Row.DataItem;
                if (PageType == "Contractor")
                {
                    lbtnQuotation.Text = ob.ContractQuotationNumber;
                    lblHCName.Text = "Contractor Name";
                    CName.Text = ob.ContractorName;
                    lblQuotationDate.Text = ob.OrderDate.ToString("dd/MM/yyyy");
                    gvViewOrder.Columns[5].Visible = false;
                    gvViewOrder.Columns[6].Visible = false;
                }
                else
                {
                    lbtnQuotation.Text = ob.SupplierQuotationNumber;
                    lbtnQuotation.CommandArgument = ob.SupplierQuotationId.ToString();
                    lbtnPrint.CommandArgument = ob.SupplierQuotationId + "," + ob.SupplierQuotationNumber;
                    lblHCName.Text = "Supplier Name";
                    CName.Text = ob.SupplierName;
                    lblQuotationDate.Text = ob.OrderDate.ToString("dd/MM/yyyy");
                }
            }
        }
        #endregion

        #region Private Method(s)

        private void LabelAssociation()
        {
            if (PageType == "Contractor")
            {
                lbl_WorkOrder.Text = " Contractor Work Order";
                lbl_Order.Text = "Contractor Work Order";
            }
            else
            {
                lbl_WorkOrder.Text = " Supplier Purchase Order";
                lbl_Order.Text = "Supplier Purchase Order";
            }
        }

        public void BindGrid(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String WorkOrderNo)
        {

            lstQuotation = new List<QuotationDOM>();
            if (ViewState["PageType"].ToString().Trim() == "Contractor")
            {
                lstQuotation = quotationBL.ReadContractorQuotationView(contractorId, toDate, fromDate, contractNo, WorkOrderNo);

            }
            else
            {
                lstQuotation = quotationBL.ReadSupplierQuotationView(contractorId, toDate, fromDate, WorkOrderNo);

            }
            if (lstQuotation.Count > 0)
            {
                gvViewOrder.DataSource = lstQuotation;
                gvViewOrder.DataBind();
                if (PageType == "Supplier")
                {
                    gvViewOrder.Columns[7].Visible = false;

                }
                else
                {
                    gvViewOrder.Columns[3].Visible = false;
                    gvViewOrder.Columns[4].Visible = false;
                }
            }
            else
            {
                BindEmptyGrid(gvViewOrder);
            }
            //ResetContorls();
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }
        private void ResetContorls()
        {
            txtWrkOrderNo.Text = string.Empty;
            ddlContractNo.SelectedValue = "0";
            ddlContractor.SelectedValue = "0";
            TextFromDate1.Text = string.Empty;
            TextToDate1.Text = string.Empty;
        }
        private void ResetViewState()
        {
            FromDate = DateTime.MinValue;
            ToDate = DateTime.MinValue;
            ContractorId = 0;
            SupplierId = 0;
            ContractNo = String.Empty;
            WorkOrderNo = String.Empty;
        }

        private void CalculateAllToalValue()
        {
            Decimal total = 0, totalDiscount = 0, totalExciseDuty = 0, totalTax = 0;

            if (lstItemTransaction != null && lstItemTransaction.Count > 0)
            {
                foreach (ItemTransaction item in lstItemTransaction)
                {
                    Decimal tempDiscount = 0, tempExciseDuty = 0;
                    total += item.NumberOfUnit * item.PerUnitCost;

                    if (item.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Value))
                    {
                        totalDiscount += item.NumberOfUnit * item.PerUnitDiscount;
                        tempDiscount = item.NumberOfUnit * item.PerUnitDiscount;
                    }
                    else
                    {
                        totalDiscount += (item.NumberOfUnit * item.PerUnitCost) * (item.PerUnitDiscount / 100);
                        tempDiscount = (item.NumberOfUnit * item.PerUnitCost) * (item.PerUnitDiscount / 100);
                    }
                    totalExciseDuty += ((item.NumberOfUnit * item.PerUnitCost) - tempDiscount) * (item.TaxInformation.ExciseDuty / 100);
                    tempExciseDuty = ((item.NumberOfUnit * item.PerUnitCost) - tempDiscount) * (item.TaxInformation.ExciseDuty / 100);
                    totalTax += ((item.NumberOfUnit * item.PerUnitCost) - tempDiscount + tempExciseDuty) * ((item.TaxInformation.ServiceTax + item.TaxInformation.VAT + item.TaxInformation.CSTWithCForm + item.TaxInformation.CSTWithoutCForm) / 100);

                }
            }
            lblTotal.Text = String.Concat("Total : ", total.ToString("F2"));
            lblTotalDiscount.Text = String.Concat("Total Discount : ", totalDiscount.ToString("F2"));
            lblTotalExciseDuty.Text = String.Concat("Total Excise Duty : ", totalExciseDuty.ToString("F2"));
            lblTotalTax.Text = String.Concat("Total Tax : ", totalTax.ToString("F2"));
            if (lstQuotation != null && lstQuotation.Count > 0)
            {
                lblPackaging.Text = String.Concat("Packaging : ", lstQuotation[0].Packaging.ToString("F2"));
                lblfreight.Text = String.Concat("Freight : ", lstQuotation[0].Freight.ToString("F2"));
                lblGrandTotal.Text = String.Concat("Total Amount : ", lstQuotation[0].TotalNetValue.ToString("F2"));
            }

        }

        #endregion

        #region public Properties

        private DateTime FromDate
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(ViewState["FromDate"]);
                }
                catch
                {

                    return DateTime.MinValue;
                }

            }

            set { ViewState["FromDate"] = value; }
        }

        private DateTime ToDate
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(ViewState["ToDate"]);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }

            set { ViewState["ToDate"] = value; }
        }

        private Int32 ContractorId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["contractorId"];
                }
                catch
                {

                    return 0;
                }
            }
            set
            {
                ViewState["contractorId"] = value;
            }
        }

        private String ContractNo
        {
            get
            {
                try
                {
                    return (String)ViewState["contractNo"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["contractNo"] = value;
            }
        }

        private String WorkOrderNo
        {
            get
            {
                try
                {
                    return (String)ViewState["WorkOrderNo"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["WorkOrderNo"] = value;
            }
        }

        public String PageType
        {
            get
            {
                try
                {
                    return (String)ViewState["PageType"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["PageType"] = value;

            }
        }

        private Int32 SupplierId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["supplierId"];
                }
                catch
                {

                    return 0;
                }
            }
            set
            {
                ViewState["supplierId"] = value;
            }
        }

        #endregion

    }
}