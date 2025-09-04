using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskListReview_PositionLink:Entity
    {
        public int TaskListReviewId { get; set; }
        public int PositionId { get; set; }
        public virtual TaskListReview TaskListReview { get; set; }
        public virtual Position Position { get; set; }

        public TaskListReview_PositionLink() { }
        public TaskListReview_PositionLink(int taskListReviewId, int positionId)
        {
            TaskListReviewId = taskListReviewId;
            PositionId = positionId;
        }
    }
}
