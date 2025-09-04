using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskRequalVM
    {
        public bool RequalRequired { get; set; }

        public DateOnly? RequalDueDate { get; set; }
    }
}
