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

namespace MiniERP.Invoice
{
    public partial class SupplierInvoice : BasePage
    {
        //#region Private global variables

        //InvoiceDom invoiceDom = null;
        //MetaData metaData = null;

        //ItemTransaction itemTransaction = null;
       
        
        //PaymentTermBL paymentTermBL = null;
        ////List<PaymentTerm> lstPaymentTerm = null;

        //IssueMaterialBL issueMaterialBL = null;
        //QuotationBL quotationBL = null;
        //ContractorInvoiceBL contractorInvoiceBL = null;

        //List<IssueMaterialDOM> lstIssueMaterialDOM = null;
        //List<QuotationDOM> lstQuotationDOM = null;
        ////List<InvoiceDom> lstInvoiceDom = null;

        
        //BasePage base_Page = new BasePage();
        
        ////
        //Decimal quantity = 0;
        //Decimal price = 0;
        //Decimal discount = 0;
        //Decimal ExciseDuty = 0;
        //Decimal ServiceTax = 0;
        //Decimal vat = 0;
        //Decimal cstWith = 0;
        //Decimal cstWithout = 0;
        //Decimal freight = 0;
        //Decimal packaging = 0;
        //Decimal tax = 0;
        //Decimal total = 0;

        ////
        //Decimal TotalNetValue = 0;
        //Decimal TotalAmount = 0;
        //Decimal Percentage = 0;
        //int i = 0;
        //int j = 0;
        //string strInvalid = string.Empty;
        //decimal dec = 0;
        //int cnt = 0;
        //int index = 0;
        //String s = string.Empty;
        //decimal BigNo = 0;
        //Boolean flag = false;
        //Boolean track = false;

        //RadioButton rdbtn = null;
        //Label lblPercenatageValue = null;
        //HiddenField hdfPaymentTermId = null;
        //HiddenField hdfPaymentTypeId = null;


        ////CheckBox chkbxQuotationDetails = null;
        ////CheckBox chbxSelectAll = null;
        ////HiddenField hdfSupplierPOMappingId = null;
        ////Label lblActivityDiscription = null;
        //////Label lblSupplierPOMappingId = null;
        ////Label lblItem = null;
        ////Label lblSpecification = null;
        ////Label lblItemCategory = null;
        ////Label lblBrand = null;
        ////Label lblNOF = null;
        ////Label lblQuantityReceived = null;
        ////Label lblActualNOFUnit = null;
        ////Label lblBilledUnit = null;
        ////Label lblUnitForBilled = null;
        ////Label lblCostPerUnit = null;
        ////Label lblPUD = null;
        ////Label lblDT = null;
        ////HiddenField hdfDTId = null;
        ////Label lblST = null;
        ////Label lblVAT = null;
        ////Label lblCSTW = null;
        ////Label lblCSTWO = null;            
        ////Label lblExciseDuty = null;
        ////Label lblTotalAmount = null;
        ////TextBox txtBilledNoOfUnit = null;
        ////Label lblIndex = null;           
        //SupplierRecieveMaterialBAL supplierRecieveMaterialBAL = null;
        //List<SupplierRecieveMatarial> lstSupplierRecieveMatarial = null;
        //SupplierInvoiceBL supplierInvoiceBL = null;

        //#endregion

        //#region protected methods

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        txtInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");               
        //        if (Request.QueryString["invoiceId"] != null)
        //        {
        //            InvoiceId = 0;
        //            InvoiceId = Convert.ToInt32(Request.QueryString["InvoiceId"]);
        //            EditData(InvoiceId);
        //        }
        //        else
        //        {
        //            InvoiceId = 0;
        //            Panel1.Visible = false;
        //            Enabled(false);
        //            txtSupplierPONumber.Focus();                   
        //        }
        //    }
        //}
        ////protected void Page_LoadComplete(object sender, EventArgs e)
        ////{
        ////    ctrl_UploadDocument.BindDocument();
        ////}

        //protected void imgbtn_search_Click(object sender, ImageClickEventArgs e)
        //{
        //    Reset();
        //    Enabled(false);
        //    lblPayableAmount.Text = string.Empty;
        //    ctrl_UploadDocument.EmptyDocumentList();

        //    supplierInvoiceBL = new SupplierInvoiceBL();
        //    //paymentTermBL = new PaymentTermBL();
            
        //    lstInvoiceDom = new List<InvoiceDom>();
        //    lstPaymentTerm = new List<PaymentTerm>();
            

        //    lstInvoiceDom = supplierInvoiceBL.ReadSupplierQuotation(null, txtSupplierPONumber.Text.Trim().ToString());
        //    lstMetaData = new List<MetaData>();
        //    lstMetaData = supplierInvoiceBL.ReadSupplierPaymentTerm(null, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, txtSupplierPONumber.Text.Trim());
        //    if (lstInvoiceDom[0].LeftAmount == 0)
        //    {
        //        base_Page.Alert(" All Payment terms have been completed for this Supplier Purchase Order Number!", imgbtn_search);
        //        Panel1.Visible = false;
        //    }
        //    else
        //    {
        //        if (lstInvoiceDom.Count > 0 && lstInvoiceDom[0].ReceiveMaterial.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
        //        {
        //            SetAllDefaultData(lstInvoiceDom);
        //            if (lstPaymentTerm.Count > 0)
        //            {
        //                txtSupplierPONumber.Text = String.Empty;
        //                Panel1.Visible = true;
        //            }

        //        }
        //        else if (string.IsNullOrEmpty(txtSupplierPONumber.Text.ToString()))
        //        {
        //            base_Page.Alert("Please enter a valid Supplier Purchase Order Number!", imgbtn_search);
        //        }
        //        else if (lstInvoiceDom.Count > 0 && lstInvoiceDom[0].ReceiveMaterial.Quotation.StatusType.Id != Convert.ToInt32(StatusType.Generated))
        //        {
        //            base_Page.Alert("Supplier Quotation is not generated!", imgbtn_search);
        //        }
        //        else
        //        {
        //            base_Page.Alert("Invalid Supplier Purchase Order Number!", imgbtn_search);
        //        }
        //    }
        //}
        //protected void gvPaymentType_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        rdbtn = (RadioButton)e.Row.FindControl("rdbtn");
        //        hdfPaymentTermId = (HiddenField)e.Row.FindControl("hdfPaymentTermId");
        //        //hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
        //        if (lstMetaData.Count != 0 && InvoiceId==0)
        //        {
        //            foreach (MetaData item in lstMetaData)
        //            {
        //                if (Convert.ToInt32(hdfPaymentTermId.Value) == item.Id)
        //                {
        //                    rdbtn.Checked = false;
        //                    rdbtn.Enabled = false;
        //                }
        //            }
        //        }
        //        else if (lstInvoiceDom.Count != 0 && InvoiceId != 0)
        //        {
        //            //foreach (MetaData item in lstMetaData)
        //            //{
        //            if (Convert.ToInt32(hdfPaymentTermId.Value) == lstInvoiceDom[0].Payment.PaymentTermId)
        //                {                            
        //                    rdbtn.Checked = true;
        //                    rdbtn.Enabled = true;
        //                }
        //            else if (Convert.ToInt32(hdfPaymentTermId.Value) != lstInvoiceDom[0].Payment.PaymentTermId)
        //                {
        //                    rdbtn.Checked = false;
        //                    rdbtn.Enabled = false;
        //                }
        //           // }
        //        }                
        //    }
        //}
        //protected void btnSaveDraft_Click(object sender, EventArgs e)
        //{
        //    //ltrl_err_msg.Text = string.Empty;
        //    metaData = new MetaData();
        //    invoiceDom = new InvoiceDom();
        //    base_Page = new BasePage();
        //    invoiceDom = GetSupplierInvoiceDetails();
        //    // Sum();
        //    //if (!string.IsNullOrEmpty(strInvalid))
        //    //{
        //    //    ltrl_err_msg.Text = strInvalid;
        //    //}
        //    //else if (string.IsNullOrEmpty(lblTotalAmt.Text) || Convert.ToDecimal(lblTotalAmt.Text)==0)
        //    //{
        //    //    Alert("Please calculate Sum", btnSaveDraft);
        //    //}
        //    //else if (Convert.ToDecimal(lblTotalAmt.Text)>Convert.ToDecimal(lblLeftAmount.Text))
        //    //{
        //    //    Alert("Total Amount can not be more than the Left Amount", btnSaveDraft);
        //    //}
        //    //else
        //    //{
        //    if (InvoiceId > 0)
        //    {
        //        metaData = CreateSupplierInvoice(invoiceDom, InvoiceId);
        //    }
        //    else
        //    {
        //        metaData = CreateSupplierInvoice(invoiceDom, null);
        //    }
        //    if (metaData.Id > 0)
        //    {
        //        ctrl_UploadDocument.CreateDocumentMapping();
        //        if (InvoiceId > 0)
        //        {
        //            base_Page.Alert("Supplier Invoice No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewSupplierInvoice.aspx");
        //            Reset();
        //        }
        //        else
        //        {
        //            base_Page.Alert("Supplier Invoice No: " + metaData.Name + " Created Successfully", btnSaveDraft, "SupplierInvoice.aspx");
        //            Reset();
        //        }
        //        //ltrl_err_msg.Text = string.Empty;
        //        //lstItemTransaction = null;
        //        //BindGridSupplierInvoice();
        //    }
        //    //}
        //}
        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    Reset();
        //}
        //protected void rdbtn_Click(object sender, EventArgs e)
        //{
        //    invoiceDom = new InvoiceDom();
        //    foreach (GridViewRow row in gvPaymentType.Rows)
        //    {
        //        rdbtn = (RadioButton)row.FindControl("rdbtn");
        //        lblPercenatageValue = (Label)row.FindControl("lblPercenatageValue");
        //        if (rdbtn.Checked == true && InvoiceId==0)
        //        {
        //            invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(lblPercenatageValue.Text))) / 100;
        //            lblPayableAmount.Text = invoiceDom.PayableAmount.ToString("F2");
        //            if (Convert.ToDecimal(lblPayableAmount.Text) > 0)
        //            {
        //                imgPayableAmt.Visible = true;
        //            }
        //            else
        //            {
        //                imgPayableAmt.Visible = false;
        //            }
        //            Enabled(true);
        //            break;
        //        }
        //        //else if (rdbtn.Checked == true && InvoiceId != 0)
        //        //{
        //        //    invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(lblPercenatageValue.Text))) / 100;
        //        //    lblPayableAmount.Text = invoiceDom.PayableAmount.ToString("F2");
        //        //    if (Convert.ToDecimal(lblPayableAmount.Text) > 0)
        //        //    {
        //        //        PayableAmt.Visible = true;
        //        //        lblInvoicedAmount.Text = (lstInvoiceDom[0].InvoicedAmount - invoiceDom.PayableAmount).ToString();
        //        //        lblLeftAmount.Text = (Convert.ToDecimal(lblTotalNetValue.Text) - Convert.ToDecimal(lblInvoicedAmount.Text)).ToString();
        //        //    }
        //        //    else
        //        //    {
        //        //        PayableAmt.Visible = false;
        //        //    }
        //        //    Enabled(true);
        //        //    break;
        //        //}
        //    }
        //}
        ////protected void chbxSelectAll_Click(object sender, EventArgs e)
        ////{
        ////    foreach (GridViewRow row in gvQuotationDetails.Rows)
        ////    {
        ////        chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        ////        if (chkbxQuotationDetails.Enabled == true)
        ////        {
        ////            chkbxQuotationDetails.Checked = ((CheckBox)sender).Checked;
        ////        }
        ////    }

        ////}
        ////protected void chkbxQuotationDetails_Click(object sender, EventArgs e)
        ////{
        ////    flag = false;
        ////    chbxSelectAll = (CheckBox)gvQuotationDetails.HeaderRow.FindControl("chbxSelectAll");
        ////    foreach (GridViewRow row in gvQuotationDetails.Rows)
        ////    {
        ////        chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        ////        if (!chkbxQuotationDetails.Checked)
        ////            flag = true;
        ////    }
        ////    if (flag == true)
        ////    {
        ////        chbxSelectAll.Checked = false;
        ////    }
        ////    else
        ////    {
        ////        chbxSelectAll.Checked = true;
        ////    }
        ////}
        ////protected void btnAddQuotationDetails_Click(object sender, EventArgs e)
        ////{
        ////    base_Page = new BasePage();
        ////    if (lstItemTransaction == null)
        ////    {
        ////        lstItemTransaction = new List<ItemTransaction>();
        ////    }
        ////    foreach (GridViewRow row in gvQuotationDetails.Rows)
        ////    {
        ////        chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        ////        hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
        ////        lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
        ////        lblItem = (Label)row.FindControl("lblItem");
        ////        lblSpecification = (Label)row.FindControl("lblSpecification");
        ////        lblItemCategory = (Label)row.FindControl("lblItemCategory");
        ////        lblBrand = (Label)row.FindControl("lblBrand");
        ////        lblNOF = (Label)row.FindControl("lblNOF");
        ////        lblQuantityReceived = (Label)row.FindControl("lblQuantityReceived");
        ////        lblActualNOFUnit = (Label)row.FindControl("lblActualNOFUnit");
        ////        lblBilledUnit = (Label)row.FindControl("lblBilledUnit");
        ////        lblUnitForBilled = (Label)row.FindControl("lblUnitForBilled");
        ////        lblCostPerUnit = (Label)row.FindControl("lblCostPerUnit");
        ////        lblPUD = (Label)row.FindControl("lblPUD");
        ////        lblDT = (Label)row.FindControl("lblDT");
        ////        hdfDTId = (HiddenField)row.FindControl("hdfDTId");
        ////        lblST = (Label)row.FindControl("lblST");
        ////        lblVAT = (Label)row.FindControl("lblVAT");
        ////        lblCSTW = (Label)row.FindControl("lblCSTW");
        ////        lblCSTWO = (Label)row.FindControl("lblCSTWO");
        ////        lblExciseDuty = (Label)row.FindControl("lblExciseDuty");
        ////        //lblPackaging = (Label)row.FindControl("lblPackaging");
        ////        lblTotalAmount = (Label)row.FindControl("lblTotalAmount");

        ////        if (chkbxQuotationDetails.Checked == true && hdfSupplierPOMappingId != null)
        ////        {
        ////            if (chkbxQuotationDetails.Checked.Equals(true))
        ////            {
        ////                itemTransaction = new ItemTransaction();
        ////                itemTransaction.MetaProperty = new MetaData();
        ////                itemTransaction.TaxInformation = new Tax();
        ////                itemTransaction.TaxInformation.DiscountMode = new MetaData();
        ////                itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
        ////                itemTransaction.Item = new Item();
        ////                itemTransaction.Item.ModelSpecification = new ModelSpecification();
        ////                itemTransaction.Item.ModelSpecification.Category = new ItemCategory();

        ////                itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfSupplierPOMappingId.Value);
        ////                //itemTransaction.DeliverySchedule.ActivityDescription = lblActivityDiscription.Text.ToString();
        ////                itemTransaction.Item.ItemName = lblItem.Text.ToString();
        ////                itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
        ////                itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
        ////                itemTransaction.Item.ModelSpecification.Brand = lblBrand.Text.ToString();
        ////                itemTransaction.NumberOfUnit = Convert.ToDecimal(lblNOF.Text.ToString());
        ////                itemTransaction.QuantityReceived = Convert.ToDecimal(lblQuantityReceived.Text.ToString());
        ////                //itemTransaction.UnitIssued = Convert.ToDecimal(lblActualNOFUnit.Text.ToString());
        ////                itemTransaction.BilledUnit = Convert.ToDecimal(lblBilledUnit.Text.ToString());
        ////                itemTransaction.UnitLeft = Convert.ToDecimal(lblUnitForBilled.Text.ToString());
        ////                itemTransaction.UnitForBilled = Convert.ToDecimal(lblUnitForBilled.Text.ToString());
        ////                itemTransaction.PerUnitCost = Convert.ToDecimal(lblCostPerUnit.Text.ToString());
        ////                itemTransaction.PerUnitDiscount = Convert.ToDecimal(lblPUD.Text.ToString());
        ////                itemTransaction.TaxInformation.DiscountMode.Name = lblDT.Text.ToString();
        ////                itemTransaction.TaxInformation.DiscountMode.Id = Convert.ToInt32(hdfDTId.Value);
        ////                itemTransaction.TaxInformation.ServiceTax = Convert.ToDecimal(lblST.Text.ToString());
        ////                itemTransaction.TaxInformation.VAT = Convert.ToDecimal(lblVAT.Text.ToString());
        ////                itemTransaction.TaxInformation.CSTWithCForm = Convert.ToDecimal(lblCSTW.Text.ToString());
        ////                itemTransaction.TaxInformation.CSTWithoutCForm = Convert.ToDecimal(lblCSTWO.Text.ToString());
        ////                itemTransaction.TaxInformation.ExciseDuty = Convert.ToDecimal(lblExciseDuty.Text.ToString());                            
        ////                itemTransaction.TotalAmount = Convert.ToDecimal(lblTotalAmount.Text.ToString());


        ////                itemTransaction.CreatedBy = base_Page.LoggedInUser.UserLoginId;
        ////                lstItemTransaction.Add(itemTransaction);                            
        ////                chkbxQuotationDetails.Checked = false;
        ////                chkbxQuotationDetails.Enabled = false;
        ////            }
        ////        }

        ////    }
        ////    if (lstItemTransaction.Count > 0)
        ////    {

        ////        BindGridSupplierInvoice();
        ////        Enabled(true);
        ////    }
        ////    else
        ////    {
        ////        Enabled(false);
        ////        base_Page.Alert("Please Check At Least One Activity Description", btnAddQuotationDetails);
        ////    }
        ////    foreach (TableCell item in gvQuotationDetails.HeaderRow.Cells)
        ////    {
        ////        CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
        ////        if (chbxSelectAll.Checked == true)
        ////        {
        ////            chbxSelectAll.Checked = false;
        ////        }
        ////    }
        ////    // BindGridQuotationDetails(lstQuotationDOM);

        ////}
        ////protected void gvInvoiceDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        ////{
        ////    s = string.Empty;
        ////    index = 0;
        ////    index = Convert.ToInt32(e.CommandArgument);
        ////    //s = lstItemTransaction[index].DeliverySchedule.ActivityDescription.ToString();
        ////    s = lstItemTransaction[index].DeliverySchedule.Id.ToString();
        ////    if (e.CommandName == "cmdDelete")
        ////    {
        ////        lstItemTransaction.RemoveAt(index);
        ////        foreach (GridViewRow row in gvQuotationDetails.Rows)
        ////        {
        ////            chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        ////            //lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
        ////            hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
        ////            if (hdfSupplierPOMappingId.Value == s)
        ////            {
        ////                chkbxQuotationDetails.Enabled = true;
        ////                chkbxQuotationDetails.Checked = false;
        ////                //flag = true;
        ////                break;
        ////            }
        ////        }
        ////        if (lstItemTransaction.Count == 0)
        ////        {
        ////            lstItemTransaction = null;
        ////            ltrl_err_msg.Text = string.Empty;
        ////            Enabled(false);
        ////        }
        ////        BindGridSupplierInvoice();
        ////    }
        ////    Sum();
        ////}
        ////protected void gvQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        ////{
        ////    foreach (GridViewRow row in gvQuotationDetails.Rows)
        ////    {
        ////        chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        ////        lblUnitForBilled = (Label)row.FindControl("lblUnitForBilled");
        ////        hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
        ////        if (InvoiceId != 0 && lstItemTransaction != null)
        ////        {
        ////            foreach (ItemTransaction item in lstItemTransaction)
        ////            {
        ////                if (Convert.ToDecimal(lblUnitForBilled.Text.ToString()) == 0 || Convert.ToInt32(hdfSupplierPOMappingId.Value) == item.DeliverySchedule.Id)
        ////                {
        ////                    chkbxQuotationDetails.Checked = false;
        ////                    chkbxQuotationDetails.Enabled = false;
        ////                }
        ////            }
        ////        }
        ////        else if (Convert.ToDecimal(lblUnitForBilled.Text.ToString()) == 0)
        ////        {
        ////            chkbxQuotationDetails.Checked = false;
        ////            chkbxQuotationDetails.Enabled = false;
        ////        }
        ////    }
        ////}
        ////protected void gvInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        ////{
        ////    base_Page = new BasePage();
        ////    if (InvoiceId != 0)
        ////    {
        ////        if (lstItemTransaction != null)
        ////        {
        ////            foreach (ItemTransaction Item in lstItemTransaction)
        ////            {
        ////                foreach (ItemTransaction Items in lstPreItemTransaction)
        ////                {
        ////                    if (Item.DeliverySchedule.Id == Items.DeliverySchedule.Id)
        ////                    {
        ////                        Item.BilledUnit = Items.BilledUnit;
        ////                        Item.UnitLeft = Items.UnitForBilled;
        ////                        Item.CreatedBy = base_Page.LoggedInUser.UserLoginId;
        ////                    }
        ////                }
        ////            }
        ////        }
        ////    }
        ////}
        ////protected void btnSum_Click(object sender, EventArgs e)
        ////{
        ////    Sum();
        ////}

        ////protected void rdbtnlstPayment_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    if (rdbtnlstPayment.SelectedItem.Value == "2")
        ////    {
        ////        gvQuotationDetails.Visible = true;
        ////        btnAddQuotationDetails.Visible = true;
        ////        Enabled(false);
        ////    }
        ////    else if (rdbtnlstPayment.SelectedItem.Value == "1")
        ////    {
        ////        gvQuotationDetails.Visible = false;
        ////        btnAddQuotationDetails.Visible = false;
        ////        Enabled(true);
        ////    }
        ////}
        //#endregion

        //#region private methods
        ////private void Sum()
        ////{
        ////    i = 0;
        ////    j = 0;
        ////    TotalAmount = 0;
        ////    freight=Convert.ToDecimal(lblFreight.Text);
        ////    packaging=Convert.ToDecimal(lblPackaging.Text);
        ////    foreach (GridViewRow row in gvInvoiceDetails.Rows)
        ////    {
        ////        txtBilledNoOfUnit = (TextBox)row.FindControl("txtBilledNoOfUnit");
        ////        lstItemTransaction[i].UnitForBilled = Convert.ToDecimal(txtBilledNoOfUnit.Text);
        ////        i++;
        ////    }
        ////    foreach (ItemTransaction item in lstItemTransaction)
        ////    {
        ////        total = 0;
        ////        quantity = lstItemTransaction[j].UnitForBilled;
        ////        price = lstItemTransaction[j].PerUnitCost;
        ////        discount = lstItemTransaction[j].PerUnitDiscount;
        ////        ExciseDuty = lstItemTransaction[j].TaxInformation.ExciseDuty;
        ////        ServiceTax = lstItemTransaction[j].TaxInformation.ServiceTax;
        ////        vat = lstItemTransaction[j].TaxInformation.VAT;
        ////        cstWith = lstItemTransaction[j].TaxInformation.CSTWithCForm;
        ////        cstWithout = lstItemTransaction[j].TaxInformation.CSTWithoutCForm;
        ////        total = price * quantity ;
        ////        total=total-(discount * quantity);
        ////        total = total + (total * ExciseDuty) / 100;
        ////        tax = lstItemTransaction[j].TaxInformation.ServiceTax + lstItemTransaction[j].TaxInformation.VAT + lstItemTransaction[j].TaxInformation.CSTWithCForm + lstItemTransaction[j].TaxInformation.CSTWithoutCForm;
        ////        total = total + (total * tax) / 100;
        ////        TotalAmount = TotalAmount + total;
        ////        j++;
        ////    }
        ////    TotalAmount = TotalAmount + freight + packaging;
        ////    lblTotalAmt.Text = TotalAmount.ToString();
        ////}
        //private void Reset()
        //{
        //    txtRemarks.Text = string.Empty;
        //    txtInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");              
        //}
        //private void EditData(Int32 InvoiceId)
        //{
        //    lstInvoiceDom = new List<InvoiceDom>();
        //    supplierInvoiceBL = new SupplierInvoiceBL();
        //    lstInvoiceDom = supplierInvoiceBL.ReadSupplierInvoice(InvoiceId, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, null);
        //    lstMetaData = supplierInvoiceBL.ReadSupplierPaymentTerm(null, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationNumber.ToString());
        //    if (lstInvoiceDom.Count > 0)
        //    {
        //        BindUpdateText(lstInvoiceDom);
        //        BindGridPaymentType(lstInvoiceDom);
        //    }
        //    ////lstQuotationDOM = new List<QuotationDOM>();
        //    //lstIssueMaterialDOM = new List<IssueMaterialDOM>();
        //    //lstItemTransaction = new List<ItemTransaction>();
        //    //lstItemTransaction = contractorInvoiceBL.ReadInvoiceMapping(InvoiceId);
        //    ////quotationBL = new QuotationBL();
        //    //issueMaterialBL = new IssueMaterialBL();
        //    ////lstQuotationDOM = quotationBL.ReadContractorQuotation(null, lstInvoiceDom[0].Quotation.ContractQuotationNumber.ToString());
        //    //lstIssueMaterialDOM = issueMaterialBL.ReadIssueMaterial(null, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, lstInvoiceDom[0].IssueMaterial.IssueMaterialNumber);
        //    //if (lstIssueMaterialDOM.Count > 0)
        //    //{
        //    //    BindGridQuotationDetails(lstIssueMaterialDOM, null);
        //    //    BindUpdateText(lstInvoiceDom);
        //    //    BindGridContractorInvoice();
        //    //}
        //    pnlSearch.Visible = false;
        //    Panel1.Visible = true;
        //    Enabled(true);
        //}
        //private void BindUpdateText(List<InvoiceDom> lstInvoiceDom)
        //{
        //    //
        //    lblSupplierPurchaseOrderNumber.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationNumber.ToString();
        //    hdnSupplierPOId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationId.ToString();
        //    lblSupplierName.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierName.ToString();
        //    hdnSupplierId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierId.ToString();
        //    lblTotalNetValue.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.TotalNetValue.ToString();
        //    lblInvoicedAmount.Text = (lstInvoiceDom[0].InvoicedAmount - lstInvoiceDom[0].PayableAmount).ToString();
        //    lblLeftAmount.Text = (Convert.ToDecimal(lblTotalNetValue.Text) - Convert.ToDecimal(lblInvoicedAmount.Text)).ToString();
        //    lblPayableAmount.Text = lstInvoiceDom[0].PayableAmount.ToString();
        //    if (Convert.ToDecimal(lblPayableAmount.Text)>0)
        //    {
        //        imgPayableAmt.Visible = true;
        //    }
        //    lblFreight.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.Freight.ToString();
        //    lblPackaging.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.Packaging.ToString();
        //    hdnPaymentTermId.Value = lstInvoiceDom[0].Payment.PaymentTermId.ToString();
        //    //lblTotalNetValue.Text = TotalNetValue.ToString();
        //    txtInvoiceDate.Text = lstInvoiceDom[0].InvoiceDate.ToString("dd-MMM-yyyy");
        //    ctrl_UploadDocument.GetDocumentData(lstInvoiceDom[0].ReceiveMaterial.Quotation.UploadDocumentId);
        //    txtRemarks.Text = lstInvoiceDom[0].Remarks.ToString();
        //    //lblFreight.Text = freight.ToString();
        //    //lblPackaging.Text = packaging.ToString();
        //    hdnStatusTypeId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.StatusType.Id.ToString();
        //    ////
        //    //lblContractorWorkOrder.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber.ToString();
        //    //lblIssueMaterialnumber.Text = lstInvoiceDom[0].IssueMaterial.IssueMaterialNumber.ToString();
        //    //lblDemandVoucher.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.IssueDemandVoucherNumber.ToString();
        //    //lblContractorName.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractorName.ToString();
        //    //hdnContractorId.Value = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractorId.ToString();
        //    //lblContractNo.Text = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.ContractNumber.ToString();
        //    //txtInvoiceDate.Text = lstInvoiceDom[0].InvoiceDate.ToString("dd-MMM-yyyy");
        //    //txtRemarks.Text = lstInvoiceDom[0].Remarks.ToString();
        //    //ctrl_UploadDocument.GetDocumentData(lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.UploadDocumentId);
        //    //lblTotalNetValue.Text = lstInvoiceDom[0].TotalAmount.ToString();
        //    //hdnStatusTypeId.Value = lstInvoiceDom[0].IssueMaterial.DemandVoucher.Quotation.StatusType.Id.ToString();
        //    ////
        //}
        ////private void BindPaymentType(List<PaymentTerm> lstPaymentTerm)
        ////{
        ////    rdbtnlstPayment.DataSource = lstPaymentTerm;
        ////    rdbtnlstPayment.DataValueField = "MyId";
        ////    rdbtnlstPayment.DataTextField = "MyName";
        ////    rdbtnlstPayment.DataBind();

        ////        rdbtnlstPayment.Items[0].Selected = true;
        ////        if (rdbtnlstPayment.SelectedItem.Value == "1")
        ////        {
        ////            Enabled(true);
        ////            lblPerventageOfAdvance.Text = lstPaymentTerm[0].NumberOfDays.ToString();
        ////        }

        ////}
        ////private void BindGridSupplierInvoice()
        ////{
        ////    gvInvoiceDetails.DataSource = lstItemTransaction;
        ////    gvInvoiceDetails.DataBind();
        ////}
        //private void Enabled(Boolean Condition)
        //{
        //    btnSaveDraft.Visible = Condition;
        //    btnReset.Visible = Condition;
        //    //lblPercentageofAdvance1.Visible = Condition;
        //    //lblPerventageOfAdvance.Visible = Condition;
        //}
        //private void BindGridPaymentType(List<InvoiceDom> lstInvoice_Dom)
        //{
        //    //lstPaymentTerm = new List<PaymentTerm>();
        //    paymentTermBL = new PaymentTermBL();
        //    lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(lstInvoice_Dom[0].ReceiveMaterial.Quotation.SupplierQuotationId, Convert.ToInt16(QuotationType.Supplier));
            
            
        //    gvPaymentType.DataSource = lstPaymentTerm;
        //    gvPaymentType.DataBind();

            
            
        //}
        ////private bool IsContractorInvoice(String page)
        ////{
        ////    if (page == "ContractorInvoice")
        ////    {
        ////        return true;
        ////    }
        ////    else
        ////    {
        ////        return false;
        ////    }
        ////}
        //private MetaData CreateSupplierInvoice(InvoiceDom invoiceDom, Int32? InvoiceId)
        //{
        //    //if (lstItemTransaction != null)
        //    //{
        //    metaData = new MetaData();
        //    supplierInvoiceBL = new SupplierInvoiceBL();
        //    metaData = supplierInvoiceBL.CreateSupplierInvoice(invoiceDom, InvoiceId);
        //    //}
        //    return metaData;
        //}
        //private InvoiceDom GetSupplierInvoiceDetails()
        //{
        //    //-----------------------New-----------------------------------------------------

        //    invoiceDom = new InvoiceDom();
        //    //invoiceDom = new InvoiceDom();
        //    invoiceDom.Payment = new PaymentTerm();
        //    invoiceDom.Payment.PaymentType = new MetaData();
        //    invoiceDom.ReceiveMaterial = new SupplierRecieveMatarial();
        //    invoiceDom.ReceiveMaterial.Quotation = new QuotationDOM();
        //    invoiceDom.ReceiveMaterial.Quotation.StatusType = new MetaData();
        //    base_Page = new BasePage();
        //    if (InvoiceId > 0)
        //    {
        //        invoiceDom.SupplierInvoiceId = InvoiceId;
        //    }
        //    invoiceDom.ReceiveMaterial.Quotation.SupplierQuotationNumber = lblSupplierPurchaseOrderNumber.Text.ToString();
        //    invoiceDom.ReceiveMaterial.Quotation.SupplierQuotationId=Convert.ToInt32(hdnSupplierPOId.Value);

        //    invoiceDom.ReceiveMaterial.Quotation.SupplierId = Convert.ToInt32(hdnSupplierId.Value);
        //    invoiceDom.ReceiveMaterial.Quotation.SupplierName = lblSupplierName.Text.Trim();
        //    invoiceDom.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text.Trim());
        //    invoiceDom.Remarks = txtRemarks.Text.Trim();
        //    invoiceDom.ReceiveMaterial.Quotation.UploadDocumentId = DocumentStackId;
        //    invoiceDom.ReceiveMaterial.Quotation.TotalNetValue = Convert.ToDecimal(lblTotalNetValue.Text.Trim());
        //    invoiceDom.InvoicedAmount = Convert.ToDecimal(lblPayableAmount.Text.Trim());
        //    invoiceDom.ReceiveMaterial.Quotation.Freight = Convert.ToDecimal(lblFreight.Text);
        //    invoiceDom.ReceiveMaterial.Quotation.Packaging = Convert.ToDecimal(lblPackaging.Text);
        //    invoiceDom.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
        //    invoiceDom.Remarks = txtRemarks.Text.ToString();

        //    invoiceDom.ReceiveMaterial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
        //    invoiceDom.CreatedBy = base_Page.LoggedInUser.UserLoginId;
        //    foreach (GridViewRow row in gvPaymentType.Rows)
        //    {
        //        rdbtn = (RadioButton)row.FindControl("rdbtn");
        //        lblPercenatageValue = (Label)row.FindControl("lblPercenatageValue");
        //        hdfPaymentTermId = (HiddenField)row.FindControl("hdfPaymentTermId");
        //        hdfPaymentTypeId = (HiddenField)row.FindControl("hdfPaymentTypeId");
        //        if (rdbtn.Checked == true)
        //        {
        //            invoiceDom.PayableAmount = ((Convert.ToDecimal(lblTotalNetValue.Text)) * (Convert.ToDecimal(lblPercenatageValue.Text))) / 100;
        //            lblPayableAmount.Text = invoiceDom.PayableAmount.ToString("F");
        //            invoiceDom.Payment.PercentageValue = Convert.ToDecimal(lblPercenatageValue.Text);
        //            invoiceDom.Payment.PaymentTermId = Convert.ToInt32(hdfPaymentTermId.Value);
        //            invoiceDom.Payment.PaymentType.Id = Convert.ToInt32(hdfPaymentTypeId.Value);
        //            break;
        //        }
        //    }
        //    return invoiceDom;

        //    //-----------------------New-End----------------------------------------------------

        //    //-------------Old--------------------------------------------------
        //    //i = 0;
        //    //invoiceDom = new InvoiceDom();
        //    //invoiceDom.Payment = new PaymentTerm();
        //    //invoiceDom.ReceiveMaterial = new SupplierRecieveMatarial();
        //    //invoiceDom.ReceiveMaterial.Quotation = new QuotationDOM();
        //    //invoiceDom.ReceiveMaterial.Quotation.StatusType = new MetaData();
        //    //base_Page = new BasePage();
        //    //if (InvoiceId > 0)
        //    //{
        //    //    invoiceDom.SupplierInvoiceId = InvoiceId;
        //    //}                
        //    //invoiceDom.ReceiveMaterial.Quotation.SupplierQuotationNumber = lblSupplierPurchaseOrderNumber.Text.ToString();

        //    //invoiceDom.ReceiveMaterial.Quotation.SupplierId = Convert.ToInt32(hdnSupplierId.Value);
        //    //invoiceDom.ReceiveMaterial.Quotation.SupplierName = lblSupplierName.Text.Trim();
        //    //invoiceDom.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text.Trim());
        //    //invoiceDom.Remarks = txtRemarks.Text.Trim();
        //    //invoiceDom.ReceiveMaterial.Quotation.UploadDocumentId = DocumentStackId;
        //    //invoiceDom.ReceiveMaterial.Quotation.TotalNetValue = Convert.ToDecimal(lblTotalNetValue.Text.Trim());
        //    //if (!string.IsNullOrEmpty(lblTotalAmt.Text))
        //    //{
        //    //    invoiceDom.InvoicedAmount = Convert.ToDecimal(lblTotalAmt.Text.Trim());
        //    //}
        //    //else
        //    //{
        //    //    invoiceDom.InvoicedAmount = 0;
        //    //}
        //    //invoiceDom.ReceiveMaterial.Quotation.Freight=Convert.ToDecimal(lblFreight.Text);
        //    //invoiceDom.ReceiveMaterial.Quotation.Packaging = Convert.ToDecimal(lblPackaging.Text);
        //    //invoiceDom.InvoiceDate=Convert.ToDateTime(txtInvoiceDate.Text);
        //    //invoiceDom.Remarks = txtRemarks.Text.ToString();                

        //    //invoiceDom.ReceiveMaterial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
        //    //invoiceDom.CreatedBy = base_Page.LoggedInUser.UserLoginId;
        //    //foreach (GridViewRow row in gvInvoiceDetails.Rows)
        //    //{
        //    //    dec = 0;
        //    //    cnt = 0;
        //    //    BigNo = 0;
        //    //    BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].UnitForBilled);
        //    //    txtBilledNoOfUnit = (TextBox)row.FindControl("txtBilledNoOfUnit");
        //    //    lblIndex = (Label)row.FindControl("lblIndex");
        //    //    //lstItemTransaction[i].BilledUnit = Convert.ToDecimal(txtBilledNoOfUnit.Text);
        //    //    dec = TryToParse(txtBilledNoOfUnit.Text);
        //    //    if (dec > 0)
        //    //    {
        //    //        cnt = NumberDecimalPlaces(dec);
        //    //        if (InvoiceId > 0)
        //    //        {
        //    //            if (cnt > 2 || dec > BigNo)
        //    //            {
        //    //                if (j > 0)
        //    //                {
        //    //                    strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
        //    //                    j++;
        //    //                }
        //    //                else
        //    //                {
        //    //                    strInvalid = strInvalid + lblIndex.Text.Trim();
        //    //                    j++;
        //    //                }
        //    //            }
        //    //        }
        //    //        else if (Convert.ToDecimal(txtBilledNoOfUnit.Text) > lstItemTransaction[i].UnitLeft || cnt > 2)
        //    //        {
        //    //            if (j > 0)
        //    //            {
        //    //                strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
        //    //                j++;
        //    //            }
        //    //            else
        //    //            {
        //    //                strInvalid = strInvalid + lblIndex.Text.Trim();
        //    //                j++;
        //    //            }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (j > 0)
        //    //        {
        //    //            strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
        //    //            j++;
        //    //        }
        //    //        else
        //    //        {
        //    //            strInvalid = strInvalid + lblIndex.Text.Trim();
        //    //            j++;
        //    //        }
        //    //    }
        //    //    if (string.IsNullOrEmpty(strInvalid))
        //    //    {
        //    //        lstItemTransaction[i].UnitForBilled = Convert.ToDecimal(txtBilledNoOfUnit.Text);
        //    //    }
        //    //    i++;
        //    //}
        //    //if (!string.IsNullOrEmpty(strInvalid))
        //    //{
        //    //    strInvalid = "Unit For Billed allows only valid numeric value <= Unit Left at S.No: " + strInvalid;
        //    //}
        //    //else
        //    //{
        //    //    invoiceDom.ReceiveMaterial.Quotation.ItemTransaction = lstItemTransaction;
        //    //}
        //    //return invoiceDom;
        //    //-------------Old-End-------------------------------------------------
        //}
        //private Decimal TryToParse(string Value)
        //{
        //    dec = 0;
        //    Decimal.TryParse(Value, out dec);
        //    return dec;
        //}
        //private int NumberDecimalPlaces(Decimal dec)
        //{
        //    string testdec = Convert.ToString(dec);
        //    int s = (testdec.IndexOf(".") + 1); // the first numbers plus decimal point 
        //    if (s > 0)
        //    {
        //        return ((testdec.Length) - s);   //total length minus beginning numbers and decimal = number of decimal points 
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //private void SetAllDefaultData(List<InvoiceDom> lstInvoice)
        //{
        //    BindGridPaymentType(lstInvoice);
        //    //BindGridQuotationDetails(lstInvoiceDom);               
        //    if (lstPaymentTerm.Count > 0)
        //    {
        //        BindText(lstInvoiceDom);
        //    }
        //}
        //private void BindText(List<InvoiceDom> lstInvoiceDom)
        //{
        //    //TotalNetValue = 0;
        //    //lstPaymentTerm = new List<PaymentTerm>();
        //    //paymentTermBL = new PaymentTermBL();
        //    //lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationId, Convert.ToInt16(QuotationType.Supplier));

        //    lblSupplierPurchaseOrderNumber.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationNumber.ToString();
        //    hdnSupplierPOId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationId.ToString();            
        //    lblSupplierName.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierName.ToString();
        //    hdnSupplierId.Value = lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierId.ToString();
        //    TotalNetValue = lstInvoiceDom[0].ReceiveMaterial.Quotation.TotalNetValue;
        //    lblInvoicedAmount.Text = lstInvoiceDom[0].InvoicedAmount.ToString();
        //    lblLeftAmount.Text = lstInvoiceDom[0].LeftAmount.ToString();
        //    freight = lstInvoiceDom[0].ReceiveMaterial.Quotation.Freight;
        //    packaging = lstInvoiceDom[0].ReceiveMaterial.Quotation.Packaging;
        //    lblTotalNetValue.Text = TotalNetValue.ToString();
        //    lblFreight.Text = freight.ToString();
        //    lblPackaging.Text = packaging.ToString();
        //    //if (Convert.ToDecimal(lblInvoicedAmount.Text) == 0 && lstPaymentTerm[0].MyName.ToString()==PaymentType.Advance.ToString())
        //    //{
        //    //    lblPaymentType.Text = "Advance";
        //    //    Percentage = lstPaymentTerm[0].NumberOfDays;
        //    //    //lblPercentageOfAdvance.Text = lstPaymentTerm[0].NumberOfDays.ToString();
        //    //}
        //    //else if (Convert.ToDecimal(lblInvoicedAmount.Text) == 0 && lstPaymentTerm[0].MyName.ToString() ==PaymentType.AfterDays.ToString())
        //    //{
        //    //    lblPaymentType.Text = "After Days";
        //    //}
        //    //else if (Convert.ToDecimal(lblInvoicedAmount.Text) > 0 && lstPaymentTerm[0].MyName.ToString() == PaymentType.AfterDays.ToString())
        //    //{
        //    //    lblPaymentType.Text = "After Days";
        //    //}
        //    //else if (Convert.ToDecimal(lblInvoicedAmount.Text) > 0 && lstPaymentTerm.Count>1)
        //    //{
        //    //    lblPaymentType.Text = "After Days";
        //    //}
        //    //if (lblPaymentType.Text == "After Days")
        //    //{
        //    //    gvQuotationDetails.Visible = true;
        //    //    btnAddQuotationDetails.Visible = true;
        //    //    Enabled(false);
        //    //    foreach (ItemTransaction item in lstPreItemTransaction)
        //    //    {
        //    //        TotalNetValue = TotalNetValue + item.TotalAmount;
        //    //    }
        //    //    lblTotalNetValue.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.TotalNetValue.ToString();
        //    //    lblFreight.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.Freight.ToString();
        //    //    lblPackaging.Text = lstInvoiceDom[0].ReceiveMaterial.Quotation.Packaging.ToString();
        //    //}
        //    //else if (lblPaymentType.Text == "Advance")
        //    //{
        //    //    Enabled(true);                    
        //    //    TotalAmount = (TotalNetValue * Percentage) / 100;
        //    //    lblAdvancedAmount.Text = TotalAmount.ToString();
        //    //    lblTotalAmt.Text = TotalAmount.ToString();
        //    //    lblPercentageOfAdvance.Text = Percentage.ToString();
        //    //    freight = (freight * Percentage) / 100;
        //    //    packaging = (packaging * Percentage) / 100;
        //    //    lblFreight.Text = freight.ToString();
        //    //    lblPackaging.Text = packaging.ToString();
        //    //    lblTotalNetValue.Text = TotalNetValue.ToString();
        //    //}
        //}
        ////private void BindGridQuotationDetails(List<InvoiceDom> lstInvoiceDom)
        ////{              
        ////        supplierInvoiceBL = new SupplierInvoiceBL();
        ////        lstPreItemTransaction = new List<ItemTransaction>();
        ////        lstPreItemTransaction = supplierInvoiceBL.ReadSupplierPOReceiveMaterialMapping(null, lstInvoiceDom[0].ReceiveMaterial.Quotation.SupplierQuotationNumber);
        ////        gvQuotationDetails.DataSource = lstPreItemTransaction;
        ////        gvQuotationDetails.DataBind();               
        ////}
        //#endregion

        //#region Property
        //private List<PaymentTerm> lstPaymentTerm
        //{
        //    get
        //    {
        //        return (List<PaymentTerm>)ViewState["lstPaymentTerm"];
        //    }
        //    set
        //    {
        //        ViewState["lstPaymentTerm"] = value;
        //    }
        //}
        //private List<InvoiceDom> lstInvoiceDom
        //{
        //    get
        //    {
        //        return (List<InvoiceDom>)ViewState["lstInvoiceDom"];
        //    }
        //    set
        //    {
        //        ViewState["lstInvoiceDom"] = value;
        //    }
        //}
        //private List<MetaData> lstMetaData
        //{
        //    get
        //    {
        //        return (List<MetaData>)ViewState["lstMetaData"];
        //    }
        //    set
        //    {
        //        ViewState["lstMetaData"] = value;
        //    }
        //}
        //private int InvoiceId
        //{
        //    get
        //    {
        //        return (Int32)ViewState["InvoiceId"];
        //    }
        //    set
        //    {
        //        ViewState["InvoiceId"] = value;
        //    }
        //}
        //private string pageName
        //{
        //    get
        //    {
        //        return (String)ViewState["pageName"];
        //    }
        //    set
        //    {
        //        ViewState["pageName"] = value;
        //    }
        //}
        //#endregion
        


    }
}