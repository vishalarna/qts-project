using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ProspectiveILA_ArchivesService : Common.Service<InstructorWorkbook_ProspectiveILA_Archives>, IInstructorWorkbook_ProspectiveILA_ArchivesService

    {
        public InstructorWorkbook_ProspectiveILA_ArchivesService(IInstructorWorkbook_ProspectiveILA_ArchivesRepository repository, IInstructorWorkbook_ProspectiveILA_ArchivesValidation validation)
            : base(repository, validation)
        {
        }
    }
}