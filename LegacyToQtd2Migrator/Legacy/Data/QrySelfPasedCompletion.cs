using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QrySelfPasedCompletion
    {
        public int Clid { get; set; }
        public DateTime? SecondDate { get; set; }
        public DateTime? Cldate { get; set; }
        public string CompGrade { get; set; }
        public int Eid { get; set; }
        public int? Corid { get; set; }
        public float? PartialStd { get; set; }
        public float? PartialSim { get; set; }
        public float? PartialTotalCehs { get; set; }
        public float? PartialExtra { get; set; }
        public int? ProctorId { get; set; }
        public float? PartialReg { get; set; }
        public float? PartialReg2 { get; set; }
        public float? PartialOther { get; set; }
        public float? PartialTotal { get; set; }
    }
}
