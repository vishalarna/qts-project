using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblGroupScheduleEmployee
    {
        public int Gsid { get; set; }
        public int Eid { get; set; }

        public virtual TblGroupSchedule Gs { get; set; }
    }
}
