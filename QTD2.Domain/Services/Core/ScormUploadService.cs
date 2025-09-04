using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
  public class ScormUploadService : Common.Service<CBT_ScormUpload>, IScormUploadService
    {
        public ScormUploadService(IScormUploadRepository repository, IScormUploadValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<CBT_ScormUpload>> GetAllByCBTIdAsync(int cbtId, bool current)
        {
            var cbtScormUploads = await FindWithIncludeAsync(r => r.CbtId == cbtId && (!current || r.Active) && (!current || r.ScormStatus == "Uploaded"), new[] { "CBT_ScormRegistration.ClassScheduleEmployee.Employee" });
            return cbtScormUploads.ToList();
        }

        public async Task<CBT_ScormUpload> GetByIlaIdAsync(int iLAID)
        {
            var scormUpload = await FindAsync(r => r.CBT.ILAId == iLAID && r.Active && r.CBT.Active);
            return scormUpload.FirstOrDefault();
        }

        public async Task<List<CBT_ScormUpload>> GetAllCBTScormUploadsAsync()
        {
            return (await AllWithIncludeAsync(new[] { "CBT.ILA" })).ToList();
        }
    }
}
