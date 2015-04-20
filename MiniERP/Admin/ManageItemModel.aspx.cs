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
    public partial class ManageItemModel : BasePage
    {
        #region Protected_Method

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgvItemModel();
                //SetValidationExp();
            }  
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int IsSuccesfull;
            ModelSpecification itemModel = GetItemModelDetail();
            itemModel.ModelSpecificationId = ItemModelId;
            ItemCategoryBL ItemCategoryBL = new ItemCategoryBL();
            ItemModelBL itemModelBL = new ItemModelBL();
            IsSuccesfull = itemModelBL.UpdateItemModel(itemModel);
            if (IsSuccesfull > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_ITEMMODEL_MESSAGE);
                BindgvItemModel();
                SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
                ResetContorls();
            }
            else if (IsSuccesfull == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
                ResetContorls();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int itemModelId;
            itemModelId = CreateItemModel(GetItemModelDetail());

            if (itemModelId > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_ITEMMODEL_MESSAGE);
                BindgvItemModel();
                //ResetContorls();
            }
            else if (itemModelId == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);

            }
            ResetContorls();
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetContorls();
        }

        protected void gvItemModel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemModel.PageIndex = e.NewPageIndex;
            BindgvItemModel();
        } 

        protected void gvItemModel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int itemModelId;
            string message;
            ItemModelId = itemModelId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "cmdEdit")
            {

                setControlsValue(GetItemModelById(itemModelId));
                SetControlsVisiblity(GlobalConstants.C_MODE_UPDATE);
            }
            else if (e.CommandName == "cmdDelete")
            {
                
                message = DeleteItemModel(itemModelId);
                if (message == "")
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DELETE_ITEMMODEL_MESSAGE);
                }
                else
                {
                    ShowMessageWithUpdatePanel(message);
                }
                BindgvItemModel();
                ResetContorls();
            }
        }
        #endregion

        #region Private Metods
        //public void SetValidationExp()
        //{
        //    revName.ValidationExpression = ValidationExpression.C_TEXT_BOX_MULTILINE;
           
        //}

        /// <summary>
        /// Bindgvs the ItemModel.
        /// </summary>
        private void BindgvItemModel()
        {
            gvItemModel.DataSource = GetItemModel();
            gvItemModel.DataBind();
        }

        /// <summary>
        /// Gets the ItemModel detail.
        /// </summary>
        /// <returns></returns>
        private ModelSpecification GetItemModelDetail()
        {
            ModelSpecification itemModel = new ModelSpecification();
            itemModel.ModelSpecificationName = txtName.Text.Trim();
            itemModel.Description = txtdescription.Text.Trim();
            itemModel.CreatedBy = LoggedInUser.UserLoginId;
            itemModel.CreatedDate = DateTime.Now;
            return itemModel;
        }

        /// <summary>
        /// Creates the itemModel.
        /// </summary>
        /// <param name="itemModel">The itemModel.</param>
        /// <returns></returns>
        private int CreateItemModel(ModelSpecification itemModel)
        {
            ItemModelBL itemModelBL = new ItemModelBL();
            int ItemModelId = itemModelBL.CreateItemModel(itemModel);
            return ItemModelId;
        }

        /// <summary>
        /// Gets the itemModel.
        /// </summary>
        /// <returns></returns>
        private List<ModelSpecification> GetItemModel()
        {
            List<ModelSpecification> listitemModel = new List<ModelSpecification>();
            ItemModelBL itemModelBL = new ItemModelBL();
            listitemModel = itemModelBL.ReadItemModel(null);
            return listitemModel;

        }

        /// <summary>
        /// Gets the itemModel by id.
        /// </summary>
        /// <param name="itemModelId">The itemModel id.</param>
        /// <returns></returns>
        private ModelSpecification GetItemModelById(int itemModelId)
        {
            List<ModelSpecification> listItemCategory = new List<ModelSpecification>();
            ItemModelBL itemModelBL = new ItemModelBL();
            //return itemcategoryBL.ReadItemCategoryById(itemModelId);
            return itemModelBL.ReadItemModelById(itemModelId);
        }

        /// <summary>
        /// Sets the controls value.
        /// </summary>
        /// <param name="itemModel">The itemModel.</param>
        private void setControlsValue(ModelSpecification itemModel)
        {
            txtdescription.Text = itemModel.Description;
            txtName.Text = itemModel.ModelSpecificationName;

        }

        /// <summary>
        /// Resets the contorls.
        /// </summary>
        private void ResetContorls()
        {
            SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
            txtdescription.Text = "";
            txtName.Text = "";
        }

        /// <summary>
        /// Deletes the itemModel.
        /// </summary>
        /// <param name="ItemCategoryId">The itemModel id.</param>
        private string DeleteItemModel(int itemModelId)
        {
            ItemModelBL itemModelBL = new ItemModelBL();
            return itemModelBL.DeleteItemModel(itemModelId, LoggedInUser.UserLoginId, DateTime.Now);
        }

        /// <summary>
        /// Sets the controls visiblity.
        /// </summary>
        /// <param name="Mode">The mode.</param>
        private void SetControlsVisiblity(String Mode)
        {
            if (Mode == GlobalConstants.C_MODE_INSERT)
            {
                btnUpdate.Visible = false;
                btnSave.Visible = true;
            }
            else if (Mode == GlobalConstants.C_MODE_UPDATE)
            {
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        #endregion

        # region  privateProperities
        /// <summary>
        /// Gets or sets the ItemModel id.
        /// </summary>
        /// <value>The ItemModel id.</value>
        public int ItemModelId
        {
            get
            {
                return (int)ViewState["ItemModelId"];
            }
            set
            {
                ViewState["ItemModelId"] = value;
            }
        }
        #endregion

        
    }
}