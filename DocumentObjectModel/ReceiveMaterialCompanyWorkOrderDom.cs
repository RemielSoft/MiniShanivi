using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
     [Serializable]
    public class ReceiveMaterialCompanyWorkOrderDom : Base
    {   
         public Int32 ContractReceiveMaterialId { get; set; }

         public string ContractReceiveMaterialNumber { get; set; }
         
         public DateTime Receive_Date { get; set; }

         public string Description { get; set; }

         public QuotationDOM Quotation { get; set; }

         public CompanyWorkOrderDOM CompanyWorkOrder { get; set; }

         public Document UploadFile { get; set; }

    }   
}
