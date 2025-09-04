using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskQualificationStatus : Common.Entity
    {
        public TaskQualificationStatus(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public TaskQualificationStatus()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<TaskQualification> TaskQualifications { get; set; } = new List<TaskQualification>();
    }
}
