using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_Position_Link : Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_PositionId { get; set; }

        public string Version_Number { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_Position Version_Position { get; set; }

        public Version_EnablingObjective_Position_Link(Version_EnablingObjective eo, Version_Position pos,string version_number = "1.0")
        {
            Version_EnablingObjectiveId = eo.Id;
            Version_PositionId = pos.Id;
        }

        public Version_EnablingObjective_Position_Link()
        {
        }
    }
}
