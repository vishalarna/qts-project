using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassScheduleEmployeeService : Common.IService<ClassSchedule_Employee>
    {
        System.Threading.Tasks.Task<ClassSchedule_Employee> GetEmployeeForClassScheduleAsync(int classScheduleId, int employeeId);

        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetEmployeeSelfRegistrationDeniedCourse(int employeeId);

        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetEmployeeSelfRegistrationApprovedCourse(int employeeId);

        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetEmployeeSelfRegistrationDroppedCourse(int employeeId);

        System.Threading.Tasks.Task<int> GetEmployeeByClassScheduleByIdAsync(int employeeId, int ClassScheduleId);
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetForCEHExportAsync(int classScheduleId);
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetClassScheduleEmployeeByIdAndInclude(int empId, string[] includes);

        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetCSEmpsWithConditionAndIncludes(Expression<Func<ClassSchedule_Employee, bool>> predicate, string[] includes);

        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetCompactCSEmpsWithConditionAndIncludes(Expression<Func<ClassSchedule_Employee, bool>> predicate, string[] includes);
        Task<List<ClassSchedule_Employee>> GetForNercCertificationCalculationAsync(List<int> employeeIds);
        Task<List<ClassSchedule_Employee>> GetForEmergencyResponseCertificationCalculationAsync(List<int> employeeIds, List<int> certificationIds);
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetClassSchedulesForIDP(int iLAId, DateTime iDPYear, int employeeId);
        System.Threading.Tasks.Task<ClassSchedule_Employee> GetForNotificationAsync(int classScheduleRosterId);
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetForCertificationCalculationAsync(List<int> employeeIds);
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetCompletedOnlineCoursesForEmployeeAsync(int employeeId);
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetPendingOnlineCoursesForEmployeeAsync(int empId);
        System.Threading.Tasks.Task<ClassSchedule_Employee> GetByEmployeeIdAndClassScheduleIdAsync(int employeeId, int classScheduleId);
        System.Threading.Tasks.Task<string> GetEmployeeNameByIdAsync(int classScheduleEmployeeId);
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetWaitlistedAsync();
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetClassScheduleEmployeesReadyForRetakeAsync();
        System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetClassScheduleEmployeeByCBT(CBT cbt);
        Task<List<ClassSchedule_Employee>> GetClassSchdulesEmployeeByEmployeeIdAsync(List<int> employeeIds);
        Task<List<ClassSchedule_Employee>> GetClassCertificatesAsync(List<int> classScheduleIds, bool printForThoseWithNoGradeAwarded, bool includeFailedStudents);
        Task<List<ClassSchedule>> GetClassSchedulesForILAsAndEmployeeAsync(List<int> ilaIds, int employeeId);
    }
}
