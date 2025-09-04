using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_Test : TrainingIssue_DataElement
    {
        public int? TestId { get; set; }
        public Test Test { get; set; }
        public DataElement_Test(int trainingIssueId, int? testId) : base(trainingIssueId)
        {
            TestId = testId;
        }
        public DataElement_Test()
        {

        }
    }
}
