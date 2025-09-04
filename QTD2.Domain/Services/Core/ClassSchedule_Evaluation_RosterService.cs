using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ClassSchedule_Evaluation_RosterService : Common.Service<ClassSchedule_Evaluation_Roster>, IClassSchedule_Evaluation_RosterService
    {
        public ClassSchedule_Evaluation_RosterService(IClassSchedule_Evaluation_RosterRepository repository, IClassSchedule_Evaluation_RosterValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationsRostersByIdAsync(int employeeId)
        {
            var evaluationRosters = await FindAsync(x => x.EmployeeId == employeeId, true);
            return evaluationRosters.ToList();
        }
        public async Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationsByIdAsync(int employeeId)
        {
            var evaluations = await FindAsync(x => x.EmployeeId == employeeId && x.IsReleased == true, true);
            return evaluations.ToList();
        }
        public async Task<ClassSchedule_Evaluation_Roster> GetClassScheduleRosterByIdAsync(int employeeId, int evaluationId, int classId)
        {
            var classScheduleEvaluationRosterItem = await FindAsync(x => x.EmployeeId == employeeId && x.ClassScheduleId == classId && x.StudentEvaluationId == evaluationId);
            return classScheduleEvaluationRosterItem?.FirstOrDefault();
        }

        public async Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationRoasterByEmpId(int employeeId, DateTime date)
        {
            List<Expression<Func<ClassSchedule_Evaluation_Roster, bool>>> predicates = new List<Expression<Func<ClassSchedule_Evaluation_Roster, bool>>>();
            if (employeeId > 0)
            {
                predicates.Add(r => r.EmployeeId == employeeId);
            }
            predicates.Add(r => r.ReleaseDate < DateTime.Today && r.CompletedDate > DateTime.Today && r.IsCompleted == false && r.IsReleased == true);
            var evaluationRoasters = await FindWithIncludeAsync(predicates, new string[] { "ClassScheduleInfo.ILA.EvaluationReleaseEMPSetting", "StudentEvaluationInfo" });
            evaluationRoasters = evaluationRoasters.Where(r => (r.ClassScheduleInfo.ILA.EvaluationReleaseEMPSetting != null ? r.ClassScheduleInfo.ILA.EvaluationReleaseEMPSetting.GetDueDate(r.ClassScheduleInfo.EndDateTime) : r.ClassScheduleInfo.EndDateTime.AddDays(10)).Date == Convert.ToDateTime(date).Date);
            return evaluationRoasters.ToList();
        }

        public async Task<ClassSchedule_Evaluation_Roster> GetForNotificationAsync(int classScheduleEvaluationRosterId)
        {
            var classScheduleEvaluationRoster = await FindWithIncludeAsync(x => classScheduleEvaluationRosterId == x.Id,
                                         new string[] {
                                                        "ClassScheduleInfo",
                                                        "ClassScheduleInfo.Instructor",
                                                        "ClassScheduleInfo.Location",
                                                        "ClassScheduleInfo.ILA.Provider",
                                                        "Employee.Person"
                                         });

            return classScheduleEvaluationRoster.FirstOrDefault();
        }

        public async Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluation_RostersByEvalId(List<int> classScheduleIDs, List<int> studentEvalIds)
        {
            List<Expression<Func<ClassSchedule_Evaluation_Roster, bool>>> predicates = new List<Expression<Func<ClassSchedule_Evaluation_Roster, bool>>>();
            predicates.Add(r => classScheduleIDs.Contains(r.ClassScheduleId.Value));
            predicates.Add(r => studentEvalIds.Contains(r.StudentEvaluationId));
            var evaluationRoasters = await FindWithIncludeAsync(predicates, new string[] { "StudentEvaluationInfo.RatingScaleN", "ClassScheduleInfo", "StudentEvaluationInfo.StudentEvaluationQuestions.QuestionBank" });
            return evaluationRoasters.ToList();
        }

        public async Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationsByEmpIdAsync(int employeeId)
        {
            List<Expression<Func<ClassSchedule_Evaluation_Roster, bool>>> predicates = new List<Expression<Func<ClassSchedule_Evaluation_Roster, bool>>>();
            predicates.Add(r => r.EmployeeId == employeeId);
            predicates.Add(r => r.IsReleased == true);
            predicates.Add(r => (r.ClassScheduleInfo.ILA.Provider != null && r.StudentEvaluationInfo != null));
            predicates.Add(r => r.ClassScheduleInfo.ClassSchedule_Employee.Where(m => m.EmployeeId == r.EmployeeId).All(e => e.IsEnrolled && e.Active));
            predicates.Add(r => r.Active);
            var evaluations = await FindWithIncludeAsync(predicates, new string[] { "StudentEvaluationInfo", "ClassScheduleInfo.ILA.Provider", "ClassScheduleInfo.ILA.EvaluationReleaseEMPSetting" });
            return evaluations.ToList();
        }

        public async Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationsRosterByEvalIdILAIdAsync(List<int> ilaIds, List<int> evalIds,List<DateTime> dateRanges)
        {
            List<Expression<Func<ClassSchedule_Evaluation_Roster, bool>>> predicates = new List<Expression<Func<ClassSchedule_Evaluation_Roster, bool>>>();
            predicates.Add(r => r.Active && !r.Deleted);
            predicates.Add(r => ilaIds.Contains(r.ClassScheduleInfo.ILA.Id) && !r.ClassScheduleInfo.Deleted && r.ClassScheduleInfo.Active); 
            predicates.Add(r=>evalIds.Contains(r.StudentEvaluationId) && !r.StudentEvaluationInfo.Deleted && r.StudentEvaluationInfo.Active);
            if(dateRanges.Count() > 0)
            {
                predicates.Add(r => r.ClassScheduleInfo.StartDateTime >= dateRanges[0] && r.ClassScheduleInfo.StartDateTime <= dateRanges[1]);
            }
            return (await FindWithIncludeAsync(predicates, new string[] { "ClassScheduleInfo.ILA.Provider", "StudentEvaluationInfo.RatingScaleN", "ClassScheduleInfo.ILA.DeliveryMethod", "StudentEvaluationInfo.StudentEvaluationQuestions.QuestionBank", "ClassScheduleInfo.ClassSchedule_Evaluation_Rosters" })).Distinct().ToList();
        }

        public async Task<List<ClassSchedule_Evaluation_Roster>> GetPendingClassScheduleEvaluationRosterByILAIdAsync(int ilaId)
        {
            return (await FindAsync(x => x.ClassScheduleInfo.ILAID == ilaId && !x.IsCompleted && x.IsReleased && !x.IsRecalled)).ToList();
        }

        public async Task<List<ClassSchedule_Evaluation_Roster>> GetClassScheduleEvaluationRosterByMetaILAIdsAsync(List<int> metaIlaIds)
        {
            return (await FindAsync(m => metaIlaIds.Contains(m.MetaIlaId.Value))).ToList();
        }

    }
}
