using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.NercStandardMemberSpecs
{
    public class NercStandardMemberStdIdRequiredSpec : ISpecification<NercStandardMember>
    {
        public bool IsSatisfiedBy(NercStandardMember entity, params object[] args)
        {
            return entity.StdId > 0;
        }
    }
}
