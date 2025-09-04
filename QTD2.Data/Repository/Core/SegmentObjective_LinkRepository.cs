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
    public class SegmentObjective_LinkRepository : Common.Repository<SegmentObjective_Link>, ISegmentObjective_LinkRepository
    {
        public SegmentObjective_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
