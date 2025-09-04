using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblRsaw2
    {
        public int Rsawid { get; set; }
        public string RegisteredEntity { get; set; }
        public string Ncrnumber { get; set; }
        public string ApplicableFunction { get; set; }
        public DateTime? ComplianceAssessmentDate { get; set; }
        public string ComplianceMonitoringMethod { get; set; }
        public string NamesOfAuditors { get; set; }
        public string ComplianceEnforcementAuthority { get; set; }
        public string R1response { get; set; }
        public string R2response { get; set; }
        public string R3response1 { get; set; }
        public string R3response2 { get; set; }
        public bool? R3question { get; set; }
        public string R4response1 { get; set; }
        public string R4response2 { get; set; }
        public string R4response3 { get; set; }
        public bool? R4question1 { get; set; }
        public bool? R4question2 { get; set; }
        public string R5response { get; set; }
        public string R6response { get; set; }
        public byte[] Ts { get; set; }
    }
}
