using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReviewFinding_VM
    {
        public int Id { get; set; }
        public string Finding { get; set; }

        public TaskReviewFinding_VM(int id,string finding)
        {
            Id = id;
            Finding = finding;
        }
        public TaskReviewFinding_VM()
        {

        }
    }
}
