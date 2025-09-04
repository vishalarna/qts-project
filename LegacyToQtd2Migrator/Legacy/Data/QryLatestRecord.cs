using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryLatestRecord
    {
        public int Eid { get; set; }
        public int? Corid { get; set; }
        public int? Clyear { get; set; }
        public DateTime? CompDate { get; set; }
        public DateTime? SecondDate { get; set; }
    }
}
