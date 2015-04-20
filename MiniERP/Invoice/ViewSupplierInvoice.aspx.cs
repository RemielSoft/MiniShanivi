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

namespace MiniERP.Invoice
{
    public partial class ViewSupplierInvoice : BasePage
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
                lblName.Text = "Supplier Name";

                basePage.BindDropDownData(ddlSupplierName, "Name", "SupplierId", supplierBL.ReadSupplier(null));
                ddlSupplierName.Items.Insert(0, new ListItem("All", "0"));
                ddlSupplierName.SelectedValue = "0";

                txtFromDate.Text = this.fromDate.ToString("dd/MM/yyyy");
                txtToDate.Text = this.toDate.ToString("dd/MM/yyyy");

                BindGrid(null, 0, toDate, fromDate, String.Empty, String.Empty);
                ModalPopupExtender2.Hide();
            }
        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            BindEmptyGrid(gvSupplierInvoice);
            ResetViewState();
            SupplierId = Convert.ToInt32(ddlSupplierName.SelectedValue);
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

            SupplierPONo = txtSupplierPONo.Text;
            BindGrid(null, SupplierId, toDate, fromDate, InvoiceNo, SupplierPONo);
            //ResetControls();
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            ResetViewState();
            if (txtInvoiceNo.Text == string.Empty)
            {
                basePage.Alert("Please Enter Invoice No.", LinkSearch);
            }
            else
            {
                InvoiceNo = txtInvoiceNo.Text;
                BindGrid(null, SupplierId, toDate, fromDate, InvoiceNo, SupplierPONo);
            }
            //ResetControls();
        }

        protected void gvSupplierInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string Message;
            string str = Convert.ToString(e.CommandArgument);
            string[] strid = str.Split(',');
            int id = Convert.ToInt32(strid[0].ToString());

            if (e.CommandName == "lnkEdit1")
            {
                Response.Redirect("~/invoice/SupplierInvoice.aspx?invoiceId=" + id);
            }
            if (e.CommandName == "lnkPrint1")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtnPrint1");
                HiddenField hdfInvoiceType = (HiddenField)row.FindControl("hdnInvoiceType");
                if (Convert.ToString(hdfInvoiceType.Value) == Convert.ToString(Convert.ToInt32(InvoiceType.Advance)))
                {
                    OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/AdvanceSupplierInvoiceReport.aspx?InvoiceId=" + id + "", "SupplierInvoice");
                }
                else
                    OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/SupplierInvoiceReport.aspx?InvoiceId=" + id + "", "SupplierInvoice");
            }
            if (e.CommandName == "lnkDelete1")
            {

                Message = supplierInvoiceBL.DeleteSupplierInvoice(id, basePage.LoggedInUser.UserLoginId, DateTime.Now);

                if (Message == "")
                {
                    //Alert("Supplier Invoice is Deleted Successfully", gvSupplierInvoice);
                }
                else
                {
                    basePage.Alert(Message, gvSupplierInvoice);
                }
                BindGrid(null, 0, toDate, fromDate, String.Empty, String.Empty);
            }
            if (e.CommandName == "lnkGenerate1")
            {
                InvoiceDom Invoice = new InvoiceDom();
                Invoice.ReceiveMaterial = new SupplierRecieveMatarial();
                Invoice.ReceiveMaterial.Quotation = new QuotationDOM();
                Invoice.SupplierInvoiceId = id;
                Invoice.ReceiveMaterial.Quotation.StatusType = new MetaData();
                Invoice.ReceiveMaterial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                Invoice.ReceiveMaterial.Quotation.GeneratedBy = basePage.LoggedInUser.UserLoginId;
                int outId = supplierInvoiceBL.UpdateSupplierInvoiceStatus(Invoice);
                if (outId > 0)
                {
                    BindGrid(null, 0, toDate, fromDate, String.Empty, String.Empty);
                    //basePage.Alert("Supplier Invoice Is Generated Successfully", gvSupplierInvoice);
                }
            }

            if (e.CommandName == "lnkInvoice1")
            {
                //For Document
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);
                //End
                lstItemtransation = supplierInvoiceBL.ReadSupplierInvoiceMapping(id);
                if (lstItemtransation.Count > 0)
                {
                    gvItemInfo.DataSource = lstItemtransation;
                    gvItemInfo.DataBind();


                    //advance case visble false 
                    HiddenField hdfInvoiceType = (HiddenField)row.FindControl("hdfInvoiceTypeId");
                    VisibleColumn(hdfInvoiceType);
                }

                else
                {
                    gvItemInfo.DataSource = new List<Object>();
                    gvItemInfo.DataBind();
                }
                Label supplierName = null;
                supplierName = (Label)row.FindControl("lblConName1");
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
                lblSupplierPONo = (Label)row.FindControl("lblConNo1");
                if (lblSupplierPONo != null)
                {
                    lblSuppPONo.Text = lblSupplierPONo.Text;
                }

                ModalPopupExtender2.Show();
            }
        }

        protected void gvSupplierInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit1 = (LinkButton)e.Row.FindControl("lbtnEdit1");
                LinkButton lnkDelete1 = (LinkButton)e.Row.FindControl("lbtnDelete1");
                LinkButton lnkGenerate1 = (LinkButton)e.Row.FindControl("lbtnGenerate1");
                LinkButton lnkPrint1 = (LinkButton)e.Row.FindControl("lbtnPrint1");
                HiddenField hiddenfld1 = (HiddenField)e.Row.FindControl("hdfStatusId1");
                Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");
                int Status = Convert.ToInt32((hiddenfld1).Value);
                if (LoggedInUser.Role().Equals(AuthorityLevelType.Admin.ToString()))
                {
                    if (Convert.ToInt32(StatusType.Approved) == Status)
                    {
                        if (lnkDelete1 != null || lnkEdit1 != null)
                        {
                            lnkEdit1.Visible = false;
                            lnkDelete1.Visible = false;
                            lnkPrint1.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Pending) == Status)
                    {
                        if (lnkGenerate1 != null)
                        {
                            lnkGenerate1.Visible = false;
                            lnkPrint1.Visible = false;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Rejected) == Status )
                    {
                        if (lnkGenerate1 != null || lnkDelete1 != null || lnkEdit1 != null)
                        {
                            lnkDelete1.Visible = true;
                            lnkEdit1.Visible = false;
                            lnkGenerate1.Visible = false;
                            lnkPrint1.Visible = false;
                        }
                    }

                    if (Convert.ToInt32(StatusType.Generated) == Status)
                    {
                        if (lnkGenerate1 != null || lnkDelete1 != null || lnkEdit1 != null)
                        {
                            lnkDelete1.Visible = true;
                            lnkEdit1.Visible = false;
                            lnkGenerate1.Visible = false;
                            lnkPrint1.Visible = true;
                        }
                        
                    }
                }
                else
                {
                    if (Convert.ToInt32(StatusType.Pending) == Status || Convert.ToInt32(StatusType.Rejected) == Status )
                    {
                        if (lnkGenerate1 != null || lnkDelete1 != null || lnkEdit1 != null)
                        {
                            lnkDelete1.Visible = true;
                            lnkEdit1.Visible = true;
                            lnkGenerate1.Visible = false;
                            lnkPrint1.Visible = false;
                        }
                    }
                    else
                    {
                        if (lnkGenerate1 != null || lnkDelete1 != null || lnkEdit1 != null)
                        {
                            lnkDelete1.Visible = false;
                            lnkEdit1.Visible = false;
                            lnkGenerate1.Visible = false;
                            lnkPrint1.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(StatusType.Approved) == Status)
                    {
                        if (lnkDelete1 != null || lnkEdit1 != null)
                        {
                            lnkEdit1.Visible = false;
                            lnkDelete1.Visible = false;
                            lnkPrint1.Visible = true;
                            lnkGenerate1.Visible = true; 
                        }
                    }
                }
            }
        }

        protected void gvSupplierInvoice_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvSupplierInvoice.PageIndex = e.NewPageIndex;
            BindGrid(null, SupplierId, toDate, fromDate, InvoiceNo, SupplierPONo);
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
        protected void gvItemInfo_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (txtSupplierPONo.Text != null)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", "attachment;filename=InvoiceReport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";

                StringBuilder sb = new StringBuilder();
                StringWriter stringWrite = new StringWriter(sb);
                HtmlTextWriter htm = new HtmlTextWriter(stringWrite);
                gvSupplierInvoice.AllowPaging = false;
                gvSupplierInvoice.Columns.RemoveAt(10);
                gvSupplierInvoice.DataSource = supplierInvoiceBL.ReadSupplierInvoice(null, null, DateTime.MinValue, DateTime.MinValue, null, txtSupplierPONo.Text);
                gvSupplierInvoice.DataBind();

                //gvSample is Gridview server control
                gvSupplierInvoice.RenderControl(htm);
                Response.Write(stringWrite);
                Response.End();

            }
            else
            {
                Alert("Please insert the supplier purchase order no.", LnkBtnExport);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Do nothing
        }
        #endregion

        #region Priavte Methods

        private void BindGrid(Int32? SupplierInvoiceId, Int32? SupplierId, DateTime toDate, DateTime fromDate, String InvoiceNo, String SupplierPONo)
        {
            lstInvoice = new List<InvoiceDom>();

            lstInvoice = supplierInvoiceBL.ReadSupplierInvoice(null, SupplierId, toDate, fromDate, InvoiceNo, SupplierPONo);
            if (lstInvoice.Count > 0)
            {
                gvSupplierInvoice.DataSource = lstInvoice;
                gvSupplierInvoice.DataBind();
            }
            else
            {
                BindEmptyGrid(gvSupplierInvoice);
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
            txtSupplierPONo.Text = String.Empty;
            ddlSupplierName.SelectedValue = "0";
        }

        private void ResetViewState()
        {
            fromDate = DateTime.MinValue;
            toDate = DateTime.MinValue;
            SupplierPONo = String.Empty;
            InvoiceNo = String.Empty;
        }

        private void VisibleColumn(HiddenField hdfInvoiceType)
        {
            //Advance
            //visible false gvInvoicePopupItem
            if (Convert.ToInt32(hdfInvoiceType.Value) == Convert.ToInt32(InvoiceType.Advance))
            {

                gvItemInfo.Columns[0].Visible = false;
                gvItemInfo.Columns[1].Visible = false;
                gvItemInfo.Columns[2].Visible = false;
                gvItemInfo.Columns[3].Visible = false;
                gvItemInfo.Columns[4].Visible = false;
                gvItemInfo.Columns[5].Visible = false;
            }
            else
            {
                gvItemInfo.Columns[0].Visible = true;
                gvItemInfo.Columns[1].Visible = true;
                gvItemInfo.Columns[2].Visible = true;
                gvItemInfo.Columns[3].Visible = true;
                gvItemInfo.Columns[4].Visible = true;
                gvItemInfo.Columns[5].Visible = true;
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

        public String SupplierPONo
        {
            get
            {
                try
                {
                    return (String)ViewState["SupplierPONo"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["SupplierPONo"] = value;
            }
        }

        public Int32 SupplierId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["SupplierId"];
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                ViewState["SupplierId"] = value;
            }
        }

        #endregion

    }
}