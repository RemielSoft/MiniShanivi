using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using DocumentObjectModel;
using System.Data;




namespace DataAccessLayer
{
    public class GroupDAL:BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public GroupDAL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        #region Group CRUD Methods

        /// <summary>
        /// Creates the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        public int CreateGroup(Group group)
        {
            int groupId;

            String sqlCommand = DBConstants.CREATE_GROUP_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Name", DbType.String, group.Name);
            MyDataBase.AddInParameter(dbCommand, "@Description", DbType.String, group.Description);
            MyDataBase.AddInParameter(dbCommand, "@Authority_Level", DbType.Int32, group.AuthorityLevel.Id);          
            MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, group.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_groupId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_groupId").ToString(), out groupId);

            return groupId;
        }

        /// <summary>
        /// Updates the group.
        /// </summary>
        /// <param name="group">The group.</param>
        public int UpdateGroup(Group group)
        {
            int groupId;
            String sqlCommand = DBConstants.UPDATE_GROUP_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Id", DbType.Int32, group.Id);
            MyDataBase.AddInParameter(dbCommand, "@Name", DbType.String, group.Name);
            MyDataBase.AddInParameter(dbCommand, "@Description", DbType.String, group.Description);
            //TODO: Add other input parameters to update 
            MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, group.CreatedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, group.CreatedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_groupId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_groupId").ToString(), out groupId);
            return groupId;
        }

        /// <summary>
        /// Deletes the group.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteGroup(int groupId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_GROUP_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Group_Id", DbType.Int32, groupId);
            MyDataBase.AddInParameter(dbCommand, "@modified_By", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@modified_Date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Reads the groups.
        /// </summary>
        /// <returns></returns>
        public List<Group> ReadGroups()
        {
            List<Group> groups = new List<Group>();
            Group group = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_GROUP_MASTER);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    group = GenerateGroupFromDataReader(reader);
                    groups.Add(group);
                }
            }
            return groups;
        }

        /// <summary>
        /// Reads the group by id.
        /// </summary>
        /// <returns></returns>
        public Group ReadGroupById(int groupId)
        {
            Group group = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_GROUP_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@groupId", DbType.Int32, groupId);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    group = GenerateGroupFromDataReader(reader);
                }
            }
            return group;
        }

        
        public string ValidateGroup(int groupId, string errorGroupUsedinEmployee)
        {

            String sqlCommand = DBConstants.VALIDATE_GROUP_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);

            MyDataBase.AddInParameter(dbCommand, "@in_Group_Id", DbType.Int32, groupId);
            MyDataBase.AddInParameter(dbCommand, "@in_errorGroupUsedinEmployee", DbType.String, errorGroupUsedinEmployee);
            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 100);
            MyDataBase.ExecuteNonQuery(dbCommand);

            return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        }
        #endregion

        #region Private Section

        /// <summary>
        /// Generates the group from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private Group GenerateGroupFromDataReader(IDataReader reader)
        {
            Group group = new Group();
            group.AuthorityLevel = new MetaData();
            group.Id = GetIntegerFromDataReader(reader, "Id");
            group.Name = GetStringFromDataReader(reader, "Name");
            group.Description = GetStringFromDataReader(reader, "Description");
            group.AuthorityLevel.Id = GetIntegerFromDataReader(reader, "Authority_level_Id");
            group.RedirectUrl = GetStringFromDataReader(reader, "Redirect_Url");
            return group;
        }

        #endregion

    }
}
