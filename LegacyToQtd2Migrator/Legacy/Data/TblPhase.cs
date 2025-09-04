using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblPhase
    {
        public TblPhase()
        {
            CoursePhases = new HashSet<CoursePhase>();
        }

        public long CoursePhaseId { get; set; }
        public string CoursePhaseDescription { get; set; }

        public virtual ICollection<CoursePhase> CoursePhases { get; set; }
    }
}
