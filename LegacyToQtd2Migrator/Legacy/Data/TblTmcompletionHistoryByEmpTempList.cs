using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTmcompletionHistoryByEmpTempList
    {
        public int Id { get; set; }
        public int? Eid { get; set; }
        public string EmpName { get; set; }
        public string CurrentUser { get; set; }
        public int Selection { get; set; }
        public DateTime? Modifiedtime { get; set; }
        public int? ActiveEmp { get; set; }
    }
}
