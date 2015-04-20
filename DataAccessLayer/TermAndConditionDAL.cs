using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DocumentObjectModel;
using System.Data;
using System.Data.Common;

namespace DataAccessLayer
{
    public class TermAndConditionDAL : BaseDAL
    {
        #region private global variable(s)

        private Database myDataBase;
        DbCommand dbCommand = null;
        int id = 0;

        TermAndCondition termAndCondition = null;
        List<TermAndCondition> lstTermAndCondition = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public TermAndConditionDAL(Database dataBase)
        {
            myDataBase = dataBase;
        }

        #endregion

        #region Department CRUD Methods

        /// <summary>
        /// Creates the department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        public int CreateTermsAndConditions(TermAndCondition termAndCondition)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_TERMS_AND_CONDITION_MASTER);

            myDataBase.AddInParameter(dbCommand, "@Term_Condition_Type", DbType.String, termAndCondition.TermAndConditionType);
            myDataBase.AddInParameter(dbCommand, "@Term_Name", DbType.String, termAndCondition.Name);
            myDataBase.AddInParameter(dbCommand, "@Description", DbType.String, termAndCondition.Description);
            myDataBase.AddInParameter(dbCommand, "@Created_By", DbType.String, termAndCondition.CreatedBy);

            myDataBase.AddOutParameter(dbCommand, "@out_Term_Id", DbType.Int32, 10);

            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Term_Id").ToString(), out id);

            return id;
        }

        //
        public int CreateTermAndCondition(TermAndCondition termAndCondition, int quotationID)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_TERM_AND_CONDITION);

            if (termAndCondition.Id == 0)
                myDataBase.AddInParameter(dbCommand, "@in_Terms_And_Conditions_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "@in_Terms_And_Conditions_Id", DbType.Int32, termAndCondition.Id);

            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationID);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int32, termAndCondition.QuotationType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Term_Id", DbType.Int32, termAndCondition.TermsId);
            myDataBase.AddInParameter(dbCommand, "@in_Term_Name", DbType.String, termAndCondition.Name);
            myDataBase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, termAndCondition.CreatedBy);

            myDataBase.AddOutParameter(dbCommand, "@out_Term_Condition_Id", DbType.Int32, 10);

            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Term_Condition_Id").ToString(), out id);

            return id;

        }
        /// <summary>
        /// Updates the department.
        /// </summary>
        /// <param name="department">The department.</param>
        public int UpdateTermsAndConditions(TermAndCondition termAndCondition)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.UPDATE_TERMS_AND_CONDITION_MASTER);

            myDataBase.AddInParameter(dbCommand, "@Term_Id", DbType.Int32, termAndCondition.TermsId);
            myDataBase.AddInParameter(dbCommand, "@Term_Condition_Type", DbType.Int32, termAndCondition.TermAndConditionType);
            myDataBase.AddInParameter(dbCommand, "@Term_Name", DbType.String, termAndCondition.Name);
            myDataBase.AddInParameter(dbCommand, "@Description", DbType.String, termAndCondition.Description);
            myDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, termAndCondition.ModifiedBy);

            myDataBase.AddOutParameter(dbCommand, "@out_Term_Id", DbType.Int32, 10);

            myDataBase.ExecuteNonQuery(dbCommand);

            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Term_Id").ToString(), out id);

            return id;
        }

        /// <summary>
        /// Deletes the department.
        /// </summary>
        /// <param name="departmentId">The department id.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <param name="modifiedOn">The modified on.</param>
        public void DeleteTermsAndConditions(int termsId, string modifiedBy, DateTime modifiedOn)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_TERMS_AND_CONDITION_MASTER);

            myDataBase.AddInParameter(dbCommand, "@Term_Id", DbType.Int32, termsId);
            myDataBase.AddInParameter(dbCommand, "@Modified_By", DbType.String, modifiedBy);

            myDataBase.ExecuteNonQuery(dbCommand);

        }

        public void DeleteTermAndCondition(Int32 quotationId, Int16 quotationType, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_TERM_AND_CONDITION);
            myDataBase.AddInParameter(dbCommand, "@in_Terms_And_Conditions_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int16, quotationType);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Reads the departments.
        /// </summary>
        /// <returns></returns>
        public List<TermAndCondition> ReadTermsAndConditions(int? termsId, Int32? termConditionType)
        {
            lstTermAndCondition = new List<TermAndCondition>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_TERMS_AND_CONDITION_MASTER);
            myDataBase.AddInParameter(dbCommand, "@Term_Id", DbType.Int32, termsId);
            myDataBase.AddInParameter(dbCommand, "in_term_condition_type", DbType.Int32, termConditionType);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    termAndCondition = GenerateDepartmentFromDataReader(reader);
                    lstTermAndCondition.Add(termAndCondition);
                }
            }
            return lstTermAndCondition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotationID"></param>
        /// <param name="quotationType"></param>
        /// <returns></returns>
        public List<TermAndCondition> ReadTermAndConditionByQuotationID(int quotationID, Int16 quotationType)
        {
            lstTermAndCondition = new List<TermAndCondition>();
            TermAndCondition tac = null;
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_TERM_AND_CONDITION);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationID);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int16, quotationType); ;
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    tac = GenerateTermAndConditionFromDataReader(reader);
                    lstTermAndCondition.Add(tac);
                }
            }

            return lstTermAndCondition;
        }

        public void ResetTermAndConditions(int quotationID,Int32 quotationType)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.RESET_TERM_AND_CONDITION);

            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationID);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int32, quotationType);


            myDataBase.ExecuteNonQuery(dbCommand);
        }


        #endregion

        #region Private Section

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        private TermAndCondition GenerateDepartmentFromDataReader(IDataReader reader)
        {
            termAndCondition = new TermAndCondition();
            termAndCondition.TermsId = GetIntegerFromDataReader(reader, "Term_Id");
            termAndCondition.TermAndConditionType = GetIntegerFromDataReader(reader, "Term_Condition_Type");
            termAndCondition.Name = GetStringFromDataReader(reader, "Term_Name");
            termAndCondition.Description = GetStringFromDataReader(reader, "Description");

            termAndCondition.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            termAndCondition.CreatedDate = GetDateFromReader(reader, "Created_Date");
            termAndCondition.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            termAndCondition.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            return termAndCondition;
        }

        private TermAndCondition GenerateTermAndConditionFromDataReader(IDataReader reader)
        {
            termAndCondition = new TermAndCondition();
            termAndCondition.QuotationType = new MetaData();

            termAndCondition.Id = GetIntegerFromDataReader(reader, "Terms_And_Conditions_Id");
            termAndCondition.QuotationType.Id = GetIntegerFromDataReader(reader, "Quotation_Type");
            termAndCondition.QuotationType.Name = GetStringFromDataReader(reader, "Name");
            termAndCondition.TermsId = GetIntegerFromDataReader(reader, "Term_Id");
            termAndCondition.Name = GetStringFromDataReader(reader, "Term_Name");
            termAndCondition.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            termAndCondition.CreatedDate = GetDateFromReader(reader, "Created_Date");
            termAndCondition.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            termAndCondition.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            return termAndCondition;
        }

        #endregion
    }
}
