using System.Collections.Generic;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IDutyAreaService : Common.IService<DutyArea>
    {
        System.Threading.Tasks.Task<List<DutyArea>> GetAllOrderByNumber();
        System.Threading.Tasks.Task<List<DutyArea>> GetDutyAreasWithSubDutyAreaTaskTaskQualificationEmployees();
    }
}
