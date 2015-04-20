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
    public partial class ChangePassword : BasePage
    {
        #region Global Varriable....

        GroupBL groupBL = new GroupBL();
        DepartmentBL departmentBL = new DepartmentBL();
        UserBL userBL = new UserBL();
        Users users = new Users();
        Boolean flag = false;

        #endregion

        #region Protected......

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtOldPassword.Attributes["Type"] = "Password";
                lnkBtn.Visible = false;
                lnkBtn1.Visible = false; 
                SetValidationExp();
                txtLoginId.Text = LoggedInUser.UserLoginId;
                txtLoginId.Enabled = false;
                txtOldPassword.Focus();
               
            }
           
            
        }


        // Password Update case
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int IsSuccesfull;
            List<Users> lstuser = userBL.ReadUser();
            foreach (Users item in lstuser)
            {
                

                if (item.UserLoginId == txtLoginId.Text && item.Password == txtOldPassword.Text)
                {
                    Users User = GetUserDetail();
                    User.UserLoginId = txtLoginId.Text;
                    
                    IsSuccesfull = userBL.UpdateChangePassword(User);
                    if (IsSuccesfull > 0)
                    {
                        ShowMessageWithUpdatePanel(GlobalConstants.C_UPDATE_USER_PASSWORD);
                        Clear();
                    }
                    
                    
                }

            }

            ShowMessageWithUpdatePanel("Please Enter Valid Old Password");
                  

        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            Clear();
        }

        #endregion

        #region Private Method....

        private void SetValidationExp()
        {

            revNewPassword.ValidationExpression = ValidationExpression.C_PASSWORD;
            revNewPassword.ErrorMessage = "Password Must Contain At Least 1 Alphabate, 1 Digit, No Special Characters And Length Must Be 5-20";

        }

        private Users GetUserDetail()
        {
            Users user = new Users();
            user.UserLoginId = txtLoginId.Text.Trim();
            user.Password = txtNewPassword.Text.Trim();
            user.ModifiedBy = LoggedInUser.UserLoginId; //"Admin"; //LoggedInEmployee.UserLoginId;
            user.ModifiedDate = DateTime.Now;
            return user;
        }

        private void Clear()
        {
            txtOldPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmNewPassword.Text = string.Empty;
            txtLoginId.Text = string.Empty;
            lnkBtn.Visible = false;
            lnkBtn1.Visible = false;
        }


        #endregion

        # region  privateProperities
        /// <summary>
        /// Gets or sets the User id.
        /// </summary>
        /// <value>The User id.</value>
        public int UserLoginId
        {
            get
            {
                return (int)ViewState["UserLoginId"];
            }
            set
            {
                ViewState["UserLoginId"] = value;
            }
        }
        #endregion

        protected void txtOldPassword_TextChanged(object sender, EventArgs e)
        {
            List<Users> lstuser = userBL.ReadUser();
            foreach (Users item in lstuser)
            {
                if (item.Password == txtOldPassword.Text)
                {
                    
                    //txtOldPassword.TextMode = TextBoxMode.Password;

                    lnkBtn.Visible = true;
                    lnkBtn1.Visible = false;

                    break;
                }
                else
                {
                    lnkBtn1.Visible = true;
                    lnkBtn.Visible = false;
                   
                }

            }
       
        }




    }
}