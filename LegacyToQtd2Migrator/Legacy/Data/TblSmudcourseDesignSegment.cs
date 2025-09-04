using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDesignSegment
    {
        public int SegmentId { get; set; }
        public int? SegmentTime { get; set; }
        public string SegmentText { get; set; }
        public int? SegmentOrder { get; set; }
        public int? CourseId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
    }
}
