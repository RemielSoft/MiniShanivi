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
    public partial class ManageSupplier :BasePage
    {

        #region private Global Variables
        SupplierBL supplierBL = new SupplierBL();
        Supplier supplier = new Supplier();
        SearchBAL searchBL = new SearchBAL();
        #endregion

        #region protected Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Flag"]) == "4")
            {
                pnlSearch.Visible = false;
                supplierrIds = Session["supplierId"].ToString();
                BindgvSearchSupplier();
                btnSearch.Text = "Finish";
                btnSave.Visible = false;
                Session["Flag"] = null;
                if (supplierrIds == "0")
                {
                    GridViewEmptyText(gvSupplier);
                    BindgvSearchSupplier();
                }
            }
            if (!IsPostBack)
            {
                Session.Remove(supplierrIds);
                Session["Flag"] = null;
                
                if (btnSearch.Text == "Search")
                {
                    
                    BindgvSupplier();
                }
                
                txtSupplier.Focus();
                //BindgvSupplier();
                SetValidationExp();
               
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id;

            id = CreateSupplier(GetSupplierDetail());
            if (id > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_SUPPLIER_MESSAGE);
                ClearSupplier();
            }
            else if (id == 0)
            {
                ShowMessageWithUpdatePanel("Supplier Name already exist.");
            }
            
           BindgvSupplier();
        }
        protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string msg = string.Empty;
            int id = Convert.ToInt32(e.CommandArgument);
            supplierId = id;
            if (e.CommandName == "cmdEdit")
            {
                GetSupplierById(supplierId);
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
                if (btnSearch.Text == "Finish")
                {
                    pnlSearch.Visible = true;
                    // BindgvSearchContractor();

                }
            }
            else if (e.CommandName == "cmdDelete")
            {
                msg = DeleteSupplier(supplierId);
                
                if (!String.IsNullOrEmpty(msg))
                {
                    ShowMessageWithUpdatePanel(msg);
                }
                else
                {
                    ShowMessageWithUpdatePanel("Supplier deleted sucessfully");
                }
                ClearSupplier();
                //btnSave.Visible = true;
                //btnUpdate.Visible = false;
                if (btnSearch.Text == "Finish")
                {
                    //pnlSearch.Visible = true;
                    BindgvSearchSupplier();
                }
                else
                {
                    BindgvSupplier();
                }
            }
            if (e.CommandName == "Details")
            {
                //if (btnSearch.Text == "Finish")
                //{
                //    pnlSearch.Visible = false;
                //}
                //else
                //{
                //    pnlSearch.Visible = true;
                //}
                List<Supplier> lstSupplier = new List<Supplier>();
                lstSupplier = supplierBL.ReadSupplier(id);
                lblName.Text = lstSupplier[0].Name.ToString();
                lblAddress.Text = lstSupplier[0].Address.ToString();              
                lblCity.Text = lstSupplier[0].City.ToString();
                lblPcode.Text = lstSupplier[0].PinCode.ToString();
                lblMobile.Text = lstSupplier[0].Mobile.ToString();
                lblWebsite.Text = lstSupplier[0].Website.ToString();
                lblEmailId.Text = lstSupplier[0].Email.ToString();
                lblState.Text = lstSupplier[0].State.ToString();
                supplier.Information = new Information();
                lblPanNo.Text =Convert.ToString( lstSupplier[0].Information.PanNumber);
                lblTan.Text =Convert.ToString( lstSupplier[0].Information.TanNumber);
                lblPhn.Text =Convert.ToString( lstSupplier[0].Phone);
                lblPf.Text =Convert.ToString( lstSupplier[0].Information.PfNumber);
                lblFax.Text =Convert.ToString( lstSupplier[0].Information.FaxNumber);
                lblEsi.Text =Convert.ToString( lstSupplier[0].Information.EsiNumber);
                lblContactPName.Text = Convert.ToString(lstSupplier[0].Information.ContactPersonName);
                lblContactPEmail.Text = Convert.ToString(lstSupplier[0].Information.ContactPersonEmailId);
                lblContactPMobile.Text = Convert.ToString(lstSupplier[0].Information.ContactPersonMobileNo);
                lblContactPPhone.Text = Convert.ToString(lstSupplier[0].Information.ContactPersonPhoneNo);
                lblVendorCode.Text=lstSupplier[0].Information.VendorCode.ToString();
                ModalPopupExtender2.Show();
               
            }
            if (btnSearch.Text == "Finish")
            {
                //pnlSearch.Visible = true;
                BindgvSearchSupplier();
            }
            else 
            {
                //pnlSearch.Visible = true;
                BindgvSupplier();
            }
            
            //BindgvSupplier();
        }
        protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupplier.PageIndex = e.NewPageIndex;
            BindgvSupplierView(supplierId);
            
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id;
            
            id = UpdateSupplier(UpdateSupplierDetail());
            if (id > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_SUPPLIER_MESSAGE);
                ClearSupplier();
                //btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            else if (id == 0)
            {
                ShowMessageWithUpdatePanel("Supplier Name already exist.");

            }
            if (btnSearch.Text == "Finish")
            {
               
                //pnlSearch.Visible = true;
                BindgvSearchSupplier();
                btnSave.Visible = false;
            }
            else
            {
                pnlSearch.Visible = true;
                BindgvSupplier();
            }
           // BindgvSupplier();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearSupplier();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text=="Search")
            {
                OpenPopupWithUpdatePanel(btnSearch, "SearchItemStock.aspx?PageName=ManageSupplier", Title);
                
            }
            else if (btnSearch.Text=="Finish")
            {
                btnSearch.Text = "Search";
                pnlSearch.Visible = true;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                Session["Flag"] = null;
                BindgvSupplier();
                ClearSupplier();
            }
        }

        #endregion

        #region Private Metods
        private void SetValidationExp()
        {

            //revSupplierName.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revSupplierName.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ADDRESS;
            revEmail.ValidationExpression = ValidationExpression.C_EMAIL_ID;
            revEmail.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_EMAILID;
            revService.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revService.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ALPHANUMERIC;
            revPin.ValidationExpression = ValidationExpression.C_NUMERIC;
            revPin.ErrorMessage = "Pin Code Should be Numeric";
            revMobile.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revMobile.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_MOBILE_NUMBER;
            revCity.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            revCity.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_CITY;
            revState.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            revState.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_STATE;
            revPhn.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revPhn.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PHONE_NUMBER;
            revEsi.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revEsi.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ESI_ALPHANUMERIC;
            revPan.ValidationExpression = ValidationExpression.C_PAN_NUMBER;
            revPan.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PAN_ALPHANUMERIC;
            revTan.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revTan.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_TAN_ALPHANUMERIC;
            revFax.ValidationExpression = ValidationExpression.C_NUMERIC;
            revFax.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_FAX_NUMERIC;
            revPf.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revPf.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PF_ALPHANUMERIC;
            revWebsite.ValidationExpression = ValidationExpression.WEB_URL;
            revWebsite.ErrorMessage = "Invalid Website Name";
            //revContactName.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revContactName.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ADDRESS;
            revContactEmail.ValidationExpression = ValidationExpression.C_EMAIL_ID;
            revContactEmail.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_EMAILID;
            revContactMobile.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revContactMobile.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_MOBILE_NUMBER;
            revContactPhone.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revContactPhone.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_PHONE_NUMBER;
        }
        private string DeleteSupplier(int supplierid)
        {
            return supplierBL.DeleteSupplier(supplierid, LoggedInUser.UserLoginId, DateTime.Now);
        }
        private void ClearSupplier()
        {
            txtSupplier.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtPin.Text = string.Empty;            
            txtMobile.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtEsi.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtPan.Text = string.Empty;
            txtTan.Text = string.Empty;
            txtPf.Text = string.Empty;
            txtPhn.Text = string.Empty;
            txtContactPPhone.Text = string.Empty;
            txtContactPName.Text = string.Empty;
            txtContactPMobile.Text = string.Empty;
            txtContactPEmail.Text = string.Empty;
            txtSupplier.Focus();
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
            btnUpdate.Visible = false;
            
        }
        private void BindgvSupplierView(int supplierid)
        {
            Supplier supplier = new Supplier();
            List<Supplier> lstSupplier = new List<Supplier>();
            
                lstSupplier = supplierBL.ReadSupplier(null);
               
                 
                if (lstSupplier.Count > 0)
                {
                    gvSupplier.DataSource = lstSupplier;
                    gvSupplier.DataBind();
                }
                else
                {
                    BindEmptyGrid(gvSupplier);
                }
          
        }

        private void LabelAssociation()
        {
            if (PageType == "Title")
            {

                //lbl_Invoice_Mapping.Text = "Contractor Invoice";
                //lbl_ViewInvoice.Text = "View Contractor Invoice";
            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }
        private void BindgvSupplier()
        {
           
            gvSupplier.DataSource = GetSupplier();
            gvSupplier.DataBind();
        }
        private void BindgvSearchSupplier()
        {
            List<Supplier> lstSupplier = new List<Supplier>();
              lstSupplier = searchBL.SearchSupplierById(supplierrIds);
               
                gvSupplier.DataSource = lstSupplier;
                gvSupplier.DataBind();
            
        }
        private Supplier GetSupplierDetail()
        {

            Supplier supplier = new Supplier();
            supplier.Name = RemoveWhiteSpace(txtSupplier.Text.Trim());
            supplier.Email = txtEmail.Text.Trim();
            supplier.Address = txtAddress.Text.Trim();
            supplier.City = txtCity.Text.Trim();
            supplier.State = txtState.Text.Trim();
            supplier.PinCode = txtPin.Text.Trim();
            supplier.Mobile = txtMobile.Text.Trim();
            supplier.Website = txtWebsite.Text.Trim();
            supplier.Phone = txtPhn.Text.Trim();
            supplier.Information = new Information();
            supplier.Information.PanNumber = txtPan.Text.Trim();
            supplier.Information.TanNumber = txtTan.Text.Trim();
            supplier.Information.FaxNumber = txtFax.Text.Trim();
            supplier.Information.PfNumber = txtPf.Text.Trim();
            supplier.Information.EsiNumber = txtEsi.Text.Trim();
            supplier.Information.ContactPersonName = txtContactPName.Text.Trim();
            supplier.Information.ContactPersonEmailId = txtContactPEmail.Text.Trim();
            supplier.Information.ContactPersonMobileNo = txtContactPMobile.Text.Trim();
            supplier.Information.ContactPersonPhoneNo = txtContactPPhone.Text.Trim();
            supplier.CreatedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;            
            return supplier;
        }
        private Supplier UpdateSupplierDetail()
        {

            Supplier supplier = new Supplier();
            supplier.SupplierId = supplierId;
            supplier.Name = RemoveWhiteSpace(txtSupplier.Text.Trim());
            supplier.Email = txtEmail.Text.Trim();
            supplier.Address = txtAddress.Text.Trim();
            supplier.City = txtCity.Text.Trim();
            supplier.State = txtState.Text.Trim();
            supplier.PinCode = txtPin.Text.Trim();
            supplier.Mobile = txtMobile.Text.Trim();
            supplier.Website = txtWebsite.Text.Trim();
            supplier.Phone = txtPhn.Text.Trim();
            supplier.Information = new Information();
            supplier.Information.PanNumber = txtPan.Text.Trim();
            supplier.Information.TanNumber = txtTan.Text.Trim();
            supplier.Information.FaxNumber = txtFax.Text.Trim();
            supplier.Information.PfNumber = txtPf.Text.Trim();
            supplier.Information.EsiNumber = txtEsi.Text.Trim();
            supplier.Information.ContactPersonName = txtContactPName.Text.Trim();
            supplier.Information.ContactPersonEmailId = txtContactPEmail.Text.Trim();
            supplier.Information.ContactPersonMobileNo = txtContactPMobile.Text.Trim();
            supplier.Information.ContactPersonPhoneNo = txtContactPPhone.Text.Trim();
            supplier.ModifiedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;    
            supplier.ModifiedDate = DateTime.Now;
            return supplier;
        }
        private int CreateSupplier(Supplier supplier)
        {
            int id = supplierBL.CreateSupplier(supplier);
            return id;
        }
        private int UpdateSupplier(Supplier supplier)
        {
            int id = supplierBL.UpdateSupplier(supplier);
            return id;
        }
        private List<Supplier> GetSupplier()
        {
            
            List<Supplier> lstSupplier = new List<Supplier>();

            lstSupplier = supplierBL.ReadSupplier(null);

            return lstSupplier;
            
        }
        private void GetSupplierById(Int32 id)
        {
            id = supplierId;
            List<Supplier> lstSupplier = new List<Supplier>();
            lstSupplier = supplierBL.ReadSupplier(id);
            txtSupplier.Text = lstSupplier[0].Name.ToString();
            txtEmail.Text = lstSupplier[0].Email.ToString();
            txtAddress.Text = lstSupplier[0].Address.ToString();
            txtCity.Text = lstSupplier[0].City.ToString();
            txtState.Text = lstSupplier[0].State.ToString();
            txtPin.Text = lstSupplier[0].PinCode.ToString();
            txtMobile.Text = lstSupplier[0].Mobile.ToString();
            txtWebsite.Text = lstSupplier[0].Website.ToString();
            txtPhn.Text = lstSupplier[0].Phone.ToString();
            supplier.Information = new Information();
            txtPan.Text = Convert.ToString(lstSupplier[0].Information.PanNumber);
            txtTan.Text = Convert.ToString(lstSupplier[0].Information.TanNumber);
            txtPf.Text = Convert.ToString(lstSupplier[0].Information.PfNumber);
            txtFax.Text = Convert.ToString(lstSupplier[0].Information.FaxNumber);
            txtEsi.Text = Convert.ToString(lstSupplier[0].Information.EsiNumber);
            txtContactPName.Text = Convert.ToString(lstSupplier[0].Information.ContactPersonName);
            txtContactPEmail.Text = Convert.ToString(lstSupplier[0].Information.ContactPersonEmailId);
            txtContactPMobile.Text = Convert.ToString(lstSupplier[0].Information.ContactPersonMobileNo);
            txtContactPPhone.Text = Convert.ToString(lstSupplier[0].Information.ContactPersonPhoneNo);
        }

        #endregion

        # region  privateProperities
        /// <summary>
        /// Gets or sets the Group id.
        /// </summary>
        /// <value>The Group id.</value>
        /// 

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
        public int supplierId
        {
            get
            {
                return (int)ViewState["supplierrId"];
            }
            set
            {
                ViewState["supplierrId"] = value;
            }
        }
        public string supplierrIds
        {
            get
            {
                return (string)ViewState["supplierrIds"];
            }
            set
            {
                ViewState["supplierrIds"] = value;
            }
        }
        #endregion             

    }
}