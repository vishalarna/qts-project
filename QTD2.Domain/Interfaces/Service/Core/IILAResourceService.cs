using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILAResourceService : Common.IService<ILA_Resource>
    {
        public Task<IEnumerable<ILA_Resource>> GetAllResourcesAsync(int ilaId);
    }
}
