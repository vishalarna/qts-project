using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ClassScheduleService : Common.Service<ClassSchedule>, IClassScheduleService
    {
        public ClassScheduleService(IClassScheduleRepository repository, IClassScheduleValidation validation)
            : base(repository, validation)
        {

        }
        public async System.Threading.Tasks.Task<IEnumerable<ClassSchedule>> GetClassScheduleListAsync()
        {
            var classList = await FindWithIncludeAsync(r => r.ILAID != null, new[] { "ILA", "Provider" });
            return classList;
        }
        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetTrainingScheduleByClass(DateTime startDate, DateTime EndDate, List<int> employee, string inactiveIla)
        {
            var classSchedules = await AllWithIncludeAsync(new[] { "ILA", "Provider", "Instructor", "Location", "EmpTests.Employee.Person" });
            classSchedules = classSchedules.Where(r => r.StartDateTime <= startDate && r.EndDateTime >= EndDate).ToList();
            return classSchedules.ToList();
        }

        //New Application Services

        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetClassSchedulesByIdAsync(int ilaId)
        {
            var classSchedules = await FindAsync(x => x.ILAID == ilaId);
            return classSchedules.ToList();
        }
        public async System.Threading.Tasks.Task<ClassSchedule> GetClassScheduleByIdAsync(int classScheduleId)
        {
            var classSchedules = await FindAsync(x => x.Id == classScheduleId);
            return classSchedules.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetClassesByILAAsync(List<int> iLAIDs, DateTime startDate, DateTime endDate, List<int> instructorIds, List<int> locationIds)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            if (iLAIDs != null)
            {
                predicates.Add(r => iLAIDs.Contains(r.ILAID.GetValueOrDefault()));
            }
            if (instructorIds.Count()> 0)
            {
                predicates.Add(r => instructorIds.Contains(r.InstructorId.GetValueOrDefault()));
            }
            if (locationIds.Count() > 0)
            {
                predicates.Add(r => locationIds.Contains(r.LocationId.GetValueOrDefault()));
            }
            predicates.Add(r => r.StartDateTime.Date >= startDate && r.EndDateTime.Date <= endDate);
            var classes = (await FindWithIncludeAsync(predicates, new[] { "Instructor", "Location", "ILA.Provider" })).ToList();
            return classes;
        }


        public async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.ClassSchedule>> GetTrainingScheduleByClassAsync(string activeEmployeesDescription, List<int> classScheduleIDs)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();

            if (activeEmployeesDescription == "Active Only")
            {
                predicates.Add(r => r.Active);
            }
            if (classScheduleIDs != null)
            {
                predicates.Add(r => classScheduleIDs.Contains(r.Id));
            }

            var classSchedules = await FindWithIncludeAsync(predicates, new[] { "Location", "ClassSchedule_Employee.Employee.Person" });

            return classSchedules.ToList();
        }
        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetStudentEvalutationResultsInstructorAsync(List<int> classScheduleIDs, int instructorId)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(r => classScheduleIDs.Contains(r.Id) && r.InstructorId == instructorId);
            var classSchedule = (await FindWithIncludeAsync(predicates, new[] { "Instructor", "Location", "ILA.Provider", "StudentEvaluationWithoutEmps.RatingScaleExpanded" })).ToList();

            return classSchedule;
        }

        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetStudentEvalutationResultsInstructorAsync(List<int> classScheduleIDs)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(r => classScheduleIDs.Contains(r.Id));

            var classSchedule = (await FindWithIncludeAsync(predicates, new[] { "Instructor", "Location", "ILA.Provider", "StudentEvaluationWithoutEmps.RatingScaleExpanded" })).ToList();
            return classSchedule;
        }

        public async Task<List<ClassSchedule>> GetAllInstructorLedClassSchedulesAsync()
        {
            return (await FindWithIncludeAsync(r => !r.ILA.IsSelfPaced, new string[] { "ILA.Provider", "Location", "ILA.DeliveryMethod" })).ToList();
        }

        public async Task<List<ClassSchedule>> GetByListOfIdsWithEmployeesAsync(List<int> list)
        {
            return (await FindWithIncludeAsync(r => list.Contains(r.Id), new string[] { "ILA.Provider", "ClassSchedule_Employee.Employee.Person", "ClassSchedule_Evaluation_Rosters" })).ToList();
        }

        public async Task<List<ClassSchedule>> GetClassRosterByIdsAsync(List<int> list)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(r => list.Contains(r.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, new string[] { "ILA.Provider", "ClassSchedule_Employee.Employee.Person", "Instructor", "Location", "ClassSchedule_Employee.Employee.EmployeePositions.Position", "ClassSchedule_Employee.Employee.EmployeeCertifications.Certification", "ClassSchedule_Employee.Employee.EmployeeOrganizations.Organization", "ClassSchedule_Employee.Employee.ClassSchedule_Rosters" })).ToList();
            foreach (var schedule in classSchedules)
            {
                schedule.ClassSchedule_Employee = schedule.ClassSchedule_Employee.Where(r => r.Active).Where(r => !r.Deleted).Where(r => r.IsEnrolled).ToList();

                foreach (var classScheduleEmp in schedule.ClassSchedule_Employee)
                {
                    classScheduleEmp.Employee.EmployeePositions = classScheduleEmp.Employee.EmployeePositions.Where(x => x.Active == true).ToList();
                    classScheduleEmp.Employee.EmployeeCertifications = classScheduleEmp.Employee.EmployeeCertifications.Where(x => x.Active == true && x.ExpirationDate >= DateOnly.FromDateTime(DateTime.UtcNow) && (x.Certification?.Active ?? false)).ToList();
                }
            }
            return classSchedules;
        }

        public async Task<List<ClassSchedule>> GetClassSignInSheetByIdsAsync(List<int> list)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(r => list.Contains(r.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, new string[] { "ILA.Provider", "ClassSchedule_Employee.Employee.Person", "Instructor", "Location", "ClassSchedule_Employee.Employee.EmployeePositions.Position", "ClassSchedule_Employee.Employee.EmployeeCertifications.Certification", "ClassSchedule_Employee.Employee.EmployeeOrganizations.Organization", "ClassSchedule_Employee.Employee.ClassSchedule_Rosters" })).ToList();
            foreach (var schedule in classSchedules)
            {
                foreach (var classScheduleEmp in schedule.ClassSchedule_Employee)
                {
                    classScheduleEmp.Employee.EmployeePositions = classScheduleEmp.Employee.EmployeePositions.Where(x => x.Active == true).ToList();
                    classScheduleEmp.Employee.EmployeeCertifications = classScheduleEmp.Employee.EmployeeCertifications.Where(x => x.Active && x.ExpirationDate >= DateOnly.FromDateTime(DateTime.UtcNow) && (x.Certification?.Active ?? false)).ToList();
                }
            }
            return classSchedules;
        }

        public async Task<ClassSchedule> GetWithStudentsAsync(int classScheduleId)
        {
            var cs = await FindWithIncludeAsync(r => r.Id == classScheduleId, new string[] { "ClassSchedule_Employee" });
            return cs.FirstOrDefault();
        }

        public async Task<List<ClassSchedule>> GetClassesThatNeedEmpCourseNotificationCatchupAsync()
        {
            var cs = await FindWithIncludeAsync(r => r.ILA.CBTRequiredForCourse && r.ILA.CBTs.Where(r => r.Active).Any(), new string[] { "ILA.CBTs", "ClassSchedule_Employee.ScormRegistration", "ClassSchedule_Employee.Employee" });
            cs = cs.Where(r => DateTime.Now < getCbtDueDate(r));
            return cs.ToList();
        }

        private DateTime getCbtDueDate(ClassSchedule classSchedule)
        {
            var cbt = classSchedule.ILA.CBTs.Where(r => r.Active).FirstOrDefault();

            if (cbt == null)
                return DateTime.MaxValue;

            return cbt.GetDueDate(classSchedule.EndDateTime);
        }

        public async Task<List<ClassSchedule>> GetClassesThatNeedStudentEvaluationNotificationCatchupAsync()
        {
            var cs = await FindWithIncludeAsync(r => r.ClassSchedule_StudentEvaluations_Links.Any(), new string[] { "ILA.EvaluationReleaseEMPSetting", "StudentEvaluationWithoutEmps", "ClassSchedule_StudentEvaluations_Links" });
            cs = cs.Where(r => (r.ILA.EvaluationReleaseEMPSetting != null ? r.ILA.EvaluationReleaseEMPSetting.GetDueDate(r.EndDateTime) : r.EndDateTime.AddDays(10)) > DateTime.Now);
            return cs.ToList();
        }

        public async Task<string> GetFormattedStartDateByIdAsync(int classScheduleId)
        {
            var classSchedule = await GetAsync(classScheduleId);
            return classSchedule != null ? classSchedule.StartDateTime.ToString("yyyy-MM-dd") : null;
        }

        public async Task<ClassSchedule> GetWithDetailsAsync(int classScheduleId)
        {
            var classes = await FindWithIncludeAsync(r => r.Id == classScheduleId, new[] { "ILA.Provider", "Location", "Instructor" });
            return classes.FirstOrDefault();
        }

        public async Task<List<ClassSchedule>> GetClassSchedulesWithSelfRegistrationAvailableByIlaId(int iLAID)
        {
            var classes = await FindWithIncludeAsync(r => r.ILA.ILA_SelfRegistrationOption.MakeAvailableForSelfReg && r.StartDateTime > DateTime.Now.ToUniversalTime(), new[] { "ILA.Provider" });
            return classes.ToList();
        }

        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetSelfRegAvailableCoursesAsync()
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(x => x.ClassSchedule_SelfRegistrationOption != null);
            predicates.Add(x => x.ClassSchedule_SelfRegistrationOption.MakeAvailableForSelfReg);
            var classSchedules = await FindWithIncludeAsync(predicates, new string[] {
                "ClassSchedule_SelfRegistrationOption",
                 "ClassSchedule_Employee",
                 "Instructor",
                 "Location",
                 "ILA.ILA_Position_Links",
                 "ILA.ILA_SelfRegistrationOption"
             },true);
            return classSchedules.ToList();

        }

        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetClassScheduleRecurrences(int classScheduleId)
        {
            var recurrences = await FindAsync(x => x.RecurrenceId == classScheduleId && !x.IsRecurring);
            return recurrences.ToList();
        }

        public async System.Threading.Tasks.Task<ClassSchedule> GetClassScheduleWithEmployeesAndRostersAsync(int classScheduleId)
        {
            var classSchedules = await FindDeletedWithIncludeAsync(r => r.Id == classScheduleId, new[] { "ClassSchedule_Employee", "ClassSchedule_Rosters", "ClassSchedule_Evaluation_Rosters", "IDPSchedules" });
            return classSchedules.FirstOrDefault();
        }
        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetAllAsync()
        {
            var classSchedules = await AllWithIncludeAsync(new string[] { "ILA.Provider", "Instructor", "Location" });
            return classSchedules.ToList();
        }
        
        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetAllSelfPacedClassSchedulesAsync()
        {
            var classSchedules = await FindWithIncludeAsync((r => r.ILA.CBTs.Any()), new string[] { "ILA.Provider", "Instructor", "Location" });
            return classSchedules.ToList();
        }

        public async Task<List<ClassSchedule>> GetClassScheduleForEMPTestSummarybyClasses(List<int> classScheduleIds, List<int> testIds, List<int> employeeIds, bool showOnlyFailedGrades)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(cs => classScheduleIds.Contains(cs.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, new string[] {
                "ClassSchedule_TestReleaseEMPSettings",
                 "ClassSchedule_Rosters.TestType",
                 "ClassSchedule_Rosters.TimeRecords","ClassSchedule_Employee"
             }, true)).ToList();

            foreach (var classSchedule in classSchedules)
            {
                if (employeeIds.Count > 0)
                {
                    classSchedule.ClassSchedule_Rosters = classSchedule.ClassSchedule_Rosters.Where(csr => employeeIds.Contains(csr.EmpId)).ToList();
                }

                classSchedule.ClassSchedule_Rosters = classSchedule.ClassSchedule_Rosters.Where(csr => testIds.Contains(csr.TestId)).ToList();

                if (showOnlyFailedGrades)
                {
                    classSchedule.ClassSchedule_Rosters = classSchedule.ClassSchedule_Rosters.Where(x => x.Grade == "F").ToList();
                }
            }

            return classSchedules.ToList();
        }

        public async Task<List<ClassSchedule>> GetClassSchedulesForEMPTestStatistics(List<int> classScheduleIds)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(cs => classScheduleIds.Contains(cs.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, new string[] {
                "ClassSchedule_TestReleaseEMPSettings",
                "ILA.ILATraineeEvaluations.TestType",
                 "ClassSchedule_Rosters.Employee.Person",
                 "ClassSchedule_Rosters.Responses.Selections",
             }, true)).ToList();

            return classSchedules.ToList();
        }

        public async Task<List<ClassSchedule>> GetClassSchedulesForTrainingProgramAsync(List<int> classScheduleIds)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(cs => classScheduleIds.Contains(cs.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, new string[] { "ClassSchedule_Employee", "Instructor", "Location" }, true)).ToList();
            return classSchedules.ToList();
        }

        public async Task<ClassSchedule> GetClassScheduleWithIlaTestSettings(int classScheduleId)
        {
            var result = (await FindWithIncludeAsync(x => x.Id == classScheduleId, new string[] { "ILA.TestReleaseEMPSettings.TestReleaseEMPSetting_Retake_Links" })).FirstOrDefault();
            return result;
        }

        public async Task<ClassSchedule> GetClassScheduleWithIlaTQEMPSettings(int classScheduleId)
        {
            var result = (await FindWithIncludeAsync(x => x.Id == classScheduleId, new string[] { "ILA.TQILAEmpSettings", "ILA.ILA_Evaluator_Links" })).FirstOrDefault();
            return result;
        }

        public async Task<ClassSchedule> GetClassScheduleWithEvaluatorsAsync(int classScheduleId)
        {
            var result = (await FindWithIncludeAsync(x => x.Id == classScheduleId, new string[] { "ClassSchedule_Evaluator_Links" })).FirstOrDefault();
            return result;
        }
        public async Task<List<ClassSchedule>> GetRecurringClassSchedules(int? recurreneceId)
        {
            var classSchedules = (await FindAsync(r => r.RecurrenceId == recurreneceId)).ToList();
            return classSchedules.ToList();
        }

        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetProcedureAndRegulatoryRequirementByClassScheduleIdAsync(List<int> classScheduleIds)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(cs => classScheduleIds.Contains(cs.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, new string[] { "ILA.ILA_Procedure_Links.Procedure", "ILA.ILA_RegRequirement_Links.RegulatoryRequirement"}, true)).ToList();
            return classSchedules.ToList();
        }

        public async Task<List<ClassSchedule>> GetForSCORMCompletionSummaryByClasses(List<int> classScheduleIds, bool showOnlyFailedGrades)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(cs => classScheduleIds.Contains(cs.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, new string[] { "ILA.CBTs.ScormUploads.CBT_ScormRegistration.CBT_ScormRegistration_Responses.CBT_ScormUpload_Question_Choice.CBT_ScormUpload_Question", "ClassSchedule_Employee", "ClassSchedule_TestReleaseEMPSettings" }, true)).ToList();

            if (showOnlyFailedGrades)
            {
                foreach (var classSchedule in classSchedules)
                {
                    foreach (var cbt in classSchedule.ILA.CBTs)
                    {
                        foreach (var scormUpload in cbt.ScormUploads)
                        {
                            // Filter CBT_ScormRegistration to only include failed grades
                            scormUpload.CBT_ScormRegistration = scormUpload.CBT_ScormRegistration
                                .Where(registration => registration.Grade == "F")
                                .ToList();
                        }
                    }
                }
            }
            return classSchedules.ToList();
        }

        public async Task<List<ClassSchedule>> GetForSCORMTestCompletionStatistics(List<int> classScheduleIds)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(cs => classScheduleIds.Contains(cs.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, new string[] { "ClassSchedule_Employee", "ILA.CBTs.ScormUploads.CBT_ScormUpload_Question.CBT_ScormUpload_Question_Choices.CBT_ScormRegistration_Responses.CBT_ScormRegistration.ClassScheduleEmployee" }, true)).ToList();
            return classSchedules.Where(cs => cs.ClassSchedule_Employee.Any(cse => cse.IsEnrolled)).ToList();
        }

        public async Task<List<ClassSchedule>> GetForPretestAndFinalTestComparison(List<int> classScheduleIds)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(cs => classScheduleIds.Contains(cs.Id));
            var classSchedules = (await FindWithIncludeAsync(predicates, 
                new string[] { 
                    "ILA", 
                    "ClassSchedule_TestReleaseEMPSettings",
                    "ClassSchedule_Employee"
                }, true)).ToList();
            return classSchedules.ToList();
        }

        public async Task<ClassSchedule> GetClassScheduleByClassAndILAId(int classScheduleId, int ilaId)
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(x => x.ILAID == ilaId);
            predicates.Add(x => x.Id == classScheduleId);
            var classSchedule = (await FindWithIncludeAsync(predicates,
                new string[] {
                    "ILA.ILA_EnablingObjective_Links", "ILA.ILA_Procedure_Links.Procedure", "ILA.ILA_TaskObjective_Links", "ILA.ILA_NercStandard_Links", "ILA.ILACertificationLinks.Certification.CertifyingBody", "ILA.ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement", "Instructor", "Location"
                }, true)).FirstOrDefault();
            return classSchedule;
        }

        public async Task<List<ClassSchedule>> GetPublicallyAvailableClassSchedulesAsync()
        {
            List<Expression<Func<ClassSchedule, bool>>> predicates = new List<Expression<Func<ClassSchedule, bool>>>();
            predicates.Add(x => x.IsPubliclyAvailable);
            predicates.Add(x => x.StartDateTime > DateTime.UtcNow);
            predicates.Add(x => x.Active);
            return (await FindWithIncludeAsync(predicates, new string[] { "ILA.ILACertificationLinks", "ILA.DeliveryMethod", "Instructor", "Location" }, true)).ToList();
        }

        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetClassSchedulesByILAIdsAsync(List<int> ilaIds)
        {
            return (await FindAsync(x => ilaIds.Contains(x.ILAID.Value))).ToList();
        }

        public async System.Threading.Tasks.Task<List<ClassSchedule>> GetAllClassSchedulesAsync()
        {
            return (await AllWithIncludeAsync( new string[] { "ILA.Provider", "Location", "ILA.DeliveryMethod" })).ToList();
        }
    }
}
