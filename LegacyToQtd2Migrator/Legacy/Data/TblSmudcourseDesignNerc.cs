using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDesignNerc
    {
        public long Id { get; set; }
        public int? CourseId { get; set; }
        public int? Nsid { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
        public virtual TblNercstandard Ns { get; set; }
    }
}
