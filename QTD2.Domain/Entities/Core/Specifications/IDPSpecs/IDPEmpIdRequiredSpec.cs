using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Employee_ILASpecs
{
    public class IDPEmpIdRequiredSpec : ISpecification<IDP>
    {
        public bool IsSatisfiedBy(IDP entity, params object[] args)
        {
            return entity.EmployeeId > 0;
        }
    }
}
