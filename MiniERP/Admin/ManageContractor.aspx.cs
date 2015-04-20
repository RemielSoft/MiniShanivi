using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;

namespace MiniERP.Admin
{
    public partial class ManageContractor : BasePage
    {
        #region private Global Variables
        ContractorBL contractorBL = new ContractorBL();
        Contractor contractor = new Contractor();
        SearchBAL searchBL = new SearchBAL();
        #endregion

        #region protected Methods
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Convert.ToString(Session["Flag"]) == "3")
            {
                pnlSearch.Visible = false;
                contractorIds = Session["ContractorId"].ToString();
                BindgvSearchContractor();
                btnSearch.Text = "Finish";
                btnSave.Visible = false;
                btnCancel.Visible = false;
                Session["Flag"] = null;
                
                if (contractorIds == "0")
                {
                    GridViewEmptyText(gvContractor);
                    BindgvSearchContractor();
                }
            }

            if (!IsPostBack)
            {
                Session.Remove(contractorIds);
                Session["Flag"] = null;
                if (btnSearch.Text == "Search")
                {
                    BindgvContractor();
                }
                txtcontractorName.Focus();
                SetValidationExp();
                
                
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id;

            id = CreateContractor(GetContractorDetail());
            if (id > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_CONTRACTOR_MESSAGE);
                ClearContractor();
            }
            else if (id == 0)
            {
                ShowMessageWithUpdatePanel("Contractor Name or PAN is already exist");

            }
            BindgvContractor();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearContractor();
            
        }
        protected void gvContractor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int contrid = 0;
            string msg = string.Empty;
            contrid = Convert.ToInt32(e.CommandArgument);
            contractorId = contrid;   
            if (e.CommandName == "cmdEdit")
            {                
                GetContractorById(contractorId);
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
                txtcontractorName.Focus();
                //BindgvContractor();
                if (btnSearch.Text == "Finish")
                {
                    pnlSearch.Visible = true;
                   // BindgvSearchContractor();

                }
                
            }
            else if (e.CommandName == "cmdDelete")
            {
                msg = DeleteContractor(contractorId);
                if (!String.IsNullOrEmpty(msg))
                {
                    ShowMessageWithUpdatePanel(msg);
                }
                else
                {
                    ShowMessageWithUpdatePanel("Contractor deleted sucessfully");
                }
                ClearContractor();
                //btnSave.Visible = false;
                //btnUpdate.Visible = false;
                if (btnSearch.Text == "Finish")
                {
                    //pnlSearch.Visible = false;
                    BindgvSearchContractor();
                }
                else
                {
                     BindgvContractor();
                }
            }
            if (e.CommandName =="Details")
            {
                //if (btnSearch.Text == "Finish")
                //{
                //    pnlSearch.Visible = false;
                //}
                //else
                //{
                //    pnlSearch.Visible = true;
                //}
                List<Contractor> lstContractor = new List<Contractor>();
                lstContractor = contractorBL.ReadContractor(contrid);
                lblName.Text = lstContractor[0].Name.ToString();
                lblAddress.Text = lstContractor[0].Address.ToString();
                lblState.Text = lstContractor[0].State.ToString();
                lblMobile.Text = lstContractor[0].Mobile.ToString();
                lblPhone.Text = lstContractor[0].Phone.ToString();
                lblDes.Text = lstContractor[0].Description.ToString();
                lblEmailId.Text = lstContractor[0].Email.ToString();
                lblWebsite.Text = lstContractor[0].Website.ToString();
                lblCity.Text = lstContractor[0].City.ToString();
                lblPcode.Text = lstContractor[0].PinCode.ToString();
                contractor.Information = new Information();
                lblVendorCode.Text = lstContractor[0].Information.VendorCode;
                lblPanNo.Text =Convert.ToString(lstContractor[0].Information.PanNumber);
                lblTan.Text =Convert.ToString( lstContractor[0].Information.TanNumber);
                lblServiceNo.Text =Convert.ToString( lstContractor[0].Information.ServiceTaxNumber);
                lblPf.Text =Convert.ToString( lstContractor[0].Information.PfNumber);
                lblFax.Text =Convert.ToString( lstContractor[0].Information.FaxNumber);
                lblEsi.Text =Convert.ToString( lstContractor[0].Information.EsiNumber);
                lblContactPName.Text = Convert.ToString(lstContractor[0].Information.ContactPersonName);
                lblContactPEmail.Text = Convert.ToString(lstContractor[0].Information.ContactPersonEmailId);
                lblContactPMobile.Text = Convert.ToString(lstContractor[0].Information.ContactPersonMobileNo);
                lblContactPPhone.Text = Convert.ToString(lstContractor[0].Information.ContactPersonPhoneNo);
                ModalPopupExtender2.Show();
                //if (btnSearch.Text == "Finish")
                //{
                //    pnlSearch.Visible = false;
                //    BindgvSearchContractor();
                //}
                
            }
            if (btnSearch.Text == "Finish")
            {
                //pnlSearch.Visible = true;
                BindgvSearchContractor();
            }
            else
            {
                BindgvContractor();
            }
           // BindgvContractor();
        }        
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id;

            id = UpdateContractor(UpdateContractorDetail());
            if (id > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_CONTRACTOR_MESSAGE);
                ClearContractor();
                //btnSave.Visible = true;
                btnUpdate.Visible = false;
                //btnCancel.Visible = true;
                
            }
            else if (id == 0)
            {
                ShowMessageWithUpdatePanel("Contractor Name or PAN is already exist");

            }
            if (btnSearch.Text == "Finish" && id>0)
            {
                //pnlSearch.Visible = true;
                BindgvSearchContractor();
                btnSave.Visible = false;
            }
            else
            {
                pnlSearch.Visible = true;
                BindgvContractor();
            }
            //BindgvContractor();
            //BindgvSearchContractor();
            //Session["ListContractor"] = null;
            
        }
        protected void gvContractor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContractor.PageIndex = e.NewPageIndex;
            if (btnSearch.Text == "Finish")
            {
                BindgvSearchContractor();
            }
            else
            {
                BindgvContractor();
            }
            BindgvContractor();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text=="Search")
            {
                OpenPopupWithUpdatePanel(btnSearch, "SearchItemStock.aspx?PageName=ManageContractor", "Title");
                //btnSearch.Text = "Finish";
            }
            else if (btnSearch.Text=="Finish")
            {
                btnSearch.Text = "Search";
                pnlSearch.Visible = true;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                Session["Flag"] = null;
                BindgvContractor();
                ClearContractor();
            }
        }
        #endregion

        #region Private Metods
        public void SetValidationExp()
        {
            
              //revContractorName.ValidationExpression = ValidationExpression.C_ADDRESS;

           // revContractorName.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ADDRESS;
            //revAddress.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revAddress.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ADDRESS;
            revCity.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            revCity.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_CITY;
            revState.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            revState.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_STATE;
            revWebsite.ValidationExpression = ValidationExpression.WEB_URL;
            revWebsite.ErrorMessage = "Invalid Website Name";
            revEmail.ValidationExpression = ValidationExpression.C_EMAIL_ID;
            revEmail.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_EMAILID;
            revMobile.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revMobile.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_MOBILE_NUMBER;
            revPin.ValidationExpression = ValidationExpression.C_PIN_CODE;
            revPin.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PIN_CODE;
            revPhn.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revPhn.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PHONE_NUMBER;
            revEsi.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revEsi.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ESI_ALPHANUMERIC;
            revService.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revService.ErrorMessage = "Service Tax Number Should Be Alphanumeric";
            revPan.ValidationExpression = ValidationExpression.C_PAN_NUMBER;
            revPan.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PAN_ALPHANUMERIC;
            revTan.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revTan.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_TAN_ALPHANUMERIC;
            revFax.ValidationExpression = ValidationExpression.C_NUMERIC;
            revFax.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_FAX_NUMERIC;
            revPf.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revPf.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PF_ALPHANUMERIC;
            //revContactName.ValidationExpression = ValidationExpression.C_ADDRESS;
           // revContactName.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ADDRESS;
            revContactEmail.ValidationExpression = ValidationExpression.C_EMAIL_ID;
            revContactEmail.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_EMAILID;
            revContactMobile.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revContactMobile.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_MOBILE_NUMBER;
            revContactPhn.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revContactPhn.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PHONE_NUMBER;
            //revdescription.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revdescription.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_DESCRIPTION;
        }
        private string DeleteContractor(int contractorid)
        {
            return contractorBL.DeleteContractor(contractorid, LoggedInUser.UserLoginId, DateTime.Now);
        }
        private void ClearContractor()
        {
            txtcontractorName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtPin.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPf.Text = string.Empty;
            txtPan.Text = string.Empty;
            txtTan.Text = string.Empty;
            txtServicetax.Text = string.Empty;
            txtEsi.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtContactPPhone.Text = string.Empty;
            txtContactPName.Text = string.Empty;
            txtContactPMobile.Text = string.Empty;
            txtContactPEmail.Text = string.Empty;
            if (btnSearch.Text == "Finish")
            {
                pnlSearch.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
            else
            {
                btnSave.Visible = true;
                btnCancel.Visible = true;
            }
            //btnSave.Visible = true;
            btnUpdate.Visible = false;
            txtcontractorName.Focus();
        }        
        private void BindgvContractor()
        {            
            gvContractor.DataSource = GetContractor();
            gvContractor.DataBind();
        }
        private void BindgvSearchContractor()
        {
            List<Contractor> lstContractor = new List<Contractor>();
            lstContractor = searchBL.SearchContractorById(contractorIds);
            gvContractor.DataSource = lstContractor;

            gvContractor.DataBind();
        }
        private Contractor GetContractorDetail()
        {

            Contractor contractor = new Contractor();            
            contractor.Name = RemoveWhiteSpace(txtcontractorName.Text.Trim());
            contractor.Email = txtEmail.Text.Trim();
            contractor.Address = txtAddress.Text.Trim();            
            contractor.City = txtCity.Text.Trim();
            contractor.State = txtState.Text.Trim();
            contractor.PinCode = txtPin.Text.Trim();
            contractor.Phone = txtPhone.Text.Trim();
            contractor.Mobile = txtMobile.Text.Trim();
            contractor.Website = txtWebsite.Text.Trim();
            contractor.Description = txtDescription.Text.Trim();
            contractor.Information = new Information();
            contractor.Information.TanNumber = txtTan.Text.Trim();
            contractor.Information.PanNumber = txtPan.Text.Trim();
            contractor.Information.EsiNumber = txtEsi.Text.Trim();
            contractor.Information.ServiceTaxNumber = txtServicetax.Text.Trim();
            contractor.Information.FaxNumber = txtFax.Text.Trim();
            contractor.Information.PfNumber = txtPf.Text.Trim();
            contractor.Information.ContactPersonName =txtContactPName.Text.Trim();
            contractor.Information.ContactPersonEmailId = txtContactPEmail.Text.Trim();
            contractor.Information.ContactPersonMobileNo = txtContactPMobile.Text.Trim();
            contractor.Information.ContactPersonPhoneNo = txtContactPPhone.Text.Trim();
            contractor.CreatedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;            
            return contractor;
        }
        private Contractor UpdateContractorDetail()
        {

            Contractor contractor = new Contractor();
            contractor.ContractorId = contractorId;
            contractor.Name = RemoveWhiteSpace(txtcontractorName.Text.Trim());
            contractor.Email = txtEmail.Text.Trim();
            contractor.Address = txtAddress.Text.Trim();
            contractor.City = txtCity.Text.Trim();
            contractor.State = txtState.Text.Trim();
            contractor.PinCode = txtPin.Text.Trim();
            contractor.Phone = txtPhone.Text.Trim();
            contractor.Mobile = txtMobile.Text.Trim();
            contractor.Website = txtWebsite.Text.Trim();
            contractor.Description = txtDescription.Text.Trim();
            contractor.Information = new Information();
            contractor.Information.TanNumber = txtTan.Text.Trim();
            contractor.Information.PanNumber = txtPan.Text.Trim();
            contractor.Information.EsiNumber = txtEsi.Text.Trim();
            contractor.Information.ServiceTaxNumber = txtServicetax.Text.Trim();
            contractor.Information.FaxNumber = txtFax.Text.Trim();
            contractor.Information.PfNumber = txtPf.Text.Trim();
            contractor.Information.ContactPersonName = txtContactPName.Text.Trim();
            contractor.Information.ContactPersonEmailId = txtContactPEmail.Text.Trim();
            contractor.Information.ContactPersonMobileNo = txtContactPMobile.Text.Trim();
            contractor.Information.ContactPersonPhoneNo = txtContactPPhone.Text.Trim();
            contractor.ModifiedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;            
            contractor.ModifiedDate = DateTime.Now;
            return contractor;
        }        
        private int CreateContractor(Contractor contractor)
        {            
            int id = contractorBL.CreateContractor(contractor);
            return id;
        }
        private int UpdateContractor(Contractor contractor)
        {
            int id = contractorBL.UpdateContractor(contractor);
            return id;
        }
        private List<Contractor> GetContractor()
        {
            List<Contractor> lstContractor = new List<Contractor>();
            lstContractor = contractorBL.ReadContractor(null);
            return lstContractor;
        }
        private void GetContractorById(Int32 id)
        {
            id = contractorId;
            List<Contractor> lstContractor = new List<Contractor>();
            lstContractor = contractorBL.ReadContractor(id);
            txtcontractorName.Text = lstContractor[0].Name.ToString();
            txtEmail.Text = lstContractor[0].Email.ToString();
            txtAddress.Text = lstContractor[0].Address.ToString();
            txtCity.Text = lstContractor[0].City.ToString();
            txtState.Text = lstContractor[0].State.ToString();
            txtPin.Text = lstContractor[0].PinCode.ToString();
            txtPhone.Text = lstContractor[0].Phone.ToString();
            txtMobile.Text = lstContractor[0].Mobile.ToString();
            txtWebsite.Text = lstContractor[0].Website.ToString();
            txtDescription.Text = lstContractor[0].Description.ToString();
            contractor.Information = new Information();
            txtPan.Text = Convert.ToString(lstContractor[0].Information.PanNumber);
            txtTan.Text = Convert.ToString(lstContractor[0].Information.TanNumber);
            txtServicetax.Text = Convert.ToString(lstContractor[0].Information.ServiceTaxNumber);
            txtPf.Text = Convert.ToString(lstContractor[0].Information.PfNumber);
            txtFax.Text = Convert.ToString(lstContractor[0].Information.FaxNumber);
            txtEsi.Text = Convert.ToString(lstContractor[0].Information.EsiNumber);
            txtContactPName.Text = Convert.ToString(lstContractor[0].Information.ContactPersonName);
            txtContactPEmail.Text = Convert.ToString(lstContractor[0].Information.ContactPersonEmailId);
            txtContactPMobile.Text = Convert.ToString(lstContractor[0].Information.ContactPersonMobileNo);
            txtContactPPhone.Text = Convert.ToString(lstContractor[0].Information.ContactPersonPhoneNo);
        }   
        
        #endregion

        # region  privateProperities
        /// <summary>
        /// Gets or sets the Group id.
        /// </summary>
        /// <value>The Group id.</value>
        public int contractorId
        {
            get
            {
                return (int)ViewState["contractorId"];
            }
            set
            {
                ViewState["contractorId"] = value;
            }
        }
        public string contractorIds
        {
            get
            {
                return (string)ViewState["contractorIds"];
            }
            set
            {
                ViewState["contractorIds"] = value;
            }
        }
        #endregion 

        

       
              
    }
}