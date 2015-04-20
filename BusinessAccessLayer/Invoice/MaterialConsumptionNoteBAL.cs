using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using DataAccessLayer;
using DataAccessLayer.Quality;
using DataAccessLayer.Invoice;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BusinessAccessLayer.Invoice
{
    public class MaterialConsumptionNoteBAL : BaseBL
    {
        #region Global Varriable
        private Database myDatabase;
        MaterialConsumptionNoteDAL materialConsumptionDAL = null;
        MetaData metaData = null;
        List<MaterialConsumptionNoteDom> lstMaerialConsumptionNote = null;
        List<IssueMaterialDOM> lstissueMaterial = null;
        List<ItemTransaction> lstItemTransaction = null;
        int id = 0;

        #endregion

        #region Constructor

        public MaterialConsumptionNoteBAL()
        {
            myDatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            materialConsumptionDAL = new MaterialConsumptionNoteDAL(myDatabase);
        }

        #endregion

        #region CRUD Method For MaterialConsumtion

        public MetaData CreateMaterialConsumption(MaterialConsumptionNoteDom materialConsumption, Int32? MaterialConsumptionId)
        {
            id = 0;
            metaData = new MetaData();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    metaData = materialConsumptionDAL.CreateMaterialConsumption(materialConsumption, MaterialConsumptionId);
                    if (metaData.Id > 0)
                    {
                        if (MaterialConsumptionId > 0)
                        {
                            materialConsumptionDAL.ResetMaterialConsumptionMapping(MaterialConsumptionId);
                        }
                        id = materialConsumptionDAL.CreateMaterialConsumptionMapping(materialConsumption.IssueMaterial.DemandVoucher.Quotation.ItemTransaction, metaData.Id);
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

        public List<IssueMaterialDOM> ReadIssueMaterialConsumption(String WorkOrderNo)
        {
            lstissueMaterial = new List<IssueMaterialDOM>();
            try
            {
                lstissueMaterial = materialConsumptionDAL.ReadIssueMaterialConsumption(WorkOrderNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstissueMaterial;
        }

        public List<ItemTransaction> ReadIssueMaterialMappingConsumption(String ContractorQuotNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = materialConsumptionDAL.ReadIssueMaterialMappingConsumption(ContractorQuotNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }

        public List<MaterialConsumptionNoteDom> ReadMaterialConsumption(Int32? MaterialConsumptionId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String MaterialConsumptionNo)
        {
            lstMaerialConsumptionNote = new List<MaterialConsumptionNoteDom>();
            try
            {
                lstMaerialConsumptionNote = materialConsumptionDAL.ReadMaterialConsumption(MaterialConsumptionId, ContractorId, ToDate, FromDate, ContractNo, MaterialConsumptionNo);
            }
            catch (Exception Exp)
            {
                throw Exp;
            }
            return lstMaerialConsumptionNote;
        }

        public List<ItemTransaction> ReadMaterialConsumptionMapping(Int32? MaterialConsumptionId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {
                lstItemTransaction = materialConsumptionDAL.ReadMaterialConsumptionMapping(MaterialConsumptionId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }

        public Int32 UpdateMaterialReconciliationStatusBL(MaterialConsumptionNoteDom materialConsumptionNoteDom)
        {
            Int32 MaterialConsumptionId;
            try
            {
                MaterialConsumptionId = materialConsumptionDAL.UpdateMaterialReconciliationStatus(materialConsumptionNoteDom);
            }
            catch (Exception Exp)
            {

                throw Exp;
            }
            return MaterialConsumptionId;
        }

        public string DeleteMaterialConsumtionBL(int MaterialConsumptionId, string modifiedBy, DateTime modifiedOn)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {

                    if (errorMessage == "")
                    {

                        materialConsumptionDAL.DeleteMaterialConsumptionNotes(MaterialConsumptionId, modifiedBy, modifiedOn);
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
        // Edit
        public void DeleteMaterialConsumptionNotesMapping(int MaterialConsumptionMappingId, int MCNID)
        {
            materialConsumptionDAL.DeleteMaterialConsumptionNoteseMapping(MaterialConsumptionMappingId, MCNID);
        }

        public List<MaterialConsumptionNoteDom> ReadMaterialConsumptionNotes(Int32? MaterialConsumptionId, String Material_Consumption_Number, String Contractor_Quotation_Number)
        {

            lstMaerialConsumptionNote = new List<MaterialConsumptionNoteDom>();
            try
            {
                lstMaerialConsumptionNote = materialConsumptionDAL.ReadMaterialConsumptionNotes(MaterialConsumptionId, Material_Consumption_Number, Contractor_Quotation_Number);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstMaerialConsumptionNote;
        }

        public List<ItemTransaction> ReadMaterialConsumptionNotesEditMapping(Int32? MaterialConsumptionId, String Material_Consumption_Number)
        {
            lstItemTransaction = new List<ItemTransaction>();
            try
            {

                lstItemTransaction = materialConsumptionDAL.ReadMaterialConsumptionNotesEditMapping(MaterialConsumptionId, Material_Consumption_Number);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstItemTransaction;
        }

        #endregion









    }
}
