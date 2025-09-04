using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseImplement
    {
        public int CourseImplementId { get; set; }
        public int? CourseId { get; set; }
        public bool? SubmitRoasterForReview { get; set; }
        public string ImplementResult { get; set; }
        public string InstructorComments { get; set; }
        public string ReviewerComments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
    }
}
