using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ILAService : Common.Service<ILA>, IILAService
    {
        public ILAService(IILARepository repository, IILAValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<IEnumerable<ILA>> GetByProvidersListAsync(List<int> providerIds, string ilaStatus)
        {
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            if (providerIds.Count > 0)
                predicates.Add(r => providerIds.Contains(r.ProviderId));
            if (!string.IsNullOrEmpty(ilaStatus) && ilaStatus == "Active Only")
                predicates.Add(r => r.Active);
            else if (!string.IsNullOrEmpty(ilaStatus) && ilaStatus == "Inactive Only")
                predicates.Add(r => !r.Active);

            return await FindWithIncludeAsync(predicates, new string[] { "ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement" });
        }

        public async Task<ILA> GetWithProviderTopicDeliveryMethodAsync(int id)
        {
            //This needs a review but this solution should resolve everything

            var get = await GetAsync(id);
            var getWithProvider = await GetWithIncludeAsync(id, new string[] { "Provider" });
            var getWithCbt = await GetWithIncludeAsync(id, new string[] { "CBTs" });
            var getWithIlaTopic = await GetWithIncludeAsync(id, new string[] { "ILA_Topic_Links.ILA_Topic" });
            var getWithDeliveryMethod = await GetWithIncludeAsync(id, new string[] { "DeliveryMethod" });
            var getWithEnablingObjective = (await FindWithIncludeAsync(r => r.Id == id, new string[] { "ILA_EnablingObjective_Links.EnablingObjective" })).ToList();
            var getWithSegment = (await FindWithIncludeAsync(r => r.Id == id, new string[] { "ILA_Segment_Links.Segment.SegmentObjective_Links" })).ToList();
            var getWithTrainingTopic = (await FindWithIncludeAsync(r => r.Id == id, new string[] { "ILA_TrainingTopic_Links.TrainingTopic" })).ToList();
            var getWithCertificationLinks = (await FindWithIncludeAsync(r => r.Id == id, new string[] { "ILACertificationLinks.Certification.CertifyingBody", "ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement" })).ToList();
            var getWithRegistratonOptions = await GetWithIncludeAsync(id, new string[] { "ILA_SelfRegistrationOption" });

            if (getWithProvider != null) get.Provider = getWithProvider.Provider;
            if (getWithIlaTopic != null) get.ILA_Topic_Links = getWithIlaTopic.ILA_Topic_Links;
            if (getWithCbt != null) get.CBTs = getWithCbt.CBTs;
            if (getWithDeliveryMethod != null) get.DeliveryMethod = getWithDeliveryMethod.DeliveryMethod;
            if (getWithEnablingObjective != null) get.ILA_EnablingObjective_Links = getWithEnablingObjective.SelectMany(r => r.ILA_EnablingObjective_Links).ToList();
            if (getWithRegistratonOptions != null) get.ILA_SelfRegistrationOption = getWithRegistratonOptions.ILA_SelfRegistrationOption;
            foreach (var ila_EnablingObjective_Links in get.ILA_EnablingObjective_Links)
            {
                var enablingObjective = ila_EnablingObjective_Links.EnablingObjective;
            }
            if (getWithCertificationLinks != null) get.ILACertificationLinks = getWithCertificationLinks.SelectMany(r => r.ILACertificationLinks.Where(r => !r.Certification.Deleted)).ToList();
            if (getWithSegment != null) get.ILA_Segment_Links = getWithSegment.SelectMany(r => r.ILA_Segment_Links).ToList();
            foreach (var ila_Segment_links in get.ILA_Segment_Links)
            {
                var segment = ila_Segment_links.Segment;
                segment.SegmentObjective_Links = ila_Segment_links.Segment.SegmentObjective_Links.ToList();
            }
            if (getWithTrainingTopic != null) get.ILA_TrainingTopic_Links = getWithTrainingTopic.SelectMany(r => r.ILA_TrainingTopic_Links).ToList();
            foreach (var iLA_Training in get.ILA_TrainingTopic_Links)
            {
                var trainingTopic = iLA_Training.TrainingTopic;
            }

            return get;
        }
        public async System.Threading.Tasks.Task<List<ILA>> GetClassesByILAAsync(List<int> iLAIDs, DateTime startDate, DateTime endDate)
        {
            var iLAs = (await FindWithIncludeAsync(r => iLAIDs.Contains(r.Id), new[] { "ClassSchedules", "ClassSchedules.Instructor", "ClassSchedules.Location", "Provider" })).ToList();

            foreach (var ila in iLAs)
            {
                ila.ClassSchedules = ila.ClassSchedules.Where(r => r.StartDateTime <= startDate && r.EndDateTime <= endDate).ToList();
            }
            return iLAs;
        }

        public async System.Threading.Tasks.Task<List<ILA>> GetEmployeeSelfRegistrationAvailableCourseAsync()
        {
            var ilas = await FindWithIncludeAsync(x => x.ILA_SelfRegistrationOption != null && x.ILA_SelfRegistrationOption.MakeAvailableForSelfReg, new string[] { "ILA_SelfRegistrationOption", "ClassSchedules.ClassSchedule_Employee.Employee.Person", "ClassSchedules.Instructor", "ClassSchedules.Location", "ClassSchedules.ClassSchedule_Employee.Employee.EmployeePositions.Position", "Provider" });
            return ilas.ToList();
        }

        public async System.Threading.Tasks.Task<List<ILA>> GetILAAsync()
        {
            var ilas = await FindAsync(x => x.Active == true);
            var selectedILAs = ilas.Select(s => new ILA { Id = s.Id, Active = s.Active }).ToList();
            return selectedILAs.ToList();
        }

        //Domain service created for Reports    
        public async System.Threading.Tasks.Task<List<ILA>> GetILALessonPlanAsync(List<int> iLAIDs)
        {
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();

            if (iLAIDs != null)
            {
                predicates.Add(r => iLAIDs.Contains(r.Id));
            }

            var ilas = await FindWithIncludeAsync(predicates,
                new[] { "DeliveryMethod", "ILA_Uploads", "ILA_Resources",
            }, true);

            var ilasWithSegments = await FindWithIncludeAsync(predicates,
                new[] {

                        "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_Topic",
                        "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_Category",
                        "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_SubCategory",
                        "ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.EnablingObjective_Category.EnablingObjective_SubCategories.EnablingObjective_Topics",
                        "ILA_Segment_Links.Segment.SegmentObjective_Links.Task.SubdutyArea.DutyArea"
                });

            var ilasWithNERCTargetAudience = await FindWithIncludeAsync(predicates,
                new[] {
                        "ILA_NERCAudience_Links.NERCTargetAudience",
                });

            var ilasWithPrerequisites = await FindWithIncludeAsync(predicates,
                new[] {
                        "ILA_PreRequisite_Links.PreRequisite",
                });

            var ilasWithProcedures = await FindWithIncludeAsync(predicates,
                new[] {
                        "ILA_Procedure_Links.Procedure"
                });

            var ilasWithSaftyHazards = await FindWithIncludeAsync(predicates,
                new[] {
                        "ILA_SafetyHazard_Links.SafetyHazard",
                });

            var ilasWithEOs = await FindWithIncludeAsync(predicates,
               new[] {
                          "ILA_EnablingObjective_Links.EnablingObjective","ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Topic","ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Category","ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_SubCategory"
               });

            var ilasWithTasks = await FindWithIncludeAsync(predicates,
               new[] {
                       "ILA_TaskObjective_Links.Task.SubdutyArea.DutyArea"
               });

            var ilasWithCustomEos = await FindWithIncludeAsync(predicates,
               new[] {
                       "ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_Category.EnablingObjective_SubCategories.EnablingObjective_Topics"
               });

            var ilaWithPositions = await FindWithIncludeAsync(predicates,
               new[] {
                          "ILA_Position_Links.Position"
               });

            var ilaWithRegRequirements = await FindWithIncludeAsync(predicates,
               new[] {
                          "ILA_RegRequirement_Links.RegulatoryRequirement"
               });
            var ilaWithTopics = await FindWithIncludeAsync(predicates,
               new[] {
                          "ILA_Topic_Links.ILA_Topic"
               });
            foreach (var ila in ilas)
            {
                ila.ILA_Segment_Links = ilasWithSegments.Where(r => r.Id == ila.Id).First().ILA_Segment_Links;
                ila.ILA_SafetyHazard_Links = ilasWithSaftyHazards.Where(r => r.Id == ila.Id).First().ILA_SafetyHazard_Links;
                ila.ILA_EnablingObjective_Links = ilasWithEOs.Where(r => r.Id == ila.Id).First().ILA_EnablingObjective_Links;
                ila.ILA_SafetyHazard_Links = ilasWithSaftyHazards.Where(r => r.Id == ila.Id).First().ILA_SafetyHazard_Links;
                ila.ILA_Position_Links = ilaWithPositions.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_Position_Links;
                ila.ILA_RegRequirement_Links = ilaWithRegRequirements.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_RegRequirement_Links;
                ila.ILA_Topic_Links = ilaWithTopics.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_Topic_Links;
                ila.ILA_Procedure_Links = ilasWithProcedures.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_Procedure_Links;
                ila.ILA_NERCAudience_Links = ilasWithNERCTargetAudience.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_NERCAudience_Links;
                ila.ILA_PreRequisite_Links = ilasWithPrerequisites.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_PreRequisite_Links;
                ila.ILA_TaskObjective_Links = ilasWithTasks.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_TaskObjective_Links;
                ila.ILACustomObjective_Links = ilasWithCustomEos.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILACustomObjective_Links;

            }

            return ilas.ToList();
        }

        public async System.Threading.Tasks.Task<List<ILA>> GetILACompletionHistoryAsync(List<int> iLAIDs, string completedStatus, DateTime startDate, DateTime endDate, string activeStatus)
        {
            endDate = endDate.AddDays(1);
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            if (iLAIDs != null)
            {
                predicates.Add(r => iLAIDs.Contains(r.Id));
            }
            var iLAs = (await FindWithIncludeAsync(predicates, new[] { "ClassSchedules.Location", "ClassSchedules.Instructor", "ClassSchedules.ClassSchedule_Employee.Employee.Person", "ClassSchedules.ClassSchedule_Employee.Employee.EmployeePositions.Position", "ClassSchedules.ClassSchedule_Employee.Employee.EmployeeOrganizations.Organization" }, true)).ToList();
            var iLAsWithCerts = (await FindWithIncludeAsync(predicates, new[] { "ILACertificationLinks.Certification", "ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement", "Provider" }, true)).ToList();

            foreach (var ila in iLAs)
            {
                var marry = iLAsWithCerts.Where(r => r.Id == ila.Id).FirstOrDefault();

                if (marry != null)
                {
                    ila.Provider = marry.Provider;
                    ila.ILACertificationLinks = marry.ILACertificationLinks;
                }

                foreach (var cls in ila.ClassSchedules)
                {
                    cls.ClassSchedule_Employee = cls.ClassSchedule_Employee.Where(cse =>
                           {
                               if (cse.CompletionDate.HasValue)
                               {
                                   return cse.CompletionDate >= startDate && cse.CompletionDate < endDate;
                               }
                               else
                               {
                                   return cls.StartDateTime <= endDate && cls.EndDateTime >= startDate;
                               }
                           }).ToList();

                    if (completedStatus.ToUpper() == "NOT COMPLETED WITHIN DATE RANGE")
                    {
                        cls.ClassSchedule_Employee = cls.ClassSchedule_Employee.Where(x => x.IsEnrolled && x.Active && !x.IsComplete).ToList();
                    }
                    else if (completedStatus.ToUpper() == "COMPLETED WITHIN DATE RANGE")
                    {
                        cls.ClassSchedule_Employee = cls.ClassSchedule_Employee.Where(x => x.IsComplete && x.Active && x.IsEnrolled).ToList();
                    }
                    else if (completedStatus.ToUpper() == "COMPLETED & NOT COMPLETED WITHIN DATE RANGE")
                    {
                        cls.ClassSchedule_Employee = cls.ClassSchedule_Employee.Where(x => x.IsEnrolled && x.Active).ToList();
                    }
                    if (activeStatus == "Active Only")
                    {
                        cls.ClassSchedule_Employee = cls.ClassSchedule_Employee.Where(r => r.Employee.Active).ToList();
                    }
                    else if (activeStatus == "Inactive Only")
                    {
                        cls.ClassSchedule_Employee = cls.ClassSchedule_Employee.Where(r => !r.Employee.Active).ToList();
                    }
                }
            }

            return iLAs.ToList();
        }
        public async System.Threading.Tasks.Task<List<ILA>> GetItemByILAAsync(List<int> iLAIDs)
        {
            var iLAs = (await FindWithIncludeAsync(r => iLAIDs.Contains(r.Id), new[] { "ILA_EnablingObjective_Links.EnablingObjective.TestItems.TestItemMCQs", "Provider" })).ToList();
            return iLAs.ToList();

        }
        public async System.Threading.Tasks.Task<List<ILA>> GetAllILAsByILAIdAsync(int ilaId)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.ILA, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.ILA, bool>>>();
            predicates.Add(ila => ila.Id == ilaId);

            var iLA = (await FindWithIncludeAsync(predicates, new[] { "ILA_TaskObjective_Links" })).ToList();
            return iLA.ToList();

        }
        // New Application Services

        public async System.Threading.Tasks.Task<ILA> GetLocationByIdAsync(int? iLaId)
        {
            var ila = await FindAsync(x => x.Id == iLaId);
            return ila.FirstOrDefault();
        }

        public async Task<List<ILA>> GetTestItemsByILAAsync(List<int> list)
        {
            var ilas = await FindWithIncludeAsync(r => list.Contains(r.Id), new string[] {
                "Provider",
                "ILATraineeEvaluations.Test.Test_Item_Links.TestItem.TestItemTrueFalses",
                "ILATraineeEvaluations.Test.Test_Item_Links.TestItem.TestItemFillBlanks",
                "ILATraineeEvaluations.Test.Test_Item_Links.TestItem.TestItemMatches",
                "ILATraineeEvaluations.Test.Test_Item_Links.TestItem.TestItemMCQs",
                "ILATraineeEvaluations.Test.Test_Item_Links.TestItem.TestItemShortAnswers",
                "ILATraineeEvaluations.Test.Test_Item_Links.TestItem.EnablingObjective"
               });

            return ilas.ToList();
        }

        public async Task<List<ILA>> GetILAsWithCertificationInformationAsync(List<int> ilaIds)
        {
            return (await FindWithIncludeAsync(r => ilaIds.Contains(r.Id), new string[] { "ILACertificationLinks.Certification.CertifyingBody", "ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement", "Provider" })).ToList();
        }

        public async Task<List<ILA>> GetCompactedILA()
        {
            var ilas = await AllQuery().Select(s => new ILA
            {
                Id = s.Id,
                Active = s.Active,
                ProviderId = s.ProviderId,
                Name = s.Name,
                Number = s.Number,
                Description = s.Description,
            }).ToListAsync();

            return ilas;
        }

        public async Task<ILA> GetCompactedILA(int ilaId)
        {
            var ila = await FindQuery(x => x.Id == ilaId, true).Select(s => new ILA
            {
                Id = s.Id,
                Active = s.Active,
                ProviderId = s.ProviderId,
                Name = s.Name,
                Number = s.Number,
                Description = s.Description,
            }).FirstOrDefaultAsync();

            return ila;
        }


        public async Task<List<ILA>> GetCompactedILAActiveOnly()
        {
            var ilas = await FindQuery(x => x.Active, true).Select(s => new ILA
            {
                Id = s.Id,
                Active = s.Active,
                ProviderId = s.ProviderId,
                Name = s.Name,
                Number = s.Number,
                Description = s.Description,
            }).ToListAsync();

            return ilas;
        }

        public async Task<List<ILA>> GetCoursesByILAAsync()
        {
            try
            {
                var ilas = await AllWithIncludeAsync(new[] { "Provider" });
                return ilas.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ILA>> GetCompactedByIds(List<int> ilaIds)
        {
            var ilas = await FindQuery(x => ilaIds.Contains(x.Id), true).Select(s => new ILA
            {
                Id = s.Id,
                Active = s.Active,
                ProviderId = s.ProviderId,
                Name = s.Name,
                Number = s.Number,
                Description = s.Description,
            }).ToListAsync();

            return ilas;
        }

        public async Task<ILA> GetWithAllClassesAndStudentsAsync(int iLAId)
        {
            var ilas = await FindWithIncludeAsync(r => r.Id == iLAId, new string[] {
                "ClassSchedules.ClassSchedule_Employee.Employee.Person"
               });

            return ilas.FirstOrDefault();
        }

        public async Task<string> GetNameByIdAsync(int ilaId)
        {
            var ila = await GetAsync(ilaId);
            return ila?.Name;
        }

        public async Task<List<ILA>> GetAllActiveILAsWithNamesAsync()
        {
            return (await FindAsync(r => r.Active)).ToList();
        }

        public async Task<ILA> GetFullILADetailsAsync(int ilaId)
        {
            var ila = (await FindWithIncludeAsync(r => r.Id == ilaId, new string[] {
                "ILACertificationLinks.Certification.CertifyingBody",
                "ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement",
                //"ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_Topic",
                //"ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_SubCategory",
                //"ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_Category",
                //"ILA_Segment_Links.Segment.SegmentObjective_Links.Task.SubdutyArea.DutyArea",
                //"ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.EnablingObjective_Topic",
                //"ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.EnablingObjective_SubCategory",
                //"ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.EnablingObjective_Category",
                "ILA_TrainingTopic_Links.TrainingTopic.TrainingTopic_Category",
                "Provider"
            })).First();
            var ilaWithSegmentLinks = (await FindWithIncludeAsync(r => r.Id == ilaId, new string[] {
                "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_Topic",
                "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_SubCategory",
                "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_Category",
                "ILA_Segment_Links.Segment.SegmentObjective_Links.Task.SubdutyArea.DutyArea",
                "ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.EnablingObjective_Topic",
                "ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.EnablingObjective_SubCategory",
                "ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.EnablingObjective_Category",
                "Provider"
            })).First();
            ila.ILA_Segment_Links = ilaWithSegmentLinks.ILA_Segment_Links;
            var ilaWithTaskObjectivesLinks = (await FindWithIncludeAsync(r => r.Id == ilaId, new string[] { "ILA_TaskObjective_Links.Task.SubdutyArea.DutyArea" })).First();
            var ilaWithEOLinks = (await FindWithIncludeAsync(r => r.Id == ilaId, new string[] { "ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category" })).First();
            var ilaWithCustomEOLinks = (await FindWithIncludeAsync(r => r.Id == ilaId, new string[] { "ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category" })).First();
            ila.ILA_TaskObjective_Links = ilaWithTaskObjectivesLinks.ILA_TaskObjective_Links;
            ila.ILA_EnablingObjective_Links = ilaWithEOLinks.ILA_EnablingObjective_Links;
            ila.ILACustomObjective_Links = ilaWithCustomEOLinks.ILACustomObjective_Links;
            return ila;
        }

        public async System.Threading.Tasks.Task<ILA> GetILARequirementsDetailsByILAId(int ilaId)
        {
            return (await FindWithIncludeAsync(r => r.Id == ilaId, new string[] {
                "ILACertificationLinks","Provider","ClassSchedules.Instructor","ClassSchedules.Location"
            })).First();
        }

        public async System.Threading.Tasks.Task<List<ILA>> GetILAsWithProvider(List<int> ilaIds)
        {
            return (await FindWithIncludeAsync(r => ilaIds.Contains(r.Id), new[] { "Provider" })).ToList();
        }

        public async Task<List<ILA>> GetSelfRegistrationAvailableCourseAsync()
        {
            var ilas = await FindQueryWithIncludeAsync(x => x.ILA_SelfRegistrationOption != null && x.ILA_SelfRegistrationOption.MakeAvailableForSelfReg, new string[] {
                 "ILA_SelfRegistrationOption",
                 "ClassSchedules.ClassSchedule_Employee.Employee.Person",
                 "ClassSchedules.Instructor",
                 "ClassSchedules.Location",
                 "ClassSchedules.ClassSchedule_Employee.Employee.EmployeePositions.Position",
                 "Provider"
             }).ToListAsync();

            return ilas;
        }

        public async Task<ILA> GetForCopy(int id)
        {
            var ilaCopy = FindQueryWithIncludeAsync(r => r.Id == id, new string[]
            {
                "ILACertificationLinks.ILACertificationSubRequirementLink",
                "ILA_NercStandard_Links",
                "ILA_SafetyHazard_Links",
                "ILA_Position_Links",
                "ILACollaborators",
                "ILA_TaskObjective_Links",
                "ILA_EnablingObjective_Links",
                "ILA_Procedure_Links",
                "ILA_TrainingTopic_Links",
                "ILA_RegRequirement_Links",
                "ILA_AssessmentTool_Links",
                "Procedure_ILA_Links",
             }, true).First();

            var ilaWithLinks = FindQueryWithIncludeAsync(r => r.Id == id, new string[]
           {
                "ILA_PreRequisite_Links",
                "ILA_NERCAudience_Links",
                "ILACustomObjective_Links",
           }, true).First();
            var ilaWithLinksAgain = FindQueryWithIncludeAsync(r => r.Id == id, new string[]
            {
                "ILA_Uploads",
                "Task_ILA_Links",
                "SimulatorScenarioILA_Links",
                "EvaluationReleaseEMPSetting",
                "ILA_SelfRegistrationOption",
                "TQILAEmpSettings",
                "ILA_Evaluator_Links",
                "ILA_Resources",
                "TrainingPrograms_ILA_Links",
            }, true).First();

            var ilaTraineeEvaluationCopy = FindQueryWithIncludeAsync(r => r.Id == id, new string[]
            {
                "ILATraineeEvaluations.Test.Test_Item_Links"
            }, true).First();
            var ilaStudentEvaluationCopy = FindQueryWithIncludeAsync(r => r.Id == id, new string[]
            {
                "ILA_StudentEvaluation_Links.StudentEvaluationForm.StudentEvaluationQuestions"
            }, true).First();
            var ilaWithSegments = FindQueryWithIncludeAsync(r => r.Id == id, new string[]
           {
                "ILA_Segment_Links.Segment.SegmentObjective_Links"
           }, true).First();

            var ilaWithTopics = FindQueryWithIncludeAsync(r => r.Id == id, new string[]
           {
                "ILA_Topic_Links"
           }, true).First();

            var ilaWithTestReleaseEmpSettings = FindQueryWithIncludeAsync(r => r.Id == id, new string[]
           {
                "TestReleaseEMPSettings.TestReleaseEMPSetting_Retake_Links",
           }, true).First();

            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            predicates.Add(r => r.Id == id);
            var ilaWithCBTs = await FindWithIncludeAsync(predicates, new string[] { "CBTs.ScormUploads" }, true);

            ilaCopy.ILA_PreRequisite_Links = ilaWithLinks.ILA_PreRequisite_Links;
            ilaCopy.ILA_NERCAudience_Links = ilaWithLinks.ILA_NERCAudience_Links;
            ilaCopy.ILACustomObjective_Links = ilaWithLinks.ILACustomObjective_Links;

            ilaCopy.ILA_Uploads = ilaWithLinksAgain.ILA_Uploads;
            ilaCopy.SimulatorScenarioILA_Links = ilaWithLinksAgain.SimulatorScenarioILA_Links;
            ilaCopy.Task_ILA_Links = ilaWithLinksAgain.Task_ILA_Links;
            ilaCopy.EvaluationReleaseEMPSetting = ilaWithLinksAgain.EvaluationReleaseEMPSetting;
            ilaCopy.TestReleaseEMPSettings = ilaWithTestReleaseEmpSettings.TestReleaseEMPSettings;
            ilaCopy.ILA_SelfRegistrationOption = ilaWithLinksAgain.ILA_SelfRegistrationOption;
            ilaCopy.TQILAEmpSettings = ilaWithLinksAgain.TQILAEmpSettings;
            ilaCopy.ILA_Evaluator_Links = ilaWithLinksAgain.ILA_Evaluator_Links;
            ilaCopy.ILA_Resources = ilaWithLinksAgain.ILA_Resources;
            ilaCopy.TrainingPrograms_ILA_Links = ilaWithLinksAgain.TrainingPrograms_ILA_Links;

            ilaCopy.ILATraineeEvaluations = ilaTraineeEvaluationCopy.ILATraineeEvaluations;

            ilaCopy.ILA_StudentEvaluation_Links = ilaStudentEvaluationCopy.ILA_StudentEvaluation_Links;

            ilaCopy.ILA_Segment_Links = ilaWithSegments.ILA_Segment_Links;
            ilaCopy.ILA_Topic_Links = ilaWithTopics.ILA_Topic_Links;
            ilaCopy.CBTs = ilaWithCBTs.FirstOrDefault()?.CBTs;

            return ilaCopy;
        }

        public async Task<List<ILA>> GetActiveILADetailsAsync()
        {
            var ilaProviders = (await FindWithIncludeAsync(x => x.Active == true, new[] { "Provider" })).ToList();
            var ilaDeliveryMethod = (await FindWithIncludeAsync(x => x.Active == true, new[] { "DeliveryMethod" })).ToList();
            var ilaCertificateLinks = (await FindWithIncludeAsync(x => x.Active == true, new string[] { "ILACertificationLinks" })).ToList();
            foreach (var ilaDetail in ilaProviders)
            {
                ilaDetail.DeliveryMethod = ilaDeliveryMethod.Where(x => x.Id == ilaDetail.Id).FirstOrDefault().DeliveryMethod;
                ilaDetail.ILACertificationLinks = ilaCertificateLinks.Where(x => x.Id == ilaDetail.Id).FirstOrDefault().ILACertificationLinks;
            }
            return ilaProviders.ToList();
        }

        public async System.Threading.Tasks.Task<ILA> GetILAWithILACertLinksAsync(int id)
        {
            var ila = (await FindWithIncludeAsync(r => r.Id == id, new[] { "ILACertificationLinks.ILACertificationSubRequirementLink", "ILACertificationLinks.Certification.CertifyingBody" })).FirstOrDefault();
            return ila;
        }

        public async Task<List<ILA>> GetByListOfIdsAsync(int[] iLAIds)
        {
            return (await FindAsync(r => iLAIds.Contains(r.Id))).ToList();
        }

        public async Task<List<ILA>> GetSelfPacedCoursesByILAAsync()
        {
            return (await FindWithIncludeAsync(r => r.IsSelfPaced, new[] { "Provider" })).ToList();
        }

        public async Task<ILA> GetWithILAEvalsAsync(int ilaId)
        {
            var ila = (await FindWithIncludeAsync(r => r.Id == ilaId, new[] { "ILA_StudentEvaluation_Links.StudentEvaluationForm" })).FirstOrDefault();
            return ila;
        }

        public async Task<List<ILA>> GetILAsWithCertificationLinksOnlyAsync(List<int> ilaIds)
        {
            return (await FindWithIncludeAsync(r => ilaIds.Contains(r.Id), new string[] { "ILACertificationLinks.Certification.CertifyingBody", "Provider" })).ToList();
        }

        public async Task<List<ILA>> GetILAsWithObjectivesForMetaILAAsync(List<int> ilaIds, List<int> objectiveTypeIds)
        {
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            if (ilaIds == null || !ilaIds.Any())
            {
                return new List<ILA>();
            }

            if (ilaIds != null && ilaIds.Any())
            {
                predicates.Add(r => ilaIds.Contains(r.Id));
            }

            var ilas = await FindWithIncludeAsync(predicates, new[] { "DeliveryMethod" }, true);

            var allTaskObjectives = objectiveTypeIds.Intersect(new[] { 1, 2 }).Any()
                ? await FindWithIncludeAsync(predicates,
                    new[] { "ILA_TaskObjective_Links.Task.SubdutyArea.DutyArea", "ILA_TaskObjective_Links.Task.Task_MetaTask_Links.Task.SubdutyArea.DutyArea" }, true)
                : new List<ILA>();

            var allEnablingObjectives = objectiveTypeIds.Intersect(new[] { 3, 4, 5 }).Any()
                ? await FindWithIncludeAsync(predicates,
                    new[] { "ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Topic",
                    "ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Category",
                    "ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_SubCategory",
                    "ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_MetaEO_Links.EnablingObjective.EnablingObjective_Category.EnablingObjective_SubCategories.EnablingObjective_Topics" }, true)
                : new List<ILA>();

            var allCustomObjectives = objectiveTypeIds.Contains(6)
                ? await FindWithIncludeAsync(predicates,
                    new[] { "ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_Category.EnablingObjective_SubCategories.EnablingObjective_Topics" }, true)
                : new List<ILA>();

            foreach (var ila in ilas)
            {
                ila.ILA_TaskObjective_Links = allTaskObjectives.FirstOrDefault(x => x.Id == ila.Id)?.ILA_TaskObjective_Links ?? new List<ILA_TaskObjective_Link>();
                ila.ILA_EnablingObjective_Links = allEnablingObjectives.FirstOrDefault(x => x.Id == ila.Id)?.ILA_EnablingObjective_Links ?? new List<ILA_EnablingObjective_Link>();
                ila.ILACustomObjective_Links = allCustomObjectives.FirstOrDefault(x => x.Id == ila.Id)?.ILACustomObjective_Links ?? new List<ILACustomObjective_Link>();
            }

            return ilas.ToList();
        }

        public async System.Threading.Tasks.Task<List<ILA>> GetILAWithProvidersAsync()
        {
            return (await FindWithIncludeAsync(r => r.Provider.IsNERC, new[] { "Provider" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<ILA>> GetILAsByILAIdAsync(List<int> ilaIds)
        {
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            predicates.Add(r => ilaIds.Contains(r.Id));
            var iLAs = await FindWithIncludeAsync(predicates, new[] { "Provider" });

            var ilasWithEOs = await FindWithIncludeAsync(predicates, new[] {
                   "ILA_EnablingObjective_Links.EnablingObjective","ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Topic","ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Category","ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_SubCategory"
                  });

            var ilasWithTasks = await FindWithIncludeAsync(predicates, new[] { "ILA_TaskObjective_Links.Task.SubdutyArea.DutyArea" });

            var ilasWithCustomEos = await FindWithIncludeAsync(predicates, new[] { "ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_Category.EnablingObjective_SubCategories.EnablingObjective_Topics" });
            var ilasWithCertifications = await FindWithIncludeAsync(predicates, new[] { "ILACertificationLinks.Certification", "ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement" });
            var ilasWithSegments = await FindWithIncludeAsync(predicates,
                new[] {
                 "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_Topic",
                 "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_Category",
                 "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.EnablingObjective_SubCategory",
                 "ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.EnablingObjective_Category.EnablingObjective_SubCategories.EnablingObjective_Topics",
                 "ILA_Segment_Links.Segment.SegmentObjective_Links.Task.SubdutyArea.DutyArea",
                 "ILA_Segment_Links.Segment.SegmentObjective_Links.EnablingObjective.ILA_EnablingObjective_Links",
                 "ILA_Segment_Links.Segment.SegmentObjective_Links.CustomEnablingObjective.ILACustomObjective_Links",
                 "ILA_Segment_Links.Segment.SegmentObjective_Links.Task.ILA_TaskObjective_Links",
               });

            var ilasWithTrainingTopics = await FindWithIncludeAsync(predicates, new[] { "ILA_TrainingTopic_Links.TrainingTopic" });
            foreach (var ila in iLAs)
            {
                ila.ILA_Segment_Links = ilasWithSegments.Where(r => r.Id == ila.Id).First().ILA_Segment_Links;
                ila.ILA_EnablingObjective_Links = ilasWithEOs.Where(r => r.Id == ila.Id).First().ILA_EnablingObjective_Links;
                ila.ILACertificationLinks = ilasWithCertifications.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILACertificationLinks;
                ila.ILA_TaskObjective_Links = ilasWithTasks.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_TaskObjective_Links;
                ila.ILACustomObjective_Links = ilasWithCustomEos.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILACustomObjective_Links;
                ila.ILA_TrainingTopic_Links = ilasWithTrainingTopics.Where(r => r.Id == ila.Id).FirstOrDefault()?.ILA_TrainingTopic_Links;
            }

            return iLAs.ToList();
        }

        public async Task<List<ILA>> GetAllILAsWithDeliveryMethodAsync(string ilasType)
        {
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            switch (ilasType.Trim().ToUpper())
            {
                case "ACTIVE":
                    predicates.Add(x => x.Active == true);
                    break;
                case "DRAFT":
                    predicates.Add(x => x.Active == true && x.IsPublished == false);
                    break;
                case "PUBLISHED":
                    predicates.Add(x => x.IsPublished == true && x.Active == true);
                    break;
                default:
                    break;
            }
            var ilas = (await FindWithIncludeAsync(predicates, new[] { "DeliveryMethod" })).ToList();
            return ilas;
        }

        public async Task<List<ILA>> GetILAsNotLinkedToTopic()
        {
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            predicates.Add(x => !x.ILA_Topic_Links.Any());
            predicates.Add(x => x.Active);
            var ilas = (await FindWithIncludeAsync(predicates, new[] { "DeliveryMethod" })).ToList();
            return ilas;
        }

        public async Task<ILA> GetILAWithTrainingTopics(int id)
        {
            return (await FindWithIncludeAsync(x => x.Id == id, new[] { "ILA_TrainingTopic_Links.TrainingTopic" })).FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<ILA> GetTrainingPlanAsync(int ilaId)
        {
            var ila = (await FindAsync(r => r.Id == ilaId)).FirstOrDefault();
            return ila;
        }

        public async Task<ILA> GetILAWithProviderAndDeliveryMethodAsync(int id)
        {
            return (await FindWithIncludeAsync(x => x.Id == id, new[] { "DeliveryMethod", "Provider" })).FirstOrDefault();
        }

        public async Task<List<ILA>> GetILAsWithCertificationSubRequirementsAsync(int ilaId)
        {
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            predicates.Add(p => p.Id == ilaId);
            return (await FindWithIncludeAsync(predicates, new string[] { "ILACertificationLinks.Certification.CertifyingBody", "ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement" }, true)).ToList();
        }

        public async Task<ILA> GetILARequirementsAsync(int ilaId)
        {
            List<Expression<Func<ILA, bool>>> predicates = new List<Expression<Func<ILA, bool>>>();
            predicates.Add(p => p.Id == ilaId);
            var ilaRequirement = await FindWithIncludeAsync(predicates, new string[] { "ILA_TaskObjective_Links", "ILATraineeEvaluations.TestType", "ILA_StudentEvaluation_Links", "SimulatorScenario_ILAs", "ClassSchedules.ClassSchedule_Employee" });
            return ilaRequirement.FirstOrDefault();
        }
        public async Task<ILA> GetILAByNameOrNumberAsync(string name, string number)
        {
            return (await FindAsync(x => x.Name == name && x.Number == number)).FirstOrDefault();
        }
    }
}

