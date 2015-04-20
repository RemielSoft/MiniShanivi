using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Item : Base
    {

        public Item()
        {
            ModelSpecification = new ModelSpecification();
            ModelSpecifications = new List<ModelSpecification>();
        }
        #region Properties ...
        private int _itemId;
        private String _activity = String.Empty;

        public int ItemId
        {
            get
            {
                if (_itemId == int.MinValue)
                {
                    return 0;
                }
                return _itemId;
            }
            set
            {
                _itemId = value;
            }
        }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public string ItemDescription { get; set; }

        public int BrandId { get; set; }


        public String FinalActivityDescription
        {
            get
            {
                if (_activity == String.Empty)
                    return this.ItemName + " - " + this.ModelSpecification.ModelSpecificationName;
                else
                    return _activity;
            }
            set
            {
                _activity = value;
            }
        }

        //Removed As Moved to ModelSpecfication 
        //public MetaData UnitMeasurement { get; set; }

        public ModelSpecification ModelSpecification { get; set; }

        public List<ModelSpecification> ModelSpecifications { get; set; }

        #endregion
    }
}
