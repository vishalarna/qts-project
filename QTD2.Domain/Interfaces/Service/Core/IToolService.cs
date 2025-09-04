using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IToolService : Common.IService<Entities.Core.Tool>
    {
        System.Threading.Tasks.Task<List<Tool>> GetToolsAsync();
        public Task<string> GetToolsNameByIdAsync(int toolId);
    }
}
