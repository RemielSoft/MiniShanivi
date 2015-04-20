using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using DataAccessLayer;
using DataAccessLayer.Invoice;
using BusinessAccessLayer;
using BusinessAccessLayer.Quality;
using BusinessAccessLayer.Invoice;
using MiniERP.Shared;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;
using System.Reflection;
using System.Web.Services;

namespace MiniERP.Invoice
{

    public partial class MaterialConsumptionNote : BasePage
    {
        #region private Goble varibale(s)
        int dayscount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);
        ContractorBL contractorBL = new ContractorBL();
        CompanyWorkOrderBL companyWorkOrderBL = new CompanyWorkOrderBL();
        QuotationBL quotationBL = new QuotationBL();
        MaterialConsumptionNoteBAL materialConsumptionBAL = null;
        List<IssueMaterialDOM> lstIssueMaterialDOM = null;
        MaterialConsumptionNoteDom materialConsumption = null;
        IssueMaterialBL issueMaterialBL = null;
        ItemTransaction itemTransaction = null;
        List<IssueMaterialDOM> lstissueMaterial = null;
        List<ItemTransaction> previousConsumptions = null;

        MetaData metaData = null;
        List<QuotationDOM> lstQuotationDOM = null;
        BasePage basePage = new BasePage();

        int i = 0;
        int j = 0;
        Boolean flag = false;
        string strInvalid = string.Empty;
        decimal dec = 0;
        int cnt = 0;
        String s = string.Empty;
        decimal BigNo = 0;

        TextBox txtConsumedUnit = null;
        TextBox txtRemark = null;
        TextBox txtLostUnit = null;
        Label lblIndex = null;
        Label lblActivityDiscription = null;
        Label lblItemCategory = null;
        Label lblItem = null;
        Label lblSpecification = null;
        Label lblBrand = null;
        Label lblNOF = null;
        Label lblUnitIssued = null;
        Label lblUnitLeft = null;
        Label lblPerUnitCost = null;
        CheckBox chkbx = null;
        CheckBox chbxSelectAll = null;
        CompanyWorkOrderBL CompWorkOrderBL = new CompanyWorkOrderBL();
        HiddenField hdfContractorPOMappingId = null;
        bool track = false;

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lstissueMaterial = new List<IssueMaterialDOM>();
                issueMaterialBL = new IssueMaterialBL();
                lstissueMaterial = issueMaterialBL.ReadIssueMaterial(null, 0, DateTime.MinValue, DateTime.MinValue, null, null, null);

                var lst = lstissueMaterial.Select(a => new { a.DemandVoucher.Quotation.ContractorName, a.DemandVoucher.Quotation.ContractorId }).Distinct().ToList().OrderBy(a => a.ContractorName);


                basePage.BindDropDown(ddlConsumption, "ContractorName", "ContractorId", lst);




                pnlMaterialConsumptionNotes.Visible = false;
                txtWOrderNumber.Focus();
                btnSaveDraft.Visible = false;
                btnCancel.Visible = false;
                txtIssueDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                if (Request.QueryString["MaterialConsumptId"] != null)
                {
                    MaterialConsumptionId = 0;
                    MaterialConsumptionId = Convert.ToInt32(Request.QueryString["MaterialConsumptId"]);
                    EditData(MaterialConsumptionId);
                }
                else
                {
                    EmptyDocumentList();
                    MaterialConsumptionId = 0;
                    pnlMaterialConsumptionNotes.Visible = false;
                    txtWOrderNumber.Focus();
                }
            }
        }

        protected void chbxSelectAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvIssueMaterial.Rows)
            {
                chkbx = (CheckBox)row.FindControl("chkbxIssueDetails");
                if (chkbx.Enabled == true)
                {
                    chkbx.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void on_chbx_Activity_Click(object sender, EventArgs e)
        {
            track = false;
            chbxSelectAll = (CheckBox)gvIssueMaterial.HeaderRow.FindControl("chbxSelectAll");
            foreach (GridViewRow row in gvIssueMaterial.Rows)
            {
                chkbx = (CheckBox)row.FindControl("chkbxIssueDetails");
                if (!chkbx.Checked)
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

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            issueMaterialBL = new IssueMaterialBL();
            materialConsumptionBAL = new MaterialConsumptionNoteBAL();
            lstPreItemTransaction = new List<ItemTransaction>();
            lstPreItemTransaction = materialConsumptionBAL.ReadIssueMaterialMappingConsumption(txtWOrderNumber.Text.Trim());
            lstIssueMaterialDOM = new List<IssueMaterialDOM>();
            lstIssueMaterialDOM = materialConsumptionBAL.ReadIssueMaterialConsumption(txtWOrderNumber.Text.Trim());
            // Now Read Material Consumption 
            var lstMaterialConsumption = materialConsumptionBAL.ReadMaterialConsumptionNotes(null, null, txtWOrderNumber.Text.Trim());
            if (lstMaterialConsumption != null && lstMaterialConsumption.Count > 0)
            {
                previousConsumptions = materialConsumptionBAL.ReadMaterialConsumptionMapping(lstMaterialConsumption[0].MaterialConsumptionId);
            }

            if (lstIssueMaterialDOM.Count > 0 && lstIssueMaterialDOM[0].DemandVoucher.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
            {
                BindSearchText(lstIssueMaterialDOM);
                if (lstPreItemTransaction.Count > 0)
                {
                    BindGridIssueMaterial();
                    txtWOrderNumber.Text = String.Empty;
                    pnlMaterialConsumptionNotes.Visible = true;
                    ddlConsumption.SelectedIndex = 0;
                }
            }
            else if (string.IsNullOrEmpty(txtWOrderNumber.Text.ToString()))
            {
                Alert("Please enter a valid Work Order Number!", LinkSearch);
            }
            else if (lstIssueMaterialDOM.Count > 0 && lstIssueMaterialDOM[0].DemandVoucher.Quotation.StatusType.Id != Convert.ToInt32(StatusType.Generated))
            {
                Alert("Work Order is not generated!", LinkSearch);
            }
            else
            {
                Alert("Invalid Work Order Number!", LinkSearch);
                pnlMaterialConsumptionNotes.Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (GridViewRow row in gvIssueMaterial.Rows)
            {
                lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
                lblItemCategory = (Label)row.FindControl("lblItemCategory");
                lblItem = (Label)row.FindControl("lblItem");
                lblSpecification = (Label)row.FindControl("lblSpecification");
                lblBrand = (Label)row.FindControl("lblBrand");
                HiddenField hdnBrand = (HiddenField)row.FindControl("hdnBrand");
                lblNOF = (Label)row.FindControl("lblNOF");
                lblPerUnitCost = (Label)row.FindControl("lblPUC");
                lblUnitIssued = (Label)row.FindControl("lblUnitIssued");
                lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
                txtLostUnit = (TextBox)row.FindControl("txtLostUnit");
                txtConsumedUnit = (TextBox)row.FindControl("txtConsumedUnit");
                //txtReturnUnit = (TextBox)row.FindControl("txtReturnMaterial");
                txtRemark = (TextBox)row.FindControl("txtRemark");
                chkbx = (CheckBox)row.FindControl("chkbxIssueDetails");

                hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");

                if (chkbx.Checked == true && hdfContractorPOMappingId != null)
                {
                    if (chkbx.Checked.Equals(true))
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
                        itemTransaction.Item.ItemName = lblItem.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                        itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Brand.BrandName = lblBrand.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(hdnBrand.Value);
                        itemTransaction.NumberOfUnit = Convert.ToDecimal(lblNOF.Text.ToString());

                        itemTransaction.PerUnitCost = Convert.ToDecimal(lblPerUnitCost.Text.ToString());
                        itemTransaction.UnitLeft = Convert.ToDecimal(lblUnitLeft.Text.ToString());
                        itemTransaction.UnitIssued = Convert.ToDecimal(lblUnitIssued.Text.ToString());
                        itemTransaction.LostUnit = Math.Round(TryToParse(txtLostUnit.Text.ToString()), 2);
                        itemTransaction.ConsumedUnit = Math.Round((TryToParse(txtConsumedUnit.Text) + TryToParse(txtLostUnit.Text)), 2);
                        //itemTransaction.ReturnUnit = TryToParse(txtReturnUnit.Text.ToString());
                        itemTransaction.TotalAmount = (TryToParse(txtLostUnit.Text) * TryToParse(lblPerUnitCost.Text));
                        itemTransaction.Remark = txtRemark.Text;
                        //if (itemTransaction.UnitLeft < itemTransaction.ConsumedUnit)
                        //{
                        //    Alert("Consume Unit should be less than Unit left", btnAdd);
                        //    txtConsumedUnit.Focus();

                        //    break;
                        //}
                        if (itemTransaction.UnitIssued < itemTransaction.ConsumedUnit)
                        {
                            Alert("Consume Unit should not be greater than unit Issued", btnAdd);
                            txtConsumedUnit.Focus();

                            break;
                        }
                        itemTransaction.CreatedBy = LoggedInUser.UserLoginId;
                        lstItemTransaction.Add(itemTransaction);

                        chkbx.Checked = false;
                        chkbx.Enabled = false;
                    }
                }
            }
            if (lstItemTransaction.Count != 0)
            {
                BindGridMaterialConsumption();
                txtWOrderNumber.Text = string.Empty;
            }
            else
            {
                //Enabled(false);
                Alert("Please Check At Least One Activity Description", btnAdd);
                gvMaterialConsumption.DataSource = new List<object>();
                gvMaterialConsumption.DataBind();
            }
            foreach (TableCell item in gvIssueMaterial.HeaderRow.Cells)
            {
                CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
                if (chbxSelectAll.Checked == true)
                {
                    chbxSelectAll.Checked = false;
                }
            }
            btnSaveDraft.Visible = true;
            btnCancel.Visible = true;
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            ltrl_err_msg.Text = string.Empty;
            metaData = new MetaData();
            materialConsumption = new MaterialConsumptionNoteDom();
            materialConsumption = GetMaterialConsumptionDetails();
            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            else
            {
                if (MaterialConsumptionId > 0)
                {
                    metaData = CreateMaterialConsumption(materialConsumption, MaterialConsumptionId);
                }
                else
                {
                    metaData = CreateMaterialConsumption(materialConsumption, null);
                }
                if (metaData.Id > 0)
                {
                    CreateDocumentMapping();

                    if (MaterialConsumptionId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert( 'Material Consumption No:" + metaData.Name + " updated successfully'); window.location='" +
                        Request.ApplicationPath + "Invoice/MaterialReconciliationReport.aspx';", true);

                        //Alert("Material Consumption No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "MaterialReconciliationReport.aspx");



                    }
                    else
                    {
                        Alert("Material Consumption No: " + metaData.Name + " Created Successfully", btnSaveDraft, "MaterialConsumptionNote.aspx");
                    }
                    ltrl_err_msg.Text = string.Empty;
                    lstItemTransaction = null;
                    BindGridIssueMaterial();
                    pnlMaterialConsumptionNotes.Visible = false;

                }
            }
        }

        private MaterialConsumptionNoteDom GetMaterialConsumptionDetails()
        {
            i = 0;
            strInvalid = string.Empty;
            materialConsumptionBAL = new MaterialConsumptionNoteBAL();
            materialConsumption = new MaterialConsumptionNoteDom();
            materialConsumption.IssueMaterial = new IssueMaterialDOM();
            materialConsumption.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            if (MaterialConsumptionId > 0)
            {
                materialConsumption.MaterialConsumptionId = MaterialConsumptionId;
            }
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = lblContractorWorkOrder.Text.ToString();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractorId = Convert.ToInt32(hdfContractorId.Value);
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractorName = lblContractorName.Text.Trim();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractNumber = lblContractNumber.Text.Trim();
            if (!string.IsNullOrEmpty(txtIssueDate.Text))
            {
                materialConsumption.MaterialConsumptionDate = Convert.ToDateTime(txtIssueDate.Text.Trim());
            }
            materialConsumption.IssueMaterial.DemandVoucher.Remarks = txtRemarks.Text.Trim();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId = DocumentStackId;
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            materialConsumption.CreatedBy = LoggedInUser.UserLoginId;
            foreach (GridViewRow row in gvMaterialConsumption.Rows)
            {
                Label lblLostUnit = (Label)row.FindControl("lblLostUnit");
                Label lblAmountLostUnit = (Label)row.FindControl("lblAmountLostUnit");
                Label lblConsumedunit = (Label)row.FindControl("lblConsumedunit");
                if (string.IsNullOrEmpty(strInvalid))
                {
                    lstItemTransaction[i].LostUnit = Convert.ToDecimal(lblLostUnit.Text);
                    lstItemTransaction[i].TotalAmount = Convert.ToDecimal(lblAmountLostUnit.Text);
                    lstItemTransaction[i].ConsumedUnit = Convert.ToDecimal(lblConsumedunit.Text);
                    i++;
                }
            }
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ItemTransaction = lstItemTransaction;
            return materialConsumption;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        protected void gvMaterialConsumption_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            String S = string.Empty;
            S = lstItemTransaction[index].Item.ItemName;
            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                foreach (GridViewRow row in gvIssueMaterial.Rows)
                {
                    chkbx = (CheckBox)row.FindControl("chkbxIssueDetails");
                    lblItem = (Label)row.FindControl("lblItem");
                    if (Request.QueryString["MaterialConsumptId"] != null)
                    {
                        int MaterialConsumptionId = Convert.ToInt32(Request.QueryString["MaterialConsumptId"]);
                        HiddenField hdfId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
                        int MCId = Convert.ToInt32(hdfId.Value);
                        materialConsumptionBAL = new MaterialConsumptionNoteBAL();
                        materialConsumptionBAL.DeleteMaterialConsumptionNotesMapping(MCId, MaterialConsumptionId);
                        BindgvMaterialConsumptionDelete(lstMaterialConsumtionDom);
                        BindGridIssueMaterial();
                    }
                    if (lblItem.Text.Trim() == S)
                    {
                        chkbx.Enabled = true;
                        chkbx.Checked = false;
                        flag = true;
                        break;
                    }
                }
                if (lstItemTransaction.Count == 0)
                {
                    lstItemTransaction = null;
                    ltrl_err_msg.Text = string.Empty;
                    btnSaveDraft.Visible = false;
                }
                gvMaterialConsumption.DataSource = lstItemTransaction;
                gvMaterialConsumption.DataBind();

            }
        }

        protected void gvIssueMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //  previousConsumptions
            foreach (GridViewRow row in gvIssueMaterial.Rows)
            {
                chkbx = (CheckBox)row.FindControl("chkbxIssueDetails");
                lblUnitIssued = (Label)row.FindControl("lblUnitIssued");

                txtLostUnit = (TextBox)row.FindControl("txtLostUnit");
                txtConsumedUnit = (TextBox)row.FindControl("txtConsumedUnit");
                if (previousConsumptions != null && previousConsumptions.Count > 0)
                {
                    txtLostUnit.Text = previousConsumptions[row.RowIndex].LostUnit.ToString();
                    txtConsumedUnit.Text = (previousConsumptions[row.RowIndex].ConsumedUnit - previousConsumptions[row.RowIndex].LostUnit).ToString();
                }
                else
                {
                    txtLostUnit.Text = "0";
                    txtConsumedUnit.Text = "0";
                }
                lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
                hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
                //MaterialConsumptionId = 0;
                if (MaterialConsumptionId != 0 && lstItemTransaction != null)
                {
                    foreach (ItemTransaction item in lstItemTransaction)
                    {
                        if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0 || Convert.ToInt32(hdfContractorPOMappingId.Value) == item.DeliverySchedule.Id)
                        {
                            chkbx.Checked = false;
                            chkbx.Enabled = false;
                        }
                    }
                }
                //else if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0)
                //{
                //    chkbx.Checked = false;
                //    chkbx.Enabled = false;
                //}
                else if (((Convert.ToDecimal(txtConsumedUnit.Text) + (Convert.ToDecimal(txtLostUnit.Text)) == Convert.ToDecimal(lblUnitIssued.Text))))
                {
                    chkbx.Checked = false;
                    chkbx.Enabled = false;
                    txtLostUnit.Enabled = false;
                    txtConsumedUnit.Enabled = false;

                }


            }
        }

        #endregion

        #region Private Methods

        private void BindgvMaterialConsumptionDelete(List<MaterialConsumptionNoteDom> lst)
        {
            lst = new List<MaterialConsumptionNoteDom>();
            materialConsumptionBAL = new MaterialConsumptionNoteBAL();
            List<ItemTransaction> lstPreItemTransaction = new List<ItemTransaction>();
            if (lst.Count > 0)
            {

                lstPreItemTransaction = materialConsumptionBAL.ReadIssueMaterialMappingConsumption(lst[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber);
                gvIssueMaterial.DataSource = lstItemTransaction;
                gvIssueMaterial.DataBind();


            }
        }

        private void EditData(Int32 MaterialConsumptionId)
        {
            materialConsumptionBAL = new MaterialConsumptionNoteBAL();
            lstMaterialConsumtionDom = new List<MaterialConsumptionNoteDom>();
            lstIssueMaterialDOM = new List<IssueMaterialDOM>();
            lstPreItemTransaction = new List<ItemTransaction>();
            lstMaterialConsumtionDom = materialConsumptionBAL.ReadMaterialConsumptionNotes(MaterialConsumptionId, null, null);
            lstPreItemTransaction = materialConsumptionBAL.ReadIssueMaterialMappingConsumption(lstMaterialConsumtionDom[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber.Trim());
            lstIssueMaterialDOM = materialConsumptionBAL.ReadIssueMaterialConsumption(lstMaterialConsumtionDom[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber.Trim());
            if (lstPreItemTransaction.Count > 0)
            {
                BindGridIssueMaterial();
            }
            lstItemTransaction = materialConsumptionBAL.ReadMaterialConsumptionMapping(MaterialConsumptionId);
            if (lstItemTransaction.Count > 0)
            {
                gvMaterialConsumption.DataSource = lstItemTransaction;
                gvMaterialConsumption.DataBind();
            }
            BindSearchText(lstIssueMaterialDOM);
            pnlMaterialConsumptionNotes.Visible = true;
            pnlSearch.Visible = false;
            CalculateItemLeft();
            btnSaveDraft.Visible = true;
            btnCancel.Visible = true;

        }

        private MetaData CreateMaterialConsumption(MaterialConsumptionNoteDom materialConsumption, Int32? MaterialConsumptionId)
        {
            if (lstItemTransaction != null)
            {
                metaData = new MetaData();
                materialConsumptionBAL = new MaterialConsumptionNoteBAL();
                metaData = materialConsumptionBAL.CreateMaterialConsumption(materialConsumption, MaterialConsumptionId);
            }
            return metaData;
        }

        private void Reset()
        {
            txtRemarks.Text = string.Empty;
            EmptyGrid(gvMaterialConsumption);
            txtIssueDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            Response.Redirect("MaterialConsumptionNote.aspx");
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

        private void BindSearchText(List<IssueMaterialDOM> lstIssueMaterialDOM)
        {
            lblContractorWorkOrder.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractQuotationNumber.ToString();
            lblIssueMaterialDate.Text = lstIssueMaterialDOM[0].IssueMaterialDate.ToString("dd-MMM-yyyy");
            hdfContractorId.Value = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractorId.ToString();
            lblContractorName.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractorName;
            lblContractNumber.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractNumber;
        }

        private void BindGridIssueMaterial()
        {
            if (lstPreItemTransaction.Count > 0)
            {
                BindGridMain(lstPreItemTransaction);

                gvIssueMaterial.DataSource = lstPreItemTransaction;
                gvIssueMaterial.DataBind();
            }
        }

        private void BindGridMaterialConsumption()
        {
            gvMaterialConsumption.DataSource = lstItemTransaction;
            gvMaterialConsumption.DataBind();
        }

        public void EmptyGrid(GridView gv)
        {
            gv.DataSource = new List<Object>();
            gv.DataBind();
        }

        #endregion

        #region Public Properties

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

        private int MaterialConsumptionId
        {
            get
            {
                return (Int32)ViewState["MaterialConsumptionId"];
            }
            set
            {
                ViewState["MaterialConsumptionId"] = value;
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

        #endregion

        // popup search Contractor Work Order Name

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int contractorid = 0;
            contractorid = Convert.ToInt32(ddlConsumption.SelectedValue);
            BindGridIsuue(null, contractorid, DateTime.MinValue, DateTime.MinValue, null, null);
            ModalPopupExtender2.Show();
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
            txtWOrderNumber.Text = WorkOrderNo.Text.ToString();
        }

        private void BindGridIsuue(Int32? IssueMaterialId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String IssueMaterialNo)
        {

            lstissueMaterial = new List<IssueMaterialDOM>();
            issueMaterialBL = new IssueMaterialBL();
            lstissueMaterial = issueMaterialBL.ReadIssueMaterial(IssueMaterialId, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, null);
            if (lstissueMaterial.Count > 0)
            {
                // Query of the Take the data Generated Type
                var lst = lstissueMaterial.Where(e => e.DemandVoucher.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)));
                gvIssueMaterialNo.DataSource = lst;
                gvIssueMaterialNo.DataBind();
            }
            else
            {
                EmptyGrid(gvIssueMaterialNo);
            }
        }

        private void CalculateItemLeft()
        {
            for (int i = 0; i < lstItemTransaction.Count; i++)
            {
                lstItemTransaction[i].UnitLeft += lstItemTransaction[i].ConsumedUnit;
                for (int j = 0; j < lstPreItemTransaction.Count; j++)
                {
                    if (lstItemTransaction[i].Item.ModelSpecification.ModelSpecificationId == lstPreItemTransaction[j].Item.ModelSpecification.ModelSpecificationId)
                    {
                        lstPreItemTransaction[j].UnitLeft += lstItemTransaction[i].ConsumedUnit;
                        lstPreItemTransaction[j].UnitIssued -= lstItemTransaction[i].ConsumedUnit;
                    }
                }
            }
        }

        private void BindGridMaterialConsumptionNotesEdit()
        {
            gvMaterialConsumption.DataSource = lstItemTransaction;
            gvMaterialConsumption.DataBind();
        }
        public void BindGridMain(List<ItemTransaction> issuedItems)
        {


            if (issuedItems.Count > 0)
            {
                var rsIssuedItems = issuedItems.AsEnumerable()
                   .Select(x => new
                   {
                       x.Item.ItemId,
                       x.Item.ItemName,
                       x.Item.ModelSpecification.ModelSpecificationId,
                       x.Item.ModelSpecification.ModelSpecificationName,
                       x.UnitIssued,
                       x.NumberOfUnit,
                       x.PerUnitCost,
                       x.Item.ModelSpecification.Category.ItemCategoryId,
                       x.Item.ModelSpecification.Category.ItemCategoryName,
                       x.UnitLeft,
                       x.DeliverySchedule.ActivityDescription

                   })
                   .GroupBy(a => new
                   {
                       a.ItemId,
                       a.ModelSpecificationId,
                       a.ItemName,
                       a.ModelSpecificationName,
                       a.PerUnitCost,
                       a.ActivityDescription,
                       a.ItemCategoryId,
                       a.ItemCategoryName,
                       a.NumberOfUnit

                   })
                   .Select(b => new
                   {
                       ItemId = b.Key.ItemId,
                       ItemName = b.Key.ItemName,
                       ItemSpecificationId = b.Key.ModelSpecificationId,
                       ItemSpecification = b.Key.ModelSpecificationName,
                       IssuedQuantity = b.Sum(z => z.UnitIssued),
                       PerUnitCost = b.Key.PerUnitCost,
                       ActivityDescription = b.Key.ActivityDescription,
                       ItemCategoryId = b.Key.ItemCategoryId,
                       ItemCategoryName = b.Key.ItemCategoryName,
                       NumberOfUnit = b.Key.NumberOfUnit,
                       AvailableQuantity = b.Key.NumberOfUnit - b.Sum(z => z.UnitIssued)

                   }).ToList();
                //lstPreItemTransaction = quotationBL.ReadContractorQuotationMapping(lst[0].ContractorQuotationId);
                gvMain.DataSource = rsIssuedItems;
                gvMain.DataBind();
            }
        }

        private void BindGridQuotationDetails(List<QuotationDOM> lst)
        {
            quotationBL = new QuotationBL();
            lstPreItemTransaction = new List<ItemTransaction>();
            if (lst.Count > 0)
            {
                lstPreItemTransaction = quotationBL.ReadContractorQuotationMapping(lst[0].ContractorQuotationId);
                gvIssueMaterial.DataSource = lstPreItemTransaction;
                gvIssueMaterial.DataBind();
            }
        }

        private void BindUpdateText(List<MaterialConsumptionNoteDom> lst)
        {
            //ctrl_UploadDocument.GetDocumentData(lst[0].Quotation.UploadDocumentId);

            lblContractorWorkOrder.Text = lst[0].IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber.ToString();
            lblIssueMaterialDate.Text = lst[0].IssueMaterial.IssueMaterialDate.ToString("dd-MMM-yyyy");
            lblContractorName.Text = lst[0].IssueMaterial.DemandVoucher.Quotation.ContractorName.ToString();
            lblContractNumber.Text = lst[0].IssueMaterial.DemandVoucher.Quotation.ContractNumber.ToString();
            hdfContractorId.Value = lst[0].IssueMaterial.DemandVoucher.Quotation.ContractorId.ToString();
            //txtMaterialDemandDate.Text = lst[0].MaterialDemandDate.ToString("dd-MMM-yyyy");
            txtRemarks.Text = lst[0].IssueMaterial.DemandVoucher.Remarks.ToString();
            // GetDocumentData(lst[0].IssueMaterial.DemandVoucher.Quotation.UploadDocumentId);
            //GetDocumentData(lst[0].ma);
        }

        private List<MaterialConsumptionNoteDom> lstMaterialConsumtionDom
        {
            get
            {
                return (List<MaterialConsumptionNoteDom>)ViewState["lstMaterialConsumtionDom"];
            }
            set
            {
                ViewState["lstMaterialConsumtionDom"] = value;
            }
        }

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

            if (DocumentStackId == 0)
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
                lst_document = DocumentsList;
                lst_document.RemoveAt(Index);
                DocumentsList = lst_document;
                BindDocument();
            }
            else if (e.CommandName == "OpenFile")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                Index = Convert.ToInt32(e.CommandArgument);
                lst_document = new List<Document>();
                lst_document = DocumentsList;


                Response.Redirect(@"\" + lst_document[Index].Path + @"\" + lst_document[Index].Replaced_Name);

                //File_Path = lst_document[Index].Path + @"\" + lst_document[Index].Replaced_Name;
                //WebClient req = new WebClient();
                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.ClearContent();
                //response.ClearHeaders();
                //response.Buffer = true;
                //response.AddHeader("Content-Disposition", "attachment;filename=" + lst_document[Index].Replaced_Name.Replace(lst_document[Index].Replaced_Name, lst_document[Index].Original_Name));
                ////response.AddHeader("Content-Disposition", "attachment;filename=" + lst_document[Index].Replaced_Name);
                //byte[] data = req.DownloadData(Server.MapPath(File_Path));
                //response.BinaryWrite(data);
                //response.End();

                //string strURL = "~/Images/1.jpg";

                //string filename = "1.jpg";
                //WebClient req = new WebClient();
                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.ClearContent();
                //response.ClearHeaders();
                //response.Buffer = true;
                //response.AddHeader("Content-Disposition", "attachment;filename=" + filename.Replace(filename, "123.jpg"));


                //byte[] data = req.DownloadData(Server.MapPath(strURL));
                //response.BinaryWrite(data);
                //response.End();

                // Response.Redirect("../Upload_Documents/2013/SupplierReceiveMaterial/1286_1.docx",);
            }
        }

        #endregion

        #region Private Methods

        private void ManageSession(bool forCopy)
        {
            RequestPageName = pageName;
            if (forCopy)
            {
                DocumentStackId = 0;
            }
            else if (Page_Name == null || Page_Name != RequestPageName)
            {
                Page_Name = RequestPageName;
                DocumentStackId = 0;
                DocumentSerial = 0;
                DocumentsList = null;
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
            document.CreatedBy = LoggedInUser.UserLoginId;
            DocumentStackId = document_BL.CreateAndReadDocumnetStackId(document);
            return DocumentStackId;
        }

        private void DirectoryHandle(FileUpload fileupload)
        {
            if (fileupload.HasFile)
            {
                if (fileupload.FileContent.Length > 10485760)
                {
                    Alert("You can upload up to 10 megabytes (MB) in size at a time", FileUpload_Control);
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
                        if (DocumentStackId != 0)
                        {
                            document = new Document();
                            lst_document = new List<Document>();
                            flag = false;

                            document.Original_Name = fileupload.FileName.Split('\\').Last();
                            if (DocumentsList != null)
                            {
                                foreach (Document item in DocumentsList)
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
                                Alert(GlobalConstants.M_File_Exist, FileUpload_Control);
                            }
                            else
                            {
                                DocumentSerial = DocumentSerial + 1;

                                File_Extension = System.IO.Path.GetExtension(fileupload.FileName.Split('\\').Last());
                                document.Replaced_Name = Convert.ToString(DocumentStackId) + "_" + Convert.ToString(DocumentSerial) + File_Extension;

                                File_Path = Sub_Folder_Path + @"\" + document.Replaced_Name;
                                //File_Path = Sub_Folder_Path + @"\" + document.Original_Name;
                                //Upload file in respective path
                                FileUpload_Control.SaveAs(File_Path);

                                document.DocumentId = DocumentStackId;

                                document.Path = ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString() + @"\" + RequestPageName;
                                document.LastIndex = DocumentSerial;


                                if (DocumentsList == null)
                                {
                                    lst_document.Add(document);
                                }
                                else
                                {
                                    lst_document = DocumentsList;
                                    lst_document.Add(document);
                                }

                                //Add Doc's info in list
                                DocumentsList = lst_document;
                            }
                        }
                    }
                }
            }
            else
            {
                ShowMessage("Please Select File.");
            }
        }

        public void BindDocument()
        {
            if (DocumentsList != null)
            {
                gv_documents.DataSource = DocumentsList;
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
            lst_document = DocumentsList;

            if (lst_document != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                document_BL.ResetDocumentMapping(Convert.ToInt32(DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document doc in lst_document)
                {
                    document = new Document();
                    document.DocumentId = Convert.ToInt32(DocumentStackId);
                    document.Original_Name = doc.Original_Name;
                    document.Replaced_Name = doc.Replaced_Name;
                    document.Path = doc.Path;
                    //DocumentSerial is the last updated document
                    document.LastIndex = DocumentSerial;
                    document.CreatedBy = LoggedInUser.UserLoginId;
                    document.Id = doc.Id;
                    document_BL.CreateDocumentMapping(document);
                }
            }
        }

        private void GetDocumentData(List<SupplierRecieveMatarial> lst_quotation)
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = document_BL.ReadDocumnetMapping(lst_quotation[0].UploadFile.DocumentId);
            if (lst_document.Count >= 1)
            {
                DocumentsList = lst_document;
                DocumentStackId = lst_document[0].DocumentId;

                DocumentSerial = lst_document[0].LastIndex;
                Page_Name = pageName;
                BindDocument();
            }
        }

        public void EmptyDocumentList()
        {
            DocumentStackId = 0;
            DocumentSerial = 0;
            DocumentsList = null;
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

        protected void btn_upload_Click1(object sender, EventArgs e)
        {
            //For Copy Quotation, It is TRUE
            LoadData(false);
        }

        #endregion


    }
}