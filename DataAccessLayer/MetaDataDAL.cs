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
    public class MetaDataDAL:BaseDAL
    {
        #region private global variable(s)

        private Database MyDataBase;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public MetaDataDAL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MetaData> ReadMetaDataAuthority()
        {
            List<MetaData> returnMetaData = new List<MetaData>();
            MetaData metatData = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_AUTHORITY);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metatData = GenerateMetaDataDetailFromDataReader(reader);

                    returnMetaData.Add(metatData);
                }
            }
            return returnMetaData;
        }

        public List<MetaData> ReadMetaDataPaymentMode()
        {
            List<MetaData> returnMetaData = new List<MetaData>();
            MetaData metatData = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_PAYMENT_MODE_TYPE_METADATA);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metatData = GenerateMetaDataFromDataReader(reader);

                    returnMetaData.Add(metatData);
                }
            }
            return returnMetaData;
        }

        public List<MetaData> ReadMetaDataPaymentStatus()
        {
            List<MetaData> returnMetaData = new List<MetaData>();
            MetaData metatData = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_PAYMENT_STATUS);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metatData = GenerateMetaDataFromDataReader(reader);

                    returnMetaData.Add(metatData);
                }
            }
            return returnMetaData;
        }

        /// <summary>
        /// Read Authority Detail by Authority Level
        /// </summary>
        /// <returns></returns>
        public MetaData ReadMetaDataAuthorityByLevel(int Authority_Level)
        {

            MetaData metatData = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_AUTHORITY);
            MyDataBase.AddInParameter(dbCommand, "@Authority_Level", DbType.Int32, Authority_Level);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metatData = GenerateMetaDataDetailFromDataReader(reader);
                }
            }
            return metatData;
        }
        public List<MetaData> ReadMetadataUnitMeasurement()
        {
            List<MetaData> returnMetaData = new List<MetaData>();
            MetaData metadata = null;

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_UNITMEASUREMENT);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metadata = GenerateMetaDataFromDataReader(reader);
                    returnMetaData.Add(metadata);
                }
            }
            return returnMetaData;
        }
        public List<MetaData> ReadMetadataInvoiceType()
        {
            List<MetaData> returnMetaData = new List<MetaData>();
            MetaData metadata = null;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_INVOICE_TYPE);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metadata = GenerateMetaDataInvoiceTypeFromDataReader(reader);
                    returnMetaData.Add(metadata);
                }
            }
            return returnMetaData;
        }
        public List<MetaData> ReadMetadataTaxType()
        {
            List<MetaData> returnMetaData = new List<MetaData>();
            MetaData metadata = null;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_TAX_TYPE);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metadata = GenerateMetaDataInvoiceTypeFromDataReader(reader);
                    returnMetaData.Add(metadata);
                }
            }
            return returnMetaData;
        }

        public List<MetaData> ReadMetaDataApprovalStatus()
        {
            List<MetaData> returnMetaData = new List<MetaData>();
            MetaData metadata = null;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_APPROVAL_STATUS);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    metadata = GenerateMetaDataFromDataReader(reader);
                    returnMetaData.Add(metadata);
                }
            }
            return returnMetaData;
        }
        #region private section
        
        private MetaData GenerateMetaDataDetailFromDataReader(IDataReader reader)
        {
            MetaData metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Code = GetStringFromDataReader(reader, "Code");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }
        private MetaData GenerateMetaDataFromDataReader(IDataReader reader)
        {
            MetaData metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            return metaData;
        }
        private MetaData GenerateMetaDataInvoiceTypeFromDataReader(IDataReader reader)
        {
            MetaData metaData = new MetaData();
            metaData.Id = GetIntegerFromDataReader(reader, "Id");
            metaData.Name = GetStringFromDataReader(reader, "Name");
            metaData.Description = GetStringFromDataReader(reader, "Description");
            return metaData;
        }

        #endregion

       
    }
}
