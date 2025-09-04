using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_RosterService : Common.IService<ClassSchedule_Roster>
    {
        public Task<List<ClassSchedule_Roster>> GetClassScheduleRostersByEmpId(int employeeId);
        public Task<int?> GetClassScheduleRostersStatus(int testId, int classScheduleId, int employeeId);
        public Task<List<ClassSchedule_Roster>> GetClassScheduleRostersByIdAsync(int employeeId);
        public Task<ClassSchedule_Roster> GetPretestAsync(int employeeId, int classScheduleId);
        public Task<List<ClassSchedule_Roster>> GetFinalTestsToNotifyAsync();
        public Task<List<ClassSchedule_Roster>> GetPreTestsToNotifyAsync();
        public Task<List<ClassSchedule_Roster>> GetClassScheduleRosterByUserId(int userId);
        public Task<string> GetTestTitleByIdAsync(int classScheduleRosterId);
        public Task<ClassSchedule_Roster> GetForNotificationAsync(int classScheduleRosterId);
        Task<List<ClassSchedule_Roster>> GetReleasedByEmployeeUsernameAsync(string userName);
        public Task<ClassSchedule_Roster> GetWithTestItemDetailsAsync(int rosterId);
        public Task<List<ClassSchedule_Roster>> GetForPretestAndFinalTestComparison(List<int> classScheduleIds, List<int> employeeIds);
        public Task<List<ClassSchedule_Roster>> GetByClassScheduleIdListAsync(List<int> classScheduleIdList);
        Task<List<ClassSchedule_Roster>> GetClassScheduleRostersByEmployeeIdAndClassScheduleId(int employeeId, int classScheduleId);
        Task<List<ClassSchedule_Roster>> GetPendingClassScheduleRosterByILAIdAsync(int ilaId);
        Task<List<ClassSchedule_Roster>> GetClassScheduleRosterByMetaEmployeeIdAsync(int metaEmployeeId);
    }
   
}
