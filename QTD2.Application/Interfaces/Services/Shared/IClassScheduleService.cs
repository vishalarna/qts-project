using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.EmpSelfRegistration;
using QTD2.Infrastructure.Model.ILA;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClassScheduleService
    {
        //public Task<List<ClassSchedule>> GetAsync();

        public Task<List<ClassScheduleVM>> GetAsync();

        public Task<ClassScheduleDetailVM> GetAsync(int id);

        public Task<ClassSchedule> CreateAsync(ClassScheduleCreateOptions options);

        public Task<List<ClassSchedule>> GetRecurrencesAsync(int classId, bool includeCurrentClass);

        public Task<CBT_ScormRegistration> GetCBT_ScormRegistrationAsync(int classScheduleId, int employeeId);

        public Task<ClassSchedule> UpdateAsync(int id, ClassScheduleCreateOptions options);

        public System.Threading.Tasks.Task<ClassSchedule> DeleteAsync(int id);

        public  Task<ClassSchedule> LinkEmployee(int classScheduleId, ClassSchedule_EmployeeCreateOptions options);
        public System.Threading.Tasks.Task UnlinkEmployee(int classScheduleId, int[] empIDs);

        public Task<List<ClassSchedule>> GetClassSchedulesEmployeeIsLinkedTo(int id);

        public Task<List<EmployeesLinkedToSchedule>> GetLinkedEmployees(int id);

        public Task<ScheduleClassesStats> GetStatsAsync();

        public Task<List<ScheduleEvalVM>> GetLinkedStudentEvalsAsync(int ilaId, int classId);

        public Task<List<ToDoILAsVM>> GetSelfRegNeedingApprovalAsync();

        public Task<List<ToDoILAsVM>> GetTestNeedingReReleaseAsync();

        public Task<List<ToDoILAsVM>> GetRetakeToReleaseAsync();

        public Task<List<ILASummaryVM>> GetILANeedingToBeScheduled();

        public Task<List<ToDoILAsVM>> GetWaitlistedDataAsync();

        public Task<ClassSchedule_SelfRegistrationOptions_ViewModel> GetClassSchedule_SelfRegistrationOptionsAsync(int id);
        public Task<ClassSchedule_SelfRegistrationOptions_ViewModel> CreateClassSchedule_SelfRegistrationAsync(ClassSchedule_RegistrationCreateOptions options);
        public System.Threading.Tasks.Task EnrollStudentAsync(ClassScheduleEnrollOptions options,bool isCallHander = true);
        public System.Threading.Tasks.Task EnrollStudentWithClassSizeByPassAsync(ClassScheduleEnrollOptions options);

        public System.Threading.Tasks.Task WaitListStudentAsync(ClassScheduleEnrollOptions options);

        public System.Threading.Tasks.Task DeclineEmployee(ClassScheduleEnrollOptions options);

        public  Task<List<ClassSchedule_Employee>> UpdateBulkGradeAsync(int id, ClassScheduleGradeCreateOptions options);
        public  Task<ClassSchedule_Employee> UpdateGradeAsync(int id, ClassScheduleGradeCreateOptions options);
        public  Task<ClassSchedule_Employee> UpdateScoreAsync(int id, ClassScheduleGradeCreateOptions options);
        public Task<ClassSchedule_Employee> UpdateNotesAsync(int id, ClassScheduleGradeCreateOptions options);

        public Task<List<ClassScheduleData>> GetClassSchedulesByILA(int ilaId);

        public Task<ClassSchedule> UpdateTrainingAsync(int id, ClassScheduleCreateOptions options);
        public Task<List<EmpSelfregistrationCourses>> GetSelfRegAvailableCoursesAsync(DateTime currentUtcDateTime);

        public System.Threading.Tasks.Task RegisterBySelfRegistrationAsync(int classId, int ilaId);

        public Task<List<EmpCourses>> GetSelfRegEmployeeAprovedCoursesAsync();

        public Task<List<EmpCourses>> GetSelfRegEmployeeDeniedCoursesAsync();

        public Task<List<EmpCourses>> GetSelfRegEmployeeDroppedCoursesAsync();

        public System.Threading.Tasks.Task DropCourseAsync(int classId, int ilaId);

        public System.Threading.Tasks.Task JoinWaitListAsync(int classId, int ilaId);

        public Task<ILAClassDetailsVM> ViewILAAndClassDetailAsync(int classId, int ilaId);

        public Task<List<ClassScheduleVM>> GetByStartDateAndEndDateAsync(DateTime startDate, DateTime endDate);

        public Task<List<ClassScheduleVM>> GetByILAIdAsync(int ilaId);

        public System.Threading.Tasks.Task ReReleaseTest(ReReleaseOptions options);

        //New Application Services

        public Task<List<EmpSelfregistrationCourses>> GetSelfRegAvailableCoursesByIdAsync(int employeeId);

        public Task<List<EmpCourses>> GetSelfRegEmployeeDroppedCoursesByIdAsync(int employeeId);

        public Task<List<EmpCourses>> GetSelfRegEmployeeApprovedCoursesByIdAsync(int employeeId);
        public Task<List<EmpCourses>> GetSelfRegEmployeeDeniedCoursesByIdAsync(int employeeId);

        public Task<ClassScheduleReviewDataVM> GetClassScheduleReviewData(int classId, int ilaId);       

        public Task<ClassSchedule_Employee> UpdateClassScheduleEnrollmentOptions(int id, ClassScheduleEnrollmentOptions options);
        public ClassScheduleDetailVM MapClassScheduleToClassScheduleDetailVM(ClassSchedule cls);

        public Task<List<PublicallyAvailableClassSchedulesVM>> GetPublicallyAvailableClassesAsync();

    }
}
