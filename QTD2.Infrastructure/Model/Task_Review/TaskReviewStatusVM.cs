using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReviewStatusVM
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public TaskReviewStatusVM(int id,string status)
        {
            Id = id;
            Status = status;
        }
        public TaskReviewStatusVM()
        {

        }
    }
}
