using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataAccessLayer;
using DocumentObjectModel;
using System.Transactions;

namespace BusinessAccessLayer
{
   public class IssueDemandVoucherBL:BaseBL
   {
       #region private global variables
       private Database mydatabase;
       private IssueDemandVoucherDAL issueDemandVoucherDAL = null;

       MetaData metaData = null;
       //List<QuotationDOM> lstQuotation =null;
       List<IssueDemandVoucherDOM> lstIssueDemandVoucherDOM = null;
       List<ItemTransaction> lstItemTransaction = null;
       int id = 0;
       #endregion

       #region Constructors
       public IssueDemandVoucherBL()
       {
           mydatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
           issueDemandVoucherDAL = new IssueDemandVoucherDAL(mydatabase);
       }
       #endregion

       #region issueDemandVoucher CRUD
       public MetaData CreateIssueDemandVoucher(IssueDemandVoucherDOM issueDemandVoucherDOM, Int32? IDVID)
       {
           id = 0;
           metaData = new MetaData();
           try
           {
               using(TransactionScope transactionScope=new TransactionScope(TransactionScopeOption.Required,base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
               {
                   metaData = issueDemandVoucherDAL.CreateIssueDemandVoucher(issueDemandVoucherDOM, IDVID);
                   if (metaData.Id > 0)
                   {
                       if (IDVID > 0)
                       {
                           issueDemandVoucherDAL.ResetIssueDemandVoucherMapping(IDVID);
                       }                       
                       id = issueDemandVoucherDAL.CreateIssueDemandVoucherMapping(issueDemandVoucherDOM.Quotation.ItemTransaction, metaData.Id);
                   }
                   transactionScope.Complete();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return metaData;
       }
       //public List<ItemTransaction> CountItemRequired(string ContractorQuotationNumber, int ActivityId)
       //{           
       //    lstItemTransaction = new List<ItemTransaction>();
       //    try
       //    {
       //        lstItemTransaction = issueDemandVoucherDAL.CountItemRequired(ContractorQuotationNumber, ActivityId);
       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //    return lstItemTransaction;
       //}
       public List<IssueDemandVoucherDOM> ViewIssueDemand(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String IDVNo)
       {
           lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
           try
           {
               lstIssueDemandVoucherDOM = issueDemandVoucherDAL.ViewIssueDemand(contractorId, toDate, fromDate, contractNo, IDVNo);
           }
           catch (Exception exp)
           {
               throw exp;
           }
           return lstIssueDemandVoucherDOM;
       }
       public List<IssueDemandVoucherDOM> ReadMaterialIssueDemandVoucher(Int32? IssueDemandVoucherId, String IssueDemandVoucherNo)
       {
           lstIssueDemandVoucherDOM = new List<IssueDemandVoucherDOM>();
           try
           {
               lstIssueDemandVoucherDOM = issueDemandVoucherDAL.ReadMaterialIssueDemandVoucher(IssueDemandVoucherId, IssueDemandVoucherNo);
           }
           catch (Exception exp)
           {
               throw exp;
           }
           return lstIssueDemandVoucherDOM;
       }
       public List<ItemTransaction> ReadIssueDemandMapping(Int32? IssueDemandVoucherId, String IssueDemandVoucherNo)
       {
           lstItemTransaction = new List<ItemTransaction>();
           try
           {
               lstItemTransaction = issueDemandVoucherDAL.ReadIssueDemandMapping(IssueDemandVoucherId, IssueDemandVoucherNo);
           }
           catch (Exception exp)
           {
               throw exp;
           }
           return lstItemTransaction;
       }
       public string DeleteIssueDemandVoucher(int IssueDemandVoucherId, string modifiedBy, DateTime modifiedOn)
       {
           string errorMessage = string.Empty;
           try
           {
               using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
               {
                  // errorMessage = issueDemandVoucherDAL.ValidateIssueDemandVoucher(IssueDemandVoucherId, "IDVNo is Used in Mapping Table");
                   if (errorMessage == "")
                   {
                       issueDemandVoucherDAL.DeleteIssueDemandVoucher(IssueDemandVoucherId,modifiedBy,modifiedOn);
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
       public Int32 UpdateIssueDemandVoucherStatus(IssueDemandVoucherDOM issueDemandVoucherDOM)
       {
           Int32 IssueDemandVoucherId;
           try
           {
               IssueDemandVoucherId = issueDemandVoucherDAL.UpdateIssueDemandVoucherStatus(issueDemandVoucherDOM);
           }
           catch (Exception exp)
           {
               throw exp;
           }
           return IssueDemandVoucherId;
       }
       #endregion
   }
}
