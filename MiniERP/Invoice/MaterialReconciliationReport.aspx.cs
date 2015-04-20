using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using BusinessAccessLayer.Invoice;
using BusinessAccessLayer.Quality;
using DataAccessLayer;
using DataAccessLayer.Invoice;
using DocumentObjectModel;
using MiniERP.Shared;
using System.Configuration;



namespace MiniERP.Invoice
{
    public partial class MaterialReconciliationReport : BasePage
    {
        #region private Goble varibale(s)
        Int32 DaysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        ContractorBL contractorBL = new ContractorBL();
        CompanyWorkOrderBL CompWorkOrderBL = new CompanyWorkOrderBL();
        MaterialConsumptionNoteBAL materialConsumptionNoteBAL = null;
        QuotationBL quotationBL = null;
        ItemTransaction itemTransaction = null;
        List<ItemTransaction> lstitemTransaction = null;
        MaterialConsumptionNoteDom materialConsumptionNoteDom = null;

        IssueMaterialDOM issueMaterialDOM = null;
        IssueMaterialBL issueMaterialBL = null;

        List<IssueMaterialDOM> lstIssueMaterialDOM = null;

        MetaData metaData = null;
        int index = 0;
        int i = 0;
        int j = 0;
        Boolean flag = true;
        string strInvalid = string.Empty;
        decimal dec = 0;
        int cnt = 0;
        String s = string.Empty;
        decimal BigNo = 0;

        #endregion

        #region Protected Method



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ToDate = DateTime.Now;
                FromDate = DateTime.Now.AddDays(-DaysCount);
                BindDropDown(ddlContName, "Name", "ContractorId", contractorBL.ReadContractor(null));
                BindDropDown(ddlContractNo, "ContractNumber", "CompanyWorkOrderId", CompWorkOrderBL.ReadCompOrder(null));
                CalExtfromDate.EndDate = DateTime.Now;
                CalExtToDate.EndDate = DateTime.Now;
                BindGrid(null, 0, ToDate, FromDate, string.Empty, string.Empty);
                txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindEmptyGrid(gvMaterialConsumptionReport);
            ResetViewState();
            if (ddlContractNo.SelectedItem.Text != "--Select--")
                ContractNo = Convert.ToString(ddlContractNo.SelectedItem.Text);

            ContractorId = Convert.ToInt32(ddlContName.SelectedValue);

            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                // FromDate = DateTime.ParseExact(txtFromDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtToDate.Text))
            {
                // ToDate = DateTime.ParseExact(txtToDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (ToDate < FromDate)
            {
                ShowMessageWithUpdatePanel("To Date should be Greater Than From Date");
            }
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, MaterialConsumptionNo);
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            ResetViewState();
            MaterialConsumptionNo = txtMCNo.Text;
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, MaterialConsumptionNo);
        }


        protected void gvMaterialConsumptionReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("LinkDelete");
                LinkButton lnkGenQuot = (LinkButton)e.Row.FindControl("LinkGenQuot");
                LinkButton linkPrint = (LinkButton)e.Row.FindControl("LinkPrint");
                HiddenField HdfStatusId = (HiddenField)e.Row.FindControl("hdfStatusId");

                int status = Convert.ToInt32((HdfStatusId).Value);
                if (Convert.ToInt32(StatusType.Generated) == status)
                {

                    lnkEdit.Visible = false;
                    lnkDelete.Visible = false;
                    lnkGenQuot.Visible = false;
                    linkPrint.Visible = true;
                }
                else if (Convert.ToInt32(StatusType.Generated) != status)
                {
                    lnkDelete.Visible = true;
                    lnkGenQuot.Visible = true;
                    linkPrint.Visible = true;

                }


            }

        }

        protected void gvMaterialConsumptionReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String Message;
            int Id = Convert.ToInt32(e.CommandArgument);
            //String str = Convert.ToString(e.CommandArgument);
            //String[] strid = str.Split(',');
            //int MCNId = Convert.ToInt32(strid[0].ToString());
            if (e.CommandName == "lnkIMCNoDetails")
            {
                lstitemTransaction = new List<ItemTransaction>();
                materialConsumptionNoteBAL = new MaterialConsumptionNoteBAL();
                lstitemTransaction = materialConsumptionNoteBAL.ReadMaterialConsumptionMapping(Id);
                BindEmptyGrid(gvMaterialConsumptionReport);
                if (lstitemTransaction.Count > 0)
                {
                    gvItemInfo.DataSource = lstitemTransaction;
                    gvItemInfo.DataBind();
                }
                ModalPopupExtender2.Show();
            }
            if (e.CommandName == "cmdPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("LinkPrint");
                Response.Redirect("~/SSRReport/MaterialConsumptionNote.aspx?MatCNId=" + Id + "");
                //OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/MaterialConsumptionNote.aspx?MatCNId=" + Id + "", "Material Consumption");
            }

            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("~/Invoice/MaterialConsumptionNote.aspx?MaterialConsumptId=" + Id);

            }
            if (e.CommandName == "cmdDelete")
            {
                MaterialConsumptionNoteBAL materialConsumptionNoteBAL = new MaterialConsumptionNoteBAL();
                Message = materialConsumptionNoteBAL.DeleteMaterialConsumtionBL(Id, LoggedInUser.UserLoginId, DateTime.Now);
                if (Message == "")
                {
                    ShowMessageWithUpdatePanel("Material Consumption Notes is Deleted Successfully");
                }
                else
                {
                    ShowMessageWithUpdatePanel(Message);
                }
                BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, MaterialConsumptionNo);

            }
            if (e.CommandName == "cmdGenerate")
            {
                MaterialConsumptionNoteBAL materialConsumptionNoteBAL = new MaterialConsumptionNoteBAL();
                MaterialConsumptionNoteDom materialConsumptionNoteDom = new MaterialConsumptionNoteDom();
                materialConsumptionNoteDom.IssueMaterial = new IssueMaterialDOM();
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
                materialConsumptionNoteDom.MaterialConsumptionId = Id;
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
                //quotation.StatusType = new MetaData();
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.GeneratedBy = LoggedInUser.UserLoginId;
                int outId = materialConsumptionNoteBAL.UpdateMaterialReconciliationStatusBL(materialConsumptionNoteDom);
                if (outId > 0)
                {
                    //BindGrid(0, toDate, fromDate, String.Empty, String.Empty);
                    BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, MaterialConsumptionNo);
                    //ShowMessageWithUpdatePanel("Material Reconciliation Generated Successfully");
                }
            }
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, MaterialConsumptionNo);
        }

        protected void gvMaterialConsumptionReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMaterialConsumptionReport.PageIndex = e.NewPageIndex;
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, MaterialConsumptionNo);
        }

        protected void lnkToDate_Click(object sender, EventArgs e)
        {
            txtToDate.Text = String.Empty;
        }

        protected void lnkFromDate_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = String.Empty;
        }

        #endregion

        #region Private Method

        private void BindGrid(Int32? MaterialConsumptionId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String MaterialConsumptionNo)
        {
            List<MaterialConsumptionNoteDom> lstMaterialConsumption = new List<MaterialConsumptionNoteDom>();
            materialConsumptionNoteBAL = new MaterialConsumptionNoteBAL();
            lstMaterialConsumption = materialConsumptionNoteBAL.ReadMaterialConsumption(MaterialConsumptionId, ContractorId, ToDate, FromDate, ContractNo, MaterialConsumptionNo);
            if (lstMaterialConsumption.Count > 0)
            {
                gvMaterialConsumptionReport.DataSource = lstMaterialConsumption;
                gvMaterialConsumptionReport.DataBind();
            }
            else
            {
                BindEmptyGrid(gvMaterialConsumptionReport);
            }
        }

        private void ResetControles()
        {
            ddlContName.SelectedValue = "0";
            ddlContractNo.SelectedValue = "0";
            txtFromDate.Text = String.Empty;
            txtToDate.Text = String.Empty;
            txtMCNo.Text = String.Empty;
        }

        private void ResetViewState()
        {
            MaterialConsumptionId = 0;
            ContractNo = String.Empty;
            ToDate = DateTime.MinValue;
            FromDate = DateTime.MinValue;
            ContractorId = 0;
        }

        public void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        #endregion

        #region Public Properties

        public DateTime ToDate
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

        public DateTime FromDate
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

        public String ContractNo
        {
            get
            {
                try
                {
                    return (String)ViewState["ContractNo"];
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["ContractNo"] = value; }
        }

        public String MaterialConsumptionNo
        {
            get
            {
                try
                {
                    return (String)ViewState["MaterialConsumptionNo"];
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["MaterialConsumptionNo"] = value; }
        }

        public Int32 ContractorId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["ContractorId"];
                }
                catch
                {
                    return 0;
                }
            }
            set { ViewState["ContractorId"] = value; }
        }

        public Int32 MaterialConsumptionId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["MaterialConsumptionId"];
                }
                catch
                {
                    return 0;
                }
            }
            set { ViewState["MaterialConsumptionId"] = value; }
        }

        #endregion

    }
}