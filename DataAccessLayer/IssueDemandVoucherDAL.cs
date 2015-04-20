using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace DataAccessLayer
{
    public class IssueDemandVoucherDAL : BaseDAL
    {
        #region private global variables
        private Database myDatabase;
        DbCommand dbCommand = null;

        ItemTransaction itemTransaction = null;
        MetaData metaData = null;
        IssueDemandVoucherDOM issueDemandVoucherDOM = null;
        List<IssueDemandVoucherDOM> lstIssueDemandVoucherDOM = null;

        //List<QuotationDOM> lstQuotation = null;
        List<ItemTransaction> lstItemTransaction = null;
        int id = 0;
        #endregion

        #region Constructors
        public IssueDemandVoucherDAL(Database database)
        {
            myDatabase = database;
        }
        #endregion

        #region IssueDemandVoucher CRUD
        public MetaData CreateIssueDemandVoucher(IssueDemandVoucherDOM issueDemandVoucherDOM, Int32? IDVID)
        {
            string sqlCommand = DBConstants.CREATE_ISSUE_DEMAND_VOUCHER;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            if (issueDemandVoucherDOM.IssueDemandVoucherId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Material_Issue_Demand_Voucher_Id", DbType.Int32, issueDemandVoucherDOM.IssueDemandVoucherId);
            }
            myDatabase.AddInParameter(dbCommand, "@in_Upload_Document_Id", DbType.Int32, issueDemandVoucherDOM.Quotation.UploadDocumentId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Quotation_Number", DbType.String, issueDemandVoucherDOM.Quotation.ContractQuotationNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, issueDemandVoucherDOM.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, issueDemandVoucherDOM.Quotation.ContractorId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Name", DbType.String, issueDemandVoucherDOM.Quotation.ContractorName);
            myDatabase.AddInParameter(dbCommand, "@in_Contract_Number", DbType.String, issueDemandVoucherDOM.Quotation.ContractNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Quotation_Date", DbType.DateTime, issueDemandVoucherDOM.Quotation.OrderDate);
            myDatabase.AddInParameter(dbCommand, "@in_Material_demand_Date", DbType.DateTime, issueDemandVoucherDOM.MaterialDemandDate);
            myDatabase.AddInParameter(dbCommand, "@in_Remarks", DbType.String, issueDemandVoucherDOM.Remarks);
            myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, issueDemandVoucherDOM.CreatedBy);
            // myDatabase.ExecuteNonQuery(dbCommand);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData = GenerateIssueDemandVoucherInformationFromDataReader(reader);
                }
            }
            return metaData;
        }
        public int CreateIssueDemandVoucherMapping(List<ItemTransaction> lstItemTransaction, int IDVId)
        {
            //itemTransaction.MetaProperty = new MetaData();
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string sqlCommand = DBConstants.CREATE_ISSUE_DEMAND_VOUCHER_MAPPING;
                DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Material_Issue_Demand_Voucher_Id", DbType.Int32, IDVId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Material_Issue_Demand_Voucher_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Material_Issue_Demand_Voucher_Mapping_Id", DbType.Int32, DBNull.Value);
                }
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Id", DbType.Int32, itemTransaction.DeliverySchedule.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Description", DbType.String, itemTransaction.DeliverySchedule.ActivityDescription);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category", DbType.String, itemTransaction.Item.ModelSpecification.Category.ItemCategoryName);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Id", DbType.Int32, itemTransaction.Item.ItemId);
                myDatabase.AddInParameter(dbCommand, "@in_Item", DbType.String, itemTransaction.Item.ItemName);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Model_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.ModelSpecificationId);
                myDatabase.AddInParameter(dbCommand, "@in_Specification", DbType.String, itemTransaction.Item.ModelSpecification.ModelSpecificationName);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Measurement_Id", DbType.Int32, itemTransaction.Item.ModelSpecification.UnitMeasurement.Id);
              //  myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_Number_Of_Unit", DbType.Decimal, itemTransaction.NumberOfUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Required", DbType.Decimal, itemTransaction.ItemRequired);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Material_Issue_Demand_Voucher_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);
                int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Material_Issue_Demand_Voucher_Mapping_Id").ToString(), out id);
            }
            return id;
        }
        public void ResetIssueDemandVoucherMapping(Int32? IDVId)
        {
            string sqlCommand = DBConstants.RESET_ISSUE_DEMAND_VOUCHER_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Issue_Demand_Voucher_Id", DbType.Int32, IDVId);
            myDatabase.ExecuteNonQuery(dbCommand);
        }
        //public List<ItemTransaction> CountItemRequired(string ContractorQuotationNumber, int ActivityId)
        //{
        //    lstItemTransaction = new List<ItemTransaction>();
        //    string sqlCommand = DBConstants.COUNT_ITEM_REQUIRED;
        //    DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
        //    myDatabase.AddInParameter(dbCommand,"@in_Contractor_Quotation_Number",DbType.String,ContractorQuotationNumber);
        //    myDatabase.AddInParameter(dbCommand, "@in_Activity_Id", DbType.Int32, ActivityId);
        //    using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            itemTransaction = new ItemTransaction();
        //            itemTransaction.ItemRequired = GetDecimalFromDataReader(reader, "Item_Required");
        //            lstItemTransaction.Add(itemTransaction);
        //        }
        //    }
        //    return lstItemTransaction;
        //}
        public List<IssueDemandVoucherDOM> ViewIssueDemand(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String IDVNo)
        {
            lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();

            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.SEARCH_ISSUE_DEMAND_VOUCHER);
            if (contractorId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, contractorId);
            if (toDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDatabase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDatabase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }

            if (contractNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, contractNo);

            if (IDVNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_IDV_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_IDV_No", DbType.String, IDVNo);


            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    issueDemandVoucherDOM = GenerateIssueDemandVoucherFromDataReader(reader);
                    lstIssueDemandVoucherDOM.Add(issueDemandVoucherDOM);
                }
            }
            return lstIssueDemandVoucherDOM;
        }
        public List<IssueDemandVoucherDOM> ReadMaterialIssueDemandVoucher(Int32? IssueDemandVoucherId, String IssueDemandVoucherNo)
        {
            lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_MATERIAL_ISSUE_DEMAND_VOUCHER);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Issue_Demand_Voucher_Id", DbType.Int32, IssueDemandVoucherId);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Demand_Voucher_Number", DbType.String, IssueDemandVoucherNo);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    issueDemandVoucherDOM = new IssueDemandVoucherDOM();
                    issueDemandVoucherDOM = GenerateMaterialIssueDemandVoucheFromDataReader(reader);
                    lstIssueDemandVoucherDOM.Add(issueDemandVoucherDOM);
                }
            }
            return lstIssueDemandVoucherDOM;
        }
        public List<ItemTransaction> ReadIssueDemandMapping(Int32? IssueDemandVoucherId, String IssueDemandVoucherNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ISSUE_DEMAND_VOUCHER_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_Material_Issue_Demand_Voucher_Id", DbType.Int32, IssueDemandVoucherId);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Demand_Voucher_Number", DbType.String, IssueDemandVoucherNo);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateIssueDemandVoucherMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }

            return lstItemTransaction;
        }
        public void DeleteIssueDemandVoucher(int IssueDemandVoucherId, string modifiedBy, DateTime modifiedOn)
        {
            String sqlCommand = DBConstants.DELETE_ISSUE_DEMAND_VOUCHER;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_IssueDemandVoudher_Id", DbType.Int32, IssueDemandVoucherId);
            myDatabase.AddInParameter(dbCommand, "@Modified_By", DbType.String, modifiedBy);
            myDatabase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, modifiedOn);
            myDatabase.ExecuteNonQuery(dbCommand);
        }
        public Int32 UpdateIssueDemandVoucherStatus(IssueDemandVoucherDOM issueDemandVoucherDOM)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_ISSUE_DEMAND_VOUCHER_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_IssueDemandVoucher_Id", DbType.Int32, issueDemandVoucherDOM.IssueDemandVoucherId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, issueDemandVoucherDOM.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, issueDemandVoucherDOM.Quotation.GeneratedBy);

            myDatabase.AddOutParameter(dbCommand, "@out_IssueDemandVoucherId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_IssueDemandVoucherId").ToString(), out id);
            return id;
        }
        //public String ValidateIssueDemandVoucher(int IssueDemandVoucherId, String errorIssueDemandVoucher)
        //{
        //    String sqlCommand = DBConstants.VALIDATE_ISSUE_DEMAND_VOUCHER;
        //    DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
        //    myDatabase.AddInParameter(dbCommand, "@in_IssueDemandVoucher_Id", DbType.Int32, IssueDemandVoucherId);
        //    myDatabase.AddInParameter(dbCommand, "@in_errorIssueDemandVoucher", DbType.String, errorIssueDemandVoucher);
        //    myDatabase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 100);
        //    myDatabase.ExecuteNonQuery(dbCommand);
        //    return Convert.ToString(myDatabase.GetParameterValue(dbCommand, "@out_errorCode"));
        //}
        #endregion
        #region Public Method
        public IssueDemandVoucherDOM GenerateMaterialIssueDemandVoucheFromDataReader(IDataReader reader)
        {
            issueDemandVoucherDOM = new IssueDemandVoucherDOM();
            issueDemandVoucherDOM.IssueDemandVoucherId = GetIntegerFromDataReader(reader, "Material_Issue_Demand_Voucher_Id");
            issueDemandVoucherDOM.IssueDemandVoucherNumber = GetStringFromDataReader(reader, "Issue_Demand_Voucher_Number");
            issueDemandVoucherDOM.MaterialDemandDate = GetDateFromReader(reader, "Material_demand_Date");
            issueDemandVoucherDOM.Remarks = GetStringFromDataReader(reader, "Remarks");
            //quotation.ContractorQuotationId = GetIntegerFromDataReader(reader, "Contractor_Purchase_Order_Id");
            issueDemandVoucherDOM.Quotation = new QuotationDOM();
            issueDemandVoucherDOM.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Document_Id");
            issueDemandVoucherDOM.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            issueDemandVoucherDOM.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            issueDemandVoucherDOM.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            issueDemandVoucherDOM.Quotation.QuotationDate = GetDateFromReader(reader, "Contractor_Quotation_Date");
            issueDemandVoucherDOM.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            issueDemandVoucherDOM.Quotation.StatusType = new MetaData();
            issueDemandVoucherDOM.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");

            issueDemandVoucherDOM.Quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            issueDemandVoucherDOM.Quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
            return issueDemandVoucherDOM;
        }
        private IssueDemandVoucherDOM GenerateIssueDemandVoucherFromDataReader(IDataReader reader)
        {
            issueDemandVoucherDOM = new IssueDemandVoucherDOM();
            issueDemandVoucherDOM.Quotation = new QuotationDOM();
            issueDemandVoucherDOM.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            issueDemandVoucherDOM.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            issueDemandVoucherDOM.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            issueDemandVoucherDOM.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            issueDemandVoucherDOM.Quotation.OrderDate = GetDateFromReader(reader, "Contractor_Quotation_Date");
            issueDemandVoucherDOM.IssueDemandVoucherId = GetIntegerFromDataReader(reader, "Material_Issue_Demand_Voucher_Id");
            issueDemandVoucherDOM.IssueDemandVoucherNumber = GetStringFromDataReader(reader, "Issue_Demand_Voucher_Number");
            issueDemandVoucherDOM.MaterialDemandDate = GetDateFromReader(reader, "Material_demand_Date");
            issueDemandVoucherDOM.Remarks = GetStringFromDataReader(reader, "Remarks");

            issueDemandVoucherDOM.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Document_Id");
            issueDemandVoucherDOM.Quotation.StatusType = new MetaData();
            issueDemandVoucherDOM.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            issueDemandVoucherDOM.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Name");
            issueDemandVoucherDOM.Quotation.CreatedDate = GetDateFromReader(reader, "Created_Date");

            return issueDemandVoucherDOM;
        }
        private ItemTransaction GenerateIssueDemandVoucherMappingFromDataReader(IDataReader reader)
        {
            ItemTransaction itemTransaction = new ItemTransaction();
            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Material_Issue_Demand_Voucher_Mapping_Id");
            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            itemTransaction.ItemRequired = GetDecimalFromDataReader(reader, "Item_Required");
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Unit_Left");

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Activity_Id");

            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.TaxId = GetIntegerFromDataReader(reader, "Material_Issue_Demand_Voucher_Id");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");

            return itemTransaction;
        }
        private MetaData GenerateIssueDemandVoucherInformationFromDataReader(IDataReader reader)
        {
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }
        #endregion
    }
}
