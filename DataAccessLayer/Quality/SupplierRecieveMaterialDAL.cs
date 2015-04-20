using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccessLayer.Quality
{
    public class SupplierRecieveMaterialDAL : BaseDAL
    {
        #region Global Declaration
        private Database myDatabase;
        DbCommand dbCommand = null;
        MetaData metaData = null;
        QuotationDOM quotation = null;
        SupplierRecieveMatarial supplierRecieveMaterial = null;
        List<QuotationDOM> lstQuotation = null;
        ItemTransaction itemTransaction = null;
        MetaData MetaProperty = new MetaData();

        List<ItemTransaction> lstitemtransaction = null;
        List<SupplierRecieveMatarial> lstRecieveMaterial = null;
        //ItemTransaction itemTransaction=new ItemTransaction();

        int id = 0;
        // itemTransaction.MetaProperty.Id=0;
        #endregion

        #region Constructors
        public SupplierRecieveMaterialDAL(Database database)
        {
            myDatabase = database;
        }

        public SupplierRecieveMaterialDAL()
        {
            // TODO: Complete member initialization
        }

        #endregion

        #region Supplier Recieve Material CRUD
        public MetaData CreateSupplierRecieveMetarial(SupplierRecieveMatarial supplierRecieveMaterial, Int32? SRMID)
        {
            string sqlCommand = DBConstants.CREATE_SUPPLIER_RECIEVE_MATERIAL;
            dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            if (supplierRecieveMaterial.SupplierRecieveMatarialId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Recieve_Material_Id", DbType.Int32, supplierRecieveMaterial.SupplierRecieveMatarialId);

            }
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Purchase_Order_Number", DbType.String, supplierRecieveMaterial.Quotation.SupplierQuotationNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Quotation_Order_Date", DbType.DateTime, supplierRecieveMaterial.Quotation.OrderDate);
            // myDatabase.AddInParameter(dbCommand, "@in_Contract_Number", DbType.String, quotationDOM.ContractNumber);

            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Delivery_Chalan", DbType.String, supplierRecieveMaterial.DeliveryChallanNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Recieve_Material_Date", DbType.DateTime, supplierRecieveMaterial.RecieveMaterialDate);
            myDatabase.AddInParameter(dbCommand, "@in_Upload_Documnet_Id", DbType.String, supplierRecieveMaterial.UploadFile.DocumentId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.String, supplierRecieveMaterial.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, supplierRecieveMaterial.CreatedBy);
            // myDatabase.ExecuteNonQuery(dbCommand);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData = GenerateRecieveMaterialInformationFromDataReader(reader);
                }
            }
            return metaData;
        }

        public int CreateSuppplierRecieveMaterialMapping(List<ItemTransaction> lstItemTransaction, int SRMMId)
        {
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string sqlCommand = DBConstants.CREATE_SUPPLIER_RECIEVE_MATERIAL_MAPPING;
                DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);

                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Recieve_Material_Id", DbType.Int32, SRMMId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Supplier_Recieve_Material_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Supplier_Recieve_Material_Mapping_Id", DbType.Int32, DBNull.Value);
                }
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_Mapping_Id", DbType.Int32, itemTransaction.DeliverySchedule.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, itemTransaction.Item.ItemId);
                myDatabase.AddInParameter(dbCommand, "@in_Item", DbType.String, itemTransaction.Item.ItemName);
                myDatabase.AddInParameter(dbCommand, "@in_Specification", DbType.String, itemTransaction.Item.ModelSpecification.ModelSpecificationName);

                myDatabase.AddInParameter(dbCommand, "@in_Store_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Store.StoreId);
                myDatabase.AddInParameter(dbCommand, "@in_Brand_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Brand.BrandId);

                myDatabase.AddInParameter(dbCommand, "@in_SpecificationID", DbType.Int32, itemTransaction.Item.ModelSpecification.ModelSpecificationId);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category", DbType.String, itemTransaction.Item.ModelSpecification.Category.ItemCategoryName);
                myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_MeasurmentID", DbType.Int32, itemTransaction.Item.ModelSpecification.UnitMeasurement.Id);

                myDatabase.AddInParameter(dbCommand, "@in_Unit_Measurment", DbType.String, itemTransaction.Item.ModelSpecification.UnitMeasurement.Name);
                myDatabase.AddInParameter(dbCommand, "@in_Number_Of_Unit", DbType.Decimal, itemTransaction.NumberOfUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Required", DbType.Decimal, itemTransaction.ItemRequired);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "out_Supplier_Recieve_Material_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);
                int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Supplier_Recieve_Material_Mapping_Id").ToString(), out id);
            }
            return id;
        }

        public void ResetIssueDemandVoucherMapping(Int32? SRMID)
        {
            string sqlCommand = DBConstants.RESET_SUPPLIER_RECIEVE_MATERIAL_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Recieve_Material_Id", DbType.Int32, SRMID);
            myDatabase.ExecuteNonQuery(dbCommand);
        }


        public List<SupplierRecieveMatarial> ReadSupplierReceiveMaterial(Int32? supplierReceiveMaterialId, String ReceiveMaterialNumber)
        {
            lstRecieveMaterial = new List<SupplierRecieveMatarial>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_RECIEVE_MATERIAL);
            myDatabase.AddInParameter(dbCommand, "@in_supplier_receive_materialId", DbType.Int32, supplierReceiveMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Recieve_Material_Number", DbType.String, ReceiveMaterialNumber);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    supplierRecieveMaterial = GenerateSupplierReceiveMaterialFromDataReader(reader);
                    lstRecieveMaterial.Add(supplierRecieveMaterial);
                }
            }
            return lstRecieveMaterial;
        }

        public List<ItemTransaction> ReadSupplierReceiveMaterialMapping(Int32 supplierReceiveMaterialId)
        {
            lstitemtransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_RECIEVE_MATERIAL_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_supplier_receive_materialId", DbType.Int32, supplierReceiveMaterialId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateSupplierReceiveMaterialMappingFromDataReader(reader);
                    lstitemtransaction.Add(itemTransaction);
                }
            }
            return lstitemtransaction;
        }

        public List<SupplierRecieveMatarial> SearchReceiveMaterial(String SupplierPONumber, String DeliveryChallanNo, String ReceiveMaterialNo, DateTime toDate, DateTime fromDate,string name)
        {
            //lstQuotation = new List<QuotationDOM>();
            lstRecieveMaterial = new List<SupplierRecieveMatarial>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.SEARCH_SUPPLIER_RECIEVE_MATERIAL);
            if (string.IsNullOrEmpty( SupplierPONumber))
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PONumber", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PONumber", DbType.String, SupplierPONumber);

            if (string.IsNullOrEmpty(DeliveryChallanNo))
            {
                myDatabase.AddInParameter(dbCommand, "@in_DeliveryChallanNo", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_DeliveryChallanNo", DbType.String, DeliveryChallanNo);

            if (string.IsNullOrEmpty(ReceiveMaterialNo))
            {
                myDatabase.AddInParameter(dbCommand, "@in_ReceiveMaterial_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_ReceiveMaterial_No", DbType.String, ReceiveMaterialNo);
            if (toDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_ToDate", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_ToDate", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, fromDate);
            }
            if (string.IsNullOrEmpty(name))
            {
                myDatabase.AddInParameter(dbCommand, "@in_SupplierName", DbType.String, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_SupplierName", DbType.String, name);
            }
           
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    supplierRecieveMaterial = GenerateSearchReceiveMaterialFromDataReader(reader);
                    lstRecieveMaterial.Add(supplierRecieveMaterial);
                }
            }
            return lstRecieveMaterial;
        }

        public Int32 UpdateSupplierReceiveMaterialStatus(SupplierRecieveMatarial supplierRecieveMaterial)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_SUPPLIER_RECIEVE_MATERIAL_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_SupplierReceiveMaterial_Id", DbType.Int32, supplierRecieveMaterial.SupplierRecieveMatarialId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, supplierRecieveMaterial.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, supplierRecieveMaterial.Quotation.GeneratedBy);

            myDatabase.AddOutParameter(dbCommand, "@out_SupplierReceiveMaterialId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_SupplierReceiveMaterialId").ToString(), out id);
            return id;
        }

        public void UpdateStockReceiveIssueQuantity(int storeId, int brandId, Int32 itemId, Int32 itemSpecificationId, Int32 unitMeasurementId, String ItemUnitText, Decimal quantity, Int32 stockUpdateType, String createdBy)
        {
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_STOCK_RECEIVE_ISSUE_MATERIAL);
            myDatabase.AddInParameter(dbCommand, "in_Item_Id", DbType.Int32, itemId);
            myDatabase.AddInParameter(dbCommand, "in_Store_Id", DbType.Int32, storeId);
            myDatabase.AddInParameter(dbCommand, "in_Brand_Id", DbType.Int32, brandId);
            myDatabase.AddInParameter(dbCommand, "in_item_model_id", DbType.Int32, itemSpecificationId);
            myDatabase.AddInParameter(dbCommand, "in_Unit_Measurement_Id", DbType.Int32, unitMeasurementId);
            myDatabase.AddInParameter(dbCommand, "in_Item_Unit", DbType.String, ItemUnitText);
            myDatabase.AddInParameter(dbCommand, "in_Quantity", DbType.Decimal, quantity);
            myDatabase.AddInParameter(dbCommand, "in_stock_Update_Type", DbType.Int32, stockUpdateType);
            myDatabase.AddInParameter(dbCommand, "in_Created_By", DbType.String, createdBy);

            myDatabase.ExecuteNonQuery(dbCommand);

        }


        public void DeleteSupplierReceiveMaterial(int SupplierReceiveMaterialId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_SUPPLIER_RECEIVE_MATERIAL;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_SupplierReceiveMaterial_Id", DbType.Int32, SupplierReceiveMaterialId);
            myDatabase.AddInParameter(dbCommand, "@Modified_By", DbType.String, modifiedBy);
            myDatabase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, modifiedOn);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region Private Method
        private SupplierRecieveMatarial GenerateSupplierReceiveMaterialFromDataReader(IDataReader reader)
        {
            supplierRecieveMaterial = new SupplierRecieveMatarial();
            supplierRecieveMaterial.SupplierRecieveMatarialId = GetIntegerFromDataReader(reader, "Supplier_Recieve_Material_Id");
            supplierRecieveMaterial.Quotation = new QuotationDOM();
            supplierRecieveMaterial.Quotation.SupplierQuotationNumber = GetStringFromDataReader(reader, "Supplier_Purchase_Order_Number");
            supplierRecieveMaterial.SupplierRecieveMaterialNumber = GetStringFromDataReader(reader, "Supplier_Recieve_Material_Number");
            supplierRecieveMaterial.Quotation.OrderDate = GetDateFromReader(reader, "Quotation_Order_Date");
            supplierRecieveMaterial.DeliveryChallanNumber = GetStringFromDataReader(reader, "Supplier_Delivery_Chalan");
            supplierRecieveMaterial.RecieveMaterialDate = GetDateFromReader(reader, "Recieve_Material_Date");
            supplierRecieveMaterial.UploadFile = new Document();
            supplierRecieveMaterial.UploadFile.DocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");
            supplierRecieveMaterial.Quotation.StatusType = new MetaData();
            supplierRecieveMaterial.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");

            supplierRecieveMaterial.Quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            supplierRecieveMaterial.Quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
            supplierRecieveMaterial.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            return supplierRecieveMaterial;
        }

        private ItemTransaction GenerateSupplierReceiveMaterialMappingFromDataReader(IDataReader reader)
        {
            ItemTransaction ItemTransaction = new ItemTransaction();
            ItemTransaction.MetaProperty = new MetaData();
            ItemTransaction.Item = new Item();
            ItemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            //itemTransaction.MetaProperty.Id = 0;
            ItemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Supplier_Recieve_Material_Mapping_Id");
            //This field Is Used For the SSRS Report
            ItemTransaction.MetaProperty.Name = GetStringFromDataReader(reader, "Supplier_Recieve_Material_Number");

            ItemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Supplier_PO_Mapping_Id");

            ItemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "ItemId");
            ItemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            ItemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            ItemTransaction.ItemRequired = GetDecimalFromDataReader(reader, "Item_Required");

            ItemTransaction.Item.ModelSpecification = new ModelSpecification();
            ItemTransaction.Item.ModelSpecification.Store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            ItemTransaction.Item.ModelSpecification.Store.StoreName = GetStringFromDataReader(reader, "Store_Name");
            ItemTransaction.Item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            ItemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            ItemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            ItemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
            ItemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Unit_Measurement");
            ItemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");//////
            ItemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");

            ItemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            ItemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");

            ItemTransaction.CreatedBy = GetStringFromDataReader(reader, "created_by");

            return ItemTransaction;

        }

        private SupplierRecieveMatarial GenerateSearchReceiveMaterialFromDataReader(IDataReader reader)
        {
            supplierRecieveMaterial = new SupplierRecieveMatarial();
            supplierRecieveMaterial.Quotation = new QuotationDOM();
            supplierRecieveMaterial.Quotation.SupplierQuotationId = GetIntegerFromDataReader(reader, "Supplier_Purchase_Order_Id");
            supplierRecieveMaterial.Quotation.SupplierQuotationNumber = GetStringFromDataReader(reader, "Supplier_Purchase_Order_Number");
            supplierRecieveMaterial.Quotation.SupplierId = GetIntegerFromDataReader(reader, "Supplier_Id");
            supplierRecieveMaterial.Quotation.SupplierName = GetStringFromDataReader(reader, "Supplier_Name");
            supplierRecieveMaterial.SupplierRecieveMaterialNumber = GetStringFromDataReader(reader, "Supplier_Recieve_Material_Number");
            supplierRecieveMaterial.SupplierRecieveMatarialId = GetIntegerFromDataReader(reader, "Supplier_Recieve_Material_Id");
            supplierRecieveMaterial.RecieveMaterialDate = GetDateFromReader(reader, "Recieve_Material_Date");
            supplierRecieveMaterial.Quotation.OrderDate = GetDateFromReader(reader, "Quotation_Order_Date");
            supplierRecieveMaterial.DeliveryChallanNumber = GetStringFromDataReader(reader, "Supplier_Delivery_Chalan");

            supplierRecieveMaterial.Quotation.StatusType = new MetaData();
            supplierRecieveMaterial.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            supplierRecieveMaterial.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Name");
            supplierRecieveMaterial.Quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            supplierRecieveMaterial.Quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");

            supplierRecieveMaterial.UploadFile = new Document();
            supplierRecieveMaterial.UploadFile.Id = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");
            return supplierRecieveMaterial;
        }

        private MetaData GenerateRecieveMaterialInformationFromDataReader(IDataReader reader)
        {
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }
        #endregion
        public List<MetaData> GetSupplierName(string prefixText)
        {
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_NUMBER_PREFIX);
            myDatabase.AddInParameter(dbCommand, "@in_prefixText", DbType.String, prefixText);
            List<MetaData> lstMetaData = new List<MetaData>();
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData.Id = GetIntegerFromDataReader(reader, "Supplier_Id");
                    metaData.Name = GetStringFromDataReader(reader, "Supplier_Name");
                    lstMetaData.Add(metaData);
                }
            }
            return lstMetaData;
        }
    }
}
