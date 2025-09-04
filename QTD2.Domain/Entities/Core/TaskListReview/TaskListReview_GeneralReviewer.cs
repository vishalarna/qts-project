using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskListReview_GeneralReviewer : Common.Entity
    {
        public int TaskListReviewId { get; set; }
        public int GeneralReviewerId { get; set; }
        public virtual TaskListReview TaskListReview { get; set; }
        public virtual QTDUser GeneralReviewer { get; set; }
        public TaskListReview_GeneralReviewer(int taskListReviewId, int generalReviewerId)
        {
            TaskListReviewId = taskListReviewId;
            GeneralReviewerId = generalReviewerId;
        }
        public TaskListReview_GeneralReviewer()
        {

        }
    }
}
