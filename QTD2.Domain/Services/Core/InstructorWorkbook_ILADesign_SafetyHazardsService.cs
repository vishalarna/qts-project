using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class InstructorWorkbook_ILADesign_SafetyHazardsService : Common.Service<InstructorWorkbook_ILADesign_SafetyHazards>, IInstructorWorkbook_ILADesign_SafetyHazardsService
    {
        public InstructorWorkbook_ILADesign_SafetyHazardsService(IInstructorWorkbook_ILADesign_SafetyHazardsRepository repository, IInstructorWorkbook_ILADesign_SafetyHazardsValidation validation)
            : base(repository, validation)
        {
        }
    }
}
