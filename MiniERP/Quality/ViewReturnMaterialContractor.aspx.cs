using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using DataAccessLayer;
using DataAccessLayer.Quality;
using BusinessAccessLayer.Quality;
using MiniERP.Shared;
using System.Configuration;


namespace MiniERP.Quality
{
    public partial class ViewReturnMaterialContractor : BasePage
    {
        #region Private Gloabl Varriable(s)

        Int32 DaysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        ContractorBL contractorBL = new ContractorBL();
        CompanyWorkOrderBL CompWorkOrderBL = new CompanyWorkOrderBL();
        //MaterialConsumptionNoteBAL materialConsumptionNoteBAL = null;
        ReturnMaterialContractorBL returnMaterilContractorBAL = new ReturnMaterialContractorBL();
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

        protected void lnkFromDate_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = String.Empty;
        }

        protected void lnkToDate_Click(object sender, EventArgs e)
        {
            txtToDate.Text = String.Empty;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            BindEmptyGrid(gvReturnMaterialContractor);
            ResetViewState();
            if (ddlContractNo.SelectedItem.Text != "--Select--")
                ContractNo = Convert.ToString(ddlContractNo.SelectedItem.Text);

            ContractorId = Convert.ToInt32(ddlContName.SelectedValue);

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
            if (ToDate < FromDate)
            {
                ShowMessageWithUpdatePanel("To Date should be Greater Than From Date");
            }
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, ReturnMaterialContractorNo);



        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            ResetViewState();
            ReturnMaterialContractorNo = txtRMCNo.Text;
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, ReturnMaterialContractorNo);

        }

        #endregion

        #region Private Method


        private void BindGrid(Int32? ReturnMaterialContractorId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String ReturnMaterialContractorNo)
        {
            List<MaterialConsumptionNoteDom> lstMaterialConsumption = new List<MaterialConsumptionNoteDom>();
            returnMaterilContractorBAL = new ReturnMaterialContractorBL();
            lstMaterialConsumption = returnMaterilContractorBAL.ReadReturnMaterialContractor(ReturnMaterialContractorId, ContractorId, ToDate, FromDate, ContractNo, ReturnMaterialContractorNo);
            if (lstMaterialConsumption.Count > 0)
            {

                gvReturnMaterialContractor.DataSource = lstMaterialConsumption;
                gvReturnMaterialContractor.DataBind();
            }
            else
            {
                BindEmptyGrid(gvReturnMaterialContractor);
            }
        }

        private void ResetControles()
        {
            ddlContName.SelectedValue = "0";
            ddlContractNo.SelectedValue = "0";
            txtFromDate.Text = String.Empty;
            txtToDate.Text = String.Empty;
            txtRMCNo.Text = String.Empty;
        }

        private void ResetViewState()
        {
            ReturnMaterialContractorId = 0;
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

        #region Public Property

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
            set
            {
                ViewState["ToDate"] = value;
            }
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

        public String ReturnMaterialContractorNo
        {
            get
            {
                try
                {
                    return (String)ViewState["ReturnMaterialContractorNo"];
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["ReturnMaterialContractorNo"] = value; }
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

        public Int32 ReturnMaterialContractorId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["ReturnMaterialContractorId"];
                }
                catch
                {
                    return 0;
                }
            }
            set { ViewState["ReturnMaterialContractorId"] = value; }
        }


        #endregion


        protected void gvReturnMaterialContractor_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("LinkDelete");
                LinkButton lnkGenQuot = (LinkButton)e.Row.FindControl("LinkGenQuot");
                LinkButton linkPrint = (LinkButton)e.Row.FindControl("LinkPrint");
                HiddenField HdfStatusId = (HiddenField)e.Row.FindControl("hdfStatusId");

                int status = Convert.ToInt32((HdfStatusId).Value);
                if (Convert.ToInt32(StatusType.Generated) == status)
                {

                    
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


        protected void gvReturnMaterialContractor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String Message;
            int Id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "lnkIRMCNoDetails")
            {
                lstitemTransaction = new List<ItemTransaction>();
                returnMaterilContractorBAL = new ReturnMaterialContractorBL();
                lstitemTransaction = returnMaterilContractorBAL.ReadReturnMaterialContractorMapping(Id);
                BindEmptyGrid(gvReturnMaterialContractor);
                if (lstitemTransaction.Count > 0)
                {
                    gvItemReturnMaterialContractor.DataSource = lstitemTransaction;
                    gvItemReturnMaterialContractor.DataBind();

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
            if (e.CommandName == "cmdGenerate")
            {
                materialConsumptionNoteDom = new MaterialConsumptionNoteDom();
                materialConsumptionNoteDom.IssueMaterial = new IssueMaterialDOM();
                materialConsumptionNoteDom.ReturnMaterialContractorId = Id;
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
                //quotation.StatusType = new MetaData();
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.GeneratedBy = LoggedInUser.UserLoginId;
                int outId = returnMaterilContractorBAL.UpdateReturnMaterialContractorStatus(materialConsumptionNoteDom);
                if (outId > 0)
                {
                    BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, ReturnMaterialContractorNo);
                    ShowMessageWithUpdatePanel("Issue Material Is Generated Successfully");
                } 

            }

            if (e.CommandName == "cmdPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("LinkPrint");
                Response.Redirect("~/SSRReport/ReturnMaterialContractorRpt.aspx?RMCNId=" + Id + "");
                //OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/ReturnMaterialContractorRpt.aspx?RMCNId=" + Id + "", "Material Consumption");
            }

            if (e.CommandName=="cmdDelete")
            {
                ReturnMaterialContractorBL returnMaterialContractorBL = new ReturnMaterialContractorBL();

                Message = returnMaterialContractorBL.DeleteReturnMaterialContractor(Id, LoggedInUser.UserLoginId, DateTime.Now);
                if (Message == "")
                {
                    ShowMessageWithUpdatePanel("Material Consumption Notes is Deleted Successfully");
                }
                else
                {
                    ShowMessageWithUpdatePanel(Message);
                }
                
            }
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, ReturnMaterialContractorNo);
        }

        
    }
}