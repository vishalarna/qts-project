using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIlaDetail
    {
        public int Corid { get; set; }
        public bool? WrittenTestQuiz { get; set; }
        public bool? VerbalTestQuiz { get; set; }
        public bool? Checklist { get; set; }
        public bool? PerformanceDemonstration { get; set; }
        public bool? Ojt { get; set; }
        public bool? FieldVisit { get; set; }
        public bool? Simulation { get; set; }
        public bool? SituationalAwarenessAssmt { get; set; }
        public bool? CriticalDecisionAssmt { get; set; }
        public bool? ProblemSolvingAssmt { get; set; }
        public bool? OtherTypeAssmt { get; set; }
        public bool? PassingGradeAssmt { get; set; }
        public float? PassingGradePercent { get; set; }
        public bool? TrainerPresent { get; set; }
        public bool? ProctorPresent { get; set; }
        public bool? LearningContract { get; set; }
        public bool? UniqueLogin { get; set; }
        public bool? CourseEvaluationEachStudent { get; set; }
        public int? AttendanceVerificationId { get; set; }
        public string AttendanceVerificationOther { get; set; }
        public bool? ProviderFollowsSat { get; set; }
        public bool? LearningAssmtTimeInc { get; set; }
        public bool? InstructionalDeliveryTeamEngineer { get; set; }
        public bool? InstructionalDeliveryTeamSubjMatter { get; set; }
        public bool? InstructionalDeliveryTeamCertSysOp { get; set; }
        public bool? InstructionalDeliveryTeamOther { get; set; }
        public string InstructionalDeliveryTeamOtherText { get; set; }
        public DateTime? StartDate1 { get; set; }
        public DateTime? StartDate2 { get; set; }
        public DateTime? StartDate3 { get; set; }
        public DateTime? StartDate4 { get; set; }
        public DateTime? StartDate5 { get; set; }
        public DateTime? StartDate6 { get; set; }
        public bool? ExpeditedProcessing { get; set; }
        public bool? AvsignInSheet { get; set; }
        public bool? Avroster { get; set; }
        public bool? Avidentification { get; set; }
        public bool? Avother { get; set; }
        public string OtherTypeAssmtText { get; set; }
        public bool? InstructionalDeliveryTeamTrainer { get; set; }
        public string LearningAssessJustf { get; set; }
        public string AttVerComments { get; set; }
        public string CourseComments { get; set; }
        public string FormVersion { get; set; }
    }
}
