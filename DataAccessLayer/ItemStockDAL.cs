using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DocumentObjectModel;
using System.Data.Common;
using System.Data;

namespace DataAccessLayer
{
    public class ItemStockDAL : BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;
        ItemStock itemStock = new ItemStock();

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public ItemStockDAL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        #region ItemStock CRUD

        public int CreateItemStock(ItemStock itemStock)
        {
            int id;
            String sqlCommand = DBConstants.CREATE_ITEMSTOCK_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Item_Id", DbType.Int32, itemStock.ItemId);
            MyDataBase.AddInParameter(dbCommand, "@Store_Id", DbType.Int32, itemStock.Store.StoreId);
            //MyDataBase.AddInParameter(dbCommand, "@Item_Category_Id", DbType.Int32, itemStock.ItemCategoryId);
            MyDataBase.AddInParameter(dbCommand, "@Item_Model_Mapping_Id", DbType.Int32, itemStock.ItemSpecificationId);
            MyDataBase.AddInParameter(dbCommand, "@Unit_Measurement_Id", DbType.Int32, itemStock.UnitMeasurementId);
            MyDataBase.AddInParameter(dbCommand, "@Item_Unit", DbType.String, itemStock.ItemUnit);
            MyDataBase.AddInParameter(dbCommand, "@Brand_Id", DbType.Int32, itemStock.Brand.BrandId);
            MyDataBase.AddInParameter(dbCommand, "@Quantity_Onhand", DbType.Int32, itemStock.QuantityOnhand);
            MyDataBase.AddInParameter(dbCommand, "@Minimum_Level", DbType.Int32, itemStock.MinimumLevel);
            MyDataBase.AddInParameter(dbCommand, "@Maximum_Level", DbType.Int32, itemStock.MaximumLevel);
            MyDataBase.AddInParameter(dbCommand, "@Reorder_Level", DbType.Int32, itemStock.ReorderLevel);
            MyDataBase.AddInParameter(dbCommand, "@Maximum_Consumption", DbType.Int32, itemStock.MaximumConsumption);
            MyDataBase.AddInParameter(dbCommand, "@Minimum_Consumption", DbType.Int32, itemStock.MinimumConsumption);
            MyDataBase.AddInParameter(dbCommand, "@Normal_Consumption", DbType.Int32, itemStock.NormalConsumption);
            MyDataBase.AddInParameter(dbCommand, "@Lead_Time", DbType.Int32, itemStock.LeadTime);
            MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, itemStock.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_Item_Stock_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Item_Stock_Id").ToString(), out id);

            return id;
        }
        public int UpdateItemStock(ItemStock itemStock)
        {
            int id;
            String sqlCommand = DBConstants.UPDATE_ITEMSTOCK_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Item_Stock_Id", DbType.Int32, itemStock.ItemStockId);
            MyDataBase.AddInParameter(dbCommand, "@Item_Id", DbType.Int32, itemStock.ItemId);
            MyDataBase.AddInParameter(dbCommand, "@Store_Id", DbType.Int32, itemStock.Store.StoreId);
            //MyDataBase.AddInParameter(dbCommand, "@Item_Category_Id", DbType.Int32, itemStock.ItemCategoryId);
            MyDataBase.AddInParameter(dbCommand, "@Item_Model_Mapping_Id", DbType.Int32, itemStock.ItemSpecificationId);
            MyDataBase.AddInParameter(dbCommand, "@Unit_Measurement_Id", DbType.Int32, itemStock.UnitMeasurementId);
            MyDataBase.AddInParameter(dbCommand, "@Item_Unit", DbType.String, itemStock.ItemUnit);
            MyDataBase.AddInParameter(dbCommand, "@Quantity_Onhand", DbType.Int32, itemStock.QuantityOnhand);
            MyDataBase.AddInParameter(dbCommand, "@Brand_Id", DbType.Int32, itemStock.Brand.BrandId);
            MyDataBase.AddInParameter(dbCommand, "@Minimum_Level", DbType.Int32, itemStock.MinimumLevel);
            MyDataBase.AddInParameter(dbCommand, "@Maximum_Level", DbType.Int32, itemStock.MaximumLevel);
            MyDataBase.AddInParameter(dbCommand, "@Reorder_Level", DbType.Int32, itemStock.ReorderLevel);
            MyDataBase.AddInParameter(dbCommand, "@Maximum_Consumption", DbType.Int32, itemStock.MaximumConsumption);
            MyDataBase.AddInParameter(dbCommand, "@Minimum_Consumption", DbType.Int32, itemStock.MinimumConsumption);
            MyDataBase.AddInParameter(dbCommand, "@Normal_Consumption", DbType.Int32, itemStock.NormalConsumption);
            MyDataBase.AddInParameter(dbCommand, "@Lead_Time", DbType.Int32, itemStock.LeadTime);
            MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, itemStock.ModifiedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_Item_Stock_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Item_Stock_Id").ToString(), out id);

            return id;
        }
        public List<ItemStock> ReadItemStock(Int32? itemstockid)
        {
            List<ItemStock> lstItemStock = new List<ItemStock>();

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_ITEMSTOCK_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Item_Stock_Id", DbType.Int32, itemstockid);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemStock = GenerateitemStockDetailFromDataReader(reader);
                    lstItemStock.Add(itemStock);
                }
            }
            return lstItemStock;
        }
        public List<ModelSpecification> ReadUnitMeasurementBySpecification(int? ModelspecificationId)
        {
            List<ModelSpecification> lstModelSpecification = new List<ModelSpecification>();
            ModelSpecification modelSpecification = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_SPECIFICATION_MASTER_FOR_UNITMEASUREMET);
            MyDataBase.AddInParameter(dbCommand, "@in_item_model_mapping_id", DbType.Int32, ModelspecificationId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    modelSpecification = new ModelSpecification();
                    modelSpecification.UnitMeasurement = new MetaData();
                    modelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
                    modelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
                    modelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Name");
                    modelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
                    lstModelSpecification.Add(modelSpecification);
                }
            }
            return lstModelSpecification;
        }
        public void DeleteItemStock(int itemstockid, string modifiedBy, DateTime modifiedOn)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.DELETE_ITEMSTOCK_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Item_Stock_Id", DbType.Int32, itemstockid);
            MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);

        }
        public string ValidateItemStock(int itemstockid)
        {

            String sqlCommand = DBConstants.VALIDATE_ITEMSTOCK_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);

            MyDataBase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, itemstockid);

            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            MyDataBase.ExecuteNonQuery(dbCommand);

            return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        }
        #endregion

        #region Public Section

        public int ValidateUser(String loginId, String password)
        {
            int userId;

            String sqlCommand = DBConstants.VALIDATE_USER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);

            MyDataBase.AddInParameter(dbCommand, "in_login_id", DbType.String, loginId);
            MyDataBase.AddInParameter(dbCommand, "in_password", DbType.String, password);
            MyDataBase.AddOutParameter(dbCommand, "@out_userId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_userId").ToString(), out userId);

            return userId;
        }

        #endregion

        #region Private Section

        private ItemStock GenerateitemStockDetailFromDataReader(IDataReader reader)
        {
            ItemStock itemStock = new ItemStock();
            itemStock.ItemStockId = GetIntegerFromDataReader(reader, "Item_Stock_Id");
            itemStock.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemStock.UnitMeasurementId = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
            itemStock.ItemName = GetStringFromDataReader(reader, "Item_Name");
            itemStock.ItemSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemStock.ItemSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
            //itemStock.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");
            itemStock.ItemUnit = GetStringFromDataReader(reader, "Item_Unit");
            itemStock.QuantityOnhand = GetIntegerFromDataReader(reader, "Quantity_Onhand");
            itemStock.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemStock.Brand.BrandName = GetStringFromDataReader(reader, "Brand_Name");
            itemStock.Store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            itemStock.Store.StoreName = GetStringFromDataReader(reader, "Store_Name");
            itemStock.MinimumLevel = GetIntegerFromDataReader(reader, "Minimum_Level");
            itemStock.MaximumLevel = GetIntegerFromDataReader(reader, "Maximum_Level");
            itemStock.ReorderLevel = GetIntegerFromDataReader(reader, "Reorder_Level");
            itemStock.MaximumConsumption = GetIntegerFromDataReader(reader, "Maximum_Consumption");
            itemStock.MinimumConsumption = GetIntegerFromDataReader(reader, "Minimum_Consumption");
            itemStock.NormalConsumption = GetIntegerFromDataReader(reader, "Normal_Consumption");
            itemStock.LeadTime = GetIntegerFromDataReader(reader, "Lead_Time");
            itemStock.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            itemStock.CreatedDate = GetDateFromReader(reader, "Created_Date");
            itemStock.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            itemStock.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            return itemStock;
        }

        #endregion
    }
}
