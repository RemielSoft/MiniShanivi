using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.Configuration;
using MiniERP.Shared;

namespace MiniERP.Company
{
    public partial class ViewCompanyWorkOrder : BasePage
    {

        #region Global Variable(s)

        Int32 daysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        CompanyWorkOrderBL companyWOBL = new CompanyWorkOrderBL();

        List<CompanyWorkOrderDOM> lstCompanyWorkOrder = null;
        List<WorkOrderMappingDOM> lstWorkOderMapping = null;
        List<BankGuaranteeDOM> lstBankGuarantee = null;
        List<ServiceDetailDOM> lstServiceDetail = null;

        DateTime currentDate = DateTime.MinValue;
        DateTime twoYearBackDate = DateTime.MinValue;

        #endregion

        #region  Protected Event(s)

        protected void Page_Load(object sender, EventArgs e)
        {
            //int flag = Convert.ToInt32(Request.QueryString["flag"]);
            //if (flag > 0)
            //{
            //    Alert("", btnClose);
            //}
            PageDefaults();
            currentDate = DateTime.Now;
            twoYearBackDate = DateTime.Now.AddDays(-daysCount);

            this.FromDate = twoYearBackDate;
            this.ToDate = currentDate;

            if (!IsPostBack)
            {
                BindGrid(twoYearBackDate, currentDate);
                txtFromDate.Text = twoYearBackDate.ToString("dd'/'MM'/'yyyy");
                txtToDate.Text = currentDate.ToString("dd'/'MM'/'yyyy");
                //calFromDate.EndDate = DateTime.Now.AddMonths(6);
                //calToDate.EndDate = DateTime.Now.AddMonths(6);
            }
        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();

        }
        protected void btnSearch_Click(object sender, CommandEventArgs e)
        {
            DateTime toDate = DateTime.MinValue;
            DateTime fromDate = DateTime.MinValue;
            String contractNo = string.Empty;

            if (e.CommandName == "Go")
            {
                txtContractNo.Text = string.Empty;
                if (!String.IsNullOrEmpty(txtFromDate.Text.Trim()))
                    fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (!String.IsNullOrEmpty(txtToDate.Text.Trim()))
                    toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                this.FromDate = FromDate;
                this.ToDate = toDate;
                lstCompanyWorkOrder = new List<CompanyWorkOrderDOM>();
                lstCompanyWorkOrder = companyWOBL.ReadCompanyOrderByDate(fromDate, toDate, contractNo);
                BindEmptyGrid(gvViewCWO);
                if (lstCompanyWorkOrder.Count > 0)
                {
                    gvViewCWO.DataSource = lstCompanyWorkOrder;
                    gvViewCWO.DataBind();
                }

            }

            if (e.CommandName == "Search")
            {
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                if (txtContractNo.Text.Trim() == String.Empty)
                {
                    Alert("Please Enter Company Work Order Number", LinkSearch);
                }
                else
                {
                    contractNo = txtContractNo.Text.Trim();
                    this.ContractNo = contractNo;
                    lstCompanyWorkOrder = new List<CompanyWorkOrderDOM>();
                    lstCompanyWorkOrder = companyWOBL.ReadCompanyOrderByDate(fromDate, toDate, contractNo);
                    BindEmptyGrid(gvViewCWO);
                    if (lstCompanyWorkOrder.Count > 0)
                    {
                        gvViewCWO.DataSource = lstCompanyWorkOrder;
                        gvViewCWO.DataBind();
                    }
                }
            }

        }

        protected void gvViewCWO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strStatus = string.Empty;
            strStatus = e.CommandArgument.ToString();
            string[] strSplit = strStatus.Split(',');
            int workOrderId = Convert.ToInt32(strSplit[0]);

            this.WorkOrderId = workOrderId;

            if (e.CommandName == "lnkContractNo")
            {

                lstWorkOderMapping = new List<WorkOrderMappingDOM>();
                lstWorkOderMapping = companyWOBL.ReadCompanyWorkOrderMapping(workOrderId);
                BindEmptyGrid(gvWorkOrder);
                if (lstWorkOderMapping.Count > 0)
                {
                    gvWorkOrder.DataSource = lstWorkOderMapping;
                    gvWorkOrder.DataBind();
                }

                lstBankGuarantee = new List<BankGuaranteeDOM>();
                lstBankGuarantee = companyWOBL.ReadCompanyWorkOrderBankGuarantee(workOrderId);
                BindEmptyGrid(gvBankGuarantee);
                List<BankGuaranteeDOM> lstDoc = new List<BankGuaranteeDOM>();
                BankGuaranteeDOM bankGuarantee = new BankGuaranteeDOM();
                if (lstBankGuarantee.Count > 0)
                {
                    gvBankGuarantee.DataSource = lstBankGuarantee;
                    gvBankGuarantee.DataBind();

                    foreach (BankGuaranteeDOM item in lstBankGuarantee)
                    {
                        String[] str = item.UploadedDocument.Split(',');
                        foreach (String doc in str)
                        {
                            if (!string.IsNullOrEmpty(doc))
                            {
                                String subStr = doc.Substring(20);
                                bankGuarantee.UploadedDocument = subStr;
                                bankGuarantee.UploadedDocumentPath = "BankGuaranteeDoc\\" + doc;
                                lstDoc.Add(bankGuarantee);
                            }
                        }
                    }

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                    if (hdfc != null)
                        updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                    else
                        updcFile.GetDocumentData(Int32.MinValue);
                    //gvDocuments.DataSource = lstDoc;
                    //gvDocuments.DataBind();
                }

                lstServiceDetail = new List<ServiceDetailDOM>();
                lstServiceDetail = companyWOBL.ReadCompanyWorkOrderServiceDetail(workOrderId);
                //BindEmptyGrid(gvServiceDetail);
                //if (lstServiceDetail.Count > 0)
                //{
                //    gvServiceDetail.DataSource = lstServiceDetail;
                //    gvServiceDetail.DataBind();
                //}

                ModalPopupExtender2.Show();
            }

            if (e.CommandName == "lnkDelete")
            {
                String errorMessage = string.Empty;
                errorMessage = companyWOBL.DeleteCompanyWorkOrder(workOrderId, LoggedInUser.UserLoginId);
                if (errorMessage == "")
                {

                    lstCompanyWorkOrder = new List<CompanyWorkOrderDOM>();
                    if (this.ContractNo == string.Empty && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
                    {
                        BindGrid(this.FromDate, this.ToDate);
                        this.FromDate = DateTime.MinValue;
                        this.ToDate = DateTime.MinValue;

                    }
                    else
                    {
                        lstCompanyWorkOrder = companyWOBL.ReadCompanyOrderByDate(this.FromDate, this.ToDate, this.ContractNo);
                        if (lstCompanyWorkOrder.Count > 0)
                        {
                            gvViewCWO.DataSource = lstCompanyWorkOrder;
                            gvViewCWO.DataBind();
                            ResetViewState();
                        }
                        else
                        {
                            GridViewEmptyText(gvViewCWO);
                        }
                    }


                }
                else
                {
                    Alert(errorMessage, gvViewCWO);
                }
            }

            if (e.CommandName == "lnkEdit")
            {
                Response.Redirect("~/Company/CompanyWorkOrder.aspx?companyWOId=" + workOrderId);
            }

            if (e.CommandName == "lnkGenerate")
            {

                int outId = 0;
                lstCompanyWorkOrder = new List<CompanyWorkOrderDOM>();

                outId = companyWOBL.UpdateCompanyWorkOrderStatus(workOrderId, Convert.ToInt16(StatusType.Generated), LoggedInUser.UserLoginId,null);

                if (outId > 0)
                {
                    //Alert("Company Work Order Generated Successfully", gvViewCWO);

                    lstCompanyWorkOrder = new List<CompanyWorkOrderDOM>();
                    if (this.ContractNo == string.Empty && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
                    {
                        BindGrid(this.FromDate, this.ToDate);
                        this.FromDate = DateTime.MinValue;
                        this.ToDate = DateTime.MinValue;

                    }
                    else
                    {
                        lstCompanyWorkOrder = companyWOBL.ReadCompanyOrderByDate(this.FromDate, this.ToDate, this.ContractNo);
                        if (lstCompanyWorkOrder.Count > 0)
                        {
                            gvViewCWO.DataSource = lstCompanyWorkOrder;
                            gvViewCWO.DataBind();
                            ResetViewState();
                        }
                        else
                        {
                            GridViewEmptyText(gvViewCWO);
                        }
                    }
                }
            }

            if (e.CommandName == "lnkPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtnPrint");
                Int32 statusType = Convert.ToInt32(strSplit[1]);
                //Response.Redirect(@"~\SSRReport\CompanyWorkOrderReport.aspx?workOrderId=" + workOrderId + "&" + "statusType=" + statusType);
                OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/CompanyWorkOrderReport.aspx?workOrderId=" + workOrderId + "&" + "statusType=" + statusType + "", "CompanyWorkOrder");
            }
        }

        protected void gvViewCWO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvViewCWO.PageIndex = e.NewPageIndex;

            lstCompanyWorkOrder = new List<CompanyWorkOrderDOM>();
            if (this.ContractNo == string.Empty && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
            {
                BindGrid(this.FromDate, this.ToDate);

            }
            else
            {
                lstCompanyWorkOrder = companyWOBL.ReadCompanyOrderByDate(this.FromDate, this.ToDate, this.ContractNo);

                if (lstCompanyWorkOrder.Count > 0)
                {
                    gvViewCWO.DataSource = lstCompanyWorkOrder;
                    gvViewCWO.DataBind();

                }
            }
            // BindGrid(this.FromDate, this.ToDate);
        }

        protected void gvViewCWO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnDelet = (LinkButton)e.Row.FindControl("lbtnDelete");
                LinkButton lbtnGenerate = (LinkButton)e.Row.FindControl("lbtnGenerate");
                LinkButton lbtnPrint = (LinkButton)e.Row.FindControl("lbtnPrint");
                int status = Convert.ToInt32(((HiddenField)e.Row.FindControl("approveStatus")).Value);
                Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");

                if (LoggedInUser.Role().Equals(AuthorityLevelType.Admin.ToString()))
                {
                    if (Convert.ToInt32(StatusType.Approved) == status)
                    {
                        lbtnEdit.Visible = false;
                        lbtnDelet.Visible = true;
                        lbtnGenerate.Visible = true;
                        lbtnPrint.Visible = true;
                    }
                    if (Convert.ToInt32(StatusType.Rejected) == status)
                    {
                        lbtnEdit.Visible = false;
                        lbtnDelet.Visible = false;
                        lbtnGenerate.Visible = false;
                        lbtnPrint.Visible = false;
                    }
                    if (Convert.ToInt32(StatusType.Generated) == status)
                    {
                        lbtnEdit.Visible = false;
                        lbtnDelet.Visible = true;
                        lbtnGenerate.Visible = false;
                        lbtnPrint.Visible = true;

                    }
                    if (Convert.ToInt32(StatusType.Pending) == status)
                    {
                        lbtnGenerate.Visible = false;
                        lbtnPrint.Visible = false;
                    }
                    if (Convert.ToInt32(StatusType.InComplete) == status)
                    {
                        lbtnGenerate.Visible = false;
                        lbtnPrint.Visible = false;
                    }
                }
                else
                {
                    if (Convert.ToInt32(StatusType.Pending) == status && lblCreatedBy.Text.Trim().Equals(LoggedInUser.UserLoginId))
                    {
                        lbtnDelet.Visible = true;
                        lbtnEdit.Visible = true;
                        lbtnGenerate.Visible = false;
                        lbtnPrint.Visible = false;
                    }
                    if (Convert.ToInt32(StatusType.InComplete) == status && lblCreatedBy.Text.Trim().Equals(LoggedInUser.UserLoginId))
                    {
                        lbtnDelet.Visible = true;
                        lbtnEdit.Visible = true;
                        lbtnGenerate.Visible = false;
                        lbtnPrint.Visible = false;
                    }
                    else
                    {
                        lbtnDelet.Visible = false;
                        lbtnEdit.Visible = false;
                        lbtnGenerate.Visible = false;
                        lbtnPrint.Visible = false;
                    }
                }

            }
        }

        protected void gvBankGuarantee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BankGuaranteeDOM bankGuarantee = new BankGuaranteeDOM();
                bankGuarantee = (BankGuaranteeDOM)e.Row.DataItem;
                Int32 workOrderId = bankGuarantee.CompanyWorkOrderId;
                lstBankGuarantee = new List<BankGuaranteeDOM>();
                lstBankGuarantee = companyWOBL.ReadCompanyWorkOrderBankGuarantee(workOrderId);
                List<BankGuaranteeDOM> lstDoc = new List<BankGuaranteeDOM>();
                foreach (BankGuaranteeDOM item in lstBankGuarantee)
                {
                    String[] str = item.UploadedDocument.Split(',');
                    foreach (String doc in str)
                    {
                        if (!string.IsNullOrEmpty(doc))
                        {
                            String subStr = doc.Substring(20);
                            bankGuarantee.UploadedDocument = subStr;
                            lstDoc.Add(bankGuarantee);
                        }
                    }
                }


                //gvDocuments.DataSource = lstDoc;
                //gvDocuments.DataBind();
            }
        }

        //protected void gvServiceDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvServiceDetail.EditIndex = e.NewPageIndex;
        //    lstServiceDetail = new List<ServiceDetailDOM>();
        //    lstServiceDetail = companyWOBL.ReadCompanyWorkOrderServiceDetail(this.WorkOrderId);
        //    BindEmptyGrid(gvServiceDetail);
        //    if (lstServiceDetail.Count > 0)
        //    {
        //        gvServiceDetail.DataSource = lstServiceDetail;
        //        gvServiceDetail.DataBind();
        //    }
        //}

        #endregion

        #region Private Methods

        public void BindGrid(DateTime fromDate, DateTime toDate)
        {
            lstCompanyWorkOrder = new List<CompanyWorkOrderDOM>();
            lstCompanyWorkOrder = companyWOBL.ReadCompanyOrderByDate(fromDate, toDate, null);
            if (lstCompanyWorkOrder.Count > 0)
            {
                gvViewCWO.DataSource = lstCompanyWorkOrder;
                gvViewCWO.DataBind();
            }
            else
            {
                BindEmptyGrid(gvViewCWO);
            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void ResetViewState()
        {
            this.FromDate = DateTime.MinValue;
            this.ToDate = DateTime.MinValue;
            this.ContractNo = string.Empty;
        }

        #endregion

        #region Public Properties

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

        public Int32 WorkOrderId
        {
            get { return Convert.ToInt32(ViewState["WorkOrderId"]); }

            set { ViewState["WorkOrderId"] = value; }
        }

        #endregion


    }
}