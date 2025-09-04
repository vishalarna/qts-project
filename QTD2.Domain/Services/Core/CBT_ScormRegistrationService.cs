using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using ICBTDomainService = QTD2.Domain.Interfaces.Service.Core.ICBTService;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace QTD2.Domain.Services.Core
{
   public class CBT_ScormRegistrationService : Common.Service<CBT_ScormRegistration>, ICBT_ScormRegistrationService
    {
        private readonly ICBTDomainService _cbtService;
        public CBT_ScormRegistrationService(ICBT_ScormRegistrationRepository repository, ICBT_ScormRegistrationValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<CBT_ScormRegistration> GetByClassScheduleEmployeeId(int classScheduleEmployeeId)
        {
            var registration = await FindAsync(r => r.ClassScheduleEmployeeId == classScheduleEmployeeId);
            return registration.FirstOrDefault();
        }

        public async Task<List<CBT_ScormRegistration>> GetByClassScheduleIdAsync(int classScheduleId)
        {
            var registrations = await FindAsync(r => r.ClassScheduleEmployee.ClassScheduleId == classScheduleId);
            return registrations.ToList();
        }

        public async System.Threading.Tasks.Task<CBT> GetCbtWithId(int cbtId)
        {
            var cbt = await _cbtService.GetWithIncludeAsync(cbtId, new[] { "ScormUploads", "ILA" });
            return cbt;
        }
        public async Task<CBT_ScormRegistration> GetByIdAsync(int id)
        {
            var cBT_ScormRegistrations = await FindWithIncludeAsync(r=> r.Id == id, new string[] { "ClassScheduleEmployee.ClassSchedule.ClassSchedule_TestReleaseEMPSettings" });
            return cBT_ScormRegistrations.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<CBT_ScormRegistration> GetCbtScormRegistrationAsync(int empId, int? classScheduleId, int? cbtScormUploadId)
        {
            var cBTScormRegistration = (await FindAsync(x => x.ClassScheduleEmployee.EmployeeId == empId && x.ClassScheduleEmployee.ClassScheduleId == classScheduleId && x.CBTScormUploadId == cbtScormUploadId)).FirstOrDefault();
            return cBTScormRegistration;
        }

        public async Task<List<CBT_ScormRegistration>> GetPendingCBTScormRegistrationByILAIdAsync(int ilaId)
        {
            return (await FindAsync(x => x.ScormUpload.CBT.ILAId == ilaId && x.Active && x.RegistrationCompletion != CBT_ScormRegistrationCompletion.COMPLETED && x.RegistrationSuccess != CBT_ScormRegistrationSuccess.FAILED)).ToList();
        }

    }
}
