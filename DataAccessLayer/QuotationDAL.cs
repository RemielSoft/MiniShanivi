using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DocumentObjectModel;


namespace DataAccessLayer
{
    public class QuotationDAL : BaseDAL
    {
        #region Private Global Variable(s)

        private Database myDataBase;
        DbCommand dbCommand = null;

        Int32 id = 0;

        QuotationDOM quotation = null;
        MetaData metaData = null;
        ItemTransaction itemTransaction = null;

        List<QuotationDOM> lstQuotation = null;
        List<MetaData> lstMetaData = null;
        List<ItemTransaction> lstItemTransaction = null;
        #endregion

        #region Constructor

        public QuotationDAL(Database database)
        {
            myDataBase = database;
        }

        #endregion

        #region ContractorQuatation
        #region ContractorPurchaseOrder CRUD Methods

        public List<MetaData> CreatePurchaseOrder(QuotationDOM quotation)
        {
            lstMetaData = new List<MetaData>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_CONTRACTOR_PURCHASE_ORDER);

            if (quotation.ContractorQuotationId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Contractor_Purchase_Order_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Contractor_Purchase_Order_Id", DbType.Int32, quotation.ContractorQuotationId);

            myDataBase.AddInParameter(dbCommand, "in_Contractor_Id", DbType.Int32, quotation.ContractorId);
            myDataBase.AddInParameter(dbCommand, "in_Contract_Id", DbType.String, quotation.ContractId);
            myDataBase.AddInParameter(dbCommand, "in_Work_Order_Id", DbType.String, quotation.WorkOrderId);


            if (quotation.OrderDate == DateTime.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Order_Date", DbType.DateTime, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Order_Date", DbType.DateTime, quotation.OrderDate);

            if (quotation.UploadDocumentId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Upload_Documnet_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Upload_Documnet_Id", DbType.Int32, quotation.UploadDocumentId);

            myDataBase.AddInParameter(dbCommand, "in_Freight", DbType.Decimal, quotation.Freight);

            //myDataBase.AddInParameter(dbCommand, "in_Packaging", DbType.Decimal, quotation.Packaging);

            if (quotation.TotalNetValue == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Total_Net_Value", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Total_Net_Value", DbType.Decimal, quotation.TotalNetValue);

            myDataBase.AddInParameter(dbCommand, "in_Status_Type_Id", DbType.Int32, quotation.StatusType.Id);


            myDataBase.AddInParameter(dbCommand, "in_Tax_Type", DbType.String, quotation.TaxType);
            myDataBase.AddInParameter(dbCommand, "@in_subject_Description", DbType.String, quotation.subjectdescription);
            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, quotation.CreatedBy);
            myDataBase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, "");

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = GenerateDataReader(reader);
                    lstMetaData.Add(metaData);
                }
            }

            return lstMetaData;
        }

        public Int32 CreatePurchaseOrderMapping(ItemTransaction item_Transaction, Int32 contractor_PO_Id)
        {
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_CONTRACTOR_PURCHASE_ORDER_MAPPING);

            ///Is to be Done As unable to Find DeliverySchedule.Id == 0 allotment
            if (item_Transaction.DeliverySchedule.Id == 0)
                myDataBase.AddInParameter(dbCommand, "in_Contractor_PO_Mapping_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Contractor_PO_Mapping_Id", DbType.Int32, item_Transaction.DeliverySchedule.Id);

            myDataBase.AddInParameter(dbCommand, "in_Contractor_PO_Id", DbType.Int32, contractor_PO_Id);

            myDataBase.AddInParameter(dbCommand, "in_Activity_Description_Id", DbType.Int32, item_Transaction.DeliverySchedule.ActivityDescriptionId);

            myDataBase.AddInParameter(dbCommand, "in_Activity_Description", DbType.String, item_Transaction.DeliverySchedule.ActivityDescription);

            if (item_Transaction.Item.ModelSpecification.Category.ItemCategoryId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Item_Category_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Item_Category_Id", DbType.Int32, item_Transaction.Item.ModelSpecification.Category.ItemCategoryId);
            myDataBase.AddInParameter(dbCommand, "in_Item_Category_Name", DbType.String, item_Transaction.Item.ModelSpecification.Category.ItemCategoryName);

            if (item_Transaction.Item.ItemId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Item_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Item_Id", DbType.Int32, item_Transaction.Item.ItemId);
            myDataBase.AddInParameter(dbCommand, "in_Item_Name", DbType.String, item_Transaction.Item.ItemName);

            if (item_Transaction.Item.ModelSpecification.ModelSpecificationId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Item_Model_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Item_Model_Id", DbType.Int32, item_Transaction.Item.ModelSpecification.ModelSpecificationId);
            myDataBase.AddInParameter(dbCommand, "in_Item_Model_Name", DbType.String, item_Transaction.Item.ModelSpecification.ModelSpecificationName);

            myDataBase.AddInParameter(dbCommand, "in_Measurement_Unit_Id", DbType.Int32, item_Transaction.Item.ModelSpecification.UnitMeasurement.Id);
            myDataBase.AddInParameter(dbCommand, "in_Measurement_Unit_Name", DbType.String, item_Transaction.Item.ModelSpecification.UnitMeasurement.Name);

            // myDataBase.AddInParameter(dbCommand, "in_Make", DbType.String, item_Transaction.Item.ModelSpecification.Brand.BrandName);


            myDataBase.AddInParameter(dbCommand, "in_Quantity", DbType.Decimal, item_Transaction.NumberOfUnit);

            myDataBase.AddInParameter(dbCommand, "in_Per_Unit_Cost", DbType.Decimal, item_Transaction.PerUnitCost);

            //myDataBase.AddInParameter(dbCommand, "in_Item_Number", DbType.String, item_Transaction.Service_Detail.ItemNumber);

            //myDataBase.AddInParameter(dbCommand, "in_Service_Number", DbType.String, item_Transaction.Service_Detail.ServiceNumber);

            //myDataBase.AddInParameter(dbCommand, "in_Service_Description", DbType.String, item_Transaction.Service_Detail.ServiceDescription);

            //myDataBase.AddInParameter(dbCommand, "in_Quantity", DbType.Decimal, item_Transaction.Service_Detail.Quantity);

            //myDataBase.AddInParameter(dbCommand, "in_Quantity_Issued", DbType.Decimal, item_Transaction.Service_Detail.QuantityIssued);

            //myDataBase.AddInParameter(dbCommand, "in_Measurement_Unit", DbType.Int32, item_Transaction.Service_Detail.Unit.Id);
            //myDataBase.AddInParameter(dbCommand, "in_Measurement_Unit_Name", DbType.String, item_Transaction.Service_Detail.Unit.Name);

            //myDataBase.AddInParameter(dbCommand, "in_Unit_Rate", DbType.Decimal, item_Transaction.Service_Detail.UnitRate);

            //myDataBase.AddInParameter(dbCommand, "in_Applicable_Rate", DbType.Decimal, item_Transaction.Service_Detail.ApplicableRate);
            if (item_Transaction.Discount_Rates == Decimal.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Discount_Rate", DbType.Decimal, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Discount_Rate", DbType.Decimal, item_Transaction.Discount_Rates);

            if (item_Transaction.TaxInformation.DiscountMode.Id == 0)
                myDataBase.AddInParameter(dbCommand, "in_Discount_Type", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Discount_Type", DbType.Int32, item_Transaction.TaxInformation.DiscountMode.Id);

            if (item_Transaction.PerUnitDiscount == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Per_Unit_Discount", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Per_Unit_Discount", DbType.Decimal, item_Transaction.PerUnitDiscount);

            if (item_Transaction.TaxInformation.ExciseDuty == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Excise_Duty", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Excise_Duty", DbType.Decimal, item_Transaction.TaxInformation.ExciseDuty);


            if (item_Transaction.TaxInformation.ServiceTax == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Service_Tax", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Service_Tax", DbType.Decimal, item_Transaction.TaxInformation.ServiceTax);

            if (item_Transaction.TaxInformation.VAT == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_VAT", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_VAT", DbType.Decimal, item_Transaction.TaxInformation.VAT);

            if (item_Transaction.TaxInformation.CSTWithCForm == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_CST_with_C_Form", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_CST_with_C_Form", DbType.Decimal, item_Transaction.TaxInformation.CSTWithCForm);

            if (item_Transaction.TaxInformation.CSTWithCForm == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_CST_without_C_Form", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_CST_without_C_Form", DbType.Decimal, item_Transaction.TaxInformation.CSTWithoutCForm);

            myDataBase.AddInParameter(dbCommand, "in_TotalAmount", DbType.Decimal, item_Transaction.TotalAmount);
            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, item_Transaction.CreatedBy);

            myDataBase.AddOutParameter(dbCommand, "@out_Contractor_PO_Mapping_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Contractor_PO_Mapping_Id").ToString(), out id);
            return id;

        }

        public List<QuotationDOM> ReadContractorQuotation(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String quotationNo)
        {
            lstQuotation = new List<QuotationDOM>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_QUOTATION);
            if (contractorId == 0)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, contractorId);
            if (toDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }

            if (contractNo == string.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, contractNo);
            if (quotationNo == string.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Quotation_No", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Quotation_No", DbType.String, quotationNo);

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateContractorQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadContractorQuotationView(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String WorkOrderNo)
        {
            lstQuotation = new List<QuotationDOM>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_ORDER);
            if (contractorId == 0)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, contractorId);
            if (toDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }

            if (contractNo == String.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, contractNo);

            if (WorkOrderNo == String.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Work_Order_No", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Work_Order_No", DbType.String, WorkOrderNo);


            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateContractorQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadContractorQuotation(DateTime toDate, DateTime fromDate)
        {
            lstQuotation = new List<QuotationDOM>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_QUOTATION_BY_STATUS_TYPE);

            if (toDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateContractorQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadContractorQuotation(Int32 StatusTypeId)
        {
            lstQuotation = new List<QuotationDOM>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_QUOTATION_BY_STATUS_TYPE);
            myDataBase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, StatusTypeId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateContractorQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadContractorQuotation(Int32? quotationId, String quotationNumber)
        {
            lstQuotation = new List<QuotationDOM>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_QUOTATION_BY_Id_NUMBER);
            myDataBase.AddInParameter(dbCommand, "in_Quotation_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "in_Quotation_Type", DbType.String, DBNull.Value);
            myDataBase.AddInParameter(dbCommand, "in_Contractor_Purchase_Order_Number", DbType.String, quotationNumber);


            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateContractorQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;

        }

        public List<ItemTransaction> ReadContractorQuotationMapping(Int32 quotationId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_QUOTATION_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateContractorQuotationMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }

            return lstItemTransaction;
        }

        public List<MetaData> ReadQuotationStatusMetaData()
        {
            lstMetaData = new List<MetaData>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_QUOTATION_STATUS_METADATA);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = GenerateDataReader(reader);
                    lstMetaData.Add(metaData);
                }
            }

            return lstMetaData;
        }

        public Int32 UpdateContractorQuotationStatus(QuotationDOM quotation)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.UPDATE_CONTRACTOR_QUOTATION_STATUS);
            myDataBase.AddInParameter(dbCommand, "@in_Contractor_Purchase_Order_Id", DbType.Int32, quotation.ContractorQuotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, quotation.StatusType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Approved_Rejected_By", DbType.String, quotation.ApprovedRejectedBy);
            myDataBase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, quotation.RemarkReject);

            myDataBase.AddOutParameter(dbCommand, "@out_QuotaionId", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_QuotaionId").ToString(), out id);
            return id;
        }

        public void DeleteContractorQuotation(Int32 quotationId, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_CONTRACTOR_QUOTATION);
            myDataBase.AddInParameter(dbCommand, "@in_Contractor_Quotation_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public void DeleteContractorQuotationMapping(Int32 quotationId, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_CONTRACTOR_QUOTATION_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Contractor_Quotation_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public void ResetQuotationMapping(Int32 quotationId, Int32 quotationType)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.RESET_QUOTATION_MAPPING);
            myDataBase.AddInParameter(dbCommand, "in_Quotation_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "in_Quotation_Type", DbType.Int32, quotationType);

            myDataBase.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region Private Section

        private QuotationDOM GenerateContractorQuotationFromDataReader(IDataReader reader)
        {
            quotation = new QuotationDOM();
            quotation.Contractor = new Contractor();

            quotation.Contractor.Address = GetStringFromDataReader(reader, "Address");
            quotation.Contractor.Information = new Information();
            quotation.Contractor.Information.ContactPersonName = GetStringFromDataReader(reader, "ContactPersonName");
            quotation.Contractor.Information.ContactPersonMobileNo = GetStringFromDataReader(reader, "ContactPersonMobileNo");

            quotation.ContractorQuotationId = GetIntegerFromDataReader(reader, "Contractor_Purchase_Order_Id");
            quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Purchase_Order_Number");

            quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");

            quotation.ContractId = GetIntegerFromDataReader(reader, "Contract_Id");
            quotation.CompanyWorkOrderNumber = GetStringFromDataReader(reader, "Company_Work_Order_Number");

            quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");

            quotation.WorkOrderId = GetIntegerFromDataReader(reader, "Work_Order_Id");
            quotation.WorkOrderNumber = GetStringFromDataReader(reader, "Work_Order_Number");

            quotation.OrderDate = GetDateFromReader(reader, "Order_Date");
            quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");
            quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Net_Value");

            quotation.StatusType = new MetaData();
            quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");

            quotation.ApprovedRejectedBy = GetStringFromDataReader(reader, "Approved_Rejected_By");
            quotation.ApprovedRejectedDate = GetDateFromReader(reader, "Approved_Rejected_Date");
            quotation.IsGenerated = GetShortIntegerFromDataReader(reader, "IsGenerated");
            quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
            quotation.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            quotation.CreatedDate = GetDateFromReader(reader, "Created_Date");
            quotation.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            quotation.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            quotation.RemarkReject = GetStringFromDataReader(reader, "Remark_approve_Reject");


            //quotation.Freight = GetDecimalFromDataReader(reader, "Freight");
            //quotation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            //if (quotation.Freight == Decimal.MinValue)
            //    quotation.Freight = 0;
            //if (quotation.Packaging == Decimal.MinValue)
            //    quotation.Packaging = 0;

            return quotation;
        }

        private ItemTransaction GenerateContractorQuotationMappingFromDataReader(IDataReader reader)
        {
            itemTransaction = new ItemTransaction();

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Contractor_PO_Mapping_Id");

            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.ActivityDescriptionId = GetIntegerFromDataReader(reader, "Activity_Description_Id");



            //itemTransaction.Service_Detail = new ServiceDetailDOM();
            //itemTransaction.Service_Detail.ItemNumber = GetStringFromDataReader(reader, "Item_Number");
            //itemTransaction.Service_Detail.ServiceNumber = GetStringFromDataReader(reader, "Service_Number");
            //itemTransaction.Service_Detail.Quantity = GetDecimalFromDataReader(reader, "Quantity");
            //itemTransaction.Service_Detail.QuantityIssued = GetDecimalFromDataReader(reader, "Quantity_Issued");
            //itemTransaction.Service_Detail.UnitRate = GetDecimalFromDataReader(reader, "Unit_Rate");
            //itemTransaction.Service_Detail.ApplicableRate = GetDecimalFromDataReader(reader, "Applicable_Rate");



            itemTransaction.Item = new Item();

            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            //itemTransaction.DeliverySchedule.ItemId = GetIntegerFromDataReader(reader, "Item_Id");

            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item_Name");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            //itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Make");

            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.DeliverySchedule.SpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");

            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            //itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Make");

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Item_Category_Id");
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");

            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Measurement_Unit_Id");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Measurement_Unit_Name");


            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");

            itemTransaction.NumberOfUnit = GetIntegerFromDataReader(reader, "Quantity");

            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            if (itemTransaction.UnitIssued < 0)
            {
                itemTransaction.UnitIssued = 0;
            }
            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Unit_Left");
            if (itemTransaction.UnitLeft < 0)
            {
                itemTransaction.UnitLeft = 0;
            }
            //itemTransaction.BilledUnit = GetDecimalFromDataReader(reader, "Billed_No_Of_Unit");
            //itemTransaction.UnitForBilled = GetDecimalFromDataReader(reader, "Unit_For_Billed");
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");

            itemTransaction.Discount_Rates = GetDecimalFromDataReader(reader, "Discount_Rate");
            if (itemTransaction.Discount_Rates <= 0)
            {
                itemTransaction.Discount_Rates = 0;
            }


            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.DiscountMode = new MetaData();
            itemTransaction.TaxInformation.DiscountMode.Id = GetIntegerFromDataReader(reader, "Discount_Type");
            itemTransaction.TaxInformation.DiscountMode.Name = GetStringFromDataReader(reader, "Discount_Type_Name");

            itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");


            itemTransaction.TaxInformation.ExciseDuty = GetDecimalFromDataReader(reader, "Excise_Duty");
            itemTransaction.TaxInformation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
            itemTransaction.TaxInformation.VAT = GetDecimalFromDataReader(reader, "VAT");
            itemTransaction.TaxInformation.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_with_C_Form");
            itemTransaction.TaxInformation.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_without_C_Form");
            //itemTransaction.TaxInformation.Freight = GetDecimalFromDataReader(reader, "Freight");
            //itemTransaction.TaxInformation.Packaging = GetDecimalFromDataReader(reader, "Packaging");

            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "TotalAmount");
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            itemTransaction.CreatedDate = GetDateFromReader(reader, "Created_Date");
            itemTransaction.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            itemTransaction.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            return itemTransaction;
        }

        private MetaData GenerateDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }

        #endregion

        #endregion

        #region SupplierQuotation

        #region SupplierQuotation CRUD Method

        public List<MetaData> CreateSupplierPurchaseOrder(QuotationDOM quotation)
        {
            lstMetaData = new List<MetaData>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_SUPPLIER_PURCHASE_ORDER);

            if (quotation.SupplierQuotationId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Supplier_Purchase_Order_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Supplier_Purchase_Order_Id", DbType.Int32, quotation.SupplierQuotationId);

            myDataBase.AddInParameter(dbCommand, "in_Supplier_Id", DbType.Int32, quotation.SupplierId);

            if (quotation.OrderDate == DateTime.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Quotation_Order_Date", DbType.DateTime, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Quotation_Order_Date", DbType.DateTime, quotation.OrderDate);

            if (quotation.UploadDocumentId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Upload_Documnet_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Upload_Documnet_Id", DbType.Int32, quotation.UploadDocumentId);

            if (quotation.ClosingDate == DateTime.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Closing_Date", DbType.DateTime, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Closing_Date", DbType.DateTime, quotation.ClosingDate);

            myDataBase.AddInParameter(dbCommand, "in_Delivery_Description", DbType.String, quotation.DeliveryDescription);
            myDataBase.AddInParameter(dbCommand, "in_Subject_Description", DbType.String, quotation.subjectdescription);

            //if (quotation.DiscountType.Id == 0)
            //    myDataBase.AddInParameter(dbCommand, "in_Discount_Type", DbType.Int32, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_Discount_Type", DbType.Int32, quotation.DiscountType.Id);

            //if (quotation.ServiceTax == Decimal.MinValue)
            //    myDataBase.AddInParameter(dbCommand, "in_Service_Tax", DbType.Decimal, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_Service_Tax", DbType.Decimal, quotation.ServiceTax);

            //if (quotation.VAT == Decimal.MinValue)
            //    myDataBase.AddInParameter(dbCommand, "in_VAT", DbType.Decimal, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_VAT", DbType.Decimal, quotation.VAT);

            //if (quotation.CSTwith_C_Form == Decimal.MinValue)
            //    myDataBase.AddInParameter(dbCommand, "in_CST_with_C_Form", DbType.Decimal, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_CST_with_C_Form", DbType.Decimal, quotation.CSTwith_C_Form);

            //if (quotation.CSTWithout_C_Form == Decimal.MinValue)
            //    myDataBase.AddInParameter(dbCommand, "in_CST_without_C_Form", DbType.Decimal, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_CST_without_C_Form", DbType.Decimal, quotation.CSTWithout_C_Form);

            //if (quotation.Freight == Decimal.MinValue)
            //    myDataBase.AddInParameter(dbCommand, "in_Freight", DbType.Decimal, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_Freight", DbType.Decimal, quotation.Freight);

            //if (quotation.Packaging == Decimal.MinValue)
            //    myDataBase.AddInParameter(dbCommand, "in_Packaging", DbType.Decimal, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_Packaging", DbType.Decimal, quotation.Packaging);

            if (quotation.TotalNetValue == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Total_Net_Value", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Total_Net_Value", DbType.Decimal, quotation.TotalNetValue);

            myDataBase.AddInParameter(dbCommand, "in_Status_Type_Id", DbType.Int32, quotation.StatusType.Id);

            if (quotation.Packaging == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Packaging", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Packaging", DbType.Decimal, quotation.Packaging);

            if (quotation.Freight == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Freight", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Freight", DbType.Decimal, quotation.Freight);


            //------------------------sundeep----------------
            myDataBase.AddInParameter(dbCommand, "in_Tax_Type", DbType.String, quotation.TaxType);
            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, quotation.CreatedBy);

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = GenerateDataReader(reader);
                    lstMetaData.Add(metaData);
                }
            }

            return lstMetaData;
        }

        public Int32 CreateSupplierPurchaseOrderMapping(ItemTransaction item_Transaction, Int32 supplier_PO_Id)
        {
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_SUPPLIER_PURCHASE_ORDER_MAPPING);

            if (item_Transaction.DeliverySchedule.Id == 0)
                myDataBase.AddInParameter(dbCommand, "in_Supplier_PO_Mapping_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Supplier_PO_Mapping_Id", DbType.Int32, item_Transaction.DeliverySchedule.Id);


            myDataBase.AddInParameter(dbCommand, "in_Supplier_PO_Id", DbType.Int32, supplier_PO_Id);

            myDataBase.AddInParameter(dbCommand, "in_Activity_Discription", DbType.String, item_Transaction.DeliverySchedule.ActivityDescription);

            if (item_Transaction.Item.ModelSpecification.Category.ItemCategoryId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Item_Category_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Item_Category_Id", DbType.Int32, item_Transaction.Item.ModelSpecification.Category.ItemCategoryId);
            myDataBase.AddInParameter(dbCommand, "in_Item_Category_Name", DbType.String, item_Transaction.Item.ModelSpecification.Category.ItemCategoryName);

            if (item_Transaction.Item.ItemId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Item_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Item_Id", DbType.Int32, item_Transaction.Item.ItemId);
            myDataBase.AddInParameter(dbCommand, "in_Item_Name", DbType.String, item_Transaction.Item.ItemName);

            if (item_Transaction.Item.ModelSpecification.ModelSpecificationId == 0)
                myDataBase.AddInParameter(dbCommand, "in_Item_Model_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Item_Model_Id", DbType.Int32, item_Transaction.Item.ModelSpecification.ModelSpecificationId);
            myDataBase.AddInParameter(dbCommand, "in_Item_Model_Name", DbType.String, item_Transaction.Item.ModelSpecification.ModelSpecificationName);

            myDataBase.AddInParameter(dbCommand, "in_Measurement_Unit_Id", DbType.Int32, item_Transaction.Item.ModelSpecification.UnitMeasurement.Id);
            myDataBase.AddInParameter(dbCommand, "in_Measurement_Unit_Name", DbType.String, item_Transaction.Item.ModelSpecification.UnitMeasurement.Name);

            myDataBase.AddInParameter(dbCommand, "in_Make", DbType.String, item_Transaction.Item.ModelSpecification.Brand.BrandName);

            myDataBase.AddInParameter(dbCommand, "in_Number_Of_Unit", DbType.Decimal, item_Transaction.NumberOfUnit);

            myDataBase.AddInParameter(dbCommand, "in_Per_Unit_Cost", DbType.Decimal, item_Transaction.PerUnitCost);

            if (item_Transaction.TaxInformation.DiscountMode.Id == 0)
                myDataBase.AddInParameter(dbCommand, "in_Discount_Type", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Discount_Type", DbType.Int32, item_Transaction.TaxInformation.DiscountMode.Id);

            if (item_Transaction.Discount_Rates == Decimal.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Discount_Rate", DbType.Decimal, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Discount_Rate", DbType.Decimal, item_Transaction.Discount_Rates);



            if (item_Transaction.PerUnitDiscount == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Per_Unit_Discount", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Per_Unit_Discount", DbType.Decimal, item_Transaction.PerUnitDiscount);

            if (item_Transaction.TaxInformation.ServiceTax == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Service_Tax", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Service_Tax", DbType.Decimal, item_Transaction.TaxInformation.ServiceTax);

            if (item_Transaction.TaxInformation.VAT == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_VAT", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_VAT", DbType.Decimal, item_Transaction.TaxInformation.VAT);

            if (item_Transaction.TaxInformation.CSTWithCForm == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_CST_with_C_Form", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_CST_with_C_Form", DbType.Decimal, item_Transaction.TaxInformation.CSTWithCForm);

            if (item_Transaction.TaxInformation.CSTWithCForm == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_CST_without_C_Form", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_CST_without_C_Form", DbType.Decimal, item_Transaction.TaxInformation.CSTWithoutCForm);

            if (item_Transaction.TaxInformation.ExciseDuty == Decimal.MinValue)
                myDataBase.AddInParameter(dbCommand, "in_Excise_Duty", DbType.Decimal, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Excise_Duty", DbType.Decimal, item_Transaction.TaxInformation.ExciseDuty);



            //if (item_Transaction.TaxInformation.Freight == Decimal.MinValue)
            //    myDataBase.AddInParameter(dbCommand, "in_Freight", DbType.Decimal, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_Freight", DbType.Decimal, item_Transaction.TaxInformation.Freight);

            //if (item_Transaction.TaxInformation.Packaging == Decimal.MinValue)
            //    myDataBase.AddInParameter(dbCommand, "in_Packaging", DbType.Decimal, DBNull.Value);
            //else
            //    myDataBase.AddInParameter(dbCommand, "in_Packaging", DbType.Decimal, item_Transaction.TaxInformation.Packaging);

            myDataBase.AddInParameter(dbCommand, "in_TotalAmount", DbType.Decimal, item_Transaction.TotalAmount);
            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, item_Transaction.CreatedBy);

            myDataBase.AddOutParameter(dbCommand, "@out_Supplier_PO_Mapping_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Supplier_PO_Mapping_Id").ToString(), out id);
            return id;

        }

        public List<QuotationDOM> ReadSupplierQuotation(Int32? quotationId, String quotationNumber)
        {
            lstQuotation = new List<QuotationDOM>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_QUOTATION_BY_Id_NUMBER);
            myDataBase.AddInParameter(dbCommand, "in_Quotation_Id", DbType.Int32, quotationId);
            //myDataBase.AddInParameter(dbCommand, "in_Quotation_Type", DbType.String, DBNull.Value);
            myDataBase.AddInParameter(dbCommand, "in_Supplier_Purchase_Order_Number", DbType.String, quotationNumber);

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateSupplierQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;

        }

        public List<QuotationDOM> ReadSupplierQuotation(Int32 supplierId, DateTime toDate, DateTime fromDate, String contractNo, String quotationNo)
        {
            lstQuotation = new List<QuotationDOM>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_QUOTATION);
            if (supplierId == 0)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, supplierId);
            if (toDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }

            if (contractNo == string.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, contractNo);
            if (quotationNo == string.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Quotation_No", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Quotation_No", DbType.String, quotationNo);

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateSupplierQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadSupplierQuotation(DateTime toDate, DateTime fromDate)
        {
            lstQuotation = new List<QuotationDOM>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_QUOTATION_BY_STATUS_TYPE);

            if (toDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateSupplierQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;
        }

        public List<ItemTransaction> ReadSupplierQuotationMapping(Int32 quotationId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_QUOTATION_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateSupplierQuotationMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }

            return lstItemTransaction;
        }

        public List<QuotationDOM> ReadSupplierQuotation(Int32 StatusTypeId)
        {
            lstQuotation = new List<QuotationDOM>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_QUOTATION_BY_STATUS_TYPE);
            myDataBase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, StatusTypeId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateSupplierQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;
        }

        public List<QuotationDOM> ReadSupplierQuotationView(Int32 supplierId, DateTime toDate, DateTime fromDate, String WorkOrderNo)
        {
            lstQuotation = new List<QuotationDOM>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_ORDER);
            if (supplierId == 0)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, supplierId);
            if (toDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_To_Date", DbType.DateTime, toDate);
            }
            if (fromDate == DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, DBNull.Value);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_From_Date", DbType.DateTime, fromDate);
            }

            if (WorkOrderNo == String.Empty)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Work_Order_No", DbType.String, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Work_Order_No", DbType.String, WorkOrderNo);


            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    quotation = GenerateSupplierQuotationFromDataReader(reader);
                    lstQuotation.Add(quotation);
                }
            }
            return lstQuotation;
        }

        public Int32 UpdateSupplierQuotationStatus(QuotationDOM quotation)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.UPDATE_SUPPLIER_QUOTATION_STATUS);
            myDataBase.AddInParameter(dbCommand, "@in_Supplier_Purchase_Order_Id", DbType.Int32, quotation.SupplierQuotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, quotation.StatusType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Approved_Rejected_By", DbType.String, quotation.ApprovedRejectedBy);

            myDataBase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, quotation.RemarkReject);



            myDataBase.AddOutParameter(dbCommand, "@out_QuotaionId", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_QuotaionId").ToString(), out id);
            return id;
        }

        public void DeleteSupplierQuotation(Int32 quotationId, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_SUPPLIER_QUOTATION);
            myDataBase.AddInParameter(dbCommand, "@in_Supplier_Quotation_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public void DeleteSupplierQuotationMapping(Int32 quotationId, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_SUPPLIER_QUOTATION_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Supplier_Quotation_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }
        #endregion

        #region Private Section

        private QuotationDOM GenerateSupplierQuotationFromDataReader(IDataReader reader)
        {
            quotation = new QuotationDOM();
            //quotation.DiscountType = new MetaData();
            quotation.StatusType = new MetaData();
            quotation.RemarkReject = GetStringFromDataReader(reader, "Remark_approve_Reject");
            quotation.SupplierQuotationId = GetIntegerFromDataReader(reader, "Supplier_Purchase_Order_Id");
            quotation.SupplierQuotationNumber = GetStringFromDataReader(reader, "Supplier_Purchase_Order_Number");
            quotation.SupplierId = GetIntegerFromDataReader(reader, "Supplier_Id");
            quotation.SupplierName = GetStringFromDataReader(reader, "Supplier_Name");
            quotation.Supplier = new Supplier();
            quotation.Supplier.Address = GetStringFromDataReader(reader, "Supplier_Address");
            quotation.Supplier.Information = new Information();
            quotation.Supplier.Information.ContactPersonName = GetStringFromDataReader(reader, "ContactPersonName");
            quotation.Supplier.Information.ContactPersonMobileNo = GetStringFromDataReader(reader, "ContactPersonMobile");
            quotation.OrderDate = GetDateFromReader(reader, "Quotation_Order_Date");
            quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");
            quotation.ClosingDate = GetDateFromReader(reader, "Closing_Date");
            quotation.DeliveryDescription = GetStringFromDataReader(reader, "Delivery_Description");
            quotation.subjectdescription = GetStringFromDataReader(reader, "Subject_Description");

            quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Net_Value");
            quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");

            quotation.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            quotation.CreatedDate = GetDateFromReader(reader, "Created_Date");
            quotation.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            quotation.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            quotation.ApprovedRejectedBy = GetStringFromDataReader(reader, "Approved_Rejected_By");
            quotation.ApprovedRejectedDate = GetDateFromReader(reader, "Approved_Rejected_Date");
            quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
            quotation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            quotation.Freight = GetDecimalFromDataReader(reader, "Freight");
            if (quotation.Packaging == Decimal.MinValue)
                quotation.Packaging = 0;
            if (quotation.Freight == Decimal.MinValue)
                quotation.Freight = 0;

            return quotation;
        }

        private ItemTransaction GenerateSupplierQuotationMappingFromDataReader(IDataReader reader)
        {
            itemTransaction = new ItemTransaction();

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();

            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Supplier_PO_Mapping_Id");

            // itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Discription");

            itemTransaction.Item = new Item();

            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item_Name");

            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand_Name");
            itemTransaction.Item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemTransaction.Item.ModelSpecification.Store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            itemTransaction.Item.ModelSpecification.Store.StoreName = GetStringFromDataReader(reader, "Store_Name");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");


            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Measurement_Unit_Id");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Measurement_Unit_Name");

            //For Delivery Schedule of Supplier[Manveer]
            itemTransaction.DeliverySchedule.ItemDescription = itemTransaction.Item.FinalActivityDescription;
            itemTransaction.DeliverySchedule.SpecificationId = itemTransaction.Item.ModelSpecification.ModelSpecificationId;
            itemTransaction.DeliverySchedule.SpecificationUnit = itemTransaction.Item.ModelSpecification.UnitMeasurement.Name;
            //itemTransaction.DeliverySchedule.SpecificationUnit = itemTransaction.Item.ModelSpecification.UnitMeasurement.Name;
            //End

            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Item_Category_Id");
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");

            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Item_Required");
            if (itemTransaction.UnitIssued < 0)
            {
                itemTransaction.UnitIssued = 0;
            }
            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Quantity_Left");
            if (itemTransaction.UnitLeft < 0)
            {
                itemTransaction.UnitLeft = 0;
            }
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");
            itemTransaction.Discount_Rates = GetDecimalFromDataReader(reader, "Discount_Rate");
            if (itemTransaction.Discount_Rates <= 0)
            {
                itemTransaction.Discount_Rates = 0;
            }

            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.DiscountMode = new MetaData();
            itemTransaction.TaxInformation.DiscountMode.Id = GetIntegerFromDataReader(reader, "Discount_Type");
            itemTransaction.TaxInformation.DiscountMode.Name = GetStringFromDataReader(reader, "Discount_Type_Name");

            itemTransaction.TaxInformation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
            itemTransaction.TaxInformation.VAT = GetDecimalFromDataReader(reader, "VAT");
            itemTransaction.TaxInformation.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_with_C_Form");
            itemTransaction.TaxInformation.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_without_C_Form");

            itemTransaction.TaxInformation.ExciseDuty = GetDecimalFromDataReader(reader, "excise_duty");
            if (itemTransaction.TaxInformation.ExciseDuty == Decimal.MinValue)
                itemTransaction.TaxInformation.ExciseDuty = 0;
            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "TotalAmount");
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            itemTransaction.CreatedDate = GetDateFromReader(reader, "Created_Date");
            itemTransaction.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            itemTransaction.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            itemTransaction.PreviousUnitRate = GetStringFromDataReader(reader, "PreviousUnitRate");
            return itemTransaction;
        }

        //private QuotationDOM GenerateSupplierQuotationFromDataReader(IDataReader reader)
        //{
        //    quotation = new QuotationDOM();

        //    quotation.ContractorQuotationId = GetIntegerFromDataReader(reader, "Contractor_Purchase_Order_Id");
        //    quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Purchase_Order_Number");
        //    quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
        //    quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
        //    quotation.QuotationDate = GetDateFromReader(reader, "Order_Date");
        //    quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
        //    quotation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
        //    quotation.VAT = GetDecimalFromDataReader(reader, "VAT");
        //    quotation.CSTwith_C_Form = GetDecimalFromDataReader(reader, "CST_with_C_Form");
        //    quotation.CSTWithout_C_Form = GetDecimalFromDataReader(reader, "CST_without_C_Form");
        //    quotation.Freight = GetDecimalFromDataReader(reader, "Freight");
        //    quotation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
        //    quotation.DiscountType = new MetaData();
        //    quotation.DiscountType.Id = GetIntegerFromDataReader(reader, "Discount_Type");
        //    quotation.DiscountType.Name = GetStringFromDataReader(reader, "Discount_Type_Name");
        //    quotation.TotalDiscount = GetDecimalFromDataReader(reader, "Total_Discount");
        //    quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Net_Value");
        //    quotation.StatusType = new MetaData();
        //    quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
        //    quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
        //    quotation.OrderDate = GetDateFromReader(reader, "Order_Date");
        //    quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");

        //    quotation.ApprovedRejectedBy = GetStringFromDataReader(reader, "Approved_Rejected_By");
        //    quotation.ApprovedRejectedDate = GetDateFromReader(reader, "Approved_Rejected_Date");
        //    quotation.IsGenerated = GetShortIntegerFromDataReader(reader, "IsGenerated");
        //    quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
        //    quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
        //    quotation.CreatedBy = GetStringFromDataReader(reader, "Created_By");
        //    quotation.CreatedDate = GetDateFromReader(reader, "Created_Date");
        //    quotation.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
        //    quotation.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

        //    return quotation;
        //}


        #endregion

        #endregion
    }
}
