using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReviewType_VM
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public TaskListReviewType_VM(int id, string type)
        {
            Id = id;
            Type = type;
        }
        public TaskListReviewType_VM()
        {
        }
    }
}
