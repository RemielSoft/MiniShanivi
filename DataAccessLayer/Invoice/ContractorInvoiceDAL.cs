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
    public class ContractorInvoiceDAL : BaseDAL
    {
        #region private global variables

        private Database myDatabase;
        DbCommand dbCommand = null;

        InvoiceDom invoice = null;
        ItemTransaction itemTransaction = null;
        List<InvoiceDom> lstInvoice = null;
        List<ItemTransaction> lstItemTransaction = null;
        List<MetaData> lstmetaData = null;

        MetaData metaData = null;
        Int32 id = 0;
        #endregion

        #region Constructors

        public ContractorInvoiceDAL(Database database)
        {
            myDatabase = database;
        }

        #endregion

        #region Contractor Invoice CRUD Methods

        public List<InvoiceDom> ReadContractorQuotation(Int32? quotationId, String quotationNumber)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_QUTATION_BY_NUMBER);
            myDatabase.AddInParameter(dbCommand, "in_Quotation_Id", DbType.Int32, quotationId);
            myDatabase.AddInParameter(dbCommand, "in_Quotation_Type", DbType.String, DBNull.Value);
            myDatabase.AddInParameter(dbCommand, "in_Contractor_Purchase_Order_Number", DbType.String, quotationNumber);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateContractorQuotationFromDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;

        }

        public MetaData CreateContractorInvoice(InvoiceDom invoiceDom, Int32? INVId)
        {
            string SqlCommand = DBConstants.CREATE_CONTRACTOR_INVOICE;
            dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
            if (invoiceDom.ContractorInvoiceId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, invoiceDom.ContractorInvoiceId);
            }
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_WO_Id", DbType.Int32, invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Quotation_Number", DbType.String, invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Demand_Voucher_Number", DbType.String, invoiceDom.IssueMaterial.DemandVoucher.IssueDemandVoucherNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Number", DbType.String, invoiceDom.IssueMaterial.IssueMaterialNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Work_Order_Number", DbType.String, invoiceDom.IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractorId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Name", DbType.String, invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractorName);
            myDatabase.AddInParameter(dbCommand, "@in_Contract_Number", DbType.String, invoiceDom.IssueMaterial.DemandVoucher.Quotation.ContractNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Invoice_Date", DbType.DateTime, invoiceDom.InvoiceDate);
            myDatabase.AddInParameter(dbCommand, "@in_BillDate", DbType.DateTime, invoiceDom.BillDate);
            myDatabase.AddInParameter(dbCommand, "@in_BillNumber", DbType.String, invoiceDom.BillNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Remarks", DbType.String, invoiceDom.Remarks);
            myDatabase.AddInParameter(dbCommand, "@in_Document_Id", DbType.Int32, invoiceDom.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId);
            myDatabase.AddInParameter(dbCommand, "@in_Total_Amount", DbType.Decimal, invoiceDom.IssueMaterial.DemandVoucher.Quotation.TotalNetValue);
            myDatabase.AddInParameter(dbCommand, "@in_Invoiced_Amount", DbType.Decimal, invoiceDom.InvoicedAmount);
            myDatabase.AddInParameter(dbCommand, "@in_Freight", DbType.Decimal, invoiceDom.IssueMaterial.DemandVoucher.Quotation.Freight);
            myDatabase.AddInParameter(dbCommand, "@in_Packaging", DbType.Decimal, invoiceDom.IssueMaterial.DemandVoucher.Quotation.Packaging);
            myDatabase.AddInParameter(dbCommand, "@in_Payment_Type", DbType.Int32, invoiceDom.Payment.PaymentType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Payment_Term_Id", DbType.Int32, invoiceDom.Payment.PaymentTermId);
            myDatabase.AddInParameter(dbCommand, "@in_Invoice_Type", DbType.Int32, invoiceDom.InvoiceType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Percentage_Value", DbType.Decimal, invoiceDom.Payment.PercentageValue);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, invoiceDom.IssueMaterial.DemandVoucher.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, invoiceDom.CreatedBy);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = new MetaData();
                    metaData = GenerateContractorInvoiceFromDataReader(reader);
                }
            }
            return metaData;
        }

        public int CreateContractorInvoiceMapping(List<ItemTransaction> lstItemTransaction, Int32 ContractorInvoiceId)
        {
            id = 0;
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string SqlCommand = DBConstants.CREATE_CONTRACTOR_INVOICE_MAPPING;
                dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, ContractorInvoiceId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Mapping_Id", DbType.Int32, DBNull.Value);
                }
                if (itemTransaction.DeliverySchedule.Id == 0)
                    myDatabase.AddInParameter(dbCommand, "@in_Activity_Id", DbType.Int32, DBNull.Value);
                else
                    myDatabase.AddInParameter(dbCommand, "@in_Activity_Id", DbType.Int32, itemTransaction.DeliverySchedule.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Activity_Description", DbType.String, itemTransaction.DeliverySchedule.ActivityDescription);
                myDatabase.AddInParameter(dbCommand, "@in_Item", DbType.String, itemTransaction.Item.ItemName);
                myDatabase.AddInParameter(dbCommand, "@in_Specification", DbType.String, itemTransaction.Item.ModelSpecification.ModelSpecificationName);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category", DbType.String, itemTransaction.Item.ModelSpecification.Category.ItemCategoryName);
                myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_Number_Of_Unit", DbType.Decimal, itemTransaction.NumberOfUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Unit_Demanded", DbType.Decimal, itemTransaction.UnitDemanded);
                myDatabase.AddInParameter(dbCommand, "@in_Actual_No_Of_Unit", DbType.Decimal, itemTransaction.UnitIssued);
                myDatabase.AddInParameter(dbCommand, "@in_Billed_No_Of_Unit", DbType.Decimal, itemTransaction.UnitForBilled);
                myDatabase.AddInParameter(dbCommand, "@in_Per_Unit_Cost", DbType.Decimal, itemTransaction.PerUnitCost);
                myDatabase.AddInParameter(dbCommand, "@in_Per_Unit_Discount", DbType.Decimal, itemTransaction.PerUnitDiscount);
                myDatabase.AddInParameter(dbCommand, "@in_Discount_Type", DbType.Int32, itemTransaction.TaxInformation.DiscountMode.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Service_Tax", DbType.Decimal, itemTransaction.TaxInformation.ServiceTax);
                myDatabase.AddInParameter(dbCommand, "@in_VAT", DbType.Decimal, itemTransaction.TaxInformation.VAT);
                myDatabase.AddInParameter(dbCommand, "@in_CST_with_C_Form", DbType.Decimal, itemTransaction.TaxInformation.CSTWithCForm);
                myDatabase.AddInParameter(dbCommand, "@in_CST_without_C_Form", DbType.Decimal, itemTransaction.TaxInformation.CSTWithoutCForm);
                myDatabase.AddInParameter(dbCommand, "@in_Freight", DbType.Decimal, itemTransaction.TaxInformation.Freight);
                myDatabase.AddInParameter(dbCommand, "@in_Packaging", DbType.Decimal, itemTransaction.TaxInformation.Packaging);
                myDatabase.AddInParameter(dbCommand, "@in_Total_Amount", DbType.Decimal, itemTransaction.TotalAmount);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Contractor_Invoice_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);
                int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Contractor_Invoice_Mapping_Id").ToString(), out id);

            }
            return id;
        }

        public int CreateContractorInvoiceMappingAdvance(List<ItemTransaction> lstItemTransaction, Int32 ContractorInvoiceId)
        {
            id = 0;
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string SqlCommand = DBConstants.CREATE_CONTRACTOR_INVOICE_MAPPING_ADVANCE;
                dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, ContractorInvoiceId);
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Mapping_Id", DbType.Int32, DBNull.Value);
                myDatabase.AddInParameter(dbCommand, "@in_Advance_Value", DbType.Decimal, itemTransaction.AdvanceValue);
                myDatabase.AddInParameter(dbCommand, "@in_Service_Tax", DbType.Decimal, itemTransaction.TaxInformation.ServiceTax);
                myDatabase.AddInParameter(dbCommand, "@in_VAT", DbType.Decimal, itemTransaction.TaxInformation.VAT);
                myDatabase.AddInParameter(dbCommand, "@in_CST_with_C_Form", DbType.Decimal, itemTransaction.TaxInformation.CSTWithCForm);
                myDatabase.AddInParameter(dbCommand, "@in_CST_without_C_Form", DbType.Decimal, itemTransaction.TaxInformation.CSTWithoutCForm);
                myDatabase.AddInParameter(dbCommand, "@in_Total_Amount", DbType.Decimal, itemTransaction.TaxInformation.TotalNetValue);
                myDatabase.AddInParameter(dbCommand, "@in_Freight", DbType.Decimal, itemTransaction.TaxInformation.Freight);
                myDatabase.AddInParameter(dbCommand, "@in_Packaging", DbType.Decimal, itemTransaction.TaxInformation.Packaging);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Contractor_Invoice_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);

                int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Contractor_Invoice_Mapping_Id").ToString(), out id);

            }
            return id;
        }

        public void DeleteContractorInvoice(int ContractorInvoiceId, string modifiedBy, DateTime modifiedOn)
        {
            string sqlCommand = DBConstants.DELETE_CONTRACTOR_INVOICE;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_ContractorInvoice_Id", DbType.Int32, ContractorInvoiceId);
            myDatabase.AddInParameter(dbCommand, "@in_modifiedBy", DbType.String, modifiedBy);
            myDatabase.AddInParameter(dbCommand, "@in_modifiedDate", DbType.DateTime, modifiedOn);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        public List<ItemTransaction> ReadQuotationDemandVoucher(String IssueMaterialNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_QUOTATION_DEMAND_VOUCHER);
            myDatabase.AddInParameter(dbCommand, "@in_Issue_Material_Number", DbType.String, IssueMaterialNo);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateQuotationDemandVoucherFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }

            return lstItemTransaction;
        }

        public List<InvoiceDom> ReadInvoice(String invoiceNumber)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_INVOICE_BY_INVOICE_NUMBER);
            myDatabase.AddInParameter(dbCommand, "@in_Invoice_Number", DbType.String, invoiceNumber);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateInvoiceFromDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;
        }

        public List<InvoiceDom> ReadContractorInvoice(Int32? ContractorInvoiceId, Int32? contractorId, DateTime toDate, DateTime fromDate, String InvoiceNo, String ContractorWorkOrderNo, string contractorName)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_INVOICE);
            if (ContractorInvoiceId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, ContractorInvoiceId);
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

            if (InvoiceNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_No", DbType.String, InvoiceNo);

            if (ContractorWorkOrderNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_WorkOrder_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_WorkOrder_No", DbType.String, ContractorWorkOrderNo);

            if (contractorName == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, contractorName);


            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateContractorInvoiceFrmDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;
        }


        // Contractor view and edit
        public List<InvoiceDom> ReadContractorInvoiceView(Int32? ContractorInvoiceId, Int32? contractorId, DateTime toDate, DateTime fromDate, String InvoiceNo, String ContractorWorkOrderNo)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_INVOICE_VIEW);
            if (ContractorInvoiceId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, ContractorInvoiceId);
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

            if (InvoiceNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_No", DbType.String, InvoiceNo);

            if (ContractorWorkOrderNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_WorkOrder_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_WorkOrder_No", DbType.String, ContractorWorkOrderNo);


            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateContractorInvoiceViewFrmDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;
        }






        public List<MetaData> ReadContractorPaymentTerm(Int32? ContractorInvoiceId, Int32 contractorId, DateTime toDate, DateTime fromDate, String InvoiceNo, String ContractorWorkOrderNo)
        {
            lstmetaData = new List<MetaData>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_INVOICE);
            if (ContractorInvoiceId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, ContractorInvoiceId);
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

            if (InvoiceNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_No", DbType.String, InvoiceNo);

            if (ContractorWorkOrderNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_WorkOrder_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_WorkOrder_No", DbType.String, ContractorWorkOrderNo);


            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = GenerateContractorPaymentTermFrmDataReader(reader);
                    lstmetaData.Add(metaData);
                }
            }
            return lstmetaData;
        }
        /// <summary>
        /// sundeep read the view data
        /// </summary>
        /// <param name="ContractorInvoiceId"></param>
        /// <returns></returns>
        /// 
        public List<ItemTransaction> ReadContractorInvoiceMapping(Int32? ContractorInvoiceId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READCONTRACTOR_INVOICE_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, ContractorInvoiceId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateContractorInvoiceMappingFromReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }

        public List<InvoiceDom> ReadContractorInvoiceStatusWise(int StatusTypeId)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_INVOICE_BY_STATUS_WISE);
            myDatabase.AddInParameter(dbCommand, "@in_StatusType_Id", DbType.Int32, StatusTypeId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateContractorInvoiceFrmDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;
        }

        public List<ItemTransaction> ReadInvoiceMapping(String invoiceNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_INVOICE_MAPPING_BY_INVOICE_NUMBER);
            myDatabase.AddInParameter(dbCommand, "@in_Invoice_Number", DbType.String, invoiceNo);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateInvoiceMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }

        public List<ItemTransaction> ReadInvoiceMapping(Int32? invoiceId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_INVOICE_MAPPING_BY_INVOICE_ID);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.String, invoiceId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateInvoiceMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }

        public List<ItemTransaction> ReadContractorWorkOrderMapping(int? WorkOrderId, String WorkOrderNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_WORK_ORDER_MAPPING);
            //myDatabase.AddInParameter(dbCommand, "@in_Contractor_Work_Order_Id", DbType.Int32, WorkOrderId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Work_Order_No", DbType.String, WorkOrderNo);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateContractorWorkOrderMappingFromDataReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }

            return lstItemTransaction;
        }

        public Int32 UpdateContractorInvoiceStatusType(InvoiceDom Invoice)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_CONTRACTOR_INVOICE_STATUS_TYPE);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, Invoice.ContractorInvoiceId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, Invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Approved_Rejected_By", DbType.String, Invoice.IssueMaterial.DemandVoucher.Quotation.ApprovedRejectedBy);
            myDatabase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, Invoice.RemarkReject);

            myDatabase.AddOutParameter(dbCommand, "@out_InvoiceId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_InvoiceId").ToString(), out id);
            return id;
        }

        public Int32 UpdateContractorInvoiceStatus(InvoiceDom Invoice)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_CONTRACTOR_INVOICE_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_ContInvoice_Id", DbType.Int32, Invoice.ContractorInvoiceId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, Invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, Invoice.IssueMaterial.DemandVoucher.Quotation.GeneratedBy);

            myDatabase.AddOutParameter(dbCommand, "@out_ContInvoiceId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_ContInvoiceId").ToString(), out id);
            return id;
        }
        #endregion

        #region private methods

        private ItemTransaction GenerateContractorWorkOrderMappingFromDataReader(IDataReader reader)
        {
            itemTransaction = new ItemTransaction();

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();

            //itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Contractor_PO_Mapping_Id");
            //itemTransaction.DeliverySchedule.CreatedBy = GetStringFromDataReader(reader, "Contractor_Purchase_Order_Number");
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.ActivityDescriptionId = GetIntegerFromDataReader(reader, "Activity_Id");
            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            //itemTransaction.Item.ModelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Item_Category_Id");
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Measurement_Unit_Id");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Measurement_Unit_Name");
           // itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Quantity");
            //itemTransaction.ConsumedUnit = GetDecimalFromDataReader(reader, "Consumed_Unit");
            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Unit_Left");
            if (itemTransaction.UnitLeft<=0)
            {
                itemTransaction.UnitLeft = 0;
            }
            itemTransaction.Remark = GetStringFromDataReader(reader, "Remarks");
            itemTransaction.QuantityReceived = GetDecimalFromDataReader(reader, "Billed_No_Of_Unit");
            if (itemTransaction.QuantityReceived<=0)
            {
                itemTransaction.QuantityReceived = 0;
            }
            itemTransaction.LostUnit = GetDecimalFromDataReader(reader, "Lost_Unit");
            if (itemTransaction.LostUnit<=0)
            {
                itemTransaction.LostUnit = 0; 
            }
            itemTransaction.NumberOfUnit = GetIntegerFromDataReader(reader, "Quantity");
            //itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Unit_Issued");
            if (itemTransaction.UnitIssued<=0)
            {
                itemTransaction.UnitIssued = 0;
            }
            itemTransaction.BilledUnit = GetDecimalFromDataReader(reader, "Unit_For_Build");
            if (itemTransaction.BilledUnit<=0)
            {
                itemTransaction.BilledUnit = 0;
            }
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            itemTransaction.Discount_Rates = GetDecimalFromDataReader(reader, "Discount_Rate");
            itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");
            itemTransaction.ConsumedUnit = GetDecimalFromDataReader(reader, "Consumed_Unit");
            if (itemTransaction.ConsumedUnit<=0)
            {
                itemTransaction.ConsumedUnit = 0;
            }
            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "Total_LostUnit_Amount");
            if (itemTransaction.TotalAmount<=0)
            {
                itemTransaction.TotalAmount = 0;
            }
            //itemTransaction.TaxInformation = new Tax();
            //itemTransaction.TaxInformation.DiscountMode = new MetaData();
            //itemTransaction.TaxInformation.DiscountMode.Id = GetIntegerFromDataReader(reader, "Discount_Type");
            //itemTransaction.TaxInformation.DiscountMode.Name = GetStringFromDataReader(reader, "Discount_Type_Name");
            //itemTransaction.TaxInformation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
            //itemTransaction.TaxInformation.VAT = GetDecimalFromDataReader(reader, "VAT");
            //itemTransaction.TaxInformation.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_with_C_Form");
            //itemTransaction.TaxInformation.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_without_C_Form");
            //itemTransaction.TaxInformation.ExciseDuty = GetDecimalFromDataReader(reader, "Excise_Duty");
            //itemTransaction.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            //itemTransaction.CreatedDate = GetDateFromReader(reader, "Created_Date");
            //itemTransaction.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            //itemTransaction.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return itemTransaction;
        }

        private InvoiceDom GenerateContractorQuotationFromDataReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            invoice.IssueMaterial = new IssueMaterialDOM();
            invoice.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            invoice.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId = GetIntegerFromDataReader(reader, "Contractor_Purchase_Order_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Purchase_Order_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractId = GetIntegerFromDataReader(reader, "Contract_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.WorkOrderId = GetIntegerFromDataReader(reader, "Work_Order_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber = GetStringFromDataReader(reader, "Work_Order_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.OrderDate = GetDateFromReader(reader, "Order_Date");
            invoice.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Net_Value");
            if (invoice.IssueMaterial.DemandVoucher.Quotation.TotalNetValue<=0)
            {
                invoice.IssueMaterial.DemandVoucher.Quotation.TotalNetValue = 0;   
            }
            invoice.InvoicedAmount = GetDecimalFromDataReader(reader, "Invoiced_Amount");
            if (invoice.InvoicedAmount<=0)
            {
                invoice.InvoicedAmount = 0;
            }
            invoice.LeftAmount = GetDecimalFromDataReader(reader, "Left_Amount");
            if (invoice.LeftAmount<=0)
            {
                invoice.LeftAmount = 0;
            }
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            invoice.IssueMaterial.DemandVoucher.Quotation.ApprovedRejectedBy = GetStringFromDataReader(reader, "Approved_Rejected_By");
            invoice.IssueMaterial.DemandVoucher.Quotation.ApprovedRejectedDate = GetDateFromReader(reader, "Approved_Rejected_Date");
            invoice.IssueMaterial.DemandVoucher.Quotation.IsGenerated = GetShortIntegerFromDataReader(reader, "IsGenerated");
            invoice.IssueMaterial.DemandVoucher.Quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            invoice.IssueMaterial.DemandVoucher.Quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
            invoice.IssueMaterial.DemandVoucher.Quotation.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            invoice.IssueMaterial.DemandVoucher.Quotation.CreatedDate = GetDateFromReader(reader, "Created_Date");
            invoice.IssueMaterial.DemandVoucher.Quotation.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            invoice.IssueMaterial.DemandVoucher.Quotation.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return invoice;
        }

        private MetaData GenerateContractorInvoiceFromDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }

        public void ResetContractorQuotationInvoiceMapping(Int32? INVId)
        {
            string sqlCommand = DBConstants.RESET_CONTRACTOR_QUOTATION_INVOICE_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, INVId);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        public void DeleteContractorInvoiceMapping(Int32? INVmappingId,int InvoiceId)
        {
            string sqlCommand = DBConstants.DELETE_CONTRACTOR_INVOICE_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_InvoiceMapping_Id", DbType.Int32, INVmappingId);
            myDatabase.AddInParameter(dbCommand, "@in_Contractor_Invoice_Id", DbType.Int32, InvoiceId);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        private ItemTransaction GenerateQuotationDemandVoucherFromDataReader(IDataReader reader)
        {
            itemTransaction = new ItemTransaction();

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();

            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Contractor_PO_Mapping_Id");

            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Discription");

            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item_Name");
            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Make");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Item_Category_Id");
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category_Name");
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Measurement_Unit_Id");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Measurement_Unit_Name");
            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            itemTransaction.UnitDemanded = GetDecimalFromDataReader(reader, "Unit_Demanded");
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
            itemTransaction.BilledUnit = GetDecimalFromDataReader(reader, "Billed_No_Of_Unit");
            itemTransaction.UnitForBilled = GetDecimalFromDataReader(reader, "Unit_For_Billed");
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");
            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.DiscountMode = new MetaData();
            itemTransaction.TaxInformation.DiscountMode.Id = GetIntegerFromDataReader(reader, "Discount_Type");
            itemTransaction.TaxInformation.DiscountMode.Name = GetStringFromDataReader(reader, "Discount_Type_Name");
            itemTransaction.TaxInformation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
            itemTransaction.TaxInformation.VAT = GetDecimalFromDataReader(reader, "VAT");
            itemTransaction.TaxInformation.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_with_C_Form");
            itemTransaction.TaxInformation.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_without_C_Form");
            itemTransaction.TaxInformation.Freight = GetDecimalFromDataReader(reader, "Freight");
            itemTransaction.TaxInformation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "TotalAmount");
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            itemTransaction.CreatedDate = GetDateFromReader(reader, "Created_Date");
            itemTransaction.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            itemTransaction.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return itemTransaction;
        }

        private InvoiceDom GenerateInvoiceFromDataReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            invoice.IssueMaterial = new IssueMaterialDOM();
            invoice.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            invoice.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            invoice.ContractorInvoiceId = GetIntegerFromDataReader(reader, "Contractor_Invoice_Id");
            invoice.InvoiceNumber = GetStringFromDataReader(reader, "Contractor_Invoice_Number");
            invoice.InvoiceDate = GetDateFromReader(reader, "Invoice_Date");
            invoice.Remarks = GetStringFromDataReader(reader, "Remarks");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            invoice.TotalAmount = GetDecimalFromDataReader(reader, "Total_Amount");
            invoice.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            invoice.CreatedDate = GetDateFromReader(reader, "Created_Date");
            invoice.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            invoice.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return invoice;
        }

        private InvoiceDom GenerateContractorInvoiceFrmDataReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            invoice.Payment = new PaymentTerm();
            invoice.IssueMaterial = new IssueMaterialDOM();
            invoice.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            invoice.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            invoice.InvoiceType = new MetaData();
            invoice.PaymentDom = new PaymentDOM();
            invoice.PaymentDom.Paymentstatus = new MetaData();
            invoice.PaymentDom.Paymentstatus.Id = GetIntegerFromDataReader(reader, "Payment_Status");
            if (invoice.PaymentDom.Paymentstatus.Id == 0)
            {
                invoice.PaymentDom.Paymentstatus.Id = 1;
            }
            //invoice.PaymentDom.PaymentStatus.Id = GetIntegerFromDataReader(reader, "Payment_Status");

            invoice.InvoiceType.Id = GetIntegerFromDataReader(reader, "Invoice_Type");
            invoice.InvoiceType.Name = GetStringFromDataReader(reader, "Invoice_Type_Name");

            invoice.ContractorInvoiceId = GetIntegerFromDataReader(reader, "Contractor_Invoice_Id");
            invoice.InvoiceNumber = GetStringFromDataReader(reader, "Contractor_Invoice_Number");
            invoice.IssueMaterial.IssueMaterialNumber = GetStringFromDataReader(reader, "Issue_Material_Number");
            invoice.IssueMaterial.DemandVoucher.IssueDemandVoucherNumber = GetStringFromDataReader(reader, "Demand_Voucher_Number");
            invoice.InvoiceDate = GetDateFromReader(reader, "Invoice_Date");
            invoice.BillDate = GetDateFromReader(reader, "BillDate");
            invoice.BillNumber = GetStringFromDataReader(reader, "BillNumber");
            if (invoice.BillNumber==null)
            {
                invoice.BillNumber = String.Empty;
            }
            invoice.Remarks = GetStringFromDataReader(reader, "Remarks");
            invoice.IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber = GetStringFromDataReader(reader, "Work_Order_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId = GetIntegerFromDataReader(reader, "Contractor_WO_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Document_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Amount");
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            invoice.IssueMaterial.DemandVoucher.Quotation.Freight = GetDecimalFromDataReader(reader, "Freight");
            invoice.IssueMaterial.DemandVoucher.Quotation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            invoice.Payment.PaymentTermId = GetIntegerFromDataReader(reader, "Payment_Term_Id");
            invoice.PayableAmount = GetDecimalFromDataReader(reader, "Payable_Amount");
            invoice.InvoicedAmount = GetDecimalFromDataReader(reader, "Invoiced_Amount");
            invoice.LeftAmount = GetDecimalFromDataReader(reader, "Left_Amount");
            invoice.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            invoice.CreatedDate = GetDateFromReader(reader, "Created_Date");
            invoice.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            invoice.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            invoice.RemarkReject = GetStringFromDataReader(reader, "Remark_approve_Reject");
            return invoice;
        }

        //contractor view and edit 
        private InvoiceDom GenerateContractorInvoiceViewFrmDataReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            invoice.Payment = new PaymentTerm();
            invoice.IssueMaterial = new IssueMaterialDOM();
            invoice.IssueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
            invoice.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
            invoice.InvoiceType = new MetaData();
            invoice.PaymentDom = new PaymentDOM();
            invoice.PaymentDom.Paymentstatus = new MetaData();
            

            invoice.InvoiceType.Id = GetIntegerFromDataReader(reader, "Invoice_Type");
            invoice.InvoiceType.Name = GetStringFromDataReader(reader, "Invoice_Type_Name");

            invoice.ContractorInvoiceId = GetIntegerFromDataReader(reader, "Contractor_Invoice_Id");
            invoice.InvoiceNumber = GetStringFromDataReader(reader, "Contractor_Invoice_Number");
            invoice.IssueMaterial.IssueMaterialNumber = GetStringFromDataReader(reader, "Issue_Material_Number");
            invoice.IssueMaterial.DemandVoucher.IssueDemandVoucherNumber = GetStringFromDataReader(reader, "Demand_Voucher_Number");
            invoice.InvoiceDate = GetDateFromReader(reader, "Invoice_Date");
            invoice.BillDate = GetDateFromReader(reader, "BillDate");
            invoice.BillNumber = GetStringFromDataReader(reader, "BillNumber");
            if (invoice.BillNumber == null)
            {
                invoice.BillNumber = String.Empty;
            }
            invoice.Remarks = GetStringFromDataReader(reader, "Remarks");
            invoice.IssueMaterial.DemandVoucher.Quotation.WorkOrderNumber = GetStringFromDataReader(reader, "Work_Order_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorName = GetStringFromDataReader(reader, "Contractor_Name");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractorQuotationId = GetIntegerFromDataReader(reader, "Contractor_WO_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.ContractQuotationNumber = GetStringFromDataReader(reader, "Contractor_Quotation_Number");
            invoice.IssueMaterial.DemandVoucher.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Document_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Amount");
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            invoice.IssueMaterial.DemandVoucher.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            invoice.IssueMaterial.DemandVoucher.Quotation.Freight = GetDecimalFromDataReader(reader, "Freight");
            invoice.IssueMaterial.DemandVoucher.Quotation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            invoice.Payment.PaymentTermId = GetIntegerFromDataReader(reader, "Payment_Term_Id");
            invoice.PayableAmount = GetDecimalFromDataReader(reader, "Payable_Amount");
            invoice.InvoicedAmount = GetDecimalFromDataReader(reader, "Invoiced_Amount");
            invoice.LeftAmount = GetDecimalFromDataReader(reader, "Left_Amount");
            invoice.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            invoice.CreatedDate = GetDateFromReader(reader, "Created_Date");
            invoice.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            invoice.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            invoice.RemarkReject = GetStringFromDataReader(reader, "Remark_approve_Reject");
            return invoice;
        }

        private MetaData GenerateContractorPaymentTermFrmDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Payment_Term_Id");
            return metaData;
        }

        private ItemTransaction GenerateInvoiceMappingFromDataReader(IDataReader reader)
        {
            itemTransaction = new ItemTransaction();
            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Contractor_Invoice_Mapping_Id");


            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Activity_Id");
            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            itemTransaction.UnitDemanded = GetDecimalFromDataReader(reader, "Unit_Demanded");
            itemTransaction.UnitForBilled = GetDecimalFromDataReader(reader, "Billed_No_Of_Unit");
            itemTransaction.UnitIssued = GetDecimalFromDataReader(reader, "Actual_No_Of_Unit");
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");
            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "Total_Amount");
            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
            itemTransaction.TaxInformation.VAT = GetDecimalFromDataReader(reader, "VAT");
            itemTransaction.TaxInformation.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_with_C_Form");
            itemTransaction.TaxInformation.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_without_C_Form");
            itemTransaction.TaxInformation.Freight = GetDecimalFromDataReader(reader, "Freight");
            itemTransaction.TaxInformation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            itemTransaction.TaxInformation.DiscountMode = new MetaData();
            itemTransaction.TaxInformation.DiscountMode.Id = GetIntegerFromDataReader(reader, "Discount_Type");
            itemTransaction.TaxInformation.DiscountMode.Name = GetStringFromDataReader(reader, "Discount_Type_Name");
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            itemTransaction.CreatedDate = GetDateFromReader(reader, "Created_Date");
            itemTransaction.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            itemTransaction.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return itemTransaction;
        }

       private ItemTransaction GenerateContractorInvoiceMappingFromReader(IDataReader reader)
        {
            itemTransaction = new ItemTransaction();
            
            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.MetaProperty = new MetaData();
            //invoice.InvoiceType = new MetaData();
            //invoice.InvoiceType.Id = GetIntegerFromDataReader(reader, "Invoice_Type");
            //invoice.InvoiceType.Name = GetStringFromDataReader(reader, "Invoice_Type_Name");
            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            itemTransaction.DeliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Activity_Description");
            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "Total_Amount");
            if (itemTransaction.TotalAmount<=0)
            {
                itemTransaction.TotalAmount = 0;
            }
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Per_Unit_Discount");
            itemTransaction.Discount_Rates = GetDecimalFromDataReader(reader, "Discount_Rate");
            if (itemTransaction.Discount_Rates <= 0)
            {
                itemTransaction.Discount_Rates = 0;
            }
            itemTransaction.UnitForBilled = GetDecimalFromDataReader(reader, "Billed_No_Of_Unit");
            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
            itemTransaction.TaxInformation.VAT = GetDecimalFromDataReader(reader, "VAT");
            itemTransaction.TaxInformation.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_with_C_Form");
            itemTransaction.TaxInformation.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_without_C_Form");
            itemTransaction.TaxInformation.Packaging = GetIntegerFromDataReader(reader, "Packaging");
            itemTransaction.TaxInformation.Freight = GetIntegerFromDataReader(reader, "Packaging");
            itemTransaction.TaxInformation.DiscountMode = new MetaData();
            itemTransaction.TaxInformation.DiscountMode.Id = GetIntegerFromDataReader(reader, "Discount_Type");
            //itemTransaction.TaxInformation.DiscountMode.Name = GetStringFromDataReader(reader, "Discount_Type");
            
            return itemTransaction;

        }

        #endregion

    }
}
