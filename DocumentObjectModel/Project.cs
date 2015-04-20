using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Project:Base
    {
        #region Properties ...
        private int _projectId;

        public int ProjectId
        {
            get
            {
                if (_projectId == int.MinValue)
                {
                    return 0;
                }
                return _projectId;
            }
            set
            {
                _projectId = value;
            }
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Description { get; set; }

        #endregion
    }
}
