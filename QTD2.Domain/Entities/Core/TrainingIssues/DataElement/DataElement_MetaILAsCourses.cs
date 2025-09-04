using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_MetaILAsCourses : TrainingIssue_DataElement
    {
        public int? MetaILAId { get; set; }
        public MetaILA MetaILA { get; set; }
        public DataElement_MetaILAsCourses(int trainingIssueId, int? metaILAId) : base(trainingIssueId)
        {
            MetaILAId = metaILAId;
        }
        public DataElement_MetaILAsCourses()
        {

        }
    }
}
