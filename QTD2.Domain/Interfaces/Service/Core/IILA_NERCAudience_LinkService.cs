using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILA_NERCAudience_LinkService : Common.IService<ILA_NERCAudience_Link>
    {
        Task<List<ILA_NERCAudience_Link>> GetActiveNercAudienceLinks(int ilaId);
    }
}
