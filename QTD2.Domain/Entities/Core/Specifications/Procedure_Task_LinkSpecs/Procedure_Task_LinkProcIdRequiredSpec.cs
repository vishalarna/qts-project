using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Procedure_Task_LinkSpecs
{
    public class Procedure_Task_LinkProcIdRequiredSpec : Interfaces.Specification.ISpecification<Procedure_Task_Link>
    {
        public bool IsSatisfiedBy(Procedure_Task_Link entity, params object[] args)
        {
            return entity.ProcedureId > 0;
        }
    }
}
