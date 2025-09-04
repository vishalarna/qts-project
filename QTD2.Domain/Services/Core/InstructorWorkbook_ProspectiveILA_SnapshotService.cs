using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ProspectiveILA_SnapshotService : Common.Service<InstructorWorkbook_ProspectiveILA_Snapshot>, IInstructorWorkbook_ProspectiveILA_SnapshotService

    {
        public InstructorWorkbook_ProspectiveILA_SnapshotService(IInstructorWorkbook_ProspectiveILA_SnapshotRepository repository, IInstructorWorkbook_ProspectiveILA_SnapshotValidation validation)
            : base(repository, validation)
        {
        }
    }
}