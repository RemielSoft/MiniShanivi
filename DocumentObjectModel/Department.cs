using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Department:Base
    {
        #region Properties ...

        private int _departmentId;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int DepartmentId
        {
            get
            {
                if (_departmentId == int.MinValue)
                {
                    return 0;
                }
                return _departmentId;
            }
            set
            {
                _departmentId = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        
        #endregion
    }
}
