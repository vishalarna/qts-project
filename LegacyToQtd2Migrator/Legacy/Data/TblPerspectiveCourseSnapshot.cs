using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblPerspectiveCourseSnapshot
    {
        public int Id { get; set; }
        public int PerspectiveCourseId { get; set; }
        public DateTime ChangeDate { get; set; }
        public int Version { get; set; }
        public int ChangedBy { get; set; }
        public string ProblemStatementResponse { get; set; }
        public DateTime? ProblemStatementResponseLastUpdated { get; set; }
        public int? ProblemStatementResponseLastUpdatedBy { get; set; }
        public string GoalsResponse { get; set; }
        public DateTime? GoalsResponseLastUpdated { get; set; }
        public int? GoalsResponseLastUpdatedBy { get; set; }
        public string ResultsResponse { get; set; }
        public DateTime? ResultsResponseLastUpdated { get; set; }
        public int? ResultsResponseLastUpdatedBy { get; set; }
        public string PerformanceObjectivesResponse { get; set; }
        public DateTime? PerformanceObjectivesResponseLastUpdated { get; set; }
        public int? PerformanceObjectivesResponseLastUpdatedBy { get; set; }
        public string PerformanceMetricResponse { get; set; }
        public DateTime? PerformanceMetricResponseLastUpdated { get; set; }
        public int? PerformanceMetricResponseLastUpdatedBy { get; set; }
        public string KnowledgeResponse { get; set; }
        public DateTime? KnowledgeResponseLastUpdated { get; set; }
        public int? KnowledgeResponseLastUpdatedBy { get; set; }
        public string LearningMetricResponse { get; set; }
        public DateTime? LearningMetricResponseLastUpdated { get; set; }
        public int? LearningMetricResponseLastUpdatedBy { get; set; }
        public string PerceptionResponse { get; set; }
        public DateTime? PerceptionResponseLastUpdated { get; set; }
        public int? PerceptionResponseLastUpdatedBy { get; set; }
        public string MotivationResponse { get; set; }
        public DateTime? MotivationResponseLastUpdated { get; set; }
        public int? MotivationResponseLastUpdatedBy { get; set; }

        public virtual TblPerspectiveCourse PerspectiveCourse { get; set; }
    }
}
