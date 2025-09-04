using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingProgram
{
    public class TrainingProgramVM
    {
        public int InitialTrainingProgramActive { get; set; }

        public int InitialTrainingProgramInActive { get; set; }

        public int ContinuingTrainingProgramActive { get; set; }

        public int ContinuingTrainingProgramInActive { get; set; }

        public int CycleTrainingProgramActive { get; set; }

        public int CyclelTrainingProgramInActive { get; set; }
    }
}
