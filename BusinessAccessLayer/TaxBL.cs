using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using DataAccessLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Transactions;

namespace BusinessAccessLayer
{
   public class TaxBL:BaseBL
   {
       #region Private Global Variable(s)
       TaxDAL taxDAL = null;
       Database mydatabase = null;
       #endregion

       #region Constructor
       public TaxBL()
       {
           mydatabase = DatabaseFactory.CreateDatabase(C_ConnectionString);
           taxDAL = new TaxDAL(mydatabase);
       }
       #endregion

       #region Tax CRUD Methods
       public int CreateTaxMaster(Tax tax)
       {
           int id=0;
           try
           {
               using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
               {
                   id = taxDAL.CreateTaxMaster(tax);
                   scope.Complete();
               }
           }
           catch(Exception ex)
           {
               Logger.Write(ex.Message);
           }
           return id;
       }
       public int UpdateTaxMaster(Tax tax)
       {
           int id = 0;
           try
           {
               using(TransactionScope Scope=new TransactionScope(TransactionScopeOption.Required,base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
               {
                   id=taxDAL.UpdateTaxMaster(tax);
                   Scope.Complete(); 
               }
           }
           catch (Exception ex)
           {
               Logger.Write(ex.Message);
           }
           return id;

       }
       public string DeleteTax(int taxid, string modifiedBy, DateTime modifiedOn)
       {
           string errorMessage = string.Empty;
           try
           {

               using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
               {
                   //errorMessage = departmentDAL.ValidateDepartment(departmentId);
                   if (errorMessage == "")
                   {
                       taxDAL.DeleteTax(taxid, modifiedBy, modifiedOn);
                   }
                   scope.Complete();
               }
           }
           catch (Exception exp)
           {
               Logger.Write(exp.Message);
           }
           return errorMessage;
       }
       public List<Tax> ReadTaxMaster(int? taxid)
       {
           List<Tax> lstTax = new List<Tax>();
           try
           {
               lstTax = taxDAL.ReadTaxMaster(taxid);
           }
           catch(Exception ex)
           {
               Logger.Write(ex.Message);
           }
           return lstTax;
       }
       public List<MetaData> ReadDiscountMode(int? id)
       {
           List<MetaData> lstMetaData = new List<MetaData>();
           try
           {
               lstMetaData = taxDAL.ReadDiscountMode(id);
           }
           catch (Exception ex)
           {
               Logger.Write(ex.Message);
           }
           return lstMetaData;
       }

       public List<Tax> ReadTaxByDiscountModeId(int? discounModeId)
       {
           List<Tax> lstTax = new List<Tax>();
           try
           {
               lstTax = taxDAL.ReadTaxByDiscountModeId(discounModeId);
           }
           catch (Exception ex)
           {
               Logger.Write(ex.Message);
           }
           return lstTax;
       }

       #endregion
   }
}
