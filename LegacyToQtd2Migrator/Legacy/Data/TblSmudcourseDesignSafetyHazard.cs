using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDesignSafetyHazard
    {
        public long Id { get; set; }
        public int? CourseId { get; set; }
        public int? Shzid { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
        public virtual TblSafetyHazard Shz { get; set; }
    }
}
