using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
   public class WorkOrderMappingDOM:Base
    {
        public Int32 CompanyWorkOrderMappingId { get; set; }
        public Int32 CompanyWorkOrderId { get; set; }
        public Decimal Amount { get; set; }
        public String WorkOrderNumber { get; set; }
        public String Area { get; set; }
        public String Location { get; set; }
        public Tax TaxInformation { get; set; }
    }
}

