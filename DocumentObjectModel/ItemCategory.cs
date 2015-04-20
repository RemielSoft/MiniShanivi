using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class ItemCategory:Base
    {
        #region Properties ...
        private int _itemCategoryId;

        public int ItemCategoryId
        {
            get
            {
                if (_itemCategoryId == int.MinValue)
                {
                    return 0;
                }
                return _itemCategoryId;
            }
            set
            {
                _itemCategoryId = value;
            }
        }

        public string ItemCategoryName { get; set; }

        public int StartRange { get; set; }

        public int EndRange { get; set; }

        public string Description { get; set; }

        public string Range { get; set; }
        
        #endregion
    }
}
