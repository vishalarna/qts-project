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
    public class RR_IssuingAuthority_StatusHistoryRepository : Common.Repository<RR_IssuingAuthority_StatusHistory>, IRR_IssuingAuthority_StatusHistoryRepository
    {
        public RR_IssuingAuthority_StatusHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
