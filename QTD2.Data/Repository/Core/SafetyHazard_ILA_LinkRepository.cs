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
    public class SafetyHazard_ILA_LinkRepository : Common.Repository<SafetyHazard_ILA_Link>, ISafetyHazard_ILA_LinkRepository
    {
        public SafetyHazard_ILA_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
