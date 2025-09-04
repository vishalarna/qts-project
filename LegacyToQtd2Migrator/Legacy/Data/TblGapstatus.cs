using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblGapstatus
    {
        public int Gapsid { get; set; }
        public int Prjid { get; set; }
        public int Pid { get; set; }
        public int Eid { get; set; }
        public bool? ReleasedToEmp { get; set; }
        public bool Complete { get; set; }
        public bool Started { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
        public virtual TblPosition PidNavigation { get; set; }
        public virtual TblGapProject Prj { get; set; }
    }
}
