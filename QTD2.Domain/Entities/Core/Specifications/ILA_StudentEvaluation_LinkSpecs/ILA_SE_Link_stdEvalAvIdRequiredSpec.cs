using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_StudentEvaluation_LinkSpecs
{
    public class ILA_SE_Link_stdEvalAvIdRequiredSpec : ISpecification<ILA_StudentEvaluation_Link>
    {
        public bool IsSatisfiedBy(ILA_StudentEvaluation_Link entity, params object[] args)
        {
            return entity.studentEvalFormID > 0;
        }
    }
}
