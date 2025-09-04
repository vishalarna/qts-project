using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IOrganizationService : IService<Organization>
    {
        System.Threading.Tasks.Task<List<Organization>> GetEmployeesByOrganizationAsync(List<int> organizationIds, string active);
        System.Threading.Tasks.Task<IEnumerable<Organization>> GetByIdListAsync(IEnumerable<int> organizationIds);
        Task<List<Organization>> GetPublicOrganizationAsync();
    }
}
