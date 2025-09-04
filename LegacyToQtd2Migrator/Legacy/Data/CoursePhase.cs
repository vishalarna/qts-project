using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class CoursePhase
    {
        public long Id { get; set; }
        public int? CourseId { get; set; }
        public long? CoursePhaseId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
        public virtual TblPhase CoursePhaseNavigation { get; set; }
    }
}
