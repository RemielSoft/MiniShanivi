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
    public class ContractorBL:BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private ContractorDL contractorDL = null;

        #endregion
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public ContractorBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            contractorDL = new ContractorDL(myDataBase);
        }

        #endregion
        #region Group CRUD Methods

        /// <summary>
        /// Creates the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        public int CreateContractor(Contractor contractor)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = contractorDL.CreateContractor(contractor);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return id;
        }
        public int UpdateContractor(Contractor contractor)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = contractorDL.UpdateContractor(contractor);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return id;
        }
        public List<Contractor> ReadContractor(Int32? contractorid)
        {
            List<Contractor> lstContractor = new List<Contractor>();
            try
            {
                lstContractor=contractorDL.ReadContractor(contractorid);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
           return lstContractor;
        }
        public string DeleteContractor(int contractorid, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    errorMessage =contractorDL.ValidateContractor(contractorid);

                    if (errorMessage == "")
                    {
                        contractorDL.DeleteContractor(contractorid, modifiedBy, modifiedOn);
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
