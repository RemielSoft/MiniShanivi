using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DocumentObjectModel;
using System.Data.Common;
using System.Data;

namespace DataAccessLayer
{
    public class SupplierDAL:BaseDAL
    {
         #region private global variable(s)

        private Database MyDataBase;
        Supplier supplier = new Supplier();
        #endregion
         #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public SupplierDAL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion
         #region Supplier CRUD Method
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="group">The user.</param>
        /// <returns></returns>

        public int CreateSupplier(Supplier supplier)
        {
            int id;
            String sqlCommand = DBConstants.CREATE_SUPPLIER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Supplier_Name", DbType.String, supplier.Name);
            MyDataBase.AddInParameter(dbCommand, "@Email", DbType.String, supplier.Email);
            MyDataBase.AddInParameter(dbCommand, "@Supplier_Address", DbType.String, supplier.Address);
            MyDataBase.AddInParameter(dbCommand, "@City", DbType.String, supplier.City);
            MyDataBase.AddInParameter(dbCommand, "@Pin_Code", DbType.String, supplier.PinCode);
            MyDataBase.AddInParameter(dbCommand, "@State", DbType.String, supplier.State);
            MyDataBase.AddInParameter(dbCommand, "@Mobile", DbType.String, supplier.Mobile);
            MyDataBase.AddInParameter(dbCommand, "@Website", DbType.String, supplier.Website);
            MyDataBase.AddInParameter(dbCommand, "@Phone", DbType.String, supplier.Phone);
            MyDataBase.AddInParameter(dbCommand, "@ServiceTax_no", DbType.String, supplier.Information.ServiceTaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@Tan_no", DbType.String, supplier.Information.TanNumber);
            MyDataBase.AddInParameter(dbCommand, "@Esi_no", DbType.String, supplier.Information.EsiNumber);
            MyDataBase.AddInParameter(dbCommand, "@Fax_no", DbType.String, supplier.Information.FaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@Pan_no", DbType.String, supplier.Information.PanNumber);
            MyDataBase.AddInParameter(dbCommand, "@Pf_no", DbType.String, supplier.Information.PfNumber);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_Name", DbType.String, supplier.Information.ContactPersonName);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_EmailId", DbType.String, supplier.Information.ContactPersonEmailId);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_PhoneNo", DbType.String, supplier.Information.ContactPersonPhoneNo);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_MobileNo", DbType.String, supplier.Information.ContactPersonMobileNo);
            MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, supplier.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_Supplier_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Supplier_Id").ToString(), out id);

            return id;
        }
        public int UpdateSupplier(Supplier supplier)
        {
            int id;
            String sqlCommand = DBConstants.UPDATE_SUPPLIER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Supplier_Id", DbType.Int32, supplier.SupplierId);
            MyDataBase.AddInParameter(dbCommand, "@Supplier_Name", DbType.String, supplier.Name);
            MyDataBase.AddInParameter(dbCommand, "@Email", DbType.String, supplier.Email);
            MyDataBase.AddInParameter(dbCommand, "@Supplier_Address", DbType.String, supplier.Address);
            MyDataBase.AddInParameter(dbCommand, "@City", DbType.String, supplier.City);
            MyDataBase.AddInParameter(dbCommand, "@State", DbType.String, supplier.State);
            MyDataBase.AddInParameter(dbCommand, "@Pin_Code", DbType.String, supplier.PinCode);
            MyDataBase.AddInParameter(dbCommand, "@Mobile", DbType.String, supplier.Mobile);
            MyDataBase.AddInParameter(dbCommand, "@Website", DbType.String, supplier.Website);
            MyDataBase.AddInParameter(dbCommand, "@Phone", DbType.String, supplier.Phone);
            MyDataBase.AddInParameter(dbCommand, "@ServiceTax_no", DbType.String, supplier.Information.ServiceTaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@Tan_no", DbType.String, supplier.Information.TanNumber);
            MyDataBase.AddInParameter(dbCommand, "@Esi_no", DbType.String, supplier.Information.EsiNumber);
            MyDataBase.AddInParameter(dbCommand, "@Fax_no", DbType.String, supplier.Information.FaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@Pan_no", DbType.String, supplier.Information.PanNumber);
            MyDataBase.AddInParameter(dbCommand, "@Pf_no", DbType.String, supplier.Information.PfNumber);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_Name", DbType.String, supplier.Information.ContactPersonName);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_EmailId", DbType.String, supplier.Information.ContactPersonEmailId);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_PhoneNo", DbType.String, supplier.Information.ContactPersonPhoneNo);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_MobileNo", DbType.String, supplier.Information.ContactPersonMobileNo);
            MyDataBase.AddInParameter(dbCommand, "@Modified_by", DbType.String, supplier.ModifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_date", DbType.DateTime, supplier.ModifiedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_Supplier_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Supplier_Id").ToString(), out id);

            return id;
        }
        public List<Supplier> ReadSupplier(Int32? supplierid)
        {
            List<Supplier> lstSupplier = new List<Supplier>();

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_SUPPLIER_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Supplier_Id", DbType.Int32, supplierid);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    supplier = GenerateSupplierDetailFromDataReader(reader);
                    lstSupplier.Add(supplier);
                }
            }
            return lstSupplier;
        }
        public void DeleteSupplier(int supplierid, string modifiedBy, DateTime modifiedOn)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.DELETE_SUPPLIER_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Supplier_Id", DbType.Int32, supplierid);
            MyDataBase.AddInParameter(dbCommand, "@Modified_by", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);

        }
        public string ValidateSupplier(int contractorid)
        {

            String sqlCommand = DBConstants.VALIDATE_SUPPLIER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);

            MyDataBase.AddInParameter(dbCommand, "@in_Supplier_Id", DbType.Int32, contractorid);

            MyDataBase.AddOutParameter(dbCommand, "@out_errorCode", DbType.String, 400);
            MyDataBase.ExecuteNonQuery(dbCommand);

            return Convert.ToString(MyDataBase.GetParameterValue(dbCommand, "@out_errorCode"));
        }
           
        #endregion

         #region Public Section

        public int ValidateUser(String loginId, String password)
        {
            int userId;

            String sqlCommand = DBConstants.VALIDATE_USER_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);

            MyDataBase.AddInParameter(dbCommand, "in_login_id", DbType.String, loginId);
            MyDataBase.AddInParameter(dbCommand, "in_password", DbType.String, password);
            MyDataBase.AddOutParameter(dbCommand, "@out_userId", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_userId").ToString(), out userId);

            return userId;
        }
        #endregion

         #region Private Section
        private Supplier GenerateSupplierDetailFromDataReader(IDataReader reader)
        {
            Supplier supplier = new Supplier();
            supplier.SupplierId = GetIntegerFromDataReader(reader, "Supplier_Id");
            supplier.Name = GetStringFromDataReader(reader, "Supplier_Name");
            supplier.Email = GetStringFromDataReader(reader, "Email");
            supplier.Address = GetStringFromDataReader(reader, "Supplier_Address");
            supplier.City = GetStringFromDataReader(reader, "City");
            supplier.State = GetStringFromDataReader(reader, "State");
            supplier.PinCode = GetStringFromDataReader(reader, "Pin_Code");
            supplier.Mobile = GetStringFromDataReader(reader, "Mobile");
            supplier.Phone = GetStringFromDataReader(reader, "Phone");
            supplier.Information = new Information();
            supplier.Information.VendorCode = GetStringFromDataReader(reader, "VendorCode");
            supplier.Information.ServiceTaxNumber = GetStringFromDataReader(reader, "ServiceTax_No");
            supplier.Information.PanNumber = GetStringFromDataReader(reader, "PAN_No");
            supplier.Information.TanNumber = GetStringFromDataReader(reader, "TAN_No");
            supplier.Information.FaxNumber = GetStringFromDataReader(reader, "Fax_No");
            supplier.Information.EsiNumber = GetStringFromDataReader(reader, "ESI_No");
            supplier.Information.PfNumber = GetStringFromDataReader(reader, "PF_No");
            supplier.Website = GetStringFromDataReader(reader, "Website");
            supplier.Information.ContactPersonName = GetStringFromDataReader(reader, "ContactPersonName");
            supplier.Information.ContactPersonEmailId = GetStringFromDataReader(reader, "ContactPersonEmailId");
            supplier.Information.ContactPersonMobileNo = GetStringFromDataReader(reader, "ContactPersonMobile");
            supplier.Information.ContactPersonPhoneNo = GetStringFromDataReader(reader, "ContactPersonPhone");
            supplier.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            supplier.CreatedDate = GetDateFromReader(reader, "Created_Date");
            supplier.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            supplier.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            return supplier;
        }
        #endregion
    }
}
