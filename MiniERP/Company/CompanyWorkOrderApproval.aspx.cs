using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Configuration;

namespace MiniERP.Company
{
    public partial class CompanyWorkOrderApproval : BasePage
    {
        #region Global Variable(s)

        bool track;

        Int32 daysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        CompanyWorkOrderBL companyWOBL = new CompanyWorkOrderBL();
        QuotationBL quotationBL = new QuotationBL();
        CompanyWorkOrderDOM companyWorkorder = new CompanyWorkOrderDOM();
        

        List<CompanyWorkOrderDOM> lstCompanyWorkOrder = null;
        List<WorkOrderMappingDOM> lstWorkOderMapping = null;
        List<BankGuaranteeDOM> lstBankGuarantee = null;
        List<ServiceDetailDOM> lstServiceDetail = null;
        QuotationDOM quotation = null;


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
                var list = quotationBL.ReadQuotationStatusMetaData().Where(p => p.Name != (StatusType.InComplete).ToString());
                BindDropDownData(ddlStatus, "Name", "Id", list);
                BindGrid(Convert.ToInt32(StatusType.Pending));
            }


        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void ddlStatusSelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            BindGrid(statusId);
            ButtonVisibility(statusId);
        }

        protected void gvViewCWO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int workOrderId = Convert.ToInt32(e.CommandArgument);

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
                if (lstBankGuarantee.Count > 0)
                {
                    gvBankGuarantee.DataSource = lstBankGuarantee;
                    gvBankGuarantee.DataBind();
                }

                lstServiceDetail = new List<ServiceDetailDOM>();
                lstServiceDetail = companyWOBL.ReadCompanyWorkOrderServiceDetail(workOrderId);

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);
                //BindEmptyGrid(gvServiceDetail);
                //if (lstServiceDetail.Count > 0)
                //{
                //    gvServiceDetail.DataSource = lstServiceDetail;
                //    gvServiceDetail.DataBind();
                //}

                ModalPopupExtender2.Show();
            }

        }

        protected void btnApproveReject_Click(object sender, CommandEventArgs e)
        {

            GetSelectedData(e.CommandName);

            BindGrid(Convert.ToInt32(ddlStatus.SelectedValue));


        }

        protected void chbxSelectAll(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvViewCWO.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbxCWO");
                hdf = (HiddenField)row.FindControl("hdfCWOId");
                if (chbx != null && hdf != null)
                {
                    chbx.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void chbxSelect(object sender, EventArgs e)
        {
            track = false;
            CheckBox chb = (CheckBox)gvViewCWO.HeaderRow.FindControl("chbxSelectAll");
            foreach (GridViewRow row in gvViewCWO.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbxCWO");
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

        protected void gvViewCWO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvViewCWO.PageIndex = e.NewPageIndex;
            BindGrid(Convert.ToInt32(ddlStatus.SelectedValue));
        }

        protected void gvServiceDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

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

        private void BindGrid(Int32 statusId)
        {
            lstCompanyWorkOrder = new List<CompanyWorkOrderDOM>();
            lstCompanyWorkOrder = companyWOBL.ReadCompWorkOrderByStatusId(statusId);
            if (lstCompanyWorkOrder.Count > 0)
            {
                gvViewCWO.DataSource = lstCompanyWorkOrder;
                gvViewCWO.DataBind();
                if (statusId != Convert.ToInt32(StatusType.Pending))
                {

                    gvViewCWO.Columns[0].Visible = false;
                }
                else
                {
                    gvViewCWO.Columns[0].Visible = true;
                }
            }
            else
            {
                BindEmptyGrid(gvViewCWO);
            }


        }

        private void GetSelectedData(String action)
        {
            Int32 workOrderId = 0;
            Int32 statusId = 0;
            if (Convert.ToInt32(ddlStatus.SelectedValue) == (Convert.ToInt32(StatusType.Pending)))
            {
                foreach (GridViewRow row in gvViewCWO.Rows)
                {
                    chbx = (CheckBox)row.FindControl("chbxCWO");
                    hdf = (HiddenField)row.FindControl("hdfCWOId");
                    txtRemarkReject = (TextBox)row.FindControl("txtRemarkReject");
                    
                    if (chbx != null && hdf != null)
                    {
                        if (chbx.Checked.Equals(true))
                        {
                            workOrderId = Convert.ToInt32(hdf.Value);
                           // quotation.RemarkReject = txtRemarkReject.Text.ToString();
                            companyWorkorder.RemarkReject = txtRemarkReject.Text.ToString();

                            if (action == "Approve")
                                statusId = Convert.ToInt32(StatusType.Approved);
                                
                            else if (action == "Reject")
                                statusId = Convert.ToInt32(StatusType.InComplete);

                            companyWOBL.UpdateCompanyWorkOrderStatus(workOrderId, statusId, LoggedInUser.UserLoginId, companyWorkorder.RemarkReject);

                        }
                    }
                }
            }
            if (workOrderId == 0)
            {
                if (action == "Approve")
                    Alert("Kindly Select Company Work Order Number.", btnApprove);
                else if (action == "Reject")
                    Alert("Kindly Select Company Work Order Number.", btnReject);
            }
        }

        private void ButtonVisibility(Int32 status)
        {
            //on Empty Grid is to be also not visible
            if (status == Convert.ToInt32(StatusType.Pending))
            {
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
        }

        #endregion



        
    }
}