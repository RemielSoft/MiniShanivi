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
    public class ItemBL : BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private ItemDL itemDL = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        public ItemBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            itemDL = new ItemDL(myDataBase);
        }

        #endregion

        #region Item CRUD Methods

        /// <summary>
        /// Creates the Item.
        /// </summary>
        /// <param name="Item">The Item.</param>
        /// <returns></returns>
        public int CreateItem(Item item)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = itemDL.CreateItem(item);


                    if (id > 0)
                    {
                        //Commented by sarfaraz
                        itemDL.CreateitemModel(item.ModelSpecifications, id);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return id;
        }
        public int UpdateItem(Item item)
        {
            int id = 0;

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = itemDL.UpdateItem(item);
                    if (id > 0)
                    {
                        //Commented by Jai
                        itemDL.UpdateModelSpecification(item.ModelSpecifications, id);

                        // itemDL.UpdateModelSpecification(item, id);
                        //itemDL.DeleteModelSpecifications(id,item.ModelSpecification.ModelSpecificationId,item.ModifiedBy);
                        //itemDL.CreateitemModel(item.ModelSpecifications, id);
                        //

                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return id;
        }
        //public string DeleteModelSpecifications(Int32 ItemId, Int32 mappingId, string modifiedBy)
        //{
        //        string errorMessage = string.Empty;  
        //        try
        //        {
        //            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
        //            {
        //                errorMessage = itemDL.ValidateModelSpecifications(ItemId, mappingId);
        //                if (errorMessage == "")
        //                {
        //                    itemDL.DeleteModelSpecifications(ItemId, mappingId, modifiedBy);
        //                }
        //                scope.Complete();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        return errorMessage;
        //}
        public string DeleteModelSpecifications(Int32 mappingId, string modifiedBy)
        {
            string errorMessage = string.Empty;
            int itemId = 0;
            int SpecificationIds = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    itemId = itemDL.FindItemIdBySpecificationId(mappingId);
                    SpecificationIds = itemDL.CountSpecificationIdsByItemId(itemId);
                    errorMessage = itemDL.ValidateModelSpecifications(mappingId);
                    if (errorMessage == "" && SpecificationIds == 1)
                    {
                        errorMessage = itemDL.ValidateItem(mappingId);
                        if (errorMessage == "")
                        {
                            itemDL.DeleteModelSpecifications(mappingId, modifiedBy);
                        }
                    }
                    else if (errorMessage == "")
                    {
                        itemDL.DeleteModelSpecifications(mappingId, modifiedBy);
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errorMessage;
        }
        public List<ItemCategory> ReadRanges(Int32? categoryId)
        {
            List<ItemCategory> lstItemCategory = new List<ItemCategory>();
            try
            {
                lstItemCategory = itemDL.ReadRanges(categoryId);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstItemCategory;
        }

        public List<Brand> ReadBrands()
        {
            List<Brand> lstBrands = new List<Brand>();
            try
            {
                lstBrands = itemDL.ReadBrands();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstBrands;
        }

        public List<Item> ReadItemByModelSpecificationId(Int32? ModelSpecificationId)
        {
            List<Item> lstItem = new List<Item>();
            try
            {
                lstItem = itemDL.ReadItemByModelSpecificationId(ModelSpecificationId);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstItem;
        }
        public List<Item> ReadItem(Int32? Itemid)
        {

            List<Item> lstItem = new List<Item>();
            try
            {
                lstItem = itemDL.ReadItem(Itemid);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstItem;
        }
        public List<ModelSpecification> ReadModelSpecification(int? itemId)
        {
            List<ModelSpecification> lstModelSpecification = new List<ModelSpecification>();
            try
            {
                lstModelSpecification = itemDL.ReadModelSpecification(itemId);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstModelSpecification;
        }

        public Item ReaditemModelById(int itemId)
        {
            Item item = new Item();
            List<Item> lstItem = new List<Item>();
            lstItem = itemDL.ReadItem(itemId);
            foreach (Item items in lstItem)
            {
                item = items;
            }

            //foreach (ModelSpecification itemModel in itemDL.ReaditemModelById(itemId))
            //{
            //    item.ModelSpecifications.Add(itemModel);
            //}
            return item;
        }
        public List<Item> ReadItemByCategoryId(int? CategoryId)
        {
            List<Item> lstItem = new List<Item>();
            try
            {
                lstItem = itemDL.ReadItemCategoryById(CategoryId);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstItem;
        }
        public string DeleteItem(int itemid, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    //errorMessage = itemDL.ValidateItem(itemid);
                    if (errorMessage == "")
                    {
                        itemDL.DeleteItem(itemid, modifiedBy, modifiedOn);
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
