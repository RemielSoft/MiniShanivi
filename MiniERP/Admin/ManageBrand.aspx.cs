using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DocumentObjectModel;
using MiniERP.Shared;

namespace MiniERP.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ManageBrand : BasePage
    {
        #region Private Properties

        private String CommandNames
        {
            get
            {
                return (String)ViewState["CommandNames"];
            }
            set
            {
                ViewState["CommandNames"] = value;
            }
        }


        #endregion


        BrandBL brandBL = new BrandBL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // focus should be on the text box while page is loading for the first time
                txtname.Focus();
                CommandNames = null;
            }
            //Bind the available Brands 
            BindGrid();
            btnupdate.Visible = false;
            btnsave.Visible = true;
        }

        /// <summary>
        ///  Handles the Save Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            int id;
            Brand brand = new Brand();
            brand.BrandName = txtname.Text;
            brand.CreatedBy = LoggedInUser.UserLoginId;
            id = brandBL.CreateBrand(brand);
            if (id > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_BRAND_MESSAGE);
                ResetBrand();
                BindGrid();
            }
            else
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
            }
        }

        /// <summary>
        /// Reset Brand
        /// </summary>
        private void ResetBrand()
        {

            txtname.Text = string.Empty;
        }

        /// <summary>
        /// LIst all the Available Brands in The Database
        /// </summary>
        private void BindGrid()
        {
            List<Brand> availableBrands = new List<Brand>();
            // Read all the Brands from the Database
            availableBrands = brandBL.ReadBrands(null);

            // Bind to the Grid View
            gvBrand.DataSource = availableBrands;
            gvBrand.DataBind();
        }

        /// <summary>
        /// HAndles the Row Command Event of The GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBrand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Check for Null
            if (e.CommandArgument != null)
            {
                int brandId = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "cmdEdit")
                {
                    btnsave.Visible = false;
                    btnupdate.Visible = true;
                    //Read the Brand On the basis of that Id
                    var brands = brandBL.ReadBrands(brandId);
                    if (brands != null && brands.Count > 0)
                    {
                        Brand brand = new Brand();
                        brand = brands.FirstOrDefault();
                        // Store the Brand Id in the ViewState for Temporary Storage Purpose
                        ViewState["ID"] = brand.BrandId;
                        FillForm(brand);
                    }
                }
                else
                {
                    CommandNames = "DELETE";
                    // Delete the Brand On the Basis of Brand Id
                    brandBL.DeleteBrand(brandId, LoggedInUser.UserLoginId, DateTime.UtcNow);
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DELETE_BRAND_MESSAGE);

                    BindGrid();
                }
            }
        }

        /// <summary>
        /// Fill Form
        /// </summary>
        private void FillForm(Brand brand)
        {
            txtname.Text = brand.BrandName;
        }


        /// <summary>
        ///  Handles the Button Update Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            // CHeck For NUll 
            if (ViewState["ID"] != null)
            {
                Brand brand = new Brand();
                brand.BrandId = Convert.ToInt32(ViewState["ID"].ToString());
                brand.BrandName = txtname.Text;
                brand.ModifiedBy = LoggedInUser.UserLoginId;
                int id = brandBL.UpdateBrand(brand);
                // If Id >0 that means Brand Update Successfully
                if (id > 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_BRAND_MESSAGE);
                    ResetBrand();
                }
                else
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
                }
                BindGrid();
            }
        }

        /// <summary>
        /// Manage Controls Visiblity
        /// </summary>        
        private void ManageControlsVisiblity()
        {
            // Handles the Visiblity of Save & Update Button
            if (CommandNames != null && CommandNames == "EDIT")
            {
                btnsave.Visible = false;
                btnupdate.Visible = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ResetBrand();
        }

    }
}