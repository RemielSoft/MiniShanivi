using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniERP.Shared;
using DocumentObjectModel;
using BusinessAccessLayer;
using BusinessAccessLayer.Quality;
using System.Drawing;

namespace MiniERP.IssueMaterial
{
    public partial class IssuingMaterial : BasePage
    {
        //#region private Global Variables
        //IssueDemandVoucherDOM issueDemandVoucherDOM = null;
        //IssueDemandVoucherBL issueDemandVoucherBL = null;
        //List<IssueDemandVoucherDOM> lstIssueDemandVoucherDOM = null;
        //List<IssueMaterialDOM> lstIssueMaterialDOM = null;
        //IssueMaterialDOM issueMaterialDOM = null;
        //IssueMaterialBL issueMaterialBL = null;
        //ItemTransaction itemTransaction = null;
        //MetaData metaData = null;        
        //int index = 0;
        //int i = 0;
        //int j = 0;
        //string strInvalid = string.Empty;
        //decimal dec = 0;
        //int cnt = 0;        
        //String s = string.Empty;        
        //decimal BigNo = 0;

        //Label lblActivityDiscription = null;
        //Label lblItemCategory = null;
        //Label lblItem = null;
        //Label lblSpecification = null;
        //Label lblBrand = null;
        //Label lblNOF = null;
        //Label lblUnitIssued = null;
        //Label lblUnitLeft = null;
        //Label lblIndex = null;
        //Label lblUnitDemanded = null;
        //CheckBox chkbxDemandVoucherDetails = null;
        //CheckBox chbxSelectAll = null;
        //HiddenField hdfContractorPOMappingId = null;        
        //TextBox txtUnitIssue = null;
        //bool track = false;
        //#endregion

        //#region Protected Methods
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {                
        //        txtIssueDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //        if (Request.QueryString["IssueMaterialId"] != null)
        //        {
        //            IssueMaterialId = 0;
        //            IssueMaterialId = Convert.ToInt32(Request.QueryString["IssueMaterialId"]);
        //            EditData(IssueMaterialId);                    
        //        }
        //        else
        //        {
        //            IssueMaterialId = 0;
        //            pnlIssueMaterial.Visible = false;
        //            Enabled(false);
        //            txtDemandNoteNumber.Focus();
        //        }
                
        //    }
            
        //}

        //protected void LinkSearch_Click(object sender, EventArgs e)
        //{
        //    //For Empty Already Exist Data
        //    ctrl_UploadDocument.EmptyDocumentList();
        //    //Ended
        //    issueDemandVoucherBL = new IssueDemandVoucherBL();
        //    lstPreItemTransaction = new List<ItemTransaction>();
        //    lstPreItemTransaction = issueDemandVoucherBL.ReadIssueDemandMapping(null, txtDemandNoteNumber.Text.ToString());
        //    lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
        //    lstIssueDemandVoucherDOM = issueDemandVoucherBL.ReadMaterialIssueDemandVoucher(null, txtDemandNoteNumber.Text.ToString());
        //    if (lstIssueDemandVoucherDOM.Count > 0)
        //    {
        //        BindSearchText(lstIssueDemandVoucherDOM);
        //    }
        //    if (lstPreItemTransaction.Count > 0)
        //    {
        //        BindGridDemandMaterial();
        //        txtDemandNoteNumber.Text = String.Empty;
        //        pnlIssueMaterial.Visible = true;
        //    }
        //    else
        //    {
        //        Alert("No Issue Demand Voucher Matched!", LinkSearch);
        //    }
        //}
        //protected void chbxSelectAll_Click(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in gvDemandMaterial.Rows)
        //    {
        //        chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
        //        if (chkbxDemandVoucherDetails.Enabled == true)
        //        {
        //            chkbxDemandVoucherDetails.Checked = ((CheckBox)sender).Checked;
        //        }
        //    }
        //}
        //protected void on_chbx_Activity_Click(object sender, EventArgs e)
        //{
        //    track = false;
        //    chbxSelectAll = (CheckBox)gvDemandMaterial.HeaderRow.FindControl("chbxSelectAll");
        //    foreach (GridViewRow row in gvDemandMaterial.Rows)
        //    {
        //        chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
        //        if (!chkbxDemandVoucherDetails.Checked)
        //            track = true;
        //    }
        //    if (track == true)
        //    {
        //        chbxSelectAll.Checked = false;
        //    }
        //    else
        //    {
        //        chbxSelectAll.Checked = true;
        //    }
        //}
        //protected void btnAddDemandMaterial_Click(object sender, EventArgs e)
        //{
        //    if (lstItemTransaction == null)
        //    {
        //        lstItemTransaction = new List<ItemTransaction>();
        //    }
        //    foreach (GridViewRow row in gvDemandMaterial.Rows)
        //    {
        //        lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
        //        lblItemCategory = (Label)row.FindControl("lblItemCategory");
        //        lblItem = (Label)row.FindControl("lblItem");
        //        lblSpecification = (Label)row.FindControl("lblSpecification");
        //        lblBrand = (Label)row.FindControl("lblBrand");
        //        lblNOF = (Label)row.FindControl("lblNOF");
        //        lblUnitDemanded = (Label)row.FindControl("lblUnitDemanded");
        //        lblUnitIssued = (Label)row.FindControl("lblUnitIssued");
        //        lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
        //        chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
        //        hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");

        //        if (chkbxDemandVoucherDetails.Checked == true && hdfContractorPOMappingId != null)
        //        {
        //            if (chkbxDemandVoucherDetails.Checked.Equals(true))
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
        //                itemTransaction.UnitDemanded = Convert.ToDecimal(lblUnitDemanded.Text.ToString());
        //                itemTransaction.UnitLeft = Convert.ToDecimal(lblUnitLeft.Text.ToString());
        //                itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
        //                itemTransaction.CreatedBy = LoggedInUser.UserLoginId;
        //                lstItemTransaction.Add(itemTransaction);
        //                chkbxDemandVoucherDetails.Checked = false;
        //                chkbxDemandVoucherDetails.Enabled = false;
        //            }
        //        }

        //    }
        //    if (lstItemTransaction.Count != 0)
        //    {

        //        BindGridIssueMaterial();
        //        Enabled(true);
        //        txtDemandNoteNumber.Text = string.Empty;
        //    }
        //    else
        //    {
        //        Enabled(false);
        //        Alert("Please Check At Least One Activity Description", btnAddDemandMaterial);
        //    }
        //    foreach (TableCell item in gvDemandMaterial.HeaderRow.Cells)
        //    {
        //        CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
        //        if (chbxSelectAll.Checked == true)
        //        {
        //            chbxSelectAll.Checked = false;
        //        }
        //    }
        //}
        //protected void gvIssueMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    index = 0;
        //    index = Convert.ToInt32(e.CommandArgument);
        //    String s = string.Empty;
        //    s = lstItemTransaction[index].DeliverySchedule.ActivityDescription.ToString();
        //    if (e.CommandName == "cmdDelete")
        //    {
        //        lstItemTransaction.RemoveAt(index);
        //        foreach (GridViewRow row in gvDemandMaterial.Rows)
        //        {
        //            chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
        //            lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
        //            if (lblActivityDiscription.Text.Trim() == s)
        //            {
        //                chkbxDemandVoucherDetails.Enabled = true;
        //                chkbxDemandVoucherDetails.Checked = false;
        //                //flag = true;
        //                break;
        //            }
        //        }
        //        if (lstItemTransaction.Count == 0)
        //        {
        //            lstItemTransaction = null;
        //            ltrl_err_msg.Text = string.Empty;
        //        }
        //        BindGridIssueMaterial();
        //    }
        //}
        //protected void Page_LoadComplete(object sender, EventArgs e)
        //{
        //    ctrl_UploadDocument.BindDocument();
        //}
        //protected void btnSaveDraft_Click(object sender, EventArgs e)
        //{
        //    ltrl_err_msg.Text = string.Empty;
        //    metaData = new MetaData();
        //    issueMaterialDOM = new IssueMaterialDOM();
        //    issueMaterialDOM = GetIssueMaterialDetails();
        //    if (!string.IsNullOrEmpty(strInvalid))
        //    {
        //        ltrl_err_msg.Text = strInvalid;
        //    }
        //    else
        //    {
        //        if (IssueMaterialId > 0)
        //        {
        //            metaData = CreateIssueMaterial(issueMaterialDOM, IssueMaterialId);                    
        //        }
        //        else
        //        {
        //            metaData = CreateIssueMaterial(issueMaterialDOM, null);
        //        }
        //        if (metaData.Id > 0)
        //        {
        //            ctrl_UploadDocument.CreateDocumentMapping();
        //            if (IssueMaterialId > 0)
        //            {
        //                Alert("Issue Material No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewIssueMaterial.aspx");
        //            }
        //            else
        //            {
        //                Alert("Issue Material No: " + metaData.Name + " Created Successfully", btnSaveDraft, "IssuingMaterial.aspx");
        //            }
        //            ltrl_err_msg.Text = string.Empty;
        //            lstItemTransaction = null;
        //            BindGridIssueMaterial();
        //        }
        //    }
        //}
        //protected void gvDemandMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
            
        //    foreach (GridViewRow row in gvDemandMaterial.Rows)
        //    {
        //        chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
        //        lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
        //        hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
        //        if (IssueMaterialId != 0 && lstItemTransaction != null)
        //        {
        //            foreach (ItemTransaction item in lstItemTransaction)
        //            {
        //                if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0 || Convert.ToInt32(hdfContractorPOMappingId.Value) == item.DeliverySchedule.Id)
        //                {
        //                    chkbxDemandVoucherDetails.Checked = false;
        //                    chkbxDemandVoucherDetails.Enabled = false;
        //                }
        //            }
        //        }
        //        else if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0)
        //        {
        //            chkbxDemandVoucherDetails.Checked = false;
        //            chkbxDemandVoucherDetails.Enabled = false;
        //        }
        //    }
        //}
        //protected void gvIssueMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (IssueMaterialId != 0)
        //    {
        //        if (lstItemTransaction != null)
        //        {
        //            foreach (ItemTransaction Item in lstItemTransaction)
        //            {
        //                foreach (ItemTransaction Items in lstPreItemTransaction)
        //                {
        //                    if (Item.DeliverySchedule.Id == Items.DeliverySchedule.Id)
        //                    {
        //                        Item.UnitLeft = Items.UnitLeft;
        //                        //Item.UnitLeft = Items.UnitForBilled;
        //                        Item.CreatedBy = LoggedInUser.UserLoginId;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    txtRemarks.Text = string.Empty;
        //}
        //#endregion

        //#region private Methods
        //private void BindUpdateText(List<IssueMaterialDOM> lstIssueMaterialDOM)
        //{            
        //    lblContractorWorkOrder.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractQuotationNumber.ToString();
        //    lblIssueDemandVoucher.Text = lstIssueMaterialDOM[0].DemandVoucher.IssueDemandVoucherNumber.ToString();
        //    hdnDemandVoucherId.Value = lstIssueMaterialDOM[0].DemandVoucher.IssueDemandVoucherId.ToString();
        //    hdfContractorId.Value = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractorId.ToString();
        //    lblContractorName.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractorName.ToString();
        //    lblContractNumber.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractNumber.ToString();
        //    lblIssueDemandDate.Text = lstIssueMaterialDOM[0].DemandVoucher.MaterialDemandDate.ToString("dd-MMM-yyyy");
        //    txtIssueDate.Text = lstIssueMaterialDOM[0].IssueMaterialDate.ToString("dd-MMM-yyyy");
        //    txtRemarks.Text = lstIssueMaterialDOM[0].DemandVoucher.Remarks.ToString();
        //    ctrl_UploadDocument.GetDocumentData(lstIssueMaterialDOM[0].DemandVoucher.Quotation.UploadDocumentId);           
        //    hdnStatusTypeId.Value = lstIssueMaterialDOM[0].DemandVoucher.Quotation.StatusType.Id.ToString();
        //}
        //private MetaData CreateIssueMaterial(IssueMaterialDOM issueMaterialDOM, Int32? IssueMaterialId)
        //{
        //    if (lstItemTransaction != null)
        //    {
        //        metaData = new MetaData();                
        //        issueMaterialBL = new IssueMaterialBL();
        //        metaData = issueMaterialBL.CreateIssueMaterial(issueMaterialDOM, IssueMaterialId);
        //    }
        //    return metaData;
        //}
        //private void EditData(Int32 IssueMaterialId)
        //{
        //    lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
        //    issueDemandVoucherBL = new IssueDemandVoucherBL();
        //    lstPreItemTransaction = new List<ItemTransaction>();
        //    lstItemTransaction = new List<ItemTransaction>();
        //    lstIssueMaterialDOM = new List<IssueMaterialDOM>();
        //    issueMaterialBL = new IssueMaterialBL();
        //    lstIssueMaterialDOM = issueMaterialBL.ReadIssueMaterial(IssueMaterialId, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, null);
        //    lstItemTransaction = issueMaterialBL.ReadIssueMaterialMapping(IssueMaterialId);
        //    lstPreItemTransaction = issueDemandVoucherBL.ReadIssueDemandMapping(lstIssueMaterialDOM[0].DemandVoucher.IssueDemandVoucherId, null);

        //    if (lstPreItemTransaction.Count > 0)
        //    {
        //        BindUpdateText(lstIssueMaterialDOM);
        //        BindGridDemandMaterial();
        //        BindGridIssueMaterial();
        //    }

        //    pnlSearch.Visible = false;
        //    pnlIssueMaterial.Visible = true;
        //    Enabled(true);
        //}
        //private IssueMaterialDOM GetIssueMaterialDetails()
        //{
        //    i = 0;
        //    strInvalid = string.Empty;
        //    issueMaterialDOM = new IssueMaterialDOM();
        //    issueMaterialDOM.DemandVoucher = new IssueDemandVoucherDOM();
        //    issueMaterialDOM.DemandVoucher.Quotation = new QuotationDOM();
        //    issueMaterialDOM.DemandVoucher.Quotation.StatusType = new MetaData();
        //    if (IssueMaterialId > 0)
        //    {
        //        issueMaterialDOM.IssueMaterialId = IssueMaterialId;
        //    }
        //    //issueMaterialDOM.DemandVoucher.Quotation.ContractQuotationNumber = lblContractorWorkOrderNumber.Text.Trim();
        //    issueMaterialDOM.DemandVoucher.IssueDemandVoucherId = Convert.ToInt32(hdnDemandVoucherId.Value);
        //    issueMaterialDOM.DemandVoucher.Quotation.ContractQuotationNumber = lblContractorWorkOrder.Text.ToString();
        //    issueMaterialDOM.DemandVoucher.IssueDemandVoucherNumber = lblIssueDemandVoucher.Text.ToString();
        //    issueMaterialDOM.DemandVoucher.Quotation.ContractorId = Convert.ToInt32(hdfContractorId.Value);
        //    issueMaterialDOM.DemandVoucher.Quotation.ContractorName = lblContractorName.Text.Trim();
        //    issueMaterialDOM.DemandVoucher.Quotation.ContractNumber = lblContractNumber.Text.Trim();
        //    issueMaterialDOM.DemandVoucher.MaterialDemandDate =Convert.ToDateTime(lblIssueDemandDate.Text.ToString());
        //    issueMaterialDOM.IssueMaterialDate = Convert.ToDateTime(txtIssueDate.Text.Trim());
        //    issueMaterialDOM.DemandVoucher.Remarks = txtRemarks.Text.Trim();
        //    issueMaterialDOM.DemandVoucher.Quotation.UploadDocumentId = DocumentStackId;
        //    //invoiceDom.TotalAmount = Convert.ToDecimal(lblTotalNetValue.Text.Trim());
        //    // invoiceDom.Quotation.StatusType.Id = 1;
        //    issueMaterialDOM.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
        //    issueMaterialDOM.CreatedBy = LoggedInUser.UserLoginId;
        //    foreach (GridViewRow row in gvIssueMaterial.Rows)
        //    {
        //        dec = 0;
        //        cnt = 0;
        //        BigNo = 0;
        //        BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].UnitIssued);
        //        txtUnitIssue = (TextBox)row.FindControl("txtUnitIssue");
        //        lblIndex = (Label)row.FindControl("lblIndex");
        //        //lstItemTransaction[i].BilledUnit = Convert.ToDecimal(txtBilledNoOfUnit.Text);
        //        dec = TryToParse(txtUnitIssue.Text);
        //        if (dec > 0)
        //        {
        //            cnt = NumberDecimalPlaces(dec);
        //            if (IssueMaterialId > 0)
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
        //                }
        //            }
        //            else if (Convert.ToDecimal(txtUnitIssue.Text) > lstItemTransaction[i].UnitLeft || cnt > 2)
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
        //            }
        //        }
        //        else
        //        {
        //            if (j > 0)
        //            {
        //                strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
        //                j++;
        //            }
        //            else
        //            {
        //                strInvalid = strInvalid + lblIndex.Text.Trim();
        //                j++;
        //            }
        //        }
        //        if (string.IsNullOrEmpty(strInvalid))
        //        {
        //            lstItemTransaction[i].UnitIssued = Convert.ToDecimal(txtUnitIssue.Text);
        //        }
        //        i++;
        //    }
        //    if (!string.IsNullOrEmpty(strInvalid))
        //    {
        //        strInvalid = "Unit Issue allows only valid numeric value <= Unit Left at S.No: " + strInvalid;
        //    }
        //    else
        //    {
        //        issueMaterialDOM.DemandVoucher.Quotation.ItemTransaction = lstItemTransaction;
        //    }
        //    return issueMaterialDOM;
        //}
        //private void BindSearchText(List<IssueDemandVoucherDOM> lstIssueDemandVoucherDOM)
        //{            
        //    lblIssueDemandVoucher.Text = lstIssueDemandVoucherDOM[0].IssueDemandVoucherNumber;
        //    lblContractorWorkOrder.Text = lstIssueDemandVoucherDOM[0].Quotation.ContractQuotationNumber.ToString();
        //    lblIssueDemandDate.Text = lstIssueDemandVoucherDOM[0].MaterialDemandDate.ToString("dd-MMM-yyyy");
        //    hdfContractorId.Value = lstIssueDemandVoucherDOM[0].Quotation.ContractorId.ToString();
        //    lblContractorName.Text = lstIssueDemandVoucherDOM[0].Quotation.ContractorName;
        //    lblContractNumber.Text = lstIssueDemandVoucherDOM[0].Quotation.ContractNumber;
        //    hdnDemandVoucherId.Value = lstIssueDemandVoucherDOM[0].IssueDemandVoucherId.ToString();
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
        //private void BindGridDemandMaterial()
        //{
        //    if (lstPreItemTransaction.Count > 0)
        //    {
        //        gvDemandMaterial.DataSource = lstPreItemTransaction;
        //        gvDemandMaterial.DataBind();
        //    }
        //}
        //private void BindGridIssueMaterial()
        //{            
        //        gvIssueMaterial.DataSource = lstItemTransaction;
        //        gvIssueMaterial.DataBind();           
        //}
        //private void Enabled(Boolean Condition)
        //{
        //    btnSaveDraft.Visible = Condition;
        //    btnReset.Visible = Condition;
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
        //private int IssueMaterialId
        //{
        //    get
        //    {
        //        return (Int32)ViewState["IssueMaterialId"];
        //    }
        //    set
        //    {
        //        ViewState["IssueMaterialId"] = value;
        //    }
        //}

        //#endregion       

    }
}