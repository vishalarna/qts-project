using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.Instance;

namespace QTD2.Application.Interfaces.Services.QTD
{
    public interface IInstanceService
    {
        Task<Instance> GetInstanceAsync(string name);
        Task<List<Instance>> GetAllInstancesAsync();
    }
}
