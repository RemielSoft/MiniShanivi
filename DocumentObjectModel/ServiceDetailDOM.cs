using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class ServiceDetailDOM : Base
    {
        private Decimal _total;

        private Decimal _quantityLeft;

        public Int32 ServiceDetailId { get; set; }

        public Int32 CompanyWorkOrderId { get; set; }

        public String WorkOrderNumber { get; set; }

        public String ItemNumber { get; set; }

        public String ServiceNumber { get; set; }

        public String ServiceDescription { get; set; }

        public MetaData Unit { get; set; }

        public Decimal Quantity { get; set; }

        public Decimal QuantityIssued { get; set; }

        public Decimal QuantityLeft
        {
            get;
            set;
            //get
            //{
            //    _quantityLeft = (this.Quantity - this.QuantityIssued);
            //    return _quantityLeft;
            //}
            //set
            //{
            //    _quantityLeft = value;
            //}
        }

        public Decimal UnitRate { get; set; }

        public Decimal ApplicableRate { get; set; }

        public Decimal Total
        {
            get
            {
                _total = this.QuantityLeft * this.ApplicableRate;
                return _total;
            }
            set
            {
                _total = value;
            }
        }
    }
}
