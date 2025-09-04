using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class RegRequirement_EO_LinkRepository : Common.Repository<Domain.Entities.Core.RegRequirement_EO_Link>, IRegRequirement_EO_LinkRepository
    {
        public RegRequirement_EO_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
