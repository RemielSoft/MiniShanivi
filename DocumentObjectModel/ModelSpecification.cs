using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class ModelSpecification : Base
    {
        public ModelSpecification()
        {
            Brand = new Brand();
            Store = new Store();
        }

        #region Properties ...

        private int _modelSpecificationId;

        public int ModelSpecificationId
        {
            get
            {
                if (_modelSpecificationId == int.MinValue)
                {
                    return 0;
                }
                return _modelSpecificationId;
            }
            set
            {
                _modelSpecificationId = value;
            }
        }

        public String ModelCode { get; set; }

        public string ModelSpecificationName { get; set; }

        public String Description { get; set; }

        public Brand Brand { get; set; }

        public Store Store { get; set; } 

        public MetaData UnitMeasurement { get; set; }

        public Int32 CategoryUsageValue { get; set; }

        public ItemCategory Category { get; set; }

        #endregion
    }
}
