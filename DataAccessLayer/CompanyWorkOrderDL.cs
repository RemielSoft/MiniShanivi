using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using DocumentObjectModel;

namespace DataAccessLayer
{
    public class CompanyWorkOrderDL : BaseDAL
    {
        #region Global Variables

        private Database myDataBase = null;
        DbCommand dbCommand = null;

        List<CompanyWorkOrderDOM> lstCompWorkOrder = null;
        List<WorkOrderMappingDOM> lstWorkOrderMapping = null;
        List<BankGuaranteeDOM> lstBankGuarantee = null;
        List<ServiceDetailDOM> lstServiceDetail = null;
        List<MetaData> lstMetaData = null;

        CompanyWorkOrderDOM CompWorkOrder = null;
        WorkOrderMappingDOM workOrderMapping = null;
        BankGuaranteeDOM bankGuarantee = null;
        ServiceDetailDOM serviceDetail = null;
        MetaData metaData = null;

        int id = 0;
        string str = string.Empty;

        #endregion

        #region Constructor

        public CompanyWorkOrderDL(Database database)
        {
            myDataBase = database;
        }

        #endregion

        #region CURD Methods

        public List<MetaData> CreateCompanyWorkOrder(CompanyWorkOrderDOM companyWO)
        {
            lstMetaData = new List<MetaData>();
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_COMPANY_WORK_ORDER);
            myDataBase.AddInParameter(dbCommand, "in_Contract_Number", DbType.String, companyWO.ContractNumber);
            //if(
            myDataBase.AddInParameter(dbCommand, "in_Contract_Date", DbType.DateTime, companyWO.ContractDate);
            myDataBase.AddInParameter(dbCommand, "in_Work_Order_Description", DbType.String, companyWO.WorkOrderDescription);
            myDataBase.AddInParameter(dbCommand, "in_Total_Net_Value", DbType.Decimal, companyWO.TotalNetValue);
            myDataBase.AddInParameter(dbCommand, "in_Upload_Documnet_Id", DbType.Int32, companyWO.UploadDocumentId);
            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, companyWO.CreatedBy);
            myDataBase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, "");

            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metaData = GenerateDataReader(reader);
                    lstMetaData.Add(metaData);
                }
            }

            //myDataBase.AddOutParameter(dbCommand, "out_Company_Work_Order_Id", DbType.Int32, 10);
            //myDataBase.ExecuteNonQuery(dbCommand);
            //int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Company_Work_Order_Id").ToString(), out id);
            //return id;
            return lstMetaData;
        }

        public int CreateCompanyWorkOrderMapping(WorkOrderMappingDOM companyWOM, Int32 companyWOId)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_COMPANY_WORK_ORDER_MAPPING);
            if (companyWOM.CompanyWorkOrderMappingId > 0)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Mapping_Id", DbType.Int32, companyWOM.CompanyWorkOrderMappingId);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Mapping_Id", DbType.Int32, DBNull.Value);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.String, companyWOId);
            myDataBase.AddInParameter(dbCommand, "@in_Amount", DbType.Decimal, companyWOM.Amount);
            myDataBase.AddInParameter(dbCommand, "@in_Work_Order_Number", DbType.String, companyWOM.WorkOrderNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Area", DbType.String, companyWOM.Area);
            myDataBase.AddInParameter(dbCommand, "@in_Location", DbType.String, companyWOM.Location);

            myDataBase.AddInParameter(dbCommand, "in_Service_Tax", DbType.Decimal, companyWOM.TaxInformation.ServiceTax);
            myDataBase.AddInParameter(dbCommand, "in_VAT", DbType.Decimal, companyWOM.TaxInformation.VAT);
            myDataBase.AddInParameter(dbCommand, "in_CST_with_C_Form", DbType.Decimal, companyWOM.TaxInformation.CSTWithCForm);
            myDataBase.AddInParameter(dbCommand, "in_CST_without_C_Form", DbType.Decimal, companyWOM.TaxInformation.CSTWithoutCForm);
            myDataBase.AddInParameter(dbCommand, "in_Freight", DbType.Decimal, companyWOM.TaxInformation.Freight);

            if (companyWOM.TaxInformation.DiscountMode.Id == 0)
                myDataBase.AddInParameter(dbCommand, "in_Discount_Type", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "in_Discount_Type", DbType.Int32, companyWOM.TaxInformation.DiscountMode.Id);

            myDataBase.AddInParameter(dbCommand, "in_Total_Discount", DbType.Decimal, companyWOM.TaxInformation.TotalDiscount);
            myDataBase.AddInParameter(dbCommand, "in_Total_Net_Value", DbType.Decimal, companyWOM.TaxInformation.TotalNetValue);

            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, companyWOM.CreatedBy);

            myDataBase.AddOutParameter(dbCommand, "@out_Work_Order_Mapping_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Work_Order_Mapping_Id").ToString(), out id);
            return id;
        }

        public int CreateCompanyWorkOrderBankGuarantee(BankGuaranteeDOM bankGuarantee, Int32 companyWOId)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_COMPANY_WORK_ORDER_BANK_GUARANTEE);
            if (bankGuarantee.BankGuaranteeId > 0)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Bank_Guarantee_Id", DbType.String, bankGuarantee.BankGuaranteeId);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Bank_Guarantee_Id", DbType.String, DBNull.Value);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.String, companyWOId);
            myDataBase.AddInParameter(dbCommand, "@in_Start_Date", DbType.DateTime, bankGuarantee.StartDate);
            myDataBase.AddInParameter(dbCommand, "@in_End_Date", DbType.DateTime, bankGuarantee.EndDate);
            myDataBase.AddInParameter(dbCommand, "@in_Amount", DbType.Decimal, bankGuarantee.Amount);
            myDataBase.AddInParameter(dbCommand, "@in_Bank_Name", DbType.String, bankGuarantee.BankName);
            myDataBase.AddInParameter(dbCommand, "@in_Uploaded_Document", DbType.String, bankGuarantee.UploadedDocument);
            //myDataBase.AddInParameter(dbCommand, "@in_Uploaded_Document_Id", DbType.String, bankGuarantee.UploadedDocumentId);
            myDataBase.AddInParameter(dbCommand, "@in_Remarks", DbType.String, bankGuarantee.Remarks);

            myDataBase.AddInParameter(dbCommand, "in_Created_By", DbType.String, bankGuarantee.CreatedBy);
            myDataBase.AddOutParameter(dbCommand, "@out_Work_Order_Bank_Guarantee_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Work_Order_Bank_Guarantee_Id").ToString(), out id);
            return id;
        }

        public int CreateCompanyServiceDetail(ServiceDetailDOM serviceDetail, Int32? companyWOId)
        {

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_COMPANY_WORK_ORDER_SERVICE_DETAIL);

            if (companyWOId != null)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Service_Detail_Id", DbType.String, companyWOId);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Service_Detail_Id", DbType.String, DBNull.Value);

            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, companyWOId);
            myDataBase.AddInParameter(dbCommand, "@in_Work_Order_Number", DbType.String, serviceDetail.WorkOrderNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Item_Number", DbType.String, serviceDetail.ItemNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Service_Number", DbType.String, serviceDetail.ServiceNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Service_Description", DbType.String, serviceDetail.ServiceDescription);
            myDataBase.AddInParameter(dbCommand, "@in_Unit", DbType.Int32, serviceDetail.Unit.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Quantity", DbType.Decimal, serviceDetail.Quantity);
            myDataBase.AddInParameter(dbCommand, "@in_Unit_Rate", DbType.Decimal, serviceDetail.UnitRate);
            myDataBase.AddInParameter(dbCommand, "@in_Applicable_Rate", DbType.Decimal, serviceDetail.ApplicableRate);
            myDataBase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, serviceDetail.CreatedBy);
            myDataBase.AddOutParameter(dbCommand, "@out_Service_Detail_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Service_Detail_Id").ToString(), out id);
            return id;
        }

        public List<CompanyWorkOrderDOM> ReadCompWorkOrder(Int32? CompWorkOrderId)
        {
            lstCompWorkOrder = new List<CompanyWorkOrderDOM>();
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_COMPANY_WORK_ORDER);
            myDataBase.AddInParameter(dbCommand, "@Company_Work_Order_Id", DbType.Int32, CompWorkOrderId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    CompWorkOrder = GenerateCompanyWorkOrderDetailFromDataReader(reader);
                    lstCompWorkOrder.Add(CompWorkOrder);
                }
            }
            return lstCompWorkOrder;
        }

        public List<CompanyWorkOrderDOM> ReadCompWorkOrderByStatusId(Int32? statusId)
        {
            lstCompWorkOrder = new List<CompanyWorkOrderDOM>();
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_COMPANY_WORK_ORDER);
            myDataBase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, statusId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    CompWorkOrder = GenerateCompanyWorkOrderDetailFromDataReader(reader);
                    lstCompWorkOrder.Add(CompWorkOrder);
                }
            }
            return lstCompWorkOrder;
        }

        public List<CompanyWorkOrderDOM> ReadCompanyWorkOrderByDate(DateTime startDate, DateTime endDate, String contractNo)
        {
            lstCompWorkOrder = new List<CompanyWorkOrderDOM>();
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_COMPANY_WORK_ORDER_BY_DATE);
            if (startDate != DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Start_Date", DbType.DateTime, startDate);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Start_Date", DbType.DateTime, DBNull.Value);

            if (endDate != DateTime.MinValue)
            {
                myDataBase.AddInParameter(dbCommand, "@in_End_Date", DbType.DateTime, endDate);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_End_Date", DbType.DateTime, DBNull.Value);

            if (!string.IsNullOrEmpty(contractNo))
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, contractNo);
            }
            else
            {
                myDataBase.AddInParameter(dbCommand, "@in_Contract_No", DbType.String, DBNull.Value);
            }
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    CompWorkOrder = GenerateCompanyWorkOrderDetailFromDataReader(reader);
                    lstCompWorkOrder.Add(CompWorkOrder);
                }
            }
            return lstCompWorkOrder;
        }

        public List<WorkOrderMappingDOM> ReadCompanyWorkOrderMapping(Int32? CompWorkOrderId)
        {
            lstWorkOrderMapping = new List<WorkOrderMappingDOM>();
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_COMPANY_WORK_ORDER_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, CompWorkOrderId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    workOrderMapping = GenerateWorkOrderMappingDetailFromDataReader(reader);
                    lstWorkOrderMapping.Add(workOrderMapping);
                }
            }
            return lstWorkOrderMapping;
        }

        public List<BankGuaranteeDOM> ReadCompanyWorkOrderBankGuarantee(Int32? CompWorkOrderId)
        {
            lstBankGuarantee = new List<BankGuaranteeDOM>();
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_COMPANY_WORK_ORDER_BANK_GUARANTEE);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, CompWorkOrderId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    bankGuarantee = GenerateWorkOrderBankGuaranteeDetailFromDataReader(reader);
                    lstBankGuarantee.Add(bankGuarantee);
                }
            }
            return lstBankGuarantee;
        }

        public List<ServiceDetailDOM> ReadCompanyWorkOrderServiceDetail(Int32? CompWorkOrderId)
        {
            lstServiceDetail = new List<ServiceDetailDOM>();
            DbCommand dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_COMPANY_WORK_ORDER_SERVICE_DETAIL);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, CompWorkOrderId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    serviceDetail = GenerateWorkOrderServiceDetailFromDataReader(reader);
                    lstServiceDetail.Add(serviceDetail);
                }
            }
            return lstServiceDetail;
        }

        public void DeleteCompanyWorkOrder(Int32 companyWOId, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_COMPANY_WORK_ORDER);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, companyWOId);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public void DeleteCompanyWorkOrderMapping(Int32 companyWOId, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_COMPANY_WORK_ORDER_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, companyWOId);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public void DeleteCompanyWorkOrderBankGuarantee(Int32 companyWOId, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_COMPANY_WORK_ORDER_BANK_GUARANTEE);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, companyWOId);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public void DeleteCompanyWorkOrderServiceDetail(Int32 companyWOId, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_COMPANY_WORK_ORDER_SERVICE_DETAIL);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, companyWOId);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public int UpdateCompanyWorkOrder(CompanyWorkOrderDOM companyWO, Int32 companyWOId)
        {

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.UPDATE_COMPANY_WORK_ORDER);

            myDataBase.AddInParameter(dbCommand, "in_Company_Work_Order_Id", DbType.String, companyWOId);
            myDataBase.AddInParameter(dbCommand, "in_Contract_Number", DbType.String, companyWO.ContractNumber);
            myDataBase.AddInParameter(dbCommand, "in_Contract_Date", DbType.DateTime, companyWO.ContractDate);
            myDataBase.AddInParameter(dbCommand, "in_Work_Order_Description", DbType.String, companyWO.WorkOrderDescription);
            myDataBase.AddInParameter(dbCommand, "in_Total_Net_Value", DbType.Decimal, companyWO.TotalNetValue);
            myDataBase.AddInParameter(dbCommand, "in_Upload_Documnet_Id", DbType.Int32, companyWO.UploadDocumentId);
            myDataBase.AddInParameter(dbCommand, "in_Modified_By", DbType.String, companyWO.ModifiedBy);
            myDataBase.AddOutParameter(dbCommand, "out_Company_Work_Order_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Company_Work_Order_Id").ToString(), out id);
            return id;
        }

        public Int32 UpdateCompanyWorkOrderStatus(Int32 workOrderId, Int32 statusId, String approvedRegectedBy, String RemarkReject)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.UPDATE_COMPANY_WORK_ORDER_STATUS);
            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, workOrderId);
            myDataBase.AddInParameter(dbCommand, "@in_Status_Type_Id", DbType.Int32, statusId);
            myDataBase.AddInParameter(dbCommand, "@in_Approved_Rejected_By", DbType.String, approvedRegectedBy);
            myDataBase.AddInParameter(dbCommand, "@in_Remark_approve_Reject", DbType.String, RemarkReject);
            myDataBase.AddOutParameter(dbCommand, "@out_Company_Work_Order_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            Int32.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Company_Work_Order_Id").ToString(), out id);
            return id;
        }

        public String ValidateCompanyWorkOrder(Int32 companyWOId)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.VALIDATE_COMPANY_WORK_ORDER);

            myDataBase.AddInParameter(dbCommand, "@in_Company_Work_Order_Id", DbType.Int32, companyWOId);

            myDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            myDataBase.ExecuteNonQuery(dbCommand);
            str = myDataBase.GetParameterValue(dbCommand, "@out_errorCode").ToString();
            return str;
        }

        #endregion

        #region Private Section

        private CompanyWorkOrderDOM GenerateCompanyWorkOrderDetailFromDataReader(IDataReader reader)
        {
            CompWorkOrder = new CompanyWorkOrderDOM();
            CompWorkOrder.CompanyWorkOrderNumber = GetStringFromDataReader(reader, "Company_Work_Order_Number");
            CompWorkOrder.CompanyWorkOrderId = GetIntegerFromDataReader(reader, "Company_Work_Order_Id");
            CompWorkOrder.ContractNumber = GetStringFromDataReader(reader, "Contract_Number");
            CompWorkOrder.ContractDate = GetDateFromReader(reader, "Contract_Date");
            CompWorkOrder.WorkOrderDescription = GetStringFromDataReader(reader, "Work_Order_Description");
            CompWorkOrder.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Net_Value");
            CompWorkOrder.UploadDocumentId = GetIntegerFromDataReader(reader, "Upload_Documnet_Id");
            CompWorkOrder.StatusType = new MetaData();
            CompWorkOrder.StatusType.Id = GetIntegerFromDataReader(reader, "Status_Type_Id");
            CompWorkOrder.StatusType.Name = GetStringFromDataReader(reader, "Status_Type_Name");
            CompWorkOrder.ApprovedRejectedBy = GetStringFromDataReader(reader, "Approved_Rejected_By");
            CompWorkOrder.ApprovedRejectedDate = GetDateFromReader(reader, "Approved_Rejected_Date");
            CompWorkOrder.IsGenerated = GetIntegerFromDataReader(reader, "IsGenerated");
            CompWorkOrder.GeneratedBy = GetStringFromDataReader(reader, "Generated_By");
            CompWorkOrder.GeneratedDate = GetDateFromReader(reader, "Generated_Date");
            CompWorkOrder.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            CompWorkOrder.CreatedDate = GetDateFromReader(reader, "Created_Date");
            CompWorkOrder.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            CompWorkOrder.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            CompWorkOrder.RemarkReject = GetStringFromDataReader(reader, "Remark_approve_Reject");
            


            return CompWorkOrder;
        }

        private WorkOrderMappingDOM GenerateWorkOrderMappingDetailFromDataReader(IDataReader reader)
        {
            workOrderMapping = new WorkOrderMappingDOM();

            workOrderMapping.CompanyWorkOrderMappingId = GetIntegerFromDataReader(reader, "Company_Work_Order_Mapping_Id");
            workOrderMapping.CompanyWorkOrderId = GetIntegerFromDataReader(reader, "Company_Work_Order_Id");
            workOrderMapping.Amount = GetDecimalFromDataReader(reader, "Amount");
            workOrderMapping.WorkOrderNumber = GetStringFromDataReader(reader, "Work_Order_Number");
            workOrderMapping.Area = GetStringFromDataReader(reader, "Area");
            workOrderMapping.Location = GetStringFromDataReader(reader, "Location");

            workOrderMapping.TaxInformation = new Tax();
            workOrderMapping.TaxInformation.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_With_C_Form");
            workOrderMapping.TaxInformation.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_Without_C_Form");
            workOrderMapping.TaxInformation.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
            workOrderMapping.TaxInformation.VAT = GetDecimalFromDataReader(reader, "VAT");
            workOrderMapping.TaxInformation.Freight = GetDecimalFromDataReader(reader, "Freight");
            workOrderMapping.TaxInformation.TotalDiscount = GetDecimalFromDataReader(reader, "Total_Discount");
            workOrderMapping.TaxInformation.TotalNetValue = GetDecimalFromDataReader(reader, "Total_Net_Value");
            workOrderMapping.TaxInformation.DiscountMode = new MetaData();
            workOrderMapping.TaxInformation.DiscountMode.Id = GetIntegerFromDataReader(reader, "Discount_Type");
            workOrderMapping.TaxInformation.DiscountMode.Name = GetStringFromDataReader(reader, "Discount_Type_Name");

            workOrderMapping.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            workOrderMapping.CreatedDate = GetDateFromReader(reader, "Created_Date");
            workOrderMapping.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            workOrderMapping.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            return workOrderMapping;
        }

        private BankGuaranteeDOM GenerateWorkOrderBankGuaranteeDetailFromDataReader(IDataReader reader)
        {
            bankGuarantee = new BankGuaranteeDOM();
            bankGuarantee.BankGuaranteeId = GetIntegerFromDataReader(reader, "Bank_Guarantee_Id");
            bankGuarantee.CompanyWorkOrderId = GetIntegerFromDataReader(reader, "Company_Work_Order_Id");
            bankGuarantee.StartDate = GetDateFromReader(reader, "Start_Date");
            bankGuarantee.EndDate = GetDateFromReader(reader, "End_Date");
            bankGuarantee.Amount = GetDecimalFromDataReader(reader, "Amount");
            bankGuarantee.BankName = GetStringFromDataReader(reader, "Bank_Name");
            bankGuarantee.UploadedDocument = GetStringFromDataReader(reader, "Uploaded_Document");
            bankGuarantee.Remarks = GetStringFromDataReader(reader, "Remarks");
            bankGuarantee.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            bankGuarantee.CreatedDate = GetDateFromReader(reader, "Created_Date");
            bankGuarantee.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            bankGuarantee.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return bankGuarantee;
        }

        private ServiceDetailDOM GenerateWorkOrderServiceDetailFromDataReader(IDataReader reader)
        {
            serviceDetail = new ServiceDetailDOM();
            serviceDetail.ServiceDetailId = GetIntegerFromDataReader(reader, "Service_Detail_Id");
            serviceDetail.CompanyWorkOrderId = GetIntegerFromDataReader(reader, "Company_Work_Order_Id");
            serviceDetail.WorkOrderNumber = GetStringFromDataReader(reader, "Work_Order_Number");
            serviceDetail.ItemNumber = GetStringFromDataReader(reader, "Item_Number");
            serviceDetail.ServiceNumber = GetStringFromDataReader(reader, "Service_Number");
            serviceDetail.Quantity = GetDecimalFromDataReader(reader, "Quantity");
            serviceDetail.Unit = new MetaData();
            serviceDetail.Unit.Id = GetIntegerFromDataReader(reader, "Unit");
            serviceDetail.Unit.Name = GetStringFromDataReader(reader, "Unit_Name");
            serviceDetail.ServiceDescription = GetStringFromDataReader(reader, "Service_Description");
            serviceDetail.UnitRate = GetDecimalFromDataReader(reader, "Unit_Rate");
            serviceDetail.ApplicableRate = GetDecimalFromDataReader(reader, "Applicable_Rate");
            serviceDetail.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            serviceDetail.CreatedDate = GetDateFromReader(reader, "Created_Date");
            serviceDetail.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            serviceDetail.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            return serviceDetail;
        }

        private MetaData GenerateDataReader(IDataReader reader)
        {
            metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Number");
            return metaData;
        }

        #endregion
    }
}
