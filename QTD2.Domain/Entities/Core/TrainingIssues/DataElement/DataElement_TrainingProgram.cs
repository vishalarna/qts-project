using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_TrainingProgram : TrainingIssue_DataElement
    {
        public int? TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }

        public DataElement_TrainingProgram() { }

        public DataElement_TrainingProgram(int trainingIssueId,int? trainingProgramId) : base(trainingIssueId)
        {
            TrainingProgramId = trainingProgramId;
        }
    }
}
