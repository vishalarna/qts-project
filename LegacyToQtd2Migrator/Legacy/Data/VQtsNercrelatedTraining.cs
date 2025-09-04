using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsNercrelatedTraining
    {
        public int Clid { get; set; }
        public int Eid { get; set; }
        public string CompGrade { get; set; }
        public DateTime? SecondDate { get; set; }
        public float? PartialStd { get; set; }
        public float? PartialSim { get; set; }
        public float? PartialTotalCehs { get; set; }
        public float? PartialExtra { get; set; }
        public byte[] Ts { get; set; }
    }
}
