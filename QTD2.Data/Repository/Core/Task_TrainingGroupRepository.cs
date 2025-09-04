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
    public class Task_TrainingGroupRepository : Common.Repository<Task_TrainingGroup>, ITask_TrainingGroupRepository
    {
        public Task_TrainingGroupRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
