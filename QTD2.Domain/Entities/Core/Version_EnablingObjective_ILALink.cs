using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_ILALink : Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_ILAId { get; set; }

        public string Version_Number { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_ILA Version_ILA { get; set; }

        public Version_EnablingObjective_ILALink()
        {
        }

        public Version_EnablingObjective_ILALink(Version_EnablingObjective version_EnablingObjective, Version_ILA version_ILA, string version_number = "1.0")
        {
            Version_EnablingObjectiveId = version_EnablingObjective.Id;
            Version_ILAId = version_ILA.Id;
            Version_EnablingObjective = version_EnablingObjective;
            Version_ILA = version_ILA;
            Version_Number = version_number;
        }
    }
}
