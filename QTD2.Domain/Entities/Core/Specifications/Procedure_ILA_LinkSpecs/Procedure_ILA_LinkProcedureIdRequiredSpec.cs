using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Procedure_ILA_LinkSpecs
{
    public class Procedure_ILA_LinkProcedureIdRequiredSpec : ISpecification<Procedure_ILA_Link>
    {
        public bool IsSatisfiedBy(Procedure_ILA_Link entity, params object[] args)
        {
            return entity.ProcedureId > 0;
        }
    }
}
