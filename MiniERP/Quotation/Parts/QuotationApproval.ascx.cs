using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Text.RegularExpressions;
using System.Drawing;

namespace MiniERP.Parts
{
    public partial class QuotationApproval : System.Web.UI.UserControl
    {
        #region Private Global Variables

        bool track;
        Int32 id = 0;

        CheckBox chbx = null;
        HiddenField hdf = null;
        HiddenField hdf_id = null;
        HiddenField hdf_doc_id = null;
        LinkButton lbtn = null;
        TextBox txtRemarkReject = null;

        BasePage base_page = new BasePage();
        QuotationDOM quotation = null;

        QuotationBL quotationBL = null;
        DeliveryScheduleBL deliveryScheduleBL = null;
        PaymentTermBL paymentTermBL = null;
        TermAndConditionBL termAndConditionBL = null;

        List<QuotationDOM> lstQuotation = null;
        List<DeliveryScheduleDOM> lstDeliverySchedule = null;
        List<PaymentTerm> lstPaymentTerm = null;
        List<TermAndCondition> lstTermAndCondition = null;
        List<ItemTransaction> lstItemTransaction = null;

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                BindStatus();
                LabelAssociation();
                BindQuotation(Convert.ToInt32(StatusType.Pending));
                ModalPopupExtender2.Hide();
                //Panel2.Visible = false;
            }
        }

        private void PageDefaults()
        {
            base_page.Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void on_check_uncheck_all(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvQuotation.Rows)
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
            CheckBox chb = (CheckBox)gvQuotation.HeaderRow.FindControl("chbx_select_all");
            foreach (GridViewRow row in gvQuotation.Rows)
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

        protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = Convert.ToInt32(ddl_status.SelectedValue);
            BindQuotation(id);
            ButtonVisibility(id);
        }

        protected void gvQuotation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridRowDataBind(e);
                GridColumnVisibility(e);
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridColumnVisibility(e);
            }
        }

        protected void gvQuotation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkQuotation")
            {
                id = Convert.ToInt32(e.CommandArgument);
                lstItemTransaction = new List<ItemTransaction>();
                lstQuotation = new List<QuotationDOM>();
                quotationBL = new QuotationBL();
                if (IsContractor)
                {
                    lstItemTransaction = quotationBL.ReadContractorQuotationMapping(id);
                    lstQuotation = quotationBL.ReadContractorQuotation(id, null);
                    
                }
                else
                {
                    lstItemTransaction = quotationBL.ReadSupplierQuotationMapping(id);
                    lstQuotation = quotationBL.ReadSupplierQuotation(id, null);
                }

                if (lstItemTransaction.Count > 0)
                {
                    gvSupplierQuotationItems.DataSource = lstItemTransaction;
                    gvSupplierQuotationItems.DataBind();
                    if (!IsContractor)
                    {
                        gvSupplierQuotationItems.Columns[0].Visible = false;
                    }
                    else
                    {
                        gvSupplierQuotationItems.Columns[10].Visible = true;
                        lblTotalExciseDuty.Visible = false;
                    }
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                    if (hdfc != null)
                        updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                    else
                        updcFile.GetDocumentData(Int32.MinValue);
                }
                else
                {
                    gvSupplierQuotationItems.DataSource = null;
                    gvSupplierQuotationItems.DataBind();
                }
                //}


                CalculateAllToalValue();



                lstDeliverySchedule = new List<DeliveryScheduleDOM>();
                deliveryScheduleBL = new DeliveryScheduleBL();
                if (IsContractor)
                    lstDeliverySchedule = deliveryScheduleBL.ReadDeliveryScheduleByQuotationID(id, Convert.ToInt16(QuotationType.Contractor));
                else
                    lstDeliverySchedule = deliveryScheduleBL.ReadDeliveryScheduleByQuotationID(id, Convert.ToInt16(QuotationType.Supplier));
                if (lstDeliverySchedule.Count > 0)
                {
                    gvDeliverySchedule.DataSource = lstDeliverySchedule;
                    gvDeliverySchedule.DataBind();
                    if (IsContractor)
                    {
                        //gvDeliverySchedule.Columns[1].Visible = false;
                        // gvDeliverySchedule.Columns[2].Visible = false;
                    }
                    else
                    {
                        gvDeliverySchedule.Columns[0].Visible = false;
                    }
                }
                else
                {
                    gvDeliverySchedule.DataSource = null;
                    gvDeliverySchedule.DataBind();
                }

                lstPaymentTerm = new List<PaymentTerm>();
                paymentTermBL = new PaymentTermBL();
                if (IsContractor)
                    lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(id, Convert.ToInt16(QuotationType.Contractor));
                else
                    lstPaymentTerm = paymentTermBL.ReadPaymentTermByPurchaseOI(id, Convert.ToInt16(QuotationType.Supplier));

                if (lstPaymentTerm.Count > 0)
                {
                    gvPaymentTerm.DataSource = lstPaymentTerm;
                    gvPaymentTerm.DataBind();
                }
                else
                {
                    gvPaymentTerm.DataSource = null;
                    gvPaymentTerm.DataBind();
                }

                lstTermAndCondition = new List<TermAndCondition>();
                termAndConditionBL = new TermAndConditionBL();
                if (IsContractor)
                    lstTermAndCondition = termAndConditionBL.ReadTermAndConditionByQuotationID(id, Convert.ToInt16(QuotationType.Contractor));
                else
                    lstTermAndCondition = termAndConditionBL.ReadTermAndConditionByQuotationID(id, Convert.ToInt16(QuotationType.Supplier));
                if (lstTermAndCondition.Count > 0)
                {
                    gvTermAndCondition.DataSource = lstTermAndCondition;
                    gvTermAndCondition.DataBind();
                }
                else
                {
                    gvTermAndCondition.DataSource = null;
                    gvTermAndCondition.DataBind();
                }

                Panel2.Visible = true;
                ModalPopupExtender2.Show();
            }
        }

        protected void gvQuotation_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvQuotation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvQuotation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQuotation.PageIndex = e.NewPageIndex;
            BindQuotation(Convert.ToInt32(ddl_status.SelectedValue));
        }

        protected void btn_Approve_Reject_Click(object sender, CommandEventArgs e)
        {
            if (!GetSelectedData(e.CommandName))
            {
                if (e.CommandName == btn_Approve.CommandName)
                    base_page.Alert("Kindly Select Quotation Number", btn_Approve);
                else
                    base_page.Alert("Kindly Select Quotation Number", btn_Reject);
            }
            else
            {
                //if (e.CommandName == btn_Approve.CommandName)
                //    base_page.Alert("Successfully Approved", btn_Approve);
                //else
                //    base_page.Alert("Successfully Rejected", btn_Reject);
            }
            BindQuotation(Convert.ToInt32(ddl_status.SelectedValue));

        }


        protected void btn_ok_cancel_Click(object sender, EventArgs e)
        {
            //mpe_msg.Show();
        }


        #endregion

        #region Private Methods

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void LabelAssociation()
        {
            if (IsContractor)
            {
                lbl_quotaion_approval.Text = " Contractor Work Order Approval";
                lbl_quotation.Text = "Contractor Work Order";
            }
            else
            {
                lbl_quotaion_approval.Text = " Supplier Purchase Order Approval";
                lbl_quotation.Text = "Supplier Purchase Order";
            }
        }

        private void BindStatus()
        {
            base_page = new BasePage();
            quotationBL = new QuotationBL();
            base_page.BindDropDownData(ddl_status, "Name", "Id", quotationBL.ReadQuotationStatusMetaData());
        }

        private void BindQuotation(Int32 status)
        {
            quotationBL = new QuotationBL();
            if (IsContractor)
            {
                lstQuotation = quotationBL.ReadContractorQuotation(status);
                gvQuotation.DataSource = lstQuotation;
            }
            else
            {
                lstQuotation = quotationBL.ReadSupplierQuotation(status);
                gvQuotation.DataSource = lstQuotation;
            }
            gvQuotation.DataBind();
            gvQuotation.EmptyDataText = GlobalConstants.EMPTY_TEXT;
        }

        private void ButtonVisibility(Int32 status)
        {
            //on Empty Grid is to be also not visible
            if (status == Convert.ToInt32(StatusType.Pending))
            {
                btn_Approve.Visible = true;
                btn_Reject.Visible = true;
            }
            else
            {
                btn_Approve.Visible = false;
                btn_Reject.Visible = false;
            }
        }

        private bool GetSelectedData(String action)
        {
            LstQuotaionId = new List<MetaData>();
            lstQuotation = new List<QuotationDOM>();
            quotationBL = new QuotationBL();
            if (Convert.ToInt32(ddl_status.SelectedValue) == (Convert.ToInt32(StatusType.Pending)))
            {
                foreach (GridViewRow row in gvQuotation.Rows)
                {
                    chbx = (CheckBox)row.FindControl("chbx_Quotation");
                    hdf = (HiddenField)row.FindControl("hdf_quotation_id");

                    txtRemarkReject = (TextBox)row.FindControl("txtRemark");
                    if (chbx != null && hdf != null)
                    {
                        if (chbx.Checked.Equals(true))
                        {
                            track = true;
                            quotation = new QuotationDOM();
                            quotation.StatusType = new MetaData();

                            if (IsContractor)
                            {
                                quotation.ContractorQuotationId = Convert.ToInt32(hdf.Value);

                                quotation.RemarkReject = txtRemarkReject.Text.ToString();
                            }
                            else
                                quotation.SupplierQuotationId = Convert.ToInt32(hdf.Value);

                                quotation.RemarkReject = txtRemarkReject.Text.ToString();


                            //Reject Case 


                            if (action == btn_Approve.CommandName)

                                quotation.StatusType.Id = Convert.ToInt32(StatusType.Approved);
                            else if (action == btn_Reject.CommandName)
                                quotation.StatusType.Id = Convert.ToInt32(StatusType.InComplete);
                            lstQuotation.Add(quotation);

                            quotation.ApprovedRejectedBy = base_page.LoggedInUser.UserLoginId;

                            if (IsContractor)
                                quotationBL.UpdateContractorQuotationStatus(quotation);
                            else
                                quotationBL.UpdateSupplierQuotationStatus(quotation);
                        }
                    }
                }
            }
            if (lstQuotation.Count == 0)
            {
                track = false;
                //lbl_error_msg.Text = "Kindly Select ";
            }
            return track;
        }

        private void GridColumnVisibility(GridViewRowEventArgs e)
        {
            hdf = (HiddenField)e.Row.FindControl("hdf_status_id");
            //For Select Column
            if ((hdf != null && Convert.ToInt32(hdf.Value) != Convert.ToInt32(StatusType.Pending))
                || (Convert.ToInt32(ddl_status.SelectedValue) != Convert.ToInt32(StatusType.Pending))
                || base_page.LoggedInUser.Role() != Convert.ToString(AuthorityLevelType.Admin))
            {
                e.Row.Cells[0].Visible = false;
            }

            if (IsContractor)
            {
                //For Supplier Name
                e.Row.Cells[5].Visible = false;
                //For Closing Date
                e.Row.Cells[7].Visible = false;
                //For Delivery Desc.
                e.Row.Cells[8].Visible = false;
                //For Packaging
                e.Row.Cells[9].Visible = true;
                //For Freight
                e.Row.Cells[10].Visible = true;
            }
            else
            {
                //For Contractor Name
                e.Row.Cells[2].Visible = false;
                //For Contract Number
                e.Row.Cells[3].Visible = false;
                //For Work Order Number
                e.Row.Cells[4].Visible = false;

            }
        }

        private void GridRowDataBind(GridViewRowEventArgs e)
        {
            hdf_id = (HiddenField)e.Row.FindControl("hdf_quotation_id");
            lbtn = (LinkButton)e.Row.FindControl("lbtnQuotation");
            hdf_doc_id = (HiddenField)e.Row.FindControl("hdf_documnent_Id");
            quotation = (QuotationDOM)e.Row.DataItem;
            if (hdf_id != null && lbtn != null)
            {
                if (IsContractor)
                {
                    hdf_id.Value = quotation.MyContractorQuotationId.ToString();
                    lbtn.Text = quotation.ContractQuotationNumber.ToString();
                    
                    lbtn.CommandArgument = quotation.MyContractorQuotationId.ToString();
                }
                else
                {
                    hdf_id.Value = quotation.MySupplierQuotationId.ToString();
                    lbtn.Text = quotation.SupplierQuotationNumber.ToString();
                    lbtn.CommandArgument = quotation.MySupplierQuotationId.ToString();
                }
                hdf_doc_id.Value = quotation.UploadDocumentId.ToString();
            }
        }

        private void CalculateAllToalValue()
        {
            Decimal total = 0, totalDiscount = 0, totalExciseDuty = 0, totalTax = 0;

            if (lstItemTransaction != null && lstItemTransaction.Count > 0)
            {
                foreach (ItemTransaction item in lstItemTransaction)
                {
                    Decimal tempDiscount = 0, tempExciseDuty = 0;

                    //if (!IsContractor)
                    //{
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
                    //}
                    //else
                    //{
                    //    total += item.Service_Detail.QuantityIssued * item.Service_Detail.ApplicableRate;
                    //    if (item.TaxInformation.DiscountMode.Id == Convert.ToInt32(DiscountType.Value))
                    //    {
                    //        totalDiscount += item.Service_Detail.QuantityIssued * item.PerUnitDiscount;
                    //        tempDiscount = item.Service_Detail.QuantityIssued * item.PerUnitDiscount;
                    //    }
                    //    else
                    //    {
                    //        totalDiscount += (item.Service_Detail.QuantityIssued * item.Service_Detail.ApplicableRate) * (item.PerUnitDiscount / 100);
                    //        tempDiscount = (item.Service_Detail.QuantityIssued * item.Service_Detail.ApplicableRate) * (item.PerUnitDiscount / 100);
                    //    }
                    //    totalExciseDuty += ((item.Service_Detail.QuantityIssued * item.Service_Detail.ApplicableRate) - tempDiscount) * (item.TaxInformation.ExciseDuty / 100);
                    //    tempExciseDuty = ((item.Service_Detail.QuantityIssued * item.Service_Detail.ApplicableRate) - tempDiscount) * (item.TaxInformation.ExciseDuty / 100);
                    //    totalTax += ((item.Service_Detail.QuantityIssued * item.Service_Detail.ApplicableRate) - tempDiscount + tempExciseDuty) * ((item.TaxInformation.ServiceTax + item.TaxInformation.VAT + item.TaxInformation.CSTWithCForm + item.TaxInformation.CSTWithoutCForm) / 100);
                    //}
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

            if (IsContractor)
            {
                lblPackaging.Visible = false;
                lblfreight.Visible = false;
            }

        }

        #endregion

        #region Private Property

        private List<MetaData> LstQuotaionId
        {
            get
            {

                return (List<MetaData>)ViewState["Index"];
            }
            set
            {
                ViewState["Index"] = value;
            }
        }

        private bool IsContractor
        {
            get
            {
                if (base_page.Page_Name == GlobalConstants.P_Contractor_Quotation_Approval)
                {
                    base_page.Page_Name = GlobalConstants.P_Contractor_Quotation_Approval;
                    Session["IsContractor"] = true;
                    return (bool)Session["IsContractor"];
                }
                else
                {
                    Session["IsContractor"] = false;
                    return (bool)Session["IsContractor"];
                }
            }
            set
            {
                Session["IsContractor"] = value;
            }
        }

        #endregion

    }
}