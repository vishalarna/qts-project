using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTrainingSummaryByPosition
    {
        public int Eid { get; set; }
        public string Employee { get; set; }
        public string Position { get; set; }
        public float? CehNerc { get; set; }
        public float? CehRc { get; set; }
        public float? CehBito { get; set; }
        public float? CehBio { get; set; }
        public float? CehTo { get; set; }
        public float? CehProf { get; set; }
        public float? SimHours { get; set; }
        public float? EmergencyOpsHours { get; set; }
        public DateTime? CompDate { get; set; }
        public int? Tyear { get; set; }
        public int? NerccertArea { get; set; }
        public int Id { get; set; }
        public bool Selected { get; set; }
        public string CertAbbrev { get; set; }
        public DateTime? NerccertIssueDate { get; set; }
        public DateTime? NerccertExpDate { get; set; }
        public int? Nercpolicy { get; set; }
        public int? CredReqHours { get; set; }
        public int? TotalReqCehs { get; set; }
        public float? TotalCeh { get; set; }
        public float? CehReg { get; set; }
        public float? Reg2 { get; set; }
        public float? Other { get; set; }
        public float? TotalHours { get; set; }
        public string NerccertNum { get; set; }
        public string RegCertType { get; set; }
        public int? Corid { get; set; }
        public float StandardsFinal { get; set; }
        public float SimFinal { get; set; }
        public float TotalCehfinal { get; set; }
        public float TotalRegFinal { get; set; }
        public float TotalReg2Final { get; set; }
        public float TotalOtherFinal { get; set; }
        public float TotalTotalFinal { get; set; }
        public float? CoTotalCeh { get; set; }
    }
}
