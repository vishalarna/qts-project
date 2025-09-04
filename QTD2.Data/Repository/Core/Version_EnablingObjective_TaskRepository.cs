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
    public class Version_EnablingObjective_TaskRepository : Common.Repository<Version_EnablingObjective_Task>, IVersion_EnablingObjective_TaskRepository
    {
        public Version_EnablingObjective_TaskRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
