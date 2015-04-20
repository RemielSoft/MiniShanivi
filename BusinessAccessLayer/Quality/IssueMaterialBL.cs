using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using DataAccessLayer.Quality;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BusinessAccessLayer.Quality
{
    public class IssueMaterialBL : BaseBL
    {
        #region private global variables

        private Database myDatabase;
        private IssueMaterialDAL issueMaterialDAL = null;
        MetaData metaData = null;

        List<IssueMaterialDOM> lstissueMaterial = null;
        List<ItemTransaction> lstItemTransaction = null;

        int id = 0;
        #endregion

        #region Constructor

        public IssueMaterialBL()
        {
            myDatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            issueMaterialDAL = new IssueMaterialDAL(myDatabase);
        }

        #endregion

        #region Issue Material CRUD Methods

        public MetaData CreateIssueMaterial(IssueMaterialDOM issueMaterialDOM, Int32? IsssueMaterialId, bool isFinal)
        {
            string issueMaterialNumber;
            id = 0;
            metaData = new MetaData();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    metaData = issueMaterialDAL.CreateIssueMaterial(issueMaterialDOM, IsssueMaterialId);
                    if (metaData.Id > 0)
                    {
                        if (IsssueMaterialId > 0)
                        {
                            issueMaterialDAL.ResetIssueMaterialMapping(IsssueMaterialId);
                        }
                        id = issueMaterialDAL.CreateIssueMaterialMapping(issueMaterialDOM.DemandVoucher.Quotation.ItemTransaction, metaData.Id);
                        // TODO: Call The Generate Issue Material Number
                        //if (isFinal)
                        //{
                        issueMaterialDAL.GenerateIssueMaterialNumber((int)metaData.Id, out issueMaterialNumber);
                        // }
                        metaData.Name = issueMaterialNumber;
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
        public List<IssueMaterialDOM> ReadIssueMaterial(Int32? IssueMaterialId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String IssueMaterialNo, String ContractorQuotNo)
        {
            lstissueMaterial = new List<IssueMaterialDOM>();
            try
            {
                lstissueMaterial = issueMaterialDAL.ReadIssueMaterial(IssueMaterialId, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, ContractorQuotNo);
            }
            catch (Exception Exp)
            {
                throw Exp;
            }
            return lstissueMaterial;
        }
        public List<ItemTransaction> ReadIssueMaterialMapping(Int32? IssueMaterialId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = issueMaterialDAL.ReadIssueMaterialMapping(IssueMaterialId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }
        public int ReadStockStatus(Int32 ItemId, Int32 ItemModelId, Decimal QuantityIssued)
        {
            id = 0;
            try
            {
                id = issueMaterialDAL.ReadStockStatus(ItemId, ItemModelId, QuantityIssued);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int ReadStockStatus(Int32 ItemId, Int32 ItemModelId, int StoreId, int BrandId, out int StockAvailable)
        {
            id = 0;
            try
            {
                id = issueMaterialDAL.ReadStockStatus(ItemId, ItemModelId, StoreId, BrandId, out StockAvailable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }
        public string DeleteIssueMaterial(int IssueMaterialId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    // errorMessage = issueDemandVoucherDAL.ValidateIssueDemandVoucher(IssueDemandVoucherId, "IDVNo is Used in Mapping Table");
                    if (errorMessage == "")
                    {
                        issueMaterialDAL.DeleteIssueMaterial(IssueMaterialId, modifiedBy, modifiedOn);
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
        public Int32 UpdateIssueMaterialStatus(IssueMaterialDOM issueMaterialDOM)
        {
            Int32 IssueMaterialId;
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    //SupplierRecieveMaterialDAL supplierRecieveMaterialDAL = null;
                    //supplierRecieveMaterialDAL = new SupplierRecieveMaterialDAL();

                    List<ItemTransaction> lstItemTransaction = issueMaterialDAL.ReadIssueMaterialMapping(issueMaterialDOM.IssueMaterialId);
                    foreach (ItemTransaction item in lstItemTransaction)
                    {
                        issueMaterialDAL.UpdateStockReceiveIssueQuantity(item.Item.ItemId, item.Item.ModelSpecification.ModelSpecificationId, item.Item.ModelSpecification.Store.StoreId, item.Item.ModelSpecification.Brand.BrandId, item.Item.ModelSpecification.UnitMeasurement.Id, item.Item.ModelSpecification.UnitMeasurement.Name, item.ItemRequired, Convert.ToInt32(StockUpdateType.StockIssue), item.CreatedBy);
                    }
                    IssueMaterialId = issueMaterialDAL.UpdateIssueMaterialStatus(issueMaterialDOM);

                    transactionScope.Complete();
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
            return IssueMaterialId;
        }




        public IssueMaterialDOM ReadIssueMaterialByDemandVoucher(string demandVoucherNumber)
        {
            return issueMaterialDAL.ReadIssueMaterialByDemandVoucher(demandVoucherNumber);
        }
        #endregion

        public List<MetaData> GetContractorName(string prefixText)
        {
            return issueMaterialDAL.GetContractorName(prefixText);
        }
    }
}
