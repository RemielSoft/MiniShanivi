using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DocumentObjectModel;
using System.Data.Common;
using System.Data;


namespace DataAccessLayer
{
    public class UserDL : BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public UserDL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        public Users ReadEmployeeByLoginID(String loginID)
        {

            Users user = null;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_USER_BY_LOGIN_ID);
            MyDataBase.AddInParameter(dbCommand, "in_userLoginId", DbType.String, loginID);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    user = GenerateEmployeeDetailFromDataReader(reader);

                }
            }

            return user;
        }

        public List<Group> ReadGroupByUserId(int userId)
        {
            List<Group> groups = new List<Group>();
            Group group = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_USER_GROUP);
            MyDataBase.AddInParameter(dbCommand, "@user_Id", DbType.Int32, userId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    group = new Group();
                    group.Id = GetIntegerFromDataReader(reader, "Group_Id");
                    group.Name = GetStringFromDataReader(reader, "name");
                    group.AuthorityLevel = new MetaData();
                    group.AuthorityLevel.Id = GetIntegerFromDataReader(reader, "authority_level_id");
                    group.AuthorityLevel.Name = GetStringFromDataReader(reader, "authority_level_name");
                    group.Description = GetStringFromDataReader(reader, "description");
                    group.RedirectUrl = GetStringFromDataReader(reader, "redirect_url");
                    group.CreatedBy = GetStringFromDataReader(reader, "created_by");
                    group.CreatedDate = GetDateFromReader(reader, "created_date");
                    groups.Add(group);
                }
            }
            return groups;
        }

        public int ValidateUser(String loginId, String password)
        {
            int userId;

            String sqlCommand = DBConstants.VALIDATE_USER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);

            MyDataBase.AddInParameter(dbCommand, "in_login_id", DbType.String, loginId);
            MyDataBase.AddInParameter(dbCommand, "in_password", DbType.String, password);
            MyDataBase.AddOutParameter(dbCommand, "@out_userId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_userId").ToString(), out userId);

            return userId;
        }

        /// <summary>
        /// Reads the user by id.
        /// </summary>
        /// <returns></returns>
        public Users ReadUserById(int userId)
        {
            
            Users user = new Users();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_USER_MASTER); 
           // DbCommand 
            MyDataBase.AddInParameter(dbCommand, "@userId", DbType.Int32, userId);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    user = GenerateEmployeeDetailFromDataReader(reader);
                    
                }
            }
            return user;
        }
        /// <summary>
        /// Method Used To Open Details In PopUp Controle
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Users> ReadUserDetailsById(int UserId)
        {
            List<Users> lstUser = new List<Users>();

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_USER_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@userId", DbType.Int32, UserId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Users User = new Users();
                    User = GenerateEmployeeDetailFromDataReader(reader);
                    lstUser.Add(User);
                }
            }
            return lstUser;
        }
        //public List<Group> ReadUserGroupsByUserId()
        //{
        //    List<Group> groups = new List<Group>();
        //    Group group = null;

        //    DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_USER_GROUP);
        //    using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            group = GenerateGroupFromDataReader(reader);
        //            groups.Add(group);
        //        }
        //    }
        //    return groups;
        //}
        public Users ReadLoginId(String Id)
        {
            Users users=new Users();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_LOGINID);
            MyDataBase.AddInParameter(dbCommand, "@Login_Id", DbType.String, Id);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    users.UserLoginId = GetStringFromDataReader(reader, "Login_Id");
                }
            }
            return users;
        }
        //public Group ReadGroupById(int UserId)
        //{
        //    Group group = null;
        //    DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_USER); 
        //   // DbCommand 
        //    MyDataBase.AddInParameter(dbCommand, "@user_id", DbType.Int32, UserId);

        //    using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            group = GenerateGroupDetailsFromDataReader(reader);
        //        }
        //    }
        //    return group;
        //}
            #region User CRUD Method
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="group">The user.</param>
        /// <returns></returns>

        public int CreateUser(Users user)
        {
            int UserId;
            String sqlCommand = DBConstants.CREATE_USER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@First_Name", DbType.String, user.FirstName);
            MyDataBase.AddInParameter(dbCommand, "@Last_Name", DbType.String, user.LastName);
            MyDataBase.AddInParameter(dbCommand, "@Middle_Name", DbType.String, user.MiddleName);
            MyDataBase.AddInParameter(dbCommand, "@Login_Id", DbType.String, user.UserLoginId);
            MyDataBase.AddInParameter(dbCommand, "@Password", DbType.String, user.Password);
            MyDataBase.AddInParameter(dbCommand, "@Email_Id", DbType.String, user.EmailId);
            MyDataBase.AddInParameter(dbCommand, "@Gender", DbType.String, user.Gender);
            MyDataBase.AddInParameter(dbCommand, "@Phone", DbType.String, user.Phone);
            MyDataBase.AddInParameter(dbCommand, "@Mobile", DbType.String, user.Mobile);
            if (user.DateOfBirth==DateTime.MinValue)
            {
                MyDataBase.AddInParameter(dbCommand, "@DOB", DbType.DateTime,DBNull.Value);
            }
            else
            {
                MyDataBase.AddInParameter(dbCommand, "@DOB", DbType.DateTime, user.DateOfBirth);
            }
            MyDataBase.AddInParameter(dbCommand, "@MaritalStatus", DbType.String, user.MaritalStatus);
            MyDataBase.AddInParameter(dbCommand, "@EmpCode", DbType.String, user.EmpCode);
            MyDataBase.AddInParameter(dbCommand, "@OfficeExtNo", DbType.String, user.OfficeExtensionNumber);
            MyDataBase.AddInParameter(dbCommand, "@Department_Id", DbType.String, user.Department.DepartmentId);
            MyDataBase.AddInParameter(dbCommand, "@Address", DbType.String, user.Address);
            MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, user.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_UserId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_UserId").ToString(), out UserId);
            return UserId;
        }

        public void CreateUserGroup(List<Group> lstgrp, int id)
        {
            foreach (Group item in lstgrp)
            {
                String sqlCommand = DBConstants.CREATE_USERGROUP;
                DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
                MyDataBase.AddInParameter(dbCommand, "@Group_Id", DbType.String, item.Id);
                MyDataBase.AddInParameter(dbCommand, "@User_Id", DbType.String, id);
                MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, item.CreatedBy);
                MyDataBase.ExecuteNonQuery(dbCommand);
                //int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Id").ToString(), out Id); 
            }
        }

        // Change Password

        public int UpdateUserPassword(Users user)
        {
            int loginId;
            String sqlCommand = DBConstants.UPDATE_USER_MASTER_CHANGE_PASSWORD;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            //MyDataBase.AddInParameter(dbCommand, "@User_Id", DbType.Int32, user.UserId);
            MyDataBase.AddInParameter(dbCommand, "@Login_Id", DbType.String, user.UserLoginId);
            MyDataBase.AddInParameter(dbCommand, "@Password", DbType.String, user.Password);
            MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, user.ModifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, user.ModifiedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_User_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_User_Id").ToString(), out loginId);
            return loginId;

        }
        
        public int UpdateUser(Users user)
        {
            int UserId;
            String sqlCommand = DBConstants.UPDATE_USER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@User_Id", DbType.Int32, user.UserId);
            MyDataBase.AddInParameter(dbCommand, "@First_Name", DbType.String, user.FirstName);
            MyDataBase.AddInParameter(dbCommand, "@Last_Name", DbType.String, user.LastName);
            MyDataBase.AddInParameter(dbCommand, "@Middle_Name", DbType.String, user.MiddleName);
            MyDataBase.AddInParameter(dbCommand, "@Login_Id", DbType.String, user.UserLoginId);
            MyDataBase.AddInParameter(dbCommand, "@Password", DbType.String, user.Password);
            MyDataBase.AddInParameter(dbCommand, "@Email_Id", DbType.String, user.EmailId);
            MyDataBase.AddInParameter(dbCommand, "@Gender", DbType.String, user.Gender);
            MyDataBase.AddInParameter(dbCommand, "@Phone", DbType.String, user.Phone);
            MyDataBase.AddInParameter(dbCommand, "@Mobile", DbType.String, user.Mobile);
            if (user.DateOfBirth == DateTime.MinValue)
            {
                MyDataBase.AddInParameter(dbCommand, "@DOB", DbType.DateTime, DBNull.Value);
            }
            else
            {
                MyDataBase.AddInParameter(dbCommand, "@DOB", DbType.DateTime, user.DateOfBirth);
            }
            MyDataBase.AddInParameter(dbCommand, "@MaritalStatus", DbType.String, user.MaritalStatus);
            MyDataBase.AddInParameter(dbCommand, "@EmpCode", DbType.String, user.EmpCode);
            MyDataBase.AddInParameter(dbCommand, "@Department_Id", DbType.Int32, user.Department.DepartmentId);
            MyDataBase.AddInParameter(dbCommand, "@OfficeExtNo", DbType.String, user.OfficeExtensionNumber);
            MyDataBase.AddInParameter(dbCommand, "@Address", DbType.String, user.Address);
            //TODO: Add other input parameters to update 
            MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, user.CreatedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, user.CreatedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_User_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_User_Id").ToString(), out UserId);
            return UserId;
        }

        public List<Users> ReadUser()
        {
            List<Users> users = new List<Users>();
            Users user = null;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_USER_MASTER);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    user = GenerateEmployeeDetailFromDataReader(reader);
                    users.Add(user);
                }
            }
            return users;
        }
       
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteUser(int userId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_USER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@User_Id", DbType.Int32, userId);
            MyDataBase.AddInParameter(dbCommand, "@modified_By", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@modified_Date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);
        }
        public string ValidateUser(int userId, string errorGroupUsedinEmployee)
        {
            String sqlCommand = DBConstants.VALIDATE_USER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@in_login_Id", DbType.Int32, userId);
            MyDataBase.AddInParameter(dbCommand, "@in_errorGroupUsedinEmployee", DbType.String, errorGroupUsedinEmployee);
            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 100);
            MyDataBase.ExecuteNonQuery(dbCommand);
            return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        }

        /// <summary>
        /// Check If given User Id is still Used.return the proper Error Message
        /// </summary>
        /// <param name="User Id"></param>
        /// <returns></returns>
        //public string ValidationUser(int userId, string errorGroupUsedinEmployee)
        //{
        //    String sqlCommand = DBConstants.VALIDATION_USER;
        //    DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
        //    MyDataBase.AddInParameter(dbCommand, "@in_user_Id", DbType.Int32, userId);
        //    MyDataBase.AddInParameter(dbCommand, "@in_errorGroupUsedinEmployee", DbType.String, errorGroupUsedinEmployee);
        //    MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 100);
        //    MyDataBase.ExecuteNonQuery(dbCommand);
        //    return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        //}
        #endregion


        #region Private Section

        private Users  GenerateEmployeeDetailFromDataReader(IDataReader reader)
        {
            
            Users user = new Users();
            user.UserId = GetIntegerFromDataReader(reader, "User_Id");
            user.FirstName = GetStringFromDataReader(reader, "First_Name");
            user.MiddleName = GetStringFromDataReader(reader, "Middle_Name");
            user.LastName = GetStringFromDataReader(reader, "Last_Name");
            user.UserLoginId = GetStringFromDataReader(reader, "Login_Id");
            user.Password = GetStringFromDataReader(reader, "Password");
            user.EmailId = GetStringFromDataReader(reader, "Email_Id");
            user.Phone = GetStringFromDataReader(reader, "Phone");
            user.Mobile = GetStringFromDataReader(reader, "Mobile");
            user.DateOfBirth = GetDateFromReader(reader, "DOB");
            user.MaritalStatus = GetStringFromDataReader(reader, "Marital_Status");
            user.Gender = GetStringFromDataReader(reader, "Gender");
            user.EmpCode = GetStringFromDataReader(reader, "EmpCode");
            user.Department = new Department();
            user.Department.DepartmentId = GetIntegerFromDataReader(reader, "Department_Id");
            user.Department.Name = GetStringFromDataReader(reader, "DepartmentName");
            //user.Department.DepartmentId = GetIntegerFromDataReader(reader, "Id");
            //user.Department.Name = GetStringFromDataReader(reader, "name");
            user.Address = GetStringFromDataReader(reader, "Address");
            user.OfficeExtensionNumber = GetStringFromDataReader(reader, "OfficeExtensionNo");
            user.GroupName = GetStringFromDataReader(reader, "GroupName");
            user.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            user.CreatedDate = GetDateFromReader(reader, "Created_Date");
            user.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            user.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return user;
        }

        private Group GenerateGroupDetailsFromDataReader(IDataReader reader)
        {
            Group grp = new Group();
            grp.Id = GetIntegerFromDataReader(reader, "Id");
            grp.Name = GetStringFromDataReader(reader, "name");
            grp.AuthorityLevel = new MetaData();
            grp.AuthorityLevel.Id = GetIntegerFromDataReader(reader, "authority_level_id");
            grp.CreatedBy = GetStringFromDataReader(reader, "created_by");
            grp.CreatedDate = GetDateFromReader(reader, "created_date");
            grp.Description = GetStringFromDataReader(reader, "description");
            return grp;
        }
        #endregion
    }
}
