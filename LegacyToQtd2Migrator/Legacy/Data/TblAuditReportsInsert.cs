using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblAuditReportsInsert
    {
        public int Arid { get; set; }
        public int AuditMenu { get; set; }
        public int AuditMenuLevel { get; set; }
        public string ReportTitle { get; set; }
        public string CriteriaForm { get; set; }
        public int ReportId { get; set; }
        public int? SortOrder { get; set; }
    }
}
