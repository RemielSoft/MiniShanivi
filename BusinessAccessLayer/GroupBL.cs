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
    public class GroupBL:BaseBL
    {
         #region private global variable(s)

        private Database myDataBase;
        private GroupDAL groupDAL = null;

        #endregion

         #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public GroupBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            groupDAL = new GroupDAL(myDataBase);
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
            int groupId = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    groupId = groupDAL.CreateGroup(group);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return groupId;
        }

        /// <summary>
        /// Updates the group.
        /// </summary>
        /// <param name="group">The group.</param>
        public int UpdateGroup(Group group)
        {
            int IsSuccesfull = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    IsSuccesfull = groupDAL.UpdateGroup(group);

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
        /// Deletes the group.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public string DeleteGroup(int groupId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    errorMessage = groupDAL.ValidateGroup(groupId, GlobalConstants.C_ERROR_MESSAGE_GROUP_USEDIN_User);

                    if (errorMessage == "")
                    {
                        groupDAL.DeleteGroup(groupId, modifiedBy, modifiedOn);
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
        /// Reads the groups.
        /// </summary>
        /// <returns></returns>
        public List<Group> ReadGroups()
        {
            List<Group> lst = new List<Group>();
            try
            {
                lst = groupDAL.ReadGroups();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return lst;
        }

        /// <summary>
        /// Reads the group by id.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <returns></returns>
        public Group ReadGroupById(int groupId)
        {
            Group grp = new Group();
            try
            {
                grp = groupDAL.ReadGroupById(groupId);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return grp;
        }

        
        #endregion

    }
}
