using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingProgram
{
    public class TrainingProgram_HistoryCreateOption
    {
        public int TrainingProgramId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public string ActionType { get; set; }
        public int[] trainingProgramIds { get; set; }

    }
}
