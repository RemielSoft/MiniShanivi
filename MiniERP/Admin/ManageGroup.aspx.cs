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
    public partial class ManageGroup : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindgvGroup();
                txtName.Focus();
                SetValidationExp();
            }
        }


        #region protected Methods

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int IsSuccesfull;
            Group group = GetGroupDetail();
            group.Id = GroupId;
            GroupBL GroupBL = new GroupBL();
            IsSuccesfull = GroupBL.UpdateGroup(group);
            if (IsSuccesfull > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_GROUP_MESSAGE);
                BindgvGroup();
                SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
                ResetContorls();
            }
            else if (IsSuccesfull == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
                //ResetContorls();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int groupId;

            groupId = CreateGroup(GetGroupDetail());
            if (groupId > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_GROUP_MESSAGE);
                BindgvGroup();
                ResetContorls();
            }
            else if (groupId == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);

            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetContorls();
            
        }

        protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int groupId;
            string message;
            if (e.CommandName == "cmdEdit")
            {
                GroupId = groupId = Convert.ToInt32(e.CommandArgument);

                setControlsValue(GetGroupById(groupId));
                SetControlsVisiblity(GlobalConstants.C_MODE_UPDATE);
                txtName.Focus();
            }
            else if (e.CommandName == "cmdDelete")
            {
                groupId = Convert.ToInt32(e.CommandArgument);
                message = DeleteGroup(groupId);
                if (message == "")
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_GROUP_DELETE_MESSAGE);
                }
                else
                {
                    ShowMessageWithUpdatePanel(message);
                }
                BindgvGroup();
                ResetContorls();
            }
        }

        protected void gvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroup.PageIndex = e.NewPageIndex;
            BindgvGroup();
        }

        protected void gvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Group group = (Group)e.Row.DataItem;
                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Text = group.Name;
                //if(String.Equals(lblName.Text.ToUpper(),"Admin".ToUpper()))
                //if (GlobalConstants.ADMIN_USERS.Contains(employee.UserLoginId.ToUpper(), new CaseInSensetiveComparision()))
                //if (GlobalConstants.ADMIN_GROUPS.Contains(group.Name.ToUpper(), new CaseInSensetiveComparision())) // .Contains(lblName.Text.ToUpper(), new CaseInSensetiveComparision()))
                //{
                //    e.Row.FindControl("lnkEdit").Visible = false;
                //    e.Row.FindControl("lnkDelete").Visible = false;
                //}
                //else
                //{
                //    e.Row.FindControl("lnkEdit").Visible = true;
                //    e.Row.FindControl("lnkDelete").Visible = true;
                //}
            }
        }

        #endregion

        #region Private Metods
        private void SetValidationExp()
        {
            revName.ValidationExpression = ValidationExpression.C_ALPHABETS_NAMES;
            revName.ErrorMessage = "Group Name Should Be In Characters";
            //revdescription.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revdescription.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_DESCRIPTION;
        }
        /// <summary>
        /// Bindgvs the Group.
        /// </summary>
        private void BindgvGroup()
        {
            gvGroup.DataSource = GetGroups();
            gvGroup.DataBind();
        }

        /// <summary>
        /// Gets the Group detail.
        /// </summary>
        /// <returns></returns>
        private Group GetGroupDetail()
        {

            Group group = new Group();
            group.AuthorityLevel = new MetaData();
            group.Name = RemoveWhiteSpace(txtName.Text.Trim());
            group.Description = txtdiscription.Text.Trim();
            group.AuthorityLevel = new MetaData();
            group.AuthorityLevel.Id = Convert.ToInt32(AuthorityLevelType.User);
            group.CreatedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;
            group.CreatedDate = DateTime.Now;
            return group;
        }

        /// <summary>
        /// Creates the Group.
        /// </summary>
        /// <param name="Group">The Group.</param>
        /// <returns></returns>
        private int CreateGroup(Group group)
        {
            GroupBL groupBL = new GroupBL();
            int groupId = groupBL.CreateGroup(group);
            return groupId;
        }

        /// <summary>
        /// Gets the Groups.
        /// </summary>
        /// <returns></returns>
        private List<Group> GetGroups()
        {
            List<Group> listgroup = new List<Group>();
            GroupBL groupBL = new GroupBL();
            listgroup = groupBL.ReadGroups();
            return listgroup;

        }

        /// <summary>
        /// Gets the Group by id.
        /// </summary>
        /// <param name="GroupId">The Group id.</param>
        /// <returns></returns>
        private Group GetGroupById(int groupId)
        {
            List<Group> listGroup = new List<Group>();
            GroupBL groupBL = new GroupBL();
            return groupBL.ReadGroupById(groupId);
        }

        /// <summary>
        /// Sets the controls value.
        /// </summary>
        /// <param name="Group">The Group.</param>
        private void setControlsValue(Group group)
        {
            txtdiscription.Text = group.Description;
            txtName.Text = group.Name;
            //ddlauthority.SelectedValue = Convert.ToString(group.AuthorityLevel.Authority_level);
        }

        /// <summary>
        /// Resets the contorls.
        /// </summary>
        private void ResetContorls()
        {
            SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
            //ddlauthority.SelectedIndex = 0;
            txtdiscription.Text = string.Empty;
            txtName.Text = string.Empty;
            txtName.Focus();
        }

        /// <summary>
        /// Deletes the Group.
        /// </summary>
        /// <param name="GroupId">The Group id.</param>
        private string DeleteGroup(int GroupId)
        {
            GroupBL groupBL = new GroupBL();
            return groupBL.DeleteGroup(GroupId, LoggedInUser.UserLoginId, DateTime.Now);
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
        /***********code is commented for further use......please dont delete **********/
        //private void BindAuthorityLevel()
        //{
        //    MetaDataBL metadataBl = new MetaDataBL();
        //    ddlauthority.Items.Clear();
        //    ddlauthority.Items.Add(new ListItem("--Select--", "0"));
        //    ddlauthority.DataSource = metadataBl.ReadMetaDataAuthority();
        //    ddlauthority.DataTextField = "Name";
        //    ddlauthority.DataValueField = "Authority_Level";
        //    ddlauthority.DataBind();
        //}
        //-***********code is commented for further use......please dont delete **********--%>//
        #endregion

        # region  privateProperities
        /// <summary>
        /// Gets or sets the Group id.
        /// </summary>
        /// <value>The Group id.</value>
        public int GroupId
        {
            get
            {
                return (int)ViewState["GroupId"];
            }
            set
            {
                ViewState["GroupId"] = value;
            }
        }
        #endregion

      
        
    }
}