using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_Evaluation_RosterService : Common.IService<ClassSchedule_Evaluation_Roster>
    {
        public Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationsRostersByIdAsync(int employeeId);

        public Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationsByIdAsync(int employeeId);

        public Task<ClassSchedule_Evaluation_Roster> GetClassScheduleRosterByIdAsync(int employeeId, int evaluationId, int classId);
        public Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationRoasterByEmpId(int employeeId, DateTime date);
        public Task<ClassSchedule_Evaluation_Roster> GetForNotificationAsync(int classScheduleEvaluationRosterId);
        public Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluation_RostersByEvalId(List<int> classScheduleIDs, List<int> studentEvalIds);
        public Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationsByEmpIdAsync(int employeeId);
        public Task<List<ClassSchedule_Evaluation_Roster>> GetEvaluationsRosterByEvalIdILAIdAsync(List<int>ilaIds, List<int> evalIds, List<DateTime> dateRanges);
        public Task<List<ClassSchedule_Evaluation_Roster>> GetPendingClassScheduleEvaluationRosterByILAIdAsync(int ilaId);
        public Task<List<ClassSchedule_Evaluation_Roster>> GetClassScheduleEvaluationRosterByMetaILAIdsAsync(List<int> metaIlaIds);
    }
}
