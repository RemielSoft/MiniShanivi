using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DocumentObjectModel;
using DataAccessLayer;
using System.Transactions;

namespace BusinessAccessLayer
{
    public class DeliveryScheduleBL : BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private DeliveryScheduleDL deliveryScheduleDL = null;

        List<DeliveryScheduleDOM> lstDeliverySchedule = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryScheduleDL"/> class.
        /// </summary>
        public DeliveryScheduleBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            deliveryScheduleDL = new DeliveryScheduleDL(myDataBase);
        }

        #endregion

        #region CRUD Methods

        public int CreateDeliverySchedule(DeliveryScheduleDOM ds, int quotationID)
        {
            int id = 0;
            try
            {
                id = deliveryScheduleDL.CreateDeliverySchedule(ds, quotationID);
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return id;
        }

        public List<DeliveryScheduleDOM> ReadDeliveryScheduleByQuotationID(int quotationID, Int16 quotationType)
        {
            lstDeliverySchedule = new List<DeliveryScheduleDOM>();
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstDeliverySchedule = deliveryScheduleDL.ReadDeliveryScheduleByQuotationID(quotationID, quotationType);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return lstDeliverySchedule;
        }

        public String DeleteDeliverySchedule(Int32 quotationId, Int16 quotationType, String modifiedBy)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        deliveryScheduleDL.DeleteDeliverySchedule(quotationId, quotationType, modifiedBy);
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

        public void ResetDeliverySchedule(Int32 quotationId, Int32 quotationType)
        {
            try
            {
                deliveryScheduleDL.ResetDeliverySchedule(quotationId, quotationType);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion
    }
}
