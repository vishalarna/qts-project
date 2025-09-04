using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_Procedure
{
    public class Version_ProcedureCreateOptions
    {
        public int ProcedureId { get; set; }

        public string ProcedureNumber { get; set; }

        public string Title { get; set; }

        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }

        public string VersionNumber { get; set; }
    }
}
