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
    public class BrandBL : BaseBL
    {
        #region private global variable(s)

        /// <summary>
        ///  Database Object
        /// </summary>
        private Database myDataBase;

        /// <summary>
        ///  Instance of DAL
        /// </summary>
        private BrandDL brandDAL = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandDAL"/> class.
        /// </summary>
        public BrandBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            brandDAL = new BrandDL(myDataBase);
        }

        #endregion

        #region Department CRUD Methods

        /// <summary>
        /// Creates the Brand.
        /// </summary>
        /// <param name="brand">The department.</param>
        /// <returns></returns>
        public int CreateBrand(Brand brand)
        {
            int id = 0;
            try
            {
                id = brandDAL.CreateBrand(brand);
            }
            catch (Exception exp)
            {

                Logger.Write(exp.Message);

            }
            return id;
        }

        /// <summary>
        /// Updates the Brand.
        /// </summary>
        /// <param name="brand">The Brand.</param>
        public int UpdateBrand(Brand brand)
        {
            int id = 0;
            try
            {
                id = brandDAL.UpdateBrand(brand);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }


            return id;
        }

        /// <summary>
        /// Deletes the Brand.
        /// </summary>
        /// <param name="locationId">The department id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public string DeleteBrand(int brandId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    // First Validate the Brand Wheteher it should be deleted
                    errorMessage = brandDAL.ValidateBrand(brandId);
                    if (errorMessage == "")
                    {
                        brandDAL.DeleteBrand(brandId, modifiedBy, modifiedOn);
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
        /// Reads the Brands.
        /// </summary>
        /// <returns></returns>
        public List<Brand> ReadBrands(int? brandId)
        {
            List<Brand> brands = new List<Brand>();
            try
            {
                brands = brandDAL.ReadBrands(brandId);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return brands;
        }


        /// <summary>
        /// Create Item Brands.
        /// </summary>
        /// <param name="brand">The itemSpecification.</param>
        /// <returns></returns>
        public int CreateItemBrands(ModelSpecification itemSpecification)
        {
            int id = 0;
            try
            {
                id = brandDAL.CreateItemBrands(itemSpecification);
            }
            catch (Exception exp)
            {

                Logger.Write(exp.Message);

            }
            return id;
        }

        /// <summary>
        /// Deletes the brand.
        /// </summary>
        /// <param name="brandId">The brand id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteItemBrands(int itemSpecificationId, int brandId, string modifiedBy, DateTime modifiedOn)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {

                    brandDAL.DeleteItemBrands(itemSpecificationId, brandId, modifiedBy, modifiedOn);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
        }
        /// <summary>
        /// Read Item Brands By Id
        /// </summary>
        /// <returns></returns>  
        public List<ModelSpecification> ReadItemBrandsById(int itemSpecificationId, int? issueMaterialId)
        {
            List<ModelSpecification> itemSpecifications = new List<ModelSpecification>();
            try
            {
                itemSpecifications = brandDAL.ReadItemBrandsById(itemSpecificationId, issueMaterialId, false, 0);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return itemSpecifications;
        }

        public List<ModelSpecification> ReadItemBrandsById(int itemSpecificationId, int? issueMaterialId, bool isStockCheckRequired, int storeId)
        {
            List<ModelSpecification> itemSpecifications = new List<ModelSpecification>();
            try
            {
                itemSpecifications = brandDAL.ReadItemBrandsById(itemSpecificationId, issueMaterialId, isStockCheckRequired, storeId);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return itemSpecifications;
        }

        #endregion
    }
}
