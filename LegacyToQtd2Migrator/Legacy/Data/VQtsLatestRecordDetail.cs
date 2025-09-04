using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsLatestRecordDetail
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
        public DateTime? SecondDate { get; set; }
        public float? PartialStd { get; set; }
        public float? PartialSim { get; set; }
        public float? PartialTotalCehs { get; set; }
        public float? PartialExtra { get; set; }
        public float? PartialReg { get; set; }
        public float? PartialReg2 { get; set; }
        public float? PartialOther { get; set; }
        public float? PartialTotal { get; set; }
        public float? Score { get; set; }
        public string ReasonWo { get; set; }
    }
}
