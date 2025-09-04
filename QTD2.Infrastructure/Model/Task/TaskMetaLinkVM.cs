using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskMetaLinkVM
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public bool IsRR { get; set; }

        public int MetaTaskId { get; set; }

        public bool Active { get; set; }
    }
}
