using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DocumentObjectModel;
using DataAccessLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace BusinessAccessLayer
{
    public class CompanyWorkOrderBL : BaseBL
    {
        #region Private Global Variables

        private Database Mydatabase;

        private CompanyWorkOrderDL companyWorkOrderDL = null;
        List<WorkOrderMappingDOM> lstWorkOrderMapping = null;
        List<BankGuaranteeDOM> lstBankGuarnatee = null;
        List<ServiceDetailDOM> lstServiceDetail = null;
        List<MetaData> lstMetaData = null;

        #endregion

        #region Constructor(s)

        public CompanyWorkOrderBL()
        {
            Mydatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            companyWorkOrderDL = new CompanyWorkOrderDL(Mydatabase);
        }

        #endregion

        #region CURD Methods

        public List<MetaData> CreateCompanyWorkOrder(CompanyWorkOrderDOM companyWO, List<WorkOrderMappingDOM> lstWOM, List<BankGuaranteeDOM> lstBankGuarantee)
        {
            lstMetaData=new List<MetaData>();
            //int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstMetaData = companyWorkOrderDL.CreateCompanyWorkOrder(companyWO);
                    if (lstMetaData.Count > 0)
                    {
                        foreach (WorkOrderMappingDOM item in lstWOM)
                        {
                            companyWorkOrderDL.CreateCompanyWorkOrderMapping(item, lstMetaData[0].Id);
                        }
                        foreach (BankGuaranteeDOM item in lstBankGuarantee)
                        {
                            companyWorkOrderDL.CreateCompanyWorkOrderBankGuarantee(item, lstMetaData[0].Id);
                        }
                        //foreach (ServiceDetailDOM item in lstServiceDetail)
                        //{
                        //    companyWorkOrderDL.CreateCompanyServiceDetail(item, id);
                        //}
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                //Logger.Write(exp.Message);
                throw exp;
            }

            return lstMetaData;
        }

        public List<CompanyWorkOrderDOM> ReadCompOrder(Int32? CompWorkOrderId)
        {
            List<CompanyWorkOrderDOM> lstCompWorkOrder = new List<CompanyWorkOrderDOM>();
            try
            {
                lstCompWorkOrder = companyWorkOrderDL.ReadCompWorkOrder(CompWorkOrderId);
            }
            catch (Exception ex)
            {
                //Logger.Write(ex.Message);
                throw ex;
            }
            return lstCompWorkOrder;
        }

        public List<CompanyWorkOrderDOM> ReadCompWorkOrderByStatusId(Int32? statusId)
        {
            List<CompanyWorkOrderDOM> lstCompWorkOrder = new List<CompanyWorkOrderDOM>();
            try
            {
                lstCompWorkOrder = companyWorkOrderDL.ReadCompWorkOrderByStatusId(statusId);
            }
            catch (Exception ex)
            {
                //Logger.Write(ex.Message);
                throw ex;
            }
            return lstCompWorkOrder;
        }

        public List<CompanyWorkOrderDOM> ReadCompanyOrderByDate(DateTime startDate, DateTime endDate, String contractNo)
        {
            List<CompanyWorkOrderDOM> lstCompWorkOrder = new List<CompanyWorkOrderDOM>();
            try
            {
                lstCompWorkOrder = companyWorkOrderDL.ReadCompanyWorkOrderByDate(startDate, endDate, contractNo);
            }
            catch (Exception ex)
            {
                //Logger.Write(ex.Message);
                throw ex;
            }
            return lstCompWorkOrder;
        }

        public List<WorkOrderMappingDOM> ReadCompanyWorkOrderMapping(Int32? CompWorkOrderId)
        {
            lstWorkOrderMapping = new List<WorkOrderMappingDOM>();
            try
            {
                lstWorkOrderMapping = companyWorkOrderDL.ReadCompanyWorkOrderMapping(CompWorkOrderId);
            }
            catch (Exception ex)
            {
                //Logger.Write(ex.Message);
                throw ex;
            }
            return lstWorkOrderMapping;
        }

        public List<BankGuaranteeDOM> ReadCompanyWorkOrderBankGuarantee(Int32? CompWorkOrderId)
        {
            lstBankGuarnatee = new List<BankGuaranteeDOM>();
            try
            {
                lstBankGuarnatee = companyWorkOrderDL.ReadCompanyWorkOrderBankGuarantee(CompWorkOrderId);
            }
            catch (Exception ex)
            {
                //Logger.Write(ex.Message);
                throw ex;
            }
            return lstBankGuarnatee;
        }

        public List<ServiceDetailDOM> ReadCompanyWorkOrderServiceDetail(Int32? CompWorkOrderId)
        {
            lstServiceDetail = new List<ServiceDetailDOM>();
            try
            {
                lstServiceDetail = companyWorkOrderDL.ReadCompanyWorkOrderServiceDetail(CompWorkOrderId);
            }
            catch (Exception ex)
            {
                //Logger.Write(ex.Message);
                throw ex;
            }
            return lstServiceDetail;
        }

        public String DeleteCompanyWorkOrder(Int32 companyWOId, String modifiedBy)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    errorMessage = companyWorkOrderDL.ValidateCompanyWorkOrder(companyWOId);
                    if (errorMessage == "")
                    {
                        companyWorkOrderDL.DeleteCompanyWorkOrderMapping(companyWOId, modifiedBy);
                        companyWorkOrderDL.DeleteCompanyWorkOrderBankGuarantee(companyWOId, modifiedBy);
                        companyWorkOrderDL.DeleteCompanyWorkOrder(companyWOId, modifiedBy);
                        companyWorkOrderDL.DeleteCompanyWorkOrderServiceDetail(companyWOId, modifiedBy);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return errorMessage;
        }

        public int UpdateCompanyWorkOrder(CompanyWorkOrderDOM companyWO, List<WorkOrderMappingDOM> lstWOM, List<BankGuaranteeDOM> lstBankGuarantee, Int32 companyWOId)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = companyWorkOrderDL.UpdateCompanyWorkOrder(companyWO, companyWOId);
                    if (id > 0)
                    {
                        companyWorkOrderDL.DeleteCompanyWorkOrderMapping(companyWOId, companyWO.ModifiedBy);

                        foreach (WorkOrderMappingDOM item in lstWOM)
                        {
                            companyWorkOrderDL.CreateCompanyWorkOrderMapping(item, companyWOId);
                        }

                        companyWorkOrderDL.DeleteCompanyWorkOrderBankGuarantee(companyWOId, companyWO.ModifiedBy);

                        foreach (BankGuaranteeDOM item in lstBankGuarantee)
                        {
                            companyWorkOrderDL.CreateCompanyWorkOrderBankGuarantee(item, companyWOId);
                        }

                        //companyWorkOrderDL.DeleteCompanyWorkOrderServiceDetail(companyWOId, companyWO.ModifiedBy);
                        //foreach (ServiceDetailDOM item in lstServiceDetail)
                        //{
                        //    companyWorkOrderDL.CreateCompanyServiceDetail(item, companyWOId);
                        //}
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                //Logger.Write(exp.Message);
                throw exp;
            }

            return id;
        }

        public Int32 UpdateCompanyWorkOrderStatus(Int32 workOrderId, Int32 statusId, String approvedRegectedBy, String RemarkReject)
        {
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = companyWorkOrderDL.UpdateCompanyWorkOrderStatus(workOrderId, statusId, approvedRegectedBy,RemarkReject);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return id;
        }

        public int CreateServiceDetail(ServiceDetailDOM serviceDetail)
        {
            int id = 0;

            try
            {
                id = companyWorkOrderDL.CreateCompanyServiceDetail(serviceDetail, null);
            }
            catch (Exception exp)
            {
                throw exp;

                //Logger.Write(exp.Message);
            }

            return id;
        }

        public int UpdateServiceDetail(ServiceDetailDOM serviceDetail, Int32 serviceDetailId)
        {
            int id = 0;
            try
            {
                id = companyWorkOrderDL.CreateCompanyServiceDetail(serviceDetail, serviceDetailId);
            }
            catch (Exception exp)
            {
                throw exp;
                //Logger.Write(exp.Message);
            }

            return id;
        }

        public String DeleteServiceDetail(Int32 serviceDetailId, String modifiedBy)
        {
            string errorMessage = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    //errorMessage = companyWorkOrderDL.ValidateCompanyWorkOrder(serviceDetailId);
                    if (errorMessage == "")
                    {
                        companyWorkOrderDL.DeleteCompanyWorkOrderServiceDetail(serviceDetailId, modifiedBy);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return errorMessage;
        }

        #endregion
    }
}
