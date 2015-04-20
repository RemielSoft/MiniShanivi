using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace DocumentObjectModel
{
    [Serializable]
    public class Document : Base
    {
        #region Properties ...

        public Int32 Id { get; set; }

        private int _documentId;

        public int DocumentId
        {
            get
            {
                if (_documentId == int.MinValue)
                {
                    return 0;
                }
                return _documentId;
            }
            set
            {
                _documentId = value;
            }
        }

        public String Original_Name { get; set; }

        public String Replaced_Name { get; set; }

        public String Path { get; set; }

        public Int32 LastIndex { get; set; }

        public string FileCompletePath
        {
            get
            {
                return ConfigurationSettings.AppSettings["SiteUrl"].ToString() + this.Path + @"\" + this.Replaced_Name;
            }

        }

        #endregion

    }
}
