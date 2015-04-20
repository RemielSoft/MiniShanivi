using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class TermAndCondition : Base
    {
        #region Properties ...

        public int Id { get; set; }

        public int TermsId{get;set;}

        public string Name { get; set; }

        public string Description { get; set; }

        public MetaData QuotationType { get; set; }

        public int TermAndConditionType { get; set; }

        public string TermAndConditionName { get; set; }

        

        #endregion
    }
}
