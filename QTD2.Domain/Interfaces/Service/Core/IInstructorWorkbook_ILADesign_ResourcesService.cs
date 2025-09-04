using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IInstructorWorkbook_ILADesign_ResourcesService : Common.IService<InstructorWorkbook_ILADesign_Resources>
    {
        public System.Threading.Tasks.Task<InstructorWorkbook_ILADesign_Resources> GetUploadedFileFromIwbResourcesAsync(int UploadId);
    }
}
