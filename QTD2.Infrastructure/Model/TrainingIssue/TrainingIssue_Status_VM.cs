using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
   public class TrainingIssue_Status_VM
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public TrainingIssue_Status_VM() { }

        public TrainingIssue_Status_VM(int id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}
