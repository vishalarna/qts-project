using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_Procedure_SaftyHazard_Link
{
    public class Version_Procedure_SaftyHazard_LinkCreateOptions
    {
        public int Version_SaftyHazardId { get; set; }

        public int Version_ProcedureId { get; set; }

        public string VersionNumber { get; set; }
    }
}
