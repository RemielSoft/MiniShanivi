using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using DocumentObjectModel;

namespace DataAccessLayer
{
    public class PaymentTermDL : BaseDAL
    {
        #region Global Variables

        private Database myDataBase = null;
        DbCommand dbCommand = null;

        int paymentTermId = 0;
        PaymentTerm paymentTerm = null;
        List<PaymentTerm> lstPaymentTerm = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentTermDL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public PaymentTermDL(Database dataBase)
        {
            myDataBase = dataBase;
        }

        #endregion

        #region CURD Methods

        public int CreatePaymentTerm(PaymentTerm paymentTerm, int quotationID)
        {
            
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_PAYMENT_TERM);

            if (paymentTerm.PaymentTermId == 0)
                myDataBase.AddInParameter(dbCommand, "@in_Payment_Term_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "@in_Payment_Term_Id", DbType.Int32, paymentTerm.PaymentTermId);

            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationID);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int32, paymentTerm.QuotationType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Type_Id", DbType.Int32, paymentTerm.PaymentType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Number_Of_Days", DbType.Int32, paymentTerm.NumberOfDays);
            myDataBase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, paymentTerm.CreatedBy);
            myDataBase.AddInParameter(dbCommand, "@in_percentage_value", DbType.Decimal, paymentTerm.PercentageValue);
            myDataBase.AddInParameter(dbCommand, "@in_payment_description", DbType.String, paymentTerm.PaymentDescription);
            myDataBase.AddOutParameter(dbCommand, "@out_Payment_Term_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Payment_Term_Id").ToString(), out paymentTermId);
            return paymentTermId;
        }

        public List<PaymentTerm> ReadPaymentTermByPurchaseOI(int quotationID, Int16 quotationType)
        {
            lstPaymentTerm = new List<PaymentTerm>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_PAYMENT_TERM);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationID);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int16, quotationType); ;
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    paymentTerm = GeneratePaymentTermFromDataReader(reader);
                    lstPaymentTerm.Add(paymentTerm);
                }
            }

            return lstPaymentTerm;
        }

        public List<PaymentTerm> ReadPaymentTermMeta()
        {
            lstPaymentTerm = new List<PaymentTerm>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_PAYMENT_TERM_META);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    paymentTerm = GeneratePaymentTermMetaFromDataReader(reader);
                    lstPaymentTerm.Add(paymentTerm);
                }
            }

            return lstPaymentTerm;
        }

        public void DeletePaymentTerm(Int32 quotationId, Int32 quotationType, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_PAYMENT_TERM);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Term_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int16, quotationType);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public void ResetPaymentTerm(Int32 quotationId, Int32 quotationType)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.RESET_PAYMENT_TERM);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int32, quotationType);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region Private Section

        private PaymentTerm GeneratePaymentTermFromDataReader(IDataReader reader)
        {
            paymentTerm = new PaymentTerm();

            paymentTerm.PaymentType = new MetaData();
            paymentTerm.PaymentTermId = GetIntegerFromDataReader(reader, "Payment_Term_Id");
            paymentTerm.PaymentType.Id = GetIntegerFromDataReader(reader, "Payment_Type_Id");
            paymentTerm.PaymentType.Name = GetStringFromDataReader(reader, "PaymentTypeName");

            paymentTerm.QuotationType = new MetaData();
            paymentTerm.QuotationType.Id = GetIntegerFromDataReader(reader, "Quotation_Id");
            paymentTerm.QuotationType.Name = GetStringFromDataReader(reader, "QuotationTypeName");
            paymentTerm.NumberOfDays = GetIntegerFromDataReader(reader, "Number_Of_Days");

            paymentTerm.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            paymentTerm.CreatedDate = GetDateFromReader(reader, "Created_Date");
            paymentTerm.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            paymentTerm.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            paymentTerm.PercentageValue = GetDecimalFromDataReader(reader, "percentage_value");
            paymentTerm.PaymentDescription = GetStringFromDataReader(reader, "payment_description");

            return paymentTerm;
        }

        private PaymentTerm GeneratePaymentTermMetaFromDataReader(IDataReader reader)
        {
            paymentTerm = new PaymentTerm();
            paymentTerm.PaymentType = new MetaData();
            paymentTerm.PaymentType.Id = GetIntegerFromDataReader(reader, "Id");
            paymentTerm.PaymentType.Name = GetStringFromDataReader(reader, "Name");

            return paymentTerm;
        }
        #endregion
    }
}
