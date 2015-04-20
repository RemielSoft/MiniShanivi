using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
   public class InvoiceDom :Base
    {
        public MetaData InvoiceType { get; set; }

        //For Contractor
       public int ContractorInvoiceId { get; set; }

       public DateTime InvoiceDate { get; set; }

       public String InvoiceNumber { get; set; }

       public IssueMaterialDOM IssueMaterial { get; set; }

       public String Remarks { get; set; }

       public Decimal TotalAmount { get; set; }

       //For Supplier

       public int SupplierInvoiceId { get; set; }

       public SupplierRecieveMatarial ReceiveMaterial { get; set; }

       public Decimal InvoicedAmount { get; set; }

       public Decimal LeftAmount { get; set; }

       public Decimal PayableAmount { get; set; }

       public PaymentTerm Payment { get; set; }

       public PaymentDOM PaymentDom { get; set; }

       public string RemarkReject { get; set; }

       public DateTime BillDate { get; set; }

       public String BillNumber { get; set; }

        //billamount for supplier
       public string SupplierOrderNumber { get; set; }

       public DateTime OrderDate { get; set; }

       public string SupplierName{ get; set; }


       public Decimal PendingAmount { get; set; }

       public Decimal ApprovedAmount { get; set; }

       public Decimal ToBillAmount { get; set; }
    }
}
