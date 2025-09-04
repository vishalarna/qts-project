using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDesignEnablingObjective
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? EnablingObjectiveId { get; set; }
        public string EnablingObjectivesDescription { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
    }
}
