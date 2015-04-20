using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class PaymentTerm : Base
    {
        public int PaymentTermId { get; set; }

        public MetaData PaymentType { get; set; }

        public int NumberOfDays { get; set; }

        public Decimal PercentageValue { get; set; }

        public String PaymentDescription { get; set; }
        
        public MetaData QuotationType { get; set; }


        public int MyId
        {
            get
            {
               return this.PaymentType.Id;
            }
        }
        public String MyName
        {
            get
            {
                return this.PaymentType.Name;
            }
        }
    }
}
