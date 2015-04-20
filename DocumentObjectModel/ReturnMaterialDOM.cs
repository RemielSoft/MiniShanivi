using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class ReturnMaterialDOM : Base
    {
        public int RetutrnMaterialId { get; set; }

        public string ReturnMaterialNumber { get; set; }

        public DateTime ReturnMaterialDate { get; set; }

        public SupplierRecieveMatarial RecieveMatarial { get; set; }

        public MaterialConsumptionNoteDom ConsumptionNote { get; set; }
    }
}
