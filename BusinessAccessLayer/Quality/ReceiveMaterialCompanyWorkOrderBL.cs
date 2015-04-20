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
    public class ReceiveMaterialCompanyWorkOrderBL : BaseBL
    {
        #region Global Declaratin
        private Database mydatabase;
        List<ReceiveMaterialCompanyWorkOrderDom> lstRMCWO = null;
        ReceiveMaterialCompanyWorkOrderDAL receiveMaterialCWODal = null;
        IssueMaterialDAL issueMaterialDAL = null;
        int id = 0;
        MetaData metaData = null;

        #endregion

        #region Constructors
        public ReceiveMaterialCompanyWorkOrderBL()
        {
            mydatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            receiveMaterialCWODal = new ReceiveMaterialCompanyWorkOrderDAL(mydatabase);
            issueMaterialDAL = new IssueMaterialDAL(mydatabase);
        }
        #endregion

        #region ReceiveMaterialCWO CURD

        public MetaData CreateRecieveMaterialCWO(ReceiveMaterialCompanyWorkOrderDom recieveMaterialCWO, Int32? RMCWOID)
        {
            metaData = new MetaData();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    metaData = receiveMaterialCWODal.CreateRecieveMetarialCWO(recieveMaterialCWO, RMCWOID);
                    if (metaData.Id > 0)
                    {
                        if (RMCWOID > 0)
                        {
                            receiveMaterialCWODal.ResetReceiveMaterialCWOMapping(RMCWOID);
                        }
                        id = receiveMaterialCWODal.CreateRecieveMaterialCWOMapping(recieveMaterialCWO.Quotation.ItemTransaction, metaData.Id);
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
        public List<ReceiveMaterialCompanyWorkOrderDom> ReadRMCWO(String CRMNo, int CompanyWorkOrderId, DateTime FromDate, DateTime ToDate)
        {
            lstRMCWO = new List<ReceiveMaterialCompanyWorkOrderDom>();
            try
            {
                lstRMCWO = receiveMaterialCWODal.ReadRMCWO(CRMNo, CompanyWorkOrderId, FromDate, ToDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstRMCWO;
        }

        public List<ReceiveMaterialCompanyWorkOrderDom> ReadReceiveMaterailCompanyWorkOrderById(Int32 RMCWOId)
        {

            lstRMCWO = new List<ReceiveMaterialCompanyWorkOrderDom>();
            //receiveMaterialCWODal = new ReceiveMaterialCompanyWorkOrderDAL();
            try
            {
                lstRMCWO = receiveMaterialCWODal.ReadReceiveMaterailCompanyWorkOrder(RMCWOId);
            }
            catch (Exception exp)
            {
                
                throw exp;
            }
            return lstRMCWO;
        }
        public string DeleteReceiveMaterialCWO(int ReceiveMaterialCWOId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        receiveMaterialCWODal.DeleteReceiveMaterialCWO(ReceiveMaterialCWOId, modifiedBy, modifiedOn);
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
        public Int32 UpdateReceiveMaterialCWOStatus(ReceiveMaterialCompanyWorkOrderDom ReceiveMaterialCWO)
        {
            Int32 ReceiveMaterialCWOId;
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    ReceiveMaterialCWOId = receiveMaterialCWODal.UpdateReceiveMaterialCWOStatus(ReceiveMaterialCWO);
                    transactionScope.Complete();
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReceiveMaterialCWOId;
        }
        public List<ItemTransaction> ReadRMCWOMapping(int CRMId)
        {
            List<ItemTransaction> lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = receiveMaterialCWODal.ReadRMCWOMapping(CRMId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }

        
        public void UpdateStockReceiveIssueQuantity(List<ItemTransaction> lstitemTransaction)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    foreach (var lstitem in lstitemTransaction)
                    {
                        issueMaterialDAL.UpdateStockReceiveIssueQuantity(lstitem.Item.ItemId, lstitem.Item.ModelSpecification.ModelSpecificationId, lstitem.Item.ModelSpecification.Store.StoreId, lstitem.Item.ModelSpecification.Brand.BrandId, lstitem.Item.ModelSpecification.UnitMeasurement.Id, lstitem.Item.ItemName, lstitem.NumberOfUnit, Convert.ToInt32(StockUpdateType.StockReceive), lstitem.CreatedBy);
                    }
                    transactionScope.Complete();
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
       
    }
}
