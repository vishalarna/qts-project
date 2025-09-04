using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_TrainingProgram_ILA_Link : Entity
    {
        public int Version_TrainingProgramId { get; set; }

        public int Version_ILAId { get; set; }

        public string VersionNumber { get; set; }
        public int? State { get; set; }
        public bool? IsInUse { get; set; }

        public virtual Version_TrainingProgram Version_TrainingProgram { get; set; }

        public virtual Version_ILA Version_ILA { get; set; }

        public Version_TrainingProgram_ILA_Link()
        {
        }

        public Version_TrainingProgram_ILA_Link(int version_TrainingProgramId, int version_ILAId, string versionNumber)
        {
            Version_TrainingProgramId = version_TrainingProgramId;
            Version_ILAId = version_ILAId;
            VersionNumber = versionNumber;
        }
    }
}