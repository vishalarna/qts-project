using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_Task
{
    public class Version_EnablingObjective_StepCreateOptions
    {
        public int TaskId { get; set; }

        public string TaskNumber { get; set; }

        public string VersionNumber { get; set; }

        public string Description { get; set; }

        public string Conditions { get; set; }

        public string Standards { get; set; }

        public bool Critical { get; set; }

        public string Tools { get; set; }

        public string References { get; set; }

        public int RequiredTime { get; set; }

        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }
    }
}
