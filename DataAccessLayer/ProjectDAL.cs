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
    public class ProjectDAL:BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public ProjectDAL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        #region Project CRUD Methods

        /// <summary>
        /// Creates the Project.
        /// </summary>
        /// <param name="Project">The Project.</param>
        /// <returns></returns>
        public int CreateProject(Project project)
        {
            int projectId;

            String sqlCommand = DBConstants.CREATE_PROJECT_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Project_Name", DbType.String, project.Name);
            MyDataBase.AddInParameter(dbCommand, "@City", DbType.String, project.City);
            MyDataBase.AddInParameter(dbCommand, "@State", DbType.String, project.State);
            MyDataBase.AddInParameter(dbCommand, "@Address", DbType.String, project.Address);
            MyDataBase.AddInParameter(dbCommand, "@Description", DbType.String, project.Description);
            MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, project.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_ProjectId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_ProjectId").ToString(), out projectId);

            return projectId;
        }

        /// <summary>
        /// Updates the Project.
        /// </summary>
        /// <param name="Project">The Project.</param>
        public int UpdateProject(Project project)
        {
            int projectId;
            String sqlCommand = DBConstants.UPDATE_PROJECT_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Project_Id", DbType.Int32, project.ProjectId);
            MyDataBase.AddInParameter(dbCommand, "@Project_Name", DbType.String, project.Name);
            MyDataBase.AddInParameter(dbCommand, "@City", DbType.String, project.City);
            MyDataBase.AddInParameter(dbCommand, "@State", DbType.String, project.State);
            MyDataBase.AddInParameter(dbCommand, "@Address", DbType.String, project.Address);
            MyDataBase.AddInParameter(dbCommand, "@Description", DbType.String, project.Description);
            //TODO: Add other input parameters to update 
            MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, project.CreatedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, project.CreatedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_Project_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Project_Id").ToString(), out projectId);
            return projectId;
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteProject(int projectId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_PROJECT_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Project_Id", DbType.Int32, projectId);
            MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Reads the project.
        /// </summary>
        /// <returns></returns>
        public List<Project> ReadProject()
        {
            List<Project> projects = new List<Project>();
            Project project = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_PROJECT_MASTER);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    project = GenerateProjectFromDataReader(reader);
                    projects.Add(project);
                }
            }
            return projects;
        }

        /// <summary>
        /// Reads the group by id.
        /// </summary>
        /// <returns></returns>
        public Project ReadProjectById(int projectId)
        {
            Project project = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_PROJECT_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Project_Id", DbType.Int32, projectId);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    project = GenerateProjectFromDataReader(reader);
                }
            }
            return project;
        }

        public String ValidateProject(int projectid)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.VALIDATE_PROJECT_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@in_project_id", DbType.Int32, projectid);
            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            MyDataBase.ExecuteNonQuery(dbCommand);
            return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));

        }

        #endregion

        #region Private Section

        /// <summary>
        /// Generates the project from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private Project GenerateProjectFromDataReader(IDataReader reader)
        {
            Project project = new Project();
            project.ProjectId = GetIntegerFromDataReader(reader, "Project_Id");
            project.Name = GetStringFromDataReader(reader, "Project_Name");
            project.City = GetStringFromDataReader(reader, "City");
            project.State = GetStringFromDataReader(reader, "State");
            project.Address = GetStringFromDataReader(reader, "Address");
            project.Description = GetStringFromDataReader(reader, "Description");
            return project;
        }

        #endregion
    }
}
