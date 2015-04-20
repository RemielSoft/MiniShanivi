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

namespace MiniERP.Quality
{
    public partial class ReturnMaterialContractor : BasePage
    {
        #region private Goble varibale(s)

        int dayscount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);
        ContractorBL contractorBL = new ContractorBL();
        CompanyWorkOrderBL companyWorkOrderBL = new CompanyWorkOrderBL();
        ContractorInvoiceBL contractorInvoiceBL = null;
        MaterialConsumptionNoteBAL materialConsumptionBAL = null;
        ReturnMaterialContractorBL returnMaterialContractorBAL = new ReturnMaterialContractorBL();
        List<IssueMaterialDOM> lstIssueMaterialDOM = null;
        MaterialConsumptionNoteDom materialConsumption = null;
        IssueMaterialBL issueMaterialBL = null;

        ItemTransaction itemTransaction = null;
        List<IssueMaterialDOM> lstissueMaterial = null;
        MetaData metaData = null;
        //File Upload Use
        BasePage basePage = new BasePage();

        int i = 0;
        int j = 0;
        Boolean flag = true;
        string strInvalid = string.Empty;
        decimal dec = 0;
        int cnt = 0;
        String s = string.Empty;
        decimal BigNo = 0;
        CheckBox chkbx = null;
        CheckBox chbxSelectAll = null;
        bool track = false;

        Label lblunitleft = null;
        HiddenField hdfContractorPOMappingId = null;
        // HiddenField hdfActivityId = null;

        #endregion

        #region Protected Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lstIssueMaterialDOM = new List<IssueMaterialDOM>();
                issueMaterialBL = new IssueMaterialBL();
                lstIssueMaterialDOM = issueMaterialBL.ReadIssueMaterial(null, 0, DateTime.MinValue, DateTime.MinValue, null, null, null);
                var lst = lstIssueMaterialDOM.Select(a => new { a.DemandVoucher.Quotation.ContractorName, a.DemandVoucher.Quotation.ContractorId }).Distinct().ToList().OrderBy(a => a.ContractorName);


                basePage.BindDropDown(ddlReturnmatcon, "ContractorName", "ContractorId", lst);





                txtIssueDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                btnCancel.Visible = false;
                btnSaveDraft.Visible = false;
                btnAdd.Visible = false;

            }

        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            MaterialConsumptionNoteBAL materialConsumptionBAL = new MaterialConsumptionNoteBAL();
            materialConsumptionBAL = new MaterialConsumptionNoteBAL();
            contractorInvoiceBL = new ContractorInvoiceBL();
            lstPreItemTransaction = new List<ItemTransaction>();
            //lstPreItemTransaction = contractorInvoiceBL.ReadContractorWorkOrderMapping(null, txtWOrderNumber.Text.Trim());
            lstPreItemTransaction = returnMaterialContractorBAL.ReadIssueMaterialMappingConsumption(txtWOrderNumber.Text.Trim());

            lstIssueMaterialDOM = new List<IssueMaterialDOM>();
            lstIssueMaterialDOM = materialConsumptionBAL.ReadIssueMaterialConsumption(txtWOrderNumber.Text.Trim());

            if (lstIssueMaterialDOM.Count > 0 && lstIssueMaterialDOM[0].DemandVoucher.Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated))
            {
                BindSearchText(lstIssueMaterialDOM);
                if (lstPreItemTransaction.Count > 0)
                {
                    BindGridIssueMaterial();
                    txtWOrderNumber.Text = String.Empty;
                    pnlMaterialConsumptionNotes.Visible = true;
                    btnCancel.Visible = false;
                    btnSaveDraft.Visible = false;
                    btnAdd.Visible = true;
                    ddlReturnmatcon.SelectedIndex = 0;
                }
            }
            else if (string.IsNullOrEmpty(txtWOrderNumber.Text.ToString()))
            {
                btnAdd.Visible = true;
                basePage.Alert("Please enter a valid Work Order Number!", LinkSearch);
            }
            else if (lstIssueMaterialDOM.Count > 0 && lstIssueMaterialDOM[0].DemandVoucher.Quotation.StatusType.Id != Convert.ToInt32(StatusType.Generated))
            {
                btnAdd.Visible = true;
                basePage.Alert("Work Order is not generated!", LinkSearch);
            }
            else
            {
                btnAdd.Visible = true;
                basePage.Alert("Invalid Work Order Number!", LinkSearch);
                pnlMaterialConsumptionNotes.Visible = false;
            }
        }

        protected void gvIssueMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            foreach (GridViewRow row in gvIssueMaterial.Rows)
            {
                chkbx = (CheckBox)row.FindControl("chkbxIssueDetails");
                lblunitleft = (Label)row.FindControl("lblUnitLeft");
                hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
                HiddenField hdfActivityId = (HiddenField)row.FindControl("hdfActivityId");
                ReturnMaterialContractorId = 0;
                if (ReturnMaterialContractorId != 0 && lstItemTransaction != null)
                {
                    foreach (ItemTransaction item in lstItemTransaction)
                    {
                        if (Convert.ToDecimal(lblunitleft.Text.ToString()) == 0 || Convert.ToInt32(hdfContractorPOMappingId.Value) == item.DeliverySchedule.Id)
                        {
                            chkbx.Checked = false;
                            chkbx.Enabled = false;
                        }
                    }
                }
                else if (Convert.ToDecimal(lblunitleft.Text.ToString()) == 0)
                {
                    chkbx.Checked = false;
                    chkbx.Enabled = false;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (GridViewRow row in gvIssueMaterial.Rows)
            {
                Label lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
                Label lblItemCategory = (Label)row.FindControl("lblItemCategory");
                Label lblItem = (Label)row.FindControl("lblItem");
                Label lblPUC = (Label)row.FindControl("lblPUC");
                Label lblSpecification = (Label)row.FindControl("lblSpecification");
                HiddenField hdfBrandId = (HiddenField)row.FindControl("hdfBrandId");
                Label lblBrand = (Label)row.FindControl("lblBrand");
                Label lblNOF = (Label)row.FindControl("lblNOF");
                Label lblUnitDemanded = (Label)row.FindControl("lblUnitDemanded");
                Label lblUnitIssued = (Label)row.FindControl("lblUnitIssued");
                Label lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
                Label lblMeasurement = (Label)row.FindControl("lblMeasurement");
                TextBox txtRemak = (TextBox)row.FindControl("txtRemark");
                TextBox txtReturnUnit = (TextBox)row.FindControl("txtReturnMaterial");
                chkbx = (CheckBox)row.FindControl("chkbxIssueDetails");
                HiddenField hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
                HiddenField hdfItemId = (HiddenField)row.FindControl("hdfItemId");
                HiddenField hdfSpecificationId = (HiddenField)row.FindControl("hdfSpecificationId");
                HiddenField hdfUnitMeasurementId = (HiddenField)row.FindControl("hdfUnitMeasurementId");
                HiddenField hdfUnitMeasurementName = (HiddenField)row.FindControl("hdfUnitMeasurementName");
                // HiddenField hdfActivityId = (HiddenField)row.FindControl("hdfActivityId");

                if (chkbx.Checked == true && hdfContractorPOMappingId != null)
                    if (chkbx.Checked == true)
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
                            itemTransaction.Item.ItemId = Convert.ToInt32(hdfItemId.Value);
                            itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                            itemTransaction.Item.ModelSpecification.ModelSpecificationId = Convert.ToInt32(hdfSpecificationId.Value);
                            itemTransaction.Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(hdfBrandId.Value);
                            itemTransaction.Item.ModelSpecification.Brand.BrandName = lblBrand.Text.ToString();
                            itemTransaction.NumberOfUnit = Convert.ToDecimal(lblNOF.Text.ToString());
                            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = Convert.ToInt32(hdfUnitMeasurementId.Value);
                            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = hdfUnitMeasurementName.Value;
                            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString();
                            //itemTransaction.UnitDemanded = Convert.ToDecimal(lblUnitDemanded.Text.ToString());
                            itemTransaction.UnitLeft = Convert.ToDecimal(lblUnitLeft.Text.ToString());
                            itemTransaction.QuantityReturned = Math.Round(Convert.ToDecimal(txtReturnUnit.Text.ToString()), 2);
                            if (itemTransaction.UnitLeft < itemTransaction.QuantityReturned)
                            {
                                Alert("Quantity Item Unit should be less than unit left", btnAdd);
                                break;
                            }

                            itemTransaction.Remark = txtRemak.Text;
                            itemTransaction.UnitIssued = Convert.ToDecimal(lblUnitIssued.Text.ToString());
                            itemTransaction.ItemRequired = Convert.ToDecimal(lblUnitLeft.Text.ToString());
                            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                            itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
                            lstItemTransaction.Add(itemTransaction);
                            chkbx.Checked = false;
                            chkbx.Enabled = false;
                            btnCancel.Visible = true;
                            btnSaveDraft.Visible = true;
                        }
                    }

            }
            if (lstItemTransaction.Count != 0)
            {

                gvReturnMaterialContractor.DataSource = lstItemTransaction;
                gvReturnMaterialContractor.DataBind();
                //Enabled(true);
                txtWOrderNumber.Text = string.Empty;
            }
            else
            {
                //Enabled(false);
                basePage.Alert("Please Check At Least One Activity Description", btnAdd);
            }
            foreach (TableCell item in gvIssueMaterial.HeaderRow.Cells)
            {
                CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
                if (chbxSelectAll.Checked == true)
                {
                    chbxSelectAll.Checked = false;
                }


            }
        }

        protected void gvReturnMaterialContractor_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Label lblItem = (Label)row.FindControl("lblItem");
                    if (lblItem.Text.Trim() == S)
                    {
                        chkbx.Enabled = true;
                        chkbx.Checked = false;
                        break;
                    }
                }
                if (lstItemTransaction.Count == 0)
                {
                    lstItemTransaction = null;
                    ltrl_err_msg.Text = string.Empty;
                }
                gvReturnMaterialContractor.DataSource = lstItemTransaction;
                gvReturnMaterialContractor.DataBind();
                //btnSaveDraft.Visible = false;

            }

        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {

            ltrl_err_msg.Text = string.Empty;
            metaData = new MetaData();
            materialConsumption = new MaterialConsumptionNoteDom();
            materialConsumption = GetMaterialReturnDetails();


            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            else
            {
                if (ReturnMaterialContractorId > 0)
                {

                    metaData = CreateReturnMaterialContractor(materialConsumption, ReturnMaterialContractorId);
                }
                else
                {
                    metaData = CreateReturnMaterialContractor(materialConsumption, null);
                }
                if (metaData.Id > 0)
                {
                    //CreateDocumentMapping();
                    //if (MaterialConsumptionId > 0)
                    //{
                    //    Alert("Material Consumption No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewContractorInvoice.aspx");
                    //}
                    //else
                    //{
                    basePage.Alert("Return Material Contractor No: " + metaData.Name + " Created Successfully", btnSaveDraft, "ReturnMaterialContractor.aspx");
                    //}
                    ltrl_err_msg.Text = string.Empty;
                    lstItemTransaction = null;
                    BindGridIssueMaterial();
                }
            }






        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReturnMaterialContractor.aspx");
            pnlMaterialConsumptionNotes.Visible = false;
            btnSaveDraft.Visible = false;
            btnCancel.Visible = false;
            btnAdd.Visible = false;
        }

        //For popup
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int ContractorId = 0;
            ContractorId = Convert.ToInt32(ddlReturnmatcon.SelectedValue);


            BindGridIsuue(null, ContractorId, DateTime.MinValue, DateTime.MinValue, null, null);
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
            txtWOrderNumber.Text = WorkOrderNo.Text.ToString();
        }

        #endregion

        #region Private Methods

        private MaterialConsumptionNoteDom GetMaterialReturnDetails()
        {
            i = 0;
            strInvalid = string.Empty;
            //  materialConsumptionBAL = new MaterialConsumptionNoteBAL();
            returnMaterialContractorBAL = new ReturnMaterialContractorBL();
            materialConsumption = new MaterialConsumptionNoteDom();
            materialConsumption.IssueMaterial = new IssueMaterialDOM();
            materialConsumption.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            if (ReturnMaterialContractorId > 0)
            {
                // materialConsumption.MaterialConsumptionId = MaterialConsumptionId;
                materialConsumption.ReturnMaterialContractorId = ReturnMaterialContractorId;

            }
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = lblContractorWorkOrder.Text.ToString();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractorId = Convert.ToInt32(hdfContractorId.Value);
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractorName = lblContractorName.Text.Trim();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractNumber = lblContractNumber.Text.Trim();
            materialConsumption.IssueMaterial.IssueMaterialId = Convert.ToInt32(lblIssudemandId.Text.Trim());
            materialConsumption.IssueMaterial.IssueMaterialNumber = lblIssueDemandNumber.Text.Trim();
            if (!string.IsNullOrEmpty(txtIssueDate.Text))
            {
                materialConsumption.MaterialConsumptionDate = Convert.ToDateTime(txtIssueDate.Text.Trim());
            }
            materialConsumption.IssueMaterial.DemandVoucher.Remarks = txtRemarks.Text.Trim();
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId = basePage.DocumentStackId;
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            materialConsumption.CreatedBy = basePage.LoggedInUser.UserLoginId;

            foreach (GridViewRow row in gvReturnMaterialContractor.Rows)
            {
                Label lblRetunUnit = (Label)row.FindControl("lblRetunUnit");
                Label lblRemark = (Label)row.FindControl("lblRemark");
                HiddenField hdfBrand = (HiddenField)row.FindControl("hdfBrandId");
                lstItemTransaction[i].Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(hdfBrand.Value);
                DropDownList ddlStore = (DropDownList)row.FindControl("ddlStore");
                if (ddlStore.SelectedIndex>0)
                {
                    lstItemTransaction[i].Item.ModelSpecification.Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                }
                else
                {
                    strInvalid += "Please select store at row "+i;
                }
                if (string.IsNullOrEmpty(strInvalid))
                {
                    lstItemTransaction[i].QuantityReturned = Convert.ToDecimal(lblRetunUnit.Text);
                    lstItemTransaction[i].Remark = lblRemark.Text;
                    i++;
                }

            }
            //foreach (GridViewRow row in gvMaterialConsumption.Rows)
            //{
            //    dec = 0;
            //    cnt = 0;
            //    BigNo = 0;
            //    BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].ConsumedUnit);
            //    Label lblConsumedunit = (Label)row.FindControl("lblConsumedunit");
            //    Label lblIndex = (Label)row.FindControl("lblIndex");
            //    lstItemTransaction[i].BilledUnit = Convert.ToDecimal(lblConsumedunit.Text);
            //    dec = TryToParse(lblConsumedunit.Text);
            //    if (dec > 0)
            //    {
            //        cnt = NumberDecimalPlaces(dec);
            //        if (MaterialConsumptionId > 0)
            //        {
            //            if (cnt > 2 || dec > BigNo)
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
            //        else if (Convert.ToDecimal(lblConsumedunit.Text) > lstItemTransaction[i].UnitLeft || cnt > 2)
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
            //    }
            //    else
            //    {
            //        if (j > 0)
            //        {
            //            strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
            //            j++;
            //        }
            //        else
            //        {
            //            strInvalid = strInvalid + lblIndex.Text.Trim();
            //            j++;
            //        }
            //    }
            //    if (string.IsNullOrEmpty(strInvalid))
            //    {
            //        lstItemTransaction[i].ConsumedUnit = Convert.ToDecimal(lblConsumedunit.Text);
            //    }
            //    i++;
            //}
            //if (!string.IsNullOrEmpty(strInvalid))
            //{
            //    strInvalid = "Unit For Billed allows only valid numeric value and <= Unit Left at S.No: " + strInvalid;
            //}
            //else
            //{
            materialConsumption.IssueMaterial.DemandVoucher.Quotation.ItemTransaction = lstItemTransaction;
            // }
            return materialConsumption;
        }


        private MetaData CreateReturnMaterialContractor(MaterialConsumptionNoteDom materialConsumption, Int32? ReturnMaterialContractorId)
        {
            if (lstItemTransaction != null)
            {
                metaData = new MetaData();
                materialConsumptionBAL = new MaterialConsumptionNoteBAL();

                metaData = returnMaterialContractorBAL.CreateReturnMaterialContractor(materialConsumption, ReturnMaterialContractorId);
            }
            return metaData;
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


        //For poupu

        private void BindGridIsuue(Int32? IssueMaterialId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String IssueMaterialNo)
        {

            lstIssueMaterialDOM = new List<IssueMaterialDOM>();
            issueMaterialBL = new IssueMaterialBL();
            lstIssueMaterialDOM = issueMaterialBL.ReadIssueMaterial(IssueMaterialId, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, null);
            if (lstIssueMaterialDOM.Count > 0)
            {
                // Query of the Take the data Generated Type
                var lst = lstIssueMaterialDOM.Where(e => e.DemandVoucher.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)));




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

        // For Serach Item 
        private void BindGridIssueMaterial()
        {

            if (lstPreItemTransaction.Count > 0)
            {
                //var lst = lstIssueMaterialDOM.Where(e => e.DemandVoucher.Quotation.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)));
                //var lst = lstItemTransaction.Where(e => !e.Item.ItemId.Equals(0) && string.IsNullOrEmpty(e.Item.ItemName));
                var lst = lstPreItemTransaction.Where(e => !e.Item.ItemId.Equals(0));
                gvIssueMaterial.DataSource = lst;
                gvIssueMaterial.DataBind();
            }
        }

        private void BindSearchText(List<IssueMaterialDOM> lstIssueMaterialDOM)
        {
            lblContractorWorkOrder.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractQuotationNumber.ToString();
            lblIssueMaterialDate.Text = lstIssueMaterialDOM[0].IssueMaterialDate.ToString("dd-MMM-yyyy");
            hdfContractorId.Value = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractorId.ToString();
            lblContractorName.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractorName;
            lblContractNumber.Text = lstIssueMaterialDOM[0].DemandVoucher.Quotation.ContractNumber;
            lblIssudemandId.Text = lstIssueMaterialDOM[0].IssueMaterialId.ToString();
            lblIssueDemandNumber.Text = lstIssueMaterialDOM[0].IssueMaterialNumber;
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

        private int ReturnMaterialContractorId
        {
            get
            {
                return (Int32)ViewState["ReturnMaterialContractorId"];
            }
            set
            {
                ViewState["ReturnMaterialContractorId"] = value;
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

        protected void btn_upload_Click1(object sender, EventArgs e)
        {

            //For Copy Quotation, It is TRUE
            LoadData(false);
        }
        #endregion

        protected void gvReturnMaterialContractor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<Store> stores = new List<Store>();
                StoreBL storeBl = new StoreBL();
                stores=storeBl.ReadStore(null);
                DropDownList ddlStore = (DropDownList)e.Row.FindControl("ddlStore");
                ddlStore.DataSource = stores;
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataValueField = "StoreId";
                ddlStore.DataBind();
                
                ddlStore.Items.Insert(0, "--Select--");

            }
        }


    }
}