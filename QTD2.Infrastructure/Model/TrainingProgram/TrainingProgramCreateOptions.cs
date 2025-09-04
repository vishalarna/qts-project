using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingProgram
{
    public class TrainingProgramCreateOptions
    {
        public int PositionId { get; set; }

        public int TrainingProgramTypeId { get; set; }

        public string TPVersionNo { get; set; }

        public string ProgramTitle { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public int? State { get; set; }

        public bool? IsInUse { get; set; }
 
        public Nullable<int> copiedTrainingId { get; set; }

        public bool? Publish { get; set; }
    }
}
