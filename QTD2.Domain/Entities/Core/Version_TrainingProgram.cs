using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_TrainingProgram : Common.Entity
    {
        public int PositionId { get; set; }

        public int TrainingProgramId { get; set; }
        public int TrainingProgramTypeId { get; set; }

        public string TPVersionNo { get; set; }

        public string ProgramTitle { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public DateTime Year { get; set; }
        public string VersionNumber { get; set; }
        public int? State { get; set; }
        public bool? IsInUse { get; set; }
        public virtual TrainingProgram trainingProgram { get; set; }
        public virtual Position Position { get; set; }

        public virtual ICollection<Version_TrainingProgram_ILA_Link> Version_TrainingProgram_ILA_Links { get; set; } = new List<Version_TrainingProgram_ILA_Link>();

        public Version_TrainingProgram(TrainingProgram trainingProgram, bool? inUse, int? state, string versionNumber = "")
        {
            PositionId = trainingProgram.PositionId;
            TPVersionNo = trainingProgram.TPVersionNo;
            ProgramTitle = trainingProgram.ProgramTitle;
            TrainingProgramTypeId = trainingProgram.TrainingProgramTypeId;
            StartDate = trainingProgram.StartDate;
            EndDate = trainingProgram.EndDate;
            Year = trainingProgram.Year;
            Description = trainingProgram.Description;
            VersionNumber = versionNumber;
            TrainingProgramId = trainingProgram.Id;
            IsInUse = inUse;
            State = state;
        }

        public Version_TrainingProgram()
        {
        }
    }
}
