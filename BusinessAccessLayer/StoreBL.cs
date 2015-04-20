using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DataAccessLayer;
using DocumentObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace BusinessAccessLayer
{
    public class StoreBL : BaseBL
    {
        #region private global variable(s)

        /// <summary>
        ///  Database Object
        /// </summary>
        private Database myDataBase;

        /// <summary>
        ///  Instance of DAL
        /// </summary>
        private StoreDL storeDAL = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandDAL"/> class.
        /// </summary>
        public StoreBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            storeDAL = new StoreDL(myDataBase);
        }

        #endregion

        #region Department CRUD Methods

        /// <summary>
        /// Creates the Store.
        /// </summary>
        /// <param name="store">The Store.</param>
        /// <returns></returns>
        public int CreateStore(Store store)
        {
            int id = 0;
            try
            {
                id = storeDAL.CreateStore(store);
            }
            catch (Exception exp)
            {

                Logger.Write(exp.Message);

            }
            return id;
        }

        /// <summary>
        /// Updates the Store.
        /// </summary>
        /// <param name="store">The Store.</param>
        public int UpdateStore(Store store)
        {
            int id = 0;
            try
            {
                id = storeDAL.UpdateStore(store);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }


            return id;
        }

        /// <summary>
        /// Deletes the Store.
        /// </summary>
        /// <param name="locationId">The Store id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public string DeleteStore(int storeId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    // First Validate the Brand Wheteher it should be deleted
                    // errorMessage = storeDAL.ValidateBrand(brandId);
                    if (errorMessage == "")
                    {
                        storeDAL.DeleteStore(storeId, modifiedBy, modifiedOn);
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

        /// <summary>
        /// Reads the Store.
        /// </summary>
        /// <returns></returns>
        public List<Store> ReadStore(int? storeId)
        {
            List<Store> stores = new List<Store>();
            try
            {
                stores = storeDAL.ReadStores(storeId);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return stores;
        }

        /// <summary>
        /// Read Store Based On Stock
        /// </summary>
        /// <param name="itemModelId"></param>
        /// <returns></returns>
        public List<Store> ReadStoreBasedOnStock(int itemModelId)
        {
            List<Store> stores = new List<Store>();
            try
            {
                stores = storeDAL.ReadStoresBasedOnStock(itemModelId);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return stores;
        }




        #endregion
    }
}