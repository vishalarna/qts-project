using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIdpreleaseEmpSummary
    {
        public int RelId { get; set; }
        public int Idpid { get; set; }
        public string IdpTitle { get; set; }
        public int Eid { get; set; }
        public int Pid { get; set; }
        public bool? ReleasedToEmp { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool? EmpIdpresponse { get; set; }
        public string Empcomments { get; set; }
        public string Status { get; set; }
    }
}
