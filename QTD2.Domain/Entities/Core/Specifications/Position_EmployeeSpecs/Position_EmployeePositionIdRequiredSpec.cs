using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Position_EmployeeSpecs
{
    public class Position_EmployeePositionIdRequiredSpec : ISpecification<Position_Employee>
    {
        public bool IsSatisfiedBy(Position_Employee entity, params object[] args)
        {
            return entity.PositionId > 0;
        }
    }
}
