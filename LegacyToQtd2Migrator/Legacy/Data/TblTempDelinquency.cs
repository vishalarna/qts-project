using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTempDelinquency
    {
        public int Eid { get; set; }
        public string Employee { get; set; }
        public string Pdesc { get; set; }
        public float? CehNerc { get; set; }
        public float? CehRc { get; set; }
        public float? CehBito { get; set; }
        public float? CehBio { get; set; }
        public float? CehTo { get; set; }
        public float? CehProf { get; set; }
        public float? CehReg { get; set; }
        public float? SimHours { get; set; }
        public float? EmergencyOpsHours { get; set; }
        public DateTime? CompDate { get; set; }
        public int? Clyear { get; set; }
        public int? NerccertArea { get; set; }
        public string CertAbbrev { get; set; }
        public DateTime? NerccertIssueDate { get; set; }
        public float? Other { get; set; }
        public DateTime? NerccertExpDate { get; set; }
        public float? Nercpolicy { get; set; }
        public float? CredReqHours { get; set; }
        public float? TotalReqCehs { get; set; }
        public float? TotalCeh { get; set; }
        public int? Flag { get; set; }
        public string NerccertNum { get; set; }
        public string RegCertType { get; set; }
        public string RegCertNum { get; set; }
        public DateTime? RegCertIssueDate { get; set; }
        public DateTime? ReqCertExpDate { get; set; }
        public string Pabbrev { get; set; }
        public string Oname { get; set; }
        public float? EmResp { get; set; }
        public float? RegReq { get; set; }
        public byte[] UpsizeTs { get; set; }
    }
}
