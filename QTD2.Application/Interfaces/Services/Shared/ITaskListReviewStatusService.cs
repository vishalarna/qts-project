using QTD2.Infrastructure.Model.TaskListReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskListReviewStatusService
    {
        public Task<List<TaskListReviewStatus_VM>> GetAllAsync();
    }
}
