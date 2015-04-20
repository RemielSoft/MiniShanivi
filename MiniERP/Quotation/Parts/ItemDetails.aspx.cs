using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.Configuration;
using MiniERP.Shared;
using System.IO;
using System.Net;

namespace MiniERP.Quotation.Parts
{
    public partial class ItemDetails : BasePage
    {
        #region Private Global Variables

        String number = String.Empty;
        int id = 0;

        bool track = false;

        Decimal total;
        Decimal quantity = 0;
        Decimal rate = 0;
        Decimal discount = 0;
        Decimal excise_duty = 0;
        Decimal service_tax = 0;
        Decimal vat = 0;
        Decimal cst_c = 0;
        Decimal cst_w_c = 0;
        Decimal packaging = 0, freight = 0;

        CheckBox chbx = null;
        DropDownList ddl_discount = null;
        HiddenField hdf = null;

        TextBox txt = null;
        TextBox txtIssueQuantity = null;
        TextBox txtApplicableRate = null;
        TextBox txtDiscount = null;
        TextBox txtExciseDuty = null;
        TextBox txtServiceTax = null;
        TextBox txtVAT = null;
        TextBox txtCSTwithC = null;
        TextBox txtCSTwithoutC = null;

        Label lbl = null;

        ServiceDetailDOM serviceDetail = null;
        ItemTransaction itemTransaction = null;

        CompanyWorkOrderBL companyWorkOrderBL = null;
        TaxBL taxBL = null;

        List<ServiceDetailDOM> lstServiceDetail = null;

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["id"] != null)
            {
                //FileDownLoad();
                SetDefault();
            }
        }

        //private void FileDownLoad()
        //{
        //    string strURL = (String)Session["FilePath"];
        //    WebClient req = new WebClient();
        //    HttpResponse response = HttpContext.Current.Response;
        //    response.Clear();
        //    response.ClearContent();
        //    response.ClearHeaders();
        //    response.Buffer = true;
        //    response.AddHeader("Content-Disposition", "attachment;filename=" + Session["OriginalFileName"]);
        //    byte[] data = req.DownloadData(strURL);
        //    response.BinaryWrite(data);
        //    response.End();
        //}

        protected void on_check_uncheck_all(object sender, EventArgs e)
        {
            if (ItemTransactionList == null)
                ItemTransactionList = new List<ItemTransaction>();

            foreach (GridViewRow row in gv_Item_Data.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbx_item");
                hdf = (HiddenField)row.FindControl("hdf_item_id");
                if (chbx != null)
                {
                    if (ItemTransactionList.Count != 0)
                    {
                        foreach (ItemTransaction item in ItemTransactionList)
                        {
                            if (item.Service_Detail.ItemNumber == hdf.Value)
                            {
                                chbx.Checked = false;
                                chbx.Enabled = false;
                                break;
                            }
                            else
                            {
                                chbx.Checked = ((CheckBox)sender).Checked;
                            }
                        }
                    }
                    else
                    {
                        chbx.Checked = ((CheckBox)sender).Checked;
                    }
                }
            }
        }

        protected void on_chbx_item(object sender, EventArgs e)
        {
            if (ItemTransactionList == null)
                ItemTransactionList = new List<ItemTransaction>();

            track = false;
            CheckBox chb = (CheckBox)gv_Item_Data.HeaderRow.FindControl("chbx_select_all");
            foreach (GridViewRow row in gv_Item_Data.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbx_item");
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

        protected void gv_Item_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                chbx = (CheckBox)e.Row.FindControl("chbx_item");
                hdf = (HiddenField)e.Row.FindControl("hdf_item_id");
                if (chbx != null)
                {
                    if (ItemTransactionList.Count != 0 || ItemTransactionList != null)
                    {
                        foreach (ItemTransaction item in ItemTransactionList)
                        {
                            if (item.Service_Detail.ItemNumber == hdf.Value)
                            {
                                chbx.Checked = false;
                                chbx.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            lstServiceDetail = new List<ServiceDetailDOM>();
            foreach (GridViewRow row in gv_Item_Data.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbx_item");
                hdf = (HiddenField)row.FindControl("hdf_item_id");
                if (chbx != null && hdf != null)
                {
                    if (chbx.Checked.Equals(true))
                    {
                        serviceDetail = new ServiceDetailDOM();
                        serviceDetail.ItemNumber = hdf.Value;
                        lstServiceDetail.Add(serviceDetail);
                    }
                }
            }
            if (lstServiceDetail.Count != 0)
            {
                SetItemData(lstServiceDetail);
                BindItemData();
                pnl_Total.Visible = true;
            }
            else
            {
                Alert("Please Select the Item.", btn_add);
            }
        }

        //*********************************For Final Grid*************START***********************

        protected void txt_Issue_Quantity_changed(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            txtIssueQuantity = (TextBox)row.FindControl("txt_Issue_Quantity");
            track = Decimal.TryParse(txtIssueQuantity.Text.Trim(), out quantity);
            if (track)
            {
                if (quantity == 0)
                {
                    txtIssueQuantity.Focus();
                    Alert("Issue Quantity can not be Zero(0).", txtIssueQuantity);
                }
                else
                {
                    CalculateOnTextChanged(row);
                    txtApplicableRate.Focus();
                }
            }
            else
            {
                txtIssueQuantity.Focus();
                Alert("In Issue Quantity Only Valid Numeric values are allowed.", txtIssueQuantity);
            }
        }

        protected void txt_Applicable_Rate_changed(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            txtApplicableRate = (TextBox)row.FindControl("txt_Applicable_Rate");
            track = Decimal.TryParse(txtApplicableRate.Text.Trim(), out rate);
            if (track)
            {
                if (rate == 0)
                {
                    txtApplicableRate.Focus();
                    Alert("Applicable Rate can not be Zero(0).", txtApplicableRate);
                }
                else
                {
                    CalculateOnTextChanged(row);
                    ddl_discount.Focus();
                }

            }
            else
            {
                txtApplicableRate.Focus();
                Alert("In Applicable Rate Only Valid Numeric values are allowed.", txtApplicableRate);
            }
        }

        protected void ddl_discount_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            ddl_discount = (DropDownList)row.FindControl("ddl_discount_type");
            txtDiscount = (TextBox)row.FindControl("txt_discount");
            if (ddl_discount != null && txtDiscount != null)
            {
                if (ddl_discount.SelectedValue == Convert.ToInt16(DiscountType.Value).ToString()
                    || ddl_discount.SelectedValue == Convert.ToInt16(DiscountType.Percentage).ToString())
                {
                    txtDiscount.Enabled = true;
                    txtDiscount.Focus();
                    CalculateOnTextChanged(row);
                }
                else
                {
                    txtDiscount.Text = String.Empty;
                    txtDiscount.Enabled = false;
                    CalculateOnTextChanged(row);
                }
            }
        }

        protected void txt_Discount_changed(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            txtDiscount = (TextBox)row.FindControl("txt_Discount");
            track = Decimal.TryParse(txtDiscount.Text.Trim(), out discount);
            if (track)
            {
                CalculateOnTextChanged(row);
                txtExciseDuty.Focus();
            }
            else
            {
                txtDiscount.Focus();
                Alert("In Discount Only Valid Numeric values are allowed.", txtDiscount);
            }

        }

        protected void txt_Excise_Duty_changed(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            txtExciseDuty = (TextBox)row.FindControl("txt_Excise_Duty");
            track = Decimal.TryParse(txtExciseDuty.Text.Trim(), out excise_duty);
            if (track)
            {
                CalculateOnTextChanged(row);
                txtServiceTax.Focus();
            }
            else
            {
                txtExciseDuty.Focus();
                Alert("In Excise Duty Only Valid Numeric values are allowed.", txtExciseDuty);
            }
        }

        protected void txt_Service_Tax_changed(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            txtServiceTax = (TextBox)row.FindControl("txt_Service_Tax");
            track = Decimal.TryParse(txtServiceTax.Text.Trim(), out service_tax);
            if (track)
            {
                CalculateOnTextChanged(row);
                txtVAT.Focus();
            }
            else
            {
                txtServiceTax.Focus();
                Alert("In Service Tax Only Valid Numeric values are allowed.", txtServiceTax);
            }
        }

        protected void txt_VAT_changed(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            txtVAT = (TextBox)row.FindControl("txt_VAT");
            track = Decimal.TryParse(txtVAT.Text.Trim(), out vat);
            if (track)
            {
                CalculateOnTextChanged(row);
                txtCSTwithC.Focus();
            }
            else
            {
                txtVAT.Focus();
                Alert("In VAT Only Valid Numeric values are allowed.", txtVAT);
            }
        }

        protected void txt_CST_with_C_Form_changed(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            txtCSTwithC = (TextBox)row.FindControl("txt_CST_with_C_Form");
            track = Decimal.TryParse(txtCSTwithC.Text.Trim(), out cst_c);
            if (track)
            {
                CalculateOnTextChanged(row);
                txtCSTwithoutC.Focus();
            }
            else
            {
                txtCSTwithC.Focus();
                Alert("In CST with C Form Only Valid Numeric values are allowed.", txtCSTwithC);
            }
        }

        protected void txt_CST_without_C_Form_changed(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
            txtCSTwithoutC = (TextBox)row.FindControl("txt_CST_without_C_Form");
            track = Decimal.TryParse(txtCSTwithoutC.Text.Trim(), out cst_w_c);
            if (track)
            {
                CalculateOnTextChanged(row);
            }
            else
            {
                txtCSTwithoutC.Focus();
                Alert("In CST without C Form Only Valid Numeric values are allowed.", txtCSTwithoutC);
            }
        }

        protected void gv_final_item_data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddl_discount = (DropDownList)e.Row.FindControl("ddl_discount_type");
                txtDiscount = (TextBox)e.Row.FindControl("txt_discount");

                txtIssueQuantity = (TextBox)e.Row.FindControl("txt_Issue_Quantity");

                if (txtIssueQuantity.Text != "0")
                {
                    total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
                }

                if (ddl_discount != null)
                {
                    taxBL = new TaxBL();
                    BindDropDown(ddl_discount, "Name", "Id", taxBL.ReadDiscountMode(null));

                    if (ddl_discount.SelectedValue == Convert.ToInt16(DiscountType.Value).ToString()
                    || ddl_discount.SelectedValue == Convert.ToInt16(DiscountType.Percentage).ToString())
                    {
                        txtDiscount.Enabled = true;

                    }
                    else
                    {
                        txtDiscount.Text = String.Empty;
                        txtDiscount.Enabled = false;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                lbl = (Label)e.Row.FindControl("lblTotal");
                lbl.Text = total.ToString();
                TotalAmount = total;
            }
        }

        protected void gv_final_item_data_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                ItemTransactionList.RemoveAt(id);
                if (ItemTransactionList.Count == 0)
                    EmptyTotal();
                BindItemData();
                BindFinalItemData();
            }
        }

        protected void gv_final_item_data_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gv_final_item_data_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnAddTotal_Click(object sender, EventArgs e)
        {
            lblGrandTotal.Text = String.Concat("Grand Total : ", TotalGrandValue().ToString("F2"));
        }

        protected void btn_save_draft_Click(object sender, EventArgs e)
        {
            if (TotalAmount == 0)
            {
                Alert("Please Add the Item", btn_save_draft);
            }
            else
            {
                if (SetItemData())
                {
                    Alert("Issue Quantity Can not be Empty or Zero(0).", btn_save_draft);
                }
                else
                {
                    TotalGrandValue();
                    ItemTransactionList[0].TaxInformation.Freight = freight;
                    ItemTransactionList[0].TaxInformation.Packaging = packaging;
                    ItemTransactionList[0].TotalAmount = TotalGrandValue();

                    Alert("Work Details Saved Successfully", btn_save_draft);
                    CloseWinWithUpdatePanel(btn_save_draft);
                }
            }
        }

        protected void btn_cancel_draft_Click(object sender, EventArgs e)
        {
            SetDefault();
            BindFinalItemData();
        }

        #endregion

        #region Private Methods

        private void SetDefault()
        {
            ItemTransactionList = new List<ItemTransaction>();
            GetItemData();
            SetRegularExpression();
            EmptyTotal();
        }

        private void SetPreviousData()
        {
            track = Int32.TryParse(Convert.ToString(Request.QueryString["id"]), out id);
            lbl_contract_number.Text = Convert.ToString(Request.QueryString["contract_no"]);
            number = Convert.ToString(Request.QueryString["no"]);
            lbl_work_order_number.Text = Convert.ToString(Request.QueryString["work_order_no"]);
            BindItemData();

        }

        private void SetRegularExpression()
        {
            rev_freight.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_freight.ErrorMessage = "In Freight only Numeric values are allowed upto 2 decimal digits";
            rev_packaging.ValidationExpression = ValidationExpression.C_DECIMAL;
            rev_packaging.ErrorMessage = "In Packaging only Numeric values are allowed upto 2 decimal digits";
        }

        private void GetItemData()
        {
            track = Int32.TryParse(Convert.ToString(Request.QueryString["id"]), out id);
            lbl_contract_number.Text = Convert.ToString(Request.QueryString["contract_no"]);
            number = Convert.ToString(Request.QueryString["no"]);
            lbl_work_order_number.Text = Convert.ToString(Request.QueryString["work_order_no"]);

            if (track)
            {
                lstServiceDetail = new List<ServiceDetailDOM>();
                companyWorkOrderBL = new CompanyWorkOrderBL();
                lstServiceDetail = companyWorkOrderBL.ReadCompanyWorkOrderServiceDetail(id);
                if (lstServiceDetail.Count > 0)
                {
                    for (int i = 0; i < lstServiceDetail.Count; i++)
                    {
                        if (lstServiceDetail[i].WorkOrderNumber != lbl_work_order_number.Text)
                        {
                            lstServiceDetail.RemoveAt(i);
                        }
                        //Have to Be Change After Updation in data Base
                        else
                        {
                            lstServiceDetail[i].QuantityLeft = lstServiceDetail[i].Quantity;
                        }
                        //
                    }
                    this.ServiceDetail = lstServiceDetail;
                }
                BindItemData();
            }
        }

        private void SetItemData(List<ServiceDetailDOM> lstServiceDetail)
        {
            if (ItemTransactionList == null)
            {
                ItemTransactionList = new List<ItemTransaction>();
            }

            id = 0;
            foreach (ServiceDetailDOM item_main in lstServiceDetail)
            {
                foreach (ServiceDetailDOM item in this.ServiceDetail)
                {
                    if (item_main.ItemNumber == item.ItemNumber)
                    {
                        itemTransaction = new ItemTransaction();
                        itemTransaction.TaxInformation = new Tax();
                        itemTransaction.TaxInformation.DiscountMode = new MetaData();
                        itemTransaction.Service_Detail = new ServiceDetailDOM();
                        itemTransaction.Service_Detail.Unit = new MetaData();

                        itemTransaction.Service_Detail.ItemNumber = item.ItemNumber;
                        itemTransaction.Service_Detail.ServiceNumber = item.ServiceNumber;
                        itemTransaction.Service_Detail.ServiceDescription = item.ServiceDescription;
                        itemTransaction.Service_Detail.Quantity = item.Quantity;
                        itemTransaction.Service_Detail.QuantityIssued = item.QuantityIssued;
                        //itemTransaction.Service_Detail.QuantityLeft = item.QuantityLeft;
                        itemTransaction.Service_Detail.QuantityLeft = item.Quantity - item.QuantityIssued;
                        itemTransaction.Service_Detail.Unit.Id = item.Unit.Id;
                        itemTransaction.Service_Detail.Unit.Name = item.Unit.Name;
                        itemTransaction.Service_Detail.UnitRate = item.UnitRate;
                        itemTransaction.Service_Detail.ApplicableRate = item.ApplicableRate;
                        //itemTransaction.TotalAmount = item.Total;
                        ItemTransactionList.Add(itemTransaction);
                    }
                }
                id++;
            }
            BindFinalItemData();
        }

        private void CalculateOnTextChanged(GridViewRow row)
        {
            hdf = (HiddenField)row.FindControl("hdfitemid");

            txtApplicableRate = (TextBox)row.FindControl("txt_Applicable_Rate");
            track = Decimal.TryParse(txtApplicableRate.Text.Trim(), out rate);

            txtIssueQuantity = (TextBox)row.FindControl("txt_Issue_Quantity");
            track = Decimal.TryParse(txtIssueQuantity.Text.Trim(), out quantity);

            txtDiscount = (TextBox)row.FindControl("txt_Discount");
            track = Decimal.TryParse(txtDiscount.Text.Trim(), out discount);

            txtExciseDuty = (TextBox)row.FindControl("txt_Excise_Duty");
            track = Decimal.TryParse(txtExciseDuty.Text.Trim(), out excise_duty);

            txtServiceTax = (TextBox)row.FindControl("txt_Service_Tax");
            track = Decimal.TryParse(txtServiceTax.Text.Trim(), out service_tax);

            txtVAT = (TextBox)row.FindControl("txt_VAT");
            track = Decimal.TryParse(txtVAT.Text.Trim(), out vat);

            txtCSTwithC = (TextBox)row.FindControl("txt_CST_with_C_Form");
            track = Decimal.TryParse(txtCSTwithC.Text.Trim(), out cst_c);

            txtCSTwithoutC = (TextBox)row.FindControl("txt_CST_without_C_Form");
            track = Decimal.TryParse(txtCSTwithoutC.Text.Trim(), out cst_w_c);


            total = rate * quantity;

            ddl_discount = (DropDownList)row.FindControl("ddl_discount_type");
            if (ddl_discount.SelectedIndex != 0)
            {
                if (ddl_discount.SelectedValue == Convert.ToInt16(DiscountType.Value).ToString())
                {
                    total = total - (quantity * discount);
                }
                else
                {
                    total = total - (total * discount) / 100;
                }
            }

            total = total + (total * excise_duty / 100);
            total = total + (total * (service_tax + vat + cst_c + cst_w_c)) / 100;

            lbl = (Label)row.FindControl("lblTotalAmount");
            lbl.Text = Math.Round(total, 2).ToString();

            TotalAmount = Math.Round((TotalAmount + total), 2);

            for (int i = 0; i < ItemTransactionList.Count; i++)
            {
                if (ItemTransactionList[i].Service_Detail.ItemNumber == hdf.Value)
                {
                    ItemTransactionList[i].Service_Detail.ApplicableRate = rate;
                    ItemTransactionList[i].Service_Detail.QuantityIssued = quantity;
                    //Will be Changed After When be Using from data base for Left
                    //ItemTransactionList[i].Service_Detail.QuantityLeft = quantity;
                    //.................HAVE to be CHANGE.........................
                    ItemTransactionList[i].TaxInformation.DiscountMode.Id = Convert.ToInt32(ddl_discount.SelectedValue);
                    ItemTransactionList[i].TaxInformation.TotalDiscount = discount;
                    ItemTransactionList[i].TaxInformation.ExciseDuty = excise_duty;
                    ItemTransactionList[i].TaxInformation.ServiceTax = service_tax;
                    ItemTransactionList[i].TaxInformation.VAT = vat;
                    ItemTransactionList[i].TaxInformation.CSTWithCForm = cst_c;
                    ItemTransactionList[i].TaxInformation.CSTWithoutCForm = cst_w_c;
                    ItemTransactionList[i].TotalAmount = total;
                }

            }

            TotalAmount = 0;
            foreach (ItemTransaction item in ItemTransactionList)
            {
                if (item.Service_Detail.QuantityIssued != 0)
                    TotalAmount += item.TotalAmount;
            }

            //For Show to the Total in Footer..........................................................
            ((Label)gv_final_item_data.FooterRow.FindControl("lblTotal")).Text = Math.Round(TotalAmount, 2).ToString();
            btnAddTotal_Click(null, null);
        }

        private void BindItemData()
        {
            gv_Item_Data.DataSource = this.ServiceDetail;
            gv_Item_Data.DataBind();
        }

        private void BindFinalItemData()
        {
            if (ItemTransactionList != null)
            {
                gv_final_item_data.DataSource = ItemTransactionList;
            }
            else
            {
                gv_final_item_data.DataSource = null;
            }
            gv_final_item_data.DataBind();
        }

        private void EmptyTotal()
        {
            pnl_Total.Visible = false;
            txt_freight.Text = String.Empty;
            txt_packaging.Text = String.Empty;
            TotalAmount = 0;
            lblGrandTotal.Text = String.Empty;
        }

        private bool SetItemData()
        {
            track = false;
            for (int i = 0; i < ItemTransactionList.Count; i++)
            {
                if (ItemTransactionList[i].Service_Detail.QuantityIssued == 0)
                {
                    track = true;
                }
            }
            return track;
        }

        #endregion

        #region Private Property

        public List<ServiceDetailDOM> ServiceDetail
        {
            get
            {
                return (List<ServiceDetailDOM>)ViewState["ServiceDetail"];
            }
            set
            {
                ViewState["ServiceDetail"] = value;
            }
        }

        private Decimal TotalAmount
        {
            get
            {
                try
                {
                    return (Decimal)ViewState["TotalAmount"];
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                ViewState["TotalAmount"] = value;
            }
        }

        private Decimal TotalGrandValue()
        {
            if (!String.IsNullOrEmpty(txt_packaging.Text.Trim()))
            {
                packaging = Convert.ToDecimal(txt_packaging.Text.Trim());
            }
            if (!String.IsNullOrEmpty(txt_freight.Text.Trim()))
            {
                freight = Convert.ToDecimal(txt_freight.Text.Trim());
            }

            return TotalAmount + packaging + freight;
        }

        #endregion
    }
}