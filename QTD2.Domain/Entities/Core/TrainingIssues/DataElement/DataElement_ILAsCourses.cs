using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_ILAsCourses : TrainingIssue_DataElement
    {
        public int? ILAId { get; set; }
        public ILA ILA { get; set; }
        public DataElement_ILAsCourses(int trainingIssueId, int? ilaId) : base(trainingIssueId)
        {
            ILAId = ilaId;
        }
        public DataElement_ILAsCourses()
        {

        }
    }
}
