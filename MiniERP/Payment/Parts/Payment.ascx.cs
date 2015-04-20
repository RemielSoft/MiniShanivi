using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DocumentObjectModel;
using BusinessAccessLayer.Quality;
using MiniERP.Shared;
using BusinessAccessLayer.Invoice;
using System.IO;
using System.Data;
using System.Configuration;

namespace MiniERP.Parts
{
    public partial class Payment : System.Web.UI.UserControl
    {
        #region Global Variable(s)

        bool flag;

        string pageName = String.Empty;

        BasePage basePage = new BasePage();

        String invoiceNo = String.Empty;

        ContractorBL contractorBL = new ContractorBL();
        SupplierBL supplierBL = new SupplierBL();
        ProjectBAL projectBL = new ProjectBAL();
        MetaDataBL metaDataBL = new MetaDataBL();
        PaymentBL paymentBL = new PaymentBL();
        SupplierRecieveMaterialBAL supplierRecieveMaterialBAL = new SupplierRecieveMaterialBAL();
        ContractorInvoiceBL invoiceBL = new ContractorInvoiceBL();

        SupplierInvoiceBL supplierInvoiveBL = null;

        List<PaymentDOM> lstPayment = null;
        List<InvoiceDom> lstInvoice = null;
        List<ItemTransaction> lstItemDetail = null;

        PaymentDOM payment = null;

        int id = 0;

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            if (!IsPostBack)
            {
                txtInvoiceNumber.Enabled = false;
                txtTDS.Text = "0.00";
                lblLeftAmount.Text = "0.00";
                txtPaymentDate.Enabled = false;
                DefaultLoad();
                SetRegularExpressions();

            }
        }

        protected void imgbtn_search_Click(object sender, EventArgs e)
        {
            uplfile.Visible = false;
            lstPayment = new List<PaymentDOM>();
            invoiceNo = txtInvoiceNumber.Text.Trim();
            if (invoiceNo == String.Empty)
            {
                basePage.Alert("Please Enter Invoice Number", imgbtnSearch);
            }
            else
            {
                if (IsContractor)
                {
                    paymentBL = new PaymentBL();
                    lstInvoice = new List<InvoiceDom>();
                    invoiceBL = new ContractorInvoiceBL();
                    lstPayment = new List<PaymentDOM>();
                    lstPayment = paymentBL.ReadPayment(String.Empty, DateTime.MinValue, DateTime.MinValue, String.Empty, invoiceNo, Convert.ToInt16(QuotationType.Contractor));
                    if (lstPayment.Count > 0)
                    {
                        if (lstPayment[0].PaymentLeftAmount == 0)
                        {
                            basePage.Alert("Payment for this Invoice is Already Submitted", imgbtnSearch);
                        }
                        //basePage.Alert("Payment for this Invoice submitted and now it is " + lstPayment[0].ApprovalStatusType.Name, imgbtnSearch);
                        // basePage.Alert("Payment for this Invoice is Already Submitted", imgbtnSearch);
                        else
                        {
                            lstInvoice = invoiceBL.ReadContractorInvoice(null, 0, DateTime.MinValue, DateTime.MinValue, invoiceNo, String.Empty);
                            if (lstInvoice.Count > 0)
                            {
                                if (lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
                                {
                                    panelVisiblity(true);
                                    hdfInvoiceNumber.Value = lstInvoice[0].InvoiceNumber;
                                    lblCName.Text = lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.ContractorName;
                                    lblContrctNumber.Text = lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.ContractNumber;
                                    lblWorkOrderNumber.Text = lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber;
                                    lblInvoiceDate.Text = lstInvoice[0].InvoiceDate.ToString("dd'/'MM'/'yyyy");
                                    lblRemark.Text = lstInvoice[0].Remarks;
                                    lblInvoiceAmount.Text = lstInvoice[0].PayableAmount.ToString();
                                    lblCOrderNumber.Text = lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber;
                                    lblLeftAmount.Text = lstPayment[0].PaymentLeftAmount.ToString();
                                    txtPaymentAmount.Text = lstPayment[0].PaymentLeftAmount.ToString();
                                    lblBill.Text = lstInvoice[0].BillNumber;
                                }
                            }
                        }
                    }
                    else
                    {
                        lstInvoice = invoiceBL.ReadContractorInvoice(null, 0, DateTime.MinValue, DateTime.MinValue, invoiceNo, String.Empty);
                        if (lstInvoice.Count > 0)
                        {
                            if (lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
                            {
                                panelVisiblity(true);
                                hdfInvoiceNumber.Value = lstInvoice[0].InvoiceNumber;
                                lblCName.Text = lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.ContractorName;
                                lblContrctNumber.Text = lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.ContractNumber;
                                lblWorkOrderNumber.Text = lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber;
                                lblInvoiceDate.Text = lstInvoice[0].InvoiceDate.ToString("dd'/'MM'/'yyyy");
                                lblRemark.Text = lstInvoice[0].Remarks;
                                lblInvoiceAmount.Text = lstInvoice[0].PayableAmount.ToString();
                                lblCOrderNumber.Text = lstInvoice[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber;
                                lblLeftAmount.Text = lstInvoice[0].PayableAmount.ToString();
                                txtPaymentAmount.Text = lstInvoice[0].PayableAmount.ToString();
                                lblBill.Text = lstInvoice[0].BillNumber;
                            }
                            else
                            {
                                basePage.Alert("Invoice is not Generated.", imgbtnSearch);
                            }
                        }
                        else
                        {
                            basePage.Alert("Please Enter Valid Invoice Number", imgbtnSearch);
                        }
                    }
                }

                // For Supplier
                else
                {
                    paymentBL = new PaymentBL();
                    supplierInvoiveBL = new SupplierInvoiceBL();
                    lstPayment = new List<PaymentDOM>();
                    lstPayment = paymentBL.ReadPayment(String.Empty, DateTime.MinValue, DateTime.MinValue, String.Empty, invoiceNo, Convert.ToInt16(QuotationType.Supplier));
                    if (lstPayment.Count > 0)
                    {
                        if (lstPayment[0].PaymentLeftAmount == 0)
                        {
                            basePage.Alert("Payment for this Invoce is submitted and now is " + lstPayment[0].ApprovalStatusType.Name, imgbtnSearch);
                        }
                        else
                        {
                            lstInvoice = supplierInvoiveBL.ReadSupplierInvoice(null, 0, DateTime.MinValue, DateTime.MinValue, invoiceNo, String.Empty);
                            if (lstInvoice.Count > 0)
                            {
                                if (lstInvoice[0].ReceiveMaterial.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
                                {
                                    panelVisiblity(true);
                                    hdfInvoiceNumber.Value = lstInvoice[0].InvoiceNumber;
                                    lblCName.Text = lstInvoice[0].ReceiveMaterial.Quotation.SupplierName;
                                    lblInvoiceDate.Text = lstInvoice[0].InvoiceDate.ToString("dd'/'MM'/'yyyy");
                                    lblRemark.Text = lstInvoice[0].Remarks;
                                    lblInvoiceAmount.Text = lstInvoice[0].PayableAmount.ToString();
                                    lblCOrderNumber.Text = lstInvoice[0].ReceiveMaterial.Quotation.SupplierQuotationNumber;
                                    lblLeftAmount.Text = lstPayment[0].PaymentLeftAmount.ToString();
                                    txtPaymentAmount.Text = lstPayment[0].PaymentLeftAmount.ToString();
                                    lblBill.Text = lstInvoice[0].BillNumber.ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        lstInvoice = new List<InvoiceDom>();
                        lstInvoice = supplierInvoiveBL.ReadSupplierInvoice(null, 0, DateTime.MinValue, DateTime.MinValue, invoiceNo, String.Empty);
                        if (lstInvoice.Count > 0)
                        {
                            if (lstInvoice[0].ReceiveMaterial.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
                            {
                                panelVisiblity(true);
                                hdfInvoiceNumber.Value = lstInvoice[0].InvoiceNumber;
                                lblCName.Text = lstInvoice[0].ReceiveMaterial.Quotation.SupplierName;
                                lblInvoiceDate.Text = lstInvoice[0].InvoiceDate.ToString("dd'/'MM'/'yyyy");
                                lblRemark.Text = lstInvoice[0].Remarks;
                                lblInvoiceAmount.Text = lstInvoice[0].PayableAmount.ToString();
                                lblCOrderNumber.Text = lstInvoice[0].ReceiveMaterial.Quotation.SupplierQuotationNumber;
                                txtPaymentAmount.Text = lstInvoice[0].PayableAmount.ToString();
                                lblLeftAmount.Text = lstInvoice[0].PayableAmount.ToString();
                                lblBill.Text = lstInvoice[0].BillNumber.ToString();
                            }
                            else
                            {
                                basePage.Alert("Invoice is not Generated.", imgbtnSearch);
                            }
                        }
                        else
                        {
                            basePage.Alert("Please Enter Valid Invoice Number", imgbtnSearch);
                        }
                    }
                }
            }

            //if (lstInvoice.Count > 0)
            //{
            //    gvInvoiceDetail.DataSource = lstInvoice;
            //    gvInvoiceDetail.DataBind();
            //}
            //else
            //{
            //    BindEmptyGrid(gvInvoiceDetail);
            //}
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            SetRegularExpressions();
            payment = GetFormData();
            if (payment != null)
            {
                //UploadFile(payment);
                if (this.PaymentId == 0)
                {
                    id = paymentBL.CreatePayment(payment);
                }
                else
                {
                    id = paymentBL.UpdatePayment(payment, this.PaymentId);
                }
                if (id > 0)
                {
                    CreateDocumentMapping();
                    if (this.PaymentId == 0)
                    {
                        if (IsContractor)
                            basePage.Alert("Payment Saved Successfully", btnSaveDraft, "ContractorPayment.aspx");
                        else
                            basePage.Alert("Payment Saved Successfully", btnSaveDraft, "SupplierPayment.aspx");
                    }
                    else
                    {
                        if (IsContractor)
                            basePage.Alert("Payment Updated Successfully", btnSaveDraft, "ViewContractorPayment.aspx");
                        else
                            basePage.Alert("Payment Updated Successfully", btnSaveDraft, "ViewSupplierPayment.aspx");
                    }

                    ClearFromData();
                }
                else
                {
                    basePage.Alert("Payment Already Created For This Invoice Number", btnSaveDraft);
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            EmptyData();

            if (IsContractor)
                Response.Redirect("ContractorPayment.aspx");
            else
                Response.Redirect("SupplierPayment.aspx");
        }

        protected void gvInvoiceDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int invoiceId = 0;
            invoiceId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "lnkInvoiceNo")
            {
                lstItemDetail = new List<ItemTransaction>();
                lstItemDetail = invoiceBL.ReadInvoiceMapping(invoiceId);
                if (lstItemDetail.Count > 0)
                {
                    //gvInvoiceItems.DataSource = lstItemDetail;
                    //gvInvoiceItems.DataBind();
                }
                else
                {
                    //BindEmptyGrid(gvInvoiceItems);
                }
                //ModalPopupExtender2.Show();
            }
        }

        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentMode.SelectedValue == Convert.ToInt32(PaymentMode.Cheque).ToString() || ddlPaymentMode.SelectedValue == Convert.ToInt32(PaymentMode.DD).ToString())
            {
                rfvReferenceNumber.ValidationGroup = "pd";
            }
            else
            {
                rfvReferenceNumber.ValidationGroup = "empty";
            }
        }
        // Open the popup Search button Invoice Number
        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            var contractorName = txtContractorName.Text;
        
            DateTime fromDate;
            string FDate = txtFromDate.Text;
            string TDate = txtToDate.Text;

            DateTime toDate;
            if (txtFromDate.Text=="")
            {
                fromDate = DateTime.MinValue;
            }
            else
            {
                fromDate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
               // fromDate = Convert.ToDateTime(txtFromDate.Text);
            }
            if (txtToDate.Text == "")
            {
                toDate = DateTime.MinValue;
            }
            else
            {
                toDate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
               // toDate = Convert.ToDateTime(txtToDate.Text);
            }

            BindGrid(null, null, fromDate, toDate, null, null, contractorName);
            ClearFromData();
            ModalPopupExtender2.Show();
        }
        #endregion
        protected void rbtSelect_OncheckChanged(object sender, System.EventArgs e)
        {
            foreach (GridViewRow oldRow in gvInvoice.Rows)
            {
                ((RadioButton)oldRow.FindControl("rbtSelect")).Checked = false;
            }
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtSelect")).Checked = true;
            Label InvoiceNo = (Label)row.FindControl("lnkbtnInvoice");
            txtInvoiceNumber.Text = InvoiceNo.Text.ToString();

        }



        private void BindGrid(Int32? InvoiceId, Int32? contractorId, DateTime fromDate, DateTime toDate, String InvoiceNo, String ContractorWorkOrderNo, string contractorName)
        {
            ContractorInvoiceBL contractorInvoiceBL = new ContractorInvoiceBL();
            lstInvoice = new List<InvoiceDom>();
            //for pyament case 

            lstPayment = new List<PaymentDOM>();
            lstPayment = paymentBL.ReadPayment(fromDate, toDate, null, InvoiceNo, contractorName);
          
          
            List<int> arrInvoice = new List<int>();
            foreach (PaymentDOM item in lstPayment)
            {
                item.PaymentStatus = new MetaData();
                arrInvoice.Add(item.PaymentStatus.Id);
            }
          

            if (IsContractor)
            {
                gvInvoice.Columns[4].Visible = false;
                gvInvoice.Columns[5].Visible = false;
                lstInvoice = contractorInvoiceBL.ReadApprovedContractorInvoice(null, contractorId, toDate, fromDate, InvoiceNo, ContractorWorkOrderNo, contractorName);
  //
                //var all = lstInvoice.Where(b => lstPayment.Any(a => a.PaymentAmount > b.PayableAmount));

                //var lst = lstInvoice.Where(e => e.ReceiveMaterial.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Approved)));
                //var all = listB.Where(b => listA.Any(a => a.code == b.code));


                var allpayment = lstInvoice.Where(m =>!arrInvoice.Contains(m.PaymentDom.Paymentstatus.Id=2));


                gvInvoice.DataSource = allpayment;
                gvInvoice.DataBind();

            }
            else
            {
                gvInvoice.Columns[2].Visible = false;
                gvInvoice.Columns[3].Visible = false;
                supplierInvoiveBL = new SupplierInvoiceBL();
                lstInvoice = supplierInvoiveBL.ReadApprovedSupplierInvoice(null, contractorId, toDate, fromDate, InvoiceNo, ContractorWorkOrderNo, contractorName);
                
               // var lst = lstInvoice.Where(e => e.ReceiveMaterial.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Approved)));
                //var allpayment = lstInvoice.Where(m => !arrInvoice.Contains(m.InvoiceNumber));
                var allpayment = lstInvoice.Where(m => !arrInvoice.Contains(m.PaymentDom.Paymentstatus.Id = 2));

                gvInvoice.DataSource = allpayment;
                gvInvoice.DataBind();
            }
        }

        #region Private Methods

        private void DefaultLoad()
        {
            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(btn_upload);
            //*************For Document Only
            EmptyDocumentList();
            BindDocument();
            //***********END
            SetRegularExpressions();
            txtPaymentDate.Text = DateTime.Now.Date.ToString("dd'/'MM'/'yyyy");

            basePage.BindDropDown(ddlPaymentMode, "Name", "Id", metaDataBL.ReadMetaDataPaymentMode());
            basePage.BindDropDown(ddlPaymentStatus, "Name", "Id", metaDataBL.ReadMetaDataPaymentStatus());

            Int32 paymentId = Convert.ToInt32(Request.QueryString["paymentId"]);
            Int32 invoiceType = Convert.ToInt32(Request.QueryString["invoiceType"]);
            this.PaymentId = paymentId;


          
            ControlsVisibility();
            panelVisiblity(false);

            if (paymentId != 0 && invoiceType != 0)
            {
                DisableTextApproval();
                panelVisiblity(true);
                if (invoiceType == 1)
                {
                    lstPayment = new List<PaymentDOM>();
                    lstPayment = paymentBL.ReadPayment(paymentId);
                    PopulateFormControls(lstPayment[0]);
                    for (int i = 0; i < lstPayment.Count; i++)
                    {
                        if (lstPayment[i].ApprovalStatusType.Id == 1)
                        {
                            uplfile.Visible = false;
                        }
                        else if (lstPayment[i].ApprovalStatusType.Id == 3)
                        {
                            uplfile.Visible = true;
                        }
                    }
                }
                else
                {
                    lstPayment = new List<PaymentDOM>();
                    lstPayment = paymentBL.ReadPayment(paymentId);
                    PopulateFormControls(lstPayment[0]);
                    for (int i = 0; i < lstPayment.Count; i++)
                    {
                        if (lstPayment[i].ApprovalStatusType.Id == 1)
                        {
                            uplfile.Visible = false;
                        }
                        else if (lstPayment[i].ApprovalStatusType.Id == 3)
                        {
                            uplfile.Visible = true;
                        }
                    }
                }
            }


        }

        private void SetRegularExpressions()
        {
            revtxtTDS.ValidationExpression = ValidationExpression.C_NUMERIC;
            revtxtTDS.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_NUMERIC;
        }

        private void ControlsVisibility()
        {

            if (IsContractor)
            {
                lblTitle.Text = "Contractor Payment";
                lblName.Text = "Contractor Name";
                lblOrderNumber.Text = "Contractor Work Order";
                //lblSubTitle.Text = "Contractor Invoice";
            }
            else
            {
                lblTitle.Text = "Supplier Payment";
                lblName.Text = "Supplier Name";
                lblOrderNumber.Text = "Supplier Purchase Order";
                lblContractor.Text = "Supplier Name";
                // lblSubTitle.Text = "Supplier Invoice";

                trContractNumber.Visible = false;
                trWorkOrderNumber.Visible = false;
            }
        }

        private void panelVisiblity(Boolean condition)
        {
            pnl_main.Visible = condition;
            pnl_payment.Visible = condition;
            btnPart.Visible = condition;
        }

        private PaymentDOM GetFormData()
        {

            payment = new PaymentDOM();
            payment.InvoiceNumber = hdfInvoiceNumber.Value;
            payment.InvoiceDate = DateTime.ParseExact(lblInvoiceDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            payment.InvoiceRemarks = lblRemark.Text;

            payment.ContractorSupplierName = lblCName.Text;
            payment.QuotationNumber = lblCOrderNumber.Text;

            if (IsContractor)
            {
                payment.ContractNumber = lblContrctNumber.Text;
                payment.WorkOrderNumber = lblWorkOrderNumber.Text;
            }

           
            payment.PaymentModeType = new MetaData();
            payment.Paymentstatus = new MetaData();
            payment.PaymentLeftAmount = Convert.ToDecimal(lblLeftAmount.Text);
            payment.PaymentModeType.Id = Convert.ToInt32(ddlPaymentMode.SelectedValue);
            payment.Paymentstatus.Id = Convert.ToInt32(ddlPaymentStatus.SelectedValue);
            payment.PaymentAmount = Convert.ToDecimal(lblInvoiceAmount.Text);
            payment.PaymentDate = DateTime.ParseExact(txtPaymentDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            payment.UploadedDocument = basePage.DocumentStackId;
            payment.Remark = txtRemark.Text.Trim();
            payment.BankName = txtBankName.Text.Trim();
            payment.ReferenceNumber = txtReferenceNo.Text.Trim();
            payment.TDS = Convert.ToDecimal(txtTDS.Text.Trim());
            payment.BillNumber = lblBill.Text;
            payment.TDSWithPayment = Convert.ToDecimal(txtPaymentAmount.Text.Trim());
           
             
            payment.InvoiceType = new MetaData();

            if (IsContractor)
                payment.InvoiceType.Id = Convert.ToInt32(QuotationType.Contractor);
            else
                payment.InvoiceType.Id = Convert.ToInt32(QuotationType.Supplier);

            payment.CreatedBy = basePage.LoggedInUser.UserLoginId;

            if (payment.Paymentstatus.Id==2)
            {
                return payment;
            }
            else if (payment.TDSWithPayment>payment.PaymentLeftAmount)
            {
                basePage.Alert("Payment amount should be equal or less then left amount",btnSaveDraft);
                return null;
                
            }
            
            else
            {
                return payment;
            }
        }

        public void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void DisableTextApproval()
        {
            ddlPaymentMode.Enabled = false;
            ddlPaymentStatus.Enabled = false;
            txtPaymentAmount.Enabled = false;
            txtTDS.Enabled = false;
            txtBankName.Enabled = false;
            txtReferenceNo.Enabled = false;

        }

        //private void DisableTextApproval()
        //{
        //    ddlPaymentMode.Enabled = false;
        //    txtPaymentAmount.Enabled = false;
        //    txtTDS.Enabled = false;
        //    txtBankName.Enabled = false;
        //    txtReferenceNo.Enabled = false;

        //}


        private void ClearFromData()
        {
            txtInvoiceNumber.Text = string.Empty;
            lblCName.Text = string.Empty;
            lblContrctNumber.Text = string.Empty;
            //txtPaymentDate.Text = string.Empty;
            txtRemark.Text = string.Empty;
            ddlPaymentMode.SelectedValue = "0";
            txtPaymentAmount.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtReferenceNo.Text = string.Empty;
            this.PaymentId = 0;
            btnSaveDraft.Text = "Save Draft";
            btnaddPayment.Text = "Add";
            txtTDS.Text = string.Empty;
            ddlPaymentStatus.SelectedIndex = 0;
            txtContractorName.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

            //BindEmptyGrid(gvInvoiceDetail);
        }

        private void GetUploadedFiles(out String filesName, out HttpPostedFile[] HPF)
        {
            int j = 0;
            filesName = string.Empty;
            HPF = new HttpPostedFile[20];
            try
            {
                // Get the HttpFileCollection 
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (!string.IsNullOrEmpty(hpf.FileName))
                    {
                        //hpf.SaveAs(Server.MapPath("Images") + "\\" +
                        //  Path.GetFileName(hpf.FileName));
                        filesName = filesName + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "/" + Path.GetFileName(hpf.FileName) + ",";
                        HPF[j] = hpf;
                        j++;
                    }
                }
                filesName = filesName.TrimEnd(',');
            }
            catch (Exception ex)
            {
                basePage.Alert(ex.Message, btnSaveDraft);
            }

        }



        private void PopulateFormControls(PaymentDOM payment)
        {
            if (payment != null)
            {                
                lstInvoice = new List<InvoiceDom>();
                lstInvoice = invoiceBL.ReadInvoice(payment.InvoiceNumber);

                if (lstInvoice.Count > 0)
                {
                    //gvInvoiceDetail.DataSource = lstInvoice;
                    //gvInvoiceDetail.DataBind();
                }
                else
                {
                    //BindEmptyGrid(gvInvoiceDetail);
                }
                txtInvoiceNumber.Text = payment.InvoiceNumber;
                txtInvoiceNumber.Enabled = false;
                lblCOrderNumber.Text = payment.QuotationNumber;
                lblInvoiceAmount.Text = payment.PaymentAmount.ToString();
                lblLeftAmount.Text = payment.PaymentLeftAmount.ToString();
                lblInvoiceDate.Text = payment.InvoiceDate.ToString("dd/MM/yyyy");
                lblRemark.Text = payment.InvoiceRemarks;
                lblBill.Text = payment.BillNumber;
                basePage.DocumentStackId = payment.UploadedDocument;
                lblCName.Text = payment.ContractorSupplierName;
                lblContrctNumber.Text = payment.ContractNumber;
                lblWorkOrderNumber.Text = payment.WorkOrderNumber;
                txtPaymentDate.Text = payment.PaymentDate.ToString("dd/MM/yyyy");
                txtRemark.Text = payment.Remark;
                txtTDS.Text = payment.TDS.ToString();
                ddlPaymentMode.SelectedValue = payment.PaymentModeType.Id.ToString();
                ddlPaymentStatus.SelectedValue = payment.Paymentstatus.Id.ToString();
                txtPaymentAmount.Text = payment.TDSWithPayment.ToString();
                txtBankName.Text = payment.BankName;
                txtReferenceNo.Text = payment.ReferenceNumber;
                btnSaveDraft.Text = "Update Draft";
                
                GetDocumentData(payment.UploadedDocument);
            }

        }

        private void EmptyData()
        {
            ddlPaymentMode.SelectedIndex = 0;
            ddlPaymentStatus.SelectedIndex = 0;
            txtPaymentAmount.Text = string.Empty;
            txtReferenceNo.Text = String.Empty;
            txtPaymentDate.Text = DateTime.Now.Date.ToString("dd'/'MM'/'yyyy");
            EmptyDocumentList();
            BindDocument();
            txtRemark.Text = String.Empty;
            txtBankName.Text = String.Empty;
            txtInvoiceNumber.Text = String.Empty;
        }

        #endregion

        #region Public Properties

        public String PageType
        {
            get { return Convert.ToString(ViewState["PageType"]); }

            set { ViewState["PageType"] = value; }
        }

        public Boolean IsContractor
        {
            get
            {
                if (this.PageType == "Contractor")
                {
                    flag = true;
                    return flag;
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }

            set
            {
                flag = value;
            }
        }

        public Int32 PaymentId
        {
            get { return Convert.ToInt32(ViewState["PaymentId"]); }

            set { ViewState["PaymentId"] = value; }
        }

        #endregion

        #region Upload Document Code

        #region Private Global Variable(s)

        DocumentBL document_BL = new DocumentBL();

        Document document = null;
        Int32 Year = 0;
        Int32 Index = 0;

        String Head_Folder_Path = String.Empty;
        String Sub_Folder_Path = String.Empty;
        String File_Extension = String.Empty;
        String File_Path = String.Empty;

        DataSet page_Data = null;

        List<Document> lst_document = null;
        #endregion

        #region Protected Methods

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            //For Copy Quotation, It is TRUE
            LoadData(false);
        }

        private void LoadData(bool forCopy)
        {
            LoadDocument(forCopy);
            DirectoryHandle(FileUpload_Control);
            BindDocument();
        }

        private void LoadDocument(bool forCopy)
        {
            ManageSession(forCopy);

            if (basePage.DocumentStackId == 0)
            {
                CreateAndReadDocumentStackId();
            }

            BindDocument();
        }

        protected void gv_documents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "FileDelete")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                lst_document = new List<Document>();
                lst_document = basePage.DocumentsList;
                lst_document.RemoveAt(Index);
                basePage.DocumentsList = lst_document;
                BindDocument();
            }
            else if (e.CommandName == "OpenFile")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                Index = Convert.ToInt32(e.CommandArgument);
                lst_document = new List<Document>();
                lst_document = basePage.DocumentsList;

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtn_file");

                string fileName = lst_document[Index].Replaced_Name;
                string strURL = lst_document[Index].Path + @"\" + lst_document[Index].Replaced_Name;

                Session["FilePath"] = Server.MapPath(strURL);
                Session["OriginalFileName"] = lst_document[Index].Original_Name;
                Session["ReplacedFileName"] = lst_document[Index].Replaced_Name;
                basePage.OpenPopupWithUpdatePanelForFileDownload(lbtn, "../Parts/FileDownload.aspx?id=" + "File", "DownloadFile");
            }
        }

        #endregion

        #region Private Methods

        private void ManageSession(bool forCopy)
        {
            RequestPageName = pageName;
            if (forCopy)
            {
                basePage.DocumentStackId = 0;
            }
            else if (basePage.Page_Name == null || basePage.Page_Name != RequestPageName)
            {
                basePage.Page_Name = RequestPageName;
                basePage.DocumentStackId = 0;
                basePage.DocumentSerial = 0;
                basePage.DocumentsList = null;
            }
            else
            {
                //GO AHEAD
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Int32 CreateAndReadDocumentStackId()
        {
            document = new Document();
            document.CreatedBy = basePage.LoggedInUser.UserLoginId;
            basePage.DocumentStackId = document_BL.CreateAndReadDocumnetStackId(document);
            return basePage.DocumentStackId;
        }

        private void DirectoryHandle(FileUpload fileupload)
        {
            if (fileupload.HasFile)
            {
                if (fileupload.FileContent.Length > 10485760)
                {
                    basePage.Alert("You can upload up to 10 megabytes (MB) in size at a time", FileUpload_Control);
                }
                else
                {
                    Year = DateTime.Now.Year;

                    //Get list of pages
                    page_Data = new DataSet();
                    page_Data.ReadXml(Server.MapPath(ConfigurationManager.AppSettings["PageDictionary_Path"].ToString()));

                    Head_Folder_Path = Server.MapPath(@"\" + ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString());
                    //Check to existance of Main Folder
                    if (!Directory.Exists(Head_Folder_Path))
                    {
                        Directory.CreateDirectory(Head_Folder_Path);
                    }
                    //For Check to existance of Sub-Folders and if not, then create
                    foreach (DataRow dr in page_Data.Tables[0].Rows)
                    {
                        Sub_Folder_Path = Head_Folder_Path + @"\" + dr["Page"].ToString();
                        if (!Directory.Exists(Sub_Folder_Path))
                        {
                            Directory.CreateDirectory(Sub_Folder_Path);
                        }
                    }
                    //If folder exist then Upload Document in respective folder
                    Sub_Folder_Path = Head_Folder_Path + @"\" + RequestPageName;

                    if (Directory.Exists(Sub_Folder_Path))
                    {
                        if (basePage.DocumentStackId != 0)
                        {
                            document = new Document();
                            lst_document = new List<Document>();
                            flag = false;

                            document.Original_Name = fileupload.FileName.Split('\\').Last();
                            if (basePage.DocumentsList != null)
                            {
                                foreach (Document item in basePage.DocumentsList)
                                {
                                    if (item.Original_Name == document.Original_Name)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (flag == true)
                            {
                                basePage.Alert(GlobalConstants.M_File_Exist, FileUpload_Control);
                            }
                            else
                            {
                                basePage.DocumentSerial = basePage.DocumentSerial + 1;

                                File_Extension = System.IO.Path.GetExtension(fileupload.FileName.Split('\\').Last());
                                document.Replaced_Name = Convert.ToString(basePage.DocumentStackId) + "_" + Convert.ToString(basePage.DocumentSerial) + File_Extension;

                                File_Path = Sub_Folder_Path + @"\" + document.Replaced_Name;
                                //File_Path = Sub_Folder_Path + @"\" + document.Original_Name;
                                //Upload file in respective path
                                FileUpload_Control.SaveAs(File_Path);

                                document.DocumentId = basePage.DocumentStackId;

                                document.Path = ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString() + @"\" + RequestPageName;
                                document.LastIndex = basePage.DocumentSerial;


                                if (basePage.DocumentsList == null)
                                {
                                    lst_document.Add(document);
                                }
                                else
                                {
                                    lst_document = basePage.DocumentsList;
                                    lst_document.Add(document);
                                }

                                //Add Doc's info in list
                                basePage.DocumentsList = lst_document;
                            }
                        }
                    }
                }
            }
        }

        public void BindDocument()
        {
            if (basePage.DocumentsList != null)
            {
                gv_documents.DataSource = basePage.DocumentsList;
            }
            else
            {
                gv_documents.DataSource = null;
            }
            gv_documents.DataBind();
        }

        private void CreateDocumentMapping()
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = basePage.DocumentsList;

            if (lst_document != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                document_BL.ResetDocumentMapping(Convert.ToInt32(basePage.DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document doc in lst_document)
                {
                    document = new Document();
                    document.DocumentId = Convert.ToInt32(basePage.DocumentStackId);
                    document.Original_Name = doc.Original_Name;
                    document.Replaced_Name = doc.Replaced_Name;
                    document.Path = doc.Path;
                    //DocumentSerial is the last updated document
                    document.LastIndex = basePage.DocumentSerial;
                    document.CreatedBy = basePage.LoggedInUser.UserLoginId;
                    document.Id = doc.Id;
                    document_BL.CreateDocumentMapping(document);
                }
            }
        }

        private void GetDocumentData(Int32 DocumentId)
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = document_BL.ReadDocumnetMapping(DocumentId);
            if (lst_document.Count >= 1)
            {
                basePage.DocumentsList = lst_document;
                basePage.DocumentStackId = lst_document[0].DocumentId;

                basePage.DocumentSerial = lst_document[0].LastIndex;
                basePage.Page_Name = pageName;
                BindDocument();
            }
        }

        public void EmptyDocumentList()
        {
            basePage.DocumentStackId = 0;
            basePage.DocumentSerial = 0;
            basePage.DocumentsList = null;
            BindDocument();
        }
        #endregion

        #region Public Properties

        public String RequestPageName
        {
            get
            {
                return (String)ViewState["Page"];
            }
            set
            {
                ViewState["Page"] = value;
            }
        }
        #endregion

        protected void BtnClose_Click(object sender, EventArgs e)
        {

        }


        #endregion

        decimal TotalAnount = 0;

        protected void btnaddPayment_Click(object sender, EventArgs e)
        {
            decimal x = ((Convert.ToDecimal(txtPaymentAmount.Text)) * (Convert.ToDecimal(txtTDS.Text)) / 100);

            TotalAnount = ((Convert.ToDecimal(txtPaymentAmount.Text)) - x);
            txtPaymentAmount.Text = TotalAnount.ToString("F2");
        }

        decimal GrandTotal = 0;
        protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GrandTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PayableAmount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = GrandTotal.ToString();
                TotalAmount = GrandTotal;

            }

        }


        private Decimal TotalAmount
        {
            get
            {
                if (ViewState["PayableAmount"] == null)
                    return 0;
                else
                    return (Decimal)ViewState["PayableAmount"];
            }
            set
            {
                ViewState["PayableAmount"] = value;
            }
        }



    }

}