using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniERP.Shared;
using DocumentObjectModel;
using BusinessAccessLayer;
using BusinessAccessLayer.Invoice;


namespace MiniERP.Parts
{
    public partial class PaymentApproval : System.Web.UI.UserControl
    {
        #region Private Global Variables

        Int32 id = 0;
        bool track;
        BasePage basePage = new BasePage();

        QuotationBL quotationBL = new QuotationBL();
        PaymentBL paymentBL = new PaymentBL();
        ContractorInvoiceBL invoiceBL = new ContractorInvoiceBL();

        List<PaymentDOM> lstPayment = null;
        List<ItemTransaction> lstItemDetail = null;
       
        PaymentDOM paymentDOM = new PaymentDOM();
        
        

        

        CheckBox chbx = null;
        HiddenField hdf = null;
        TextBox txtRemarkReject = null;

        #endregion

        #region Protected Events

        protected void Page_Load(object sender, EventArgs e)
        {
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
            var list = quotationBL.ReadQuotationStatusMetaData().Where(p => p.Name != (StatusType.InComplete).ToString());
            basePage.BindDropDownData(ddlStatus, "Name", "Id", list);
            ModalPopupExtender2.Hide();
            if (this.PageType == "Contractor")
            {

                lbl_quotaion_approval.Text = "Contractor Payment Approval";
                BindGrid(Convert.ToInt32(StatusType.Pending), Convert.ToInt32(QuotationType.Contractor));
                BindHeader();
            }

            else
            {
                lbl_quotaion_approval.Text = "Supplier Payment Approval";
                BindGrid(Convert.ToInt32(StatusType.Pending), Convert.ToInt32(QuotationType.Supplier));
                BindHeader();
            }
        }

        private void BindHeader()
        {
            if (gvInvoiceDetail.Rows.Count != 0)
            {

                Label lbl = (Label)gvInvoiceDetail.HeaderRow.FindControl("lblHName");
                Label lblQuotation = (Label)gvInvoiceDetail.HeaderRow.FindControl("lblHQuotation");
                if (this.PageType == "Contractor")
                {
                    lbl.Text = "Contractor Name";
                    lblQuotation.Text = "Contractor Work Order";
                }
                else
                {
                    lbl.Text = "Supplier Name";
                    lblQuotation.Text = "Supplier Purchase Order";
                    gvInvoiceDetail.Columns[5].Visible = false;
                    gvInvoiceDetail.Columns[6].Visible = false;
                }
            }
            if (ddlStatus.SelectedValue != Convert.ToInt32(StatusType.Pending).ToString())
            {
                ViewControls(false);
            }
            else
            {
                ViewControls(true);
            }
        }

        private void ViewControls(Boolean condition)
        {
            gvInvoiceDetail.Columns[0].Visible = condition;
            btnApprove.Visible = condition;
            btnReject.Visible = condition;
        }


        protected void ddlStatusSelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 statusId = Convert.ToInt32(ddlStatus.SelectedValue);
            if (this.PageType == "Contractor")
            {
                BindGrid(statusId, Convert.ToInt32(QuotationType.Contractor));
                BindHeader();
            }
            else
            {
                BindGrid(statusId, Convert.ToInt32(QuotationType.Supplier));
                BindHeader();

            }
        }

        protected void gvInvoiceDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String invoiceNo = String.Empty;

          //  string contractorName = string.Empty;
            invoiceNo = e.CommandArgument.ToString();
            if (e.CommandName == "lnkInvoiceNo")
            {

                String InvoiceNumber = e.CommandArgument.ToString();
                //For Document
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);
                //End



                // -----------Sundeep---------
                lstPayment = new List<PaymentDOM>();
                lstPayment = paymentBL.ReadPayment(System.DateTime.MinValue, System.DateTime.MinValue, null, InvoiceNumber,null);
                gvInvoiceDetailPopup.DataSource = lstPayment;
                gvInvoiceDetailPopup.DataBind();

                //-----------End--------------------

                //lstItemDetail = new List<ItemTransaction>();
                //lstItemDetail = invoiceBL.ReadInvoiceMapping(invoiceNo);
                //if (lstItemDetail.Count > 0)
                //{
                //    gvInvoiceItems.DataSource = lstItemDetail;
                //    gvInvoiceItems.DataBind();
                //}
                //else
                //{
                //    BindEmptyGrid(gvInvoiceItems);
                //}
                ModalPopupExtender2.Show();
            }
        }

        protected void btnApproveReject_Click(object sender, CommandEventArgs e)
        {

            GetSelectedData(e.CommandName);
            if (this.PageType == "Contractor")
            {
                BindGrid(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(QuotationType.Contractor));
            }
            else
            {
                BindGrid(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(QuotationType.Supplier));
            }

            //if (e.CommandName == "Approve")
            //    basePage.Alert("Successfully Approved", btnApprove);
            //else
            //    basePage.Alert("Successfully Rejected", btnReject);

        }

        protected void chbxSelectAll(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvInvoiceDetail.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbxPayment");
                hdf = (HiddenField)row.FindControl("hdfPaymentId");
                if (chbx != null && hdf != null)
                {
                    chbx.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void chbxSelect(object sender, EventArgs e)
        {
            track = false;
            CheckBox chb = (CheckBox)gvInvoiceDetail.HeaderRow.FindControl("chbxSelectAll");
            foreach (GridViewRow row in gvInvoiceDetail.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbxPayment");
                if (!chbx.Checked)
                    track = true;
            }
            if (track == true)
            {
                chb.Checked = false;
            }
            else
            {
                chb.Checked = true;
            }
        }

        protected void gvInvoiceDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInvoiceDetail.PageIndex = e.NewPageIndex;
            if (this.PageType == "Contractor")
            {
                BindGrid(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(QuotationType.Contractor));
            }
            else
            {
                BindGrid(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(QuotationType.Supplier));
            }
        }

        #endregion

        #region Private Methods

        private void BindGrid(Int32 statusId, Int32 invoiceType)
        {
            lstPayment = new List<PaymentDOM>();
            lstPayment = paymentBL.ReadPaymentByStatusId(statusId, invoiceType);
            if (lstPayment.Count > 0)
            {
                gvInvoiceDetail.DataSource = lstPayment;
                gvInvoiceDetail.DataBind();
            }
            else
            {
                BindEmptyGrid(gvInvoiceDetail);
            }

        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void GetSelectedData(String action)
        {
            Int32 paymentId = 0;
            Int32 statusId = 0;
            if (Convert.ToInt32(ddlStatus.SelectedValue) == (Convert.ToInt32(StatusType.Pending)))
            {
                foreach (GridViewRow row in gvInvoiceDetail.Rows)
                {
                    chbx = (CheckBox)row.FindControl("chbxPayment");
                    hdf = (HiddenField)row.FindControl("hdfPaymentId");
                    txtRemarkReject = (TextBox)row.FindControl("txtRemarkReject");

                    if (chbx != null && hdf != null)
                    {
                        if (chbx.Checked.Equals(true))
                        {
                            paymentId = Convert.ToInt32(hdf.Value);
                            paymentDOM.RemarkReject = txtRemarkReject.Text.ToString();


                            if (action == "Approve")
                                statusId = Convert.ToInt32(StatusType.Approved);
                            else if (action == "Reject")
                                statusId = Convert.ToInt32(StatusType.InComplete);
                            if (this.PageType == "Contractor")
                            {
                                paymentBL.UpdatePaymentStatus(paymentId, Convert.ToInt16(QuotationType.Contractor), statusId, basePage.LoggedInUser.UserLoginId, paymentDOM.RemarkReject);
                            }
                            else
                            {
                                paymentBL.UpdatePaymentStatus(paymentId, Convert.ToInt16(QuotationType.Supplier), statusId, basePage.LoggedInUser.UserLoginId, paymentDOM.RemarkReject);
                            }

                        }
                    }
                }
            }
            if (paymentId == 0)
            {
                basePage.Alert("Kindly Select any Invoice Number.", btnApprove);
            }
        }

        #endregion

        #region Public Properties

        public String PageType
        {
            get { return Convert.ToString(ViewState["PageType"]); }

            set { ViewState["PageType"] = value; }
        }

        #endregion



    }
}