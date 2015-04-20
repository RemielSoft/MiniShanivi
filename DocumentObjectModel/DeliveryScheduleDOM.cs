using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class DeliveryScheduleDOM : Base
    {
        public int Id { get; set; }

        public String ItemNumber { get; set; }

        public int ActivityDescriptionId { get; set; }

        public String ActivityDescription { get; set; }

        public String Item { get; set; }

        public int ItemId { get; set; }

        public String Specification { get; set; }

        public DateTime DeliveryDate { get; set; }

        public MetaData QuotationType { get; set; }

        //For Supplier 
        public decimal ActualNumberOfUnit { get; set; }

        public int SpecificationId { get; set; }

        public String ItemDescription { get; set; }

        public int ItemDescriptionId { get; set; }

        public decimal ItemQuantity { get; set; }

        public String SpecificationUnit { get; set; }
        //End


    }
}
