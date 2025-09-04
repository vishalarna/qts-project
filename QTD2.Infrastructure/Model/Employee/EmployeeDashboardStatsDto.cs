namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeDashboardStatsDto
    {
        public int TestInProgressCount { get; set; }
        public int TestNotStartedCount { get; set; }
        public int OnlineCourseInProgressCount { get; set; }
        public int OnlineCourseNotStartedCount { get; set; }
        public int StudentEvaluationInProgressCount { get; set; }
        public int StudentEvaluationNotStartedCount { get; set; }
        public int ProcedureReviewsInProgressCount { get; set; }
        public int ProcedureReviewsNotStartedCount { get; set; }
        public int TaskQualificationInProgressCount { get; set; }
        public int TaskQualificationNotStartedCount { get; set; }
    }
}