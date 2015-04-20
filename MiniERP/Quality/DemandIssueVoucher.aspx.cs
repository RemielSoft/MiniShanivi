using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace MiniERP.IssueMaterial
{
    public partial class DemandIssueVoucher : BasePage
    {
        //#region private Global Variables
        //ItemTransaction itemTransaction = null;
        //IssueDemandVoucherBL issueDemandVoucherBL = null;
        //List<QuotationDOM> lstQuotationDOM = null;
        //List<IssueDemandVoucherDOM> lstQutn = null;
        //MetaData metaData = null;
        //QuotationDOM quotationDOM = null;
        //QuotationBL quotationBL = null;
        //IssueDemandVoucherDOM issueDemandVoucherDOM = null;

        //Boolean flag = false;
        //string strInvalid = string.Empty;
        //int cnt = 0;
        //decimal dec = 0;
        //decimal BigNo = 0;
        //int i = 0;
        //int j = 0;
        ////int IDVNId = 0;
        //Label lblActivityDiscription = null;
        //Label lblItemCategory = null;
        //Label lblItem = null;
        //Label lblSpecification = null;
        //Label lblBrand = null;
        //Label lblNOF = null;
        //Label lblUnitIssued = null;
        //Label lblUnitLeft = null;
        //Label lblIndex = null;
        //CheckBox chkbxQuotationDetails = null;
        //CheckBox chbxSelectAll = null;
        //HiddenField hdfContractorPOMappingId = null;
        //TextBox box = null;
        //#endregion

        //#region Protected Methods
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        txtMaterialDemandDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //        if (Request.QueryString["IssueDemandVoucherId"] != null)
        //        {
        //            quotationBL = new QuotationBL();
        //            issueDemandVoucherBL = new IssueDemandVoucherBL();
        //            lstQutn = new List<IssueDemandVoucherDOM>();
        //            lstQuotationDOM = new List<QuotationDOM>();
        //            lstItemTransaction = new List<ItemTransaction>();
        //            IDVNId = 0;
        //            IDVNId = Convert.ToInt32(Request.QueryString["IssueDemandVoucherId"]);
        //            lstQutn = issueDemandVoucherBL.ReadMaterialIssueDemandVoucher(IDVNId, null);
        //            lstItemTransaction = issueDemandVoucherBL.ReadIssueDemandMapping(IDVNId, null);
        //            lstQuotationDOM = quotationBL.ReadContractorQuotation(null, lstQutn[0].Quotation.ContractQuotationNumber.Trim());
        //            BindGridQuotationDetails(lstQuotationDOM);
        //            BindUpdateText(lstQutn);
        //            BindGridIssueDemandVoucher();
        //            pnlSearch.Visible = false;
        //            pnlIDV.Visible = true;
        //            Enabled(true);
        //        }
        //        else
        //        {
        //            IDVNId = 0;
        //            pnlIDV.Visible = false;
        //            Enabled(false);
        //            txtContractorQuotationNumber.Focus();
        //        }

        //    }
        //}

        //protected void btnSearchContractorQuotationNumber_Click(object sender, EventArgs e)
        //{
        //    ctrl_UploadDocument.EmptyDocumentList();
        //    quotationBL = new QuotationBL();
        //    quotationDOM = new QuotationDOM();
        //    lstQuotationDOM = new List<QuotationDOM>();
        //    quotationDOM.StatusType = new MetaData();
        //    lstQuotationDOM = quotationBL.ReadContractorQuotation(null, txtContractorQuotationNumber.Text.Trim());

        //    if (lstQuotationDOM.Count > 0 && lstQuotationDOM[0].StatusType.Id == Convert.ToInt32(StatusType.Generated))
        //    {
        //        lstItemTransaction = null;
        //        txtRemarks.Text = string.Empty;
        //        ltrl_err_msg.Text = string.Empty;
        //        txtMaterialDemandDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //        BindGridIssueDemandVoucher();
        //        SetAllDefaultData(lstQuotationDOM);
        //        pnlIDV.Visible = true;
        //        Enabled(false);

        //        txtContractorQuotationNumber.Text = String.Empty;
        //    }
        //    else if (lstQuotationDOM.Count > 0 && lstQuotationDOM[0].StatusType.Id <= 2)
        //    {
        //        Alert("Quotation is not approved!", btnSearchContractorQuotationNumber);
        //    }
        //    else
        //    {
        //        Alert("No quotation is matched!", btnSearchContractorQuotationNumber);
        //        pnlIDV.Visible = false;
        //    }
        //}
        //protected void btnAddQuotationDetails_Click(object sender, EventArgs e)
        //{
        //    if (lstItemTransaction == null)
        //    {
        //        lstItemTransaction = new List<ItemTransaction>();
        //    }
        //    foreach (GridViewRow row in gvQuotationDetails.Rows)
        //    {
        //        lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
        //        lblItemCategory = (Label)row.FindControl("lblItemCategory");
        //        lblItem = (Label)row.FindControl("lblItem");
        //        lblSpecification = (Label)row.FindControl("lblSpecification");
        //        lblBrand = (Label)row.FindControl("lblBrand");
        //        lblNOF = (Label)row.FindControl("lblNOF");
        //        lblUnitIssued = (Label)row.FindControl("lblUnitIssued");
        //        lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
        //        chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        //        hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");

        //        if (chkbxQuotationDetails.Checked == true && hdfContractorPOMappingId != null)
        //        {
        //            if (chkbxQuotationDetails.Checked.Equals(true))
        //            {
        //                itemTransaction = new ItemTransaction();
        //                itemTransaction.MetaProperty = new MetaData();
        //                itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
        //                itemTransaction.Item = new Item();
        //                itemTransaction.Item.ModelSpecification = new ModelSpecification();
        //                itemTransaction.Item.ModelSpecification.Category = new ItemCategory();

        //                itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfContractorPOMappingId.Value);
        //                itemTransaction.DeliverySchedule.ActivityDescription = lblActivityDiscription.Text.ToString();
        //                itemTransaction.Item.ItemName = lblItem.Text.ToString();
        //                itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
        //                itemTransaction.Item.ModelSpecification.Brand = lblBrand.Text.ToString();
        //                itemTransaction.NumberOfUnit = Convert.ToDecimal(lblNOF.Text.ToString());
        //                itemTransaction.UnitIssued = Convert.ToDecimal(lblUnitIssued.Text.ToString());
        //                itemTransaction.UnitLeft = Convert.ToDecimal(lblUnitLeft.Text.ToString());
        //                itemTransaction.ItemRequired = Convert.ToDecimal(lblUnitLeft.Text.ToString());
        //                itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
        //                itemTransaction.CreatedBy = LoggedInUser.UserLoginId;
        //                lstItemTransaction.Add(itemTransaction);
        //                chkbxQuotationDetails.Checked = false;
        //                chkbxQuotationDetails.Enabled = false;
        //            }
        //        }

        //    }
        //    if (lstItemTransaction.Count != 0)
        //    {

        //        BindGridIssueDemandVoucher();
        //        Enabled(true);
        //        txtContractorQuotationNumber.Text = string.Empty;
        //    }
        //    else
        //    {
        //        Enabled(false);
        //        Alert("Please Check At Least One Activity Description", btnAddQuotationDetails);
        //    }
        //    foreach (TableCell item in gvQuotationDetails.HeaderRow.Cells)
        //    {
        //        CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
        //        if (chbxSelectAll.Checked == true)
        //        {
        //            chbxSelectAll.Checked = false;
        //        }
        //    }
        //    // BindGridQuotationDetails(lstQuotationDOM);

        //}
        //protected void chbxSelectAll_Click(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in gvQuotationDetails.Rows)
        //    {
        //        chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        //        if (chkbxQuotationDetails.Enabled == true)
        //        {
        //            chkbxQuotationDetails.Checked = ((CheckBox)sender).Checked;
        //        }
        //    }

        //}
        //protected void chkbxQuotationDetails_Click(object sender, EventArgs e)
        //{
        //    flag = false;
        //    chbxSelectAll = (CheckBox)gvQuotationDetails.HeaderRow.FindControl("chbxSelectAll");
        //    foreach (GridViewRow row in gvQuotationDetails.Rows)
        //    {
        //        chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        //        if (!chkbxQuotationDetails.Checked)
        //            flag = true;
        //    }
        //    if (flag == true)
        //    {
        //        chbxSelectAll.Checked = false;
        //    }
        //    else
        //    {
        //        chbxSelectAll.Checked = true;
        //    }
        //}
        //protected void gvQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //if (e.Row.RowType == DataControlRowType.Header)
        //    //{
        //    //    foreach (GridViewRow row in gvQuotationDetails.Rows)
        //    //    {
        //    //        chbxSelectAll = (CheckBox)row.FindControl("chbxSelectAll");
        //    //        chbxSelectAll.Checked = false;
        //    //    }                
        //    //}
        //    foreach (GridViewRow row in gvQuotationDetails.Rows)
        //    {
        //        chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        //        lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
        //        hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
        //        if (IDVNId != 0 && lstItemTransaction != null)
        //        {
        //            foreach (ItemTransaction item in lstItemTransaction)
        //            {
        //                if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0 || Convert.ToInt32(hdfContractorPOMappingId.Value) == item.DeliverySchedule.Id)
        //                {
        //                    chkbxQuotationDetails.Checked = false;
        //                    chkbxQuotationDetails.Enabled = false;
        //                }
        //            }
        //        }
        //        else if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0)
        //        {
        //            chkbxQuotationDetails.Checked = false;
        //            chkbxQuotationDetails.Enabled = false;
        //        }
        //    }
        //    //}
        //}
        //protected void gvIssueDemandVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (IDVNId != 0)
        //    {
        //        if (lstItemTransaction != null)
        //        {
        //            foreach (ItemTransaction Item in lstItemTransaction)
        //            {
        //                foreach (ItemTransaction Items in lstPreItemTransaction)
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

        //protected void gvIssueDemandVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    String s = string.Empty;
        //    s = lstItemTransaction[index].DeliverySchedule.ActivityDescription.ToString();
        //    if (e.CommandName == "cmdDelete")
        //    {
        //        lstItemTransaction.RemoveAt(index);
        //        foreach (GridViewRow row in gvQuotationDetails.Rows)
        //        {
        //            chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
        //            lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
        //            if (lblActivityDiscription.Text.Trim() == s)
        //            {
        //                chkbxQuotationDetails.Enabled = true;
        //                chkbxQuotationDetails.Checked = false;
        //                flag = true;
        //                break;
        //            }
        //        }
        //        if (lstItemTransaction.Count == 0)
        //        {
        //            lstItemTransaction = null;
        //            ltrl_err_msg.Text = string.Empty;
        //        }
        //        BindGridIssueDemandVoucher();
        //    }
        //}
        //protected void btnSaveDraft_Click(object sender, EventArgs e)
        //{
        //    ltrl_err_msg.Text = String.Empty;
        //    //quotationDOM = new QuotationDOM();
        //    issueDemandVoucherDOM = new IssueDemandVoucherDOM();
        //    issueDemandVoucherDOM = GetIssueDemandVoucherDetails();
        //    if (!string.IsNullOrEmpty(strInvalid))
        //    {
        //        ltrl_err_msg.Text = strInvalid;
        //    }
        //    else
        //    {
        //        if (IDVNId > 0)
        //        {
        //            metaData = CreateIssueDemandVoucher(issueDemandVoucherDOM, IDVNId);
        //        }
        //        else
        //        {
        //            metaData = CreateIssueDemandVoucher(issueDemandVoucherDOM, null);
        //        }
        //        if (metaData.Id > 0)
        //        {
        //            lstItemTransaction = null;
        //            lstPreItemTransaction = null;
        //            lstQuotationDOM = null;
        //            lstQutn = null;
        //            if (IDVNId > 0)
        //            {
        //                Alert("Issue Demand Voucher No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewDemandIssueVoucher.aspx");
        //                pnlSearch.Visible = false;
        //            }
        //            else
        //            {
        //                Alert("Issue Demand Voucher No: " + metaData.Name + " Created Successfully", btnSaveDraft);
        //                pnlSearch.Visible = true;
        //            }
        //            BindGridIssueDemandVoucher();
        //            pnlIDV.Visible = false;
        //        }
        //        else
        //        {
        //            Alert(GlobalConstants.C_DUPLICATE_MESSAGE, btnSaveDraft);
        //        }
        //    }
        //}
        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    Reset();
        //}
        ////protected void btnCancel_Click(object sender, EventArgs e)
        ////{            
        ////    pnlIDV.Visible = false;
        ////    txtRemarks.Text = string.Empty;
        ////    lstItemTransaction = null;
        ////    BindGridIssueDemandVoucher();
        ////    txtMaterialDemandDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        ////}
        //#endregion

        //#region private Methods
        //private void BindGridIssueDemandVoucher()
        //{
        //    gvIssueDemandVoucher.DataSource = lstItemTransaction;
        //    gvIssueDemandVoucher.DataBind();
        //}
        //private void Reset()
        //{
        //    i = 0;
        //    txtRemarks.Text = string.Empty;
        //    ltrl_err_msg.Text = string.Empty;
        //    txtMaterialDemandDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //    for (i = 0; i < lstItemTransaction.Count; i++)
        //    {
        //        lstItemTransaction[i].ItemRequired = 0;
        //    }
        //    BindGridIssueDemandVoucher();
        //}
        //private Decimal TryToParse(string Value)
        //{
        //    dec = 0;
        //    Decimal.TryParse(Value, out dec);
        //    return dec;
        //}
        ////private void SetAllUpdateData(List<QuotationDOM> lst)
        ////{
        ////    BindUpdateText(lst);
        ////}
        //private void SetAllDefaultData(List<QuotationDOM> lst)
        //{
        //    BindText(lst);
        //    BindGridQuotationDetails(lst);
        //}
        //private void BindUpdateText(List<IssueDemandVoucherDOM> lst)
        //{
        //    ctrl_UploadDocument.GetDocumentData(lst[0].Quotation.UploadDocumentId);
        //    lblContractorQuotationNumber.Text = lst[0].Quotation.ContractQuotationNumber.ToString();
        //    lblContractorQuotationDate.Text = lst[0].Quotation.QuotationDate.ToString("dd-MMM-yyyy");
        //    lblContractorName.Text = lst[0].Quotation.ContractorName.ToString();
        //    lblContractNumber.Text = lst[0].Quotation.ContractNumber.ToString();
        //    hdfContractorId.Value = lst[0].Quotation.ContractorId.ToString();
        //    txtMaterialDemandDate.Text = lst[0].MaterialDemandDate.ToString("dd-MMM-yyyy");
        //    txtRemarks.Text = lst[0].Remarks.ToString();
        //}
        ////private void SetValidationExp()
        ////{
        ////    revItemRequired.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
        ////    revName.ErrorMessage = "Department Name Should Be In Characters";            
        ////}
        //private void BindText(List<QuotationDOM> lst)
        //{
        //    lblContractorQuotationNumber.Text = lst[0].ContractQuotationNumber.ToString();
        //    lblContractorQuotationDate.Text = lst[0].OrderDate.ToString("dd-MMM-yyyy");
        //    lblContractorName.Text = lst[0].ContractorName.ToString();
        //    lblContractNumber.Text = lst[0].ContractNumber.ToString();
        //    hdfContractorId.Value = lst[0].ContractorId.ToString();
        //}
        //private void BindGridQuotationDetails(List<QuotationDOM> lst)
        //{
        //    quotationBL = new QuotationBL();
        //    lstPreItemTransaction = new List<ItemTransaction>();
        //    if (lst.Count > 0)
        //    {
        //        lstPreItemTransaction = quotationBL.ReadContractorQuotationMapping(lst[0].ContractorQuotationId);
        //        gvQuotationDetails.DataSource = lstPreItemTransaction;
        //        gvQuotationDetails.DataBind();
        //    }
        //}
        //public MetaData CreateIssueDemandVoucher(IssueDemandVoucherDOM issueDemandVoucherDOM, Int32? IDVID)
        //{
        //    if (lstItemTransaction != null)
        //    {
        //        metaData = new MetaData();
        //        issueDemandVoucherBL = new IssueDemandVoucherBL();
        //        metaData = issueDemandVoucherBL.CreateIssueDemandVoucher(issueDemandVoucherDOM, IDVID);
        //    }
        //    return metaData;
        //}
        //private void Enabled(Boolean Condition)
        //{
        //    btnSaveDraft.Visible = Condition;
        //    btnReset.Visible = Condition;
        //    //btnCancel.Visible = Condition;
        //}
        //private IssueDemandVoucherDOM GetIssueDemandVoucherDetails()
        //{

        //    strInvalid = string.Empty;
        //    flag = false;
        //    j = 0;
        //    quotationDOM = new QuotationDOM();
        //    issueDemandVoucherBL = new IssueDemandVoucherBL();
        //    issueDemandVoucherDOM = new IssueDemandVoucherDOM();
        //    issueDemandVoucherDOM.Quotation = new QuotationDOM();
        //    issueDemandVoucherDOM.Quotation.StatusType = new MetaData();
        //    if (IDVNId > 0)
        //    {
        //        issueDemandVoucherDOM.IssueDemandVoucherId = IDVNId;
        //    }
        //    issueDemandVoucherDOM.Quotation.UploadDocumentId = DocumentStackId;
        //    issueDemandVoucherDOM.Quotation.ContractQuotationNumber = lblContractorQuotationNumber.Text;
        //    issueDemandVoucherDOM.Quotation.ContractorId = Convert.ToInt32(hdfContractorId.Value);
        //    issueDemandVoucherDOM.Quotation.ContractorName = lblContractorName.Text;
        //    issueDemandVoucherDOM.Quotation.ContractNumber = lblContractNumber.Text;
        //    issueDemandVoucherDOM.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
        //    issueDemandVoucherDOM.Quotation.OrderDate = Convert.ToDateTime(lblContractorQuotationDate.Text);

        //    issueDemandVoucherDOM.MaterialDemandDate = Convert.ToDateTime(txtMaterialDemandDate.Text.Trim());
        //    issueDemandVoucherDOM.Remarks = txtRemarks.Text.Trim();
        //    issueDemandVoucherDOM.CreatedBy = LoggedInUser.UserLoginId;
        //    for (i = 0; i < gvIssueDemandVoucher.Rows.Count; i++)
        //    {
        //        BigNo = 0;
        //        dec = 0;
        //        BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].ItemRequired);
        //        TextBox box = (TextBox)gvIssueDemandVoucher.Rows[i].Cells[9].FindControl("txtItemRequired");
        //        lblIndex = (Label)gvIssueDemandVoucher.Rows[i].Cells[0].FindControl("lblIndex");
        //        dec = TryToParse(box.Text);
        //        if (dec > 0)
        //        {
        //            cnt = 0;
        //            cnt = NumberDecimalPlaces(dec);
        //            if (IDVNId > 0)
        //            {
        //                if (cnt > 2 || dec > BigNo)
        //                {
        //                    if (j > 0)
        //                    {
        //                        strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
        //                        j++;
        //                    }
        //                    else
        //                    {
        //                        strInvalid = strInvalid + lblIndex.Text.Trim();
        //                        j++;
        //                    }
        //                    flag = true;
        //                }
        //            }
        //            else if (cnt > 2 || dec > lstItemTransaction[i].UnitLeft)
        //            {
        //                if (j > 0)
        //                {
        //                    strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
        //                    j++;
        //                }
        //                else
        //                {
        //                    strInvalid = strInvalid + lblIndex.Text.Trim();
        //                    j++;
        //                }
        //                flag = true;
        //            }
        //        }
        //        else
        //        {
        //            if (j > 0)
        //            {
        //                strInvalid = strInvalid + ",   " + " " + lblIndex.Text.Trim();
        //                j++;
        //            }
        //            else
        //            {
        //                strInvalid = strInvalid + lblIndex.Text.Trim();
        //                j++;
        //            }
        //            flag = true;
        //        }
        //        if (string.IsNullOrEmpty(strInvalid))
        //        {
        //            lstItemTransaction[i].ItemRequired = Convert.ToDecimal(box.Text);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(strInvalid))
        //    {
        //        strInvalid = "Unit Required allows only numeric value up to 2 decimal places & less than or equal to unit left at Index: " + strInvalid;
        //    }
        //    else
        //    {
        //        issueDemandVoucherDOM.Quotation.ItemTransaction = lstItemTransaction;
        //    }
        //    return issueDemandVoucherDOM;
        //}
        ////private decimal GetGreaterNumber(Decimal a, Decimal b)
        ////{
        ////    if (a > b)
        ////    {
        ////        return a;
        ////    }
        ////    else
        ////    {
        ////        return b;
        ////    }
        ////}
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
        //#endregion

        //#region private properties

        //private List<ItemTransaction> lstItemTransaction
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

        //private List<ItemTransaction> lstPreItemTransaction
        //{
        //    get
        //    {
        //        return (List<ItemTransaction>)ViewState["lstPreItemTransaction"];
        //    }
        //    set
        //    {
        //        ViewState["lstPreItemTransaction"] = value;
        //    }
        //}
        //private int IDVNId
        //{
        //    get
        //    {
        //        return (Int32)ViewState["IDVNId"];
        //    }
        //    set
        //    {
        //        ViewState["IDVNId"] = value;
        //    }
        //}
        ////private List<QuotationDOM> lstQuotationDOM
        ////{
        ////    get
        ////    {
        ////        return (List<QuotationDOM>)ViewState["lstQuotationDOM"];
        ////    }
        ////    set
        ////    {
        ////        ViewState["lstQuotationDOM"] = value;
        ////    }
        ////}
        //#endregion

    }
}