using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Repository.Authentication;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Service.Authentication;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Authentication
{
    public class AdminMessageAuthService : Common.Service<AdminMessageAuth>, IAdminMessageAuthService
    {
        public AdminMessageAuthService(IAdminMessageAuthRepository repository, IAdminMessageAuthValidation validation) : base(repository, validation)
        {
        }

        public async Task<List<AdminMessageAuth>> GetAdminMessagesForInstanceAsync(string instanceName)
        {
            var messages =( await FindAsync(x => x.Instance == instanceName && !x.Received && x.ExpirationDate > DateTime.UtcNow)).ToList();
            return messages;
        }

        public async Task<List<AdminMessageAuth>> GetAdminMessagesBySourceMessageIdAsync(string instance, List<int> sourceMessageIds)
        {
            var messages = (await FindAsync(o => sourceMessageIds.Contains(o.Id) && o.Instance == instance)).ToList();
            return messages;
        }

        public async Task<List<AdminMessageAuth>> GetAllAdminMessagesAsync()
        {
            var messages = (await FindAsync(m => m.ExpirationDate > DateTime.UtcNow)).ToList();
            return messages;
        }
        
    }
}
