using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Authentication
{
    public interface IClientService : IService<Client>
    {
        Task<IEnumerable<Client>> GetByInstanceListAsync(List<string> instanceNames);
    }
}
