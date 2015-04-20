using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Information
    {
        public String PanNumber { get; set; }

        public String TanNumber { get; set; }

        public String ServiceTaxNumber { get; set; }

        public String EsiNumber { get; set; }

        public String PfNumber { get; set; }

        public String FaxNumber { get; set; }

        public String VendorCode { get; set; }

        public String ContactPersonName { get; set; }

        public String ContactPersonEmailId { get; set; }

        public String ContactPersonPhoneNo { get; set; }

        public String ContactPersonMobileNo { get; set; }
    }
}
