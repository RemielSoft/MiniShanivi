using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DocumentObjectModel
{
    [Serializable]
    public class BankGuaranteeDOM:Base
    {
        public Int32 BankGuaranteeId { get; set; }
        public Int32 CompanyWorkOrderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Decimal Amount { get; set; }
        public String BankName { get; set; }
        public String UploadedDocument { get; set; }
        public String UploadedDocumentPath { get; set; }
        public String  FileName { get; set; }
        public HttpPostedFile[] HPF { get; set; }
        public String Remarks { get; set; }
    }
}
