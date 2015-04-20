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
using BusinessAccessLayer.Quality;


namespace MiniERP.Quality
{
    public partial class ViewReceiveMaterialCompanyWorkOrder : BasePage
    {
        #region Global Region
        Int32 DaysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        ReceiveMaterialCompanyWorkOrderBL ReceiveMaterialCWO = new ReceiveMaterialCompanyWorkOrderBL();
        CompanyWorkOrderBL companyWorkOrderBL = new CompanyWorkOrderBL();
        ReceiveMaterialCompanyWorkOrderDom ReceiveMaterialCompanyDOM = null;
        List<ReceiveMaterialCompanyWorkOrderDom> lstRMCWO = null;
        #endregion

        #region Protected Region
        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                ToDate = DateTime.Now;
                FromDate = DateTime.Now.AddDays(-DaysCount);
                BindDropDown(ddl_CWO_Number, "CompanyWorkOrderNumber", "CompanyWorkOrderId", companyWorkOrderBL.ReadCompOrder(null));
                txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
                txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                BindReceiveMaterialCWOGrid(CRMNo, CompanyWorkOrderId, FromDate, ToDate);
                CalExtFromDate.EndDate = DateTime.Now;
                CalExtToDate.EndDate = DateTime.Now;
            }
            
        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void gvCRM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String Message;

            string str =Convert.ToString(e.CommandArgument);
            string[] strid = str.Split(',');
            int RMCWOId =Convert.ToInt32(strid[0].ToString());
            if (e.CommandName=="CRMDetails")
            {
                List<ItemTransaction> lstitemTransaction = new List<ItemTransaction>();
                lstitemTransaction = ReceiveMaterialCWO.ReadRMCWOMapping(RMCWOId);
                BindEmptyGrid(gvItemInfo);
                if (lstitemTransaction.Count>0)
                {
                    gvItemInfo.DataSource = lstitemTransaction;
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
            if (e.CommandName=="cmdEdit")
            {
                Response.Redirect("~/Quality/ReceiveMaterialCompanyWorkOrder.aspx?ReceiveMaterialCWOId=" + RMCWOId);
            }
            if (e.CommandName=="cmdDelete")
            {
                Message = ReceiveMaterialCWO.DeleteReceiveMaterialCWO(RMCWOId, LoggedInUser.UserLoginId, DateTime.Now);
                if (Message == "")
                {
                    ShowMessageWithUpdatePanel("Receive Material Company Work Order is Deleted Successfully");
                }
                else
                {
                    ShowMessageWithUpdatePanel(Message);
                }
               BindReceiveMaterialCWOGrid(CRMNo, CompanyWorkOrderId, FromDate, ToDate); 
            }
            if (e.CommandName == "cmdPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lnkPrint");
                OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/ReceiveMaterialCWORpt.aspx?id=" + RMCWOId + "", "ReceiveMaterialCWO");
            }
            if (e.CommandName == "cmdGenerate")
            {
                ReceiveMaterialCompanyDOM = new ReceiveMaterialCompanyWorkOrderDom();
                ReceiveMaterialCompanyDOM.ContractReceiveMaterialId = RMCWOId;
                
                ReceiveMaterialCompanyDOM.Quotation = new QuotationDOM();
                ReceiveMaterialCompanyDOM.Quotation.StatusType = new MetaData();

                ReceiveMaterialCompanyDOM.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                ReceiveMaterialCompanyDOM.Quotation.GeneratedBy = LoggedInUser.UserLoginId;
                int outId = ReceiveMaterialCWO.UpdateReceiveMaterialCWOStatus(ReceiveMaterialCompanyDOM);
                if (outId > 0)
                {
                    BindReceiveMaterialCWOGrid(CRMNo, CompanyWorkOrderId, FromDate, ToDate);
                    ShowMessageWithUpdatePanel("Receive Material Company Work Order Is Generated Successfully");
                }
            }
        }

        protected void gvCRM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton lnkGenerate = (LinkButton)e.Row.FindControl("lnkGenerate");
                LinkButton lnkPrint = (LinkButton)e.Row.FindControl("lnkPrint");
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

        protected void gvCRM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCRM.PageIndex = e.NewPageIndex;
            BindReceiveMaterialCWOGrid(CRMNo, CompanyWorkOrderId, FromDate, ToDate);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindEmptyGrid(gvCRM);
            ResetViewState();
            CRMNo = txtCRMNumber.Text;
            CompanyWorkOrderId = Convert.ToInt32(ddl_CWO_Number.SelectedValue);
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtToDate.Text))
            {
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (ToDate < FromDate && (ToDate != DateTime.MinValue && FromDate != DateTime.MinValue))
            {
                ShowMessageWithUpdatePanel("To Date should be Greater Than From Date");
            }
            BindReceiveMaterialCWOGrid(CRMNo, CompanyWorkOrderId, FromDate, ToDate);
        }

        protected void LnkBtnFrom_Clear_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = string.Empty;
        }

        protected void LnkBtnTo_Clear_Click(object sender, EventArgs e)
        {
            txtToDate.Text = string.Empty;
        }
        #endregion

        #region Private Method
        public void BindReceiveMaterialCWOGrid(String CRMNo, int CompanyWorkOrderId, DateTime FromDate, DateTime ToDate)
        {
            lstRMCWO = new List<ReceiveMaterialCompanyWorkOrderDom>();
            lstRMCWO = ReceiveMaterialCWO.ReadRMCWO(CRMNo, CompanyWorkOrderId, FromDate, ToDate);
            if (lstRMCWO.Count > 0)
            {
                gvCRM.DataSource = lstRMCWO;
                gvCRM.DataBind();
            }
            else
            {
                BindEmptyGrid(gvCRM);
            }
        }
        public void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<Object>();
            gv.DataBind();
        }
        private void ResetViewState()
        {
            CRMNo = String.Empty;
            ToDate = DateTime.MinValue;
            FromDate = DateTime.MinValue;
            CompanyWorkOrderId = 0;
        }
        #endregion

        #region Private Properties

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
        public String CRMNo
        {
            get
            {
                try
                {
                    return (String)ViewState["CRMNo"];
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["CRMNo"] = value; }
        }
        public Int32 CompanyWorkOrderId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["CompanyWorkOrderId"];
                }
                catch
                {
                    return 0;
                }
            }
            set { ViewState["CompanyWorkOrderId"] = value; }
        }
        #endregion


    }
}