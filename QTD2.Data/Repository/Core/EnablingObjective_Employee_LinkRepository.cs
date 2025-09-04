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
    public class EnablingObjective_Employee_LinkRepository : Common.Repository<EnablingObjective_Employee_Link>, IEnablingObjective_Employee_LinkRepository
    {
        public EnablingObjective_Employee_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
