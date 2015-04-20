using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using DocumentObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Transactions;

namespace BusinessAccessLayer
{
    public class PaymentBL:BaseBL
    {
        #region Global Variable(s)

        private Database myDatabase = null;
        PaymentDAL paymentDL = null;

        List<PaymentDOM> lstPayment = null;

        Int32 id = 0;

        #endregion

        #region Constructor(s)

        public PaymentBL()
        {
            myDatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            paymentDL = new PaymentDAL(myDatabase);
        }

        #endregion

        #region CURD

         public Int32 CreatePayment(PaymentDOM payment)
        {
            try
            {
                using(TransactionScope scope=new TransactionScope(TransactionScopeOption.Required,base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = paymentDL.CreatePayment(payment);
                    scope.Complete();
                }

            }
            catch (Exception exp)
            {
                
                throw exp;
            }
            return id;
         }

         public Int32 UpdatePayment(PaymentDOM payment, Int32  paymentId)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                 {
                     id = paymentDL.UpdatePayment(payment,paymentId);
                     scope.Complete();
                 }
             }
             catch (Exception exp)
             {

                 throw exp;
             }
             return id;
         }

         public Int32 UpdatePaymentStatus(Int32 paymentId,Int16 invoiceType ,Int32 statusId, String approvedRegectedBy ,String RemarkReject )
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                 {
                     id = paymentDL.UpdatePaymentStatus(paymentId,invoiceType, statusId, approvedRegectedBy,RemarkReject);
                     scope.Complete();
                 }
             }
             catch (Exception exp)
             {

                 throw exp;
             }
             return id;
         }

         public List<PaymentDOM> ReadPayment(Int32? paymentId)
         {
             lstPayment = new List<PaymentDOM>();
             try
             {
                 using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                 {
                     lstPayment = paymentDL.ReadPayment(paymentId);
                     scope.Complete();
                 }
             }
             catch (Exception exp)
             {

                 throw exp;
             }
             return lstPayment;
         }

        //payment case 
         public List<PaymentDOM> ReadPayment(DateTime fromDate, DateTime toDate, Int32? invoiceType, String InvoiceNumber, string contractorName)
         {
             lstPayment = new List<PaymentDOM>();
             try
             {

                 lstPayment = paymentDL.ReadPayment(fromDate, toDate, invoiceType, InvoiceNumber, contractorName);
                 
               
             }
             catch (Exception exp)
             {

                 throw exp;
             }
             return lstPayment;
         }

         public List<PaymentDOM> ReadPayment(String CSName, DateTime fromDate, DateTime toDate, String contractNo, String invoiceNo, Int16 invoiceType)
         {
             lstPayment = new List<PaymentDOM>();
             try
             {
                

                     lstPayment = paymentDL.ReadPayment(CSName,fromDate,toDate,contractNo,invoiceNo,invoiceType);
                     
                
             }
             catch (Exception exp)
             {

                 throw exp;
             }
             return lstPayment;
         }

         public List<PaymentDOM> ReadPaymentByStatusId(Int32? statusId, Int32? invoiceType)
         {
             lstPayment = new List<PaymentDOM>();
             try
             {
                 using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                 {
                     
                     lstPayment = paymentDL.ReadPaymentByStatusId(statusId, invoiceType);
                     scope.Complete();
                 }
                 
             }
             catch (Exception exp)
             {

                 throw exp;
             }
             return lstPayment;
         }

         public String DeletePayment(Int32 paymentId, Int16 invoiceType, String modifiedBy, String RemarkReject)
         {
             String msg = string.Empty;
             try
             {
                 using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                 {
                     paymentDL.DeletePayment(paymentId,invoiceType, modifiedBy,RemarkReject);
                     scope.Complete();
                 }
             }
             catch (Exception exp)
             {

                 throw exp;
             }
             return msg;
         }

        #endregion

    }
}
