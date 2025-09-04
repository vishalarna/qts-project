using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class ProvidersMap : Common.MigrationMap<LktblSupplier, Provider>
    {
        List<TblImage> _images;
        List<LktblSupplier> _providers;
        List<LkTblIlaproviderStatus> _providerStatuses;
        List<TblClass> _classes;
        List<TblCourse> _courses;
        List<RsTblClassStudent> _students;
        List<RsTblCoursesSkillsKnowledge> _skillsKnowleges;
        List<TblEmpsetting> _empSettings;
        List<TblObjectivesUserAdd> _customObjectives;
        List<TblTest> _tests;
        List<TblIlaresource> _resources;
        List<LktblAnnualTrainingRequirement> _additionalCertsInfo;
        List<TblLabelReplacementText> _labelReplacements;
        List<TblIlaNercstandard> _ilaNercStandards;
        List<TblNercstandard> _nercStandards;

        List<DeliveryMethod> _deliveryMethods;
        List<NERCTargetAudience> _targetAudiences;
        List<RegulatoryRequirement> _regulatoryRequirements;

        List<ProviderLevel> usedProviderStatuses = new List<ProviderLevel>();

        public ProvidersMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<LktblSupplier> getSourceRecords()
        {
            _images = (_source as EMP_DemoContext).TblImages.ToList();
            _providers = (_source as EMP_DemoContext).LktblSuppliers.ToList();
            _providerStatuses = (_source as EMP_DemoContext).LkTblIlaproviderStatuses.ToList();
            _classes = (_source as EMP_DemoContext).TblClasses.ToList();
            _courses = (_source as EMP_DemoContext).TblCourses.ToList();
            _students = (_source as EMP_DemoContext).RsTblClassStudents.ToList();
            _labelReplacements = (_source as EMP_DemoContext).TblLabelReplacementTexts.ToList();
            _skillsKnowleges = (_source as EMP_DemoContext).RsTblCoursesSkillsKnowledges.ToList();
            _empSettings = (_source as EMP_DemoContext).TblEmpsettings.ToList();
            _customObjectives = (_source as EMP_DemoContext).TblObjectivesUserAdds.ToList();
            _tests = (_source as EMP_DemoContext).TblTests.ToList();
            _resources = (_source as EMP_DemoContext).TblIlaresources.ToList();
            _additionalCertsInfo = (_source as EMP_DemoContext).LktblAnnualTrainingRequirements.ToList();
            _ilaNercStandards = (_source as EMP_DemoContext).TblIlaNercstandards.ToList();
            _nercStandards = (_source as EMP_DemoContext).TblNercstandards.ToList();

            _deliveryMethods = (_target as QTD2.Data.QTDContext).DeliveryMethods.ToList();
            _targetAudiences = (_target as QTD2.Data.QTDContext).NERCTargetAudiences.ToList();
            _regulatoryRequirements = (_target as QTD2.Data.QTDContext).RegulatoryRequirements.ToList();

            return _providers;
        }

        protected override Provider mapRecord(LktblSupplier obj)
        {
            var repSig = _images.Where(r => r.Imtype == 1).Where(r => r.ImparentId == obj.Suid).FirstOrDefault();

            return new Provider()
            {
                Active = obj.Inactive.HasValue ? !obj.Inactive.Value : true,
                Name = obj.Suname,
                //ProviderLevelId,
                Number = obj.Nercid,
                ContactName = obj.ContactPerson,
                ContactTitle = obj.CpTitle,
                ContactPhone = obj.CpPhone,
                ContactMobile = obj.Cell,
                ContactEmail = obj.CpEmail,
                CompanyWebsite = obj.CpWebsite,
                RepName = obj.TprName,
                RepTitle = obj.TprTitle,
                RepPhone = obj.TprPhone,
                RepEmail = obj.TprEmail,
                RepSignature = repSig == null ? null : "data:image/png;base64," +Convert.ToBase64String(repSig.Imbody),
                IsNERC = obj.Nercid != null ? true : false,
                //IsPriority
                Deleted = false,
                ILAs = getILAs(obj),
                ProviderLevelId = 3,
                IsPriority = obj.DefaultProvider,
            };
        }

        private ProviderLevel getProviderLevel(LktblSupplier obj)
        {
            if (obj.ProviderStatus == null) return null;

            var sourceStatus = _providerStatuses.Where(r => r.StatusId == obj.ProviderStatus).First();
            var targetStatus = usedProviderStatuses.Where(r => r.Name == sourceStatus.StatusDesc).FirstOrDefault();

            if (targetStatus != null) return targetStatus;

            var level = new ProviderLevel()
            {
                Name = sourceStatus.StatusDesc
            };

            usedProviderStatuses.Add(level);

            return level;
        }

        private ICollection<ILA> getILAs(LktblSupplier obj)
        {
            List<ILA> iLAs = new List<ILA>();
            var courses = _courses.Where(r => r.Suid == obj.Suid).ToList();
            foreach (var ila in courses)
            {
                var ilaDetails = (_source as EMP_DemoContext).TblIlaDetails.Where(r => r.Corid == ila.Corid).FirstOrDefault();

                var traineeEvals = getIlaTraineeEvaluations(ila);

                iLAs.Add(new ILA()
                {
                    Active = !ila.Type10,
                    Deleted = false,
                    Name = ila.Cordesc,
                    NickName = ila.Cornum,
                    //image
                    //ProviderId
                    //TopicId 
                    IsPublished = true,
                    PublishDate = DateTime.Now.ToQtsTime(false),
                    HasPilotData = ila.ActMayBeUsedAsPdh,
                    ILA_RegRequirement_Links = getRegulatoryRequirements(ila),
                    //IsProgramManual
                    SubmissionDate = ila.CehAppDate.HasValue ? DateOnly.FromDateTime(ila.CehAppDate.Value) : null,
                    //EffectiveDate
                    TrainingEvalMethods = ila.EvaluationMethod,
                    DeliveryMethod = getDeliveryMethod(ila), //ila.SelfPased ? (ila.OnlineClass.GetValueOrDefault() ? _deliveryMethods.Where(r => r.Name == "Computer Based Training (CBT)").First() : _deliveryMethods.Where(r => r.Name == "Self-Study (e.g., paperbased)").First()) : _deliveryMethods.Where(r => r.Name == "Classroom").First(),
                    Number = ila.Cornum,
                    Description = ila.Cordesc,
                    TrainingPlan = (ila.TrainingPlan ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(ila.TrainingPlan ?? "") : ila.TrainingPlan,
                    IsSelfPaced = ila.SelfPased,
                    IsOptional = ila.IsOptional ?? false,
                    //DeliveryMethodId = ila.DeliveryMethodId,
                    ApprovalDate = ila.CehApprovalDate.HasValue ? DateOnly.FromDateTime(ila.CehApprovalDate.Value) : null,
                    ExpirationDate = ila.CorexpDate.HasValue ? DateOnly.FromDateTime(ila.CorexpDate.Value) : null,
                    ILA_EnablingObjective_Links = getILA_EnablingObjective_Links(ila),
                    //ILA_Topic = getILA_Topic(ila),
                    Task_ILA_Links = getTasks(ila),
                    ILA_TaskObjective_Links = getTaskObjectiveLinks(ila),
                    ILA_SafetyHazard_Links = getSaftyHazardLinks(ila),
                    ClassSchedules = getClassSchedules(ila, traineeEvals), 
                    ILA_TrainingTopic_Links = getTrainingTopicLinks(ila),
                    StartDate = ilaDetails == null ? DateOnly.FromDateTime(DateTime.MinValue) : ilaDetails.StartDate1.HasValue ? DateOnly.FromDateTime(ilaDetails.StartDate1.Value) : null,
                    ILATraineeEvaluations = getIlaTraineeEvaluations(ila),
                    ILACertificationLinks = getIlaCertificationLinks(ila),
                    EvaluationReleaseEMPSetting = getSettings(ila),
                    TotalTrainingHours = ila.TotalHours,
                    ILACustomObjective_Links = getIlaCustomObjectiveLinks(ila),
                    TestReleaseEMPSettings = getTestReleaseSettings(ila, traineeEvals),
                    ILA_NERCAudience_Links = getAudiences(ila),
                    TQILAEmpSettings = getTqIlaSettings(ila),
                    DoesActivityConform = ila.ActMayBeUsedByCso,
                    // WriitenOrOnlineAssessmentTool = ila.on
                    // PerformAssessmentTool = 
                    // OtherAssesmentTool = ""
                    Prerequisites = ila.Prerequisites,
                    ILA_Resources = getResources(ila),
                    WriitenOrOnlineAssessmentTool = ilaDetails?.WrittenTestQuiz,
                    PerformAssessmentTool = ilaDetails?.PerformanceDemonstration,
                    OtherAssesmentTool = ilaDetails?.OtherTypeAssmtText,
                    PilotDataNA = ila.LerningActivitySelfStudyNa
                });
            }
            return iLAs;
        }

        private ICollection<ILA_RegRequirement_Link> getRegulatoryRequirements(TblCourse ila)
        {
            List<ILA_RegRequirement_Link> links = new List<ILA_RegRequirement_Link>();

            var sourceLinks = _ilaNercStandards.Where(r => r.Corid == ila.Corid);

            foreach (var sourceLink in sourceLinks)
            {
                var sourceNercStandard = _nercStandards.Where(r => r.Nsid == sourceLink.Nsid).First();
                var targetRR = _regulatoryRequirements.Where(r => r.Title == sourceNercStandard.Nsname).First();

                links.Add(new ILA_RegRequirement_Link()
                {
                    Active = true,
                    RegulatoryRequirementId = targetRR.Id
                });
            }

            return links;
        }

        private ICollection<ILA_Resource> getResources(TblCourse ila)
        {
            List<ILA_Resource> resources = new List<ILA_Resource>();

            var source = _resources.Where(r => r.Corid == ila.Corid);

            foreach (var resource in source)
            {
                resources.Add(new ILA_Resource()
                {
                    Active = true,
                    Chapter = resource.Ilarchapter,
                    Comments = resource.Ilarcomments,
                    Deleted = false,
                    Hyperlink = resource.Ilarhyperlink,
                    HyperlinkText = resource.IlarhyperlinkText,
                    ResourceNumber = resource.Ilarnum,
                    Section = resource.Ilarsection,
                    Title = resource.Ilarcomments
                });
            }

            return resources;
        }

        private ICollection<TQILAEmpSetting> getTqIlaSettings(TblCourse ila)
        {
            List<TQILAEmpSetting> settings = new List<TQILAEmpSetting>();

            var signOffsRequired = _empSettings.Where(r => r.EmpsettingDesc.ToUpper() == "TaskQual_SignoffsRequired".ToUpper()).FirstOrDefault();
            settings.Add(new TQILAEmpSetting()
            {
                Active = true,
                Deleted = false,
                ReleaseAtOnce = false,
                ReleaseOneAtTime = false,
                EmpSettingsReleaseTypeId = 1,
                TQDueDate = 30,
                MultipleSignOffRequired = signOffsRequired == null ? 0 : Convert.ToInt32(signOffsRequired.EmpsettingValue),
                OneSignOffRequired = signOffsRequired == null ? false : Convert.ToInt32(signOffsRequired.EmpsettingValue) == 1
            });

            return settings;
        }

        private ICollection<ILA_NERCAudience_Link> getAudiences(TblCourse ila)
        {
            List<ILA_NERCAudience_Link> audiences = new List<ILA_NERCAudience_Link>();

            if (ila.TargetAudienceBio)
                audiences.Add(new ILA_NERCAudience_Link()
                {
                    NERCTargetAudience = _targetAudiences.Where(r => r.Name == "Balancing and Interchange Operator").First()
                });

            if (ila.TargetAudienceCrs)
                audiences.Add(new ILA_NERCAudience_Link()
                {
                    NERCTargetAudience = _targetAudiences.Where(r => r.Name == "Control Room Supervision/Management and Support Staff").First()
                });

            if (ila.TargetAudienceGo)
                audiences.Add(new ILA_NERCAudience_Link()
                {
                    NERCTargetAudience = _targetAudiences.Where(r => r.Name == "Generator Operator").First()
                });


            if (ila.TargetAudienceMo)
                audiences.Add(new ILA_NERCAudience_Link()
                {
                    NERCTargetAudience = _targetAudiences.Where(r => r.Name == "Market Operator").First()
                });

            if (ila.TargetAudienceOpe)
                audiences.Add(new ILA_NERCAudience_Link()
                {
                    NERCTargetAudience = _targetAudiences.Where(r => r.Name == "Operator And Planning Engineers").First()
                });

            if (ila.TargetAudienceRc)
                audiences.Add(new ILA_NERCAudience_Link()
                {
                    NERCTargetAudience = _targetAudiences.Where(r => r.Name == "Reliability Operator").First()
                });

            if (ila.TargetAudienceTo)
                audiences.Add(new ILA_NERCAudience_Link()
                {
                    NERCTargetAudience = _targetAudiences.Where(r => r.Name == "Transimission Operator").First()
                });

            if (ila.TargetAudienceOther)
            {
                var targetAudience = _targetAudiences.Where(r => r.Name == ila.TargetAudienceOtherSpecify).FirstOrDefault();

                if (targetAudience != null)
                {
                    audiences.Add(new ILA_NERCAudience_Link()
                    {
                        NERCTargetAudience = targetAudience
                    });
                }
            }

            return audiences;
        }

        private DeliveryMethod getDeliveryMethod(TblCourse ila)
        {
            if (ila.TypeClassroom) return _deliveryMethods.Where(r => r.Name == "Classroom").First();
            if (ila.TypeWorkshop) return _deliveryMethods.Where(r => r.Name == "Workshop").First();
            if (ila.TypeConference) return _deliveryMethods.Where(r => r.Name == "Conferences").First();
            if (ila.TypeOjttraining) return _deliveryMethods.Where(r => r.Name == "On-the-Job Training (OJT)").First();
            if (ila.TypeSelfStudy) return _deliveryMethods.Where(r => r.Name == "Self-Study (e.g., paperbased)").First();
            if (ila.TypeInternetBased) return _deliveryMethods.Where(r => r.Name == "Online and distance - learning activities").First();
            if (ila.TypeOtsimulation) return _deliveryMethods.Where(r => r.Name == "Operator Training Simulations").First();
            if (ila.TypeComputerBased) return _deliveryMethods.Where(r => r.Name == "Computer Based Training (CBT)").First();
            if (ila.TypeOther) return _deliveryMethods.Where(r => r.Name.ToUpper() == ila.TypeOtherSpecify.ToUpper()).FirstOrDefault();

            return null;
        }

        private ICollection<ILACustomObjective_Link> getIlaCustomObjectiveLinks(TblCourse ila)
        {
            List<ILACustomObjective_Link> links = new List<ILACustomObjective_Link>();

            var customObjectives = _customObjectives.Where(r => r.ObjCorid == ila.Corid);

            foreach (var customObjective in customObjectives)
            {
                links.Add(new ILACustomObjective_Link()
                {
                    CustomEnablingObjective = getCustomObjective(customObjective)
                });
            }

            return links;
        }

        private CustomEnablingObjective getCustomObjective(TblObjectivesUserAdd customObjective)
        {
            return new CustomEnablingObjective()
            {
                Active = true,
                CustomEONumber = customObjective.ObjId,
                IsAddtoEO = customObjective.ObjIsAdded.GetValueOrDefault(),
                Deleted = false,
                Description = customObjective.ObjText
            };
        }

        private ClassSchedule_TestReleaseEMPSetting getClassScheduleTestReleaseSettings(TblCourse ila, ICollection<ILATraineeEvaluation> traineeEvals)
        {

            int devaultEvalTimePeroid = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultEvalTimePeriod").First().EmpsettingValue);
            int testCutScore = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestCutScore").First().EmpsettingValue);
            int defaultTestTimePeriod = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestTimePeriod").First().EmpsettingValue);
            //int defaultTestReleaseTime = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestReleaseTime").First().EmpsettingValue);
            //int courseAvailableStart = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "CourseAvailableStart").First().EmpsettingValue);

            int courseAvailableEnd = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "CourseAvailableEnd").First().EmpsettingValue);

            bool CorEMP_ShowAnswers = ila.CorEmpShowAnswers.GetValueOrDefault();
            bool CorEMP_Showcorrect = ila.CorEmpShowCorrect.GetValueOrDefault();
            bool CorEMP_AllowRetake = ila.CorEmpAllowRetake.GetValueOrDefault();
            int CorEMP_RetakeCount = ila.CorEmpRetakeCount.GetValueOrDefault();
            bool CorEMP_AutoReleaseRetake = ila.CorEmpAutoReleaseRetake.GetValueOrDefault();

            var hasTests = _tests.Where(r => r.Corid == ila.Corid).Count() > 0;


            ClassSchedule_TestReleaseEMPSetting settings = new ClassSchedule_TestReleaseEMPSetting()
            {
                AutoReleaseRetake = CorEMP_AutoReleaseRetake,
                MakeAvailableBeforeDays = 0,
                MakeFinalTestAvailableAfterCBTCompleted = false,
                MakeFinalTestAvailableImmediatelyAfterStartDate = false,
                MakeFinalTestAvailableOnClassEndDate = false,
                MakeFinalTestAvailableOnSpecificTime = 0,
                PreTestAvailableOneStartDate = false,
                PreTestAvailableOnEnrollment = true,
                ShowCorrectIncorrectFinalTestAnswers = CorEMP_Showcorrect,
                ShowCorrectIncorrectPreTestAnswers = CorEMP_Showcorrect,
                ShowCorrectIncorrectRetakeTestAnswers = CorEMP_Showcorrect,
                ShowStudentSubmittedFinalTestAnswers = CorEMP_Showcorrect,
                RetakeEnabled = CorEMP_AllowRetake,
                ShowStudentSubmittedPreTestAnswers = CorEMP_ShowAnswers,
                ShowStudentSubmittedRetakeTestAnswers = CorEMP_ShowAnswers,
                UsePreTestAndTest = hasTests,
                FinalTestDueDate = defaultTestTimePeriod,
                //FinalTestId
                PreTestRequired = false,
                PreTestScore = testCutScore,
                NumberOfRetakes = CorEMP_RetakeCount,
                FinalTestPassingScore = testCutScore.ToString(),
                FinalTestSpecificTimePrior = true,
                Active = true,
                EmpSettingsReleaseTypeId = 1
                //PreTestId = traineeEvals.Where(r => r.TestTypeId == 1).First().TestId,
                //FinalTestId = traineeEvals.Where(r => r.TestTypeId == 2).First().TestId
            };

            return settings;
        }

        private TestReleaseEMPSettings getTestReleaseSettings(TblCourse ila, ICollection<ILATraineeEvaluation> traineeEvals)
        {

            int devaultEvalTimePeroid = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultEvalTimePeriod").First().EmpsettingValue);
            int testCutScore = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestCutScore").First().EmpsettingValue);
            int defaultTestTimePeriod = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestTimePeriod").First().EmpsettingValue);
            //int defaultTestReleaseTime = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestReleaseTime").First().EmpsettingValue);
            //int courseAvailableStart = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "CourseAvailableStart").First().EmpsettingValue);

            int courseAvailableEnd = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "CourseAvailableEnd").First().EmpsettingValue);

            bool CorEMP_ShowAnswers = ila.CorEmpShowAnswers.GetValueOrDefault();
            bool CorEMP_Showcorrect = ila.CorEmpShowCorrect.GetValueOrDefault();
            bool CorEMP_AllowRetake = ila.CorEmpAllowRetake.GetValueOrDefault();
            int CorEMP_RetakeCount = ila.CorEmpRetakeCount.GetValueOrDefault();
            bool CorEMP_AutoReleaseRetake = ila.CorEmpAutoReleaseRetake.GetValueOrDefault();

            var hasTests = _tests.Where(r => r.Corid == ila.Corid).Count() > 0;


            TestReleaseEMPSettings settings = new TestReleaseEMPSettings()
            {
                AutoReleaseRetake = CorEMP_AutoReleaseRetake,
                MakeAvailableBeforeDays = 0,
                MakeFinalTestAvailableAfterCBTCompleted = false,
                MakeFinalTestAvailableImmediatelyAfterStartDate = false,
                MakeFinalTestAvailableOnClassEndDate = false,
                MakeFinalTestAvailableOnSpecificTime = 0,
                PreTestAvailableOneStartDate = false,
                PreTestAvailableOnEnrollment = true,
                ShowCorrectIncorrectFinalTestAnswers = CorEMP_Showcorrect,
                ShowCorrectIncorrectPreTestAnswers = CorEMP_Showcorrect,
                ShowCorrectIncorrectRetakeTestAnswers = CorEMP_Showcorrect,
                ShowStudentSubmittedFinalTestAnswers = CorEMP_Showcorrect,
                RetakeEnabled = CorEMP_AllowRetake,
                ShowStudentSubmittedPreTestAnswers = CorEMP_ShowAnswers,
                ShowStudentSubmittedRetakeTestAnswers = CorEMP_ShowAnswers,
                UsePreTestAndTest = hasTests,
                FinalTestDueDate = defaultTestTimePeriod,
                //FinalTestId
                PreTestRequired = false,
                PreTestScore = testCutScore,
                NumberOfRetakes = CorEMP_RetakeCount,
                FinalTestPassingScore = testCutScore.ToString(),
                FinalTestSpecificTimePrior = true,
                Active = true,
                EmpSettingsReleaseTypeId = 1
                //PreTestId = traineeEvals.Where(r => r.TestTypeId == 1).First().TestId,
                //FinalTestId = traineeEvals.Where(r => r.TestTypeId == 2).First().TestId
            };

            return settings;
        }

        private EvaluationReleaseEMPSettings getSettings(TblCourse ila)
        {
            //Hours, Weeks, Days.
            //between 0-24hrs -> 1 day, 24-48s -> 2 days so on
            //weesk = 7*value
            var defaultEvalTimePeriodRecord = _empSettings.Where(r => r.EmpsettingDesc.ToUpper() == "DefaultEvalTimePeriod".ToUpper()).FirstOrDefault();


            int days = 0;

            if (defaultEvalTimePeriodRecord.EmpsettingUnit.ToUpper() == "HOURS")
            {
                days = (int)Math.Ceiling(TimeSpan.FromHours(Convert.ToInt32(defaultEvalTimePeriodRecord.EmpsettingValue)).TotalDays);
            }
            else if (defaultEvalTimePeriodRecord.EmpsettingUnit.ToUpper() == "DAYS")
            {
                days = Convert.ToInt32(defaultEvalTimePeriodRecord.EmpsettingValue);
            }
            else if (defaultEvalTimePeriodRecord.EmpsettingUnit.ToUpper() == "WEEKS")
            {
                days = Convert.ToInt32(defaultEvalTimePeriodRecord.EmpsettingValue) * 7;
            }

            EvaluationReleaseEMPSettings settings = new EvaluationReleaseEMPSettings()
            {
                Active = true,
                EvaluationAvailableOnEndDate = false,
                EvaluationAvailableOnStartDate = false,
                EvaluationDueDate = days,
                EvaluationRequired = false,
                ReleaseAfterGradeAssigned = true,
                ReleaseAfterEndTime = 0,
                ReleaseOnSpecificTimeAfterClassEndDate = false,
                EvaluationUsedToDeployStudentEvaluation = true,
                FinalGradeRequired = false,
                EmpSettingsReleaseTypeId = 1,
                ReleasePrior = false
            };

            return settings;
        }

        private ICollection<ILACertificationLink> getIlaCertificationLinks(TblCourse ila)
        {
            List<ILACertificationLink> links = new List<ILACertificationLink>();

            var certs = (_target as QTD2.Data.QTDContext).Certifications.ToList();

            foreach (var cert in certs.Where(r => r.CertifyingBodyId == 1))
            {
                links.Add(new ILACertificationLink()
                {
                    CertificationId = cert.Id,
                    ILACertificationSubRequirementLink = getIlaCertificationSubRequirementLInks(ila, cert),
                    IsEmergencyOpHours = ila.TopicsEo,
                    IsPartialCreditHours = ila.ActPartialCredits,
                    IsIncludeSimulation = ila.ReginalExerciseIncluded,
                    CEHHours = ila.TotalCeh.HasValue ? ila.TotalCeh.Value : null
                });
            }

            string legacyName = "Emergency Response";
            var labelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == legacyName.ToUpper()).FirstOrDefault();

            string name = labelreplacement == null ? legacyName : labelreplacement.ReplacementText;

            var emergencyResponseCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == name).First();

            links.Add(new ILACertificationLink()
            {
                CertificationId = emergencyResponseCert.Id,
                CEHHours = ila.EmergencyOpsHours.HasValue ?ila.EmergencyOpsHours.Value : null
            });


            string regLegacyName = "Reg";
            var regLabelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == regLegacyName.ToUpper()).FirstOrDefault();

            string regName = labelreplacement == null ? regLegacyName : regLabelreplacement.ReplacementText;

            var regCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == regName).First();

            links.Add(new ILACertificationLink()
            {
                CertificationId = regCert.Id,
                CEHHours = ila.CehReg.HasValue ? ila.CehReg.Value : null
            });


            string reg2LegacyName = "Reg2";
            var reg2Labelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == reg2LegacyName.ToUpper()).FirstOrDefault();

            string reg2Name = reg2Labelreplacement == null ? reg2LegacyName : reg2Labelreplacement.ReplacementText;

            var reg2Cert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == reg2Name).First();

            links.Add(new ILACertificationLink()
            {
                CertificationId = reg2Cert.Id,
                CEHHours = ila.Reg2.HasValue ? ila.Reg2.Value : null
            });

            string otherLegacyName = "Other";
            var otherLabelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == otherLegacyName.ToUpper()).FirstOrDefault();

            string otherName = otherLabelreplacement == null ? otherLegacyName : otherLabelreplacement.ReplacementText;

            var otherCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == otherName).First();

            links.Add(new ILACertificationLink()
            {
                CertificationId = otherCert.Id,
                CEHHours = ila.Other.HasValue ? ila.Other.Value : null
            });

            for (int i = 4; i <= 20; i++)
            {
                string fieldName = "Ceh" + i.ToString();
                var field = ila.GetType().GetProperty(fieldName).GetValue(ila, null);
                double? value = field == null ? default(double?) : Convert.ToDouble(field);

                if (value.HasValue)
                {
                    var sourceCert = _additionalCertsInfo.Where(r => r.TrainingTypeId == i).FirstOrDefault();
                    if (sourceCert == null) continue;

                    var trainingType = sourceCert.TrainingType;

                    var targetCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == trainingType).First();

                    links.Add(new ILACertificationLink()
                    {
                        CertificationId = targetCert.Id,
                        CEHHours = value
                    });
                }
            }

            string proflegacyName = "Professional";
            var proflabelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == proflegacyName.ToUpper()).FirstOrDefault();

            string profName = proflabelreplacement == null ? proflegacyName : proflabelreplacement.ReplacementText;

            var profCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == profName).First();

            links.Add(new ILACertificationLink()
            {
                CertificationId = profCert.Id,
                CEHHours = ila.CehProf.HasValue ? ila.CehProf.Value : null
            });

            return links;
        }

        private ICollection<ILACertificationSubRequirementLink> getIlaCertificationSubRequirementLInks(TblCourse ila, Certification cert)
        {
            List<ILACertificationSubRequirementLink> links = new List<ILACertificationSubRequirementLink>();

            var standards = cert.CertificationSubRequirements.Where(r => r.ReqName == "Standards").First();
            var sims = cert.CertificationSubRequirements.Where(r => r.ReqName == "Simulations").First();
            //var operations = cert.CertificationSubRequirements.Where(r => r.ReqName == "Operation CEHs").First();

            links.Add(new ILACertificationSubRequirementLink()
            {
                CertificationSubRequirementId = standards.Id,
                ReqHour = ila.CehNerc.GetValueOrDefault()
            });

            links.Add(new ILACertificationSubRequirementLink()
            {
                CertificationSubRequirementId = sims.Id,
                ReqHour =ila.SimHours.GetValueOrDefault()
            });

            //links.Add(new ILACertificationSubRequirementLink()
            //{
            //    CertificationSubRequirementId = operations.Id,
            //    ReqHour = Convert.ToInt32(ila.TotalCeh.GetValueOrDefault())
            //});

            return links;
        }

        private ICollection<ILA_SafetyHazard_Link> getSaftyHazardLinks(TblCourse ila)
        {
            List<ILA_SafetyHazard_Link> links = new List<ILA_SafetyHazard_Link>();

            return links;
        }

        private ICollection<ILA_TaskObjective_Link> getTaskObjectiveLinks(TblCourse ila)
        {
            List<ILA_TaskObjective_Link> links = new List<ILA_TaskObjective_Link>();

            foreach (var task in ila.RsTblCoursesTasks)
            {
                var sourceTask = task.TidNavigation;
                var sourceDutyArea = sourceTask.Da;
                var targetTask = (_target as QTD2.Data.QTDContext)
                    .DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First()
                    .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                    .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

                links.Add(new ILA_TaskObjective_Link()
                {
                    TaskId = targetTask.Id
                });
            }

            return links;
        }

        private ICollection<Task_ILA_Link> getTasks(TblCourse ila)
        {
            List<Task_ILA_Link> taskLinks = new List<Task_ILA_Link>();

            foreach (var task in ila.RsTblCoursesTasks)
            {
                var sourceTask = task.TidNavigation;
                var sourceDutyArea = sourceTask.Da;
                var targetTask = (_target as QTD2.Data.QTDContext)
                    .DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First()
                    .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                    .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

                taskLinks.Add(new Task_ILA_Link()
                {
                    TaskId = targetTask.Id
                });
            }

            return taskLinks;
        }

        private ICollection<ILATraineeEvaluation> getIlaTraineeEvaluations(TblCourse ila)
        {
            List<ILATraineeEvaluation> evaluations = new List<ILATraineeEvaluation>();

            //evaluations.Add(new ILATraineeEvaluation()
            //{
            //    EvaluationTypeId = 1,
            //    Active = true,
            //    TestId = 
            //});

            return evaluations;
        }

        private ICollection<ILA_TrainingTopic_Link> getTrainingTopicLinks(TblCourse ila)
        {
            List<ILA_TrainingTopic_Link> links = new List<ILA_TrainingTopic_Link>();

            var trainingTopics = (_target as QTD2.Data.QTDContext).TrainingTopics.ToList();
            var sourceTrainingTopic = (_source as EMP_DemoContext).TblIlaTrainingTopics.Where(r => r.Corid == ila.Corid).FirstOrDefault();

            if (sourceTrainingTopic == null) return links;

            foreach (var trainingTopic in trainingTopics)
            {
                string topicName = trainingTopic.Name;
                string legacyTopicName = getLegacyTopicName(trainingTopic.Name);

                var prop = sourceTrainingTopic.GetType().GetProperty(legacyTopicName.Replace(" ", ""), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (prop == null) continue;

                var value = prop.GetValue(sourceTrainingTopic, null);

                links.Add(new ILA_TrainingTopic_Link()
                {
                    TrTopicId = trainingTopic.Id,
                    Active = Convert.ToBoolean(value)
                });
            }

            return links;
        }

        private string getLegacyTopicName(string name)
        {
            if (name.ToUpper() == "CAPACITANCE") return "Capacitance";
            if (name.ToUpper() == "INDUCTANCE") return "Inductance";
            if (name.ToUpper() == "IMPEDANCE") return "Impedance";
            if (name.ToUpper() == "REAL REACTIVE POWER") return "Real reactive power";
            if (name.ToUpper() == "ELECTRICAL CIRCUITS") return "Electrical circuits";
            if (name.ToUpper() == "MAGNETISM") return "Magnetism";
            if (name.ToUpper() == "BASIC TRIGONOMETRY") return "Trigonometry";
            if (name.ToUpper() == "RATIOS") return "Ratios";
            if (name.ToUpper() == "PER UNIT VALUES") return "Per unit";
            if (name.ToUpper() == "PYTHAGOREAN THEOREM") return "Pythagorean";
            if (name.ToUpper() == "OHM’S LAW") return "Ohms law";
            if (name.ToUpper() == "KIRCHOFF’S LAWS") return "Kirchhoffs law";
            if (name.ToUpper() == "TRANSMISSION LINES") return "Transmission";
            if (name.ToUpper() == "TRANSFORMERS") return "Transformers";
            if (name.ToUpper() == "SUBSTATIONS") return "Substations";
            if (name.ToUpper() == "POWER PLANTS") return "Power plants";
            if (name.ToUpper() == "PROTECTION") return "Protection";
            if (name.ToUpper() == "INTRODUCTION TO POWER SYSTEM OPERATIONS AND INTERCONNECTED OPERATIONS") return "Introduction";
            if (name.ToUpper() == "FREQUENCY") return "";
            if (name.ToUpper() == "TRANSMISSION LINES") return "Transmission principle";
            if (name.ToUpper() == "TRANSFORMERS") return "Transformers principle";
            if (name.ToUpper() == "BUSSES") return "Busses";
            if (name.ToUpper() == "GENERATORS") return "Generators";
            if (name.ToUpper() == "RELAYS AND PROTECTION SCHEMES") return "Relays";
            if (name.ToUpper() == "POWER SYSTEM FAULTS") return "Power system faults";
            if (name.ToUpper() == "SYNCHRONIZING EQUIPMENT") return "Syncronizing equipment";
            if (name.ToUpper() == "UNDER-FREQUENCY LOAD SHEDDING") return "";
            if (name.ToUpper() == "UNDER-VOLTAGE LOAD SHEDDING") return "Under voltage";
            if (name.ToUpper() == "COMMUNICATION SYSTEMS UTILIZED") return "Comm systems";
            if (name.ToUpper() == "VOLTAGE CONTROL") return "Voltage control";
            if (name.ToUpper() == "FREQUENCY CONTROL") return "Frequency control";
            if (name.ToUpper() == "POWER SYSTEM STABILITY") return "Stability";
            if (name.ToUpper() == "FACILITY OUTAGE BOTH PLANNED AND UNPLANNED") return "Outage";
            if (name.ToUpper() == "ENERGY ACCOUNTING") return "Energy accounting";
            if (name.ToUpper() == "INADVERTENT ENERGY") return "Inadvertent energy";
            if (name.ToUpper() == "TIME ERROR CONTROL") return "Time error";
            if (name.ToUpper() == "BALANCING OF LOAD AND RESOURCES") return "Balancing resources";
            if (name.ToUpper() == "LOSS OF GENERATION RESOURCE(S)") return "Generation loss";
            if (name.ToUpper() == "LOSS OF TRANSMISSION ELEMENT(S)") return "Transmission loss";
            if (name.ToUpper() == "OPERATING RESERVES") return "Operating reserves";
            if (name.ToUpper() == "CONTINGENCY RESERVES") return "Contingency reserves";
            if (name.ToUpper() == "LINE LOADING RELIEF") return "Line loading relief";
            if (name.ToUpper() == "LOAD SHEDDING") return "Load shedding";
            if (name.ToUpper() == "VOLTAGE AND REACTIVE FLOWS DURING EMERGENCIES") return "Emergencies";
            if (name.ToUpper() == "LOSS OF EMS") return "Ems loss";
            if (name.ToUpper() == "LOSS OF PRIMARY CONTROL CENTER") return "Primary control center loss";
            if (name.ToUpper() == "RESTORATION PHILOSOPHIES") return "Restoration philosophies";
            if (name.ToUpper() == "FACILITY RESTORATION PRIORITIES") return "Facility restoration priorities";
            if (name.ToUpper() == "BLACKSTART RESTORATION") return "Blackstart";
            if (name.ToUpper() == "STABILITY (ANGLE AND VOLTAGE)") return "Stability angle voltage";
            if (name.ToUpper() == "ISLANDING AND SYNCHRONIZING") return "Islanding and synchronizing";
            if (name.ToUpper() == "NAESB STANDARDS") return "Naesb";
            if (name.ToUpper() == "STANDARDS OF CONDUCT") return "Standards of conduct";
            if (name.ToUpper() == "TARIFFS") return "Tariffs";
            if (name.ToUpper() == "OASIS APPLICATIONS (TRANSMISSION RESERVATIONS)") return "Oasis";
            if (name.ToUpper() == "E-TAG APPLICATION") return "E tag";
            if (name.ToUpper() == "TRANSACTION SCHEDULING") return "Transaction scheduleing";
            if (name.ToUpper() == "MARKET APPLICATIONS") return "Market applications";
            if (name.ToUpper() == "INTERCHANGE") return "Interchange";
            if (name.ToUpper() == "SUPERVISORY CONTROL AND DATA ACQUISITION (SCADA)") return "Scada";
            if (name.ToUpper() == "AUTOMATIC GENERATION CONTROL (AGC) APPLICATION") return "Agc";
            if (name.ToUpper() == "POWER FLOW APPLICATION") return "Power flow";
            if (name.ToUpper() == "STATE ESTIMATOR APPLICATION") return "State estimator";
            if (name.ToUpper() == "CONTINGENCY ANALYSIS APPLICATION") return "Contingency analysis";
            if (name.ToUpper() == "P-V CURVES") return "Pv curves";
            if (name.ToUpper() == "LOAD FORECASTING APPLICATION") return "Forecasting";
            if (name.ToUpper() == "ENERGY ACCOUNTING APPLICATION") return "Energy accounting app";
            if (name.ToUpper() == "VOICE AND DATA COMMUNICATION SYSTEMS") return "Voice and data comms";
            if (name.ToUpper() == "DEMAND-SIDE MANAGEMENT PROGRAMS") return "Demand side";
            if (name.ToUpper() == "IDENTIFYING LOSS OF FACILITIES") return "Facilities loss";
            if (name.ToUpper() == "RECOGNIZING LOSS OF COMMUNICATION FACILITIES") return "Communications loss";
            if (name.ToUpper() == "RECOGNIZING TELEMETRY PROBLEMS") return "Telemetry problems";
            if (name.ToUpper() == "RECOGNIZING AND IDENTIFYING CONTINGENCY PROBLEMS") return "Contingency problems";
            if (name.ToUpper() == "PROPER COMMUNICATIONS (THREE-PART)") return "Proper communications";
            if (name.ToUpper() == "COMMUNICATION WITH APPROPRIATE ENTITIES INCLUDING THE RC") return "Appropriate communications";
            if (name.ToUpper() == "CYBER AND PHYSICAL SECURITY AND THREATS") return "Cyber security";
            if (name.ToUpper() == "REDUCING SYSTEM OPERATOR ERRORS THROUGH THE USE OF HPI TOOLS (SELF-CHECKING, PEER CHECKING, PLACE KEEPING AND PROCEDURE USE") return "";
            if (name.ToUpper() == "ISO/RTO OPERATIONAL AND EMERGENCY POLICIES AND PROCEDURES") return "Iso rto";
            if (name.ToUpper() == "REGIONAL OPERATIONAL AND EMERGENCY POLICIES AND PROCEDURES") return "Regional";
            if (name.ToUpper() == "COMPANY-SPECIFIC OPERATIONAL AND EMERGENCY POLICIES AND PROCEDURES") return "Company policies";
            if (name.ToUpper() == "APPLICATION AND/OR IMPLEMENTATION OF NERC RELIABILITY STANDARDS") return "Reliability standards";
            if (name.ToUpper() == "") return "";

            return "";
        }

        private ICollection<ClassSchedule> getClassSchedules(TblCourse ila, ICollection<ILATraineeEvaluation> traineeEvals)
        {
            List<ClassSchedule> classSchedules = new List<ClassSchedule>();

            foreach (var c in _classes.Where(r => r.Corid == ila.Corid))
            {
                    int? instructorId = c.Inid.HasValue ? (_target as QTD2.Data.QTDContext).Instructors.Where(r => r.InstructorName == c.In.Inname).First().Id : null;
                int? locationId = c.Lcid.HasValue ? (_target as QTD2.Data.QTDContext).Locations.Where(r => r.LocName == c.Lc.Lcdesc).First().Id : null;

                int startAMPM = c.StartAmPm.HasValue ? c.StartAmPm.Value : -1;
                int endAMPM = c.EndAmPm.HasValue ? c.EndAmPm.Value : -1;

                string startTime = c.StartTimeStr;
                string endTime = c.EndTimeStr;

                string[] startTimeParts = (startTime ?? "").Split(":");
                string[] endTimeParts = (endTime ?? "").Split(":");

                int starTimeHours = (startAMPM == -1 || startTimeParts.Length == 1) ? 0 :
                         startAMPM == 0 ?
                            startTimeParts[0] == "12" ? 0 : Convert.ToInt32(startTimeParts[0])
                         :
                            startTimeParts[0] == "12" ? 12 : 12 + Convert.ToInt32(startTimeParts[0]);


                int endTimeHours = (endAMPM == -1 || endTimeParts.Length == 1) ? 0 :
                         endAMPM == 0 ?
                            endTimeParts[0] == "12" ? 0 : Convert.ToInt32(endTimeParts[0])
                         :
                            endTimeParts[0] == "12" ? 12 : 12 + Convert.ToInt32(endTimeParts[0]);

                int startTimeMinutes = startTimeParts.Length == 2 ? Convert.ToInt32(startTimeParts[1]) : 0;
                int endTimeMinutes = endTimeParts.Length == 2 ? Convert.ToInt32(endTimeParts[1]) : 0;

                DateTime classStartDate;

                if (ila.SelfPased)
                {
                    var student = _students.Where(r => r.Clid == c.Clid).FirstOrDefault();

                    if (student == null || !student.SecondDate.HasValue)
                    {
                        classStartDate = c.ClstartDate == null ? c.Cldate.GetValueOrDefault() : c.ClstartDate.GetValueOrDefault();
                    }
                    else
                    {
                        classStartDate = student.SecondDate.Value;
                    }
                }
                else
                {
                    classStartDate = c.StartDate == null ? c.ClstartDate.GetValueOrDefault() : c.StartDate.GetValueOrDefault();
                }

                classStartDate = classStartDate.Hour == 0 ? classStartDate.AddHours(starTimeHours).AddMinutes(startTimeMinutes) : classStartDate;
                
                DateTime classEndTime = c.Cldate.GetValueOrDefault();
                classEndTime = classEndTime.Hour == 0 ? classEndTime.AddHours(endTimeHours).AddMinutes(endTimeMinutes) : classEndTime;

                classSchedules.Add(new ClassSchedule()
                {
                    ClassSchedule_Employee = getClassScheduleEmployees(c),
                    StartDateTime = classStartDate.ToQtsTime(false),
                    EndDateTime = classEndTime.ToQtsTime(false),
                    InstructorId = instructorId,
                    LocationId = locationId,
                    Notes = c.Clnote1 + Environment.NewLine + c.Clnote2,
                    IsRecurring = false,
                    SpecialInstructions = c.Clid.ToString(),
                    ProviderID = null,
                    ClassSchedule_TestReleaseEMPSettings = getClassScheduleTestReleaseSettings(ila, traineeEvals)
                });
            }

            return classSchedules;
        }

        private ICollection<ClassSchedule_Employee> getClassScheduleEmployees(TblClass c)
        {
            List<ClassSchedule_Employee> employees = new List<ClassSchedule_Employee>();

            foreach (var student in _students.Where(r => r.Clid == c.Clid))
            {
                var targetEmployee = (_target as QTD2.Data.QTDContext).Employees.Where(r => r.Person.FirstName == student.EidNavigation.EfirstName).Where(r => r.Person.LastName == student.EidNavigation.ElastName).First();

                employees.Add(new ClassSchedule_Employee()
                {
                    CBTStatusId = 1,
                    EmployeeId = targetEmployee.Id,
                    IsEnrolled = true,
                    IsDenied = false,
                    IsDropped = false,
                    IsWaitlisted = false,
                    FinalGrade = student.CompGrade,
                    FinalScore = Convert.ToInt32(student.Score),
                    PreTestStatusId = 1,
                    TestStatusId = 1,
                    RetakeStatusId = 1,
                    CompletionDate = student.CompGrade == null ? 
                            null :
                            (c.Cldate.HasValue && c.Cldate.Value.Minute == 0 && c.Cldate.Value.Second == 0 && c.Cldate.Value.Millisecond == 0 ? 
                                new DateTime(c.Cldate.Value.Year, c.Cldate.Value.Month, c.Cldate.Value.Day, 12, 0, 0, DateTimeKind.Utc)
                                : c.Cldate.ToQtsTime(false))
                });
            }

            return employees;
        }

        private ICollection<ILA_EnablingObjective_Link> getILA_EnablingObjective_Links(TblCourse obj)
        {
            List<ILA_EnablingObjective_Link> iLA_EnablingObjective_Links = new List<ILA_EnablingObjective_Link>();

            var eos = _skillsKnowleges.Where(r => r.Corid == obj.Corid);

            foreach (var enablingobjective in eos)
            {
                var sourceEo = enablingobjective.Sk;

                if (sourceEo == null) continue;

                var sourceCateogry = (_source as EMP_DemoContext).TblCategories.Where(r => r.Cid == sourceEo.Cid).First();

                EnablingObjective targetEo = new EnablingObjective();
                if (enablingobjective.Sk.Sknum == 0)
                {
                    targetEo = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == sourceCateogry.Cnum).First()
                                                    .EnablingObjectives.Where(r => r.Number == sourceEo.SksubNum.Value.ToString()).First();
                }
                else if (sourceEo.SksubNum.GetValueOrDefault() == 0)
                {
                    //07/07/2023
                    //these are topics links to ILAs which don't have a home yet
                    continue;
                }
                else
                {
                    targetEo = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == sourceCateogry.Cnum).First()
                                .EnablingObjective_SubCategories.Where(r => r.Number == sourceCateogry.CsubNum).First()
                                .EnablingObjective_Topics.Where(r => r.Number == sourceEo.Sknum).First()
                                .EnablingObjectives.Where(r => r.Number == sourceEo.SksubNum.Value.ToString()).First();
                }


                iLA_EnablingObjective_Links.Add(new ILA_EnablingObjective_Link()
                {
                    Active = true,
                    Deleted = false,
                    //ILAId
                    EnablingObjectiveId = targetEo.Id
                });
            }
            return iLA_EnablingObjective_Links;
        }

        //private ILA_Topic getILA_Topic(TblCourse obj)
        //{
        //    var rstbl = obj.RsTblCoursesSkillsKnowledges.Where(r => !r.Sk.SksubNum.HasValue || r.Sk.SksubNum == 0).FirstOrDefault();

        //    if (rstbl == null) return null;

        //    var sourceTopic = rstbl.Sk;

        //    var sourceCateogry = (_source as EMP_DemoContext).TblCategories.Where(r => r.Cid == sourceTopic.Cid).First();
        //    var targeTopic = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == sourceCateogry.Cnum).First()
        //                    .EnablingObjective_SubCategories.Where(r => r.Number == sourceCateogry.CsubNum).First()
        //                    .EnablingObjective_Topics.Where(r => r.Number == sourceTopic.Sknum).First();

        //    return new ILA_Topic
        //    {
        //        //IsPriority
        //        Name = targeTopic.Title,
        //        Active = true,
        //        Deleted = false,
        //    };
        //}

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _providers.Count();
        }

        protected override void updateTarget(Provider record)
        {
            (_target as QTD2.Data.QTDContext).Providers.Add(record);
        }

    }
}
