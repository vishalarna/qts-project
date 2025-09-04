using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblDocumentLink
    {
        public int Ldlid { get; set; }
        public int LinkedDocId { get; set; }
        public int TypeId { get; set; }
        public int DocTypeId { get; set; }
        public int LinkItemId { get; set; }
        public string Comment { get; set; }
    }
}
