using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEnablingObjective_SubCategoryService : Common.IService<EnablingObjective_SubCategory>
    {
        Task<List<EnablingObjective_SubCategory>> GetMinimalEOSubCatData();
    }
}
