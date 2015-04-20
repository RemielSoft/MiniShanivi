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
    public partial class ManageDepartment :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDepartment();
            txtName.Focus();
            SetValidationExp();
            //CalExt.EndDate = DateTime.Now.AddYears(-15);
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            int departmentId;
            departmentId = CreateDepartment(GetDepartmentDetail());
            if (departmentId > 0)
            {

                ShowMessageWithUpdatePanel(GlobalConstants.C_DEPARTMENT_CREATE_MESSAGE);
                BindDepartment();
                ResetContorls();
            }
            else if (departmentId == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int IsSuccesfull;
            Department department = GetDepartmentDetail();
            department.DepartmentId = DepartmentId;
            DepartmentBL departmentBL = new DepartmentBL();
            IsSuccesfull = departmentBL.UpdateDepartment(department);

            if (IsSuccesfull > 0)
            {

                ShowMessageWithUpdatePanel(GlobalConstants.C_DEPARTMENT_UPDATE_MESSAGE);
                ResetContorls();
                BindDepartment();
            }
            else if (IsSuccesfull == 0)
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
            ResetContorls();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetContorls();
            
        }


        protected void gvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int departmentId;
            string message;
            if (e.CommandName == "cmdEdit")
            {
                DepartmentId = departmentId = Convert.ToInt32(e.CommandArgument);

                setControlsValue(GetDepartmentId(departmentId));

                SetControlsVisiblity(GlobalConstants.C_MODE_UPDATE);
                txtName.Focus();
            }
            else if (e.CommandName == "cmdDelete")
            {
                departmentId = Convert.ToInt32(e.CommandArgument);
                message = DeleteDepartment(departmentId);
                if (message == "")
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DEPARTMENT_DELETE_MESSAGE);
                }
                else
                {
                    ShowMessageWithUpdatePanel(message);
                }
                BindDepartment();
                ResetContorls();

            }
        }

        protected void gvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDepartment.PageIndex = e.NewPageIndex;
            BindDepartment();
        }

        protected void gvDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region Private Methods

        private void SetValidationExp()
        {
            revName.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            revName.ErrorMessage = "Department Name Should Be In Characters";
            //revdescription.ValidationExpression = ValidationExpression.C_New;
            //revdescription.ErrorMessage = "Max Length 10";
        }

        private List<Department> LoadDepartments()
        {
            DepartmentBL departmentBL = new DepartmentBL();
            return departmentBL.ReadDepartments();
        }
        private void BindDepartment()
        {
            gvDepartment.DataSource = LoadDepartments();
            gvDepartment.DataBind();
        }
        private void BindDepartment(object dataSource)
        {
            gvDepartment.DataSource = dataSource;
            gvDepartment.DataBind();
        }
        private void ResetContorls()
        {
            SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
            txtdiscription.Text = string.Empty;
            txtName.Text = string.Empty;
            txtName.Focus();
        }
        private void setControlsValue(Department department)
        {
            txtdiscription.Text = department.Description;
            txtName.Text = department.Name;   
        }
        private int CreateDepartment(Department department)
        {
            DepartmentBL departmentBL = new DepartmentBL();
            int DepartmentId = departmentBL.CreateDepartment(department);
            return DepartmentId;
        }
        private Department GetDepartmentDetail()
        {
            Department department = new Department();
            department.Name = RemoveWhiteSpace(txtName.Text.Trim());
            department.Description = txtdiscription.Text;
            department.CreatedBy = LoggedInUser.UserLoginId;
            department.CreatedDate = DateTime.Now;
            return department;
        }
        private void SetControlsVisiblity(String Mode)
        {
            if (Mode == GlobalConstants.C_MODE_INSERT)
            {
                btnUpdate.Visible = false;
                btnSave.Visible = true;
            }
            else if (Mode == GlobalConstants.C_MODE_UPDATE)
            {
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }
        private Department GetDepartmentId(int departmentId)
        {
            List<Department> listLocation = new List<Department>();
            DepartmentBL locationBL = new DepartmentBL();
            return locationBL.ReadDepartmentById(departmentId);
        }
        private string DeleteDepartment(int departmentId)
        {
            DepartmentBL departmentBL = new DepartmentBL();
            return departmentBL.DeleteDepartment(departmentId, LoggedInUser.UserLoginId, DateTime.Now);
        }

        #endregion

        # region  private Properities
        /// <summary>
        /// Gets or sets the location id.
        /// </summary>
        /// <value>The location id.</value>
        private int DepartmentId
        {
            get
            {
                return (int)ViewState["DepartmentId"];
            }
            set
            {
                ViewState["DepartmentId"] = value;
            }
        }
        #endregion

    }
}