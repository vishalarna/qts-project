using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblPerspectiveCourse
    {
        public TblPerspectiveCourse()
        {
            CoursePhases = new HashSet<CoursePhase>();
            TblCourseEvaluationTrainingIssues = new HashSet<TblCourseEvaluationTrainingIssue>();
            TblPerspectiveCourseArchives = new HashSet<TblPerspectiveCourseArchive>();
            TblPerspectiveCourseSnapshots = new HashSet<TblPerspectiveCourseSnapshot>();
            TblSmudCourseDesignReviewers = new HashSet<TblSmudCourseDesignReviewer>();
            TblSmudCourseDevelopReviewers = new HashSet<TblSmudCourseDevelopReviewer>();
            TblSmudCourseImplementReviewers = new HashSet<TblSmudCourseImplementReviewer>();
            TblSmudcourseDesignEnablingObjectives = new HashSet<TblSmudcourseDesignEnablingObjective>();
            TblSmudcourseDesignNercs = new HashSet<TblSmudcourseDesignNerc>();
            TblSmudcourseDesignPrerequisties = new HashSet<TblSmudcourseDesignPrerequistie>();
            TblSmudcourseDesignProcedures = new HashSet<TblSmudcourseDesignProcedure>();
            TblSmudcourseDesignResources = new HashSet<TblSmudcourseDesignResource>();
            TblSmudcourseDesignSafetyHazards = new HashSet<TblSmudcourseDesignSafetyHazard>();
            TblSmudcourseDesignSegments = new HashSet<TblSmudcourseDesignSegment>();
            TblSmudcourseDesignTasks = new HashSet<TblSmudcourseDesignTask>();
            TblSmudcourseDesignTrainingTopics = new HashSet<TblSmudcourseDesignTrainingTopic>();
            TblSmudcourseDesigns = new HashSet<TblSmudcourseDesign>();
            TblSmudcourseDevelops = new HashSet<TblSmudcourseDevelop>();
            TblSmudcourseEvaluationTestAnalyses = new HashSet<TblSmudcourseEvaluationTestAnalysis>();
            TblSmudcourseEvaluations = new HashSet<TblSmudcourseEvaluation>();
            TblSmudcourseImplementClassSchedules = new HashSet<TblSmudcourseImplementClassSchedule>();
            TblSmudcourseImplements = new HashSet<TblSmudcourseImplement>();
        }

        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string CourseName { get; set; }
        public string CourseNumber { get; set; }
        public int InstructorId { get; set; }
        public DateTime? DateOfAnalysis { get; set; }
        public bool SubmittedForReview { get; set; }
        public int? ReviewerId { get; set; }
        public bool? TrainingRecommended { get; set; }
        public string ReviewerNotes { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string Result { get; set; }
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
        public string NtrComments { get; set; }
        public int? TblCourseCorId { get; set; }
        public int? ConvertedBy { get; set; }
        public DateTime? ConvertedDate { get; set; }
        public bool InActive { get; set; }

        public virtual LktblInstructor Instructor { get; set; }
        public virtual LktblSupplier Provider { get; set; }
        public virtual LktblInstructor Reviewer { get; set; }
        public virtual TblCourse TblCourseCor { get; set; }
        public virtual ICollection<CoursePhase> CoursePhases { get; set; }
        public virtual ICollection<TblCourseEvaluationTrainingIssue> TblCourseEvaluationTrainingIssues { get; set; }
        public virtual ICollection<TblPerspectiveCourseArchive> TblPerspectiveCourseArchives { get; set; }
        public virtual ICollection<TblPerspectiveCourseSnapshot> TblPerspectiveCourseSnapshots { get; set; }
        public virtual ICollection<TblSmudCourseDesignReviewer> TblSmudCourseDesignReviewers { get; set; }
        public virtual ICollection<TblSmudCourseDevelopReviewer> TblSmudCourseDevelopReviewers { get; set; }
        public virtual ICollection<TblSmudCourseImplementReviewer> TblSmudCourseImplementReviewers { get; set; }
        public virtual ICollection<TblSmudcourseDesignEnablingObjective> TblSmudcourseDesignEnablingObjectives { get; set; }
        public virtual ICollection<TblSmudcourseDesignNerc> TblSmudcourseDesignNercs { get; set; }
        public virtual ICollection<TblSmudcourseDesignPrerequistie> TblSmudcourseDesignPrerequisties { get; set; }
        public virtual ICollection<TblSmudcourseDesignProcedure> TblSmudcourseDesignProcedures { get; set; }
        public virtual ICollection<TblSmudcourseDesignResource> TblSmudcourseDesignResources { get; set; }
        public virtual ICollection<TblSmudcourseDesignSafetyHazard> TblSmudcourseDesignSafetyHazards { get; set; }
        public virtual ICollection<TblSmudcourseDesignSegment> TblSmudcourseDesignSegments { get; set; }
        public virtual ICollection<TblSmudcourseDesignTask> TblSmudcourseDesignTasks { get; set; }
        public virtual ICollection<TblSmudcourseDesignTrainingTopic> TblSmudcourseDesignTrainingTopics { get; set; }
        public virtual ICollection<TblSmudcourseDesign> TblSmudcourseDesigns { get; set; }
        public virtual ICollection<TblSmudcourseDevelop> TblSmudcourseDevelops { get; set; }
        public virtual ICollection<TblSmudcourseEvaluationTestAnalysis> TblSmudcourseEvaluationTestAnalyses { get; set; }
        public virtual ICollection<TblSmudcourseEvaluation> TblSmudcourseEvaluations { get; set; }
        public virtual ICollection<TblSmudcourseImplementClassSchedule> TblSmudcourseImplementClassSchedules { get; set; }
        public virtual ICollection<TblSmudcourseImplement> TblSmudcourseImplements { get; set; }
    }
}
