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
    public partial class PaymentTerms : System.Web.UI.UserControl
    {
        #region Private Global Variables

        bool track = false;
        int id = 0;

        BasePage base_Page = new BasePage();

        PaymentTerm payment_Term = null;

        PaymentTermBL payment_Term_BL = null;

        List<PaymentTerm> lst_payment_term = null;

        #endregion

        #region Main Page Method

        public void LoadControl()
        {
            if (base_Page.ActiveTab == 2)
            {
                DefaultLoad();
            }
        }

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base_Page.ActiveTab == 0)
            {
                DefaultLoad();
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            lbl_duplicate_payment_term.Text = String.Empty;

            if (Convert.ToDecimal(txtPercentageValue.Text.Trim()) <= 0)
            {
                lbl_duplicate_payment_term.Text = ("Percentage value will be greater than 0");
                return;
            }
            else if (Convert.ToDecimal(txtPercentageValue.Text.Trim()) > 100)
            {
                lbl_duplicate_payment_term.Text = ("Percentage value will be not greater than 100");
                return;
            }

            if (ddl_payment_type.SelectedValue == Convert.ToInt16(PaymentType.AfterDays).ToString() && (txt_number_of_days.Text.Trim() == String.Empty))
            {
                if (ddl_payment_type.SelectedValue == Convert.ToInt16(PaymentType.AfterDays).ToString())
                {
                    lbl_duplicate_payment_term.Text = GlobalConstants.M_Days_Payment;
                }
            }
            else if (txtPercentageValue.Text.Trim() == String.Empty)
            {
                if (ddl_payment_type.SelectedValue == Convert.ToInt16(PaymentType.AfterDays).ToString())
                {
                    lbl_duplicate_payment_term.Text = GlobalConstants.M_Days_Payment;
                }
                else
                {
                    lbl_duplicate_payment_term.Text = GlobalConstants.M_Advance_Payment;
                }
            }


            else if (btn_add.Text == "Add")
            {
                payment_Term = GetControlsData();


                if (base_Page.PaymentTermsList == null)
                {
                    base_Page.PaymentTermsList = new List<PaymentTerm>();
                }

                if (CheckPayentTerm(payment_Term))
                {
                    lbl_duplicate_payment_term.Text = ("Percentage value in payment terms not greater than 100%");
                    return;
                }
                else
                {
                    base_Page.PaymentTermsList.Add(payment_Term);
                    ClearAll();
                }
            }
            else if (btn_add.Text == "Update")
            {
                if (this.LastIndex >= 0)
                {
                    payment_Term = GetControlsData();
                    if (payment_Term.PercentageValue > 100)
                    {
                        lbl_duplicate_payment_term.Text = ("Warning ! : Percentage value in payment terms not greater than 100%");
                        return;
                    }
                    else if (CheckPayentTerm(payment_Term))
                    {
                        lbl_duplicate_payment_term.Text = ("Warning ! : Percentage value in payment terms not greater than 100%");
                        return;
                    }
                    else
                    {
                        base_Page.PaymentTermsList = UpdateData(payment_Term);
                        btn_add.Text = "Add";
                        LastIndex = -1;
                    }
                }
                //Used for Single Payment Type
                //if (track == false)
                //{
                //    ClearAll();
                //}
                //Ended
            }

            BindGrid();
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected void gv_paymnetTerms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edit")
            {
                ddl_payment_type.SelectedValue = base_Page.PaymentTermsList[index].PaymentType.Id.ToString();
                txt_number_of_days.Text = base_Page.PaymentTermsList[index].NumberOfDays.ToString();
                txtPercentageValue.Text = base_Page.PaymentTermsList[index].PercentageValue.ToString();
                txtPaymentDescription.Text = base_Page.PaymentTermsList[index].PaymentDescription;

                CheckNumberofDayes();

                btn_add.Text = "Update";
                this.LastIndex = index;
            }
            else if (e.CommandName == "Delete")
            {
                base_Page.PaymentTermsList.RemoveAt(index);
                if (base_Page.PaymentTermsList.Count == 0)
                {
                    ClearAll();
                    btn_add.Text = "Add";
                    base_Page.PaymentTermsList = null;
                }
                BindGrid();
                ClearAll();
            }
        }

        protected void gv_paymnetTerms_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gv_paymnetTerms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_paymnetTerms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_paymnetTerms.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddl_payment_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckNumberofDayes();
        }
        #endregion

        #region Private Methods

        private void DefaultLoad()
        {
           
            ControlsView();
            BindPaymentTerm();
            BindGrid();

            revPaymentDescription.ValidationExpression = ValidationExpression.C_DESCRIPTION;
            revPaymentDescription.ErrorMessage = "More than 250 Characters are not Allowed in Payment Description";
        }

        private void BindPaymentTerm()
        {
            if (base_Page.ActiveTab == 0)
            {
                payment_Term_BL = new PaymentTermBL();
                lst_payment_term = new List<PaymentTerm>();
                lst_payment_term = payment_Term_BL.ReadPaymentTermMeta();
                base_Page.BindDropDown(ddl_payment_type, "MyName", "MyId", lst_payment_term);
            }
        }

        private PaymentTerm GetControlsData()
        {
            payment_Term = new PaymentTerm();
            payment_Term.PaymentType = new MetaData();
            if (ddl_payment_type.SelectedIndex != 0)
            {
                payment_Term.PaymentType.Id = Convert.ToInt32(ddl_payment_type.SelectedValue);
                payment_Term.PaymentType.Name = ddl_payment_type.SelectedItem.ToString();
                if (txt_number_of_days.Text.Trim() != String.Empty)
                {
                    payment_Term.NumberOfDays = Convert.ToInt32(txt_number_of_days.Text.Trim());
                }
                if (!String.IsNullOrEmpty(txtPercentageValue.Text.Trim()))
                {
                    payment_Term.PercentageValue = Convert.ToDecimal(txtPercentageValue.Text.Trim());
                }
                payment_Term.PaymentDescription = txtPaymentDescription.Text.Trim();
            }
            return payment_Term;
        }

        private void BindGrid()
        {
            if (base_Page.PaymentTermsList != null)
                gv_paymnetTerms.DataSource = base_Page.PaymentTermsList;
            else
                gv_paymnetTerms.DataSource = null;
            gv_paymnetTerms.DataBind();
        }

        private List<PaymentTerm> UpdateData(PaymentTerm payment_Term)
        {
            base_Page.PaymentTermsList[this.LastIndex].PaymentType.Id = payment_Term.PaymentType.Id;
            base_Page.PaymentTermsList[this.LastIndex].PaymentType.Name = payment_Term.PaymentType.Name;
            base_Page.PaymentTermsList[this.LastIndex].NumberOfDays = payment_Term.NumberOfDays;
            base_Page.PaymentTermsList[this.LastIndex].PercentageValue = payment_Term.PercentageValue;
            base_Page.PaymentTermsList[this.LastIndex].PaymentDescription = payment_Term.PaymentDescription;

            return base_Page.PaymentTermsList;
        }

        private Boolean CheckDuplicatePaymentTerm(PaymentTerm payment_Term)
        {
            id = 0;
            foreach (PaymentTerm item in base_Page.PaymentTermsList)
            {
                id = id + 1;
                if (payment_Term.PaymentType.Id == item.PaymentType.Id)
                {
                    if (LastIndex != -1 && id == LastIndex + 1)
                    {
                        track = false;
                        break;
                    }
                    else
                    {
                        track = true;
                        break;
                    }
                }
                else
                {
                    track = false;
                }
            }
            return track;
        }

        private void ClearAll()
        {
            btn_add.Text = "Add";
            lbl_duplicate_payment_term.Text = String.Empty;
            ddl_payment_type.SelectedIndex = 0;
            txt_number_of_days.Text = String.Empty;
            txt_number_of_days.Enabled = true;
            txtPercentageValue.Text = String.Empty;
            txtPaymentDescription.Text = String.Empty;
        }

        private void ControlsView()
        {
            if ((base_Page.QuotationStatusID == Convert.ToInt32(StatusType.Pending)) || (base_Page.QuotationStatusID == Convert.ToInt32(StatusType.InComplete)))
            {
                EnableDisableControls(true);
            }
            else
            {
                EnableDisableControls(false);
            }
        }

        private void EnableDisableControls(bool condition)
        {
            tbl_payment.Visible = condition;
            dv_button.Visible = condition;

            gv_paymnetTerms.Columns[4].Visible = condition;
        }

        private void CheckNumberofDayes()
        {
            if (ddl_payment_type.SelectedValue == Convert.ToInt16(PaymentType.Advance).ToString() || ddl_payment_type.SelectedValue == Convert.ToInt16(PaymentType.Other).ToString())
            {
                txt_number_of_days.Enabled = false;
                txt_number_of_days.Text = String.Empty;
            }
            else
            {
                txt_number_of_days.Enabled = true;
            }
        }


        private Boolean CheckPayentTerm(PaymentTerm payment_Term)
        {
            Decimal tot = 0;
            if (!string.IsNullOrEmpty(txtPercentageValue.Text.Trim()))
            {
                tot = Convert.ToDecimal(txtPercentageValue.Text.Trim());
            }


            foreach (PaymentTerm item in base_Page.PaymentTermsList)
            {
                tot += item.PercentageValue;
            }

            //tot += payment_Term.PercentageValue;

            if (tot > 100)
            {
                return true;
            }

            return false;
        }

        #endregion


        #region Private Property
        private int LastIndex
        {
            get
            {
                try
                {
                    return (int)ViewState["Index"];
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                ViewState["Index"] = value;
            }
        }
        #endregion


    }
}