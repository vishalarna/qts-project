using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_Segments_LinkObjectivesService : Common.Service<InstructorWorkbook_Segments_LinkObjectives>, IInstructorWorkbook_Segments_LinkObjectivesService

    {
        public InstructorWorkbook_Segments_LinkObjectivesService(IInstructorWorkbook_Segments_LinkObjectivesRepository repository, IInstructorWorkbook_Segments_LinkObjectivesValidation validation)
            : base(repository, validation)
        {
        }
    }
}