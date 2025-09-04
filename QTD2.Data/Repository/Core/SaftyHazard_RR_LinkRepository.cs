using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SaftyHazard_RR_LinkRepository : Common.Repository<SaftyHazard_RR_Link>, ISaftyHazard_RR_LinkRepository
    {
        public SaftyHazard_RR_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
