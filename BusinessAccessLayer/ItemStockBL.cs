using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataAccessLayer;
using DocumentObjectModel;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace BusinessAccessLayer
{
    public class ItemStockBL:BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private ItemStockDAL itemStockDAL = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public ItemStockBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            itemStockDAL = new ItemStockDAL(myDataBase);
        }

        #endregion

        #region ItemStock CRUD

        public int CreateItemStock(ItemStock itemStock)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = itemStockDAL.CreateItemStock(itemStock);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return id;
        }
        public int UpdateItemStock(ItemStock itemStock)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = itemStockDAL.UpdateItemStock(itemStock);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return id;
        }
        public List<ItemStock> ReadItemStock(Int32? itemstockid)
        {
            List<ItemStock> lstItemStock = new List<ItemStock>();
            try
            {
                lstItemStock = itemStockDAL.ReadItemStock(itemstockid);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstItemStock;
        }
        public List<ModelSpecification> ReadUnitMeasurementBySpecification(int? ModelspecificationId)
        {
            List<ModelSpecification> lstModelSpecification = new List<ModelSpecification>();
            try
            {
                lstModelSpecification = itemStockDAL.ReadUnitMeasurementBySpecification(ModelspecificationId);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstModelSpecification;
        }
        public string DeleteItemStock(int itemstockid, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    errorMessage = itemStockDAL.ValidateItemStock(itemstockid);

                    if (errorMessage == "")
                    {
                        itemStockDAL.DeleteItemStock(itemstockid, modifiedBy, modifiedOn);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return errorMessage;

        }
        
        #endregion      

    }
}
