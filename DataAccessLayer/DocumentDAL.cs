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
    public class DocumentDAL : BaseDAL
    {
        #region private global variable(s)

        private Database myDataBase;
        Document document = null;
        DbCommand dbCommand = null;
        Int32 stack_Id = 0;

        List<Document> lstDocument = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public DocumentDAL(Database dataBase)
        {
            myDataBase = dataBase;
        }

        #endregion

        #region Public Section

        public Int32 CreateAndReadDocumnetStackId(Document doc)
        {
            
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_AND_READ_DOCUMENT_STACK_ID);
            myDataBase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, doc.CreatedBy);
            myDataBase.AddOutParameter(dbCommand, "@out_Document_Stack_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Document_Stack_Id").ToString(), out stack_Id);

            return stack_Id;
        }


        #endregion

        #region DOCUMENT CRUD METHODS

        public Boolean CreateDocumentMapping(Document doc)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_DOCUMENT_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Document_Id", DbType.Int32, doc.DocumentId);
            myDataBase.AddInParameter(dbCommand, "@in_Original_Name", DbType.String, doc.Original_Name);
            myDataBase.AddInParameter(dbCommand, "@in_Replaced_Name", DbType.String, doc.Replaced_Name);
            myDataBase.AddInParameter(dbCommand, "@in_Path", DbType.String, doc.Path);
            myDataBase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, doc.CreatedBy);
            myDataBase.AddInParameter(dbCommand, "@in_Last_Index", DbType.String, doc.LastIndex);

            if (doc.Id == 0)
            {
                myDataBase.AddInParameter(dbCommand, "@in_Id", DbType.Int32, DBNull.Value);
            }
            else
                myDataBase.AddInParameter(dbCommand, "@in_Id", DbType.Int32, doc.Id);

            myDataBase.ExecuteNonQuery(dbCommand);

            return true;
        }

        public List<Document> ReadDocumnetMapping(Int32 documentId)
        {
            lstDocument = new List<Document>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_DOCUMENT_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Document_Id", DbType.Int32, documentId);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    document = GenerateDocumentMappingReader(reader);
                    lstDocument.Add(document);
                }
            }

            return lstDocument;
        }

        public void ResetDocumentMapping(Int32 documentId)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.RESET_DOCUMENT_MAPPING);
            myDataBase.AddInParameter(dbCommand, "@in_Document_Id", DbType.Int32, documentId);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region Private Section

        private Document GenerateDocumentMappingReader(IDataReader reader)
        {
            document = new Document();
            document.LastIndex = GetIntegerFromDataReader(reader, "Last_Index");
            document.DocumentId = GetIntegerFromDataReader(reader, "Document_Id");
            document.Original_Name = GetStringFromDataReader(reader, "Original_Name");
            document.Replaced_Name = GetStringFromDataReader(reader, "Replaced_Name");
            document.Path = GetStringFromDataReader(reader, "Path");
            document.Id = GetIntegerFromDataReader(reader, "Id");
            return document;
        }

        #endregion
    }
}
