using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;

namespace MiniERP.Admin
{
    public partial class TermAndConditionMaster :BasePage
    {
        //List<QuotationType> lstQuotationType = null;

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rdbtnlst.SelectedIndex = 0;
                BindTermsAndCondition();
                SetValidationExp();
                txtName.Focus();
                BindCheckbokList();
            }
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int termsId;
            termsId = CreateTermsAndConditions(GetTermsAndConditionsDetail());
            if (termsId > 0)
            {

                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_TERMS_AND_CONDITION_MESSAGE);
                BindTermsAndCondition();
                ResetContorls();
            }
            else if (termsId == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
            }
            
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int IsSuccesfull;
            TermAndCondition termAndCondition = GetTermsAndConditionsDetail();
            // department.DepartmentId = DepartmentId;
            TermAndConditionBL termAndConditionBL = new TermAndConditionBL();
            IsSuccesfull = termAndConditionBL.UpdateTermsAndConditions(termAndCondition);

            if (IsSuccesfull > 0)
            {

                ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_TERMS_AND_CONDITION_MESSAGE);
                ResetContorls();
                BindTermsAndCondition();
                SetValidationExp();
            }
            else if (IsSuccesfull == 0)
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
            ResetContorls();
        }
        protected void gvterms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvterms.PageIndex = e.NewPageIndex;
            BindTermsAndCondition();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetContorls();
        }

        protected void gvterms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int termsId;
            TermsId = termsId = Convert.ToInt32(e.CommandArgument);
            string message;
            if (e.CommandName == "cmdEdit")
            {


                GetTermAndConditionById(TermsId);
                btnUpdate.Visible = true;
                btnSave.Visible = false;
                //SetControlsVisiblity(GlobalConstants.C_MODE_UPDATE);
            }
            else if (e.CommandName == "cmdDelete")
            {
                //termsId = Convert.ToInt32(e.CommandArgument);
                message = DeleteTermsAndConditions(TermsId);
                if (message == "")
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DELETE_TERMS_AND_CONDITION_MESSAGE);
                }
                else
                {
                    ShowMessageWithUpdatePanel(message);
                }                
                ResetContorls();

            }
            BindTermsAndCondition();
        }        


        #endregion         

        #region Private Methods
        private void BindCheckbokList()
        {
            rdbtnlst.DataSource = Enum.GetNames(typeof(TermAndConditionType));
            rdbtnlst.DataBind();
        }
        private void SetValidationExp()
        {
            //revName.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            //revName.ErrorMessage = "Terms & conditions should be in characters";           
            //revDescription.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revDescription.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_DESCRIPTION;               
            
        }
        private string DeleteTermsAndConditions(int termsId)
        {
            TermAndConditionBL termAndConditionBL = new TermAndConditionBL();
            return termAndConditionBL.DeleteTermsAndConditions(termsId, LoggedInUser.UserLoginId, DateTime.Now);
        }
        private int CreateTermsAndConditions(TermAndCondition termAndCondition)
        {
            TermAndConditionBL termAndConditionBL = new TermAndConditionBL();
            int termsId = termAndConditionBL.CreateTermsAndConditions(termAndCondition);
            return termsId;
        }
        private List<TermAndCondition> LoadTermAndCondition()
        {
            TermAndCondition termAndCondition = new TermAndCondition();
            TermAndConditionBL termAndConditionBL = new TermAndConditionBL();
            List<TermAndCondition> lstTermAndCondition=new List<TermAndCondition>();
            lstTermAndCondition= termAndConditionBL.ReadTermsAndConditions(null,null);
            for (int i = 0; i < lstTermAndCondition.Count; i++)
            {
                if (lstTermAndCondition[i].TermAndConditionType == 1)
                {
                    termAndCondition.TermAndConditionName = TermAndConditionType.Contractor.ToString();
                }
                else if (lstTermAndCondition[i].TermAndConditionType == 2)
                {
                    termAndCondition.TermAndConditionName = TermAndConditionType.Supplier.ToString();
                }
                else if (lstTermAndCondition[i].TermAndConditionType == 3)
                {
                    termAndCondition.TermAndConditionName = TermAndConditionType.Both.ToString();
                }
                lstTermAndCondition[i].TermAndConditionName = termAndCondition.TermAndConditionName;
            }
            return lstTermAndCondition;
        }
        private void BindTermsAndCondition()
        {
            gvterms.DataSource = LoadTermAndCondition();
            gvterms.DataBind();
        }

        private TermAndCondition GetTermsAndConditionsDetail()
        {
            TermAndCondition termAndCondition = new TermAndCondition();
            if (btnUpdate.Visible==true)
            {
                termAndCondition.TermsId = TermsId;
                termAndCondition.ModifiedBy = LoggedInUser.UserLoginId;
            }
            termAndCondition.Name = RemoveWhiteSpace(txtName.Text.Trim());
            termAndCondition.Description = txtdescription.Text;
            if (rdbtnlst.SelectedItem.Text == TermAndConditionType.Contractor.ToString())
            {
                termAndCondition.TermAndConditionType = Convert.ToInt32(rdbtnlst.SelectedIndex)+1;
            }
            else if (rdbtnlst.SelectedItem.Text == TermAndConditionType.Supplier.ToString())
            {
                termAndCondition.TermAndConditionType = Convert.ToInt32(rdbtnlst.SelectedIndex) + 1;
            }
            else if (rdbtnlst.SelectedItem.Text == TermAndConditionType.Both.ToString())
            {
                termAndCondition.TermAndConditionType = Convert.ToInt32(rdbtnlst.SelectedIndex) + 1;
            }
            if (btnSave.Visible == true)
            {
                termAndCondition.CreatedBy = LoggedInUser.UserLoginId;
                termAndCondition.CreatedDate = DateTime.Now;
            }
            
            return termAndCondition;
        }
        private void GetTermAndConditionById(int termsId)
        {
            List<TermAndCondition> lstTermAndCondition = new List<TermAndCondition>();
            TermAndConditionBL termAndConditionBL = new TermAndConditionBL();
            lstTermAndCondition=termAndConditionBL.ReadTermsAndConditions(termsId,null);
            txtName.Text = lstTermAndCondition[0].Name;
            txtdescription.Text = lstTermAndCondition[0].Description;
            rdbtnlst.SelectedIndex = lstTermAndCondition[0].TermAndConditionType-1;           
        }
        //private void setControlsValue(TermAndCondition termAndCondition)
        //{


        //    txtdescription.Text = termAndCondition.Description;
        //    txtName.Text = termAndCondition.Name;


        //}

        private void ResetContorls()
        {
            //SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
            txtdescription.Text = string.Empty;
            rdbtnlst.SelectedIndex = 0;
            txtName.Text = string.Empty;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            txtName.Focus();
        }
        #endregion        

        #region Private Properties

        private int TermsId
        {
            get
            {
                return (int)ViewState["TermsId"];
            }
            set
            {
                ViewState["TermsId"] = value;
            }
        }

        #endregion

        
    }
}