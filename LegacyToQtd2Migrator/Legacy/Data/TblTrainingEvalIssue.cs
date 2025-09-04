using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingEvalIssue
    {
        public int IssueId { get; set; }
        public int? IssueIdcounter { get; set; }
        public int? IssueIdmonth { get; set; }
        public int IssueIdyear { get; set; }
        public int? Severity { get; set; }
        public bool? ChangeInTools { get; set; }
        public bool? ChangeInProcedure { get; set; }
        public bool? NewOrModifiedTask { get; set; }
        public bool? PerformanceProblem { get; set; }
        public bool? InstructorFeedback { get; set; }
        public bool? EmployeeStudentFeedback { get; set; }
        public bool? Other { get; set; }
        public bool? ProgramType { get; set; }
        public bool? LearningActivity { get; set; }
        public bool? OjttaskQualification { get; set; }
        public string ChangeInToolsDesc { get; set; }
        public int? ChangeInProcedurePid { get; set; }
        public string PerformanceProblemDesc { get; set; }
        public int? InstructorFeedbackClass { get; set; }
        public int? EmpStdntFeedbackClass { get; set; }
        public string OtherDesc { get; set; }
        public int? ProgramTypeSelection { get; set; }
        public int? ProgramTypePosId { get; set; }
        public int? ProgramTypeYear { get; set; }
        public DateTime? ProgramTypeStartDate { get; set; }
        public int? LearningActivityCorid { get; set; }
        public int? OjttaskQualificationTask { get; set; }
        public string Problem { get; set; }
        public string Summary { get; set; }
        public string Action { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? Dra { get; set; }
        public string Raby { get; set; }
        public int? IssueStatus { get; set; }
        public string InstFeedbackText { get; set; }
        public int? TrProgVersionId { get; set; }
        public string IssueIdnum { get; set; }
        public DateTime? IssueDate { get; set; }

        public virtual TblProcedure ChangeInProcedureP { get; set; }
    }
}
