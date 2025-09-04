using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ICertifyingBodyService : IService<CertifyingBody>
    {
        public Task<List<CertifyingBody>> GetCertifyingBodiesAsync(bool isLevelEditing);
    }
}
