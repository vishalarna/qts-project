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
    public class Version_EnablingObjective_Employee_LinkRepository : Common.Repository<Version_EnablingObjective_Employee_Link>, IVersion_EnablingObjective_Employee_LinkRepository
    {
        public Version_EnablingObjective_Employee_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
