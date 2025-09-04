using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingProgram
{
    public class TrainingProgramFilterOptions
    {
        public string StartYear { get; set; }

        public string EndYear { get; set; }

        public int[] positionIds { get; set; }

        public string trainingProgramTitle { get; set; }

    }
}
