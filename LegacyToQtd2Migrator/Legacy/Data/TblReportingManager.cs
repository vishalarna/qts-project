using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblReportingManager
    {
        public TblReportingManager()
        {
            RsTblReportingMgrEmployees = new HashSet<RsTblReportingMgrEmployee>();
        }

        public int Rmid { get; set; }
        public int? RmEid { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Email { get; set; }
        public string Ename { get; set; }

        public virtual ICollection<RsTblReportingMgrEmployee> RsTblReportingMgrEmployees { get; set; }
    }
}
