using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblRsaw
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
        public string R11response { get; set; }
        public string R111response { get; set; }
        public string R12response { get; set; }
        public string R13response { get; set; }
        public string R14response { get; set; }
        public bool? R14question1 { get; set; }
        public bool? R14question2 { get; set; }
        public string R2response { get; set; }
        public bool? R21question1 { get; set; }
        public string R21response { get; set; }
        public string R3response { get; set; }
        public bool? R31question1 { get; set; }
        public bool? R31question2 { get; set; }
        public string R31response1 { get; set; }
        public string R31response2 { get; set; }
        public byte[] Ts { get; set; }
    }
}
