using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
    public class TrainingIssue_Severity_VM
    {
        public int Id { get; set; }
        public string Severity { get; set; }

        public TrainingIssue_Severity_VM() { }

        public TrainingIssue_Severity_VM(int id, string severity)
        {
            Id = id;
            Severity = severity;
        }
    }
}
