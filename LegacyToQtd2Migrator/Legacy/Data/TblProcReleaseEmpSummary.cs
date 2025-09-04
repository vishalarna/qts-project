using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblProcReleaseEmpSummary
    {
        public int RelId { get; set; }
        public int Prid { get; set; }
        public int Pid { get; set; }
        public int Eid { get; set; }
        public bool? ReleasedToEmp { get; set; }
        public bool Complete { get; set; }
        public bool Started { get; set; }
        public string PosComments { get; set; }
        public string ProcEmpstatus { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? ProcStartDateAtRel { get; set; }
        public DateTime? ProcEndDateAtRel { get; set; }
        public bool? EmployeeProcResponse { get; set; }
    }
}
