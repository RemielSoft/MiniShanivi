using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
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

namespace MiniERP.Parts
{
    public partial class Quotation : System.Web.UI.UserControl
    {
        #region Private Global Variables

        //For Duplicate Activity
        bool track = false;
        bool flag;

        //For Tax calculation
        decimal discount = 0;
        decimal net_value = 0;
        decimal total_net_value = 0;
        decimal total_net_value_with_tax = 0;
        decimal total = 0;
        decimal DiscountRate = 0;

        //For Id's
        int id = 0;
        int index = 0;

        string pageName = String.Empty;
        String error_msg = String.Empty;
        String selected = "0";
        String Quotation_number = String.Empty;

        ContractorBL contractor_BL = null;
        SupplierBL supplier_BL = null;
        CompanyWorkOrderBL companyWorkOrderBL = null;

        ItemBL item_BL = null;
        ItemModelBL item_Model_BL = null;
        QuotationBL quotation_BL = null;
        DocumentBL document_BL = null;
        TaxBL taxBL = new TaxBL();
        DeliveryScheduleBL delivery_schedule_BL = null;
        PaymentTermBL payment_Term_BL = null;
        TermAndConditionBL term_condition_BL = null;

        BasePage base_Page = new BasePage();
        Item item = null;
        ItemTransaction item_Transaction = null;
        ModelSpecification modelSpecification = null;
        Document document = null;
        QuotationDOM quotation = null;
        DeliveryScheduleDOM deliverySchedule = null;
        PaymentTerm paymentTerm = null;
        TermAndCondition termAndCondition = null;

        List<Contractor> lst_contractor = null;
        List<Supplier> lst_supplier = null;
        List<WorkOrderMappingDOM> lst_work_order = null;
        List<CompanyWorkOrderDOM> lst_companyWorkOrder = null;
        List<Item> lst_item = null;
        List<ModelSpecification> lst_item_model = null;
        List<DeliveryScheduleDOM> lst_delivery_schedule = null;
        List<MetaData> lst_metaData = null;
        List<QuotationDOM> lst_quotation = null;
        List<Document> lst_document = null;
        List<ItemTransaction> lst_item_transaction = null;
        List<PaymentTerm> lst_payment_term = null;
        List<TermAndCondition> lst_term_condition = null;
        List<ServiceDetailDOM> lstServiceDetail = null;



        #endregion

        public void LoadControl()
        {
            if (base_Page.ActiveTab == 0)
            {
            }
        }

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            if ((!IsPostBack))
            {
                //Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                LoadDefaultData();
                BindRadioButton();
            }
            if ((!IsPostBack) && Request.QueryString["quotationId"] != null)
            {
                int number;
                bool result = Int32.TryParse(Convert.ToString(Request.QueryString["quotationId"]), out number);
                if (result)
                {
                    id = number;
                    imgbtn_search_Click(null, null);
                }
            }


        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        protected void ddl_contrcat_number_SelectedIndexChanged(object sender, EventArgs e)
        {
            // On Here Work Order Number of Company Work Order be bind in a Drop Down 
            //and on basis of that the Item Details are to be Shown in Pop-Up
            if (ddl_contrcat_number.SelectedIndex != 0)
            {
                id = Convert.ToInt32(ddl_contrcat_number.SelectedValue);
            }
            BindWorkOrder(id);

        }

        protected void ddl_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_item.SelectedItem.ToString() != "--Select--")
            {
                id = Convert.ToInt32(ddl_item.SelectedValue);
            }
            BindItemModel(id);
        }

        protected void lbtn_work_details_Click(object sender, EventArgs e)
        {
            if (ddl_contrcat_number.SelectedIndex != 0 && ddl_work_order_number.SelectedIndex != 0)
                base_Page.OpenPopupWithUpdatePanel(lbtn_work_details,
                    "Parts/ItemDetails.aspx?id=" + ddl_contrcat_number.SelectedValue +
                                            "&contract_no=" + ddl_contrcat_number.SelectedItem.Text +
                                            "&no=" + ddl_work_order_number.SelectedValue +
                                            "&work_order_no=" + ddl_work_order_number.SelectedItem.Text,
                                            "Work Details");
            else
            {
                ddl_work_order_number.Focus();
                base_Page.Alert("Please Select Work Order Number.", lbtn_work_details);
            }
        }

        protected void lbtnServiceDescription_Click(object sender, EventArgs e)
        {
            base_Page.OpenPopupWithUpdatePanel(lbtn_work_details, "Parts/ServiceDescription.aspx", "Work Details");
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

        protected void rbtn_AddItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbtnTaxType.SelectedValue = "2";
            selected = rbtn_AddItem.SelectedValue;

            //----------------SUNDEEP------------
            if (selected == "0")
            {
                EnableDisableItem(true);
                rbtn_AddItem.SelectedValue = "0";
                SetRequiredFieldValidatorsForItem("add");
                //rfv_activity_desc.ValidationGroup = "cancel";
            }
            else
            {
                rbtn_AddItem.SelectedValue = "1";
                ClearItemFields();
                EnableDisableItem(false);
                SetRequiredFieldValidatorsForItem("cancel");
                //rfv_activity_desc.ValidationGroup = "add";

                //--------------------------------
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(FileUpload1.FileName))
            //{
            //    //code for file upload
            //    ReadDataFromExcel();
            //}

            track = false;
            net_value = Convert.ToDecimal(txt_Per_Unit_Cost.Text.Trim());
            if (ddl_discount_mode.SelectedValue != Convert.ToString(DiscountType.None) && txt_Per_Unit_Discount.Text.Trim() != String.Empty)
            {
                discount = Convert.ToDecimal(txt_Per_Unit_Discount.Text.Trim());
                if (ddl_discount_mode.SelectedValue == Convert.ToInt32(DiscountType.Value).ToString())
                {
                    if (discount > net_value)
                    {
                        lbl_duplicate_activity.Text = GlobalConstants.M_Discount_More_Than_Cost;
                        track = true;
                        txt_Per_Unit_Discount.Focus();
                    }
                }
                else if (ddl_discount_mode.SelectedValue == Convert.ToInt32(DiscountType.Percentage).ToString())
                {
                    if ((net_value * discount / 100) > net_value)
                    {
                        lbl_duplicate_activity.Text = GlobalConstants.M_Discount_More_Than_Cost;
                        track = true;
                        txt_Per_Unit_Discount.Focus();
                    }
                }

            }

            if (track == false)
            {
                lbl_duplicate_activity.Text = String.Empty;
                if (btn_add.Text == "Add")
                {
                    item_Transaction = GetControlsData();
                    if (base_Page.ItemTransactionList == null)
                    {
                        base_Page.ItemTransactionList = new List<ItemTransaction>();
                        base_Page.ItemTransactionList.Add(item_Transaction);
                    }
                    else
                    {
                        if (pageName == GlobalConstants.P_Contractor_Quotation)
                        {
                            if (CheckDuplicateActivity(item_Transaction))
                            {
                                lbl_duplicate_activity.Text = GlobalConstants.L_Duplicate_Activity;
                                ddlServiceDescription.Focus();
                            }
                            else
                            {
                                base_Page.ItemTransactionList.Add(item_Transaction);
                            }
                        }
                        else if (pageName == GlobalConstants.P_Supplier_Quotation)
                        {
                            if (CheckDuplicateItem(item_Transaction))
                            {
                                lbl_duplicate_activity.Text = GlobalConstants.L_Duplicate_Item;
                                ddl_item.Focus();
                            }
                            else
                            {
                                base_Page.ItemTransactionList.Add(item_Transaction);
                            }
                        }
                    }

                }
                else if (btn_add.Text == "Update")
                {
                    if (this.lstIndex >= 0)
                    {
                        item_Transaction = GetControlsData();

                        if (pageName == GlobalConstants.P_Contractor_Quotation)
                        {
                            if (CheckDuplicateActivity(item_Transaction))
                            {
                                lbl_duplicate_activity.Text = GlobalConstants.L_Duplicate_Activity;
                                ddlServiceDescription.Focus();
                            }
                            else
                            {
                                base_Page.ItemTransactionList = UpdateTempData(item_Transaction);
                                btn_add.Text = "Add";
                                lstIndex = -1;
                            }
                        }
                        else if (pageName == GlobalConstants.P_Supplier_Quotation)
                        {
                            if (CheckDuplicateItem(item_Transaction))
                            {
                                lbl_duplicate_activity.Text = GlobalConstants.L_Duplicate_Item;
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
                }

                //RemoveTaxAssociateWithItem();

                BindItemtransaction();
                //lblLabelGrandTotal.Text = "Grand Total : ";
                lblGrandTotal.Text = String.Concat("Grand Total : ", TotalGrandValue().ToString());
                hdnfGrandTotal.Value = TotalGrandValue().ToString();
                //For Total of All amount as Label Under GridView 
                //TotalAmount();

                //For Clear All data related to Item
                Clear_Transaction_Fields();
                //if (track == false)
                //{
                //    btn_cancel_Click(null, null);
                //}

            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_add.Text = "Add";
            Clear_Transaction_Fields();
            ddlServiceDescription.SelectedValue = "0";
            Clear_Tax_Fields();
            ddl_discount_mode_SelectedIndexChanged(null, null);
        }

        protected void gv_Item_Data_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edit")
            {
                //lst_item_transaction = (List<ItemTransaction>)Session["Data"];
                if (base_Page.ItemTransactionList[index].DeliverySchedule.ActivityDescription != null)
                    ddlServiceDescription.SelectedValue = base_Page.ItemTransactionList[index].DeliverySchedule.ActivityDescriptionId.ToString();

                // ViewState for catch the Desc_id at Update Case...

                //***********************sundeep*****************

                ViewState["ddlServiceDesc"] = ddlServiceDescription.SelectedValue;

                //*********************************************
                //ddl_item_category.SelectedValue = base_Page.ItemTransactionList[index].Item.ModelSpecification.Category.ItemCategoryId.ToString();
                //ddl_item_category_SelectedIndexChanged(null, null);
                ddl_item.SelectedValue = base_Page.ItemTransactionList[index].Item.ItemId.ToString();
                ddl_item_SelectedIndexChanged(null, null);

                lbl_category_level.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.Category.ItemCategoryName;
                hdf_category_level_id.Value = base_Page.ItemTransactionList[index].Item.ModelSpecification.Category.ItemCategoryId.ToString();
                ddl_item_model.SelectedValue = base_Page.ItemTransactionList[index].Item.ModelSpecification.ModelSpecificationId.ToString();

                if (!String.IsNullOrEmpty(base_Page.ItemTransactionList[index].Item.ModelSpecification.Brand.BrandName))
                    lbl_make.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.Brand.ToString();

                txt_Number_of_Unit.Text = base_Page.ItemTransactionList[index].NumberOfUnit.ToString();
                if (base_Page.ItemTransactionList[index].Item.ModelSpecification.UnitMeasurement.Name != String.Empty)
                {
                    lbl_unit_of_measurement.Text = base_Page.ItemTransactionList[index].Item.ModelSpecification.UnitMeasurement.Name;
                    hdf_unit_of_measurement.Value = base_Page.ItemTransactionList[index].Item.ModelSpecification.UnitMeasurement.Id.ToString();
                }
                txt_Per_Unit_Cost.Text = base_Page.ItemTransactionList[index].PerUnitCost.ToString();


                //Tax
                if (base_Page.ItemTransactionList[index].TaxInformation.DiscountMode.Name == Convert.ToString(DiscountType.Value)
                    || base_Page.ItemTransactionList[index].TaxInformation.DiscountMode.Name == Convert.ToString(DiscountType.Percentage))
                {
                    txt_Per_Unit_Discount.Text = base_Page.ItemTransactionList[index].PerUnitDiscount.ToString();
                    ddl_discount_mode.SelectedValue = base_Page.ItemTransactionList[index].TaxInformation.DiscountMode.Id.ToString();
                    hdf_discount_mode.Value = base_Page.ItemTransactionList[index].TaxInformation.DiscountMode.Id.ToString();
                }
                else
                {
                    ddl_discount_mode.SelectedIndex = 0;
                    ddl_discount_mode_SelectedIndexChanged(null, null);
                }

                if (ddl_discount_mode.SelectedValue != "0")
                {
                    txt_Per_Unit_Discount.Visible = true;
                    lblPerUnitDiscount.Visible = true;
                }

                txtExciseDuty.Text = base_Page.ItemTransactionList[index].TaxInformation.ExciseDuty.ToString();

                txt_service_tax.Text = base_Page.ItemTransactionList[index].TaxInformation.ServiceTax.ToString();
                txt_vat.Text = base_Page.ItemTransactionList[index].TaxInformation.VAT.ToString();
                txt_cst_with_c_form.Text = base_Page.ItemTransactionList[index].TaxInformation.CSTWithCForm.ToString();
                txt_cst_without_c_form.Text = base_Page.ItemTransactionList[index].TaxInformation.CSTWithoutCForm.ToString();
                //txt_freight.Text = base_Page.ItemTransactionList[index].TaxInformation.Freight.ToString();
                //txt_packaging.Text = base_Page.ItemTransactionList[index].TaxInformation.Packaging.ToString();
                //ddl_discount_mode.SelectedItem.Text = base_Page.ItemTransactionList[index].TaxInformation.DiscountMode.Name;
                // txt_Per_Unit_Discount.Text = base_Page.ItemTransactionList[index].TaxInformation.TotalDiscount.ToString();

                //End Tax 

                btn_add.Text = "Update";
                this.lstIndex = index;
                if (ddl_item_model.SelectedValue == "0")
                {
                    rbtn_AddItem.SelectedValue = "1";
                    SetItemState();
                }

            }
            else if (e.CommandName == "Delete")
            {
                //lst_item_transaction = (List<ItemTransaction>)Session["Data"];
                base_Page.ItemTransactionList.RemoveAt(index);
                //Session["Data"] = lst_item_transaction;
                if (base_Page.ItemTransactionList.Count == 0)
                {
                    Clear_Transaction_Fields();
                    ddlServiceDescription.SelectedValue = "0";
                    btn_add.Text = "Add";
                    base_Page.ItemTransactionList = null;

                }
                gv_Item_Data.DataSource = base_Page.ItemTransactionList;
                gv_Item_Data.DataBind();
            }

            //For Total of All amount as Label Under GridView 
            //TotalAmount();
        }

        decimal priceTotal = 0;
        protected void gv_Item_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                priceTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = priceTotal.ToString();
                TotalAmount = priceTotal;
            }
        }

        protected void gv_Item_Data_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gv_Item_Data_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_Item_Data_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Item_Data.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void ddl_discount_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_discount_mode.SelectedIndex != 0)
                DiscountId = Convert.ToInt32(ddl_discount_mode.SelectedValue);
            else
                DiscountId = Convert.ToInt16(DiscountType.None);

            if (DiscountId == Convert.ToInt16(DiscountType.Percentage))
            {
                lbl_discount_mode.Text = "%";
                //BindTaxData(DiscountId);
                ddl_discount_mode.SelectedValue = Convert.ToInt16(DiscountType.Percentage).ToString();
                hdf_discount_mode.Value = ddl_discount_mode.SelectedValue;
                txt_Per_Unit_Discount.Visible = true;
                lblPerUnitDiscount.Visible = true;
            }
            else if (DiscountId == Convert.ToInt16(DiscountType.Value))
            {
                lbl_discount_mode.Text = String.Empty;
                imgRupee.Visible = true;
                //BindTaxData(DiscountId);
                ddl_discount_mode.SelectedValue = Convert.ToInt16(DiscountType.Value).ToString();
                hdf_discount_mode.Value = ddl_discount_mode.SelectedValue;
                txt_Per_Unit_Discount.Visible = true;
                lblPerUnitDiscount.Visible = true;
            }
            else
            {
                lbl_discount_mode.Text = String.Empty;
                imgRupee.Visible = false;
                // BindTaxData(Convert.ToInt16(DiscountType.None));
                // BindWithoutTax();
                hdf_discount_mode.Value = ddl_discount_mode.SelectedValue;
                txt_Per_Unit_Discount.Visible = false;
                lblPerUnitDiscount.Visible = false;
            }
        }

        //protected void chbx_tax_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chbx_tax.Checked == true)
        //    {
        //        if (base_Page.ItemTransactionList != null)
        //        {
        //            track = false;
        //            if (txt_total_discount.Text.Trim() != String.Empty)
        //                discount = Convert.ToDecimal(txt_total_discount.Text.Trim());
        //            net_value = Convert.ToDecimal(lbl_Total_Amount.Text);
        //            if (ddl_discount_mode.SelectedValue != Convert.ToString(DiscountType.None) && discount != Decimal.MinValue)
        //            {
        //                if (ddl_discount_mode.SelectedValue == Convert.ToInt32(DiscountType.Fix).ToString())
        //                {
        //                    if (discount > net_value)
        //                    {
        //                        ShowDiscountErrorMsg();
        //                    }
        //                    else
        //                        lbl_calculate_msg.Text = String.Empty;
        //                }
        //                else if (ddl_discount_mode.SelectedValue == Convert.ToInt32(DiscountType.Percentage).ToString())
        //                {
        //                    if ((net_value * discount / 100) > net_value)
        //                    {
        //                        ShowDiscountErrorMsg();
        //                    }
        //                    else
        //                        lbl_calculate_msg.Text = String.Empty;
        //                }
        //            }
        //            if (track == false)
        //            {
        //                Total_Net_Value_With_Taxes();
        //                EnableDisableTax(false);
        //                lbl_total_net_value.Text = (total_net_value + total_net_value_with_tax).ToString();
        //                lbl_calculate_msg.Text = String.Empty;
        //            }
        //        }
        //        else
        //        {
        //            if (IsContractor)
        //                lbl_calculate_msg.Text = GlobalConstants.M_Tax_without_Activity_Desc;
        //            else
        //                lbl_calculate_msg.Text = GlobalConstants.M_Tax_without_Item;

        //            chbx_tax.Checked = false;
        //        }
        //    }
        //    else
        //    {
        //        if (base_Page.ItemTransactionList != null)
        //        {
        //            BindWithoutTax();
        //        }
        //    }
        //}

        //protected void btn_calculate_Click(object sender, EventArgs e)
        //{
        //    if (base_Page.ItemTransactionList != null)
        //    {
        //        if (Total_Net_Value_With_Taxes() > 0)
        //        {
        //            lbl_total_net_value.Text = (total_net_value + total_net_value_with_tax).ToString();
        //            lbl_calculate_msg.Text = String.Empty;
        //        }
        //    }
        //    else
        //    {
        //        lbl_calculate_msg.Text = "Please Add the Activity Description to add taxes";
        //        lbl_calculate_msg.Focus();
        //    }
        //}

        protected void btn_save_draft_Click(object sender, CommandEventArgs e)
        {
            if (!IsContractor && txt_Closing_Date.Text != String.Empty && (DateTime.ParseExact(txt_order_date.Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txt_Closing_Date.Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture)))
            {
                base_Page.Alert(GlobalConstants.M_Date_Difference, btn_save_draft);
                txt_Closing_Date.Focus();
            }
            else if (base_Page.ItemTransactionList == null)
            {
                if (base_Page.ItemTransactionList == null)
                    if (IsContractor)
                        error_msg += "<ul><li>Service Description not created.</li></ul>";
                    else
                        error_msg += "<ul><li>Work Details not created.</li></ul>";

                if (error_msg != String.Empty)
                {
                    ltrl_error_msg.Text = error_msg;
                    lbtn_error.Focus();
                }
            }

            else
            {
                SaveAllData(e.CommandName);
                lblGrandTotal.Text = String.Empty;
                hdnfGrandTotal.Value = String.Empty;
                txt_freight.Text = String.Empty;
                txt_packaging.Text = String.Empty;
                lbtn_error.Focus();
            }

            if (error_msg != String.Empty)
            {
                ltrl_error_msg.Text = error_msg;
                lbtn_error.Focus();
            }
        }

        protected void btn_submit_draft_Click(object sender, CommandEventArgs e)
        {
            //On Change in ItemTarnsaction after createing delivery Schedule to Crross Check[Manveer,10.05.2013]
            //CheckDeliverySchedule();
            //On True.......Show Message .....Delivery Schedule is not Created for All the Items.

            if (!IsContractor && txt_Closing_Date.Text != String.Empty && (DateTime.ParseExact(txt_order_date.Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txt_Closing_Date.Text, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture)))
            {
                base_Page.Alert(GlobalConstants.M_Date_Difference, btn_save_draft);
                txt_Closing_Date.Focus();
            }
            else if (base_Page.ItemTransactionList == null || base_Page.DeliveryScheduleList == null || base_Page.PaymentTermsList == null || base_Page.TermConditionList == null)
            {
                if (base_Page.ItemTransactionList == null)
                    if (IsContractor)
                        error_msg += "<ul><li>Work Details not created.</li></ul>";
                    else
                        error_msg += "<ul><li>Activity Description not created.</li></ul>";
                if (base_Page.DeliveryScheduleList == null)
                    error_msg += "<ul><li>Delivery Schedule not created.</li></ul>";
                if (base_Page.PaymentTermsList == null)
                    error_msg += "<ul><li>Payment Terms not created.</li></ul>";
                if (base_Page.TermConditionList == null)
                    error_msg += "<ul><li>Terms & Conditions not created.</li></ul>";

                if (error_msg != String.Empty)
                {
                    ltrl_error_msg.Text = error_msg;
                    lbtn_error.Focus();
                }
            }

            else if (!CheckPayentTerm())
            {
                error_msg += "<ul><li>Payment Terms not upto 100%.</li></ul>";
            }

            else
            {
                SaveAllData(e.CommandName);
                lblGrandTotal.Text = String.Empty;
                hdnfGrandTotal.Value = String.Empty;
                txt_freight.Text = String.Empty;
                txt_packaging.Text = String.Empty;
                lbtn_error.Focus();
            }

            if (error_msg != String.Empty)
            {
                ltrl_error_msg.Text = error_msg;
                lbtn_error.Focus();
            }
        }



        protected void btn_cancel_draft_Click(object sender, EventArgs e)
        {
            if (IsContractor)
                Response.Redirect("ContractorQuotation.aspx");
            else
                Response.Redirect("SupplierQuotation.aspx");
        }

        protected void btnAddTotal_Click(object sender, EventArgs e)
        {
            lblGrandTotal.Text = String.Concat("Grand Total : ", TotalGrandValue().ToString());
            hdnfGrandTotal.Value = TotalGrandValue().ToString();
        }

        #endregion

        #region Private Methods

        private void LoadDefaultData()
        {
            base_Page.Page_Name = pageName;
            txt_order_date.Text = DateTime.Now.Date.ToString("dd'/'MM'/'yyyy");
            //cal_ext_order_date.EndDate = DateTime.Now;
            //cal_ext_closing_date.StartDate = DateTime.Now;
            txt_Closing_Date.Text = DateTime.Now.Date.ToString("dd'/'MM'/'yyyy");

            LabelAssociation(pageName);
            BindContractor();
            BindSupplier();
            BindContractNumber();
            BindServiceDescription();
            BindItem(null);
            //.....Has To Be Removed As it is not Picked from Master of Tax[Manveer,10.04.2013]
            HoldTaxData();
            //TotalAmount();
            //------sundeep-------------
            if (IsContractor)
            {
                rbtn_AddItem.SelectedValue = "1";
                ClearItemFields();
                EnableDisableItem(false);
                SetRequiredFieldValidatorsForItem("cancel");
            }
            else
            {
                rbtn_AddItem.SelectedValue = "0";
            }


            //--------------------------------
            // rbtn_AddItem.SelectedValue = "0";
            SetItemState();
            SetRegularExpressions();
            Clear_Session();
            // EnableDisableItem(true);

        }

        private void SetRegularExpressions()
        {
            //rev_delivery_desc.ValidationExpression = ValidationExpression.C_ADDRESS;
            //rev_delivery_desc.ErrorMessage = "<,>,\",^ characters are not allowed with Max Length 2000 in Delivery Description";

            rev_delivery_desc.ValidationExpression = ValidationExpression.C_DESCRIPTION;
            rev_delivery_desc.ErrorMessage = "More than 250 Characters are not Allowed in Delivery Description";

            //rev_activity_desc.ValidationExpression = ValidationExpression.C_DESCRIPTION;
            //rev_activity_desc.ErrorMessage = "More than 250 Characters are not Allowed in Activity Description";

            rev_number_of_unit.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_number_of_unit.ErrorMessage = "In Number of Unit only Numeric values are allowed upto 2 decimal digits";
            rev_per_unit_cost.ValidationExpression = ValidationExpression.C_DECIMAL;

            rev_per_unit_cost.ErrorMessage = "In Cost only Numeric values are allowed upto 2 decimal digits";
            rev_per_unit_discount.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_per_unit_discount.ErrorMessage = "Only Numeric values less than 100 are allowed in Per Unit Discount";

            revExciseDuty.ValidationExpression = ValidationExpression.C_DECIMAL;
            revExciseDuty.ErrorMessage = "In Excise Duty only Numeric values are allowed upto 2 decimal digits";

            rev_service_tax.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_service_tax.ErrorMessage = "In Service Tax only Numeric values are allowed upto 2 decimal digits";
            rev_vat.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_vat.ErrorMessage = "In VAT only Numeric values are allowed upto 2 decimal digits";
            rev_cst_with_c_form.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_cst_with_c_form.ErrorMessage = "In CST with C Form only Numeric values are allowed upto 2 decimal digits";
            rev_cst_without_c_form.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_cst_without_c_form.ErrorMessage = "In CST without C Form only Numeric values are allowed upto 2 decimal digits";
            rev_freight.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_freight.ErrorMessage = "In Freight only Numeric values are allowed upto 2 decimal digits";
            rev_packaging.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_packaging.ErrorMessage = "In Packaging only Numeric values are allowed upto 2 decimal digits";
            //rev_Discount.ValidationExpression = ValidationExpression.C_DECIMAL;
            //rev_Discount.ErrorMessage = "In Discount only Numeric values are allowed upto 2 decimal digits";
        }

        private void SetRequiredFieldValidatorsForItem(String validation_group)
        {

            rfv_item.ValidationGroup = validation_group;
            rfv_item_model.ValidationGroup = validation_group;
            // rfv_number_of_unit.ValidationGroup = validation_group;
            //rfv_per_unit_cost.ValidationGroup = validation_group;
        }

        private void LabelAssociation(String pageName)
        {
            if (pageName == GlobalConstants.P_Contractor_Quotation)
            {
                lbl_head.Text = "Generate Contractor Work Order";
                lbl_quotation_number.Text = "Contractor Purchase order Number";

                rw_supplier.Visible = false;

                lbl_order_date.Text = "Contractor Purchase order Date";
                date_and_desc.Visible = false;

                //After Adding WorkDetails Functionality
                //pnl_activity.Visible = false;
                //For Excise Duty
                excise.Visible = false;
                //End
                pnl_total.Visible = false;
            }
            else if (pageName == GlobalConstants.P_Supplier_Quotation)
            {
                lbl_head.Text = "Generate Supplier Purchase Order";
                lbl_quotation_number.Text = "Supplier Purchase order Number";

                rw_contractor.Visible = false;
                rw_contract.Visible = false;

                lbl_order_date.Text = "Supplier Purchase order Date";

                rw_activity_desc.Visible = false;
                rw_addItem.Visible = false;
            }
        }

        private void BindContractor()
        {
            contractor_BL = new ContractorBL();
            lst_contractor = new List<Contractor>();
            lst_contractor = contractor_BL.ReadContractor(null);
            base_Page.BindDropDown(ddl_contractor_name, "Name", "ContractorId", lst_contractor);
        }

        private void BindSupplier()
        {
            supplier_BL = new SupplierBL();
            lst_supplier = new List<Supplier>();
            lst_supplier = supplier_BL.ReadSupplier(null);
            base_Page.BindDropDown(ddl_supplier, "Name", "SupplierId", lst_supplier);
        }

        private void BindRadioButton()
        {
            MetaDataBL metadataBL = new MetaDataBL();
            rbtnTaxType.DataSource = metadataBL.ReadMetaDataTaxType();
            rbtnTaxType.DataValueField = "Id";
            rbtnTaxType.DataTextField = "Name";
            rbtnTaxType.DataBind();
        }

        private void BindContractNumber()
        {
            companyWorkOrderBL = new CompanyWorkOrderBL();
            lst_companyWorkOrder = new List<CompanyWorkOrderDOM>();
            lst_companyWorkOrder = companyWorkOrderBL.ReadCompOrder(null);
            var lstNew = lst_companyWorkOrder.Where(p => p.StatusType.Id == Convert.ToInt16(StatusType.Generated));
            //foreach (CompanyWorkOrderDOM item in lst_companyWorkOrder)
            //{
            //}

            base_Page.BindDropDown(ddl_contrcat_number, "CompanyWorkOrderNumber", "CompanyWorkOrderId", lstNew);
        }

        private void BindWorkOrder(Int32 id)
        {
            lst_work_order = new List<WorkOrderMappingDOM>();
            companyWorkOrderBL = new CompanyWorkOrderBL();

            lst_work_order = companyWorkOrderBL.ReadCompanyWorkOrderMapping(id);

            if (lst_work_order.Count > 0)
                base_Page.BindDropDown(ddl_work_order_number, "WorkOrderNumber", "CompanyWorkOrderMappingId", lst_work_order);
            else
                base_Page.BindEmptyDropDown(ddl_work_order_number);
        }

        private void BindServiceDescription()
        {
            lstServiceDetail = new List<ServiceDetailDOM>();
            companyWorkOrderBL = new CompanyWorkOrderBL();
            lstServiceDetail = companyWorkOrderBL.ReadCompanyWorkOrderServiceDetail(null);
            if (lstServiceDetail.Count > 0)
            {
                base_Page.BindDropDown(ddlServiceDescription, "ServiceDescription", "ServiceDetailId", lstServiceDetail);
            }
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

        private void BindData()
        {
            if (base_Page.ItemTransactionList != null)
            {
                gv_Item_Data.DataSource = base_Page.ItemTransactionList;
                gv_Item_Data.DataBind();
            }
        }

        private void BindMake_UnitMeasurement(Item item)
        {
            modelSpecification = new ModelSpecification();
            modelSpecification.UnitMeasurement = new MetaData();

            modelSpecification = item_Model_BL.ReadMakeandUnitofMeasurement(item);

            lbl_make.Text = modelSpecification.Brand.BrandName;

            lbl_unit_of_measurement.Text = modelSpecification.UnitMeasurement.Name;
            hdf_unit_of_measurement.Value = modelSpecification.UnitMeasurement.Id.ToString();

            hdf_category_level_id.Value = modelSpecification.Category.ItemCategoryId.ToString();
            lbl_category_level.Text = modelSpecification.Category.ItemCategoryName;
        }

        private void BindItemtransaction()
        {
            gv_Item_Data.DataSource = base_Page.ItemTransactionList;
            gv_Item_Data.DataBind();
            if (pageName == GlobalConstants.P_Supplier_Quotation)
            {
                gv_Item_Data.Columns[0].Visible = false;
            }
            else
            {
                gv_Item_Data.Columns[10].Visible = false;
            }
        }

        private ItemTransaction GetControlsData()
         {
            item_Transaction = new ItemTransaction();
            item_Transaction.Item = new Item();
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
                item_Transaction.Item.ModelSpecification.Brand.BrandName = lbl_make.Text.Trim();

                item_Transaction.Item.ModelSpecification.UnitMeasurement.Id = Convert.ToInt32(hdf_unit_of_measurement.Value);
                item_Transaction.Item.ModelSpecification.UnitMeasurement.Name = lbl_unit_of_measurement.Text;

                //For Delivery Schedule in case of Supplier
                item_Transaction.DeliverySchedule.ItemDescription = item_Transaction.Item.FinalActivityDescription;
                item_Transaction.DeliverySchedule.SpecificationId = item_Transaction.Item.ModelSpecification.ModelSpecificationId;
                item_Transaction.DeliverySchedule.SpecificationUnit = item_Transaction.Item.ModelSpecification.UnitMeasurement.Name;
                item_Transaction.DeliverySchedule.ActualNumberOfUnit = item_Transaction.NumberOfUnit;
            }

            //if (!String.IsNullOrEmpty(txt_activity_description.Text.Trim()))
            //    item_Transaction.DeliverySchedule.ActivityDescription = txt_activity_description.Text.Trim();
            //else
            //    item_Transaction.DeliverySchedule.ActivityDescription = item_Transaction.Item.FinalActivityDescription;

            if (Convert.ToInt32(ddlServiceDescription.SelectedValue) == 0)
            {

                item_Transaction.DeliverySchedule.ActivityDescriptionId = 0;
                item_Transaction.DeliverySchedule.ActivityDescription = String.Empty;
            }
            else
            {
                item_Transaction.DeliverySchedule.ActivityDescriptionId = Convert.ToInt32(ddlServiceDescription.SelectedValue);
                item_Transaction.DeliverySchedule.ActivityDescription = ddlServiceDescription.SelectedItem.ToString();
            }

            if (!string.IsNullOrEmpty(txt_Per_Unit_Cost.Text.Trim()))
                item_Transaction.PerUnitCost = Convert.ToDecimal(txt_Per_Unit_Cost.Text.Trim());

            if (!string.IsNullOrEmpty(txt_Per_Unit_Discount.Text.Trim()))
                item_Transaction.PerUnitDiscount = Convert.ToDecimal(txt_Per_Unit_Discount.Text.Trim());

            if (!string.IsNullOrEmpty(hdf_discount_mode.Value))
            {
                item_Transaction.TaxInformation.DiscountMode.Id = Convert.ToInt32(hdf_discount_mode.Value);
                if (item_Transaction.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Percentage))
                    item_Transaction.TaxInformation.DiscountMode.Name = Convert.ToString(DiscountType.Percentage);
                else if (item_Transaction.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Value))
                    item_Transaction.TaxInformation.DiscountMode.Name = Convert.ToString(DiscountType.Value);
            }

            if (!String.IsNullOrEmpty(txtExciseDuty.Text.Trim()))
                item_Transaction.TaxInformation.ExciseDuty = Convert.ToDecimal(txtExciseDuty.Text.Trim());

            if (!string.IsNullOrEmpty(txt_service_tax.Text.Trim()))
                item_Transaction.TaxInformation.ServiceTax = Convert.ToDecimal(txt_service_tax.Text.Trim());

            if (!string.IsNullOrEmpty(txt_vat.Text.Trim()))
                item_Transaction.TaxInformation.VAT = Convert.ToDecimal(txt_vat.Text.Trim());

            if (!string.IsNullOrEmpty(txt_cst_with_c_form.Text.Trim()))
                item_Transaction.TaxInformation.CSTWithCForm = Convert.ToDecimal(txt_cst_with_c_form.Text.Trim());

            if (!string.IsNullOrEmpty(txt_cst_without_c_form.Text.Trim()))
                item_Transaction.TaxInformation.CSTWithoutCForm = Convert.ToDecimal(txt_cst_without_c_form.Text.Trim());

            //if (!string.IsNullOrEmpty(txt_freight.Text.Trim()))
            //    item_Transaction.TaxInformation.Freight = Convert.ToDecimal(txt_freight.Text.Trim());

            //if (!string.IsNullOrEmpty(txt_packaging.Text.Trim()))
            //    item_Transaction.TaxInformation.Packaging = Convert.ToDecimal(txt_packaging.Text.Trim());

            total_net_value = (item_Transaction.NumberOfUnit * item_Transaction.PerUnitCost);
            item_Transaction.TotalAmount = Math.Round(Total_Net_Value_With_Taxes(total_net_value), 2);
            item_Transaction.Discount_Rates = Math.Round(Total_Net_DiscountPrice(), 2);

            return item_Transaction;
        }

        private Boolean CheckDuplicateActivity(ItemTransaction item_Transaction)
        {
            id = 0;
            foreach (ItemTransaction item in base_Page.ItemTransactionList)
            {
                id = id + 1;
                if (item_Transaction.DeliverySchedule.ActivityDescriptionId == item.DeliverySchedule.ActivityDescriptionId
                    && item_Transaction.Item.ItemId == item.Item.ItemId
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

        private Boolean CheckDuplicateItem(ItemTransaction item_Transaction)
        {
            id = 0;
            foreach (ItemTransaction item in base_Page.ItemTransactionList)
            {
                id = id + 1;
                if (item_Transaction.Item.ItemId == item.Item.ItemId && item_Transaction.Item.ModelSpecification.ModelSpecificationId == item.Item.ModelSpecification.ModelSpecificationId)
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

        private QuotationDOM GetFormData(String action)
        {
            quotation = new QuotationDOM();
            quotation.StatusType = new MetaData();

            if (hdf_quotation_id.Value != String.Empty)
            {
                quotation.ContractorQuotationId = Convert.ToInt32(hdf_quotation_id.Value);
                quotation.SupplierQuotationId = Convert.ToInt32(hdf_quotation_id.Value);
            }
            if (IsContractor)
            {
                quotation.ContractorId = Convert.ToInt32(ddl_contractor_name.SelectedValue);
                quotation.ContractId = Convert.ToInt32(ddl_contrcat_number.SelectedValue);
                quotation.WorkOrderId = Convert.ToInt32(ddl_work_order_number.SelectedValue);


            }
            else
                quotation.SupplierId = Convert.ToInt32(ddl_supplier.SelectedValue);

            quotation.UploadDocumentId = base_Page.DocumentStackId;

            if (!string.IsNullOrEmpty(txt_order_date.Text))
                quotation.OrderDate = DateTime.ParseExact(txt_order_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            quotation.DeliveryDescription = txt_Delivery_Description.Text.Trim();
            quotation.subjectdescription = txt_Subject_Description.Text.Trim();

            if (!string.IsNullOrEmpty(txt_Closing_Date.Text))
                quotation.ClosingDate = DateTime.ParseExact(txt_Closing_Date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //quotation.TotalNetValue = TotalAmount;
            if (IsContractor)
            {
                //id = base_Page.ItemTransactionList.Count - 1;
                quotation.TotalNetValue = TotalAmount;
                quotation.DiscountPrice = DiscountRate;

            }
            else
                quotation.TotalNetValue = Convert.ToDecimal(hdnfGrandTotal.Value);//Convert.ToDecimal(lblGrandTotal.Text); //TotalGrandValue();
            quotation.DiscountPrice = DiscountRate;

            if (action == btn_submit_draft.CommandName)
                quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            else if (action == btn_save_draft.CommandName)
                quotation.StatusType.Id = Convert.ToInt32(StatusType.InComplete);
            //--------------sundeep-------------
            quotation.CreatedBy = base_Page.LoggedInUser.UserLoginId;
            quotation.TaxType = rbtnTaxType.SelectedValue;
            quotation.DiscountPrice = DiscountRate;

            if (String.IsNullOrEmpty(txt_packaging.Text.Trim()))
            {
                quotation.Packaging = 0;
            }
            else
            {
                quotation.Packaging = Convert.ToDecimal(txt_packaging.Text.Trim());
            }
            if (String.IsNullOrEmpty(txt_freight.Text.Trim()))
            {
                quotation.Freight = 0;
            }
            else
            {
                quotation.Freight = Convert.ToDecimal(txt_freight.Text.Trim());
            }
            //}

            return quotation;
        }

        private void CreateDocumentMapping()
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = base_Page.DocumentsList;

            if (lst_document != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                document_BL.ResetDocumentMapping(Convert.ToInt32(base_Page.DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document doc in lst_document)
                {
                    document = new Document();
                    document.DocumentId = Convert.ToInt32(base_Page.DocumentStackId);
                    document.Original_Name = doc.Original_Name;
                    document.Replaced_Name = doc.Replaced_Name;
                    document.Path = doc.Path;
                    //base_Page.DocumentSerial is the last updated document
                    document.LastIndex = base_Page.DocumentSerial;
                    document.CreatedBy = base_Page.LoggedInUser.UserLoginId;
                    document.Id = doc.Id;
                    document_BL.CreateDocumentMapping(document);
                }
            }
        }

        private List<DeliveryScheduleDOM> CreateQuotationMapping(Int32 quotationId)
        {
            quotation_BL = new QuotationBL();
            lst_delivery_schedule = new List<DeliveryScheduleDOM>();


            if (IsContractor)
            {
                //To Reset Quotation mapping for Contractor
                quotation_BL.ResetQuotationMapping(quotationId, Convert.ToInt32(QuotationType.Contractor));

                //To Insert/Update Quotation mapping for Contractor
                foreach (ItemTransaction item in base_Page.ItemTransactionList)
                {
                    deliverySchedule = new DeliveryScheduleDOM();
                    item.CreatedBy = base_Page.LoggedInUser.UserLoginId;

                    deliverySchedule.ActivityDescriptionId = quotation_BL.CreatePurchaseOrderMapping(item, quotationId);

                    deliverySchedule.ItemDescriptionId = item.DeliverySchedule.ActivityDescriptionId;
                    deliverySchedule.ItemId = item.Item.ItemId;
                    deliverySchedule.SpecificationId = item.DeliverySchedule.SpecificationId;

                    lst_delivery_schedule.Add(deliverySchedule);
                }
            }
            else
            {
                //To Reset Quotation mapping for Supplier
                quotation_BL.ResetQuotationMapping(quotationId, Convert.ToInt32(QuotationType.Supplier));

                //To Insert/Update Quotation mapping for Supplier
                foreach (ItemTransaction item in base_Page.ItemTransactionList)
                {
                    deliverySchedule = new DeliveryScheduleDOM();
                    item.CreatedBy = base_Page.LoggedInUser.UserLoginId;
                    deliverySchedule.ActivityDescriptionId = quotation_BL.CreateSupplierPurchaseOrderMapping(item, quotationId);

                    deliverySchedule.ItemId = item.Item.ItemId;
                    deliverySchedule.SpecificationId = item.Item.ModelSpecification.ModelSpecificationId;

                    lst_delivery_schedule.Add(deliverySchedule);
                }
            }
            return lst_delivery_schedule;
        }

        private void CreateDeliverySchedule(Int32 quotationId, List<DeliveryScheduleDOM> lst_delivery_schedule)
        {
            delivery_schedule_BL = new DeliveryScheduleBL();
            //Reset Delivery Schedule
            if (IsContractor)
                delivery_schedule_BL.ResetDeliverySchedule(quotationId, Convert.ToInt32(QuotationType.Contractor));
            else
                delivery_schedule_BL.ResetDeliverySchedule(quotationId, Convert.ToInt32(QuotationType.Supplier));

            //To Insert/Update Delivery Schedule

            if (base_Page.DeliveryScheduleList != null)
                for (int i = 0; i < base_Page.DeliveryScheduleList.Count; i++)
                {
                    foreach (DeliveryScheduleDOM item_main in lst_delivery_schedule)
                    {
                        if (IsContractor)
                        {
                            if (item_main.ItemDescriptionId == base_Page.DeliveryScheduleList[i].ItemDescriptionId
                                && item_main.ItemId == base_Page.DeliveryScheduleList[i].ItemId
                                && item_main.SpecificationId == base_Page.DeliveryScheduleList[i].SpecificationId
                                )
                                InsertDeliverySchedule(i, item_main.ActivityDescriptionId, quotationId, Convert.ToInt32(QuotationType.Contractor));
                        }
                        else
                        {
                            if (item_main.ItemId == base_Page.DeliveryScheduleList[i].ItemId
                                && item_main.SpecificationId == base_Page.DeliveryScheduleList[i].SpecificationId
                                )
                                InsertDeliverySchedule(i, item_main.ActivityDescriptionId, quotationId, Convert.ToInt32(QuotationType.Supplier));
                        }
                    }
                }
        }

        private void InsertDeliverySchedule(Int32 index, Int32 activityId, Int32 quotationId, Int32 QuotationType)
        {
            delivery_schedule_BL = new DeliveryScheduleBL();
            deliverySchedule = new DeliveryScheduleDOM();
            deliverySchedule.QuotationType = new MetaData();

            deliverySchedule.Id = base_Page.DeliveryScheduleList[index].Id;

            deliverySchedule.QuotationType.Id = QuotationType;

            deliverySchedule.ActivityDescriptionId = activityId;

            //if (IsContractor)
            deliverySchedule.ItemDescriptionId = base_Page.DeliveryScheduleList[index].ItemDescriptionId;
            //else
            //    deliverySchedule.ItemDescriptionId = base_Page.DeliveryScheduleList[index].SpecificationId;

            deliverySchedule.ItemQuantity = base_Page.DeliveryScheduleList[index].ItemQuantity;

            deliverySchedule.DeliveryDate = base_Page.DeliveryScheduleList[index].DeliveryDate;

            deliverySchedule.CreatedBy = base_Page.LoggedInUser.UserLoginId;

            delivery_schedule_BL.CreateDeliverySchedule(deliverySchedule, quotationId);
        }

        private void CreatePaymentTerms(Int32 quotationId)
        {
            if (base_Page.PaymentTermsList != null)
            {
                payment_Term_BL = new PaymentTermBL();

                //To Reset Payment Term
                if (IsContractor)
                    payment_Term_BL.ResetPaymentTerm(quotationId, Convert.ToInt32(QuotationType.Contractor));
                else
                    payment_Term_BL.ResetPaymentTerm(quotationId, Convert.ToInt32(QuotationType.Supplier));

                //To Insert/Update payment Term
                foreach (PaymentTerm item in base_Page.PaymentTermsList)
                {
                    paymentTerm = new PaymentTerm();
                    paymentTerm.QuotationType = new MetaData();
                    paymentTerm.PaymentType = new MetaData();

                    if (IsContractor)
                        paymentTerm.QuotationType.Id = Convert.ToInt32(QuotationType.Contractor);
                    else
                        paymentTerm.QuotationType.Id = Convert.ToInt32(QuotationType.Supplier);

                    paymentTerm.PaymentType.Id = item.PaymentType.Id;
                    paymentTerm.NumberOfDays = item.NumberOfDays;
                    paymentTerm.PercentageValue = item.PercentageValue;

                    paymentTerm.PaymentDescription = item.PaymentDescription;
                    paymentTerm.CreatedBy = base_Page.LoggedInUser.UserLoginId;

                    payment_Term_BL.CreatePaymentTerm(paymentTerm, quotationId);
                }
            }
        }

        private void CreateTermsCondition(Int32 quotationId)
        {
            if (base_Page.TermConditionList != null)
            {
                term_condition_BL = new TermAndConditionBL();

                //To Reset Term and Conditions
                if (IsContractor)
                    term_condition_BL.ResetTermAndConditions(quotationId, Convert.ToInt32(QuotationType.Contractor));
                else
                    term_condition_BL.ResetTermAndConditions(quotationId, Convert.ToInt32(QuotationType.Supplier));

                //To Insert/Update Term and Conditions
                foreach (TermAndCondition item in base_Page.TermConditionList)
                {
                    termAndCondition = new TermAndCondition();
                    termAndCondition.QuotationType = new MetaData();

                    termAndCondition.Id = item.Id;

                    if (IsContractor)
                        termAndCondition.QuotationType.Id = Convert.ToInt32(QuotationType.Contractor);
                    else
                        termAndCondition.QuotationType.Id = Convert.ToInt32(QuotationType.Supplier);

                    termAndCondition.TermsId = item.TermsId;
                    termAndCondition.Name = item.Name;
                    termAndCondition.CreatedBy = base_Page.LoggedInUser.UserLoginId;
                    term_condition_BL.CreateTermAndCondition(termAndCondition, quotationId);
                }
            }
        }

        private List<ItemTransaction> UpdateTempData(ItemTransaction item_Transaction)
        {
            base_Page.ItemTransactionList[this.lstIndex].DeliverySchedule.ActivityDescription = item_Transaction.DeliverySchedule.ActivityDescription;
            base_Page.ItemTransactionList[this.lstIndex].DeliverySchedule.ActivityDescriptionId = item_Transaction.DeliverySchedule.ActivityDescriptionId;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.Category.ItemCategoryId = item_Transaction.Item.ModelSpecification.Category.ItemCategoryId;
            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.Category.ItemCategoryName = item_Transaction.Item.ModelSpecification.Category.ItemCategoryName;

            base_Page.ItemTransactionList[this.lstIndex].Item.ItemId = item_Transaction.Item.ItemId;
            base_Page.ItemTransactionList[this.lstIndex].Item.ItemName = item_Transaction.Item.ItemName;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.ModelSpecificationId = item_Transaction.Item.ModelSpecification.ModelSpecificationId;
            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.ModelSpecificationName = item_Transaction.Item.ModelSpecification.ModelSpecificationName;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.Brand = item_Transaction.Item.ModelSpecification.Brand;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.UnitMeasurement.Id = item_Transaction.Item.ModelSpecification.UnitMeasurement.Id;

            base_Page.ItemTransactionList[this.lstIndex].Item.ModelSpecification.UnitMeasurement.Name = item_Transaction.Item.ModelSpecification.UnitMeasurement.Name;

            //For Delivery Schedule in case of Supplier
            base_Page.ItemTransactionList[this.lstIndex].DeliverySchedule.ItemDescription = item_Transaction.DeliverySchedule.ItemDescription;
            base_Page.ItemTransactionList[this.lstIndex].DeliverySchedule.SpecificationId = item_Transaction.DeliverySchedule.SpecificationId;
            base_Page.ItemTransactionList[this.lstIndex].DeliverySchedule.SpecificationUnit = item_Transaction.DeliverySchedule.SpecificationUnit;
            base_Page.ItemTransactionList[this.lstIndex].DeliverySchedule.ActualNumberOfUnit = item_Transaction.DeliverySchedule.ActualNumberOfUnit;
            //End

            base_Page.ItemTransactionList[this.lstIndex].NumberOfUnit = item_Transaction.NumberOfUnit;
            base_Page.ItemTransactionList[this.lstIndex].PerUnitCost = item_Transaction.PerUnitCost;

            if (txt_Per_Unit_Discount.Text == String.Empty)
            {
                base_Page.ItemTransactionList[this.lstIndex].PerUnitDiscount = 0;
            }
            else
            {
                base_Page.ItemTransactionList[this.lstIndex].PerUnitDiscount = item_Transaction.PerUnitDiscount;
            }

            //Tax Info
            base_Page.ItemTransactionList[this.lstIndex].TaxInformation.DiscountMode.Id = item_Transaction.TaxInformation.DiscountMode.Id;
            base_Page.ItemTransactionList[this.lstIndex].TaxInformation.DiscountMode.Name = item_Transaction.TaxInformation.DiscountMode.Name;

            base_Page.ItemTransactionList[this.lstIndex].TaxInformation.ServiceTax = item_Transaction.TaxInformation.ServiceTax;
            base_Page.ItemTransactionList[this.lstIndex].TaxInformation.VAT = item_Transaction.TaxInformation.VAT;
            base_Page.ItemTransactionList[this.lstIndex].TaxInformation.CSTWithCForm = item_Transaction.TaxInformation.CSTWithCForm;
            base_Page.ItemTransactionList[this.lstIndex].TaxInformation.CSTWithoutCForm = item_Transaction.TaxInformation.CSTWithoutCForm;
            //base_Page.ItemTransactionList[this.lstIndex].TaxInformation.Freight = item_Transaction.TaxInformation.Freight;
            //base_Page.ItemTransactionList[this.lstIndex].TaxInformation.Packaging = item_Transaction.TaxInformation.Packaging;
            base_Page.ItemTransactionList[this.lstIndex].TaxInformation.ExciseDuty = item_Transaction.TaxInformation.ExciseDuty;
            //End

            base_Page.ItemTransactionList[this.lstIndex].TotalAmount = item_Transaction.TotalAmount;
            base_Page.ItemTransactionList[this.lstIndex].Discount_Rates = item_Transaction.Discount_Rates;

            // This Code is for Update all Service description at once
            //***********************sundeep****************************



            int Desc_id = Convert.ToInt32(ViewState["ddlServiceDesc"]);
            for (int i = 0; i < base_Page.ItemTransactionList.Count; i++)
            {
                if (base_Page.ItemTransactionList[i].DeliverySchedule.ActivityDescriptionId == Desc_id)
                {
                    base_Page.ItemTransactionList[i].DeliverySchedule.ActivityDescription = item_Transaction.DeliverySchedule.ActivityDescription;
                    base_Page.ItemTransactionList[i].DeliverySchedule.ActivityDescriptionId = item_Transaction.DeliverySchedule.ActivityDescriptionId;
                }

            }
            //***********************sundeep****************************

            ///gv_Item_Data_RowCommand open there comment 



            return base_Page.ItemTransactionList;
        }

        private void HoldTaxData()
        {
            base_Page.BindDropDown(ddl_discount_mode, "Name", "Id", taxBL.ReadDiscountMode(null));
            DiscountId = Convert.ToInt32(DiscountType.None);
            ddl_discount_mode_SelectedIndexChanged(null, null);
        }

        private void BindTaxData(int? discount_id)
        {
            List<Tax> lstTax = new List<Tax>();
            lstTax = taxBL.ReadTaxByDiscountModeId(discount_id);
            if (lstTax.Count != 0)
            {
                txtExciseDuty.Text = lstTax[0].ExciseDuty.ToString();
                txt_service_tax.Text = lstTax[0].ServiceTax.ToString();
                txt_vat.Text = lstTax[0].VAT.ToString();
                txt_cst_with_c_form.Text = lstTax[0].CSTWithCForm.ToString();
                txt_cst_without_c_form.Text = lstTax[0].CSTWithoutCForm.ToString();
                //txt_freight.Text = lstTax[0].Freight.ToString();
                //txt_packaging.Text = lstTax[0].Packaging.ToString();
                txt_Per_Unit_Discount.Text = lstTax[0].TotalDiscount.ToString();
                DiscountId = Convert.ToInt16(DiscountType.None);
                txt_Per_Unit_Discount.Visible = true;
                lblPerUnitDiscount.Visible = true;
            }
            else
            {
                //Clear_Tax_Fields();
                txt_Per_Unit_Discount.Visible = false;
                lblPerUnitDiscount.Visible = false;
            }
        }

        // Discount Rate
        private Decimal Total_Net_DiscountPrice()
        {
            // DiscountRate = TotalDiscountPrice;


            if (ddl_discount_mode.SelectedValue != Convert.ToString(DiscountType.None) && txt_Per_Unit_Discount.Text != String.Empty)
            {
                if (ddl_discount_mode.SelectedValue == Convert.ToInt32(DiscountType.Value).ToString())
                    DiscountRate = Math.Round((item_Transaction.PerUnitCost - item_Transaction.PerUnitDiscount), 2);

                else if (ddl_discount_mode.SelectedValue == Convert.ToInt32(DiscountType.Percentage).ToString())
                    DiscountRate = Math.Round((item_Transaction.PerUnitCost - (item_Transaction.PerUnitCost * item_Transaction.PerUnitDiscount / 100)), 2);
            }



            return DiscountRate;
        }

        private Decimal Total_Net_Value_With_Taxes(Decimal total_net_value)
        {
            total = total_net_value;


            if (ddl_discount_mode.SelectedValue != Convert.ToString(DiscountType.None) && txt_Per_Unit_Discount.Text != String.Empty)
            {
                if (ddl_discount_mode.SelectedValue == Convert.ToInt32(DiscountType.Value).ToString())
                    total = Math.Round((total_net_value - (item_Transaction.NumberOfUnit * item_Transaction.PerUnitDiscount)), 2);

                else if (ddl_discount_mode.SelectedValue == Convert.ToInt32(DiscountType.Percentage).ToString())
                    total = Math.Round((total_net_value - (total_net_value * item_Transaction.PerUnitDiscount / 100)), 2);
            }





            if (!String.IsNullOrEmpty(txtExciseDuty.Text.Trim()))
            {
                total = Math.Round((total + (total * Convert.ToDecimal(txtExciseDuty.Text.Trim()) / 100)), 2);
            }

            if (txt_service_tax.Text.Trim() != String.Empty)
            {
                total_net_value_with_tax = total_net_value_with_tax + (total * Convert.ToDecimal(txt_service_tax.Text.Trim())) / 100;
            }
            if (txt_vat.Text.Trim() != String.Empty)
            {
                total_net_value_with_tax = total_net_value_with_tax + (total * Convert.ToDecimal(txt_vat.Text.Trim())) / 100;
            }
            if (txt_cst_with_c_form.Text.Trim() != String.Empty)
            {
                total_net_value_with_tax = total_net_value_with_tax + (total * Convert.ToDecimal(txt_cst_with_c_form.Text.Trim())) / 100;
            }
            if (txt_cst_without_c_form.Text.Trim() != String.Empty)
            {
                total_net_value_with_tax = total_net_value_with_tax + (total * Convert.ToDecimal(txt_cst_without_c_form.Text.Trim())) / 100;
            }
            //if (txt_freight.Text.Trim() != String.Empty)
            //{
            //    total_net_value_with_tax = total_net_value_with_tax + Convert.ToDecimal(txt_freight.Text.Trim());
            //}
            //if (txt_packaging.Text.Trim() != String.Empty)
            //{
            //    total_net_value_with_tax = total_net_value_with_tax + Convert.ToDecimal(txt_packaging.Text.Trim());
            //}

            return total + total_net_value_with_tax;
        }

        private Boolean CheckDeliverySchedule()
        {
            foreach (var item in base_Page.ItemTransactionList)
            {
                track = true;
                total = 0;
                foreach (var delivery in base_Page.DeliveryScheduleList)
                {
                    if (IsContractor)
                    {
                        if (item.DeliverySchedule.ActivityDescriptionId == delivery.ItemDescriptionId
                            && item.Item.ItemId == delivery.ItemId
                            && item.Item.ModelSpecification.ModelSpecificationId == delivery.SpecificationId)
                        {
                            var lst = base_Page.DeliveryScheduleList.Where(v => v.ItemDescriptionId == item.DeliverySchedule.ActivityDescriptionId
                                && v.ItemId == item.Item.ItemId && v.SpecificationId == item.Item.ModelSpecification.ModelSpecificationId);
                            total = lst.Sum(p => p.ItemQuantity);
                            if (item.NumberOfUnit != total)
                            {
                                track = false;
                            }
                        }
                    }
                    else
                    {
                        //For Supplier..............
                    }
                }
                if (track)
                {
                    break;
                }
            }

            return track;
        }

        private Boolean CheckPayentTerm()
        {
            Decimal tot = 0;
            foreach (PaymentTerm item in base_Page.PaymentTermsList)
            {
                tot += item.PercentageValue;
            }
            if (tot == 100)
            {
                return true;
            }

            return false;
        }

        private void Clear_All()
        {
            Clear_Common_Data();
            Clear_Transaction_Fields();
            ddlServiceDescription.SelectedValue = "0";
            Clear_Tax_Fields();
            EnableDisableTax(true);
        }

        private void Clear_Transaction_Fields()
        {
            rbtn_AddItem.SelectedValue = "0";
            hdf_category_level_id.Value = String.Empty;

            SetItemState();

            lbl_duplicate_activity.Text = String.Empty;
            lbl_category_level.Text = String.Empty;
            BindItem(null);
            BindItemModel(0);
            lbl_make.Text = String.Empty;
            lbl_unit_of_measurement.Text = String.Empty;
            //txt_activity_description.Text = String.Empty;
            //ddlServiceDescription.SelectedValue = "0";
            txt_Number_of_Unit.Text = String.Empty;
            txt_Per_Unit_Cost.Text = String.Empty;
            txt_Per_Unit_Discount.Text = String.Empty;

        }

        private void Clear_Common_Data()
        {
            ddl_contractor_name.SelectedIndex = 0;
            ddl_supplier.SelectedIndex = 0;
            ddl_contrcat_number.SelectedIndex = 0;
            txt_Delivery_Description.Text = String.Empty;
            txt_Subject_Description.Text = string.Empty;
            txt_Closing_Date.Text = String.Empty;
            txt_order_date.Text = String.Empty;

            base_Page.ItemTransactionList = null;

            //To Clear Total Amount Field:
            gv_Item_Data.DataSource = null;
            gv_Item_Data.DataBind();
        }

        private void Clear_Tax_Fields()
        {
            lbl_calculate_msg.Text = String.Empty;
            txtExciseDuty.Text = String.Empty;
            txt_service_tax.Text = String.Empty;
            txt_vat.Text = String.Empty;
            txt_cst_with_c_form.Text = String.Empty;
            txt_cst_without_c_form.Text = String.Empty;
            //txt_freight.Text = String.Empty;
            //txt_packaging.Text = String.Empty;
            txt_Per_Unit_Discount.Text = String.Empty;
            ddl_discount_mode.SelectedIndex = 0;
        }

        private void Clear_Session()
        {
            base_Page.DocumentStackId = 0;
            base_Page.DocumentsList = null;
            base_Page.ItemTransactionList = null;
            base_Page.DeliveryScheduleList = null;
            base_Page.PaymentTermsList = null;
            base_Page.TermConditionList = null;
            base_Page.QuotationStatusID = Convert.ToInt32(StatusType.Pending);
            ltrl_error_msg.Text = String.Empty;
        }

        private void SaveAllData(String action)
        {
            quotation_BL = new QuotationBL();
            lst_metaData = new List<MetaData>();
            if (IsContractor)
                lst_metaData = quotation_BL.CreatePurchaseOrder(GetFormData(action));
            else
                lst_metaData = quotation_BL.CreateSupplierPurchaseOrder(GetFormData(action));

            id = lst_metaData[0].Id;
            Quotation_number = lst_metaData[0].Name;

            if (id > 0)
            {
                CreateDocumentMapping();
                lst_delivery_schedule = CreateQuotationMapping(id);
                CreateDeliverySchedule(id, lst_delivery_schedule);
                CreatePaymentTerms(id);
                CreateTermsCondition(id);
                Clear_All();
                Clear_Session();

                if (IsContractor)
                {
                    if (action == btn_submit_draft.CommandName)
                        base_Page.Alert("Contractor Quotation Submitted successfully                                                       Quotation No : " + Quotation_number, btn_save_draft, "ContractorQuotation.aspx");
                    else
                        base_Page.Alert("Contractor Quotation Saved successfully                                                       Quotation No : " + Quotation_number, btn_save_draft, "ContractorQuotation.aspx");
                }
                else
                {
                    if (action == btn_submit_draft.CommandName)
                        base_Page.Alert("Supplier Quotation Submitted successfully                                                         Quotation No : " + Quotation_number, btn_save_draft, "SupplierQuotation.aspx");
                    else
                        base_Page.Alert("Supplier Quotation Saved successfully                                                         Quotation No : " + Quotation_number, btn_save_draft, "SupplierQuotation.aspx");

                }
            }
        }

        private void SetItemState()
        {
            rbtn_AddItem_SelectedIndexChanged(null, null);
        }

        private void EnableDisableItem(bool condition)
        {
            ddl_item.Enabled = condition;
            ddl_item_model.Enabled = condition;
            if (condition == true)
                lbl_cost.Text = "Cost (Per Unit)";
            else
                lbl_cost.Text = "Cost (Per Unit)";
        }

        private void ShowDiscountErrorMsg()
        {
            lbl_calculate_msg.Text = GlobalConstants.M_Discount_More_Than_Amount;
            track = true;
            EnableDisableTax(true);
        }

        private void EnableDisableTax(bool condition)
        {
            txtExciseDuty.Enabled = condition;
            txt_service_tax.Enabled = condition;
            txt_vat.Enabled = condition;
            txt_cst_with_c_form.Enabled = condition;
            txt_cst_without_c_form.Enabled = condition;
            //txt_freight.Enabled = condition;
            //txt_packaging.Enabled = condition;
            txt_Per_Unit_Discount.Enabled = condition;
            ddl_discount_mode.Enabled = condition;
        }

        private void ClearItemFields()
        {
            lbl_category_level.Text = String.Empty;
            ddl_item.SelectedIndex = 0;
            ddl_item_model.SelectedIndex = 0;
            lbl_make.Text = String.Empty;
        }

        private Decimal TotalGrandValue()
        {
            Decimal packaging = 0, freight = 0;
            if (!String.IsNullOrEmpty(txt_packaging.Text.Trim()))
            {
                packaging = Convert.ToDecimal(txt_packaging.Text.Trim());
            }
            if (!String.IsNullOrEmpty(txt_freight.Text.Trim()))
            {
                freight = Convert.ToDecimal(txt_freight.Text.Trim());
            }

            return TotalAmount + packaging + freight;
        }

        #endregion

        #region Private Property

        private int lstIndex
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

        private Int32 DiscountId
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

        private Decimal TotalAmount
        {
            get
            {
                if (ViewState["TotalAmount"] == null)
                    return 0;
                else
                    return (Decimal)ViewState["TotalAmount"];
            }
            set
            {
                ViewState["TotalAmount"] = value;
            }
        }

        private bool IsContractor
        {
            get
            {
                if (base_Page.Page_Name == GlobalConstants.P_Contractor_Quotation)
                {
                    flag = true;
                    return flag;
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            set
            {
                flag = value;
            }
        }

        #endregion

        #region Search Methods

        //protected void imgbtn_search_Click(object sender, ImageClickEventArgs e)

        protected void imgbtn_search_Click(object sender, CommandEventArgs e)
        {
            lst_quotation = new List<QuotationDOM>();
            quotation_BL = new QuotationBL();
            if (id == 0)
            {
                if (String.IsNullOrEmpty(txt_search.Text))
                {
                    if (IsContractor)
                        base_Page.Alert("Please Enter Contractor Quotation Number", imgbtn_search, "ContractorQuotation.aspx");
                    else
                        base_Page.Alert("Please Enter Supplier Quotation Number", imgbtn_search, "SupplierQuotation.aspx");
                    txt_search.Focus();
                    return;
                }
                else
                {
                    if (IsContractor)
                        lst_quotation = quotation_BL.ReadContractorQuotation(null, txt_search.Text.Trim());
                    else
                        lst_quotation = quotation_BL.ReadSupplierQuotation(null, txt_search.Text.Trim());
                }
            }
            else
            {

                if (IsContractor)
                    lst_quotation = quotation_BL.ReadContractorQuotation(id, null);
                else
                    lst_quotation = quotation_BL.ReadSupplierQuotation(id, null);
            }
            //If Quotaion Exist
            if (lst_quotation.Count == 1)
            {
                //In Copy
                if (e != null)
                    if (e.CommandName == imgbtn_copy.CommandName)
                    {
                        lst_quotation[0].StatusType.Id = Convert.ToInt32(StatusType.Pending);
                        base_Page.QuotationStatusID = lst_quotation[0].StatusType.Id;
                        lst_quotation[0].StatusType.Name = Convert.ToString(StatusType.Pending);
                        hdf_quotation_id.Value = String.Empty;
                        track = true;
                    }

                    //Set Quotaion_Id in Search
                    else
                    {
                        if (IsContractor)
                            hdf_quotation_id.Value = lst_quotation[0].ContractorQuotationId.ToString();
                        else
                            hdf_quotation_id.Value = lst_quotation[0].SupplierQuotationId.ToString();
                        //Set Quotation Status Id
                        base_Page.QuotationStatusID = lst_quotation[0].StatusType.Id;
                    }
                else
                {
                    if (IsContractor)
                        hdf_quotation_id.Value = lst_quotation[0].ContractorQuotationId.ToString();
                    else
                        hdf_quotation_id.Value = lst_quotation[0].SupplierQuotationId.ToString();
                    //Set Quotation Status Id
                    base_Page.QuotationStatusID = lst_quotation[0].StatusType.Id;
                }

                if ((lst_quotation[0].StatusType.Name == Convert.ToString(StatusType.Pending)) || (lst_quotation[0].StatusType.Name == Convert.ToString(StatusType.InComplete)))
                {
                    SetAllDefaultData(lst_quotation, track);
                    EnableDisableControls(true);
                }
                else
                {
                    SetAllDefaultData(lst_quotation, false);
                    EnableDisableControls(false);
                    //EnableDisableTax(false);
                }

                ltrl_error_msg.Text = String.Empty;
            }
            else
            {
                if (IsContractor)
                    base_Page.Alert(GlobalConstants.M_Quotaion_Not_Exist, btn_save_draft, "ContractorQuotation.aspx");
                else
                    base_Page.Alert(GlobalConstants.M_Quotaion_Not_Exist, btn_save_draft, "SupplierQuotation.aspx");
            }
        }

        private void SetAllDefaultData(List<QuotationDOM> lst_quotation, bool isCopy)
        {
            GetQuotaionMapping(lst_quotation, isCopy);
            GetCommonData(lst_quotation, isCopy);
            GetDocumentData(lst_quotation, isCopy);
            GetDeliveryScheduleData(lst_quotation, isCopy);
            GetPaymentTerms(lst_quotation, isCopy);
            GetTermsConditions(lst_quotation, isCopy);

        }

        private void GetCommonData(List<QuotationDOM> lst_quotation, bool isCopy)
        {
            if (IsContractor)
            {
                ddl_contractor_name.SelectedValue = lst_quotation[0].ContractorId.ToString();
                ddl_contrcat_number.SelectedValue = lst_quotation[0].ContractId.ToString();
                ddl_contrcat_number_SelectedIndexChanged(null, null);
                ddl_work_order_number.SelectedValue = lst_quotation[0].WorkOrderId.ToString();
                txt_Subject_Description.Text = lst_quotation[0].subjectdescription;
                //ddl_contractor_name.SelectedIndex = ddl_contractor_name.Items.IndexOf(ddl_contractor_name.Items.FindByText(lst_quotation[0].ContractorName));
                //ddl_contrcat_number.SelectedIndex = ddl_contrcat_number.Items.IndexOf(ddl_contrcat_number.Items.FindByText(lst_quotation[0].ContractNumber));
                //ddl_contrcat_number_SelectedIndexChanged(null, null);
                //ddl_work_order_number.SelectedIndex = ddl_work_order_number.Items.IndexOf(ddl_work_order_number.Items.FindByText(lst_quotation[0].WorkOrderNumber));
            }
            else
            {
                ddl_supplier.SelectedValue = lst_quotation[0].SupplierId.ToString();
                // ddl_supplier.SelectedIndex = ddl_supplier.Items.IndexOf(ddl_supplier.Items.FindByText(lst_quotation[0].SupplierName));
                txt_Closing_Date.Text = Convert.ToString(lst_quotation[0].ClosingDate.ToString("dd'/'MM'/'yyyy"));
                txt_Delivery_Description.Text = lst_quotation[0].DeliveryDescription;
                txt_Subject_Description.Text = lst_quotation[0].subjectdescription;
                txt_freight.Text = lst_quotation[0].Freight.ToString();
                txt_packaging.Text = lst_quotation[0].Packaging.ToString();
                lblGrandTotal.Text = String.Concat("Grand Total : ", TotalGrandValue().ToString());
                hdnfGrandTotal.Value = TotalGrandValue().ToString();
            }
            txt_order_date.Text = Convert.ToString(lst_quotation[0].OrderDate.ToString("dd'/'MM'/'yyyy"));
            TotalAmount = lst_quotation[0].TotalNetValue;
        }

        private void GetDocumentData(List<QuotationDOM> lst_quotation, bool isCopy)
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = document_BL.ReadDocumnetMapping(lst_quotation[0].UploadDocumentId);
            if (lst_document.Count >= 1)
            {
                if (!isCopy)
                {
                    base_Page.DocumentsList = lst_document;

                    if (isCopy)
                        LoadData(true);
                    else
                        base_Page.DocumentStackId = lst_document[0].DocumentId;

                    base_Page.DocumentSerial = lst_document[0].LastIndex;
                    base_Page.Page_Name = pageName;
                    BindDocument();
                }
            }
        }

        private void GetQuotaionMapping(List<QuotationDOM> lst_quotation, bool isCopy)
        {
            lst_item_transaction = new List<ItemTransaction>();
            quotation_BL = new QuotationBL();
            if (IsContractor)
                lst_item_transaction = quotation_BL.ReadContractorQuotationMapping(lst_quotation[0].ContractorQuotationId);
            else
                lst_item_transaction = quotation_BL.ReadSupplierQuotationMapping(lst_quotation[0].SupplierQuotationId);

            //For Copy Case
            if (isCopy && lst_item_transaction.Count != 0)
            {
                for (int item = 0; item < lst_item_transaction.Count; item++)
                {
                    lst_item_transaction[item].DeliverySchedule.Id = 0;
                }
            }

            if (lst_item_transaction.Count >= 1)
            {
                base_Page.ItemTransactionList = lst_item_transaction;
                BindItemtransaction();
            }
        }

        private void GetDeliveryScheduleData(List<QuotationDOM> lst_quotation, bool isCopy)
        {
            delivery_schedule_BL = new DeliveryScheduleBL();
            lst_delivery_schedule = new List<DeliveryScheduleDOM>();

            if (IsContractor)
                lst_delivery_schedule = delivery_schedule_BL.ReadDeliveryScheduleByQuotationID(lst_quotation[0].ContractorQuotationId, Convert.ToInt16(QuotationType.Contractor));
            else
                lst_delivery_schedule = delivery_schedule_BL.ReadDeliveryScheduleByQuotationID(lst_quotation[0].SupplierQuotationId, Convert.ToInt16(QuotationType.Supplier));

            //For Copy Case
            if (isCopy && lst_delivery_schedule.Count != 0)
            {
                for (int item = 0; item < lst_delivery_schedule.Count; item++)
                {
                    lst_delivery_schedule[item].Id = 0;
                }
            }

            if (lst_delivery_schedule.Count >= 1)
            {
                base_Page.DeliveryScheduleList = lst_delivery_schedule;
            }
        }

        private void GetPaymentTerms(List<QuotationDOM> lst_quotation, bool isCopy)
        {
            payment_Term_BL = new PaymentTermBL();
            lst_payment_term = new List<PaymentTerm>();

            if (IsContractor)
                lst_payment_term = payment_Term_BL.ReadPaymentTermByPurchaseOI(lst_quotation[0].ContractorQuotationId, Convert.ToInt16(QuotationType.Contractor));
            else
                lst_payment_term = payment_Term_BL.ReadPaymentTermByPurchaseOI(lst_quotation[0].SupplierQuotationId, Convert.ToInt16(QuotationType.Supplier));

            //For Copy Case
            if (isCopy && lst_payment_term.Count != 0)
            {
                for (int item = 0; item < lst_payment_term.Count; item++)
                {
                    lst_payment_term[item].PaymentTermId = 0;
                }
            }

            if (lst_payment_term.Count >= 1)
            {
                base_Page.PaymentTermsList = lst_payment_term;
            }
        }

        private void GetTermsConditions(List<QuotationDOM> lst_quotation, bool isCopy)
        {
            term_condition_BL = new TermAndConditionBL();
            lst_term_condition = new List<TermAndCondition>();

            if (IsContractor)
                lst_term_condition = term_condition_BL.ReadTermAndConditionByQuotationID(lst_quotation[0].ContractorQuotationId, Convert.ToInt16(QuotationType.Contractor));
            else
                lst_term_condition = term_condition_BL.ReadTermAndConditionByQuotationID(lst_quotation[0].SupplierQuotationId, Convert.ToInt16(QuotationType.Supplier));

            //For Copy Case
            if (isCopy && lst_term_condition.Count != 0)
            {
                for (int item = 0; item < lst_term_condition.Count; item++)
                {
                    lst_term_condition[item].Id = 0;
                }
            }

            if (lst_term_condition.Count >= 1)
            {
                base_Page.TermConditionList = lst_term_condition;
            }
        }

        private void EnableDisableControls(bool condition)
        {
            ddl_contractor_name.Enabled = condition;
            ddl_contrcat_number.Enabled = condition;
            ddl_work_order_number.Enabled = condition;
            ddl_supplier.Enabled = condition;

            rbtnTaxType.Enabled = condition;

            img_btn_calander_order.Visible = condition;

            //txt_order_date.Enabled = condition;
            //txt_Closing_Date.Enabled = condition;

            ajaxupload.Visible = condition;

            tbl_itemTransaction.Visible = condition;
            div_button.Visible = condition;

            btn_submit_draft.Visible = condition;
            btn_save_draft.Visible = condition;
            btn_cancel_draft.Visible = condition;

            //Column 9 : Action
            gv_Item_Data.Columns[18].Visible = condition;

            //Column 9 : Delete for Documents
            gv_documents.Columns[1].Visible = condition;

        }

        #endregion End Search Methods

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
            else if (e.CommandName == "OpenFile")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                Index = Convert.ToInt32(e.CommandArgument);
                lst_documents = new List<Document>();
                lst_documents = base_Page.DocumentsList;

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtn_file");

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

        protected void btn_uploadExcel_Click(object sender, EventArgs e)
        {
            ReadDataFromExcel();
            BindItemtransaction();
        }
        private void Bindgrid(DataTable csvdt)
        {
            GridView1.DataSource = csvdt;
            //  gv_Item_Data.DataSource = csvdt;
            GridView1.DataBind();
        }
        private void ReadDataFromExcel()
        {
            //Creating object of datatable  
            DataTable tblcsv = new DataTable();
            string path = Server.MapPath("~/ExcelFile/" + FileUpload1.FileName);
            FileUpload1.PostedFile.SaveAs(path);
            string CSVFilePath = Path.GetFullPath(path);
            //Reading All text  
            var CSVLines = File.ReadAllLines(CSVFilePath);
            base_Page.ItemTransactionList = new List<ItemTransaction>();

            foreach (var line in CSVLines)
            {
                var itemTransaction = new ItemTransaction();
                itemTransaction.Item = new Item();
                itemTransaction.TaxInformation = new Tax();
                itemTransaction.TaxInformation.DiscountMode = new MetaData();
                itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                itemTransaction.Item.ModelSpecification = new ModelSpecification();
                itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
                itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                var dataArray = line.Split(',');
                if (dataArray[0] == "Item Name")
                {
                    continue;
                }
                itemTransaction.Item.ItemName = dataArray[0];
                itemTransaction.Item.ItemId = int.Parse(dataArray[1]);
                itemTransaction.Item.ModelSpecification.ModelSpecificationName = dataArray[2];
                itemTransaction.Item.ModelSpecification.ModelSpecificationId = int.Parse(dataArray[3]);
                itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = dataArray[4];
                itemTransaction.Item.ModelSpecification.Category.ItemCategoryId = int.Parse(dataArray[5]);
                itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = dataArray[7];
                itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = int.Parse(dataArray[8]);
                itemTransaction.TaxInformation.DiscountMode.Name = dataArray[10];
                itemTransaction.TaxInformation.DiscountMode.Id = int.Parse(dataArray[11]);
                itemTransaction.NumberOfUnit = decimal.Parse(dataArray[6]);
                itemTransaction.PerUnitCost = decimal.Parse(dataArray[9]);
                itemTransaction.PerUnitDiscount = decimal.Parse(dataArray[12]);
                itemTransaction.DeliverySchedule.ActivityDescriptionId = 0;
                itemTransaction.DeliverySchedule.ActivityDescription = String.Empty;

                total_net_value = (itemTransaction.NumberOfUnit * itemTransaction.PerUnitCost);
                total = total_net_value;
                if (itemTransaction.TaxInformation.DiscountMode.Id != Convert.ToInt32(DiscountType.None) && itemTransaction.PerUnitDiscount != Decimal.MinValue)
                {
                    if (itemTransaction.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Value))
                        total = Math.Round((total_net_value - (itemTransaction.NumberOfUnit * itemTransaction.PerUnitDiscount)), 2);

                    else if (itemTransaction.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Percentage))
                        total = Math.Round((total_net_value - (total_net_value * itemTransaction.PerUnitDiscount / 100)), 2);
                }

                itemTransaction.TotalAmount = total;

                if (itemTransaction.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Value))
                    DiscountRate = Math.Round((itemTransaction.PerUnitCost - itemTransaction.PerUnitDiscount), 2);

                else if (itemTransaction.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Percentage))
                    DiscountRate = Math.Round((itemTransaction.PerUnitCost - (itemTransaction.PerUnitCost * itemTransaction.PerUnitDiscount / 100)), 2);

                itemTransaction.Discount_Rates = DiscountRate;
                base_Page.ItemTransactionList.Add(itemTransaction);
            }
            //spliting row after new line  
            //foreach (string csvRow in ReadCSV.Split('\n'))
            //{
            //    if (!string.IsNullOrEmpty(csvRow))
            //    {
            //        //Adding each row into datatable  
            //        tblcsv.Rows.Add();
            //        int count = 0;
            //        foreach (string FileRec in csvRow.Split(','))
            //        {
            //            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
            //            count++;

            //            base_Page.ItemTransactionList = new List<ItemTransaction>()
            //            {

            //                item_Transaction.Item.ItemName = tblcsv.Rows[tblcsv.Rows.Count - 1]
            //                // item_Transaction.Item.ItemName= tblcsv.Rows[tblcsv.Rows.Count - 1][count],
            //            };
            //        }//
            //    }
            //    //Calling Bind Grid Functions  
            //    Bindgrid(tblcsv);

            //}
        }
        #endregion
    }
}