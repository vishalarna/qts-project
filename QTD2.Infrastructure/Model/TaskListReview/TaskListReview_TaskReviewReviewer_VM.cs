using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReview_TaskReviewReviewer_VM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMe { get; set; }

        public TaskListReview_TaskReviewReviewer_VM(int id, string name, bool isMe)
        {
            Id = id;
            Name = name;
            IsMe = isMe;
        }
    }
}
