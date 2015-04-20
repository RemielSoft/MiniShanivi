using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Store : Base
    {
        /// <summary>
        ///  Indicates Store Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Indicates the Name of the Store
        /// </summary>
        public string StoreName { get; set; }

    }
}
