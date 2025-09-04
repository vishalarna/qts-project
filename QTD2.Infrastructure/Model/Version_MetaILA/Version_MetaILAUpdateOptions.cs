using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_MetaILA
{
    public class Version_MetaILAUpdateOptions
    {
        public int MetaILAId { get; set; }

        public string MetaILAName { get; set; }

        public string MetaILADesc { get; set; }

        public string VersionNumber { get; set; }

        public int MetaILAAssessmentID { get; set; }

        public int MetaILAStatusId { get; set; }

        public string Reason { get; set; }
    }
}
