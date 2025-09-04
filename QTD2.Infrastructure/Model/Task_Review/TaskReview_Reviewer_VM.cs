using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReview_Reviewer_VM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TaskReview_Reviewer_VM(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public TaskReview_Reviewer_VM(){}
    }
}
