using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseDesign
    {
        public long CourseDetailId { get; set; }
        public int CourseId { get; set; }
        public bool? IsSefPacedTraining { get; set; }
        public bool? IsSelfRegistrationEmp { get; set; }
        public bool? IsScormBased { get; set; }
        public bool? IsOptional { get; set; }
        public string IsOptionalText { get; set; }
        public double? StandardRelatedHours { get; set; }
        public double? OperatingTopicHours { get; set; }
        public double? SimulationHours { get; set; }
        public double? TotalCehs { get; set; }
        public double? EmergOpHours { get; set; }
        public double? Regional1 { get; set; }
        public double? Regional2 { get; set; }
        public double? Other { get; set; }
        public double? TotalTrainingHours { get; set; }
        public bool? IncludeSimulations { get; set; }
        public bool? IsPartialCreditHoursAllowed { get; set; }
        public bool? IsEmergencyOperationHours { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? SegmentOrder { get; set; }
        public string DesignResult { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? IsOperatorCredentialMaintenance { get; set; }
        public bool? IsWrittenOrOnlineExam { get; set; }
        public bool? IsPerformanceDemonstration { get; set; }
        public string OtherTypeAssesmentTool { get; set; }
        public string CourseDetailsStatus { get; set; }
        public DateTime? CourseDetailsStatusLastUpdated { get; set; }
        public int? CourseDetailsStatusLastUpdatedBy { get; set; }
        public string ObjectivesAndSegmentsStatus { get; set; }
        public DateTime? ObjectivesAndSegmentsLastUpdated { get; set; }
        public int? ObjectivesAndSegmentsLastUpdatedBy { get; set; }
        public string ProceduresStatus { get; set; }
        public DateTime? ProceduresStatusLastUpdated { get; set; }
        public int? ProceduresStatusLastUpdatedBy { get; set; }
        public string TrainingPlanResponse { get; set; }
        public string TrainingPlanStatus { get; set; }
        public DateTime? TrainingPlanStatusLastUpdated { get; set; }
        public int? TrainingPlanStatusLastUpdatedBy { get; set; }
        public string EvaluationMethodResponse { get; set; }
        public string EvaluationMethodResponseStatus { get; set; }
        public DateTime? EvaluationMethodResponseLastUpdated { get; set; }
        public int? EvaluationMethodResponseLastUpdatedBy { get; set; }
        public string PrerequistiesStatus { get; set; }
        public DateTime? PrerequistiesStatusLastUpdated { get; set; }
        public int? PrerequistiesStatusLastUpdatedBy { get; set; }
        public string ResourcesStatus { get; set; }
        public DateTime? ResourcesStatusLastUpdated { get; set; }
        public int? ResourcesStatusLastUpdatedBy { get; set; }
        public string IlaapplicationStatus { get; set; }
        public DateTime? IlaapplicationStatusLastUpdated { get; set; }
        public int? IlastatusLastUpdatedBy { get; set; }
        public bool? SubmitForReview { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
        public string PilotDataApplicable { get; set; }
        public string InstructorComments { get; set; }
        public string ReviewerComments { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
    }
}
