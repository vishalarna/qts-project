using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RR_IssuingAuthority_StatusHistorySpecs
{
    public class RR_IssuingAuthority_StatusHistoryRRIAIdRequiredSpec : ISpecification<RR_IssuingAuthority_StatusHistory>
    {
        public bool IsSatisfiedBy(RR_IssuingAuthority_StatusHistory entity, params object[] args)
        {
            return entity.RRIssuingAuthorityId > 0;
        }
    }
}
