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
    public class RR_StatusHistoryRepository : Common.Repository<RR_StatusHistory>, IRR_StatusHistoryRepository
    {
        public RR_StatusHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
