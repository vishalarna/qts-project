using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsLatestCompletedGrade
    {
        public int Eid { get; set; }
        public int? Corid { get; set; }
        public DateTime? LastCompleted { get; set; }
        public string CompGrade { get; set; }
        public string Partial { get; set; }
        public float? Score { get; set; }
        public string ReasonWo { get; set; }
    }
}
