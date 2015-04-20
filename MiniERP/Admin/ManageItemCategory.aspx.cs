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
    public partial class ManageItemCategory : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgvItemCategory();
                SetValidationExp();
                txtName.Focus();
                BindRanges();
            }
        }

        #region Protected Method

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int IsSuccesfull=0;
            int flag = 0;
            string str = string.Empty;
            //--------
            if (Convert.ToInt32(txtStart.Text) >= Convert.ToInt32(txtEnd.Text))
            {
                ShowMessageWithUpdatePanel("End Range Should Be Greater Than Start Range");
            }
            else
            {
                ItemCategory itemcategory = new ItemCategory();
                itemcategory.ItemCategoryId = ItemCategoryId;
                itemcategory.ItemCategoryName = txtName.Text.Trim();
                itemcategory.StartRange = Convert.ToInt32(txtStart.Text);
                itemcategory.EndRange = Convert.ToInt32(txtEnd.Text);
                itemcategory.Description = txtdescription.Text.Trim();
                itemcategory.ModifiedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;
                itemcategory.ModifiedDate = DateTime.Now;
            
                List<ItemCategory> lst = new List<ItemCategory>();
                lst = GetItemCategory();
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].ItemCategoryId != ItemCategoryId)
                    {
                        if ((Convert.ToInt32(txtStart.Text) < lst[i].StartRange && Convert.ToInt32(txtEnd.Text) < lst[i].StartRange) || (Convert.ToInt32(txtStart.Text) > lst[i].EndRange))
                        {
                            flag = flag + 1;
                        }
                        else
                        {
                            flag = -1;
                            str = "This Range Is Used With Category " + lst[i].ItemCategoryName + " For Range " + lst[i].StartRange + "-" + lst[i].EndRange;
                            break;
                        }
                    }
                }
                if (flag > 0)
                {
                    ItemCategoryBL ItemCategoryBL = new ItemCategoryBL();
                    IsSuccesfull = ItemCategoryBL.UpdateItemCategory(itemcategory);
                }
                else
                {
                    IsSuccesfull = -1;
                }

            }
           
            //--------------

            //ItemCategory itemcategory = GetItemCategoryDetail();
            //itemcategory.ItemCategoryId = ItemCategoryId;
            //ItemCategoryBL ItemCategoryBL = new ItemCategoryBL();
            //IsSuccesfull = ItemCategoryBL.UpdateItemCategory(itemcategory);
            if (IsSuccesfull > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_ITEMCATEGORY_MESSAGE);
                BindgvItemCategory();
                SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
                ResetContorls();
                BindRanges();
            }
            else if (IsSuccesfull == 0)
            {
                //ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);                
                ShowMessageWithUpdatePanel("This Category Name Already Exists");
                txtName.Focus();
            }
            else if (IsSuccesfull == -1)
            {
                //ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
                ShowMessageWithUpdatePanel(str);
                txtStart.Focus();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetContorls();
            BindRanges();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int itemcategoruId=0;
            int flag = 0;
            string str = string.Empty;
            //
            if (Convert.ToInt32(txtStart.Text) >= Convert.ToInt32(txtEnd.Text))
            {
                ShowMessageWithUpdatePanel("End Range Should Be Greater Than Start Range");
            }
            else
            {
                ItemCategory itemcategory = new ItemCategory();
                itemcategory.ItemCategoryName = txtName.Text.Trim();
                itemcategory.StartRange = Convert.ToInt32(txtStart.Text);
                itemcategory.EndRange = Convert.ToInt32(txtEnd.Text);
                itemcategory.Description = txtdescription.Text.Trim();
                itemcategory.CreatedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;
                itemcategory.CreatedDate = DateTime.Now;
                
                List<ItemCategory> lst = new List<ItemCategory>();
                lst = GetItemCategory();
                for (int i = 0; i < lst.Count; i++)
                {
                    if ((Convert.ToInt32(txtStart.Text) < lst[i].StartRange && Convert.ToInt32(txtEnd.Text) < lst[i].StartRange) || (Convert.ToInt32(txtStart.Text) > lst[i].EndRange))
                    {
                        flag = flag + 1;
                    }
                    else
                    {
                        flag = -1;
                        str = "This Range Is Used With Category " + lst[i].ItemCategoryName + " For Range " + lst[i].StartRange + "-" + lst[i].EndRange;
                        break;
                    }
                }
                if (flag >= 0)
                {
                    itemcategoruId = CreateItemCategory(itemcategory);
                }
                else
                {
                    itemcategoruId = -1;
                }
            }              
           
            if (itemcategoruId > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_ITEMCATEGORY_MESSAGE);
                BindgvItemCategory();
                ResetContorls();
                BindRanges();
            }
            else if (itemcategoruId == 0)
            {                
                ShowMessageWithUpdatePanel("This Category Name Is Already Exists");
                txtName.Focus();

            }
            else if (itemcategoruId == -1)
            {
                
                ShowMessageWithUpdatePanel(str);
                txtStart.Focus();
            }
        }

        protected void gvItemCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemCategory.PageIndex = e.NewPageIndex;
            BindgvItemCategory();
        }

        protected void gvItemCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            string message;
            ItemCategoryId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "cmdEdit")
            {


                setControlsValue(GetItemCategoryById(ItemCategoryId));
                SetControlsVisiblity(GlobalConstants.C_MODE_UPDATE);
            }
            else if (e.CommandName == "cmdDelete")
            {                
                message = DeleteItemCategory(ItemCategoryId);
                if (message == "")
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DELETE_ITEMCATEGORY_MESSAGE);
                    BindRanges();
                }
                else
                {
                    string str = SplitString(message);
                    ShowMessageWithUpdatePanel(str);
                }
                BindgvItemCategory();
                ResetContorls();

            }
        }

        #endregion

        #region Private Metods
        private void SetValidationExp()
        {
            //revName.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            //revName.ErrorMessage = "Category Name Should Be In Characters";
            revStartRange.ValidationExpression = ValidationExpression.C_NUMERIC;
            revStartRange.ErrorMessage = "Category Level Start Range Should Be Numeric";
            revEndRange.ValidationExpression = ValidationExpression.C_NUMERIC;
            revEndRange.ErrorMessage = "Category Level End Range Should Be Numeric";
            //revDescription.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revDescription.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_DESCRIPTION;
        }
        /// <summary>
        /// Bindgvs the ItemCategory.
        /// </summary>
        private void BindgvItemCategory()
        {
            gvItemCategory.DataSource = GetItemCategory();
            gvItemCategory.DataBind();
        }

        /// <summary>
        /// Gets the itemcategory detail.
        /// </summary>
        /// <returns></returns>
        private ItemCategory GetItemCategoryDetail()
        {
            ItemCategory itemcategory = new ItemCategory();
            itemcategory.ItemCategoryName = txtName.Text.Trim();
            itemcategory.StartRange = Convert.ToInt32(txtStart.Text);
            itemcategory.EndRange = Convert.ToInt32(txtEnd.Text);
            itemcategory.Description = txtdescription.Text.Trim();
            itemcategory.CreatedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;
            itemcategory.CreatedDate = DateTime.Now;            
            return itemcategory;

        }

        /// <summary>
        /// Creates the itemcategory.
        /// </summary>
        /// <param name="itemcategory">The itemcategory.</param>
        /// <returns></returns>
        private int CreateItemCategory(ItemCategory itemcategory)
        {
            ItemCategoryBL itemcategoryBL = new ItemCategoryBL();
            int ItemCategoryId = itemcategoryBL.CreateItemCategory(itemcategory);
            return ItemCategoryId;
        }

        /// <summary>
        /// Gets the itemcategory.
        /// </summary>
        /// <returns></returns>
        private List<ItemCategory> GetItemCategory()
        {
            List<ItemCategory> listitemcategory = new List<ItemCategory>();
            ItemCategoryBL itemcategoryBL = new ItemCategoryBL();
            listitemcategory = itemcategoryBL.ReadItemCategory();
            return listitemcategory;

        }

        /// <summary>
        /// Gets the ItemCategory by id.
        /// </summary>
        /// <param name="ItemCategoryId">The ItemCategory id.</param>
        /// <returns></returns>
        private ItemCategory GetItemCategoryById(int itemcategoryId)
        {
            List<ItemCategory> listItemCategory = new List<ItemCategory>();
            ItemCategoryBL itemcategoryBL = new ItemCategoryBL();
            return itemcategoryBL.ReadItemCategoryById(itemcategoryId);
        }

        /// <summary>
        /// Sets the controls value.
        /// </summary>
        /// <param name="itemcategory">The itemcategory.</param>
        private void setControlsValue(ItemCategory itemcategory)
        {            
            txtName.Text = itemcategory.ItemCategoryName;
            txtdescription.Text = itemcategory.Description;
            txtStart.Text =itemcategory.StartRange.ToString();
            txtEnd.Text = itemcategory.EndRange.ToString();
        }

        /// <summary>
        /// Resets the contorls.
        /// </summary>
        private void ResetContorls()
        {
            SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
            txtdescription.Text = string.Empty;
            txtStart.Text = string.Empty;
            txtEnd.Text = string.Empty;
            txtName.Text = string.Empty;
            txtName.Focus();
        }

        /// <summary>
        /// Deletes the ItemCategory.
        /// </summary>
        /// <param name="ItemCategoryId">The ItemCategory id.</param>
        private string DeleteItemCategory(int itemcategoryId)
        {
            ItemCategoryBL itemcategoryBL = new ItemCategoryBL();
            return itemcategoryBL.DeleteItemCategory(itemcategoryId,LoggedInUser.UserLoginId, DateTime.Now);
        }
        private void BindRanges()
        {
            ItemBL itemBL = new ItemBL();
            ItemCategory itemCategory = new ItemCategory();
            List<ItemCategory> lstItemCategory = new List<ItemCategory>();
            lstItemCategory = itemBL.ReadRanges(null);
            if (lstItemCategory.Count == 0)
            {
                ddlRanges.Visible = false;
            }
            else
            {
                ddlRanges.Visible = true;
                ddlRanges.DataSource = lstItemCategory;
                ddlRanges.DataValueField = "ItemCategoryId";
                ddlRanges.DataTextField = "Range";
                ddlRanges.DataBind();
            }
        }
        private string SplitString(string message)
        {
            string msg = string.Empty;
            //int k = 1;
            string final = string.Empty;
            string m = string.Empty;
            string[] myarray = message.Split('.');

            for (int s = 1; s < myarray.Count();s++ )
            {
                //k = k + s;
                    m =  myarray[s-1];
                    msg = msg + s+". "+" "+ m + " ";

            }
            final = "This Category Used In " + msg;
            return final;
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
        ///***********code is commented for further use......please dont delete **********/
        ////private void BindAuthorityLevel()
        ////{
        ////    MetaDataBL metadataBl = new MetaDataBL();
        ////    ddlauthority.Items.Clear();
        ////    ddlauthority.Items.Add(new ListItem("--Select--", "0"));
        ////    ddlauthority.DataSource = metadataBl.ReadMetaDataAuthority();
        ////    ddlauthority.DataTextField = "Name";
        ////    ddlauthority.DataValueField = "Authority_Level";
        ////    ddlauthority.DataBind();
        ////}
        ////-***********code is commented for further use......please dont delete **********--%>//
        #endregion

        # region  privateProperities
        /// <summary>
        /// Gets or sets the ItemCategory id.
        /// </summary>
        /// <value>The ItemCategory id.</value>
        public int ItemCategoryId
        {
            get
            {
                return (int)ViewState["ItemCategoryId"];
            }
            set
            {
                ViewState["ItemCategoryId"] = value;
            }
        }
        #endregion
  
    }
}