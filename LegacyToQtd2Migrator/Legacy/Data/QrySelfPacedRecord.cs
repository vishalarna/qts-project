using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QrySelfPacedRecord
    {
        public int Corid { get; set; }
        public string Cornum { get; set; }
        public int? Suid { get; set; }
        public DateTime? Cldate { get; set; }
    }
}
