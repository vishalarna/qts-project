using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryExchangeOfElectronicRecordsAdvanced
    {
        public string Cornum { get; set; }
        public DateTime? Cldate { get; set; }
        public string Inname { get; set; }
        public string Lcdesc { get; set; }
        public string Enum { get; set; }
        public string CompGrade { get; set; }
        public float? PartialStd { get; set; }
        public float? PartialSim { get; set; }
        public float? PartialTotalCehs { get; set; }
        public float? PartialExtra { get; set; }
        public string NerccertNum { get; set; }
        public int? Oid { get; set; }
        public int Clid { get; set; }
        public int? Corid { get; set; }
    }
}
