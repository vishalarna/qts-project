using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwImportedDocument
    {
        public int Ldlid { get; set; }
        public int LinkedDocId { get; set; }
        public int TypeId { get; set; }
        public int DocTypeId { get; set; }
        public int LinkItemId { get; set; }
        public string Comment { get; set; }
        public string TypeDesc { get; set; }
        public int Dtid { get; set; }
        public string Dtdesc { get; set; }
        public int DtsortOrder { get; set; }
        public int Ldid { get; set; }
        public string LdfileName { get; set; }
        public int Ldtype { get; set; }
        public int? Ldreference { get; set; }
        public string Ldcomment { get; set; }
        public DateTime? LddocDate { get; set; }
        public DateTime? LddateStamp { get; set; }
    }
}
