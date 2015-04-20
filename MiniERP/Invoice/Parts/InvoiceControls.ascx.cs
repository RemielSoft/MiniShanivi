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
using System.Web.Services;
using System.Web.Script.Services;

namespace MiniERP.Invoice.Parts
{
    public partial class InvoiceControls : System.Web.UI.UserControl
    {

        #region Private global variables

        ItemTransaction itemTransaction = null;
        QuotationDOM quotationDOM = null;
        IssueMaterialBL issueMaterialBL = new IssueMaterialBL();
        PaymentTermBL paymentTermBL = null;
        List<IssueMaterialDOM> lstIssueMaterialDOM = null;
        InvoiceDom invoiceDom = null;
        MetaData metaData = null;
        MetaDataBL metadataBL = new MetaDataBL();
        BasePage basePage = new BasePage();
        ContractorInvoiceBL contractorInvoiceBL = null;
        Decimal TotalNetValue = 0;
        decimal priceTotal = 0;
        int i = 0;
        int j = 0;
        string strInvalid = string.Empty;
        decimal dec = 0;
        int cnt = 0;
        int index = 0;
        String s = string.Empty;
        decimal BigNo = 0;
        Boolean flag = false;
        Boolean track = false;
        Decimal TotalAmount = 0;
        RadioButton rdbtn = null;
        Label lblPercenatageValue = null;
        HiddenField hdfPaymentTermId = null;
        HiddenField hdfPaymentTypeId = null;
        Label lblNoOfDays = null;
        DateTime dateAfterDays;
        String pageName = String.Empty;

        #endregion

        #region protected methods

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                BindRadioButton();
                rbtnInvoiceType.SelectedValue = Convert.ToInt32(InvoiceType.Normal).ToString();
                btnAddInvoice.Visible = false;
                imgPayableAmt.Visible = false;
                txtInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtBillDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                if (Request.QueryString["InvoiceId"] != null)
                {
                    InvoiceId = 0;
                    InvoiceId = Convert.ToInt32(Request.QueryString["InvoiceId"]);
                    EditData(InvoiceId);
                }
                else
                {
                    InvoiceId = 0;
                    Panel1.Visible = false;
                    Enabled(false);
                    txtContractorWorkOrderNo.Focus();
                }

                if (rbtnInvoiceType.SelectedValue == Convert.ToInt32(InvoiceType.Advance).ToString())
                {
                    gvContractorAddItem.Enabled = false;
                    btnContractorAdd.Visible = false;
                    btnAddInvoice.Visible = false;
                    btnSaveDraft.Visible = false;
                    gvAddContractorItem.Visible = false;
                    gvContractorInvoice.Visible = false;
                    gvContractorAdvanceSave.Visible = false;
                    btnAdvance.Visible = true;
                    btnReset.Visible = false;
                    excise1.Visible = true;
                    excise2.Visible = true;
                    excise3.Visible = true;
                    Reset();
                }
                else
                {
                    gvContractorAddItem.Enabled = true;
                    btnContractorAdd.Visible = true;
                    excise1.Visible = false;
                    excise2.Visible = false;
                    excise3.Visible = false;
                    gvContractorAdvanceSave.Visible = false;
                    btnAdvance.Visible = true;
                    Reset();
                }
            }
        }

        protected void imgbtn_search_Click(object sender, EventArgs e)
        {
            if (ContractorWorkOrderNumber != String.Empty)
            {
                ViewState["InvoiceAdded"] = null;
                ViewState["AdvanceInvoice"] = null;
                lstItemTransaction = null;
                gvAddContractorItem.DataSource = lstItemTransaction;
                gvAddContractorItem.DataBind();
                gvContractorInvoice.DataSource = lstItemTransaction;
                gvContractorInvoice.DataBind();
                LoadDefaultData();
                LoadAllData();
            }
            else
            {
                if (string.IsNullOrEmpty(txtContractorWorkOrderNo.Text.Trim()))
                {
                    basePage.Alert("Please enter Contractor Purchase Order Number!", imgbtn_search);
                }
                else
                {
                    LoadAllData();
                }
            }
        }

        protected void btnContractorAdd_Click(object sender, EventArgs e)
        {
            basePage = new BasePage();
            if (ViewState["AddItem"] == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (GridViewRow row in gvContractorAddItem.Rows)
            {
                CheckBox chkbxQuotationDetails = (CheckBox)row.FindControl("chkSelect");
                HiddenField hdfActivityId = (HiddenField)row.FindControl("hdfActivityId");
                Label lblActivityDiscription = (Label)row.FindControl("lblActivityDescription");
                Label lblItem = (Label)row.FindControl("lblItem");
                Label lblSpecification = (Label)row.FindControl("lblSpecification");
                Label lblItemCategory = (Label)row.FindControl("lblItemCategory");
                Label lblBrand = (Label)row.FindControl("lblMake");
                Label lblMeasurement = (Label)row.FindControl("lblMeasurement");
                Label lblPerUnitCost = (Label)row.FindControl("lblPerUnitCost");
                Label lblPerUnitDiscount = (Label)row.FindControl("lblPerunitDiscount");
                Label lblDiscountRate = (Label)row.FindControl("lblDiscountRate");
                // Label lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
                Label lblIssuedItem = (Label)row.FindControl("lblIssuedItem");
                Label lblItemLeft = (Label)row.FindControl("lblItemLeft");
                Label lblInvoicedItem = (Label)row.FindControl("lblInvoicedItem");
                Label lblRecieveQuantity = (Label)row.FindControl("lblRecieveQuantity");
                //used invoice recieve
                Decimal lblItemReceivedAuto = Convert.ToDecimal(lblIssuedItem.Text) - (Convert.ToDecimal(lblInvoicedItem.Text));


                if (chkbxQuotationDetails.Checked == true && hdfActivityId != null)
                {
                    if (chkbxQuotationDetails.Checked.Equals(true))
                    {
                        itemTransaction = new ItemTransaction();
                        itemTransaction.MetaProperty = new MetaData();
                        itemTransaction.TaxInformation = new Tax();
                        itemTransaction.TaxInformation.DiscountMode = new MetaData();
                        itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                        itemTransaction.Item = new Item();
                        itemTransaction.Item.ModelSpecification = new ModelSpecification();
                        itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
                        itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                        itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfActivityId.Value);
                        itemTransaction.DeliverySchedule.ActivityDescription = lblActivityDiscription.Text.ToString();
                        itemTransaction.Item.ItemName = lblItem.Text.ToString();
                        itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                        itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Brand.BrandName = lblBrand.Text.ToString();

                        itemTransaction.QuantityReceived = lblItemReceivedAuto;
                        // itemTransaction.NumberOfUnit = TryToParse(lblItemQuantity.Text.ToString());
                        itemTransaction.PerUnitCost = TryToParse(lblPerUnitCost.Text.ToString());
                        itemTransaction.PerUnitDiscount = TryToParse(lblPerUnitDiscount.Text.ToString());
                        itemTransaction.Discount_Rates = TryToParse(lblDiscountRate.Text.ToString());

                        //itemTransaction.UnitLeft = TryToParse(lblItemLeft.Text.ToString());
                        itemTransaction.UnitLeft = TryToParse(lblIssuedItem.Text.ToString());
                        itemTransaction.UnitIssued = TryToParse(lblIssuedItem.Text.ToString());
                        itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
                        lstItemTransaction.Add(itemTransaction);
                        chkbxQuotationDetails.Checked = false;
                        chkbxQuotationDetails.Enabled = false;
                        ViewState["AddItem"] = "AddItemEnd";
                    }
                }

            }
            if (lstItemTransaction.Count != 0)
            {
                gvAddContractorItem.DataSource = lstItemTransaction;
                gvAddContractorItem.DataBind();
                Enabled(true);
            }
            else
            {
                Enabled(false);
                basePage.Alert("Please Check At Least One Activity Description", btnContractorAdd);
            }
            foreach (TableCell item in gvContractorAddItem.HeaderRow.Cells)
            {
                CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
                if (chbxSelectAll.Checked == true)
                {
                    chbxSelectAll.Checked = false;
                }
            }
            gvAddContractorItem.Visible = true;
            btnAddInvoice.Visible = true;
            btnSaveDraft.Visible = false;
            btnReset.Visible = false;
        }

        protected void btnAddInvoice_Click(object sender, EventArgs e)
        {
            basePage = new BasePage();
            Label TotalAmount = new Label();
            Label Totaltax = new Label();
            decimal amount = 0;
            decimal TotalTax = 0;
            decimal z = 0;
            decimal DicountRate = 0;
            decimal y = 0;


            lstItemTransaction = new List<ItemTransaction>();

            foreach (GridViewRow row in gvAddContractorItem.Rows)
            {
                Label lblActivityDiscription = (Label)row.FindControl("lblActivityDescription");
                Label lblItem = (Label)row.FindControl("lblItem");
                Label lblSpecification = (Label)row.FindControl("lblSpecification");
                Label lblItemCategory = (Label)row.FindControl("lblItemCategory");
                Label lblBrand = (Label)row.FindControl("lblMake");
                Label lblMeasurement = (Label)row.FindControl("lblMeasurement");
                Label lblPerUnitCost = (Label)row.FindControl("lblPerUnitCost");
                Label lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
                Label lblItemLeft = (Label)row.FindControl("lblItemLeft");
                Label lblInvoicedItem = (Label)row.FindControl("lblInvoicedItem");
                Label lblPerUnitdiscount = (Label)row.FindControl("lblPerUnitdiscount");

                TextBox txtRecieveQuantity = (TextBox)row.FindControl("txtRecieveQuantity");

                TextBox txt_service_tax = (TextBox)row.FindControl("txt_service_tax");
                TextBox txt_vat = (TextBox)row.FindControl("txt_vat");
                TextBox txt_cst_with_c_form = (TextBox)row.FindControl("txt_cst_with_c_form");
                TextBox txt_cst_without_c_form = (TextBox)row.FindControl("txt_cst_without_c_form");
                HiddenField hdfActivityId = (HiddenField)row.FindControl("hdfActivityId");

                y = (TryToParse(lblPerUnitCost.Text) * TryToParse(lblPerUnitdiscount.Text) / 100);

                DicountRate = Convert.ToDecimal(lblPerUnitCost.Text) - y;
                amount = TryToParse(txtRecieveQuantity.Text) * DicountRate;
                TotalTax = (TryToParse(txt_service_tax.Text) + TryToParse(txt_vat.Text) + TryToParse(txt_cst_with_c_form.Text) + TryToParse(txt_cst_without_c_form.Text)) * amount / 100;

                z = amount + TotalTax;
                Totaltax.Text = TotalTax.ToString("F2");
                TotalAmount.Text = z.ToString("F2");

                itemTransaction = new ItemTransaction();
                itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                itemTransaction.MetaProperty = new MetaData();
                itemTransaction.TaxInformation = new Tax();
                itemTransaction.TaxInformation.DiscountMode = new MetaData();
                itemTransaction.Item = new Item();

                itemTransaction.Item.ModelSpecification = new ModelSpecification();
                itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
                itemTransaction.ItemReceivedInvoice = Math.Round(Convert.ToDecimal(txtRecieveQuantity.Text), 3);
                itemTransaction.Item.ItemName = lblItem.Text.ToString();

                itemTransaction.DeliverySchedule.ActivityDescription = lblActivityDiscription.Text.ToString();
                itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                itemTransaction.Item.ModelSpecification.Brand.BrandName = lblBrand.Text.ToString();
                itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString();
                itemTransaction.TaxInformation.ServiceTax = Math.Round(Convert.ToDecimal(txt_service_tax.Text), 2);
                itemTransaction.TaxInformation.VAT = Math.Round((Convert.ToDecimal(txt_vat.Text)), 2);
                itemTransaction.TaxInformation.CSTWithCForm = Math.Round(Convert.ToDecimal(txt_cst_with_c_form.Text), 2);
                itemTransaction.TaxInformation.CSTWithoutCForm = Math.Round(Convert.ToDecimal(txt_cst_without_c_form.Text), 2);
                itemTransaction.NumberOfUnit = TryToParse(lblItemQuantity.Text.ToString());
                itemTransaction.PerUnitCost = TryToParse(lblPerUnitCost.Text.ToString());
                itemTransaction.UnitLeft = TryToParse(lblItemLeft.Text.ToString());
                if (itemTransaction.UnitLeft < itemTransaction.ItemReceivedInvoice)
                {
                    basePage.Alert("Received quantity should be less than item left", btnAddInvoice);
                    txtRecieveQuantity.Focus();
                    break;
                }
                itemTransaction.UnitForBilled = Convert.ToDecimal(Totaltax.Text);
                itemTransaction.TotalAmount = Convert.ToDecimal(TotalAmount.Text);
                itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
                itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfActivityId.Value);
                lstItemTransaction.Add(itemTransaction);
                ViewState["AddItemInvoice"] = "AddItemInvoiceEnd";
            }
            if (lstItemTransaction.Count != 0)
            {
                gvContractorInvoice.DataSource = lstItemTransaction;
                gvContractorInvoice.DataBind();
                Enabled(true);
            }
            else
            {
                gvContractorInvoice.DataSource = new List<object>();
                gvContractorInvoice.DataBind();
            }

            btnSaveDraft.Visible = true;
            btnReset.Visible = true;
        }

        protected void TextLeave_Click(object sender, EventArgs e)
        {
            decimal amount = 0;
            foreach (GridViewRow row in gvAddContractorItem.Rows)
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

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            ltrl_err_msg.Text = string.Empty;
            metaData = new MetaData();
            invoiceDom = new InvoiceDom();
            invoiceDom = GetCotracrorInvoiceDetails();

            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            else if (InvoiceId > 0)
            {
                metaData = CreateContractorInvoice(invoiceDom, InvoiceId);
            }
            else
            {
                metaData = CreateContractorInvoice(invoiceDom, null);
            }
            if (metaData.Id > 0)
            {
                CreateDocumentMapping();
                if (InvoiceId > 0)
                {
                    basePage.Alert("Contractor Invoice No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewContractorInvoice.aspx");
                    Reset();
                }
                else
                {
                    basePage.Alert("Contractor Invoice No: " + metaData.Name + " Created Successfully", btnSaveDraft, "ContractorInvoice.aspx");
                    Reset();
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContractorInvoice.aspx");
            imgbtn_search_Click(null, null);
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
                else if (lstMetaData.Count != 0 && InvoiceId != 0)
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

        protected void chbxSelectAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvContractorAddItem.Rows)
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
            CheckBox chbxSelectAll = (CheckBox)gvContractorAddItem.HeaderRow.FindControl("chbxSelectAll");
            foreach (GridViewRow row in gvContractorAddItem.Rows)
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

        protected void rdbtn_Click(object sender, EventArgs e)
        {
            imgPayableAmt.Visible = false;
            invoiceDom = new InvoiceDom();
            foreach (GridViewRow row in gvPaymentType.Rows)
            {
                rdbtn = (RadioButton)row.FindControl("rdbtn");
                lblPercenatageValue = (Label)row.FindControl("lblPercenatageValue");
                if (rdbtn.Checked == true)
                {
                    invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(lblPercenatageValue.Text))) / 100;
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

        protected void rbtnInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnInvoiceType.SelectedValue == Convert.ToInt32(InvoiceType.Advance).ToString())
            {
                gvContractorAddItem.Enabled = false;
                btnContractorAdd.Visible = false;
                btnAddInvoice.Visible = false;
                btnSaveDraft.Visible = false;
                btnAdvance.Visible = true;
                btnReset.Visible = false;
                gvAddContractorItem.Visible = false;
                gvContractorInvoice.Visible = false;
                gvContractorAdvanceSave.Visible = false;
                excise1.Visible = true;
                excise2.Visible = true;
                excise3.Visible = true;
                btnReset.Visible = true;
                Reset();
            }
            else
            {
                gvContractorInvoice.Visible = true;
                gvContractorAddItem.Enabled = true;
                btnContractorAdd.Visible = true;
                btnAdvance.Visible = false;
                excise1.Visible = false;
                excise2.Visible = false;
                excise3.Visible = false;
                gvContractorAdvanceSave.DataSource = null;
                gvContractorAdvanceSave.DataBind();
                Reset();
            }
        }

        protected void btnAdvance_Click(object sender, EventArgs e)
        {
            basePage = new BasePage();
            Label TotalAmount = new Label();
            Label Totaltax = new Label();
            decimal amount = 0;
            decimal TotalTax = 0;
            decimal z = 0;
            if (ViewState["AdvanceInvoice"] == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            itemTransaction = new ItemTransaction();
            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.Freight = TryToParse(txt_Freight.Text.Trim());
            itemTransaction.TaxInformation.Packaging = TryToParse(txt_Packaging.Text.Trim());
            itemTransaction.AdvanceValue = TryToParse(txtAdvance.Text.Trim());
            itemTransaction.TaxInformation.ServiceTax = TryToParse(txt_service_tax.Text);
            itemTransaction.TaxInformation.VAT = TryToParse(txt_vat.Text);
            itemTransaction.TaxInformation.CSTWithCForm = TryToParse(txt_cst_with_c_form.Text);
            itemTransaction.TaxInformation.CSTWithoutCForm = TryToParse(txt_cst_without_c_form.Text);
            itemTransaction.TaxInformation.Freight = TryToParse(txt_Freight.Text);
            itemTransaction.TaxInformation.Packaging = TryToParse(txt_Packaging.Text);


            amount = (TryToParse(txtAdvance.Text) * TryToParse(lblTotalNetValue.Text)) / 100;
            TotalTax = ((itemTransaction.TaxInformation.ServiceTax + itemTransaction.TaxInformation.VAT + itemTransaction.TaxInformation.CSTWithCForm + itemTransaction.TaxInformation.CSTWithoutCForm) * amount) / 100;

            z = amount + TotalTax + itemTransaction.TaxInformation.Freight + itemTransaction.TaxInformation.Packaging;
            Totaltax.Text = TotalTax.ToString("F2");
            TotalAmount.Text = z.ToString("F2");

            itemTransaction.TaxInformation.TotalTax = Convert.ToDecimal(Totaltax.Text);
            itemTransaction.TaxInformation.TotalNetValue = Convert.ToDecimal(TotalAmount.Text);
            itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
            lstItemTransaction.Add(itemTransaction);
            ViewState["AdvanceInvoice"] = "AdvanceInvoiceEnd";

            if (lstItemTransaction.Count == 1)
            {
                gvContractorAdvanceSave.DataSource = lstItemTransaction;
                gvContractorAdvanceSave.DataBind();
                Enabled(true);
                gvContractorAdvanceSave.Visible = true;
                Reset();
            }

            else if (lstItemTransaction.Count >= 1)
            {
                basePage.Alert("Advanced amount has been already added.", btnAdvance);

            }
        }

        protected void gvAddContractorItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            String s = string.Empty;
            String y = string.Empty;
            s = lstItemTransaction[index].Item.ItemName;
            y = lstItemTransaction[index].DeliverySchedule.ActivityDescriptionId.ToString();

            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                foreach (GridViewRow row in gvContractorAddItem.Rows)
                {
                    CheckBox chkbxQuotationDetails = (CheckBox)row.FindControl("chkSelect");
                    Label lblItem = (Label)row.FindControl("lblItem");
                    Label lblActivityDescription = (Label)row.FindControl("lblActivityDescription");
                    HiddenField hdfContractorMappingId = (HiddenField)row.FindControl("hdfActivityId");
                    if (InvoiceId > 0)
                    {
                        int ContQuotMappId = Convert.ToInt32(hdfContractorMappingId.Value);
                        contractorInvoiceBL = new ContractorInvoiceBL();
                        contractorInvoiceBL.DeleteContractorInvoiceMapping(ContQuotMappId, InvoiceId);
                        BindgvContractorAddItem(lstInvoiceDom);
                        btnContractorAdd.Visible = true;
                    }
                    if (lblItem.Text != "")
                    {
                        if (lblActivityDescription.Text == y)
                        {
                            chkbxQuotationDetails.Enabled = true;
                            chkbxQuotationDetails.Checked = false;
                            flag = true;
                            break;
                        }
                    }
                    if (lblItem.Text.Trim() == s)
                    {
                        chkbxQuotationDetails.Enabled = true;
                        chkbxQuotationDetails.Checked = false;
                        flag = true;
                        break;

                    }
                }
                gvAddContractorItem.DataSource = lstItemTransaction;
                gvAddContractorItem.DataBind();
            }
        }
        protected void gvContractorInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }

        protected void gvContractorAdvanceSave_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvContractorAdvanceSave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                gvContractorAdvanceSave.DataSource = lstItemTransaction;
                gvContractorAdvanceSave.DataBind();
                gvContractorAdvanceSave.Visible = false;
                btnSaveDraft.Visible = false;
                btnReset.Visible = false;
            }
        }

        protected void gvContractorAddItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ItemLeftInvoice = (Label)e.Row.FindControl("lblItemLeft");
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                if (Convert.ToDecimal(ItemLeftInvoice.Text) == 0)
                {
                    chkSelect.Enabled = false;
                }
            }
        }

        protected void gvContractorInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                gvContractorInvoice.DataSource = lstItemTransaction;
                gvContractorInvoice.DataBind();
                btnSaveDraft.Visible = false;
                btnReset.Visible = false;
            }
        }
        [WebMethod]
        [ScriptMethod()]
        public static List<string> GetContractorName(string prefixText)
        {
            List<string> contractorName = new List<string>();
            //List<MetaData> contractorName = new List<MetaData>();
            //contractorName = issueMaterialBL.GetContractorName(prefixText);
            return contractorName;
        }

        // popup search Contractor Work Order Name
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string contractorName = txtContractorName.Text;
            List<MetaData> lstContractorId = new List<MetaData>();
            DateTime fromDate;
            DateTime toDate;
            int ContractorId;
            if (contractorName!="")
            {
                lstContractorId = issueMaterialBL.GetContractorName(contractorName);
                ContractorId = lstContractorId.FirstOrDefault().Id;
            }
            else
            {
                ContractorId = 0;
            }
           
            if (txtFromDate.Text=="")
            {
                fromDate = DateTime.MinValue;
            }
            else
            {
                fromDate=Convert.ToDateTime(txtFromDate.Text);
            }
            if (txtToDate.Text == "")
            {
                toDate = DateTime.MinValue;
            }
            else
            {
                toDate = Convert.ToDateTime(txtToDate.Text);
            }
            BindGridIsuue(null, ContractorId, toDate,fromDate, null, null);
            Reset();
            ModalPopupExtender2.Show();
        }

        protected void gvIssueMaterialNo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIssueMaterialNo.PageIndex = e.NewPageIndex;
            gvIssueMaterialNo.DataSource = lstIssueMaterialDOM;
            gvIssueMaterialNo.DataBind();
           

        }

        protected void rbtSelect_OncheckChanged(object sender, System.EventArgs e)
        {
            foreach (GridViewRow oldRow in gvIssueMaterialNo.Rows)
            {
                ((RadioButton)oldRow.FindControl("rbtSelect")).Checked = false;
            }
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtSelect")).Checked = true;
            Label WorkOrderNo = (Label)row.FindControl("lblContractorWONo");
            txtContractorWorkOrderNo.Text = WorkOrderNo.Text.ToString();
        }

        #endregion

        #region private methods

        //For Poupop Open search For Contractor Work Order Number
        private void BindGridIsuue(Int32? IssueMaterialId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String IssueMaterialNo)
        {
            lstIssueMaterialDOM = new List<IssueMaterialDOM>();
            lstIssueMaterialDOM = issueMaterialBL.ReadIssueMaterial(IssueMaterialId, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, null);
            if (lstIssueMaterialDOM.Count > 0)
            {
                // Query of the Take the data Generated Type
                var lst = lstIssueMaterialDOM.Where(e => e.DemandVoucher.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)));
                //var lst = lstIssueMaterialDOM.Where(e => e.DemandVoucher.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)) || e.DemandVoucher.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Close)));


                gvIssueMaterialNo.DataSource = lst;
                gvIssueMaterialNo.DataBind();
            }
            else
            {
                BindEmptyGrid(gvIssueMaterialNo);
            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void DefaultLoad()
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBack(btn_upload);
            scriptManager.RegisterAsyncPostBackControl(btn_upload);
        }

        private void LoadDefaultData()
        {
            btnContractorAdd.Visible = false;
            btnAddInvoice.Visible = false;
            btnSaveDraft.Visible = false;
            btnReset.Visible = false;
            //SPON.Visible = false;
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
                txtContractorWorkOrderNo.Focus();
            }
            if (rbtnInvoiceType.SelectedValue == Convert.ToInt32(InvoiceType.Advance).ToString())
            {
                gvContractorAddItem.Enabled = false;
                btnContractorAdd.Visible = false;
                btnAddInvoice.Visible = false;
                btnSaveDraft.Visible = false;
                btnAdvance.Visible = true;
                btnReset.Visible = false;

                excise1.Visible = true;
                excise2.Visible = true;
                excise3.Visible = true;
                excies4.Visible = true;
                Reset();
            }
            else
            {
                gvContractorAddItem.Enabled = true;
                btnContractorAdd.Visible = false;
                btnAdvance.Visible = false;
                // excise.Visible = false;
                excise1.Visible = false;
                excise2.Visible = false;
                excise3.Visible = false;
                excies4.Visible = false;
                Reset();
            }

        }

        private void LoadAllData()
        {
            contractorInvoiceBL = new ContractorInvoiceBL();
            lstItemTransaction = new List<ItemTransaction>();
            lstInvoiceDom = new List<InvoiceDom>();
            lstPaymentTerm = new List<PaymentTerm>();

            if (ContractorWorkOrderNumber == String.Empty)
            {
                ContractorWorkOrderNumber = txtContractorWorkOrderNo.Text.Trim().ToString();
            }
            lstInvoiceDom = contractorInvoiceBL.ReadContractorQuotation(null, ContractorWorkOrderNumber);
            lstMetaData = new List<MetaData>();
            lstMetaData = contractorInvoiceBL.ReadContractorPaymentTerm(null, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, ContractorWorkOrderNumber);
            if (lstInvoiceDom.Count > 0)
            {
                if (lstInvoiceDom[0].LeftAmount == 0)
                {
                    basePage.Alert(" All Payment terms have been completed for this Contractor Order Number!", imgbtn_search);
                }
                else
                {
                    if (lstInvoiceDom.Count > 0 && lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
                    {
                        SetAllDefaultData(lstInvoiceDom);
                        if (lstPaymentTerm.Count > 0)
                        {
                            Panel1.Visible = true;
                        }
                        lstItemTransaction = contractorInvoiceBL.ReadContractorWorkOrderMapping(null, ContractorWorkOrderNumber);
                        if (lstItemTransaction.Count > 0)
                        {
                            BindgvContractorAdd();
                            gvContractorAddItem.Visible = true;
                        }
                        txtContractorWorkOrderNo.Text = String.Empty;
                        btnContractorAdd.Visible = true;
                        //CPON.Visible = true;
                    }
                    //

                    else if (lstInvoiceDom.Count > 0 && lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.StatusType.Id != Convert.ToInt32(StatusType.Generated))
                    {
                        basePage.Alert("Contractor Work Order Number is not generated!", imgbtn_search);
                    }
                    else
                    {
                        basePage.Alert("Invalid Contractor Work Order Number!", imgbtn_search);
                    }
                }
            }

            else
            {
                basePage.Alert("Please Enter Valid Contractor Work   Order Number!", imgbtn_search);
            }
            ContractorWorkOrderNumber = string.Empty;
        }

        private void BindRadioButton()
        {
            rbtnInvoiceType.DataSource = metadataBL.ReadMetaDataInvoiceType();
            rbtnInvoiceType.DataValueField = "Id";
            rbtnInvoiceType.DataTextField = "Name";
            rbtnInvoiceType.DataBind();
        }

        private void BindgvContractorAdd()
        {
            gvContractorAddItem.DataSource = lstItemTransaction;
            gvContractorAddItem.DataBind();
        }

        private void PageDefaults()
        {
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            basePage.Page_Name = pageName;
        }

        private void EditData(Int32 InvoiceId)
        {
            lstInvoiceDom = new List<InvoiceDom>();
            contractorInvoiceBL = new ContractorInvoiceBL();
            lstIssueMaterialDOM = new List<IssueMaterialDOM>();
            issueMaterialBL = new IssueMaterialBL();
            lstInvoiceDom = contractorInvoiceBL.ReadContractorInvoice(InvoiceId, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, null);
            lstMetaData = contractorInvoiceBL.ReadContractorPaymentTerm(null, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber.ToString());
            lstItemTransaction = contractorInvoiceBL.ReadContractorInvoiceMappingView(InvoiceId);
            if (lstInvoiceDom.Count > 0)
            {
                BindUpdateText(lstInvoiceDom);
                BindGridPaymentType(lstInvoiceDom);
            }
            gvAddContractorItem.DataSource = lstItemTransaction;
            gvAddContractorItem.DataBind();
            BindgvContractorAddItem(lstInvoiceDom);
            pnlSearch.Visible = false;
            Panel1.Visible = true;
            Enabled(true);
        }

        // Bind grid in edit case
        private void BindgvContractorAddItem(List<InvoiceDom> lst)
        {
            contractorInvoiceBL = new ContractorInvoiceBL();
            List<ItemTransaction> lstPreItemTransaction = new List<ItemTransaction>();
            if (lst.Count > 0)
            {
                lstPreItemTransaction = contractorInvoiceBL.ReadContractorWorkOrderMapping(null, lst[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber);
                gvContractorAddItem.DataSource = lstPreItemTransaction;
                gvContractorAddItem.DataBind();
            }
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
            txtRemarks.Text = string.Empty;
            txtInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtBillDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtBillNumber.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            txtContractorName.Text = string.Empty;
            EmptyDocumentList();
        }

        private void BindUpdateText(List<InvoiceDom> lstInvoiceDom)
        {
            lblContractorWorkOrderNumber.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber.ToString();
            hdnContractorWOId.Value = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId.ToString();
            lblContractorName.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractorName.ToString();
            lblContractNo.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractNumber.ToString();
            lblWorkOrderNumber.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber.ToString();
            hdnContractorId.Value = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractorId.ToString();
            lblTotalNetValue.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.TotalNetValue.ToString();
            lblInvoicedAmount.Text = (lstInvoiceDom[0].InvoicedAmount - lstInvoiceDom[0].PayableAmount).ToString();
            lblLeftAmount.Text = (Convert.ToDecimal(lblTotalNetValue.Text) - Convert.ToDecimal(lblInvoicedAmount.Text)).ToString();
            lblPayableAmount.Text = lstInvoiceDom[0].PayableAmount.ToString();
            if (Convert.ToDecimal(lblPayableAmount.Text) > 0)
            {
                imgPayableAmt.Visible = true;
            }
            hdnPaymentTermId.Value = lstInvoiceDom[0].Payment.PaymentTermId.ToString();
            txtInvoiceDate.Text = lstInvoiceDom[0].InvoiceDate.ToString("dd-MMM-yyyy");
            txtBillDate.Text = lstInvoiceDom[0].BillDate.ToString("dd-MMM-yyyy");
            txtBillNumber.Text = lstInvoiceDom[0].BillNumber.ToString();
            GetDocumentData(lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.UploadDocumentId);
            txtRemarks.Text = lstInvoiceDom[0].Remarks.ToString();
            hdnStatusTypeId.Value = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.StatusType.Id.ToString();
        }

        private void Enabled(Boolean Condition)
        {
            btnSaveDraft.Visible = Condition;
            btnReset.Visible = Condition;
        }

        private bool IsContractorInvoice(String page)
        {
            if (page == "ContractorInvoice")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private MetaData CreateContractorInvoice(InvoiceDom invoiceDom, Int32? InvoiceId)
        {
            metaData = new MetaData();
            contractorInvoiceBL = new BusinessAccessLayer.Invoice.ContractorInvoiceBL();
            metaData = contractorInvoiceBL.CreateContractorInvoice(invoiceDom, InvoiceId);
            return metaData;
        }

        private InvoiceDom GetCotracrorInvoiceDetails()
        {
            //-----sundeep---------
            invoiceDom = new InvoiceDom();
            invoiceDom.Payment = new PaymentTerm();
            invoiceDom.Payment.PaymentType = new MetaData();
            invoiceDom.IssueMaterial = new IssueMaterialDOM();
            invoiceDom.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            invoiceDom.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            invoiceDom.InvoiceType = new MetaData();
            basePage = new BasePage();
            if (InvoiceId > 0)
            {
                invoiceDom.ContractorInvoiceId = InvoiceId;
            }
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = lblContractorWorkOrderNumber.Text.ToString();
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId = Convert.ToInt32(hdnContractorWOId.Value);
            invoiceDom.InvoiceType.Id = Convert.ToInt32(rbtnInvoiceType.SelectedValue);
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractorId = Convert.ToInt32(hdnContractorId.Value);
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractorName = lblContractorName.Text.ToString();
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber = lblWorkOrderNumber.Text.ToString();
            invoiceDom.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text.Trim());
            invoiceDom.BillDate = Convert.ToDateTime(txtBillDate.Text.Trim());
            invoiceDom.BillNumber = txtBillNumber.Text.Trim();
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractNumber = lblContractNo.Text.ToString();
            invoiceDom.Remarks = txtRemarks.Text.Trim();
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId = basePage.DocumentStackId;
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.TotalNetValue = Convert.ToDecimal(lblTotalNetValue.Text.ToString());
            //invoiceDom.InvoicedAmount = Convert.ToDecimal(lblPayableAmount.Text.ToString());
            // invoiceDom.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
            if (rbtnInvoiceType.SelectedValue == Convert.ToInt32(InvoiceType.Advance).ToString())
            {
                invoiceDom.InvoicedAmount = Convert.ToDecimal(ViewState["InvoiceAmount"]);
            }
            else
            {
                invoiceDom.InvoicedAmount = Convert.ToDecimal(ViewState["InvoiceAmount"]);
            }
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.Freight = TryToParse(txt_Freight.Text);
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.Packaging = TryToParse(txt_Packaging.Text);
            invoiceDom.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            invoiceDom.CreatedBy = basePage.LoggedInUser.UserLoginId;
            foreach (GridViewRow row in gvPaymentType.Rows)
            {
                rdbtn = (RadioButton)row.FindControl("rdbtn");
                lblPercenatageValue = (Label)row.FindControl("lblPercenatageValue");
                hdfPaymentTermId = (HiddenField)row.FindControl("hdfPaymentTermId");
                hdfPaymentTypeId = (HiddenField)row.FindControl("hdfPaymentTypeId");
                if (rdbtn.Checked == true)
                {
                    invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(lblPercenatageValue.Text))) / 100;
                    lblPayableAmount.Text = invoiceDom.PayableAmount.ToString("F2");
                    invoiceDom.Payment.PercentageValue = Convert.ToDecimal(lblPercenatageValue.Text);
                    invoiceDom.Payment.PaymentTermId = Convert.ToInt32(hdfPaymentTermId.Value);
                    invoiceDom.Payment.PaymentType.Id = Convert.ToInt32(hdfPaymentTypeId.Value);
                    break;
                }
            }

            foreach (GridViewRow row in gvAddContractorItem.Rows)
            {
                dec = 0;
                cnt = 0;
                BigNo = 0;
                BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].UnitForBilled);
                TextBox txtRecieveQuantity = (TextBox)row.FindControl("txtRecieveQuantity");
                Label lblIndex = (Label)row.FindControl("lblIndex");
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
                invoiceDom.IssueMaterial.DemandVoucher.Quotation.ItemTransaction = lstItemTransaction;
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

        private void SetAllDefaultData(List<InvoiceDom> lstInvoiceDom)
        {
            if (lstInvoiceDom != null)
            {
                BindText(lstInvoiceDom);

                BindGridPaymentType(lstInvoiceDom);
            }
        }

        private void BindGridPaymentType(List<InvoiceDom> lstInvoice_Dom)
        {
            paymentTermBL = new PaymentTermBL();
            lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(lstInvoice_Dom[0].IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId, Convert.ToInt16(QuotationType.Contractor));
            gvPaymentType.DataSource = lstPaymentTerm;
            gvPaymentType.DataBind();

        }

        private void BindText(List<InvoiceDom> lstInvoiceDom)
        {
            lblContractorWorkOrderNumber.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber.ToString();
            hdnContractorWOId.Value = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId.ToString();
            lblContractorName.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractorName.ToString();
            lblWorkOrderNumber.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber.ToString();
            hdnContractorId.Value = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractorId.ToString();
            lblContractNo.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractNumber.ToString();
            TotalNetValue = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.TotalNetValue;
            lblInvoicedAmount.Text = lstInvoiceDom[0].InvoicedAmount.ToString();
            hdnQutnGenDate.Value = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.GeneratedDate.ToString();
            lblLeftAmount.Text = lstInvoiceDom[0].LeftAmount.ToString();
            lblTotalNetValue.Text = TotalNetValue.ToString();

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

        #endregion

        #region Property

        //public void BindDocument()
        //{
        //    if (basePage.DocumentsList != null)
        //    {
        //        gv_documents.DataSource = basePage.DocumentsList;
        //    }
        //    else
        //    {
        //        gv_documents.DataSource = null;
        //    }
        //    gv_documents.DataBind();
        //}

        public String ContractorWorkOrderNumber
        {
            get
            {
                if (ViewState["CPON"] != null)
                {
                    return (String)ViewState["CPON"];
                }
                else
                    return String.Empty;
            }
            set
            {
                ViewState["CPON"] = value;
            }
        }

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
