using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblDocumentType
    {
        public TblDocumentType()
        {
            TblLinkedDocuments = new HashSet<TblLinkedDocument>();
        }

        public int Dtid { get; set; }
        public string Dtdesc { get; set; }
        public int DtsortOrder { get; set; }

        public virtual ICollection<TblLinkedDocument> TblLinkedDocuments { get; set; }
    }
}
