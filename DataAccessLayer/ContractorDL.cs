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
   public class ContractorDL:BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;
        Contractor contractor = new Contractor();
        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public ContractorDL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        #region Contractor CRUD Method
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="group">The user.</param>
        /// <returns></returns>

        public int CreateContractor(Contractor contractor)
        {
            int id;
            String sqlCommand = DBConstants.CREATE_CONTRACTOR_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Contractor_Name", DbType.String, contractor.Name);
            MyDataBase.AddInParameter(dbCommand, "@Email", DbType.String, contractor.Email);
            MyDataBase.AddInParameter(dbCommand, "@Address", DbType.String, contractor.Address);
            MyDataBase.AddInParameter(dbCommand, "@City", DbType.String, contractor.City);
            MyDataBase.AddInParameter(dbCommand, "@State", DbType.String, contractor.State);
            MyDataBase.AddInParameter(dbCommand, "@Pin", DbType.String, contractor.PinCode);
            MyDataBase.AddInParameter(dbCommand, "@Phone", DbType.String, contractor.Phone);
            MyDataBase.AddInParameter(dbCommand, "@Mobile", DbType.String, contractor.Mobile);
            MyDataBase.AddInParameter(dbCommand, "@Esi_no", DbType.String, contractor.Information.EsiNumber);
            MyDataBase.AddInParameter(dbCommand, "@Fax_no", DbType.String, contractor.Information.FaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@Pan_no", DbType.String, contractor.Information.PanNumber);
            MyDataBase.AddInParameter(dbCommand, "@Pf_no", DbType.String, contractor.Information.PfNumber);
            MyDataBase.AddInParameter(dbCommand, "@ServiceTax_no", DbType.String, contractor.Information.ServiceTaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@Tan_no", DbType.String, contractor.Information.TanNumber);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_Name", DbType.String, contractor.Information.ContactPersonName);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_EmailId", DbType.String, contractor.Information.ContactPersonEmailId);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_PhoneNo", DbType.String, contractor.Information.ContactPersonPhoneNo);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_MobileNo", DbType.String, contractor.Information.ContactPersonMobileNo);
            MyDataBase.AddInParameter(dbCommand, "@Website", DbType.String, contractor.Website);
            MyDataBase.AddInParameter(dbCommand, "@Description", DbType.String, contractor.Description);
            MyDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, contractor.CreatedBy);
            MyDataBase.AddOutParameter(dbCommand, "@out_Contractor_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Contractor_Id").ToString(), out id);

            return id;
        }
        public int UpdateContractor(Contractor contractor)
        {
            int id;
            String sqlCommand = DBConstants.UPDATE_CONTRACTOR_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);
            MyDataBase.AddInParameter(dbCommand, "@Contractor_Id", DbType.Int32, contractor.ContractorId);
            MyDataBase.AddInParameter(dbCommand, "@Contractor_Name", DbType.String, contractor.Name);
            MyDataBase.AddInParameter(dbCommand, "@Email", DbType.String, contractor.Email);
            MyDataBase.AddInParameter(dbCommand, "@Address", DbType.String, contractor.Address);
            MyDataBase.AddInParameter(dbCommand, "@City", DbType.String, contractor.City);
            MyDataBase.AddInParameter(dbCommand, "@State", DbType.String, contractor.State);
            MyDataBase.AddInParameter(dbCommand, "@Pin", DbType.String, contractor.PinCode);
            MyDataBase.AddInParameter(dbCommand, "@Phone", DbType.String, contractor.Phone);
            MyDataBase.AddInParameter(dbCommand, "@Mobile", DbType.String, contractor.Mobile);
            MyDataBase.AddInParameter(dbCommand, "@Website", DbType.String, contractor.Website);
            MyDataBase.AddInParameter(dbCommand, "@Description", DbType.String, contractor.Description);
            MyDataBase.AddInParameter(dbCommand, "@Esi_no", DbType.String, contractor.Information.EsiNumber);
            MyDataBase.AddInParameter(dbCommand, "@Fax_no", DbType.String, contractor.Information.FaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@Pan_no", DbType.String, contractor.Information.PanNumber);
            MyDataBase.AddInParameter(dbCommand, "@Pf_no", DbType.String, contractor.Information.PfNumber);
            MyDataBase.AddInParameter(dbCommand, "@ServiceTax_no", DbType.String, contractor.Information.ServiceTaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@Tan_no", DbType.String, contractor.Information.TanNumber);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_Name", DbType.String, contractor.Information.ContactPersonName);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_EmailId", DbType.String, contractor.Information.ContactPersonEmailId);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_PhoneNo", DbType.String, contractor.Information.ContactPersonPhoneNo);
            MyDataBase.AddInParameter(dbCommand, "@Contact_Person_MobileNo", DbType.String, contractor.Information.ContactPersonMobileNo);
            MyDataBase.AddInParameter(dbCommand, "@Modified_by", DbType.String, contractor.ModifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_date", DbType.DateTime, contractor.ModifiedDate);
            MyDataBase.AddOutParameter(dbCommand, "@out_Contractor_Id", DbType.Int32, 10);
            MyDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(MyDataBase.GetParameterValue(dbCommand, "@out_Contractor_Id").ToString(), out id);

            return id;
        }
        public List<Contractor> ReadContractor(Int32 ?contractorid)
        {
            List<Contractor> lstContractor = new List<Contractor>();

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_CONTRACTOR_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Contractor_Id", DbType.Int32, contractorid);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    contractor = GenerateContractorDetailFromDataReader(reader);
                    lstContractor.Add(contractor);
                }
            }
            return lstContractor;
        }
        public void DeleteContractor(int contractorid, string modifiedBy, DateTime modifiedOn)
        {
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.DELETE_CONTRACTOR_MASTER);
            MyDataBase.AddInParameter(dbCommand, "@Contractor_Id", DbType.Int32, contractorid);
            MyDataBase.AddInParameter(dbCommand, "@Modified_by", DbType.String, modifiedBy);
            MyDataBase.AddInParameter(dbCommand, "@Modified_date", DbType.DateTime, modifiedOn);
            MyDataBase.ExecuteNonQuery(dbCommand);
            
        }
        public string ValidateContractor(int contractorid)
        {

            String sqlCommand = DBConstants.VALIDATE_CONTRACTOR_MASTER;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(sqlCommand);

            MyDataBase.AddInParameter(dbCommand, "@in_Contractor_Id", DbType.Int32, contractorid);

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
        private Contractor GenerateContractorDetailFromDataReader(IDataReader reader)
        {
            Contractor contractor = new Contractor();
            contractor.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            contractor.Name = GetStringFromDataReader(reader, "Contractor_Name");
            contractor.Email = GetStringFromDataReader(reader, "Email");
            contractor.Address = GetStringFromDataReader(reader, "Address");
            contractor.City = GetStringFromDataReader(reader, "City");
            contractor.State = GetStringFromDataReader(reader, "State");
            contractor.PinCode = GetStringFromDataReader(reader, "Pin");
            contractor.Phone = GetStringFromDataReader(reader, "Phone");
            contractor.Mobile = GetStringFromDataReader(reader, "Mobile");
            contractor.Website = GetStringFromDataReader(reader, "Website");
            contractor.Information = new Information();
            contractor.Information.VendorCode = GetStringFromDataReader(reader, "VendorCode");
            contractor.Information.ServiceTaxNumber = GetStringFromDataReader(reader, "Service_Tax_No");
            contractor.Information.PanNumber = GetStringFromDataReader(reader, "PAN_No");
            contractor.Information.TanNumber = GetStringFromDataReader(reader, "TAN_No");
            contractor.Information.FaxNumber = GetStringFromDataReader(reader, "Fax_No");
            contractor.Information.EsiNumber = GetStringFromDataReader(reader, "ESI_No");
            contractor.Information.PfNumber = GetStringFromDataReader(reader, "PF_No");
            contractor.Information.ContactPersonName = GetStringFromDataReader(reader, "ContactPersonName");
            contractor.Information.ContactPersonEmailId = GetStringFromDataReader(reader, "ContactPersonEmailId");
            contractor.Information.ContactPersonMobileNo = GetStringFromDataReader(reader, "ContactPersonMobileNo");
            contractor.Information.ContactPersonPhoneNo = GetStringFromDataReader(reader, "ContactPersonPhoneNo");
            contractor.Description = GetStringFromDataReader(reader, "Description");
            contractor.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            contractor.CreatedDate = GetDateFromReader(reader, "Created_Date");
            contractor.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            contractor.ModifiedDate = GetDateFromReader(reader, "Modified_Date");


            return contractor;
        }
        #endregion
    }
}
