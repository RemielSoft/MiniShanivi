using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataAccessLayer;
using DocumentObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace BusinessAccessLayer
{
    public class MetaDataBL:BaseBL
    {
        #region private global variable(s)

        private Database myDataBase;
        private MetaDataDAL metaDataDAL = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public MetaDataBL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            metaDataDAL = new MetaDataDAL(myDataBase);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MetaData> ReadMetaDataAuthority()
        {
            List<MetaData> lst = new List<MetaData>();
            try
            {
                lst = metaDataDAL.ReadMetaDataAuthority();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);

            }
            return lst;
        }
        /// <summary>
        /// Read Data From Unit Measurement
        /// </summary>
        /// <returns></returns>
        public List<MetaData> ReadMetaDataUnitMeasurement()
        {
            List<MetaData> lst = new List<MetaData>();
            try
            {
                lst = metaDataDAL.ReadMetadataUnitMeasurement();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);

            }
            return lst;
        }
        public List<MetaData> ReadMetaDataInvoiceType()
        {
            List<MetaData> lst = new List<MetaData>();
            try
            {
                lst = metaDataDAL.ReadMetadataInvoiceType();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);

            }
            return lst;
        }
        public List<MetaData> ReadMetaDataTaxType()
        {
            List<MetaData> lst = new List<MetaData>();
            try
            {
                lst = metaDataDAL.ReadMetadataTaxType();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);

            }
            return lst;
        }
        public List<MetaData> ReadMetaDataPaymentMode()
        {
            List<MetaData> lst = new List<MetaData>();
            try
            {
                lst = metaDataDAL.ReadMetaDataPaymentMode();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);

            }
            return lst;
        }

        public List<MetaData> ReadMetaDataPaymentStatus()
        {
            List<MetaData> lst = new List<MetaData>();
            try
            {
                lst = metaDataDAL.ReadMetaDataPaymentStatus();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);

            }
            return lst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorityLevel"></param>
        /// <returns></returns>
        public MetaData ReadMetaDataAuthorityByLevel(int authorityLevel)
        {
            MetaData MD = new MetaData();
            try
            {
                MD = metaDataDAL.ReadMetaDataAuthorityByLevel(authorityLevel);
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);
            }
            return MD;
        }

        public List<MetaData> ReadMetadataApprovalStatus()
        {
            List<MetaData> lst = new List<MetaData>();
            try
            {
                lst = metaDataDAL.ReadMetaDataApprovalStatus();
            }
            catch (Exception exp)
            {
                Logger.Write(exp.Message);

            }
            return lst;
        }
    }
}
