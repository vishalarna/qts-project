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
    public class Task_HistoryRepository : Common.Repository<Task_History>, ITask_HistoryRepository
    {
        public Task_HistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
