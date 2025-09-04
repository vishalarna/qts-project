using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.EmployeeTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClassRosterService
    {
        public  Task<bool> CreateRoster(ClassRoasterModel options);

        public  Task<List<ClassScheduleEMPWithGradesVM>> GetRoasterEmployeesAsync(RosterFetchOptions options);

        public  Task<List<ClassSchedule_Roster>> UpdateBulkGradeAsync(int id, int testId, ClassRoasterUpdateOptions options);

        public  Task<ClassSchedule_Roster> UpdateGradeAsync(int empId, ClassRoasterUpdateOptions options);

        public Task<ClassSchedule_Roster> UpdateScoreAsync(int id, ClassRoasterUpdateOptions options);

        public Task<ClassSchedule_Roster> UpdateCompDateAsync(int id, ClassRoasterUpdateOptions options);

        public System.Threading.Tasks.Task RemoveEmployeeAsync(int classId, int testId, string testType, int empId);

        public Task<ClassSchedule_Roster> ReleaseTestAsync(int empId, ClassRoasterUpdateOptions options);

        public Task<ClassSchedule_Roster> RecallTestAsync(int empId, ClassRoasterUpdateOptions options);
        public System.Threading.Tasks.Task SubmitEvaluationAsync(int classId, int evalId, int empId);

        public Task<List<RosterOverviewVM>> GetRosterOverviewDataAsync(int classId);
        public Task<RosterScoreGradeModel> SetFinalScoreAndGradeValueAsync(ClassSchedule_Employee classSchedule_Employee, List<ClassSchedule_Roster> classSchedule_Rosters);
        Task<List<ReviewTestModel>> ReviewTestAsync(int rosterId);
        Task<ClassSchedule_Roster_Response> CreateRosterResponseAsync(EmpTestCreateOptions options);
        Task<List<EmployeeTestModel>> GetEmployeesTestAsync();
        Task<EmployeeAnswerModel> GetTestAnswerAsync(int classId, int testId, int questionId, int rosterId);
        Task<List<SubmitTestModel>> SubmitTestAsync(int classId, int testId, int rosterId, DateTime completionDate);
        Task<ClassSchedule_Roster> ExitTestAsync(int rosterId);
    }
}
