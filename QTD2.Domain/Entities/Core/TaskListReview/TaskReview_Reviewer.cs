using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskReview_Reviewer : Common.Entity
    {
        public int TaskReviewId { get; set; }
        public int ReviewerId { get; set; }
        public virtual TaskReview TaskReview { get; set; }
        public virtual QTDUser Reviewer { get; set; }
        
        public TaskReview_Reviewer(int taskReviewId, int reviewerId)
        {
            TaskReviewId = taskReviewId;
            ReviewerId = reviewerId;
        }
        public TaskReview_Reviewer() { }
       
    }
}
