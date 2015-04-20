using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Contractor : Base
    {

        public Contractor()
        {
            Information = new Information();
        }
        #region Properties ...
        private int _ContractorId;

        public int ContractorId
        {
            get
            {
                if (_ContractorId == int.MinValue)
                {
                    return 0;
                }
                return _ContractorId;
            }
            set
            {
                _ContractorId = value;
            }
        }

        public string Name { get; set; }

        public string Address { get; set; }

        //public string MiddleName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public String PinCode { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public String Description { get; set; }

        public Information Information { get; set; }

        #endregion
    }
}
