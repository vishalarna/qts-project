using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ILA_NERCAudience_LinkRepository : Common.Repository<ILA_NERCAudience_Link>, IILA_NERCAudience_LinkRepository
    {
        public ILA_NERCAudience_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
