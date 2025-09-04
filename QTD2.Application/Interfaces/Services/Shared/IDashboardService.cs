using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Dashboard;
using QTD2.Infrastructure.Model.Employee;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IDashboardService
    {
        Task<object> GetDueTrainingsData(string date);
        Task<object> GetTrainingScheduleFinal(GetDueTrainingOptions options);
        Task<object> GetTrainingScheduleFinalInProgress(DateTime startDate, DateTime endDate);
        Task<object> GetTrainingScheduleToday(DateTime date);
        Task<RequiredEMPSettingVM> GetEMPSettings(int ilaID);
        Task<bool> CheckCourseAvailabilityForSelfRegestration();
        Task<ClassInfoVM> GetClassInfoAsync(int id);
        Task<object> GetCourseInfoByILAId(int ilaId);

        //New Application Services

        Task<Result<EmployeeDashboardStatsDto>> GetDashboardStatisticsByIdAsync(int employeeId);
        Task<bool> CheckCourseAvailabilityForSelfRegestration(int employeeId);

        Task<string> GetCurrentEmployeeName(int employeeId);

        Task<InProgressNotStartedCountModel> GetTestDashboardCountsAsync(int empId, int? inProgressStatusId, int? notStartedStatusId);
        Task<InProgressNotStartedCountModel> GetStudentEvaluationDashboardCountsAsync(int empId);
        Task<InProgressNotStartedCountModel> GetOnlineCoursesDashboardCountsAsync(int empId);
        Task<InProgressNotStartedCountModel> GetProcedureReviewDashboardCountsAsync(int empId);
        Task<RequiredEMPSettingVM> GetEMPSettingsByClass(int classScheduleId);
    }
}
