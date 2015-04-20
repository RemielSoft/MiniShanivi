using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class IssueMaterialDOM:Base
    {
        public int IssueMaterialId { get; set; } 
      
        public string IssueMaterialNumber { get; set; }
        
        public DateTime IssueMaterialDate { get; set; }

        public IssueDemandVoucherDOM DemandVoucher { get; set; }
    }
}
