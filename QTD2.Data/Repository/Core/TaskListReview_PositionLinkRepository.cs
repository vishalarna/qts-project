using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class TaskListReview_PositionLinkRepository : Common.Repository<TaskListReview_PositionLink>, ITaskListReview_PositionLinkRepository
    {
        public TaskListReview_PositionLinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
