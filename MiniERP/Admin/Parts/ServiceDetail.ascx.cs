using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.IO;
using System.Net;
using System.Data;
using System.Configuration;

namespace MiniERP.Parts
{
    public partial class ServiceDetail : System.Web.UI.UserControl
    {
        #region Global Variable(s)

        BasePage basePage = new BasePage();
        Int32 id = 0;
        CompanyWorkOrderBL companyWOBL = new CompanyWorkOrderBL();
        MetaDataBL metaDataBL = new MetaDataBL();

        List<ServiceDetailDOM> lstServiceDetail = new List<ServiceDetailDOM>();

        ServiceDetailDOM serviceDetail = null;
        #endregion

        #region Protected Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                basePage.BindDropDown(ddlUnit, "Name", "Id", metaDataBL.ReadMetaDataUnitMeasurement());
                SetValidationExpression();
                SetRegularExpressions();
                BindGrid();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                serviceDetail = GetFormData();
                serviceDetail.CreatedBy = basePage.LoggedInUser.UserLoginId;
                id = companyWOBL.CreateServiceDetail(serviceDetail);
                if (id > 0)
                {
                    BindGrid();
                    // basePage.Alert("Service Detail With Id " + id + " Created Successfully", btnSave);
                    ClearServiceDetail();
                    basePage.Alert("Service Detail Created Successfully", btnSave);
                }
                else
                {
                    basePage.Alert("Service Detail Already Exist", btnSave);
                }
            }
            if (btnSave.Text == "Update")
            {

                serviceDetail = GetFormData();
                serviceDetail.ModifiedBy = basePage.LoggedInUser.UserLoginId;
                id = companyWOBL.UpdateServiceDetail(serviceDetail, this.ServiceDetailId);
                if (id > 0)
                {
                    BindGrid();
                    //basePage.Alert("Service Detail With Id " + this.ServiceDetailId + " Updated Successfully", btnSave);
                    basePage.Alert("Service Detail Updated Successfully", btnSave);
                    ClearServiceDetail();
                }
                else
                {
                    basePage.Alert("Service Detail Already Exist", btnSave);
                }
            }

        }

        protected void btnResetSD_Click(object sender, EventArgs e)
        {
            ClearServiceDetail();
        }

        protected void gvServiceDetail_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
          //  gvServiceDetail.EditIndex = e.NewPageIndex;
            gvServiceDetail.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvServiceDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int serviceDetailId = Convert.ToInt32(e.CommandArgument);
            this.ServiceDetailId = serviceDetailId;
            if (e.CommandName == "lnkEdit")
            {
                lstServiceDetail = companyWOBL.ReadCompanyWorkOrderServiceDetail(serviceDetailId);

                // ddlWONumber.SelectedValue = lstServiceDetail[0].WorkOrderNumber.ToString();

                txtItemNumber.Text = lstServiceDetail[0].ItemNumber.ToString();
                txtServiceNumber.Text = lstServiceDetail[0].ServiceNumber.ToString();
                txtServiceDesc.Text = lstServiceDetail[0].ServiceDescription;
                ddlUnit.SelectedValue = lstServiceDetail[0].Unit.Id.ToString();
                txtUnitRate.Text = lstServiceDetail[0].UnitRate.ToString();
                txtQuantity.Text = lstServiceDetail[0].Quantity.ToString();
                txtApplicableRate.Text = lstServiceDetail[0].ApplicableRate.ToString();

                btnSave.Text = "Update";
            }

            if (e.CommandName == "lnkDelete")
            {
                String msg = companyWOBL.DeleteServiceDetail(serviceDetailId, basePage.LoggedInUser.UserLoginId);
                if (true)
                {
                    basePage.Alert("Service Detail Deleted Successfully", gvServiceDetail);
                }
                else
                {
                    basePage.Alert("Service Detail Is Used On Another Page", gvServiceDetail);
                }

                BindGrid();
                ClearServiceDetail();
            }
        }

        #endregion


        #region Private Methods

        private ServiceDetailDOM GetFormData()
        {
            serviceDetail = new ServiceDetailDOM();
            serviceDetail.WorkOrderNumber = txtWONumber.Text.Trim();
            serviceDetail.ItemNumber = txtItemNumber.Text.Trim();
            serviceDetail.ServiceNumber = txtServiceNumber.Text.Trim();
            serviceDetail.ServiceDescription = txtServiceDesc.Text.Trim();
            serviceDetail.Unit = new MetaData();
            //Hard Coded
            serviceDetail.Unit.Id = 1;
            //serviceDetail.Unit.Id = Convert.ToInt32(ddlUnit.SelectedValue);
            serviceDetail.Unit.Name = ddlUnit.SelectedItem.Text;
            if (!String.IsNullOrEmpty(txtQuantity.Text))
            {
                serviceDetail.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
            }
            else
            {
                serviceDetail.Quantity = 0;
            }
            if (!String.IsNullOrEmpty(txtUnitRate.Text))
            {
                serviceDetail.UnitRate = Convert.ToDecimal(txtUnitRate.Text.Trim());
            }
            else
            {
                serviceDetail.UnitRate = 0;
            }
            if (!String.IsNullOrEmpty(txtApplicableRate.Text))
            {
                serviceDetail.ApplicableRate = Convert.ToDecimal(txtApplicableRate.Text.Trim());
            }
            else
            {
                serviceDetail.ApplicableRate = 0;
            }

            return serviceDetail;
        }

        private void ClearServiceDetail()
        {
            //ddlWONumber.SelectedValue = "0";
            txtItemNumber.Text = string.Empty;
            txtServiceNumber.Text = string.Empty;
            txtServiceDesc.Text = string.Empty;
            ddlUnit.SelectedValue = "0";
            txtQuantity.Text = string.Empty;
            txtUnitRate.Text = string.Empty;
            txtApplicableRate.Text = string.Empty;
            btnSave.Text = "Save";
        }

        private void SetValidationExpression()
        {
            rvQuantity.ErrorMessage = "Quantity Should Be Numeric";
            rvRate.ErrorMessage = "Unit Rate Should Be Numeric";
            rvAppRate.ErrorMessage = "Applicable Rate Should Be Numeric";
        }
        private void SetRegularExpressions()
        {
            revServiceDesc.ValidationExpression = ValidationExpression.C_DESCRIPTION;
            revServiceDesc.ErrorMessage = "More than 80 Characters are not Allowed in Delivery Description";
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void BindGrid()
        {

            lstServiceDetail = new List<ServiceDetailDOM>();
            lstServiceDetail = companyWOBL.ReadCompanyWorkOrderServiceDetail(null);
            if (lstServiceDetail.Count > 0)
            {
                gvServiceDetail.DataSource = lstServiceDetail;
                gvServiceDetail.DataBind();
            }
        }

        #endregion

        #region Public Properties

        public int ServiceDetailId
        {
            get { return Convert.ToInt32(ViewState["ServiceDetailId"]); }
            set { ViewState["ServiceDetailId"] = value; }
        }

        #endregion

        protected void gvServiceDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.NewEditIndex = -1;
        }
    }
}