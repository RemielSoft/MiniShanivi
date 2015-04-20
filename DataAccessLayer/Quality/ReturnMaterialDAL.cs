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
    [Serializable]
    public class ReturnMaterialDAL : BaseDAL
    {
        #region private Global Variables

        private Database myDatabase;
        DbCommand dbCommand = null;
        MetaData metaData = null;
        ReturnMaterialDOM returnMaterialDOM = null;
        List<ReturnMaterialDOM> lstReturnMaterialDOM = null;
        ItemTransaction itemTransaction = null;
        List<ItemTransaction> lstItemTransaction = null;
        Int32 Id = 0;
        #endregion

        #region Constructors

        public ReturnMaterialDAL(Database database)
        {
            myDatabase = database;
        }

        #endregion

        #region Issue Material CRUD Methods

        public MetaData CreateReturnMaterial(ReturnMaterialDOM returnMaterialDOM, Int32? ReturnMaterialId)
        {
            string SqlCommand = DBConstants.CREATE_RETURN_MATERIAL;
            dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
            if (ReturnMaterialId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Id", DbType.Int32, ReturnMaterialId);
            }
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Number", DbType.String, returnMaterialDOM.ReturnMaterialNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Receive_Material_Number", DbType.String, returnMaterialDOM.RecieveMatarial.SupplierRecieveMaterialNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_Number", DbType.String, returnMaterialDOM.RecieveMatarial.Quotation.SupplierQuotationNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Quotation_Order_date", DbType.DateTime, returnMaterialDOM.RecieveMatarial.Quotation.OrderDate);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, returnMaterialDOM.RecieveMatarial.Quotation.SupplierId);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Name", DbType.String, returnMaterialDOM.RecieveMatarial.Quotation.SupplierName);
            myDatabase.AddInParameter(dbCommand, "@in_Delivery_Challan_Number", DbType.String, returnMaterialDOM.RecieveMatarial.DeliveryChallanNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Date", DbType.DateTime, returnMaterialDOM.ReturnMaterialDate);
            myDatabase.AddInParameter(dbCommand, "@in_Upload_Documnet_Id", DbType.Int32, returnMaterialDOM.RecieveMatarial.Quotation.UploadDocumentId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, returnMaterialDOM.RecieveMatarial.Quotation.StatusType.Id);

            myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, returnMaterialDOM.CreatedBy);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData = GenerateReturnMaterialFromDataReader(reader);
                }
            }
            return metaData;
        }
        public Int32 CreateReturnMaterialMapping(List<ItemTransaction> lstItemTransaction, Int32 ReturnMaterialId)
        {
            Id = 0;
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string SqlCommand = DBConstants.CREATE_RETURN_MATERIAL_MAPPING;
                dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Id", DbType.Int32, ReturnMaterialId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Mapping_Id", DbType.Int32, DBNull.Value);
                }
                //myDatabase.AddInParameter(dbCommand, "@in_Activity_Id", DbType.Int32, itemTransaction.DeliverySchedule.Id);
                //myDatabase.AddInParameter(dbCommand, "@in_Activity_Description", DbType.String, itemTransaction.DeliverySchedule.ActivityDescription);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category", DbType.String, itemTransaction.Item.ModelSpecification.Category.ItemCategoryName);
                myDatabase.AddInParameter(dbCommand, "@in_ItemId", DbType.Int32, itemTransaction.Item.ItemId);
                myDatabase.AddInParameter(dbCommand, "@in_Item", DbType.String, itemTransaction.Item.ItemName);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Model_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.ModelSpecificationId);
                myDatabase.AddInParameter(dbCommand, "@in_Specification", DbType.String, itemTransaction.Item.ModelSpecification.ModelSpecificationName);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Measurement", DbType.String, itemTransaction.Item.ModelSpecification.UnitMeasurement.Name);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Measurement_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.UnitMeasurement.Id);

                myDatabase.AddInParameter(dbCommand, "@in_Store_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Store.StoreId);
                myDatabase.AddInParameter(dbCommand, "@in_Brand_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Brand.BrandId);

                myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_Number_Of_Unit", DbType.Decimal, itemTransaction.NumberOfUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Receive", DbType.Decimal, itemTransaction.QuantityReceived);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Return", DbType.Decimal, itemTransaction.QuantityReturned);
                myDatabase.AddInParameter(dbCommand, "@in_Remark", DbType.String, itemTransaction.Remark);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Return_Material_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);

                Int32.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Return_Material_Mapping_Id").ToString(), out Id);
            }
            return Id;
        }
        public List<ItemTransaction> ReadSupplierReturnMaterialMapping(String ReceiveMaterialNumber, String ReturnMaterialNumber)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_RETURN_MATERIAL_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_Receive_Material_Number", DbType.String, ReceiveMaterialNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Number", DbType.String, ReturnMaterialNumber);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateSupplierReturnMaterialMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }
        public List<ReturnMaterialDOM> ReadSupplierReturnMaterial(Int32? ReturnMaterialId, String ReturnMaterialNumber, String ReceiveMaterialNumber, String SupplierPONumber, Int32? SupplierId, String DeliveryChallanNo, DateTime FromDate, DateTime EndDate)
        {
            lstReturnMaterialDOM = new List<ReturnMaterialDOM>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_RETURN_MATERIAL);
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Id", DbType.Int32, ReturnMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Number", DbType.String, ReturnMaterialNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Receive_Material_Number", DbType.String, ReceiveMaterialNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_Number", DbType.String, SupplierPONumber);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, SupplierId);
            myDatabase.AddInParameter(dbCommand, "@in_Delivery_Challan_Number", DbType.String, DeliveryChallanNo);
            if (FromDate == System.DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, FromDate);
            }
            if (EndDate == System.DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_End_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_End_Date", DbType.DateTime, EndDate);
            }
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    returnMaterialDOM = GenerateSupplierReturnMaterialFromDataReader(reader);
                    lstReturnMaterialDOM.Add(returnMaterialDOM);
                }
            }
            return lstReturnMaterialDOM;
        }
        public void DeleteReturnMaterial(int ReturnMaterialId, string modifiedBy)
        {
            string sqlCommand = DBConstants.DELETE_SUPPLIER_RETURN_MATERIAL;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Id", DbType.Int32, ReturnMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_modifiedBy", DbType.String, modifiedBy);

            myDatabase.ExecuteNonQuery(dbCommand);
        }
        public Int32 UpdateSupplierReturnMaterialStatus(ReturnMaterialDOM returnMaterialDOM)
        {
            Id = 0;
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_SUPPLIER_RETURN_MATERIAL_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_ReturnMaterial_Id", DbType.Int32, returnMaterialDOM.RetutrnMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, returnMaterialDOM.RecieveMatarial.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, returnMaterialDOM.RecieveMatarial.Quotation.GeneratedBy);

            myDatabase.AddOutParameter(dbCommand, "@out_ReturnMaterialId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_ReturnMaterialId").ToString(), out Id);
            return Id;
        }
        public void ResetReturnMaterialMapping(Int32? ReturnMaterialId)
        {
            string sqlCommand = DBConstants.RESET_RETURN_MATERIAL_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Return_Material_Id", DbType.Int32, ReturnMaterialId);
            myDatabase.ExecuteNonQuery(dbCommand);
        }
        #endregion

        #region private methods

        private MetaData GenerateReturnMaterialFromDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }
        private ReturnMaterialDOM GenerateSupplierReturnMaterialFromDataReader(IDataReader reader)
        {
            returnMaterialDOM = new ReturnMaterialDOM();
            returnMaterialDOM.RecieveMatarial = new SupplierRecieveMatarial();
            returnMaterialDOM.RecieveMatarial.Quotation = new QuotationDOM();
            returnMaterialDOM.RecieveMatarial.Quotation.StatusType = new MetaData();
            returnMaterialDOM.RetutrnMaterialId = GetIntegerFromDataReader(reader, "Return_Material_Id");
            returnMaterialDOM.ReturnMaterialNumber = GetStringFromDataReader(reader, "Return_Material_Number");
            returnMaterialDOM.RecieveMatarial.Quotation.SupplierQuotationNumber = GetStringFromDataReader(reader, "Supplier_PO_Number");
            returnMaterialDOM.RecieveMatarial.SupplierRecieveMaterialNumber = GetStringFromDataReader(reader, "Receive_Material_Number");
            returnMaterialDOM.RecieveMatarial.Quotation.OrderDate = GetDateFromReader(reader, "Quotation_Order_date");
            returnMaterialDOM.RecieveMatarial.Quotation.SupplierName = GetStringFromDataReader(reader, "Supplier_Name");
            returnMaterialDOM.RecieveMatarial.DeliveryChallanNumber = GetStringFromDataReader(reader, "Delivery_Challan_Number");
            returnMaterialDOM.ReturnMaterialDate = GetDateFromReader(reader, "Return_Material_Date");
            returnMaterialDOM.RecieveMatarial.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");
            returnMaterialDOM.RecieveMatarial.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Name");
            returnMaterialDOM.RecieveMatarial.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            returnMaterialDOM.ReturnMaterialDate = GetDateFromReader(reader, "Return_Material_Date");
            returnMaterialDOM.CreatedBy = GetStringFromDataReader(reader, "Created_By");

            return returnMaterialDOM;
        }
        private ItemTransaction GenerateSupplierReturnMaterialMappingFromDataReader(IDataReader reader)
        {
            ItemTransaction ItemTransaction = new ItemTransaction();
            ItemTransaction.MetaProperty = new MetaData();
            ItemTransaction.Item = new Item();
            ItemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            //itemTransaction.MetaProperty.Id = 0;
            ItemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Return_Material_Mapping_Id");
            //This field Is Used For the SSRS Report
            // ItemTransaction.MetaProperty.Name = GetStringFromDataReader(reader, "Supplier_Recieve_Material_Number");

            //ItemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Supplier_PO_Mapping_Id");

            ItemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "ItemId");
            ItemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            ItemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            ItemTransaction.QuantityReceived = GetDecimalFromDataReader(reader, "Item_Receive");
            ItemTransaction.QuantityReturned = GetDecimalFromDataReader(reader, "Item_Return");

            ItemTransaction.Item.ModelSpecification = new ModelSpecification();
            ItemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand_Name");
            ItemTransaction.Item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            ItemTransaction.Item.ModelSpecification.Store.StoreName = GetStringFromDataReader(reader, "Store_Name");
            ItemTransaction.Item.ModelSpecification.Store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            ItemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            ItemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
            ItemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Unit_Measurement");
            ItemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");//////
            ItemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");

            ItemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            ItemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");

            ItemTransaction.Remark = GetStringFromDataReader(reader, "Remark");

            return ItemTransaction;

        }
        #endregion
    }
}
