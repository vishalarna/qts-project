using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReviewStatus_VM
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public TaskListReviewStatus_VM(int id, string type)
        {
            Id = id;
            Type = type;
        }
        public TaskListReviewStatus_VM()
        {

        }

    }
}
