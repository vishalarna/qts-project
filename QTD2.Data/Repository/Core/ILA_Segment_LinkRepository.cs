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
    public class ILA_Segment_LinkRepository : Common.Repository<ILA_Segment_Link>, IILA_Segment_LinkRepository
    {
        public ILA_Segment_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
