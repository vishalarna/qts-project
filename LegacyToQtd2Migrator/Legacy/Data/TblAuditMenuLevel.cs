using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblAuditMenuLevel
    {
        public int Amlid { get; set; }
        public int Amid { get; set; }
        public string Amldesc { get; set; }
        public int SortOrder { get; set; }
    }
}
