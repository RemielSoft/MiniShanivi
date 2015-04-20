using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccessLayer.Invoice
{
    public class MaterialConsumptionNoteDAL : BaseDAL
    {
        #region Material Consumption

        #region  Private Global Varriable
        private Database myDatabase;
        MetaData metaData = null;
        DbCommand dbCommand = null;
        QuotationDOM quotationDOM = new QuotationDOM();
        //MaterialConsumptionNoteDom materialConsumptionsNote = null;
        List<IssueMaterialDOM> lstissueMaterial = null;
        IssueMaterialDOM issueMaterialDOM = null;
        MaterialConsumptionNoteDom materialConsumptionsNote = new MaterialConsumptionNoteDom();


        ItemTransaction itemTransaction = null;
        List<ItemTransaction> lstItemTransaction = null;
        MaterialConsumptionNoteDom materialConsumptionNoteDom = null;
        List<MaterialConsumptionNoteDom> lstMaterialConsumptionNoteDom = null;
        Int32 Id = 0;
        #endregion

        #region Constructors

        public MaterialConsumptionNoteDAL(Database database)
        {
            myDatabase = database;
        }

        #endregion

        #region Material Consumption CRUD Method

        public MetaData CreateMaterialConsumption(MaterialConsumptionNoteDom materialConsumption, Int32? MaterialConsumptionId)
        {
            string SqlCommand = DBConstants.CREATE_MATERIAL_CONSUMPTION_NOTES;
            dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
            if (MaterialConsumptionId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_MaterialConsumption_Id", DbType.Int32, MaterialConsumptionId);
            }
            myDatabase.AddInParameter(dbCommand, "@in_ContractorWO_No", DbType.String, materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractorId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Name", DbType.String, materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractorName);
            myDatabase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractNumber);
            if (materialConsumption.MaterialConsumptionDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Consumption_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_Consumption_Date", DbType.DateTime, materialConsumption.MaterialConsumptionDate);
            }

            myDatabase.AddInParameter(dbCommand, "@in_Document_Id", DbType.Int32, materialConsumption.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId);
            myDatabase.AddInParameter(dbCommand, "@in_Remark", DbType.String, materialConsumption.IssueMaterial.DemandVoucher.Remarks);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, materialConsumption.IssueMaterial.DemandVoucher.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, materialConsumption.CreatedBy);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData = GenerateMaterialConsumptionFromDataReader(reader);
                }
            }
            return metaData;
        }

        public Int32 CreateMaterialConsumptionMapping(List<ItemTransaction> lstItemTransaction, Int32 MaterialConsumptionId)
        {
            Id = 0;
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string SqlCommand = DBConstants.CREATE_MATERIAL_CONSUMPTION_NOTES_MAPPING;
                dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, MaterialConsumptionId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Mapping_Id", DbType.Int32, DBNull.Value);
                }
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Id", DbType.Int32, itemTransaction.DeliverySchedule.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Description", DbType.String, itemTransaction.DeliverySchedule.ActivityDescription);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category", DbType.String, itemTransaction.Item.ModelSpecification.Category.ItemCategoryName);
                myDatabase.AddInParameter(dbCommand, "@in_Item", DbType.String, itemTransaction.Item.ItemName);
                myDatabase.AddInParameter(dbCommand, "@in_Remark", DbType.String, itemTransaction.Remark);
                myDatabase.AddInParameter(dbCommand, "@in_Specification", DbType.String, itemTransaction.Item.ModelSpecification.ModelSpecificationName);
                myDatabase.AddInParameter(dbCommand, "@in_Brand_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Brand.BrandId);
                myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_Per_Unit_Cost", DbType.Decimal, itemTransaction.PerUnitCost);
                myDatabase.AddInParameter(dbCommand, "@in_Lost_Unit", DbType.Decimal, itemTransaction.LostUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Total_LostUnit_Amount", DbType.Decimal, itemTransaction.TotalAmount);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Issued", DbType.Decimal, itemTransaction.UnitIssued);
                myDatabase.AddInParameter(dbCommand, "@in_Consumed_Unit", DbType.Decimal, itemTransaction.ConsumedUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Material_Consumption_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);
                Int32.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Material_Consumption_Mapping_Id").ToString(), out Id);
            }
            return Id;
        }

        public List<ItemTransaction> ReadIssueMaterialMappingConsumption(String ContractorQuotNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_MATERIAL_MAPPING_CONSUMPTION);
            //myDatabase.AddInParameter(dbCommand, "@in_IssueMaterial_Id", DbType.Int32, IssueMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_ContractorWorkOrder_No", DbType.String, ContractorQuotNo);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = new ItemTransaction();
                    itemTransaction = GenerateIssueMaterialMappingConsumptionFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }

        public List<IssueMaterialDOM> ReadIssueMaterialConsumption(String WorkOrderNo)
        {
            lstissueMaterial = new List<IssueMaterialDOM>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_MATERIAL_CONSUMPTION);
            myDatabase.AddInParameter(dbCommand, "@in_ContractorWorkOrder_No", DbType.String, WorkOrderNo);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    issueMaterialDOM = new IssueMaterialDOM();
                    issueMaterialDOM = GenerateIssueMaterialConsumptionFromDataReader(reader);
                    lstissueMaterial.Add(issueMaterialDOM);
                }
            }
            return lstissueMaterial;
        }

        public List<MaterialConsumptionNoteDom> ReadMaterialConsumption(Int32? MaterialConsumptionId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String MaterialConsumptionNo)
        {
            lstMaterialConsumptionNoteDom = new List<MaterialConsumptionNoteDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_MATERIAL_RECONCILATION_REPORT);
            if (MaterialConsumptionId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, MaterialConsumptionId);
            if (ContractorId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, ContractorId);
            if (ToDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_ToDate", DbType.DateTime, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_ToDate", DbType.DateTime, ToDate);
            if (FromDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, FromDate);
            if (ContractNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, ContractNo);
            if (MaterialConsumptionNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_No", DbType.String, MaterialConsumptionNo);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    materialConsumptionsNote = GenerateMaterialConsumptionFromReader(reader);
                    lstMaterialConsumptionNoteDom.Add(materialConsumptionsNote);
                }
            }
            return lstMaterialConsumptionNoteDom;
        }

        public void ResetMaterialConsumptionMapping(Int32? MaterialConsumptionId)
        {
            string sqlCommand = DBConstants.RESET_MATERIAL_CONSUMPTION_NOTES_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, MaterialConsumptionId);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        public List<ItemTransaction> ReadMaterialConsumptionMapping(Int32? MaterialConsumptionId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_MATERIAL_CONSUMPTION_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_MaterialConsumption_Id", DbType.Int32, MaterialConsumptionId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = new ItemTransaction();
                    itemTransaction = GenerateMaterialConsumptionMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }

        public Int32 UpdateMaterialReconciliationStatus(MaterialConsumptionNoteDom materialConsumptionDom)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_MATERIAL_RECONCILIATION_STATUS);

            myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, materialConsumptionDom.MaterialConsumptionId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, materialConsumptionDom.IssueMaterial.DemandVoucher.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, materialConsumptionDom.IssueMaterial.DemandVoucher.Quotation.GeneratedBy);
            myDatabase.AddOutParameter(dbCommand, "@out_Material_ConsumptionId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Material_ConsumptionId").ToString(), out Id);
            return Id;
        }

        public void DeleteMaterialConsumptionNotes(int MaterialConsumptionId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_MATERIAL_CONSUMPTION_NOTES;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, MaterialConsumptionId);
            myDatabase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDatabase.AddInParameter(dbCommand, "@in_Modified_Date", DbType.DateTime, modifiedOn);
            myDatabase.ExecuteNonQuery(dbCommand);
        }


        public List<MaterialConsumptionNoteDom> ReadMaterialConsumptionNotes(Int32? MaterialConsumptionId, String Material_Consumption_Number, String Contractor_Quotation_Number)
        {
            lstMaterialConsumptionNoteDom = new List<MaterialConsumptionNoteDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_MATERIAL_CONSUMPTION_NOTES);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, MaterialConsumptionId);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Number", DbType.String, Material_Consumption_Number);
            myDatabase.AddInParameter(dbCommand, "@in_Contract_Quotation_Number", DbType.String, Contractor_Quotation_Number);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    // issueDemandVoucherDOM = new IssueDemandVoucherDOM();
                    materialConsumptionNoteDom = new MaterialConsumptionNoteDom();
                    materialConsumptionNoteDom = GenerateMaterialConsumptionNotesFromDataReader(reader);
                    lstMaterialConsumptionNoteDom.Add(materialConsumptionNoteDom);

                }
            }
            return lstMaterialConsumptionNoteDom;
        }

        public void DeleteMaterialConsumptionNoteseMapping(Int32? MaterialConsumptionMappingId, int MCNID)
        {
            string sqlCommand = DBConstants.DELETE_MATERIAL_CONSUMPTION_NOTE_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Mapping_Id", DbType.Int32, MaterialConsumptionMappingId);
            myDatabase.AddInParameter(dbCommand, "@in_MCN_Id", DbType.Int32, MCNID);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        public List<ItemTransaction> ReadMaterialConsumptionNotesEditMapping(Int32? MaterialConsumptionId, String Material_Consumption_Number)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_MATERIAL_CONSUMPTION_NOTES_EDIT_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, MaterialConsumptionId);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Number", DbType.String, Material_Consumption_Number);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateMaterialConsumptionNotesEditMappingFromDataReader(reader);

                    lstItemTransaction.Add(itemTransaction);
                }
            }

            return lstItemTransaction;
        }

        #endregion

        #region Private Method

        private MaterialConsumptionNoteDom GenerateMaterialConsumptionNotesFromDataReader(IDataReader reader)
        {
            materialConsumptionNoteDom = new MaterialConsumptionNoteDom();
            materialConsumptionNoteDom.IssueMaterial = new IssueMaterialDOM();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            materialConsumptionNoteDom.MaterialConsumptionId = GetIntegerFromDataReader(reader, "Material_Consumption_Id");
            materialConsumptionNoteDom.MaterialConsumptionNo = GetStringFromDataReader(reader, "Material_Consumption_Number");
            materialConsumptionNoteDom.MaterialConsumptionDate = GetDateFromReader(reader, "Consumption_Date");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Remarks = GetStringFromDataReader(reader, "Remarks");
            //quotation.ContractorQuotationId = GetIntegerFromDataReader(reader, "Contractor_Purchase_Order_Id");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Document_Id");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            materialConsumptionNoteDom.MaterialConsumptionDate = GetDateFromReader(reader, "Consumption_Date");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");

            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");





            return materialConsumptionNoteDom;
        }

        private ItemTransaction GenerateMaterialConsumptionNotesEditMappingFromDataReader(IDataReader reader)
        {
            ItemTransaction itemTransaction = new ItemTransaction();
            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Material_Consumption_Mapping_Id");
            // itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "ActualNumberofUnit");
            itemTransaction.ConsumedUnit = GetDecimalFromDataReader(reader, "Consumed_Unit");
            if (itemTransaction.ConsumedUnit <= 0)
            {
                itemTransaction.ConsumedUnit = 0;
            }
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            if (itemTransaction.UnitIssued <= 0)
            {
                itemTransaction.UnitIssued = 0;
            }
            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Unit_Left");
            if (itemTransaction.UnitLeft <= 0)
            {
                itemTransaction.UnitLeft = 0;
            }
            itemTransaction.LostUnit = GetDecimalFromDataReader(reader, "Lost_Unit");
            if (itemTransaction.LostUnit <= 0)
            {
                itemTransaction.LostUnit = 0;
            }

            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Activity_Id");

            itemTransaction.Item = new Item();
            //itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.TaxId = GetIntegerFromDataReader(reader, "Material_Consumption_Id");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            // itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            //itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");



            return itemTransaction;

        }

        private ItemTransaction GenerateMaterialConsumptionMappingFromDataReader(IDataReader reader)
        {
            ItemTransaction itemTransaction = new ItemTransaction();
            itemTransaction.MetaProperty = new MetaData();

            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Material_Consumption_Mapping_Id");
            //This field is Used For the SSRS Reports
            itemTransaction.MetaProperty.Name = GetStringFromDataReader(reader, "Material_Consumption_Number");

            itemTransaction.ConsumedUnit = GetDecimalFromDataReader(reader, "Consumed_Unit");
            if (itemTransaction.ConsumedUnit <= 0)
            {
                itemTransaction.ConsumedUnit = 0;
            }
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            if (itemTransaction.UnitIssued <= 0)
            {
                itemTransaction.UnitIssued = 0;
            }
            itemTransaction.LostUnit = GetDecimalFromDataReader(reader, "Lost_Unit");
            if (itemTransaction.LostUnit <= 0)
            {
                itemTransaction.LostUnit = 0;
            }
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            if (itemTransaction.PerUnitCost <= 0)
            {
                itemTransaction.PerUnitCost = 0;
            }
            itemTransaction.Remark = GetStringFromDataReader(reader, "Remarks");
            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "Total_LostUnit_Amount");
            if (itemTransaction.TotalAmount <= 0)
            {
                itemTransaction.TotalAmount = 0;
            }

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Activity_Id");

            itemTransaction.Item = new Item();
            //itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");

            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            //itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "created_by");

            return itemTransaction;
        }

        private MaterialConsumptionNoteDom GenerateMaterialConsumptionFromReader(IDataReader reader)
        {
            materialConsumptionNoteDom = new MaterialConsumptionNoteDom();
            materialConsumptionNoteDom.MaterialConsumptionDate = GetDateFromReader(reader, "Consumption_Date");
            materialConsumptionNoteDom.MaterialConsumptionNo = GetStringFromDataReader(reader, "Material_Consumption_Number");
            materialConsumptionNoteDom.MaterialConsumptionId = GetIntegerFromDataReader(reader, "Material_Consumption_Id");

            materialConsumptionNoteDom.IssueMaterial = new IssueMaterialDOM();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Remarks = GetStringFromDataReader(reader, "Remarks");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");

            return materialConsumptionNoteDom;
        }

        private ItemTransaction GenerateIssueMaterialMappingConsumptionFromDataReader(IDataReader reader)
        {
            ItemTransaction itemTransaction = new ItemTransaction();

            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.MetaProperty.Name = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Unit_Left");

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Activity_Id");

            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            return itemTransaction;
        }

        private IssueMaterialDOM GenerateIssueMaterialConsumptionFromDataReader(IDataReader reader)
        {
            issueMaterialDOM = new IssueMaterialDOM();
            issueMaterialDOM.IssueMaterialId = GetIntegerFromDataReader(reader, "Issue_Material_Id");
            issueMaterialDOM.IssueMaterialDate = GetDateFromReader(reader, "Issue_Material_Date");

            issueMaterialDOM.DemandVoucher = new IssueDemandVoucherDOM();
            issueMaterialDOM.DemandVoucher.MaterialDemandDate = GetDateFromReader(reader, "Issue_Demand_Date");

            issueMaterialDOM.DemandVoucher.Quotation = new QuotationDOM();
            issueMaterialDOM.DemandVoucher.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Document_Id");
            issueMaterialDOM.DemandVoucher.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            issueMaterialDOM.DemandVoucher.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            issueMaterialDOM.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            issueMaterialDOM.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");

            issueMaterialDOM.DemandVoucher.Quotation.StatusType = new MetaData();
            issueMaterialDOM.DemandVoucher.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");

            return issueMaterialDOM;
        }

        private MetaData GenerateMaterialConsumptionFromDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }

        #endregion

        #endregion










    }

}


