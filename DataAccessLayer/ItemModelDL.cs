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
    public class ItemModelDL : BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;
        ModelSpecification modelSpecification = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public ItemModelDL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        #region CRUD Method

        /// <summary>
        /// Creates the Item Model.
        /// </summary>
        /// <param name="itemModel">The Item Model.</param>
        /// <returns></returns>
        public int CreateItemModel(ModelSpecification itemModel)
        {
            int itemModelId;
            String sqlCommand = DBConstants.CREATE_ITEMMODEL;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@in_Name", DbType.String, itemModel.ModelSpecificationName);
            MyDataBase.AddInParameter(dbCommand, "@in_Description", DbType.String, itemModel.Description);
            MyDataBase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemModel.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_model_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_model_Id").ToString(), out itemModelId);

            return itemModelId;
        }
        /// <summary>
        /// Updates the itemModel.
        /// </summary>
        /// <param name="itemModel">The itemModel.</param>
        public int UpdateItemModel(ModelSpecification itemModel)
        {
            int itemModelId;
            String sqlCommand = DBConstants.UPDATE_ITEMMODEL;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@in_Model_Id", DbType.Int32, itemModel.ModelSpecificationId);
            MyDataBase.AddInParameter(dbCommand, "@in_Model_Name", DbType.String, itemModel.ModelSpecificationName);
            MyDataBase.AddInParameter(dbCommand, "@in_Description", DbType.String, itemModel.Description);
            //TODO: Add other input parameters to update 
            MyDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, itemModel.CreatedBy);
            MyDataBase.AddInParameter(dbCommand, "@in_Modified_Date", DbType.DateTime, itemModel.CreatedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_Model_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Model_Id").ToString(), out itemModelId);
            return itemModelId;
        }
        /// <summary>
        /// Deletes the itemModel.
        /// </summary>
        /// <param name="itemModelId">The itemModel id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteItemModel(int itemModelId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_ITEMMODEL;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@in_model_id", DbType.Int32, itemModelId);
            MyDataBase.AddInParameter(dbCommand, "@modified_By", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@modified_Date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// Reads the itemModel.
        /// </summary>
        /// <returns></returns>
        public List<ModelSpecification> ReadItemModel(int? itemId)
        {
            List<ModelSpecification> lstItemModel = new List<ModelSpecification>();
            ModelSpecification itemModel = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_SPECIFICATION_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@in_item_id", DbType.Int32, itemId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemModel = GenerateItemModelFromDataReader(reader);
                    lstItemModel.Add(itemModel);
                }
            }
            return lstItemModel;
        }
        /// <summary>
        /// Reads the ItemModel by id.
        /// </summary>
        /// <returns></returns>
        public ModelSpecification ReadItemModelById(int itemModelId)
        {
            ModelSpecification itemModel = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_ITEMMODEL);
            MyDataBase.AddInParameter(dbCommand, "@in_model_id", DbType.Int32, itemModelId);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemModel = GenerateItemModelFromDataReader(reader);
                }
            }
            return itemModel;
        }

        /// <summary>
        /// Read Make and Unit of Measurement on basis of ItemId and Model Id[Manveer,13.02.2013]
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ModelSpecification ReadMakeandUnitofMeasurement(Item item)
        {
            modelSpecification = new ModelSpecification();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_MAKE_UNIT_MEASUREMENT);
            MyDataBase.AddInParameter(dbCommand, "@in_Item_Model_Mapping_Id", DbType.Int32, item.ModelSpecification.ModelSpecificationId);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    modelSpecification = GenerateMakeandUnitMeasurement(reader);
                }
            }
            return modelSpecification;
        }

        public String ValidateItemModel(int itemModelId)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.VALIDATE_ITEMMODEL);
            MyDataBase.AddInParameter(dbCommand, "@in_item_model_Id", DbType.Int32, itemModelId);
            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            MyDataBase.ExecuteNonQuery(dbCommand);
            return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));

        }
        #endregion

        #region Private Section

        /// <summary>
        /// Generates the Item Model from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private ModelSpecification GenerateItemModelFromDataReader(IDataReader reader)
        {
            ModelSpecification itemModel = new ModelSpecification();
            itemModel.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
            itemModel.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            //itemModel.Description = GetStringFromDataReader(reader, "Description");
            return itemModel;
        }

        /// <summary>
        /// Read Make(Brand) and Unit of Measurement[Manveer,13.02.2013]
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private ModelSpecification GenerateMakeandUnitMeasurement(IDataReader reader)
        {
            modelSpecification = new ModelSpecification();
            modelSpecification.UnitMeasurement = new MetaData();
            modelSpecification.Category = new ItemCategory();

            modelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Make");

            modelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Name");
            modelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
            modelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Category_Id");
            modelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category");
            return modelSpecification;
        }

        #endregion
    }
}
