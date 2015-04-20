using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using BusinessAccessLayer.Quality;
using MiniERP.Shared;
using System.Text;
using System.Drawing;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;
using System.Reflection;

namespace MiniERP.Quality.Parts
{
    public partial class IssuingMaterial : System.Web.UI.UserControl
    {
        #region private Global Variables

        BasePage basePage = new BasePage();
        IssueDemandVoucherBL issueDemandBL = new IssueDemandVoucherBL();
        IssueDemandVoucherDOM issueDemandVoucherDOM = null;
        IssueDemandVoucherBL issueDemandVoucherBL = null;
        List<IssueDemandVoucherDOM> lstIssueDemandVoucherDOM = null;
        List<IssueMaterialDOM> lstIssueMaterialDOM = null;
        IssueMaterialDOM issueMaterialDOM = null;
        IssueMaterialBL issueMaterialBL = null;
        ItemTransaction itemTransaction = null;
        StoreBL storeBL = new StoreBL();
        MetaData metaData = null;
        int index = 0;
        int count = 0;
        int i = 0;
        int j = 0;
        string strInvalid = string.Empty;
        decimal dec = 0;
        int cnt = 0;
        String s = string.Empty;
        decimal BigNo = 0;
        int StockStatus = 0;

        Label lblActivityDiscription = null;
        Label lblItemCategory = null;
        Label lblItem = null;
        Label lblSpecification = null;
        Label lblBrand = null;
        Label lblNOF = null;
        Label lblUnitIssued = null;
        Label lblUnitLeft = null;
        Label lblIndex = null;
        Label lblUnitDemanded = null;
        CheckBox chkbxDemandVoucherDetails = null;
        CheckBox chbxSelectAll = null;
        HiddenField hdfContractorPOMappingId = null;
        HiddenField hdfItemId = null;
        HiddenField hdfSpecificationId = null;
        HiddenField hdfUnitMeasurementId = null;
        TextBox txtUnitIssue = null;
        bool track = false;

        String pageName = String.Empty;

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            if (!IsPostBack)
            {
                lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
                lstIssueDemandVoucherDOM = issueDemandBL.ViewIssueDemand(0, DateTime.MinValue, DateTime.MinValue, null, null);
                var lst = lstIssueDemandVoucherDOM.Select(a => new { a.Quotation.ContractorName, a.Quotation.ContractorId }).Distinct().ToList().OrderBy(a => a.ContractorName);

                basePage.BindDropDown(ddlIssueMat, "ContractorName", "ContractorId", lst);

                //  basePage.BindDropDown(ddlItem, "Item.ItemName", "Item.ItemId", lstItemTransaction.Select(x => new { x.Item.ItemId, x.Item.ItemName }).ToList());



                txtIssueDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                if (Request.QueryString["IssueMaterialId"] != null)
                {
                    EmptyDocumentList();
                    IssueMaterialId = 0;
                    IssueMaterialId = Convert.ToInt32(Request.QueryString["IssueMaterialId"]);
                    EditData(IssueMaterialId);
                    CalculateItemLeft();
                    // BindGridIssueMaterial();
                    //gvDemandMaterial.DataSource = lstPreItemTransaction;
                    //gvDemandMaterial.DataBind();
                }
                else
                {
                    EmptyDocumentList();
                    IssueMaterialId = 0;
                    pnlIssueMaterial.Visible = false;
                    Enabled(false);
                    txtDemandNoteNumber.Focus();
                    txtDemandNoteNumber.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + LinkSearch.ClientID + "').click();return false;}} else {return true}; ");
                }
            }
        }

        private void CalculateItemLeft()
        {
            for (int i = 0; i < lstItemTransaction.Count; i++)
            {
                lstItemTransaction[i].UnitLeft += lstItemTransaction[i].ItemRequired;
                for (int j = 0; j < lstPreItemTransaction.Count; j++)
                {
                    if (lstItemTransaction[i].Item.ModelSpecification.ModelSpecificationId == lstPreItemTransaction[j].Item.ModelSpecification.ModelSpecificationId)
                    {
                        lstPreItemTransaction[j].UnitLeft += lstItemTransaction[i].ItemRequired;
                        lstPreItemTransaction[j].UnitIssued -= lstItemTransaction[i].ItemRequired;
                    }
                }
            }
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            //For Empty Already Exist Data
            //ctrl_UploadDocument.EmptyDocumentList();
            //Ended
            issueDemandVoucherBL = new IssueDemandVoucherBL();
            lstPreItemTransaction = new List<ItemTransaction>();
            lstPreItemTransaction = issueDemandVoucherBL.ReadIssueDemandMapping(null, txtDemandNoteNumber.Text.Trim());
            lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
            lstIssueDemandVoucherDOM = issueDemandVoucherBL.ReadMaterialIssueDemandVoucher(null, txtDemandNoteNumber.Text.Trim());


            if (lstIssueDemandVoucherDOM.Count > 0 && lstIssueDemandVoucherDOM[0].Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
            {
                BindSearchText(lstIssueDemandVoucherDOM);
                if (lstPreItemTransaction.Count > 0)
                {
                    BindGridDemandMaterial();
                    txtDemandNoteNumber.Text = String.Empty;
                    ddlIssueMat.SelectedIndex = 0;
                    pnlIssueMaterial.Visible = true;
                }
            }
            else if (string.IsNullOrEmpty(txtDemandNoteNumber.Text.ToString()))
            {
                basePage.Alert("Please enter a valid Issue Demand Voucher Number!", LinkSearch);
            }
            else if (lstIssueDemandVoucherDOM.Count > 0 && lstIssueDemandVoucherDOM[0].Quotation.StatusType.Id != Convert.ToInt32(StatusType.Generated))
            {
                basePage.Alert("Quotation is not generated!", LinkSearch);
            }
            else
            {
                basePage.Alert("Invalid Issue Demand Voucher Number!", LinkSearch);
                pnlIssueMaterial.Visible = false;
            }
        }

        protected void chbxSelectAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvDemandMaterial.Rows)
            {
                chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
                if (chkbxDemandVoucherDetails.Enabled == true)
                {
                    chkbxDemandVoucherDetails.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void on_chbx_Activity_Click(object sender, EventArgs e)
        {
            track = false;
            chbxSelectAll = (CheckBox)gvDemandMaterial.HeaderRow.FindControl("chbxSelectAll");
            foreach (GridViewRow row in gvDemandMaterial.Rows)
            {
                chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
                if (!chkbxDemandVoucherDetails.Checked)
                    track = true;
            }
            if (track == true)
            {
                chbxSelectAll.Checked = false;
            }
            else
            {
                chbxSelectAll.Checked = true;
            }
        }

        protected void btnAddDemandMaterial_Click(object sender, EventArgs e)
        {
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (GridViewRow row in gvDemandMaterial.Rows)
            {
                lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
                lblItemCategory = (Label)row.FindControl("lblItemCategory");
                hdfItemId = (HiddenField)row.FindControl("hdfItemId");
                lblItem = (Label)row.FindControl("lblItem");
                hdfSpecificationId = (HiddenField)row.FindControl("hdfSpecificationId");
                lblSpecification = (Label)row.FindControl("lblSpecification");
                hdfUnitMeasurementId = (HiddenField)row.FindControl("hdfUnitMeasurementId");
                lblBrand = (Label)row.FindControl("lblBrand");
                lblNOF = (Label)row.FindControl("lblNOF");
                lblUnitDemanded = (Label)row.FindControl("lblUnitDemanded");
                lblUnitIssued = (Label)row.FindControl("lblUnitIssued");
                lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
                chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
                hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
                DropDownList ddlBrand = (DropDownList)row.FindControl("ddlBrand");

                if (chkbxDemandVoucherDetails.Checked == true && hdfContractorPOMappingId != null)
                {
                    if (chkbxDemandVoucherDetails.Checked.Equals(true))
                    {
                        itemTransaction = new ItemTransaction();
                        itemTransaction.MetaProperty = new MetaData();
                        itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                        itemTransaction.Item = new Item();
                        itemTransaction.Item.ModelSpecification = new ModelSpecification();
                        itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                        itemTransaction.Item.ModelSpecification.Category = new ItemCategory();

                        itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfContractorPOMappingId.Value);
                        itemTransaction.DeliverySchedule.ActivityDescription = lblActivityDiscription.Text.ToString();
                        itemTransaction.Item.ItemId = Convert.ToInt32(hdfItemId.Value);
                        itemTransaction.Item.ItemName = lblItem.Text.ToString();
                        itemTransaction.Item.ModelSpecification.ModelSpecificationId = Convert.ToInt32(hdfSpecificationId.Value);
                        itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                        itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = Convert.ToInt32(hdfUnitMeasurementId.Value);
                        itemTransaction.Item.ModelSpecification.Brand.BrandName = lblBrand.Text.ToString();
                        itemTransaction.NumberOfUnit = Convert.ToDecimal(lblNOF.Text.ToString());
                        itemTransaction.UnitDemanded = Convert.ToDecimal(lblUnitDemanded.Text.ToString());
                        itemTransaction.UnitLeft = Convert.ToDecimal(lblUnitLeft.Text.ToString());
                        itemTransaction.QuantityReturned = Convert.ToDecimal(lblUnitLeft.Text.ToString());
                        itemTransaction.UnitIssued = Convert.ToDecimal(lblUnitIssued.Text.ToString());
                        itemTransaction.ItemRequired = Convert.ToDecimal(lblUnitLeft.Text.ToString());
                        itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                        itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
                        lstItemTransaction.Add(itemTransaction);
                        chkbxDemandVoucherDetails.Checked = false;
                        chkbxDemandVoucherDetails.Enabled = false;
                    }
                }

            }
            if (lstItemTransaction.Count != 0)
            {

                BindGridIssueMaterial();
                Enabled(true);
                txtDemandNoteNumber.Text = string.Empty;
            }
            else
            {
                Enabled(false);
                basePage.Alert("Please Check At Least One Activity Description", btnAddDemandMaterial);
            }
            foreach (TableCell item in gvDemandMaterial.HeaderRow.Cells)
            {
                CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
                if (chbxSelectAll.Checked == true)
                {
                    chbxSelectAll.Checked = false;
                }
            }
        }

        protected void gvIssueMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            index = 0;
            index = Convert.ToInt32(e.CommandArgument);
            String s = string.Empty;
            s = lstItemTransaction[index].DeliverySchedule.ActivityDescription.ToString();
            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                foreach (GridViewRow row in gvDemandMaterial.Rows)
                {
                    chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
                    lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
                    if (lblActivityDiscription.Text.Trim() == s)
                    {
                        chkbxDemandVoucherDetails.Enabled = true;
                        chkbxDemandVoucherDetails.Checked = false;
                        //flag = true;
                        break;
                    }
                }
                if (lstItemTransaction.Count == 0)
                {
                    lstItemTransaction = null;
                    ltrl_err_msg.Text = string.Empty;
                }
                BindGridIssueMaterial();
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            ltrl_err_msg.Text = string.Empty;
            metaData = new MetaData();
            issueMaterialDOM = new IssueMaterialDOM();
            issueMaterialDOM = GetIssueMaterialDetails();
            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            else
            {
                if (IssueMaterialId > 0)
                {
                    metaData = CreateIssueMaterial(issueMaterialDOM, IssueMaterialId, false);
                }
                else
                {
                    metaData = CreateIssueMaterial(issueMaterialDOM, null, false);
                }
                if (metaData.Id > 0)
                {
                    ltrl_err_msg.Text = string.Empty;
                    lstItemTransaction = null;
                    BindGridIssueMaterial();
                    pnlIssueMaterial.Visible = false;

                    CreateDocumentMapping();
                    if (IssueMaterialId > 0)
                    {
                        // Response.Redirect("ViewIssueMaterial.aspx");
                        // basePage.Alert("Issue Material No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewIssueMaterial.aspx");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
"alert( 'Issue Material No: " + metaData.Name + " updated successfully'); window.location='" +
Request.ApplicationPath + "Quality/ViewIssueMaterial.aspx';", true);
                    }
                    else
                    {
                        basePage.Alert("Issue Material No: " + metaData.Name + " Created Successfully", btnSaveDraft, "IssuingMaterial.aspx");
                    }

                }
            }
        }

        protected void gvDemandMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            foreach (GridViewRow row in gvDemandMaterial.Rows)
            {
                chkbxDemandVoucherDetails = (CheckBox)row.FindControl("chkbxDemandVoucherDetails");
                lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
                lblUnitIssued = (Label)row.FindControl("lblUnitIssued");
                hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
                if (IssueMaterialId != 0 && lstItemTransaction != null)
                {
                    foreach (ItemTransaction item in lstItemTransaction)
                    {
                        if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0 || Convert.ToInt32(hdfContractorPOMappingId.Value) == item.DeliverySchedule.Id)
                        {
                            chkbxDemandVoucherDetails.Checked = false;
                            chkbxDemandVoucherDetails.Enabled = false;
                        }
                    }
                }
                //else if (Convert.ToDecimal(lblUnitIssued.Text.ToString()) > 0)
                //{
                //    chkbxDemandVoucherDetails.Checked = false;
                //    chkbxDemandVoucherDetails.Enabled = false;
                //}
                else if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) <= 0)
                {
                    chkbxDemandVoucherDetails.Checked = false;
                    chkbxDemandVoucherDetails.Enabled = false;
                }
            }
        }
        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrandBL brandBL = new BrandBL();

            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;
            int idx = row.RowIndex;

            HiddenField hdnItemSpecification = (HiddenField)row.Cells[0].FindControl("hdfSpecificationId");
            DropDownList ddlBrandlist = (DropDownList)row.Cells[0].FindControl("ddlBrand");

            issueMaterialBL = new IssueMaterialBL();
            issueMaterialDOM = issueMaterialBL.ReadIssueMaterialByDemandVoucher(lblIssueDemandVoucher.Text.Trim());
            List<ModelSpecification> lstBrand = null;
            if (issueMaterialDOM.IssueMaterialId > 0 && IssueMaterialId == 0)
            {
                lstBrand = brandBL.ReadItemBrandsById(Convert.ToInt32(hdnItemSpecification.Value), issueMaterialDOM.IssueMaterialId, true, Convert.ToInt32(ddl.SelectedValue));
            }
            else
            {
                lstBrand = brandBL.ReadItemBrandsById(Convert.ToInt32(hdnItemSpecification.Value), null, true, Convert.ToInt32(ddl.SelectedValue));
            }

            if (lstBrand != null && lstBrand.Count > 0)
            {

                var brands = lstBrand.ConvertAll(x => new MetaData { Id = x.Brand.BrandId, Name = x.Brand.BrandName }).ToList();
                basePage.BindDropDown(ddlBrandlist, "Name", "Id", brands);
                // ddlBrandlist.SelectedValue = lstItemTransaction[count].Item.ModelSpecification.Brand.BrandId.ToString();
            }

        }
        protected void gvIssueMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //IssueMaterialDOM issueMaterialDOM = new IssueMaterialDOM();
            BrandBL brandBL = new BrandBL();
            HiddenField hdnItemSpecification = (HiddenField)e.Row.FindControl("hdfSpecificationId");
            DropDownList ddlBrandlist = (DropDownList)e.Row.FindControl("ddlBrand");
            DropDownList ddlStorelist = (DropDownList)e.Row.FindControl("ddlStore");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (hdnItemSpecification != null && ddlStorelist != null)
                {

                    var stores = storeBL.ReadStoreBasedOnStock(Convert.ToInt32(hdnItemSpecification.Value));
                    basePage.BindDropDown(ddlStorelist, "StoreName", "StoreId", stores);
                    ddlStorelist.SelectedValue = lstItemTransaction[count].Item.ModelSpecification.Store.StoreId.ToString();

                    List<ModelSpecification> lstBrand = null;
                    //issueMaterialDOM.IssueMaterialId > 0 &&
                    if (IssueMaterialId == 0)
                    {
                        if (issueMaterialDOM != null)
                        {
                            lstBrand = brandBL.ReadItemBrandsById(Convert.ToInt32(hdnItemSpecification.Value), issueMaterialDOM.IssueMaterialId);
                        }
                        //else
                        //{
                        //    lstBrand = brandBL.ReadItemBrandsById(Convert.ToInt32(hdnItemSpecification.Value), null);
                        //}
                    }
                    else
                    {
                        lstBrand = brandBL.ReadItemBrandsById(Convert.ToInt32(hdnItemSpecification.Value), null, true, lstItemTransaction[count].Item.ModelSpecification.Store.StoreId);
                    }

                    if (lstBrand != null && lstBrand.Count > 0)
                    {
                        if (lstItemTransaction.Count <= count)
                        {
                            count = 0;
                        }
                        var brands = lstBrand.ConvertAll(x => new MetaData { Id = x.Brand.BrandId, Name = x.Brand.BrandName }).ToList();
                        basePage.BindDropDown(ddlBrandlist, "Name", "Id", brands);

                        if (lstItemTransaction.Count > count)
                        {
                            // ddlStorelist.SelectedValue = lstItemTransaction[count].Item.ModelSpecification.Store.StoreId.ToString();
                            ddlBrandlist.SelectedValue = lstItemTransaction[count].Item.ModelSpecification.Brand.BrandId.ToString();
                            count++;
                        }
                    }
                    else
                    {
                        basePage.BindEmptyDropDown(ddlBrandlist);
                    }

                }

            }


            if (IssueMaterialId != 0)
            {
                if (lstItemTransaction != null)
                {
                    foreach (ItemTransaction Item in lstItemTransaction)
                    {
                        // ddlBrandlist.SelectedValue = Item.Item.ModelSpecification.Brand.BrandId.ToString();
                        foreach (ItemTransaction Items in lstPreItemTransaction)
                        {
                            if (Item.DeliverySchedule.Id == Items.DeliverySchedule.Id)
                            {
                                Item.UnitLeft = Items.UnitLeft;
                                Item.UnitIssued = Items.UnitIssued;

                                //Item.UnitLeft = Items.UnitForBilled; 
                                // Item.CreatedBy = basePage.LoggedInUser.UserLoginId;                                
                            }

                        }
                    }
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // Find the Issue Demand Vouchar Number
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int ContractorId = 0;
            ContractorId = Convert.ToInt32(ddlIssueMat.SelectedValue);
            BindGrid(ContractorId, DateTime.MinValue, DateTime.MinValue, null, null);
            ModalPopupExtender2.Show();
        }

        protected void rbtSelect_OncheckChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow oldRow in gvIssueDemandVoucher.Rows)
            {
                ((RadioButton)oldRow.FindControl("rbtSelect")).Checked = false;
            }
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtSelect")).Checked = true;
            Label IssueDemandVoucher = (Label)row.FindControl("lblIDVNo");
            txtDemandNoteNumber.Text = IssueDemandVoucher.Text.ToString();
        }

        private void BindGrid(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String IDVNo)
        {
            lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
            lstIssueDemandVoucherDOM = issueDemandBL.ViewIssueDemand(contractorId, toDate, fromDate, contractNo, IDVNo);

            if (lstIssueDemandVoucherDOM.Count > 0)
            {
                // Query of the Take the data Generated Type
                var lst = lstIssueDemandVoucherDOM.Where(e => e.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)));

                gvIssueDemandVoucher.DataSource = lst;
                gvIssueDemandVoucher.DataBind();
            }
            else
            {
                BindEmptyGrid(gvIssueDemandVoucher);
            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        #endregion

        #region private Methods
        private void Reset()
        {
            txtRemarks.Text = string.Empty;
            EmptyDocumentList();
            txtIssueDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        private void BindUpdateText(List<IssueMaterialDOM> lstIssueMaterialDOM)
        {
            lblContractorWorkOrder.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractQuotationNumber.ToString();
            lblIssueDemandVoucher.Text = lstIssueMaterialDOM[0].DemandVoucher.IssueDemandVoucherNumber.ToString();
            hdnDemandVoucherId.Value = lstIssueMaterialDOM[0].DemandVoucher.IssueDemandVoucherId.ToString();
            hdfContractorId.Value = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractorId.ToString();
            lblContractorName.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractorName.ToString();
            lblContractNumber.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractNumber.ToString();
            lblIssueDemandDate.Text = lstIssueMaterialDOM[0].DemandVoucher.MaterialDemandDate.ToString("dd-MMM-yyyy");
            txtIssueDate.Text = lstIssueMaterialDOM[0].IssueMaterialDate.ToString("dd-MMM-yyyy");
            txtRemarks.Text = lstIssueMaterialDOM[0].DemandVoucher.Remarks.ToString();
            GetDocumentData(lstIssueMaterialDOM[0].DemandVoucher.Quotation.UploadDocumentId);
            hdnStatusTypeId.Value = lstIssueMaterialDOM[0].DemandVoucher.Quotation.StatusType.Id.ToString();
        }
        private MetaData CreateIssueMaterial(IssueMaterialDOM issueMaterialDOM, Int32? IssueMaterialId, bool isFinal)
        {
            if (lstItemTransaction != null)
            {
                metaData = new MetaData();
                issueMaterialBL = new IssueMaterialBL();
                metaData = issueMaterialBL.CreateIssueMaterial(issueMaterialDOM, IssueMaterialId, isFinal);
            }
            return metaData;
        }
        private void EditData(Int32 IssueMaterialId)
        {
            lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
            issueDemandVoucherBL = new IssueDemandVoucherBL();
            lstPreItemTransaction = new List<ItemTransaction>();
            lstItemTransaction = new List<ItemTransaction>();
            lstIssueMaterialDOM = new List<IssueMaterialDOM>();
            issueMaterialBL = new IssueMaterialBL();
            lstIssueMaterialDOM = issueMaterialBL.ReadIssueMaterial(IssueMaterialId, 0, System.DateTime.MinValue, System.DateTime.MinValue, null, null, null);
            lstItemTransaction = issueMaterialBL.ReadIssueMaterialMapping(IssueMaterialId);
            lstPreItemTransaction = issueDemandVoucherBL.ReadIssueDemandMapping(lstIssueMaterialDOM[0].DemandVoucher.IssueDemandVoucherId, null);

            if (lstPreItemTransaction.Count > 0)
            {
                BindUpdateText(lstIssueMaterialDOM);
                BindGridDemandMaterial();
                BindGridIssueMaterial();
            }

            pnlSearch.Visible = false;
            pnlIssueMaterial.Visible = true;
            Enabled(true);
        }
        private IssueMaterialDOM GetIssueMaterialDetails()
        {
            i = 0;
            strInvalid = string.Empty;
            issueMaterialBL = new IssueMaterialBL();
            issueMaterialDOM = new IssueMaterialDOM();
            issueMaterialDOM.DemandVoucher = new IssueDemandVoucherDOM();
            issueMaterialDOM.DemandVoucher.Quotation = new QuotationDOM();
            issueMaterialDOM.DemandVoucher.Quotation.StatusType = new MetaData();
            if (IssueMaterialId > 0)
            {
                issueMaterialDOM.IssueMaterialId = IssueMaterialId;
            }
            //issueMaterialDOM.DemandVoucher.Quotation.ContractQuotationNumber = lblContractorWorkOrderNumber.Text.Trim();
            issueMaterialDOM.DemandVoucher.IssueDemandVoucherId = Convert.ToInt32(hdnDemandVoucherId.Value);
            issueMaterialDOM.DemandVoucher.Quotation.ContractQuotationNumber = lblContractorWorkOrder.Text.ToString();
            issueMaterialDOM.DemandVoucher.IssueDemandVoucherNumber = lblIssueDemandVoucher.Text.ToString();
            issueMaterialDOM.DemandVoucher.Quotation.ContractorId = Convert.ToInt32(hdfContractorId.Value);
            issueMaterialDOM.DemandVoucher.Quotation.ContractorName = lblContractorName.Text.Trim();
            issueMaterialDOM.DemandVoucher.Quotation.ContractNumber = lblContractNumber.Text.Trim();
            issueMaterialDOM.DemandVoucher.MaterialDemandDate = Convert.ToDateTime(lblIssueDemandDate.Text.ToString());
            issueMaterialDOM.IssueMaterialDate = Convert.ToDateTime(txtIssueDate.Text.Trim());
            issueMaterialDOM.DemandVoucher.Remarks = txtRemarks.Text.Trim();
            issueMaterialDOM.DemandVoucher.Quotation.UploadDocumentId = basePage.DocumentStackId;
            //invoiceDom.TotalAmount = Convert.ToDecimal(lblTotalNetValue.Text.Trim());
            // invoiceDom.Quotation.StatusType.Id = 1;
            issueMaterialDOM.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            issueMaterialDOM.CreatedBy = basePage.LoggedInUser.UserLoginId;
            int counter = 0;
            List<ValidateIssueItem> issuedItems = new List<ValidateIssueItem>();



            foreach (GridViewRow row in gvIssueMaterial.Rows)
            {
                ValidateIssueItem issuedItem = new ValidateIssueItem();
                lblIndex = (Label)row.FindControl("lblIndex");
                dec = 0;
                StockStatus = 0;
                cnt = 0;
                BigNo = 0;
                if (IssueMaterialId > 0)
                {
                    BigNo = lstItemTransaction[i].UnitLeft;
                }
                else
                {
                    BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].ItemRequired);
                }
                issuedItem.QuantityDemand = lstItemTransaction[i].UnitDemanded;
                // GEt The Value of Brand Selected
                DropDownList ddlBrands = (DropDownList)row.FindControl("ddlBrand");
                if (ddlBrands.SelectedIndex > 0)
                {
                    lstItemTransaction[counter].Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(ddlBrands.SelectedValue);
                    lstItemTransaction[counter].Item.ModelSpecification.Brand.BrandName = ddlBrands.SelectedItem.Text;
                    issuedItem.BrandId = Convert.ToInt32(ddlBrands.SelectedValue);
                    issuedItem.BrandName = ddlBrands.SelectedItem.Text;
                }
                else
                {
                    strInvalid = "Please select Brand at S.No:" + lblIndex.Text.Trim() + "</br>";
                }
                // GEt The Value of Brand Selected
                DropDownList ddlStores = (DropDownList)row.FindControl("ddlStore");
                if (ddlStores.SelectedIndex > 0)
                {
                    lstItemTransaction[counter].Item.ModelSpecification.Store.StoreId = Convert.ToInt32(ddlStores.SelectedValue);
                    lstItemTransaction[counter].Item.ModelSpecification.Store.StoreName = ddlStores.SelectedItem.Text;
                    issuedItem.StoreId = Convert.ToInt32(ddlStores.SelectedValue);
                    issuedItem.StoreName = ddlStores.SelectedItem.Text;
                }
                else
                {
                    strInvalid = "Please select Store at S.No:" + lblIndex.Text.Trim() + "</br>";
                }
                counter++;
                // issueMaterialDOM[]
                txtUnitIssue = (TextBox)row.FindControl("txtUnitIssue");
                issuedItem.IssuedQuantity = Convert.ToDecimal(txtUnitIssue.Text.ToString());
                lblIndex = (Label)row.FindControl("lblIndex");
                hdfItemId = (HiddenField)row.FindControl("hdfItemId");
                issuedItem.ItemId = Convert.ToInt32(hdfItemId.Value.ToString());
                issuedItem.ItemName = ((Label)row.FindControl("lblItem")).Text;
                hdfSpecificationId = (HiddenField)row.FindControl("hdfSpecificationId");
                issuedItem.ItemSpecificationId = Convert.ToInt32(hdfSpecificationId.Value.ToString());
                issuedItem.ItemSpecification = ((Label)row.FindControl("lblSpecification")).Text;
                dec = TryToParse(txtUnitIssue.Text);
                int AvailableStock = 0;
                //StockStatus = issueMaterialBL.ReadStockStatus(Convert.ToInt32(hdfItemId.Value), Convert.ToInt32(hdfSpecificationId.Value), dec);
                StockStatus = issueMaterialBL.ReadStockStatus(Convert.ToInt32(hdfItemId.Value), Convert.ToInt32(hdfSpecificationId.Value), Convert.ToInt32(ddlStores.SelectedValue), Convert.ToInt32(ddlBrands.SelectedValue), out AvailableStock);
                issuedItem.AvailableQuantity = AvailableStock;

                cnt = NumberDecimalPlaces(dec);
                if (cnt > 2)
                {

                    strInvalid = strInvalid + "Unit Issue allows only valid numeric value <= Unit Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                }
                //if (StockStatus == 1)
                //{
                //    if (dec > 0)
                //    {
                //        cnt = NumberDecimalPlaces(dec);
                //        if (IssueMaterialId > 0)
                //        {
                //            if (cnt > 2 || dec > BigNo)
                //            {
                //                if (j > 0)
                //                {
                //                    //strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                //                    strInvalid = strInvalid + "Unit Issue allows only valid numeric value <= Unit Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                //                    j++;
                //                }
                //                else
                //                {
                //                    //strInvalid = strInvalid + lblIndex.Text.Trim();
                //                    strInvalid = strInvalid + "Unit Issue allows only valid numeric value <= Unit Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                //                    j++;
                //                }
                //            }
                //        }
                //        else if (Convert.ToDecimal(txtUnitIssue.Text) > lstItemTransaction[i].UnitLeft || cnt > 2)
                //        {
                //            if (j > 0)
                //            {
                //                //strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                //                strInvalid = strInvalid + "Unit Issue allows only valid numeric value <= Unit Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                //                j++;
                //            }
                //            else
                //            {
                //                //strInvalid = strInvalid + lblIndex.Text.Trim();
                //                strInvalid = strInvalid + "Unit Issue allows only valid numeric value <= Unit Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                //                j++;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (j > 0)
                //        {
                //            //strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                //            strInvalid = strInvalid + "Unit Issue allows only valid numeric value <= Unit Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                //            j++;
                //        }
                //        else
                //        {
                //            //strInvalid = strInvalid + lblIndex.Text.Trim();
                //            strInvalid = strInvalid + "Unit Issue allows only valid numeric value <= Unit Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                //            j++;
                //        }
                //    }
                //}
                //else
                //{
                //    if (j > 0)
                //    {
                //        //strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                //        strInvalid = strInvalid + "Unit Issue are more than the item stock at S.No:" + lblIndex.Text.Trim() + "</br>";
                //        j++;
                //    }
                //    else
                //    {
                //        //strInvalid = strInvalid + lblIndex.Text.Trim();
                //        strInvalid = strInvalid + "Unit Issue are more than the item stock at S.No:" + lblIndex.Text.Trim() + "</br>";
                //        j++;
                //    }
                //}
                if (string.IsNullOrEmpty(strInvalid))
                {
                    lstItemTransaction[i].ItemRequired = Convert.ToDecimal(txtUnitIssue.Text);
                }
                i++;

                issuedItems.Add(issuedItem);
            }

            //  TODO : 

            //var rsIssuedItems = new List<ValidateIssueItem>();
            var rsIssuedItems = issuedItems.AsEnumerable()
                   .Select(x => new
                   {
                       x.ItemId,
                       x.ItemSpecificationId,
                       x.StoreId,
                       x.BrandId,
                       x.AvailableQuantity,
                       x.IssuedQuantity,
                       x.ItemName,
                       x.ItemSpecification,
                       x.StoreName,
                       x.BrandName,
                       x.QuantityDemand
                   })
                   .GroupBy(a => new
                   {
                       a.ItemId,
                       a.ItemSpecificationId,
                       a.StoreId,
                       a.BrandId,
                       a.AvailableQuantity,
                       a.ItemName,
                       a.ItemSpecification,
                       a.StoreName,
                       a.BrandName,
                       a.QuantityDemand

                   })
                   .Select(b => new
                   {
                       ItemId = b.Key.ItemId,
                       ItemName = b.Key.ItemName,
                       ItemSpecificationId = b.Key.ItemSpecificationId,
                       ItemSpecification = b.Key.ItemSpecification,
                       StoreId = b.Key.StoreId,
                       StoreName = b.Key.StoreName,
                       BrandId = b.Key.BrandId,
                       BrandName = b.Key.BrandName,
                       AvailableQuantity = b.Key.AvailableQuantity,
                       IssuedQuantity = b.Sum(z => z.IssuedQuantity),
                       QuantityDemand = b.Key.QuantityDemand
                   }).ToList();


            foreach (var item in rsIssuedItems)
            {
                if (item.IssuedQuantity > item.AvailableQuantity)
                {
                    strInvalid = strInvalid + "Issued quantity shouldn't be greater than available quantity for Item : " + item.ItemName + "-" + item.ItemSpecification + "</br>";
                }
                if (item.IssuedQuantity > item.QuantityDemand)
                {
                    strInvalid = strInvalid + "Issued quantity shouldn't be greater than demand for Item : " + item.ItemName + "-" + item.ItemSpecification + "</br>";
                }
            }

            if (!string.IsNullOrEmpty(strInvalid))
            {
                //strInvalid = "Unit Issue allows only valid numeric value <= Unit Left at S.No: " + strInvalid;                
            }
            else
            {
                issueMaterialDOM.DemandVoucher.Quotation.ItemTransaction = lstItemTransaction;
            }
            return issueMaterialDOM;
        }
        private void BindSearchText(List<IssueDemandVoucherDOM> lstIssueDemandVoucherDOM)
        {
            lblIssueDemandVoucher.Text = lstIssueDemandVoucherDOM[0].IssueDemandVoucherNumber;
            lblContractorWorkOrder.Text = lstIssueDemandVoucherDOM[0].Quotation.ContractQuotationNumber.ToString();
            lblIssueDemandDate.Text = lstIssueDemandVoucherDOM[0].MaterialDemandDate.ToString("dd-MMM-yyyy");
            hdfContractorId.Value = lstIssueDemandVoucherDOM[0].Quotation.ContractorId.ToString();
            lblContractorName.Text = lstIssueDemandVoucherDOM[0].Quotation.ContractorName;
            lblContractNumber.Text = lstIssueDemandVoucherDOM[0].Quotation.ContractNumber;
            hdnDemandVoucherId.Value = lstIssueDemandVoucherDOM[0].IssueDemandVoucherId.ToString();
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
        private void BindGridDemandMaterial()
        {
            if (lstPreItemTransaction.Count > 0)
            {
                gvDemandMaterial.DataSource = lstPreItemTransaction;
                gvDemandMaterial.DataBind();
            }

        }
        private void BindGridIssueMaterial()
        {
            gvIssueMaterial.DataSource = lstItemTransaction;
            gvIssueMaterial.DataBind();
        }
        private void Enabled(Boolean Condition)
        {
            btnSaveDraft.Visible = Condition;
            btnReset.Visible = Condition;
        }
        #endregion

        #region private properties

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
        private List<ItemTransaction> lstPreItemTransaction
        {
            get
            {
                return (List<ItemTransaction>)ViewState["lstPreItemTransaction"];
            }
            set
            {
                ViewState["lstPreItemTransaction"] = value;
            }
        }
        private int IssueMaterialId
        {
            get
            {
                return (Int32)ViewState["IssueMaterialId"];
            }
            set
            {
                ViewState["IssueMaterialId"] = value;
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
                        track = false;

                        document.Original_Name = fileupload.FileName.Split('\\').Last();
                        if (basePage.DocumentsList != null)
                        {
                            foreach (Document item in basePage.DocumentsList)
                            {
                                if (item.Original_Name == document.Original_Name)
                                {
                                    track = true;
                                    break;
                                }
                            }
                        }
                        if (track == true)
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ltrl_err_msg.Text = string.Empty;
            metaData = new MetaData();
            issueMaterialDOM = new IssueMaterialDOM();
            issueMaterialDOM = GetIssueMaterialDetails();
            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            else
            {
                if (IssueMaterialId > 0)
                {
                    metaData = CreateIssueMaterial(issueMaterialDOM, IssueMaterialId, true);
                }
                else
                {
                    metaData = CreateIssueMaterial(issueMaterialDOM, null, true);
                }
                if (metaData.Id > 0)
                {
                    CreateDocumentMapping();
                    if (IssueMaterialId > 0)
                    {
                        basePage.Alert("Issue Material No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewIssueMaterial.aspx");

                    }
                    else
                    {
                        basePage.Alert("Issue Material No: " + metaData.Name + " Created Successfully", btnSaveDraft, "IssuingMaterial.aspx");
                    }
                    ltrl_err_msg.Text = string.Empty;
                    lstItemTransaction = null;
                    BindGridIssueMaterial();
                    pnlIssueMaterial.Visible = false;
                }
            }
        }

        #endregion
    }
}