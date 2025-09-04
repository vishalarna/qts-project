using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryCarryOverTotalCeh
    {
        public int Eid { get; set; }
        public DateTime? CompDate { get; set; }
        public int? TotalReqCehs { get; set; }
        public float TotalCehfinal { get; set; }
        public DateTime? CompDate6 { get; set; }
        public string CompGrade { get; set; }
        public float? PartialTotalCehs { get; set; }
        public bool ActPartialCredits { get; set; }
    }
}
