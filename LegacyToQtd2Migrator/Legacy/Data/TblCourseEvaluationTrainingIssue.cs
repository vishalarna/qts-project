using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCourseEvaluationTrainingIssue
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public string IssueTitle { get; set; }
        public string IssueDescription { get; set; }
        public string FeedbackType { get; set; }
        public string Severity { get; set; }
        public int? LevelNum { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
    }
}
