using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class ItemTransaction : Base
    {
        public ItemTransaction()
        {
            Item = new Item();
        }
        public DeliveryScheduleDOM DeliverySchedule { get; set; }

        public Item Item { get; set; }

        public ServiceDetailDOM Service_Detail { get; set; }

        public List<ServiceDetailDOM> ServiceDetail { get; set; }

        public Decimal AdvanceValue { get; set; }

        //has to Quantity
        public decimal NumberOfUnit { get; set; }

        //has to QuantityRequired
        public decimal ItemRequired { get; set; }

        //QuantityDemanded
        public decimal UnitDemanded { get; set; }

        //PerQuantityCost
        public decimal PerUnitCost { get; set; }

        //PerQuantityDiscount
        public decimal PerUnitDiscount { get; set; }

        public Decimal TotalAmount { get; set; }

        //QuantityIssued
        public decimal UnitIssued { get; set; }

        //QuantityLeft
        public decimal UnitLeft { get; set; }

        //BilledQuantity
        public decimal BilledUnit { get; set; }

        //LostUnit
        public decimal LostUnit { get; set; }

        //QuantityForBilled
        public decimal UnitForBilled { get; set; }

        public Decimal QuantityReceived { get; set; }

        public Decimal QuantityReturned { get; set; }

        public MetaData MetaProperty { get; set; }

        public Tax TaxInformation { get; set; }

        public string Remark { get; set; }

        public decimal ActualNumberofUnit { get; set; }

        public String PreviousUnitRate { get; set; }

        public decimal ConsumedUnit { get; set; }

        public decimal ItemReceivedQuality { get; set; }

        public decimal ItemReceivedInvoice { get; set; }

        public decimal Discount_Rates { get; set; }







    }
}
