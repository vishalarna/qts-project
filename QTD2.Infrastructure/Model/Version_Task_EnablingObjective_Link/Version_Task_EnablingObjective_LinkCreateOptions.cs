using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_Task_EnablingObjective_Link
{
    public class Version_Task_EnablingObjective_LinkCreateOptions
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_TaskId { get; set; }

        public string VersionNumber { get; set; }
    }
}
