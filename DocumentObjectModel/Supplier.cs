using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Supplier:Base
    {
        #region Properties ...
        private int _SupplierId;

        public Supplier()
        {
            Information = new Information();
        }

        public int SupplierId
        {
            get
            {
                if (_SupplierId == int.MinValue)
                {
                    return 0;
                }
                return _SupplierId;
            }
            set
            {
                _SupplierId = value;
            }
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public String PinCode { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public Information Information { get; set; }

        #endregion
    }
}
