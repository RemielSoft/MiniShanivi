using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataAccessLayer;
using DocumentObjectModel;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace BusinessAccessLayer
{
    public class DocumentBL : BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private DocumentDAL documentDAL = null;

        int id = 0;
        List<Document> lstDocument = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentBL"/> class.
        /// </summary>
        public DocumentBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            documentDAL = new DocumentDAL(myDataBase);
        }

        #endregion

        #region Public Section

        public Int32 CreateAndReadDocumnetStackId(Document doc)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    id = documentDAL.CreateAndReadDocumnetStackId(doc);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return id;
        }

        #endregion

        #region DOCUMENT CRUD METHODS

        public Boolean CreateDocumentMapping(Document doc)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    documentDAL.CreateDocumentMapping(doc);

                    scope.Complete();
                    return true;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<Document> ReadDocumnetMapping(Int32 documentId)
        {
            lstDocument = new List<Document>();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, base.C_TRANSACTIONSCOPE_ISOLATION_LEVEL))
                {
                    lstDocument = documentDAL.ReadDocumnetMapping(documentId);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return lstDocument;
        }

        public void ResetDocumentMapping(Int32 documentId)
        {
            try
            {
                documentDAL.ResetDocumentMapping(documentId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion

    }
}
