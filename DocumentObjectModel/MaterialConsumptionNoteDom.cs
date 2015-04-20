using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class MaterialConsumptionNoteDom : Base
    {
        //public MaterialConsumptionNoteDom()
        //{
        //    Quotation = new QuotationDOM();
        //}
        //public QuotationDOM Quotation { get; set; }
        //public List<ItemTransaction> Transaction { get; set; }
        //public ItemTransaction itemTransaction { get; set; }
        //public int MaterialConsumptionNoteId { get; set; }
        //public int Material_Consumption_Mapping_Id { get; set; }
        //public int Contractor_Id { get; set; }
        //public int StatusTypeID { get; set; }
        //public int Activity_Id { get; set; }
        //public string Contractor_Name { get; set; }
        //public string Contract_Number { get; set; }
        //public string Activity_Description { get; set; }
        //public string Item_Category { get; set; }
        //public string Item { get; set; }
        //public string Specification { get; set; }
        //public string Brand { get; set; }
        //public string Order_Number_Of_Unit { get; set; }
        //public string Unit_Issued { get; set; }
        //public decimal ActualNumberofUnit { get; set; }
        //public string Remarks { get; set; }
        //public string WorkOrderNumber { get; set; }
        //public MetaData StatusType { get; set; }
        //public decimal ConsumedUnit { get; set; }

        public Int32 MaterialConsumptionId { get; set; }

        public int MaterialConsumptionMappingId { get; set; }
        
        public String MaterialConsumptionNo { get; set; }
        
        public DateTime  MaterialConsumptionDate { get; set; }

        public IssueMaterialDOM IssueMaterial { get; set; }

        public Int32 ReturnMaterialContractorId { get; set; }

        public DateTime ReturnMaterialDate { get; set; }

        public int ReturnMaterialContractorMappingId { get; set; }

        public String ReturnMaterialContractorNo { get; set; }

        public Decimal ConsumeUnit { get; set; }
        

    }
}
