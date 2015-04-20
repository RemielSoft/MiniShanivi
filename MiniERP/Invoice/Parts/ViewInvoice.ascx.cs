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
using System.Configuration;
using System.Text;
using System.IO;

namespace MiniERP.Invoice.Parts
{
    public partial class ViewInvoice : System.Web.UI.UserControl
    {
        #region Global varriables

        Int32 daysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        ContractorInvoiceBL contractorInvoiceBL = new ContractorInvoiceBL();
        SupplierInvoiceBL supplierInvoiceBL = new SupplierInvoiceBL();
        ContractorBL contractorBL = new ContractorBL();
        SupplierBL supplierBL = new SupplierBL();
        BasePage basePage = new BasePage();
        List<ItemTransaction> lstItemtransation = null;
        List<InvoiceDom> lstInvoice = null;
        string Message;
        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                toDate = DateTime.Now;
                fromDate = DateTime.Now.AddDays(-daysCount);
                CalExtFromDate.EndDate = DateTime.Now;
                CalExtToDate.EndDate = DateTime.Now;
                LabelAssociation();
                if (PageType == "Contractor")
                {

                    lblName.Text = "Contractor Name";
                    basePage.BindDropDownData(ddlContractorName, "Name", "ContractorId", contractorBL.ReadContractor(null));
                    ddlContractorName.Items.Insert(0, new ListItem("All", "0"));
                    ddlContractorName.SelectedValue = "0";
                }
                txtFromDate.Text = this.fromDate.ToString("dd/MM/yyyy");
                txtToDate.Text = this.toDate.ToString("dd/MM/yyyy");

                BindGrid(null, 0, toDate, fromDate, String.Empty, String.Empty);
                ModalPopupExtender2.Hide();
            }
        }

        private void PageDefaults()
        {
            basePage.Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            BindEmptyGrid(gvInvoice);
            ResetViewState();
            contractorId = Convert.ToInt32(ddlContractorName.SelectedValue);
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtToDate.Text))
            {
                toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            // Compare that to date must be greater than from date
            if (toDate < fromDate && (toDate != DateTime.MinValue && fromDate != DateTime.MinValue))
            {
                basePage.Alert("To Date should be Greater Than From Date", btnSearch);
            }


            ContractorQuotationNo = txtContQuotNo.Text;
            BindGrid(null, contractorId, toDate, fromDate, InvoiceNo, ContractorQuotationNo);
            //ResetControls();
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            ResetViewState();
            if (txtInvoiceNo.Text == string.Empty)
            {
                basePage.Alert("Please Select Invoice No.", LinkSearch);
            }
            else
            {
                InvoiceNo = txtInvoiceNo.Text;
                BindGrid(null, contractorId, toDate, fromDate, InvoiceNo, ContractorQuotationNo);
            }
        }

        protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str = Convert.ToString(e.CommandArgument);
            string[] strid = str.Split(',');
            int id = Convert.ToInt32(strid[0].ToString());

            if (e.CommandName == "lnkEdit")
            {
                Response.Redirect("~/Invoice/ContractorInvoice.aspx?InvoiceId=" + id);
            }

            if (e.CommandName == "lnkPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtnPrint");
                HiddenField hdfInvoiceType = (HiddenField)row.FindControl("hdfInvoiceTypeId");
                if (PageType == "Contractor")
                {
                    if (Convert.ToString(hdfInvoiceType.Value) == Convert.ToString(Convert.ToInt32(InvoiceType.Advance)))
                    {

                        basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/AdvanceSupplierInvoiceReport.aspx?InvoiceId=" + id + "", "ContractorInvoice");
                    }
                    else
                        basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/ContractorInvoiceReport.aspx?InvoiceId=" + id + "", "ContractorInvoice");
                }
            }
            if (e.CommandName == "lnkDelete")
            {

                Message = contractorInvoiceBL.DeleteContractorInvoice(id, basePage.LoggedInUser.UserLoginId, DateTime.Now);

                if (Message == "")
                {
                    //basePage.Alert("Contractor Invoice is Deleted Successfully", gvInvoice);
                }
                else
                {
                    basePage.Alert(Message, gvInvoice);
                }
                BindGrid(null, 0, toDate, fromDate, String.Empty, String.Empty);
            }

            if (e.CommandName == "lnkGenerate")
            {
                InvoiceDom Invoice = new InvoiceDom();
                Invoice.IssueMaterial = new IssueMaterialDOM();
                Invoice.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
                Invoice.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
                Invoice.ContractorInvoiceId = id;
                Invoice.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
                //quotation.StatusType = new MetaData();
                Invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                Invoice.IssueMaterial.DemandVoucher.Quotation.GeneratedBy = basePage.LoggedInUser.UserLoginId;
                int outId = contractorInvoiceBL.UpdateContractorInvoiceStatus(Invoice);
                if (outId > 0)
                {
                    BindGrid(null, 0, toDate, fromDate, String.Empty, String.Empty);
                    //basePage.Alert("Contractor Invoice Is Generated Successfully", gvInvoice);
                }
            }
            if (e.CommandName == "lnkInvoice")
            {
                //For Document
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);

                lstItemtransation = new List<ItemTransaction>();
                BindEmptyGrid(gvItemInfoPopup);
                ContractorInvoiceBL contractorInvoiceBL = new ContractorInvoiceBL();
                lstItemtransation = contractorInvoiceBL.ReadContractorInvoiceMappingView(id);

                if (lstItemtransation.Count > 0)
                {
                    gvItemInfoPopup.DataSource = lstItemtransation;
                    gvItemInfoPopup.DataBind();
                }
                //advance case visble false 
                HiddenField hdfInvoiceType = (HiddenField)row.FindControl("hdfInvoiceTypeId");
                VisibleColumn(hdfInvoiceType);




                Label lblContractorName = null;
                lblContractorName = (Label)row.FindControl("lblConName");
                if (lblContractorName != null)
                {
                    lblContName.Text = lblContractorName.Text;
                }

                Label lblInvoiceTypeName = null;
                lblInvoiceTypeName = (Label)row.FindControl("lblInvoicetypeName");
                if (lblInvoiceTypeName != null)
                {
                    lblInvoiceT.Text = lblInvoiceTypeName.Text;
                }

                Label lblConNo = null;
                lblConNo = (Label)row.FindControl("lblConNo");
                if (lblConNo != null)
                {
                    lblcontractorWONo.Text = lblConNo.Text;
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
                gvItemInfoPopup.Columns[0].Visible = false;
                gvItemInfoPopup.Columns[1].Visible = false;
                gvItemInfoPopup.Columns[2].Visible = false;
                gvItemInfoPopup.Columns[3].Visible = false;
                gvItemInfoPopup.Columns[4].Visible = false;
                gvItemInfoPopup.Columns[5].Visible = false;
                gvItemInfoPopup.Columns[6].Visible = false;
            }
            else
            {

                //gvItemInfoPopup.Columns[0].Visible = true;
                gvItemInfoPopup.Columns[1].Visible = true;
                gvItemInfoPopup.Columns[2].Visible = true;
                gvItemInfoPopup.Columns[3].Visible = true;
                gvItemInfoPopup.Columns[4].Visible = true;
                gvItemInfoPopup.Columns[5].Visible = true;
                gvItemInfoPopup.Columns[6].Visible = true;
            }
        }

        protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInvoice.PageIndex = e.NewPageIndex;
            BindGrid(null, contractorId, toDate, fromDate, InvoiceNo, ContractorQuotationNo);
        }

        protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                LinkButton lnkGenerate = (LinkButton)e.Row.FindControl("lbtnGenerate");
                LinkButton lnkPrint = (LinkButton)e.Row.FindControl("lbtnPrint");
                HiddenField hiddenfld = (HiddenField)e.Row.FindControl("hdfStatusId");
                int Status = Convert.ToInt32((hiddenfld).Value);
                if (basePage.LoggedInUser.Role().Equals(AuthorityLevelType.Admin.ToString()))
                {
                    if (Convert.ToInt32(StatusType.Approved) == Status)
                    {
                        if (lnkDelete != null || lnkEdit != null || lnkPrint != null)
                        {
                            lnkEdit.Visible = false;
                            lnkDelete.Visible = false;
                            lnkPrint.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Generated) == Status)
                    {
                        if (lnkGenerate != null || lnkDelete != null || lnkEdit != null)
                        {
                            lnkDelete.Visible = true;
                            lnkEdit.Visible = false;
                            lnkGenerate.Visible = false;
                            lnkPrint.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Pending) == Status)
                    {
                        if (lnkGenerate != null || lnkPrint != null)
                        {
                            lnkGenerate.Visible = false;
                            lnkEdit.Visible = false;
                            lnkPrint.Visible = false;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Rejected) == Status)
                    {
                        if (lnkGenerate != null || lnkDelete != null || lnkEdit != null ||lnkPrint !=null)
                        {
                            lnkDelete.Visible = true;
                            lnkEdit.Visible = false;
                            lnkGenerate.Visible = false;
                            lnkPrint.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(StatusType.Approved) == Status)
                    {
                        if (lnkDelete != null || lnkEdit != null)
                        {
                            lnkEdit.Visible = false;
                            lnkDelete.Visible = false;
                            lnkPrint.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Generated) == Status)
                    {
                        if (lnkGenerate != null || lnkDelete != null || lnkEdit != null)
                        {
                            lnkDelete.Visible = false;
                            lnkEdit.Visible = false;
                            lnkGenerate.Visible = false;
                            lnkPrint.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Pending) == Status)
                    {
                        if (lnkGenerate != null || lnkPrint != null)
                        {
                            lnkGenerate.Visible = false;
                            lnkEdit.Visible = true;
                            lnkDelete.Visible = true;
                            lnkPrint.Visible = false;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Rejected) == Status)
                    {
                        if (lnkGenerate != null || lnkDelete != null || lnkEdit != null || lnkPrint != null)
                        {
                            lnkDelete.Visible = true;
                            lnkEdit.Visible = true;
                            lnkGenerate.Visible = false;
                            lnkPrint.Visible = false;
                        }
                    }
                }
            }
      }

        protected void LinkbtnClear_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = String.Empty;
        }

        protected void lnkbuttonClear_Click(object sender, EventArgs e)
        {
            txtToDate.Text = String.Empty;
        }

        decimal priceTotal = 0;
        decimal TotalAmount = 0;
        protected void gvItemInfoPopup_RowDataBound(object sender, GridViewRowEventArgs e)
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

     
        protected void LnkBtnExport_Click(object sender, EventArgs e)
        {
            if (txtContQuotNo.Text != null)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", "attachment;filename=InvoiceReport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                StringBuilder sb = new StringBuilder();
                StringWriter stringWrite = new StringWriter(sb);
                HtmlTextWriter htm = new HtmlTextWriter(stringWrite);
                gvInvoice.AllowPaging = false;
                gvInvoice.Columns.RemoveAt(10);
                gvInvoice.DataSource = contractorInvoiceBL.ReadContractorInvoice(null, null, DateTime.MinValue,DateTime.MinValue,null,txtContQuotNo.Text);
                gvInvoice.DataBind();

                //gvSample is Gridview server control
                gvInvoice.RenderControl(htm);
                Response.Write(stringWrite);
                Response.End();

            }
            else
            {
                basePage.Alert("Please insert the contractor work order no.", LnkBtnExport);
            }
        }
       
        #endregion

        #region Priavte Methods

        private void BindGrid(Int32? ContractorInvoiceId, Int32? contractorId, DateTime toDate, DateTime fromDate, String InvoiceNo, String ContractorWorkOrderNo)
        {
            lstInvoice = new List<InvoiceDom>();
            if (ViewState["PageType"].ToString().Trim() == "Contractor")
            {
                lstInvoice = contractorInvoiceBL.ReadContractorInvoice(null, contractorId, toDate, fromDate, InvoiceNo, ContractorWorkOrderNo);
                if (lstInvoice.Count > 0)
                {
                    gvInvoice.DataSource = lstInvoice;
                    gvInvoice.DataBind();
                }
                else
                {
                    BindEmptyGrid(gvInvoice);
                }
            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void ResetControls()
        {
            txtToDate.Text = String.Empty;
            txtInvoiceNo.Text = String.Empty;
            txtFromDate.Text = String.Empty;
            txtContQuotNo.Text = String.Empty;
            ddlContractorName.SelectedValue = "0";
        }

        private void ResetViewState()
        {
            fromDate = DateTime.MinValue;
            toDate = DateTime.MinValue;
            contractorId = 0;
            ContractorQuotationNo = String.Empty;
            InvoiceNo = String.Empty;
        }

        private void LabelAssociation()
        {
            if (PageType == "Contractor")
            {

                //lbl_Invoice_Mapping.Text = "Contractor Invoice";
                lbl_ViewInvoice.Text = "View Contractor Invoice";
            }
        }
        #endregion

        #region public Properties
      
       
        public DateTime fromDate
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

        public DateTime toDate
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

        public Int32 contractorId
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

        public String InvoiceNo
        {
            get
            {
                try
                {
                    return (String)ViewState["InvoiceNo"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["InvoiceNo"] = value;
            }
        }

        public String ContractorQuotationNo
        {
            get
            {
                try
                {
                    return (String)ViewState["ContractorQuotationNo"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["ContractorQuotationNo"] = value;
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

        public Int32 supplierId
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