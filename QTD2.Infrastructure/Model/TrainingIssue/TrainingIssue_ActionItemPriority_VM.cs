using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
    public class TrainingIssue_ActionItemPriority_VM
    {
        public int Id { get; set; }
        public string Priority { get; set; }

        public TrainingIssue_ActionItemPriority_VM() { }

        public TrainingIssue_ActionItemPriority_VM(int id, string priority) {
            Id = id;
            Priority = priority;
        }
    }
}
