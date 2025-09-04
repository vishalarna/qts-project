using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDevelop
    {
        public int CourseDevelopId { get; set; }
        public int? CourseId { get; set; }
        public bool? IsPracticeTeachRequired { get; set; }
        public bool? IsPracticeTeachCompleted { get; set; }
        public bool? IsInstructorCheckListRequired { get; set; }
        public bool? IsInstructorCheckListCompleted { get; set; }
        public bool? SubmitForApproval { get; set; }
        public string DevelopResult { get; set; }
        public string InstructorComments { get; set; }
        public string ReviewerComments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
    }
}
