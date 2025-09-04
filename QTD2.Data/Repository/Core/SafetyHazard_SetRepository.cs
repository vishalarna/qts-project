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
    public class SafetyHazard_SetRepository : Common.Repository<SafetyHazard_Set>, ISafetyHazard_SetRepository
    {
        public SafetyHazard_SetRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
