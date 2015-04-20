using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Drawing;
using System.Text;

namespace MiniERP.Admin
{
    public partial class ManageItem : BasePage
    {

        #region private Global Variables

        bool track = false;

        ItemCategoryBL itemcategoryBL = new ItemCategoryBL();
        MetaDataBL metadataBL = new MetaDataBL();
        BrandBL brandBL = new BrandBL();

        SearchBAL searchBAL = new SearchBAL();
        ItemBL itemBL = new ItemBL();
        ModelSpecification itemModelSpecification = null;
        // Int32 index = 0;
        #endregion

        #region Protected Method

        protected void Page_Load(object sender, EventArgs e)
        {
            //-----------------------Done By Jai on 27-02-2013---------------------
            if (CommandNames == "cmdEdit")
            {
                btnAdd.Visible = false;
            }
            else
            {
                btnAdd.Visible = true;
            }
            if (btnSearch.Text == "Finish")
            {
                btnsave.Visible = false;
                if (CommandNames == "cmdAddition")
                {
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                }
            }
            //btnReset.Visible = true;
            //btncancel.Visible = true;
            Enabled(true);
            if (Convert.ToString(Session["Flag"]) == "2")
            {
                SpecificationIds = Session["SpecificationIds"].ToString();
                MaxId = searchBAL.ReadMaxSpecificationId();
                maxId = SpecificationIds;
                BindgvSearchItem();
                btnSearch.Text = "Finish";
                btnAdd.Visible = false;
                btnsave.Visible = false;
                Enabled(false);
                if (maxId == "0")
                {
                    ShowMessage("No Item Matched");
                    btnSearch.Text = "Search";
                    btnsave.Visible = true;
                    btnAdd.Visible = true;
                    btncancel.Visible = true;
                    btnReset.Visible = true;
                    Enabled(true);
                }
            }
            //------------------------------------------End--------------------------------

            if (!IsPostBack)
            {

                //-----------------------
                Session.Remove(SpecificationIds);
                Session["Flag"] = null;
                if (btnSearch.Text == "Search")
                {
                    BindgvItem();
                }

                BindDropDown(ddlUnitMeasure, "Name", "Id", metadataBL.ReadMetaDataUnitMeasurement());
                txtname.Focus();
                SetValidationExp();
                BindRanges();
                lstModelSpecification = null;
                lstPreModelSpecification = null;
                CommandNames = null;
                SpecificationName = null;
                //ModalPopupExtender2.Hide();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(lblCatId.Text.ToString()))
            {
                ShowMessageWithUpdatePanel("Invalid Category Range");
                track = true;
            }
            else
            {
                itemModelSpecification = GetModelSpecifications();
            }
            if (lstModelSpecification == null)
            {
                lstModelSpecification = new List<ModelSpecification>();

            }

            int PreCount = 0;
            int CurCount = 0;
            int i = 0;
            int j = 0;
            if (lblCategory.Text.ToString() == "Invalid Category Range")
            {
                ShowMessageWithUpdatePanel("Invalid Category Range");
            }
            else
            {
                if (btnAdd.Text == "Add")
                {
                    if (CommandNames == "cmdAddition" && track != true)
                    {
                        PreCount = lstPreModelSpecification.Count;
                        CurCount = lstModelSpecification.Count;
                        if (PreCount >= CurCount)
                        {
                            if (CurCount == 0)
                            {
                                for (int k = 0; k < PreCount; k++)
                                {
                                    if ((txtSpecification.Text.Trim()).ToLower() == lstPreModelSpecification[k].ModelSpecificationName.ToLower())
                                    {
                                        track = true;
                                        break;
                                    }
                                    else
                                    {
                                        track = false;
                                    }
                                }
                            }
                            else
                            {
                                for (i = 0; i < CurCount; i++)
                                {
                                    for (j = 0; j < PreCount; j++)
                                    {
                                        if ((txtSpecification.Text.Trim()).ToLower() == lstModelSpecification[i].ModelSpecificationName.ToLower() || (txtSpecification.Text.Trim()).ToLower() == lstPreModelSpecification[j].ModelSpecificationName.ToLower())
                                        {
                                            track = true;
                                            break;
                                        }
                                        else
                                        {
                                            track = false;
                                        }
                                    }
                                    if (track == true)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (i = 0; i < PreCount; i++)
                            {
                                for (j = 0; j < CurCount; j++)
                                {
                                    if ((txtSpecification.Text.Trim()).ToLower() == lstModelSpecification[j].ModelSpecificationName.ToLower() || (txtSpecification.Text.Trim()).ToLower() == lstPreModelSpecification[i].ModelSpecificationName.ToLower())
                                    {
                                        track = true;
                                        break;
                                    }
                                    else
                                    {
                                        track = false;
                                    }
                                }
                                if (track == true)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (i = 0; i < lstModelSpecification.Count; i++)
                        {
                            if ((txtSpecification.Text.Trim()).ToLower() == lstModelSpecification[i].ModelSpecificationName.ToLower() || lblCategory.Text.ToString() == "Invalid Category Range")
                            {
                                track = true;
                                break;
                            }
                            else
                            {
                                track = false;
                            }
                        }
                    }
                    if (track == true)
                    {
                        ShowMessageWithUpdatePanel("Specification: " + txtSpecification.Text + " Already Exists For This Item");
                    }
                    else
                    {
                        lstModelSpecification.Add(itemModelSpecification);
                    }

                    gvSpecification.DataSource = lstModelSpecification;
                    gvSpecification.DataBind();

                    if (btnSearch.Text == "Finish")
                    {
                        MaxId = MaxId + 1;
                        maxId = maxId + "," + MaxId.ToString();
                    }
                }

                if (btnAdd.Text == "Update")
                {
                    if (CommandNames == "cmdAddition")
                    {
                        PreCount = lstPreModelSpecification.Count;
                        CurCount = lstModelSpecification.Count;
                        if (PreCount >= CurCount)
                        {
                            for (i = 0; i < CurCount; i++)
                            {
                                for (j = 0; j < PreCount; j++)
                                {
                                    if ((txtSpecification.Text.Trim()).ToLower() == lstModelSpecification[i].ModelSpecificationName.ToLower() || (txtSpecification.Text.Trim()).ToLower() == lstPreModelSpecification[j].ModelSpecificationName.ToLower())
                                    {
                                        track = true;
                                        break;
                                    }
                                    else
                                    {
                                        track = false;
                                    }
                                }
                                if (track == true)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (i = 0; i < PreCount; i++)
                            {
                                for (j = 0; j < CurCount; j++)
                                {
                                    if ((txtSpecification.Text.Trim()).ToLower() == lstModelSpecification[j].ModelSpecificationName.ToLower() || (txtSpecification.Text.Trim()).ToLower() == lstPreModelSpecification[i].ModelSpecificationName.ToLower())
                                    {
                                        track = true;
                                        break;
                                    }
                                    else
                                    {
                                        track = false;
                                    }
                                }
                                if (track == true)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (i = 0; i < lstModelSpecification.Count; i++)
                        {
                            if ((txtSpecification.Text.Trim()).ToLower() == lstModelSpecification[i].ModelSpecificationName.ToLower())
                            {
                                track = true;
                                break;
                            }
                            else
                            {
                                track = false;
                            }
                        }
                    }
                    if (track == true)
                    {
                        if ((txtSpecification.Text.Trim()).ToLower() == SpecificationName.ToLower())
                        {
                            track = false;
                        }
                        else
                        {
                            track = true;
                        }
                    }
                    if (track == true)
                    {
                        ShowMessageWithUpdatePanel("Specification: " + txtSpecification.Text + " Already Exists For This Item");
                    }
                    else
                    {
                        lstModelSpecification[LastIndex].UnitMeasurement.Id = Convert.ToInt32(ddlUnitMeasure.SelectedValue);
                        lstModelSpecification[LastIndex].UnitMeasurement.Name = ddlUnitMeasure.SelectedItem.Text;
                        lstModelSpecification[LastIndex].ModelSpecificationName = txtSpecification.Text.Trim();
                        lstModelSpecification[LastIndex].Category.ItemCategoryId = Convert.ToInt32(lblCatId.Text);
                        lstModelSpecification[LastIndex].Category.ItemCategoryName = lblCategory.Text.ToString();
                        lstModelSpecification[LastIndex].CategoryUsageValue = Convert.ToInt32(txtUsageValue.Text);
                        lstModelSpecification[LastIndex].Brand.BrandName = ddlMake.SelectedItem.Text.ToString();
                        lstModelSpecification[LastIndex].ModifiedBy = LoggedInUser.UserLoginId;

                    }
                    gvSpecification.DataSource = lstModelSpecification;
                    gvSpecification.DataBind();

                }

                if (track == false)
                {
                    ClearModelSpecifications();
                }
            }
        }
        protected void txtUsageValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtUsageValue.Text) > 0)
                {
                    List<ItemCategory> lst = new List<ItemCategory>();
                    lst = GetItemCategory();
                    Int32 UsageValue = Convert.ToInt32(txtUsageValue.Text);
                    for (Int32 i = 0; i < lst.Count; i++)
                    {
                        if (UsageValue >= lst[i].StartRange && UsageValue <= lst[i].EndRange)
                        {
                            lblCategory.Text = lst[i].ItemCategoryName;
                            lblCatId.Text = lst[i].ItemCategoryId.ToString();
                            lblCategory.ForeColor = Color.Green;
                            lblCategory.Font.Bold = true;
                            break;
                        }
                        else
                        {
                            lblCategory.Text = "Invalid Category Range";
                            lblCategory.ForeColor = Color.Red;
                            lblCategory.Font.Bold = true;
                        }
                    }
                }
            }
            catch
            {
                ShowMessageWithUpdatePanel("Yearly Consumption Value Should Be Integer");
            }

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            int id;
            if (lblCategory.Text == "Invalid Category Range")
            {
                ShowMessageWithUpdatePanel("Please Add Model Specification");
            }
            else
            {
                id = CreateItem(GetItemDetail());
                if (id > 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_ITEM_MESSAGE);
                    ResetItem();
                    ClearModelSpecifications();
                    BindRanges();
                    lstModelSpecification = null;
                    lstPreModelSpecification = null;
                }
                else if (id == 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);

                }
            }
            BindgvItem();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearModelSpecifications();
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            int id = 0;
            string msg = string.Empty;

            if (CommandNames == "cmdEdit")
            {
                if ((txtSpecification.Text.Trim()).ToLower() == SpecificationName.ToLower())
                {
                    track = false;
                }
                else
                {
                    for (int i = 0; i < lstPreModelSpecification.Count; i++)
                    {
                        if ((txtSpecification.Text.Trim()).ToLower() == lstPreModelSpecification[i].ModelSpecificationName.ToLower())
                        {
                            track = true;
                            break;
                        }
                        else
                        {
                            track = false;
                        }
                    }
                }
            }
            if (track == true)
            {
                ShowMessageWithUpdatePanel("Specification: " + txtSpecification.Text + " Already Exists For This Item");
            }
            else
            {
                if (ddlUnitMeasure.SelectedValue == "0" && string.IsNullOrEmpty(txtSpecification.Text) && string.IsNullOrEmpty(txtUsageValue.Text) && lstModelSpecification == null)
                {
                    ShowMessageWithUpdatePanel("Please Add Model Specification");
                }
                else if (lblCategory.Text == "Invalid Category Range" && CommandNames == "cmdAddition")
                {
                    ShowMessageWithUpdatePanel("Please Add Model Specification");
                }
                else if (lblCategory.Text == "Invalid Category Range")
                {
                    ShowMessageWithUpdatePanel("Please Enter Valid Category Range");
                }
                else if (CommandNames == "cmdAddition" && lstModelSpecification == null)
                {
                    ShowMessageWithUpdatePanel("Please Add Model Specification");
                }
                else if (lstModelSpecification != null)
                {
                    id = UpdateItem(UpdateItemDetail());
                }
                else if (ddlUnitMeasure.SelectedValue == "0" && CommandNames == "cmdAddition")
                {
                    ShowMessageWithUpdatePanel("Please Add Model Specification");
                }
                else if (ddlUnitMeasure.SelectedValue == "0")
                {
                    ShowMessageWithUpdatePanel("Please Enter Unit Measurement");
                }
                else if (string.IsNullOrEmpty(txtSpecification.Text))
                {
                    ShowMessageWithUpdatePanel("Please Enter Specification");
                }
                else if (string.IsNullOrEmpty(txtUsageValue.Text))
                {
                    ShowMessageWithUpdatePanel("Please Enter Yearly Consumption Value");
                }
                else if (SpecificationId > 0)
                {
                    id = UpdateItem(UpdateItemDetail());
                }
                else
                {
                    ShowMessageWithUpdatePanel("Please Add Model Specification");
                }

                if (id > 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_ITEM_MESSAGE);
                    ResetItem();
                    ClearModelSpecifications();

                    btnsave.Visible = true;
                    btnupdate.Visible = false;
                    //ViewState["CommandNames"] = null;
                    BindRanges();
                    // ViewState["ModelSpecification"] = null;
                    lstModelSpecification = null;
                    lstPreModelSpecification = null;
                    CommandNames = null;
                    SpecificationName = null;
                    BindgvItem();
                    if (btnSearch.Text == "Finish")
                    {
                        BindgvSearchItem();
                        btnAdd.Visible = false;
                        btnsave.Visible = false;
                        Enabled(false);
                    }
                }
                else if (id == 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
                }
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ResetItem();
            BindRanges();
            ClearModelSpecifications();
            if (btnSearch.Text == "Finish")
            {
                btnAdd.Visible = false;
                Enabled(false);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Search")
            {
                OpenPopupWithUpdatePanel(btnSearch, "SearchItemStock.aspx?PageName=ManageItem", "Title");
                // btnSearch.Text = "Finish";
            }
            else if (btnSearch.Text == "Finish")
            {
                btnSearch.Text = "Search";
                maxId = null;
                btnupdate.Visible = false;
                btnsave.Visible = true;
                Session["Flag"] = null;
                ResetItem();
                ClearModelSpecifications();

                BindgvItem();
            }
        }
        protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItem.PageIndex = e.NewPageIndex;
            if (btnSearch.Text == "Finish")
            {
                BindgvSearchItem();
            }
            else
            {
                BindgvItem();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = lstModelSpecification;
            GridView1.DataBind();
        }
        protected void gvSpecification_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LastIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "cmdEdit")
            {

                //lstModelSpecification = (List<ModelSpecification>)ViewState["ModelSpecification"];
                //Commented by sarfaraz
                ddlUnitMeasure.SelectedValue = lstModelSpecification[LastIndex].UnitMeasurement.Id.ToString();
                txtSpecification.Text = lstModelSpecification[LastIndex].ModelSpecificationName.ToString();
                SpecificationName = lstModelSpecification[LastIndex].ModelSpecificationName.ToString();
                txtUsageValue.Text = lstModelSpecification[LastIndex].CategoryUsageValue.ToString();
                //added by vishal 
                ddlMake.SelectedValue = lstModelSpecification[LastIndex].Brand.BrandId.ToString();
                //  txtMake.Text = lstModelSpecification[LastIndex].Brand.BrandId.ToString();
                lblCategory.Text = lstModelSpecification[LastIndex].Category.ItemCategoryName.ToString();
                lblCatId.Text = lstModelSpecification[LastIndex].Category.ItemCategoryId.ToString();
                btnAdd.Text = "Update";

            }
            else if (e.CommandName == "cmdDelete")
            {
                //lstModelSpecification = (List<ModelSpecification>)ViewState["ModelSpecification"];
                lstModelSpecification.RemoveAt(LastIndex);
                if (lstModelSpecification.Count == 0)
                {
                    lstModelSpecification = null;
                }
                gvSpecification.DataSource = lstModelSpecification;
                gvSpecification.DataBind();
                ClearModelSpecifications();
            }
        }
        protected void gvSpecification_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string msg = string.Empty;


            if (e.CommandName == "cmdEdit")
            {
                CommandNames = "cmdEdit";
                SpecificationId = Convert.ToInt32(e.CommandArgument);
                GetitemById(SpecificationId);
                lstPreModelSpecification = itemBL.ReadModelSpecification(ItemId);
                txtname.Enabled = true;
                txtDescription.Enabled = true;
                lblCategory.ForeColor = Color.Green;
                lblCategory.Font.Bold = true;
                btnAdd.Visible = false;
                btnsave.Visible = false;
                btnupdate.Visible = true;
                if (btnSearch.Text == "Finish")
                {
                    Enabled(true);
                }
                txtname.Focus();
            }
            else if (e.CommandName == "cmdDelete")
            {
                CommandNames = "Details";
                SpecificationId = Convert.ToInt32(e.CommandArgument);
                msg = itemBL.DeleteModelSpecifications(SpecificationId, LoggedInUser.UserLoginId);
                if (msg != string.Empty)
                {
                    Alert(msg, gvItem);
                }
                else
                {
                    Alert("Model Specification Has Been Deleted Successfully", gvItem);
                }
                ResetItem();
                ClearModelSpecifications();
                if (btnSearch.Text == "Finish")
                {
                    BindgvSearchItem();
                    Enabled(false);
                    btnAdd.Visible = false;
                }
                else
                {
                    BindgvItem();
                }
            }
            if (e.CommandName == "Details")
            {
                CommandNames = "Details";
                ItemId = Convert.ToInt32(e.CommandArgument);
                List<Item> lstItem = new List<Item>();
                lstItem = itemBL.ReadItem(ItemId);
                lblItemName.Text = lstItem[0].ItemName.ToString();
                lblItemDescription.Text = lstItem[0].ItemDescription.ToString();
                string Strspecification = string.Empty;
                string StrBrand = string.Empty;
                string StrCategory = string.Empty;
                lstModelSpecification = itemBL.ReadModelSpecification(ItemId);
                //foreach (ModelSpecification modelSpecification in lstModelSpecification)
                //{
                //    Strspecification = Strspecification + modelSpecification.ModelSpecificationName + ",";
                //    StrBrand = StrBrand + modelSpecification.Brand + ",";
                //    StrCategory = StrCategory + modelSpecification.Category.ItemCategoryName + ",";
                //}

                //lblSpecification.Text = Strspecification.TrimEnd(',');
                //lblBrand.Text = StrBrand.TrimEnd(',');
                //lblCategory1.Text = StrCategory.TrimEnd(',');
                GridView1.DataSource = lstModelSpecification;
                GridView1.DataBind();
                ModalPopupExtender2.Show();
            }
            if (e.CommandName == "ManageBrand")
            {
                CommandNames = "ManageBrand";
                BindDropDown(ddlMake, "BrandName", "BrandId", brandBL.ReadBrands(null));
                if (e.CommandArgument != null)
                {
                    int itemSpecificationId = Convert.ToInt32(e.CommandArgument.ToString());
                    ItemSpecificationId = itemSpecificationId;
                    if (itemSpecificationId > 0)
                    {
                        gvBrand.DataSource = brandBL.ReadItemBrandsById(itemSpecificationId, null);
                        gvBrand.DataBind();
                    }
                }

                ModalPopupManageBrand.Show();
            }
            if (e.CommandName == "cmdAddition")
            {
                CommandNames = "cmdAddition";
                txtname.Enabled = false;
                txtDescription.Enabled = false;
                ItemId = Convert.ToInt32(e.CommandArgument);
                List<Item> lstItem = new List<Item>();
                lstItem = itemBL.ReadItem(ItemId);
                lstPreModelSpecification = itemBL.ReadModelSpecification(ItemId);
                txtname.Text = lstItem[0].ItemName.ToString();
                txtDescription.Text = lstItem[0].ItemDescription.ToString();
                btnsave.Visible = false;
                btnupdate.Visible = true;
                btnAdd.Visible = true;
                if (btnSearch.Text == "Finish")
                {
                    Enabled(true);
                }
                ClearModelSpecifications();
                txtSpecification.Focus();

            }
            // BindgvItem();
        }
        #endregion

        #region Private Metods
        //private void SetValidationExp()
        //{
        //    revName.ValidationExpression = ValidationExpression.C_TEXT_BOX_MULTILINE;
        //}
        //private void BindChkbxlstItemModel()
        //{
        //    ItemModelBL itemModelBL = new ItemModelBL();
        //    List<ItemModel> lstItemModel = new List<ItemModel>();
        //    lstItemModel = itemModelBL.ReadItemModel();
        //    if (lstItemModel.Count > 0)
        //    {
        //        chkItemModel.DataSource = lstItemModel;
        //        chkItemModel.DataTextField = "Name";
        //        chkItemModel.DataValueField = "ItemModelId";
        //        chkItemModel.DataBind();
        //    }
        //}

        private void BindRanges()
        {

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

        private int LastIndex
        {
            get
            {
                return (int)ViewState["lstIndex"];
            }
            set
            {
                ViewState["lstIndex"] = value;
            }
        }

        private List<ItemCategory> GetItemCategory()
        {
            List<ItemCategory> listitemcategory = new List<ItemCategory>();
            ItemCategoryBL itemcategoryBL = new ItemCategoryBL();
            listitemcategory = itemcategoryBL.ReadItemCategory();
            return listitemcategory;

        }
        private void ClearModelSpecifications()
        {
            ddlUnitMeasure.SelectedValue = "0";
            txtSpecification.Text = string.Empty;
            txtUsageValue.Text = string.Empty;
            // Changed By Vishal
            ddlMake.SelectedValue = "0";
            // txtMake.Text = string.Empty;
            lblCategory.Text = string.Empty;
            lblCatId.Text = string.Empty;
            btnAdd.Text = "Add";
            txtname.Focus();
        }
        private ModelSpecification GetModelSpecifications()
        {
            itemModelSpecification = new ModelSpecification();
            itemModelSpecification.Category = new ItemCategory();
            itemModelSpecification.ModelSpecificationName = txtSpecification.Text;
            if (!string.IsNullOrEmpty(lblCatId.Text.ToString()))
            {
                itemModelSpecification.Category.ItemCategoryId = Convert.ToInt32(lblCatId.Text);
            }
            else
            {
                ShowMessageWithUpdatePanel("Invalid Category Range");
                return itemModelSpecification = null;
            }
            itemModelSpecification.UnitMeasurement = new MetaData();
            itemModelSpecification.UnitMeasurement.Id = Convert.ToInt32(ddlUnitMeasure.SelectedValue);
            itemModelSpecification.UnitMeasurement.Name = ddlUnitMeasure.SelectedItem.Text;
            itemModelSpecification.Category.ItemCategoryName = lblCategory.Text;
            itemModelSpecification.CategoryUsageValue = Convert.ToInt32(txtUsageValue.Text);
            itemModelSpecification.CreatedBy = LoggedInUser.UserLoginId;
            itemModelSpecification.ModifiedBy = LoggedInUser.UserLoginId;
            //Added By Vishal
            // itemModelSpecification.Brand.BrandName = ddlMake.SelectedItem.Text;
            // itemModelSpecification.Brand.BrandId = Convert.ToInt32(ddlMake.SelectedValue);
            //itemModelSpecification.Brand = txtMake.Text;
            return itemModelSpecification;
        }
        private string DeleteItem(int itemid)
        {
            return itemBL.DeleteItem(itemid, LoggedInUser.UserLoginId, DateTime.Now);
        }

        private void ResetItem()
        {

            txtDescription.Text = "";
            btnAdd.Text = "Add";
            btnsave.Text = "Save";
            btnupdate.Visible = false;
            btnsave.Visible = true;
            btnAdd.Visible = true;

            //foreach (ListItem listItem in chkItemModel.Items)
            //{
            //    listItem.Selected = false;
            //}
            txtname.Text = "";
            gvSpecification.DataSource = null;
            gvSpecification.DataBind();
            ViewState["ModelSpecification"] = null;
            txtname.Enabled = true;
            txtDescription.Enabled = true;
            if (btnSearch.Text == "Finish")
            {
                btnsave.Visible = false;
            }
        }
        private void BindgvItem()
        {
            gvItem.DataSource = GetItem();
            gvItem.DataBind();
        }

        private void BindgvSearchItem()
        {
            List<Item> listItem = new List<Item>();
            listItem = searchBAL.SearchItemBySpecificationId(maxId);
            gvItem.DataSource = listItem;
            gvItem.DataBind();
        }

        private Item GetItemDetail()
        {
            Item item = new Item();
            //item.ItemCategory.ItemCategoryId =Convert.ToInt32( ddlcategory.SelectedValue);
            // item.UnitMeasurement = new MetaData();
            //item.UnitMeasurement.Id = Convert.ToInt32(ddlUnitMeasure.SelectedValue);
            //item.ItemCode = txtcode.Text.Trim();
            item.ItemName = txtname.Text.Trim();
            item.ItemDescription = txtDescription.Text.Trim();
            //item.ItemModel = txtmodel.Text.Trim();

            //foreach (ListItem lstitems in chkItemModel.Items)
            //{
            //    if (lstitems.Selected == true)
            //    {
            //        ItemModel itemModel = new ItemModel();
            //        itemModel.ItemModelId = Convert.ToInt32(lstitems.Value);
            //        itemModel.CreatedBy = LoggedInUser.UserLoginId;                    
            //        item.ItemModels.Add(itemModel);
            //    }
            //}
            //----------

            //Commented by sarfaraz
            item.ModelSpecifications = (List<ModelSpecification>)ViewState["ModelSpecification"];



            item.CreatedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;

            return item;
        }
        private Item UpdateItemDetail()
        {
            Item item = new Item();
            item.ItemId = ItemId;
            item.ItemDescription = txtDescription.Text.Trim();
            item.ItemName = txtname.Text.Trim();
            item.CreatedBy = LoggedInUser.UserLoginId;
            item.ModifiedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;    
            item.ModifiedDate = DateTime.Now;
            if (lstModelSpecification != null)
            {
                item.ModelSpecifications = lstModelSpecification; //Commented by jai for complete list Updating
            }
            else
            {
                lstModelSpecification = new List<ModelSpecification>();
                itemModelSpecification = new ModelSpecification();
                itemModelSpecification.Category = new ItemCategory();
                itemModelSpecification.UnitMeasurement = new MetaData();

                itemModelSpecification.UnitMeasurement.Id = Convert.ToInt32(ddlUnitMeasure.SelectedValue);
                itemModelSpecification.ModelSpecificationId = SpecificationId;
                itemModelSpecification.ModelSpecificationName = txtSpecification.Text.ToString();
                itemModelSpecification.CategoryUsageValue = Convert.ToInt32(txtUsageValue.Text);
                itemModelSpecification.Brand.BrandName = ddlMake.SelectedItem.Text.ToString();
                itemModelSpecification.Category.ItemCategoryId = Convert.ToInt32(lblCatId.Text);
                itemModelSpecification.Category.ItemCategoryName = lblCategory.Text.ToString();
                itemModelSpecification.ModifiedBy = LoggedInUser.UserLoginId;
                lstModelSpecification.Add(itemModelSpecification);
                item.ModelSpecifications = lstModelSpecification;

            }
            return item;
        }
        private int CreateItem(Item item)
        {
            int id = 0;
            if (ViewState["ModelSpecification"] != null)
            {
                id = itemBL.CreateItem(item);
                txtSpecification.Focus();
            }
            else
            {
                ShowMessageWithUpdatePanel("Please Add Model Specification");
            }
            return id;
        }
        private int UpdateItem(Item item)
        {
            //Commented by jai on 18-01-13
            //int id=0;
            //if (Session["ModelSpecification"] != null)
            //{
            //    id = itemBL.UpdateItem(item);
            //    txtSpecification.Focus();
            //}
            //else
            //{
            //    ShowMessageWithUpdatePanel("Please add model specification");
            //}
            //return id;

            //-----------End----------
            //Commented by jai on 18-02-13
            int id = 0;
            id = itemBL.UpdateItem(item);
            txtSpecification.Focus();
            return id;
            //---------End----------

        }
        private List<Item> GetItem()
        {
            List<Item> lstItem = new List<Item>();
            lstItem = itemBL.ReadItemByModelSpecificationId(null);
            return lstItem;
        }
        private void GetitemById(Int32 id)
        {
            id = SpecificationId;
            List<Item> lstItem = new List<Item>();
            //---------Jai---------
            //lstItem = itemBL.ReadItem(id);
            //txtname.Text = lstItem[0].ItemName.ToString();           
            //txtDescription.Text = lstItem[0].ItemDescription.ToString();
            //---------End---------
            lstItem = itemBL.ReadItemByModelSpecificationId(id);
            ItemId = lstItem[0].ItemId;
            txtname.Text = lstItem[0].ItemName.ToString();
            txtDescription.Text = lstItem[0].ItemDescription.ToString();
            ddlUnitMeasure.SelectedValue = lstItem[0].ModelSpecification.UnitMeasurement.Id.ToString();
            txtSpecification.Text = lstItem[0].ModelSpecification.ModelSpecificationName.ToString();
            SpecificationName = lstItem[0].ModelSpecification.ModelSpecificationName.ToString();
            txtUsageValue.Text = lstItem[0].ModelSpecification.CategoryUsageValue.ToString();
            ddlMake.SelectedValue = lstItem[0].ModelSpecification.Brand.BrandId.ToString();
            lblCatId.Text = lstItem[0].ModelSpecification.Category.ItemCategoryId.ToString();
            lblCategory.Text = lstItem[0].ModelSpecification.Category.ItemCategoryName.ToString();
        }
        public void SetValidationExp()
        {
            revUsageValue.ValidationExpression = ValidationExpression.C_NUMERIC;
            revUsageValue.ErrorMessage = "Yearly Consumption Value Should Be Integer";
            //revDescription.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revDescription.ErrorMessage = "Invalid description entry";
            //revName.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            //revName.ErrorMessage = "Item name should be in characters";
            //revSpecification.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            //revSpecification.ErrorMessage = "Item name should be in characters";
            //revMake.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revMake.ErrorMessage = "Invalid make entry";
        }
        private void GetItemModelById(Int32 id)
        {
            Item item = new Item();
            id = ItemId;

            item = itemBL.ReaditemModelById(id);
            //foreach (ListItem lstitem in chkItemModel.Items)
            //{
            //    foreach (ItemModel itemModel in item.ItemModels)
            //    {
            //        if (Convert.ToInt32(lstitem.Value) == itemModel.ItemModelId)
            //        {
            //            lstitem.Selected = true;
            //        }
            //    }
            //}
        }
        private void BindModelSpecification(Int32? id)
        {
            lstModelSpecification = itemBL.ReadModelSpecification(id);
            ViewState["PreModelSpecification"] = lstModelSpecification;
            gvSpecification.DataSource = lstModelSpecification;
            gvSpecification.DataBind();
        }
        private void Enabled(Boolean condition)
        {
            if (CommandNames == "cmdAddition")
            {
                txtname.Enabled = false;
                txtDescription.Enabled = false;
            }
            else
            {
                txtname.Enabled = condition;
                txtDescription.Enabled = condition;
            }
            txtSpecification.Enabled = condition;
            ddlUnitMeasure.Enabled = condition;
            txtUsageValue.Enabled = condition;
            ddlMake.Enabled = condition;
            btnReset.Visible = condition;
            btncancel.Visible = condition;
            ddlRanges.Enabled = condition;
        }
        #endregion

        #region  privateProperities
        /// <summary>
        /// Gets or sets the Item id.
        /// </summary>
        /// <value>The Item id.</value>
        private int ItemId
        {
            get
            {
                return (int)ViewState["ItemId"];
            }
            set
            {
                ViewState["ItemId"] = value;
            }
        }
        private int ItemSpecificationId
        {
            get
            {
                return (int)ViewState["ItemSpecificationId"];
            }
            set
            {
                ViewState["ItemSpecificationId"] = value;
            }
        }
        private int SpecificationId
        {
            get
            {
                try
                {
                    return (int)ViewState["SpecificationId"];
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                ViewState["SpecificationId"] = value;
            }
        }
        private string SpecificationIds
        {
            get
            {
                return (String)Session["SpecificationIds"];
            }
            set
            {
                Session["ModelCodes"] = value;
            }
        }
        private int MaxId
        {
            get
            {
                return (int)ViewState["MaxId"];
            }
            set
            {

                ViewState["MaxId"] = value;
            }
        }
        private String maxId
        {
            get
            {
                return (String)ViewState["maxId"];
            }
            set
            {
                ViewState["maxId"] = value;
            }
        }
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
        private List<ModelSpecification> lstModelSpecification
        {
            get
            {
                return (List<ModelSpecification>)ViewState["ModelSpecification"];
            }
            set
            {
                ViewState["ModelSpecification"] = value;
            }
        }
        private List<ModelSpecification> lstPreModelSpecification
        {
            get
            {
                return (List<ModelSpecification>)ViewState["lstPreModelSpecification"];
            }
            set
            {
                ViewState["lstPreModelSpecification"] = value;
            }
        }
        private string SpecificationName
        {
            get
            {
                return (string)ViewState["SpecificationName"];
            }
            set
            {
                ViewState["SpecificationName"] = value;
            }
        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }

        protected void LnkBtnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=ItemStockReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";

            StringBuilder sb = new StringBuilder();
            StringWriter stringWrite = new StringWriter(sb);
            HtmlTextWriter htm = new HtmlTextWriter(stringWrite);
            gvItem.AllowPaging = false;
            gvItem.Columns.RemoveAt(7);
            BindgvItem();
            //gvSample is Gridview server control
            gvItem.RenderControl(htm);
            Response.Write(stringWrite);
            Response.End();

        }

        protected void btnAddBrand_Click(object sender, EventArgs e)
        {
            int id;
            ModelSpecification modelSpecification = new ModelSpecification();
            modelSpecification.ModelSpecificationId = ItemSpecificationId;
            modelSpecification.Brand.BrandId = Convert.ToInt32(ddlMake.SelectedValue);
            modelSpecification.CreatedBy = LoggedInUser.UserLoginId;

            id = brandBL.CreateItemBrands(modelSpecification);
            gvBrand.DataSource = brandBL.ReadItemBrandsById(ItemSpecificationId, null);
            gvBrand.DataBind();
            ModalPopupManageBrand.Show();
        }

        /// <summary>
        /// Handles the Row Command Event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBrand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {
                int brandId;
                int.TryParse(e.CommandArgument.ToString(), out brandId);
                if (brandId > 0)
                {
                    brandBL.DeleteItemBrands(ItemSpecificationId, brandId, LoggedInUser.UserLoginId, DateTime.UtcNow);
                }
                gvBrand.DataSource = brandBL.ReadItemBrandsById(ItemSpecificationId, null);
                gvBrand.DataBind();
                ModalPopupManageBrand.Show();
            }
        }


    }
}