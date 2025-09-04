using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryLatestRecordDetailesNow
    {
        public int Eid { get; set; }
        public int? Corid { get; set; }
        public int? Clyear { get; set; }
        public DateTime? CompDate { get; set; }
        public int Clid { get; set; }
        public string Lcdesc { get; set; }
        public string Inname { get; set; }
        public string CompGrade { get; set; }
        public DateTime? CehAppDate { get; set; }
    }
}
