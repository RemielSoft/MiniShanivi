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
    public partial class ManageStore : BasePage
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


        StoreBL storeBL = new StoreBL();

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
            Store store = new Store();
            store.StoreName = txtname.Text;
            store.CreatedBy = LoggedInUser.UserLoginId;
            id = storeBL.CreateStore(store);
            if (id > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_STORE_MESSAGE);
                ResetStore();
                BindGrid();
            }
            else
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
            }
        }

        /// <summary>
        /// Reset Store
        /// </summary>
        private void ResetStore()
        {

            txtname.Text = string.Empty;
        }

        /// <summary>
        /// LIst all the Available Store in The Database
        /// </summary>
        private void BindGrid()
        {
            List<Store> availableStore = new List<Store>();
            // Read all the Store from the Database
            availableStore = storeBL.ReadStore(null);

            // Bind to the Grid View
            gvStore.DataSource = availableStore;
            gvStore.DataBind();
        }

        /// <summary>
        /// HAndles the Row Command Event of The GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvStore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Check for Null
            if (e.CommandArgument != null)
            {
                int storeId = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "cmdEdit")
                {
                    btnsave.Visible = false;
                    btnupdate.Visible = true;
                    //Read the Store On the basis of that Id
                    var stores = storeBL.ReadStore(storeId);
                    if (stores != null && stores.Count > 0)
                    {
                        Store store = new Store();
                        store = stores.FirstOrDefault();
                        // Store the Store Id in the ViewState for Temporary Storage Purpose
                        ViewState["ID"] = store.StoreId;
                        FillForm(store);
                    }
                }
                else
                {
                    CommandNames = "DELETE";
                    // Delete the Store On the Basis of Store Id
                    storeBL.DeleteStore(storeId, LoggedInUser.UserLoginId, DateTime.UtcNow);
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DELETE_STORE_MESSAGE);

                    BindGrid();
                }
            }
        }


        /// <summary>
        /// Fill Form
        /// </summary>
        private void FillForm(Store store)
        {
            txtname.Text = store.StoreName;
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
                Store store = new Store();
                store.StoreId = Convert.ToInt32(ViewState["ID"].ToString());
                store.StoreName = txtname.Text;
                store.ModifiedBy = LoggedInUser.UserLoginId;
                int id = storeBL.UpdateStore(store);
                // If Id >0 that means Store Update Successfully
                if (id > 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_STORE_MESSAGE);
                    ResetStore();
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
            ResetStore();
        }

    }
}