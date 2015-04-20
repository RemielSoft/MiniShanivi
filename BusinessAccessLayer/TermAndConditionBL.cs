using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using DocumentObjectModel;
using DataAccessLayer;
using System.Transactions;

namespace BusinessAccessLayer
{
    public class TermAndConditionBL:BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private TermAndConditionDAL termAndConditionDAL = null;

        int id = 0;
        List<TermAndCondition> lstTermAndCondition = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public TermAndConditionBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            termAndConditionDAL = new TermAndConditionDAL(myDataBase);
        }

        #endregion

        #region Department CRUD Methods

        /// <summary>
        /// Creates the Term
        /// </summary>
        /// <param name="termAndCondition"></param>
        /// <returns></returns>
        public int CreateTermsAndConditions(TermAndCondition termAndCondition)
        {
            try
            {
                id = termAndConditionDAL.CreateTermsAndConditions(termAndCondition);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return id;
        }

        /// <summary>
        /// Creates the Term and Condition for Particular Quotation
        /// </summary>
        /// <param name="termAndCondition"></param>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        public int CreateTermAndCondition(TermAndCondition termAndCondition, int quotationID)
        {
            try
            {
                id = termAndConditionDAL.CreateTermAndCondition(termAndCondition, quotationID);
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return id;
        }

        /// <summary>
        /// Update the Term
        /// </summary>
        /// <param name="termAndCondition"></param>
        /// <returns></returns>
        public int UpdateTermsAndConditions(TermAndCondition termAndCondition)
        {
            try
            {
                id = termAndConditionDAL.UpdateTermsAndConditions(termAndCondition);
            }
            catch (Exception exp)
            {
                throw exp;
            }


            return id;
        }

        /// <summary>
        /// Delete the Term
        /// </summary>
        /// <param name="termsId"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="modifiedOn"></param>
        /// <returns></returns>
        public string DeleteTermsAndConditions(int termsId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    //errorMessage = termAndConditionDAL.ValidateTermsAndConditions(departmentId);
                    if (errorMessage == "")
                    {
                        termAndConditionDAL.DeleteTermsAndConditions(termsId, modifiedBy, modifiedOn);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return errorMessage;
        }

        public String DeleteTermAndCondition(Int32 quotationId, Int16 quotationType, String modifiedBy)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        termAndConditionDAL.DeleteTermAndCondition(quotationId, quotationType, modifiedBy);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                
                throw exp;
            }
            return errorMessage;
        }

        /// <summary>
        /// Reads the Terms
        /// </summary>
        /// <returns></returns>
        public List<TermAndCondition> ReadTermsAndConditions(int? termsId, Int32? termConditionType)
        {
            lstTermAndCondition = new List<TermAndCondition>();
            try
            {
                lstTermAndCondition = termAndConditionDAL.ReadTermsAndConditions(termsId, termConditionType);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstTermAndCondition;
        }

        /// <summary>
        /// Read Term and Condition for Particular Quotation
        /// </summary>
        /// <param name="quotationID"></param>
        /// <param name="quotationType"></param>
        /// <returns></returns>
        public List<TermAndCondition> ReadTermAndConditionByQuotationID(int quotationID, Int16 quotationType)
        {
            lstTermAndCondition = new List<TermAndCondition>();
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstTermAndCondition = termAndConditionDAL.ReadTermAndConditionByQuotationID(quotationID, quotationType);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return lstTermAndCondition;
        }

        public void ResetTermAndConditions(int quotationID, Int32 quotationType)
        {
            try
            {
                termAndConditionDAL.ResetTermAndConditions(quotationID, quotationType);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        #endregion
    }
}
