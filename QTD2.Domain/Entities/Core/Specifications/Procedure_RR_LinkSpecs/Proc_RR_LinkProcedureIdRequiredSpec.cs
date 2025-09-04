using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Procedure_RegRequirement_LinkSpecs
{
    internal class Proc_RR_LinkProcedureIdRequiredSpec : Interfaces.Specification.ISpecification<Procedure_RR_Link>
    {
        public bool IsSatisfiedBy(Procedure_RR_Link entity, params object[] args)
        {
            return entity.ProcedureId > 0;
        }
    }
}
