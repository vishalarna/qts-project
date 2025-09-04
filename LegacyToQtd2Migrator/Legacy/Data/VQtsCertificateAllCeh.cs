using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsCertificateAllCeh
    {
        public string Name { get; set; }
        public string Enum { get; set; }
        public string Cornum { get; set; }
        public int Clid { get; set; }
        public int? Corid { get; set; }
        public DateTime? Cldate { get; set; }
        public string CompGrade { get; set; }
        public string Lcdesc { get; set; }
        public string Inname { get; set; }
        public string Nercid { get; set; }
        public string Cordesc { get; set; }
        public string NerccertNum { get; set; }
        public float? CehNerc { get; set; }
        public float? CehRc { get; set; }
        public float? CehBito { get; set; }
        public float? CehBio { get; set; }
        public float? CehTo { get; set; }
        public float? CehProf { get; set; }
        public float? CehReg { get; set; }
        public float? SimHours { get; set; }
        public float? EmergencyOpsHours { get; set; }
        public float? Other { get; set; }
        public float? TotalCeh { get; set; }
        public float? TotalHours { get; set; }
        public int? NerccertArea { get; set; }
        public int Eid { get; set; }
        public string CpStreetAddress { get; set; }
        public string CpCity { get; set; }
        public string CpState { get; set; }
        public string CpZip { get; set; }
        public string Suname { get; set; }
        public int? Suid { get; set; }
        public string ContactPerson { get; set; }
        public string CpPhone { get; set; }
        public string PassFailed { get; set; }
        public float StandardsFinal { get; set; }
        public float SimFinal { get; set; }
        public float TotalCehfinal { get; set; }
        public string TprName { get; set; }
        public string TprSignaturePath { get; set; }
        public string Oname { get; set; }
        public float? Score { get; set; }
        public string ReasonWo { get; set; }
    }
}
