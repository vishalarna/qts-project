using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ClientUserService : Common.Service<ClientUser>, IClientUserService
    {
        public ClientUserService(IClientUserRepository clientUserRepository, IClientUserValidation clientUserValidation)
            : base(clientUserRepository, clientUserValidation)
        {
        }
    }
}
