using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Task_Reference : Entity
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Task_Reference_Link> Task_Reference_Links { get; set; } = new List<Task_Reference_Link>();

        public Task_Reference()
        {
        }

        public Task_Reference(string displayName, string description)
        {
            DisplayName = displayName;
            Description = description;
        }
    }
}
