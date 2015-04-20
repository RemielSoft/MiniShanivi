using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccessLayer.Quality
{
    public class IssueMaterialDAL : BaseDAL
    {
        #region private Global Variables

        private Database myDatabase;
        DbCommand dbCommand = null;
        MetaData metaData = null;
        IssueMaterialDOM issueMaterial = null;
        List<IssueMaterialDOM> lstIssueMaterial = null;
        ItemTransaction itemTransaction = null;
        List<ItemTransaction> lstItemTransaction = null;
        Int32 Id = 0;
        #endregion

        #region Constructors

        public IssueMaterialDAL(Database database)
        {
            myDatabase = database;
        }

        #endregion

        #region Issue Material CRUD Methods

        public MetaData CreateIssueMaterial(IssueMaterialDOM issueMaterialDOM, Int32? IsssueMaterialId)
        {
            string SqlCommand = DBConstants.CREATE_ISSUE_MATERIAL;
            dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
            if (IsssueMaterialId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Id", DbType.Int32, IsssueMaterialId);
            }
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Demand_Voucher_Id", DbType.Int32, issueMaterialDOM.DemandVoucher.IssueDemandVoucherId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Quotation_Number", DbType.String, issueMaterialDOM.DemandVoucher.Quotation.ContractQuotationNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Demand_Voucher_Number", DbType.String, issueMaterialDOM.DemandVoucher.IssueDemandVoucherNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Document_Id", DbType.Int32, issueMaterialDOM.DemandVoucher.Quotation.UploadDocumentId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, issueMaterialDOM.DemandVoucher.Quotation.ContractorId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Name", DbType.String, issueMaterialDOM.DemandVoucher.Quotation.ContractorName);
            myDatabase.AddInParameter(dbCommand, "@in_Contract_Number", DbType.String, issueMaterialDOM.DemandVoucher.Quotation.ContractNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Demand_Date", DbType.DateTime, issueMaterialDOM.DemandVoucher.MaterialDemandDate);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Date", DbType.DateTime, issueMaterialDOM.IssueMaterialDate);
            myDatabase.AddInParameter(dbCommand, "@in_Remarks", DbType.String, issueMaterialDOM.DemandVoucher.Remarks);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, issueMaterialDOM.DemandVoucher.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, issueMaterialDOM.CreatedBy);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData = GenerateIssueMaterialFromDataReader(reader);
                }
            }
            return metaData;
        }
        public Int32 CreateIssueMaterialMapping(List<ItemTransaction> lstItemTransaction, Int32 IsssueMaterialId)
        {
            Id = 0;
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string SqlCommand = DBConstants.CREATE_ISSUE_MATERIAL_MAPPING;
                dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Id", DbType.Int32, IsssueMaterialId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Mapping_Id", DbType.Int32, DBNull.Value);
                }
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Id", DbType.Int32, itemTransaction.DeliverySchedule.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Description", DbType.String, itemTransaction.DeliverySchedule.ActivityDescription);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category", DbType.String, itemTransaction.Item.ModelSpecification.Category.ItemCategoryName);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, itemTransaction.Item.ItemId);
                myDatabase.AddInParameter(dbCommand, "@in_Item", DbType.String, itemTransaction.Item.ItemName);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Model_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.ModelSpecificationId);
                myDatabase.AddInParameter(dbCommand, "@in_Specification", DbType.String, itemTransaction.Item.ModelSpecification.ModelSpecificationName);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Measurement_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.UnitMeasurement.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Store_Id", DbType.String, itemTransaction.Item.ModelSpecification.Store.StoreId);
                myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_Brand_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Brand.BrandId);
                myDatabase.AddInParameter(dbCommand, "@in_Number_Of_Unit", DbType.Decimal, itemTransaction.NumberOfUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Demanded", DbType.Decimal, itemTransaction.UnitDemanded);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Issued", DbType.Decimal, itemTransaction.ItemRequired);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Issue_Material_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);

                Int32.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Issue_Material_Mapping_Id").ToString(), out Id);
            }
            return Id;
        }
        public void GenerateIssueMaterialNumber(int issueMaterialId, out string issueMaterialNumber)
        {

            string sqlCommand = DBConstants.GENERATE_ISSUE_MATERIAL_NUMBER;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Id", DbType.Int32, issueMaterialId);
            myDatabase.AddOutParameter(dbCommand, "@out_IssueMaterial_Number", DbType.String, 30);
            myDatabase.ExecuteNonQuery(dbCommand);
            issueMaterialNumber = myDatabase.GetParameterValue(dbCommand, "@out_IssueMaterial_Number").ToString();
        }
        public void ResetIssueMaterialMapping(Int32? IssueMaterialId)
        {
            string sqlCommand = DBConstants.RESET_ISSUE_MATERIAL_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Id", DbType.Int32, IssueMaterialId);
            myDatabase.ExecuteNonQuery(dbCommand);
        }
        public List<IssueMaterialDOM> ReadIssueMaterial(Int32? IssueMaterialId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String IssueMaterialNo, String ContractorQuotNo)
        {
            lstIssueMaterial = new List<IssueMaterialDOM>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_MATERIAL);
            if (IssueMaterialId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Id", DbType.Int32, IssueMaterialId);
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
            {
                myDatabase.AddInParameter(dbCommand, "@in_ToDate", DbType.DateTime, ToDate);
            }
            if (FromDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, FromDate);
            }
            if (ContractNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, ContractNo);
            }
            if (IssueMaterialNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_IssueMaterial_No", DbType.String, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_IssueMaterial_No", DbType.String, IssueMaterialNo);
            }
            if (ContractorQuotNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Quot_no", DbType.String, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Quot_no", DbType.String, ContractorQuotNo);
            }
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    issueMaterial = GenerateIssueMaterialFromReader(reader);
                    lstIssueMaterial.Add(issueMaterial);
                }
            }
            return lstIssueMaterial;
        }
        public List<ItemTransaction> ReadIssueMaterialMapping(Int32? IssueMaterialId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_MATERIAL_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_IssueMaterial_Id", DbType.Int32, IssueMaterialId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = new ItemTransaction();
                    itemTransaction = GenerateIssueDemandVoucherMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }
        public IssueMaterialDOM ReadIssueMaterialByDemandVoucher(string demandVoucherNumber)
        {
            IssueMaterialDOM issueMaterialDOM = new IssueMaterialDOM();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_MATERIALBY_DEMAND_VOUCHER);
            myDatabase.AddInParameter(dbCommand, "@in_IssueDemandVoucher_Number", DbType.String, demandVoucherNumber);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {

                    issueMaterialDOM = GenerateIssueMaterialFromReader(reader);

                }
            }
            return issueMaterialDOM;
        }

        public int ReadStockStatus(Int32 ItemId, Int32 ItemModelId, Decimal QuantityIssued)
        {
            Id = 0;
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_STOCK_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, ItemId);
            myDatabase.AddInParameter(dbCommand, "@in_Item_Mode_Id", DbType.Int32, ItemModelId);
            myDatabase.AddInParameter(dbCommand, "@in_Quantity_Issued", DbType.Decimal, QuantityIssued);
            myDatabase.AddOutParameter(dbCommand, "@out_StockStatus", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            Int32.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_StockStatus").ToString(), out Id);
            return Id;

        }


        public int ReadStockStatus(Int32 ItemId, Int32 ItemModelId, int StoreId, int BrandId, out int StockAvailable)
        {

            StockAvailable = 0;
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_STOCK_STATUS_TEMP);
            myDatabase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, ItemId);
            myDatabase.AddInParameter(dbCommand, "@in_Item_Mode_Id", DbType.Int32, ItemModelId);
            myDatabase.AddInParameter(dbCommand, "@in_StoreId", DbType.Int32, StoreId);
            myDatabase.AddInParameter(dbCommand, "@In_BrandId", DbType.Int32, BrandId);
            myDatabase.AddOutParameter(dbCommand, "@out_StockStatus", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            Int32.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_StockStatus").ToString(), out StockAvailable);
            if (StockAvailable > 0)
            {
                return 1;
            }
            return 0;

        }


        public void DeleteIssueMaterial(int IssueMaterialId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_ISSUE_MATERIAL;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_IssueMaterial_Id", DbType.Int32, IssueMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDatabase.AddInParameter(dbCommand, "@in_Modified_Date", DbType.DateTime, modifiedOn);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        public void UpdateStockReceiveIssueQuantity(Int32 itemId, Int32 itemSpecificationId, Int32 storeId, Int32 brandId, Int32 unitMeasurementId, String ItemUnitText, Decimal quantity, Int32 stockUpdateType, String createdBy)
        {
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_STOCK_RECEIVE_ISSUE_MATERIAL);
            myDatabase.AddInParameter(dbCommand, "in_Item_Id", DbType.Int32, itemId);
            myDatabase.AddInParameter(dbCommand, "in_item_model_id", DbType.Int32, itemSpecificationId);
            // Brand,Store Introduced
            myDatabase.AddInParameter(dbCommand, "in_Store_Id", DbType.Int32, storeId);
            myDatabase.AddInParameter(dbCommand, "in_Brand_Id", DbType.Int32, brandId);
            myDatabase.AddInParameter(dbCommand, "in_Unit_Measurement_Id", DbType.Int32, unitMeasurementId);
            myDatabase.AddInParameter(dbCommand, "in_Item_Unit", DbType.String, ItemUnitText);
            myDatabase.AddInParameter(dbCommand, "in_Quantity", DbType.Decimal, quantity);
            myDatabase.AddInParameter(dbCommand, "in_stock_Update_Type", DbType.Int32, stockUpdateType);
            myDatabase.AddInParameter(dbCommand, "in_Created_By", DbType.String, createdBy);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        public Int32 UpdateIssueMaterialStatus(IssueMaterialDOM issueMaterialDOM)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_ISSUE_MATERIAL_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_IssueMaterial_Id", DbType.Int32, issueMaterialDOM.IssueMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, issueMaterialDOM.DemandVoucher.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, issueMaterialDOM.DemandVoucher.Quotation.GeneratedBy);
            myDatabase.AddOutParameter(dbCommand, "@out_IssueMaterialId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_IssueMaterialId").ToString(), out Id);
            return Id;
        }
        #endregion

        #region private methods
        private IssueMaterialDOM GenerateIssueMaterialFromReader(IDataReader reader)
        {
            IssueMaterialDOM issueMaterial = new IssueMaterialDOM();
            issueMaterial.IssueMaterialId = GetIntegerFromDataReader(reader, "Issue_Material_Id");
            issueMaterial.IssueMaterialNumber = GetStringFromDataReader(reader, "Issue_Material_Number");
            issueMaterial.IssueMaterialDate = GetDateFromReader(reader, "Issue_Material_Date");
            issueMaterial.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            issueMaterial.CreatedDate = GetDateFromReader(reader, "Created_Date");
            issueMaterial.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            issueMaterial.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            issueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            issueMaterial.DemandVoucher.IssueDemandVoucherId = GetIntegerFromDataReader(reader, "Issue_Demand_Voucher_Id");
            issueMaterial.DemandVoucher.IssueDemandVoucherNumber = GetStringFromDataReader(reader, "Issue_Demand_Voucher_Number");
            issueMaterial.DemandVoucher.Remarks = GetStringFromDataReader(reader, "Remarks");

            issueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            issueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            issueMaterial.DemandVoucher.MaterialDemandDate = GetDateFromReader(reader, "Issue_Demand_Date");
            issueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            issueMaterial.DemandVoucher.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            issueMaterial.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            //issueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contractor_Name");
            issueMaterial.DemandVoucher.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Document_Id");

            issueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            issueMaterial.DemandVoucher.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            issueMaterial.DemandVoucher.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Name");

            return issueMaterial;
        }

        private ItemTransaction GenerateIssueDemandVoucherMappingFromDataReader(IDataReader reader)
        {
            ItemTransaction itemTransaction = new ItemTransaction();
            itemTransaction.MetaProperty = new MetaData();

            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Issue_Material_Mapping_Id");
            //This field is Used For the SSRS Reports
            itemTransaction.MetaProperty.Name = GetStringFromDataReader(reader, "Issue_Material_Number");

            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            itemTransaction.ItemRequired = GetDecimalFromDataReader(reader, "Unit_Issued");
            itemTransaction.UnitDemanded = GetDecimalFromDataReader(reader, "Unit_Demanded");

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Activity_Id");

            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.Store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            itemTransaction.Item.ModelSpecification.Store.StoreName = GetStringFromDataReader(reader, "Store_Name");
            itemTransaction.Item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand_Name");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "created_by");

            return itemTransaction;
        }
        private MetaData GenerateIssueMaterialFromDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            //if (!isFinal)
            //{
            //    metaData.Name = GetStringFromDataReader(reader, "Name");
            //}
            return metaData;
        }

        #endregion

        public List<MetaData> GetContractorName(string prefixText)
        {
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_NAMEBY_PREFIX);
            myDatabase.AddInParameter(dbCommand, "in_prefixText", DbType.String, prefixText);
            List<MetaData> lstMetaData = new List<MetaData>();
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData.Id=GetIntegerFromDataReader(reader,"Contractor_Id");
                    metaData.Name = GetStringFromDataReader(reader, "Contractor_Name");
                    lstMetaData.Add(metaData);
                }
            }
            return lstMetaData;
        }
    }
}
