using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class EnablingObjective_StepRepository : Common.Repository<EnablingObjective_Step>, IEnablingObjective_StepRepository
    {
        public EnablingObjective_StepRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
