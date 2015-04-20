using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class ValidateIssueItem
    {
        /// <summary>
        /// Item Id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Item Specification Id
        /// </summary>
        public int ItemSpecificationId { get; set; }

        /// <summary>
        /// Item Specification
        /// </summary>
        public string ItemSpecification { get; set; }


        public int ItemCategoryId { get; set; }

        public string ItemCategoryName { get; set; }

        public string UnitMeasurement { get; set; }


        /// <summary>
        /// Store Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Brand Id
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Brand Name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Issued Quantity
        /// </summary>
        public decimal IssuedQuantity { get; set; }

        /// <summary>
        /// Available Quantity
        /// </summary>
        public decimal AvailableQuantity { get; set; }

        /// <summary>
        /// Quantity Demand
        /// </summary>
        public decimal QuantityDemand { get; set; }

        /// <summary>
        /// Supplier Purchase Order Id
        /// </summary>
        public int SupplierPurchaseOrderId { get; set; }

        /// <summary>
        ///  Activity Description
        /// </summary>
        public string ActivityDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal PerUnitCost { get; set; }

        public decimal RecievedQuantity { get; set; }


    }
}
