using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_MetaILA : Entity
    {
        public int? MetaILAId { get; set; }

        public string MetaILAName { get; set; } 

        public string MetaILADesc { get; set; }

        public int  MetaILAStatusId { get; set; }

        public string VersionNumber { get; set; }

        public string Reason { get; set; }

        public virtual MetaILA MetaILA { get; set; }

        public virtual MetaILA_Status MetaILA_Status { get; set; }

        public Version_MetaILA()
        {
        }

        public Version_MetaILA(int metaILAId, string metaILAName, string metaILADesc, int status, string versionNumber, int metaILAAssessmentID, string reason)
        {
            MetaILAId = metaILAId;
            MetaILAName = metaILAName;
            MetaILADesc = metaILADesc;
            MetaILAStatusId = status;
            VersionNumber = versionNumber;
            Reason = reason;
        }
    }
}
