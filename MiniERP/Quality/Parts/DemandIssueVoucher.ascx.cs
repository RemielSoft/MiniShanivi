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
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;
using System.Reflection;

namespace MiniERP.Quality.Parts
{
    public partial class DemandIssueVoucher : System.Web.UI.UserControl
    {
        #region private Global Variables

        BasePage basePage = new BasePage();
        List<QuotationDOM> lstQuotation = null;
        ItemTransaction itemTransaction = null;
        IssueDemandVoucherBL issueDemandVoucherBL = null;
        List<QuotationDOM> lstQuotationDOM = null;
        List<IssueDemandVoucherDOM> lstQutn = null;
        MetaData metaData = null;
        QuotationDOM quotationDOM = null;
        QuotationBL quotationBL = null;
        IssueDemandVoucherDOM issueDemandVoucherDOM = null;

        Boolean flag = false;
        string strInvalid = string.Empty;
        int cnt = 0;
        decimal dec = 0;
        decimal BigNo = 0;
        int i = 0;
        int j = 0;
        //int IDVNId = 0;
        Label lblActivityDiscription = null;
        Label lblItemCategory = null;
        Label lblItem = null;
        Label lblSpecification = null;
        Label lblBrand = null;
        Label lblNOF = null;
        Label lblUnitIssued = null;
        Label lblUnitLeft = null;
        Label lblIndex = null;
        CheckBox chkbxQuotationDetails = null;
        CheckBox chbxSelectAll = null;
        HiddenField hdfContractorPOMappingId = null;
        HiddenField hdfItemId = null;
        HiddenField hdfSpecificationId = null;
        HiddenField hdfUnitMeasurementId = null;
        TextBox box = null;

        String pageName = String.Empty;

        #endregion

        #region Protected Methods


        protected void Page_Load(object sender, EventArgs e)
        {
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            if (!IsPostBack)
            {
                List<QuotationDOM> lstQuotation = null;
                QuotationBL quotationBL = new QuotationBL();
                lstQuotation = new List<QuotationDOM>();
                lstQuotation = quotationBL.ReadContractorQuotationView(0, DateTime.MinValue, DateTime.MinValue, null, null);
                var lst = lstQuotation.Select(a => new { a.ContractorName, a.ContractorId }).Distinct().ToList().OrderBy(a=> a.ContractorName);
                

                basePage.BindDropDown(ddlDemandVoucher, "ContractorName", "ContractorId", lst);






                txtMaterialDemandDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                if (Request.QueryString["IssueDemandVoucherId"] != null)
                {
                    EmptyDocumentList();
                    quotationBL = new QuotationBL();
                    issueDemandVoucherBL = new IssueDemandVoucherBL();
                    lstQutn = new List<IssueDemandVoucherDOM>();
                    lstQuotationDOM = new List<QuotationDOM>();
                    lstItemTransaction = new List<ItemTransaction>();
                    IDVNId = 0;
                    IDVNId = Convert.ToInt32(Request.QueryString["IssueDemandVoucherId"]);
                    lstQutn = issueDemandVoucherBL.ReadMaterialIssueDemandVoucher(IDVNId, null);
                    lstItemTransaction = issueDemandVoucherBL.ReadIssueDemandMapping(IDVNId, null);
                    lstQuotationDOM = quotationBL.ReadContractorQuotation(null, lstQutn[0].Quotation.ContractQuotationNumber.Trim());
                    BindGridQuotationDetails(lstQuotationDOM);
                    BindUpdateText(lstQutn);
                    BindGridIssueDemandVoucher();
                    pnlSearch.Visible = false;
                    pnlIDV.Visible = true;
                    Enabled(true);
                    CalculateItemLeft();
                    gvQuotationDetails.DataSource = lstPreItemTransaction;
                    gvQuotationDetails.DataBind();
                }
                else
                {
                    EmptyDocumentList();
                    IDVNId = 0;
                    pnlIDV.Visible = false;
                    Enabled(false);
                    txtContractorQuotationNumber.Focus();
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

        protected void btnSearchContractorQuotationNumber_Click(object sender, EventArgs e)
        {
            //ctrl_UploadDocument.EmptyDocumentList();
            quotationBL = new QuotationBL();
            quotationDOM = new QuotationDOM();
            lstQuotationDOM = new List<QuotationDOM>();
            quotationDOM.StatusType = new MetaData();
            lstQuotationDOM = quotationBL.ReadContractorQuotation(null, txtContractorQuotationNumber.Text.Trim());
            if (lstQuotationDOM.Count > 0 && lstQuotationDOM[0].StatusType.Id == Convert.ToInt32(StatusType.Generated))
            {
                lstItemTransaction = null;
                txtRemarks.Text = string.Empty;
                ltrl_err_msg.Text = string.Empty;
                txtMaterialDemandDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                BindGridIssueDemandVoucher();
                SetAllDefaultData(lstQuotationDOM);
                pnlIDV.Visible = true;
                Enabled(false);
                txtContractorQuotationNumber.Text = String.Empty;
                ddlDemandVoucher.SelectedIndex = 0;
            }
            else if (string.IsNullOrEmpty(txtContractorQuotationNumber.Text.ToString()))
            {
                basePage.Alert("Please enter a valid Contractor Work Order Number!", btnSearchContractorQuotationNumber);
            }
            else if (lstQuotationDOM.Count > 0 && lstQuotationDOM[0].StatusType.Id != Convert.ToInt32(StatusType.Generated))
            {
                basePage.Alert("Quotation is not generated!", btnSearchContractorQuotationNumber);
            }
            else
            {
                basePage.Alert("Invalid Contractor Work Order Number!!", btnSearchContractorQuotationNumber);
                pnlIDV.Visible = false;
            }
        }

        protected void btnAddQuotationDetails_Click(object sender, EventArgs e)
        {
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (GridViewRow row in gvQuotationDetails.Rows)
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
                lblUnitIssued = (Label)row.FindControl("lblUnitIssued");
                lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
                chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
                hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");

                if (chkbxQuotationDetails.Checked == true && hdfContractorPOMappingId != null)
                {
                    if (chkbxQuotationDetails.Checked.Equals(true))
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
                        itemTransaction.UnitIssued = Convert.ToDecimal(lblUnitIssued.Text.ToString());
                        itemTransaction.UnitLeft = Convert.ToDecimal(lblUnitLeft.Text.ToString());
                        itemTransaction.ItemRequired = Convert.ToDecimal(lblUnitLeft.Text.ToString());
                        itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                        itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;
                        lstItemTransaction.Add(itemTransaction);
                        chkbxQuotationDetails.Checked = false;
                        chkbxQuotationDetails.Enabled = false;
                    }
                }
            }
            if (lstItemTransaction.Count != 0)
            {

                BindGridIssueDemandVoucher();
                Enabled(true);
                txtContractorQuotationNumber.Text = string.Empty;
            }
            else
            {
                Enabled(false);
                basePage.Alert("Please Check At Least One Service Description", btnAddQuotationDetails);
            }
            foreach (TableCell item in gvQuotationDetails.HeaderRow.Cells)
            {
                CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
                if (chbxSelectAll.Checked == true)
                {
                    chbxSelectAll.Checked = false;
                }
            }
        }

        protected void chbxSelectAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvQuotationDetails.Rows)
            {
                chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
                if (chkbxQuotationDetails.Enabled == true)
                {
                    chkbxQuotationDetails.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void chkbxQuotationDetails_Click(object sender, EventArgs e)
        {
            flag = false;
            chbxSelectAll = (CheckBox)gvQuotationDetails.HeaderRow.FindControl("chbxSelectAll");
            foreach (GridViewRow row in gvQuotationDetails.Rows)
            {
                chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
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

        protected void gvQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvQuotationDetails.Rows)
            {
                chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
                lblUnitLeft = (Label)row.FindControl("lblUnitLeft");
                hdfContractorPOMappingId = (HiddenField)row.FindControl("hdfContractorPOMappingId");
                if (IDVNId != 0 && lstItemTransaction != null)
                {
                    foreach (ItemTransaction item in lstItemTransaction)
                    {
                        if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0 || Convert.ToInt32(hdfContractorPOMappingId.Value) == item.DeliverySchedule.Id)
                        {
                            chkbxQuotationDetails.Checked = false;
                            chkbxQuotationDetails.Enabled = false;
                        }
                    }
                }
                else if (Convert.ToDecimal(lblUnitLeft.Text.ToString()) == 0)
                {
                    chkbxQuotationDetails.Checked = false;
                    chkbxQuotationDetails.Enabled = false;
                }
            }
        }

        protected void gvIssueDemandVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IDVNId != 0)
            {
                if (lstItemTransaction != null)
                {
                    foreach (ItemTransaction Item in lstItemTransaction)
                    {
                        foreach (ItemTransaction Items in lstPreItemTransaction)
                        {
                            if (Item.DeliverySchedule.Id == Items.DeliverySchedule.Id)
                            {
                                Item.UnitIssued = Items.UnitIssued;
                                Item.UnitLeft = Items.UnitLeft;
                                Item.CreatedBy = basePage.LoggedInUser.UserLoginId;
                            }
                        }
                    }
                }
            }
        }

        protected void gvIssueDemandVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            String s = string.Empty;
            s = lstItemTransaction[index].DeliverySchedule.ActivityDescription.ToString();
            if (e.CommandName == "cmdDelete")
            {
                lstItemTransaction.RemoveAt(index);
                foreach (GridViewRow row in gvQuotationDetails.Rows)
                {
                    chkbxQuotationDetails = (CheckBox)row.FindControl("chkbxQuotationDetails");
                    lblActivityDiscription = (Label)row.FindControl("lblActivityDiscription");
                    if (lblActivityDiscription.Text.Trim() == s)
                    {
                        chkbxQuotationDetails.Enabled = true;
                        chkbxQuotationDetails.Checked = false;
                        flag = true;
                        break;
                    }
                }
                if (lstItemTransaction.Count == 0)
                {
                    lstItemTransaction = null;
                    ltrl_err_msg.Text = string.Empty;
                }
                BindGridIssueDemandVoucher();
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            ltrl_err_msg.Text = String.Empty;
            //quotationDOM = new QuotationDOM();
            issueDemandVoucherDOM = new IssueDemandVoucherDOM();
            issueDemandVoucherDOM = GetIssueDemandVoucherDetails();
            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            else
            {
                if (IDVNId > 0)
                {
                    metaData = CreateIssueDemandVoucher(issueDemandVoucherDOM, IDVNId);
                }
                else
                {
                    metaData = CreateIssueDemandVoucher(issueDemandVoucherDOM, null);
                }
                if (metaData.Id > 0)
                {
                    CreateDocumentMapping();
                    lstItemTransaction = null;
                    lstPreItemTransaction = null;
                    lstQuotationDOM = null;
                    lstQutn = null;
                    if (IDVNId > 0)
                    {
                        basePage.Alert("Issue Demand Voucher No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewDemandIssueVoucher.aspx");
                        pnlSearch.Visible = false;
                    }
                    else
                    {
                        basePage.Alert("Issue Demand Voucher No: " + metaData.Name + " Created Successfully", btnSaveDraft);
                        pnlSearch.Visible = true;
                    }
                    BindGridIssueDemandVoucher();
                    pnlIDV.Visible = false;
                }
                else
                {
                    basePage.Alert(GlobalConstants.C_DUPLICATE_MESSAGE, btnSaveDraft);
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // popup search Contractor Work Order Name

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int contractorId;
            contractorId = Convert.ToInt32(ddlDemandVoucher.SelectedValue);
            BindGrid(contractorId, DateTime.MinValue, DateTime.MinValue, null, null);
            ModalPopupExtender2.Show();
        }

        protected void rbtSelect_OncheckChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow oldRow in gvViewQuotation.Rows)
            {
                ((RadioButton)oldRow.FindControl("rbtSelect")).Checked = false;
            }
            RadioButton rbt = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rbt.NamingContainer;
            ((RadioButton)row.FindControl("rbtSelect")).Checked = true;
            Label WorkOrderNumber = (Label)row.FindControl("lbtnQuotation");
            txtContractorQuotationNumber.Text = WorkOrderNumber.Text.ToString();
        }

        public void BindGrid(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String WorkOrderNo)
        {
            List<QuotationDOM> lstQuotation = null;
            QuotationBL quotationBL = new QuotationBL();
            lstQuotation = new List<QuotationDOM>();
            lstQuotation = quotationBL.ReadContractorQuotationView(contractorId, toDate, fromDate, contractNo, WorkOrderNo);
            if (lstQuotation.Count > 0)
            {
                // Query for the take the data Generated Type
                var lst = lstQuotation.Where(e => e.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)));
                gvViewQuotation.DataSource = lst;
                gvViewQuotation.DataBind();
            }
            else
            {
                BindEmptyGrid(gvViewQuotation);
            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        #endregion

        #region private Methods

        private void BindGridIssueDemandVoucher()
        {
            gvIssueDemandVoucher.DataSource = lstItemTransaction;
            gvIssueDemandVoucher.DataBind();
        }

        private void Reset()
        {
            i = 0;
            txtRemarks.Text = string.Empty;
            ltrl_err_msg.Text = string.Empty;
            txtMaterialDemandDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            for (i = 0; i < lstItemTransaction.Count; i++)
            {
                lstItemTransaction[i].ItemRequired = 0;
            }
            BindGridIssueDemandVoucher();
            EmptyDocumentList();
        }

        private Decimal TryToParse(string Value)
        {
            dec = 0;
            Decimal.TryParse(Value, out dec);
            return dec;
        }

        private void SetAllDefaultData(List<QuotationDOM> lst)
        {
            BindText(lst);
            BindGridQuotationDetails(lst);
        }

        private void BindUpdateText(List<IssueDemandVoucherDOM> lst)
        {
            //ctrl_UploadDocument.GetDocumentData(lst[0].Quotation.UploadDocumentId);
            lblContractorQuotationNumber.Text = lst[0].Quotation.ContractQuotationNumber.ToString();
            lblContractorQuotationDate.Text = lst[0].Quotation.QuotationDate.ToString("dd-MMM-yyyy");
            lblContractorName.Text = lst[0].Quotation.ContractorName.ToString();
            lblContractNumber.Text = lst[0].Quotation.ContractNumber.ToString();
            hdfContractorId.Value = lst[0].Quotation.ContractorId.ToString();
            txtMaterialDemandDate.Text = lst[0].MaterialDemandDate.ToString("dd-MMM-yyyy");
            txtRemarks.Text = lst[0].Remarks.ToString();
            GetDocumentData(lst[0].Quotation.UploadDocumentId);
        }

        private void BindText(List<QuotationDOM> lst)
        {
            lblContractorQuotationNumber.Text = lst[0].ContractQuotationNumber.ToString();
            lblContractorQuotationDate.Text = lst[0].OrderDate.ToString("dd-MMM-yyyy");
            lblContractorName.Text = lst[0].ContractorName.ToString();
            lblContractNumber.Text = lst[0].ContractNumber.ToString();
            hdfContractorId.Value = lst[0].ContractorId.ToString();
        }

        private void BindGridQuotationDetails(List<QuotationDOM> lst)
        {
            quotationBL = new QuotationBL();
            lstPreItemTransaction = new List<ItemTransaction>();
            if (lst.Count > 0)
            {
                lstPreItemTransaction = quotationBL.ReadContractorQuotationMapping(lst[0].ContractorQuotationId);
                gvQuotationDetails.DataSource = lstPreItemTransaction;
                gvQuotationDetails.DataBind();
            }
        }

        public MetaData CreateIssueDemandVoucher(IssueDemandVoucherDOM issueDemandVoucherDOM, Int32? IDVID)
        {
            if (lstItemTransaction != null)
            {
                metaData = new MetaData();
                issueDemandVoucherBL = new IssueDemandVoucherBL();
                metaData = issueDemandVoucherBL.CreateIssueDemandVoucher(issueDemandVoucherDOM, IDVID);
            }
            return metaData;
        }

        private void Enabled(Boolean Condition)
        {
            btnSaveDraft.Visible = Condition;
            btnReset.Visible = Condition;
            //btnCancel.Visible = Condition;
        }

        private IssueDemandVoucherDOM GetIssueDemandVoucherDetails()
        {
            strInvalid = string.Empty;
            flag = false;
            j = 0;
            quotationDOM = new QuotationDOM();
            issueDemandVoucherBL = new IssueDemandVoucherBL();
            issueDemandVoucherDOM = new IssueDemandVoucherDOM();
            issueDemandVoucherDOM.Quotation = new QuotationDOM();
            issueDemandVoucherDOM.Quotation.StatusType = new MetaData();
            if (IDVNId > 0)
            {
                issueDemandVoucherDOM.IssueDemandVoucherId = IDVNId;
            }
            issueDemandVoucherDOM.Quotation.UploadDocumentId = basePage.DocumentStackId;
            issueDemandVoucherDOM.Quotation.ContractQuotationNumber = lblContractorQuotationNumber.Text;
            issueDemandVoucherDOM.Quotation.ContractorId = Convert.ToInt32(hdfContractorId.Value);
            issueDemandVoucherDOM.Quotation.ContractorName = lblContractorName.Text;
            issueDemandVoucherDOM.Quotation.ContractNumber = lblContractNumber.Text;
            issueDemandVoucherDOM.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            issueDemandVoucherDOM.Quotation.OrderDate = Convert.ToDateTime(lblContractorQuotationDate.Text);

            issueDemandVoucherDOM.MaterialDemandDate = Convert.ToDateTime(txtMaterialDemandDate.Text.Trim());
            issueDemandVoucherDOM.Remarks = txtRemarks.Text.Trim();
            issueDemandVoucherDOM.CreatedBy = basePage.LoggedInUser.UserLoginId;
            for (i = 0; i < gvIssueDemandVoucher.Rows.Count; i++)
            {
                BigNo = 0;
                dec = 0;
                if (IDVNId > 0)
                {
                    BigNo = (lstItemTransaction[i].UnitLeft);
                }
                else
                {
                    BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].ItemRequired);
                }

                TextBox box = (TextBox)gvIssueDemandVoucher.Rows[i].Cells[9].FindControl("txtItemRequired");
                lblIndex = (Label)gvIssueDemandVoucher.Rows[i].Cells[0].FindControl("lblIndex");
                dec = TryToParse(box.Text);
                if (dec > 0)
                {
                    cnt = 0;
                    cnt = NumberDecimalPlaces(dec);
                    if (IDVNId > 0)
                    {
                        if (cnt > 2 || dec > BigNo)
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
                            flag = true;
                        }
                    }
                    else if (cnt > 2 || dec > lstItemTransaction[i].UnitLeft)
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
                        flag = true;
                    }
                }
                else
                {
                    if (j > 0)
                    {
                        strInvalid = strInvalid + ",   " + " " + lblIndex.Text.Trim();
                        j++;
                    }
                    else
                    {
                        strInvalid = strInvalid + lblIndex.Text.Trim();
                        j++;
                    }
                    flag = true;
                }
                if (string.IsNullOrEmpty(strInvalid))
                {
                    lstItemTransaction[i].ItemRequired = Convert.ToDecimal(box.Text);
                }
            }
            if (!string.IsNullOrEmpty(strInvalid))
            {
                strInvalid = "Unit Required allows only numeric value up to 2 decimal places & less than or equal to unit left at Index: " + strInvalid;
            }
            else
            {
                issueDemandVoucherDOM.Quotation.ItemTransaction = lstItemTransaction;
            }
            return issueDemandVoucherDOM;
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

        private int IDVNId
        {
            get
            {
                return (Int32)ViewState["IDVNId"];
            }
            set
            {
                ViewState["IDVNId"] = value;
            }
        }

        //private List<QuotationDOM> lstQuotationDOM
        //{
        //    get
        //    {
        //        return (List<QuotationDOM>)ViewState["lstQuotationDOM"];
        //    }
        //    set
        //    {
        //        ViewState["lstQuotationDOM"] = value;
        //    }
        //}

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