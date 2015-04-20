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
    public class ProjectBAL:BaseBL
    {
         #region private global variable(s)

        private Database myDataBase;
        private ProjectDAL projectDAL = null;

        #endregion

         #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public ProjectBAL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            projectDAL = new ProjectDAL(myDataBase);
        }

        #endregion

        #region Project CRUD Methods

        /// <summary>
        /// Creates the Project.
        /// </summary>
        /// <param name="project">The Project.</param>
        /// <returns></returns>
        public int CreateProject(Project project)
        {
            int projectId = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    projectId = projectDAL.CreateProject(project);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return projectId;
        }

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="group">The project.</param>
        public int UpdateProject(Project project)
        {
            int IsSuccesfull = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    IsSuccesfull = projectDAL.UpdateProject(project);

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
        /// Deletes the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public string DeleteProject(int projectId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    //errorMessage = projectDAL.ValidateGroup(projectId, GlobalConstants.C_ERROR_MESSAGE_GROUP_USEDIN_User);

                    if (errorMessage == "")
                    {
                        projectDAL.DeleteProject(projectId, modifiedBy, modifiedOn);
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
        /// Reads the Project.
        /// </summary>
        /// <returns></returns>
        public List<Project> ReadProject()
        {
            List<Project> lst = new List<Project>();
            try
            {
                lst = projectDAL.ReadProject();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return lst;
        }

        /// <summary>
        /// Reads the Project by id.
        /// </summary>
        /// <param name="groupId">The Project id.</param>
        /// <returns></returns>
        public Project ReadProjectById(int projectId)
        {
            Project grp = new Project();
            try
            {
                grp = projectDAL.ReadProjectById(projectId);
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
