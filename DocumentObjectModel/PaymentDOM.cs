using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DocumentObjectModel
{
    [Serializable]
    public class PaymentDOM:Base
    {
        public Int32 PaymentId { get; set; }
        public String InvoiceNumber { get; set; }
        public int InvoiceId { get; set; }
        public MetaData InvoiceType { get; set; }
        public DateTime InvoiceDate { get; set; }
        public String InvoiceRemarks { get; set; }
        public Int32 ContractorSupplierId { get; set; }
        public String ContractorSupplierName { get; set; }
        public String QuotationNumber { get; set; }
        public String  ContractNumber { get; set; }
        public String WorkOrderNumber { get; set; }
        public String Remark { get; set; }
        public DateTime PaymentDate { get; set; }
        public Int32 UploadedDocument { get; set; }
        public String FileName { get; set; }
        public HttpPostedFile[] HPF { get; set; }
        public MetaData PaymentModeType { get; set; }
        public String OtherPayment { get; set; }
        public Decimal PaymentAmount { get; set; }
        public String BankName { get; set; }
        public String ReferenceNumber { get; set; }
        public MetaData ApprovalStatusType { get; set; }
        public MetaData Paymentstatus { get; set; }
        public String ApprovedRegectedBy { get; set; }
        public DateTime ApprovedRejectedDate { get; set; }
        public Int16 IsGenerated { get; set; }
        public String GeneratedBy { get; set; }
        public DateTime GeneratedDate { get; set; }
        public String RemarkReject { get; set; }
        public decimal TDS { get; set; }
        public decimal TDSWithPayment { get; set; }
        public String BillNumber { get; set; }
        public decimal PaidAmount { get; set; }
        public MetaData PaymentStatus { get; set; }
        public decimal PaymentLeftAmount { get; set; }
    }
}
