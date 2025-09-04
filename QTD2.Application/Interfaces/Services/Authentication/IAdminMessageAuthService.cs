using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.AdminMessageAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Authentication
{
    public interface IAdminMessageAuthService
    {
        Task CreateAdminMessageAsyn(AdminMessageAuthCreateOptions options);
        Task<List<AdminMessageAuth>> GetAllAdminMessageForInstanceAsync(string instanceName);
        Task<List<AdminMessageAuth>> UpdateAdminMessageAuthReceivedStatusAsync(string instance, AdminMessageSourceIdOptions options);
        Task<List<AdminMessageAuth>> GetAllAdminMessageAsync();
    }
}
