using QTD2.Infrastructure.Model.Task_Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskReview_ActionItemPriorityService
    {
        public Task<List<TaskReviewActionItemPriority_VM>> GetAllAsync();
    }
}
