using QTD2.Infrastructure.Model.AdminMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IAdminMessageService
    {
        Task CreateAdminMessageAsync(AdminMessageCreateOptions options);
    }
}
