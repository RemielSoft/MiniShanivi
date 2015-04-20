using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataAccessLayer;
using DocumentObjectModel;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace BusinessAccessLayer
{
   public class UserBL:BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private UserDL userDL = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public UserBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            userDL = new UserDL(myDataBase);
        }

        #endregion
        #region User CRUD Method
        /// <summary>
        /// Creates the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>

        public Users ReadLoginId(String Id)
        {
            Users users = new Users();
            try
            {
                users = userDL.ReadLoginId(Id);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return users;
        }
        public int CreateUser(Users user)
        {
            int UserId = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    UserId = userDL.CreateUser(user);
                    if (UserId>0)
                    {
                        userDL.CreateUserGroup(user.Groups, UserId);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return UserId;
        }

       ///<summary>
       ///update the Password
       /// </summary>
       /// <param name="User">The User</param>

        public int UpdateChangePassword(Users user)
        {
            int Id = 0;
            try
            {
                using (TransactionScope scop=new TransactionScope(TransactionScopeOption.Required,base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    Id = userDL.UpdateUserPassword(user);
                    scop.Complete();
                }

            }
            catch (Exception exp)
            {

                Logger.Write(exp.Message);
            }
            return Id;
        }
        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="group">The user.</param>
        public int UpdateUser(Users User)
        {
            int IsSuccesfull = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    IsSuccesfull = userDL.UpdateUser(User);
                    if (IsSuccesfull>0)
                    {
                        userDL.CreateUserGroup(User.Groups, User.UserId); 
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return IsSuccesfull;
        }
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="UserId">The user id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public string DeleteUser(int UserId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    //errorMessage = userDL.ValidateUser(UserId, GlobalConstants.C_ERROR_MESSAGE_GROUP_USEDIN_User);
                    //errorMessage = userDL.ValidationUser(UserId, GlobalConstants.C_ERROR_MESSAGE_GROUP_USEDIN_User);
                    if (errorMessage == "")
                    {
                        userDL.DeleteUser(UserId, modifiedBy, modifiedOn);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return errorMessage;
        }
        /// <summary>
        /// Reads the User.
        /// </summary>
        /// <returns></returns>
        public List<Users> ReadUser()
        {
            List<Users> lst = new List<Users>();
            try
            {
                lst = userDL.ReadUser();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);                
            }
            return lst;
        }
        /// <summary>
        /// Reads the user by id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public Users ReadUserById(int userId)
        {
            Users user = new Users();
            try
            {
                user = userDL.ReadUserById(userId);
                user.Groups = new List<Group>();
                foreach (Group item in userDL.ReadGroupByUserId(userId))
                {
                    user.Groups.Add(item); 
                }              
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return user;
        } 
        /// <summary>
        /// PopUp details 
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public List<Users> ReadUserDetails(int UserId)
        {
            List<Users> lstUser = new List<Users>();
            try
            {
                lstUser = userDL.ReadUserDetailsById(UserId); //supplierDAL.ReadSupplier(supplierid);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstUser;
        }
        #endregion

        public Users ReadUserByLoginID(String loginId)
        {   
            Users user = userDL.ReadEmployeeByLoginID(loginId);
            try
            {
                user.Groups = userDL.ReadGroupByUserId(user.UserId);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return user;
        }
        public List<Group> ReadGroupByUserId(int userId)
        {
            List<Group> lstGroup = new List<Group>();
            try
            {
                lstGroup = userDL.ReadGroupByUserId(userId);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstGroup;
        }
        public int ValidateUser(String loginName, String password)
        {
            Int32 userId = 0;

            try
            {
                userId = userDL.ValidateUser(loginName, password);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return userId;
        }
    }
}
