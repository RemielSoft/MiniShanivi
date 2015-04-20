using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MiniERP.Shared;
using DocumentObjectModel;
using BusinessAccessLayer;

namespace MiniERP.Quality
{
    public partial class ViewDemandIssueVoucher : BasePage
    {
        #region Global Varriable
        Int32 daysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        ContractorBL contractorBL = new ContractorBL();
        CompanyWorkOrderBL CompWorkOrderBL = new CompanyWorkOrderBL();
        IssueDemandVoucherBL issueDemandBL = new IssueDemandVoucherBL();

        QuotationDOM quotation = null;
        List<QuotationDOM> lstQuotation = null;
        List<IssueDemandVoucherDOM> lstIssueDemandVoucherDOM = null;
        List<ItemTransaction> lstItemTransaction = null;
        #endregion

        #region Protected Method

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                toDate = DateTime.Now;
                fromDate = DateTime.Now.AddDays(-daysCount);
                BindDropDown(ddlContractor, "Name", "ContractorId", contractorBL.ReadContractor(null));
                BindDropDown(ddlContractNo, "ContractNumber", "CompanyWorkOrderId", CompWorkOrderBL.ReadCompOrder(null));
                CalExtToDate.EndDate = DateTime.Now;
                CalExtFromDate.EndDate = DateTime.Now;
                BindGrid(0, toDate, fromDate, String.Empty, String.Empty);
                txtFromDate.Text = fromDate.ToString("dd/MM/yyyy");
                txtToDate.Text = toDate.ToString("dd/MM/yyyy");
            }
        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindEmptyGrid(gvIssueDemandVoucher);
            ResetViewState();
            if (ddlContractNo.SelectedItem.Text != "--Select--")
                contractNo = Convert.ToString(ddlContractNo.SelectedItem.Text);
            contractorId = Convert.ToInt32(ddlContractor.SelectedValue);
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

            if (toDate < fromDate && (toDate != DateTime.MinValue && fromDate != DateTime.MinValue))
            {
                ShowMessageWithUpdatePanel("To Date should be Greater Than From Date");
            }
            BindGrid(contractorId, toDate, fromDate, contractNo, IDVNo);
           // ResetControls();
        }
        protected void lnksearch_Click(object sender, EventArgs e)
        {

            ResetViewState();
            IDVNo = txtIssueDmdVoucher.Text;
            BindGrid(contractorId, toDate, fromDate, contractNo, IDVNo);
            //ResetControls();
        }
        protected void LnkBtn_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = string.Empty;
        }
        protected void lnkbtnClear_Click(object sender, EventArgs e)
        {
            txtToDate.Text = string.Empty;
        }
        protected void gvIssueDemandVoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIssueDemandVoucher.PageIndex = e.NewPageIndex;
            BindGrid(contractorId, toDate, fromDate, contractNo, IDVNo);
        }
        protected void gvIssueDemandVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String Message;

            String str = Convert.ToString(e.CommandArgument);
            string[] strid = str.Split(',');

            int idvnID = Convert.ToInt32(strid[0].ToString());
            lstItemTransaction = new List<ItemTransaction>();
            if (e.CommandName == "lnkIDVNDetails")
            {
                lstItemTransaction = issueDemandBL.ReadIssueDemandMapping(idvnID, null);
                BindEmptyGrid(gvItemInfo);
                if (lstItemTransaction.Count > 0)
                {
                    gvItemInfo.DataSource = lstItemTransaction;
                    gvItemInfo.DataBind();

                    //For Document
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                    if (hdfc != null)
                        updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                    else
                        updcFile.GetDocumentData(Int32.MinValue);
                    //End............
                }
                ModalPopupExtender2.Show();
            }
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("~/Quality/DemandIssueVoucher.aspx?IssueDemandVoucherId=" + idvnID);
            }
            if (e.CommandName == "cmdDelete")
            {
                Message = issueDemandBL.DeleteIssueDemandVoucher(idvnID, LoggedInUser.UserLoginId, DateTime.Now);
                if (Message == "")
                {
                    ShowMessageWithUpdatePanel("Issue Demand Voucher is Deleted Successfully");
                }
                else
                {
                    ShowMessageWithUpdatePanel(Message);
                }
                BindGrid(0, toDate, fromDate, String.Empty, String.Empty);
            }
            if (e.CommandName == "cmdPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("LinkPrint");
                string IDVNo = strid[1].ToString();
                //Response.Redirect("~/SSRReport/IssueDemandVoucherReport.aspx?id=" + idvnID + "&IDVNo=" + IDVNo + "");
                OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/IssueDemandVoucherReport.aspx?id=" + idvnID + "&IDVNo=" + IDVNo + "", "IssueDemandVoucher");
            }
            if (e.CommandName == "cmdGenerate")
            {
                //quotation = new QuotationDOM();
                //quotation.DemandVoucher = new IssueDemandVoucherDOM();
                IssueDemandVoucherDOM issueDemandVoucherDOM = new IssueDemandVoucherDOM();
                issueDemandVoucherDOM.IssueDemandVoucherId = idvnID;
                issueDemandVoucherDOM.Quotation = new QuotationDOM();
                issueDemandVoucherDOM.Quotation.StatusType = new MetaData();
                //quotation.StatusType = new MetaData();
                issueDemandVoucherDOM.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                issueDemandVoucherDOM.Quotation.GeneratedBy = LoggedInUser.UserLoginId;
                int outId = issueDemandBL.UpdateIssueDemandVoucherStatus(issueDemandVoucherDOM);
                if (outId > 0)
                {
                    BindGrid(0, toDate, fromDate, String.Empty, String.Empty);
                   
                }
            }
        }
        protected void gvIssueDemandVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("LinkDelete");
                LinkButton lnkGenerate = (LinkButton)e.Row.FindControl("LinkGenQuot");
                LinkButton lnkPrint = (LinkButton)e.Row.FindControl("LinkPrint");
                HiddenField hiddenfld = (HiddenField)e.Row.FindControl("hdfStatusId");
                int Status = Convert.ToInt32((hiddenfld).Value);
                if (Convert.ToInt32(StatusType.Generated) == Status)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    lnkGenerate.Visible = false;
                }
                else if (Convert.ToInt32(StatusType.Generated) != Status)
                {
                    lnkPrint.Visible = true;
                    lnkGenerate.Visible = true;
                    lnkDelete.Visible = true;
                    lnkEdit.Visible = true;
                }
            }
        }
        #endregion

        #region Private Method
        private void ResetControls()
        {
            txtIssueDmdVoucher.Text = String.Empty;
            txtFromDate.Text = String.Empty;
            txtToDate.Text = String.Empty;
            ddlContractNo.SelectedValue = "0";
            ddlContractor.SelectedValue = "0";
        }
        private void ResetViewState()
        {
            fromDate = DateTime.MinValue;
            toDate = DateTime.MinValue;
            contractorId = 0;
            contractNo = String.Empty;
            IDVNo = String.Empty;
        }
        private void BindGrid(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String IDVNo)
        {

            //lstQuotation = new List<QuotationDOM>();
            lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
            lstIssueDemandVoucherDOM = issueDemandBL.ViewIssueDemand(contractorId, toDate, fromDate, contractNo, IDVNo);
           
            if (lstIssueDemandVoucherDOM.Count > 0)
            {

                gvIssueDemandVoucher.DataSource = lstIssueDemandVoucherDOM;
                gvIssueDemandVoucher.DataBind();
            }
            else
            {
                BindEmptyGrid(gvIssueDemandVoucher);
                //basePage.GridViewEmptyText(gvViewOrder); 
            }
            //ResetControls();
        }
        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
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

        public String contractNo
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

        public String IDVNo
        {
            get
            {
                try
                {
                    return (String)ViewState["IDVNo"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["IDVNo"] = value;
            }
        }

        #endregion

    }
}