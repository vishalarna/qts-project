using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblGroupScheduleIla
    {
        public int Gsid { get; set; }
        public int Corid { get; set; }
        public int? Inid { get; set; }
        public int? LocId { get; set; }
        public DateTime? ClassDate { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual TblGroupSchedule Gs { get; set; }
    }
}
