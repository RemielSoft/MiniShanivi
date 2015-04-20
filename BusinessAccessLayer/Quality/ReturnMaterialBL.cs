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
    [Serializable]
    public class ReturnMaterialBL:BaseBL
    {
        #region private global variables

        private Database myDatabase;
        private ReturnMaterialDAL returnMaterialDAL = null;
        MetaData metaData = null;

        List<ReturnMaterialDOM> lstReturnMaterialDOM = null;
        List<ItemTransaction> lstItemTransaction = null;

        int id = 0;
        #endregion

        #region Constructor

        public ReturnMaterialBL()
        {
            myDatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            returnMaterialDAL = new ReturnMaterialDAL(myDatabase);
        }

        #endregion

        #region Issue Material CRUD Methods
        public MetaData CreateReturnMaterial(ReturnMaterialDOM returnMaterialDOM, Int32? ReturnMaterialId)
        {
            id = 0;
            metaData = new MetaData();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    metaData = returnMaterialDAL.CreateReturnMaterial(returnMaterialDOM, ReturnMaterialId);
                    if (metaData.Id > 0)
                    {
                        if (ReturnMaterialId > 0)
                        {
                            returnMaterialDAL.ResetReturnMaterialMapping(ReturnMaterialId);
                        }
                        id = returnMaterialDAL.CreateReturnMaterialMapping(returnMaterialDOM.RecieveMatarial.Quotation.ItemTransaction, metaData.Id);
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
        public List<ItemTransaction> ReadSupplierReturnMaterialMapping(String ReceiveMaterialNumber, String ReturnMaterialNumber)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = returnMaterialDAL.ReadSupplierReturnMaterialMapping(ReceiveMaterialNumber, ReturnMaterialNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstItemTransaction;
        }
        public String DeleteReturnMaterial(int ReturnMaterialId, string modifiedBy)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    if (errorMessage == "")
                    {
                        returnMaterialDAL.DeleteReturnMaterial(ReturnMaterialId, modifiedBy);
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
        public Int32 UpdateSupplierReturnMaterialStatus(ReturnMaterialDOM returnMaterialDOM)
        {
            Int32 returnMaterialId=0;
            try
            {
                returnMaterialId = returnMaterialDAL.UpdateSupplierReturnMaterialStatus(returnMaterialDOM);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return returnMaterialId;
        }
        public List<ReturnMaterialDOM> ReadSupplierReturnMaterial(Int32? ReturnMaterialId, String ReturnMaterialNumber, String ReceiveMaterialNumber, String SupplierPONumber, Int32? SupplierId, String DeliveryChallanNo, DateTime FromDate, DateTime EndDate)
        {
            lstReturnMaterialDOM = new List<ReturnMaterialDOM>();
            try
            {
                lstReturnMaterialDOM = returnMaterialDAL.ReadSupplierReturnMaterial(ReturnMaterialId, ReturnMaterialNumber, ReceiveMaterialNumber, SupplierPONumber, SupplierId, DeliveryChallanNo, FromDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstReturnMaterialDOM;
        }
        #endregion
    }
}
