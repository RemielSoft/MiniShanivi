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
    public class BrandDL : BaseDAL
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
        public BrandDL(Database dataBase)
        {
            myDataBase = dataBase;
        }

        #endregion

        #region Brand CRUD Methods

        /// <summary>
        /// Creates the Brand.
        /// </summary>
        /// <param name="brand">The Brand.</param>
        /// <returns></returns>
        public int CreateBrand(Brand brand)
        {
            int brandId;
            String sqlCommand = DBConstants.CREATE_BRAND_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "in_Brand_Name", DbType.String, brand.BrandName);
            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, brand.CreatedBy);
            myDataBase.AddOutParameter(dbCommand, "out_BrandId", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "out_BrandId").ToString(), out brandId);
            // return newly generated brand Id
            return brandId;
        }

        /// <summary>
        /// Updates the Brand.
        /// </summary>
        /// <param name="brand">The Brand.</param>
        public int UpdateBrand(Brand brand)
        {
            int brandId;
            String sqlCommand = DBConstants.UPDATE_BRAND_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "in_Brand_Id", DbType.Int32, brand.BrandId);
            myDataBase.AddInParameter(dbCommand, "in_Brand_Name", DbType.String, brand.BrandName);
            myDataBase.AddInParameter(dbCommand, "in_Modified_By", DbType.String, brand.ModifiedBy);
            myDataBase.AddOutParameter(dbCommand, "out_Brand_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Brand_Id").ToString(), out brandId);

            return brandId;
        }

        /// <summary>
        /// Deletes the brand.
        /// </summary>
        /// <param name="brandId">The brand id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteBrand(int brandId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_BRAND_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);
            myDataBase.AddInParameter(dbCommand, "in_Brand_Id", DbType.Int32, brandId);
            myDataBase.AddInParameter(dbCommand, "in_ModifiedBy", DbType.String, modifiedBy);
            myDataBase.AddInParameter(dbCommand, "in_ModifiedDate", DbType.DateTime, modifiedOn);
            myDataBase.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// Reads the Brands.
        /// </summary>
        /// <returns></returns>
        public List<Brand> ReadBrands(int? brandId)
        {
            List<Brand> brands = new List<Brand>();
            Brand brand = null;

            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_BRAND_MASTER);
            myDataBase.AddInParameter(dbCommand, "in_Brand_Id", DbType.Int32, brandId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    brand = GenerateBrandFromDataReader(reader);
                    brands.Add(brand);
                }
            }
            return brands;
        }


        /// <summary>
        /// Create Item Brands
        /// </summary>
        /// <param name="brand">The Brand.</param>
        /// <returns></returns>
        public int CreateItemBrands(ModelSpecification itemSpecification)
        {
            int isSuccess;
            String sqlCommand = DBConstants.CREATE_ITEM_BRAND_MAPPING;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);

            myDataBase.AddInParameter(dbCommand, "in_Item_Model_Id", DbType.String, itemSpecification.ModelSpecificationId);
            myDataBase.AddInParameter(dbCommand, "in_Brand_Id", DbType.String, itemSpecification.Brand.BrandId);
            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, itemSpecification.CreatedBy);
            myDataBase.AddOutParameter(dbCommand, "out_Success", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "out_Success").ToString(), out isSuccess);
            // return newly generated brand Id
            return isSuccess;
        }



        /// <summary>
        /// Read Brand By Item Specification Id
        /// </summary>
        /// <param name="itemSpecificationId"></param>
        /// <returns></returns>
        public List<ModelSpecification> ReadItemBrandsById(int itemSpecificationId, int? issueMaterialId, bool isStockCheckRequired, int storeId)
        {
            List<ModelSpecification> itemSpecifications = new List<ModelSpecification>();
            ModelSpecification itemSpecification = null;

            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_ITEM_BRAND_MAPPING);
            myDataBase.AddInParameter(dbCommand, "in_Item_Model_Id", DbType.Int32, itemSpecificationId);
            myDataBase.AddInParameter(dbCommand, "in_IssueMaterial_Id", DbType.Int32, issueMaterialId);
            myDataBase.AddInParameter(dbCommand, "in_IsStockCheckRequired", DbType.Boolean, isStockCheckRequired);
            myDataBase.AddInParameter(dbCommand, "in_Store_Id", DbType.Int32, storeId);


            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemSpecification = GenerateItemModelSpecificationFromDataReader(reader);
                    itemSpecifications.Add(itemSpecification);
                }
            }
            return itemSpecifications;
        }



        /// <summary>
        /// Deletes the brand.
        /// </summary>
        /// <param name="brandId">The brand id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteItemBrands(int itemSpecificationId, int brandId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_ITEM_BRAND_MAPPING;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);
            myDataBase.AddInParameter(dbCommand, "in_Item_Model_Id", DbType.Int32, itemSpecificationId);
            myDataBase.AddInParameter(dbCommand, "in_Brand_Id", DbType.Int32, brandId);
            myDataBase.AddInParameter(dbCommand, "in_Modified_By", DbType.String, modifiedBy);
            myDataBase.AddInParameter(dbCommand, "in_Modified_Date", DbType.DateTime, modifiedOn);
            myDataBase.ExecuteNonQuery(dbCommand);
        }


        /// <summary>
        ///  Validate Whether this brand could be deleteds or not
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public string ValidateBrand(int brandId)
        {

            String sqlCommand = DBConstants.VALIDATE_BRAND_MASTER;
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(sqlCommand);
            myDataBase.AddInParameter(dbCommand, "@in_Brand_Id", DbType.Int32, brandId);
            myDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            myDataBase.ExecuteNonQuery(dbCommand);

            // return the error Code
            return Convert.ToString(myDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        }


        #endregion

        #region Private Section

        /// <summary>
        /// Generate Brand From Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <returns></returns>
        private Brand GenerateBrandFromDataReader(IDataReader reader)
        {
            Brand brand = new Brand();
            brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            brand.BrandName = GetStringFromDataReader(reader, "Brand_Name");
            brand.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            brand.CreatedDate = GetDateFromReader(reader, "Created_Date");
            brand.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            brand.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return brand;
        }

        /// <summary>
        /// Generate Item Model Specification From Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <returns></returns>
        private ModelSpecification GenerateItemModelSpecificationFromDataReader(IDataReader reader)
        {
            ModelSpecification itemSpecification = new ModelSpecification();
            itemSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
            itemSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand_Name");
            return itemSpecification;
        }

        #endregion
    }
}
