using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmployeeTest
    {
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
        public DateTime? DateAdded { get; set; }
        public string AddedBy { get; set; }
        public int TestScore { get; set; }
        public bool? IsPreTest { get; set; }
    }
}
