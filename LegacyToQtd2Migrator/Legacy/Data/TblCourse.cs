using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCourse
    {
        public TblCourse()
        {
            RsTblCoursesSkillsKnowledges = new HashSet<RsTblCoursesSkillsKnowledge>();
            RsTblCoursesTasks = new HashSet<RsTblCoursesTask>();
            TblClasses = new HashSet<TblClass>();
            TblContentObjectPresentations = new HashSet<TblContentObjectPresentation>();
            TblCourseSegments = new HashSet<TblCourseSegment>();
            TblIdps = new HashSet<TblIdp>();
            TblIlaresources = new HashSet<TblIlaresource>();
            TblPerspectiveCourses = new HashSet<TblPerspectiveCourse>();
            TblSafetyHazardIlas = new HashSet<TblSafetyHazardIla>();
            TblTests = new HashSet<TblTest>();
            TblTrainingModuleIlas = new HashSet<TblTrainingModuleIla>();
        }

        public int Corid { get; set; }
        public string Cornum { get; set; }
        public string Cordesc { get; set; }
        public float? CehNerc { get; set; }
        public float? CehRc { get; set; }
        public float? CehBito { get; set; }
        public float? CehBio { get; set; }
        public float? CehTo { get; set; }
        public float? CehProf { get; set; }
        public float? CehReg { get; set; }
        public float? SimHours { get; set; }
        public float? EmergencyOpsHours { get; set; }
        public int? Suid { get; set; }
        public float? Other { get; set; }
        public float? TotalCeh { get; set; }
        public float? TotalHours { get; set; }
        public string CehappDataType { get; set; }
        public bool CehappDataFee { get; set; }
        public bool TargetAudienceTo { get; set; }
        public bool TargetAudienceRc { get; set; }
        public bool TargetAudienceBio { get; set; }
        public bool TargetAudienceOther { get; set; }
        public string TrainingPlan { get; set; }
        public string Content { get; set; }
        public string DeliveryMethod { get; set; }
        public string DeliveryTeam { get; set; }
        public string EvaluationMethod { get; set; }
        public string VerifAndDocOfCehhours { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public bool TypeClassroom { get; set; }
        public bool TypeWorkshop { get; set; }
        public bool TypeConference { get; set; }
        public bool TypeOjttraining { get; set; }
        public bool TypeSelfStudy { get; set; }
        public bool TypeInternetBased { get; set; }
        public bool TypeOtsimulation { get; set; }
        public bool TypeComputerBased { get; set; }
        public bool TypeOther { get; set; }
        public bool Type10 { get; set; }
        public bool Type11 { get; set; }
        public string TypeOtherSpecify { get; set; }
        public bool LerningActivitySelfStudy { get; set; }
        public bool LerningActivityMultiDeliv { get; set; }
        public bool LerningActivityPublic { get; set; }
        public string TargetAudienceOtherSpecify { get; set; }
        public bool TargetAudienceGo { get; set; }
        public bool TargetAudienceMo { get; set; }
        public bool TargetAudienceOpe { get; set; }
        public bool TargetAudienceCrs { get; set; }
        public bool TargetAudience9 { get; set; }
        public bool TargetAudience10 { get; set; }
        public bool CatNercstandards { get; set; }
        public bool CatOperatingTopics { get; set; }
        public bool CatProfRelated { get; set; }
        public bool ActSimulationTotal { get; set; }
        public bool ActEoptotal { get; set; }
        public bool TopicsBc { get; set; }
        public bool TopicsPtee { get; set; }
        public bool TopicsSp { get; set; }
        public bool TopicsIpso { get; set; }
        public bool TopicsEo { get; set; }
        public bool TopicsPsr { get; set; }
        public bool TopicsMo { get; set; }
        public bool TopicsTools { get; set; }
        public bool TopicsOa { get; set; }
        public bool TopicsPp { get; set; }
        public bool Topics11 { get; set; }
        public bool Topics12 { get; set; }
        public bool ActMayBeUsedByCso { get; set; }
        public bool ActMayBeUsedAsPdh { get; set; }
        public DateTime? CehAppDate { get; set; }
        public bool ActPartialCredits { get; set; }
        public DateTime? CehApprovalDate { get; set; }
        public int? Fid { get; set; }
        public bool ReginalExerciseIncluded { get; set; }
        public bool SelfPased { get; set; }
        public bool EopsPer002 { get; set; }
        public float? Reg2 { get; set; }
        public bool NotNercrelated { get; set; }
        public DateTime? CorexpDate { get; set; }
        public string Prerequisites { get; set; }
        public bool? NotOnNercreport { get; set; }
        public byte[] Ts { get; set; }
        public string CourseProcedures { get; set; }
        public bool? LerningActivitySelfStudyNa { get; set; }
        public bool? OnlineClass { get; set; }
        public int? AssessmentMethodId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public int? InstructorId { get; set; }
        public bool? SelfReg { get; set; }
        public bool? IsOptional { get; set; }
        public string OptionalComments { get; set; }
        public float? Ceh4 { get; set; }
        public float? Ceh5 { get; set; }
        public float? Ceh6 { get; set; }
        public float? Ceh7 { get; set; }
        public float? Ceh8 { get; set; }
        public float? Ceh9 { get; set; }
        public float? Ceh10 { get; set; }
        public float? Ceh11 { get; set; }
        public float? Ceh12 { get; set; }
        public float? Ceh13 { get; set; }
        public float? Ceh14 { get; set; }
        public float? Ceh15 { get; set; }
        public float? Ceh16 { get; set; }
        public float? Ceh17 { get; set; }
        public float? Ceh18 { get; set; }
        public float? Ceh19 { get; set; }
        public float? Ceh20 { get; set; }
        public bool? CorEmpShowAnswers { get; set; }
        public bool? CorEmpShowCorrect { get; set; }
        public bool? CorEmpAllowRetake { get; set; }
        public int? CorEmpRetakeCount { get; set; }
        public bool? CorEmpAutoReleaseRetake { get; set; }

        public virtual LktblSupplier Su { get; set; }
        public virtual ICollection<RsTblCoursesSkillsKnowledge> RsTblCoursesSkillsKnowledges { get; set; }
        public virtual ICollection<RsTblCoursesTask> RsTblCoursesTasks { get; set; }
        public virtual ICollection<TblClass> TblClasses { get; set; }
        public virtual ICollection<TblContentObjectPresentation> TblContentObjectPresentations { get; set; }
        public virtual ICollection<TblCourseSegment> TblCourseSegments { get; set; }
        public virtual ICollection<TblIdp> TblIdps { get; set; }
        public virtual ICollection<TblIlaresource> TblIlaresources { get; set; }
        public virtual ICollection<TblPerspectiveCourse> TblPerspectiveCourses { get; set; }
        public virtual ICollection<TblSafetyHazardIla> TblSafetyHazardIlas { get; set; }
        public virtual ICollection<TblTest> TblTests { get; set; }
        public virtual ICollection<TblTrainingModuleIla> TblTrainingModuleIlas { get; set; }
    }
}
