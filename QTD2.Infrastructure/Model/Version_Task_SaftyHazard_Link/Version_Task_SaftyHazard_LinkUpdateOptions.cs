using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_Task_SaftyHazard_Link
{
    public class Version_Task_SaftyHazard_LinkUpdateOptions
    {
        public int Version_TaskId { get; set; }

        public int Version_SaftyHazardId { get; set; }

        public string VersionNumber { get; set; }
    }
}
