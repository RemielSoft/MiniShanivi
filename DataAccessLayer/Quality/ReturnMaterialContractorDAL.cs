using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccessLayer
{
    public class ReturnMaterialContractorDAL : BaseDAL
    {
        #region Return Material Contractor

        #region  Private Global Varriable
        private Database myDatabase;
        MetaData metaData = null;
        DbCommand dbCommand = null;
        QuotationDOM quotationDOM = new QuotationDOM();
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
        public ReturnMaterialContractorDAL(Database database)
        {
            myDatabase = database;

        }





        #endregion



        #region Return Material Contractor CRUD Methods
        #endregion

        public MetaData CreateReturnMaterialContractor(MaterialConsumptionNoteDom materialConsumption, Int32? ReturnMaterialContractorId)
        {
            //CreateMaterialConsumption

            string SqlCommand = DBConstants.CREATE_RETURN_MATERIAL_CONTRACTOE;
            dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
            if (ReturnMaterialContractorId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Contractor_Id", DbType.Int32, ReturnMaterialContractorId);
            }
            myDatabase.AddInParameter(dbCommand, "@in_ContractorWO_No", DbType.String, materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractorId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Name", DbType.String, materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractorName);
            myDatabase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, materialConsumption.IssueMaterial.DemandVoucher.Quotation.ContractNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Id", DbType.Int32, materialConsumption.IssueMaterial.IssueMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Number", DbType.String, materialConsumption.IssueMaterial.IssueMaterialNumber);
            if (materialConsumption.MaterialConsumptionDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Date", DbType.DateTime, materialConsumption.MaterialConsumptionDate);
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
                    metaData = GenerateReturnMaterialContractorFromDataReader(reader);
                    //GenerateMaterialConsumptionFromDataReader
                }
            }
            return metaData;
        }

        public Int32 CreateReturnMaterialContractorMapping(List<ItemTransaction> lstItemTransaction, Int32 ReturnMaterialContractorId)
        {

            //CreateMaterialConsumptionMapping

            Id = 0;
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                //string SqlCommand = DBConstants.CREATE_MATERIAL_CONSUMPTION_NOTES_MAPPING;
                string SqlCommand = DBConstants.CREATE_RETURN_MATERIAL_CONTRACTOR_MAPPING;
                dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Contractor_Id", DbType.Int32, ReturnMaterialContractorId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Mapping_Contractor_ID", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Mapping_Contractor_ID", DbType.Int32, DBNull.Value);
                }
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Id", DbType.Int32, itemTransaction.DeliverySchedule.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Description", DbType.String, itemTransaction.DeliverySchedule.ActivityDescription);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category", DbType.String, itemTransaction.Item.ModelSpecification.Category.ItemCategoryName);
                myDatabase.AddInParameter(dbCommand, "@in_ItemId", DbType.Int32, itemTransaction.Item.ItemId);
                myDatabase.AddInParameter(dbCommand, "@in_Item", DbType.String, itemTransaction.Item.ItemName);
                myDatabase.AddInParameter(dbCommand, "@in_Remark", DbType.String, itemTransaction.Remark);
                myDatabase.AddInParameter(dbCommand, "@in_Specification", DbType.String, itemTransaction.Item.ModelSpecification.ModelSpecificationName);
                myDatabase.AddInParameter(dbCommand, "@in_Specification_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.ModelSpecificationId);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Measurement_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.UnitMeasurement.Id);

                
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Measurement", DbType.String, itemTransaction.Item.ModelSpecification.UnitMeasurement.Name);
                myDatabase.AddInParameter(dbCommand, "@in_Store_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Store.StoreId);
                myDatabase.AddInParameter(dbCommand, "@in_Brand_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Brand.BrandId);
                myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_Per_Unit_Cost", DbType.Decimal, itemTransaction.PerUnitCost);
                // myDatabase.AddInParameter(dbCommand, "@in_Lost_Unit", DbType.Decimal, itemTransaction.LostUnit);
                // myDatabase.AddInParameter(dbCommand, "@in_Total_LostUnit_Amount", DbType.Decimal, itemTransaction.TotalAmount);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Issued", DbType.Decimal, itemTransaction.UnitIssued);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Return", DbType.Decimal, itemTransaction.QuantityReturned);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Return_Material_Mapping_Contractor_ID", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);
                Int32.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Return_Material_Mapping_Contractor_ID").ToString(), out Id);
            }
            return Id;
        }

        //public List<ItemTransaction> ReadIssueMaterialMappingConsumption(String IssueMaterialNo)
        //{
        //    lstItemTransaction = new List<ItemTransaction>();
        //    dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_MATERIAL_MAPPING_CONSUMPTION);
        //    //myDatabase.AddInParameter(dbCommand, "@in_IssueMaterial_Id", DbType.Int32, IssueMaterialId);
        //    myDatabase.AddInParameter(dbCommand, "@in_ContractorWorkOrder_No", DbType.String, IssueMaterialNo);
        //    using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            itemTransaction = new ItemTransaction();
        //            itemTransaction = GenerateIssueMaterialMappingConsumptionFromDataReader(reader);
        //            lstItemTransaction.Add(itemTransaction);
        //        }
        //    }
        //    return lstItemTransaction;
        //}

        //public List<IssueMaterialDOM> ReadIssueMaterialConsumption(String WorkOrderNo)
        //{
        //    lstissueMaterial = new List<IssueMaterialDOM>();
        //    dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_MATERIAL_CONSUMPTION);
        //    myDatabase.AddInParameter(dbCommand, "@in_ContractorWorkOrder_No", DbType.String, WorkOrderNo);
        //    using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            issueMaterialDOM = new IssueMaterialDOM();
        //            issueMaterialDOM = GenerateIssueMaterialConsumptionFromDataReader(reader);
        //            lstissueMaterial.Add(issueMaterialDOM);
        //        }
        //    }
        //    return lstissueMaterial;
        //}

        public List<MaterialConsumptionNoteDom> ReadReturnMaterialContractor(Int32? ReturnMaterialContractorId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String Return_Material_Number_Contractor)
        {

            //ReadMaterialConsumption
            lstMaterialConsumptionNoteDom = new List<MaterialConsumptionNoteDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_RETURN_MATERIAL_CONTRACTOR);
            if (ReturnMaterialContractorId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Contractor_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Contractor_Id", DbType.Int32, ReturnMaterialContractorId);
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
            if (Return_Material_Number_Contractor == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Number_Contractor", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Number_Contractor", DbType.String, Return_Material_Number_Contractor);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    //materialConsumptionsNote = GenerateMaterialConsumptionFromReader(reader);
                    materialConsumptionsNote = GenerateReturnMaterialContractorFromReader(reader);

                    lstMaterialConsumptionNoteDom.Add(materialConsumptionsNote);
                }
            }
            return lstMaterialConsumptionNoteDom;
        }

        //public void ResetMaterialConsumptionMapping(Int32? MaterialConsumptionId)
        //{
        //    string sqlCommand = DBConstants.RESET_MATERIAL_CONSUMPTION_NOTES_MAPPING;
        //    DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
        //    myDatabase.AddInParameter(dbCommand, "@in_Material_Consumption_Id", DbType.Int32, MaterialConsumptionId);
        //    myDatabase.ExecuteNonQuery(dbCommand);
        //}

        public void DeleteReturnMaterialContractor(int ReturnMaterialContractorId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_RETURN_MATERIAL_CONTRACTOR;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Contractor_Id", DbType.Int32, ReturnMaterialContractorId);
            myDatabase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDatabase.AddInParameter(dbCommand, "@in_Modified_Date", DbType.DateTime, modifiedOn);
            myDatabase.ExecuteNonQuery(dbCommand);
        }



        public List<ItemTransaction> ReadReturnMaterialContractorMapping(Int32? ReturnMaterialContractorId)
        {
            //ReadMaterialConsumptionMapping
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_RETURN_MATERIAL_CONTRACTOR_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Contractor_Id", DbType.Int32, ReturnMaterialContractorId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = new ItemTransaction();
                    itemTransaction = GenerateIssueReturnMaterialContractorMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }


        public List<ItemTransaction> ReadIssueMaterialMappingConsumption(String ContractorQuotNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_RETURN_MATERIAL_CONTRACTOR_MAPPING);
            //myDatabase.AddInParameter(dbCommand, "@in_IssueMaterial_Id", DbType.Int32, IssueMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_Work_Order_Number", DbType.String, ContractorQuotNo);
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


        // Method used 4 update Stock also
        public Int32 UpdateReturnMaterialContractorStatus(MaterialConsumptionNoteDom materialConsumption)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_RETURN_MATERIAL_CONTRACTOR_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_ReturnMaterial_Id", DbType.Int32, materialConsumption.ReturnMaterialContractorId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, materialConsumption.IssueMaterial.DemandVoucher.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, materialConsumption.IssueMaterial.DemandVoucher.Quotation.GeneratedBy);
            myDatabase.AddOutParameter(dbCommand, "@out_ReturnMaterialId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_ReturnMaterialId").ToString(), out Id);
            return Id;
        }



        public void UpdateStockReceiveIssueQuantity(Int32 itemId, Int32 itemSpecificationId, Int32 unitMeasurementId, String ItemUnitText, Decimal quantity, Int32 stockUpdateType,int storeId,int brandId, String createdBy)
        {
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_STOCK_RECEIVE_ISSUE_MATERIAL);
            myDatabase.AddInParameter(dbCommand, "in_Item_Id", DbType.Int32, itemId);
            myDatabase.AddInParameter(dbCommand, "in_item_model_id", DbType.Int32, itemSpecificationId);
            myDatabase.AddInParameter(dbCommand, "in_Unit_Measurement_Id", DbType.Int32, unitMeasurementId);
            myDatabase.AddInParameter(dbCommand, "in_Item_Unit", DbType.String, ItemUnitText);
            myDatabase.AddInParameter(dbCommand, "in_Quantity", DbType.Decimal, quantity);
            myDatabase.AddInParameter(dbCommand, "in_stock_Update_Type", DbType.Int32, stockUpdateType);
            myDatabase.AddInParameter(dbCommand, "in_Store_Id", DbType.Int32, storeId);
            myDatabase.AddInParameter(dbCommand, "in_Brand_Id", DbType.Int32, brandId);
            myDatabase.AddInParameter(dbCommand, "in_Created_By", DbType.String, createdBy);
            myDatabase.ExecuteNonQuery(dbCommand);
        }



        #region Private Method


        private ItemTransaction GenerateIssueMaterialMappingConsumptionFromDataReader(IDataReader reader)
        {
            ItemTransaction itemTransaction = new ItemTransaction();

            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.MetaProperty.Name = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            //itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            itemTransaction.ConsumedUnit = GetDecimalFromDataReader(reader, "ConsumedUnit");

            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Unit_Left");

            if (itemTransaction.UnitLeft <= 0)
            {
                itemTransaction.UnitLeft = 0;
            }
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
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Name");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            return itemTransaction;
        }


        private ItemTransaction GenerateReturnMaterialContractorMappingFromDataReader(IDataReader reader)
        {


            //GenerateMaterialConsumptionMappingFromDataReader
            ItemTransaction itemTransaction = new ItemTransaction();
            itemTransaction.MetaProperty = new MetaData();

            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Material_Consumption_Mapping_Id");
            //This field is Used For the SSRS Reports
            itemTransaction.MetaProperty.Name = GetStringFromDataReader(reader, "Material_Consumption_Number");

            itemTransaction.ConsumedUnit = GetDecimalFromDataReader(reader, "Consumed_Unit");
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            itemTransaction.LostUnit = GetDecimalFromDataReader(reader, "Lost_Unit");
            itemTransaction.Remark = GetStringFromDataReader(reader, "Remarks");
            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "Total_LostUnit_Amount");

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Activity_Id");

            itemTransaction.Item = new Item();
            //itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");

            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            //itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "created_by");

            return itemTransaction;
        }

        private MaterialConsumptionNoteDom GenerateReturnMaterialContractorFromReader(IDataReader reader)
        {


            //GenerateMaterialConsumptionFromReader
            materialConsumptionNoteDom = new MaterialConsumptionNoteDom();

            materialConsumptionNoteDom.ReturnMaterialDate = GetDateFromReader(reader, "Return_Material_Date");
            materialConsumptionNoteDom.ReturnMaterialContractorNo = GetStringFromDataReader(reader, "Return_Material_Number_Contractor");
            materialConsumptionNoteDom.ReturnMaterialContractorId = GetIntegerFromDataReader(reader, "Return_Material_Contractor_Id");

            materialConsumptionNoteDom.IssueMaterial = new IssueMaterialDOM();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Remarks = GetStringFromDataReader(reader, "Remarks");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            materialConsumptionNoteDom.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");

            return materialConsumptionNoteDom;
        }

        private ItemTransaction GenerateIssueReturnMaterialContractorMappingFromDataReader(IDataReader reader)
        {

            ItemTransaction itemTransaction = new ItemTransaction();
            itemTransaction.Remark = GetStringFromDataReader(reader, "Remark");
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "created_by");
            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.MetaProperty.Name = GetStringFromDataReader(reader, "Return_Material_Number_Contractor");
            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Return_Material_Mapping_Contractor_ID");
            //itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            //itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Item_Return");
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Item_Return");

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Activity_Id");

            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "ItemId");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Unit_Measurement");
            itemTransaction.Item.ModelSpecification.Store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            itemTransaction.Item.ModelSpecification.Store.StoreName = GetStringFromDataReader(reader, "Store_Name");
            itemTransaction.Item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Specification_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            return itemTransaction;
        }

        private IssueMaterialDOM GenerateIssueReturnMaterialContractorFromDataReader(IDataReader reader)
        {


            //GenerateIssueMaterialConsumptionFromDataReader
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

        private MetaData GenerateReturnMaterialContractorFromDataReader(IDataReader reader)
        {
            //GenerateMaterialConsumptionFromDataReader
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;

        }



        #endregion









        #endregion

    }
}
