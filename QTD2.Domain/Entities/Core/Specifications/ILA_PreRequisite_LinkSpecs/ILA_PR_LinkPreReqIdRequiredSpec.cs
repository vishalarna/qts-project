using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_PreRequisite_LinkSpecs
{
    public class ILA_PR_LinkPreReqIdRequiredSpec : ISpecification<ILA_PreRequisite_Link>
    {
        public bool IsSatisfiedBy(ILA_PreRequisite_Link entity, params object[] args)
        {
            return entity.PreRequisiteId > 0;
        }
    }
}
