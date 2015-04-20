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
    public partial class TermConditions : System.Web.UI.UserControl
    {
        #region Private Global Variables

        int id = 0;
        bool flag;
        bool track = false;

        BasePage base_Page = new BasePage();

        TermAndCondition termCondition = null;

        TermAndConditionBL termAndConditionBL = null;

        CheckBox chbx = null;
        Label lbl = null;
        HiddenField hdf = null;
        LinkButton lbtn = null;

        #endregion

        #region Main Page Method

        public void LoadControl()
        {
            if (base_Page.ActiveTab == 3)
            {
                DefaultLoad();
            }
        }

        #endregion

        #region protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base_Page.ActiveTab == 0)
            {
                DefaultLoad();
            }
        }

        protected void on_check_uncheck_all(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_TermConditions_Master.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbx_Terms");
                lbl = (Label)row.FindControl("lbl_terms_master");
                if (chbx != null)
                {
                    if (base_Page.TermConditionList != null)
                    {
                        foreach (TermAndCondition item in base_Page.TermConditionList)
                        {
                            if (item.Name == lbl.Text)
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

        protected void on_chbx_Terms(object sender, EventArgs e)
        {
                track = false;
                CheckBox chb = (CheckBox)gv_TermConditions_Master.HeaderRow.FindControl("chbx_select_all");
                foreach (GridViewRow row in gv_TermConditions_Master.Rows)
                {
                    chbx = (CheckBox)row.FindControl("chbx_Terms");
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

        protected void gv_TermConditions_Master_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                chbx = (CheckBox)e.Row.FindControl("chbx_Terms");
                lbl = (Label)e.Row.FindControl("lbl_terms_master");
                if (chbx != null)
                {
                    if (base_Page.TermConditionList != null)
                    {
                        foreach (TermAndCondition item in base_Page.TermConditionList)
                        {
                            if (item.Name == lbl.Text)
                            {
                                chbx.Checked = false;
                                chbx.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        protected void btnAddTerm_Click(object sender, EventArgs e)
        {
            lstTermCondition = new List<TermAndCondition>();
            Boolean flag = false;
            foreach (GridViewRow row in gv_TermConditions_Master.Rows)
            {
                chbx = (CheckBox)row.FindControl("chbx_Terms");
                hdf = (HiddenField)row.FindControl("hdf_term_master_id");
                lbl = (Label)row.FindControl("lbl_terms_master");
                if (chbx != null && hdf != null)
                {
                    if (chbx.Checked.Equals(true))
                    {
                        flag = true;
                        termCondition = new TermAndCondition();
                        termCondition.TermsId = Convert.ToInt32(hdf.Value);
                        termCondition.Name = lbl.Text;
                        lstTermCondition.Add(termCondition);
                    }
                }
            }
            if (flag == false)
            {
                base_Page.Alert("Please select any term and condition.", btnAddTerm);
                return;
            }

            if (lstTermCondition != null)
            {
                if (base_Page.TermConditionList == null)
                {
                    base_Page.TermConditionList = new List<TermAndCondition>();
                    base_Page.TermConditionList = lstTermCondition;
                }
                else
                {
                    foreach (TermAndCondition item in lstTermCondition)
                    {
                        base_Page.TermConditionList.Add(item);
                    }
                }
            }

            LoadControl();
        }

        protected void btn_add_final_Click(object sender, EventArgs e)
        {
            if (btn_add_final.Text == "Add")
            {
                termCondition = GetControlsData();
                if (base_Page.TermConditionList == null)
                {
                    base_Page.TermConditionList = new List<TermAndCondition>();
                    if (CheckMasterTermCondition(termCondition))
                        lbl_duplicate_term_condition.Text = GlobalConstants.L_Duplicate_Master_TermCondition;
                    else
                        base_Page.TermConditionList.Add(termCondition);
                }
                else
                {
                    if (CheckMasterTermCondition(termCondition))
                        lbl_duplicate_term_condition.Text = GlobalConstants.L_Duplicate_Master_TermCondition;
                    else if (CheckDuplicateTermCondition(termCondition))
                        lbl_duplicate_term_condition.Text = GlobalConstants.L_Duplicate_TermCondition;
                    else
                        base_Page.TermConditionList.Add(termCondition);
                }
            }
            else if (btn_add_final.Text == "Update")
            {
                if (this.LastIndex >= 0)
                {
                    termCondition = GetControlsData();
                    if (CheckMasterTermCondition(termCondition))
                        lbl_duplicate_term_condition.Text = GlobalConstants.L_Duplicate_Master_TermCondition;
                    else if (CheckDuplicateTermCondition(termCondition))
                        lbl_duplicate_term_condition.Text = GlobalConstants.L_Duplicate_TermCondition;
                    else
                    {
                        base_Page.TermConditionList[this.LastIndex].Name = termCondition.Name;
                        btn_add_final.Text = "Add";
                        LastIndex = -1;
                    }
                }
            }
            LoadControl();
            if (track == false)
            {
                ClearAll();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected void gv_final_TermsConditions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hdf = (HiddenField)e.Row.FindControl("hdf_term_id");
                lbtn = (LinkButton)e.Row.FindControl("lnkEdit");
                if (hdf != null && lbtn != null)
                {
                    foreach (TermAndCondition item in base_Page.TermConditionList)
                    {
                        if (Convert.ToInt32(hdf.Value) != 0)
                        {
                            lbtn.Visible = false;
                        }
                    }
                }
            }
        }

        protected void gv_final_TermsConditions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edit")
            {
                txt_term.Text = base_Page.TermConditionList[index].Name;

                btn_add_final.Text = "Update";
                this.LastIndex = index;
            }
            else if (e.CommandName == "Delete")
            {
                base_Page.TermConditionList.RemoveAt(index);
                if (base_Page.TermConditionList.Count == 0)
                {
                    ClearAll();
                    btn_add_final.Text = "Add";
                    base_Page.TermConditionList = null;
                }
                LoadControl();
            }
        }

        protected void gv_final_TermsConditions_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gv_final_TermsConditions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_final_TermsConditions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_final_TermsConditions.PageIndex = e.NewPageIndex;
            BindTermCondition();
        }

        #endregion

        #region Private Methods

        private void DefaultLoad()
        {
            BindTermAndConditionMaster();
            BindTermCondition();
            ControlsView();
        }

        private void ValidationExpression()
        {

        }

        private void BindTermAndConditionMaster()
        {
            try
            {
                termAndConditionBL = new TermAndConditionBL();
                lstTermCondition = new List<TermAndCondition>();
                if (IsContractor)
                {
                    lstTermCondition = termAndConditionBL.ReadTermsAndConditions(null, Convert.ToInt32(TermAndConditionType.Contractor));
                }
                else
                {
                    lstTermCondition = termAndConditionBL.ReadTermsAndConditions(null, Convert.ToInt32(TermAndConditionType.Supplier));
                }
                gv_TermConditions_Master.DataSource = lstTermCondition;
                gv_TermConditions_Master.DataBind();
            }
            catch
            {
                gv_TermConditions_Master.DataSource = null;
                gv_TermConditions_Master.DataBind();
            }
        }

        private void BindTermCondition()
        {
            if (base_Page.TermConditionList != null)
            {
                gv_final_TermsConditions.DataSource = base_Page.TermConditionList;
            }
            else
            {
                gv_final_TermsConditions.DataSource = null;
            }
            gv_final_TermsConditions.DataBind();
        }

        private TermAndCondition GetControlsData()
        {
            termCondition = new TermAndCondition();
            if (txt_term.Text != String.Empty)
            {
                termCondition.TermsId = 0;
                termCondition.Name = txt_term.Text.Trim();
            }
            return termCondition;
        }

        /// <summary>
        /// Check duplicate in Newly Created Term and Condiotion List
        /// </summary>
        /// <param name="termCondition"></param>
        /// <returns></returns>
        private Boolean CheckDuplicateTermCondition(TermAndCondition termCondition)
        {
            id = 0;
            foreach (TermAndCondition item in base_Page.TermConditionList)
            {
                id = id + 1;
                if (termCondition.Name.ToLower() == item.Name.ToLower())
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
        /// <summary>
        ///  Check duplicate in Master Term and condition List
        /// </summary>
        /// <param name="termCondition"></param>
        /// <returns></returns>
        private Boolean CheckMasterTermCondition(TermAndCondition termCondition)
        {
            id = 0;
            foreach (TermAndCondition item in lstTermCondition)
            {
                id = id + 1;
                if (termCondition.Name.ToLower() == item.Name.ToLower())
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

        /// <summary>
        /// Clear all Form Controls
        /// </summary>
        private void ClearAll()
        {
            btn_add_final.Text = "Add";
            txt_term.Text = String.Empty;
            lbl_duplicate_term_condition.Text = String.Empty;
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
            dv_terms.Visible = condition;
            dv_button.Visible = condition;
            tbl_add_term.Visible = condition;

            gv_final_TermsConditions.Columns[2].Visible = condition;
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

        private List<TermAndCondition> lstTermCondition
        {
            get
            {
                return (List<TermAndCondition>)ViewState["TermCondition"];
            }
            set
            {
                ViewState["TermCondition"] = value;
            }
        }

        private bool IsContractor
        {
            get
            {
                if (base_Page.Page_Name == GlobalConstants.P_Contractor_Quotation)
                {
                    flag = true;
                    return flag;
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            set
            {
                flag = value;
            }
        }

        #endregion







    }
}