using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Web.UI.HtmlControls;
using System.Text;

namespace MiniERP.Admin
{
    public partial class ManageItemStock : BasePage
    {
        #region Private Global Variables
        ItemStockBL itemStockBL = new ItemStockBL();
        SearchBAL searchBL = new SearchBAL();
        List<Item> lstItem = new List<Item>();
        BrandBL brandBL = new BrandBL();
        StoreBL storeBL = new StoreBL();
        Item item = new Item();
        List<ModelSpecification> lstSpecification = new List<ModelSpecification>();
        ItemBL itemBL = new ItemBL();
        #endregion

        #region Protected Section

        protected void Page_Load(object sender, EventArgs e)
        {

            Enabled(true);
            if (Convert.ToString(Session["Flag"]) == "1")
            {
                ItemStockIds = Session["ItemStockIds"].ToString();
                BindgvSearchItemStock();
                btnSearch.Text = "Finish";
                btnSave.Visible = false;
                btnCancel.Visible = false;
                Session["Flag"] = null;
                Enabled(false);
                if (ItemStockIds == "0")
                {
                    ShowMessage("No Item Matched");
                    btnSearch.Text = "Search";
                    Enabled(true);
                }
            }

            if (!IsPostBack)
            {
                Session.Remove(ItemStockIds);
                Session["Flag"] = null;
                if (btnSearch.Text == "Search")
                {
                    BindgvItemStock();
                }
                BindItem();
                SetValidationExp();
                List<Store> lstStore=storeBL.ReadStore(null);
                BindDropDown(ddlStore,"StoreName","StoreId",lstStore);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id;
            if (Convert.ToInt32(txtMaximumlevel.Text) < Convert.ToInt32(txtMinimumlevel.Text))
            {
                ShowMessageWithUpdatePanel("Maximum Consumption Must Be Grater Than Minimum Consumption");
            }
            else if (Convert.ToInt32(txtMaximumlevel.Text) < Convert.ToInt32(txtNormallevel.Text))
            {
                ShowMessageWithUpdatePanel("Maximum Consumption Must Be Grater Than Normal Consumption");
            }
            else if (Convert.ToInt32(txtNormallevel.Text) < Convert.ToInt32(txtMinimumlevel.Text))
            {
                ShowMessageWithUpdatePanel("Normal Consumption Must Be Grater Than Minimum Consumption");
            }
            else
            {
                id = CreateItemStock(GetItemStockDetail());

                if (id > 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_ItemStock_MESSAGE);
                    ClearItemStock();
                }
                else if (id == 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);

                }
                //BindgvItemStock();
            }

            BindgvItemStock();
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId = Convert.ToInt32(ddlItem.SelectedValue);

            lstSpecification = itemBL.ReadModelSpecification(itemId);

            if (lstSpecification.Count > 0)
            {
                BindDropDown(ddlModelSpecification, "ModelSpecificationName", "ModelSpecificationId", lstSpecification);
            }
            else
            {
                BindEmptyDropDown(ddlModelSpecification);
            }
        }

        protected void ddlModelSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ModelspecificationId = Convert.ToInt32(ddlModelSpecification.SelectedValue);
            lstSpecification = itemStockBL.ReadUnitMeasurementBySpecification(ModelspecificationId);

            var itemBrands = brandBL.ReadItemBrandsById(Convert.ToInt32(ddlModelSpecification.SelectedValue), null);
            if (itemBrands != null && itemBrands.Count > 0)
            {
                List<MetaData> brands = itemBrands.ConvertAll(x => new MetaData { Id = x.Brand.BrandId, Name = x.Brand.BrandName }).ToList();
                BindDropDown(ddlBrand, "Name", "Id", brands);
            }
            if (lstSpecification != null && lstSpecification.Count > 0)
            {
                lblUnit.Text = lstSpecification[0].UnitMeasurement.Name;
                UnitMeasurementId = lstSpecification[0].UnitMeasurement.Id;
            }

        }

        protected void gvItemStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemStock.PageIndex = e.NewPageIndex;
            BindgvItemStock();
        }

        protected void gvItemStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string msg = string.Empty;
            int id = Convert.ToInt32(e.CommandArgument);
            itemStockId = id;
            if (e.CommandName == "cmdEdit")
            {

                GetItemStockById(itemStockId);
                btnSave.Visible = false;
                btnCancel.Visible = true;
                btnUpdate.Visible = true;
            }
            else if (e.CommandName == "cmdDelete")
            {
                msg = DeleteItemStock(itemStockId);
                ClearItemStock();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                if (btnSearch.Text == "Finish")
                {
                    BindgvSearchItemStock();
                    Enabled(false);
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    btnUpdate.Visible = false;
                }
                else
                {
                    BindgvItemStock();
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearItemStock();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            if (btnSearch.Text == "Finish")
            {
                Enabled(false);
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id;
            if (Convert.ToInt32(txtMaximumlevel.Text) < Convert.ToInt32(txtMinimumlevel.Text))
            {
                ShowMessageWithUpdatePanel("Maximum Consumption Must Be Grater Than Minimum Consumption");
                //ShowMessage("Maximum consumption must be grater than minimum consumption");
            }
            else if (Convert.ToInt32(txtMaximumlevel.Text) < Convert.ToInt32(txtNormallevel.Text))
            {
                ShowMessageWithUpdatePanel("Maximum Consumption Must Be Grater Than Normal Consumption");
                //ShowMessage("Maximum consumption must be grater than minimum consumption");
            }
            else if (Convert.ToInt32(txtNormallevel.Text) < Convert.ToInt32(txtMinimumlevel.Text))
            {
                //ShowMessage("Normal consumption must be grater than minimum consumption");
                ShowMessageWithUpdatePanel("Normal Consumption Must Be Grater Than Minimum Consumption");
            }
            else
            {
                id = UpdateItemStock(UpdateItemStockDetail());
                if (id > 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_ItemStock_MESSAGE);
                    ClearItemStock();
                    BindgvItemStock();
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    if (btnSearch.Text == "Finish")
                    {
                        BindgvSearchItemStock();
                        btnSave.Visible = false;
                        btnUpdate.Visible = false;
                        btnCancel.Visible = false;
                        Enabled(false);
                    }
                }
                else if (id == 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Search")
            {
                OpenPopupWithUpdatePanel(btnSearch, "SearchItemStock.aspx?PageName=ManageItemStock", "Title");
                //btnSearch.Text = "Finish";
            }
            else if (btnSearch.Text == "Finish")
            {
                btnSearch.Text = "Search";
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                Session["Flag"] = null;
                BindgvItemStock();
                ClearItemStock();
            }
        }

        protected void gvItemStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Quantity = (int)DataBinder.Eval(e.Row.DataItem, "QuantityOnhand");
                int MinLevel = (int)DataBinder.Eval(e.Row.DataItem, "MinimumLevel");
                int Maxlevel = (int)DataBinder.Eval(e.Row.DataItem, "MaximumLevel");
                int ReorderLevel = (int)DataBinder.Eval(e.Row.DataItem, "ReorderLevel");
                if (Quantity <= MinLevel)
                {
                    //e.Row.BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Tomato;


                }
                else if (Quantity >= Maxlevel)
                {
                    //e.Row.BackColor = System.Drawing.Color.LawnGreen;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Orange;
                }
                else
                {
                    //e.Row.BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

        //showing the data in Excel Sheet code
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
            gvItemStock.AllowPaging = false;
            gvItemStock.Columns.RemoveAt(8);
            gvItemStock.DataSource = GetItemStock();
            gvItemStock.DataBind();

            //gvSample is Gridview server control
            gvItemStock.RenderControl(htm);
            Response.Write(stringWrite);
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Do nothing
        }

        // import the data in Database code
        protected void btnImport_Click(object sender, EventArgs e)
        {

        }




        protected void gvItemStock_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Private Section
        public void SetValidationExp()
        {

            revQtOnHand.ValidationExpression = ValidationExpression.C_NUMERIC;
            revQtOnHand.ErrorMessage = "Quantity In Hand Should Be Numeric Value";
            revMinConsp.ValidationExpression = ValidationExpression.C_NUMERIC;
            revMinConsp.ErrorMessage = "Minimum Consumption Should Be Numeric Value";
            revMaxConsp.ValidationExpression = ValidationExpression.C_NUMERIC;
            revMaxConsp.ErrorMessage = "Maximum Consumption Should Be Numeric Value";
            revNormalConsp.ValidationExpression = ValidationExpression.C_NUMERIC;
            revNormalConsp.ErrorMessage = "Normal Consumption Should Be Numeric Value";
            revLeadTime.ValidationExpression = ValidationExpression.C_NUMERIC;
            revLeadTime.ErrorMessage = "Lead Time Should Be Numeric Value";
        }

        private string DeleteItemStock(int itemstockid)
        {
            return itemStockBL.DeleteItemStock(itemstockid, LoggedInUser.UserLoginId, DateTime.Now);
        }

        private void BindDropDownSpecification()
        {
            int itemId = Convert.ToInt32(ddlItem.SelectedValue);

            lstItem = itemBL.ReadItem(itemId);
            lstSpecification = itemBL.ReadModelSpecification(itemId);
            if (lstSpecification.Count > 0)
            {
                UnitMeasurementId = lstSpecification[0].UnitMeasurement.Id;
                BindDropDown(ddlModelSpecification, "ModelSpecificationName", "ModelSpecificationId", lstSpecification);
            }
            else
            {
                ddlModelSpecification.DataSource = new List<object>();
                ddlModelSpecification.DataBind();
            }
        }
        private void BindDropDownBrand()
        {
            int specificationId = Convert.ToInt32(ddlModelSpecification.SelectedValue);

            var itemBrands = brandBL.ReadItemBrandsById(specificationId, null);
            var brands = itemBrands.ConvertAll(x => new MetaData { Id = x.Brand.BrandId, Name = x.Brand.BrandName });
            if (brands.Count > 0)
            {

                BindDropDown(ddlBrand, "Name", "Id", brands);
            }
            else
            {
                ddlBrand.DataSource = new List<object>();
                ddlBrand.DataBind();
            }
        }
        private void Enabled(Boolean Condition)
        {
            ddlItem.Enabled = Condition;
            ddlModelSpecification.Enabled = Condition;
            lblUnit.Enabled = Condition;
            txtQuantity.Enabled = Condition;
            txtMinimumlevel.Enabled = Condition;
            txtMaximumlevel.Enabled = Condition;
            txtNormallevel.Enabled = Condition;
            txtLeadTime.Enabled = Condition;
        }

        private ItemStock UpdateItemStockDetail()
        {
            Decimal Max = Convert.ToInt32(txtMaximumlevel.Text.Trim());
            Decimal Min = Convert.ToInt32(txtMinimumlevel.Text.Trim());
            Decimal Nor = Convert.ToInt32(txtNormallevel.Text.Trim());
            Decimal LeadTime = Convert.ToInt32(txtLeadTime.Text.Trim());
            Decimal Avg = (Max + Min + Nor) / 3;

            Decimal Reorder_Level = (Max * LeadTime);
            Decimal Maximum_Level = (Reorder_Level + Reorder_Level - (Avg * LeadTime));
            Decimal Minimum_Level = (Reorder_Level - (Avg * LeadTime));

            ItemStock itemStock = new ItemStock();
            itemStock.ItemStockId = itemStockId;
            itemStock.Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            itemStock.ItemId = Convert.ToInt32(ddlItem.SelectedValue);
            //itemStock.ItemCategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            itemStock.ItemSpecificationId = Convert.ToInt32(ddlModelSpecification.SelectedValue);
            itemStock.ItemUnit = lblUnit.Text.Trim();
            itemStock.UnitMeasurementId = UnitMeasurementId;
            itemStock.QuantityOnhand = Convert.ToInt32(txtQuantity.Text.Trim());
            itemStock.MinimumLevel = Convert.ToInt32(Math.Round(Minimum_Level));
            itemStock.MaximumLevel = Convert.ToInt32(Math.Round(Maximum_Level));
            itemStock.ReorderLevel = Convert.ToInt32(Math.Round(Reorder_Level));
            itemStock.MaximumConsumption = Convert.ToInt32(txtMaximumlevel.Text.Trim());
            itemStock.MinimumConsumption = Convert.ToInt32(txtMinimumlevel.Text.Trim());
            itemStock.NormalConsumption = Convert.ToInt32(txtNormallevel.Text.Trim());
            itemStock.LeadTime = Convert.ToInt32(txtLeadTime.Text.Trim());
            itemStock.ModifiedBy = LoggedInUser.UserLoginId;
            itemStock.Brand.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
            return itemStock;
        }

        private int UpdateItemStock(ItemStock itemStock)
        {
            int id = itemStockBL.UpdateItemStock(itemStock);
            return id;
        }

        private void GetItemStockById(Int32 id)
        {
            id = itemStockId;
            List<ItemStock> lstItemStock = new List<ItemStock>();
            lstItemStock = itemStockBL.ReadItemStock(id);
            //ddlCategory.SelectedValue = lstItemStock[0].ItemCategoryId.ToString();
            //BindDropDownItem();
            if (lstItemStock[0].Store.StoreId>0)
            {
                ddlStore.SelectedValue = lstItemStock[0].Store.StoreId.ToString();
            }
          
            ddlItem.SelectedValue = lstItemStock[0].ItemId.ToString();
            BindDropDownSpecification();
            //item.ModelSpecification = new ModelSpecification();
            ddlModelSpecification.SelectedValue = lstItemStock[0].ItemSpecificationId.ToString();
            BindDropDownBrand();
            lblUnit.Text = lstItemStock[0].ItemUnit.ToString();
            UnitMeasurementId = lstItemStock[0].UnitMeasurementId;
            txtQuantity.Text = lstItemStock[0].QuantityOnhand.ToString();
            txtMinimumlevel.Text = lstItemStock[0].MinimumConsumption.ToString();
            txtMaximumlevel.Text = lstItemStock[0].MaximumConsumption.ToString();
            txtNormallevel.Text = lstItemStock[0].NormalConsumption.ToString();
            txtLeadTime.Text = lstItemStock[0].LeadTime.ToString();
            ddlBrand.SelectedValue = lstItemStock[0].Brand.BrandId.ToString();
        }

        private void BindgvItemStock()
        {
            gvItemStock.DataSource = GetItemStock();
            gvItemStock.DataBind();
        }

        private void BindgvSearchItemStock()
        {
            List<ItemStock> listItemStock = new List<ItemStock>();
            //listItemStock = (List<ItemStock>)Session["ListItemStock"];
            listItemStock = searchBL.SearchItemStockById(ItemStockIds);
            gvItemStock.DataSource = listItemStock;

            gvItemStock.DataBind();
        }

        private List<ItemStock> GetItemStock()
        {
            List<ItemStock> lstItemStock = new List<ItemStock>();
            lstItemStock = itemStockBL.ReadItemStock(null);
            return lstItemStock;
        }

        public void BindItem()
        {
            ItemBL itemBL = new ItemBL();
            List<Item> lstItem = new List<Item>();
            lstItem = itemBL.ReadItem(null);
            ddlItem.DataSource = lstItem;
            ddlItem.DataValueField = "ItemId";
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataBind();

            ddlItem.Items.Insert(0, "--Select--");
        }

        private void ClearItemStock()
        {
            ddlStore.SelectedIndex = 0;
            ddlItem.SelectedIndex = 0;
            BindEmptyDropDown(ddlModelSpecification);
            BindEmptyDropDown(ddlBrand);
            lblUnit.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtMinimumlevel.Text = string.Empty;
            txtMaximumlevel.Text = string.Empty;
            txtNormallevel.Text = string.Empty;
            txtLeadTime.Text = string.Empty;
        }

        private int CreateItemStock(ItemStock itemStock)
        {
            int id = itemStockBL.CreateItemStock(itemStock);
            return id;
        }

        private ItemStock GetItemStockDetail()
        {
            Decimal Max = Convert.ToInt32(txtMaximumlevel.Text.Trim());
            Decimal Min = Convert.ToInt32(txtMinimumlevel.Text.Trim());
            Decimal Nor = Convert.ToInt32(txtNormallevel.Text.Trim());
            Decimal LeadTime = Convert.ToInt32(txtLeadTime.Text.Trim());
            Decimal Avg = (Max + Min + Nor) / 3;

            Decimal Reorder_Level = (Max * LeadTime);
            Decimal Maximum_Level = (Reorder_Level + Reorder_Level - (Avg * LeadTime));
            Decimal Minimum_Level = (Reorder_Level - (Avg * LeadTime));

            ItemStock itemStock = new ItemStock();
            itemStock.Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            itemStock.ItemId = Convert.ToInt32(ddlItem.SelectedValue);
            itemStock.ItemSpecificationId = Convert.ToInt32(ddlModelSpecification.SelectedValue);
            itemStock.ItemUnit = lblUnit.Text.Trim();
            itemStock.UnitMeasurementId = UnitMeasurementId;
            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                itemStock.QuantityOnhand = Convert.ToInt32(txtQuantity.Text.Trim());
            }
            itemStock.MinimumLevel = Convert.ToInt32(Math.Round(Minimum_Level, 0));
            itemStock.MaximumLevel = Convert.ToInt32(Math.Round(Maximum_Level, 0));
            itemStock.ReorderLevel = Convert.ToInt32(Math.Round(Reorder_Level));
            itemStock.MaximumConsumption = Convert.ToInt32(txtMaximumlevel.Text.Trim());
            itemStock.MinimumConsumption = Convert.ToInt32(txtMinimumlevel.Text.Trim());
            itemStock.NormalConsumption = Convert.ToInt32(txtNormallevel.Text.Trim());
            itemStock.LeadTime = Convert.ToInt32(txtLeadTime.Text.Trim());
            itemStock.CreatedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;   

            itemStock.Brand.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
            return itemStock;
        }
        #endregion

        #region Public Property

        public int itemStockId
        {
            get
            {
                return (int)ViewState["itemStockId"];
            }
            set
            {
                ViewState["itemStockId"] = value;
            }
        }
        public string ItemStockIds
        {
            get
            {
                return (string)Session["ItemStockIds"];
            }
            set
            {
                Session["ItemStockIds"] = value;
            }
        }
        public int UnitMeasurementId
        {
            get
            {
                return (int)ViewState["UnitMeasurementId"];
            }
            set
            {
                ViewState["UnitMeasurementId"] = value;
            }
        }
        #endregion






    }
}