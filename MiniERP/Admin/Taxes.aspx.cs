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
    public partial class Taxes : BasePage
    {
        #region Private Global Variable(s)
        TaxBL taxBL = new TaxBL();
        #endregion

        #region Protected Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtServiceTax.Focus();
                //BindDropDown(ddlDiscountMode, "Name", "Id", taxBL.ReadDiscountMode(null));
                DropdownBind();
                BindgvTax();
                SetValidationExp();
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            int id = 0;
            if (DiscountId == 1 && Convert.ToDecimal(txtTotalDiscount.Text) >100)
            {                
                ShowMessageWithUpdatePanel("Discount Percentage Cannot Be More Than 100");
            }
            else
            {
                id = CreateTaxMaster(GetTaxDetails());
                if (id > 0)
                {
                    ShowMessageWithUpdatePanel("Tax Has Been Created Successfully");                    
                    ResetControls();
                }
                else if (id == 0)
                {
                    ShowMessageWithUpdatePanel("Tax Already Exist For Discount Mode: " + ddlDiscountMode.SelectedItem.Text.ToString());
                }
            }
            BindgvTax();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }
        protected void gvTax_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            TaxId = id;
            string message = string.Empty;
            if (e.CommandName == "cmdEdit")
            {
                GetTaxById(TaxId);
                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
            else if (e.CommandName == "cmdDelete")
            {
                message = DeleteTax(TaxId);
                if (message == "")
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_TAX_DELETE_MESSAGE);
                }
                else
                {
                    ShowMessageWithUpdatePanel(message);
                }
                BindgvTax();
                ResetControls();
            }
        }
        protected void ddlDiscountMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int id;
            DiscountId = Convert.ToInt32(ddlDiscountMode.SelectedValue);
            if (DiscountId == 1)
            {
                lblPerInr.Text = "%";
            }
            else if (DiscountId == 2)
            {
                lblPerInr.Text = "INR";
            }
            else
            {
                lblPerInr.Text = string.Empty;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = 0;
            Tax tax = new Tax();
            tax = GetTaxDetails();
            id = taxBL.UpdateTaxMaster(tax);
            if (id > 0)
            {
                if (DiscountId == 1 && Convert.ToDecimal(txtTotalDiscount.Text) > 100)
                {
                    ShowMessageWithUpdatePanel("Discount Percentage Cannot Be More Than 100");
                }
                else
                {
                    ShowMessageWithUpdatePanel("Tax Has Been Updated Successfully");
                    BindgvTax();
                    ResetControls();
                }               
            }
            else if (id == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
            }
        }
        protected void gvTax_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            //List<Tax> lstTax = new List<Tax>();
            //lstTax = (List<Tax>)ViewState["lstTax"];
            //for(int i=0;i<lstTax.Count;i++)
            //{
            //    if (e.Row.RowType == DataControlRowType.Header)
            //    {
            //        e.Row.Cells[6].Text = lstTax[i].DiscountMode.Name;
            //    }
            //}
        }
        #endregion

        #region Private Methods
        private string DeleteTax(int taxid)
        {
            return taxBL.DeleteTax(taxid, LoggedInUser.UserLoginId, DateTime.Now);
        }
        private int CreateTaxMaster(Tax tax)
        {
            int id = 0;
            id = taxBL.CreateTaxMaster(tax);
            return id;
        }
        //private void SetValidationExp()
        //{
        //    revDiscount.ValidationExpression = ValidationExpression.C_NUMERIC_DISCOUNT;
        //    revDiscount.ErrorMessage = "Discount percentage cannot be more than 100";
        //}
        private void GetTaxById(int id)
        {
            List<Tax> lstTax = new List<Tax>();
            lstTax = taxBL.ReadTaxMaster(id);
            txtServiceTax.Text = lstTax[0].ServiceTax.ToString();
            txtVat.Text = lstTax[0].VAT.ToString();
            txtCSTWithForm.Text = lstTax[0].CSTWithCForm.ToString();
            txtCSTWithoutForm.Text = lstTax[0].CSTWithoutCForm.ToString();
            txtFreight.Text = lstTax[0].Freight.ToString();
            //Commented by sarfaraz
            ddlDiscountMode.SelectedValue = lstTax[0].DiscountMode.Id.ToString();
            if (ddlDiscountMode.SelectedValue == "1")
            {
                lblPerInr.Text = "%";
            }
            else if (ddlDiscountMode.SelectedValue == "2")
            {
                lblPerInr.Text = "INR";
            }            
            else
            {
                lblPerInr.Text = string.Empty;
            }
            DiscountId =Convert.ToInt32 (ddlDiscountMode.SelectedValue);
            txtTotalDiscount.Text = lstTax[0].TotalDiscount.ToString();
            txtPackaging.Text = lstTax[0].Packaging.ToString();
        }
        private List<Tax> GetTax()
        {
            List<Tax> lstTax = new List<Tax>();
            lstTax = taxBL.ReadTaxMaster(null);
            ViewState["lstTax"] = lstTax;
            return lstTax;
        }
        private void BindgvTax()
        {
            gvTax.DataSource = GetTax();
            gvTax.DataBind();
        }
        private Tax GetTaxDetails()
        {
            Tax tax = new Tax();
            if (btnUpdate.Visible == true)
            {
                tax.TaxId = TaxId;
                tax.ModifiedBy = LoggedInUser.UserLoginId;
            }
            tax.ServiceTax = Convert.ToDecimal(txtServiceTax.Text);
            if (!string.IsNullOrEmpty(txtVat.Text))
            {
                tax.VAT = Convert.ToDecimal(txtVat.Text);
            }
            if (!string.IsNullOrEmpty(txtCSTWithForm.Text))
            {
                tax.CSTWithCForm = Convert.ToDecimal(txtCSTWithForm.Text);
            }
            if (!string.IsNullOrEmpty(txtCSTWithoutForm.Text))
            {
                tax.CSTWithoutCForm = Convert.ToDecimal(txtCSTWithoutForm.Text);
            }
            if (!string.IsNullOrEmpty(txtFreight.Text))
            {
                tax.Freight = Convert.ToDecimal(txtFreight.Text);
            }
            tax.DiscountMode = new MetaData();
            tax.DiscountMode.Id = Convert.ToInt32(ddlDiscountMode.SelectedValue);
            if (!string.IsNullOrEmpty(txtTotalDiscount.Text))
            {
                tax.TotalDiscount = Convert.ToDecimal(txtTotalDiscount.Text);
            }
            if (!string.IsNullOrEmpty(txtPackaging.Text))
            {
                tax.Packaging = Convert.ToDecimal(txtPackaging.Text);
            }            
            if (btnSave.Visible == true)
            {
                tax.CreatedBy = LoggedInUser.UserLoginId;
            }

            return tax;
        }
        private void ResetControls()
        {
            txtServiceTax.Text = string.Empty;
            txtVat.Text = string.Empty;
            txtCSTWithForm.Text = string.Empty;
            txtCSTWithoutForm.Text = string.Empty;
            txtFreight.Text = string.Empty;
            ddlDiscountMode.SelectedValue = "0";
            txtTotalDiscount.Text = string.Empty;
            txtPackaging.Text = string.Empty;
            txtServiceTax.Focus();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            lblPerInr.Text = string.Empty;
        }

        #endregion


        #region Private Properties
        private void DropdownBind()
        {
            BindDropDown(ddlDiscountMode, "Name", "Id", taxBL.ReadDiscountMode(null));

            //ddlDiscountMode.Items.Insert(0, "--Select--");
        }
        private void SetValidationExp()
        {
            //revServiceTax.ValidationExpression = ValidationExpression.C_DECIMAL_7_2;
            //if (!string.IsNullOrEmpty(revServiceTax.ErrorMessage))
            //{
            //    revServiceTax.ErrorMessage = "Service Tax Should Be Numeric Or Only 2 Decimal Points Are Allowed";
            //}
            //else
            //{
            //    rngServiceTax.ErrorMessage = "Service Tax Should Be Less Than 1000000";
            //}
            //revVat.ValidationExpression = ValidationExpression.C_DECIMAL_7_2;
            //if (!string.IsNullOrEmpty(revVat.ErrorMessage))
            //{
            //    revVat.ErrorMessage = "VAT Should Be Numeric Or Only 2 Decimal Points Are Allowed";
            //}
            //else
            //{
            //    rngVat.ErrorMessage = "VAT Should Be Less Than 1000000";
            //}
            //revCSTWith.ValidationExpression = ValidationExpression.C_DECIMAL_7_2;
            //if (!string.IsNullOrEmpty(revCSTWith.ErrorMessage))
            //{
            //    revCSTWith.ErrorMessage = "CST With Form Should Be Numeric Or Only 2 Decimal Points Are Allowed";
            //}
            //else
            //{
            //    rngCST.ErrorMessage = "CST (With C Form) Should Be Less Than 1000000";
            //}
            //revCSTWithout.ValidationExpression = ValidationExpression.C_DECIMAL_7_2;
            //if (!string.IsNullOrEmpty(revCSTWithout.ErrorMessage))
            //{
            //    revCSTWithout.ErrorMessage = "CST Without Form Should Be Numeric Or Only 2 Decimal Points Are Allowed";
            //}
            //else
            //{
            //    rngCSTWithout.ErrorMessage = "CST (Without C Form) Should Be Less Than 1000000";
            //}
            //revFreight.ValidationExpression = ValidationExpression.C_DECIMAL_7_2;
            //if (!string.IsNullOrEmpty(revFreight.ErrorMessage))
            //{
            //    revFreight.ErrorMessage = "Frieght Should Be Numeric";
            //}
            //else
            //{
            //    rngFreight.ErrorMessage = "Freight Should Be Less Than 1000000";
            //}
            //revDiscount.ValidationExpression = ValidationExpression.C_DECIMAL_7_2;
            //if (!string.IsNullOrEmpty(revDiscount.ErrorMessage))
            //{
            //    revDiscount.ErrorMessage = "Total Discount Should Be Numeric";
            //}
            //else
            //{
            //    rngPackaging.ErrorMessage = "Packaging Should Be Less Than 1000000";
            //}
            //revPackaging.ValidationExpression = ValidationExpression.C_DECIMAL_7_2;
            //if (!string.IsNullOrEmpty(revPackaging.ErrorMessage))
            //{
            //    revPackaging.ErrorMessage = "Packaging Should Be Numeric";
            //}
            //else
            //{
            //    rngTotalDiscount.ErrorMessage = "Total Discount Should Be Less Than 1000000";
            //}     


                rngServiceTax.ErrorMessage = "Service Tax Should Be Numeric ";

                rngVat.ErrorMessage = "VAT Should Be Numeric ";


                rngCST.ErrorMessage = "CST (With C Form) Should Be Numeric ";

                rngCSTWithout.ErrorMessage = "CST (Without C Form) Should Be Numeric ";

                rngFreight.ErrorMessage = "Freight Should Be Numeric ";

                rngPackaging.ErrorMessage = "Packaging Should Be Numeric ";

                rngTotalDiscount.ErrorMessage = "Total Discount Should Be Numeric ";        
            
            
        }
        private int TaxId
        {
            get
            {
                return (int)ViewState["TaxId"];
            }
            set
            {
                ViewState["TaxId"] = value;
            }
        }
        private int DiscountId
        {
            get
            {
                return (int)ViewState["DiscountId"];
            }
            set
            {
                ViewState["DiscountId"] = value;
            }
        }
        #endregion     

    }
}