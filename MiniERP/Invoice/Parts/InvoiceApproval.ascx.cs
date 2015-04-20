using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using BusinessAccessLayer.Invoice;
using MiniERP.Shared;
using System.Configuration;

namespace MiniERP.Invoice.Parts
{
    public partial class InvoiceApproval : System.Web.UI.UserControl
    {
        #region Private Global Variables
        bool track;
        Int32 id = 0;
        InvoiceDom Invoice = null;
        List<InvoiceDom> lstInvoice = null;
        ContractorInvoiceBL contractorInvoiceBL = null;
        List<ItemTransaction> lstItemtransaction = null;
        TextBox txtRemarkReject = null;

        QuotationBL quotationBL = new QuotationBL();
        BasePage basePage = new BasePage();
        #endregion

        #region Protected Section

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                basePage.BindDropDownData(ddlStatus, "Name", "Id", quotationBL.ReadQuotationStatusMetaData());
                BindInvoice(Convert.ToInt32(StatusType.Pending));
                ModalPopupExtender2.Hide();
            }

        }

        private void PageDefaults()
        {
            basePage.Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void oncheck_uncheck_all(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvInvoice.Rows)
            {
                CheckBox chbx = (CheckBox)row.FindControl("chbx_Invoice");
                HiddenField hdf = (HiddenField)row.FindControl("hdf_Invoice_id");
                if (chbx != null && hdf != null)
                {
                    chbx.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void on_check_uncheck_quotation(object sender, EventArgs e)
        {
            bool track = false;
            CheckBox chb = (CheckBox)gvInvoice.HeaderRow.FindControl("chbx_select_all");
            foreach (GridViewRow row in gvInvoice.Rows)
            {
                CheckBox chbx = (CheckBox)row.FindControl("chbx_Invoice");
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

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = Convert.ToInt32(ddlStatus.SelectedValue);
            ViewState["StatusId"] = id;
            BindInvoice(id);
            ButtonShow(id);
        }

        protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInvoice.PageIndex = e.NewPageIndex;
            BindInvoice(Convert.ToInt32(ViewState["StatusId"]));
        }

        protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //Check the condition Normal and advance case
            //if (e.Row.RowType==DataControlRowType.Header)
            //{

            //}


            //if (e.Row.RowType==DataControlRowType.DataRow)
            //{
            //    string status = Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Normal"));
            //    int status = 2;
            //    if (status=="Advance")
            //    {
            //        e.Row.Cells[0].Visible = false;
            //        e.Row.Cells[1].Visible = false;
            //        e.Row.Cells[2].Visible = false;

            //    }

            //}



            GridColumnVisibility(e);
        }

        protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "lnkQuotation")
            {

                //For Document
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);
                //End

                ContractorInvoiceBL contractorInvoiceBL = new ContractorInvoiceBL();
                lstItemtransaction = new List<ItemTransaction>();
                lstItemtransaction = contractorInvoiceBL.ReadContractorInvoiceMappingView(id);
                if (lstItemtransaction.Count > 0)
                {
                    gvInvoicePopupItem.DataSource = lstItemtransaction;
                    gvInvoicePopupItem.DataBind();

                    //advance case visble false 
                    HiddenField hdfInvoiceType = (HiddenField)row.FindControl("hdfInvoiceTypeId");
                    VisibleColumn(hdfInvoiceType);
                }

                Label lblContractorName = null;
                lblContractorName = (Label)row.FindControl("lblContName");
                if (lblContractorName != null)
                {
                    lblContName.Text = lblContractorName.Text;
                }

                Label lblInvoiceTypeName = null;
                lblInvoiceTypeName = (Label)row.FindControl("lblInvoiceTypeName");
                if (lblInvoiceTypeName != null)
                {
                    lblInvoiceT.Text = lblInvoiceTypeName.Text;
                }

                Label lblContQuotNo = null;
                lblContQuotNo = (Label)row.FindControl("lblContQuotNo");
                if (lblContQuotNo != null)
                {
                    lblcontractorWONo.Text = lblContQuotNo.Text;
                }
                
                ModalPopupExtender2.Show();
            }
        }

        private void VisibleColumn(HiddenField hdfInvoiceType)
        {
            //Advance
            //visible false gvInvoicePopupItem
            if (Convert.ToInt32(hdfInvoiceType.Value) == Convert.ToInt32(InvoiceType.Advance))
            {

                gvInvoicePopupItem.Columns[0].Visible = false;
                gvInvoicePopupItem.Columns[1].Visible = false;
                gvInvoicePopupItem.Columns[2].Visible = false;
                gvInvoicePopupItem.Columns[3].Visible = false;
                gvInvoicePopupItem.Columns[4].Visible = false;
                gvInvoicePopupItem.Columns[5].Visible = false;
                gvInvoicePopupItem.Columns[6].Visible = false;
            }
            else
            {

               
                gvInvoicePopupItem.Columns[1].Visible = true;
                gvInvoicePopupItem.Columns[2].Visible = true;
                gvInvoicePopupItem.Columns[3].Visible = true;
               // gvInvoicePopupItem.Columns[4].Visible = true;
                gvInvoicePopupItem.Columns[5].Visible = true;
                gvInvoicePopupItem.Columns[6].Visible = true;


            }
        }

        protected void btn_Approve_Reject_Click(object sender, CommandEventArgs e)
        {
            if (!GetSelectData(e.CommandName))
            {
                if (e.CommandName == "Approve")
                    basePage.Alert("Please Select Contractor Invoice No.", btnApprove);
                else
                    basePage.Alert("Please Select Supplier Invoice No.", btnReject);
            }
            else
            {
                //if (e.CommandName == "Approve")
                //    basePage.Alert("Successfully Approved", btnApprove);
                //else
                //    basePage.Alert("Successfully Rejected", btnReject);
            }
            BindInvoice(Convert.ToInt32(ddlStatus.SelectedValue));
        }

        decimal priceTotal = 0;
        decimal TotalAmount = 0;
        protected void gvInvoicePopupItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                priceTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = priceTotal.ToString();
                TotalAmount = priceTotal;
            }

        }

        #endregion

        #region Private Section
        private void ButtonShow(Int32 statusType)
        {
            if (statusType == Convert.ToInt32(StatusType.Pending))
            {
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                btnReject.Visible = false;
                btnApprove.Visible = false;
            }
        }
        private void BindInvoice(Int32 status)
        {
            contractorInvoiceBL = new ContractorInvoiceBL();
            lstInvoice = contractorInvoiceBL.ReadContractorInvoiceStatusWise(status);
            gvInvoice.DataSource = lstInvoice;
            gvInvoice.DataBind();

        }
        private void BindEmptyGrid(GridView Grid)
        {
            Grid.DataSource = new List<object>();
            Grid.DataBind();
        }
        private bool GetSelectData(String action)
        {
            LstInvoiceId = new List<MetaData>();
            lstInvoice = new List<InvoiceDom>();
            contractorInvoiceBL = new ContractorInvoiceBL();
            if (Convert.ToInt32(ddlStatus.SelectedValue) == (Convert.ToInt32(StatusType.Pending)))
            {
                foreach (GridViewRow row in gvInvoice.Rows)
                {
                    CheckBox chbx = (CheckBox)row.FindControl("chbx_Invoice");
                    HiddenField hdf = (HiddenField)row.FindControl("hdf_Invoice_Id");
                    txtRemarkReject = (TextBox)row.FindControl("txtRmarkReject");
                    
                    if (chbx != null && hdf != null)
                    {
                        if (chbx.Checked.Equals(true))
                        {
                            track = true;
                            Invoice = new InvoiceDom();
                            Invoice.IssueMaterial = new IssueMaterialDOM();
                            Invoice.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
                            Invoice.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
                            Invoice.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
                            Invoice.ContractorInvoiceId = Convert.ToInt32(hdf.Value);
                            Invoice.RemarkReject = txtRemarkReject.Text.ToString();
                            if (action == "Approve")
                            {
                                Invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Approved);
                            }
                            else if (action == "Reject")
                            {
                                Invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Rejected);
                            }
                            lstInvoice.Add(Invoice);
                            Invoice.IssueMaterial.DemandVoucher.Quotation.ApprovedRejectedBy = basePage.LoggedInUser.UserLoginId;
                            contractorInvoiceBL.UpdateContractorInvoiceStatusType(Invoice);
                        }
                    }
                }
                if (lstInvoice.Count < 0)
                {
                    track = false;
                }
            }
            return track;
        }
        private void GridColumnVisibility(GridViewRowEventArgs e)
        {
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdf_status_id");
            if ((hdf != null && Convert.ToInt32(hdf.Value) != Convert.ToInt32(StatusType.Pending)) || (Convert.ToInt32(ddlStatus.SelectedValue) != Convert.ToInt32(StatusType.Pending)))
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        #endregion

        #region Private Property
        private List<MetaData> LstInvoiceId
        {
            get
            {

                return (List<MetaData>)ViewState["Index"];
            }
            set
            {
                ViewState["Index"] = value;
            }
        }
        #endregion
    }
}



