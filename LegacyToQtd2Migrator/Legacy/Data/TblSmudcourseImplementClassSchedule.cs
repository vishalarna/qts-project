using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseImplementClassSchedule
    {
        public int CourseScheduleId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? InstructorId { get; set; }
        public int? LocationId { get; set; }
        public bool? IsInstructorEnrolled { get; set; }
        public bool? IsTestLinked { get; set; }
        public bool? IsRetakeLinked { get; set; }
        public int? TestId { get; set; }
        public int? RetakeTestId { get; set; }
        public int? EvaluationId { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
        public virtual TblForm Evaluation { get; set; }
        public virtual LktblInstructor Instructor { get; set; }
        public virtual LktblLocation Location { get; set; }
        public virtual TblTest RetakeTest { get; set; }
        public virtual TblTest Test { get; set; }
    }
}
