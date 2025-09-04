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
    public class AdminMessageService : Common.Service<AdminMessage>, IAdminMessageService
    {
        public AdminMessageService(IAdminMessageRepository repository, IAdminMessageValidation validation) : base(repository, validation)
        {
        }

        public async Task<List<AdminMessage>> GetAllAdminMessageAsync()
        {
            var messages = (await FindAsync(m => m.ExpirationDate > DateTime.UtcNow)).ToList();
            return messages;
        }
    }
}
