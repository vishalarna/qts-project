using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDesignResource
    {
        public long Id { get; set; }
        public int? CourseId { get; set; }
        public string ResourceName { get; set; }
        public string ResourcePath { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
    }
}
