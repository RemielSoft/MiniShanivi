using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using DataAccessLayer;
using DataAccessLayer.Quality;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BusinessAccessLayer.Quality
{
    public class ReturnMaterialContractorBL : BaseBL
    {

        #region Global Variable
        private Database myDatabase;
        //MaterialConsumptionNoteDAL materialConsumptionDAL = null;
        ReturnMaterialContractorDAL returnMaterialContractorDAL = null;
        IssueMaterialDAL issueMaterialDAL = null;
        MetaData metaData = null;
        List<MaterialConsumptionNoteDom> lstMaerialConsumptionNote = null;
        List<IssueMaterialDOM> lstissueMaterial = null;
        List<ItemTransaction> lstItemTransaction = null;
        int id = 0;

        #endregion

        #region Constructor

        public ReturnMaterialContractorBL()
        {
            myDatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            returnMaterialContractorDAL = new ReturnMaterialContractorDAL(myDatabase);
            issueMaterialDAL = new IssueMaterialDAL(myDatabase);
        }


        #endregion

        #region CRUD Method For ReturnMaterialContractor

        public MetaData CreateReturnMaterialContractor(MaterialConsumptionNoteDom materialConsumption, Int32? ReturnMaterialContractorId)
        {
            //CreateMaterialConsumption

            id = 0;
            metaData = new MetaData();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    metaData = returnMaterialContractorDAL.CreateReturnMaterialContractor(materialConsumption, ReturnMaterialContractorId);
                    if (metaData.Id > 0)
                    {
                        if (ReturnMaterialContractorId > 0)
                        {
                            //returnMaterialContractorDAL.ResetMaterialConsumptionMapping(ReturnMaterialContractorId);
                        }
                        id = returnMaterialContractorDAL.CreateReturnMaterialContractorMapping(materialConsumption.IssueMaterial.DemandVoucher.Quotation.ItemTransaction, metaData.Id);

                    }
                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return metaData;
        }


        public List<MaterialConsumptionNoteDom> ReadReturnMaterialContractor(Int32? ReturnMaterialContractorId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String Return_Material_Number_Contractor)
        {
            lstMaerialConsumptionNote = new List<MaterialConsumptionNoteDom>();
            try
            {
                lstMaerialConsumptionNote = returnMaterialContractorDAL.ReadReturnMaterialContractor(ReturnMaterialContractorId, ContractorId, ToDate, FromDate, ContractNo, Return_Material_Number_Contractor);
            }
            catch (Exception Exp)
            {
                throw Exp;
            }
            return lstMaerialConsumptionNote;
        }

        public string DeleteReturnMaterialContractor(int ReturnMaterialContractorId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {

                    if (errorMessage == "")
                    {


                        returnMaterialContractorDAL.DeleteReturnMaterialContractor(ReturnMaterialContractorId, modifiedBy, modifiedOn);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return errorMessage;
        }






        public List<ItemTransaction> ReadReturnMaterialContractorMapping(Int32? ReturnMaterialContractorId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = returnMaterialContractorDAL.ReadReturnMaterialContractorMapping(ReturnMaterialContractorId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }


        public Int32 UpdateReturnMaterialContractorStatus(MaterialConsumptionNoteDom materialConsumption)
        {
            Int32 ReturnMaterialContractorId;
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    //SupplierRecieveMaterialDAL supplierRecieveMaterialDAL = null;
                    //supplierRecieveMaterialDAL = new SupplierRecieveMaterialDAL();

                    List<ItemTransaction> lstItemTransaction = returnMaterialContractorDAL.ReadReturnMaterialContractorMapping(materialConsumption.ReturnMaterialContractorId);
                    foreach (ItemTransaction item in lstItemTransaction)
                    {
                        returnMaterialContractorDAL.UpdateStockReceiveIssueQuantity(item.Item.ItemId, item.Item.ModelSpecification.ModelSpecificationId, item.Item.ModelSpecification.UnitMeasurement.Id, item.Item.ModelSpecification.UnitMeasurement.Name, item.UnitLeft, Convert.ToInt32(StockUpdateType.StockReceive),item.Item.ModelSpecification.Store.StoreId,item.Item.ModelSpecification.Brand.BrandId, item.CreatedBy);
                    }
                    ReturnMaterialContractorId = returnMaterialContractorDAL.UpdateReturnMaterialContractorStatus(materialConsumption);

                    transactionScope.Complete();
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnMaterialContractorId;
        }


        public List<ItemTransaction> ReadIssueMaterialMappingConsumption(String ContractorQuotNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                //lstItemTransaction = materialConsumptionDAL.ReadIssueMaterialMappingConsumption(ContractorQuotNo);
                lstItemTransaction = returnMaterialContractorDAL.ReadIssueMaterialMappingConsumption(ContractorQuotNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }



        #endregion




        public void UpdateStockReceiveIssueQuantity(List<ItemTransaction> lstitemTransaction)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    foreach (var lstitem in lstitemTransaction)
                    {
                        issueMaterialDAL.UpdateStockReceiveIssueQuantity(lstitem.Item.ItemId, lstitem.Item.ModelSpecification.ModelSpecificationId, lstitem.Item.ModelSpecification.Store.StoreId, lstitem.Item.ModelSpecification.Brand.BrandId, lstitem.Item.ModelSpecification.UnitMeasurement.Id, lstitem.Item.ItemName, lstitem.NumberOfUnit, Convert.ToInt32(StockUpdateType.StockReceive), lstitem.CreatedBy);
                    }
                    transactionScope.Complete();
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
