using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingProgramType : Common.Entity
    {
        public string TrainingProgramTypeTitle { get; set; }
        public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();

        public TrainingProgramType(string trainingProgramTypeTitle)
        {
            TrainingProgramTypeTitle = trainingProgramTypeTitle;
        }
        public TrainingProgramType()
        {
        }
    }
}
