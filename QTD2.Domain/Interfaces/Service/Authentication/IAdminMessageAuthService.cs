using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Authentication
{
    public interface IAdminMessageAuthService : IService<AdminMessageAuth>
    {
        Task<List<AdminMessageAuth>> GetAdminMessagesForInstanceAsync(string instanceName);
        Task<List<AdminMessageAuth>> GetAdminMessagesBySourceMessageIdAsync(string instance, List<int> sourceMessageIds);
        Task<List<AdminMessageAuth>> GetAllAdminMessagesAsync();
    }
}
