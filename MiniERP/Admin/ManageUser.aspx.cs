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
    public partial class ManageUser : BasePage
    {
        #region Global Varriable
        GroupBL groupBL = new GroupBL();
        DepartmentBL departmentBL = new DepartmentBL();
        UserBL userBL = new UserBL();
        Users users = new Users();
        Boolean flag = false;
        #endregion
       
        #region Protected  method

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgvUser();
                BindDropDown(ddldepartmentId, "Name", "DepartmentId", departmentBL.ReadDepartments());
                //BindCheckBoxList();
                BindRadioButtonList();
                SetValidationExp();
                txtlogin.Focus();
                CalExt.EndDate = DateTime.Now.AddYears(-15);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!CheckGroup())
            {
                ShowMessageWithUpdatePanel("Please select at list one group");
                return;
            }

            int IsSuccesfull;
            Users User = GetUserDetail();
            User.UserId = UserId;
            IsSuccesfull = userBL.UpdateUser(User);
            if (IsSuccesfull > 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_USER_MESSAGE);
                BindgvUser();
                SetControlsVisiblity(GlobalConstants.C_MODE_INSERT);
                ResetControles();
            }
            else if (IsSuccesfull == 0)
            {
                ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
                ResetControles();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckGroup())
            {
                ShowMessageWithUpdatePanel("Please select at list one group");
                return;
            }
            List<Users> lstuser = userBL.ReadUser();
            foreach (Users item in lstuser)
            {
                if (item.UserLoginId == txtlogin.Text)
                {
                    ShowMessageWithUpdatePanel("Please Enter Different LoginId");
                    flag = true;
                    break;
                }
                else if (item.EmpCode == txtempcode.Text)
                {
                    ShowMessageWithUpdatePanel("Please Enter Different Employee Code");
                    flag = true;
                    break;
                }
            }
            if (flag == false)
            {
                int userId = 0;
                Users u = GetUserDetail();
                userId = CreateUser(u);
                if (userId > 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_CREATE_USER_MESSAGE);
                    BindgvUser();
                    ResetControles();
                }
                else if (userId == 0)
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_DUPLICATE_MESSAGE);
                }
            }
            BindgvUser();
        }

        //protected void txtlogin_TextChanged(object sender, EventArgs e)
        //{
        //    String id = string.Empty;
        //    id = txtlogin.Text.Trim();
        //    users = userBL.ReadLoginId(id);
        //    if (users.UserLoginId == id)
        //    {
        //        lblMsg.Visible = true;
        //        txtlogin.Text = string.Empty;
        //    }
        //    else
        //    {
        //        lblMsg.Visible = false;
        //    }
        //}
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControles();
        }

        protected void gvUser_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            BindgvUser();
        }

        protected void gvUser_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            int userId = 0;
            UserId = userId = Convert.ToInt32(e.CommandArgument);
            string message;
            if (e.CommandName == "cmdEdit")
            {
                txtlogin.Enabled = false;
                setControlsValue(GetUserById(userId));
                SetControlsVisiblity(GlobalConstants.C_MODE_UPDATE);

            }
            else if (e.CommandName == "cmdDelete")
            {

                message = DeleteUser(UserId);
                if (message == "")
                {
                    ShowMessageWithUpdatePanel(GlobalConstants.C_USER_DELETE_MESSAGE);
                }
                else
                {
                    ShowMessageWithUpdatePanel(message);
                }

                ResetControles();
            }
            if (e.CommandName == "Details")
            {
                List<Users> listUser = new List<Users>();
                UserBL userBL = new UserBL();
                listUser = userBL.ReadUserDetails(userId);

                lblAddress.Text = listUser[0].Address.ToString();
                if (listUser[0].DateOfBirth != DateTime.MinValue)
                {
                    lblDob.Text = listUser[0].DateOfBirth.ToString("dd/MMM/yyyy");
                }
                else
                {
                    lblDob.Text = string.Empty;
                }
                lblEmailId.Text = listUser[0].EmailId.ToString();
                lblEmpCode.Text = listUser[0].EmpCode.ToString();
                lblGender.Text = listUser[0].Gender.ToString();
                //lblGrp.Text = listUser[0].Groups.ToString();
                lblLoginId.Text = listUser[0].UserLoginId.ToString();
                lblMaritalStatus.Text = listUser[0].MaritalStatus.ToString();
                lblMobile.Text = listUser[0].Mobile.ToString();
                lblName.Text = listUser[0].FullName.ToString();
                lblOfficeExtNo.Text = listUser[0].OfficeExtensionNumber.ToString();
                lblPwd.Text = listUser[0].Password.ToString();
                lblDept.Text = listUser[0].Department.Name.ToString();
                lblPhn.Text = listUser[0].Phone.ToString();
                string str = string.Empty;
                List<Group> lstGroup = new List<Group>();
                lstGroup = userBL.ReadGroupByUserId(userId);
                foreach (Group item in lstGroup)
                {
                    str = str + item.Name + ",";
                }

               // lblgrp.Text = str.TrimEnd(',');
                //
                //List<Group> lstGroup = new List<Group>();
                //lstGroup = userBL.ReadGroupByUserId(userId);
                //ChkbxGrouplst.DataSource = lstGroup;
                //ChkbxGrouplst.DataTextField = "Name";
                //ChkbxGrouplst.DataValueField = "Id";
                //ChkbxGrouplst.DataBind();
                //
                ModalPopupExtender2.Show();
            }
            BindgvUser();
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Users user = (Users)e.Row.DataItem;
                Label lblDOB = (Label)e.Row.FindControl("lblDOB");
                if (user.DateOfBirth != DateTime.MinValue)
                {
                    lblDOB.Text = user.DateOfBirth.ToString("dd/MMM/yyyy");
                }
                else
                {
                    lblDOB.Text = String.Empty;
                }
            }
        }
        #endregion

        #region Private Method

        private Boolean CheckGroup()
        {
            //foreach (ListItem item in chbgrp.Items )
            //{
            //    if (item.Selected)
            //        return true;
            //}
            foreach (ListItem item in rdoGroup.Items)
            {
                if (item.Selected)
                    return true;
            }
            return false;
        }

        private void SetValidationExp()
        {
            revFName.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revFName.ErrorMessage = "First Name Should Be Alphanumeric";
            revMName.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revMName.ErrorMessage = "Middle Name Should Be Alphanumeric";
            revLName.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revLName.ErrorMessage = "Last Name Should Be Alphanumeric";
            revLoginId.ValidationExpression = ValidationExpression.C_USER_ID;
            revLoginId.ErrorMessage = "Login-Id Should Be Alphanumeric";
            revEmpCode.ValidationExpression = ValidationExpression.C_ALPHANUMERIC;
            revEmpCode.ErrorMessage = "Emp Code Should Be Alphanumeric";
            revMobile.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revMobile.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_MOBILE_NUMBER;
            revPhone.ValidationExpression = ValidationExpression.C_MOBILE_PHONE_NUMBER;
            revPhone.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_MOBILE_NUMBER;
            revOfficeExtNo.ValidationExpression = ValidationExpression.C_NUMERIC;
            revOfficeExtNo.ErrorMessage = "Office Extension No. Should Be Numeric";
            revEmailId.ValidationExpression = ValidationExpression.C_EMAIL_ID;
            revEmailId.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_EMAILID;
            revPassword.ValidationExpression = ValidationExpression.C_PASSWORD;
            revPassword.ErrorMessage = "Password Must Contain At Least 1 Alphabate, 1 Digit, No Special Characters And Length Must Be 5-20";
            //revAddress.ValidationExpression = ValidationExpression.C_ADDRESS;
            //revAddress.ErrorMessage = GlobalConstants.C_ERROR_MESSAGE_ADDRESS;
        }

        /// <summary>
        /// Bindgvs the User.
        /// </summary>
        private void BindgvUser()
        {
            gvUser.DataSource = GetUsers();
            gvUser.DataBind();
        }

        /// <summary>
        /// Sets the controls value.
        /// </summary>
        /// <param name="User">The User.</param>
        private void setControlsValue(Users user)
        {
            txtfname.Text = user.FirstName;
            txtlname.Text = user.LastName;
            txtmname.Text = user.MiddleName;

            if (user.DateOfBirth != DateTime.MinValue)
            {
                //txtdob.Text = Convert.ToString(user.DateOfBirth);
                txtdob.Text = user.DateOfBirth.ToShortDateString();
            }
            else
            {
                txtdob.Text = String.Empty;
            }
            txtemail.Text = user.EmailId;
            txtempcode.Text = user.EmpCode;
            txtaddress.Text = user.Address;
            rdbgender.SelectedValue = user.Gender;
            rdbMaritalstatus.SelectedValue = user.MaritalStatus;
            txtlogin.Text = user.UserLoginId;
            txtmobile.Text = user.Mobile;
            txtofficeextno.Text = user.OfficeExtensionNumber;
            txtpasswrd.Text = user.Password;
            txtConfirmPassword.Text = user.Password;
            txtphn.Text = user.Phone;

            //foreach (ListItem item in chbgrp.Items)
            //{
            //    item.Selected = false;
            //}
            foreach (ListItem item in rdoGroup.Items)
            {
                item.Selected = false;
            }

            //foreach (ListItem item in chbgrp.Items)
            //{
            //    foreach (Group pop in user.Groups)
            //    {
            //        if (Convert.ToInt32(item.Value) == pop.Id)
            //        {
            //            item.Selected = true;
            //        }
            //    }
            //}
            foreach (ListItem item in rdoGroup.Items)
            {
                foreach (Group pop in user.Groups)
                {
                    if (Convert.ToInt32(item.Value) == pop.Id)
                    {
                        item.Selected = true;
                    }
                }
            }
            ddldepartmentId.SelectedValue = user.Department.DepartmentId.ToString();
        }

        /// <summary>
        /// Gets the User by id.
        /// </summary>
        /// <param name="UserId">The User id.</param>
        /// <returns></returns>
        private Users GetUserById(int userId)
        {
            List<Users> listUser = new List<Users>();
            UserBL userBL = new UserBL();
            return userBL.ReadUserById(userId);

        }

        /// <summary>
        /// Gets the Users.
        /// </summary>
        /// <returns></returns>
        private List<Users> GetUsers()
        {
            List<Users> listUsers = new List<Users>();
            UserBL userBL = new UserBL();
            listUsers = userBL.ReadUser();
            return listUsers;

        }
        /// <summary>
        /// Creates the User.
        /// </summary>
        /// <param name="Group">The User.</param>
        /// <returns></returns>
        private int CreateUser(Users User)
        {
            UserBL userBL = new UserBL();
            int userId = userBL.CreateUser(User);
            return userId;
        }

        /// <summary>
        /// Deletes the User.
        /// </summary>
        /// <param name="UserId">The User id.</param>
        private string DeleteUser(int UserId)
        {
            UserBL userBL = new UserBL();
            return userBL.DeleteUser(UserId, LoggedInUser.UserLoginId, DateTime.Now);
        }
        /// <summary>
        /// Gets the Group detail.
        /// </summary>
        /// <returns></returns>
        private Users GetUserDetail()
        {
            txtlogin.Enabled = true;
            Users user = new Users();

            user.FirstName = txtfname.Text.Trim();
            user.LastName = txtlname.Text.Trim();
            user.MiddleName = txtmname.Text.Trim();
            user.UserLoginId = txtlogin.Text.Trim();
            user.MaritalStatus = rdbMaritalstatus.SelectedItem.Text;
            user.Gender = rdbgender.SelectedItem.Text;
            user.EmailId = txtemail.Text.Trim();
            user.EmpCode = txtempcode.Text.Trim();
            user.UserLoginId = txtlogin.Text.Trim();
            user.Password = txtpasswrd.Text.Trim();
            user.Address = txtaddress.Text.Trim();
            user.Phone = txtphn.Text.Trim();
            user.Mobile = txtmobile.Text.Trim();
            user.OfficeExtensionNumber = txtofficeextno.Text.Trim();
            //user.Department.DepartmentId = Convert.ToInt32(ddldepartmentId.SelectedValue);
            if (!string.IsNullOrEmpty(txtdob.Text))
            {
                user.DateOfBirth = Convert.ToDateTime(txtdob.Text.Trim());
            }
            user.Groups = new List<Group>();
            //foreach (ListItem item in chbgrp.Items)
            //{
            //    if (item.Selected == true)
            //    {
            //        Group grp = new Group();
            //        grp.Id = Convert.ToInt32(item.Value);
            //        grp.CreatedBy = LoggedInUser.UserLoginId;
            //        user.Groups.Add(grp);
            //    }
            //}
            foreach (ListItem item in rdoGroup.Items)
            {
                if (item.Selected == true)
                {
                    Group grp = new Group();
                    grp.Id = Convert.ToInt32(item.Value);
                    grp.CreatedBy = LoggedInUser.UserLoginId;
                    user.Groups.Add(grp);
                }
            }

            //user.Department.DepartmentId =Convert.ToInt32(ddldepart.SelectedValue);

            if (!string.IsNullOrEmpty(ddldepartmentId.SelectedValue.ToString()))
            {
                user.Department = new Department();
                user.Department.DepartmentId = Convert.ToInt32(ddldepartmentId.SelectedValue);
            }
            user.CreatedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;

            user.CreatedDate = DateTime.Now;
            return user;
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

        private void ResetControles()
        {
            //foreach (ListItem listItem in chbgrp.Items)
            //{
            //    listItem.Selected = false;
            //}
            foreach (ListItem listItem in rdoGroup.Items)
            {
                listItem.Selected = false;
            }

            txtfname.Text = string.Empty;
            txtlname.Text = string.Empty;
            txtmname.Text = string.Empty;
            txtpasswrd.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtphn.Text = string.Empty;
            txtofficeextno.Text = string.Empty;
            txtmobile.Text = string.Empty;
            txtlogin.Text = string.Empty;
            txtempcode.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtdob.Text = string.Empty;
            txtaddress.Text = string.Empty;
            ddldepartmentId.SelectedValue = "0";
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            txtlogin.Focus();
            txtlogin.Enabled = true;
        }

        //private void BindCheckBoxList()
        //{
        //    List<Group> lstgrp = new List<Group>();
        //    lstgrp = groupBL.ReadGroups();
        //    if (lstgrp.Count > 0)
        //    {
        //        chbgrp.DataSource = lstgrp;
        //        chbgrp.DataTextField = "Name";
        //        chbgrp.DataValueField = "Id";
        //        chbgrp.DataBind();
        //    }
        //}

        private void BindRadioButtonList()
        {
            List<Group> lstgrp = new List<Group>();
            lstgrp = groupBL.ReadGroups();
            if (lstgrp.Count > 0)
            {
               rdoGroup.DataSource = lstgrp;
               rdoGroup.DataTextField = "Name";
               rdoGroup.DataValueField = "Id";
               rdoGroup.DataBind();
            }
        }

        #endregion

        # region  privateProperities
        /// <summary>
        /// Gets or sets the User id.
        /// </summary>
        /// <value>The User id.</value>
        public int UserId
        {
            get
            {
                return (int)ViewState["UserId"];
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }
        #endregion


       
    }
}