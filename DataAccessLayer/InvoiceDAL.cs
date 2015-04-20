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
    public class InvoiceDAL:BaseDAL
    {
        //#region private global variables
        //private Database myDataBase;
        //DbCommand dbCommand = null;

        //ItemTransaction itemTransaction = null;
        //MetaData metaData = null;
        //QuotationDOM quotation = null;
        //InvoiceDOM invoice = null;

        //List<QuotationDOM> lstQuotation = null;
        //List<InvoiceDOM> lstInvoice = null;
        //List<ItemTransaction> lstItemTransaction = null;
        //int id = 0;
        //#endregion

        //#region Constructors
        //public InvoiceDAL(Database database)
        //{
        //    myDataBase = database;
        //}
        //#endregion

        //#region CURD Invoice

        //public List<InvoiceDOM> ReadContractorInvoice(Int32 StatusTypeId)
        //{
        //    lstInvoice = new List<InvoiceDOM>();
        //    dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_QUOTATION_BY_STATUS_TYPE);
        //    myDataBase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, StatusTypeId);
        //    using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            invoice = GenerateContractorInvoiceFromDataReader(reader);
        //            lstInvoice.Add(invoice);
        //        }
        //    }
        //    return lstInvoice;
        //}

        //public List<ItemTransaction> ReadContractorQuotationMapping(Int32 quotationId)
        //{
        //    lstItemTransaction = new List<ItemTransaction>();
        //    dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_QUOTATION_MAPPING);
        //    myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationId);
        //    using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            itemTransaction = GenerateContractorInvoiceMappingFromDataReader(reader);
        //            lstItemTransaction.Add(itemTransaction);
        //        }
        //    }

        //    return lstItemTransaction;
        //}

        //#endregion

        //#region Private Section

        //private InvoiceDOM GenerateContractorInvoiceFromDataReader(IDataReader reader)
        //{
        //    invoice = new InvoiceDOM();
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

        //    return invoice;
        //}

        //private ItemTransaction GenerateContractorInvoiceMappingFromDataReader(IDataReader reader)
        //{
        //    itemTransaction = new ItemTransaction();

        //    itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();

        //    itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Contractor_PO_Mapping_Id");

        //    itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Discription");

        //    itemTransaction.Item = new Item();
        //    itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
        //    itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item_Name");

        //    itemTransaction.Item.ModelSpecification = new ModelSpecification();
        //    itemTransaction.Item.ModelSpecification.Brand = GetStringFromDataReader(reader, "Make");
        //    //itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
        //    //itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");

        //    itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
        //    itemTransaction.Item.ModelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Item_Category_Id");
        //    itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");

        //    itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
        //    itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Measurement_Unit_Id");
        //    itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Measurement_Unit_Name");

        //    itemTransaction.NumberOfUnit = GetIntegerFromDataReader(reader, "Number_Of_Unit");

        //    itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
        //    if (itemTransaction.UnitIssued < 0)
        //    {
        //        itemTransaction.UnitIssued = 0;
        //    }
        //    itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Unit_Left");
        //    if (itemTransaction.UnitLeft < 0)
        //    {
        //        itemTransaction.UnitLeft = 0;
        //    }
        //    itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
        //    itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");
        //    itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "TotalAmount");
        //    itemTransaction.CreatedBy = GetStringFromDataReader(reader, "Created_By");
        //    itemTransaction.CreatedDate = GetDateFromReader(reader, "Created_Date");
        //    itemTransaction.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
        //    itemTransaction.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

        //    return itemTransaction;
        //}
        //#endregion
    }
}
