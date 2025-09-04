using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_EnablingObjective_SaftyHazard_Link
{
    public class Version_EO_SH_LinkUpdateOptions
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_SaftyHazardId { get; set; }

        public string VersionNumber { get; set; }
    }
}
