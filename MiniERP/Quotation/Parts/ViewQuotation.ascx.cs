using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniERP.Shared;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.Configuration;
using MiniERP.Quotation;

namespace MiniERP.Parts
{
    public partial class ViewQuotation : System.Web.UI.UserControl
    {
        #region Global Variable(s)

        Int32 daysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);

        bool track;
        CheckBox chbx = null;
        HiddenField hdf = null;
        HiddenField hdf_id = null;
        BasePage basePage = new BasePage();
        ContractorBL contractorBL = new ContractorBL();
        SupplierBL supplierBL = new SupplierBL();
        QuotationBL quotationBL = new QuotationBL();
        DeliveryScheduleBL deliveryScheduleBL = new DeliveryScheduleBL();
        PaymentTermBL paymentTermBL = new PaymentTermBL();
        TermAndConditionBL termAndConditionBL = new TermAndConditionBL();

        List<QuotationDOM> lstQuotation = null;
        List<DeliveryScheduleDOM> lstDeliverySchedule = null;
        List<PaymentTerm> lstPaymentTerm = null;
        List<TermAndCondition> lstTermAndCondition = null;
        List<ItemTransaction> lstItemTransaction = null;

        QuotationDOM quotation = null;
        BasePage base_page = new BasePage();

        DateTime currentDate = DateTime.MinValue;
        DateTime twoYearBackDate = DateTime.MinValue;


        #endregion

        #region Protected Events

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                String pageType = this.PageType;

                currentDate = DateTime.Now;
                twoYearBackDate = DateTime.Now.AddDays(-daysCount);
                if (rbtnList.SelectedValue != "2" && rbtnList.SelectedValue != "3")
                {
                    this.FromDate = twoYearBackDate;
                    this.ToDate = currentDate;
                    txtFromDate.Text = this.FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Text = this.ToDate.ToString("dd/MM/yyyy");
                }



                if (this.PageType == "Contractor")
                {
                    basePage.BindDropDownData(ddlName, "Name", "ContractorId", contractorBL.ReadContractor(null));
                }
                else
                {
                    basePage.BindDropDownData(ddlName, "Name", "SupplierId", supplierBL.ReadSupplier(null));
                    lblTitle.Text = "View Supplier Purchase Order";
                    lblSubtitle.Text = "Supplier Purchase Order";
                    lblName.Text = "Supplier Name";
                    rbtnList.Items[0].Text = "Supplier Name";
                    rbtnList.Items.RemoveAt(1);
                }
                ddlName.Items.Insert(0, new ListItem("All", "0"));
                ddlName.SelectedValue = "0";

                BindGrid(currentDate, twoYearBackDate);
                calFromDate.EndDate = DateTime.Now;
                calToDate.EndDate = DateTime.Now;

            }
        }

        private void PageDefaults()
        {
            basePage.Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();

        }

        protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rbtnList.SelectedValue == "1")
            {
                pnlName.Visible = true;
                pnlNumber.Visible = false;
                txtNumber.Text = string.Empty;

                txtFromDate.Text = DateTime.Now.AddDays(-daysCount).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                BindGrid(currentDate, twoYearBackDate);

                //gvViewQuotation.DataSource = new List<object>();
                //gvViewQuotation.DataBind();
            }
            if (rbtnList.SelectedValue == "2")
            {
                pnlNumber.Visible = true;
                pnlName.Visible = false;
                lblNumber.Text = "Contract Number";

                txtNumber.Text = string.Empty;
                ddlName.SelectedValue = "0";
                txtToDate.Text = string.Empty;
                txtFromDate.Text = string.Empty;

                gvViewQuotation.DataSource = new List<object>();
                gvViewQuotation.DataBind();

                this.FromDate = DateTime.MinValue;
                this.ToDate = DateTime.MinValue;
            }
            if (rbtnList.SelectedValue == "3")
            {
                pnlNumber.Visible = true;
                pnlName.Visible = false;
                lblNumber.Text = "Purchase Order Number";

                txtNumber.Text = string.Empty;
                ddlName.SelectedValue = "0";
                txtToDate.Text = string.Empty;
                txtFromDate.Text = string.Empty;

                gvViewQuotation.DataSource = new List<object>();
                gvViewQuotation.DataBind();

                this.FromDate = DateTime.MinValue;
                this.ToDate = DateTime.MinValue;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Int32 contractorId = 0;
            Int32 supplierId = 0;
            DateTime toDate = DateTime.MinValue;
            DateTime fromDate = DateTime.MinValue;
            String contractNo = String.Empty;
            String quotationNo = String.Empty;

            lstQuotation = new List<QuotationDOM>();

            if (rbtnList.SelectedValue == "1")
            {
                //if (ddlName.SelectedIndex != 0 && (!String.IsNullOrEmpty(txtFromDate.Text)) && (!String.IsNullOrEmpty(txtToDate.Text)))
                if ((!String.IsNullOrEmpty(txtFromDate.Text)) && (!String.IsNullOrEmpty(txtToDate.Text)))
                {
                    if (this.PageType == "Contractor")
                    {
                        contractorId = Convert.ToInt32(ddlName.SelectedValue);
                    }
                    else
                        supplierId = Convert.ToInt32(ddlName.SelectedValue);
                    fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            if (rbtnList.SelectedValue == "2")
            {
                if (!String.IsNullOrEmpty(txtNumber.Text))
                {
                    contractNo = txtNumber.Text.Trim();
                }

            }

            if (rbtnList.SelectedValue == "3")
            {
                if (!String.IsNullOrEmpty(txtNumber.Text))
                {
                    quotationNo = txtNumber.Text.Trim();
                }

            }

            this.ContractorId = contractorId;
            this.SupplierId = supplierId;
            this.FromDate = fromDate;
            this.ToDate = toDate;
            this.ContractNo = contractNo;
            this.QuotationNo = quotationNo;

            if (this.PageType == "Contractor")
            {
                lstQuotation = quotationBL.ReadContractorQuotation(contractorId, toDate, fromDate, contractNo, quotationNo);
            }
            else
                lstQuotation = quotationBL.ReadSupplierQuotation(supplierId, toDate, fromDate, contractNo, quotationNo);
            if (lstQuotation.Count > 0)
            {
                gvViewQuotation.DataSource = lstQuotation;
                gvViewQuotation.DataBind();
            }
            else
            {
                BindEmptyGrid(gvViewQuotation);
                
            }

        }

        protected void gvViewQuotation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str = Convert.ToString(e.CommandArgument);
            string[] strid = str.Split(',');
            int id = Convert.ToInt32(strid[0].ToString());



            if (e.CommandName == "lnkPrint")
            {
                string quotationNo = strid[1].ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtnPrint");
                if (this.PageType == "Contractor")
                {

                    basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/ContractorQuotation.aspx?id=" + id + "&quotationNo=" + quotationNo + "", "ContractorQuotation");

                }
                else
                {
                    //Response.Redirect("~/SSRReport/SupplierQuotation.aspx?id=" + id + "&quotationNo=" + quotationNo + "");
                    basePage.OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/SupplierQuotation.aspx?id=" + id + "&quotationNo=" + quotationNo + "", "ContractorQuotation");
                }
            }

            if (e.CommandName == "lnkQuotation")
            {
                lstItemTransaction = new List<ItemTransaction>();
                lstQuotation = new List<QuotationDOM>();


                if (this.PageType == "Contractor")
                {
                    lstItemTransaction = quotationBL.ReadContractorQuotationMapping(id);
                    lstQuotation = quotationBL.ReadContractorQuotation(id, null);
                    BindEmptyGrid(gvContractorQuotationItems);
                    if (lstItemTransaction.Count > 0)
                    {
                        gvContractorQuotationItems.DataSource = lstItemTransaction;
                        gvContractorQuotationItems.DataBind();
                        gvContractorQuotationItems.Columns[9].Visible = true;
                        lblTotalExciseDuty.Visible = false;
                    }

                }
                else
                {
                    lstItemTransaction = quotationBL.ReadSupplierQuotationMapping(id);
                    lstQuotation = quotationBL.ReadSupplierQuotation(id, null);
                    //gvQuotationItems.Columns[0].Visible = false;
                    BindEmptyGrid(gvSupplierQuotationItems);
                    if (lstItemTransaction.Count > 0)
                    {
                        gvSupplierQuotationItems.DataSource = lstItemTransaction;
                        gvSupplierQuotationItems.DataBind();

                    }

                }
                //For Document
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                if (hdfc != null)
                    updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                else
                    updcFile.GetDocumentData(Int32.MinValue);
                //End............
                CalculateAllToalValue();

                lstDeliverySchedule = new List<DeliveryScheduleDOM>();
                if (this.PageType == "Contractor")
                {
                    lstDeliverySchedule = deliveryScheduleBL.ReadDeliveryScheduleByQuotationID(id, Convert.ToInt16(QuotationType.Contractor));
                }
                else
                {
                    lstDeliverySchedule = deliveryScheduleBL.ReadDeliveryScheduleByQuotationID(id, Convert.ToInt16(QuotationType.Supplier));
                    gvDeliverySchedule.Columns[0].Visible = false;
                }
                BindEmptyGrid(gvDeliverySchedule);
                if (lstDeliverySchedule.Count > 0)
                {
                    gvDeliverySchedule.DataSource = lstDeliverySchedule;
                    gvDeliverySchedule.DataBind();
                }

                lstPaymentTerm = new List<PaymentTerm>();
                if (this.PageType == "Contractor")
                {
                    lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(id, Convert.ToInt16(QuotationType.Contractor));
                }
                else
                    lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(id, Convert.ToInt16(QuotationType.Supplier));
                BindEmptyGrid(gvPaymentTerm);
                if (lstPaymentTerm.Count > 0)
                {
                    gvPaymentTerm.DataSource = lstPaymentTerm;
                    gvPaymentTerm.DataBind();
                }

                lstTermAndCondition = new List<TermAndCondition>();
                if (this.PageType == "Contractor")
                {
                    lstTermAndCondition = termAndConditionBL.ReadTermAndConditionByQuotationID(id, Convert.ToInt16(QuotationType.Contractor));
                }
                else
                    lstTermAndCondition = termAndConditionBL.ReadTermAndConditionByQuotationID(id, Convert.ToInt16(QuotationType.Supplier));
                BindEmptyGrid(gvTermAndCondition);
                if (lstTermAndCondition.Count > 0)
                {
                    gvTermAndCondition.DataSource = lstTermAndCondition;
                    gvTermAndCondition.DataBind();
                }
                ModalPopupExtender2.Show();
            }

            if (e.CommandName == "lnkEdit")
            {
                if (this.PageType == "Contractor")
                {
                    Response.Redirect("~/Quotation/ContractorQuotation.aspx?quotationId=" + id);
                }
                else
                    Response.Redirect("~/Quotation/SupplierQuotation.aspx?quotationId=" + id);
            }

            if (e.CommandName == "lnkDelete")
            {
                String errorMessage = string.Empty;
                if (this.PageType == "Contractor")
                {
                    errorMessage = quotationBL.DeleteContractorQuotation(id, Convert.ToInt16(QuotationType.Contractor), basePage.LoggedInUser.UserLoginId);
                }
                else
                    errorMessage = quotationBL.DeleteSupplierQuotation(id, Convert.ToInt16(QuotationType.Supplier), basePage.LoggedInUser.UserLoginId);
                if (errorMessage == "")
                {
                    // basePage.Alert("Quotation Deleted Successfully", gvViewQuotation);
                    lstQuotation = new List<QuotationDOM>();
                    if (this.ContractorId == 0 && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
                    {
                        BindGrid(this.ToDate, this.FromDate);
                        ViewState["FromDate"] = DateTime.MinValue;
                        ViewState["ToDate"] = DateTime.MinValue;
                    }
                    else
                    {
                        if (this.PageType == "Contractor")
                        {
                            lstQuotation = quotationBL.ReadContractorQuotation(this.ContractorId, this.ToDate, this.FromDate, this.ContractNo, this.QuotationNo);
                        }
                        else
                            lstQuotation = quotationBL.ReadSupplierQuotation(this.SupplierId, this.ToDate, this.FromDate, this.ContractNo, this.QuotationNo);
                        if (lstQuotation.Count > 0)
                        {
                            gvViewQuotation.DataSource = lstQuotation;
                            gvViewQuotation.DataBind();
                            ResetViewState();
                        }

                    }


                }
                else
                {
                    basePage.ShowMessageWithUpdatePanel(errorMessage);
                }

            }




            if (e.CommandName == "lnkGenerate")
            {
                int outId = 0;
                lstQuotation = new List<QuotationDOM>();
                quotation = new QuotationDOM();
                quotation.StatusType = new MetaData();
                if (this.PageType == "Contractor")
                {
                    quotation.ContractorQuotationId = id;
                }
                else
                    quotation.SupplierQuotationId = id;
                quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                quotation.ApprovedRejectedBy = basePage.LoggedInUser.UserLoginId;
                if (this.PageType == "Contractor")
                {
                    outId = quotationBL.UpdateContractorQuotationStatus(quotation);
                }
                else
                    outId = quotationBL.UpdateSupplierQuotationStatus(quotation);
                if (outId > 0)
                {
                    //basePage.Alert("Quotation Generated Successfully", gvViewQuotation);
                    if (this.ContractorId == 0 && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
                    {
                        BindGrid(this.ToDate, this.FromDate);
                    }

                }
            }

            ///Close Quotations




            if (e.CommandName == "lnkClose")
            {
                int outId = 0;
                lstQuotation = new List<QuotationDOM>();
                quotation = new QuotationDOM();
                quotation.StatusType = new MetaData();
                if (this.PageType == "Contractor")
                {
                    quotation.ContractorQuotationId = id;
                }
                else
                    quotation.SupplierQuotationId = id;
                quotation.StatusType.Id = Convert.ToInt32(StatusType.Close);
                quotation.ApprovedRejectedBy = basePage.LoggedInUser.UserLoginId;
                if (this.PageType == "Contractor")
                {
                    outId = quotationBL.UpdateContractorQuotationStatus(quotation);
                }
                else
                    outId = quotationBL.UpdateSupplierQuotationStatus(quotation);
                if (outId > 0)
                {
                    //basePage.Alert("Quotation Generated Successfully", gvViewQuotation);
                    if (this.ContractorId == 0 && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
                    {
                        BindGrid(this.ToDate, this.FromDate);
                    }

                }
            }





        }

        protected void gvViewQuotation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnDelet = (LinkButton)e.Row.FindControl("lbtnDelete");
                LinkButton lbtnGenerate = (LinkButton)e.Row.FindControl("lbtnGenerate");
                LinkButton lbtnPrint = (LinkButton)e.Row.FindControl("lbtnPrint");
                LinkButton lbtnClose = (LinkButton)e.Row.FindControl("lbtnClose");
                int status = Convert.ToInt32(((HiddenField)e.Row.FindControl("approveStatus")).Value);
                Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");

                if (basePage.LoggedInUser.Role().Equals(AuthorityLevelType.Admin.ToString()))
                {
                    if (Convert.ToInt32(StatusType.InComplete) == status)
                    {
                        lbtnGenerate.Visible = false;
                        lbtnClose.Visible = false;
                        lbtnPrint.Visible = false;

                    }
                    if(Convert.ToInt32(StatusType.Pending) == status)
                    {
                        lbtnGenerate.Visible = false;
                        lbtnClose.Visible = false;
                        lbtnPrint.Visible = true;
                    }
                    if (Convert.ToInt32(StatusType.Approved) == status)
                    {
                        lbtnEdit.Visible = false;
                        lbtnDelet.Visible = false;
                        lbtnGenerate.Visible = true;
                        lbtnClose.Visible = false;
                        lbtnPrint.Visible = true;



                    }
                    if (Convert.ToInt32(StatusType.Rejected) == status)
                    {
                        lbtnEdit.Visible = false;
                        lbtnDelet.Visible = true;
                        lbtnGenerate.Visible = false;
                        lbtnClose.Visible = true;
                        lbtnPrint.Visible = false;
                        
                    }
                    if(Convert.ToInt32(StatusType.Generated) == status)
                    {
                        lbtnEdit.Visible = false;
                        lbtnDelet.Visible = true;
                        lbtnGenerate.Visible = false;
                        lbtnClose.Visible = true;
                        lbtnPrint.Visible = true;




                    }
                    if (Convert.ToInt32(StatusType.Close) == status || Convert.ToInt32(StatusType.Close) == status)
                    {
                        lbtnClose.Visible = false;
                        lbtnDelet.Visible = false;
                        lbtnGenerate.Visible = false;
                        lbtnClose.Visible = false;
                        lbtnEdit.Visible = false;
                        lbtnGenerate.Visible = false;

                    }
                }
                else
                {
                    if ((Convert.ToInt32(StatusType.Pending) == status || Convert.ToInt32(StatusType.InComplete) == status) && lblCreatedBy.Text.Trim().Equals(basePage.LoggedInUser.UserLoginId))
                    {
                        lbtnDelet.Visible = true;
                        lbtnEdit.Visible = true;
                        lbtnGenerate.Visible = false;
                        lbtnClose.Visible = false;
                        lbtnPrint.Visible = false;
                    }
                    else
                    {
                        lbtnDelet.Visible = false;
                        lbtnEdit.Visible = false;
                        lbtnGenerate.Visible = false;
                        lbtnClose.Visible = false;
                    }
                }




                Label lblHCName = ((Label)gvViewQuotation.HeaderRow.Cells[2].FindControl("lblHCName"));
                Label CName = ((Label)e.Row.FindControl("lblCName"));
                Label lblWorkOrderDate = ((Label)gvViewQuotation.HeaderRow.Cells[1].FindControl("lblWorkOrderDate"));
                

                //Label lblHCNumber = ((Label)gvViewQuotation.HeaderRow.Cells[3].FindControl("lblHCNumber"));
                //Label lblCNumber = ((Label)e.Row.FindControl("lblCNumber"));
                //Label lblQuotationDate = ((Label)e.Row.FindControl("lblQuotationDate"));
                //Label CNumber = ((Label)e.Row.FindControl("lblCNumber"));
                LinkButton lbtnQuotation = (LinkButton)e.Row.FindControl("lbtnQuotation");
                HiddenField hdf_doc_id = (HiddenField)e.Row.FindControl("hdf_documnent_Id");
                QuotationDOM ob = (QuotationDOM)e.Row.DataItem;
                if (this.PageType == "Contractor")
                {
                    lbtnQuotation.Text = ob.ContractQuotationNumber;
                    lblHCName.Text = "Contractor Name";
                    CName.Text = ob.ContractorName;
                    lblWorkOrderDate.Text = "Purchase order Date";
                    //lblQuotationDate.Text = ob.QuotationDate.ToString("dd/MMM/yyyy");
                }
                else
                {
                    lbtnQuotation.Text = ob.SupplierQuotationNumber;
                    lblHCName.Text = "Supplier Name";
                    lblWorkOrderDate.Text = "Purchase order Date";
                    CName.Text = ob.SupplierName;
                    lbtnQuotation.CommandArgument = ob.SupplierQuotationId.ToString();
                    //lblQuotationDate.Text = ob.OrderDate.ToString("dd/MMM/yyyy");
                    lbtnEdit.CommandArgument = ob.SupplierQuotationId.ToString();
                    lbtnDelet.CommandArgument = ob.SupplierQuotationId.ToString();
                    lbtnGenerate.CommandArgument = ob.SupplierQuotationId.ToString();
                    lbtnClose.CommandArgument = ob.SupplierQuotationId.ToString();
                    lbtnPrint.CommandArgument = ob.SupplierQuotationId.ToString() + "," + ob.SupplierQuotationNumber.ToString();

                }
                hdf_doc_id.Value = ob.UploadDocumentId.ToString();
            }
        }

        protected void gvViewQuotation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvViewQuotation.PageIndex = e.NewPageIndex;
            if (this.ContractorId == 0 && this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue)
            {
                BindGrid(this.ToDate, this.FromDate);
            }
            else
            {
                lstQuotation = quotationBL.ReadContractorQuotation(this.ContractorId, this.ToDate, this.FromDate, this.ContractNo, this.QuotationNo);
                if (lstQuotation.Count > 0)
                {
                    gvViewQuotation.DataSource = lstQuotation;
                    gvViewQuotation.DataBind();
                }
            }
        }

        //Close The Quotations
        //protected void btn_Approve_Reject_Click(object sender, CommandEventArgs e)
        //{
        //    if (!GetSelectedData(e.CommandName))
        //    {
        //        if (e.CommandName == btn_Approve.CommandName)
        //            base_page.Alert("Kindly Select Quotation Number", btn_Approve);
        //        else
        //            base_page.Alert("Kindly Select Quotation Number", btn_Reject);
        //    }
        //    else
        //    {
        //        if (e.CommandName == btn_Approve.CommandName)
        //            base_page.Alert("Successfully Approved", btn_Approve);
        //        else
        //            base_page.Alert("Successfully Rejected", btn_Reject);
        //    }


        //}

        protected void on_check_uncheck_all(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvViewQuotation.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbx_Quotation");
                hdf = (HiddenField)row.FindControl("hdf_quotation_id");
                if (chbx != null && hdf != null)
                {
                    chbx.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void on_check_uncheck_quotation(object sender, EventArgs e)
        {
            track = false;
            CheckBox chb = (CheckBox)gvViewQuotation.HeaderRow.FindControl("chbx_select_all");
            foreach (GridViewRow row in gvViewQuotation.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbx_Quotation");
                if (!chbx.Checked)
                    track = true;
            }
            if (track == true)
            {
                chb.Checked = false;
            }
            else
            {
                chb.Checked = true;
            }
        }

        protected void btn_Close_Command(object sender, CommandEventArgs e)
        {

        }



        #endregion

        #region Private Methods





        public void BindGrid(DateTime fromDate, DateTime toDate)
        {


            lstQuotation = new List<QuotationDOM>();


            // Label lblWorkOrderDate = (Label)gvViewQuotation.HeaderRow.FindControl("OrderDate");
            //Label lbl = (Label)gvViewQuotation.HeaderRow.FindControl("lblHQuotation");

            if (this.PageType == "Contractor")
            {

                lstQuotation = quotationBL.ReadContractorQuotation(fromDate, toDate);


                gvViewQuotation.Columns[5].Visible = true;
                gvViewQuotation.Columns[6].Visible = false;
                gvViewQuotation.Columns[7].Visible = false;
                gvViewQuotation.Columns[8].Visible = true;
            }
            else
                lstQuotation = quotationBL.ReadSupplierQuotation(fromDate, toDate);
            if (lstQuotation.Count > 0)
            {
                gvViewQuotation.DataSource = lstQuotation;
                gvViewQuotation.DataBind();
                if (this.PageType == "Supplier")
                {



                    gvViewQuotation.Columns[4].Visible = false;
                    gvViewQuotation.Columns[5].Visible = false;
                }
            }

            else
            {
                BindEmptyGrid(gvViewQuotation);
                //basePage.GridViewEmptyText(gvViewQuotation);
            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void ResetViewState()
        {
            //ViewState["ContractorId"] = null;
            //ViewState["SupplierId"] = null;
            //ViewState["FromDate"] = null;
            //ViewState["ToDate"] = null;
            //ViewState["ContractNo"] = null;
            //ViewState["QuotationNo"] = null; 
            this.ContractorId = 0;
            this.SupplierId = 0;
            this.FromDate = DateTime.MinValue;
            this.ToDate = DateTime.MinValue;
            this.ContractNo = string.Empty;
            this.QuotationNo = string.Empty;
        }

        private void CalculateAllToalValue()
        {
            Decimal total = 0, totalDiscount = 0, totalExciseDuty = 0, totalTax = 0;

            if (lstItemTransaction != null && lstItemTransaction.Count > 0)
            {
                foreach (ItemTransaction item in lstItemTransaction)
                {
                    Decimal tempDiscount = 0, tempExciseDuty = 0;
                    total += item.NumberOfUnit * item.PerUnitCost;

                    if (item.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Value))
                    {
                        totalDiscount += item.NumberOfUnit * item.PerUnitDiscount;
                        tempDiscount = item.NumberOfUnit * item.PerUnitDiscount;
                    }
                    else
                    {
                        totalDiscount += (item.NumberOfUnit * item.PerUnitCost) * (item.PerUnitDiscount / 100);
                        tempDiscount = (item.NumberOfUnit * item.PerUnitCost) * (item.PerUnitDiscount / 100);
                    }
                    totalExciseDuty += ((item.NumberOfUnit * item.PerUnitCost) - tempDiscount) * (item.TaxInformation.ExciseDuty / 100);
                    tempExciseDuty = ((item.NumberOfUnit * item.PerUnitCost) - tempDiscount) * (item.TaxInformation.ExciseDuty / 100);
                    totalTax += ((item.NumberOfUnit * item.PerUnitCost) - tempDiscount + tempExciseDuty) * ((item.TaxInformation.ServiceTax + item.TaxInformation.VAT + item.TaxInformation.CSTWithCForm + item.TaxInformation.CSTWithoutCForm) / 100);

                }
            }
            lblTotal.Text = String.Concat("Total : ", total.ToString("F2"));
            lblTotalDiscount.Text = String.Concat("Total Discount : ", totalDiscount.ToString("F2"));
            lblTotalExciseDuty.Text = String.Concat("Total Excise Duty : ", totalExciseDuty.ToString("F2"));
            lblTotalTax.Text = String.Concat("Total Tax : ", totalTax.ToString("F2"));
            if (lstQuotation != null && lstQuotation.Count > 0)
            {
                lblPackaging.Text = String.Concat("Packaging : ", lstQuotation[0].Packaging.ToString("F2"));
                lblfreight.Text = String.Concat("Freight : ", lstQuotation[0].Freight.ToString("F2"));
                lblGrandTotal.Text = String.Concat("Total Amount : ", lstQuotation[0].TotalNetValue.ToString("F2"));
            }
            if (this.PageType == "Contractor")
            {
                lblPackaging.Visible = false;
                lblfreight.Visible = false;
            }
        }

        #endregion

        #region Public Properties

        public Int32 ContractorId
        {
            get
            {
                if (ViewState["ContractorId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["ContractorId"]);
            }
            set { ViewState["ContractorId"] = value; }
        }

        public Int32 SupplierId
        {
            get { return Convert.ToInt32(ViewState["SupplierId"]); }

            set { ViewState["SupplierId"] = value; }
        }

        public DateTime FromDate
        {
            get { return Convert.ToDateTime(ViewState["FromDate"]); }

            set { ViewState["FromDate"] = value; }
        }

        public DateTime ToDate
        {
            get { return Convert.ToDateTime(ViewState["ToDate"]); }

            set { ViewState["ToDate"] = value; }
        }

        public String ContractNo
        {
            get { return Convert.ToString(ViewState["ContractNo"]); }

            set { ViewState["ContractNo"] = value; }
        }

        public String QuotationNo
        {
            get { return Convert.ToString(ViewState["QuotationNo"]); }

            set { ViewState["QuotationNo"] = value; }
        }

        public String PageType
        {
            get { return Convert.ToString(ViewState["PageType"]); }

            set { ViewState["PageType"] = value; }
        }

        #endregion




    }
}