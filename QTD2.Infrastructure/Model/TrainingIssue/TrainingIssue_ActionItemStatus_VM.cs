using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
    public class TrainingIssue_ActionItemStatus_VM
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public TrainingIssue_ActionItemStatus_VM() { }

        public TrainingIssue_ActionItemStatus_VM(int id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}
