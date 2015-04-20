using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DocumentObjectModel;
using DataAccessLayer.Quality;
using DataAccessLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace BusinessAccessLayer
{
    public class QuotationBL : BaseBL
    {
        #region Private Global Variables

        private Database Mydatabase;
        private QuotationDAL quotationDAL = null;
        private PaymentTermDL paymentTermDAL = null;
        private TermAndConditionDAL termAndConditionDL = null;
        private DeliveryScheduleDL deliveryScheduleDL = null;

        List<QuotationDOM> lstQuotation = null;
        List<MetaData> lstMetaData = null;
        List<ItemTransaction> lstItemTransaction = null;

        Int32 quotationId = 0;

        #endregion

        #region Constructor(s)

        public QuotationBL()
        {
            Mydatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            quotationDAL = new QuotationDAL(Mydatabase);
            paymentTermDAL = new PaymentTermDL(Mydatabase);
            termAndConditionDL = new TermAndConditionDAL(Mydatabase);
            deliveryScheduleDL = new DeliveryScheduleDL(Mydatabase);
        }

        #endregion

        #region Group CRUD Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractor"></param>
        /// <returns></returns>
        public List<MetaData> CreatePurchaseOrder(QuotationDOM quotation)
        {
            lstMetaData = new List<MetaData>();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstMetaData = quotationDAL.CreatePurchaseOrder(quotation);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return lstMetaData;
        }

        public Int32 CreatePurchaseOrderMapping(ItemTransaction item_Transaction, Int32 contractor_PO_Id)
        {
            Int32 contractor_Purchase_Order_Mapping_Id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    contractor_Purchase_Order_Mapping_Id = quotationDAL.CreatePurchaseOrderMapping(item_Transaction, contractor_PO_Id);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return contractor_Purchase_Order_Mapping_Id;
        }

        public List<QuotationDOM> ReadContractorQuotation(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String quotationNo)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadContractorQuotation(contractorId, toDate, fromDate, contractNo, quotationNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadContractorQuotationView(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String WorkOrderNo)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadContractorQuotationView(contractorId, toDate, fromDate, contractNo, WorkOrderNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadContractorQuotation(DateTime toDate, DateTime fromDate)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadContractorQuotation(toDate, fromDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public List<ItemTransaction> ReadContractorQuotationMapping(Int32 quotationId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = quotationDAL.ReadContractorQuotationMapping(quotationId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }

        public List<QuotationDOM> ReadContractorQuotation(Int32 StatusTypeId)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadContractorQuotation(StatusTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadContractorQuotation(Int32? quotationId, String quotationNumber)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadContractorQuotation(quotationId, quotationNumber);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public List<MetaData> ReadQuotationStatusMetaData()
        {
            lstMetaData = new List<MetaData>();
            try
            {
                lstMetaData = quotationDAL.ReadQuotationStatusMetaData();
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return lstMetaData;
        }

        public Int32 UpdateContractorQuotationStatus(QuotationDOM quotation)
        {
            IssueMaterialDAL issueMaterialDAL = new IssueMaterialDAL(Mydatabase);
            IssueDemandVoucherDAL issueDemandVoucherDAL = new IssueDemandVoucherDAL(Mydatabase);
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    quotationId = quotationDAL.UpdateContractorQuotationStatus(quotation);

                    List<QuotationDOM> lstQuotationDom = ReadContractorQuotation(quotation.ContractorQuotationId, null);

                    List<ItemTransaction> lstItems = ReadContractorQuotationMapping(quotation.ContractorQuotationId);

                    //Create A object of Demand BAL

                    if (quotation.StatusType.Id == 3 && lstQuotationDom != null && lstQuotation.Count > 0)
                    {
                        IssueDemandVoucherBL isuDVBal = new IssueDemandVoucherBL();

                        //Create a object of IssueDemandVoucherDOM 

                        IssueDemandVoucherDOM issueDemandVoucherDOM = new IssueDemandVoucherDOM();
                        issueDemandVoucherDOM.Quotation = lstQuotationDom[0];
                        issueDemandVoucherDOM.Quotation.ItemTransaction = lstItems;
                        issueDemandVoucherDOM.MaterialDemandDate = DateTime.Now;
                        issueDemandVoucherDOM.Quotation.OrderDate = DateTime.Now;
                        issueDemandVoucherDOM.Remarks = "Auto Created";
                        issueDemandVoucherDOM.Quotation.StatusType.Id = 4;
                        issueDemandVoucherDOM.CreatedBy = "admin";
                        MetaData metaData = issueDemandVoucherDAL.CreateIssueDemandVoucher(issueDemandVoucherDOM, null);
                        int demandVoucherId = issueDemandVoucherDAL.CreateIssueDemandVoucherMapping(ReadEmptyItemFromContractorQuotation(issueDemandVoucherDOM.Quotation), metaData.Id);

                        //Issue Meterial Note 

                        IssueMaterialDOM issueMaterialDOM = new IssueMaterialDOM();
                        issueMaterialDOM.DemandVoucher = new IssueDemandVoucherDOM();
                        issueMaterialDOM.DemandVoucher.Quotation = lstQuotationDom[0];
                        issueMaterialDOM.DemandVoucher.Quotation.ItemTransaction = lstItems;
                        issueMaterialDOM.DemandVoucher.MaterialDemandDate = DateTime.Now;
                        issueMaterialDOM.DemandVoucher.Quotation.StatusType.Id = 4;
                        issueMaterialDOM.DemandVoucher.Remarks = "Auto Created";
                        issueMaterialDOM.IssueMaterialDate = DateTime.Now;
                        issueMaterialDOM.DemandVoucher.IssueDemandVoucherNumber = metaData.Name;
                        issueMaterialDOM.DemandVoucher.IssueDemandVoucherId = metaData.Id;
                        issueMaterialDOM.CreatedBy = "Admin";
                        MetaData metaDATA = issueMaterialDAL.CreateIssueMaterial(issueMaterialDOM, null);
                        int IssueId = issueMaterialDAL.CreateIssueMaterialMapping(ReadEmptyItemFromContractorQuotation(issueDemandVoucherDOM.Quotation), metaDATA.Id);
                    }
                    transactionScope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return quotationId;
        }


        private List<ItemTransaction> ReadEmptyItemFromContractorQuotation(QuotationDOM quotation)
        {
            List<ItemTransaction> lstItemTransaction = new List<ItemTransaction>();
            ItemTransaction itemTransaction = null;
            foreach (ItemTransaction myItem in quotation.ItemTransaction)
            {
                if (myItem.Item.ItemId == 0)
                {
                    itemTransaction = new ItemTransaction();
                    itemTransaction.MetaProperty = new MetaData();
                    itemTransaction = new ItemTransaction();
                    itemTransaction.MetaProperty = new MetaData();
                    itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                    itemTransaction.Item = new Item();
                    itemTransaction.Item.ModelSpecification = new ModelSpecification();
                    itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                    itemTransaction.Item.ModelSpecification.Category = new ItemCategory();

                    itemTransaction.DeliverySchedule.Id = myItem.DeliverySchedule.Id;
                    //itemTransaction.DeliverySchedule.ActivityDescriptionId = myItem.DeliverySchedule.ActivityDescriptionId;
                    itemTransaction.DeliverySchedule.ActivityDescription = myItem.DeliverySchedule.ActivityDescription;
                    //itemTransaction.Item.ItemId = myItem.DeliverySchedule.Id;
                    itemTransaction.Item.ItemName = "N/A";
                    //itemTransaction.Item.ModelSpecification.ModelSpecificationId = myItem.DeliverySchedule.Id;
                    itemTransaction.Item.ModelSpecification.ModelSpecificationName = "N/A";
                    //itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = myItem.DeliverySchedule.Id;
                    //itemTransaction.Item.ModelSpecification.Brand = myItem.;

                    itemTransaction.NumberOfUnit = myItem.NumberOfUnit;
                    itemTransaction.UnitIssued = myItem.NumberOfUnit;
                    itemTransaction.UnitLeft = 0;
                    itemTransaction.ItemRequired = myItem.NumberOfUnit;
                    itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = "N/A";
                    itemTransaction.CreatedBy = "admin";
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }

        public String DeleteContractorQuotation(Int32 quotationId, Int16 quotationType, String modifiedBy)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        deliveryScheduleDL.DeleteDeliverySchedule(quotationId, quotationType, modifiedBy);
                        paymentTermDAL.DeletePaymentTerm(quotationId, quotationType, modifiedBy);
                        termAndConditionDL.DeleteTermAndCondition(quotationId, quotationType, modifiedBy);
                        quotationDAL.DeleteContractorQuotationMapping(quotationId, modifiedBy);
                        quotationDAL.DeleteContractorQuotation(quotationId, modifiedBy);
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

        public void ResetQuotationMapping(Int32 quotationId, Int32 quotationType)
        {
            try
            {
                quotationDAL.ResetQuotationMapping(quotationId, quotationType);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<QuotationDOM> ReadSupplierQuotation(Int32 supplierId, DateTime toDate, DateTime fromDate, String contractNo, String quotationNo)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadSupplierQuotation(supplierId, toDate, fromDate, contractNo, quotationNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadSupplierQuotation(DateTime toDate, DateTime fromDate)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadSupplierQuotation(toDate, fromDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public Int32 UpdateSupplierQuotationStatus(QuotationDOM quotation)
        {
            try
            {
                quotationId = quotationDAL.UpdateSupplierQuotationStatus(quotation);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return quotationId;
        }

        public String DeleteSupplierQuotation(Int32 quotationId, Int16 quotationType, String modifiedBy)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        deliveryScheduleDL.DeleteDeliverySchedule(quotationId, quotationType, modifiedBy);
                        paymentTermDAL.DeletePaymentTerm(quotationId, quotationType, modifiedBy);
                        termAndConditionDL.DeleteTermAndCondition(quotationId, quotationType, modifiedBy);
                        quotationDAL.DeleteSupplierQuotationMapping(quotationId, modifiedBy);
                        quotationDAL.DeleteSupplierQuotation(quotationId, modifiedBy);
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

        #endregion

        #region Supplier Purchage order

        public List<MetaData> CreateSupplierPurchaseOrder(QuotationDOM quotation)
        {
            lstMetaData = new List<MetaData>();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstMetaData = quotationDAL.CreateSupplierPurchaseOrder(quotation);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return lstMetaData;
        }

        public Int32 CreateSupplierPurchaseOrderMapping(ItemTransaction item_Transaction, Int32 supplier_PO_Id)
        {
            Int32 supplier_Purchase_Order_Mapping_Id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    supplier_Purchase_Order_Mapping_Id = quotationDAL.CreateSupplierPurchaseOrderMapping(item_Transaction, supplier_PO_Id);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return supplier_Purchase_Order_Mapping_Id;
        }

        public List<QuotationDOM> ReadSupplierQuotation(Int32? quotationId, String quotationNumber)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadSupplierQuotation(quotationId, quotationNumber);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadSupplierQuotation(Int32 StatusTypeId)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadSupplierQuotation(StatusTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        public List<ItemTransaction> ReadSupplierQuotationMapping(Int32 quotationId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = quotationDAL.ReadSupplierQuotationMapping(quotationId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }
        public List<QuotationDOM> ReadSupplierQuotationView(Int32 supplierId, DateTime toDate, DateTime fromDate, String WorkOrderNo)
        {
            lstQuotation = new List<QuotationDOM>();
            try
            {
                lstQuotation = quotationDAL.ReadSupplierQuotationView(supplierId, toDate, fromDate, WorkOrderNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstQuotation;
        }

        #endregion
    }
}
