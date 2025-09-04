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
    public class Task_Reference_LinkRepository : Common.Repository<Task_Reference_Link>, ITask_Reference_LinkRepository
    {
        public Task_Reference_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
