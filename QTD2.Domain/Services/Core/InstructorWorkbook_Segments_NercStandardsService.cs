using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_Segments_NercStandardsService : Common.Service<InstructorWorkbook_Segments_NercStandards>, IInstructorWorkbook_Segments_NercStandardsService

    {
        public InstructorWorkbook_Segments_NercStandardsService(IInstructorWorkbook_Segments_NercStandardsRepository repository, IInstructorWorkbook_Segments_NercStandardsValidation validation)
            : base(repository, validation)
        {
        }
    }
}