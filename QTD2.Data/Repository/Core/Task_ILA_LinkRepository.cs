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
    public class Task_ILA_LinkRepository : Common.Repository<Task_ILA_Link>, ITask_ILA_LinkRepository
    {
        public Task_ILA_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
