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
    public class SupplierInvoiceDAL : BaseDAL
    {
        #region private global variables

        private Database myDatabase;
        DbCommand dbCommand = null;
        Tax tax = null;
        InvoiceDom invoice = null;
        ItemTransaction itemTransaction = null;
        List<InvoiceDom> lstInvoice = null;
        List<ItemTransaction> lstItemTransaction = null;
        List<MetaData> lstMetaData = null;

        MetaData metaData = null;
        Int32 id = 0;
        #endregion

        #region Constructors

        public SupplierInvoiceDAL(Database database)
        {
            myDatabase = database;
        }

        #endregion

        #region Supplier Invoice CRUD Methods

        public List<InvoiceDom> ReadSupplierQuotation(Int32? quotationId, String quotationNumber)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_QUOTATION_BY_PONUMBER);
            myDatabase.AddInParameter(dbCommand, "in_Quotation_Id", DbType.Int32, quotationId);
            myDatabase.AddInParameter(dbCommand, "in_Supplier_Purchase_Order_Number", DbType.String, quotationNumber);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateSupplierQuotationFromDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;

        }

        public MetaData CreateSupplierInvoice(InvoiceDom invoiceDom, Int32? SupplierInvoiceId)
        {
            string SqlCommand = DBConstants.CREATE_SUPPLIER_INVOICE;
            dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
            if (invoiceDom.SupplierInvoiceId > 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Id", DbType.Int32, invoiceDom.SupplierInvoiceId);
            }
            myDatabase.AddInParameter(dbCommand, "@in_Payment_Type", DbType.Int32, invoiceDom.Payment.PaymentType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Payment_TermId", DbType.Int32, invoiceDom.Payment.PaymentTermId);
            myDatabase.AddInParameter(dbCommand, "@in_Percentage_Value", DbType.Decimal, invoiceDom.Payment.PercentageValue);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_Number", DbType.String, invoiceDom.ReceiveMaterial.Quotation.SupplierQuotationNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_Id", DbType.Int32, invoiceDom.ReceiveMaterial.Quotation.SupplierQuotationId);
            myDatabase.AddInParameter(dbCommand, "@in_Receive_Material_Number", DbType.String, invoiceDom.ReceiveMaterial.SupplierRecieveMaterialNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, invoiceDom.ReceiveMaterial.Quotation.SupplierId);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Name", DbType.String, invoiceDom.ReceiveMaterial.Quotation.SupplierName);
            myDatabase.AddInParameter(dbCommand, "@in_Invoice_Date", DbType.DateTime, invoiceDom.InvoiceDate);
            myDatabase.AddInParameter(dbCommand, "@in_BillDate", DbType.DateTime, invoiceDom.BillDate);
            myDatabase.AddInParameter(dbCommand, "@in_BillNumber", DbType.String, invoiceDom.BillNumber);
            myDatabase.AddInParameter(dbCommand, "@in_Remarks", DbType.String, invoiceDom.Remarks);
            myDatabase.AddInParameter(dbCommand, "@in_Document_Id", DbType.Int32, invoiceDom.ReceiveMaterial.Quotation.UploadDocumentId);
            myDatabase.AddInParameter(dbCommand, "@in_Total_Amount", DbType.Decimal, invoiceDom.ReceiveMaterial.Quotation.TotalNetValue);
            myDatabase.AddInParameter(dbCommand, "@in_Invoiced_Amount", DbType.Decimal, invoiceDom.InvoicedAmount);
            myDatabase.AddInParameter(dbCommand, "@in_Invoice_Type", DbType.Int32, invoiceDom.InvoiceType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Freight", DbType.Decimal, invoiceDom.ReceiveMaterial.Quotation.Freight);
            myDatabase.AddInParameter(dbCommand, "@in_Packaging", DbType.Decimal, invoiceDom.ReceiveMaterial.Quotation.Packaging);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, invoiceDom.ReceiveMaterial.Quotation.StatusType.Id);
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

        public int CreateSupplierInvoiceMapping(List<ItemTransaction> lstItemTransaction, Int32 SupplierInvoiceId)
        {
            id = 0;
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string SqlCommand = DBConstants.CREATE_SUPPLIER_INVOICE_MAPPING;
                dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Id", DbType.Int32, SupplierInvoiceId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Mapping_Id", DbType.Int32, DBNull.Value);
                }

                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_Mapping_Id", DbType.Int32, itemTransaction.DeliverySchedule.Id);

                myDatabase.AddInParameter(dbCommand, "@in_Item_Id", DbType.String, itemTransaction.Item.ItemId);
                myDatabase.AddInParameter(dbCommand, "@in_Item", DbType.String, itemTransaction.Item.ItemName);
                myDatabase.AddInParameter(dbCommand, "@in_Specification", DbType.String, itemTransaction.Item.ModelSpecification.ModelSpecificationName);
                myDatabase.AddInParameter(dbCommand, "@in_Item_Category", DbType.String, itemTransaction.Item.ModelSpecification.Category.ItemCategoryName);
                myDatabase.AddInParameter(dbCommand, "@in_Brand", DbType.String, itemTransaction.Item.ModelSpecification.Brand.BrandName);
                myDatabase.AddInParameter(dbCommand, "@in_Number_Of_Unit", DbType.Decimal, itemTransaction.NumberOfUnit);
                myDatabase.AddInParameter(dbCommand, "@in_Actual_No_Of_Unit", DbType.Decimal, itemTransaction.UnitIssued);
                myDatabase.AddInParameter(dbCommand, "@in_Billed_No_Of_Unit", DbType.Decimal, itemTransaction.UnitForBilled);
                myDatabase.AddInParameter(dbCommand, "@in_Per_Unit_Cost", DbType.Decimal, itemTransaction.PerUnitCost);
                myDatabase.AddInParameter(dbCommand, "@in_Per_Unit_Discount", DbType.Decimal, itemTransaction.TaxInformation.PercentageQuty);
                myDatabase.AddInParameter(dbCommand, "@in_Discount_Type", DbType.Int32, itemTransaction.TaxInformation.DiscountMode.Id);
                myDatabase.AddInParameter(dbCommand, "@in_Excise_Duty", DbType.Decimal, itemTransaction.TaxInformation.ExciseDuty);
                myDatabase.AddInParameter(dbCommand, "@in_Service_Tax", DbType.Decimal, itemTransaction.TaxInformation.ServiceTax);
                myDatabase.AddInParameter(dbCommand, "@in_VAT", DbType.Decimal, itemTransaction.TaxInformation.VAT);
                myDatabase.AddInParameter(dbCommand, "@in_CST_with_C_Form", DbType.Decimal, itemTransaction.TaxInformation.CSTWithCForm);
                myDatabase.AddInParameter(dbCommand, "@in_CST_without_C_Form", DbType.Decimal, itemTransaction.TaxInformation.CSTWithoutCForm);
                myDatabase.AddInParameter(dbCommand, "@in_Freight", DbType.Decimal, itemTransaction.TaxInformation.Freight);
                myDatabase.AddInParameter(dbCommand, "@in_Packaging", DbType.Decimal, itemTransaction.TaxInformation.Packaging);
                myDatabase.AddInParameter(dbCommand, "@in_Total_Amount", DbType.Decimal, itemTransaction.TotalAmount);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Supplier_Invoice_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);
                int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Supplier_Invoice_Mapping_Id").ToString(), out id);

            }
            return id;
        }

        public int CreateSupplierInvoiceMappingAdvance(List<ItemTransaction> lstItemTransaction, Int32 SupplierInvoiceId)
        {
            id = 0;
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();
            }
            foreach (ItemTransaction itemTransaction in lstItemTransaction)
            {
                string SqlCommand = DBConstants.CREATE_SUPPLIER_INVOICE_MAPPING_ADVANCE;
                dbCommand = myDatabase.GetStoredProcCommand(SqlCommand);
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Id", DbType.Int32, SupplierInvoiceId);
                if (itemTransaction.MetaProperty.Id > 0)
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Mapping_Id", DbType.Int32, itemTransaction.MetaProperty.Id);
                }
                else
                {
                    myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Mapping_Id", DbType.Int32, DBNull.Value);
                }
                myDatabase.AddInParameter(dbCommand, "@in_Advance_Value", DbType.Decimal, itemTransaction.AdvanceValue);
                myDatabase.AddInParameter(dbCommand, "@in_Excise_Duty", DbType.Decimal, itemTransaction.TaxInformation.ExciseDuty);
                myDatabase.AddInParameter(dbCommand, "@in_Service_Tax", DbType.Decimal, itemTransaction.TaxInformation.ServiceTax);
                myDatabase.AddInParameter(dbCommand, "@in_VAT", DbType.Decimal, itemTransaction.TaxInformation.VAT);
                myDatabase.AddInParameter(dbCommand, "@in_CST_with_C_Form", DbType.Decimal, itemTransaction.TaxInformation.CSTWithCForm);
                myDatabase.AddInParameter(dbCommand, "@in_CST_without_C_Form", DbType.Decimal, itemTransaction.TaxInformation.CSTWithoutCForm);
                myDatabase.AddInParameter(dbCommand, "@in_Total_Amount", DbType.Decimal, itemTransaction.TaxInformation.TotalNetValue);
                myDatabase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, itemTransaction.CreatedBy);
                myDatabase.AddOutParameter(dbCommand, "@out_Supplier_Invoice_Mapping_Id", DbType.Int32, 10);
                myDatabase.ExecuteNonQuery(dbCommand);

                int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_Supplier_Invoice_Mapping_Id").ToString(), out id);

            }
            return id;
        }

        public List<ItemTransaction> ReadSupplierPOReceiveMaterialMapping(Int32? SupplierPurchaseOrderId, String SupplierPurchaseOrderNo)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIERPO_RECEIVE_MATERIAL_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Purchase_Order_Id", DbType.Int32, SupplierPurchaseOrderId);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Purchase_Order_Number", DbType.String, SupplierPurchaseOrderNo);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateSupplierPOReceiveMaterialMappingFromDataReader(reader);
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

        public List<ItemTransaction> ReadSupplierInvoiceMapping(Int32? SupplierInvoiceId)
        {
            lstItemTransaction = new List<ItemTransaction>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_INVOICE_MAPPING);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Id", DbType.String, SupplierInvoiceId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemTransaction = GenerateInvoiceMappingFromReader(reader);
                    lstItemTransaction.Add(itemTransaction);
                }
            }
            return lstItemTransaction;
        }

        public void DeleteSupplierInvoice(int SupplierInvoiceId, string modifiedBy, DateTime modifiedOn)
        {
            string sqlCommand = DBConstants.DELETE_SUPPLIER_INVOICE;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_Id", DbType.Int32, SupplierInvoiceId);
            myDatabase.AddInParameter(dbCommand, "@in_modifiedBy", DbType.String, modifiedBy);
            myDatabase.AddInParameter(dbCommand, "@in_modifiedDate", DbType.DateTime, modifiedOn);
            myDatabase.ExecuteNonQuery(dbCommand);
        }

        public List<InvoiceDom> ReadSupplierInvoice(Int32? SupplierInvoiceId, Int32? SupplierId, DateTime toDate, DateTime fromDate, String SupplierInvoiceNo, String SupplierPONo, string supplierName)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_INVOICE);
            if (SupplierInvoiceId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_Id", DbType.Int32, SupplierInvoiceId);
            if (SupplierId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, SupplierId);
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

            if (SupplierInvoiceNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_No", DbType.String, SupplierInvoiceNo);

            if (SupplierPONo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_No", DbType.String, SupplierPONo);


            if (supplierName == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Contractor_Supplier_Name", DbType.String, supplierName);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateSupplierInvoiceFrmDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;
        }

        //supplier view Edit Case
        public List<InvoiceDom> ReadSupplierInvoiceView(Int32? SupplierInvoiceId, Int32? SupplierId, DateTime toDate, DateTime fromDate, String SupplierInvoiceNo, String SupplierPONo)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_INVOICE_VIEW);
            if (SupplierInvoiceId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_Id", DbType.Int32, SupplierInvoiceId);
            if (SupplierId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, SupplierId);
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

            if (SupplierInvoiceNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_No", DbType.String, SupplierInvoiceNo);

            if (SupplierPONo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_No", DbType.String, SupplierPONo);


            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateSupplierInvoiceViewFrmDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;
        }

        public Int32 UpdateSupplierInvoiceStatus(InvoiceDom Invoice)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_SUPPLIER_INVOICE_STATUS);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, Invoice.SupplierInvoiceId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, Invoice.ReceiveMaterial.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Generated_By", DbType.String, Invoice.ReceiveMaterial.Quotation.GeneratedBy);
            myDatabase.AddOutParameter(dbCommand, "@out_SupplierId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_SupplierId").ToString(), out id);
            return id;
        }

        public List<InvoiceDom> ReadSupplierInvoiceByStatusType(int StatusTypeId)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_INVOICE_BY_STATUS_WISE);
            myDatabase.AddInParameter(dbCommand, "@in_StatusType_Id", DbType.Int32, StatusTypeId);
            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateSupplierInvoiceFrmDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;
        }

        public Int32 UpdateSupplierInvoiceStatusType(InvoiceDom Invoice)
        {
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.UPDATE_SUPPLIER_INVOICE_STATUS_TYPE);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Id", DbType.Int32, Invoice.SupplierInvoiceId);
            myDatabase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, Invoice.ReceiveMaterial.Quotation.StatusType.Id);
            myDatabase.AddInParameter(dbCommand, "@in_Approved_Rejected_By", DbType.String, Invoice.ReceiveMaterial.Quotation.ApprovedRejectedBy);
            myDatabase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, Invoice.RemarkReject);

            myDatabase.AddOutParameter(dbCommand, "@out_InvoiceId", DbType.Int32, 10);
            myDatabase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDatabase.GetParameterValue(dbCommand, "@out_InvoiceId").ToString(), out id);
            return id;
        }

        public List<MetaData> ReadSupplierPaymentTerm(Int32? SupplierInvoiceId, Int32 SupplierId, DateTime toDate, DateTime fromDate, String SupplierInvoiceNo, String SupplierPONo)
        {
            lstMetaData = new List<MetaData>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_INVOICE);
            if (SupplierInvoiceId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_Id", DbType.Int32, SupplierInvoiceId);
            if (SupplierId == 0)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, SupplierId);
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

            if (SupplierInvoiceNo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_SupplierInvoice_No", DbType.String, SupplierInvoiceNo);

            if (SupplierPONo == String.Empty)
            {
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_No", DbType.String, DBNull.Value);
            }
            else
                myDatabase.AddInParameter(dbCommand, "@in_Supplier_PO_No", DbType.String, SupplierPONo);


            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = GenerateSupplierPaymentTermFrmDataReader(reader);
                    lstMetaData.Add(metaData);
                }
            }
            return lstMetaData;
        }


        #endregion

        #region private methods

        private InvoiceDom GenerateSupplierQuotationFromDataReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            invoice.ReceiveMaterial = new SupplierRecieveMatarial();
            invoice.ReceiveMaterial.Quotation = new QuotationDOM();
            invoice.ReceiveMaterial.Quotation.StatusType = new MetaData();
            invoice.ReceiveMaterial.Quotation.SupplierQuotationId = GetIntegerFromDataReader(reader, "Supplier_Purchase_Order_Id");
            invoice.ReceiveMaterial.Quotation.SupplierQuotationNumber = GetStringFromDataReader(reader, "Supplier_Purchase_Order_Number");
            invoice.ReceiveMaterial.Quotation.SupplierId = GetIntegerFromDataReader(reader, "Supplier_Id");
            invoice.ReceiveMaterial.Quotation.SupplierName = GetStringFromDataReader(reader, "Supplier_Name");
            invoice.ReceiveMaterial.Quotation.OrderDate = GetDateFromReader(reader, "Quotation_Order_Date");
            invoice.ReceiveMaterial.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");
            invoice.ReceiveMaterial.Quotation.ClosingDate = GetDateFromReader(reader, "Closing_Date");
            invoice.ReceiveMaterial.Quotation.DeliveryDescription = GetStringFromDataReader(reader, "Delivery_Description");
            invoice.ReceiveMaterial.Quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Net_Value");
            invoice.InvoicedAmount = GetDecimalFromDataReader(reader, "Invoiced_Amount");
            invoice.LeftAmount = GetDecimalFromDataReader(reader, "Left_Amount");
            invoice.ReceiveMaterial.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            invoice.ReceiveMaterial.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            invoice.ReceiveMaterial.Quotation.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            invoice.ReceiveMaterial.Quotation.CreatedDate = GetDateFromReader(reader, "Created_Date");
            invoice.ReceiveMaterial.Quotation.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            invoice.ReceiveMaterial.Quotation.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            invoice.ReceiveMaterial.Quotation.ApprovedRejectedBy = GetStringFromDataReader(reader, "Approved_Rejected_By");
            invoice.ReceiveMaterial.Quotation.ApprovedRejectedDate = GetDateFromReader(reader, "Approved_Rejected_Date");
            invoice.ReceiveMaterial.Quotation.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            invoice.ReceiveMaterial.Quotation.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
            invoice.ReceiveMaterial.Quotation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            invoice.ReceiveMaterial.Quotation.Freight = GetDecimalFromDataReader(reader, "Freight");
            if (invoice.ReceiveMaterial.Quotation.Packaging == Decimal.MinValue)
                invoice.ReceiveMaterial.Quotation.Packaging = 0;
            if (invoice.ReceiveMaterial.Quotation.Freight == Decimal.MinValue)
                invoice.ReceiveMaterial.Quotation.Freight = 0;
            return invoice;
        }
        private MetaData GenerateContractorInvoiceFromDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }
        public void ResetSupplierInvoiceMapping(Int32? SupplierInvoiceId)
        {
            string sqlCommand = DBConstants.RESET_SUPPLIER_INVOICE_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_Invoice_Id", DbType.Int32, SupplierInvoiceId);
            myDatabase.ExecuteNonQuery(dbCommand);
        }
        public void DeleteSupplierInvoiceMapping(Int32? SupplierInvoiceMappingId)
        {
            string sqlCommand = DBConstants.DELETE_SUPPLIER_INVOICE_MAPPING;
            DbCommand dbCommand = myDatabase.GetStoredProcCommand(sqlCommand);
            myDatabase.AddInParameter(dbCommand, "@in_Supplier_InvoiceMapping_Id", DbType.Int32, SupplierInvoiceMappingId);
            myDatabase.ExecuteNonQuery(dbCommand);
        }
        private ItemTransaction GenerateSupplierPOReceiveMaterialMappingFromDataReader(IDataReader reader)
        {
            itemTransaction = new ItemTransaction();
            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.DeliverySchedule.Id = GetIntegerFromDataReader(reader, "Supplier_PO_Mapping_Id");
            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item_Name");
            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Make");
            itemTransaction.Item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Item_Category_Id");
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category_Name");
            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Measurement_Unit_Id");
            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Measurement_Unit_Name");
            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
            itemTransaction.QuantityReceived = GetDecimalFromDataReader(reader, "Quantity_Received");
            itemTransaction.UnitLeft = GetDecimalFromDataReader(reader, "Unit_Left");
            if (itemTransaction.UnitLeft < 0)
            {
                itemTransaction.UnitLeft = 0;
            }
            itemTransaction.BilledUnit = GetDecimalFromDataReader(reader, "Billed_No_Of_Unit");
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
            itemTransaction.TaxInformation.ExciseDuty = GetDecimalFromDataReader(reader, "Excise_Duty");
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
        private InvoiceDom GenerateSupplierInvoiceFrmDataReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            invoice.Payment = new PaymentTerm();
            invoice.ReceiveMaterial = new SupplierRecieveMatarial();
            invoice.ReceiveMaterial.Quotation = new QuotationDOM();
            invoice.Payment = new PaymentTerm();

            invoice.InvoiceType = new MetaData();
            invoice.PaymentDom = new PaymentDOM();
            invoice.PaymentDom.Paymentstatus = new MetaData();
            invoice.PaymentDom.Paymentstatus.Id = GetIntegerFromDataReader(reader, "Payment_Status");
            invoice.InvoiceType.Id = GetIntegerFromDataReader(reader, "Invoice_Type");
            invoice.InvoiceType.Name = GetStringFromDataReader(reader, "Invoice_Type_Name");

            invoice.Payment.PaymentTermId = GetIntegerFromDataReader(reader, "Payment_TermId");
            invoice.SupplierInvoiceId = GetIntegerFromDataReader(reader, "Supplier_Invoice_Id");
            invoice.InvoiceNumber = GetStringFromDataReader(reader, "Supplier_Invoice_Number");
            invoice.InvoiceDate = GetDateFromReader(reader, "Invoice_Date");
            invoice.BillDate = GetDateFromReader(reader, "BillDate");
            invoice.BillNumber = GetStringFromDataReader(reader, "BillNumber");
            if (invoice.BillNumber == null)
            {
                invoice.BillNumber = String.Empty;
            }
            invoice.Remarks = GetStringFromDataReader(reader, "Remarks");
            invoice.ReceiveMaterial.Quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Amount");
            invoice.ReceiveMaterial.Quotation.SupplierName = GetStringFromDataReader(reader, "Supplier_Name");
            invoice.ReceiveMaterial.Quotation.SupplierId = GetIntegerFromDataReader(reader, "Supplier_Id");
            invoice.ReceiveMaterial.Quotation.SupplierQuotationNumber = GetStringFromDataReader(reader, "Supplier_PO_Number");
            invoice.ReceiveMaterial.Quotation.SupplierQuotationId = GetIntegerFromDataReader(reader, "Supplier_PO_Id");
            invoice.ReceiveMaterial.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Document_Id");
            invoice.ReceiveMaterial.Quotation.Freight = GetDecimalFromDataReader(reader, "Freight");
            invoice.ReceiveMaterial.Quotation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            invoice.PayableAmount = GetDecimalFromDataReader(reader, "Payable_Amount");
            invoice.InvoicedAmount = GetDecimalFromDataReader(reader, "Invoiced_Amount");
            invoice.LeftAmount = GetDecimalFromDataReader(reader, "Left_Amount");
            invoice.ReceiveMaterial.Quotation.StatusType = new MetaData();
            invoice.ReceiveMaterial.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            invoice.ReceiveMaterial.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            invoice.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            invoice.CreatedDate = GetDateFromReader(reader, "Created_Date");
            invoice.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            invoice.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            invoice.RemarkReject = GetStringFromDataReader(reader, "Remark_approve_Reject");
            return invoice;
        }

        //Supplier View and Edit Case
        private InvoiceDom GenerateSupplierInvoiceViewFrmDataReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            invoice.Payment = new PaymentTerm();
            invoice.ReceiveMaterial = new SupplierRecieveMatarial();
            invoice.ReceiveMaterial.Quotation = new QuotationDOM();
            invoice.Payment = new PaymentTerm();

            invoice.InvoiceType = new MetaData();
            invoice.PaymentDom = new PaymentDOM();
            invoice.PaymentDom.Paymentstatus = new MetaData();

            invoice.InvoiceType.Id = GetIntegerFromDataReader(reader, "Invoice_Type");
            invoice.InvoiceType.Name = GetStringFromDataReader(reader, "Invoice_Type_Name");

            invoice.Payment.PaymentTermId = GetIntegerFromDataReader(reader, "Payment_TermId");
            invoice.SupplierInvoiceId = GetIntegerFromDataReader(reader, "Supplier_Invoice_Id");
            invoice.InvoiceNumber = GetStringFromDataReader(reader, "Supplier_Invoice_Number");
            invoice.InvoiceDate = GetDateFromReader(reader, "Invoice_Date");
            invoice.BillDate = GetDateFromReader(reader, "BillDate");
            invoice.BillNumber = GetStringFromDataReader(reader, "BillNumber");
            if (invoice.BillNumber == null)
            {
                invoice.BillNumber = String.Empty;
            }
            invoice.Remarks = GetStringFromDataReader(reader, "Remarks");
            invoice.ReceiveMaterial.Quotation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Amount");
            invoice.ReceiveMaterial.Quotation.SupplierName = GetStringFromDataReader(reader, "Supplier_Name");
            invoice.ReceiveMaterial.Quotation.SupplierId = GetIntegerFromDataReader(reader, "Supplier_Id");
            invoice.ReceiveMaterial.Quotation.SupplierQuotationNumber = GetStringFromDataReader(reader, "Supplier_PO_Number");
            invoice.ReceiveMaterial.Quotation.SupplierQuotationId = GetIntegerFromDataReader(reader, "Supplier_PO_Id");
            invoice.ReceiveMaterial.Quotation.UploadDocumentId = GetIntegerFromDataReader(reader, "Document_Id");
            invoice.ReceiveMaterial.Quotation.Freight = GetDecimalFromDataReader(reader, "Freight");
            invoice.ReceiveMaterial.Quotation.Packaging = GetDecimalFromDataReader(reader, "Packaging");
            invoice.PayableAmount = GetDecimalFromDataReader(reader, "Payable_Amount");
            invoice.InvoicedAmount = GetDecimalFromDataReader(reader, "Invoiced_Amount");
            invoice.LeftAmount = GetDecimalFromDataReader(reader, "Left_Amount");
            invoice.ReceiveMaterial.Quotation.StatusType = new MetaData();
            invoice.ReceiveMaterial.Quotation.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            invoice.ReceiveMaterial.Quotation.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            invoice.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            invoice.CreatedDate = GetDateFromReader(reader, "Created_Date");
            invoice.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            invoice.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            invoice.RemarkReject = GetStringFromDataReader(reader, "Remark_approve_Reject");
            return invoice;
        }

        private MetaData GenerateSupplierPaymentTermFrmDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Payment_TermId");
            return metaData;
        }
        private ItemTransaction GenerateInvoiceMappingFromDataReader(IDataReader reader)
        {
            itemTransaction = new ItemTransaction();

            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Supplier_Invoice_Mapping_Id");
            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            itemTransaction.NumberOfUnit = GetDecimalFromDataReader(reader, "Number_Of_Unit");
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
            itemTransaction.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            itemTransaction.CreatedDate = GetDateFromReader(reader, "Created_Date");
            itemTransaction.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            itemTransaction.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return itemTransaction;
        }
        private ItemTransaction GenerateInvoiceMappingFromReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            itemTransaction = new ItemTransaction();
            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            itemTransaction.MetaProperty = new MetaData();
            itemTransaction.MetaProperty.Id = GetIntegerFromDataReader(reader, "Supplier_Invoice_Mapping_Id");
            itemTransaction.Item = new Item();
            itemTransaction.Item.ItemName = GetStringFromDataReader(reader, "Item");
            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            itemTransaction.Item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Brand");
            itemTransaction.Item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Item_Category");
            itemTransaction.PerUnitCost = GetDecimalFromDataReader(reader, "Per_Unit_Cost");
            //


            itemTransaction.ActualNumberofUnit = GetDecimalFromDataReader(reader, "Per_Unit_Discount");
            itemTransaction.Discount_Rates = GetDecimalFromDataReader(reader, "Discount_Rate");
            if (itemTransaction.Discount_Rates <= 0)
            {
                itemTransaction.Discount_Rates = 0;
            }
            itemTransaction.UnitForBilled = GetDecimalFromDataReader(reader, "Billed_No_Of_Unit");
            itemTransaction.TotalAmount = GetDecimalFromDataReader(reader, "Total_Amount");

            itemTransaction.TaxInformation = new Tax();
            itemTransaction.TaxInformation.ExciseDuty = GetDecimalFromDataReader(reader, "Excise_Duty");
            itemTransaction.TaxInformation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
            itemTransaction.TaxInformation.VAT = GetDecimalFromDataReader(reader, "VAT");
            itemTransaction.TaxInformation.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_with_C_Form");
            itemTransaction.TaxInformation.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_without_C_Form");

            //Below 2 properties are used only in Report
            itemTransaction.TaxInformation.TotalDiscount = GetDecimalFromDataReader(reader, "Invoiced_Amount");
            itemTransaction.TaxInformation.TotalTax = GetDecimalFromDataReader(reader, "Excise");
            itemTransaction.PerUnitDiscount = GetDecimalFromDataReader(reader, "Amt");

            itemTransaction.TaxInformation.DiscountMode = new MetaData();
            itemTransaction.TaxInformation.DiscountMode.Id = GetIntegerFromDataReader(reader, "Discount_Type");
            return itemTransaction;
        }
        #endregion

        public List<InvoiceDom> ReadSupplierBillAmount(DateTime fromDate, DateTime ToDate)
        {
            lstInvoice = new List<InvoiceDom>();
            dbCommand = myDatabase.GetStoredProcCommand(DBConstants.READ_ALL_PAYMENT);
            myDatabase.AddInParameter(dbCommand, "@in_FromDate", DbType.DateTime, fromDate);
            myDatabase.AddInParameter(dbCommand, "@in_ToDate", DbType.DateTime, ToDate);

            using (IDataReader reader = myDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoice = GenerateSupplierBillAmountFromDataReader(reader);
                    lstInvoice.Add(invoice);
                }
            }
            return lstInvoice;
        }

        private InvoiceDom GenerateSupplierBillAmountFromDataReader(IDataReader reader)
        {
            invoice = new InvoiceDom();
            invoice.SupplierOrderNumber = GetStringFromDataReader(reader, "SupplierPurchaseOrderNumber");
            invoice.SupplierName = GetStringFromDataReader(reader, "SupplierName");
            invoice.OrderDate = GetDateFromReader(reader, "QuotationOrderDate");
            invoice.TotalAmount = GetDecimalFromDataReader(reader, "TotalAmount");
            invoice.PendingAmount = GetDecimalFromDataReader(reader, "PendingAmount") == Decimal.MinValue ? 0 : GetDecimalFromDataReader(reader, "PendingAmount");
            invoice.ApprovedAmount = GetDecimalFromDataReader(reader, "ApprovedAmount") == Decimal.MinValue ? 0 : GetDecimalFromDataReader(reader, "ApprovedAmount");
            invoice.ToBillAmount = GetDecimalFromDataReader(reader, "TobeBilledAmount") == Decimal.MinValue ? 0 : GetDecimalFromDataReader(reader, "TobeBilledAmount");           
            return invoice;
        }
    }
}
