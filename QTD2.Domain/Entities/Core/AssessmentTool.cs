using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class AssessmentTool : Entity
    {
        public string Name { get; set; }

        public AssessmentTool(string name)
        {
            Name = name;
        }

        public virtual ICollection<ILA_AssessmentTool_Link> ILA_AssessmentTool_Links { get; set; } = new List<ILA_AssessmentTool_Link>();

        public AssessmentTool()
        {
        }
    }
}
