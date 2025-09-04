using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IAdminMessageService : Common.IService<AdminMessage>
    {
        Task<List<AdminMessage>> GetAllAdminMessageAsync();
    }
}
