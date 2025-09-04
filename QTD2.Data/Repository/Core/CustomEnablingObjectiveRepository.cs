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
    public class CustomEnablingObjectiveRepository : Common.Repository<CustomEnablingObjective>, ICustomEnablingObjectiveRepository
    {
        public CustomEnablingObjectiveRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
