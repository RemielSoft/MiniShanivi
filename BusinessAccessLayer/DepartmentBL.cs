using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataAccessLayer;
using DocumentObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Transactions;

namespace BusinessAccessLayer
{
    public class DepartmentBL:BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private DepartmentDAL departmentDAL = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public DepartmentBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            departmentDAL = new DepartmentDAL(myDataBase);
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
            int id = 0;
            try
            {
                id = departmentDAL.CreateDepartment(department);
            }
            catch (Exception exp)
            {

                Logger.Write(exp.Message);

            }
            return id;
        }

        /// <summary>
        /// Updates the department.
        /// </summary>
        /// <param name="department">The department.</param>
        public int UpdateDepartment(Department department)
        {
            int id = 0;
            try
            {
                id = departmentDAL.UpdateDepartment(department);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }


            return id;
        }

        /// <summary>
        /// Deletes the department.
        /// </summary>
        /// <param name="locationId">The department id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public string DeleteDepartment(int departmentId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    errorMessage = departmentDAL.ValidateDepartment(departmentId);
                    if (errorMessage == "")
                    {
                        departmentDAL.DeleteDepartment(departmentId, modifiedBy, modifiedOn);
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
        /// Reads the locations.
        /// </summary>
        /// <returns></returns>
        public List<Department> ReadDepartments()
        {
            List<Department> listDept = new List<Department>();
            try
            {
                listDept = departmentDAL.ReadDepartments();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return listDept;
        }
        
        /// <summary>
        /// Reads the department by id.
        /// </summary>
        /// <param name="locationId">The department id.</param>
        /// <returns></returns>
        public Department ReadDepartmentById(int departmentId)
        {
            Department dept = new Department();
            try
            {
                dept = departmentDAL.ReadDepartmentById(departmentId);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return dept;
        }

        #endregion
    }
}
