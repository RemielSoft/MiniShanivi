using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class ItemStock : Base
    {

        /// <summary>
        /// 
        /// </summary>
        public ItemStock()
        {
            Brand = new Brand();
            Store = new Store();
        }


        #region Properties ...
        private int _itemStockId;

        public int ItemStockId
        {
            get
            {
                if (_itemStockId == int.MinValue)
                {
                    return 0;
                }
                return _itemStockId;
            }
            set
            {
                _itemStockId = value;
            }
        }

        public Int32 ItemId { get; set; }

        public string ItemName { get; set; }

        public Int32 ItemCategoryId { get; set; }

        public string ItemCategoryName { get; set; }

        public Int32 ItemSpecificationId { get; set; }

        public String ItemSpecificationName { get; set; }

        public string ItemUnit { get; set; }

        public int UnitMeasurementId { get; set; }

        public Int32 QuantityOnhand { get; set; }

        public Int32 MinimumLevel { get; set; }

        public Int32 MaximumLevel { get; set; }

        public Int32 ReorderLevel { get; set; }

        public Int32 MinimumConsumption { get; set; }

        public Int32 MaximumConsumption { get; set; }

        public Int32 NormalConsumption { get; set; }

        public Int32 LeadTime { get; set; }

        public Brand Brand { get; set; }

        public Store Store { get; set; }

        #endregion
    }
}
