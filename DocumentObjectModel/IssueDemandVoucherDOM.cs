using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class IssueDemandVoucherDOM:Base
    {
        public Int32 IssueDemandVoucherId { get; set; }

        public Int32 IssueDemandVoucherMappingId { get; set; }

        public String IssueDemandVoucherNumber { get; set; }

        public DateTime MaterialDemandDate { get; set; }

        public QuotationDOM Quotation { get; set; }

        public ItemTransaction Transaction { get; set; }

        public String Remarks { get; set; }
    }
}
