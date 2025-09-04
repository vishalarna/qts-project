using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblLinkedDocument
    {
        public int Ldid { get; set; }
        public string LdfileName { get; set; }
        public int Ldtype { get; set; }
        public int? Ldreference { get; set; }
        public string Ldcomment { get; set; }
        public DateTime? LddocDate { get; set; }
        public DateTime? LddateStamp { get; set; }

        public virtual TblDocumentType LdtypeNavigation { get; set; }
    }
}
