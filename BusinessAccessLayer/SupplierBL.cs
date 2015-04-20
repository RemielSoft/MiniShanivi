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
    public class SupplierBL:BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private SupplierDAL supplierDAL = null;

        #endregion
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public SupplierBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            supplierDAL = new SupplierDAL(myDataBase);
        }

        #endregion
        #region Group CRUD Methods

        /// <summary>
        /// Creates the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        public int CreateSupplier(Supplier supplier)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = supplierDAL.CreateSupplier(supplier);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return id;
        }
        public int UpdateSupplier(Supplier supplier)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = supplierDAL.UpdateSupplier(supplier);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return id;
        }
        public List<Supplier> ReadSupplier(Int32? supplierid)
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            try
            {
                lstSupplier = supplierDAL.ReadSupplier(supplierid);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstSupplier;
        }
        public string DeleteSupplier(int supplierid, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    errorMessage = supplierDAL.ValidateSupplier(supplierid);

                    if (errorMessage == "")
                    {
                        supplierDAL.DeleteSupplier(supplierid, modifiedBy, modifiedOn);
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
        
        #endregion
    }
}
