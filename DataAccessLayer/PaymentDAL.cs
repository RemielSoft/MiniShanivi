using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace DataAccessLayer
{
    public class PaymentDAL : BaseDAL
    {
        #region Global Variable(s)

        private Database myDataBase = null;
        private DbCommand dbCommand = null;

        List<PaymentDOM> lstPayment = null;
        PaymentDOM payment = null;

        Int32 id = 0;

        #endregion

        #region Constructor(s)

        public PaymentDAL(Database dataBase)
        {
            myDataBase = dataBase;
        }

        #endregion

        #region CURD

        public Int32 CreatePayment(PaymentDOM payment)
        {

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_PAYMENT);
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Number", DbType.String, payment.InvoiceNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Date", DbType.DateTime, payment.InvoiceDate);
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Remarks", DbType.String, payment.InvoiceRemarks);

            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Number", DbType.String, payment.QuotationNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Id", DbType.Int32, payment.ContractorSupplierId);
            myDataBase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, payment.ContractorSupplierName);
            myDataBase.AddInParameter(dbCommand, "@in_Contract_Number", DbType.String, payment.ContractNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Work_Order_Number", DbType.String, payment.WorkOrderNumber);

            myDataBase.AddInParameter(dbCommand, "@in_Payment_Mode_Id", DbType.Int32, payment.PaymentModeType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Amount", DbType.Decimal, payment.PaymentAmount);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Date", DbType.DateTime, payment.PaymentDate);
            if (payment.UploadedDocument == 0)
                myDataBase.AddInParameter(dbCommand, "@in_Uploaded_Document", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "@in_Uploaded_Document", DbType.Int32, payment.UploadedDocument);

            myDataBase.AddInParameter(dbCommand, "@in_Remark", DbType.String, payment.Remark);
            myDataBase.AddInParameter(dbCommand, "@in_Bank_Name", DbType.String, payment.BankName);
            myDataBase.AddInParameter(dbCommand, "@in_Reference_Number", DbType.String, payment.ReferenceNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Type", DbType.Int16, payment.InvoiceType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, payment.CreatedBy);
            //myDataBase.AddInParameter(dbCommand, "@in_Left_Payment", DbType.Decimal, payment.PaymentLeftAmount);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Status", DbType.Int32, payment.Paymentstatus.Id);
            myDataBase.AddInParameter(dbCommand, "@in_TDS", DbType.Decimal, payment.TDS);
            myDataBase.AddInParameter(dbCommand, "@in_TDSPaymentAmount", DbType.Decimal, payment.TDSWithPayment);
            myDataBase.AddInParameter(dbCommand, "@in_BillNumber", DbType.String, payment.BillNumber);
            myDataBase.AddOutParameter(dbCommand, "@out_Payment_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            Int32.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Payment_Id").ToString(), out id);
            return id;
        }

        public Int32 UpdatePayment(PaymentDOM payment, Int32 paymentId)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.UPDATE_PAYMENT);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Id", DbType.String, paymentId);
            myDataBase.AddInParameter(dbCommand, "@in_Remark", DbType.String, payment.Remark);
            myDataBase.AddInParameter(dbCommand, "@in_Uploaded_Document", DbType.Int32, payment.UploadedDocument);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Mode_Id", DbType.Int32, payment.PaymentModeType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Other_Payment", DbType.String, payment.OtherPayment);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Amount", DbType.Decimal, payment.PaymentAmount);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Date", DbType.DateTime, payment.PaymentDate);
            myDataBase.AddInParameter(dbCommand, "@in_Bank_Name", DbType.String, payment.BankName);
            myDataBase.AddInParameter(dbCommand, "@in_Reference_Number", DbType.String, payment.ReferenceNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, payment.ModifiedBy);
            myDataBase.AddInParameter(dbCommand, "@in_TDS", DbType.Decimal, payment.TDS);
            myDataBase.AddInParameter(dbCommand, "@in_TDSPaymentAmount", DbType.Decimal, payment.TDSWithPayment);
            myDataBase.AddOutParameter(dbCommand, "@out_Payment_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            Int32.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Payment_Id").ToString(), out id);
            return id;
        }

        public Int32 UpdatePaymentStatus(Int32 paymentId, Int16 invoiceType, Int32 statusId, String approvedRegectedBy,String RemarkReject )
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.UPDATE_PAYMENT_STATUS);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Id", DbType.Int32, paymentId);
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Type", DbType.Int32, invoiceType);
            myDataBase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, statusId);
            myDataBase.AddInParameter(dbCommand, "@in_Approved_Rejected_By", DbType.String, approvedRegectedBy);
            myDataBase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, RemarkReject);
            myDataBase.AddOutParameter(dbCommand, "@out_Payment_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            Int32.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Payment_Id").ToString(), out id);
            return id;
        }

        public List<PaymentDOM> ReadPayment(Int32? paymentId)
        {
            lstPayment = new List<PaymentDOM>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_PAYMENT);
            myDataBase.AddInParameter(dbCommand, "in_Payment_Id", DbType.Int32, paymentId);

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    payment = GeneratePaymentDataFromDataReader(reader);
                    lstPayment.Add(payment);
                }
            }
            return lstPayment;
        }

        public List<PaymentDOM> ReadPayment(String CSName, DateTime fromDate, DateTime toDate, String contractNo, String invoiceNo, Int16 invoiceType)
        {
            lstPayment = new List<PaymentDOM>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_PAYMENT_BY_DATE_AND_ID);

            if (CSName == String.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, CSName);

            if (toDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }

            if (contractNo == String.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, contractNo);

            if (invoiceNo == String.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Invoice_Number", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Invoice_Number", DbType.String, invoiceNo);

            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Type", DbType.Int32, invoiceType);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    payment = GeneratePaymentDataFromDataReader(reader);
                    lstPayment.Add(payment);
                }
            }
            return lstPayment;
        }

        public List<PaymentDOM> ReadPayment(DateTime fromDate, DateTime toDate, Int32? invoiceType, String InvoiceNumber, string contractorName)
        {
            lstPayment = new List<PaymentDOM>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_PAYMENT_BY_DATE);

            if (toDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }
            if (string.IsNullOrEmpty(contractorName))
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, contractorName);
            }
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Type", DbType.Int32, invoiceType);
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Number", DbType.String, InvoiceNumber);
           
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    payment = GeneratePaymentDataFromDataReader(reader);
                    lstPayment.Add(payment);
                }
            }
            return lstPayment;
        }

        public List<PaymentDOM> ReadPaymentByStatusId(Int32? statusId, Int32? invoiceType)
        {
            lstPayment = new List<PaymentDOM>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_PAYMENT_BY_STATUS_ID);
            myDataBase.AddInParameter(dbCommand, "@in_Status_Id", DbType.Int32, statusId);
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Type", DbType.Int32, invoiceType);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    payment = GeneratePaymentDataFromDataReader(reader);
                    lstPayment.Add(payment);
                }
            }
            return lstPayment;
        }

        public void DeletePayment(Int32 paymentId, Int16 invoiceType, String modifiedBy,String RemarkReject )
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_PAYMENT);
            myDataBase.AddInParameter(dbCommand, "@in_Payment_Id", DbType.Int32, paymentId);
            myDataBase.AddInParameter(dbCommand, "@in_Invoice_Type", DbType.Int16, invoiceType);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, RemarkReject);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region Private Section

        private PaymentDOM GeneratePaymentDataFromDataReader(IDataReader reader)
        {
            payment = new PaymentDOM();

            payment.Paymentstatus = new MetaData();

            payment.PaymentId = GetIntegerFromDataReader(reader, "Payment_Id");
            payment.InvoiceNumber = GetStringFromDataReader(reader, "Invoice_Number");
            payment.QuotationNumber = GetStringFromDataReader(reader, "Quotation_Number");

            payment.WorkOrderNumber = GetStringFromDataReader(reader, "Work_Order_Number");
            //payment.InvoiceId = GetIntegerFromDataReader(reader, "Contractor_Invoice_Id");

            //payment.PaidAmount = GetDecimalFromDataReader(reader, "PaidAmount");
           // payment.PaymentAmount = GetDecimalFromDataReader(reader, "Payment_Amount");
            payment.PaymentLeftAmount = GetDecimalFromDataReader(reader, "LeftAmount");

            payment.InvoiceType = new MetaData();
            payment.InvoiceType.Id = GetIntegerFromDataReader(reader, "Invoice_Type");
            payment.InvoiceType.Name = GetStringFromDataReader(reader, "Invoice_Type_Name");
            payment.InvoiceDate = GetDateFromReader(reader, "Invoice_Date");
            payment.Paymentstatus.Id = GetIntegerFromDataReader(reader, "Payment_Status");
           
           
            payment.InvoiceRemarks = GetStringFromDataReader(reader, "Invoice_Remarks");
            

            payment.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            payment.ContractorSupplierName = GetStringFromDataReader(reader, "Contractor_Supplier_Name");
            payment.Remark = GetStringFromDataReader(reader, "Remark");
            payment.UploadedDocument = GetIntegerFromDataReader(reader, "Uploaded_Document");
            payment.PaymentModeType = new MetaData();
            payment.PaymentModeType.Id = GetIntegerFromDataReader(reader, "Payment_Mode_Id");
            payment.PaymentModeType.Name = GetStringFromDataReader(reader, "Payment_Mode_Name");
            payment.PaymentAmount = GetDecimalFromDataReader(reader, "Payment_Amount");
            payment.PaymentDate = GetDateFromReader(reader, "Payment_Date");
            payment.BankName = GetStringFromDataReader(reader, "Bank_Name");
            payment.TDS = GetDecimalFromDataReader(reader, "TDS");
            
           
                payment.TDSWithPayment = GetDecimalFromDataReader(reader, "TDSPaymentAmount");
            
           if (payment.TDSWithPayment==null)
            {
                payment.TDSWithPayment = 0;
            }
            payment.BillNumber = GetStringFromDataReader(reader, "BillNumber");
            payment.ReferenceNumber = GetStringFromDataReader(reader, "Reference_Number");
            payment.ApprovalStatusType = new MetaData();
            payment.ApprovalStatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            payment.ApprovalStatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            payment.ApprovedRegectedBy = GetStringFromDataReader(reader, "Approved_Rejected_By");
            payment.ApprovedRejectedDate = GetDateFromReader(reader, "Approved_Rejected_Date");
            payment.IsGenerated = GetShortFromDataReader(reader, "IsGenerated");
            payment.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            payment.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
            payment.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            payment.CreatedDate = GetDateFromReader(reader, "Created_Date");
            payment.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            payment.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            payment.RemarkReject = GetStringFromDataReader(reader, "Remark_approve_Reject");


            return payment;
        }

        #endregion

    }
}
