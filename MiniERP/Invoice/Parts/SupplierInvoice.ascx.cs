using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using BusinessAccessLayer.Quality;
using BusinessAccessLayer.Invoice;
using MiniERP.Shared;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;
using System.Reflection;


namespace MiniERP.Invoice.Parts
{
    public partial class SupplierInvoice : System.Web.UI.UserControl
    {
        #region Private global variables

        InvoiceDom invoiceDom = null;
        MetaData metaData = null;
        List<SupplierRecieveMatarial> lstSupplierRecieveMatarial = null;
        SupplierRecieveMaterialBAL SupplierReceiveMatBal = new SupplierRecieveMaterialBAL();
        ItemTransaction itemTransaction = null;
        PaymentTermBL paymentTermBL = null;
        BasePage basePage = new BasePage();

        MetaDataBL metadataBL = new MetaDataBL();

        Decimal freight = 0;
        Decimal packaging = 0;
        Decimal TotalNetValue = 0;
        Decimal TotalAmount = 0;
        int i = 0;
        int j = 0;
        string strInvalid = string.Empty;
        decimal dec = 0;
        int cnt = 0;
        String s = string.Empty;
        decimal BigNo = 0;
        Boolean flag = false;
        DateTime dateAfterDays;
        decimal priceTotal = 0;
        RadioButton rdbtn = null;
        Label lblNoOfDays = null;
        TextBox txtPercenatageValue = null;
        HiddenField hdfPaymentTermId = null;
        HiddenField hdfPaymentTypeId = null;
        Label lblIndex = null;
        SupplierInvoiceBL supplierInvoiceBL = null;

        #endregion

        #region protected methods

        protected void Page_Load(object sender, EventArgs e)
        {
          
            imgPayableAmt.Visible = false;
            if (!IsPostBack)
            {
                LoadDefaultData();
            }
        }

        private void LoadDefaultData()
        {
            btnAddSupplierItem.Visible = false;
            btnAddInvoice.Visible = false;
            btnSaveDraft.Visible = false;
            btnReset.Visible = false;
            SPON.Visible = false;
            txtInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtBillDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            BindRadioButton();
            rbtnInvoiceType.SelectedValue = Convert.ToInt32(InvoiceType.Normal).ToString();
            if (Request.QueryString["invoiceId"] != null)
            {
                EmptyDocumentList();
                InvoiceId = 0;
                InvoiceId = Convert.ToInt32(Request.QueryString["InvoiceId"]);
                EditData(InvoiceId);
            }
            else
            {
                EmptyDocumentList();
                InvoiceId = 0;
                Panel1.Visible = false;
                Enabled(false);
                txtSupplierPONumber.Focus();
            }
            if (rbtnInvoiceType.SelectedValue == Convert.ToInt32(InvoiceType.Advance).ToString())
            {
                gvSupplierAdd.Enabled = false;
                btnAddSupplierItem.Visible = false;
                btnAddInvoice.Visible = false;
                btnSaveDraft.Visible = false;
                btnAdvance.Visible = true;
                btnReset.Visible = false;
                excise.Visible = true;
                excise1.Visible = true;
                excise2.Visible = true;
                excise3.Visible = true;
                excies4.Visible = true;
                Reset();
            }
            else
            {
                gvSupplierAdd.Enabled = true;
                //btnAddSupplierItem.Visible = false;
                btnAdvance.Visible = false;
                excise.Visible = false;
                excise1.Visible = false;
                excise2.Visible = false;
                excise3.Visible = false;
                excies4.Visible = false;
                Reset();
            }
        }

        protected void imgbtn_search_Click(object sender, EventArgs e)
        {
            if (SupplierPurchseOrderNumber != String.Empty)
            {
                ViewState["InvoiceAdded"] = null;
                ViewState["AdvanceInvoice"] = null;
                lstItemTransaction = null;
                gvAddItems.DataSource = lstItemTransaction;
                gvAddItems.DataBind();
                gvInvoice.DataSource = lstItemTransaction;
                gvInvoice.DataBind();
                LoadDefaultData();
                LoadAllData();
            }
            else
            {
                if (string.IsNullOrEmpty(txtSupplierPONumber.Text.Trim()))
                {
                    basePage.Alert("Please enter Supplier Purchase Order Number!", imgbtn_search);
                }
                else
                {
                    LoadAllData();
                }
            }
        }

        private void LoadAllData()
        {
            supplierInvoiceBL = new SupplierInvoiceBL();
            lstItemTransaction = new List<ItemTransaction>();
            lstInvoiceDom = new List<InvoiceDom>();
            lstPaymentTerm = new List<PaymentTerm>();
            if (SupplierPurchseOrderNumber == String.Empty)
            {
                SupplierPurchseOrderNumber = txtSupplierPONumber.Text.Trim().ToString();
            }
            lstInvoiceDom = supplierInvoiceBL.ReadSupplierQuotation(null, SupplierPurchseOrderNumber);
            lstMetaData = new List<MetaData>();
            lstMetaData = supplierInvoiceBL.ReadSupplierPaymentTerm(null, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, SupplierPurchseOrderNumber);
            if (lstInvoiceDom.Count > 0)
            {
                if (lstInvoiceDom[0].LeftAmount == 0)
                {
                    basePage.Alert(" All Payment terms have been completed for this Supplier Purchase Order Number!", imgbtn_search);
                }
                else
                {
                    if (lstInvoiceDom.Count > 0 && lstInvoiceDom[0].ReceiveMaterial.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
                    {
                        SetAllDefaultData(lstInvoiceDom);
                        if (lstPaymentTerm.Count > 0)
                        {
                            Panel1.Visible = true;
                        }
                        lstItemTransaction = supplierInvoiceBL.ReadSupplierPOReceiveMaterialMapping(null, SupplierPurchseOrderNumber);
                        if (lstItemTransaction.Count > 0)
                        {
                            BindgvSupplierAdd();
                            gvSupplierAdd.Visible = true;
                        }
                        txtSupplierPONumber.Text = String.Empty;
                        btnAddSupplierItem.Visible = true;
                        SPON.Visible = true;
                    }
                    else if (lstInvoiceDom.Count > 0 && lstInvoiceDom[0].ReceiveMaterial.Quotation.StatusType.Id != Convert.ToInt32(StatusType.Generated))
                    {
                        basePage.Alert("Supplier Purchase Order is not generated!", imgbtn_search);
                    }
                    else
                    {
                        basePage.Alert("Invalid Supplier Purchase Order Number!", imgbtn_search);
                    }
                }
            }
            else
            {
                basePage.Alert("Please enter Valid Supplier Purchase Order Number!", imgbtn_search);
            }
            SupplierPurchseOrderNumber = string.Empty;
        }

        protected void gvPaymentType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                rdbtn = (RadioButton)e.Row.FindControl("rdbtn");
                hdfPaymentTermId = (HiddenField)e.Row.FindControl("hdfPaymentTermId");
                lblNoOfDays = (Label)e.Row.FindControl("lblNoOfDays");

                if (!string.IsNullOrEmpty(hdnQutnGenDate.Value))
                {
                    dateAfterDays = Convert.ToDateTime(hdnQutnGenDate.Value).AddDays(Convert.ToInt32(lblNoOfDays.Text));

                    foreach (PaymentTerm item in lstPaymentTerm)
                    {
                        if (dateAfterDays > DateTime.Now)
                        {
                            rdbtn.Checked = false;
                            rdbtn.Enabled = false;
                        }
                    }
                }
                if (lstMetaData.Count != 0 && InvoiceId == 0)
                {
                    foreach (MetaData item in lstMetaData)
                    {
                        if (Convert.ToInt32(hdfPaymentTermId.Value) == item.Id || dateAfterDays >= DateTime.Now)
                        {
                            rdbtn.Checked = false;
                            rdbtn.Enabled = false;
                        }
                    }
                }
                else if (lstInvoiceDom.Count != 0 && InvoiceId != 0)
                {
                    foreach (MetaData item in lstMetaData)
                    {
                        if (Convert.ToInt32(hdfPaymentTermId.Value) == lstInvoiceDom[0].Payment.PaymentTermId)
                        {
                            rdbtn.Checked = true;
                            rdbtn.Enabled = true;
                        }
                        else if (Convert.ToInt32(hdfPaymentTermId.Value) != lstInvoiceDom[0].Payment.PaymentTermId)
                        {
                            rdbtn.Checked = false;
                            rdbtn.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            ltrl_err_msg.Text = string.Empty;
            metaData = new MetaData();
            invoiceDom = new InvoiceDom();
            basePage = new BasePage();
            invoiceDom = GetSupplierInvoiceDetails();

            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            //----------------------------
            else if (InvoiceId > 0)
            {
                metaData = CreateSupplierInvoice(invoiceDom, InvoiceId);
            }
            else
            {
                metaData = CreateSupplierInvoice(invoiceDom, null);
            }
            if (metaData.Id > 0)
            {
                CreateDocumentMapping();
                if (InvoiceId > 0)
                {
                    basePage.Alert("Supplier Invoice No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewSupplierInvoice.aspx");
                    Reset();
                }
                else
                {
                    basePage.Alert("Supplier Invoice No: " + metaData.Name + " Created Successfully", btnSaveDraft, "SupplierInvoice.aspx");
                    Reset();
                }
                BindgvSupplierAdd();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierInvoice.aspx");
            imgbtn_search_Click(null, null);
        }

        protected void rdbtn_Click(object sender, EventArgs e)
        {
            imgPayableAmt.Visible = false;
            invoiceDom = new InvoiceDom();
            foreach (GridViewRow row in gvPaymentType.Rows)
            {
                rdbtn = (RadioButton)row.FindControl("rdbtn");
                txtPercenatageValue = (TextBox)row.FindControl("txtPercenatageValue");
                //lblPercenatageValue = (Label)row.FindControl("lblPercenatageValue");
                if (rdbtn.Checked == true && InvoiceId == 0)
                {
                    //  invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(lblPercenatageValue.Text))) / 100;

                    invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(txtPercenatageValue.Text))) / 100;
                    lblPayableAmount.Text = invoiceDom.PayableAmount.ToString("F2");
                    if (Convert.ToDecimal(lblPayableAmount.Text) > 0)
                    {
                        imgPayableAmt.Visible = true;
                    }
                    else
                    {
                        imgPayableAmt.Visible = false;
                    }
                    Enabled(true);
                    break;
                }

            }
        }

        protected void chbxSelectAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvSupplierAdd.Rows)
            {
                CheckBox chkbxQuotationDetails = (CheckBox)row.FindControl("chkSelect");
                if (chkbxQuotationDetails.Enabled == true)
                {
                    chkbxQuotationDetails.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void chkSelect_Click(object sender, EventArgs e)
        {
            flag = false;
            CheckBox chbxSelectAll = (CheckBox)gvSupplierAdd.HeaderRow.FindControl("chbxSelectAll");
            foreach (GridViewRow row in gvSupplierAdd.Rows)
            {
                CheckBox chkbxQuotationDetails = (CheckBox)row.FindControl("chkSelect");
                if (!chkbxQuotationDetails.Checked)
                    flag = true;
            }
            if (flag == true)
            {
                chbxSelectAll.Checked = false;
            }
            else
            {
                chbxSelectAll.Checked = true;
            }
        }

        protected void TextLeave_Click(object sender, EventArgs e)
        {
            decimal amount = 0;
            foreach (GridViewRow row in gvAddItems.Rows)
            {
                TextBox txtRecieveQuantity = (TextBox)row.FindControl("txtRecieveQuantity");
                Label lblPerUnitCost = (Label)row.FindControl("lblPerUnitCost");
                if (amount == 0)
                {
                    amount = (Convert.ToDecimal(txtRecieveQuantity.Text)) * (Convert.ToDecimal(lblPerUnitCost.Text));

                }
                else
                {
                    amount = amount + ((Convert.ToDecimal(txtRecieveQuantity.Text)) * (Convert.ToDecimal(lblPerUnitCost.Text)));
                }
                lblPayableAmount.Text = amount.ToString();
            }
        }

        protected void btnAddSupplierItem_Click(object sender, EventArgs e)
        {
            btnAddInvoice.Visible = true;
            basePage = new BasePage();
            if (ViewState["InvoiceAdded"] == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (GridViewRow row in gvSupplierAdd.Rows)
            {
                CheckBox chkbxQuotationDetails = (CheckBox)row.FindControl("chkSelect");
                HiddenField hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
                Label lblItem = (Label)row.FindControl("lblItem");
                Label lblSpecification = (Label)row.FindControl("lblSpecification");
                Label lblItemCategory = (Label)row.FindControl("lblItemCategory");
                Label lblRecieveQuantity = (Label)row.FindControl("lblRecieveQuantity");
                Label lblMake = (Label)row.FindControl("lblMake");
                Label lblMeasurement = (Label)row.FindControl("lblMeasurement");
                Label lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
                Label lblItemLeft = (Label)row.FindControl("lblItemLeft");
                Label lblPerUnitCost = (Label)row.FindControl("lblPerUnitCost");
                Label lblPerUnitDicount = (Label)row.FindControl("lblPerUnitDiscount");
                HiddenField hdnfldSupplierPOMappingId = (HiddenField)row.FindControl("hdnfldSupplierPOMappingId");
                //used for receive quantity
                Decimal lblItemReceivedAuto = Convert.ToDecimal(lblItemQuantity.Text) - (Convert.ToDecimal(lblRecieveQuantity.Text));
                if (chkbxQuotationDetails.Checked == true && hdfSupplierPOMappingId != null)
                {
                    if (chkbxQuotationDetails.Checked.Equals(true))
                    {
                        itemTransaction = new ItemTransaction();
                        itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                        itemTransaction.MetaProperty = new MetaData();
                        itemTransaction.TaxInformation = new Tax();
                        itemTransaction.TaxInformation.DiscountMode = new MetaData();
                        itemTransaction.PerUnitCost = Convert.ToDecimal(lblPerUnitCost.Text);
                        itemTransaction.Item = new Item();
                        itemTransaction.Item.ModelSpecification = new ModelSpecification();
                        itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                        itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
                        //itemTransaction.QuantityReceived = Convert.ToDecimal(lblRecieveQuantity.Text);
                        itemTransaction.QuantityReceived = lblItemReceivedAuto;
                        itemTransaction.ItemReceivedQuality = Convert.ToDecimal(lblItemQuantity.Text);
                        itemTransaction.PerUnitDiscount = Convert.ToDecimal(lblPerUnitDicount.Text.ToString());
                        itemTransaction.UnitLeft = Convert.ToDecimal(lblItemLeft.Text.ToString());
                        itemTransaction.Item.ItemName = lblItem.Text.ToString();
                        itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Brand.BrandName = lblMake.Text.ToString();
                        itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString(); ;
                        itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
                        itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdnfldSupplierPOMappingId.Value);
                        lstItemTransaction.Add(itemTransaction);
                        chkbxQuotationDetails.Checked = false;
                        chkbxQuotationDetails.Enabled = false;
                        ViewState["InvoiceAdded"] = "Added";
                    }
                }
            }
            if (lstItemTransaction.Count != 0)
            {
                gvAddItems.DataSource = lstItemTransaction;
                gvAddItems.DataBind();
                Enabled(true);
            }
            else
            {
                Enabled(false);
                basePage.Alert("Please Check At Least One Item Details", btnAddSupplierItem);
            }
            foreach (TableCell item in gvSupplierAdd.HeaderRow.Cells)
            {
                CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
                if (chbxSelectAll.Checked == true)
                {
                    chbxSelectAll.Checked = false;
                }
            }
            btnSaveDraft.Visible = false;
            btnReset.Visible = false;
        }

        protected void btnAdvance_Click(object sender, EventArgs e)
        {
            basePage = new BasePage();
            Label TotalAmount = new Label();
            Label Totaltax = new Label();
            decimal amount = 0;
            decimal TotalTax = 0;
            decimal amountwithExcise = 0;
            decimal z = 0;
            if (ViewState["AdvanceInvoice"] == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            itemTransaction = new ItemTransaction();
            itemTransaction.TaxInformation = new Tax();
            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.AdvanceValue = TryToParse(txtAdvance.Text.Trim());
            itemTransaction.TaxInformation.ExciseDuty = TryToParse(txtExciseDuty.Text.Trim());
            itemTransaction.TaxInformation.ServiceTax = TryToParse(txt_service_tax.Text.Trim());
            itemTransaction.TaxInformation.VAT = TryToParse(txt_vat.Text.Trim());
            itemTransaction.TaxInformation.CSTWithCForm = TryToParse(txt_cst_with_c_form.Text.Trim());
            itemTransaction.TaxInformation.CSTWithoutCForm = TryToParse(txt_cst_without_c_form.Text.Trim());
            itemTransaction.TaxInformation.Freight = TryToParse(txt_Freight.Text.Trim());
            itemTransaction.TaxInformation.Packaging = TryToParse(txt_Packaging.Text.Trim());
            itemTransaction.TaxInformation.PercentageQuty = TryToParse(txtPercenatageValue.Text.Trim());
            //itemTransaction.TaxInformation.

            //amount = TryToParse(lblTotalNetValue.Text) + (itemTransaction.AdvanceValue * TryToParse(lblTotalNetValue.Text)) / 100;
            //TotalTax = ((itemTransaction.TaxInformation.ServiceTax + itemTransaction.TaxInformation.VAT + itemTransaction.TaxInformation.CSTWithCForm + itemTransaction.TaxInformation.CSTWithoutCForm + itemTransaction.TaxInformation.ExciseDuty) * amount) / 100;
            amount = (TryToParse(lblTotalNetValue.Text.Trim()) * (TryToParse(txtAdvance.Text.Trim()))) / 100;
            amountwithExcise = (TryToParse(txtExciseDuty.Text.Trim()) * amount) / 100;
            TotalTax = ((itemTransaction.TaxInformation.ServiceTax + itemTransaction.TaxInformation.VAT + itemTransaction.TaxInformation.CSTWithCForm + itemTransaction.TaxInformation.CSTWithoutCForm) * amountwithExcise) / 100;

            z = amount + TotalTax + itemTransaction.TaxInformation.Freight + itemTransaction.TaxInformation.Packaging;
            Totaltax.Text = TotalTax.ToString("F2");
            TotalAmount.Text = z.ToString("F2");

            itemTransaction.TotalAmount = Convert.ToDecimal(lblTotalNetValue.Text);
            itemTransaction.TaxInformation.TotalTax = Convert.ToDecimal(Totaltax.Text);
            itemTransaction.TaxInformation.TotalNetValue = Convert.ToDecimal(TotalAmount.Text);
            itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
            lstItemTransaction.Add(itemTransaction);
            ViewState["AdvanceInvoice"] = "AdvanceInvoiceEnd";

            if (lstItemTransaction.Count == 1)
            {
                gvAdvanceSave.DataSource = lstItemTransaction;
                gvAdvanceSave.DataBind();
                gvAdvanceSave.Visible = true;
                Enabled(true);
            }
            else if (lstItemTransaction.Count >= 1)
            {
                basePage.Alert("Advanced amount has been already added.", btnAdvance);
            }
            Reset();
        }

        protected void btnAddInvoice_Click(object sender, EventArgs e)
        {

            basePage = new BasePage();
            Label TotalAmount = new Label();
            Label Totaltax = new Label();
            decimal amount = 0;
            decimal TotalTax = 0;
            decimal z = 0;
            decimal x = 0;
            decimal y = 0;

            lstItemTransaction = new List<ItemTransaction>();

            foreach (GridViewRow row in gvAddItems.Rows)
            {
                //CheckBox chkbxQuotationDetails = (CheckBox)row.FindControl("chkSelect");
                //HiddenField hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
                Label lblItem = (Label)row.FindControl("lblItem");
                Label lblSpecification = (Label)row.FindControl("lblSpecification");
                Label lblItemCategory = (Label)row.FindControl("lblItemCategory");
                Label lblRecieveQuantity = (Label)row.FindControl("lblRecieveQuantity");
                Label lblMake = (Label)row.FindControl("lblMake");
                Label lblMeasurement = (Label)row.FindControl("lblMeasurement");
                Label lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
                Label lblItemLeft = (Label)row.FindControl("lblItemLeft");
                Label lblPerUnitCost = (Label)row.FindControl("lblPerUnitCost");
                //Label lblTotalWithTax = (Label)row.FindControl("lblTotalWithTax");
                TextBox txtRecieveQuantity = (TextBox)row.FindControl("txtRecieveQuantity");
                TextBox txt_excise_duty = (TextBox)row.FindControl("txt_excise_duty");
                TextBox txt_service_tax = (TextBox)row.FindControl("txt_service_tax");
                TextBox txt_vat = (TextBox)row.FindControl("txt_vat");
                TextBox txt_cst_with_c_form = (TextBox)row.FindControl("txt_cst_with_c_form");
                TextBox txt_cst_without_c_form = (TextBox)row.FindControl("txt_cst_without_c_form");
                Label txt_Percent_Dis = (Label)row.FindControl("txtPercentageDicount");
                HiddenField hdnfldSupplierPOMappingId = (HiddenField)row.FindControl("hdnfldSupplierPOMappingId");

                //Total3Tax = (TryToParse(txt_service_tax.Text) + TryToParse(txt_vat.Text) + TryToParse(txt_cst_without_c_form.Text)) * amount / 100;
                //Total3tax.Text = TotalTax.ToString("F2");
                //sundeep=========
                //x =(TryToParse(txtRecieveQuantity.Text) * TryToParse(lblPerUnitCost.Text)) * TryToParse(txt_Percent_Dis.Text) / 100;
                //y = TryToParse(txtRecieveQuantity.Text) * TryToParse(lblPerUnitCost.Text);
                x = TryToParse(lblPerUnitCost.Text) - (TryToParse(lblPerUnitCost.Text) * TryToParse(txt_Percent_Dis.Text) / 100);
                amount = TryToParse(x.ToString("F2")) * TryToParse(txtRecieveQuantity.Text);

                TotalTax = (amount + (amount * TryToParse(txt_excise_duty.Text) / 100)) + (amount + (amount * TryToParse(txt_excise_duty.Text) / 100)) * TryToParse(txt_cst_with_c_form.Text) / 100;
                z = TotalTax + TotalTax * ((TryToParse(txt_service_tax.Text) + TryToParse(txt_vat.Text) + TryToParse(txt_cst_without_c_form.Text)) / 100);
                TotalAmount.Text = z.ToString("F2");

                itemTransaction = new ItemTransaction();
                itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                itemTransaction.MetaProperty = new MetaData();
                itemTransaction.TaxInformation = new Tax();
                itemTransaction.TaxInformation.DiscountMode = new MetaData();
                itemTransaction.PerUnitCost = Convert.ToDecimal(lblPerUnitCost.Text);
                itemTransaction.Item = new Item();
                itemTransaction.Item.ModelSpecification = new ModelSpecification();
                itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
                itemTransaction.ItemReceivedInvoice = Math.Round(Convert.ToDecimal(txtRecieveQuantity.Text), 3);
                itemTransaction.ItemReceivedQuality = Math.Round(Convert.ToDecimal(lblItemQuantity.Text), 2);
                itemTransaction.UnitLeft = Math.Round(Convert.ToDecimal(lblItemLeft.Text.ToString()), 2);
                itemTransaction.Item.ItemName = lblItem.Text.ToString();
                itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                itemTransaction.Item.ModelSpecification.Brand.BrandName= lblMake.Text.ToString();
                itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString();
                itemTransaction.TaxInformation.Freight = TryToParse(txt_Freight.Text);
                itemTransaction.TaxInformation.Packaging = TryToParse(txt_Packaging.Text);
                itemTransaction.TaxInformation.ExciseDuty = Math.Round(Convert.ToDecimal(txt_excise_duty.Text), 2);
                itemTransaction.TaxInformation.ServiceTax = Math.Round(Convert.ToDecimal(txt_service_tax.Text), 2);
                itemTransaction.TaxInformation.VAT = Math.Round(Convert.ToDecimal(txt_vat.Text), 2);
                itemTransaction.TaxInformation.CSTWithCForm = Math.Round(Convert.ToDecimal(txt_cst_with_c_form.Text), 2);
                itemTransaction.TaxInformation.CSTWithoutCForm = Math.Round(Convert.ToDecimal(txt_cst_without_c_form.Text), 2);
                itemTransaction.TaxInformation.PercentageQuty = Math.Round(Convert.ToDecimal(txt_Percent_Dis.Text), 2);
                if (itemTransaction.UnitLeft < itemTransaction.ItemReceivedInvoice)
                {
                    basePage.Alert("Receive quantity should be less than item left", btnAddInvoice);
                    txtRecieveQuantity.Focus();
                    break;
                }
                itemTransaction.TotalAmount = Convert.ToDecimal(TotalAmount.Text);
                itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
                itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdnfldSupplierPOMappingId.Value);
                lstItemTransaction.Add(itemTransaction);

            }
            if (lstItemTransaction.Count != 0)
            {

                gvInvoice.DataSource = lstItemTransaction;
                gvInvoice.DataBind();
                Enabled(true);
            }
            else
            {
                gvInvoice.DataSource = new List<object>();
                gvInvoice.DataBind();
            }

            btnSaveDraft.Visible = true;
            btnReset.Visible = true;
        }

        protected void gvAddItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            String s = string.Empty;
            s = lstItemTransaction[index].Item.ItemName;
            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                foreach (GridViewRow row in gvSupplierAdd.Rows)
                {
                    CheckBox chkbxQuotationDetails = (CheckBox)row.FindControl("chkSelect");
                    Label lblItem = (Label)row.FindControl("lblItem");
                    if (Request.QueryString["invoiceId"] != null)
                    {
                        HiddenField hdfId = (HiddenField)row.FindControl("hdnfldSupplierPOMappingId");
                        int SupMapId = Convert.ToInt32(hdfId.Value);
                        supplierInvoiceBL = new SupplierInvoiceBL();
                        supplierInvoiceBL.DeleteSupplierInvoiceMapping(SupMapId);
                        BindgvSupplierAdd(lstInvoiceDom);
                        btnAddSupplierItem.Visible = true;

                    }
                    if (lblItem.Text.Trim() == s)
                    {
                        chkbxQuotationDetails.Enabled = true;
                        chkbxQuotationDetails.Checked = false;
                        flag = true;
                        break;
                    }
                }
                gvAddItems.DataSource = lstItemTransaction;
                gvAddItems.DataBind();
            }
            gvInvoice.DataSource = new List<Object>();
            gvInvoice.DataBind();
        }

        protected void rbtnInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnInvoiceType.SelectedValue == Convert.ToInt32(InvoiceType.Advance).ToString())
            {
                gvSupplierAdd.Enabled = false;
                btnAddSupplierItem.Visible = false;
                btnAddInvoice.Visible = false;
                btnSaveDraft.Visible = false;
                btnAdvance.Visible = true;
                btnReset.Visible = false;
                excise.Visible = true;
                excise1.Visible = true;
                excise2.Visible = true;
                excise3.Visible = true;
                excies4.Visible = true;
                btnReset.Visible = true;

                Reset();
            }
            else
            {
                gvSupplierAdd.Enabled = true;
                btnAddSupplierItem.Visible = true;
                //btnAddInvoice.Visible = true;
                //btnSaveDraft.Visible = true;
                btnAdvance.Visible = false;
                excise.Visible = false;
                excise1.Visible = false;
                excise2.Visible = false;
                excise3.Visible = false;
                excies4.Visible = false;
                Reset();
            }

        }

        protected void gvAdvanceSave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                priceTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxInformation.TotalNetValue"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = priceTotal.ToString();
                TotalAmount = priceTotal + TryToParse(txt_Freight.Text) + TryToParse(txt_Packaging.Text);
                ViewState["InvoiceAmount"] = TotalAmount;
            }
        }

        protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                priceTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = priceTotal.ToString();
                TotalAmount = priceTotal + TryToParse(txt_Freight.Text) + TryToParse(txt_Packaging.Text);
                ViewState["InvoiceAmount"] = TotalAmount;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells.RemoveAt(0);
                e.Row.Cells.RemoveAt(1);
                e.Row.Cells.RemoveAt(2);
                e.Row.Cells.RemoveAt(3);
                e.Row.Cells.RemoveAt(4);
                e.Row.Cells.RemoveAt(5);
                e.Row.Cells[0].ColumnSpan = 5;
            }
        }

        protected void gvSupplierAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Decimal UnitLeft = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ItemLeft = (Label)e.Row.FindControl("lblItemLeft");
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                if (Convert.ToDecimal(ItemLeft.Text) == 0)
                {
                    chkSelect.Enabled = false;
                }
            }
        }

        protected void gvAdvanceSave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                gvAdvanceSave.DataSource = lstItemTransaction;
                gvAdvanceSave.DataBind();
                gvAdvanceSave.Visible = false;
                btnSaveDraft.Visible = false;
                btnReset.Visible = false;
            }
        }

        protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                gvInvoice.DataSource = lstItemTransaction;
                gvInvoice.DataBind();
                //gvInvoice.Visible = false;
                btnSaveDraft.Visible = false;
                btnReset.Visible = false;
            }
        }

        // popup search SPONRecive
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string supplierName = txtName.Text;
            List<MetaData> lstSupplierId = new List<MetaData>();
            DateTime fromDate;
            DateTime toDate;
            lstSupplierId = SupplierReceiveMatBal.GetSupplierName(supplierName);
            int SupplierId = lstSupplierId.FirstOrDefault().Id;
            if (txtFromDate.Text == "")
            {
                fromDate = DateTime.MinValue;
            }
            else
            {
                fromDate = Convert.ToDateTime(txtFromDate.Text);
            }
            if (txtToDate.Text == "")
            {
                toDate = DateTime.MinValue;
            }
            else
            {
                toDate = Convert.ToDateTime(txtToDate.Text);
            }
            
            BindGrid(null, null, null, toDate, fromDate, supplierName);
            Reset();
            ModalPopupExtender2.Show();
        }
        private void BindGrid(String SupplierPONumber, String ChallanNo, String SupplierReceiveMaterialNo, DateTime ToDate, DateTime FromDate,string name)
        {
            //lstQuotation = new List<QuotationDOM>();
            lstSupplierRecieveMatarial = new List<SupplierRecieveMatarial>();
            lstSupplierRecieveMatarial = SupplierReceiveMatBal.SearchReceiveMaterial(SupplierPONumber, ChallanNo, SupplierReceiveMaterialNo, ToDate, FromDate, name);
            if (lstSupplierRecieveMatarial.Count > 0)
            {
                //Query for the Take the data Generated Typr
                var lst = lstSupplierRecieveMatarial.Where(e => e.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)));
                gvRSM.DataSource = lst;
                gvRSM.DataBind();
            }
            else
            {
                BindEmptyGrid(gvRSM);
            }
        }

        protected void rbtSelect_OncheckChanged(object sender, System.EventArgs e)
        {
            foreach (GridViewRow oldRow in gvRSM.Rows)
            {
                ((RadioButton)oldRow.FindControl("rbtSelect")).Checked = false;
            }
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtSelect")).Checked = true;
            Label SupplierPONumber = (Label)row.FindControl("lblSRMNo");
            txtSupplierPONumber.Text = SupplierPONumber.Text.ToString();
        }

        #endregion

        #region private methods

        private void BindRadioButton()
        {
            rbtnInvoiceType.DataSource = metadataBL.ReadMetaDataInvoiceType();
            rbtnInvoiceType.DataValueField = "Id";
            rbtnInvoiceType.DataTextField = "Name";
            rbtnInvoiceType.DataBind();
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        //changed by shruti//
        private void BindgvSupplierAdd()
        {
            List<ItemTransaction> lstItem = new List<ItemTransaction>();

            foreach (var item in lstItemTransaction)
            {
                if (item.QuantityReceived > 0)
                {
                    lstItem.Add(item);
                }
            }
            if (lstItem.Count > 0)
            {
                gvSupplierAdd.DataSource = lstItem;
            }
            else
            {
                gvSupplierAdd.DataSource = null;
            }
            gvSupplierAdd.DataBind();
        }

       

        private void Reset()
        {
            txt_cst_with_c_form.Text = string.Empty;
            txt_cst_without_c_form.Text = string.Empty;
            txt_Freight.Text = string.Empty;
            txt_Packaging.Text = string.Empty;
            txt_service_tax.Text = string.Empty;
            txt_vat.Text = string.Empty;
            txtAdvance.Text = string.Empty;
            txtExciseDuty.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtBillDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtBillNumber.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            EmptyDocumentList();
        }

        private void EditData(Int32 InvoiceId)
        {
            lstInvoiceDom = new List<InvoiceDom>();
            supplierInvoiceBL = new SupplierInvoiceBL();

            lstInvoiceDom = supplierInvoiceBL.ReadSupplierInvoice(InvoiceId, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, null);
            lstMetaData = supplierInvoiceBL.ReadSupplierPaymentTerm(null, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationNumber.ToString());
            lstItemTransaction = supplierInvoiceBL.ReadSupplierInvoiceMapping(InvoiceId);
            if (lstInvoiceDom.Count > 0)
            {
                BindUpdateText(lstInvoiceDom);
                BindGridPaymentType(lstInvoiceDom);
            }
            gvAddItems.DataSource = lstItemTransaction;
            gvAddItems.DataBind();
            BindgvSupplierAdd(lstInvoiceDom);
            btnAddSupplierItem.Visible = true;
            pnlSearch.Visible = false;
            Panel1.Visible = true;
            Enabled(true);
        }

        private void BindgvSupplierAdd(List<InvoiceDom> lst)
        {
            supplierInvoiceBL = new SupplierInvoiceBL();
            List<ItemTransaction> lstPreItemTransaction = new List<ItemTransaction>();
            if (lst.Count > 0)
            {
                lstPreItemTransaction = supplierInvoiceBL.ReadSupplierPOReceiveMaterialMapping(null, lst[0].ReceiveMaterial.Quotation.SupplierQuotationNumber);
                gvSupplierAdd.DataSource = lstPreItemTransaction;
                gvSupplierAdd.DataBind();
            }
        }

        private void BindUpdateText(List<InvoiceDom> lstInvoiceDom)
        {
            lblSupplierPurchaseOrderNumber.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationNumber.ToString();
            hdnSupplierPOId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationId.ToString();
            lblSupplierName.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierName.ToString();
            hdnSupplierId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierId.ToString();
            lblTotalNetValue.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.TotalNetValue.ToString();
            lblInvoicedAmount.Text = (lstInvoiceDom[0].InvoicedAmount - lstInvoiceDom[0].PayableAmount).ToString();
            lblLeftAmount.Text = (Convert.ToDecimal(lblTotalNetValue.Text) - Convert.ToDecimal(lblInvoicedAmount.Text)).ToString();
            lblPayableAmount.Text = lstInvoiceDom[0].PayableAmount.ToString();
            if (Convert.ToDecimal(lblPayableAmount.Text) > 0)
            {
                imgPayableAmt.Visible = true;
            }
            txt_Freight.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.Freight.ToString();
            txt_Packaging.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.Packaging.ToString();
            hdnPaymentTermId.Value = lstInvoiceDom[0].Payment.PaymentTermId.ToString();
            //lblTotalNetValue.Text = TotalNetValue.ToString();
            txtInvoiceDate.Text = lstInvoiceDom[0].InvoiceDate.ToString("dd-MMM-yyyy");
            txtBillDate.Text = lstInvoiceDom[0].InvoiceDate.ToString("dd-MMM-yyyy");
            txtBillNumber.Text = lstInvoiceDom[0].BillNumber.ToString();
            GetDocumentData(lstInvoiceDom[0].ReceiveMaterial.Quotation.UploadDocumentId);
            txtRemarks.Text = lstInvoiceDom[0].Remarks.ToString();
            hdnStatusTypeId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.StatusType.Id.ToString();
        }

        private void Enabled(Boolean Condition)
        {
            btnSaveDraft.Visible = Condition;
            btnReset.Visible = Condition;
        }

        private void BindGridPaymentType(List<InvoiceDom> lstInvoice_Dom)
        {
            paymentTermBL = new PaymentTermBL();
            lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(lstInvoice_Dom[0].ReceiveMaterial.Quotation.SupplierQuotationId, Convert.ToInt16(QuotationType.Supplier));
            gvPaymentType.DataSource = lstPaymentTerm;
            gvPaymentType.DataBind();
        }

        private MetaData CreateSupplierInvoice(InvoiceDom invoiceDom, Int32? InvoiceId)
        {
            if (lstItemTransaction != null)
            {
                metaData = new MetaData();
                supplierInvoiceBL = new SupplierInvoiceBL();
                metaData = supplierInvoiceBL.CreateSupplierInvoice(invoiceDom, InvoiceId);
            }
            return metaData;
        }

        private InvoiceDom GetSupplierInvoiceDetails()
        {
            invoiceDom = new InvoiceDom();
            invoiceDom.Payment = new PaymentTerm();
            invoiceDom.Payment.PaymentType = new MetaData();
            invoiceDom.ReceiveMaterial = new SupplierRecieveMatarial();
            invoiceDom.ReceiveMaterial.Quotation = new QuotationDOM();
            invoiceDom.ReceiveMaterial.Quotation.StatusType = new MetaData();
            invoiceDom.InvoiceType = new MetaData();
            if (InvoiceId > 0)
            {

                invoiceDom.SupplierInvoiceId = InvoiceId;
            }
            invoiceDom.InvoiceType.Id = Convert.ToInt32(rbtnInvoiceType.SelectedValue);
            invoiceDom.ReceiveMaterial.Quotation.SupplierQuotationNumber = lblSupplierPurchaseOrderNumber.Text.ToString();
            invoiceDom.ReceiveMaterial.Quotation.SupplierQuotationId = Convert.ToInt32(hdnSupplierPOId.Value);

            invoiceDom.ReceiveMaterial.Quotation.SupplierId = Convert.ToInt32(hdnSupplierId.Value);
            invoiceDom.ReceiveMaterial.Quotation.SupplierName = lblSupplierName.Text.Trim();
            invoiceDom.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text.Trim());
            invoiceDom.BillDate = Convert.ToDateTime(txtBillDate.Text.Trim());
            invoiceDom.BillNumber = txtBillNumber.Text.Trim();
            invoiceDom.Remarks = txtRemarks.Text.Trim();
            invoiceDom.ReceiveMaterial.Quotation.UploadDocumentId = basePage.DocumentStackId;

            invoiceDom.ReceiveMaterial.Quotation.TotalNetValue = Convert.ToDecimal(lblTotalNetValue.Text.Trim());


            if (rbtnInvoiceType.SelectedValue == Convert.ToInt32(InvoiceType.Advance).ToString())
            {
                invoiceDom.InvoicedAmount = Convert.ToDecimal(ViewState["InvoiceAmount"]);
            }
            else
            {
                invoiceDom.InvoicedAmount = Convert.ToDecimal(ViewState["InvoiceAmount"]);
            }

            invoiceDom.ReceiveMaterial.Quotation.Freight = TryToParse(txt_Freight.Text);
            invoiceDom.ReceiveMaterial.Quotation.Packaging = TryToParse(txt_Packaging.Text);
            invoiceDom.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
            invoiceDom.BillDate = Convert.ToDateTime(txtBillDate.Text.Trim());
            invoiceDom.BillNumber = txtBillNumber.Text.Trim();

            invoiceDom.ReceiveMaterial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);

            invoiceDom.CreatedBy = basePage.LoggedInUser.UserLoginId;
            foreach (GridViewRow row in gvPaymentType.Rows)
            {
                rdbtn = (RadioButton)row.FindControl("rdbtn");
                //lblPercenatageValue = (Label)row.FindControl("lblPercenatageValue");

                txtPercenatageValue = (TextBox)row.FindControl("txtPercenatageValue");
                hdfPaymentTermId = (HiddenField)row.FindControl("hdfPaymentTermId");
                hdfPaymentTypeId = (HiddenField)row.FindControl("hdfPaymentTypeId");
                if (rdbtn.Checked == true)
                {
                    //invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(lblPercenatageValue.Text))) / 100;
                    invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(txtPercenatageValue.Text))) / 100;
                    lblPayableAmount.Text = invoiceDom.PayableAmount.ToString("F");
                    //invoiceDom.Payment.PercentageValue = Convert.ToDecimal(lblPercenatageValue.Text);

                    invoiceDom.Payment.PercentageValue = Convert.ToDecimal(txtPercenatageValue.Text);
                    invoiceDom.Payment.PaymentTermId = Convert.ToInt32(hdfPaymentTermId.Value);
                    invoiceDom.Payment.PaymentType.Id = Convert.ToInt32(hdfPaymentTypeId.Value);
                    break;
                }
            }

            foreach (GridViewRow row in gvAddItems.Rows)
            {
                dec = 0;
                cnt = 0;
                BigNo = 0;
                BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].UnitForBilled);
                TextBox txtRecieveQuantity = (TextBox)row.FindControl("txtRecieveQuantity");
                //lstItemTransaction[i].UnitForBilled = Convert.ToDecimal(txtRecieveQuantity.Text);
                lblIndex = (Label)row.FindControl("lblIndex");
                lstItemTransaction[i].BilledUnit = Convert.ToDecimal(txtRecieveQuantity.Text);
                dec = TryToParse(txtRecieveQuantity.Text);
                if (dec > 0)
                {
                    cnt = NumberDecimalPlaces(dec);
                    if (InvoiceId > 0)
                    {
                        if (cnt > 3 || dec > BigNo)
                        {
                            if (j > 0)
                            {
                                strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                                j++;
                            }
                            else
                            {
                                strInvalid = strInvalid + lblIndex.Text.Trim();
                                j++;
                            }
                        }
                    }
                    else if (Convert.ToDecimal(txtRecieveQuantity.Text) > lstItemTransaction[i].UnitLeft || cnt > 3)
                    {
                        if (j > 0)
                        {
                            strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                            j++;
                        }
                        else
                        {
                            strInvalid = strInvalid + lblIndex.Text.Trim();
                            j++;
                        }
                    }
                }
                else
                {
                    if (j > 0)
                    {
                        strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                        j++;
                    }
                    else
                    {
                        strInvalid = strInvalid + lblIndex.Text.Trim();
                        j++;
                    }
                }
                if (string.IsNullOrEmpty(strInvalid))
                {
                    lstItemTransaction[i].UnitForBilled = Convert.ToDecimal(txtRecieveQuantity.Text);
                }
                i++;
            }
            if (!string.IsNullOrEmpty(strInvalid))
            {
                strInvalid = "Unit For Billed allows only valid numeric value <= Unit Left at S.No: " + strInvalid;
            }
            else
            {
                invoiceDom.ReceiveMaterial.Quotation.ItemTransaction = lstItemTransaction;
            }
            return invoiceDom;
        }

        private Decimal TryToParse(string Value)
        {
            dec = 0;
            Decimal.TryParse(Value, out dec);
            return dec;
        }

        private int NumberDecimalPlaces(Decimal dec)
        {
            string testdec = Convert.ToString(dec);
            int s = (testdec.IndexOf(".") + 1); // the first numbers plus decimal point 
            if (s > 0)
            {
                return ((testdec.Length) - s);   //total length minus beginning numbers and decimal = number of decimal points 
            }
            else
            {
                return 0;
            }
        }

        private void SetAllDefaultData(List<InvoiceDom> lstInvoice)
        {
            BindText(lstInvoiceDom);
            BindGridPaymentType(lstInvoice);
        }

        private void BindText(List<InvoiceDom> lstInvoiceDom)
        {
            lblSupplierPurchaseOrderNumber.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationNumber.ToString();
            hdnSupplierPOId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationId.ToString();
            lblSupplierName.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierName.ToString();
            hdnSupplierId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierId.ToString();
            hdnQutnGenDate.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.GeneratedDate.ToString();
            TotalNetValue = lstInvoiceDom[0].ReceiveMaterial.Quotation.TotalNetValue;
            lblInvoicedAmount.Text = lstInvoiceDom[0].InvoicedAmount.ToString();
            lblLeftAmount.Text = lstInvoiceDom[0].LeftAmount.ToString();
            freight = lstInvoiceDom[0].ReceiveMaterial.Quotation.Freight;
            packaging = lstInvoiceDom[0].ReceiveMaterial.Quotation.Packaging;
            lblTotalNetValue.Text = TotalNetValue.ToString();
            txt_Freight.Text = freight.ToString();
            txt_Packaging.Text = packaging.ToString();
        }

        #endregion

        #region Public Property

        private List<PaymentTerm> lstPaymentTerm
        {
            get
            {
                return (List<PaymentTerm>)ViewState["lstPaymentTerm"];
            }
            set
            {
                ViewState["lstPaymentTerm"] = value;
            }
        }

        private List<ItemTransaction> lstItemTransaction
        {
            get
            {
                return (List<ItemTransaction>)ViewState["lstItemTransaction"];
            }
            set
            {
                ViewState["lstItemTransaction"] = value;
            }

        }

        private List<InvoiceDom> lstInvoiceDom
        {
            get
            {
                return (List<InvoiceDom>)ViewState["lstInvoiceDom"];
            }
            set
            {
                ViewState["lstInvoiceDom"] = value;
            }
        }

        private List<MetaData> lstMetaData
        {
            get
            {
                return (List<MetaData>)ViewState["lstMetaData"];
            }
            set
            {
                ViewState["lstMetaData"] = value;
            }
        }

        private int InvoiceId
        {
            get
            {
                return (Int32)ViewState["InvoiceId"];
            }
            set
            {
                ViewState["InvoiceId"] = value;
            }
        }

        private string pageName
        {
            get
            {
                return (String)ViewState["pageName"];
            }
            set
            {
                ViewState["pageName"] = value;
            }
        }

        public String SupplierPurchseOrderNumber
        {
            get
            {
                if (ViewState["SPON"] != null)
                {
                    return (String)ViewState["SPON"];
                }
                else
                    return String.Empty;
            }
            set
            {
                ViewState["SPON"] = value;
            }
        }

        #endregion

        #region Upload Document Code

        #region Private Global Variable(s)

        DocumentBL document_BL = new DocumentBL();

        Document document = null;
        Int32 Year = 0;
        Int32 Index = 0;

        String Head_Folder_Path = String.Empty;
        String Sub_Folder_Path = String.Empty;
        String File_Extension = String.Empty;
        String File_Path = String.Empty;

        DataSet page_Data = null;

        List<Document> lst_document = null;
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

            if (basePage.DocumentStackId == 0)
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
                lst_document = new List<Document>();
                lst_document = basePage.DocumentsList;
                lst_document.RemoveAt(Index);
                basePage.DocumentsList = lst_document;
                BindDocument();
            }
            else if (e.CommandName == "OpenFile")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                Index = Convert.ToInt32(e.CommandArgument);
                lst_document = new List<Document>();
                lst_document = basePage.DocumentsList;

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtn_file");

                string fileName = lst_document[Index].Replaced_Name;
                string strURL = lst_document[Index].Path + @"\" + lst_document[Index].Replaced_Name;

                Session["FilePath"] = Server.MapPath(strURL);
                Session["OriginalFileName"] = lst_document[Index].Original_Name;
                Session["ReplacedFileName"] = lst_document[Index].Replaced_Name;
                basePage.OpenPopupWithUpdatePanelForFileDownload(lbtn, "../Parts/FileDownload.aspx?id=" + "File", "DownloadFile");
            }
        }

        #endregion

        #region Private Methods

        private void ManageSession(bool forCopy)
        {
            RequestPageName = pageName;
            if (forCopy)
            {
                basePage.DocumentStackId = 0;
            }
            else if (basePage.Page_Name == null || basePage.Page_Name != RequestPageName)
            {
                basePage.Page_Name = RequestPageName;
                basePage.DocumentStackId = 0;
                basePage.DocumentSerial = 0;
                basePage.DocumentsList = null;
            }
            else
            {
                //GO AHEAD
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Int32 CreateAndReadDocumentStackId()
        {
            document = new Document();
            document.CreatedBy = basePage.LoggedInUser.UserLoginId;
            basePage.DocumentStackId = document_BL.CreateAndReadDocumnetStackId(document);
            return basePage.DocumentStackId;
        }

        private void DirectoryHandle(FileUpload fileupload)
        {
            if (fileupload.HasFile)
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
                    if (basePage.DocumentStackId != 0)
                    {
                        document = new Document();
                        lst_document = new List<Document>();
                        flag = false;

                        document.Original_Name = fileupload.FileName.Split('\\').Last();
                        if (basePage.DocumentsList != null)
                        {
                            foreach (Document item in basePage.DocumentsList)
                            {
                                if (item.Original_Name == document.Original_Name)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        if (flag == true)
                        {
                            basePage.Alert(GlobalConstants.M_File_Exist, FileUpload_Control);
                        }
                        else
                        {
                            basePage.DocumentSerial = basePage.DocumentSerial + 1;

                            File_Extension = System.IO.Path.GetExtension(fileupload.FileName.Split('\\').Last());
                            document.Replaced_Name = Convert.ToString(basePage.DocumentStackId) + "_" + Convert.ToString(basePage.DocumentSerial) + File_Extension;

                            File_Path = Sub_Folder_Path + @"\" + document.Replaced_Name;
                            //File_Path = Sub_Folder_Path + @"\" + document.Original_Name;
                            //Upload file in respective path
                            FileUpload_Control.SaveAs(File_Path);

                            document.DocumentId = basePage.DocumentStackId;

                            document.Path = ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString() + @"\" + RequestPageName;
                            document.LastIndex = basePage.DocumentSerial;


                            if (basePage.DocumentsList == null)
                            {
                                lst_document.Add(document);
                            }
                            else
                            {
                                lst_document = basePage.DocumentsList;
                                lst_document.Add(document);
                            }

                            //Add Doc's info in list
                            basePage.DocumentsList = lst_document;
                        }
                    }
                }
            }
        }

        public void BindDocument()
        {
            if (basePage.DocumentsList != null)
            {
                gv_documents.DataSource = basePage.DocumentsList;
            }
            else
            {
                gv_documents.DataSource = null;
            }
            gv_documents.DataBind();
        }

        private void CreateDocumentMapping()
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = basePage.DocumentsList;

            if (lst_document != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                document_BL.ResetDocumentMapping(Convert.ToInt32(basePage.DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document doc in lst_document)
                {
                    document = new Document();
                    document.DocumentId = Convert.ToInt32(basePage.DocumentStackId);
                    document.Original_Name = doc.Original_Name;
                    document.Replaced_Name = doc.Replaced_Name;
                    document.Path = doc.Path;
                    //DocumentSerial is the last updated document
                    document.LastIndex = basePage.DocumentSerial;
                    document.CreatedBy = basePage.LoggedInUser.UserLoginId;
                    document.Id = doc.Id;
                    document_BL.CreateDocumentMapping(document);
                }
            }
        }

        private void GetDocumentData(Int32 Document_Id)
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = document_BL.ReadDocumnetMapping(Document_Id);
            if (lst_document.Count >= 1)
            {
                basePage.DocumentsList = lst_document;
                basePage.DocumentStackId = lst_document[0].DocumentId;
                basePage.DocumentSerial = lst_document[0].LastIndex;
                basePage.Page_Name = pageName;
                BindDocument();
            }
        }

        public void EmptyDocumentList()
        {
            basePage.DocumentStackId = 0;
            basePage.DocumentSerial = 0;
            basePage.DocumentsList = null;
            BindDocument();
        }
        //private Decimal TotalAmount
        //{
        //    get
        //    {
        //        if (ViewState["TotalAmount"] == null)
        //            return 0;
        //        else
        //            return (Decimal)ViewState["TotalAmount"];
        //    }
        //    set
        //    {
        //        ViewState["TotalAmount"] = value;
        //    }
        //}
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