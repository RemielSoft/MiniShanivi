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
    public class ItemCategoryBL:BaseBL
    {
         #region private global variable(s)

        private Database myDataBase;
        private ItemCategoryDL itemcategoryDL = null;

        #endregion

         #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public ItemCategoryBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            itemcategoryDL = new ItemCategoryDL(myDataBase);
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
            int itemcategoryId = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    itemcategoryId = itemcategoryDL.CreateItemCategory(itemcategory);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return itemcategoryId;
        }

        /// <summary>
        /// Updates the itemcategory.
        /// </summary>
        /// <param name="itemcategory">The itemcategory.</param>
        public int UpdateItemCategory(ItemCategory itemcategory)
        {
            int IsSuccesfull = 0;            
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {                    
                    IsSuccesfull = itemcategoryDL.UpdateItemCategory(itemcategory);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return IsSuccesfull;
        }

        /// <summary>
        /// Deletes the ItemCategory.
        /// </summary>
        /// <param name="ItemCategoryId">The ItemCategory id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public string DeleteItemCategory(int itemcategoryId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    errorMessage = itemcategoryDL.ValidateItemCategory(itemcategoryId);

                    if (errorMessage == "")
                    {
                        itemcategoryDL.DeleteItemCategory(itemcategoryId, modifiedBy, modifiedOn);
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
        /// Reads the ItemCategory.
        /// </summary>
        /// <returns></returns>
        public List<ItemCategory> ReadItemCategory()
        {
            List<ItemCategory> lst = new List<ItemCategory>();
            try
            {
                lst = itemcategoryDL.ReadItemCategory();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return lst;
        }

        /// <summary>
        /// Reads the ItemCategory by id.
        /// </summary>
        /// <param name="ItemCategoryId">The ItemCategory id.</param>
        /// <returns></returns>
        public ItemCategory ReadItemCategoryById(int itemcategryId)
        {
            ItemCategory itemcategry = new ItemCategory();
            try
            {
                itemcategry = itemcategoryDL.ReadItemCategoryById(itemcategryId);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return itemcategry;
        }
        #endregion
    }
}
