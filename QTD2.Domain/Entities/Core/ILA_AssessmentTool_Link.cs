using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_AssessmentTool_Link : Entity
    {
        public int ILAId { get; set; }

        public int AssessmentToolId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual AssessmentTool AssessmentTool { get; set; }

        public ILA_AssessmentTool_Link(ILA ila, AssessmentTool assessmentTool)
        {
            ILAId = ila.Id;
            AssessmentToolId = assessmentTool.Id;
            ILA = ila;
            AssessmentTool = assessmentTool;
        }

        public ILA_AssessmentTool_Link()
        {
        }
    }
}
