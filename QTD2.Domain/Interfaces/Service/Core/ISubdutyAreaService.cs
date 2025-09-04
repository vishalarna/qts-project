using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISubdutyAreaService : Common.IService<SubdutyArea>
    {
        Task<List<SubdutyArea>> GetAllOrderByNumber();
        Task<List<SubdutyArea>> GetSubDutyAreasByDutyAreaIdAsync(int dutyAreaId);
    }
}
