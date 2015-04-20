using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccessLayer
{
   public class TaxDAL:BaseDAL
   {
       #region Private Global Variable(s)
       private Database mydatabase = null;
       #endregion

       #region Constructor
       public TaxDAL(Database database)
       {
           mydatabase = database;
       }
       #endregion

       #region Tax CRUD
       public int CreateTaxMaster(Tax tax)
       {
          int id;
          DbCommand dbCommand;
          dbCommand= mydatabase.GetStoredProcCommand(DBConstants.CREATE_TAX_MASTER);
          mydatabase.AddInParameter(dbCommand, "@Service_Tax", DbType.Decimal, tax.ServiceTax);
          mydatabase.AddInParameter(dbCommand, "@VAT", DbType.Decimal, tax.VAT);
          mydatabase.AddInParameter(dbCommand, "@CST_With_C_Form", DbType.Decimal, tax.CSTWithCForm);
          mydatabase.AddInParameter(dbCommand, "@CST_Without_C_Form", DbType.Decimal, tax.CSTWithoutCForm);
          mydatabase.AddInParameter(dbCommand, "@Freight", DbType.Decimal, tax.Freight);
          mydatabase.AddInParameter(dbCommand, "@Payment_Discount_Id", DbType.Int32, tax.DiscountMode.Id);
          mydatabase.AddInParameter(dbCommand, "Total_Discount", DbType.Decimal, tax.TotalDiscount);
          mydatabase.AddInParameter(dbCommand, "@Packaging", DbType.Decimal, tax.Packaging);

          mydatabase.AddInParameter(dbCommand, "Created_By", DbType.String, tax.CreatedBy);
          mydatabase.AddOutParameter(dbCommand, "@out_Tax_Id", DbType.Int32, 10);
          mydatabase.ExecuteNonQuery(dbCommand);
          int.TryParse(mydatabase.GetParameterValue(dbCommand, "@out_Tax_Id").ToString(), out id);
          return id;
       }
       public int UpdateTaxMaster(Tax tax)
       {
           int id;
           DbCommand dbCommand;
           dbCommand = mydatabase.GetStoredProcCommand(DBConstants.UPDATE_TAX_MASTER);
           mydatabase.AddInParameter(dbCommand, "@Tax_Id", DbType.Int32, tax.TaxId);
           mydatabase.AddInParameter(dbCommand, "@Service_Tax", DbType.Decimal, tax.ServiceTax);
           mydatabase.AddInParameter(dbCommand, "@VAT", DbType.Decimal, tax.VAT);
           mydatabase.AddInParameter(dbCommand, "@CST_With_C_Form", DbType.Decimal, tax.CSTWithCForm);
           mydatabase.AddInParameter(dbCommand, "@CST_Without_C_Form", DbType.Decimal, tax.CSTWithoutCForm);
           mydatabase.AddInParameter(dbCommand, "@Freight", DbType.Decimal, tax.Freight);
           mydatabase.AddInParameter(dbCommand, "@Payment_Discount_Id", DbType.Int32, tax.DiscountMode.Id);
           mydatabase.AddInParameter(dbCommand, "Total_Discount", DbType.Decimal, tax.TotalDiscount);
           mydatabase.AddInParameter(dbCommand, "@Packaging", DbType.Decimal, tax.Packaging);

           mydatabase.AddInParameter(dbCommand, "@Modified_By", DbType.String, tax.ModifiedBy);
           mydatabase.AddOutParameter(dbCommand, "@out_Tax_Id", DbType.Int32, 10);
           mydatabase.ExecuteNonQuery(dbCommand);
           int.TryParse(mydatabase.GetParameterValue(dbCommand, "@out_Tax_Id").ToString(), out id);
           return id;
       }
       public void DeleteTax(int taxid, string modifiedBy, DateTime modifiedOn)
       {
           DbCommand dbCommand = mydatabase.GetStoredProcCommand(DBConstants.DELETE_TAX_MASTER);
           mydatabase.AddInParameter(dbCommand, "@Tax_Id", DbType.Int32, taxid);
           mydatabase.AddInParameter(dbCommand, "@Modified_By", DbType.String, modifiedBy);
           mydatabase.AddInParameter(dbCommand, "@Modified_Date", DbType.DateTime, modifiedOn);
           mydatabase.ExecuteNonQuery(dbCommand);

       }
       public List<Tax> ReadTaxMaster(int? taxid)
       {
           List<Tax> lstTax = new List<Tax>();
           Tax tax = null;
           DbCommand dbCommand;
           dbCommand = mydatabase.GetStoredProcCommand(DBConstants.READ_TAX_MASTER);
           mydatabase.AddInParameter(dbCommand, "@in_Tax_id", DbType.Int32, taxid);

           using (IDataReader reader = mydatabase.ExecuteReader(dbCommand))
           {
               while (reader.Read())
               {
                   tax = GenerateTaxFromDataReader(reader);
                   lstTax.Add(tax);
               }
           }
           return lstTax;
       }
       public List<MetaData> ReadDiscountMode(int? id)
       {
           List<MetaData> lstMetaData = new List<MetaData>();
           MetaData metaData = null;
           DbCommand dbCommand;
           dbCommand = mydatabase.GetStoredProcCommand(DBConstants.READ_DISCOUNT_MODE);
           mydatabase.AddInParameter(dbCommand, "@Id", DbType.Int32, id);

           using (IDataReader reader = mydatabase.ExecuteReader(dbCommand))
           {
               while (reader.Read())
               {
                   metaData = GenerateDiscountModeFromDataReader(reader);
                   lstMetaData.Add(metaData);
               }
           }
           return lstMetaData;
       }

       public List<Tax> ReadTaxByDiscountModeId(int? discounModeId)
       {
           List<Tax> lstTax = new List<Tax>();
           Tax tax = null;
           DbCommand dbCommand;
           dbCommand = mydatabase.GetStoredProcCommand(DBConstants.READ_TAX_BY_DISCOUNT_Mode_ID);
           mydatabase.AddInParameter(dbCommand, "@in_Payment_Discount_Id", DbType.Int32, discounModeId);

           using (IDataReader reader = mydatabase.ExecuteReader(dbCommand))
           {
               while (reader.Read())
               {
                   tax = GenerateTaxByDisccountModeIdFromDataReader(reader);
                   lstTax.Add(tax);
               }
           }
           return lstTax;
       }

       #endregion

       #region Private Section
       private Tax GenerateTaxFromDataReader(IDataReader reader)
       {
           Tax tax = new Tax();
           tax.TaxId = GetIntegerFromDataReader(reader, "Tax_Id");
           tax.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
           tax.VAT = GetDecimalFromDataReader(reader, "VAT");
           tax.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_With_C_Form");
           tax.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_Without_C_Form");
           tax.Freight = GetDecimalFromDataReader(reader, "Freight");
           tax.DiscountMode = new MetaData();
           tax.DiscountMode.Id = GetIntegerFromDataReader(reader, "Payment_Discount_Id");
           tax.DiscountMode.Name = GetStringFromDataReader(reader, "Name");
           tax.TotalDiscount = GetDecimalFromDataReader(reader, "Total_Discount");
           tax.Packaging = GetDecimalFromDataReader(reader, "Packaging");
           tax.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
           tax.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

           return tax;
       }

       private Tax GenerateTaxByDisccountModeIdFromDataReader(IDataReader reader)
       {
           Tax tax = new Tax();
           tax.TaxId = GetIntegerFromDataReader(reader, "Tax_Id");
           tax.ServiceTax = GetDecimalFromDataReader(reader, "Service_Tax");
           tax.VAT = GetDecimalFromDataReader(reader, "VAT");
           tax.CSTWithCForm = GetDecimalFromDataReader(reader, "CST_With_C_Form");
           tax.CSTWithoutCForm = GetDecimalFromDataReader(reader, "CST_Without_C_Form");
           tax.Freight = GetDecimalFromDataReader(reader, "Freight");
           tax.DiscountMode = new MetaData();
           tax.DiscountMode.Id = GetIntegerFromDataReader(reader, "Payment_Discount_Id");
           tax.DiscountMode.Name = GetStringFromDataReader(reader, "Name");
           tax.TotalDiscount = GetDecimalFromDataReader(reader, "Total_Discount");
           tax.Packaging = GetDecimalFromDataReader(reader, "Packaging");

           return tax;
       }
       private MetaData GenerateDiscountModeFromDataReader(IDataReader reader)
       {
           MetaData metaData = new MetaData();
           metaData.Id = GetIntegerFromDataReader(reader, "Id");
           metaData.Name = GetStringFromDataReader(reader, "Name");
           metaData.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
           metaData.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

           return metaData;
       }
       #endregion
   }
}
