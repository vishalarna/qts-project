using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblOnlineClass
    {
        public int? Clid { get; set; }
        public int? Eid { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? DateRescheduled { get; set; }
        public DateTime? DatePaused { get; set; }
    }
}
