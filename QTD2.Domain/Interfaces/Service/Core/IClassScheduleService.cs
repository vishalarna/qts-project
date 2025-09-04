using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassScheduleService : Common.IService<ClassSchedule>
    {
        public System.Threading.Tasks.Task<IEnumerable<ClassSchedule>> GetClassScheduleListAsync();
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetTrainingScheduleByClass(DateTime startDate,DateTime EndDate, List<int> employee,string inactiveIla);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassSchedulesByIdAsync(int ilaId);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassesByILAAsync(List<int> iLAIDs, DateTime startDate, DateTime endDate, List<int> instructorIds, List<int> locationIds);
        public System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.ClassSchedule>> GetTrainingScheduleByClassAsync(string active,List<int> classScheduleIDs);
        public System.Threading.Tasks.Task<ClassSchedule> GetClassScheduleByIdAsync(int classScheduleId);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetStudentEvalutationResultsInstructorAsync(List<int> classScheduleIDs);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetAllInstructorLedClassSchedulesAsync();
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetByListOfIdsWithEmployeesAsync(List<int> enumerable);
        public System.Threading.Tasks.Task<ClassSchedule> GetWithStudentsAsync(int classScheduleId);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassesThatNeedEmpCourseNotificationCatchupAsync();
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassesThatNeedStudentEvaluationNotificationCatchupAsync();
        public System.Threading.Tasks.Task<ClassSchedule> GetWithDetailsAsync(int classScheduleId);
        public System.Threading.Tasks.Task<string> GetFormattedStartDateByIdAsync(int classScheduleId);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetSelfRegAvailableCoursesAsync();
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassSchedulesWithSelfRegistrationAvailableByIlaId(int iLAID);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassRosterByIdsAsync(List<int> enumerable);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassSignInSheetByIdsAsync(List<int> enumerable);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassScheduleRecurrences(int classScheduleId);
        public System.Threading.Tasks.Task<ClassSchedule> GetClassScheduleWithEmployeesAndRostersAsync( int classScheduleId);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetAllAsync();
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetAllSelfPacedClassSchedulesAsync();
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassScheduleForEMPTestSummarybyClasses(List<int> classScheduleIds, List<int> testIds, List<int> employeeIds,bool showOnlyFailedGrades);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassSchedulesForEMPTestStatistics(List<int> classScheduleIds);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassSchedulesForTrainingProgramAsync(List<int> classScheduleIds);
        public System.Threading.Tasks.Task<ClassSchedule> GetClassScheduleWithIlaTestSettings(int classScheduleId);
        public System.Threading.Tasks.Task<ClassSchedule> GetClassScheduleWithIlaTQEMPSettings(int classScheduleId);
        public System.Threading.Tasks.Task<ClassSchedule> GetClassScheduleWithEvaluatorsAsync(int classScheduleId);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetRecurringClassSchedules(int? recurreneceId); 
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetProcedureAndRegulatoryRequirementByClassScheduleIdAsync(List<int> classScheduleIds);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetForSCORMCompletionSummaryByClasses(List<int> classScheduleIds, bool showOnlyFailedGrades);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetForSCORMTestCompletionStatistics(List<int> classScheduleIds);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetForPretestAndFinalTestComparison(List<int> classScheduleIds);
        public System.Threading.Tasks.Task<ClassSchedule> GetClassScheduleByClassAndILAId(int classScheduleId,int ilaId);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetPublicallyAvailableClassSchedulesAsync();
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetClassSchedulesByILAIdsAsync(List<int> ilaIds);
        public System.Threading.Tasks.Task<List<ClassSchedule>> GetAllClassSchedulesAsync();
    }
}
