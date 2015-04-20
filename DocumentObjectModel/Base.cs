using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Base
    {
        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public String ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
