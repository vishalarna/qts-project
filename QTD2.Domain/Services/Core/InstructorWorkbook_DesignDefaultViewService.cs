using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_DesignDefaultViewService : Common.Service<InstructorWorkbook_DesignDefaultView>, IInstructorWorkbook_DesignDefaultViewService
    {
        public InstructorWorkbook_DesignDefaultViewService(IInstructorWorkbook_DesignDefaultViewRepository repository, IInstructorWorkbook_DesignDefaultViewValidation validation)
            : base(repository, validation)
        {
        }
    }
}
