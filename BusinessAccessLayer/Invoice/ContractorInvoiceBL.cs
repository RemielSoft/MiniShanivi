using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Invoice;
using DocumentObjectModel;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BusinessAccessLayer.Invoice
{

    public class ContractorInvoiceBL : BaseBL
    {
        #region private global variables
        private Database myDatabase;
        private ContractorInvoiceDAL contractorInvoiceDAL = null;
        MetaData metaData = null;

        List<InvoiceDom> lstInvoice = null;
        List<ItemTransaction> lstItemTransaction = null;
        List<MetaData> lstMetaData = null;

        int id = 0;
        #endregion

        #region Constructor

        public ContractorInvoiceBL()
        {
            myDatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            contractorInvoiceDAL = new ContractorInvoiceDAL(myDatabase);


        }

        #endregion

        #region Contractor Invoice CRUD Methods

        public List<InvoiceDom> ReadContractorQuotation(Int32? quotationId, String quotationNumber)
        {
            lstInvoice = new List<InvoiceDom>();
            try
            {
                lstInvoice = contractorInvoiceDAL.ReadContractorQuotation(quotationId, quotationNumber);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstInvoice;

        }

        public MetaData CreateContractorInvoice(InvoiceDom invoiceDom, Int32? INVId)
        {
            id = 0;
            metaData = new MetaData();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    metaData = contractorInvoiceDAL.CreateContractorInvoice(invoiceDom, INVId);
                    if (metaData.Id > 0)
                    {
                        if (invoiceDom.InvoiceType.Id ==Convert.ToInt32(InvoiceType.Normal))
                        {
                            if (INVId > 0)
                            {
                                contractorInvoiceDAL.ResetContractorQuotationInvoiceMapping(INVId);
                            }
                            id = contractorInvoiceDAL.CreateContractorInvoiceMapping(invoiceDom.IssueMaterial.DemandVoucher.Quotation.ItemTransaction, metaData.Id);
                        }
                        else
                        {
                            if (INVId > 0)
                            {
                                contractorInvoiceDAL.ResetContractorQuotationInvoiceMapping(INVId);
                            }
                            id = contractorInvoiceDAL.CreateContractorInvoiceMappingAdvance(invoiceDom.IssueMaterial.DemandVoucher.Quotation.ItemTransaction, metaData.Id);
                        }
                    }

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return metaData;
        }
        public void DeleteContractorInvoiceMapping(int ContractorInvoiceMappingId,int InvoiceId)
        {
            contractorInvoiceDAL.DeleteContractorInvoiceMapping(ContractorInvoiceMappingId, InvoiceId);
        }
        public List<MetaData> ReadContractorPaymentTerm(Int32? ContractorInvoiceId, Int32 contractorId, DateTime toDate, DateTime fromDate, String InvoiceNo, String ContractorWorkOrderNo)
        {
            lstMetaData = new List<MetaData>();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstMetaData = contractorInvoiceDAL.ReadContractorPaymentTerm(ContractorInvoiceId, contractorId, toDate, fromDate, InvoiceNo, ContractorWorkOrderNo);
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstMetaData;
        }
        public String DeleteContractorInvoice(int ContractorInvoiceId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        contractorInvoiceDAL.DeleteContractorInvoice(ContractorInvoiceId, modifiedBy, modifiedOn);
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
        public List<ItemTransaction> ReadQuotationDemandVoucher(String IssueMaterialNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = contractorInvoiceDAL.ReadQuotationDemandVoucher(IssueMaterialNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }
        public List<InvoiceDom> ReadInvoice(String invoiceNumber)
        {
            lstInvoice = new List<InvoiceDom>();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstInvoice = contractorInvoiceDAL.ReadInvoice(invoiceNumber);
                    transactionScope.Complete();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstInvoice;
        }
        //for View Contractor And Edit
        public List<InvoiceDom> ReadContractorInvoice(Int32? ContractorInvoiceId, Int32? contractorId, DateTime toDate, DateTime fromDate, String InvoiceNo, String ContractorWorkOrderNo)
        {
            lstInvoice = new List<InvoiceDom>();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstInvoice = contractorInvoiceDAL.ReadContractorInvoiceView(ContractorInvoiceId, contractorId, toDate, fromDate, InvoiceNo, ContractorWorkOrderNo);
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstInvoice;
        }



        // Open Serach popup With date and Payment Condition Fro Contractor Case


        public List<InvoiceDom> ReadApprovedContractorInvoice(Int32? ContractorInvoiceId, Int32? contractorId, DateTime toDate, DateTime fromDate, String InvoiceNo, String ContractorWorkOrderNo, string contractorName)
        {
            lstInvoice = new List<InvoiceDom>();
            List<InvoiceDom> aaprovedInvoicedforPayment = new List<InvoiceDom>();
            try
            {
                lstInvoice = contractorInvoiceDAL.ReadContractorInvoice(ContractorInvoiceId, contractorId, toDate, fromDate, InvoiceNo, ContractorWorkOrderNo, contractorName);
                QuotationBL qutationBal = new QuotationBL();
                PaymentTermBL pmtTermBal = new PaymentTermBL();
                PaymentBL paymentBl = new PaymentBL();


                List<QuotationDOM> lstQuotations = null;
                int numberDays = 0;
                foreach (InvoiceDom item in lstInvoice)
                {
                    //if (item.ReceiveMaterial.Quotation.StatusType.Name == StatusType.Generated.ToString())
                    if (item.IssueMaterial.DemandVoucher.Quotation.StatusType.Name == StatusType.Generated.ToString())
                    {
                        if (item.InvoiceType.Name != "Other")
                        {
                            lstQuotations = qutationBal.ReadContractorQuotation(item.IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId, item.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber);
                            if (lstQuotations.Count > 0)
                            {
                                List<PaymentTerm> lstPaymentTerms = pmtTermBal.ReadPaymentTermByPurchaseOI(lstQuotations[0].ContractorQuotationId, Convert.ToInt16(QuotationType.Contractor));

                                foreach (PaymentTerm pnd in lstPaymentTerms)
                                {
                                    if (pnd.PaymentType.Name == "After Days" || pnd.PaymentType.Name == "Advance")
                                    {
                                        numberDays = pnd.NumberOfDays;
                                    }

                                }
                                if (item.InvoiceDate.AddDays(numberDays) <= DateTime.Today)
                                {
                                    aaprovedInvoicedforPayment.Add(item);
                                }

                            }

                        }
                        else
                        {
                            aaprovedInvoicedforPayment.Add(item);
                        }
                    }

                }

            }

            catch (Exception exp)
            {

                throw exp;
            }
            return aaprovedInvoicedforPayment;

        }

        public List<InvoiceDom> ReadContractorInvoiceStatusWise(Int32 StatusTypeId)
        {
            lstInvoice = new List<InvoiceDom>();
            try
            {
                lstInvoice = contractorInvoiceDAL.ReadContractorInvoiceStatusWise(StatusTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstInvoice;
        }
        public List<ItemTransaction> ReadInvoiceMapping(Int32? invoiceId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstItemTransaction = contractorInvoiceDAL.ReadInvoiceMapping(invoiceId);
                    transactionScope.Complete();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstItemTransaction;
        }

        public List<ItemTransaction> ReadInvoiceMapping(String invoiceNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstItemTransaction = contractorInvoiceDAL.ReadInvoiceMapping(invoiceNo);
                    transactionScope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }
        public List<ItemTransaction> ReadContractorWorkOrderMapping(int? WorkOrderId, String WorkOrderNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = contractorInvoiceDAL.ReadContractorWorkOrderMapping(WorkOrderId, WorkOrderNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }
        //sundeep read  data view

        public List<ItemTransaction> ReadContractorInvoiceMappingView(Int32? ContractorInvoiceId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {

                    lstItemTransaction = contractorInvoiceDAL.ReadContractorInvoiceMapping(ContractorInvoiceId);
                    transactionScope.Complete();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstItemTransaction;
        }
        public Int32 UpdateContractorInvoiceStatus(InvoiceDom Invoice)
        {
            Int32 ContractorinvoiceId;
            try
            {
                ContractorinvoiceId = contractorInvoiceDAL.UpdateContractorInvoiceStatus(Invoice);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ContractorinvoiceId;
        }
        public Int32 UpdateContractorInvoiceStatusType(InvoiceDom Invoice)
        {
            try
            {
                id = contractorInvoiceDAL.UpdateContractorInvoiceStatusType(Invoice);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return id;
        }
        #endregion
    }
}
