using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwStudent
    {
        public int Eid { get; set; }
        public string Employee { get; set; }
        public string CompGrade { get; set; }
        public int Clid { get; set; }
        public float? PartialStd { get; set; }
        public float? PartialSim { get; set; }
        public float? PartialTotalCehs { get; set; }
        public int? Pid { get; set; }
        public float? PartialExtra { get; set; }
        public float? PartialReg { get; set; }
        public float? PartialReg2 { get; set; }
        public float? PartialOther { get; set; }
        public float? PartialTotal { get; set; }
    }
}
