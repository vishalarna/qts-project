using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class ToolService : Common.Service<Entities.Core.Tool>, IToolService
    {
        public ToolService(IToolRepository toolRepository, IToolValidation toolValidation)
            : base(toolRepository, toolValidation)
        {
        }
        public async Task<List<Tool>> GetToolsAsync()
        {
            var queryable = await AllAsync();
            return queryable.ToList();
        }


        public async Task<string> GetToolsNameByIdAsync(int toolId)
        {
            var tools  = await GetWithIncludeAsync(toolId, new[] { "ToolCategory" });
            return tools.Name;
        }
    }
}
