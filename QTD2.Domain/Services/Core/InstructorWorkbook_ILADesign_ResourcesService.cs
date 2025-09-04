using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ILADesign_ResourcesService : Common.Service<InstructorWorkbook_ILADesign_Resources>, IInstructorWorkbook_ILADesign_ResourcesService
    {
        public InstructorWorkbook_ILADesign_ResourcesService(IInstructorWorkbook_ILADesign_ResourcesRepository repository, IInstructorWorkbook_ILADesign_ResourcesValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<InstructorWorkbook_ILADesign_Resources> GetUploadedFileFromIwbResourcesAsync(int UploadId)
        {
            var resources = await FindAsync(r => r.ILA_UploadId == UploadId);
            return resources.FirstOrDefault();
        }
    }
}