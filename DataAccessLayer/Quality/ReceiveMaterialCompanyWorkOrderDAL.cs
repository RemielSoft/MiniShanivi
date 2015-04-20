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
    public class ReceiveMaterialCompanyWorkOrderDAL : BaseDAL
    {
        #region Global Declaration

        private Database myDatabase;
        DbCommand dbCommand = null;
        MetaData metaData = null;
        ReceiveMaterialCompanyWorkOrderDom ReceiveMaterialCWO = null;
        List<ReceiveMaterialCompanyWorkOrderDom> lstCWO = null;
        int Id = 0;
        #endregion

        #region Constructors
        public ReceiveMaterialCompanyWorkOrderDAL(Database database)
        {
            myDatabase = database;
        }

        #endregion

        #region Recieve Material CWO CRUD

        public MetaData CreateRecieveMetarialCWO(ReceiveMaterialCompanyWorkOrderDom recieveMaterialCWO, Int32? RMCWOID)
        {
            string sqlCommand = DBConstants.CREATE_RECIEVE_MATERIAL_CWO;
            dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            if (recieveMaterialCWO.ContractReceiveMaterialId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contract_Receive_Material_Id", DbType.Int32, recieveMaterialCWO.ContractReceiveMaterialId);

            }
            myDatabase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, recieveMaterialCWO.CompanyWorkOrder.CompanyWorkOrderId);
            // myDatabase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, recieveMaterialCWO.CompanyWorkOrder.CompanyWorkOrderId);
            myDatabase.AddInParameter(dbCommand, "@in_Work_Order_Id", DbType.Int32, recieveMaterialCWO.Quotation.WorkOrderId);
            myDatabase.AddInParameter(dbCommand, "@in_Receive_Date", DbType.DateTime, recieveMaterialCWO.Receive_Date);
            myDatabase.AddInParameter(dbCommand, "@in_Description", DbType.String, recieveMaterialCWO.Description);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Id", DbType.Int32, recieveMaterialCWO.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Upload_Documnet_Id", DbType.Int32, recieveMaterialCWO.UploadFile.DocumentId);
            myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, recieveMaterialCWO.CreatedBy);
            // myDatabase.ExecuteNonQuery(dbCommand);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData = GenerateRecieveMaterialCWOInformationFromDataReader(reader);
                }
            }
            return metaData;
        }

        public int CreateRecieveMaterialCWOMapping(List<ItemTransaction> lstItemTransaction, int RMCWOMID)
        {
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string sqlCommand = DBConstants.CREATE_RECIEVE_MATERIAL_CWO_MAPPING;
                DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);

                myDatabase.AddInParameter(dbCommand, "@in_Contract_Receive_Material_Id", DbType.Int32, RMCWOMID);
                itemTransaction.MetaProperty = new MetaData();
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Contract_Receive_Material_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Contract_Receive_Material_Mapping_Id", DbType.Int32, DBNull.Value);
                }

                myDatabase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, itemTransaction.Item.ItemId);


                myDatabase.AddInParameter(dbCommand, "@in_Specification_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.ModelSpecificationId);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.Category.ItemCategoryId);
                myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_BrandId", DbType.Int32, itemTransaction.Item.ModelSpecification.Brand.BrandId);
                myDatabase.AddInParameter(dbCommand, "@in_StoreId", DbType.Int32, itemTransaction.Item.ModelSpecification.Store.StoreId);
                
                // myDatabase.AddInParameter(dbCommand, "@in_Unit_MeasurmentID", DbType.Int32, itemTransaction.Item.ModelSpecification.UnitMeasurement.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Quantity", DbType.Decimal, itemTransaction.NumberOfUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Measurement_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.UnitMeasurement.Id);

                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Contract_Receive_Material_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);
                int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Contract_Receive_Material_Mapping_Id").ToString(), out Id);
            }
            return Id;
        }

        public List<ReceiveMaterialCompanyWorkOrderDom> ReadRMCWO(String CRMNo, int CompanyWorkOrderId, DateTime FromDate, DateTime ToDate)
        {
            lstCWO = new List<ReceiveMaterialCompanyWorkOrderDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_RECEIVE_MATERIAL_CWO);
            if (CRMNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_CRM_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_CRM_No", DbType.String, CRMNo);
            if (CompanyWorkOrderId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.String, CompanyWorkOrderId);

            if (FromDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, FromDate);
            if (ToDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_ToDate", DbType.DateTime, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_ToDate", DbType.DateTime, ToDate);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ReceiveMaterialCWO = GenerateReceiveMaterialCWOFromReader(reader);
                    lstCWO.Add(ReceiveMaterialCWO);
                }
            }
            return lstCWO;
        }
        public List<ItemTransaction> ReadRMCWOMapping(int CRMId)
        {
            List<ItemTransaction> lstItem = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_RECEIVE_MATERIAL_CWO_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_CRM_Id", DbType.Int32, CRMId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ItemTransaction itemTransaction = new ItemTransaction();
                    itemTransaction = GenerateReceiveMaterialCWOMappingFromDataReader(reader);
                    lstItem.Add(itemTransaction);
                }
            }
            return lstItem;
        }
        //  By Anand
        public List<ReceiveMaterialCompanyWorkOrderDom> ReadReceiveMaterailCompanyWorkOrder(Int32 RMCWOId)
        {
            lstCWO = new List<ReceiveMaterialCompanyWorkOrderDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_RECEIVE_MATERIAL_CWO_BYID);
            myDatabase.AddInParameter(dbCommand, "@in_Contract_Receive_Material_Id", DbType.Int32, RMCWOId);
            // myDatabase.AddInParameter(dbCommand, "@in_Contract_Receive_Material_Number", DbType.String, RMCWONumber);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ReceiveMaterialCWO = GenerateReceiveMaterialCWOFromReader(reader);
                    lstCWO.Add(ReceiveMaterialCWO);
                }
            }
            return lstCWO;
        }
        public void DeleteReceiveMaterialCWO(int ReceiveMaterialCWOId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_RECEIVE_MATERIAL_CWO;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_CRM_Id", DbType.Int32, ReceiveMaterialCWOId);
            myDatabase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDatabase.AddInParameter(dbCommand, "@in_Modified_Date", DbType.DateTime, modifiedOn);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        public Int32 UpdateReceiveMaterialCWOStatus(ReceiveMaterialCompanyWorkOrderDom ReceiveMaterialCWO)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_RECEIVE_MATERIAL_CWO_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_CWO_Id", DbType.Int32, ReceiveMaterialCWO.ContractReceiveMaterialId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, ReceiveMaterialCWO.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, ReceiveMaterialCWO.Quotation.GeneratedBy);

            myDatabase.AddOutParameter(dbCommand, "@out_CWOId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_CWOId").ToString(), out Id);
            return Id;
        }

        public void ResetReceiveMaterialCWOMapping(Int32? RMCWOID)
        {
            string sqlCommand = DBConstants.RESET_SUPPLIER_RECIEVE_MATERIAL_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Recieve_Material_Id", DbType.Int32, RMCWOID);
            myDatabase.ExecuteNonQuery(dbCommand);
        }
        #endregion

        #region Public Method


        public ItemTransaction GenerateReceiveMaterialCWOMappingFromDataReader(IDataReader reader)
        {
            ItemTransaction itemTransaction = new ItemTransaction();

            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Quantity");
            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item_Name");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.Item.ModelSpecification.Store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            itemTransaction.Item.ModelSpecification.Store.StoreName = GetStringFromDataReader(reader, "Store_Name");
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "UnitMeasurment_Id");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Unit_Measurement_Name");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Specification_Id");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");

            return itemTransaction;
        }

        public ReceiveMaterialCompanyWorkOrderDom GenerateReceiveMaterialCWOFromReader(IDataReader reader)
        {
            ReceiveMaterialCWO = new ReceiveMaterialCompanyWorkOrderDom();
            ReceiveMaterialCWO.ContractReceiveMaterialId = GetIntegerFromDataReader(reader, "Contract_Receive_Material_Id");
            ReceiveMaterialCWO.ContractReceiveMaterialNumber = GetStringFromDataReader(reader, "Contract_Receive_Material_Number");
            ReceiveMaterialCWO.Receive_Date = GetDateFromReader(reader, "Receive_Date");
            ReceiveMaterialCWO.Description = GetStringFromDataReader(reader, "Description");
            ReceiveMaterialCWO.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            ReceiveMaterialCWO.CreatedDate = GetDateFromReader(reader, "Created_Date");

            ReceiveMaterialCWO.CompanyWorkOrder = new CompanyWorkOrderDOM();
            ReceiveMaterialCWO.CompanyWorkOrder.CompanyWorkOrderId = GetIntegerFromDataReader(reader, "Company_Work_Order_Id");
            ReceiveMaterialCWO.CompanyWorkOrder.CompanyWorkOrderNumber = GetStringFromDataReader(reader, "Company_Work_Order_Number");

            ReceiveMaterialCWO.UploadFile = new Document();
            ReceiveMaterialCWO.UploadFile.DocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");

            ReceiveMaterialCWO.Quotation = new QuotationDOM();
            ReceiveMaterialCWO.Quotation.WorkOrderId = GetIntegerFromDataReader(reader, "Work_Order_Id");
            ReceiveMaterialCWO.Quotation.WorkOrderNumber = GetStringFromDataReader(reader, "Work_Order_Number");
            ReceiveMaterialCWO.Quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            ReceiveMaterialCWO.Quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");

            ReceiveMaterialCWO.Quotation.StatusType = new MetaData();
            ReceiveMaterialCWO.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Id");
            ReceiveMaterialCWO.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            return ReceiveMaterialCWO;
        }
        #endregion

        #region Private Method
        private MetaData GenerateRecieveMaterialCWOInformationFromDataReader(IDataReader reader)
        {
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }
        #endregion

    }
}
