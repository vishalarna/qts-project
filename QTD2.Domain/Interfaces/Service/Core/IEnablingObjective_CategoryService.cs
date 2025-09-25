using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEnablingObjective_CategoryService : Common.IService<EnablingObjective_Category>
    {
        Task<List<EnablingObjective_Category>> GetMinimalEOCatData();

        Task<EnablingObjective_Category> GetMinimalEOCatDataById(int id);
        Task<List<EnablingObjective_Category>> GetMinimalEOCatDataByIds(List<int> id);
    }
}
