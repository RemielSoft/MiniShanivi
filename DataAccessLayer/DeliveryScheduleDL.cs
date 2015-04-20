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
    public class DeliveryScheduleDL : BaseDAL
    {
        #region Global Variables

        private Database myDataBase = null;
        DbCommand dbCommand = null;

        int deliveryScheduleId = 0;
        DeliveryScheduleDOM deliverySchedule = null;

        List<DeliveryScheduleDOM> lstDeliverySchedule = null;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryScheduleDL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public DeliveryScheduleDL(Database dataBase)
        {
            myDataBase = dataBase;
        }

        #endregion

        #region CURD Methods

        public int CreateDeliverySchedule(DeliveryScheduleDOM deliverySchedule, Int32 quotationID)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.CREATE_DELIVERY_SCHEDULE);

            if (deliverySchedule.Id == 0)
                myDataBase.AddInParameter(dbCommand, "@in_Delivery_Sechedule_Id", DbType.Int32, DBNull.Value);
            else
                myDataBase.AddInParameter(dbCommand, "@in_Delivery_Sechedule_Id", DbType.Int32, deliverySchedule.Id);


            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationID);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int16, deliverySchedule.QuotationType.Id);
            myDataBase.AddInParameter(dbCommand, "@in_Activity_Description_Id", DbType.Int32, deliverySchedule.ActivityDescriptionId);
            //myDataBase.AddInParameter(dbCommand, "@in_Item_Number", DbType.String, deliverySchedule.ItemNumber);
            myDataBase.AddInParameter(dbCommand, "@in_Item_Description_Id", DbType.Int32, deliverySchedule.ItemDescriptionId);
            // myDataBase.AddInParameter(dbCommand, "@in_Item_Description", DbType.String, deliverySchedule.ItemDescription);
            myDataBase.AddInParameter(dbCommand, "@in_Item_Quantity", DbType.Decimal, deliverySchedule.ItemQuantity);
            myDataBase.AddInParameter(dbCommand, "@in_Schedule_Date", DbType.DateTime, deliverySchedule.DeliveryDate);
            myDataBase.AddInParameter(dbCommand, "@in_Created_By", DbType.String, deliverySchedule.CreatedBy);
            myDataBase.AddOutParameter(dbCommand, "@out_Delivery_Sechedule_Id", DbType.Int32, 10);
            myDataBase.ExecuteNonQuery(dbCommand);
            int.TryParse(myDataBase.GetParameterValue(dbCommand, "@out_Delivery_Sechedule_Id").ToString(), out deliveryScheduleId);
            return deliveryScheduleId;
        }

        public List<DeliveryScheduleDOM> ReadDeliveryScheduleByQuotationID(int quotationID, int quotationType)
        {
            lstDeliverySchedule = new List<DeliveryScheduleDOM>();

            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.READ_DELIVERY_SCHEDULE);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationID);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int16, quotationType);
            using (IDataReader reader = myDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    deliverySchedule = GenerateDeliveryScheduleFromDataReader(reader);
                    lstDeliverySchedule.Add(deliverySchedule);
                }
            }

            return lstDeliverySchedule;
        }

        public void DeleteDeliverySchedule(Int32 quotationId, Int32 quotationType, String modifiedBy)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.DELETE_DELIVERY_SCHEDULE);
            myDataBase.AddInParameter(dbCommand, "@in_Delivery_Sechedule_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int16, quotationType);
            myDataBase.AddInParameter(dbCommand, "@in_Modified_By", DbType.String, modifiedBy);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        public void ResetDeliverySchedule(Int32 quotationId, Int32 quotationType)
        {
            dbCommand = myDataBase.GetStoredProcCommand(DBConstants.RESET_DELIVERY_SCHEDULE);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Id", DbType.Int32, quotationId);
            myDataBase.AddInParameter(dbCommand, "@in_Quotation_Type", DbType.Int32, quotationType);
            myDataBase.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region Private Section

        private DeliveryScheduleDOM GenerateDeliveryScheduleFromDataReader(IDataReader reader)
        {
            deliverySchedule = new DeliveryScheduleDOM();

            deliverySchedule.Id = GetIntegerFromDataReader(reader, "Delivery_Sechedule_Id");

            deliverySchedule.QuotationType = new MetaData();
            deliverySchedule.QuotationType.Id = GetIntegerFromDataReader(reader, "Quotation_Type_Id");
            deliverySchedule.QuotationType.Name = GetStringFromDataReader(reader, "Quotation_Type");

            deliverySchedule.ItemDescriptionId = GetIntegerFromDataReader(reader, "Item_Description_Id");
            
            deliverySchedule.ItemDescription = GetStringFromDataReader(reader, "Activity_Description");

            
            //if (Convert.ToInt32(QuotationType.Contractor) == deliverySchedule.QuotationType.Id)
            //{
            //    deliverySchedule.ActivityDescription = GetStringFromDataReader(reader, "Service_Description");
            //}

            deliverySchedule.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            deliverySchedule.Item = GetStringFromDataReader(reader, "Item_Name");

            deliverySchedule.SpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Id");
            deliverySchedule.Specification = GetStringFromDataReader(reader, "Item_Model_Name");

            //deliverySchedule.ItemNumber=GetStringFromDataReader(reader, "Item_Number");
            //deliverySchedule.SpecificationId = GetIntegerFromDataReader(reader, "Item_Id");
            deliverySchedule.ItemQuantity = GetDecimalFromDataReader(reader, "Item_Quantity");
            //deliverySchedule.ItemDescription = GetStringFromDataReader(reader, "Item_Description");
            deliverySchedule.SpecificationUnit = GetStringFromDataReader(reader, "Measurement_Unit_Name");
            deliverySchedule.DeliveryDate = GetDateFromReader(reader, "Schedule_Date");
            deliverySchedule.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            deliverySchedule.CreatedDate = GetDateFromReader(reader, "Created_Date");
            deliverySchedule.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            deliverySchedule.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            return deliverySchedule;
        }

        #endregion
    }
}
