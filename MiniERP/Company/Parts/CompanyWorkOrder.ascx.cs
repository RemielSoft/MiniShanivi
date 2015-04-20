using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.IO;
using System.Net;
using System.Data;
using System.Configuration;

namespace MiniERP.Company.Parts
{
    public partial class CompanyWorkOrder : System.Web.UI.UserControl
    {
        #region Global Variables

        bool track = false;
        string pageName = String.Empty;

        BasePage basePage = new BasePage();

        TaxBL taxBL = new TaxBL();
        CompanyWorkOrderBL companyWOBL = new CompanyWorkOrderBL();
        MetaDataBL metaDataBL = new MetaDataBL();

        List<CompanyWorkOrderDOM> lstCWO = null;
        List<WorkOrderMappingDOM> lstWOM = null;
        List<BankGuaranteeDOM> lstBankGuarantee = null;
        List<MetaData> lstMetaData = null;

        CompanyWorkOrderDOM companyWO = null;
        WorkOrderMappingDOM workOrderMapping = null;
        BankGuaranteeDOM bankGuarantee = null;

        Decimal serviceTax = 0;
        Decimal vat = 0;
        Decimal cstWithCForm = 0;
        Decimal cstWithoutCForm = 0;
        Decimal freight = 0;
        Decimal totalDiscount = 0;



        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(btn_upload);

            Int32 companyWO = 0;
            PageDefaults();
            if (!IsPostBack)
            {
                EmptyDocumentList();
                DefaultLoad();
                companyWO = Convert.ToInt32(Request.QueryString["companyWOId"]);
                this.CompanyWOId = companyWO;
            }
            if (companyWO > 0)
            {
                EmptyDocumentList();
                PopulateWorkOrder(companyWO);
                PopulateCompanyWorkOrder(companyWO);
                populateBankGuarantee(companyWO);
                btnSave.Text = "Update";
                //GetDocumentData();
            }
        }

        private void DefaultLoad()
        {
            txtTotalDiscount.Enabled = false;
            calContractDate.EndDate = DateTime.Now.AddMonths(6);

            Session["tempWOData"] = null;
            Session["tempBGData"] = null;
            basePage.BindDropDown(ddlDiscountType, "Name", "Id", taxBL.ReadDiscountMode(null));
            SetValidationExpression();
        }

        private void PageDefaults()
        {
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void ddlDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDiscountType.SelectedValue == Convert.ToInt32(DiscountType.Percentage).ToString())
            {
                lblPercentage.Visible = true;
                imgRupee.Visible = false;
                txtTotalDiscount.Enabled = true;
                spndiscount.Visible = true;
                rfvDiscount.ValidationGroup = "WOM";
            }
            else if (ddlDiscountType.SelectedValue == Convert.ToInt32(DiscountType.Value).ToString())
            {
                lblPercentage.Visible = false;
                imgRupee.Visible = true;
                txtTotalDiscount.Enabled = true;
                spndiscount.Visible = true;
                rfvDiscount.ValidationGroup = "WOM";
            }
            else
            {
                lblPercentage.Visible = false;
                imgRupee.Visible = false;
                txtTotalDiscount.Enabled = false;
                spndiscount.Visible = false;
                txtTotalDiscount.Text = String.Empty;
                rfvDiscount.ValidationGroup = "empty";
            }
        }

        protected void btnAddWO_Click(object sender, EventArgs e)
        {
            Decimal totValue = 0;
            lstWOM = new List<WorkOrderMappingDOM>();
            workOrderMapping = new WorkOrderMappingDOM();
            if (!String.IsNullOrEmpty(txtAmount.Text.Trim()))
            {
                totValue = Convert.ToDecimal(txtAmount.Text.Trim());
            }
            workOrderMapping.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
            workOrderMapping.WorkOrderNumber = txtWorkOrderNo.Text.Trim();
            workOrderMapping.Area = txtArea.Text.Trim();
            workOrderMapping.Location = txtLocation.Text.Trim();

            workOrderMapping.TaxInformation = new Tax();

            if (!String.IsNullOrEmpty(txtServiceTax.Text.Trim()))
            {
                serviceTax = Convert.ToDecimal(txtServiceTax.Text.Trim());
                workOrderMapping.TaxInformation.ServiceTax = serviceTax;

            }
            if (!String.IsNullOrEmpty(txtVAT.Text.Trim()))
            {
                vat = Convert.ToDecimal(txtVAT.Text.Trim());
                workOrderMapping.TaxInformation.VAT = vat;

            }
            if (!String.IsNullOrEmpty(txtCSTWithCForm.Text.Trim()))
            {
                cstWithCForm = Convert.ToDecimal(txtCSTWithCForm.Text.Trim());
                workOrderMapping.TaxInformation.CSTWithCForm = cstWithCForm;

            }
            if (!String.IsNullOrEmpty(txtCSTWithoutCForm.Text.Trim()))
            {
                cstWithoutCForm = Convert.ToDecimal(txtCSTWithoutCForm.Text.Trim());
                workOrderMapping.TaxInformation.CSTWithoutCForm = cstWithoutCForm;

            }
            if (!String.IsNullOrEmpty(txtFreight.Text.Trim()))
            {
                freight = Convert.ToDecimal(txtFreight.Text.Trim());
                workOrderMapping.TaxInformation.Freight = freight;
            }

            workOrderMapping.TaxInformation.DiscountMode = new MetaData();
            if (ddlDiscountType.SelectedValue != "0")
            {
                workOrderMapping.TaxInformation.DiscountMode.Id = Convert.ToInt32(ddlDiscountType.SelectedValue);
                workOrderMapping.TaxInformation.DiscountMode.Name = ddlDiscountType.SelectedItem.Text;
                if (!String.IsNullOrEmpty(txtTotalDiscount.Text.Trim()))
                {
                    totalDiscount = Convert.ToDecimal(txtTotalDiscount.Text.Trim());
                    workOrderMapping.TaxInformation.TotalDiscount = totalDiscount;
                }
            }

            Decimal totPercentageDiscount = 0;
            if (btnAddWO.Text == "Add")
            {
                if (ddlDiscountType.SelectedValue == "1")
                {
                    totPercentageDiscount = serviceTax + vat + cstWithCForm + cstWithoutCForm;
                    if ((totValue * totalDiscount / 100) > totValue)
                    {
                        basePage.Alert("Discount Should Be Less Than Total Amount", btnAddWO);
                        txtTotalDiscount.Focus();
                        return;
                    }
                    else
                    {
                        totValue = totValue - (totValue * totalDiscount / 100);
                        workOrderMapping.TaxInformation.TotalNetValue = totValue + (totValue * totPercentageDiscount) / 100 + freight;
                    }
                }
                else
                {
                    totPercentageDiscount = serviceTax + vat + cstWithCForm + cstWithoutCForm;
                    if (totalDiscount > totValue)
                    {
                        basePage.Alert("Discount Should Be Less Than Total Amount", btnAddWO);
                        txtTotalDiscount.Focus();
                        return;
                    }
                    else
                    {
                        totValue = totValue - totalDiscount;
                        workOrderMapping.TaxInformation.TotalNetValue = totValue + (totValue * totPercentageDiscount) / 100 + freight;
                    }
                }
            }

            workOrderMapping.CreatedBy = basePage.LoggedInUser.UserLoginId;

            if (btnAddWO.Text == "update")
            {
                Int32 index = 0;
                lstWOM = (List<WorkOrderMappingDOM>)Session["tempWOData"];

                foreach (WorkOrderMappingDOM item in lstWOM)
                {
                    if (item.WorkOrderNumber == txtWorkOrderNo.Text.Trim() && index != this.Index)
                    {
                        basePage.Alert("Work Order Already Exist", btnAddWO);
                        return;
                    }
                    ++index;
                }

                if (!String.IsNullOrEmpty(txtAmount.Text))
                {
                    lstWOM[this.Index].Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                }
                else
                {
                    lstWOM[this.Index].Amount = 0;
                }

                lstWOM[this.Index].WorkOrderNumber = txtWorkOrderNo.Text.Trim();
                lstWOM[this.Index].Area = txtArea.Text.Trim();
                lstWOM[this.Index].Location = txtLocation.Text.Trim();
                if (!String.IsNullOrEmpty(txtServiceTax.Text))
                {
                    lstWOM[this.Index].TaxInformation.ServiceTax = Convert.ToDecimal(txtServiceTax.Text.Trim());
                }
                else
                {
                    lstWOM[this.Index].TaxInformation.ServiceTax = 0;
                }

                if (!String.IsNullOrEmpty(txtVAT.Text))
                {
                    lstWOM[this.Index].TaxInformation.VAT = Convert.ToDecimal(txtVAT.Text.Trim());
                }

                if (!String.IsNullOrEmpty(txtCSTWithCForm.Text))
                {
                    lstWOM[this.Index].TaxInformation.CSTWithCForm = Convert.ToDecimal(txtCSTWithCForm.Text.Trim());
                }
                else
                {
                    lstWOM[this.Index].TaxInformation.CSTWithCForm = 0;
                }

                if (!String.IsNullOrEmpty(txtCSTWithoutCForm.Text))
                {
                    lstWOM[this.Index].TaxInformation.CSTWithoutCForm = Convert.ToDecimal(txtCSTWithoutCForm.Text.Trim());
                }
                else
                {
                    lstWOM[this.Index].TaxInformation.CSTWithoutCForm = 0;
                }

                if (!String.IsNullOrEmpty(txtFreight.Text))
                {
                    lstWOM[this.Index].TaxInformation.Freight = Convert.ToDecimal(txtFreight.Text.Trim());
                }
                else
                {
                    lstWOM[this.Index].TaxInformation.Freight = 0;
                }

                if (!String.IsNullOrEmpty(txtTotalDiscount.Text) && ddlDiscountType.SelectedValue != "0")
                {
                    lstWOM[this.Index].TaxInformation.TotalDiscount = Convert.ToDecimal(txtTotalDiscount.Text.Trim());
                    lstWOM[this.Index].TaxInformation.DiscountMode.Id = Convert.ToInt32(ddlDiscountType.SelectedValue);
                    lstWOM[this.Index].TaxInformation.DiscountMode.Name = ddlDiscountType.SelectedItem.Text;
                }
                else
                {
                    lstWOM[this.Index].TaxInformation.TotalDiscount = 0;
                    lstWOM[this.Index].TaxInformation.DiscountMode.Id = Convert.ToInt32(ddlDiscountType.SelectedValue);
                    lstWOM[this.Index].TaxInformation.DiscountMode.Name = null;
                }

                if (ddlDiscountType.SelectedValue == "1")
                {
                    totPercentageDiscount = serviceTax + vat + cstWithCForm + cstWithoutCForm;
                    if ((totValue * totalDiscount / 100) > totValue)
                    {
                        basePage.Alert("Discount Should Be Less Than Total Amount", btnAddWO);
                        txtTotalDiscount.Focus();
                        return;
                    }
                    else
                    {
                        totValue = totValue - (totValue * totalDiscount / 100);
                        lstWOM[this.Index].TaxInformation.TotalNetValue = totValue + (totValue * totPercentageDiscount) / 100 + freight;
                    }
                }
                else
                {
                    totPercentageDiscount = serviceTax + vat + cstWithCForm + cstWithoutCForm;
                    if (totalDiscount > totValue)
                    {
                        basePage.Alert("Discount Should Be Less Than Total Amount", btnAddWO);
                        txtTotalDiscount.Focus();
                        return;
                    }
                    else
                    {
                        totValue = totValue - totalDiscount;
                        lstWOM[this.Index].TaxInformation.TotalNetValue = totValue + (totValue * totPercentageDiscount) / 100 + freight;
                    }
                }

                btnAddWO.Text = "Add";
                ViewState["index"] = null;

            }
            else
            {
                if (Session["tempWOData"] != null)
                {
                    lstWOM = (List<WorkOrderMappingDOM>)Session["tempWOData"];
                    if (lstWOM.Any(c => c.WorkOrderNumber == txtWorkOrderNo.Text.Trim()))
                    {
                        basePage.Alert("Work Order Already Exist", btnAddWO);
                        return;
                    }

                    lstWOM.Add(workOrderMapping);
                    Session["tempWOData"] = lstWOM;
                }
                else
                {
                    lstWOM.Add(workOrderMapping);
                    Session["tempWOData"] = lstWOM;
                }
            }

            Decimal totAmount = lstWOM.Sum(s => s.TaxInformation.TotalNetValue);
            txtTotAmount.Text = totAmount.ToString();
            txtTotAmount.Text = totAmount.ToString();
            gvWorkOrder.DataSource = lstWOM;
            gvWorkOrder.DataBind();
            ClearTaxesData();
        }

        protected void btnResetWO_Click(object sender, EventArgs e)
        {
            ClearTaxesData();
        }

        protected void gvWorkOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            this.Index = index;
            lstWOM = new List<WorkOrderMappingDOM>();
            if (e.CommandName == "lnkEdit")
            {
                lstWOM = (List<WorkOrderMappingDOM>)Session["tempWOData"];
                txtAmount.Text = lstWOM[index].Amount.ToString();
                txtWorkOrderNo.Text = lstWOM[index].WorkOrderNumber.ToString();
                txtArea.Text = lstWOM[index].Area.ToString();
                txtLocation.Text = lstWOM[index].Location.ToString();
                txtServiceTax.Text = lstWOM[index].TaxInformation.ServiceTax.ToString();
                txtVAT.Text = lstWOM[index].TaxInformation.VAT.ToString();
                txtCSTWithCForm.Text = lstWOM[index].TaxInformation.CSTWithCForm.ToString();
                txtCSTWithoutCForm.Text = lstWOM[index].TaxInformation.CSTWithoutCForm.ToString();
                txtFreight.Text = lstWOM[index].TaxInformation.Freight.ToString();
                txtTotalDiscount.Text = lstWOM[index].TaxInformation.TotalDiscount.ToString();
                ddlDiscountType.SelectedValue = lstWOM[index].TaxInformation.DiscountMode.Id.ToString();
                ddlDiscountType_SelectedIndexChanged(null, null);

                btnAddWO.Text = "update";
            }

            if (e.CommandName == "lnkDelete")
            {
                lstWOM = (List<WorkOrderMappingDOM>)Session["tempWOData"];
                lstWOM.RemoveAt(index);
                Decimal totAmount = lstWOM.Sum(s => s.TaxInformation.TotalNetValue);
                txtTotAmount.Text = totAmount.ToString();
                gvWorkOrder.DataSource = lstWOM;
                gvWorkOrder.DataBind();
                ClearTaxesData();
                btnAddWO.Text = "Add";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int outId = 0;
            int inId = 0;
            if (Session["tempWOData"] == null)
            {
                basePage.Alert(" Please fill the Work Order Amount details", btnSave);
                return;
            }

            if (Session["tempBGData"] == null)
            {
                basePage.Alert("Please fill the bank guarantee details", btnSave);
                TabContainer1.ActiveTabIndex = 1;
                return;
            }

            track = false;
            track = Int32.TryParse(txtContractNo.Text.Trim(), out inId);
            if (track && inId == 0)
            {
                basePage.Alert("Contract Number can not be Zero.", btnSave);
                return;
            }

            if (Session["tempBGData"] != null && Session["tempWOData"] != null)
            {
                companyWO = new CompanyWorkOrderDOM();
                companyWO = GetFormData();

                lstWOM = new List<WorkOrderMappingDOM>();
                lstWOM = (List<WorkOrderMappingDOM>)Session["tempWOData"];

                lstBankGuarantee = new List<BankGuaranteeDOM>();
                lstBankGuarantee = (List<BankGuaranteeDOM>)Session["tempBGData"];


                if (lstBankGuarantee.Count > 0)
                {
                    //UploadFile(lstBankGuarantee);
                }

                if (this.CompanyWOId > 0)
                {
                    outId = companyWOBL.UpdateCompanyWorkOrder(companyWO, lstWOM, lstBankGuarantee, this.CompanyWOId);
                }
                else
                {
                    lstMetaData = new List<MetaData>();
                    lstMetaData = companyWOBL.CreateCompanyWorkOrder(companyWO, lstWOM, lstBankGuarantee);
                    if (lstMetaData.Count > 0)
                        inId = lstMetaData[0].Id;
                    else
                        basePage.Alert("Contract Number is Already Created", btnSave);
                }

                if (inId > 0 || outId > 0)
                {
                    CreateDocumentMapping();
                    if (this.CompanyWOId > 0)
                    {
                        this.CompanyWOId = 0;
                        basePage.Alert("Company Work Order Updated Successfully", btnSave, "ViewCompanyWorkOrder.aspx");
                    }
                    else
                    {
                        try
                        {
                            basePage.Alert("Contract Work Order Number " + lstMetaData[0].Name + " Saved Successfully", btnSave, GlobalConstants.P_Company_Work_Order);

                        }
                        catch (Exception)
                        {

                            basePage.Alert("Work Order With Same Contract No is Already Created", btnSave);
                        }
                    }


                    ClearBankGuarantee();
                    ClearTaxesData();
                    EmptyTempGridsAndSession();
                    ClearFormData();
                }
                else
                {
                    basePage.Alert("Work Order With Same Contract No is Already Created", btnSave);
                }
            }

        }

        protected void btnAddBG_Click(object sender, EventArgs e)
        {
            String filesName;
            HttpPostedFile[] hpf;
            bankGuarantee = new BankGuaranteeDOM();
            lstBankGuarantee = new List<BankGuaranteeDOM>();
            bankGuarantee.StartDate = DateTime.ParseExact(txtStartDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            bankGuarantee.EndDate = DateTime.ParseExact(txtEndDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan diff = bankGuarantee.EndDate.Subtract(bankGuarantee.StartDate);
            int day = diff.Days;
            if (day < 0)
            {
                basePage.Alert("End Date cannot be less then start date", btnAddBG);
                //imgBtnED.Focus();
            }
            else
            {
                bankGuarantee.Amount = Convert.ToDecimal(txtAmountBG.Text.Trim());
                bankGuarantee.BankName = txtBankName.Text.Trim();
                GetUploadedFiles(out filesName, out hpf);
                bankGuarantee.UploadedDocument = filesName;
                bankGuarantee.HPF = hpf;
                //bankGuarantee.FileName = TrimFileName(filesName);
                //bankGuarantee.UploadedDocumentId = DocumentStackId;
                bankGuarantee.Remarks = txtRemark.Text.Trim();
                bankGuarantee.CreatedBy = basePage.LoggedInUser.UserLoginId;

                if (btnAddBG.Text == "Update")
                {
                    lstBankGuarantee = (List<BankGuaranteeDOM>)Session["tempBGData"];


                    lstBankGuarantee[this.Index].StartDate = DateTime.ParseExact(txtStartDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    lstBankGuarantee[this.Index].EndDate = DateTime.ParseExact(txtEndDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (!String.IsNullOrEmpty(filesName))
                    {
                        lstBankGuarantee[this.Index].UploadedDocument = filesName;
                        //lstBankGuarantee[this.Index].FileName = TrimFileName(filesName);
                    }
                    //lstBankGuarantee[this.Index].UploadedDocumentId = DocumentStackId;
                    lstBankGuarantee[this.Index].Amount = Convert.ToDecimal(txtAmountBG.Text.Trim());
                    lstBankGuarantee[this.Index].BankName = txtBankName.Text.Trim();
                    lstBankGuarantee[this.Index].Remarks = txtRemark.Text.Trim();

                    btnAddBG.Text = "Add";
                    ViewState["index"] = null;

                }
                else
                {
                    if (Session["tempBGData"] != null)
                    {
                        lstBankGuarantee = (List<BankGuaranteeDOM>)Session["tempBGData"];
                        lstBankGuarantee.Add(bankGuarantee);
                        Session["tempBGData"] = lstBankGuarantee;
                    }
                    else
                    {
                        lstBankGuarantee.Add(bankGuarantee);
                        Session["tempBGData"] = lstBankGuarantee;
                    }
                }


                gbBankGuarantee.DataSource = lstBankGuarantee;
                gbBankGuarantee.DataBind();
                ClearBankGuarantee();
            }
        }

        protected void gbBankGuarantee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            this.Index = index;
            if (e.CommandName == "lnkEdit")
            {
                lstBankGuarantee = (List<BankGuaranteeDOM>)Session["tempBGData"];
                txtStartDate.Text = lstBankGuarantee[index].StartDate.ToString("dd/MM/yyyy");
                txtEndDate.Text = lstBankGuarantee[index].EndDate.ToString("dd/MM/yyyy");
                txtAmountBG.Text = lstBankGuarantee[index].Amount.ToString();
                txtBankName.Text = lstBankGuarantee[index].BankName;
                txtRemark.Text = lstBankGuarantee[index].Remarks;
                btnAddBG.Text = "Update";
            }

            if (e.CommandName == "lnkDelete")
            {
                lstBankGuarantee = (List<BankGuaranteeDOM>)Session["tempBGData"];
                lstBankGuarantee.RemoveAt(index);
                gbBankGuarantee.DataSource = lstBankGuarantee;
                gbBankGuarantee.DataBind();
                ClearBankGuarantee();
                btnAddBG.Text = "Add";
            }
        }

        protected void btnResetBG_Click(object sender, EventArgs e)
        {
            ClearBankGuarantee();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyWorkOrder.aspx");
            //ClearFormData();
            //ClearBankGuarantee();
            //ClearTaxesData();
            // ClearServiceDetail();
            //EmptyTempGridsAndSession();
        }

        #endregion

        #region Private Methods

        private CompanyWorkOrderDOM GetFormData()
        {
            companyWO = new CompanyWorkOrderDOM();
            if (!String.IsNullOrEmpty(txtContractDate.Text))
            {
                companyWO.ContractDate = DateTime.ParseExact(txtContractDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            companyWO.ContractNumber = txtContractNo.Text.Trim();
            companyWO.WorkOrderDescription = txtContractDesc.Text.Trim();
            companyWO.TotalNetValue = Convert.ToDecimal(txtTotAmount.Text.Trim());
            companyWO.UploadDocumentId = basePage.DocumentStackId; ;
            if (this.CompanyWOId > 0)
            {
                companyWO.ModifiedBy = basePage.LoggedInUser.UserLoginId;
            }
            else
                companyWO.CreatedBy = basePage.LoggedInUser.UserLoginId;

            return companyWO;
        }

        private void ClearBankGuarantee()
        {
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtAmountBG.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtRemark.Text = string.Empty;
            btnAddBG.Text = "Add";
            this.Index = 0;
        }

        private void ClearTaxesData()
        {
            txtAmount.Text = string.Empty;
            txtWorkOrderNo.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtArea.Text = string.Empty;

            txtServiceTax.Text = string.Empty;
            txtVAT.Text = string.Empty;
            txtCSTWithCForm.Text = string.Empty;
            txtCSTWithoutCForm.Text = string.Empty;
            txtFreight.Text = string.Empty;
            txtTotalDiscount.Text = string.Empty;
            ddlDiscountType.SelectedValue = "0";
            txtTotalDiscount.Enabled = false;
            btnAddWO.Text = "Add";
            this.Index = 0;
        }

        private void EmptyTempGridsAndSession()
        {
            txtTotAmount.Text = string.Empty;
            Session["tempBGData"] = null;
            Session["tempWOData"] = null;
            Session["tempSDData"] = null;
            BindEmptyGrid(gvWorkOrder);
            BindEmptyGrid(gbBankGuarantee);
            //BindEmptyGrid(gvServiceDetail);
            ClearTaxesData();
            btnSave.Text = "Save";
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void ClearFormData()
        {
            txtContractDate.Text = string.Empty;
            txtContractNo.Text = string.Empty;
            txtContractDesc.Text = string.Empty;
            txtContractNo.Enabled = true;
        }

        private void SetValidationExpression()
        {
            rngServiceTax.ErrorMessage = "Service Tax Should Be Numeric ";

            rngVat.ErrorMessage = "VAT Should Be Numeric ";

            rngCST.ErrorMessage = "CST (With C Form) Should Be Numeric ";

            rngCSTWithout.ErrorMessage = "CST (Without C Form) Should Be Numeric ";

            rngFreight.ErrorMessage = "Freight Should Be Numeric ";

            rngTotalDiscount.ErrorMessage = "Total Discount Should Be Numeric ";

            rvAmount.ErrorMessage = "Amount Should Be Numeric";
            rvAmountBG.ErrorMessage = "Amount Should Be Numeric";
        }

        private void GetUploadedFiles(out String filesName, out HttpPostedFile[] HPF)
        {
            int j = 0;
            filesName = string.Empty;
            HPF = new HttpPostedFile[20];
            try
            {
                // Get the HttpFileCollection 
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (!string.IsNullOrEmpty(hpf.FileName))
                    {
                        //hpf.SaveAs(Server.MapPath("Images") + "\\" +
                        //  Path.GetFileName(hpf.FileName));
                        filesName = filesName + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "/" + Path.GetFileName(hpf.FileName) + ",";
                        HPF[j] = hpf;
                        j++;
                    }
                }
                filesName = filesName.TrimEnd(',');
            }
            catch (Exception ex)
            {
                basePage.Alert(ex.Message, btnAddBG);
            }

        }

        //private void UploadFile(List<BankGuaranteeDOM> lst)
        //{
        //    String fileName = string.Empty;

        //    try
        //    {
        //        // Get the HttpFileCollection 
        //        foreach (BankGuaranteeDOM item in lst)
        //        {
        //            HttpPostedFile[] hpf = item.HPF;
        //            fileName = item.UploadedDocument;
        //            String[] str = fileName.Split(',');
        //            if (str.Length > 0)
        //            {
        //                for (int j = 0; j < str.Length; j++)
        //                {
        //                    hpf[j].SaveAs(Server.MapPath("~\\BankGuaranteeDoc") + "\\" +
        //                                             Path.GetFileName(str[j].ToString()));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Response.Write(ex.Message);

        //    }
        //}

        //private String TrimFileName(String filesName)
        //{
        //    String[] str = filesName.Split(',');
        //    String shortFileName = string.Empty;
        //    foreach (String item in str)
        //    {
        //        if (!string.IsNullOrEmpty(item))
        //        {
        //            shortFileName += item.Substring(20) + " ,";
        //        }

        //    }
        //    shortFileName = shortFileName.TrimEnd(',');

        //    return shortFileName;
        //}

        private void PopulateCompanyWorkOrder(int companyWOId)
        {
            txtContractNo.Enabled = false;
            lstCWO = new List<CompanyWorkOrderDOM>();
            lstCWO = companyWOBL.ReadCompOrder(companyWOId);
            if (lstCWO.Count > 0)
            {
                txtContractDate.Text = lstCWO[0].ContractDate.ToString("dd/MM/yyyy");
                txtContractNo.Text = lstCWO[0].ContractNumber;
                txtContractDesc.Text = lstCWO[0].WorkOrderDescription;
                txtTotAmount.Text = lstCWO[0].TotalNetValue.ToString();
                GetDocumentData(lstCWO[0].UploadDocumentId);
            }
        }

        private void PopulateWorkOrder(int companyWOId)
        {
            lstWOM = new List<WorkOrderMappingDOM>();
            lstWOM = companyWOBL.ReadCompanyWorkOrderMapping(companyWOId);
            Session["tempWOData"] = lstWOM;
            if (lstWOM.Count > 0)
            {
                gvWorkOrder.DataSource = lstWOM;
                gvWorkOrder.DataBind();
            }
        }

        private void populateBankGuarantee(int companyWOId)
        {
            lstBankGuarantee = new List<BankGuaranteeDOM>();
            lstBankGuarantee = companyWOBL.ReadCompanyWorkOrderBankGuarantee(companyWOId);
            Session["tempBGData"] = lstBankGuarantee;
            if (lstBankGuarantee.Count > 0)
            {
                gbBankGuarantee.DataSource = lstBankGuarantee;
                gbBankGuarantee.DataBind();
            }
        }

        #endregion

        #region Public Properties

        public int Index
        {
            get { return (int)ViewState["index"]; }
            set { ViewState["index"] = value; }
        }

        public int CompanyWOId
        {
            get { return Convert.ToInt32(ViewState["companyWOId"]); }
            set { ViewState["companyWOId"] = value; }
        }

        #endregion

        #region Upload Document Code

        #region Private Global Variable(s)

        DocumentBL documentBL = new DocumentBL();

        Document document = null;
        Int32 Year = 0;
        //Int32 Index = 0;
        bool flag = false;

        String Head_Folder_Path = String.Empty;
        String Sub_Folder_Path = String.Empty;
        String File_Extension = String.Empty;
        String File_Path = String.Empty;

        DataSet page_Data = null;

        List<Document> lst_documents = null;
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
                lst_documents = new List<Document>();
                lst_documents = basePage.DocumentsList;
                lst_documents.RemoveAt(Index);
                basePage.DocumentsList = lst_documents;
                BindDocument();
            }
            else if (e.CommandName == "OpenFile")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                Index = Convert.ToInt32(e.CommandArgument);
                lst_documents = new List<Document>();
                lst_documents = basePage.DocumentsList;

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtn_file");

                string fileName = lst_documents[Index].Replaced_Name;
                string strURL = lst_documents[Index].Path + @"\" + lst_documents[Index].Replaced_Name;

                Session["FilePath"] = Server.MapPath(strURL);
                Session["OriginalFileName"] = lst_documents[Index].Original_Name;
                Session["ReplacedFileName"] = lst_documents[Index].Replaced_Name;
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

        private Int32 CreateAndReadDocumentStackId()
        {
            document = new Document();
            document.CreatedBy = basePage.LoggedInUser.UserLoginId;
            basePage.DocumentStackId = documentBL.CreateAndReadDocumnetStackId(document);
            return basePage.DocumentStackId;
        }

        private void DirectoryHandle(FileUpload fileupload)
        {
            if (fileupload.HasFile)
            {
                if (fileupload.FileContent.Length > 10485760)
                {
                    basePage.Alert("You can upload up to 10 megabytes (MB) in size at a time", FileUpload_Control);
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
                        if (basePage.DocumentStackId != 0)
                        {
                            document = new Document();
                            lst_documents = new List<Document>();
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

                                //Upload file in respective path
                                FileUpload_Control.SaveAs(File_Path);

                                document.DocumentId = basePage.DocumentStackId;

                                document.Path = ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString() + @"\" + RequestPageName;
                                document.LastIndex = basePage.DocumentSerial;


                                if (basePage.DocumentsList == null)
                                {
                                    lst_documents.Add(document);
                                }
                                else
                                {
                                    lst_documents = basePage.DocumentsList;
                                    lst_documents.Add(document);
                                }

                                //Add Doc's info in list
                                basePage.DocumentsList = lst_documents;
                            }
                        }
                    }
                }
            }
            else
            {
                basePage.ShowMessage("Please Select File.");
            }
        }

        private void BindDocument()
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
            documentBL = new DocumentBL();
            lst_documents = new List<Document>();
            lst_documents = basePage.DocumentsList;

            if (lst_documents != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                documentBL.ResetDocumentMapping(Convert.ToInt32(basePage.DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document doc in lst_documents)
                {
                    document = new Document();
                    document.DocumentId = Convert.ToInt32(basePage.DocumentStackId);
                    document.Original_Name = doc.Original_Name;
                    document.Replaced_Name = doc.Replaced_Name;
                    document.Path = doc.Path;
                    //base_Page.DocumentSerial is the last updated document
                    document.LastIndex = basePage.DocumentSerial;
                    document.CreatedBy = basePage.LoggedInUser.UserLoginId;
                    document.Id = doc.Id;
                    documentBL.CreateDocumentMapping(document);
                }
            }
        }

        private void GetDocumentData(Int32 DocumentId)
        {
            documentBL = new DocumentBL();
            lst_documents = new List<Document>();
            if (DocumentId != Int32.MinValue)
            {
                lst_documents = documentBL.ReadDocumnetMapping(DocumentId);
                if (lst_documents.Count >= 1)
                {
                    basePage.DocumentsList = lst_documents;
                    basePage.DocumentStackId = lst_documents[0].DocumentId;
                    basePage.DocumentSerial = lst_documents[0].LastIndex;
                    //base_Page.Page_Name = RequestPageName;
                    BindDocument();
                }
                else
                {
                    basePage.DocumentsList = null;
                    BindDocument();
                }
            }
            else
            {
                basePage.DocumentsList = null;
                BindDocument();
            }
        }

        private void EmptyDocumentList()
        {
            basePage.DocumentStackId = 0;
            basePage.DocumentSerial = 0;
            basePage.DocumentsList = null;
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