using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryIdpCompletionSmud
    {
        public int? Corid { get; set; }
        public string Cornum { get; set; }
        public string Cordesc { get; set; }
        public DateTime? Planned { get; set; }
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
        public float? PartialExtra { get; set; }
        public float? Expr1 { get; set; }
        public int? ProctorId { get; set; }
        public float? PartialReg { get; set; }
        public float? PartialReg2 { get; set; }
        public float? PartialOther { get; set; }
        public float? PartialTotal { get; set; }
    }
}
