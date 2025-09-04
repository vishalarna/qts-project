using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ViewQtsDynamicQuery
    {
        public string ElastName { get; set; }
        public string EfirstName { get; set; }
        public string Enum { get; set; }
        public string Oname { get; set; }
        public bool? InActive { get; set; }
        public string Pdesc { get; set; }
        public bool? EnotCertified { get; set; }
        public string NerccertNum { get; set; }
        public string CertDesc { get; set; }
        public string CertDescExisting { get; set; }
        public DateTime? NerccertIssueDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? NerccertExpDate { get; set; }
        public string Suname { get; set; }
        public string Nercid { get; set; }
        public string Cordesc { get; set; }
        public string Cornum { get; set; }
        public float? TotalCeh { get; set; }
        public float? CehNerc { get; set; }
        public float? SimHours { get; set; }
        public float? TotalHours { get; set; }
        public float? EmergencyOpsHours { get; set; }
        public bool? TopicsEo { get; set; }
        public bool? SelfPased { get; set; }
        public bool? ActPartialCredits { get; set; }
        public string Lcdesc { get; set; }
        public string Inname { get; set; }
        public DateTime? Cldate { get; set; }
        public float? PartialStd { get; set; }
        public float? PartialSim { get; set; }
        public float? PartialTotalCehs { get; set; }
        public string CompGrade { get; set; }
        public DateTime? ChNerccertIssueDate { get; set; }
        public DateTime? ChNerccertExpDate { get; set; }
        public string ChNerccertNum { get; set; }
        public int? Eid { get; set; }
        public int? Suid { get; set; }
        public int? NerccertArea { get; set; }
        public int? NerccertAreaExisting { get; set; }
        public string Nsname { get; set; }
    }
}
