using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IActionItemService : Common.IService<ActionItem>
    {
        public Task<ActionItem> GetActionItemWithIncludeAsync(int id);
        public Task<List<ActionItem>> GetActionItemsByTaskReviewIds(List<int> taskReviewIds);
    }
}
