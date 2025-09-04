using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.AdminMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IAdminMessage_QTDUserService
    {
        Task<List<AdminMessageVM>> GetAdminMessagesAsync();
        Task<AdminMessage_QTDUser> UpdateAdminMessagesAsync(AdminMessageQTDUserUpdateOptions options);
    }
}
