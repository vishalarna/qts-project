using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
   public interface ICBT_ScormRegistrationService : Common.IService<CBT_ScormRegistration>
    {
        Task<CBT_ScormRegistration> GetByClassScheduleEmployeeId(int id);
        Task<List<CBT_ScormRegistration>> GetByClassScheduleIdAsync(int classScheduleId);
        public System.Threading.Tasks.Task<CBT> GetCbtWithId(int cbtId);
        Task<CBT_ScormRegistration> GetByIdAsync(int id);
        public System.Threading.Tasks.Task<CBT_ScormRegistration> GetCbtScormRegistrationAsync(int empId, int? classScheduleId, int? cbtScormUploadId);
        Task<List<CBT_ScormRegistration>> GetPendingCBTScormRegistrationByILAIdAsync(int ilaId);
    }
}
