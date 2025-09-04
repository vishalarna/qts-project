using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_ComputerBasedTraining : TrainingIssue_DataElement
    {
        public int? CBT_ScormUploadId { get; set; }
        public CBT_ScormUpload CBT_ScormUpload { get; set; }

        public DataElement_ComputerBasedTraining(int trainingIssueId, int? cBT_ScormUploadId) : base(trainingIssueId)
        {
            CBT_ScormUploadId = cBT_ScormUploadId;
        }

        public DataElement_ComputerBasedTraining() { }
    }
}
