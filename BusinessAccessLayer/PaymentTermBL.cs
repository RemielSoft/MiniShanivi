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
    public class PaymentTermBL : BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private PaymentTermDL paymentTermDAL = null;
        int id = 0;

        List<PaymentTerm> lstPaymentTerm = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentTermDL"/> class.
        /// </summary>
        public PaymentTermBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            paymentTermDAL = new PaymentTermDL(myDataBase);
        }

        #endregion

        #region Project CRUD Methods

        /// <summary>
        /// Creates PaymentTerm of Quotaion
        /// </summary>
        /// <param name="paymentTerm"></param>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        public int CreatePaymentTerm(PaymentTerm paymentTerm, int quotationID)
        {
            try
            {
                id = paymentTermDAL.CreatePaymentTerm(paymentTerm, quotationID);
            }
            catch (Exception exp)
            {
                
                throw exp;
            }
            return id;
        }

        public List<PaymentTerm> ReadPaymentTermByPurchaseOI(int quotationID, Int16 quotationType)
        {
            lstPaymentTerm = new List<PaymentTerm>();
            try
            {

                    lstPaymentTerm = paymentTermDAL.ReadPaymentTermByPurchaseOI(quotationID,quotationType);                 
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return lstPaymentTerm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PaymentTerm> ReadPaymentTermMeta()
        {
            lstPaymentTerm = new List<PaymentTerm>();
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstPaymentTerm = paymentTermDAL.ReadPaymentTermMeta();
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return lstPaymentTerm;
        }

        public String DeletePaymentTerm(Int32 quotationId, Int16 quotationType, String modifiedBy)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        paymentTermDAL.DeletePaymentTerm(quotationId, quotationType, modifiedBy);
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

        public void ResetPaymentTerm(Int32 quotationId, Int32 quotationType)
        {
            try
            {
                paymentTermDAL.ResetPaymentTerm(quotationId, quotationType);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion
    }
}
