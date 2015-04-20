using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    
        [Serializable]
        public class SupplierRecieveMatarial : Base
        {
            //public int SupplierId { get; set; }

           // public String SupplierName { get; set; }

            public Int32 SupplierRecieveMatarialId { get; set; }

            public String SupplierRecieveMaterialNumber { get; set; }

            public String DeliveryChallanNumber { get; set; }

            public DateTime RecieveMaterialDate { get; set; }

            public Document UploadFile { get; set; }

            public QuotationDOM Quotation { get; set; }

            
        }
    
}
