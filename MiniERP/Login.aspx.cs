using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DocumentObjectModel;
using System.Web.Security;
using MiniERP.Shared;

namespace MiniERP
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loginControl.Focus();
        }

        protected void loginControl_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (ValidateUser())
            {
                UserBL userBL = new UserBL();
                Users user = userBL.ReadUserByLoginID(loginControl.UserName);
                if (user == null)
                {
                    loginControl.FailureText = "User not Found..";
                    return;
                }
                FormsAuthentication.RedirectFromLoginPage(loginControl.UserName, loginControl.RememberMeSet);
                //String role = LoggedInUser.GetHighestLevelGroupId().Name;
                Response.Redirect("Home.aspx");

            }
            else
            {
                loginControl.FailureText = "User name or password incorrect. Please try again..";
            }
        }
       
        #region Private Sections

        private bool ValidateUser()
        {
            UserBL userBL = new UserBL();

            if (userBL.ValidateUser(loginControl.UserName, loginControl.Password) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        //protected void lnkChangePass_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Admin/ChangePassword.aspx");
        //}

       
    }
}