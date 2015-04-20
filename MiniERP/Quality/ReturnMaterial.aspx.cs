using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Text;
using System.Drawing;
using BusinessAccessLayer.Quality;

namespace MiniERP.Quality
{
    public partial class ReturnMaterial : BasePage
    {

        //#region Global Variables Declation
        //QuotationDOM Quotation = null;
        //List<QuotationDOM> lstQuotation = null;
        //QuotationBL quotationBL = new QuotationBL();

        //ItemTransaction itemTransaction = null;

        //int count = 0;
        //int i = 0;
        //Boolean flag = false;
        //HiddenField hdfItemId = null;
        //HiddenField hdfSupplierPOMappingId = null;
        //Label lblItem = null;
        //Label lblSpecification = null;
        //Label lblItemCategory = null;
        //Label lblBrand = null;
        //Label lblItemQuantity = null;
        //Label lblItemRecieve = null;
        //Label lblItemLeft = null;
        //Label lblMeasurement = null;
        //CheckBox chkSelectAll = null;
        //CheckBox chkSelect = null;
        //Label hdfIndex = null;
        //#endregion

        //#region Protected Method
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    SupplierRecieveMaterialId = 0;
        //    CalendarExtender1.EndDate = DateTime.Now;
        //    txtRMDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //    if (!IsPostBack)
        //    {
        //        pnlRecieveMetarial.Visible = false;
        //        pnlSearch.Visible = true;
        //        Enabled(false);
        //        txtPurchaseOrderNumber.Focus();
        //    }
            
            
        //}

        //protected void lnkSerach_Click(object sender, EventArgs e)
        //{
        //    Quotation = new QuotationDOM();
        //    lstQuotation = new List<QuotationDOM>();
        //    Quotation.StatusType = new MetaData();
        //    QuotationNumber = txtPurchaseOrderNumber.Text.Trim();
        //    lstQuotation = quotationBL.ReadSupplierQuotation(null, QuotationNumber);
        //    if (string.IsNullOrEmpty(txtPurchaseOrderNumber.Text.ToString()))
        //    {
        //        Alert("Please enter valid purchase order no", lnkSerach);
        //    }
        //    else if (lstQuotation.Count > 0 && (lstQuotation[0].StatusType.Id == Convert.ToInt32(StatusType.Generated)))
        //    {
        //        txtRMDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //        SetAllDefaultData(lstQuotation);
        //        pnlRecieveMetarial.Visible = true;
        //        Enabled(false);
        //        txtPurchaseOrderNumber.Text = string.Empty;
        //    }
        //    else if (lstQuotation.Count > 0 && lstQuotation[0].StatusType.Id <= 2)
        //    {
        //        Alert("Suppplier Purchase Order No. is not Approved or Generated!", lnkSerach);
               
        //    }
        //    else
        //    {
        //        Alert("Suppplier Purchase Order No. is not matched!", lnkSerach);
        //        pnlRecieveMetarial.Visible = false;
               
        //    }
        //}

        //protected void chkSelectAll_Click(object sender, EventArgs e)
        //{
        //    count = gvSupplier.Rows.Count;
        //    i = 0;
        //    foreach (GridViewRow row in gvSupplier.Rows)
        //    {
        //        chkSelect = (CheckBox)row.FindControl("chkSelect");

        //        if (chkSelect.Enabled == false)
        //        {
        //            i = i + 1;
        //            chkSelect.Enabled = true;
        //            chkSelect.Checked = false;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    foreach (GridViewRow row in gvSupplier.Rows)
        //    {
        //        chkSelect = (CheckBox)row.FindControl("chkSelect");
        //        if (i != count)
        //        {
        //            if (chkSelect.Enabled == true)
        //            {
        //                chkSelect.Checked = ((CheckBox)sender).Checked;
        //            }
        //        }
        //        else
        //        {
        //            chkSelect.Checked = false;
        //        }
        //    }
        //    if (i > 0)
        //    {
        //        lstItemTransaction = null;
        //        BindItemReturnAddGrid();
        //    }
        //}

        //protected void chkSelect_Click(object sender, EventArgs e)
        //{
        //    flag = false;
        //    CheckBox chkSelectAll = (CheckBox)gvSupplier.HeaderRow.FindControl("chkSelectAll");
        //    foreach (GridViewRow row in gvSupplier.Rows)
        //    {
        //        chkSelect = (CheckBox)row.FindControl("chkSelect");
        //        if (!chkSelect.Checked)
        //            flag = true;
        //    }
        //    if (flag == true)
        //    {
        //        chkSelectAll.Checked = false;
        //    }
        //    else
        //    {
        //        chkSelectAll.Checked = true;
        //    }
        //}

        //protected void btnAddSupplierItem_Click(object sender, EventArgs e)
        //{
        //    if (lstItemTransactionAdd == null)
        //    {
        //        lstItemTransactionAdd = new List<ItemTransaction>();

        //    }
        //    foreach (GridViewRow row in gvSupplier.Rows)
        //    {
        //        lblItem = (Label)row.FindControl("lblItem");
        //        lblSpecification = (Label)row.FindControl("lblSpecification");
        //        lblItemCategory = (Label)row.FindControl("lblItemCategory");
        //        lblBrand = (Label)row.FindControl("lblMake");
        //        lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
        //        lblItemRecieve = (Label)row.FindControl("lblItemRecieve");
        //        lblItemLeft = (Label)row.FindControl("lblItemLeft");
        //        lblMeasurement = (Label)row.FindControl("lblMeasurement");
        //        chkSelect = (CheckBox)row.FindControl("chkSelect");
        //        hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
        //        hdfItemId = (HiddenField)row.FindControl("hdfItemId");

        //        if (chkSelect.Checked == true && hdfSupplierPOMappingId != null)
        //        {
        //            if (chkSelect.Checked.Equals(true))
        //            {
        //                itemTransaction = new ItemTransaction();
        //                itemTransaction.MetaProperty = new MetaData();
        //                itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
        //                itemTransaction.Item = new Item();
        //                itemTransaction.Item.ModelSpecification = new ModelSpecification();
        //                itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
        //                itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
        //                itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfSupplierPOMappingId.Value);
        //                itemTransaction.Item.ItemId = Convert.ToInt32(hdfItemId.Value);
        //                itemTransaction.Item.ItemName = lblItem.Text.ToString();
        //                itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
        //                itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
        //                itemTransaction.Item.ModelSpecification.Brand = lblBrand.Text.ToString();
        //                itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString();
        //                itemTransaction.NumberOfUnit = Convert.ToDecimal(lblItemQuantity.Text.ToString());
        //                itemTransaction.UnitIssued =Convert.ToDecimal(lblItemRecieve.Text.ToString());
        //               // itemTransaction.ItemRequired = Convert.ToDecimal(lblItemLeft.Text.ToString());
        //                itemTransaction.UnitLeft = Convert.ToDecimal(lblItemLeft.Text.ToString());

        //                itemTransaction.CreatedBy = LoggedInUser.UserLoginId;

        //                lstItemTransactionAdd.Add(itemTransaction);
        //                chkSelect.Checked = false;
        //                chkSelect.Enabled = false;
        //            }
        //        }
        //    }
        //    if (lstItemTransactionAdd.Count != 0)
        //    {

        //        BindItemReturnAddGrid();
        //        Enabled(true);
        //        txtPurchaseOrderNumber.Text = string.Empty;
        //        // txtContractorQuotationNumber.Text = string.Empty;
        //    }
        //    else
        //    {
        //        Enabled(false);
        //        Alert("Please Check At Least One Activity Description", btnAddSupplierItem);
        //    }
        //    foreach (TableCell item in gvSupplier.HeaderRow.Cells)
        //    {
        //        CheckBox chbxSelectAll = (CheckBox)item.FindControl("chkSelectAll");
        //        if (chbxSelectAll.Checked == true)
        //        {
        //            chbxSelectAll.Checked = false;
        //        }
        //    }
        //}

        //protected void gvSupplierAdd_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    String s = string.Empty;
        //    s = lstItemTransactionAdd[index].Item.ItemName;
        //    if (e.CommandName == "cmdDelete")
        //    {

        //        lstItemTransactionAdd.RemoveAt(index);
        //        foreach (GridViewRow row in gvSupplier.Rows)
        //        {
        //            chkSelect = (CheckBox)row.FindControl("chkSelect");
        //            Label lblItem = (Label)row.FindControl("lblItem");
        //            if (lblItem.Text.Trim() == s)
        //            {

        //                chkSelect.Enabled = true;
        //                chkSelect.Checked = false;
        //                flag = true;
        //                break;
        //            }

        //        }
        //        if (lstItemTransactionAdd.Count == 0)
        //        {
        //            lstItemTransactionAdd = null;
        //        }
        //        BindItemReturnAddGrid();
        //    }
        //}

        //protected void gvSupplierAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (SupplierRecieveMaterialId != 0)
        //    {
        //        if (lstItemTransactionAdd != null)
        //        {
        //            foreach (ItemTransaction Item in lstItemTransactionAdd)
        //            {
        //                foreach (ItemTransaction Items in lstItemTransaction)
        //                {
        //                    if (Item.DeliverySchedule.Id == Items.DeliverySchedule.Id)
        //                    {
        //                        Item.UnitIssued = Items.UnitIssued;
        //                        Item.UnitLeft = Items.UnitLeft;
        //                        Item.CreatedBy = LoggedInUser.UserLoginId;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //#endregion

        //#region Private Method
        //private void SetAllDefaultData(List<QuotationDOM> lst)
        //{
        //    BindText(lst);
        //    BindItemReturnGrid(lst);
        //}

        //private void BindItemReturnGrid(List<QuotationDOM> lst)
        //{
        //    quotationBL = new QuotationBL();
        //    List<ItemTransaction>  lstItemTransaction = new List<ItemTransaction>();
        //    if (lst.Count > 0)
        //    {
        //        lstItemTransaction = quotationBL.ReadSupplierQuotationMapping(lst[0].SupplierQuotationId);

        //        gvSupplier.DataSource = lstItemTransaction;
        //        gvSupplier.DataBind();
        //    }
        //}

        //private void BindItemReturnAddGrid()
        //{
        //    gvSupplierAdd.DataSource = lstItemTransactionAdd;
        //    gvSupplierAdd.DataBind();
        //}

        //private void BindText(List<QuotationDOM> lst)
        //{
        //    QuotationId = lst[0].SupplierQuotationId;
        //    lblPurchaseOrderNumber.Text = lst[0].SupplierQuotationNumber;
        //    lblPurchaseOrderDate.Text = (lst[0].OrderDate).ToString("dd-MMM-yyyy");
        //   // hdfSupplierId.Value = lst[0].SupplierQuotationId.ToString();

        //}

        //private void Enabled(Boolean Condition)
        //{
        //    btnSaveDraft.Visible = Condition;
        //    btnReset.Visible = Condition;
        //}
        //#endregion

        //#region Public Property
        //public int SupplierRecieveMaterialId
        //{
        //    get
        //    {
        //        return (Int32)ViewState["SupplierRecieveMaterialId"];
        //    }
        //    set
        //    {
        //        ViewState["SupplierRecieveMaterialId"] = value;
        //    }
        //}
        //public String QuotationNumber
        //{
        //    get
        //    {
        //        try
        //        {
        //            return (String)ViewState["QuotationNumber"];
        //        }
        //        catch
        //        {

        //            return String.Empty;
        //        }
        //    }
        //    set
        //    {
        //        ViewState["QuotationNumber"] = value;
        //    }
        //}

        //public Int32 QuotationId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return (Int32)ViewState["QuotationId"];
        //        }
        //        catch
        //        {

        //            return 0;
        //        }
        //    }
        //    set
        //    {
        //        ViewState["QuotationId"] = value;
        //    }
        //}

        //public List<ItemTransaction> lstItemTransactionAdd
        //{
        //    get
        //    {
        //        return (List<ItemTransaction>)ViewState["lstItemTransactionAdd"];
        //    }
        //    set
        //    {
        //        ViewState["lstItemTransactionAdd"] = value;
        //    }
        //}
        //public List<ItemTransaction> lstItemTransaction
        //{
        //    get
        //    {
        //        return (List<ItemTransaction>)ViewState["lstItemTransaction"];
        //    }
        //    set
        //    {
        //        ViewState["lstItemTransaction"] = value;
        //    }
        //}
        //#endregion            

    }
}