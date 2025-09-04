using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ToolGroup_ToolService : Common.Service<Entities.Core.ToolGroup_Tool>, IToolGroup_ToolService
    {
        public ToolGroup_ToolService(IToolGroup_ToolRepository toolGroup_ToolRepository, IToolGroup_ToolValidation toolGroup_ToolValidation)
            : base(toolGroup_ToolRepository, toolGroup_ToolValidation)
        {
        }
    }
}
