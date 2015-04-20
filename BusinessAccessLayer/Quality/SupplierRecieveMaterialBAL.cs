using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataAccessLayer;
using DocumentObjectModel;
using System.Transactions;
using DataAccessLayer.Quality;

namespace BusinessAccessLayer.Quality
{
    public class SupplierRecieveMaterialBAL : BaseBL
    {
        #region private global variables
        private Database mydatabase;
        private SupplierRecieveMaterialDAL supplierRecieveMaterialDAL = null;
        int id = 0;
        MetaData metaData = null;
        QuotationDOM quotation;
        SupplierRecieveMatarial supplierRecieveMaterial = null;
        List<QuotationDOM> lstQuotation = null;
        ItemTransaction itemTransaction = null;
        List<ItemTransaction> lstitemtransaction = null;
        List<SupplierRecieveMatarial> lstSupplierRecieveMaterial = null;
        #endregion

        #region Constructors
        public SupplierRecieveMaterialBAL()
        {
            mydatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            supplierRecieveMaterialDAL = new SupplierRecieveMaterialDAL(mydatabase);
        }
        #endregion

        #region SupplierRecieveMatarial CRUD
        public MetaData CreateSupplierRecieveMaterial(SupplierRecieveMatarial supplierRecieveMaterial, Int32? SRMID)
        {
            metaData = new MetaData();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    metaData = supplierRecieveMaterialDAL.CreateSupplierRecieveMetarial(supplierRecieveMaterial, SRMID);
                    if (metaData.Id > 0)
                    {
                        if (SRMID > 0)
                        {
                            supplierRecieveMaterialDAL.ResetIssueDemandVoucherMapping(SRMID);
                        }
                        id = supplierRecieveMaterialDAL.CreateSuppplierRecieveMaterialMapping(supplierRecieveMaterial.Quotation.ItemTransaction, metaData.Id);
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
        public List<SupplierRecieveMatarial> SearchReceiveMaterial(String SupplierPONumber, String DeliveryChallanNo, String ReceiveMaterialNo, DateTime toDate, DateTime fromDate,string name)
        {
            lstSupplierRecieveMaterial = new List<SupplierRecieveMatarial>();
            try
            {
                lstSupplierRecieveMaterial = supplierRecieveMaterialDAL.SearchReceiveMaterial(SupplierPONumber, DeliveryChallanNo, ReceiveMaterialNo, toDate, fromDate, name);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstSupplierRecieveMaterial;
        }
        public List<ItemTransaction> ReadSupplierReceiveMaterialMapping(Int32 supplierReceiveMaterialId)
        {
            lstitemtransaction = new List<ItemTransaction>();
            try
            {
                lstitemtransaction = supplierRecieveMaterialDAL.ReadSupplierReceiveMaterialMapping(supplierReceiveMaterialId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstitemtransaction;
        }

        public List<SupplierRecieveMatarial> ReadSupplierReceiveMaterial(Int32? supplierReceiveMaterialId, String ReceiveMaterialNumber)
        {
            lstSupplierRecieveMaterial = new List<SupplierRecieveMatarial>();
            try
            {
                lstSupplierRecieveMaterial = supplierRecieveMaterialDAL.ReadSupplierReceiveMaterial(supplierReceiveMaterialId, ReceiveMaterialNumber);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstSupplierRecieveMaterial;
        }

        public Int32 UpdateSupplierReceiveMaterialStatus(SupplierRecieveMatarial supplierRecieveMatarial)
        {
            Int32 supplierReceiveMaterialId;
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    List<ItemTransaction> lstItemTransaction = supplierRecieveMaterialDAL.ReadSupplierReceiveMaterialMapping(supplierRecieveMatarial.SupplierRecieveMatarialId);
                    foreach (ItemTransaction item in lstItemTransaction)
                    {
                        supplierRecieveMaterialDAL.UpdateStockReceiveIssueQuantity(item.Item.ModelSpecification.Store.StoreId, item.Item.ModelSpecification.Brand.BrandId, item.Item.ItemId, item.Item.ModelSpecification.ModelSpecificationId, item.Item.ModelSpecification.UnitMeasurement.Id, item.Item.ModelSpecification.UnitMeasurement.Name, item.ItemRequired, Convert.ToInt32(StockUpdateType.StockReceive), item.CreatedBy);
                    }
                    supplierReceiveMaterialId = supplierRecieveMaterialDAL.UpdateSupplierReceiveMaterialStatus(supplierRecieveMatarial);

                    transactionScope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return supplierReceiveMaterialId;
        }

        public string DeleteSupplierReceiveMaterial(int SupplierReceiveMaterialId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    // errorMessage = issueDemandVoucherDAL.ValidateIssueDemandVoucher(IssueDemandVoucherId, "IDVNo is Used in Mapping Table");
                    if (errorMessage == "")
                    {
                        supplierRecieveMaterialDAL.DeleteSupplierReceiveMaterial(SupplierReceiveMaterialId, modifiedBy, modifiedOn);
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
        public List<MetaData> GetSupplierName(string prefixText)
        {
            return supplierRecieveMaterialDAL.GetSupplierName(prefixText);
        }
    }
}
