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
    public class ItemModelBL : BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private ItemModelDL itemmodelDL = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public ItemModelBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            itemmodelDL = new ItemModelDL(myDataBase);
        }

        #endregion

        #region CRUD Method

        /// <summary>
        /// Creates the ItemModel.
        /// </summary>
        /// <param name="ItemModel">The ItemModel.</param>
        /// <returns></returns>
        public int CreateItemModel(ModelSpecification itemModel)
        {
            int itemModelId = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    itemModelId = itemmodelDL.CreateItemModel(itemModel);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }

            return itemModelId;
        }

        /// <summary>
        /// Updates the itemModel.
        /// </summary>
        /// <param name="itemModel">The itemModel.</param>
        public int UpdateItemModel(ModelSpecification itemModel)
        {
            int IsSuccesfull = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    IsSuccesfull = itemmodelDL.UpdateItemModel(itemModel);
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
        /// Deletes the ItemModel.
        /// </summary>
        /// <param name="ItemModelId">The ItemModel id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public string DeleteItemModel(int ItemModelId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    //errorMessage = itemcategoryDL.ValidateGroup(itemcategoryId, GlobalConstants.C_ERROR_MESSAGE_GROUP_USEDIN_User);

                    if (errorMessage == "")
                    {
                        itemmodelDL.DeleteItemModel(ItemModelId, modifiedBy, modifiedOn);
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
        /// Reads the itemModel.
        /// </summary>
        /// <returns></returns>
        public List<ModelSpecification> ReadItemModel(int? id)
        {
            List<ModelSpecification> lst = new List<ModelSpecification>();
            try
            {
                lst = itemmodelDL.ReadItemModel(id);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return lst;
        }

        /// <summary>
        /// Reads the ItemModel by id.
        /// </summary>
        /// <param name="ItemModelId">The ItemModel id.</param>
        /// <returns></returns>
        public ModelSpecification ReadItemModelById(int itemModelId)
        {
            ModelSpecification itemModel = new ModelSpecification();
            try
            {
                itemModel = itemmodelDL.ReadItemModelById(itemModelId);
            }
            catch (Exception exp)
            {
                throw exp;
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
            ModelSpecification modelSpecification = new ModelSpecification();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {

                    modelSpecification = itemmodelDL.ReadMakeandUnitofMeasurement(item);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return modelSpecification;
        }

        #endregion
    }
}
