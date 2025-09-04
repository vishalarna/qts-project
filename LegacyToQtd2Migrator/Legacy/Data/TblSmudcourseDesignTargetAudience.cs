using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDesignTargetAudience
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? TargetAudienceId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool? OtherOption { get; set; }

        public virtual TblTargetAudience TargetAudience { get; set; }
    }
}
