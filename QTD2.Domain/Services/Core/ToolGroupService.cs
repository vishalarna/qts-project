using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ToolGroupService : Common.Service<Entities.Core.ToolGroup>, IToolGroupService
    {
        public ToolGroupService(IToolGroupRepository toolGroupRepository, IToolGroupValidation toolGroupValidation)
            : base(toolGroupRepository, toolGroupValidation)
        {
        }
    }
}
