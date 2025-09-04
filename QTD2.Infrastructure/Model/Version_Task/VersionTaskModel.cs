using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_Task
{
    public class VersionTaskModel
    {
        public int TaskId { get; set; }
        public DateOnly? RequalificationDueDate { get; set; }
        public bool RequalificationRequired { get; set; }
    }
}
