using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsIdpcompletion
    {
        public int? Corid { get; set; }
        public string Cornum { get; set; }
        public string Cordesc { get; set; }
        public string Planned { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? Completed { get; set; }
        public string Lcdesc { get; set; }
        public int Idpid { get; set; }
        public string Inname { get; set; }
        public string InstLoc { get; set; }
        public string CompGrade { get; set; }
        public int? Eid { get; set; }
        public string Tyear { get; set; }
        public int? Clid { get; set; }
        public bool SelfPased { get; set; }
        public DateTime? ReqCompDate { get; set; }
        public bool ActPartialCredits { get; set; }
        public float? PartialStd { get; set; }
        public float? PartialSim { get; set; }
        public float? PartialTotalCehs { get; set; }
        public string Partial { get; set; }
        public float? CehNerc { get; set; }
        public float? SimHours { get; set; }
        public float? TotalCeh { get; set; }
        public int? Pid { get; set; }
        public float? Score { get; set; }
        public string ReasonWo { get; set; }
        public string OptionalComments { get; set; }
    }
}
