using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryFullRecordByyear
    {
        public int Eid { get; set; }
        public int? Corid { get; set; }
        public int Clid { get; set; }
        public DateTime? Cldate { get; set; }
        public int? Clyear { get; set; }
        public int? Inid { get; set; }
        public string Inname { get; set; }
        public int? Lcid { get; set; }
        public string Lcdesc { get; set; }
        public string CompGrade { get; set; }
    }
}
