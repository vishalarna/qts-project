using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ILAResourceService : Common.Service<ILA_Resource>, IILAResourceService
    {
        public ILAResourceService(IILAResourceRepository repository, IILAResourceValidation validation)
            : base(repository, validation)
        {

        }

        public async Task<IEnumerable<ILA_Resource>> GetAllResourcesAsync(int ilaId)
        {
            var result = (await FindAsync(r => r.ILAId == ilaId));
            return result;
        }
    }
}
