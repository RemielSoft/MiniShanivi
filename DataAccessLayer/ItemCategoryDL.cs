using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using DocumentObjectModel;
using System.Data;

namespace DataAccessLayer
{
    public class ItemCategoryDL:BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public ItemCategoryDL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        #region ItemCategory CRUD Methods

        /// <summary>
        /// Creates the ItemCategory.
        /// </summary>
        /// <param name="ItemCategory">The ItemCategory.</param>
        /// <returns></returns>
        public int CreateItemCategory(ItemCategory itemcategory)
        {
            int itemcategoryId;
            String sqlCommand = DBConstants.CREATE_ITEMCATEGORY_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Name", DbType.String, itemcategory.ItemCategoryName);
            MyDataBase.AddInParameter(dbCommand, "@Start_Range", DbType.Int32, itemcategory.StartRange);
            MyDataBase.AddInParameter(dbCommand, "@End_Range", DbType.Int32, itemcategory.EndRange);
            MyDataBase.AddInParameter(dbCommand, "@Description", DbType.String, itemcategory.Description);
            MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, itemcategory.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_itemcategoryId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_itemcategoryId").ToString(), out itemcategoryId);

            return itemcategoryId;
        }

        /// <summary>
        /// Updates the itemcategory.
        /// </summary>
        /// <param name="itemcategory">The itemcategory.</param>
        public int UpdateItemCategory(ItemCategory itemcategory)
        {
            int itemcategoryId;
            String sqlCommand = DBConstants.UPDATE_ITEMCATEGORY_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, itemcategory.ItemCategoryId);
            MyDataBase.AddInParameter(dbCommand, "@Category_Name", DbType.String, itemcategory.ItemCategoryName);
            MyDataBase.AddInParameter(dbCommand, "@Start_Range", DbType.Int32, itemcategory.StartRange);
            MyDataBase.AddInParameter(dbCommand, "@End_Range", DbType.Int32, itemcategory.EndRange);
            MyDataBase.AddInParameter(dbCommand, "@Description", DbType.String, itemcategory.Description);
            //TODO: Add other input parameters to update 
            MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, itemcategory.ModifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, itemcategory.ModifiedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_Category_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Category_Id").ToString(), out itemcategoryId);
            return itemcategoryId;
        }

        /// <summary>
        /// Deletes the itemcategory.
        /// </summary>
        /// <param name="itemcategoryId">The itemcategory id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteItemCategory(int itemcategoryId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_ITEMCATEGORY_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, itemcategoryId);
            MyDataBase.AddInParameter(dbCommand, "@modified_By", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@modified_Date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);
        }        

        /// <summary>
        /// Reads the itemcategory.
        /// </summary>
        /// <returns></returns>
        public List<ItemCategory> ReadItemCategory()
        {
            List<ItemCategory> ItemCategory = new List<ItemCategory>();
            ItemCategory itemcategory = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_ITEMCATEGORY_MASTER);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemcategory = GenerateItemFromDataReader(reader);
                    ItemCategory.Add(itemcategory);
                }
            }
            return ItemCategory;
        }

        /// <summary>
        /// Reads the ItemCategory by id.
        /// </summary>
        /// <returns></returns>
        public ItemCategory ReadItemCategoryById(int itemcategoryId)
        {
            ItemCategory itemcategory = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_ITEMCATEGORY_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, itemcategoryId);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemcategory = GenerateItemFromDataReader(reader);
                }
            }
            return itemcategory;
        }

        public String ValidateItemCategory(int itemcategoryId)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.VALIDATE_ITEM_CATEGORY_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@in_item_category_Id", DbType.Int32, itemcategoryId);
            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            MyDataBase.ExecuteNonQuery(dbCommand);
            return Convert.ToString(MyDataBase.GetParameterValue(dbCommand,"@out_errorCode"));

        }
        #endregion

        #region Private Section

        /// <summary>
        /// Generates the ItemCategory from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private ItemCategory GenerateItemFromDataReader(IDataReader reader)
        {
            ItemCategory itemcategory = new ItemCategory();
            itemcategory.ItemCategoryId = GetIntegerFromDataReader(reader, "Category_Id");
            itemcategory.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");
            itemcategory.StartRange = GetIntegerFromDataReader(reader, "Start_Range");
            itemcategory.EndRange = GetIntegerFromDataReader(reader, "End_Range");
            itemcategory.Description = GetStringFromDataReader(reader, "Description");
            return itemcategory;
        }

        #endregion

    }
}
