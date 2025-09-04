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
    public class SafetyHazard_CategoryHistoryRepository : Common.Repository<SafetyHazard_CategoryHistory>, ISafetyHazard_CategoryHistoryRepository
    {
        public SafetyHazard_CategoryHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
