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
   public class DepartmentDAL:BaseDAL
    {
       #region private global variable(s)

        private Database myDataBase;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public DepartmentDAL(Database dataBase)
        {
            myDataBase = dataBase;
        }

        #endregion

        #region Department CRUD Methods

        /// <summary>
        /// Creates the department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        public int CreateDepartment(Department department)
        {
            int departmentId;

            String sqlCommand = DBConstants.CREATE_DEPARTMENT_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "in_name", DbType.String, department.Name);
            myDataBase.AddInParameter(dbCommand, "in_description", DbType.String, department.Description);
            myDataBase.AddInParameter(dbCommand, "in_created_by", DbType.String, department.CreatedBy);
            
            myDataBase.AddOutParameter(dbCommand, "@out_departmentId", DbType.Int32, 10);

            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_departmentId").ToString(), out departmentId);

            return departmentId;
        }

        /// <summary>
        /// Updates the department.
        /// </summary>
        /// <param name="department">The department.</param>
        public int UpdateDepartment(Department department)
        {
            int departmentId;
            String sqlCommand = DBConstants.UPDATE_DEPARTMENT_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "@in_departmentId", DbType.Int32, department.DepartmentId);
            myDataBase.AddInParameter(dbCommand, "@Name", DbType.String, department.Name);
            myDataBase.AddInParameter(dbCommand, "@Description", DbType.String, department.Description);
            myDataBase.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, department.CreatedBy);
            myDataBase.AddOutParameter(dbCommand, "@out_departmentId", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_departmentId").ToString(), out departmentId);
            return departmentId;
        }

        /// <summary>
        /// Deletes the department.
        /// </summary>
        /// <param name="departmentId">The department id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteDepartment(int departmentId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_DEPARTMENT_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "in_departmentId", DbType.Int32, departmentId);
            myDataBase.AddInParameter(dbCommand, "in_modifiedBy", DbType.String, modifiedBy);
            
            myDataBase.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// Reads the departments.
        /// </summary>
        /// <returns></returns>
        public List<Department> ReadDepartments()
        {
            List<Department> departments = new List<Department>();
            Department department = null;

            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_DEPARTMENT_MASTER);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    department = GenerateDepartmentFromDataReader(reader);
                    departments.Add(department);
                }
            }
            return departments;
        }

        /// <summary>
        /// Reads the department by id.
        /// </summary>
        /// <returns></returns>
        public Department ReadDepartmentById(int locationId)
        {
            Department department = null;

            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_DEPARTMENT_MASTER);
            myDataBase.AddInParameter(dbCommand, "@in_department_id", DbType.Int32, locationId);

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    department = GenerateDepartmentFromDataReader(reader);
                }
            }
            return department;
        }

        public string ValidateDepartment(int DepartmentId)
        {

            String sqlCommand = DBConstants.VALIDATE_DEPARTMENT_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "@in_Department_Id", DbType.Int32, DepartmentId);

            myDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            myDataBase.ExecuteNonQuery(dbCommand);

            return Convert.ToString(myDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        }

        #endregion

        #region Private Section

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        private Department GenerateDepartmentFromDataReader(IDataReader reader)
        {
            Department department = new Department();
            department.DepartmentId = GetIntegerFromDataReader(reader, "Id");
            department.Name = GetStringFromDataReader(reader, "Name");
            department.Description = GetStringFromDataReader(reader, "description");
            
            department.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            department.CreatedDate = GetDateFromReader(reader, "Created_Date");
            department.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            department.ModifiedDate = GetDateFromReader(reader, "Modified_Date");


            return department;
        }
 
        #endregion
    }
}
