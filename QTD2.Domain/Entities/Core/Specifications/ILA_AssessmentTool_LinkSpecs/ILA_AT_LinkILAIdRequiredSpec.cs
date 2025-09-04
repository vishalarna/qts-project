using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_AssessmentTool_LinkSpecs
{
    public class ILA_AT_LinkILAIdRequiredSpec : ISpecification<ILA_AssessmentTool_Link>
    {
        public bool IsSatisfiedBy(ILA_AssessmentTool_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
