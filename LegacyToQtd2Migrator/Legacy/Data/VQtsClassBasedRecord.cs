using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsClassBasedRecord
    {
        public int Clid { get; set; }
        public int Corid { get; set; }
        public string Cornum { get; set; }
        public string Cordesc { get; set; }
        public DateTime? Cldate { get; set; }
        public int? Suid { get; set; }
    }
}
