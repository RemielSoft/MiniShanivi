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
    public partial class ManageProject : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgvProject();
                SetValidationExp();
                txtprojectname.Focus();
            }
        }

        #region Protected Method

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int projectId;

            projectId = CreateProject(GetProjectDetail());
            if (projectId > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_PROJECT_MESSAGE);
                BindgvProject();
                ResetContorls();
            }
            else if (projectId == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);

            }
           // ResetContorls();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int IsSuccesfull;
            Project project = GetProjectDetail();
            project.ProjectId = ProjectId;
            ProjectBAL ProjectBL = new ProjectBAL();
            IsSuccesfull = ProjectBL.UpdateProject(project);
            if (IsSuccesfull > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_PROJECT_MESSAGE);
                BindgvProject();
                SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
                ResetContorls();
            }
            else if (IsSuccesfull == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
                ResetContorls();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetContorls();
            
        }

        protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProject.PageIndex = e.NewPageIndex;
            BindgvProject();
        }

        protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int projectId;
            string message;
            if (e.CommandName == "cmdEdit")
            {
                ProjectId = projectId = Convert.ToInt32(e.CommandArgument);

                setControlsValue(GetProjectById(projectId));
                SetControlsVisiblity(GlobalConstants.C_MODE_UPDATE);
            }
            else if (e.CommandName == "cmdDelete")
            {
                projectId = Convert.ToInt32(e.CommandArgument);
                message = DeleteProject(projectId);
                if (message == "")
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DELETE_PROJECT_MESSAGE);
                }
                else
                {
                    ShowMessageWithUpdatePanel(message);
                }
                BindgvProject();
                ResetContorls();
            }
        }

        #endregion

        #region Private Metods
        public void SetValidationExp()
        {
            revName.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revName.ErrorMessage = "Project Name Should be Alphanumeric";
            //revdescription.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revdescription.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_DESCRIPTION;
            //revAddress.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revAddress.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ADDRESS;
            revCity.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            revCity.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_CITY;
            revState.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            revState.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_STATE;
        }

        /// <summary>
        /// Bindgvs the Project.
        /// </summary>
        private void BindgvProject()
        {
            gvProject.DataSource = GetProject();
            gvProject.DataBind();
        }

        /// <summary>
        /// Gets the Project detail.
        /// </summary>
        /// <returns></returns>
        private Project GetProjectDetail()
        {
            Project project = new Project();
            project.Name = txtprojectname.Text.Trim();
            project.City = txtcity.Text.Trim();
            project.State = txtstate.Text.Trim();
            project.Address = txtadd.Text.Trim();
            project.Description = txtdes.Text.Trim();           
            project.CreatedBy = LoggedInUser.UserLoginId;
            project.CreatedDate = DateTime.Now;
            return project;
        }

        /// <summary>
        /// Creates the Group.
        /// </summary>
        /// <param name="Group">The Group.</param>
        /// <returns></returns>
        private int CreateProject(Project project)
        {
            ProjectBAL projectBL = new ProjectBAL();
            int projectId = projectBL.CreateProject(project);
            return projectId;
        }

        /// <summary>
        /// Gets the Project.
        /// </summary>
        /// <returns></returns>
        private List<Project> GetProject()
        {
            List<Project> listproject = new List<Project>();
            ProjectBAL projectBL = new ProjectBAL();
            listproject = projectBL.ReadProject();
            return listproject;

        }

        /// <summary>
        /// Gets the Project by id.
        /// </summary>
        /// <param name="ProjectId">The Project id.</param>
        /// <returns></returns>
        private Project GetProjectById(int projectId)
        {
            List<Project> listProject = new List<Project>();
            ProjectBAL projectBL = new ProjectBAL();
            return projectBL.ReadProjectById(projectId);
        }

        /// <summary>
        /// Sets the controls value.
        /// </summary>
        /// <param name="Project">The Project.</param>
        private void setControlsValue(Project project)
        {
            txtprojectname.Text = project.Name;
            txtcity.Text = project.City;
            txtstate.Text = project.State;
            txtadd.Text = project.Address;
            txtdes.Text = project.Description;
            //ddlauthority.SelectedValue = Convert.ToString(group.AuthorityLevel.Authority_level);
        }

        /// <summary>
        /// Resets the contorls.
        /// </summary>
        private void ResetContorls()
        {
            SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
            txtprojectname.Text = string.Empty;
            txtcity.Text = string.Empty;
            txtadd.Text = string.Empty;
            txtdes.Text = string.Empty;
            txtstate.Text = string.Empty;
            txtprojectname.Focus();
        }

        /// <summary>
        /// Deletes the Group.
        /// </summary>
        /// <param name="GroupId">The Group id.</param>
        private string DeleteProject(int ProjectId)
        {
            ProjectBAL projectBL = new ProjectBAL();
            return projectBL.DeleteProject(ProjectId, LoggedInUser.UserLoginId, DateTime.Now);
        }

        /// <summary>
        /// Sets the controls visiblity.
        /// </summary>
        /// <param name="Mode">The mode.</param>
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

        #endregion

        # region  privateProperities
        /// <summary>
        /// Gets or sets the Project id.
        /// </summary>
        /// <value>The Project id.</value>
        public int ProjectId
        {
            get
            {
                return (int)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion



    }
}