using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ILACustomObjective_LinkRepository : Common.Repository<ILACustomObjective_Link>, IILACustomObjective_LinkRepository
    {
        public ILACustomObjective_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
