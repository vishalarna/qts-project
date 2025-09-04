using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTestRecallHistory
    {
        public int RecallId { get; set; }
        public int Etid { get; set; }
        public int Eid { get; set; }
        public int Clid { get; set; }
        public bool? TestComplete { get; set; }
        public int? TestId { get; set; }
        public string TestGrade { get; set; }
        public DateTime? DateComplete { get; set; }
        public bool? DisclaimerSigned { get; set; }
        public bool? Retake { get; set; }
        public bool? TestInterrupt { get; set; }
        public bool? Restart { get; set; }
        public int? ExpOverride { get; set; }
        public DateTime? DateReleased { get; set; }
        public string ReleasedBy { get; set; }
        public int TestScore { get; set; }
        public DateTime? DateRecalled { get; set; }
        public string RecalledBy { get; set; }
    }
}
