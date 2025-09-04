using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISaftyHazardService : Common.IService<Entities.Core.SaftyHazard>
    {
        Task<List<SaftyHazard>> GetForSafetyHazardsByCategory(List<int> safetyHazardCategoryIds, bool includeInactiveSafetyHazards);
        Task<List<SaftyHazard>> GetForSafetyHazardsByPositionMatrix(List<int> safetyHazardIds, bool includeInactiveSafetyHazards);
        Task<List<SaftyHazard>> GetForSafetyHazardsAsync();
        public Task<List<SaftyHazard>> GetSafetyHazardsByIdAsync(List<int> safetyHazardIds);

    }
}
