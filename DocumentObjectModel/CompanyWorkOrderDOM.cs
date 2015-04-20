using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class CompanyWorkOrderDOM:Base
    {
        public String CompanyWorkOrderNumber { get; set; }
        public Int32 CompanyWorkOrderId { get; set; }
        public DateTime ContractDate { get; set; }
        public String ContractNumber { get; set; }
        public String WorkOrderDescription { get; set; }
        public Decimal  TotalNetValue { get; set; }
        public MetaData StatusType { get; set; }
        public String ApprovedRejectedBy { get; set; }
        public DateTime  ApprovedRejectedDate { get; set; }
        public Int32 IsGenerated { get; set; }
        public String GeneratedBy { get; set; }
        public DateTime GeneratedDate { get; set; }
        public List<WorkOrderMappingDOM> lstWorkOrderMapping { get; set; }
        public List<BankGuaranteeDOM> lstBankGuarantee { get; set; }
        public int UploadDocumentId { get; set; }
        public String RemarkReject { get; set; }
    }
}

