using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using DataAccessLayer;
using MiniERP.Shared;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;
using System.Web.Services;
using System.Globalization;
using System.IO;
using System.Data;
using System.Configuration;
using System.Net;
using BusinessAccessLayer.Quality;

namespace MiniERP.Quality.Parts
{
    public partial class ReceiveMaterialCompanyWorkOrder : System.Web.UI.UserControl
    {
        #region Global Variable
        int id = 0;
        int index = 0;
        string pageName = String.Empty;
        bool track = false;
        bool flag;
        string error_msg = string.Empty;

        MetaData metaData = new MetaData();
        BasePage base_Page = new BasePage();
        Item item = null;
        ModelSpecification modelSpecification = null;
        ItemTransaction item_Transaction = null;
        ReceiveMaterialCompanyWorkOrderDom receiveMaterialCWO = null;

        ItemBL item_BL = null;
        ItemModelBL item_Model_BL = null;
        CompanyWorkOrderBL companyWorkOrderBL = null;
        ReceiveMaterialCompanyWorkOrderBL receiveMaterialCWOBal = null;


        List<Item> lst_item = null;
        List<ModelSpecification> lst_item_model = null;
        List<CompanyWorkOrderDOM> lst_companyWorkOrder = null;
        List<WorkOrderMappingDOM> lst_work_order = null;
        List<ReceiveMaterialCompanyWorkOrderDom> lst_receiveMaterialCWO = null;
        #endregion

        #region Protected Method
        protected void Page_Load(object sender, EventArgs e)
        {
            //RecieveMaterialCWOId
            Int32 RecieveMaterialId = 0;
            //string RecieveMaterialCWONo = string.Empty;
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            if (!IsPostBack)
            {
                ClearWorkOrderFields();
                LoadDefaultData();

                btn_save_draft.Visible = false;
                btn_cancel_draft.Visible = false;

                gv_Item_Data.DataSource = null;
                gv_Item_Data.DataBind();

                RecieveMaterialId = Convert.ToInt32(Request.QueryString["ReceiveMaterialCWOId"]);
                this.RecieveMaterialCWOId = RecieveMaterialId;
            }
            if (!IsPostBack && RecieveMaterialCWOId > 0)
            {
                btn_save_draft.Visible = true;
                btn_cancel_draft.Visible = true;
                EmptyDocumentList();
                ReadCompanyWorkOrderReceiveMaterial(RecieveMaterialCWOId);
                ReadCompanyWorkOrderReceiveMaterialMappiing(RecieveMaterialCWOId);

                btn_save_draft.Text = "Update";

            }
        }

        protected void ddl_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_item.SelectedItem.ToString() != "--Select--")
            {
                id = Convert.ToInt32(ddl_item.SelectedValue);
            }
            BindItemModel(id);
        }

        protected void ddl_item_model_SelectedIndexChanged(object sender, EventArgs e)
        {
            item_Model_BL = new ItemModelBL();
            item = new Item();
            item.ModelSpecification = new ModelSpecification();

            item.ItemId = Convert.ToInt32(ddl_item.SelectedValue);
            item.ModelSpecification.ModelSpecificationId = Convert.ToInt32(ddl_item_model.SelectedValue);

            //Commented as the Data base for it is in process
            if (item.ModelSpecification.ModelSpecificationId != 0)
            {
                BindMake_UnitMeasurement(item);
            }
        }

        protected void ddl_CWO_Number_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CWO_Number.SelectedIndex != 0)
            {
                id = Convert.ToInt32(ddl_CWO_Number.SelectedValue);
            }
            BindWorkOrderNumber(id);
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (track == false)
            {
                lbl_duplicate_msg.Text = String.Empty;
                if (btn_add.Text == "Add")
                {
                    btn_save_draft.Visible = true;
                    btn_cancel_draft.Visible = true;

                    item_Transaction = GetControlsData();
                    if (base_Page.ItemTransactionList == null)
                    {

                        base_Page.ItemTransactionList = new List<ItemTransaction>();
                        base_Page.ItemTransactionList.Add(item_Transaction);
                    }
                    else
                    {
                        if (CheckDuplicateActivity(item_Transaction))
                        {
                            lbl_duplicate_msg.Text = GlobalConstants.L_Duplicate_Activity;
                            ddl_item.Focus();
                        }
                        else
                        {
                            base_Page.ItemTransactionList.Add(item_Transaction);
                        }
                    }
                }
                else if (btn_add.Text == "Update")
                {
                    if (this.lstIndex >= 0)
                    {
                        item_Transaction = GetControlsData();
                        if (CheckDuplicateActivity(item_Transaction))
                        {
                            lbl_duplicate_msg.Text = GlobalConstants.L_Duplicate_Activity;
                            ddl_item.Focus();
                        }
                        else
                        {
                            base_Page.ItemTransactionList = UpdateTempData(item_Transaction);
                            btn_add.Text = "Add";
                            lstIndex = -1;
                        }

                    }
                }
                BindItemtransaction();
                if (track == false)
                {
                    btn_cancel_Click(null, null);
                }
            }
        }

        protected void gv_Item_Data_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edit")
            {
                if (base_Page.ItemTransactionList[index].Item.ItemName != null)

                    ddl_item.SelectedValue = base_Page.ItemTransactionList[index].Item.ItemId.ToString();
                ddl_item.SelectedItem.Text = base_Page.ItemTransactionList[index].Item.ItemName.ToString();

                ddl_item_SelectedIndexChanged(null, null);
                lbl_category_level.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.Category.ItemCategoryName;
                hdf_category_level_id.Value = base_Page.ItemTransactionList[index].Item.ModelSpecification.Category.ItemCategoryId.ToString();

                ddl_item_model.SelectedValue = base_Page.ItemTransactionList[index].Item.ModelSpecification.ModelSpecificationId.ToString();
                ddl_item_model.SelectedItem.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.ModelSpecificationName.ToString();

                ddlStore.SelectedValue = base_Page.ItemTransactionList[index].Item.ModelSpecification.Store.StoreId.ToString();
                ddlStore.SelectedItem.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.Store.StoreName.ToString();
                ddlBrand.SelectedValue = base_Page.ItemTransactionList[index].Item.ModelSpecification.Brand.BrandId.ToString();
                ddlBrand.SelectedItem.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.Brand.BrandName.ToString();


                if (!String.IsNullOrEmpty(base_Page.ItemTransactionList[index].Item.ModelSpecification.Brand.BrandName))
                    //lbl_make.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.Brand.ToString();

                    txt_Number_of_Unit.Text = base_Page.ItemTransactionList[index].NumberOfUnit.ToString();
                if (base_Page.ItemTransactionList[index].Item.ModelSpecification.UnitMeasurement.Name != String.Empty)
                {
                    lbl_unit_of_measurement.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.UnitMeasurement.Name;
                    hdf_unit_of_measurement.Value = base_Page.ItemTransactionList[index].Item.ModelSpecification.UnitMeasurement.Id.ToString();
                }
                btn_add.Text = "Update";
                this.lstIndex = index;

            }
            else if (e.CommandName == "Delete")
            {
                //lst_item_transaction = (List<ItemTransaction>)Session["Data"];
                base_Page.ItemTransactionList.RemoveAt(index);
                //Session["Data"] = lst_item_transaction;
                if (base_Page.ItemTransactionList.Count == 0)
                {
                    Clear_Transaction_Fields();
                    btn_add.Text = "Add";
                    base_Page.ItemTransactionList = null;

                }
                gv_Item_Data.DataSource = base_Page.ItemTransactionList;
                gv_Item_Data.DataBind();
            }
        }

        protected void gv_Item_Data_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_Item_Data_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ClearControlField();
        }

        protected void btn_save_draft_Click(object sender, EventArgs e)
        {
            receiveMaterialCWO = new ReceiveMaterialCompanyWorkOrderDom();
            receiveMaterialCWO = GetReceiveMaterialCWODetail();
            if (RecieveMaterialCWOId > 0)
            {
                metaData = CreateRecieveMaterialCWO(receiveMaterialCWO, RecieveMaterialCWOId);
            }
            else
            {
                metaData = CreateRecieveMaterialCWO(receiveMaterialCWO, null);
            }
            if (metaData.Id > 0)
            {
                CreateDocumentMapping();
                //base_Page.ItemTransactionList = null;
                if (RecieveMaterialCWOId > 0)
                {
                    base_Page.Alert("Company Work Order Recieve Material No.: " + metaData.Name + " Updated Successfully", btn_save_draft, "ViewReceiveMaterialCompanyWorkOrder.aspx");

                }
                else
                {
                    base_Page.Alert("Company Work Order Recieve Material No.: " + metaData.Name + " Created Successfully", btn_save_draft, "ReceiveMaterialCompanyWorkOrder.aspx");

                }
            }
            if (base_Page.ItemTransactionList == null)
            {
                ddl_item.Focus();
            }
            else
            {
                ClearWorkOrderFields();
                Clear_Transaction_Fields();
                btn_save_draft.Visible = false;
                btn_cancel_draft.Visible = false;
            }
            // Response.Redirect("ReceiveMaterialCompanyWorkOrder.aspx");

        }

        protected void btn_cancel_draft_Click(object sender, EventArgs e)
        {
            ClearWorkOrderFields();
            Clear_Transaction_Fields();
            Response.Redirect("ReceiveMaterialCompanyWorkOrder.aspx");
        }

        #endregion

        #region Private Method

        public void ReadCompanyWorkOrderReceiveMaterial(Int32 RMCWOId)
        {
            lst_receiveMaterialCWO = new List<ReceiveMaterialCompanyWorkOrderDom>();
            receiveMaterialCWOBal = new ReceiveMaterialCompanyWorkOrderBL();
            lst_receiveMaterialCWO = receiveMaterialCWOBal.ReadReceiveMaterailCompanyWorkOrderById(RMCWOId);
            if (lst_receiveMaterialCWO.Count > 0)
            {
                ddl_CWO_Number.SelectedValue = lst_receiveMaterialCWO[0].CompanyWorkOrder.CompanyWorkOrderId.ToString();
                ddl_CWO_Number_SelectedIndexChanged(null, null);
                ddl_work_order_number.SelectedValue = lst_receiveMaterialCWO[0].Quotation.WorkOrderId.ToString();
                txt_receive_date.Text = lst_receiveMaterialCWO[0].Receive_Date.ToString("dd/MM/yyyy");
                txt_description.Text = lst_receiveMaterialCWO[0].Description;
                GetDocumentDataById(lst_receiveMaterialCWO[0].UploadFile.DocumentId);
            }
        }

        public void ReadCompanyWorkOrderReceiveMaterialMappiing(Int32 RMCWOId)
        {

            base_Page.ItemTransactionList = new List<ItemTransaction>();
            receiveMaterialCWOBal = new ReceiveMaterialCompanyWorkOrderBL();
            base_Page.ItemTransactionList = receiveMaterialCWOBal.ReadRMCWOMapping(RMCWOId);
            if (base_Page.ItemTransactionList.Count > 0)
            {
                gv_Item_Data.DataSource = base_Page.ItemTransactionList;
                gv_Item_Data.DataBind();
            }
        }

        private Boolean CheckDuplicateActivity(ItemTransaction item_Transaction)
        {
            id = 0;
            foreach (ItemTransaction item in base_Page.ItemTransactionList)
            {
                id = id + 1;
                if (item_Transaction.Item.ItemId == item.Item.ItemId
                    && item_Transaction.Item.ModelSpecification.ModelSpecificationId == item.Item.ModelSpecification.ModelSpecificationId)
                {
                    if (lstIndex != -1 && id == lstIndex + 1)
                    {
                        track = false;
                        break;
                    }
                    else
                    {
                        track = true;
                        break;
                    }
                }
                else
                {
                    track = false;
                }
            }

            return track;
        }

        private List<ItemTransaction> UpdateTempData(ItemTransaction item_Transaction)
        {
            base_Page.ItemTransactionList[this.lstIndex].Item.ItemId = item_Transaction.Item.ItemId;
            base_Page.ItemTransactionList[this.lstIndex].Item.ItemName = item_Transaction.Item.ItemName;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.Category.ItemCategoryId = item_Transaction.Item.ModelSpecification.Category.ItemCategoryId;
            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.Category.ItemCategoryName = item_Transaction.Item.ModelSpecification.Category.ItemCategoryName;


            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.ModelSpecificationId = item_Transaction.Item.ModelSpecification.ModelSpecificationId;
            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.ModelSpecificationName = item_Transaction.Item.ModelSpecification.ModelSpecificationName;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.Brand = item_Transaction.Item.ModelSpecification.Brand;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.UnitMeasurement.Id = item_Transaction.Item.ModelSpecification.UnitMeasurement.Id;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.UnitMeasurement.Name = item_Transaction.Item.ModelSpecification.UnitMeasurement.Name;

            base_Page.ItemTransactionList[this.lstIndex].NumberOfUnit = item_Transaction.NumberOfUnit;

            return base_Page.ItemTransactionList;
        }

        private MetaData CreateRecieveMaterialCWO(ReceiveMaterialCompanyWorkOrderDom recieveMatarialCWO, Int32? RMCWOID)
        {

            if (base_Page.ItemTransactionList != null)
            {

                receiveMaterialCWOBal = new ReceiveMaterialCompanyWorkOrderBL();
                metaData = receiveMaterialCWOBal.CreateRecieveMaterialCWO(recieveMatarialCWO, RMCWOID);
            }
            return metaData;
        }

        private ReceiveMaterialCompanyWorkOrderDom GetReceiveMaterialCWODetail()
        {
            receiveMaterialCWO = new ReceiveMaterialCompanyWorkOrderDom();
            receiveMaterialCWO.Quotation = new QuotationDOM();
            receiveMaterialCWO.Quotation.StatusType = new MetaData();
            //receiveMaterialCWO.Quotation.Item_Transaction.MetaProperty = new MetaData();

            receiveMaterialCWO.CompanyWorkOrder = new CompanyWorkOrderDOM();
            receiveMaterialCWOBal = new ReceiveMaterialCompanyWorkOrderBL();
            if (RecieveMaterialCWOId > 0)
            {
                receiveMaterialCWO.ContractReceiveMaterialId = RecieveMaterialCWOId;
            }
            receiveMaterialCWO.CompanyWorkOrder.CompanyWorkOrderId = Convert.ToInt32(ddl_CWO_Number.SelectedValue);
            receiveMaterialCWO.Quotation.WorkOrderId = Convert.ToInt32(ddl_work_order_number.SelectedValue);
            if (!String.IsNullOrEmpty(txt_receive_date.Text))
            {
                receiveMaterialCWO.Receive_Date = DateTime.ParseExact(txt_receive_date.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            //receiveMaterialCWO.Receive_Date = Convert.ToDateTime(txt_receive_date.Text.Trim());
            receiveMaterialCWO.Description = txt_description.Text;
            receiveMaterialCWO.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            receiveMaterialCWO.UploadFile = new Document();
            receiveMaterialCWO.UploadFile.DocumentId = base_Page.DocumentStackId;
            receiveMaterialCWO.CreatedBy = base_Page.LoggedInUser.UserLoginId;
            try
            {

                Label lblIndex = null;
                for (int i = 0; i < gv_Item_Data.Rows.Count; i++)
                {
                    lblIndex = (Label)gv_Item_Data.Rows[i].Cells[0].FindControl("lblIndex");


                }
                //if (base_Page.ItemTransactionList == null && string.IsNullOrEmpty(lblIndex.Text.Trim()))
                //{
                //    error_msg += "<ul><li>Work Details not created.</li></ul>";
                //}

                if (!string.IsNullOrEmpty(lblIndex.Text.Trim()) && base_Page.ItemTransactionList != null)
                {
                    receiveMaterialCWO.Quotation.ItemTransaction = base_Page.ItemTransactionList;
                }
                //if (error_msg!=string.Empty)
                //{
                //    ltrl_error_msg.Text = error_msg;
                //}
            }
            catch (Exception e)
            {
                base_Page.Alert("Work Details not created", btn_save_draft);

                //ltrl_error_msg.Text = error_msg;
            }

            return receiveMaterialCWO;

        }

        private void ClearWorkOrderFields()
        {
            ddl_CWO_Number.SelectedIndex = 0;
            ddl_work_order_number.SelectedIndex = 0;
            txt_receive_date.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            txt_description.Text = string.Empty;
            EmptyDocumentList();
        }

        private void Clear_Transaction_Fields()
        {
            hdf_category_level_id.Value = String.Empty;
            lbl_category_level.Text = String.Empty;
            BindItem(null);
            BindItemModel(0);
            //lbl_make.Text = String.Empty;
            lbl_unit_of_measurement.Text = String.Empty;

            txt_Number_of_Unit.Text = String.Empty;
            gv_Item_Data.DataSource = null;
            gv_Item_Data.DataBind();


        }

        private void ClearControlField()
        {
            hdf_category_level_id.Value = String.Empty;

            BindItem(null);
            BindItemModel(0);
            ddlStore.SelectedIndex = 0;
            ddlBrand.SelectedIndex = 0;
            ddl_item.SelectedIndex = 0;
            ddl_item_model.SelectedIndex = 0;
            lbl_category_level.Text = string.Empty;
            //lbl_make.Text = string.Empty;
            txt_Number_of_Unit.Text = string.Empty;
            lbl_unit_of_measurement.Text = string.Empty;
        }
        private void BindItemtransaction()
        {
            gv_Item_Data.DataSource = base_Page.ItemTransactionList;
            gv_Item_Data.DataBind();

        }

        private void LoadDefaultData()
        {
            base_Page.Page_Name = pageName;
            txt_receive_date.Text = DateTime.Now.Date.ToString("dd'/'MM'/'yyyy");
            Session["tempItemData"] = null;

            BindCompanyWorkOrderNumber();
            BindItem(null);
            //SetItemState();
            SetRegularExpressions();
            //Clear_Session();
            BindStore();
            BindBrand();

        }

        private void BindStore()
        {
            StoreBL storeBL = new StoreBL();
            List<Store> lstStore = new List<Store>();
            lstStore = storeBL.ReadStore(null);
            ddlStore.DataSource = lstStore;
            ddlStore.DataTextField = "StoreName";
            ddlStore.DataValueField = "StoreId";
            ddlStore.DataBind();
            ddlStore.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlStore.SelectedValue = "0";
        }

        private void BindBrand()
        {
            BrandBL brandBL = new BrandBL();
            List<Brand> lstBrand = new List<Brand>();
            lstBrand = brandBL.ReadBrands(null);
            ddlBrand.DataSource = lstBrand;
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandId";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlBrand.SelectedValue = "0";
        }

        private void BindItem(int? id)
        {
            item_BL = new ItemBL();
            lst_item = new List<Item>();
            if (id != 0)
            {
                lst_item = item_BL.ReadItemByCategoryId(id);
                ddl_item.DataSource = lst_item;
                base_Page.BindDropDown(ddl_item, "ItemName", "ItemId", lst_item);
            }
            else
            {
                ddl_item.Items.Clear();
                ddl_item_model.Items.Clear();
                base_Page.BindEmptyDropDown(ddl_item);
                base_Page.BindEmptyDropDown(ddl_item_model);
            }
        }

        private void BindItemModel(int? id)
        {
            item_Model_BL = new ItemModelBL();
            lst_item_model = new List<ModelSpecification>();
            if (id != 0)
            {
                lst_item_model = item_Model_BL.ReadItemModel(id);
                base_Page.BindDropDown(ddl_item_model, "ModelSpecificationName", "ModelSpecificationId", lst_item_model);
            }
            else
            {
                ddl_item_model.Items.Clear();
                base_Page.BindEmptyDropDown(ddl_item_model);
            }
        }

        private void BindMake_UnitMeasurement(Item item)
        {
            modelSpecification = new ModelSpecification();
            modelSpecification.UnitMeasurement = new MetaData();

            modelSpecification = item_Model_BL.ReadMakeandUnitofMeasurement(item);

            //lbl_make.Text = modelSpecification.Brand.BrandName;

            lbl_unit_of_measurement.Text = modelSpecification.UnitMeasurement.Name;
            hdf_unit_of_measurement.Value = modelSpecification.UnitMeasurement.Id.ToString();

            hdf_category_level_id.Value = modelSpecification.Category.ItemCategoryId.ToString();
            lbl_category_level.Text = modelSpecification.Category.ItemCategoryName;
        }

        private void BindCompanyWorkOrderNumber()
        {
            companyWorkOrderBL = new CompanyWorkOrderBL();
            lst_companyWorkOrder = new List<CompanyWorkOrderDOM>();
            lst_companyWorkOrder = companyWorkOrderBL.ReadCompOrder(null);
            var lstNew = lst_companyWorkOrder.Where(p => p.StatusType.Id == Convert.ToInt16(StatusType.Generated));

            base_Page.BindDropDown(ddl_CWO_Number, "CompanyWorkOrderNumber", "CompanyWorkOrderId", lstNew);
        }

        private void BindWorkOrderNumber(Int32 id)
        {
            lst_work_order = new List<WorkOrderMappingDOM>();
            companyWorkOrderBL = new CompanyWorkOrderBL();

            lst_work_order = companyWorkOrderBL.ReadCompanyWorkOrderMapping(id);

            if (lst_work_order.Count > 0)
                base_Page.BindDropDown(ddl_work_order_number, "WorkOrderNumber", "CompanyWorkOrderMappingId", lst_work_order);
            else
                base_Page.BindEmptyDropDown(ddl_work_order_number);
        }

        private ItemTransaction GetControlsData()
        {
            item_Transaction = new ItemTransaction();
            item_Transaction.Item = new Item();
            item_Transaction.MetaProperty = new MetaData();
            item_Transaction.TaxInformation = new Tax();
            item_Transaction.TaxInformation.DiscountMode = new MetaData();
            item_Transaction.DeliverySchedule = new DeliveryScheduleDOM();
            item_Transaction.Item.ModelSpecification = new ModelSpecification();
            item_Transaction.Item.ModelSpecification.Category = new ItemCategory();
            item_Transaction.Item.ModelSpecification.UnitMeasurement = new MetaData();

            if (hdf_category_level_id.Value != String.Empty)
            {
                item_Transaction.Item.ModelSpecification.Category.ItemCategoryId = Convert.ToInt32(hdf_category_level_id.Value);
                item_Transaction.Item.ModelSpecification.Category.ItemCategoryName = lbl_category_level.Text;
            }

            if (Convert.ToInt32(ddl_item.SelectedValue) == 0)
            {
                item_Transaction.Item.ItemId = 0;
                item_Transaction.Item.ItemName = String.Empty;
            }
            else
            {
                item_Transaction.Item.ItemId = Convert.ToInt32(ddl_item.SelectedValue);
                item_Transaction.Item.ItemName = ddl_item.SelectedItem.ToString();
            }

            if (!string.IsNullOrEmpty(txt_Number_of_Unit.Text.Trim()))
                item_Transaction.NumberOfUnit = Convert.ToDecimal(txt_Number_of_Unit.Text.Trim());

            if (Convert.ToInt32(ddl_item_model.SelectedValue) == 0)
            {
                item_Transaction.Item.ModelSpecification.ModelSpecificationId = 0;
                item_Transaction.Item.ModelSpecification.ModelSpecificationName = String.Empty;
            }
            else
            {
                item_Transaction.Item.ModelSpecification.ModelSpecificationId = Convert.ToInt32(ddl_item_model.SelectedValue);
                item_Transaction.Item.ModelSpecification.ModelSpecificationName = ddl_item_model.SelectedItem.ToString();
                if (ddlStore.SelectedIndex > 0)
                {
                    item_Transaction.Item.ModelSpecification.Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                    item_Transaction.Item.ModelSpecification.Store.StoreName = ddlStore.SelectedItem.Text.ToString();

                }
                if (ddlBrand.SelectedIndex > 0)
                {
                    item_Transaction.Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
                    item_Transaction.Item.ModelSpecification.Brand.BrandName = ddlBrand.SelectedItem.Text.ToString();

                }


                item_Transaction.Item.ModelSpecification.UnitMeasurement.Id = Convert.ToInt32(hdf_unit_of_measurement.Value);
                item_Transaction.Item.ModelSpecification.UnitMeasurement.Name = lbl_unit_of_measurement.Text;

            }
            return item_Transaction;
        }

        private void SetRegularExpressions()
        {

            //rev_number_of_unit.ValidationExpression = ValidationExpression.C_NUMERIC;
            rev_number_of_unit.ValidationExpression = ValidationExpression.C_NUMERIC_DISCOUNT;
            rev_number_of_unit.ErrorMessage = "In quantity only numeric  values are allowed";

        }
        #endregion

        #region Public Properties

        public String PageType
        {
            get { return Convert.ToString(ViewState["PageType"]); }

            set { ViewState["PageType"] = value; }
        }

        public int lstIndex
        {
            get
            {
                try
                {
                    return (int)ViewState["lstIndex"];
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                ViewState["lstIndex"] = value;
            }
        }

        public int RecieveMaterialCWOId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["RecieveMaterialCWOId"];
                }
                catch
                {

                    return 0;
                }
            }
            set
            {
                ViewState["RecieveMaterialCWOId"] = value;
            }
        }

        public String RecieveMaterialCWONumber
        {
            get
            {
                try
                {
                    return (String)ViewState["RecieveMaterialCWONumber"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["RecieveMaterialCWONumber"] = value;
            }
        }



        #endregion


        #region Upload Document Code

        #region Private Global Variable(s)

        DocumentBL documentBL = new DocumentBL();

        Document doc = null;
        Int32 Year = 0;
        Int32 Index = 0;

        String Head_Folder_Path = String.Empty;
        String Sub_Folder_Path = String.Empty;
        String File_Extension = String.Empty;
        String File_Path = String.Empty;

        DataSet page_Data = null;

        List<Document> lst_documents = null;
        #endregion

        #region Protected Methods

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            //For Copy Quotation, It is TRUE
            LoadData(false);
        }

        private void LoadData(bool forCopy)
        {
            LoadDocument(forCopy);
            DirectoryHandle(FileUpload_Control);
            BindDocument();
        }

        private void LoadDocument(bool forCopy)
        {
            ManageSession(forCopy);

            if (base_Page.DocumentStackId == 0)
            {
                CreateAndReadDocumentStackId();
            }

            BindDocument();
        }

        protected void gv_documents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "FileDelete")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                lst_documents = new List<Document>();
                lst_documents = base_Page.DocumentsList;
                lst_documents.RemoveAt(Index);
                base_Page.DocumentsList = lst_documents;
                BindDocument();
            }
            else if (e.CommandName == "Open_File")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                Index = Convert.ToInt32(e.CommandArgument);
                lst_documents = new List<Document>();
                lst_documents = base_Page.DocumentsList;

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lnk_btn_file");

                string fileName = lst_documents[Index].Replaced_Name;
                string strURL = lst_documents[Index].Path + @"\" + lst_documents[Index].Replaced_Name;

                Session["FilePath"] = Server.MapPath(strURL);
                Session["OriginalFileName"] = lst_documents[Index].Original_Name;
                Session["ReplacedFileName"] = lst_documents[Index].Replaced_Name;
                base_Page.OpenPopupWithUpdatePanelForFileDownload(lbtn, "../Parts/FileDownload.aspx?id=" + "File", "DownloadFile");


                //File_Path = lst_documents[Index].Path + @"\" + lst_documents[Index].Replaced_Name;
                //WebClient req = new WebClient();
                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.ClearContent();
                //response.ClearHeaders();
                //response.Buffer = true;
                ////response.AddHeader("Content-Disposition", "attachment;filename=" + lst_documents[Index].Replaced_Name.Replace(lst_documents[Index].Replaced_Name, lst_documents[Index].Original_Name));
                //response.AddHeader("Content-Disposition", "attachment;filename=" + lst_documents[Index].Replaced_Name);
                //byte[] data = req.DownloadData(Server.MapPath(File_Path));
                //response.BinaryWrite(data);
                //response.Flush();
            }
        }

        #endregion

        #region Private Methods

        private void ManageSession(bool forCopy)
        {
            RequestPageName = pageName;
            if (forCopy)
            {
                base_Page.DocumentStackId = 0;
            }
            else if (base_Page.Page_Name == null || base_Page.Page_Name != RequestPageName)
            {
                base_Page.Page_Name = RequestPageName;
                base_Page.DocumentStackId = 0;
                base_Page.DocumentSerial = 0;
                base_Page.DocumentsList = null;
            }
            else
            {
                //GO AHEAD
            }
        }

        private Int32 CreateAndReadDocumentStackId()
        {
            doc = new Document();
            doc.CreatedBy = base_Page.LoggedInUser.UserLoginId;
            base_Page.DocumentStackId = documentBL.CreateAndReadDocumnetStackId(doc);
            return base_Page.DocumentStackId;
        }

        private void DirectoryHandle(FileUpload fileupload)
        {
            if (fileupload.HasFile)
            {
                if (fileupload.FileContent.Length > 10485760)
                {
                    base_Page.Alert("You can upload up to 10 megabytes (MB) in size at a time", FileUpload_Control);
                }

                else
                {
                    Year = DateTime.Now.Year;

                    //Get list of pages
                    page_Data = new DataSet();
                    page_Data.ReadXml(Server.MapPath(ConfigurationManager.AppSettings["PageDictionary_Path"].ToString()));

                    Head_Folder_Path = Server.MapPath(@"\" + ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString());
                    //Check to existance of Main Folder
                    if (!Directory.Exists(Head_Folder_Path))
                    {
                        Directory.CreateDirectory(Head_Folder_Path);
                    }
                    //For Check to existance of Sub-Folders and if not, then create
                    foreach (DataRow dr in page_Data.Tables[0].Rows)
                    {
                        Sub_Folder_Path = Head_Folder_Path + @"\" + dr["Page"].ToString();
                        if (!Directory.Exists(Sub_Folder_Path))
                        {
                            Directory.CreateDirectory(Sub_Folder_Path);
                        }
                    }
                    //If folder exist then Upload Document in respective folder
                    Sub_Folder_Path = Head_Folder_Path + @"\" + RequestPageName;

                    if (Directory.Exists(Sub_Folder_Path))
                    {
                        if (base_Page.DocumentStackId != 0)
                        {
                            doc = new Document();
                            lst_documents = new List<Document>();
                            flag = false;

                            doc.Original_Name = fileupload.FileName.Split('\\').Last();
                            if (base_Page.DocumentsList != null)
                            {
                                foreach (Document item in base_Page.DocumentsList)
                                {
                                    if (item.Original_Name == doc.Original_Name)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (flag == true)
                            {
                                base_Page.Alert(GlobalConstants.M_File_Exist, FileUpload_Control);
                            }
                            else
                            {
                                base_Page.DocumentSerial = base_Page.DocumentSerial + 1;

                                File_Extension = System.IO.Path.GetExtension(fileupload.FileName.Split('\\').Last());
                                doc.Replaced_Name = Convert.ToString(base_Page.DocumentStackId) + "_" + Convert.ToString(base_Page.DocumentSerial) + File_Extension;

                                File_Path = Sub_Folder_Path + @"\" + doc.Replaced_Name;

                                //Upload file in respective path
                                FileUpload_Control.SaveAs(File_Path);

                                doc.DocumentId = base_Page.DocumentStackId;

                                doc.Path = ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString() + @"\" + RequestPageName;
                                doc.LastIndex = base_Page.DocumentSerial;


                                if (base_Page.DocumentsList == null)
                                {
                                    lst_documents.Add(doc);
                                }
                                else
                                {
                                    lst_documents = base_Page.DocumentsList;
                                    lst_documents.Add(doc);
                                }

                                //Add Doc's info in list
                                base_Page.DocumentsList = lst_documents;
                            }
                        }
                    }
                }
            }
        }

        private void BindDocument()
        {
            if (base_Page.DocumentsList != null)
            {
                gv_documents.DataSource = base_Page.DocumentsList;
            }
            else
            {
                gv_documents.DataSource = null;
            }
            gv_documents.DataBind();
        }

        private void CreateDocumentMapping()
        {
            documentBL = new DocumentBL();
            lst_documents = new List<Document>();
            lst_documents = base_Page.DocumentsList;

            if (lst_documents != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                documentBL.ResetDocumentMapping(Convert.ToInt32(base_Page.DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document lstdoc in lst_documents)
                {
                    doc = new Document();
                    doc.DocumentId = Convert.ToInt32(base_Page.DocumentStackId);
                    doc.Original_Name = lstdoc.Original_Name;
                    doc.Replaced_Name = lstdoc.Replaced_Name;
                    doc.Path = lstdoc.Path;
                    //DocumentSerial is the last updated document
                    doc.LastIndex = base_Page.DocumentSerial;
                    doc.CreatedBy = base_Page.LoggedInUser.UserLoginId;
                    doc.Id = lstdoc.Id;
                    documentBL.CreateDocumentMapping(doc);
                }
            }
        }

        private void GetDocumentData(List<SupplierRecieveMatarial> lst_quotation)
        {
            documentBL = new DocumentBL();
            lst_documents = new List<Document>();
            lst_documents = documentBL.ReadDocumnetMapping(lst_quotation[0].UploadFile.DocumentId);
            if (lst_documents.Count >= 1)
            {
                base_Page.DocumentsList = lst_documents;
                base_Page.DocumentStackId = lst_documents[0].DocumentId;

                base_Page.DocumentSerial = lst_documents[0].LastIndex;
                base_Page.Page_Name = pageName;
                BindDocument();
            }
        }

        //By Anand
        private void GetDocumentDataById(Int32 DocumentId)
        {
            documentBL = new DocumentBL();
            lst_documents = new List<Document>();
            if (DocumentId != Int32.MinValue)
            {
                lst_documents = documentBL.ReadDocumnetMapping(DocumentId);
                if (lst_documents.Count >= 1)
                {
                    base_Page.DocumentsList = lst_documents;
                    base_Page.DocumentStackId = lst_documents[0].DocumentId;
                    base_Page.DocumentSerial = lst_documents[0].LastIndex;
                    //base_Page.Page_Name = RequestPageName;
                    BindDocument();
                }
                else
                {
                    base_Page.DocumentsList = null;
                    BindDocument();
                }
            }
            else
            {
                base_Page.DocumentsList = null;
                BindDocument();
            }
        }

        public void EmptyDocumentList()
        {
            base_Page.DocumentStackId = 0;
            base_Page.DocumentSerial = 0;
            base_Page.DocumentsList = null;
            BindDocument();
        }

        #endregion

        #region Public Properties

        public String RequestPageName
        {
            get
            {
                return (String)ViewState["Page"];
            }
            set
            {
                ViewState["Page"] = value;
            }
        }

        #endregion



        #endregion


    }
}