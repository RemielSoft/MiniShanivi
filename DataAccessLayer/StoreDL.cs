using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccessLayer
{
    public class StoreDL : BaseDAL
    {
        #region private global variable(s)
        /// <summary>
        /// Data Base Object
        /// </summary>
        private Database myDataBase;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public StoreDL(Database dataBase)
        {
            myDataBase = dataBase;
        }

        #endregion

        #region Brand CRUD Methods

        /// <summary>
        /// Creates the Store.
        /// </summary>
        /// <param name="brand">The Store.</param>
        /// <returns></returns>
        public int CreateStore(Store store)
        {
            int storeId;
            String sqlCommand = DBConstants.CREATE_STORE_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "in_Store_Name", DbType.String, store.StoreName);
            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, store.CreatedBy);
            myDataBase.AddOutParameter(dbCommand, "out_StoreId", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "out_StoreId").ToString(), out storeId);
            // return newly generated Store Id
            return storeId;
        }

        /// <summary>
        /// Updates the Store.
        /// </summary>
        /// <param name="brand">The Store.</param>
        public int UpdateStore(Store store)
        {
            int storeId;
            String sqlCommand = DBConstants.UPDATE_STORE_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "in_Store_Id", DbType.Int32, store.StoreId);
            myDataBase.AddInParameter(dbCommand, "in_Store_Name", DbType.String, store.StoreName);
            myDataBase.AddInParameter(dbCommand, "in_Modified_By", DbType.String, store.ModifiedBy);
            myDataBase.AddOutParameter(dbCommand, "out_Store_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Store_Id").ToString(), out storeId);

            return storeId;
        }

        /// <summary>
        /// Deletes the Store.
        /// </summary>
        /// <param name="StoreId">The Store id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteStore(int storeId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_STORE_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);
            myDataBase.AddInParameter(dbCommand, "in_Store_Id", DbType.Int32, storeId);
            myDataBase.AddInParameter(dbCommand, "in_ModifiedBy", DbType.String, modifiedBy);
            myDataBase.AddInParameter(dbCommand, "in_ModifiedDate", DbType.DateTime, modifiedOn);
            myDataBase.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// Reads the Store.
        /// </summary>
        /// <returns></returns>
        public List<Store> ReadStores(int? storeId)
        {
            List<Store> stores = new List<Store>();
            Store store = null;

            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_STORE_MASTER);
            myDataBase.AddInParameter(dbCommand, "in_Store_Id", DbType.Int32, storeId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    store = GenerateStoreFromDataReader(reader);
                    stores.Add(store);
                }
            }
            return stores;
        }

        /// <summary>
        ///  Reads the Store.
        /// </summary>
        /// <param name="itemModelId">Model Specification Id</param>
        /// <returns></returns>
        public List<Store> ReadStoresBasedOnStock(int itemModelId)
        {
            List<Store> stores = new List<Store>();
            Store store = null;

            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_STORE_BASED_ON_STOCK_MASTER);
            myDataBase.AddInParameter(dbCommand, "in_Item_Model_Id", DbType.Int32, itemModelId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    store = GenerateStoreFromDataReader(reader);
                    stores.Add(store);
                }
            }
            return stores;
        }



        ///// <summary>
        /////  Validate Whether this brand could be deleteds or not
        ///// </summary>
        ///// <param name="brandId"></param>
        ///// <returns></returns>
        //public string ValidateBrand(int brandId)
        //{

        //    String sqlCommand = DBConstants.VALIDATE_BRAND_MASTER;
        //    DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);
        //    myDataBase.AddInParameter(dbCommand, "@in_Brand_Id", DbType.Int32, brandId);
        //    myDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
        //    myDataBase.ExecuteNonQuery(dbCommand);

        //    // return the error Code
        //    return Convert.ToString(myDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        //}


        #endregion

        #region Private Section

        /// <summary>
        /// Generate Store From Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <returns></returns>
        private Store GenerateStoreFromDataReader(IDataReader reader)
        {
            Store store = new Store();
            store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            store.StoreName = GetStringFromDataReader(reader, "Store_Name");
            store.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            store.CreatedDate = GetDateFromReader(reader, "Created_Date");
            store.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            store.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return store;
        }



        #endregion
    }
}
