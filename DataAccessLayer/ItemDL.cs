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
    public class ItemDL : BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;
        Item item = new Item();
        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public ItemDL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        #region Item CRUD Method
        /// <summary>
        /// Creates the Item.
        /// </summary>
        /// <param name="Item">The Item.</param>
        /// <returns></returns>

        public int CreateItem(Item item)
        {
            int id;
            String sqlCommand = DBConstants.CREATE_ITEM_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@in_item_name", DbType.String, item.ItemName);
            MyDataBase.AddInParameter(dbCommand, "@in_item_description", DbType.String, item.ItemDescription);
            MyDataBase.AddInParameter(dbCommand, "@in_created_by", DbType.String, item.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_itemId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_itemId").ToString(), out id);

            return id;
        }

        public void CreateitemModel(List<ModelSpecification> lstModelSpecification, int id)
        {
            foreach (ModelSpecification itemModelSpecification in lstModelSpecification)
            {
                //itemModelSpecification.Category = new ItemCategory();
                String sqlCommand = DBConstants.CREATE_ITEM_SPECIFICATIONS_MAPPING_MASTER;
                DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
                MyDataBase.AddInParameter(dbCommand, "@Item_Id", DbType.Int32, id);
                //Commented by sarfaraz
                MyDataBase.AddInParameter(dbCommand, "@Unit_Measurement_Id", DbType.Int32, itemModelSpecification.UnitMeasurement.Id);
                MyDataBase.AddInParameter(dbCommand, "@Specification", DbType.String, itemModelSpecification.ModelSpecificationName);
                MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, itemModelSpecification.Category.ItemCategoryId);
                MyDataBase.AddInParameter(dbCommand, "@Category", DbType.String, itemModelSpecification.Category.ItemCategoryName);
                MyDataBase.AddInParameter(dbCommand, "@in_usage_value", DbType.Decimal, itemModelSpecification.CategoryUsageValue);
               // MyDataBase.AddInParameter(dbCommand, "@Make", DbType.String, itemModelSpecification.Brand);
                MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, itemModelSpecification.CreatedBy);
                //MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String,"Admin");
                MyDataBase.ExecuteNonQuery(dbCommand);
            }
        }
        //-------------------------Commented Jai on 5-03-2013------------------------
        //public string CreateitemModel(List<ModelSpecification> lstModelSpecification, int id)
        //{
        //    string a = string.Empty;
        //    string b = string.Empty;
        //    string Specification = string.Empty;
        //    foreach (ModelSpecification itemModelSpecification in lstModelSpecification)
        //    {
        //        //itemModelSpecification.Category = new ItemCategory();
        //        String sqlCommand = DBConstants.CREATE_ITEM_SPECIFICATIONS_MAPPING_MASTER;
        //        DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
        //        MyDataBase.AddInParameter(dbCommand, "@Item_Id", DbType.Int32, id);
        //        //Commented by sarfaraz
        //        MyDataBase.AddInParameter(dbCommand, "@Unit_Measurement_Id", DbType.Int32, itemModelSpecification.UnitMeasurement.Id);
        //        MyDataBase.AddInParameter(dbCommand, "@Specification", DbType.String, itemModelSpecification.ModelSpecificationName);
        //        MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, itemModelSpecification.Category.ItemCategoryId);
        //        MyDataBase.AddInParameter(dbCommand, "@Category", DbType.String, itemModelSpecification.Category.ItemCategoryName);
        //        MyDataBase.AddInParameter(dbCommand, "@in_usage_value", DbType.Decimal, itemModelSpecification.CategoryUsageValue);
        //        MyDataBase.AddInParameter(dbCommand, "@Make", DbType.String, itemModelSpecification.Brand);
        //        MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, itemModelSpecification.CreatedBy);
        //        //MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String,"Admin");
        //        MyDataBase.ExecuteNonQuery(dbCommand);

        //        a = Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_Specification"));
        //        if (string.IsNullOrEmpty(b))
        //        {
        //            b = b + a;
        //        }
        //        else
        //        {
        //            b = b + " , " + a;
        //        }

        //    }
        //    if (!string.IsNullOrEmpty(b))
        //    {
        //        Specification = "Specification Name" + " " + b + " Already Exists";
        //    }
        //    return Specification;
        //}
        //---------------------------------End----------------------------------------------
        public void UpdateModelSpecification(List<ModelSpecification> lstModelSpecification, int id)
        {
            foreach (ModelSpecification itemModelSpecification in lstModelSpecification)
            {
                //itemModelSpecification.Category = new ItemCategory();
                String sqlCommand = DBConstants.UPDATE_ITEM_SPECIFICATIONS_MAPPING_MASTER;
                DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
                MyDataBase.AddInParameter(dbCommand, "@Item_Id", DbType.Int32, id);
                MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.Int32, itemModelSpecification.ModelSpecificationId);
                //Commented by sarfaraz
                MyDataBase.AddInParameter(dbCommand, "@Unit_Measurement_Id", DbType.Int32, itemModelSpecification.UnitMeasurement.Id);
                MyDataBase.AddInParameter(dbCommand, "@Specification", DbType.String, itemModelSpecification.ModelSpecificationName);
                MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, itemModelSpecification.Category.ItemCategoryId);
                MyDataBase.AddInParameter(dbCommand, "@Category", DbType.String, itemModelSpecification.Category.ItemCategoryName);
                MyDataBase.AddInParameter(dbCommand, "@in_usage_value", DbType.Decimal, itemModelSpecification.CategoryUsageValue);
                MyDataBase.AddInParameter(dbCommand, "@Make", DbType.String, itemModelSpecification.Brand.BrandName);
                MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, itemModelSpecification.CreatedBy);

                //MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String,"Admin");
                MyDataBase.ExecuteNonQuery(dbCommand);
            }
        }
        //public void UpdateModelSpecification(Item item,int id)
        //{            
        //        //itemModelSpecification.Category = new ItemCategory();
        //        String sqlCommand = DBConstants.UPDATE_ITEM_MODEL_MAPPING;
        //        DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
        //        MyDataBase.AddInParameter(dbCommand, "@Item_Id", DbType.Int32,id);
        //        MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.Int32,item.ModelSpecification.ModelSpecificationId);
        //        //Commented by sarfaraz
        //        //item.ModelSpecification.UnitMeasurement = new MetaData();
        //        MyDataBase.AddInParameter(dbCommand, "@Unit_Measurement_Id", DbType.Int32,item.ModelSpecification.UnitMeasurement.Id);
        //        MyDataBase.AddInParameter(dbCommand, "@Specification", DbType.String,item.ModelSpecification.ModelSpecificationName);
        //        MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, item.ModelSpecification.Category.ItemCategoryId);
        //        MyDataBase.AddInParameter(dbCommand, "@Category", DbType.String, item.ModelSpecification.Category.ItemCategoryName);
        //        MyDataBase.AddInParameter(dbCommand, "@in_usage_value", DbType.Decimal,item.ModelSpecification.CategoryUsageValue);
        //        MyDataBase.AddInParameter(dbCommand, "@Make", DbType.String,item.ModelSpecification.Brand);
        //        MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, item.ModelSpecification.ModifiedBy);

        //        //MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String,"Admin");
        //        MyDataBase.ExecuteNonQuery(dbCommand);            
        //}
        //}
        public int UpdateItem(Item item)
        {
            int id;
            String sqlCommand = DBConstants.UPDATE_ITEM_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@in_item_Id", DbType.Int32, item.ItemId);
            MyDataBase.AddInParameter(dbCommand, "@in_item_name", DbType.String, item.ItemName);

            MyDataBase.AddInParameter(dbCommand, "@in_item_description", DbType.String, item.ItemDescription);
            MyDataBase.AddInParameter(dbCommand, "@in_modified_by", DbType.String, item.ModifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@in_modified_date", DbType.DateTime, item.ModifiedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_item_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_item_Id").ToString(), out id);

            return id;
        }

        public void DeleteItemModelsByid(int Itemid, string modifiedBy, DateTime modifiedOn)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.DELETE_ITEM_SPECIFICATIONS_BY_ID_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@itemId", DbType.Int32, Itemid);
            MyDataBase.AddInParameter(dbCommand, "@modifiedBy", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@modified_Date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);

        }
        //public void DeleteModelSpecifications(Int32 ItemId,Int32 mappingId,string modifiedBy)
        //{
        //    DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.DELETE_MODEL_SPECIFICATION);
        //    MyDataBase.AddInParameter(dbCommand, "@in_item_id", DbType.Int32, ItemId);
        //    MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.String, mappingId);
        //    MyDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
        //    MyDataBase.ExecuteNonQuery(dbCommand);
        //}
        public void DeleteModelSpecifications(Int32 mappingId, string modifiedBy)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.DELETE_SPECIFICATION_MASTER);

            MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.String, mappingId);
            MyDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            MyDataBase.ExecuteNonQuery(dbCommand);
        }
        public string ValidateModelSpecifications(Int32 mappingId)
        {
            string s = string.Empty;
            String sqlCommand = DBConstants.VALIDATE_SPECIFICATION_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            //MyDataBase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, ItemId);
            MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.Int32, mappingId);
            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            MyDataBase.ExecuteNonQuery(dbCommand);
            s = Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
            return s;
        }
        //public string ValidateModelSpecifications(Int32 ItemId, Int32 mappingId)
        //{
        //    string s = string.Empty;
        //    String sqlCommand = DBConstants.VALIDATE_MODEL_SPECIFICATION;
        //    DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
        //    MyDataBase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, ItemId);
        //    MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.Int32, mappingId);
        //    MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
        //    MyDataBase.ExecuteNonQuery(dbCommand);            
        //    s=Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        //    return s;
        //}
        //public void UpdateitemModels(List<ItemModel> lstItemModel, int id)
        //{
        //    foreach (ItemModel item in lstItemModel)
        //    {
        //        String sqlCommand = DBConstants.UPDATE_ITEM_MODELS;
        //        DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
        //        MyDataBase.AddInParameter(dbCommand, "@Item_Id", DbType.Int32, id);
        //        MyDataBase.AddInParameter(dbCommand, "@Item_Model_Id", DbType.Int32, item.ItemModelId);
        //        MyDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, item.CreatedBy);
        //        MyDataBase.ExecuteNonQuery(dbCommand);
        //    }
        //}
        public void DeleteItemModels(int Itemid)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.DELETE_ITEM_SPECIFICATIONS_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Item_Id", DbType.Int32, Itemid);
            MyDataBase.ExecuteNonQuery(dbCommand);

        }
        public List<ItemCategory> ReadRanges(Int32? categoryId)
        {
            List<ItemCategory> lstItemCategory = new List<ItemCategory>();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_RANGES);
            MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, categoryId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ItemCategory itemCategory = new ItemCategory();
                    itemCategory.ItemCategoryId = GetIntegerFromDataReader(reader, "Category_Id");

                    itemCategory.StartRange = GetIntegerFromDataReader(reader, "Start_Range");
                    itemCategory.Range = String.Concat(GetIntegerFromDataReader(reader, "Start_Range"), "-", GetIntegerFromDataReader(reader, "End_Range"), " (", GetStringFromDataReader(reader, "Category_Name"), ")");
                    itemCategory.EndRange = GetIntegerFromDataReader(reader, "End_Range");
                    itemCategory.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");
                    lstItemCategory.Add(itemCategory);
                }
            }
            return lstItemCategory;
        }
        public List<Brand> ReadBrands()
        {
            List<Brand> lstBrands = new List<Brand>();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_BRANDS);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Brand brand = new Brand();
                    brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
                    brand.BrandName = GetStringFromDataReader(reader, "Brand_Name");
                    lstBrands.Add(brand);
                }
            }
            return lstBrands;
        }

        public List<Item> ReadItem(Int32? itemId)
        {
            List<Item> lstItem = new List<Item>();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_ITEM_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@in_item_id", DbType.Int32, itemId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    item = GenerateItemDetailFromDataReader(reader);
                    lstItem.Add(item);
                }
            }
            return lstItem;
        }
        public List<Item> ReadItemByModelSpecificationId(Int32? ModelSpecificationId)
        {
            List<Item> lstItem = new List<Item>();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_ITEM_BY_SPECIFICATION_ID_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.Int32, ModelSpecificationId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    item = GenerateItemDetailWithSpecificationFromDataReader(reader);
                    lstItem.Add(item);
                }
            }
            return lstItem;
        }
        public List<ModelSpecification> ReadModelSpecification(int? itemId)
        {
            List<ModelSpecification> lstModelSpecification = new List<ModelSpecification>();
            ModelSpecification modelSpecification = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_SPECIFICATION_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@in_item_id", DbType.Int32, itemId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    modelSpecification = new ModelSpecification();
                    modelSpecification.Category = new ItemCategory();
                    modelSpecification.UnitMeasurement = new MetaData();
                    modelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
                    modelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
                    modelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Name");
                    modelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
                    modelSpecification.ModelCode = GetStringFromDataReader(reader, "Model_Code");
                    modelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Category_Id");
                    modelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");
                    modelSpecification.CategoryUsageValue = GetIntegerFromDataReader(reader, "Usage_Value");
                    modelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Make");
                    modelSpecification.CreatedBy = GetStringFromDataReader(reader, "Created_By");
                    //modelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Model_Name");
                    lstModelSpecification.Add(modelSpecification);
                }
            }
            return lstModelSpecification;
        }

        public List<Item> ReadItemCategoryById(int? CategoryId)
        {
            List<Item> lstItem = new List<Item>();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_ITEM_BY_CATEGORYID);
            MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, CategoryId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    item = ReadItemDetailByCategoryId(reader);
                    lstItem.Add(item);
                }
            }
            return lstItem;
        }
        public void DeleteItem(int Itemid, string modifiedBy, DateTime modifiedOn)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.DELETE_ITEM_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@in_itemId", DbType.Int32, Itemid);
            MyDataBase.AddInParameter(dbCommand, "@in_modifiedBy", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@in_modified_Date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);

        }
        //public string ValidateItem(int itemid)
        //{
        //    String sqlCommand = DBConstants.VALIDATE_ITEM;
        //    DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
        //    MyDataBase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, itemid);
        //    MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
        //    MyDataBase.ExecuteNonQuery(dbCommand);
        //    return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        //}
        public string ValidateItem(Int32 mappingId)
        {
            String sqlCommand = DBConstants.VALIDATE_ITEM_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.Int32, mappingId);
            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            MyDataBase.ExecuteNonQuery(dbCommand);
            return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        }
        public int FindItemIdBySpecificationId(int SpecificationId)
        {
            int itemId = 0;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.FIND_ITEMID_BY_SPECIFICATIONID);
            MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.Int32, SpecificationId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemId = GetIntegerFromDataReader(reader, "Item_Id");
                }
            }
            return itemId;
        }
        public int CountSpecificationIdsByItemId(int itemId)
        {
            int SpecificationIds = 0;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.COUNT_SPECIFICATIONIDS_BY_ITEMID);
            MyDataBase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, itemId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    SpecificationIds = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
                }
            }
            return SpecificationIds;
        }
        #endregion

        #region Public Section

        public int ValidateItem(String loginId, String password)
        {
            int userId;

            String sqlCommand = DBConstants.VALIDATE_ITEM_MASTER;
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

        private Item GenerateItemDetailWithSpecificationFromDataReader(IDataReader reader)
        {
            Item item = new Item();
            item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            item.ItemName = GetStringFromDataReader(reader, "Item_Name");
            item.ItemDescription = GetStringFromDataReader(reader, "Item_Description");
            item.ModelSpecification = new ModelSpecification();
            item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
            item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            item.ModelSpecification.ModelCode = GetStringFromDataReader(reader, "Model_Code");
            item.ModelSpecification.UnitMeasurement = new MetaData();
            item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
            item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Name");
            item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Make");
            item.ModelSpecification.Category = new ItemCategory();
            item.ModelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Category_Id");
            item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");

            item.ModelSpecification.CategoryUsageValue = GetIntegerFromDataReader(reader, "Usage_Value");
            return item;
        }
        private Item GenerateItemDetailFromDataReader(IDataReader reader)
        {
            Item item = new Item();
            item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            item.ItemName = GetStringFromDataReader(reader, "Item_Name");
            item.ItemDescription = GetStringFromDataReader(reader, "Item_Description");
            return item;
        }
        private Item ReadItemDetailByCategoryId(IDataReader reader)
        {
            Item item = new Item();
            item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            item.ItemName = GetStringFromDataReader(reader, "Item_Name");

            return item;
        }
        #endregion
    }
}
