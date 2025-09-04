using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsIdplatestRecordDetail
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
        public float? Score { get; set; }
        public string ReasonWo { get; set; }
    }
}
