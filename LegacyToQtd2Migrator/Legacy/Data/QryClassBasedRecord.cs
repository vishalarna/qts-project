using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryClassBasedRecord
    {
        public int Clid { get; set; }
        public string Cornum { get; set; }
        public DateTime? Cldate { get; set; }
        public int? Suid { get; set; }
    }
}
