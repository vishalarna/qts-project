using System;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeePositionSpecs
{
    internal class EmpPosStartDateRequiredSpec : ISpecification<EmployeePosition>
    {
        public bool IsSatisfiedBy(EmployeePosition entity, params object[] args)
        {
            return entity.StartDate != DateOnly.MinValue;
        }
    }
}
