using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_MetaEOLink : Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_MetaEOId { get; set; }

        public string Version_Number { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_EnablingObjective Version_MetaEO { get; set; }

        public Version_EnablingObjective_MetaEOLink()
        {
        }

        public Version_EnablingObjective_MetaEOLink(Version_EnablingObjective version_MetaEO, Version_EnablingObjective version_EnablingObjective, string version_number = "")
        {
            Version_Number = version_number;
            Version_EnablingObjective = version_EnablingObjective;
            Version_MetaEO = version_MetaEO;
            Version_EnablingObjectiveId = version_EnablingObjective.Id;
            Version_MetaEOId = version_MetaEO.Id;
        }
    }
}
