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


namespace MiniERP.Invoice
{
    public partial class SupplierInvoiceApproval : BasePage
    {
        #region Global Varriables
        Int32 id;
        bool track;
        SupplierInvoiceBL supplierInvoiceBL = new SupplierInvoiceBL();
        List<InvoiceDom> lstInvoice = new List<InvoiceDom>();
        QuotationBL quotationBL = new QuotationBL();
        TextBox txtRemarkReject = null;
        #endregion

        #region Protected Method

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                BindDropDownData(ddlStatus, "Name", "Id", quotationBL.ReadQuotationStatusMetaData());
                //ddlStatus_SelectedIndexChanged(null, null);
                BindSupplierInvoice(Convert.ToInt32(StatusType.Pending));
                ModalPopupExtender2.Hide();
            }
        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void oncheck_uncheck_all(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvSupplierInvoice.Rows)
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
            CheckBox chb = (CheckBox)gvSupplierInvoice.HeaderRow.FindControl("chbx_select_all");
            foreach (GridViewRow row in gvSupplierInvoice.Rows)
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
            BindSupplierInvoice(id);
            ButtonShow(id);
        }

        protected void gvSupplierInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvInvoiceItems.PageIndex = e.NewPageIndex;
            BindSupplierInvoice(Convert.ToInt32(ViewState["StatusId"]));
        }

        protected void btn_Approve_Reject_Click(object sender, CommandEventArgs e)
        {
            if (!GetSelectData(e.CommandName))
            {
                if (e.CommandName == "Approve")
                    Alert("Please Select Contractor Invoice No.", btnApprove);
                else
                    Alert("Please Select Supplier Invoice No.", btnReject);
            }
            else
            {
                //if (e.CommandName == "Approve")
                //    Alert("Successfully Approved", btnApprove);
                //else
                //    Alert("Successfully Reject", btnReject);
            }
            BindSupplierInvoice(Convert.ToInt32(ddlStatus.SelectedValue));

        }

        protected void gvSupplierInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridColumnVisibility(e);
        }

        protected void gvSupplierInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "lnkInvoice")
            {
                //For Document
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);
                //End
                List<ItemTransaction> lstItemtransation = new List<ItemTransaction>();
                lstItemtransation = supplierInvoiceBL.ReadSupplierInvoiceMapping(id);
                if (lstItemtransation.Count > 0)
                {
                    gvInvoiceItems.DataSource = lstItemtransation;
                    gvInvoiceItems.DataBind();


                    //advance case visble false 
                    HiddenField hdfInvoiceType = (HiddenField)row.FindControl("hdfInvoiceTypeId");
                    VisibleColumn(hdfInvoiceType);


                }
                else
                {
                    gvInvoiceItems.DataSource = new List<Object>();
                    gvInvoiceItems.DataBind();
                }

                Label supplierName = null;
                supplierName = (Label)row.FindControl("lblSupplierName");
                if (supplierName != null)
                {
                    lblSupplrName.Text = supplierName.Text;
                }

                Label lblInvoiceTypeName = null;
                lblInvoiceTypeName = (Label)row.FindControl("lblInvoiceTypeName");
                if (lblInvoiceTypeName != null)
                {
                    lblInvoiceT.Text = lblInvoiceTypeName.Text;
                }
                Label lblSupplierPONo = null;
                lblSupplierPONo = (Label)row.FindControl("lblSupplierPONo");
                if (lblSupplierPONo != null)
                {
                    lblSuppPONo.Text = lblSupplierPONo.Text;
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

                gvInvoiceItems.Columns[0].Visible = false;
                gvInvoiceItems.Columns[1].Visible = false;
                gvInvoiceItems.Columns[2].Visible = false;
                gvInvoiceItems.Columns[3].Visible = false;
                gvInvoiceItems.Columns[4].Visible = false;
                gvInvoiceItems.Columns[5].Visible = false;
               
            }
            else
            {

                gvInvoiceItems.Columns[0].Visible = true;
                gvInvoiceItems.Columns[1].Visible = true;
                gvInvoiceItems.Columns[2].Visible = true;
                gvInvoiceItems.Columns[3].Visible = true;
                gvInvoiceItems.Columns[4].Visible = true;
                gvInvoiceItems.Columns[5].Visible = true;
               


            }
        }


        #endregion

        #region Private Method

        private void BindSupplierInvoice(int Status)
        {
            lstInvoice = supplierInvoiceBL.ReadSupplierInvoiceStatusWise(Status);
            gvSupplierInvoice.DataSource = lstInvoice;
            gvSupplierInvoice.DataBind();
        }

        private void BindEmptyGrid(GridView Grid)
        {
            Grid.DataSource = new List<object>();
            Grid.DataBind();
        }

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

        private void GridColumnVisibility(GridViewRowEventArgs e)
        {
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdf_status_id");
            if (hdf != null && Convert.ToInt32(hdf.Value) != Convert.ToInt32(StatusType.Pending) || Convert.ToInt32(ddlStatus.SelectedValue) != Convert.ToInt32(StatusType.Pending))
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        private bool GetSelectData(String action)
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == (Convert.ToInt32(StatusType.Pending)))
            {
                foreach (GridViewRow row in gvSupplierInvoice.Rows)
                {
                    CheckBox chbx = (CheckBox)row.FindControl("chbx_Invoice");
                    HiddenField hdf = (HiddenField)row.FindControl("hdf_Invoice_Id");

                    txtRemarkReject = (TextBox)row.FindControl("txtRemarkReject");
                    if (chbx != null && hdf != null)
                    {
                        if (chbx.Checked.Equals(true))
                        {
                            track = true;
                            InvoiceDom invoice = new InvoiceDom();
                            invoice.ReceiveMaterial = new SupplierRecieveMatarial();
                            invoice.ReceiveMaterial.Quotation = new QuotationDOM();
                            invoice.ReceiveMaterial.Quotation.StatusType = new MetaData();
                            invoice.SupplierInvoiceId = Convert.ToInt32(hdf.Value);
                            invoice.RemarkReject = txtRemarkReject.Text.ToString();
                            if (action == "Approve")
                            {
                                invoice.ReceiveMaterial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Approved);
                            }
                            else if (action == "Reject")
                            {
                                invoice.ReceiveMaterial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Rejected);
                            }
                            lstInvoice.Add(invoice);
                            invoice.ReceiveMaterial.Quotation.ApprovedRejectedBy = LoggedInUser.UserLoginId;
                            supplierInvoiceBL.UpdateSupplierInvoiceStatusType(invoice);
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

        #endregion

        decimal priceTotal = 0;
        decimal TotalAmount = 0;
        protected void gvInvoiceItems_RowDataBound(object sender, GridViewRowEventArgs e)
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

        #region Public Properties

        #endregion
    }
}