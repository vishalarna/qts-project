using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Vision.Data;
using System.ComponentModel;
using RtfPipe.Model;
using Microsoft.Identity.Client;
using QTD2.Data.Migrations.QTD;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class ProvidersMap : Common.MigrationMap<ProgramImpl, Provider>
    {
        int _projectId;

        List<ProgramImpl> _sourceProviders;
        List<ProgramHierarchy> _sourceProgramHeirachy;
        List<ProgramImpl> _sourceIlas;
        List<ProgramComment> _sourceProgramComments;

        List<LsLearningEventLearner> _sourceLsLearnerEventLearners;
        List<LsLearningEventProgram> _sourceLsLearningEventPrograms;

        List<Sequencing> _sourceSequencing;

        List<XrefLibLink> _sourceReferenceLinks;

        List<DeliveryMethod> _deliveryMethods;
        List<NERCTargetAudience> _targetAudiences;
        List<RegulatoryRequirement> _regulatoryRequirements;

        List<ProviderLevel> usedProviderStatuses = new List<ProviderLevel>();

        List<TrainingTopic> _targetTrainingTopics;
        List<EnablingObjective> _targetEnablingObjectives;
        List<Task> _targetTasks;
        List<Employee> _targetEmployees;
        List<Instructor> _targetInstructors;

        public ProvidersMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {
            _projectId = projectId;
        }

        protected override List<ProgramImpl> getSourceRecords()
        {
            _sourceProviders = (_source as VisionContext).ProgramImpls.Where(r => r.Id == 381 || r.Id == 2455).ToList();
            _sourceProgramHeirachy = (_source as VisionContext).ProgramHierarchies
                                        .Include("FkParentNavigation.ProgramImpls")
                                    .Where(r => r.FkExpiredBy == null).ToList();
            _sourceIlas = (_source as VisionContext).ProgramImpls
                        .Where(r => r.FkProject == _projectId)
                        .Where(r => r.FkExpiredBy == null)
                        .Where(r => r.FkProgramLevelNavigation.ProgramLevelImpls.Where(r => r.FkExpiredBy == null).First().Text == "Lesson")
                    .ToList();

            _sourceLsLearningEventPrograms = (_source as VisionContext).LsLearningEventPrograms.Include("FkLearningEventNavigation").ToList();
            _sourceLsLearnerEventLearners = (_source as VisionContext).LsLearningEventLearners.Include("FkLearnerValueNavigation").ToList();

            _sourceProgramComments = (_source as VisionContext).ProgramComments.Where(r => r.FkExpiredBy == null).ToList();

            _sourceReferenceLinks = (_source as VisionContext).XrefLibLinks.Where(r => r.FkExpiredBy == null).ToList();

            _sourceSequencing = (_source as VisionContext).Sequencings.Include("FkObjectiveNavigation.ObjectiveImpls").Where(r => r.FkExpiredBy == null).ToList();

            _deliveryMethods = (_target as QTD2.Data.QTDContext).DeliveryMethods.ToList();
            _targetAudiences = (_target as QTD2.Data.QTDContext).NERCTargetAudiences.ToList();
            _regulatoryRequirements = (_target as QTD2.Data.QTDContext).RegulatoryRequirements.ToList();

            _targetTrainingTopics = (_target as QTD2.Data.QTDContext).TrainingTopics.ToList();

            _targetEnablingObjectives = (_target as QTD2.Data.QTDContext).EnablingObjectives.ToList();
            _targetTasks = (_target as QTD2.Data.QTDContext).Tasks.ToList();
            _targetEmployees = (_target as QTD2.Data.QTDContext).Employees.Include("Person").ToList();
            _targetInstructors = (_target as QTD2.Data.QTDContext).Instructors.ToList();

            return _sourceProviders;
        }

        protected override Provider mapRecord(ProgramImpl obj)
        {
            return new Provider()
            {
                Active = true,
                Name = obj.Text.RtfToPlainText(),
                //ProviderLevelId,
                Number = obj.UserDefinedId,
                //ContactName = obj.ContactPerson,
                //ContactTitle = obj.CpTitle,
                //ContactPhone = obj.CpPhone,
                //ContactMobile = obj.Cell,
                //ContactEmail = obj.CpEmail,
                //CompanyWebsite = obj.CpWebsite,
                //RepName = obj.TprName,
                //RepTitle = obj.TprTitle,
                //RepPhone = obj.TprPhone,
                //RepEmail = obj.TprEmail,
                //RepSignature = repSig == null ? null : "data:image/png;base64," +Convert.ToBase64String(repSig.Imbody),
                IsNERC = true,
                //IsPriority
                Deleted = false,
                ILAs = getILAs(obj),
                ProviderLevelId = 3,
                IsPriority = true,
            };
        }

        private ICollection<ILA> getILAs(ProgramImpl provider)
        {
            List<ILA> iLAs = new List<ILA>();
            var courses = findCourses(provider);

            var coursesFind = courses.Where(r => r.TextAscii.Contains("ATC_085 2017 010 C2 STEPs"));

            foreach (var ila in courses)
            {
                var programComments = _sourceProgramComments.Where(r => r.FkProgram == ila.FkProgram).FirstOrDefault();
                var sourceReferences = _sourceReferenceLinks.Where(r => r.LinkToType == 3 && r.FkLinkTo == ila.FkProgram);

                //string courseType = getCourseType(sourceReferences);
                //bool partialCreditAllowed = getPartialCreditAllowed(sourceReferences);
                //List<ILA_NERCAudience_Link> ilaAudienceLInks = getTargetAudiences(sourceReferences);
                //bool eoRelated =;

                iLAs.Add(new ILA()
                {
                    Active = true,
                    Deleted = false,
                    Name = ila.Text.RtfToPlainText(),
                    NickName = ila.Text.RtfToPlainText(),
                    //image
                    //ProviderId
                    //TopicId 
                    IsPublished = true,
                    PublishDate = ila.DateCreated.ToQtsTime(false),
                    HasPilotData = false,
                    //ILA_RegRequirement_Links = getRegulatoryRequirements(ila),
                    //IsProgramManual
                    //SubmissionDate = ila.CehAppDate.HasValue ? ila.CehAppDate.Value.ToQtsTime(true) : ila.CehAppDate.ToQtsTime(true),
                    //EffectiveDate
                    //TrainingEvalMethods = ila.EvaluationMethod,
                    DeliveryMethod = getDeliveryMethod(ila), //ila.SelfPased ? (ila.OnlineClass.GetValueOrDefault() ? _deliveryMethods.Where(r => r.Name == "Computer Based Training (CBT)").First() : _deliveryMethods.Where(r => r.Name == "Self-Study (e.g., paperbased)").First()) : _deliveryMethods.Where(r => r.Name == "Classroom").First(),
                    Number = ila.Text.RtfToPlainText().GetIlaNumber(ila.FkProgram.ToString()),
                    Description = programComments == null ? ila.Text.RtfToPlainText() : programComments.Comments.RtfToPlainText(),
                    TrainingPlan = programComments == null ? ila.Text.RtfToPlainText() : programComments.Comments.RtfToPlainText(),
                    IsSelfPaced = false,
                    IsOptional = false,
                    //DeliveryMethodId = ila.DeliveryMethodId,
                    ApprovalDate = null,
                    ExpirationDate = null,
                    ILA_EnablingObjective_Links = getILA_EnablingObjective_Links(ila),
                    //ILA_Topic = getILA_Topic(ila),
                    Task_ILA_Links = getTasks(ila),
                    //ILA_TaskObjective_Links = getTaskObjectiveLinks(ila),
                    //ILA_SafetyHazard_Links = getSaftyHazardLinks(ila),
                    ClassSchedules = getClassSchedules(ila),
                    ILA_TrainingTopic_Links = getTrainingTopicLinks(ila),
                    StartDate = DateOnly.FromDateTime(ila.DateCreated),
                    //ILATraineeEvaluations = getIlaTraineeEvaluations(ila),
                    //ILACertificationLinks = getIlaCertificationLinks(ila),
                    EvaluationReleaseEMPSetting = getSettings(ila),
                    TotalTrainingHours = getTotalTrainingHours(sourceReferences),
                    //ILACustomObjective_Links = getIlaCustomObjectiveLinks(ila),
                    TestReleaseEMPSettings = getTestReleaseSettings(ila),
                    ILA_NERCAudience_Links = getTargetAudiences(sourceReferences),
                    TQILAEmpSettings = getTqIlaSettings(ila),
                    DoesActivityConform = false,
                    // WriitenOrOnlineAssessmentTool = ila.on
                    // PerformAssessmentTool = 
                    // OtherAssesmentTool = ""
                    //Prerequisites = ila.Prerequisites,
                    //ILA_Resources = getResources(ila),
                    WriitenOrOnlineAssessmentTool = false,
                    PerformAssessmentTool = false,
                    OtherAssesmentTool = "",
                    PilotDataNA = false
                });
            }
            return iLAs;
        }

        private ICollection<ClassSchedule> getClassSchedules(ProgramImpl ila)
        {
            List<ClassSchedule> classSchedules = new List<ClassSchedule>();

            var learningEventPrograms = _sourceLsLearningEventPrograms.Where(r => r.FkProgram == ila.FkProgram);

            foreach (var learningEventProgram in learningEventPrograms)
            {
                classSchedules.Add(new ClassSchedule()
                {
                    Active = true,
                    ClassSize = 9999,
                    EndDateTime = learningEventProgram.FkLearningEventNavigation.EndDate ?? DateTime.Now,
                    StartDateTime = learningEventProgram.FkLearningEventNavigation.StartDate ?? DateTime.Now,
                    Instructor = findQtdInstructor(learningEventProgram.FkInstructorNavigation),
                    IsRecurring = false,
                    Notes = learningEventProgram.FkLearningEventNavigation.Id.ToString(),
                    ClassSchedule_TestReleaseEMPSettings = new ClassSchedule_TestReleaseEMPSetting()
                    {
                        AutoReleaseRetake = false,
                        MakeAvailableBeforeDays = 0,
                        MakeFinalTestAvailableAfterCBTCompleted = false,
                        MakeFinalTestAvailableImmediatelyAfterStartDate = false,
                        MakeFinalTestAvailableOnClassEndDate = false,
                        MakeFinalTestAvailableOnSpecificTime = 0,
                        PreTestAvailableOneStartDate = false,
                        PreTestAvailableOnEnrollment = true,
                        ShowCorrectIncorrectFinalTestAnswers = false,
                        ShowCorrectIncorrectPreTestAnswers = false,
                        ShowCorrectIncorrectRetakeTestAnswers = false,
                        ShowStudentSubmittedFinalTestAnswers = false,
                        RetakeEnabled = false,
                        ShowStudentSubmittedPreTestAnswers = false,
                        ShowStudentSubmittedRetakeTestAnswers = false,
                        UsePreTestAndTest = false,
                        //FinalTestDueDate = false,
                        //FinalTestId
                        PreTestRequired = false,
                        //PreTestScore = testCutScore,
                        NumberOfRetakes = 0,
                        FinalTestPassingScore = "",
                        FinalTestSpecificTimePrior = true,
                        Active = true,
                        EmpSettingsReleaseTypeId = 1
                    },
                    ClassSchedule_Employee = getClassScheduleEmployees(learningEventProgram.FkLearningEventNavigation)
                });
            }

            return classSchedules;
        }

        private ICollection<ClassSchedule_Employee> getClassScheduleEmployees(LsLearningEvent learningEvent)
        {
            List<ClassSchedule_Employee> classSchedule_Employees = new List<ClassSchedule_Employee>();

            var targetLearners = _sourceLsLearnerEventLearners.Where(r => r.FkLearningEvent == learningEvent.Id);

            foreach (var targetLearner in targetLearners)
            {
                var employee = getTargetEmployee(targetLearner.FkLearnerValueNavigation);

                if (employee == null) continue;

                classSchedule_Employees.Add(new ClassSchedule_Employee()
                {
                    Active = false,
                    IsAwaitingForApproval = true,
                    CompletionDate = targetLearner.DateCompleted,
                    Employee = employee,
                    IsDenied = false,
                    EnrolledDate = targetLearner.DateCreated,
                    FinalGrade = targetLearner.EventScore >= targetLearner.EventPassingScore ? "P" : "F",
                    IsDropped = false,
                    IsEnrolled = true,
                    FinalScore = (int?)targetLearner.EventScore,
                    IsWaitlisted = false,
                    CBTStatusId = 1,
                    PreTestStatusId = 1,
                    RetakeStatusId = 1,
                    TestStatusId = 1
                });
            }

            return classSchedule_Employees;
        }

        private Employee getTargetEmployee(Learner learner)
        {
            string firstname = learner.FirstName;
            string lastname = learner.LastName;

            var employee = _targetEmployees
                                .Where(r => r.Person.FirstName.ToUpper() == firstname.ToUpper())
                                .Where(r => r.Person.LastName.ToUpper() == lastname.ToUpper())
                            .FirstOrDefault();

            return employee;
        }

        private Instructor findQtdInstructor(Learner learner)
        {
            if (learner == null) return null;

            string firstname = learner.FirstName;
            string lastname = learner.LastName;

            string name = firstname + " " + lastname;

            var instructor = _targetInstructors
                                .Where(r => r.InstructorName.ToUpper() == name.ToUpper())
                            .FirstOrDefault();

            if (instructor == null)
            {
                instructor = new Instructor()
                {
                    IsWorkBookAdmin = false,
                    InstructorName = name,
                    InstructorEmail = learner.Email,
                    ICategoryId = 1,
                    InstructorNumber= learner.Id.ToString()
                };

                _targetInstructors.Add(instructor);
            }

            return instructor;
        }

        private double? getTotalTrainingHours(IEnumerable<XrefLibLink> sourceReferences)
        {
            var reference = sourceReferences.Where(r => r.FkParent == 4506).Where(r => r.FkExpiredBy == null).FirstOrDefault();

            if (reference == null) return null;

            string referenceCEH = reference.FkItemNavigation.XrefLibImplFkXrefLibNavigations.Where(r => r.FkExpiredBy == null).First().Text.RtfToPlainText();

            bool isDouble = Double.TryParse(referenceCEH, out double ceh);

            if (isDouble) return ceh;

            else return 0;

        }

        private ICollection<ILA_NERCAudience_Link> getTargetAudiences(IEnumerable<XrefLibLink> sourceReferences)
        {
            List<ILA_NERCAudience_Link> targetAudiences = new List<ILA_NERCAudience_Link>();

            var sourceAudiences = sourceReferences.Where(r => r.FkItemNavigation.XrefLibImplFkXrefLibNavigations.Where(r => r.FkExpiredBy == null).First().Text.ToUpper().Contains("TARGET AUDIENCE - ")).SelectMany(r => r.FkItemNavigation.XrefLibImplFkXrefLibNavigations).Where(r => r.FkExpiredBy == null);

            foreach (var sourceAudience in sourceAudiences)
            {
                var sourceAudienceText = sourceAudience.Text.RtfToPlainText();
                var targetAudience = _targetAudiences.Where(r => r.Name == sourceAudienceText.Split(" - ")[1]).First();

                targetAudiences.Add(new ILA_NERCAudience_Link()
                {
                    Active = true,
                    NERCTargetAudience = targetAudience
                });
            }

            return targetAudiences;
        }

        private ICollection<ILA_TrainingTopic_Link> getTrainingTopicLinks(ProgramImpl ila)
        {
            List<ILA_TrainingTopic_Link> trainingTopicLinks = new List<ILA_TrainingTopic_Link>();

            var sourceTopicLinks = _sourceReferenceLinks.Where(r => r.FkParent == 5676);

            foreach (var targetTrainingTopic in _targetTrainingTopics)
            {
                XrefLibLink sourceTopicLink = sourceTopicLinks
                                                    .Where(r => r.LinkToType == 3 && r.FkLinkTo == ila.FkProgram)
                                                    .Where(r => cleanSourceTopicName(r) == targetTrainingTopic.Name)
                                                .FirstOrDefault();

                trainingTopicLinks.Add(new ILA_TrainingTopic_Link()
                {
                    TrainingTopic = targetTrainingTopic,
                    Active = sourceTopicLink != null
                });
            }

            return trainingTopicLinks;
        }

        private ICollection<Task_ILA_Link> getTasks(ProgramImpl ila)
        {
            List<Task_ILA_Link> task_links = new List<Task_ILA_Link>();

            var sequencings = _sourceSequencing.Where(r => r.FkProgram == ila.FkProgram);

            foreach (var sequencing in sequencings)
            {
                var latestObjectiveImpl = sequencing.FkObjectiveNavigation.ObjectiveImpls
                                                    .Where(r => r.FkExpiredBy == null).FirstOrDefault();

                if (latestObjectiveImpl == null) continue;

                var targetTask = _targetTasks.Where(r => r.Description == latestObjectiveImpl.Text.RtfToPlainText()).FirstOrDefault();

                if (targetTask == null) continue;

                task_links.Add(new Task_ILA_Link()
                {
                    Task = targetTask
                });
            }


            return task_links;
        }

        private ICollection<ILA_EnablingObjective_Link> getILA_EnablingObjective_Links(ProgramImpl ila)
        {
            List<ILA_EnablingObjective_Link> enablingObjective_Links = new List<ILA_EnablingObjective_Link>();

            var sequencings = _sourceSequencing.Where(r => r.FkProgram == ila.FkProgram);

            foreach (var sequencing in sequencings)
            {
                var latestObjectiveImpl = sequencing.FkObjectiveNavigation.ObjectiveImpls
                                                    .Where(r => r.FkExpiredBy == null).FirstOrDefault();

                if (latestObjectiveImpl == null) continue;

                var targetEnablingObjective = _targetEnablingObjectives.Where(r => r.Description == latestObjectiveImpl.Text.RtfToPlainText()).FirstOrDefault();

                if (targetEnablingObjective == null) continue;

                enablingObjective_Links.Add(new ILA_EnablingObjective_Link()
                {
                    EnablingObjective = targetEnablingObjective
                });
            }


            return enablingObjective_Links;
        }

        private string cleanSourceTopicName(XrefLibLink refLink)
        {
            var item = refLink.FkItemNavigation.XrefLibImplFkXrefLibNavigations.Where(r => r.FkExpiredBy == null).FirstOrDefault();

            if (item == null) return "not found";

            string[] parts = item.Text.RtfToPlainText().Split(" - ");

            if (parts.Length != 2) return "bad parts";

            string name = parts[1];

            if (name == "Communication with appropriate entities including the Reliability Coordinator")
                return "Communication with appropriate entities including the RC";

            if (name.Trim() == "Emerging technologies/equipment")
                return "Emergency technologies/equipment";

            if (name == "Kirchhoff's Laws")
                return "Kirchoff’s Laws";

            if (name == "Ohm's Law")
                return "Ohm’s Law";

            if (name == "Real and reactive power")
                return "Real reactive power";

            if (name == "Recognizing loss of communication facility")
                return "Recognizing loss of communication facilities";

            if (name.Trim() == "Reducing System Operator errors through the use of Human Performance Tools, such as Self-Checking, Peer Checking, Place Keeping, and Procedure Use.")
                return "Reducing System Operator errors through the use of HPI Tools (self-checking, peer checking, Place Keeping and Procedure Use";

            if (name == "Voice and data communications systems")
                return "Voice and data communication systems";

            return name;

        }

        private ICollection<TQILAEmpSetting> getTqIlaSettings(ProgramImpl ila)
        {
            List<TQILAEmpSetting> settings = new List<TQILAEmpSetting>();

            settings.Add(new TQILAEmpSetting()
            {
                Active = true,
                Deleted = false,
                ReleaseAtOnce = false,
                ReleaseOneAtTime = false,
                EmpSettingsReleaseTypeId = 1,
                TQDueDate = 30,
                MultipleSignOffRequired = 0,
                OneSignOffRequired = true,
            });

            return settings;
        }

        private DeliveryMethod getDeliveryMethod(ProgramImpl ila)
        {
            return _deliveryMethods.Where(r => r.Name == "Classroom").First();
        }

        private TestReleaseEMPSettings getTestReleaseSettings(ProgramImpl ila)
        {
            TestReleaseEMPSettings settings = new TestReleaseEMPSettings()
            {
                AutoReleaseRetake = false,
                MakeAvailableBeforeDays = 0,
                MakeFinalTestAvailableAfterCBTCompleted = false,
                MakeFinalTestAvailableImmediatelyAfterStartDate = false,
                MakeFinalTestAvailableOnClassEndDate = false,
                MakeFinalTestAvailableOnSpecificTime = 0,
                PreTestAvailableOneStartDate = false,
                PreTestAvailableOnEnrollment = true,
                ShowCorrectIncorrectFinalTestAnswers = false,
                ShowCorrectIncorrectPreTestAnswers = false,
                ShowCorrectIncorrectRetakeTestAnswers = false,
                ShowStudentSubmittedFinalTestAnswers = false,
                RetakeEnabled = true,
                ShowStudentSubmittedPreTestAnswers = false,
                ShowStudentSubmittedRetakeTestAnswers = false,
                UsePreTestAndTest = true,
                FinalTestDueDate = 7,
                //FinalTestId
                PreTestRequired = false,
                PreTestScore = 70,
                NumberOfRetakes = 0,
                FinalTestPassingScore = "70",
                FinalTestSpecificTimePrior = true,
                Active = true,
                EmpSettingsReleaseTypeId = 1
                //PreTestId = traineeEvals.Where(r => r.TestTypeId == 1).First().TestId,
                //FinalTestId = traineeEvals.Where(r => r.TestTypeId == 2).First().TestId
            };

            return settings;
        }

        private EvaluationReleaseEMPSettings getSettings(ProgramImpl ila)
        {
            var defaultEvalTimePeriodRecord = "DAYS";
            var defaultEvalTimeValue = 7;

            EvaluationReleaseEMPSettings settings = new EvaluationReleaseEMPSettings()
            {
                Active = true,
                EvaluationAvailableOnEndDate = false,
                EvaluationAvailableOnStartDate = false,
                EvaluationDueDate = defaultEvalTimeValue,
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

        //private ICollection<ILACertificationLink> getIlaCertificationLinks(TblCourse ila)
        //{
        //    List<ILACertificationLink> links = new List<ILACertificationLink>();

        //    var certs = (_target as QTD2.Data.QTDContext).Certifications.ToList();

        //    foreach (var cert in certs.Where(r => r.CertifyingBodyId == 1))
        //    {
        //        links.Add(new ILACertificationLink()
        //        {
        //            CertificationId = cert.Id,
        //            ILACertificationSubRequirementLink = getIlaCertificationSubRequirementLInks(ila, cert),
        //            IsEmergencyOpHours = ila.TopicsEo,
        //            IsPartialCreditHours = ila.ActPartialCredits,
        //            IsIncludeSimulation = ila.ReginalExerciseIncluded,
        //            CEHHours = ila.TotalCeh.HasValue ? ila.TotalCeh.Value : null
        //        });
        //    }

        //    string legacyName = "Emergency Response";
        //    var labelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == legacyName.ToUpper()).FirstOrDefault();

        //    string name = labelreplacement == null ? legacyName : labelreplacement.ReplacementText;

        //    var emergencyResponseCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == name).First();

        //    links.Add(new ILACertificationLink()
        //    {
        //        CertificationId = emergencyResponseCert.Id,
        //        CEHHours = ila.EmergencyOpsHours.HasValue ?ila.EmergencyOpsHours.Value : null
        //    });


        //    string regLegacyName = "Reg";
        //    var regLabelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == regLegacyName.ToUpper()).FirstOrDefault();

        //    string regName = labelreplacement == null ? regLegacyName : regLabelreplacement.ReplacementText;

        //    var regCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == regName).First();

        //    links.Add(new ILACertificationLink()
        //    {
        //        CertificationId = regCert.Id,
        //        CEHHours = ila.CehReg.HasValue ? ila.CehReg.Value : null
        //    });


        //    string reg2LegacyName = "Reg2";
        //    var reg2Labelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == reg2LegacyName.ToUpper()).FirstOrDefault();

        //    string reg2Name = reg2Labelreplacement == null ? reg2LegacyName : reg2Labelreplacement.ReplacementText;

        //    var reg2Cert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == reg2Name).First();

        //    links.Add(new ILACertificationLink()
        //    {
        //        CertificationId = reg2Cert.Id,
        //        CEHHours = ila.Reg2.HasValue ? ila.Reg2.Value : null
        //    });

        //    string otherLegacyName = "Other";
        //    var otherLabelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == otherLegacyName.ToUpper()).FirstOrDefault();

        //    string otherName = otherLabelreplacement == null ? otherLegacyName : otherLabelreplacement.ReplacementText;

        //    var otherCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == otherName).First();

        //    links.Add(new ILACertificationLink()
        //    {
        //        CertificationId = otherCert.Id,
        //        CEHHours = ila.Other.HasValue ? ila.Other.Value : null
        //    });

        //    for (int i = 4; i <= 20; i++)
        //    {
        //        string fieldName = "Ceh" + i.ToString();
        //        var field = ila.GetType().GetProperty(fieldName).GetValue(ila, null);
        //        double? value = field == null ? default(double?) : Convert.ToDouble(field);

        //        if (value.HasValue)
        //        {
        //            var sourceCert = _additionalCertsInfo.Where(r => r.TrainingTypeId == i).FirstOrDefault();
        //            if (sourceCert == null) continue;

        //            var trainingType = sourceCert.TrainingType;

        //            var targetCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == trainingType).First();

        //            links.Add(new ILACertificationLink()
        //            {
        //                CertificationId = targetCert.Id,
        //                CEHHours = value
        //            });
        //        }
        //    }

        //    string proflegacyName = "Professional";
        //    var proflabelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == proflegacyName.ToUpper()).FirstOrDefault();

        //    string profName = proflabelreplacement == null ? proflegacyName : proflabelreplacement.ReplacementText;

        //    var profCert = (_target as QTD2.Data.QTDContext).Certifications.Where(r => r.Name == profName).First();

        //    links.Add(new ILACertificationLink()
        //    {
        //        CertificationId = profCert.Id,
        //        CEHHours = ila.CehProf.HasValue ? ila.CehProf.Value : null
        //    });

        //    return links;
        //}

        //private ICollection<ILACertificationSubRequirementLink> getIlaCertificationSubRequirementLInks(TblCourse ila, Certification cert)
        //{
        //    List<ILACertificationSubRequirementLink> links = new List<ILACertificationSubRequirementLink>();

        //    var standards = cert.CertificationSubRequirements.Where(r => r.ReqName == "Standards").First();
        //    var sims = cert.CertificationSubRequirements.Where(r => r.ReqName == "Simulations").First();
        //    //var operations = cert.CertificationSubRequirements.Where(r => r.ReqName == "Operation CEHs").First();

        //    links.Add(new ILACertificationSubRequirementLink()
        //    {
        //        CertificationSubRequirementId = standards.Id,
        //        ReqHour = ila.CehNerc.GetValueOrDefault()
        //    });

        //    links.Add(new ILACertificationSubRequirementLink()
        //    {
        //        CertificationSubRequirementId = sims.Id,
        //        ReqHour =ila.SimHours.GetValueOrDefault()
        //    });

        //    //links.Add(new ILACertificationSubRequirementLink()
        //    //{
        //    //    CertificationSubRequirementId = operations.Id,
        //    //    ReqHour = Convert.ToInt32(ila.TotalCeh.GetValueOrDefault())
        //    //});

        //    return links;
        //}

        private List<ProgramImpl> findCourses(ProgramImpl provider)
        {
            List<ProgramImpl> courseInProvider = new List<ProgramImpl>();

            foreach (var course in _sourceIlas)
            {
                var visited = new HashSet<decimal>();
                if (isDecendentOf(course, provider, visited))
                    courseInProvider.Add(course);
            }

            return courseInProvider;
        }

        private bool isDecendentOf(ProgramImpl current, ProgramImpl origin, HashSet<decimal> visited)
        {
            if (current == null || visited.Contains(current.FkProgram))
                return false;

            if (current.FkProgram == origin.FkProgram)
                return true;

            visited.Add(current.FkProgram);

            var parentLinks = _sourceProgramHeirachy
                .Where(link => link.FkChild == current.FkProgram)
                .SelectMany(link => link.FkParentNavigation.ProgramImpls);

            foreach (var parent in parentLinks)
            {
                if (isDecendentOf(parent, origin, visited))
                    return true;
            }

            return false;
        }


        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceProviders.Count();
        }

        protected override void updateTarget(Provider record)
        {
            (_target as QTD2.Data.QTDContext).Providers.Add(record);
        }

    }
}
