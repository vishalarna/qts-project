using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingProgram_History
{
    public class TrainingProgram_HistoryCreateOptions
    {
        public int TrainingProgramId { get; set; }
        public int TrainingProgramVersionId { get; set; }
        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }
    }
}
