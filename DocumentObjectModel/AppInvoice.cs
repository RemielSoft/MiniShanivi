using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class AppInvoice
    {
        //For Contractor
        public int ContractorInvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public String InvoiceNumber { get; set; }

        public IssueMaterialDOM IssueMaterial { get; set; }

        public String Remarks { get; set; }

        public Decimal TotalAmount { get; set; }

        //For Supplier

        public int SupplierInvoiceId { get; set; }
        public string PONumber { get; set; }
        public List<ItemTransaction> ItemDetails { get; set; } 

        public Decimal InvoicedAmount { get; set; }

        public Decimal LeftAmount { get; set; }

        public Decimal PayableAmount { get; set; }

        public PaymentTerm Payment { get; set; }

        public decimal FraightCharges { get; set; }

        public decimal PackagingCharges { get; set; }

        public Supplier SupplierDetail
        {
            get;
            set;
        }
    }
}
