using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_ProspectiveILA_Snapshot :Common.Entity
    {
        public int ProspectiveILAID { get; set; }
        public DateTime ChangeDate { get; set; }
        public int Version { get; set; }
        public int ChangedBy { get; set; }
        public string ProblemStatementResponse { get; set; }
        public DateTime? ProblemStatementResponse_LastUpdated { get; set; }
        public int? ProblemStatementResponse_LastUpdatedBy { get; set; }
        public string GoalsResponse { get; set; }
        public DateTime? GoalsResponse_LastUpdated { get; set; }
        public int? GoalsResponse_LastUpdatedBy { get; set; }
        public string ResultsResponse { get; set; }
        public DateTime? ResultsResponse_LastUpdated { get; set; }
        public int? ResultsResponse_LastUpdatedBy { get; set; }
        public string PerformanceObjectivesResponse { get; set; }
        public DateTime? PerformanceObjectivesResponse_LastUpdated { get; set; }
        public int? PerformanceObjectivesResponse_LastUpdatedBy { get; set; }
        public string PerformanceMetricResponse { get; set; }
        public DateTime? PerformanceMetricResponse_LastUpdated { get; set; }
        public int? PerformanceMetricResponse_LastUpdatedBy { get; set; }
        public string KnowledgeResponse { get; set; }
        public DateTime? KnowledgeResponse_LastUpdated { get; set; }
        public int? KnowledgeResponse_LastUpdatedBy { get; set; }
        public string LearningMetricResponse { get; set; }
        public DateTime? LearningMetricResponse_LastUpdated { get; set; }
        public int? LearningMetricResponse_LastUpdatedBy { get; set; }
        public string PerceptionResponse { get; set; }
        public DateTime? PerceptionResponse_LastUpdated { get; set; }
        public int? PerceptionResponse_LastUpdatedBy { get; set; }
        public string MotivationResponse { get; set; }
        public DateTime? MotivationResponse_LastUpdated { get; set; }
        public int? MotivationResponse_LastUpdatedBy { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }

    }
}
