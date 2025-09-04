using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_Pretest : TrainingIssue_DataElement
    {
        public int? PreTestId { get; set; }
        public Test PreTest { get; set; }
        public DataElement_Pretest(int trainingIssueId, int? preTestId) : base(trainingIssueId)
        {
            PreTestId = preTestId;
        }
        public DataElement_Pretest()
        {

        }
    }
}
