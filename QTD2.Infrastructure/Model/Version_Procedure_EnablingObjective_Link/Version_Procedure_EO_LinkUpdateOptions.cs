using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_Procedure_EnablingObjective_Link
{
    public class Version_Procedure_EO_LinkUpdateOptions
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_ProcedureId { get; set; }

        public string VersionNumber { get; set; }
    }
}
