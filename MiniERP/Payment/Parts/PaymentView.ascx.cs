using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Configuration;
using BusinessAccessLayer.Invoice;

namespace MiniERP.Parts
{
    public partial class PaymentView : System.Web.UI.UserControl
    {

        #region Global Variable(s)

        Int32 daysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        BasePage basePage = new BasePage();
        PaymentBL paymentBL = new PaymentBL();
        ContractorBL contractorBL = new ContractorBL();
        SupplierBL supplierBL = new SupplierBL();
        ContractorInvoiceBL invoiceBL = new ContractorInvoiceBL();


        List<PaymentDOM> lstPayment = null;
        List<ItemTransaction> lstItemDetail = null;

        PaymentDOM payment = null;

        DateTime currentDate = DateTime.MinValue;
        DateTime twoYearBackDate = DateTime.MinValue;

        int paymentId = 0;
        string invoiceNo = string.Empty;
        #endregion

        #region Protected Events

        protected void Page_Load(object sender, EventArgs e)
        {
            String pageType = this.PageType;

            currentDate = DateTime.Now;
            twoYearBackDate = DateTime.Now.AddDays(-daysCount);
            if (rbtnList.SelectedValue != "2" && rbtnList.SelectedValue != "3")
            {
                this.FromDate = twoYearBackDate;
                this.ToDate = currentDate;
            }

            PageDefaults();
            if (!IsPostBack)
            {
                DefaultLoad();
            }
        }

        private void PageDefaults()
        {
            basePage.Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        private void DefaultLoad()
        {
            ModalPopupExtender2.Hide();
            BindGrid(currentDate, twoYearBackDate);
            txtFromDate.Text = twoYearBackDate.ToString("dd/MM/yyyy");
            txtToDate.Text = currentDate.ToString("dd/MM/yyyy");

            calFromDate.EndDate = DateTime.Now;
            calToDate.EndDate = DateTime.Now;
            if (this.PageType == "Contractor")
            {
                basePage.BindDropDown(ddlName, "Name", "ContractorId", contractorBL.ReadContractor(null));

            }
            else
            {
                basePage.BindDropDown(ddlName, "Name", "SupplierId", supplierBL.ReadSupplier(null));
                lblTitle.Text = "View Supplier Payment";
                lblName.Text = "Supplier Name";
                rbtnList.Items[0].Text = "Supplier Name";
                rbtnList.Items.RemoveAt(1);

            }
            if (lstPayment.Count > 0)
            {
                BindHeader();
            }
        }

        private void BindHeader()
        {
            Label lbl = (Label)gvPaymentDetail.HeaderRow.FindControl("lblHName");
            Label lblQuotation = (Label)gvPaymentDetail.HeaderRow.FindControl("lblHQuotation");
            if (this.PageType == "Contractor")
            {
                lbl.Text = "Contractor Name";
                lblQuotation.Text = "Contractor Work Order";
            }
            else
            {
                lbl.Text = "Supplier Name";
                lblQuotation.Text = "Supplier Purchase Order";
                gvPaymentDetail.Columns[4].Visible = false;
                gvPaymentDetail.Columns[5].Visible = false;


            }
        }

        protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rbtnList.SelectedValue == "1")
            {
                pnlName.Visible = true;
                pnlNumber.Visible = false;
                txtNumber.Text = string.Empty;

                BindEmptyGrid(gvPaymentDetail);
            }
            if (rbtnList.SelectedValue == "2")
            {
                pnlNumber.Visible = true;
                pnlName.Visible = false;
                lblNumber.Text = "Contract Number";

                txtNumber.Text = string.Empty;
                ddlName.SelectedValue = "0";
                txtToDate.Text = string.Empty;
                txtFromDate.Text = string.Empty;

                BindEmptyGrid(gvPaymentDetail);

                this.FromDate = DateTime.MinValue;
                this.ToDate = DateTime.MinValue;
            }
            if (rbtnList.SelectedValue == "3")
            {
                pnlNumber.Visible = true;
                pnlName.Visible = false;
                lblNumber.Text = "Invoice Number";

                txtNumber.Text = string.Empty;
                ddlName.SelectedValue = "0";
                txtToDate.Text = string.Empty;
                txtFromDate.Text = string.Empty;

                BindEmptyGrid(gvPaymentDetail);

                this.FromDate = DateTime.MinValue;
                this.ToDate = DateTime.MinValue;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String CSName = string.Empty;
            DateTime toDate = DateTime.MinValue;
            DateTime fromDate = DateTime.MinValue;
            String contractNo = String.Empty;
            String invoiceNo = String.Empty;



            if (rbtnList.SelectedValue == "1")
            {
                if (ddlName.SelectedIndex != 0)
                {
                    CSName = ddlName.SelectedItem.Text;
                }
                if (!String.IsNullOrEmpty(txtFromDate.Text))
                {
                    fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (!String.IsNullOrEmpty(txtToDate.Text))
                {
                    toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }

                if (!String.IsNullOrEmpty(txtFromDate.Text) && !String.IsNullOrEmpty(txtToDate.Text))
                {
                    if (toDate < fromDate)
                    {
                        basePage.Alert("To Date should be Greater Than From Date", btnSearch);
                        return;
                    }
                }
            }

            if (rbtnList.SelectedValue == "2")
            {
                if (!String.IsNullOrEmpty(txtNumber.Text))
                {
                    contractNo = txtNumber.Text.Trim();
                }

            }

            if (rbtnList.SelectedValue == "3")
            {
                if (!String.IsNullOrEmpty(txtNumber.Text))
                {
                    invoiceNo = txtNumber.Text.Trim();
                }

            }

            this.CSName = CSName;
            this.FromDate = fromDate;
            this.ToDate = toDate;
            this.ContractNo = contractNo;
            this.InvoiceNo = invoiceNo;

            lstPayment = new List<PaymentDOM>();

            if (this.PageType == "Contractor")
            {
                lstPayment = paymentBL.ReadPayment(CSName, fromDate, toDate, contractNo, invoiceNo, Convert.ToInt16(QuotationType.Contractor));
            }
            else
                lstPayment = paymentBL.ReadPayment(CSName, fromDate, toDate, contractNo, invoiceNo, Convert.ToInt16(QuotationType.Supplier));
            if (lstPayment.Count > 0)
            {
                gvPaymentDetail.DataSource = lstPayment;
                gvPaymentDetail.DataBind();
                BindHeader();
            }
            else
            {
                BindEmptyGrid(gvPaymentDetail);
                if (!string.IsNullOrEmpty(invoiceNo))
                {
                    basePage.Alert("Invoice Number Does Not Exist", LinkSearch);
                }
                else if (!string.IsNullOrEmpty(contractNo))
                {
                    basePage.Alert("Contract Number Does Not Exist", LinkSearch);
                }
            }

        }

        protected void gvPaymentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "lnkPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtnPrint");

                paymentId = Convert.ToInt32(e.CommandArgument);
                this.PaymentId = paymentId;
                if (this.PageType == "Contractor")
                {
                    //Response.Redirect("~/SSRReport/ContractorPayment.aspx?paymentId=" + paymentId);
                    basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/ContractorPayment.aspx?paymentId=" + paymentId + "", "IssueMaterial");
                }
                else
                {
                    //Response.Redirect("~/SSRReport/SupplierPayment.aspx?paymentId=" + paymentId);
                    basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/SupplierPayment.aspx?paymentId=" + paymentId + "", "IssueMaterial");
                }
            }
            /////////////////////////////////////
            if (e.CommandName == "lnkPrintAdvice")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lblAdvicePayment");

                paymentId = Convert.ToInt32(e.CommandArgument);
                this.PaymentId = paymentId;
                if (this.PageType == "Contractor")
                {
                    //Response.Redirect("~/SSRReport/PaymentAdvice.aspx?paymentId=" + paymentId);
                    basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/PaymentAdvice.aspx?paymentId=" + paymentId + "", "IssueMaterial");
                }
                else
                {
                    //Response.Redirect("~/SSRReport/SupplierPayment.aspx?paymentId=" + paymentId);
                    basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/SupplierPayment.aspx?paymentId=" + paymentId + "", "IssueMaterial");
                }
            }


            ////////////////////////////////////////////
            if (e.CommandName == "lnkInvoiceNo")
            {
                // -----------Sundeep---------
                String InvoiceNumber = e.CommandArgument.ToString();


                //-----------End--------------------
                //For Document
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);
                //End

                //invoiceNo = e.CommandArgument.ToString().Trim();
                //lstItemDetail = new List<ItemTransaction>();

                // -----------Sundeep---------
                lstPayment = new List<PaymentDOM>();
                lstPayment = paymentBL.ReadPayment(System.DateTime.MinValue, System.DateTime.MinValue, null, InvoiceNumber,null);
                gvInvoiceItems.DataSource = lstPayment;
                gvInvoiceItems.DataBind();

                //-----------End--------------------
                //if (this.PageType == "Contractor")
                //{
                //    lstItemDetail = invoiceBL.ReadInvoiceMapping(invoiceNo);
                //}
                //else
                //{
                //    lstItemDetail = invoiceBL.ReadInvoiceMapping(invoiceNo);
                //    //gvQuotationItems.Columns[0].Visible = false;
                //}
                //BindEmptyGrid(gvInvoiceItems);
                //if (lstItemDetail.Count > 0)
                //{
                //    gvInvoiceItems.DataSource = lstItemDetail;
                //    gvInvoiceItems.DataBind();

                //}

                ModalPopupExtender2.Show();
            }

            if (e.CommandName == "lnkEdit")
            {
                paymentId = Convert.ToInt32(e.CommandArgument);
                //this.PaymentId = paymentId;
                if (this.PageType == "Contractor")
                    Response.Redirect("~/Payment/ContractorPayment.aspx?paymentId=" + paymentId + "&" + "invoiceType=" + Convert.ToInt32(QuotationType.Contractor));
                else
                    Response.Redirect("~/Payment/SupplierPayment.aspx?paymentId=" + paymentId + "&" + "invoiceType=" + Convert.ToInt32(QuotationType.Supplier));
            }

            if (e.CommandName == "lnkDelete")
            {
                paymentId = Convert.ToInt32(e.CommandArgument);
                this.PaymentId = paymentId;
                String errorMessage = string.Empty;
                if (this.PageType == "Contractor")
                {
                    errorMessage = paymentBL.DeletePayment(paymentId, Convert.ToInt16(QuotationType.Contractor), basePage.LoggedInUser.UserLoginId, null);
                }
                else
                    errorMessage = paymentBL.DeletePayment(paymentId, Convert.ToInt16(QuotationType.Supplier), basePage.LoggedInUser.UserLoginId, null);
                if (errorMessage == "")
                {
                    //basePage.Alert("Payment Deleted Successfully", gvPaymentDetail);
                    lstPayment = new List<PaymentDOM>();
                    if (this.CSName == String.Empty && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
                    {
                        BindGrid(this.ToDate, this.FromDate);
                        //ViewState["FromDate"] = DateTime.MinValue;
                        //ViewState["ToDate"] = DateTime.MinValue;
                        this.FromDate = DateTime.MinValue;
                        this.ToDate = DateTime.MinValue;
                    }
                    else
                    {
                        if (this.PageType == "Contractor")
                        {
                            lstPayment = paymentBL.ReadPayment(this.CSName, this.ToDate, this.FromDate, this.ContractNo, this.InvoiceNo, Convert.ToInt16(QuotationType.Contractor));
                        }
                        else
                            lstPayment = paymentBL.ReadPayment(this.CSName, this.ToDate, this.FromDate, this.ContractNo, this.InvoiceNo, Convert.ToInt16(QuotationType.Contractor));
                        if (lstPayment.Count > 0)
                        {
                            gvPaymentDetail.DataSource = lstPayment;
                            gvPaymentDetail.DataBind();
                            ResetViewState();
                        }

                    }

                }
                else
                {
                    basePage.Alert(errorMessage, gvPaymentDetail);
                }

            }

            if (e.CommandName == "lnkGenerate")
            {
                paymentId = Convert.ToInt32(e.CommandArgument);
                int outId = 0;
                lstPayment = new List<PaymentDOM>();

                if (this.PageType == "Contractor")
                {

                    outId = paymentBL.UpdatePaymentStatus(paymentId, Convert.ToInt16(QuotationType.Contractor), Convert.ToInt16(StatusType.Generated), basePage.LoggedInUser.UserLoginId, null);
                }
                else
                    outId = paymentBL.UpdatePaymentStatus(paymentId, Convert.ToInt16(QuotationType.Supplier), Convert.ToInt16(StatusType.Generated), basePage.LoggedInUser.UserLoginId, null);
                if (outId > 0)
                {
                    // basePage.Alert("Payment Generated Successfully", gvPaymentDetail);

                    if (this.CSName == String.Empty && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
                    {
                        BindGrid(this.ToDate, this.FromDate);
                        //ViewState["FromDate"] = DateTime.MinValue;
                        //ViewState["ToDate"] = DateTime.MinValue;
                        this.FromDate = DateTime.MinValue;
                        this.ToDate = DateTime.MinValue;
                    }
                    else
                    {
                        if (this.PageType == "Contractor")
                        {
                            lstPayment = paymentBL.ReadPayment(this.CSName, this.ToDate, this.FromDate, this.ContractNo, this.InvoiceNo, Convert.ToInt16(QuotationType.Contractor));
                        }
                        else
                            lstPayment = paymentBL.ReadPayment(this.CSName, this.ToDate, this.FromDate, this.ContractNo, this.InvoiceNo, Convert.ToInt16(QuotationType.Contractor));
                        if (lstPayment.Count > 0)
                        {
                            gvPaymentDetail.DataSource = lstPayment;
                            gvPaymentDetail.DataBind();
                            ResetViewState();
                        }

                    }
                }
            }
        }

        protected void gvPaymentDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnDelet = (LinkButton)e.Row.FindControl("lbtnDelete");
                LinkButton lbtnGenerate = (LinkButton)e.Row.FindControl("lbtnGenerate");
                LinkButton lbtnPrint = (LinkButton)e.Row.FindControl("lbtnPrint");
                HiddenField hdfc = (HiddenField)e.Row.FindControl("hdf_documnent_Id");
                LinkButton lblAdviceprint = (LinkButton)e.Row.FindControl("lblAdvicePayment");
                int docId = Convert.ToInt32(hdfc.Value);
                int status = Convert.ToInt32(((HiddenField)e.Row.FindControl("approveStatus")).Value);
                if (Convert.ToInt32(StatusType.Approved) == status)
                {
                    if (docId > 0)
                    {
                        updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                        lbtnEdit.Visible = false;
                        lbtnGenerate.Visible = true;
                        lbtnDelet.Visible = false;
                    }
                    else
                    {
                        updcFile.GetDocumentData(Int32.MinValue);
                        lbtnEdit.Visible = true;
                        lbtnGenerate.Visible = false;
                        lbtnDelet.Visible = true;

                    }
                   // lbtnDelet.Visible = false;
                    //lbtnGenerate.Visible = true;
                    lbtnPrint.Visible = false;
                    lblAdviceprint.Visible = false;

                }

                if (Convert.ToInt32(StatusType.Rejected) == status)
                {
                    lbtnEdit.Visible = false;
                    lbtnDelet.Visible = false;
                    lbtnGenerate.Visible = false;
                    lbtnPrint.Visible = true;
                    lblAdviceprint.Visible = true;
                }
                if (Convert.ToInt32(StatusType.Generated) == status)
                {
                    lbtnPrint.Visible = true;
                    lbtnEdit.Visible = false;
                    lbtnDelet.Visible = true;
                    lbtnGenerate.Visible = false;
                    lblAdviceprint.Visible = true;

                }
                if (Convert.ToInt32(StatusType.Pending) == status)
                {
                    lbtnGenerate.Visible = false;
                    lbtnPrint.Visible = false;
                    lblAdviceprint.Visible = false;


                }

                Label lblHCName = ((Label)gvPaymentDetail.HeaderRow.Cells[2].FindControl("lblHName"));
                // Label CName = ((Label)e.Row.FindControl("lblCName"));
                // LinkButton lbtnQuotation = (LinkButton)e.Row.FindControl("lbtnQuotation");
                //QuotationDOM ob = (QuotationDOM)e.Row.DataItem;
                if (this.PageType == "Contractor")
                {
                    //lbtnQuotation.Text = ob.ContractQuotationNumber;
                    lblHCName.Text = "Contractor Name";
                    //CName.Text = ob.ContractorName;
                    //lblQuotationDate.Text = ob.QuotationDate.ToString("dd/MMM/yyyy");
                }
                else
                {
                    //lbtnQuotation.Text = ob.SupplierQuotationNumber;
                    lblHCName.Text = "Supplier Name";
                    //CName.Text = ob.SupplierName;
                    //lbtnQuotation.CommandArgument = ob.SupplierQuotationId.ToString();
                    //lblQuotationDate.Text = ob.OrderDate.ToString("dd/MMM/yyyy");
                    //lbtnEdit.CommandArgument = ob.SupplierQuotationId.ToString();
                    //lbtnDelet.CommandArgument = ob.SupplierQuotationId.ToString();
                    //lbtnGenerate.CommandArgument = ob.SupplierQuotationId.ToString();
                    //lbtnPrint.CommandArgument = ob.SupplierQuotationId.ToString() + "," + ob.SupplierQuotationNumber.ToString();

                }

            }

        }

        protected void gvPaymentDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPaymentDetail.PageIndex = e.NewPageIndex;

            if (this.CSName == string.Empty && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
            {
                BindGrid(this.ToDate, this.FromDate);
            }
            else
            {
                if (this.PageType == "Contractor")
                {
                    lstPayment = paymentBL.ReadPayment(this.CSName, this.ToDate, this.FromDate, this.ContractNo, this.InvoiceNo, Convert.ToInt16(QuotationType.Contractor));
                }
                else
                {
                    lstPayment = paymentBL.ReadPayment(this.CSName, this.ToDate, this.FromDate, this.ContractNo, this.InvoiceNo, Convert.ToInt16(QuotationType.Supplier));
                }
                if (lstPayment.Count > 0)
                {
                    gvPaymentDetail.DataSource = lstPayment;
                    gvPaymentDetail.DataBind();
                }
            }
        }

        #endregion

        #region Private Methods

        public void BindGrid(DateTime fromDate, DateTime toDate)
        {
            lstPayment = new List<PaymentDOM>();
            if (this.PageType == "Contractor")
            {
                lstPayment = paymentBL.ReadPayment(toDate, fromDate, Convert.ToInt16(QuotationType.Contractor), null,null);
                //gvPaymentDetail.Columns[3].Visible = false;
                //gvPaymentDetail.Columns[5].Visible = false;
            }
            else
            {
                lstPayment = paymentBL.ReadPayment(toDate, fromDate, Convert.ToInt16(QuotationType.Supplier), null,null);
            }
            if (lstPayment.Count > 0)
            {
                gvPaymentDetail.DataSource = lstPayment;
                gvPaymentDetail.DataBind();
                if (this.PageType == "Supplier")
                {
                    //gvPaymentDetail.Columns[3].Visible = false;
                }
            }
            else
            {
                BindEmptyGrid(gvPaymentDetail);

            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void ResetViewState()
        {

            this.PaymentId = 0;
            this.CSName = String.Empty;
            this.FromDate = DateTime.MinValue;
            this.ToDate = DateTime.MinValue;
            this.ContractNo = string.Empty;
            this.InvoiceNo = string.Empty;
        }

        #endregion

        #region Public Properties

        public Int32 PaymentId
        {
            get { return Convert.ToInt32(ViewState["PaymentId"]); }

            set { ViewState["PaymentId"] = value; }
        }

        public String CSName
        {
            get { return Convert.ToString(ViewState["CSName"]); }

            set { ViewState["CSName"] = value; }
        }

        public DateTime FromDate
        {
            get { return Convert.ToDateTime(ViewState["FromDate"]); }

            set { ViewState["FromDate"] = value; }
        }

        public DateTime ToDate
        {
            get { return Convert.ToDateTime(ViewState["ToDate"]); }

            set { ViewState["ToDate"] = value; }
        }

        public String ContractNo
        {
            get { return Convert.ToString(ViewState["ContractNo"]); }

            set { ViewState["ContractNo"] = value; }
        }

        public String InvoiceNo
        {
            get { return Convert.ToString(ViewState["InvoiceNo"]); }

            set { ViewState["InvoiceNo"] = value; }
        }

        public String PageType
        {
            get { return Convert.ToString(ViewState["PageType"]); }

            set { ViewState["PageType"] = value; }
        }

        #endregion
    }
}