using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReviewOverview_VM
    {
        public int PositionsWithoutTaskListReview { get; set; }
        public int MyPendingTaskReviews { get; set; }
        public List<TaskListReviewOverview_TaskListReview_VM> TaskListReviewOverview_TaskListReview_VMs { get; set; } = new List<TaskListReviewOverview_TaskListReview_VM>();

        public TaskListReviewOverview_VM(int positionsWithoutTaskListReview, int myPendingTaskReviews)
        {
            PositionsWithoutTaskListReview = positionsWithoutTaskListReview;
            MyPendingTaskReviews = myPendingTaskReviews;
        }
    }
}
