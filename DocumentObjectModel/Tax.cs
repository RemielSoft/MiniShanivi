using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
   public class Tax:Base
    {
       
       private int _TaxId;
       public int TaxId 
       { 
           get
            {
                if (_TaxId == int.MinValue)
                {
                    return 0;
                }
                return _TaxId;
             }    
           set
            {
                _TaxId = value;
            }
       }
       public decimal  ExciseDuty { get; set; }
       public decimal ServiceTax { get; set; }
       public decimal VAT { get; set; }
       public decimal CSTWithCForm { get; set; }
       public decimal CSTWithoutCForm { get; set; }
       public decimal Freight { get; set; }
       public decimal Packaging { get; set; }
       public decimal TotalTax { get; set; }
       public decimal TotalDiscount { get; set; }
       public decimal  TotalNetValue { get; set; }
       public MetaData DiscountMode { get; set; }
       public decimal PercentageQuty { get; set; }
    }
}
