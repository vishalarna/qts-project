using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class AdminMessage_QTDUserService : Common.Service<AdminMessage_QTDUser>, IAdminMessage_QTDUserService
    {
        public AdminMessage_QTDUserService(IAdminMessage_QTDUserRepository repository, IAdminMessage_QTDUserValidation validation) : base(repository, validation)
        {
        }

        public async Task<List<AdminMessage_QTDUser>> GetAdminMessageByUserIdAsync(int userId)
        {
            var messages = (await FindWithIncludeAsync(m => m.QTDUserId == userId && !m.Dismissed && m.AdminMessage.ExpirationDate > DateTime.UtcNow, new string[] { "AdminMessage" })).ToList();
            return messages;
        }

        public async Task<List<AdminMessage_QTDUser>> GetExistingAdminMessageByUserIdAsync(int userId)
        {
            var messages = (await FindWithIncludeAsync(m => m.QTDUserId == userId && m.AdminMessage.ExpirationDate > DateTime.UtcNow, new string[] { "AdminMessage" })).ToList();
            return messages;
        }

        public async Task<AdminMessage_QTDUser> GetAdminMessageAsync(int AdminMessage_QTDUserId)
        {
            var message = await GetAsync(AdminMessage_QTDUserId);
            return message;
        }
    }
}
