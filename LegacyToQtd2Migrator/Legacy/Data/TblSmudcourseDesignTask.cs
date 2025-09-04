using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDesignTask
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? TaskId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
        public virtual TblTask Task { get; set; }
    }
}
