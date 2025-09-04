using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingProgram_History : Common.Entity
    {
        public int TrainingProgramId { get; set; }
        public int TrainingProgramVersionId { get; set; }
        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public virtual TrainingProgram TrainingProgram { get; set; }

        public virtual Version_TrainingProgram Version_TrainingProgram { get; set; }

        public TrainingProgram_History()
        {

        }

        public TrainingProgram_History(int trainingProgramId, int trainingProgramVersionId, string changeNotes, DateTime effectiveDate)
        {
            TrainingProgramId = trainingProgramId;
            TrainingProgramVersionId = trainingProgramVersionId;
            ChangeNotes = changeNotes;
            ChangeEffectiveDate = effectiveDate;
        }



    }
}
