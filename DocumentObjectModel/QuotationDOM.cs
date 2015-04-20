using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class QuotationDOM : Base
    {
        public QuotationDOM()
        {
            Item_Transaction = new ItemTransaction();
            ItemTransaction = new List<ItemTransaction>();
        }
        //For Contractor
        public int ContractorQuotationId { get; set; }

        public int MyContractorQuotationId
        {
            get
            {
                return this.ContractorQuotationId;
            }
        }

        public String ContractQuotationNumber { get; set; }

        public String MyContractQuotationNumber
        {
            get
            {
                return this.ContractQuotationNumber;
            }
        }

        //For Supplier
        public int SupplierQuotationId { get; set; }

        public int MySupplierQuotationId
        {
            get
            {
                return this.SupplierQuotationId;
            }
        }
        public String SupplierQuotationNumber { get; set; }

        public String MySupplierQuotationNumber
        {
            get
            {
                return this.SupplierQuotationNumber;
            }
        }

        public Contractor Contractor { get; set; }

        public int ContractorId { get; set; }

        public String ContractorName { get; set; }

        public Supplier Supplier { get; set; }

        public int SupplierId { get; set; }

        public String SupplierName { get; set; }

        public int ContractId { get; set; }

        public String CompanyWorkOrderNumber { get; set; }

        public String ContractNumber { get; set; }

        public int WorkOrderId { get; set; }

        public String WorkOrderNumber { get; set; }

        public DateTime QuotationDate { get; set; }

        public Int32 UploadDocumentId { get; set; }

        public DateTime OrderDate { get; set; }

        public String DeliveryDescription { get; set; }

        public DateTime ClosingDate { get; set; }

        public ItemTransaction Item_Transaction { get; set; }

        public List<ItemTransaction> ItemTransaction { get; set; }

        public int OldContractorQuotationId { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalNetValue { get; set; }

        public MetaData StatusType { get; set; }

        public string ApprovedRejectedBy { get; set; }

        public DateTime ApprovedRejectedDate { get; set; }

        public Int16 IsGenerated { get; set; }

        public string GeneratedBy { get; set; }

        public DateTime GeneratedDate { get; set; }

        public List<DeliveryScheduleDOM> DeliverySchedule { get; set; }

        public List<PaymentTerm> PaymentTerm { get; set; }

        public List<TermAndCondition> TermAndCondition { get; set; }

        public Decimal Freight { get; set; }
        public Decimal Packaging { get; set; }




        ///  --------------------sundeep------------
        ///  
        public string TaxType { get; set; }

        public string RemarkReject { get; set; }

        public string subjectdescription { get; set; }

        public Decimal DiscountPrice { get; set; }

    }





}
