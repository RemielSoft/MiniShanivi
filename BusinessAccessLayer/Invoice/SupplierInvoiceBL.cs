using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Invoice;
using DocumentObjectModel;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BusinessAccessLayer.Quality;


namespace BusinessAccessLayer.Invoice
{
    public class SupplierInvoiceBL : BaseBL
    {
        #region private global variables
        private Database myDatabase;
        private SupplierInvoiceDAL supplierInvoiceDAL = null;
        MetaData metaData = null;

        List<InvoiceDom> lstInvoice = null;
        List<MetaData> lstMetaData = null;
        List<ItemTransaction> lstItemTransaction = null;

        int id = 0;
        #endregion



        #region Constructor

        public SupplierInvoiceBL()
        {
            myDatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            supplierInvoiceDAL = new SupplierInvoiceDAL(myDatabase);
        }

        #endregion

        #region Supplier Invoice CRUD Methods
        public List<InvoiceDom> ReadSupplierQuotation(Int32? quotationId, String quotationNumber)
        {
            lstInvoice = new List<InvoiceDom>();
            try
            {
                lstInvoice = supplierInvoiceDAL.ReadSupplierQuotation(quotationId, quotationNumber);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstInvoice;
        }
        public MetaData CreateSupplierInvoice(InvoiceDom invoiceDom, Int32? SupplierInvoiceId)
        {
            id = 0;
            metaData = new MetaData();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    metaData = supplierInvoiceDAL.CreateSupplierInvoice(invoiceDom, SupplierInvoiceId);
                    if (metaData.Id > 0)
                    {
                        if (invoiceDom.InvoiceType.Id == Convert.ToInt32(InvoiceType.Normal))
                        {
                            if (SupplierInvoiceId > 0)
                            {
                                supplierInvoiceDAL.ResetSupplierInvoiceMapping(SupplierInvoiceId);
                            }

                            id = supplierInvoiceDAL.CreateSupplierInvoiceMapping(invoiceDom.ReceiveMaterial.Quotation.ItemTransaction, metaData.Id);
                        }
                        else
                        {
                            if (SupplierInvoiceId > 0)
                            {
                                supplierInvoiceDAL.ResetSupplierInvoiceMapping(SupplierInvoiceId);
                            }
                            id = supplierInvoiceDAL.CreateSupplierInvoiceMappingAdvance(invoiceDom.ReceiveMaterial.Quotation.ItemTransaction, metaData.Id);
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
        public void DeleteSupplierInvoiceMapping(int SupplierInvoiceMappingId)
        {
            supplierInvoiceDAL.DeleteSupplierInvoiceMapping(SupplierInvoiceMappingId);
        }
        public List<ItemTransaction> ReadSupplierPOReceiveMaterialMapping(Int32? SupplierPurchaseOrderId, String SupplierPurchaseOrderNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = supplierInvoiceDAL.ReadSupplierPOReceiveMaterialMapping(SupplierPurchaseOrderId, SupplierPurchaseOrderNo);
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
                    lstInvoice = supplierInvoiceDAL.ReadInvoice(invoiceNumber);
                    transactionScope.Complete();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstInvoice;
        }

        public List<InvoiceDom> ReadSupplierInvoiceStatusWise(Int32 StatusTypeId)
        {
            lstInvoice = new List<InvoiceDom>();
            try
            {
                lstInvoice = supplierInvoiceDAL.ReadSupplierInvoiceByStatusType(StatusTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstInvoice;
        }
        public List<ItemTransaction> ReadInvoiceMapping(String invoiceNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                //using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                // {
                lstItemTransaction = supplierInvoiceDAL.ReadInvoiceMapping(invoiceNo);
                //transactionScope.Complete();
                // }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstItemTransaction;
        }


        public List<ItemTransaction> ReadSupplierInvoiceMapping(Int32? SupplierInvoiceId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstItemTransaction = supplierInvoiceDAL.ReadSupplierInvoiceMapping(SupplierInvoiceId);
                    transactionScope.Complete();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstItemTransaction;
        }
        public String DeleteSupplierInvoice(int SupplierInvoiceId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        supplierInvoiceDAL.DeleteSupplierInvoice(SupplierInvoiceId, modifiedBy, modifiedOn);
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

        //Supplier View and edit
        public List<InvoiceDom> ReadSupplierInvoice(Int32? SupplierInvoiceId, Int32? SupplierId, DateTime toDate, DateTime fromDate, String SupplierInvoiceNo, String SupplierPONo)
        {
            lstInvoice = new List<InvoiceDom>();
            try
            {

                lstInvoice = supplierInvoiceDAL.ReadSupplierInvoiceView(SupplierInvoiceId, SupplierId, toDate, fromDate, SupplierInvoiceNo, SupplierPONo);

            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstInvoice;
        }

        // for supplier case Payment term
        public List<InvoiceDom> ReadApprovedSupplierInvoice(Int32? SupplierInvoiceId, Int32? SupplierId, DateTime toDate, DateTime fromDate, String SupplierInvoiceNo, String SupplierPONo, string contractorName)
        {
            lstInvoice = new List<InvoiceDom>();
            List<InvoiceDom> aaprovedInvoicedforPayment = new List<InvoiceDom>();
            try
            {

                lstInvoice = supplierInvoiceDAL.ReadSupplierInvoice(SupplierInvoiceId, SupplierId, toDate, fromDate, SupplierInvoiceNo, SupplierPONo, contractorName);
                QuotationBL qutationBal = new QuotationBL();
                PaymentTermBL pmtTermBal = new PaymentTermBL();
                PaymentBL paymentBl = new PaymentBL();
                //List<PaymentDOM> lstPayment = null;
                // lstPayment = new List<PaymentDOM>();

                List<QuotationDOM> lstQuotations = null;
                int numberDays = 0;
                foreach (InvoiceDom item in lstInvoice)
                {
                    if (item.ReceiveMaterial.Quotation.StatusType.Name == StatusType.Generated.ToString() || item.ReceiveMaterial.Quotation.StatusType.Name == StatusType.Approved.ToString())
                    {
                        //List<PaymentDOM> lstPayment = paymentBl.ReadPayment(String.Empty, DateTime.MinValue, DateTime.MinValue, String.Empty, item.InvoiceNumber, Convert.ToInt16(QuotationType.Supplier));
                        //if (lstPayment.Count > 0)
                        //{
                        //if (lstPayment[0].PaymentLeftAmount >= 0)
                        // {

                        //if (item.InvoiceType.Name != "Advance")

                        if (item.InvoiceType.Name != "Other")
                        {
                            lstQuotations = qutationBal.ReadSupplierQuotation(item.ReceiveMaterial.Quotation.SupplierQuotationId, item.ReceiveMaterial.Quotation.SupplierQuotationNumber);
                            if (lstQuotations.Count > 0)
                            {
                                //List<PaymentTerm> lstPaymentTerms = pmtTermBal.ReadPaymentTermByPurchaseOI(lstQuotations[0].SupplierQuotationId, Convert.ToInt16(QuotationType.Supplier));
                                //foreach (PaymentTerm pnd in lstPaymentTerms)
                                //{

                                //    // 
                                //    if (pnd.PaymentType.Name == "After Days" || pnd.PaymentType.Name == "Advance")
                                //    {
                                //        numberDays = pnd.NumberOfDays;
                                //    }
                                //}
                              
                                //if (item.InvoiceDate.AddDays(numberDays) <= DateTime.Today)
                                //{
                                    //aaprovedInvoicedforPayment.Add(item);
                                //}
                                    aaprovedInvoicedforPayment.Add(item);
                            }
                        }
                        // }
                        else
                        {
                            aaprovedInvoicedforPayment.Add(item);
                        }
                        //}
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return aaprovedInvoicedforPayment;
        }
        public List<MetaData> ReadSupplierPaymentTerm(Int32? SupplierInvoiceId, Int32 SupplierId, DateTime toDate, DateTime fromDate, String SupplierInvoiceNo, String SupplierPONo)
        {
            lstMetaData = new List<MetaData>();
            try
            {

                lstMetaData = supplierInvoiceDAL.ReadSupplierPaymentTerm(SupplierInvoiceId, SupplierId, toDate, fromDate, SupplierInvoiceNo, SupplierPONo);

            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstMetaData;
        }
        public Int32 UpdateSupplierInvoiceStatus(InvoiceDom Invoice)
        {
            Int32 SupplierinvoiceId;
            try
            {
                SupplierinvoiceId = supplierInvoiceDAL.UpdateSupplierInvoiceStatus(Invoice);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return SupplierinvoiceId;
        }
        public Int32 UpdateSupplierInvoiceStatusType(InvoiceDom Invoice)
        {
            try
            {
                id = supplierInvoiceDAL.UpdateSupplierInvoiceStatusType(Invoice);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return id;
        }

        /// <summary>
        /// Pervesh
        /// </summary>
        /// <param name="quotationId"></param>
        /// <param name="quotationNumber"></param>
        /// <returns></returns>
        public Processor ReadSupplierInvoiceRelatedData(Int32? quotationId, String quotationNumber)
        {
            //1. Read Complete PO--Add To Processor
            //2. Read Received Material -- 
            //3. Read Invioce Details againest the PO --
            //4. Invoice Object Consolidate Received mateiral (Might be form Procedure also)--Add To Processor  -- FOR VALIDATION
            //5. Invoice Object Genrate the Invoice Item Object for left metrial -- Add To Processor -- to display after search the PO Number

            Processor processor = new Processor();

            QuotationBL quotationBal = new QuotationBL();
            List<QuotationDOM> lstQuotationDom = new List<QuotationDOM>();
            lstQuotationDom = quotationBal.ReadSupplierQuotation(null, quotationNumber);

            lstQuotationDom[0].ItemTransaction = quotationBal.ReadSupplierQuotationMapping(lstQuotationDom[0].SupplierQuotationId);

            SupplierRecieveMaterialBAL supplierReceiveMaterial = new SupplierRecieveMaterialBAL();
            List<SupplierRecieveMatarial> lstSupplierReciveMaterial = supplierReceiveMaterial.SearchReceiveMaterial(quotationNumber, string.Empty, string.Empty, DateTime.MinValue, DateTime.MinValue, null);

            AppInvoice totalRecivedItemsInQuality = ReadTotalMaterialReceivedAgainstPO(lstSupplierReciveMaterial, quotationNumber);
            processor.Add("QualityReceived", totalRecivedItemsInQuality);

            AppInvoice totalRecivedItemsInInvoice = ReadTotalMaterialRecievedInInvoice(quotationNumber);
            processor.Add("InvoiceReceived", totalRecivedItemsInInvoice);


            if (lstQuotationDom.Count > 0)
            {


                foreach (ItemTransaction item in totalRecivedItemsInQuality.ItemDetails)
                {

                    for (int i = 0; i < lstQuotationDom[0].ItemTransaction.Count; i++)
                    {
                        if (lstQuotationDom[0].ItemTransaction[i].Item.ItemName == item.Item.ItemName)
                        {
                            lstQuotationDom[0].ItemTransaction[i].ItemReceivedQuality = item.ItemRequired;
                        }
                    }

                }

                foreach (ItemTransaction item in totalRecivedItemsInInvoice.ItemDetails)
                {

                    for (int i = 0; i < lstQuotationDom[0].ItemTransaction.Count; i++)
                    {
                        if (lstQuotationDom[0].ItemTransaction[i].Item.ItemName == item.Item.ItemName)
                        {
                            lstQuotationDom[0].ItemTransaction[i].ItemReceivedInvoice = item.ItemRequired;
                        }
                    }

                }
                processor.Add("Quotation", lstQuotationDom[0]);
            }

            return processor;
        }

        private AppInvoice ReadTotalMaterialReceivedAgainstPO(List<SupplierRecieveMatarial> lstSupplierReciveMaterial, String poNumber)
        {

            Dictionary<string, ItemTransaction> dicTotalQuantity = new Dictionary<string, ItemTransaction>();
            List<AppInvoice> lstItemReceived = new List<AppInvoice>();
            AppInvoice appInvoice = null;
            SupplierRecieveMaterialBAL supplierRecMatBal = new SupplierRecieveMaterialBAL();
            foreach (SupplierRecieveMatarial item in lstSupplierReciveMaterial)
            {
                appInvoice = new AppInvoice();
                appInvoice.ItemDetails = supplierRecMatBal.ReadSupplierReceiveMaterialMapping(item.SupplierRecieveMatarialId);
                lstItemReceived.Add(appInvoice);
            }

            AppInvoice totalReceivedMaterial = new AppInvoice();
            foreach (AppInvoice appInvoiceItem in lstItemReceived)
            {
                totalReceivedMaterial.InvoiceNumber = appInvoiceItem.InvoiceNumber;
                totalReceivedMaterial.PONumber = poNumber;


                foreach (ItemTransaction item in appInvoiceItem.ItemDetails)
                {

                    if (dicTotalQuantity.ContainsKey(item.Item.ItemName))
                    {
                        ItemTransaction items = dicTotalQuantity[item.Item.ItemName];

                        items.ItemRequired = items.ItemRequired + item.ItemRequired;

                        dicTotalQuantity[item.Item.ItemName] = items;
                    }
                    else
                    {
                        dicTotalQuantity.Add(item.Item.ItemName, item);
                    }
                }
            }
            totalReceivedMaterial.ItemDetails = new List<ItemTransaction>();

            foreach (KeyValuePair<string, ItemTransaction> item in dicTotalQuantity)
            {
                totalReceivedMaterial.ItemDetails.Add(item.Value);
            }
            return totalReceivedMaterial;

        }

        private AppInvoice ReadTotalMaterialRecievedInInvoice(String poNumber)
        {

            Dictionary<string, ItemTransaction> dicTotalQuantity = new Dictionary<string, ItemTransaction>();
            List<AppInvoice> lstItemReceived = new List<AppInvoice>();
            AppInvoice appInvoice = null;
            SupplierInvoiceBL supplierInvoiceBal = new SupplierInvoiceBL();
            List<InvoiceDom> lstInvoices = supplierInvoiceBal.ReadSupplierInvoice(null, 0, DateTime.MinValue, DateTime.MinValue, string.Empty, poNumber);



            foreach (InvoiceDom item in lstInvoices)
            {
                appInvoice = new AppInvoice();
                appInvoice.ItemDetails = supplierInvoiceBal.ReadInvoiceMapping(item.InvoiceNumber);
                appInvoice.PayableAmount = item.PayableAmount;
                lstItemReceived.Add(appInvoice);
            }

            AppInvoice totalReceivedMaterial = new AppInvoice();
            foreach (AppInvoice appInvoiceItem in lstItemReceived)
            {
                totalReceivedMaterial.InvoiceNumber = appInvoiceItem.InvoiceNumber;
                totalReceivedMaterial.PONumber = poNumber;
                totalReceivedMaterial.PayableAmount = totalReceivedMaterial.PayableAmount + appInvoiceItem.PayableAmount;
                foreach (ItemTransaction item in appInvoice.ItemDetails)
                {

                    if (dicTotalQuantity.ContainsKey(item.Item.ItemName))
                    {
                        ItemTransaction items = dicTotalQuantity[item.Item.ItemName];

                        items.ItemRequired = items.ItemRequired + item.ItemRequired;

                        dicTotalQuantity[item.Item.ItemName] = items;
                    }
                    else
                    {
                        dicTotalQuantity.Add(item.Item.ItemName, item);
                    }
                }
            }
            totalReceivedMaterial.ItemDetails = new List<ItemTransaction>();

            foreach (KeyValuePair<string, ItemTransaction> item in dicTotalQuantity)
            {
                totalReceivedMaterial.ItemDetails.Add(item.Value);
            }
            return totalReceivedMaterial;

        }

        public List<InvoiceDom> ReadSupplierBillAmount(DateTime fromDate, DateTime ToDate)
        {
            lstInvoice = supplierInvoiceDAL.ReadSupplierBillAmount(fromDate, ToDate);
            return lstInvoice;
        }

        #endregion
    }
}
