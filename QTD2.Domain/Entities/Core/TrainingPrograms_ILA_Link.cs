using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingPrograms_ILA_Link:Common.Entity
    {
        public int TrainingProgramId { get; set; }

        public int ILAId { get; set; }

        public virtual TrainingProgram TrainingProgram { get; set; }

        public virtual ILA ILA { get; set; }

        public TrainingPrograms_ILA_Link(TrainingProgram trainingProgram, ILA ila)
        {
            TrainingProgramId = trainingProgram.Id;
            ILAId = ila.Id;
            TrainingProgram = trainingProgram;
            ILA = ila;
        }

        public TrainingPrograms_ILA_Link()
        {
        }
    }
}
