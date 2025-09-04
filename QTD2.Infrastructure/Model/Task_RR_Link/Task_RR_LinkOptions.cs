using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_RR_Link
{
    public class Task_RR_LinkOptions
    {
        public int TaskId { get; set; }

        public int[] RegulatoryRequirementIds { get; set; }
    }
}
