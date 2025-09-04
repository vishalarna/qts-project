using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
  public partial  class InstructorWorkbook_ProspectiveILA :Common.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstructorWorkbook_ProspectiveILA()
        {
            this.InstructorWorkbook_ILA_Design = new HashSet<InstructorWorkbook_ILA_Design>();
            this.InstructorWorkbook_ILA_Develop = new HashSet<InstructorWorkbook_ILA_Develop>();
            this.InstructorWorkbook_ILA_Implement = new HashSet<InstructorWorkbook_ILA_Implement>();
            this.InstructorWorkbook_ILADesign_EnablingObjectives = new HashSet<InstructorWorkbook_ILADesign_EnablingObjectives>();
            this.InstructorWorkbook_ILADesign_NERC = new HashSet<InstructorWorkbook_ILADesign_NERC>();
            this.InstructorWorkbook_ILADesign_Prerequistie = new HashSet<InstructorWorkbook_ILADesign_Prerequistie>();
            this.InstructorWorkbook_ILADesign_Procedures = new HashSet<InstructorWorkbook_ILADesign_Procedures>();
            this.InstructorWorkbook_ILADesign_Resources = new HashSet<InstructorWorkbook_ILADesign_Resources>();
            this.InstructorWorkbook_ILADesign_SafetyHazards = new HashSet<InstructorWorkbook_ILADesign_SafetyHazards>();
            this.InstructorWorkbook_ILADesign_Segments = new HashSet<InstructorWorkbook_ILADesign_Segments>();
            this.InstructorWorkbook_ILADesign_Tasks = new HashSet<InstructorWorkbook_ILADesign_Tasks>();
            this.InstructorWorkbook_ILADesign_TrainingTopics = new HashSet<InstructorWorkbook_ILADesign_TrainingTopics>();
            this.InstructorWorkbook_ILADesignReviewers = new HashSet<InstructorWorkbook_ILADesignReviewers>();
            this.InstructorWorkbook_ILADevelopReviewers = new HashSet<InstructorWorkbook_ILADevelopReviewers>();
            this.InstructorWorkbook_ILAEvaluation = new HashSet<InstructorWorkbook_ILAEvaluation>();
            this.InstructorWorkbook_ILAEvaluation_TestAnalysis = new HashSet<InstructorWorkbook_ILAEvaluation_TestAnalysis>();
            this.InstructorWorkbook_ILAImplement_ClassSchedule = new HashSet<InstructorWorkbook_ILAImplement_ClassSchedule>();
            this.InstructorWorkbook_ILAImplementReviewers = new HashSet<InstructorWorkbook_ILAImplementReviewers>();
            this.InstructorWorkbook_ILAPhases = new HashSet<InstructorWorkbook_ILAPhases>();
            this.InstructorWorkbook_ProspectiveILA_Snapshot = new HashSet<InstructorWorkbook_ProspectiveILA_Snapshot>();
            this.InstructorWorkbook_ProspectiveILA_Archives = new HashSet<InstructorWorkbook_ProspectiveILA_Archives>();
            this.InstructorWorkbook_ILAEvaluation_TrainingIssues = new HashSet<InstructorWorkbook_ILAEvaluation_TrainingIssues>();
        }
        public int ProviderId { get; set; }
        public string ILAName { get; set; }
        public string ILANumber { get; set; }
        public int InstructorId { get; set; }
        public DateTime? DateOfAnalysis { get; set; }
        public bool SubmittedForReview { get; set; }
        public int? ReviewerId { get; set; }
        public bool? TrainingRecommended { get; set; }
        public string ReviewerNotes { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Result { get; set; }
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
        public string NtrComments { get; set; }
        public int? ILACorID { get; set; }
        public int? ConvertedBy { get; set; }
        public DateTime? ConvertedDate { get; set; }
        public bool InActive { get; set; }
        public int? CreatorId { get; set; }
        public string CreatorComments { get; set; }
        public bool CreatedFromExisting { get; set; }
        public bool AwardCEH { get; set; }
        public string AcceptNotes { get; set; }

        public virtual ILA ILA { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Instructor Instructor1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILA_Design> InstructorWorkbook_ILA_Design { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILA_Develop> InstructorWorkbook_ILA_Develop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILA_Implement> InstructorWorkbook_ILA_Implement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_EnablingObjectives> InstructorWorkbook_ILADesign_EnablingObjectives { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_NERC> InstructorWorkbook_ILADesign_NERC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_Prerequistie> InstructorWorkbook_ILADesign_Prerequistie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_Procedures> InstructorWorkbook_ILADesign_Procedures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_Resources> InstructorWorkbook_ILADesign_Resources { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_SafetyHazards> InstructorWorkbook_ILADesign_SafetyHazards { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_Segments> InstructorWorkbook_ILADesign_Segments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_Tasks> InstructorWorkbook_ILADesign_Tasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesign_TrainingTopics> InstructorWorkbook_ILADesign_TrainingTopics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADesignReviewers> InstructorWorkbook_ILADesignReviewers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILADevelopReviewers> InstructorWorkbook_ILADevelopReviewers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILAEvaluation> InstructorWorkbook_ILAEvaluation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILAEvaluation_TestAnalysis> InstructorWorkbook_ILAEvaluation_TestAnalysis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILAImplement_ClassSchedule> InstructorWorkbook_ILAImplement_ClassSchedule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILAImplementReviewers> InstructorWorkbook_ILAImplementReviewers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILAPhases> InstructorWorkbook_ILAPhases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ProspectiveILA_Snapshot> InstructorWorkbook_ProspectiveILA_Snapshot { get; set; }
        public virtual Provider Provider { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ProspectiveILA_Archives> InstructorWorkbook_ProspectiveILA_Archives { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILAEvaluation_TrainingIssues> InstructorWorkbook_ILAEvaluation_TrainingIssues { get; set; }
        public virtual Person Person { get; set; }

    }
}
