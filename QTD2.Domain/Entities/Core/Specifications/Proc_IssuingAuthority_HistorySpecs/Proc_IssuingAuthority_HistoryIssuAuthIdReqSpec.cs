using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.IssuingAuthorityStatusHistorySpecs
{
    public class Proc_IssuingAuthority_HistoryIssuAuthIdReqSpec : ISpecification<Proc_IssuingAuthority_History>
    {
        public bool IsSatisfiedBy(Proc_IssuingAuthority_History entity, params object[] args)
        {
            return entity.ProcedureIssuingAuthorityId > 0;
        }
    }
}
