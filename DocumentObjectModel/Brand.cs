using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Brand : Base
    {
        /// <summary>
        ///  Indicates Brand Id
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Indicates the Name of the Brand
        /// </summary>
        public string BrandName { get; set; }

    }
}
